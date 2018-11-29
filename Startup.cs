using System;
using System.Text;
using System.Reflection;
using System.IO;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using EACA_API.Data;
using EACA_API.Models.Account;
using EACA_API.Models;
using EACA_API.Services;
using EACA_API.Controllers.ScheduleApi.Services;

using FluentValidation.AspNetCore;
using AutoMapper;
using Swashbuckle.AspNetCore.Swagger;
using EACA_API.Models.Email;
using EACA_API.Services.EmailSender;

namespace EACA_API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private readonly string SecretKey;
        private readonly SymmetricSecurityKey _signingKey;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            SecretKey = Configuration.GetSection("JwtSecretKey")["SecretKey"];
            _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AzureConnection")));

            // CORS
            services.AddCors(options => options.AddPolicy("AllowAllOrigin", x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
            services.Configure<MvcOptions>(options => options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAllOrigin")));

            // DI Custom
            services.AddSingleton<IScheduleService, ScheduleService>();
            services.TryAddSingleton<IItemRepository, ItemRepository>();
            services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();

            // Email Service 
            var emailAppSettingOptions = Configuration.GetSection(nameof(EmailOptions));
            services.Configure<EmailOptions>(options =>
            {
                options.Login = emailAppSettingOptions[nameof(EmailOptions.Login)];
                options.Password = emailAppSettingOptions[nameof(EmailOptions.Password)];
                options.Host = emailAppSettingOptions[nameof(EmailOptions.Host)];
                options.Port = emailAppSettingOptions.GetValue<int>(nameof(EmailOptions.Port));
                options.SSL = emailAppSettingOptions.GetValue<bool>(nameof(EmailOptions.SSL));
            });

            services.AddTransient<IEmailSender, EmailSender>();

            // JSON Web Token Authentication
            JWTServices(services);

            var builder = services.AddIdentityCore<ApiUser>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddRoleManager<RoleManager<IdentityRole>>();
            builder.AddEntityFrameworkStores<ApplicationDbContext>();
            builder.AddDefaultTokenProviders();

            services.AddSingleton<IJwtFactory, JwtFactory>();

            // AutoMapper
            services.AddAutoMapper();

            // MVC Config
            services.AddMvc()
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "EACA API",
                    Version = "v1",
                    Description = "WebApi для приложении ЕАСИ",
                    Contact = new Contact
                    {
                        Name = "GitHub",
                        Url = "https://github.com/Eqip3u/EACA-API"
                    },
                });
                //xml enable
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseAuthentication();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EACA API V1"));

            app.UseMvc();

        }

        private void JWTServices(IServiceCollection services)
        {
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(configureOptions =>
                {
                    configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                    configureOptions.TokenValidationParameters = tokenValidationParameters;
                    configureOptions.SaveToken = true;
                });
        }
    }
}

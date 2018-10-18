using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using EACA_API.Auth;
using EACA_API.Helpers;
using EACA_API.Models;
using EACA_API.Models.Entities;
using EACA_API.ViewModels;

using Newtonsoft.Json;


namespace EACA_API.Controllers.Account
{
    [EnableCors("AllowAllOrigin")]
    [Route("api/accounts/[controller]")]
    public class LoginController : Controller
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;

        public LoginController(UserManager<ApiUser> userManager, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
        }

        // POST api/accounts/login/
        public async Task<IActionResult> Post([FromBody]CredentialsViewModel credentials)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var identity = await GetClaimsIdentity(credentials.UserName, credentials.Password);

            if (identity == null)
                return BadRequest(Errors.AddErrorToModelState("login_failure", "Неправильный логин или пароль.", ModelState));

            var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, credentials.UserName, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return Ok(jwt);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            var userToVerify = await _userManager.FindByNameAsync(userName);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            if (await _userManager.CheckPasswordAsync(userToVerify, password))
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));

            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}
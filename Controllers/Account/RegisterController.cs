using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using EACA_API.Data;
using EACA_API.ViewModels;

using AutoMapper;
using EACA_API.Helpers;
using EACA_API.Models.Account;
using Microsoft.EntityFrameworkCore;
using EACA_API.Models.Institute;
using EACA_API.Services.EmailSender;
using EACA_API.Extensions;
using System;

namespace EACA_API.Controllers.Account
{
    [Produces("application/json")]
    [Route("api/account/[controller]/[action]")]
    public partial class RegisterController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApiUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;

        public RegisterController(UserManager<ApiUser> userManager, IMapper mapper, ApplicationDbContext context, IEmailSender emailSender)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
            _emailSender = emailSender;
        }

        /// <summary>
        /// Метод регистрации в веб-апи
        /// </summary>
        /// <param name="model">Реквизиты для регистрации пользователя</param>
        /// <response code="200">Возвращает сообщение об успешной регистрации</response>
        /// <response code="400">Возвращает файл json с некоторой информацией об ошибках</response>
        /// <returns>Возвращает статус код с сообщением</returns>
        // POST api/account/register/student
        [HttpPost]
        public async Task<IActionResult> Student([FromBody]RegistrationStudentViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userIdentity = _mapper.Map<ApiUser>(model);

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded)
                return BadRequest(Errors.AddIdentityErrorsToModelState(result, ModelState));

            var group = await _context.Groups.SingleAsync(x => x.Number == model.Group);

            if (group == null)
                return BadRequest(Errors.AddErrorToModelState("group", "такой группы не существует", ModelState));

            await _userManager.AddToRoleAsync(userIdentity, Constants.Jwt.JwtRoles.ApiAccessStudent);

            await _context.Students.AddAsync(new Student { ApiUserId = userIdentity.Id});
            await _context.SaveChangesAsync();

            var student = await _context.Students.SingleAsync(x => x.ApiUserId == userIdentity.Id);
            await _context.StudentGroups.AddAsync(new StudentGroup { StudentId = student.Id, GroupId = group.Id, Gradebook = model.Gradebook });
            await _context.SaveChangesAsync();

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(userIdentity);
            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = userIdentity.Id, code }, protocol: HttpContext.Request.Scheme);
            await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

            return Ok($"Аккаунт {model.Email} успешно создан!");
        }
    }
}

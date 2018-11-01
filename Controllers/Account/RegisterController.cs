using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using EACA_API.Data;
using EACA_API.ViewModels;

using AutoMapper;
using EACA_API.Helpers;
using EACA_API.Models.Account;

namespace EACA_API.Controllers.Account
{
    [Produces("application/json")]
    [Route("api/account/[controller]/[action]")]
    public partial class RegisterController : Controller
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly UserManager<ApiUser> _userManager;
        private readonly IMapper _mapper;

        public RegisterController(UserManager<ApiUser> userManager, IMapper mapper, ApplicationDbContext appDbContext)
        {
            _userManager = userManager;
            _mapper = mapper;
            _appDbContext = appDbContext;
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
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userIdentity = _mapper.Map<ApiUser>(model);

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded) return BadRequest(Errors.AddIdentityErrorsToModelState(result, ModelState));

            await _userManager.AddToRoleAsync(userIdentity, Constants.Jwt.JwtRoles.ApiAccessStudent);

            await _appDbContext.Students.AddAsync(
                new Student { IdentityId = userIdentity.Id, Group = model.Group, Gradebook = model.Gradebook, Headman = model.Headman });
            await _appDbContext.SaveChangesAsync();

            return Ok($"Аккаунт {model.Email} успешно создан!");
        }

        // POST api/account/register/instructor
        [HttpPost]
        public async Task<IActionResult> Instructor([FromBody]RegistrationInstructorViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userIdentity = _mapper.Map<ApiUser>(model);

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded) return BadRequest(Errors.AddIdentityErrorsToModelState(result, ModelState));

            await _userManager.AddToRoleAsync(userIdentity, Constants.Jwt.JwtRoles.ApiAccessInstructor);

            await _appDbContext.Instructors.AddAsync(new Instructor { IdentityId = userIdentity.Id });
            await _appDbContext.SaveChangesAsync();

            return Ok($"Аккаунт {model.Email} успешно создан!");
        }
    }
}

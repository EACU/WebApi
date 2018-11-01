using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using EACA_API.Models;
using EACA_API.ViewModels;
using EACA_API.Data;
using EACA_API.Services;
using EACA_API.Models.Account;

namespace EACA_API.Controllers.Account
{
    [Route("api/account/[controller]")]
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApiUser> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;

        public LoginController(ApplicationDbContext context, UserManager<ApiUser> userManager, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _context = context;
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
        }

        /// <summary>
        /// Метод авторизации в веб-апи
        /// </summary>
        /// <param name="credentials">Реквизиты для доступа к аккаунту веб-апи</param>
        /// <response code="200">Возвращает идентификатор пользователя(id), jwt-токен и время истечения токена</response>
        /// <response code="400">Возвращает файл json с некоторой информацией об ошибках</response>
        /// <returns>Возвращает jwt токен</returns>
        // POST api/account/login/
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CredentialsViewModel credentials)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var identity = await Helpers.Claims.GetClaimsIdentity(_userManager, _jwtFactory, credentials.UserName, credentials.Password);

            if (identity == null)
                return BadRequest(Helpers.Errors.AddErrorToModelState("login_failure", "Неправильный логин или пароль.", ModelState));

            var jwt = await Helpers.Tokens.GenerateJwt(_context, identity, _jwtFactory, credentials.UserName, _jwtOptions);
            return Ok(jwt);
        }
    }
}
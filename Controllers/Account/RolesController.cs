using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using EACA_API.Data;
using EACA_API.Helpers;
using EACA_API.Models.Account;

namespace EACA_API.Controllers.Account.Roles
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(Roles = "api_access_admin")]
    [Route("api/accounts/[controller]/[action]")]
    public class RolesController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<ApiUser> _userManager;
        ApplicationDbContext _appDbContext;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApiUser> userManager, ApplicationDbContext appDbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public IActionResult AllRoles() => Ok(_roleManager.Roles.ToList());

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest(Errors.AddErrorToModelState("roles_errors", "Пустая строка", ModelState));

            if (await _roleManager.RoleExistsAsync(name))
                return BadRequest(Errors.AddErrorToModelState("roles_errors", "Роль с таким именем уже существует", ModelState));

            IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
            return Ok($"Роль {name} успешно созданна");
        }

        [HttpPost]
        public async Task<IActionResult> UserRoles([FromBody]ApiUser requestUser)
        {
            ApiUser user = await _userManager.FindByIdAsync(requestUser.Id);

            if (user == null)
                return BadRequest(Errors.AddErrorToModelState("roles_errors", "Такого пользователя не существует", ModelState));

            var userRoles = await _userManager.GetRolesAsync(user);

            return Ok(new { userId = user.Id, userRoles });
         }

        [HttpPost]
        public async Task<IActionResult> AddAdministatorRole([FromBody]ApiUser requestUser)
        {
            ApiUser user = await _userManager.FindByIdAsync(requestUser.Id);

            if (user == null)
                return BadRequest(Errors.AddErrorToModelState("roles_errors", "Такого пользователя не существует", ModelState));

            var userRoles = await _userManager.GetRolesAsync(user);

            if (userRoles.Contains(Constants.Jwt.JwtRoles.ApiAccessAdmin))
                return BadRequest(Errors.AddErrorToModelState("roles_errors", $"У пользователя: {user.UserName} уже есть права администратора", ModelState));

            await _userManager.AddToRoleAsync(user, Constants.Jwt.JwtRoles.ApiAccessAdmin);

            await _appDbContext.Admins.AddAsync(new Admin { IdentityId = requestUser.Id });
            await _appDbContext.SaveChangesAsync();

            return Ok($"Пользователь: {user.UserName} получил права администратора");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAdministatorRole([FromBody]ApiUser requestUser)
        {
            ApiUser user = await _userManager.FindByIdAsync(requestUser.Id);

            if (user == null)
                return BadRequest(Errors.AddErrorToModelState("roles_errors", "Такого пользователя не существует", ModelState));

            Admin userAdmin = await _appDbContext.Admins.SingleOrDefaultAsync(x => x.IdentityId == requestUser.Id);

            if (userAdmin == null)
                return BadRequest(Errors.AddErrorToModelState("roles_errors", $"У пользователя: {user.UserName} нет прав администратора", ModelState));

            _appDbContext.Admins.Remove(userAdmin);
            await _appDbContext.SaveChangesAsync();

            await _userManager.RemoveFromRoleAsync(user, Constants.Jwt.JwtRoles.ApiAccessAdmin);

            return Ok($"У пользователя: {user.UserName} удалены права администратора");
        }

        [HttpPost]
        public async Task<IActionResult> AddStudentRole([FromBody]ApiUser requestUser)
        {
            ApiUser user = await _userManager.FindByIdAsync(requestUser.Id);

            if (user == null)
                return BadRequest(Errors.AddErrorToModelState("roles_errors", "Такого пользователя не существует", ModelState));

            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Contains(Constants.Jwt.JwtRoles.ApiAccessStudent))
                return BadRequest(Errors.AddErrorToModelState("roles_errors", $"У пользователя: {user.UserName} уже есть права студента", ModelState));

            await _userManager.AddToRoleAsync(user, Constants.Jwt.JwtRoles.ApiAccessStudent);

            await _appDbContext.Students.AddAsync(new Student { IdentityId = requestUser.Id });
            await _appDbContext.SaveChangesAsync();

            return Ok($"Пользователь: {user.UserName} получил права студента");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteStudentRole([FromBody]ApiUser requestUser)
        {
            ApiUser user = await _userManager.FindByIdAsync(requestUser.Id);

            if (user == null)
                return BadRequest(Errors.AddErrorToModelState("roles_errors", "Такого пользователя не существует", ModelState));

            Student userStudent = await _appDbContext.Students.SingleOrDefaultAsync(x => x.IdentityId == requestUser.Id);

            if (userStudent == null)
                return BadRequest(Errors.AddErrorToModelState("roles_errors", $"У пользователя: {user.UserName} нет прав студента", ModelState));

            _appDbContext.Students.Remove(userStudent);
            await _appDbContext.SaveChangesAsync();

            await _userManager.RemoveFromRoleAsync(user, Constants.Jwt.JwtRoles.ApiAccessStudent);

            return Ok($"У пользователя: {user.UserName} удалены права студента");
        }
    }
}
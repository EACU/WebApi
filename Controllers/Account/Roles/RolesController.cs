using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EACA_API.Models.Entities;
using EACA_API.Data;
using Microsoft.EntityFrameworkCore;

namespace EACA_API.Controllers.Account.Roles
{
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
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                return Ok($"Роль {name} успешно созданна");
            }
            return BadRequest("Пустая строка");
        }

        [HttpPost]
        public async Task<IActionResult> UserRoles(string userId)
        {
            ApiUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                return Ok(new { userId, userRoles });
            }
            return BadRequest("Такого пользователя не существует");
        }

        [HttpPost]
        public async Task<IActionResult> AddAdministatorRole(string userId)
        {
            ApiUser user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return BadRequest("Такого пользователя не существует");

            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Contains(Helpers.Constants.Strings.JwtClaims.ApiAccessAdmin))
                return BadRequest($"У пользователя: {user.UserName} уже есть права администратора");

            await _userManager.AddToRoleAsync(user, Helpers.Constants.Strings.JwtClaims.ApiAccessAdmin);

            await _appDbContext.Admins.AddAsync(new Admin { IdentityId = userId });
            await _appDbContext.SaveChangesAsync();

            return Ok($"Пользователь: {user.UserName} получил права администратора");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAdministatorRole(string userId)
        {
            ApiUser user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return BadRequest("Такого пользователя не существует");

            Admin userAdmin = await _appDbContext.Admins.SingleOrDefaultAsync(x => x.IdentityId == userId);

            if (userAdmin == null)
                return BadRequest($"У пользователя: {user.UserName} нет прав администратора");

            _appDbContext.Admins.Remove(userAdmin);
            await _appDbContext.SaveChangesAsync();

            await _userManager.RemoveFromRoleAsync(user, Helpers.Constants.Strings.JwtClaims.ApiAccessAdmin);

            return Ok($"У пользователя: {user.UserName} удалены права администратора");
        }

        [HttpPost]
        public async Task<IActionResult> AddStudentRole(string userId)
        {
            ApiUser user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return BadRequest("Такого пользователя не существует");

            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Contains(Helpers.Constants.Strings.JwtClaims.ApiAccessStudent))
                return BadRequest($"У пользователя: {user.UserName} уже есть права студента");

            await _userManager.AddToRoleAsync(user, Helpers.Constants.Strings.JwtClaims.ApiAccessStudent);

            await _appDbContext.Students.AddAsync(new Student { IdentityId = userId });
            await _appDbContext.SaveChangesAsync();

            return Ok($"Пользователь: {user.UserName} получил права студента");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteStudentRole(string userId)
        {
            ApiUser user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return BadRequest("Такого пользователя не существует");

            Student userStudent = await _appDbContext.Students.SingleOrDefaultAsync(x => x.IdentityId == userId);

            if (userStudent == null)
                return BadRequest($"У пользователя: {user.UserName} нет прав студента");

            _appDbContext.Students.Remove(userStudent);
            await _appDbContext.SaveChangesAsync();

            await _userManager.RemoveFromRoleAsync(user, Helpers.Constants.Strings.JwtClaims.ApiAccessStudent);

            return Ok($"У пользователя: {user.UserName} удалены права студента");
        }
    }
}
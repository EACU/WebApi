using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EACA_API.Models.Entities;

namespace EACA_API.Controllers.Account
{
    [Route("api/accounts/[controller]/[action]")]
    public class RolesController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<ApiUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApiUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
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
        public async Task<IActionResult> AddUserRole(string userId, string role)
        {
            ApiUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, role);
                return Ok($"К юзеру: {user.UserName} успешно добавлена роль: {role}");
            }
            return BadRequest("Такого пользователя не существует");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUserRole(string userId, string role)
        {
            ApiUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _userManager.RemoveFromRoleAsync(user, role);
                return Ok($"У юзера: {user.UserName} успешно удалена роль: {role}");
            }
            return BadRequest("Такого пользователя не существует");
        }
    }
}
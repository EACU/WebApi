using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EACA_API.Data;
using EACA_API.Helpers;
using EACA_API.Models.Account;
using EACA_API.ViewModels.Accounts.RolesViewModels;
using Microsoft.EntityFrameworkCore;

namespace EACA_API.Controllers.Account.Roles
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/account/[controller]/[action]")]
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

            var result = await _roleManager.CreateAsync(new IdentityRole(name));
            if (!result.Succeeded)
                return BadRequest(Errors.AddIdentityErrorsToModelState(result, ModelState));

            return Ok($"Роль {name} успешно созданна");
        }

        [HttpPost]
        public async Task<IActionResult> UserRoles([FromBody]UserRoleViewModel body)
        {
            var user = await _userManager.FindByIdAsync(body.Id);

            if (user == null)
                return BadRequest(Errors.AddErrorToModelState("roles_errors", "Такого пользователя не существует", ModelState));

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new { roles });
         }

        [HttpPost]
        public async Task<IActionResult> AddRoleUser([FromBody]UserRoleViewModel body)
        {
            var user = await _userManager.FindByIdAsync(body.Id);
            if (user == null)
                return BadRequest(Errors.AddErrorToModelState("roles_errors", "Такого пользователя не существует", ModelState));

            if (!await _roleManager.RoleExistsAsync(body.Role))
                return BadRequest(Errors.AddErrorToModelState("roles_errors", $"Роли: '{body.Role}' - не существует", ModelState));

            if (await _userManager.IsInRoleAsync(user, body.Role))
                return BadRequest(Errors.AddErrorToModelState("roles_errors", $"Пользователь: '{body.Id}' уже имеет эту роль: {body.Role}", ModelState));

            var result = await _userManager.AddToRoleAsync(user, body.Role);
            if (!result.Succeeded)
                return BadRequest(Errors.AddIdentityErrorsToModelState(result, ModelState));

            switch (body.Role)
            {
                case Constants.Jwt.JwtRoles.ApiAccessAdmin:
                    await _appDbContext.Admins.AddAsync(new Admin { IdentityId = user.Id });
                    break;

                case Constants.Jwt.JwtRoles.ApiAccessInstructor:
                    await _appDbContext.Instructors.AddAsync(new Instructor { IdentityId = user.Id });
                    break;

                case Constants.Jwt.JwtRoles.ApiAccessStudent:
                    await _appDbContext.Students.AddAsync(new Student { ApiUserId = user.Id });
                    break;
            }
            await _appDbContext.SaveChangesAsync();

            return Ok($"Пользователь: '{body.Id}' получил роль: '{body.Role}'");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRoleUser([FromBody]UserRoleViewModel body)
        {
            var user = await _userManager.FindByIdAsync(body.Id);
            if (user == null)
                return BadRequest(Errors.AddErrorToModelState("roles_errors", "Такого пользователя не существует", ModelState));

            if (!await _roleManager.RoleExistsAsync(body.Role))
                return BadRequest(Errors.AddErrorToModelState("roles_errors", $"Роли: '{body.Role}' - не существует", ModelState));

            if (!await _userManager.IsInRoleAsync(user, body.Role))
                return BadRequest(Errors.AddErrorToModelState("roles_errors", $"Пользователь: '{body.Id}' не имеет роли: {body.Role}", ModelState));

            var result = await _userManager.RemoveFromRoleAsync(user, body.Role);
            if (!result.Succeeded)
                return BadRequest(Errors.AddIdentityErrorsToModelState(result, ModelState));

            switch (body.Role)
            {
                case Constants.Jwt.JwtRoles.ApiAccessAdmin:
                    var adminAccount = await _appDbContext.Admins.SingleOrDefaultAsync(x => x.IdentityId == body.Id);
                    _appDbContext.Admins.Remove(adminAccount);
                    break;

                case Constants.Jwt.JwtRoles.ApiAccessInstructor:
                    var instructorAccount = await _appDbContext.Instructors.SingleOrDefaultAsync(x => x.IdentityId == body.Id);
                    _appDbContext.Instructors.Remove(instructorAccount);
                    break;

                case Constants.Jwt.JwtRoles.ApiAccessStudent:
                    var studentAccount = await _appDbContext.Students.SingleOrDefaultAsync(x => x.ApiUserId == body.Id);
                    _appDbContext.Students.Remove(studentAccount);
                    break;
            }
            await _appDbContext.SaveChangesAsync();

            return Ok($"У пользователя: '{body.Id}' успешно удалена роль: '{body.Role}'");
        }
    }
}
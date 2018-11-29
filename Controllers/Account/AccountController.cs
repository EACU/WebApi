using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EACA_API.Models.Account;

namespace EACA_API.Controllers.Account
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApiUser> _userManager;

        public AccountController(UserManager<ApiUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
                return View("Error");

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return View("Error");

            var result = await _userManager.ConfirmEmailAsync(user, code);

            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }
    }
}
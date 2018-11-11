using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EACA_API.Data;
using Microsoft.AspNetCore.Identity;
using EACA_API.Models.Account;
using EACA_API.ViewModels.Accounts;

namespace EACA_API.Controllers.Account
{
    [Authorize]
    [Route("api/account/[controller]")]
    public class InformationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApiUser> _userManager;
        private readonly ClaimsPrincipal _caller;

        public InformationController(ApplicationDbContext appDbContext, UserManager<ApiUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = appDbContext;
            _userManager = userManager;
            _caller = httpContextAccessor.HttpContext.User;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var user = await GetApiUser();

            UserInformationViewModel userViewModel = await GenerateUserInformationViewModel(user);

            return Ok(userViewModel);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UserInformationViewModel body)
        {
            var user = await GetApiUser();

            user.FirstName = body.FirstName;
            user.LastName = body.LastName;
            user.PhoneNumber = body.PhoneNumber;
            user.PictureUrl = body.PictureUrl;

            _context.Users.Update(user);
            _context.SaveChanges();

            UserInformationViewModel userViewModel = await GenerateUserInformationViewModel(user);

            return Ok(userViewModel);
        }

        private async Task<ApiUser> GetApiUser()
        {
            var userId = _caller.Claims.Single(c => c.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            return await _context.Users.SingleAsync(x => x.Id == userId);
        }

        private async Task<UserInformationViewModel> GenerateUserInformationViewModel(ApiUser user)
        {
            return new UserInformationViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                PictureUrl = user.PictureUrl,
                Roles = await _userManager.GetRolesAsync(user)
            };
        }
    }
}
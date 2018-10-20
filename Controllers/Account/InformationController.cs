using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EACA_API.Data;

namespace EACA_API.Controllers.Account
{
    [Authorize]
    [Route("api/accounts/[controller]")]
    public class InformationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ClaimsPrincipal _caller;

        public InformationController(ApplicationDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _context = appDbContext;
            _caller = httpContextAccessor.HttpContext.User;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = _caller.Claims.Single(c => c.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            var user = await _context.Users.SingleAsync(x => x.Id == userId);

            return Ok(new { user.FirstName, user.LastName, user.PictureUrl });
        }
    }
}
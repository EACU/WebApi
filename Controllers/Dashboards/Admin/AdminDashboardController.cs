using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EACA_API.Data;

namespace EACA_API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(Roles = "api_access_admin")]
    [Route("api/[controller]/[action]")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ClaimsPrincipal _caller;

        public AdminController(ApplicationDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _context = appDbContext;
            _caller = httpContextAccessor.HttpContext.User;
        }

        // GET /api/admin/dashboard/
        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var userId = _caller.Claims.Single(c => c.Type == ClaimsIdentity.DefaultNameClaimType);
            var admin = await _context.Admins.Include(c => c.Identity).SingleAsync(c => c.Identity.Id == userId.Value);
            
            return Ok(new { admin.Identity.FirstName, admin.Identity.LastName, admin.Identity.PictureUrl, admin.Passport});
        }
    }
}
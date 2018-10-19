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
    [Authorize(Roles = "api_access_student")]
    [Route("api/[controller]")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ClaimsPrincipal _caller;

        public DashboardController(ApplicationDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _context = appDbContext;
            _caller = httpContextAccessor.HttpContext.User;
        }

        // GET /api/dashboard/
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = _caller.Claims.Single(c => c.Type == ClaimsIdentity.DefaultNameClaimType);
            var student = await _context.Students.Include(c => c.Identity).SingleAsync(c => c.Identity.Id == userId.Value);
            
            return Ok(new { student.Identity.FirstName, student.Identity.LastName, student.Identity.PictureUrl});
        }
    }
}
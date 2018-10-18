using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EACA_API.Data;
using EACA_API.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EACA_API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(Policy = nameof(ApiUser))]
    [Route("api/[controller]/[action]")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ClaimsPrincipal _caller;

        public DashboardController(ApplicationDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _context = appDbContext;
            _caller = httpContextAccessor.HttpContext.User;
        }

        [HttpGet]
        public IActionResult Index() => View();

        // GET /api/dashboard/home
        [HttpGet]
        public async Task<IActionResult> Home()
        {
            var userId = _caller.Claims.Single(c => c.Type == "id");
            var student = await _context.Students.Include(c => c.Identity).SingleAsync(c => c.Identity.Id == userId.Value);
            
            return Ok(new { student.Identity.FirstName, student.Identity.LastName, student.Identity.PictureUrl, student.Group });
        }
    }
}
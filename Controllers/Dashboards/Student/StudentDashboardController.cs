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
    [Authorize(Roles = "api_access_student")]
    [Route("api/[controller]/[action]")]
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ClaimsPrincipal _caller;

        public StudentController(ApplicationDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _context = appDbContext;
            _caller = httpContextAccessor.HttpContext.User;
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var userId = _caller.Claims.Single(c => c.Type == ClaimsIdentity.DefaultNameClaimType);

            var student = await _context.Students.Include(c => c.ApiUser).Include(x => x.StudentGroups).ThenInclude(x => x.Group).SingleAsync(c => c.ApiUser.Id == userId.Value);

            return Ok(student);
        }
    }
}
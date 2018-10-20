using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EACA_API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(Roles = "api_access_student, api_access_admin")]
    [Route("api/[controller]")]
    public class ClaimsController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View();
    }
}
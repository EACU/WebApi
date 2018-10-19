using Microsoft.AspNetCore.Mvc;

namespace EACA_API.Controllers
{
    [Route("api/[controller]")]
    public class ClaimsController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View();
    }
}
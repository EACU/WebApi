using Microsoft.AspNetCore.Mvc;

namespace EACA_API.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
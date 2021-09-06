using Microsoft.AspNetCore.Mvc;

namespace WelcomeApp.Controllers
{
    public class WelcomeController : Controller
    {
        public WelcomeController() {}

        public IActionResult Index()
        {
            return View();
        }
    }
}

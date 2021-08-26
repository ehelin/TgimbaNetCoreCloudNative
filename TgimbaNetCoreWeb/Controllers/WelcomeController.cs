using Microsoft.AspNetCore.Mvc;
using Shared.dto;
using TgimbaNetCoreWebShared;
using TgimbaNetCoreWebShared.Controllers;

namespace TgimbaNetCoreWeb.Controllers
{
//#if !DEBUG
//    [RequireHttpsAttribute]
//#endif
    public class WelcomeController : Controller
    {
		private SharedWelcomeController sharedWelcomeController = null;

        public WelcomeController(IWebClient webClient)
        {
            System.Console.WriteLine("WelcomeController(arg)");

            sharedWelcomeController = new SharedWelcomeController(webClient);
		}

        public IActionResult Index()
        {
            System.Console.WriteLine("WelcomeController-Index()");

            return View();
        }
       
        [HttpGet]
        public SystemStatistics GetSystemStatistics()
        {
            var systemStatistics = new SystemStatistics();

            System.Console.WriteLine("WelcomeController-GetSystemStatistics()");

            systemStatistics.SystemStats = this.sharedWelcomeController.webClient.GetSystemStatistics();
            //systemStatistics.SystemBuildStats = this.sharedWelcomeController.webClient.GetSystemBuildStatistics();

            return systemStatistics;
        }
    }
}
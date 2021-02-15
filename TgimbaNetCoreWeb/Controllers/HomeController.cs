using Microsoft.AspNetCore.Mvc;						  

namespace TgimbaNetCoreWeb.Controllers
{				 
    #if !DEBUG
    [RequireHttpsAttribute]
    #endif
    public class HomeController : Controller
    {                    
        public IActionResult HtmlVanillaJsIndex()
        {
            return View();
        }
	
		public IActionResult Index() 
		{
			return View();
		}
    }
}

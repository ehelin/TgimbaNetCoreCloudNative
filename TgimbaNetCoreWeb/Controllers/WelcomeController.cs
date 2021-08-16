﻿using Microsoft.AspNetCore.Mvc;
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
            System.Console.WriteLine("Console-WelcomeController(arg)");
            System.Diagnostics.Debug.WriteLine("Debug-WelcomeController(arg)");
            System.Diagnostics.Trace.WriteLine("Trace-WelcomeController(arg)");

            sharedWelcomeController = new SharedWelcomeController(webClient);
		}

        public IActionResult Index()
        {
            System.Console.WriteLine("Console-WelcomeController-Index()");
            System.Diagnostics.Debug.WriteLine("Debug-WelcomeController-Index()");
            System.Diagnostics.Trace.WriteLine("Trace-WelcomeController-Index()");

            return View();
        }
       
        [HttpGet]
        public SystemStatistics GetSystemStatistics()
        {
            var systemStatistics = new SystemStatistics();

            System.Console.WriteLine("Console-WelcomeController-GetSystemStatistics()");
            System.Diagnostics.Debug.WriteLine("Debug-WelcomeController-GetSystemStatistics()");
            System.Diagnostics.Trace.WriteLine("Trace-WelcomeController-GetSystemStatistics()");

            systemStatistics.SystemStats = this.sharedWelcomeController.webClient.GetSystemStatistics();
            //systemStatistics.SystemBuildStats = this.sharedWelcomeController.webClient.GetSystemBuildStatistics();

            return systemStatistics;
        }
    }
}
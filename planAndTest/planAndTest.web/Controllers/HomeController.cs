using System;
using System.Diagnostics;
using System.Threading;
using callMission;
using commonLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using planAndTest.web.Helper;
using planAndTest.web.Models;

namespace planAndTest.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return RedirectToAction("Articles", "SA");
            return View();
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult SystemAnalysis()
        {
            return View();
        }
        public IActionResult SystemDesign()
        {
            return View();
        }

    }
}

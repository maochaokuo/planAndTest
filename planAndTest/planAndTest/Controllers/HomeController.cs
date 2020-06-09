using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace planAndTest.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        public HomeController()//ILogger<HomeController> logger)
        {
            //_logger = logger;
        }
        public ActionResult Index()
        {
            //return RedirectToAction("Articles", "SA");
            return View();
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public ActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
        public ActionResult SystemAnalysis()
        {
            return View();
        }
        public ActionResult SystemDesign()
        {
            return View();
        }

    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using callMission;
using commonLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using models.calls;
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
            callExe ce = new callExe();
            //clsHelloTest cht = new clsHelloTest();
            //cht.callPara = "(I am home/index)";
            //string json = jsonUtl.encodeJson(cht);
            //string err = ce.MakeAcall(reflectionUtl.TypeName<clsHelloTest>()
            //    , json);

            clsMainLoop cml = new clsMainLoop();
            clsMainLoopInput cmli = new clsMainLoopInput();
            cmli.serviceName = "";
            cmli.callTs = "";
            string json = jsonUtl.encodeJson(cmli);
            cml.callPara = json;
            string err = ce.MakeAcall(reflectionUtl.TypeName<
                clsMainLoop>(), json);
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

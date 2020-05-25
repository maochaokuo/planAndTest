using System;
using System.Diagnostics;
using System.Threading;
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
        private void testCall()
        {
            remoteCallBridge rcb = new remoteCallBridge();
            string returnJson = "";
            string ret = "";
            //ret = rcb.instantCall("", "srvHelloTest", "doCall"
            //    , "paraJson", out returnJson);
            //Thread.Sleep(0);
            ret = rcb.persistentCall("", "srvHelloTest", "doCall"
                , "string", "paraJson", "string", out returnJson);
            Thread.Sleep(0);

            //remoteCallHelper hch = new remoteCallHelper();
            //string err = hch.checkMainLoop();
            //// 若失敗，activate console
            //if (err.Length > 0)
            //    err = hch.runNewMainLoop();
            //if (err.Length > 0)
            //    throw new Exception(
            //        "remote call engine failed");
            //string echoCallId = "";
            //err = hch.makeOneCall("srvMainLoop", "echo",
            //    out echoCallId);
            //if (err.Length > 0)
            //    throw new Exception("echo main engin failed");

            //clsHelloTest cht = new clsHelloTest();
            //cht.callPara = "(I am home/index)";
            //string json = jsonUtl.encodeJson(cht);
            //string err = ce.MakeAcall(reflectionUtl.TypeName<clsHelloTest>()
            //    , json);

        }
        public IActionResult Index()
        {
            testCall();
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

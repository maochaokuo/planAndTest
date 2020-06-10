using callMission;
using commonLib;
//using Microsoft.AspNetCore.Mvc;
using modelsfwk.calls;
using planAndTest.Models.EPL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace planAndTest.Controllers
{
    public class EplController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Action1()
        {
            EplAction1vm vm = new EplAction1vm();
            return View(vm);
        }

        [HttpPost]
        public ActionResult Action1(EplAction1vm vm)// firstName, string lastName)
        {
            vm.cmdTxt = vm.cmd;
            callExe ce = new callExe();
            string callId = callExe.genCallId();
            clsHelloTest cht = new clsHelloTest(callId);
            string json = jsonUtl.encodeJson(cht);
            string err = ce.MakeAcall(callId,
                reflectionUtl.TypeName<clsHelloTest>()
                , json );
            return View(vm);
        }
        public ActionResult Action2()
        {
            return View();
        }
        public ActionResult Action3()
        {
            return View();
        }
    }
}
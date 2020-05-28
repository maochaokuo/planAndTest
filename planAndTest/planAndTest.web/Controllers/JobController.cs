using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using callMission;
using Microsoft.AspNetCore.Mvc;
using planAndTest.web.Helper;
using planAndTest.web.Models.Job;

namespace planAndTest.web.Controllers
{
    public class JobController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult JobProgress()
        {
            jProgress viewModel = new jProgress();
            //keep updating running progress
            loadJobs(viewModel);
            return View(viewModel);
        }
        private string loadJobs(jProgress viewModel)
        {
            string ret = "";
            callExe ce = new callExe();
            List<string> calls = ce.allCallsInprogress();
            viewModel.callIds.Clear();
            foreach (string aCall in calls)
                viewModel.callIds.Add(aCall);
            List<string> calldones = ce.allCalldones();
            viewModel.callDoneTodays.Clear();
            foreach (string aCalldone in calldones)
                viewModel.callDoneTodays.Add(aCalldone);
            return ret;
        }
        private void testCall()
        {
            remoteCallBridge rcb = new remoteCallBridge();
            string returnJson = "";
            string ret = "";
            //ret = rcb.instantCall("", "srvHelloTest", "doCall"
            //    , "paraJson", out returnJson);
            //Thread.Sleep(0);
            string callId;
            ret = rcb.persistentCall("", "srvHelloTest", "doCall"
                , "string", "paraJson", "string", out callId, out returnJson);
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
        private string deleteCalls()
        {
            string ret;
            callExe ce = new callExe();
            ret = ce.DeleteAllCalls();
            return ret;
        }
        private string deleteCalldones()
        {
            string ret ;
            callExe ce = new callExe();
            ret = ce.DeleteAllCalldones();
            //todo !!... deleteCalldones not working
            return ret;
        }
        [HttpPost]
        public IActionResult JobProgress(jProgress viewModel)
        {
            IActionResult retAct;
            loadJobs(viewModel);
            switch (viewModel.cmd)
            {
                case "hello":
                    testCall();
                    retAct = RedirectToAction("JobProgress");
                    break;
                case "deleteCalls":
                    deleteCalls();
                    retAct = RedirectToAction("JobProgress");
                    break;
                case "deleteCalldones":
                    deleteCalldones();
                    retAct = RedirectToAction("JobProgress");
                    break;
                default:
                    retAct = View(viewModel);
                    break;
            }
            return retAct;
        }
    }
}

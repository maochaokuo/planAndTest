using callMission;
using commonLib;
using Microsoft.AspNetCore.Mvc;
using models.calls;
using planAndTest.web.Models.EPL;

namespace planAndTest.web.Controllers
{
    public class EplController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Action1()
        {
            EplAction1vm vm = new EplAction1vm();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Action1(EplAction1vm vm)// firstName, string lastName)
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
        public IActionResult Action2()
        {
            return View();
        }
        public IActionResult Action3()
        {
            return View();
        }
    }
}
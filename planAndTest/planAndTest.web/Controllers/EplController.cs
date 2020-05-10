using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using callMission;
using Microsoft.AspNetCore.Mvc;
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
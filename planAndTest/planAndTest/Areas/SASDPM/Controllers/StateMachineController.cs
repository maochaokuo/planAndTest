using commonLib;
using models.fwk.SD;
using modelsfwk;
using planAndTest.Helper.SD;
using SASDdb.entity.fwk;
using SASDdbService.fwk;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace planAndTest.Areas.SASDPM.Controllers
{
    public class StateMachineController : ControllerBase
    {
        public StateMachineController()
            : base("stateMachineModel", "state machine")
        {
        }
        public ActionResult Index()
        {
            stateMachineViewModel viewModel;
            return View(viewModel);
        }
    }
}
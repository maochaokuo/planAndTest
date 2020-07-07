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
    public class SMstateController : ControllerBase
    {
        public SMstateController()
            : base("SMstateViewModel", "state machine state")
        {
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}
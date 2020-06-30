using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace planAndTest.Areas.SASDPM.Controllers
{
    public class SystemGroupController : Controller
    {
        //todo !!... (1) important mission:
        // need to make 1 page (single action single view) controller
        // may use accordion for 3 segment, query part, query result part, add/update/detail part
        public ActionResult Index()
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace planAndTest.Areas.SASDPM.Controllers
{
    public class PMController : Controller
    {
        // GET: SASDPM/PM
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Users()
        {
            return View();
        }
    }
}
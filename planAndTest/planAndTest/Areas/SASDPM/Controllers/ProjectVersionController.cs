using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace planAndTest.Areas.SASDPM.Controllers
{
    public class ProjectVersionController : Controller
    {
        // GET: SASDPM/ProjectVersion
        public ActionResult Index()
        {
            return View();
        }
        //todo !!... (1) a view model, b controller
        //query by projectId n version, no paging
        //iud ...
    }
}
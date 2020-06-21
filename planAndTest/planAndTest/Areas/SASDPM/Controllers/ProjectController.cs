using models.fwk.PM;
using planAndTest.Models.PM;
using SASDdbService.fwk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace planAndTest.Areas.SASDPM.Controllers
{
    public class ProjectController : Controller
    {
        // GET: SASDPM/Project
        public ActionResult Index()
        {
            projectsViewModel viewModel = new projectsViewModel();
            return View(viewModel);
        }
        protected string loadProjects(ref projectsViewModel viewModel)
        {
            string ret = "";

            return ret;
        }
        [HttpPost]
        public ActionResult Index(projectsViewModel viewModel)
        {
            ActionResult ar;
            var multiSelect = Request.Form["multiSelect"];
            // multi select 
            tblProject tp = new tblProject();
            viewModel.clearMsg();
            switch (viewModel.cmd)
            {
                default:
                    ar = View(viewModel);
                    break;
            }
            return ar;
        }
        public ActionResult AddUpdateUser()
        {
            projectEditViewModel viewModel;
            var tmpVM = TempData["projectEditViewModel"];
            if (tmpVM == null)
                viewModel = new projectEditViewModel();
            else
                viewModel = (projectEditViewModel)tmpVM;
            return View(viewModel);
        }
    }
}
using models.fwk.PM;
using planAndTest.Models.PM;
using SASDdb.entity.fwk;
using SASDdbService.fwk;
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
            usersViewModel viewModel = new usersViewModel();
            //viewModel.errorMsg = loadUsers(ref viewModel);
            return View();
        }
        protected string loadUsers(ref usersViewModel viewModel)
        {
            string ret = "";
            tblUser tu = new tblUser();
            viewModel.users.Clear();
            List<user> users = tu.getAll();
            foreach (user rec in users)
                viewModel.users.Add(rec);
            return ret;
        }
        [HttpPost]
        public ActionResult Users(usersViewModel viewModel)
        {
            ActionResult ar;
            var multiSelect = Request.Form["multiSelect"];
            // todo !!... (2) multi select not working
            switch (viewModel.cmd)
            {
                case "query":
                    viewModel.errorMsg = loadUsers(ref viewModel);
                    ar = View(viewModel);
                    break;
                case "add":
                    ar = RedirectToAction("AddUpdateUser");
                    break; 
                case "update":
                    ar = View(viewModel);
                    break;
                case "delete":
                    ar = View(viewModel);
                    break;
                default:
                    ar = View(viewModel);
                    break;
            }
            return ar;
        }
        public ActionResult AddUpdateUser()
        {
            userEditViewModel viewModel = new userEditViewModel();
            return View(viewModel);
        }
        public ActionResult AddUpdateUser(userEditViewModel viewModel)
        {
            ActionResult ar;
            switch(viewModel.cmd)
            {
                case "save":
                    ar = View(viewModel);
                    break;
                default:
                    ar = View(viewModel);
                    break;
            }
            return ar;
        }
    }
}

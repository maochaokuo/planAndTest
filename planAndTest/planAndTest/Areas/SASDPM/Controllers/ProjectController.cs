using models.fwk.PM;
using modelsfwk;
using planAndTest.Helper.PM;
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
            tblProject tp = new tblProject();
            viewModel.projects.Clear();
            List<project> projects = tp.getAll();
            if (projects != null)
            {
                foreach (project rec in projects)
                    viewModel.projects.Add(rec);
            }
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
                case "query":
                    viewModel.errorMsg = loadProjects(ref viewModel);
                    ar = View(viewModel);
                    break;
                case "add":
                    projectEditViewModel tmpVMa = new projectEditViewModel();
                    tmpVMa.pageStatus = PAGE_STATUS.ADD;
                    TempData["projectEditViewModel"] = tmpVMa;
                    ar = RedirectToAction("AddUpdateProject");
                    break;
                case "update":
                    project p = tp.getById(viewModel.singleSelect);
                    if (p != null)
                    {
                        projectEditViewModel tmpVM = new projectEditViewModel();
                        tmpVM.editModel = p;
                        tmpVM.pageStatus = PAGE_STATUS.EDIT;
                        TempData["projectEditViewModel"] = tmpVM;
                        ar = RedirectToAction("AddUpdateProject");
                        break;
                    }
                    else
                        viewModel.errorMsg = "error reading this project";
                    ar = View(viewModel);
                    break;
                case "delete":
                    if (string.IsNullOrWhiteSpace(multiSelect))
                        viewModel.errorMsg = "please select project(s) to delete";
                    else
                    {
                        string[] selected = multiSelect.Split(',');
                        foreach (string projectId in selected.ToList())
                            viewModel.errorMsg += tp.Delete(projectId);
                        tp.SaveChanges();
                        if (string.IsNullOrWhiteSpace(viewModel.errorMsg))
                            viewModel.successMsg = "successfully deleted";
                    }
                    loadProjects(ref viewModel);
                    ar = View(viewModel);
                    break;
                default:
                    ar = View(viewModel);
                    break;
            }
            return ar;
        }
        public ActionResult AddUpdateProject()
        {
            projectEditViewModel viewModel;
            var tmpVM = TempData["projectEditViewModel"];
            if (tmpVM == null)
                viewModel = new projectEditViewModel();
            else
                viewModel = (projectEditViewModel)tmpVM;
            ViewBag.userList = PMdropdownOption.userList();
            return View(viewModel);
        }
        protected string checkForm(projectEditViewModel viewModel)
        {
            string ret = "";
            if (string.IsNullOrWhiteSpace(viewModel.editModel.projectName))
                ret = "projectName cannot be empty";
            return ret;
        }
        [HttpPost]
        public ActionResult AddUpdateProject(projectEditViewModel viewModel)
        {
            ActionResult ar;
            ViewBag.userList = PMdropdownOption.userList();
            string err ;
            switch (viewModel.cmd)
            {
                case "save":
                    err = checkForm(viewModel);
                    if (err.Length > 0)
                    {
                        viewModel.errorMsg = err;
                        ar = View(viewModel);
                        break;
                    }
                    tblProject tp = new tblProject();
                    if (viewModel.pageStatus == PAGE_STATUS.ADD)
                    {
                        viewModel.editModel.projectId = Guid.NewGuid();
                        viewModel.editModel.createtime = DateTime.Now;
                        err += tp.Add(viewModel.editModel);
                        err += tp.SaveChanges();
                        if (err.Length == 0)
                        {
                            viewModel.successMsg = "new project saved";
                            viewModel.pageStatus = PAGE_STATUS.ADDSAVED;
                        }
                        else
                            viewModel.errorMsg = err;
                    }
                    else if (viewModel.pageStatus == PAGE_STATUS.EDIT)
                    {
                        err += tp.Update(viewModel.editModel);
                        err += tp.SaveChanges();
                        if (err.Length == 0)
                        {
                            viewModel.successMsg = "project updated";
                            viewModel.pageStatus = PAGE_STATUS.SAVED;
                        }
                        else
                            viewModel.errorMsg = err;
                    }
                    else
                        viewModel.errorMsg = "wrong page status " + viewModel.pageStatus;
                    //undone !!... (2), new project add an article at the root as a directory, article type project
                    ar = View(viewModel);
                    break;
                case "addNew":
                    projectEditViewModel tmpVMa = new projectEditViewModel();
                    tmpVMa.pageStatus = PAGE_STATUS.ADD;
                    TempData["projectEditViewModel"] = tmpVMa;
                    ar = RedirectToAction("AddUpdateProject");
                    break;
                case "query":
                    ar = RedirectToAction("Index");
                    break;
                default:
                    ar = View(viewModel);
                    break;
            }
            return ar;
        }

    }
}
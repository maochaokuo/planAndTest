using models.fwk.SD;
using modelsfwk;
using planAndTest.Helper.PM;
using planAndTest.Models.SD;
using SASDdb.entity.fwk;
using SASDdbService.fwk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace planAndTest.Areas.SASDPM.Controllers
{
    public class SDController : Controller
    {
        // todo !!... (1) later
        // (done)projectVersion
        // systems, systemGroup, systemEntity, 
        // stateMachine, stateMachineState, stateMachineEvent
        // stateMachineEvent2NewState
        //      (include stateMachineEventParameter, stateMachineAction)
        // database, table, field
        //      field property: primaryKeyOrder, fieldType, nullable, length(string)
        // form, control (textbox, dropdown, checkbox, button)
        // todo !!... (2) further later
        // systemTemplate
        // systemInterface, interfaceParameter, interfaceProperty
        // entityClass, entityClassVariable
        //   entityBehavior (input/output)
        // domain, domainCase
        // systemLocation, servers

        // todo !!... (1) something missing
        // (A) module level, mainly shared library 
        //  no -> change to system type add library
        // (B) database level, database, table, field
        // field property: pkOrder, type, nullable, length(string)
        // (C) user interface, form, control
        //      control include: textbox, dropdown, checkbox, button
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Systems()
        {
            systemsViewModel viewModel = new systemsViewModel();
            return View(viewModel);
        }
        protected string querySystems(ref systemsViewModel viewModel)
        {
            string ret = "";
            tblSystem ts = new tblSystem();
            systemsViewModel tmpVM = viewModel;
            var qry = (from a in ts.getAllDisp()
                       select a).AsQueryable();
            if (!string.IsNullOrWhiteSpace(viewModel.projectId))
                qry = qry.Where(x => x.projectId == new Guid(tmpVM.projectId));
            if (!string.IsNullOrWhiteSpace(viewModel.systemName))
                qry = qry.Where(x => x.systemName.Contains(tmpVM.systemName));
            viewModel.systemList = qry.ToList();
            return ret;
        }
        [HttpPost]
        public ActionResult Systems(systemsViewModel viewModel)
        {
            ActionResult ar;
            var multiSelect = Request.Form["multiSelect"];
            ViewBag.projectList = PMdropdownOption.projectList();
            viewModel.clearMsg();
            tblSystem ts = new tblSystem();
            systemEditViewModel tmpVM;
            switch (viewModel.cmd)
            {
                case "query":
                    viewModel.errorMsg = querySystems(ref viewModel);
                    ar = View(viewModel);
                    break;
                case "add":
                    tmpVM = new systemEditViewModel();
                    tmpVM.pageStatus = PAGE_STATUS.ADD;
                    TempData["systemEditViewModel"] = tmpVM;
                    ar = RedirectToAction("AddUpdate");
                    break;
                case "update":
                    systems sys = ts.getById(viewModel.singleSelect);
                    if (sys != null)
                    {
                        tmpVM = new systemEditViewModel();
                        tmpVM.editModel = sys;
                        tmpVM.pageStatus = PAGE_STATUS.EDIT;
                        TempData["systemEditViewModel"] = tmpVM;
                        ar = RedirectToAction("AddUpdate");
                        break;
                    }
                    else
                        viewModel.errorMsg = "error reading this system";
                    ar = View(viewModel);
                    break;
                case "delete":
                    //undone !!... (3) delete project delete article also
                    if (string.IsNullOrWhiteSpace(multiSelect))
                        viewModel.errorMsg = "please select system to delete";
                    else
                    {
                        string[] selected = multiSelect.Split(',');
                        foreach (string systemId in selected.ToList())
                        {
                            viewModel.errorMsg += ts.Delete(systemId);
                        }
                        viewModel.errorMsg += ts.SaveChanges();
                        if (string.IsNullOrWhiteSpace(viewModel.errorMsg))
                            viewModel.successMsg = "successfully deleted";
                    }
                    viewModel.errorMsg = querySystems(ref viewModel);
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

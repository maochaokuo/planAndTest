using models.fwk.SD;
using modelsfwk;
using planAndTest.Helper.PM;
using SASDdb.entity.fwk;
using SASDdbService.fwk.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace planAndTest.Areas.SASDPM.Controllers
{
    public class SystemGroupController : Controller
    {
        protected UnitOfWork uow = null;
        public SystemGroupController()
        {
            uow = new UnitOfWork();
        }
        //todo !!... (1) important mission:
        // need to make 1 page (single action single view) controller
        // may use accordion for 3 segment, query part, query result part, add/update/detail part
        public ActionResult Index()
        {
            systemGroupViewModel viewModel;
            var systemGroupModel = TempData["systemGroupModel"];
            if (systemGroupModel == null)
                viewModel = new systemGroupViewModel();
            else
                viewModel = (systemGroupViewModel)systemGroupModel;
            ViewBag.projectList = PMdropdownOption.projectList();
            TempData["systemGroupModel"] = systemGroupModel;
            return View(viewModel);
        }
        protected string query(ref systemGroupViewModel viewModel)
        {
            string ret = "";
            systemGroup tmpModel = viewModel.editModel;
            var qry = (from a in uow.systemGroupRepository.GetAll()
                       select a).AsQueryable();
            if (!string.IsNullOrWhiteSpace( tmpModel.projectId.ToString()))
                qry = qry.Where(x => x.projectId == tmpModel.projectId);
            if (!string.IsNullOrWhiteSpace(tmpModel.systemGroupName))
                qry = qry.Where(x => x.systemGroupName.Contains(
                    tmpModel.systemGroupName));
            if (qry.Any())
                viewModel.queryResult = qry.ToList();
            else
                viewModel.queryResult = null; 
            return ret;
        }
        [HttpPost]
        public ActionResult Index(systemGroupViewModel viewModel)
        {
            ActionResult ar;
            var multiSelect = Request.Form["multiSelect"];
            ViewBag.projectList = PMdropdownOption.projectList();
            systemGroupViewModel tmpVM;
            viewModel.clearMsg();
            switch (viewModel.cmd)
            {
                case "query":
                    viewModel.errorMsg = query(ref viewModel);
                    ar = View(viewModel);
                    break;
                case "add":
                    viewModel.editModel = new systemGroup();
                    viewModel.pageStatus = PAGE_STATUS.ADD;
                    ar = View(viewModel);
                    break;
                case "update":
                    systemGroup sg = (from a in uow.systemGroupRepository.GetAll()
                                      where a.systemGroupId.ToString()
                                        ==viewModel.singleSelect
                                      select a).FirstOrDefault();
                    if (sg != null)
                    {
                        tmpVM = new systemGroupViewModel();
                        tmpVM.editModel = sg;
                        tmpVM.pageStatus = PAGE_STATUS.EDIT;
                        TempData["systemGroupModel"] = tmpVM;
                        ar = RedirectToAction("Index");
                        break;
                    }
                    else
                        viewModel.errorMsg = "error reading this system group";
                    ar = View(viewModel);
                    break;
                default:
                    ar = View(viewModel);
                    break;
            }
            TempData["systemGroupModel"] = viewModel;
            return ar;
        }
    }
}
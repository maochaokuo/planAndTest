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
    public class GlobalEventController : ControllerBase
    {
        public GlobalEventController()
            : base("globalEventViewModel", "global event")
        {
        }
        public ActionResult Index()
        {
            globalEventViewModel viewModel;
            var stateMachineModel = TempData[modelName];
            if (stateMachineModel == null)
                viewModel = new globalEventViewModel();
            else
                viewModel = (globalEventViewModel)stateMachineModel;
            ViewBag.pageStatus = TempData[PageStatus];
            if (ViewBag.pageStatus == null)
                ViewBag.pageStatus = (int)PAGE_STATUS.QUERY;
            TempData[modelName] = viewModel;
            TempData[PageStatus] = ViewBag.pageStatus;
            return View(viewModel);
        }
        protected string query(ref globalEventViewModel viewModel)
        {
            string ret = "";
            globalEvent tmpModel = viewModel.editModel;
            var qry = (from a in uow.globalEventRepository.GetAll()
                       select a).AsQueryable();
            if (!string.IsNullOrWhiteSpace(tmpModel.globalEventName))
                qry = qry.Where(x => x.globalEventName.Contains(
                    tmpModel.globalEventName));
            if (!string.IsNullOrWhiteSpace(tmpModel.globalEventDescription))
                qry = qry.Where(x => x.globalEventDescription.Contains(
                    tmpModel.globalEventDescription));
            if (qry.Any())
                viewModel.queryResult = qry.ToList();
            else
                viewModel.queryResult = null;
            return ret;
        }
        protected string checkForm(globalEventViewModel viewModel)
        {
            string ret = "";
            if (string.IsNullOrWhiteSpace(viewModel.editModel.globalEventName))
                ret = "global event name cannot be empty";
            return ret;
        }
        [HttpPost]
        public ActionResult Index(globalEventViewModel viewModel)
        {
            ActionResult ar;
            switch(viewModel.cmd)
            {
                default:
                    ar = View(viewModel);
                    break;
            }
            return ar;
        }
    }
}
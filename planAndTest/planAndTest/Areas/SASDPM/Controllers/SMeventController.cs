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
    public class SMeventController : ControllerBase
    {
        public SMeventController()
            : base("SMeventViewModel", "state machine event")
        {
        }
        public ActionResult Index()
        {
            SMeventViewModel viewModel;
            var stateMachineModel = TempData[modelName];
            if (stateMachineModel == null)
                viewModel = new SMeventViewModel();
            else
                viewModel = (SMeventViewModel)stateMachineModel;
            ViewBag.pageStatus = TempData[PageStatus];
            if (ViewBag.pageStatus == null)
                ViewBag.pageStatus = (int)PAGE_STATUS.QUERY;
            TempData[modelName] = viewModel;
            TempData[PageStatus] = ViewBag.pageStatus;
            return View(viewModel);
        }
        protected string query(ref SMeventViewModel viewModel)
        {
            string ret = "";
            stateMachineEvent tmpModel = viewModel.editModel;
            var qry = (from a in uow.stateMachineEventRepository.GetAll()
                       select a).AsQueryable();
            if (!string.IsNullOrWhiteSpace(tmpModel.eventName))
                qry = qry.Where(x => x.eventName.Contains(
                    tmpModel.eventName));
            if (qry.Any())
                viewModel.queryResult = qry.ToList();
            else
                viewModel.queryResult = null;
            return ret;
        }
        protected string checkForm(SMeventViewModel viewModel)
        {
            string ret = "";
            if (string.IsNullOrWhiteSpace(viewModel.editModel.eventName))
                ret = "state machine event name cannot be empty";
            return ret;
        }
        [HttpPost]
        public ActionResult Index(SMeventViewModel viewModel)
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
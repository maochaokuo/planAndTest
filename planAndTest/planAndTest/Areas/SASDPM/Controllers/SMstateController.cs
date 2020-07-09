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
    public class SMstateController : ControllerBase
    {
        public SMstateController()
            : base("SMstateViewModel", "state machine state")
        {
        }
        public ActionResult Index()
        {
            SMstateViewModel viewModel;
            var stateMachineModel = TempData[modelName];
            if (stateMachineModel == null)
                viewModel = new SMstateViewModel();
            else
                viewModel = (SMstateViewModel)stateMachineModel;
            ViewBag.pageStatus = TempData[PageStatus];
            if (ViewBag.pageStatus == null)
                ViewBag.pageStatus = (int)PAGE_STATUS.QUERY;
            ViewBag.stateMachineList = SDdropdownOptions.stateMachineList();
            TempData[modelName] = viewModel;
            TempData[PageStatus] = ViewBag.pageStatus;
            return View(viewModel);
        }
        protected string query(ref SMstateViewModel viewModel)
        {
            string ret = "";
            stateMachineStateDisp tmpModel = viewModel.editModel;
            var qry = (from a in uow.stateMachineStateRepository.GetAll()
                       join b in uow.stateMachineRepository.GetAll()
                            on a.stateMachineId equals b.stateMachineId into c
                        from d in c.DefaultIfEmpty()
                       select new stateMachineStateDisp
                       {
                           stateMachineStateId=a.stateMachineStateId,
                           stateMachineId=a.stateMachineId,
                           stateMachineName=d.stateMachineName,
                           stateName=a.stateName,
                           stateDescription=a.stateDescription,
                           createtime=a.createtime
                       }).AsQueryable();
            if (!string.IsNullOrWhiteSpace(tmpModel.stateName))
                qry = qry.Where(x => x.stateName.Contains(
                    tmpModel.stateName));
            if (qry.Any())
                viewModel.queryResult = qry.ToList();
            else
                viewModel.queryResult = null;
            return ret;
        }
        protected string checkForm(SMstateViewModel viewModel)
        {
            string ret = "";
            if (string.IsNullOrWhiteSpace(viewModel.editModel.stateName))
                ret = "state machine state name cannot be empty";
            return ret;
        }
        [HttpPost]
        public ActionResult Index(SMstateViewModel viewModel)
        {
            ActionResult ar;
            switch(viewModel.cmd)
            {
                default:
                    ar = View(viewModel);
                    break;
            }
            TempData[modelName] = viewModel;
            TempData[PageStatus] = ViewBag.pageStatus;
            return ar;
        }
    }
}
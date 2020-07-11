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
            ViewBag.stateMachineName = Session["stateMachineName"] + "";
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
            var multiSelect = Request.Form[MultiSelect];
            SMstateViewModel tmpVM;
            viewModel.clearMsg();
            ViewBag.pageStatus = TempData[PageStatus];
            if (ViewBag.pageStatus == null)
                ViewBag.pageStatus = (int)PAGE_STATUS.QUERY;
            ViewBag.stateMachineName = Session["stateMachineName"] + "";
            stateMachineState model;
            switch (viewModel.cmd)
            {
                case "query":
                    if (ViewBag.pageStatus <= (int)PAGE_STATUS.QUERY)
                    {
                        viewModel.errorMsg = query(ref viewModel);
                        ar = View(viewModel);
                    }
                    else
                    {
                        ViewBag.pageStatus = (int)PAGE_STATUS.QUERY;
                        TempData[modelName] = null;
                        TempData[PageStatus] = ViewBag.pageStatus;
                        ar = RedirectToAction("Index");
                        return ar;
                    }
                    break;
                case "add":
                case "addNew":
                    viewModel.editModel = new stateMachineStateDisp();
                    ViewBag.pageStatus = (int)PAGE_STATUS.ADD;
                    TempData[modelName] = null;
                    TempData[PageStatus] = ViewBag.pageStatus;
                    ar = RedirectToAction("Index");
                    return ar;
                case "update":
                    model = (from a in uow.stateMachineStateRepository.GetAll()
                             where a.stateMachineStateId
                                   == new Guid(viewModel.singleSelect)
                             select a).FirstOrDefault();
                    if (model != null)
                    {
                        tmpVM = new SMstateViewModel();
                        tmpVM.editModel = jsonUtl.decodeJson<stateMachineStateDisp>(
                            jsonUtl.encodeJson(model));
                        TempData[PageStatus] = (int)PAGE_STATUS.EDIT;
                        TempData[modelName] = tmpVM;
                        ar = RedirectToAction("Index");
                        return ar;
                    }
                    viewModel.errorMsg = $"error reading this {modelMessage}";
                    ar = View(viewModel);
                    break;
                case "delete":
                    if (string.IsNullOrWhiteSpace(multiSelect))
                        viewModel.errorMsg = $"please select {modelMessage} to delete";
                    else
                    {
                        string[] selected = multiSelect.Split(',');
                        foreach (string recId in selected.ToList())
                        {
                            model = (from a in uow.stateMachineStateRepository.GetAll()
                                     where a.stateMachineStateId.ToString()
                                       == recId
                                     select a).FirstOrDefault();
                            if (model == null)
                                continue;
                            uow.stateMachineStateRepository.Delete(model);
                        }
                        viewModel.errorMsg = uow.SaveChanges();
                        if (string.IsNullOrWhiteSpace(viewModel.errorMsg))
                        {
                            viewModel.successMsg = "successfully deleted";
                            viewModel.errorMsg = query(ref viewModel);
                        }
                    }
                    ar = View(viewModel);
                    break;
                case "save":
                    string err = checkForm(viewModel);
                    if (err.Length > 0)
                    {
                        viewModel.errorMsg = err;
                        ar = View(viewModel);
                        break;
                    }
                    if (ViewBag.pageStatus == (int)PAGE_STATUS.ADD)
                    {
                        viewModel.editModel.stateMachineStateId = Guid.NewGuid();
                        viewModel.editModel.createtime = DateTime.Now;
                        stateMachineState toAdd = new stateMachineState();
                        toAdd = jsonUtl.decodeJson<stateMachineState>(
                            jsonUtl.encodeJson(viewModel.editModel));
                        uow.stateMachineStateRepository.Insert(toAdd);
                        viewModel.errorMsg = uow.SaveChanges();
                        if (string.IsNullOrWhiteSpace(viewModel.errorMsg))
                        {
                            viewModel.successMsg = $"new {modelMessage} saved";
                            ViewBag.pageStatus = (int)PAGE_STATUS.ADDSAVED;
                        }
                    }
                    else if (ViewBag.pageStatus == (int)PAGE_STATUS.EDIT)
                    {
                        var qry = (from a in uow.stateMachineStateRepository.GetAll()
                                   where a.stateMachineStateId
                                        == viewModel.editModel.stateMachineStateId
                                   select a).SingleOrDefault();
                        if (qry != null)
                        {
                            qry = reflectionUtl.assign<stateMachineState,
                                stateMachineState>(qry, viewModel.editModel);
                            uow.GetDbContext().Entry(qry).State
                                = EntityState.Modified;
                            viewModel.errorMsg = uow.SaveChanges();
                            if (string.IsNullOrWhiteSpace(viewModel.errorMsg))
                            {
                                viewModel.successMsg = $"{modelMessage} not found";
                                ViewBag.pageStatus = (int)PAGE_STATUS.SAVED;
                            }
                        }
                        else
                            viewModel.errorMsg = $"{modelMessage} not found";
                    }
                    else
                        viewModel.errorMsg = $"wrong page status {ViewBag.pageStatus}";
                    ar = View(viewModel);
                    break;
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
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
            ViewBag.stateMachineList = SDdropdownOptions.stateMachineList();
            ViewBag.globalEventList = SDdropdownOptions.globalEventList();
            TempData[modelName] = viewModel;
            TempData[PageStatus] = ViewBag.pageStatus;
            return View(viewModel);
        }
        protected string query(ref SMeventViewModel viewModel)
        {
            string ret = "";
            stateMachineEventDisp tmpModel = viewModel.editModel;
            var qry = (from a in uow.stateMachineEventRepository.GetAll()
                       join b in uow.stateMachineRepository.GetAll()
                            on a.stateMachineId equals b.stateMachineId into c
                       from d in c.DefaultIfEmpty()
                       join e in uow.globalEventRepository.GetAll()
                            on a.globalEventId equals e.globalEventId into f
                       from g in f.DefaultIfEmpty()
                       select new stateMachineEventDisp
                       {
                           stateMachineEventId=a.stateMachineEventId,
                           stateMachineName=d.stateMachineName,
                           eventName=a.eventName,
                           createtime=a.createtime,
                           eventActionName=a.eventActionName,
                           actionDoneEvent=a.actionDoneEvent,
                           globalEventId=a.globalEventId,
                           globalEventName=g.globalEventName,
                           eventDescription=a.eventDescription
                       }).AsQueryable();
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
            var multiSelect = Request.Form[MultiSelect];
            SMeventViewModel tmpVM;
            viewModel.clearMsg();
            ViewBag.pageStatus = TempData[PageStatus];
            if (ViewBag.pageStatus == null)
                ViewBag.pageStatus = (int)PAGE_STATUS.QUERY;
            ViewBag.stateMachineList = SDdropdownOptions.stateMachineList();
            ViewBag.globalEventList = SDdropdownOptions.globalEventList();
            stateMachineEvent model;
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
                    viewModel.editModel = new stateMachineEventDisp();
                    ViewBag.pageStatus = (int)PAGE_STATUS.ADD;
                    TempData[modelName] = null;
                    TempData[PageStatus] = ViewBag.pageStatus;
                    ar = RedirectToAction("Index");
                    return ar;
                case "update":
                    model = (from a in uow.stateMachineEventRepository.GetAll()
                          where a.stateMachineEventId
                                == new Guid(viewModel.singleSelect)
                          select a).FirstOrDefault();
                    if (model != null)
                    {
                        tmpVM = new SMeventViewModel();
                        tmpVM.editModel = jsonUtl.decodeJson<stateMachineEventDisp>(
                            jsonUtl.encodeJson(model));
                        TempData[PageStatus] = (int)PAGE_STATUS.EDIT;
                        TempData[modelName] = tmpVM;
                        ar = RedirectToAction("Index");
                        return ar;
                    }
                    else
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
                            model = (from a in uow.stateMachineEventRepository.GetAll()
                                  where a.stateMachineEventId.ToString() 
                                    == recId
                                  select a).FirstOrDefault();
                            if (model == null)
                                continue;
                            uow.stateMachineEventRepository.Delete(model);
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
                        viewModel.editModel.stateMachineEventId = Guid.NewGuid();
                        viewModel.editModel.createtime = DateTime.Now;
                        stateMachineEvent toAdd = new stateMachineEvent();
                        toAdd = jsonUtl.decodeJson<stateMachineEvent>(
                            jsonUtl.encodeJson(viewModel.editModel));
                        uow.stateMachineEventRepository.Insert(toAdd);
                        viewModel.errorMsg = uow.SaveChanges();
                        if (string.IsNullOrWhiteSpace(viewModel.errorMsg))
                        {
                            viewModel.successMsg = $"new {modelMessage} saved";
                            ViewBag.pageStatus = (int)PAGE_STATUS.ADDSAVED;
                        }
                    }
                    else if (ViewBag.pageStatus == (int)PAGE_STATUS.EDIT)
                    {
                        var qry = (from a in uow.stateMachineEventRepository.GetAll()
                                   where a.stateMachineEventId
                                        == viewModel.editModel.stateMachineEventId
                                   select a).SingleOrDefault();
                        if (qry != null)
                        {
                            qry = reflectionUtl.assign<stateMachineEvent,
                                stateMachineEvent>(qry, viewModel.editModel);
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
            return ar;
        }
    }
}
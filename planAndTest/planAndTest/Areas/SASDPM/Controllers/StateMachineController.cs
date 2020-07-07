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
    public class StateMachineController : ControllerBase
    {
        public StateMachineController()
            : base("stateMachineModel", "state machine")
        {
        }
        public ActionResult Index()
        {
            stateMachineViewModel viewModel;
            var stateMachineModel = TempData[modelName];
            if (stateMachineModel == null)
                viewModel = new stateMachineViewModel();
            else
                viewModel = (stateMachineViewModel)stateMachineModel;
            ViewBag.pageStatus = TempData[PageStatus];
            if (ViewBag.pageStatus == null)
                ViewBag.pageStatus = (int)PAGE_STATUS.QUERY;
            TempData[modelName] = viewModel;
            TempData[PageStatus] = ViewBag.pageStatus;
            return View(viewModel);
        }
        protected string query(ref stateMachineViewModel viewModel)
        {
            string ret = "";
            stateMachine tmpModel = viewModel.editModel;
            var qry = (from a in uow.stateMachineRepository.GetAll()
                       select a).AsQueryable();
            if (!string.IsNullOrWhiteSpace(tmpModel.stateMachineName))
                qry = qry.Where(x => x.stateMachineName.Contains(
                    tmpModel.stateMachineName));
            if (!string.IsNullOrWhiteSpace(tmpModel.stateMachineDescription))
                qry = qry.Where(x => x.stateMachineDescription.Contains(
                    tmpModel.stateMachineDescription));
            if (qry.Any())
                viewModel.queryResult = qry.ToList();
            else
                viewModel.queryResult = null;
            return ret;
        }
        protected string checkForm(stateMachineViewModel viewModel)
        {
            string ret = "";
            if (string.IsNullOrWhiteSpace(viewModel.editModel.stateMachineName))
                ret = "state machine name cannot be empty";
            return ret;
        }
        [HttpPost]
        public ActionResult Index(stateMachineViewModel viewModel)
        {
            ActionResult ar;
            var multiSelect = Request.Form[MultiSelect];
            stateMachineViewModel tmpVM;
            viewModel.clearMsg();
            ViewBag.pageStatus = TempData[PageStatus];
            if (ViewBag.pageStatus == null)
                ViewBag.pageStatus = (int)PAGE_STATUS.QUERY;
            stateMachine sm;
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
                    viewModel.editModel = new stateMachine();
                    ViewBag.pageStatus = (int)PAGE_STATUS.ADD;
                    TempData[modelName] = null;
                    TempData[PageStatus] = ViewBag.pageStatus;
                    ar = RedirectToAction("Index");
                    return ar;
                case "update":
                    sm = (from a in uow.stateMachineRepository.GetAll()
                          where a.stateMachineId
                                == new Guid(viewModel.singleSelect)
                          select a).FirstOrDefault();
                    if (sm!=null)
                    {
                        tmpVM = new stateMachineViewModel();
                        tmpVM.editModel = jsonUtl.decodeJson<stateMachine>(
                            jsonUtl.encodeJson(sm));
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
                        foreach (string stateMachineId in selected.ToList())
                        {
                            sm = (from a in uow.stateMachineRepository.GetAll()
                                  where a.stateMachineId.ToString() == stateMachineId
                                  select a).FirstOrDefault();
                            if (sm == null)
                                continue;
                            uow.stateMachineRepository.Delete(sm);
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
                        viewModel.editModel.stateMachineId = Guid.NewGuid();
                        viewModel.editModel.createtime = DateTime.Now;
                        stateMachine toAdd = new stateMachine();
                        toAdd = jsonUtl.decodeJson<stateMachine>(
                            jsonUtl.encodeJson(viewModel.editModel));
                        uow.stateMachineRepository.Insert(toAdd);
                        viewModel.errorMsg = uow.SaveChanges();
                        if (string.IsNullOrWhiteSpace(viewModel.errorMsg))
                        {
                            viewModel.successMsg = $"new {modelMessage} saved";
                            ViewBag.pageStatus = (int)PAGE_STATUS.ADDSAVED;
                        }
                    }
                    else if (ViewBag.pageStatus == (int)PAGE_STATUS.EDIT)
                    {
                        var qry = (from a in uow.stateMachineRepository.GetAll()
                                   where a.stateMachineId
                                        == viewModel.editModel.stateMachineId
                                   select a).SingleOrDefault();
                        if (qry != null)
                        {
                            qry = reflectionUtl.assign<stateMachine,
                                stateMachine>(qry, viewModel.editModel);
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
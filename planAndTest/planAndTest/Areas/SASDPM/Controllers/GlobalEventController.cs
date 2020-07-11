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
            var multiSelect = Request.Form[MultiSelect];
            globalEventViewModel tmpVM;
            viewModel.clearMsg();
            ViewBag.pageStatus = TempData[PageStatus];
            if (ViewBag.pageStatus == null)
                ViewBag.pageStatus = (int)PAGE_STATUS.QUERY;
            globalEvent ge;
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
                    viewModel.editModel = new globalEvent();
                    ViewBag.pageStatus = (int)PAGE_STATUS.ADD;
                    TempData[modelName] = null;
                    TempData[PageStatus] = ViewBag.pageStatus;
                    ar = RedirectToAction("Index");
                    return ar;
                case "update":
                    ge = (from a in uow.globalEventRepository.GetAll()
                          where a.globalEventId
                                == new Guid(viewModel.singleSelect)
                          select a).FirstOrDefault();
                    if (ge != null)
                    {
                        tmpVM = new globalEventViewModel();
                        tmpVM.editModel = jsonUtl.decodeJson<globalEvent>(
                            jsonUtl.encodeJson(ge));
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
                        foreach (string globalEventId in selected.ToList())
                        {
                            ge = (from a in uow.globalEventRepository.GetAll()
                                  where a.globalEventId.ToString() == globalEventId
                                  select a).FirstOrDefault();
                            if (ge == null)
                                continue;
                            uow.globalEventRepository.Delete(ge);
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
                        viewModel.editModel.globalEventId = Guid.NewGuid();
                        viewModel.editModel.createtime = DateTime.Now;
                        globalEvent toAdd = new globalEvent();
                        toAdd = jsonUtl.decodeJson<globalEvent>(
                            jsonUtl.encodeJson(viewModel.editModel));
                        uow.globalEventRepository.Insert(toAdd);
                        viewModel.errorMsg = uow.SaveChanges();
                        if (string.IsNullOrWhiteSpace(viewModel.errorMsg))
                        {
                            viewModel.successMsg = $"new {modelMessage} saved";
                            ViewBag.pageStatus = (int)PAGE_STATUS.ADDSAVED;
                        }
                    }
                    else if (ViewBag.pageStatus == (int)PAGE_STATUS.EDIT)
                    {
                        var qry = (from a in uow.globalEventRepository.GetAll()
                                   where a.globalEventId
                                        == viewModel.editModel.globalEventId
                                   select a).SingleOrDefault();
                        if (qry != null)
                        {
                            qry = reflectionUtl.assign<globalEvent,
                                globalEvent>(qry, viewModel.editModel);
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
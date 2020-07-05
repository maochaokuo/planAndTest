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
    public class SystemEntityController : ControllerBase
    {
        public SystemEntityController() 
            : base("systemEntityModel", "system entity")
        {
        }
        // GET: SASDPM/SystemEntity
        public ActionResult Index(string parentEntityIdS)
        {
            systemEntityViewModel viewModel;
            var systemEntityModel = TempData[modelName];
            if (systemEntityModel == null)
                viewModel = new systemEntityViewModel();
            else
                viewModel = (systemEntityViewModel)systemEntityModel;
            var systemId = Session["systemId"];
            if (systemId != null)
            {
                viewModel.editModel.systemId = new Guid(systemId.ToString());
                ViewBag.systemLock = true;
            }
            else
                return RedirectToAction("Systems", "SD");
            if (!string.IsNullOrWhiteSpace(parentEntityIdS))
            {
                viewModel.editModel.parentEntityId =new Guid(parentEntityIdS);
            }
            ViewBag.pageStatus = TempData[PageStatus];
            if (ViewBag.pageStatus == null)
                ViewBag.pageStatus = (int)PAGE_STATUS.QUERY;
            ViewBag.systemTemplateList = SDdropdownOptions.systemTemplateList();
            ViewBag.systemEntityList = SDdropdownOptions.systemEntityList();
            ViewBag.systemList = SDdropdownOptions.systemList();
            TempData[modelName] = systemEntityModel;
            TempData[PageStatus] = ViewBag.pageStatus;
            return View(viewModel);
        }
        protected string query(ref systemEntityViewModel viewModel)
        {
            string ret = "";
            systemEntity tmpModel = viewModel.editModel;
            tblSystem ts = new tblSystem(uow.GetDbContext());
            var qry = (from a in uow.systemEntityRepository.GetAll()
                       join b in uow.systemTemplateRepository.GetAll()
                            on a.systemTemplateId equals b.systemTemplateId
                            into c
                       from d in c.DefaultIfEmpty()
                       join e in uow.systemEntityRepository.GetAll()
                            on a.parentEntityId equals e.systemEntityId
                            into f
                        from g in f.DefaultIfEmpty()
                        join h in ts.getAll()
                            on a.systemId equals h.systemId into i
                        from j in i.DefaultIfEmpty()
                       select new systemEntityDisp
                       {
                           systemEntityId=a.systemEntityId,
                           createtime=a.createtime,
                           entityName=a.entityName,
                           entityDescription=a.entityDescription,
                           systemTemplateId=a.systemTemplateId,
                           templateName=d.templateName,
                           parentEntityId=a.parentEntityId,
                           parentEntityName=g.entityName,
                           systemId=a.systemId,
                           systemName=j.systemName
                       }).AsQueryable();
            if (!string.IsNullOrWhiteSpace(tmpModel.entityName))
                qry = qry.Where(x => x.entityName.Contains(tmpModel.entityName));
            if (!string.IsNullOrWhiteSpace(tmpModel.entityDescription))
                qry = qry.Where(x => x.entityDescription.Contains(
                    tmpModel.entityDescription));
            if (tmpModel.systemTemplateId > 0)
                qry = qry.Where(x => x.systemTemplateId == tmpModel.systemTemplateId);
            if (tmpModel.parentEntityId!=null
                    && tmpModel.parentEntityId!=Guid.Empty)
                qry = qry.Where(x => x.parentEntityId == tmpModel.parentEntityId);
            if (tmpModel.systemId!=Guid.Empty &&
                    !string.IsNullOrWhiteSpace(tmpModel.systemId.ToString()))
                qry = qry.Where(x => x.systemId == tmpModel.systemId);
            if (qry.Any())
                viewModel.queryResult = qry.ToList();
            else
                viewModel.queryResult = null;
            return ret;
        }
        protected string checkForm(systemEntityViewModel viewModel)
        {
            string ret = "";
            if (string.IsNullOrWhiteSpace(viewModel.editModel.entityName))
                ret = "entity name cannot be empty";
            else if (viewModel.editModel.systemId == Guid.Empty ||
                    string.IsNullOrWhiteSpace(viewModel.editModel.systemId.ToString()))
                ret = "systemId cannot be invalid";
            return ret;
        }
        [HttpPost]
        public ActionResult Index(systemEntityViewModel viewModel)
        {
            ActionResult ar;
            var systemId = Session["systemId"];
            if (systemId != null)
            {
                viewModel.editModel.systemId = new Guid(systemId.ToString());
                ViewBag.systemLock = true;
            }
            else
                return RedirectToAction("Systems", "SD");
            var multiSelect = Request.Form[MultiSelect];
            systemEntityViewModel tmpVM;
            viewModel.clearMsg();
            ViewBag.pageStatus = TempData[PageStatus];
            if (ViewBag.pageStatus == null)
                ViewBag.pageStatus = (int)PAGE_STATUS.QUERY;
            ViewBag.systemTemplateList = SDdropdownOptions.systemTemplateList();
            ViewBag.systemEntityList = SDdropdownOptions.systemEntityList();
            ViewBag.systemList = SDdropdownOptions.systemList();
            systemEntity se;
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
                    viewModel.editModel = new systemEntityDisp();
                    ViewBag.pageStatus = (int)PAGE_STATUS.ADD;
                    TempData[modelName] = null;
                    TempData[PageStatus] = ViewBag.pageStatus;
                    ar = RedirectToAction("Index");
                    return ar;
                case "update":
                    se = (from a in uow.systemEntityRepository.GetAll()
                                      where a.systemEntityId
                                        ==new Guid( viewModel.singleSelect)
                                      select a).FirstOrDefault();
                    if (se != null)
                    {
                        tmpVM = new systemEntityViewModel();
                        tmpVM.editModel = jsonUtl.decodeJson<systemEntityDisp>(
                            jsonUtl.encodeJson(se));
                        //se = reflectionUtl.assign<systemEntity,
                        //    systemEntity>(se, viewModel.editModel);
                        //tmpVM.editModel = se;
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
                        foreach (string systemEntityId in selected.ToList())
                        {
                            se = (from a in uow.systemEntityRepository.GetAll()
                                  where a.systemEntityId.ToString() == systemEntityId
                                  select a).FirstOrDefault();
                            if (se == null)
                                continue;
                            uow.systemEntityRepository.Delete(se);
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
                        viewModel.editModel.systemEntityId = Guid.NewGuid();
                        viewModel.editModel.createtime = DateTime.Now;
                        systemEntity toAdd = new systemEntity();
                        //toAdd = reflectionUtl.assign<systemEntity, systemEntity>
                        //    (toAdd, viewModel.editModel as systemEntity);
                        toAdd = jsonUtl.decodeJson<systemEntity>(
                            jsonUtl.encodeJson(viewModel.editModel));
                        uow.systemEntityRepository.Insert(toAdd);// viewModel.editModel);
                        viewModel.errorMsg = uow.SaveChanges();
                        if (string.IsNullOrWhiteSpace(viewModel.errorMsg))
                        {
                            viewModel.successMsg = $"new {modelMessage} saved";
                            ViewBag.pageStatus = (int)PAGE_STATUS.ADDSAVED;
                        }
                    }
                    else if (ViewBag.pageStatus == (int)PAGE_STATUS.EDIT)
                    {
                        var qry = (from a in uow.systemEntityRepository.GetAll()
                                   where a.systemEntityId
                                    == viewModel.editModel.systemEntityId
                                   select a).SingleOrDefault();
                        if (qry != null)
                        {
                            qry = reflectionUtl.assign<systemEntity,
                                systemEntity>(qry, viewModel.editModel);
                            //qry = jsonUtl.decodeJson<systemEntity>
                            //    (jsonUtl.encodeJson(viewModel.editModel));
                            uow.GetDbContext().Entry(qry).State
                                = EntityState.Modified;
                            viewModel.errorMsg = uow.SaveChanges();
                            if (string.IsNullOrWhiteSpace(viewModel.errorMsg))
                            {
                                viewModel.successMsg = $"{modelMessage} updated";
                                ViewBag.pageStatus = (int)PAGE_STATUS.SAVED;
                            }
                        }
                        else
                            viewModel.errorMsg = $"{modelMessage} not found";
                    }
                    else
                        viewModel.errorMsg = $"wrong page status " +
                            $"{ViewBag.pageStatus}";
                    ar = View(viewModel);
                    break;
                default:
                    viewModel.errorMsg = $"unhandled command {viewModel.cmd}";
                    ar = View(viewModel);
                    break;
            }
            TempData[modelName] = viewModel;
            TempData[PageStatus] = ViewBag.pageStatus;
            return ar;
        }
    }
}
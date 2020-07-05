using commonLib;
using models.fwk.SD;
using modelsfwk;
using planAndTest.Helper.PM;
using SASDdb.entity.fwk;
using SASDdbService.fwk;
using SASDdbService.fwk.repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace planAndTest.Areas.SASDPM.Controllers
{
    public class SystemGroupController : ControllerBase
    {
        //protected UnitOfWork uow = null;
        public SystemGroupController()
            : base("systemGroupModel", "system group")
        {
        }
        //need to make 1 page (single action single view) controller
        // may use accordion for 3 segment, query part, query result part, add/update/detail part
        public ActionResult Index()
        {
            systemGroupViewModel viewModel;
            var systemGroupModel = TempData[modelName];
            if (systemGroupModel == null)
                viewModel = new systemGroupViewModel();
            else
                viewModel = (systemGroupViewModel)systemGroupModel;
            ViewBag.pageStatus = TempData[PageStatus];
            if (ViewBag.pageStatus == null)
                ViewBag.pageStatus =(int) PAGE_STATUS.QUERY;
            ViewBag.projectList = PMdropdownOption.projectList();
            var projectId = Session["projectId"];
            if (projectId != null)
            {
                viewModel.editModel.projectId =new Guid( projectId.ToString());
                ViewBag.projectLock = true;
            }
            else
                return RedirectToAction("Index", "Project");
            TempData[modelName] = systemGroupModel;
            TempData[PageStatus] = ViewBag.pageStatus;
            return View(viewModel);
        }
        protected string query(ref systemGroupViewModel viewModel)
        {
            string ret = "";
            systemGroup tmpModel = viewModel.editModel;
            tblProject tp = new tblProject(uow.GetDbContext());
            var qry = (from a in uow.systemGroupRepository.GetAll()
                       join b in tp.getAll() on a.projectId 
                            equals b.projectId into c
                       from d in c.DefaultIfEmpty()
                       select new systemGroupDisp
                       {
                           systemGroupId=a.systemGroupId,
                           createtime=a.createtime,
                           systemGroupName=a.systemGroupName,
                           systemGroupDescription=a.systemGroupDescription,
                           projectId=a.projectId,
                           projectName=d.projectName
                       }).AsQueryable();
            if (tmpModel.projectId!=Guid.Empty &&
                    !string.IsNullOrWhiteSpace( tmpModel.projectId.ToString()))
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
        protected string checkForm(systemGroupViewModel viewModel)
        {
            string ret = "";
            if (string.IsNullOrWhiteSpace(viewModel.editModel.systemGroupName))
                ret = $"{modelMessage} name cannot be empty";
            return ret;
        }
        [HttpPost]
        public ActionResult Index(systemGroupViewModel viewModel)
        {
            ActionResult ar;
            var multiSelect = Request.Form[MultiSelect];
            ViewBag.projectList = PMdropdownOption.projectList();
            var projectId = Session["projectId"];
            if (projectId != null)
            {
                viewModel.editModel.projectId =new Guid( projectId.ToString());
                ViewBag.projectLock = true;
            }
            else
                return RedirectToAction("Index", "Project");
            systemGroupViewModel tmpVM;
            viewModel.clearMsg();
            ViewBag.pageStatus = TempData[PageStatus];
            if (ViewBag.pageStatus == null)
                ViewBag.pageStatus = (int)PAGE_STATUS.QUERY;
            switch (viewModel.cmd)
            {
                case "query":
                    if (ViewBag.pageStatus <=(int) PAGE_STATUS.QUERY)
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
                    viewModel.editModel = new systemGroup();
                    ViewBag.pageStatus =(int) PAGE_STATUS.ADD;
                    TempData[modelName] = null;
                    TempData[PageStatus] = ViewBag.pageStatus;
                    ar = RedirectToAction("Index");
                    return ar;
                case "update":
                    systemGroup sg = (from a in uow.systemGroupRepository.GetAll()
                                      where a.systemGroupId.ToString()
                                        ==viewModel.singleSelect
                                      select a).FirstOrDefault();
                    if (sg != null)
                    {
                        tmpVM = new systemGroupViewModel();
                        tmpVM.editModel = sg;
                        TempData[PageStatus] =(int) PAGE_STATUS.EDIT;
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
                        foreach (string systemGroupId in selected.ToList())
                        {
                            sg = (from a in uow.systemGroupRepository.GetAll()
                                  where a.systemGroupId.ToString() == systemGroupId
                                  select a).FirstOrDefault();
                            if (sg == null)
                                continue;
                            uow.systemGroupRepository.Delete(sg);
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
                    if (ViewBag.pageStatus ==(int) PAGE_STATUS.ADD)
                    {
                        viewModel.editModel.createtime = DateTime.Now;
                        uow.systemGroupRepository.Insert(viewModel.editModel);
                        viewModel.errorMsg = uow.SaveChanges();
                        if (string.IsNullOrWhiteSpace(viewModel.errorMsg))
                        {
                            viewModel.successMsg = $"new {modelMessage} saved";
                            ViewBag.pageStatus =(int) PAGE_STATUS.ADDSAVED;
                        }
                    }
                    else if (ViewBag.pageStatus ==(int) PAGE_STATUS.EDIT)
                    {
                        var qry = (from a in uow.systemGroupRepository.GetAll()
                                   where a.systemGroupId 
                                    == viewModel.editModel.systemGroupId
                                   select a).SingleOrDefault();
                        if (qry != null)
                        {
                            qry = reflectionUtl.assign<systemGroup,
                                systemGroup>(qry, viewModel.editModel);
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
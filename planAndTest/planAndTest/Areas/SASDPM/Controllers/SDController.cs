using models.fwk.SD;
using modelsfwk;
using planAndTest.Helper.PM;
using planAndTest.Helper.SD;
using planAndTest.Models.SD;
using SASDconstants;
using SASDdb.entity.fwk;
using SASDdbService;
using SASDdbService.fwk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace planAndTest.Areas.SASDPM.Controllers
{
    public class SDController : Controller
    {
        // from project->system (and/or system group)->system entity
        // todo !!... (1) later
        // (done)projectVersion
        // (done)systems, (done)systemGroup, (done)systemEntity, 
        // stateMachine, stateMachineState, stateMachineEvent
        // stateMachineEvent2NewState
        //      (include stateMachineEventParameter, stateMachineAction)
        // database, table, field
        //      field property: primaryKeyOrder, fieldType, nullable, length(string)
        // form, control (textbox, dropdown, checkbox, button)
        // todo !!... (2) further later
        // systemTemplate
        // systemInterface, interfaceParameter, interfaceProperty
        // entityClass, entityClassVariable
        //   entityBehavior (input/output)
        // domain, domainCase
        // systemLocation, servers

        // todo !!... (1) something missing
        // (A) module level, mainly shared library 
        //  no -> change to system type add library
        // (B) database level, database, table, field
        // field property: pkOrder, type, nullable, length(string)
        // (C) user interface, form, control
        //      control include: textbox, dropdown, checkbox, button
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Systems()
        {
            systemsViewModel viewModel = new systemsViewModel();
            var projectId = Session["projectId"];
            if (projectId == null)
                return RedirectToAction("Index", "Project");
            {
                viewModel.projectId = projectId.ToString();
                ViewBag.projectName = Session["projectName"] + "";
                ViewBag.projectLock = true;
            }
            //else
            ViewBag.projectList = PMdropdownOption.projectList();
            ViewBag.systemTypeList = SDdropdownOptions.systemTypeList();
            ViewBag.systemGroupList = SDdropdownOptions.systemGroupList();
            return View(viewModel);
        }
        protected string querySystems(ref systemsViewModel viewModel)
        {
            string ret = "";
            tblSystem ts = new tblSystem();
            systemsViewModel tmpVM = viewModel;
            var qry = (from a in ts.getAllDisp()
                       select a).AsQueryable();
            if (!string.IsNullOrWhiteSpace(viewModel.projectId))
                qry = qry.Where(x => x.projectId == new Guid(tmpVM.projectId));
            if (!string.IsNullOrWhiteSpace(viewModel.systemName))
                qry = qry.Where(x => x.systemName.Contains(tmpVM.systemName));
            viewModel.systemList = qry.ToList();
            return ret;
        }
        [HttpPost]
        public ActionResult Systems(systemsViewModel viewModel)
        {
            ActionResult ar;
            var multiSelect = Request.Form["multiSelect"];
            ViewBag.projectList = PMdropdownOption.projectList();
            var projectId = Session["projectId"];
            if (projectId == null)
                return RedirectToAction("Index", "Project");
            {
                viewModel.projectId = projectId.ToString();
                ViewBag.projectName = Session["projectName"] + "";
                ViewBag.projectLock = true;
            }
            //else
            viewModel.clearMsg();
            tblSystem ts = new tblSystem();
            systemEditViewModel tmpVM;
            systems sys;
            switch (viewModel.cmd)
            {
                case "query":
                    viewModel.errorMsg = querySystems(ref viewModel);
                    ar = View(viewModel);
                    break;
                case "add":
                    tmpVM = new systemEditViewModel();
                    tmpVM.pageStatus = (int)PAGE_STATUS.ADD;
                    TempData["systemEditViewModel"] = tmpVM;
                    ar = RedirectToAction("AddUpdate");
                    return ar;
                case "update":
                    sys = ts.getById(viewModel.singleSelect);
                    if (sys != null)
                    {
                        tmpVM = new systemEditViewModel();
                        tmpVM.editModel = sys;
                        tmpVM.pageStatus = (int)PAGE_STATUS.EDIT;
                        TempData["systemEditViewModel"] = tmpVM;
                        ar = RedirectToAction("AddUpdate");
                        return ar;
                    }
                    viewModel.errorMsg = "error reading this system";
                    ar = View(viewModel);
                    break;
                case "entity":
                    sys = ts.getById(viewModel.singleSelect);
                    if (sys != null)
                    {
                        Session["systemId"] = sys.systemId.ToString();
                        Session["systemName"] = sys.systemName + "";
                        ar = RedirectToAction("Index", "SystemEntity");
                        return ar;
                    }
                    viewModel.errorMsg = "error reading this system";
                    ar = View(viewModel);
                    break;
                case "delete":
                    //undone !!... (3) delete project delete article also
                    if (string.IsNullOrWhiteSpace(multiSelect))
                        viewModel.errorMsg = "please select system to delete";
                    else
                    {
                        string[] selected = multiSelect.Split(',');
                        foreach (string systemId in selected.ToList())
                        {
                            viewModel.errorMsg += ts.Delete(systemId);
                        }
                        viewModel.errorMsg += ts.SaveChanges();
                        if (string.IsNullOrWhiteSpace(viewModel.errorMsg))
                        { 
                            viewModel.successMsg = "successfully deleted";
                            viewModel.errorMsg =
                                querySystems(ref viewModel);
                        }
                    }
                    //viewModel.errorMsg = querySystems(ref viewModel);
                    ar = View(viewModel);
                    break;
                default:
                    ar = View(viewModel);
                    break;
            }
            return ar;
        }
        public ActionResult AddUpdate()
        {
            systemEditViewModel viewModel;
            var tmpVM = TempData["systemEditViewModel"];
            if (tmpVM == null)
                viewModel = new systemEditViewModel();
            else
                viewModel = (systemEditViewModel)tmpVM;
            var projectId = Session["projectId"];
            if (projectId != null)
            {
                viewModel.editModel.projectId =new Guid( projectId.ToString());
                ViewBag.projectName = Session["projectName"] + "";
                ViewBag.projectLock = true;
            }
            else
                return RedirectToAction("Index", "Project");
            ViewBag.projectList = PMdropdownOption.projectList();
            // two combo system type, system group
            ViewBag.systemTypeList = SDdropdownOptions.systemTypeList();
            ViewBag.systemGroupList = SDdropdownOptions.systemGroupList();
            return View(viewModel);
        }
        protected string checkForm(systemEditViewModel viewModel)
        {
            string ret = "";
            if (string.IsNullOrWhiteSpace(viewModel.editModel.systemName))
                ret = "system name cannot be empty";
            else if (string.IsNullOrWhiteSpace(viewModel.editModel.systemType))
                ret = "system type cannot be empty";
            return ret;
        }
        protected string addSystemArticle(systemEditViewModel viewModel
            , SASDdbContext db)
        {
            string ret;
            tblArticle ta = new tblArticle(db);
            tblProject tp = new tblProject();
            string projectName = tp.nameById(viewModel.editModel.projectId.ToString());
            article prjArticle = ta.GetByProjectId(viewModel.editModel.projectId.ToString());
            article pva = new article();
            pva.articleId = (Guid)viewModel.editModel.systemArticleId;
            pva.createtime = DateTime.Now;
            pva.articleTitle = $"project {projectName} system " +
                $"{viewModel.editModel.systemName}";
            pva.articleHtmlContent = string.Format(@"
<h1>{0} system {1}</h1>
<p>type {2}</p>
<p>{3}</p>
", projectName, viewModel.editModel.systemName
, viewModel.editModel.systemType
, viewModel.editModel.systemDescription);
            pva.articleContent = string.Format("{0} {1} {2} {3}"
                , projectName, viewModel.editModel.systemName
                , viewModel.editModel.systemType
                , viewModel.editModel.systemDescription);
            pva.isDir = true;
            pva.belongToArticleDirId = prjArticle.articleId;
            pva.articleType = ARTICLE_TYPE.Project.ToString();
            pva.articleStatus = ARTICLE_STATUS.New.ToString();
            pva.priority = 1;
            pva.projectId = viewModel.editModel.projectId;
            ret = ta.Add(pva);
            ret += ta.SaveChanges();
            return ret;
        }
        [HttpPost]
        public ActionResult AddUpdate(systemEditViewModel viewModel)
        {
            ActionResult ar;
            var projectId = Session["projectId"];
            if (projectId != null)
            {
                viewModel.editModel.projectId = new Guid(projectId.ToString());
                ViewBag.projectName = Session["projectName"] + "";
                ViewBag.projectLock = true;
            }
            else
                return RedirectToAction("Index", "Project");
            ViewBag.projectList = PMdropdownOption.projectList();
            // two combo system type, system group
            ViewBag.systemTypeList = SDdropdownOptions.systemTypeList();
            ViewBag.systemGroupList = SDdropdownOptions.systemGroupList();
            string err;
            viewModel.clearMsg();
            switch (viewModel.cmd)
            {
                case "save":
                    err = checkForm(viewModel);
                    if (err.Length > 0)
                    {
                        viewModel.errorMsg = err;
                        ar = View(viewModel);
                        break;
                    }
                    tblSystem ts = new tblSystem();
                    if (viewModel.pageStatus == (int)PAGE_STATUS.ADD)
                    {
                        viewModel.editModel.systemId = Guid.NewGuid();
                        viewModel.editModel.createtime = DateTime.Now;
                        viewModel.editModel.systemArticleId = Guid.NewGuid();
                        using (var trans = ts.BeginTransaction())
                        {
                            err += ts.Add(viewModel.editModel);
                            err += ts.SaveChanges();
                            err += addSystemArticle(viewModel,
                                ts.GetDbContext());
                            if (err.Length > 0)
                                trans.Rollback();
                            else
                                trans.Commit();
                        }
                        if (err.Length == 0)
                        {
                            viewModel.successMsg = "new system saved";
                            viewModel.pageStatus = (int)PAGE_STATUS.ADDSAVED;
                        }
                        else
                            viewModel.errorMsg = err;
                    }
                    else if (viewModel.pageStatus == (int)PAGE_STATUS.EDIT)
                    {
                        err += ts.Update(viewModel.editModel);
                        err += ts.SaveChanges();
                        if (err.Length == 0)
                        {
                            viewModel.successMsg = "system updated";
                            viewModel.pageStatus = (int)PAGE_STATUS.SAVED;
                        }
                        else
                            viewModel.errorMsg = err;
                    }
                    else
                        viewModel.errorMsg = "wrong page status " + viewModel.pageStatus;
                    ar = View(viewModel);
                    break;
                case "addNew":
                    systemEditViewModel tmpVM = new systemEditViewModel();
                    tmpVM.pageStatus = (int)PAGE_STATUS.ADD;
                    TempData["systemEditViewModel"] = tmpVM;
                    ar = RedirectToAction("AddUpdate");
                    return ar;
                case "query":
                    ar = RedirectToAction("Systems");
                    return ar;
                default:
                    ar = View(viewModel);
                    break;
            }
            return ar;
        }
    }
}

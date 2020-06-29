using models.fwk.SD;
using modelsfwk;
using planAndTest.Helper.PM;
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
        // todo !!... (1) later
        // (done)projectVersion
        // systems, systemGroup, systemEntity, 
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
            ViewBag.projectList = PMdropdownOption.projectList();
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
            viewModel.clearMsg();
            tblSystem ts = new tblSystem();
            systemEditViewModel tmpVM;
            //todo (1) two combo system type, system group
            switch (viewModel.cmd)
            {
                case "query":
                    viewModel.errorMsg = querySystems(ref viewModel);
                    ar = View(viewModel);
                    break;
                case "add":
                    tmpVM = new systemEditViewModel();
                    tmpVM.pageStatus = PAGE_STATUS.ADD;
                    TempData["systemEditViewModel"] = tmpVM;
                    ar = RedirectToAction("AddUpdate");
                    break;
                case "update":
                    systems sys = ts.getById(viewModel.singleSelect);
                    if (sys != null)
                    {
                        tmpVM = new systemEditViewModel();
                        tmpVM.editModel = sys;
                        tmpVM.pageStatus = PAGE_STATUS.EDIT;
                        TempData["systemEditViewModel"] = tmpVM;
                        ar = RedirectToAction("AddUpdate");
                        break;
                    }
                    else
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
                            viewModel.successMsg = "successfully deleted";
                    }
                    viewModel.errorMsg = querySystems(ref viewModel);
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
            ViewBag.projectList = PMdropdownOption.projectList();
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
<h1>{0} version {1}</h1>
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
            ViewBag.projectList = PMdropdownOption.projectList();
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
                    if (viewModel.pageStatus == PAGE_STATUS.ADD)
                    {
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
                            viewModel.pageStatus = PAGE_STATUS.ADDSAVED;
                        }
                        else
                            viewModel.errorMsg = err;
                    }
                    else if (viewModel.pageStatus == PAGE_STATUS.EDIT)
                    {
                        err += ts.Update(viewModel.editModel);
                        err += ts.SaveChanges();
                        if (err.Length == 0)
                        {
                            viewModel.successMsg = "system update";
                            viewModel.pageStatus = PAGE_STATUS.SAVED;
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
                    tmpVM.pageStatus = PAGE_STATUS.ADD;
                    TempData["systemEditViewModel"] = tmpVM;
                    ar = RedirectToAction("AddUpdate");
                    break;
                case "query":
                    ar = RedirectToAction("Index");
                    break;
                default:
                    ar = View(viewModel);
                    break;
            }
            return ar;
        }
    }
}

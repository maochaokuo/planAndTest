using models.fwk.PM;
using modelsfwk;
using planAndTest.Helper.PM;
using planAndTest.Models.PM;
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
    public class ProjectController : Controller
    {
        // GET: SASDPM/Project
        public ActionResult Index()
        {
            projectsViewModel viewModel = new projectsViewModel();
            return View(viewModel);
        }
        protected string loadProjects(ref projectsViewModel viewModel)
        {
            string ret = "";
            //undone !!... (3) not yet conditional query for projects
            tblProject tp = new tblProject();
            viewModel.projects.Clear();
            List<project> projects = tp.getAll().ToList();
            if (projects != null)
            {
                foreach (project rec in projects)
                    viewModel.projects.Add(rec);
            }
            return ret;
        }
        [HttpPost]
        public ActionResult Index(projectsViewModel viewModel)
        {
            ActionResult ar;
            var multiSelect = Request.Form["multiSelect"];
            // multi select 
            tblProject tp = new tblProject();
            viewModel.clearMsg();
            project p;
            switch (viewModel.cmd)
            {
                case "query":
                    // project article link
                    viewModel.errorMsg = loadProjects(ref viewModel);
                    ar = View(viewModel);
                    break;
                case "add":
                    projectEditViewModel tmpVMa = new projectEditViewModel();
                    tmpVMa.pageStatus = (int)PAGE_STATUS.ADD;
                    TempData["projectEditViewModel"] = tmpVMa;
                    ar = RedirectToAction("AddUpdateProject");
                    return ar;
                case "update":
                    p = tp.getById(viewModel.singleSelect);
                    if (p != null)
                    {
                        projectEditViewModel tmpVM = new projectEditViewModel();
                        tmpVM.editModel = p;
                        tmpVM.pageStatus = (int)PAGE_STATUS.EDIT;
                        TempData["projectEditViewModel"] = tmpVM;
                        ar = RedirectToAction("AddUpdateProject");
                        return ar;
                    }
                    viewModel.errorMsg = "error reading this project";
                    ar = View(viewModel);
                    break;
                case "systems":
                    p = tp.getById(viewModel.singleSelect);
                    if (p != null)
                    {
                        Session["projectId"] = p.projectId.ToString();
                        Session["projectName"] = p.projectName;
                        ar = RedirectToAction("Systems", "SD");
                        return ar;
                    }
                    viewModel.errorMsg = "error reading this project";
                    ar = View(viewModel);
                    break;
                case "delete":
                    //undone !!... (3) delete project delete article also
                    if (string.IsNullOrWhiteSpace(multiSelect))
                        viewModel.errorMsg = "please select project(s) to delete";
                    else
                    {
                        string[] selected = multiSelect.Split(',');
                        foreach (string projectId in selected.ToList())
                            viewModel.errorMsg += tp.Delete(projectId);
                        viewModel.errorMsg += tp.SaveChanges();
                        if (string.IsNullOrWhiteSpace(viewModel.errorMsg))
                            viewModel.successMsg = "successfully deleted";
                    }
                    loadProjects(ref viewModel);
                    ar = View(viewModel);
                    break;
                case "versions":
                    ar = RedirectToAction("Index", "ProjectVersion");
                    return ar;
                default:
                    ar = View(viewModel);
                    break;
            }
            return ar;
        }
        public ActionResult AddUpdateProject()
        {
            projectEditViewModel viewModel;
            var tmpVM = TempData["projectEditViewModel"];
            if (tmpVM == null)
                viewModel = new projectEditViewModel();
            else
                viewModel = (projectEditViewModel)tmpVM;
            ViewBag.userList = PMdropdownOption.userList();
            return View(viewModel);
        }
        protected string checkForm(projectEditViewModel viewModel)
        {
            string ret = "";
            if (string.IsNullOrWhiteSpace(viewModel.editModel.projectName))
                ret = "projectName cannot be empty";
            return ret;
        }
        protected string addProjectArticle(projectEditViewModel viewModel
            , SASDdbContext db)
        {
            string ret ;
            tblArticle ta = new tblArticle(db);
            article pa = new article();
            pa.articleId =(Guid) viewModel.editModel.projectArticleId;// Guid.NewGuid();
            pa.createtime = DateTime.Now;
            pa.articleTitle = viewModel.editModel.projectName;
            pa.articleHtmlContent = string.Format(@"
<h1>{0}</h1>
<p>{1}</p>
", viewModel.editModel.projectName, viewModel.editModel.projectDescription);
            pa.articleContent = string.Format("{0} {1}"
                , viewModel.editModel.projectName
                , viewModel.editModel.projectDescription);
            pa.isDir = true;
            pa.articleType = ARTICLE_TYPE.Project.ToString();
            pa.articleStatus = ARTICLE_STATUS.New.ToString();
            pa.priority = 1;
            pa.projectId = viewModel.editModel.projectId;
            ret = ta.Add(pa);
            ret += ta.SaveChanges();
            return ret;
        }
        [HttpPost]
        public ActionResult AddUpdateProject(projectEditViewModel viewModel)
        {
            ActionResult ar;
            ViewBag.userList = PMdropdownOption.userList();
            string err ;
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
                    tblProject tp = new tblProject();
                    if (viewModel.pageStatus == (int)PAGE_STATUS.ADD)
                    {
                        viewModel.editModel.projectId = Guid.NewGuid();
                        viewModel.editModel.createtime = DateTime.Now;
                        viewModel.editModel.projectArticleId
                            = Guid.NewGuid();
                        using (var trans = tp.BeginTransaction())
                        {
                            err += tp.Add(viewModel.editModel);
                            err += tp.SaveChanges();
                            err += addProjectArticle(viewModel, tp.GetDbContext());
                            if (err.Length > 0)
                                trans.Rollback();
                            else
                                trans.Commit();
                            // new project add an article at the root as a directory, article type project
                        }
                        if (err.Length == 0)
                        {
                            viewModel.successMsg = "new project saved";
                            viewModel.pageStatus = (int)PAGE_STATUS.ADDSAVED;
                        }
                        else
                            viewModel.errorMsg = err;
                    }
                    else if (viewModel.pageStatus == (int)PAGE_STATUS.EDIT)
                    {
                        err += tp.Update(viewModel.editModel);
                        err += tp.SaveChanges();
                        if (err.Length == 0)
                        {
                            viewModel.successMsg = "project updated";
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
                    projectEditViewModel tmpVMa = new projectEditViewModel();
                    tmpVMa.pageStatus = (int)PAGE_STATUS.ADD;
                    TempData["projectEditViewModel"] = tmpVMa;
                    ar = RedirectToAction("AddUpdateProject");
                    return ar;
                case "query":
                    ar = RedirectToAction("Index");
                    return ar;
                default:
                    ar = View(viewModel);
                    break;
            }
            return ar;
        }
    }
}

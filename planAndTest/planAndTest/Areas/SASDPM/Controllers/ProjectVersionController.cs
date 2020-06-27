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
using System.Web.UI;

namespace planAndTest.Areas.SASDPM.Controllers
{
    public class ProjectVersionController : Controller
    {
        // GET: SASDPM/ProjectVersion
        public ActionResult Index()
        {
            projectVersionsViewModel viewModel = new projectVersionsViewModel();
            ViewBag.projectList = PMdropdownOption.projectList();
            return View(viewModel);
        }
        protected string queryProjectVersions(ref projectVersionsViewModel viewModel)
        {
            string ret = "";
            tblProjectVersion tpv = new tblProjectVersion();
            projectVersionsViewModel tmpVM = viewModel;
            var qry = (from a in tpv.getAll()
                       select a).AsQueryable();
            if (!string.IsNullOrWhiteSpace(viewModel.projectId))
                qry = qry.Where(x => x.projectId == new Guid(tmpVM.projectId));
            if (!string.IsNullOrWhiteSpace(viewModel.version))
                qry = qry.Where(x => x.version.Contains(tmpVM.version));
            viewModel.projectVersions = qry.ToList();
            return ret;
        }
        [HttpPost]
        public ActionResult Index(projectVersionsViewModel viewModel)
        {
            ActionResult ar;
            var multiSelect = Request.Form["multiSelect"];
            ViewBag.projectList = PMdropdownOption.projectList();
            viewModel.clearMsg();
            tblProjectVersion tpv = new tblProjectVersion();
            projectVersionEditViewModel tmpVM;
            switch (viewModel.cmd)
            {
                case "query":
                    viewModel.errorMsg = queryProjectVersions(ref viewModel);
                    ar = View(viewModel);
                    break;
                case "add":
                    tmpVM = new projectVersionEditViewModel();
                    tmpVM.pageStatus = PAGE_STATUS.ADD;
                    TempData["projectVersionEditViewModel"] = tmpVM;
                    ar = RedirectToAction("AddUpdate");
                    break;
                case "update":
                    projectVersion pv = tpv.getById(viewModel.singleSelect);
                    if (pv != null)
                    {
                        tmpVM = new projectVersionEditViewModel();
                        tmpVM.editModel = pv;
                        tmpVM.pageStatus = PAGE_STATUS.EDIT;
                        TempData["projectVersionEditViewModel"] = tmpVM;
                        ar = RedirectToAction("AddUpdate");
                        break;
                    }
                    else
                        viewModel.errorMsg = "error reading this project version";
                    ar = View(viewModel);
                    break;
                case "delete":
                    //undone !!... (3) delete project delete article also
                    if (string.IsNullOrWhiteSpace(multiSelect))
                        viewModel.errorMsg = "please select project version to delete";
                    else
                    {
                        string[] selected = multiSelect.Split(',');
                        foreach(string projectVersionS in selected.ToList())
                        {
                            int projectVersionId ;
                            if (!int.TryParse(projectVersionS, out projectVersionId))
                                continue;
                            viewModel.errorMsg += tpv.Delete(projectVersionId);
                        }
                        viewModel.errorMsg += tpv.SaveChanges();
                        if (string.IsNullOrWhiteSpace(viewModel.errorMsg))
                            viewModel.successMsg = "successfully deleted";
                    }
                    viewModel.errorMsg = queryProjectVersions(ref viewModel);
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
            projectVersionEditViewModel viewModel;
            var tmpVM = TempData["projectVersionEditViewModel"];
            if (tmpVM == null)
                viewModel = new projectVersionEditViewModel();
            else
                viewModel = (projectVersionEditViewModel)tmpVM;
            ViewBag.projectList = PMdropdownOption.projectList();
            return View(viewModel);
        }
        protected string checkForm(projectVersionEditViewModel viewModel)
        {
            string ret = "";
            if (string.IsNullOrWhiteSpace(viewModel.editModel.version))
                ret = "version cannot be empty";
            return ret;
        }
        protected string addProjectVersionArticle(projectVersionEditViewModel 
            viewModel, SASDdbContext db)
        {
            string ret;
            tblArticle ta = new tblArticle(db);
            tblProject tp = new tblProject();
            string projectName = tp.nameById(viewModel.editModel.projectId.ToString());
            article prjArticle = ta.GetByProjectId(viewModel.editModel.projectId.ToString());
            article pva = new article();
            pva.articleId =(Guid) viewModel.editModel.versionArticleId;
            pva.createtime = DateTime.Now;
            pva.articleTitle = $"project {projectName} version " +
                $"{viewModel.editModel.version}";
            pva.articleHtmlContent = string.Format(@"
<h1>{0} version {1}</h1>
<p>{2}</p>
", projectName, viewModel.editModel.version, 
viewModel.editModel.versionDescription);
            pva.articleContent = string.Format("{0} {1} {2}"
                , projectName, viewModel.editModel.version
                , viewModel.editModel.versionDescription);
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
        public ActionResult AddUpdate(projectVersionEditViewModel viewModel)
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
                    tblProjectVersion tpv = new tblProjectVersion();
                    if (viewModel.pageStatus == PAGE_STATUS.ADD)
                    {
                        viewModel.editModel.createtime = DateTime.Now;
                        viewModel.editModel.versionArticleId = Guid.NewGuid();
                        using (var trans = tpv.BeginTransaction())
                        {
                            err += tpv.Add(viewModel.editModel);
                            err += tpv.SaveChanges();
                            err += addProjectVersionArticle(viewModel,
                                tpv.GetDbContext());
                            if (err.Length > 0)
                                trans.Rollback();
                            else
                                trans.Commit();
                        }
                        if (err.Length == 0)
                        {
                            viewModel.successMsg = "new project version saved";
                            viewModel.pageStatus = PAGE_STATUS.ADDSAVED;
                        }
                        else
                            viewModel.errorMsg = err;
                    }
                    else if (viewModel.pageStatus == PAGE_STATUS.EDIT)
                    {
                        err += tpv.Update(viewModel.editModel);
                        err += tpv.SaveChanges();
                        if (err.Length == 0)
                        {
                            viewModel.successMsg = "project version update";
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
                    projectVersionEditViewModel tmpVM = new projectVersionEditViewModel();
                    tmpVM.pageStatus = PAGE_STATUS.ADD;
                    TempData["projectVersionEditViewModel"] = tmpVM;
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

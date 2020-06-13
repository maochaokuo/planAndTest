using commonLib;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Razor.Language;
using modelsfwk.SA;
using planAndTest.Helper;
using planAndTest.Models.SA;
using SASDdb.entity.fwk;
//using SASDdb.entity.Models;
using SASDdbService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace planAndTest.Controllers
{
    public class SAController : Controller
    {
        const string EMPTY_PARENT_TITLE = "(root)";
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Articles(string articleId, string parentDirId)
        {
            articlesViewModel viewModel = new articlesViewModel();
            string err = "";
            err = loadArticle(articleId, parentDirId, ref viewModel);
            viewModel.errorMsg = err;
            //viewModel.articleId = articleId;
            //todo !!...(5) full text search for articles
            //(1) when click on a subject in subject list: 1. need to show currently selected subject in content pane
            //2. need to enable edit/reply to two button (disabled before then)
            // special layout dir(left top), subject(right top), content(bottom most left), relation link (bottom rightmost)
            return View(viewModel);
        }
        private string loadArticle(string articleId, string parentDirId
            , ref articlesViewModel viewModel)
        {
            string ret = "";
            // load directories
            tblArticle tbart = new tblArticle();
            article parent = null;
            if (!string.IsNullOrWhiteSpace(articleId))
            {
                article art = tbart.GetArticleById(articleId);
                viewModel.articleTitle = art.articleTitle;
                viewModel.articleHtmlContent = art.articleHtmlContent;
                parentDirId = art.belongToArticleDirId.ToString();
            }
            else
            {
                viewModel.articleTitle = "";
                viewModel.articleHtmlContent = "";
                parentDirId = "";
            }
            if (!string.IsNullOrWhiteSpace(parentDirId))
                parent = tbart.GetArticleById(parentDirId);
            if (parent==null)
            {
                viewModel.parentDirId = "";
                viewModel.parentDirTitle = "";
            }
            else
            {
                viewModel.parentDirId = parent.articleId.ToString();
                viewModel.parentDirTitle = parent.articleTitle;
            }
            viewModel.directories = tbart.directoriesByParentArticleId(parentDirId);
            // load subjects
            viewModel.subjects = tbart.subjectsByParentArticleId(parentDirId);
            // undone !!...(1) the article of the current directory, should be listed at the top of the subject list
            // and view its content when click on it
            return ret;
        }
        [HttpPost]
        public ActionResult Articles(articlesViewModel viewModel)
        {
            ActionResult ret;
            var selectedArticle = Request.Form["selectedArticle"];
            string err = loadArticle(viewModel.articleId, viewModel.parentDirId, ref viewModel);
            viewModel.errorMsg = err;
            articleEditViewModel aevm;
            tblArticle ta;
            article art=null;
            switch (viewModel.cmd)
            {
                case "create":
                    //if (viewModel.editingArticle==null ||
                    //    viewModel.editingArticle.BelongToArticleDirId==null)
                    //{
                    //    // !!... show error message
                    //    ret = View(viewModel);
                    //    break;
                    //}
                    //string BelongToArticleDirId = viewModel.parentDirId;
                    aevm = new articleEditViewModel();
                    if (!string.IsNullOrWhiteSpace(viewModel.parentDirId))
                    {
                        aevm.belongToArticleDirId = Guid.Parse(viewModel.parentDirId);
                        ta = new tblArticle();
                        art = ta.GetArticleById(viewModel.parentDirId);
                        aevm.parentDirTitle = art.articleTitle;
                    }
                    aevm.changeMode = ARTICLE_CHANGE_MODE.CREATE;
                    TempData["articleEditViewModel"] = jsonUtl.encodeJson( aevm);
                    //viewModel.editingArticle = new article();
                    //if (!string.IsNullOrWhiteSpace(BelongToArticleDirId))
                    //    viewModel.editingArticle.BelongToArticleDirId = Guid.Parse(BelongToArticleDirId);
                    ret = RedirectToAction("EditArticle");
                    break;
                case "edit":
                    //if (viewModel.editingArticle == null ||
                    //    viewModel.editingArticle.BelongToArticleDirId == null)
                    //{
                    //    //   !!... show error message
                    //    ret = View(viewModel);
                    //    break;
                    //}
                    ta = new tblArticle();
                    art = ta.GetArticleById(viewModel.articleId);
                    aevm = jsonUtl.decodeJson<articleEditViewModel>(jsonUtl.encodeJson(art));
                    //aevm = new articleEditViewModel();
                    if (art == null || art.belongToArticleDirId == null)
                        aevm.parentDirTitle = EMPTY_PARENT_TITLE;
                    else
                    {
                        article artParent = ta.GetArticleById(art.belongToArticleDirId.ToString());
                        aevm.parentDirTitle = artParent.articleTitle;
                    }
                    //aevm.ArticleContent = null;
                    //aevm.ArticleHtmlContent = null;
                    aevm.changeMode = ARTICLE_CHANGE_MODE.EDIT;
                    string json = jsonUtl.encodeJson(aevm);
                    TempData["articleEditViewModel"] = json;
                    ret = RedirectToAction("EditArticle");
                    break;
                case "replyTo":
                    aevm = new articleEditViewModel();
                    aevm.belongToArticleDirId =new Guid( viewModel.articleId);
                    aevm.parentDirTitle = viewModel.articleTitle;
                    aevm.changeMode = ARTICLE_CHANGE_MODE.REPLY_TO;
                    TempData["articleEditViewModel"] = jsonUtl.encodeJson(aevm);
                    ret = RedirectToAction("EditArticle");
                    break;
                case "parentDir":
                    ret = RedirectToAction("Articles", new { articleId=viewModel.parentDirId });
                    break;
                case "delete":
                    //todo !!..(2) delete confirm
                    ViewBag.confirmDelete = "1";
                    ret = View(viewModel);
                    break;
                case "realDelete":
                    //todo !!..(2) to real delete 
                    ret = View(viewModel);
                    break;
                default:
                    ret = View(viewModel);
                    break;
            }
            TempData["articlesViewModel"] =jsonUtl.encodeJson( viewModel);
            return ret;
        }

        private SelectList articleTypeOption()
        {
            List<SelectListItem> _itemType = new List<SelectListItem>();
            _itemType.Add(new SelectListItem() { Text = "General", Value = "General", Selected = true });
            _itemType.Add(new SelectListItem() { Text = "Requirement", Value = "Requirement", Selected = false });
            _itemType.Add(new SelectListItem() { Text = "Solution", Value = "Solution", Selected = false });
            _itemType.Add(new SelectListItem() { Text = "Issue", Value = "Issue", Selected = false });
            _itemType.Add(new SelectListItem() { Text = "Question", Value = "Question", Selected = false });
            _itemType.Add(new SelectListItem() { Text = "Answer", Value = "Answer", Selected = false });
            _itemType.Add(new SelectListItem() { Text = "Task", Value = "Task", Selected = false });
            return new SelectList(_itemType, "Value", "Text", null);
        }
        private SelectList articleStatusOption()
        {
            List<SelectListItem> _itemType = new List<SelectListItem>();
            _itemType.Add(new SelectListItem() { Text = "New", Value = "New", Selected = true });
            _itemType.Add(new SelectListItem() { Text = "Open", Value = "Open", Selected = false });
            _itemType.Add(new SelectListItem() { Text = "Assigned", Value = "Assigned", Selected = false });
            _itemType.Add(new SelectListItem() { Text = "Resolved", Value = "Resolved", Selected = false });
            _itemType.Add(new SelectListItem() { Text = "Closed", Value = "Closed", Selected = false });
            _itemType.Add(new SelectListItem() { Text = "Removed", Value = "Removed", Selected = false });
            _itemType.Add(new SelectListItem() { Text = "Suspended", Value = "Suspended", Selected = false });
            return new SelectList(_itemType, "Value", "Text", null);
        }
        private SelectList articlePriorityOption()
        {
            List<SelectListItem> _itemType = new List<SelectListItem>();
            for (int i = 1; i <= 9; i++)
            {
                if (i==5)
                    _itemType.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString(), Selected = true });
                else
                    _itemType.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString(), Selected = false });
            }
            return new SelectList(_itemType, "Value", "Text", null);
        }

        public ActionResult EditArticle()//string isDir)
        {
            articleEditViewModel viewModel;
            var tmpvar = TempData["articleEditViewModel"];
            if (tmpvar != null)
                viewModel = jsonUtl.decodeJson<articleEditViewModel>(tmpvar + "");
            else
            {
                viewModel = new articleEditViewModel();
                viewModel.priority = 5;
            }
            //if (!string.IsNullOrWhiteSpace(isDir) && isDir == "1")
            //    viewModel.isDir = 1;
            //else
            //    viewModel.isDir = 0;
            //ViewBag.isDir = viewModel.isDir;
            if (viewModel.belongToArticleDirId == null)
                viewModel.parentDirTitle = EMPTY_PARENT_TITLE;
            // if editing, article/directory cannot be changed
            ViewBag.articleTypeOption = articleTypeOption();
            ViewBag.articleStatusOption = articleStatusOption();
            ViewBag.articlePriorityOption = articlePriorityOption();
            TempData["articleEditViewModel"] = jsonUtl.encodeJson(viewModel);
            return View(viewModel);
        }
        private string checkForm(articleEditViewModel viewModel)
        {
            string ret = "";
            if (string.IsNullOrWhiteSpace(viewModel.articleTitle))
                ret = "article (or directory) title cannot be empty";
            return ret;
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditArticle(articleEditViewModel viewModel)
        {
            ActionResult ret;
            ViewBag.articleTypeOption = articleTypeOption();
            ViewBag.articleStatusOption = articleStatusOption();
            ViewBag.articlePriorityOption = articlePriorityOption();
            string err;
            // articles, ckeditor, paste base64 image
            switch (viewModel.cmd)
            {
                case "save":
                    err = checkForm(viewModel);
                    if (err.Length>0)
                    {
                        viewModel.errorMsg = err;
                        ret = View(viewModel);
                        break;
                    }
                    //article article2add = new article();
                    //article2add.ArticleId = Guid.NewGuid();
                    //article2add.ArticleTitle = 
                    //    viewModel.ArticleTitle;
                    //article2add.ArticleHtmlContent = 
                    //    viewModel.ArticleHtmlContent;
                    string pureText;
                    err = htmlHelper.removeHtmlTags(
                        viewModel.articleHtmlContent, out pureText);
                    if (err.Length > 0)
                    {
                        viewModel.errorMsg = err;
                        ret = View(viewModel);
                        break;
                    }
                    viewModel.articleContent = pureText;
                    //article2add.IsDir = viewModel.IsDir ;
                    tblArticle tArticle = new tblArticle();
                    if (viewModel.changeMode == ARTICLE_CHANGE_MODE.CREATE)
                    {
                        viewModel.articleId = Guid.NewGuid();
                        viewModel.createtime = DateTime.Now;
                        //article art = new article();
                        //art.articleId = viewModel.articleId;
                        //art.createtime = DateTime.Now;
                        //art.articleTitle = viewModel.articleTitle;
                        //art.articleHtmlContent = viewModel.articleHtmlContent;
                        //art.articleContent = viewModel.articleContent;
                        //art.isDir = viewModel.isDir;
                        //art.belongToArticleDirId = viewModel.belongToArticleDirId;

                        err = tArticle.Add(viewModel.GetArticle());// as article);
                        err += tArticle.SaveChanges();
                    }
                    else if (viewModel.changeMode == ARTICLE_CHANGE_MODE.EDIT)
                    {
                        err = tArticle.Update(viewModel as article);
                        err += tArticle.SaveChanges();
                    }
                    else if (viewModel.changeMode == ARTICLE_CHANGE_MODE.REPLY_TO)
                    {
                        // transaction, 1. create replied article 2. change original article to be directory type
                        SASDdbBase db = new SASDdbBase();
                        using (var transaction = db.BeginTransaction())
                        {
                            viewModel.articleId = Guid.NewGuid();
                            viewModel.createtime = DateTime.Now;
                            err = tArticle.Add(viewModel.GetArticle());// as article);
                            err += tArticle.SaveChanges();
                            tblArticle tart = new tblArticle();
                            article replied = tart.GetArticleById(viewModel.belongToArticleDirId.ToString());
                            replied.isDir = true;
                            err += tart.Update(replied);
                            err += tart.SaveChanges();
                            transaction.Commit();
                        }
                    }
                    if (err.Length > 0)
                        viewModel.errorMsg = err;
                    else
                        viewModel.successMsg = "new article successfully added";
                    //undone !!...(1) notification failed
                    //ViewBag.Message = "article/directory saved";                    
                    ret = View(viewModel);
                    break;
                default:
                    ret = View(viewModel);
                    break;
            }
            TempData["articleEditViewModel"] = jsonUtl.encodeJson(viewModel);
            return ret;
        }
    }
}
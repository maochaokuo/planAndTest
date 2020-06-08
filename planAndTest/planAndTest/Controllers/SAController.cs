using commonLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using models.SA;
using planAndTest.web.Helper;
using planAndTest.web.Models.SA;
using SASDdb.entity.Models;
using SASDdbService;
using System;

namespace planAndTest.web.Controllers
{
    public class SAController : Controller
    {
        const string EMPTY_PARENT_TITLE = "(root)";
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Articles(string articleId, string parentDirId)
        {
            articlesViewModel viewModel = new articlesViewModel();
            string err = "";
            err = loadArticle(articleId, parentDirId, ref viewModel);
            viewModel.errorMsg = err;
            //viewModel.articleId = articleId;
            //todo !!... full text search for articles

            //todo !!... special layout dir(left top), subject(right top), content(bottom most left), relation link (bottom rightmost)
            return View(viewModel);
        }
        private string loadArticle(string articleId, string parentDirId
            , ref articlesViewModel viewModel)
        {
            string ret = "";
            // load directories
            tblArticle tbart = new tblArticle();
            Article parent = null;
            if (!string.IsNullOrWhiteSpace(articleId))
            {
                Article art = tbart.GetArticleById(articleId);
                viewModel.articleTitle = art.ArticleTitle;
                viewModel.articleHtmlContent = art.ArticleHtmlContent;
                parentDirId = art.BelongToArticleDirId.ToString();
            }
            else
                viewModel.articleTitle = "";
            if (!string.IsNullOrWhiteSpace(parentDirId))
                parent = tbart.GetArticleById(parentDirId);
            if (parent==null)
            {
                viewModel.parentDirId = "";
                viewModel.parentDirTitle = "";
            }
            else
            {
                viewModel.parentDirId = parent.ArticleId.ToString();
                viewModel.parentDirTitle = parent.ArticleTitle;
            }
            viewModel.directories = tbart.directoriesByParentArticleId(parentDirId);
            // load subjects
            viewModel.subjects = tbart.subjectsByParentArticleId(parentDirId);
            return ret;
        }
        [HttpPost]
        public IActionResult Articles(articlesViewModel viewModel)
        {
            IActionResult ret;
            var selectedArticle = Request.Form["selectedArticle"];
            string err = loadArticle(viewModel.articleId, viewModel.parentDirId, ref viewModel);
            viewModel.errorMsg = err;
            articleEditViewModel aevm;
            tblArticle ta;
            Article art=null;
            switch (viewModel.cmd)
            {
                case "create":
                    //if (viewModel.editingArticle==null ||
                    //    viewModel.editingArticle.BelongToArticleDirId==null)
                    //{
                    //    //todo !!... show error message
                    //    ret = View(viewModel);
                    //    break;
                    //}
                    //string BelongToArticleDirId = viewModel.parentDirId;
                    aevm = new articleEditViewModel();
                    if (!string.IsNullOrWhiteSpace(viewModel.parentDirId))
                    {
                        aevm.BelongToArticleDirId = Guid.Parse(viewModel.parentDirId);
                        ta = new tblArticle();
                        art = ta.GetArticleById(viewModel.parentDirId);
                        aevm.parentDirTitle = art.ArticleTitle;
                    }
                    aevm.changeMode = ARTICLE_CHANGE_MODE.CREATE;
                    TempData["articleEditViewModel"] = jsonUtl.encodeJson( aevm);
                    //viewModel.editingArticle = new Article();
                    //if (!string.IsNullOrWhiteSpace(BelongToArticleDirId))
                    //    viewModel.editingArticle.BelongToArticleDirId = Guid.Parse(BelongToArticleDirId);
                    ret = RedirectToAction("EditArticle");
                    break;
                case "edit":
                    //if (viewModel.editingArticle == null ||
                    //    viewModel.editingArticle.BelongToArticleDirId == null)
                    //{
                    //    //todo !!... show error message
                    //    ret = View(viewModel);
                    //    break;
                    //}
                    ta = new tblArticle();
                    art = ta.GetArticleById(viewModel.articleId);
                    aevm = jsonUtl.decodeJson<articleEditViewModel>(jsonUtl.encodeJson(art));
                    //aevm = new articleEditViewModel();
                    if (art == null || art.BelongToArticleDirId == null)
                        aevm.parentDirTitle = EMPTY_PARENT_TITLE;
                    else
                    {
                        Article artParent = ta.GetArticleById(art.BelongToArticleDirId.ToString());
                        aevm.parentDirTitle = artParent.ArticleTitle;
                    }
                    // undone !!... there is a huge big issue here, if there is image base64, then edit will crash, then crash the whole web project
                    //aevm.ArticleContent = null;
                    //aevm.ArticleHtmlContent = null;
                    aevm.changeMode = ARTICLE_CHANGE_MODE.EDIT;
                    string json = jsonUtl.encodeJson(aevm);
                    TempData["articleEditViewModel"] = json;
                    ret = RedirectToAction("EditArticle");
                    break;
                case "replyTo":
                    aevm = new articleEditViewModel();
                    aevm.BelongToArticleDirId =new Guid( viewModel.articleId);
                    aevm.parentDirTitle = viewModel.articleTitle;
                    aevm.changeMode = ARTICLE_CHANGE_MODE.REPLY_TO;
                    TempData["articleEditViewModel"] = jsonUtl.encodeJson(aevm);
                    ret = RedirectToAction("EditArticle");
                    break;
                case "delete":
                    //todo !!.. delete confirm
                    ViewBag.confirmDelete = "1";
                    ret = View(viewModel);
                    break;
                case "realDelete":
                    //todo !!.. to real delete 
                    ret = View(viewModel);
                    break;
                default:
                    ret = View(viewModel);
                    break;
            }
            TempData["articlesViewModel"] =jsonUtl.encodeJson( viewModel);
            return ret;
        }
        public IActionResult EditArticle()//string isDir)
        {
            articleEditViewModel viewModel;
            var tmpvar = TempData["articleEditViewModel"];
            if (tmpvar != null)
            {
                viewModel = jsonUtl.decodeJson<articleEditViewModel>(tmpvar + "");
                // todo !!... need to reload html content
            }
            else
                viewModel = new articleEditViewModel();
            //if (!string.IsNullOrWhiteSpace(isDir) && isDir == "1")
            //    viewModel.isDir = 1;
            //else
            //    viewModel.isDir = 0;
            //ViewBag.isDir = viewModel.isDir;
            if (viewModel.BelongToArticleDirId == null)
                viewModel.parentDirTitle = EMPTY_PARENT_TITLE;
            // todo !!... if editing, article/directory cannot be changed
            TempData["articleEditViewModel"] = jsonUtl.encodeJson(viewModel);
            return View(viewModel);
        }
        private string checkForm(articleEditViewModel viewModel)
        {
            string ret = "";
            if (string.IsNullOrWhiteSpace(viewModel.ArticleTitle))
                ret = "article (or directory) title cannot be empty";
            return ret;
        }
        [HttpPost]
        public IActionResult EditArticle(articleEditViewModel viewModel)
        {
            IActionResult ret;
            string err;
            //todo !!... edit article
            // articles, ckeditor, paste base64 image
            object obj = Request.Form;
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
                    //Article article2add = new Article();
                    //article2add.ArticleId = Guid.NewGuid();
                    //article2add.ArticleTitle = 
                    //    viewModel.ArticleTitle;
                    //article2add.ArticleHtmlContent = 
                    //    viewModel.ArticleHtmlContent;
                    string pureText;
                    err = htmlHelper.removeHtmlTags(
                        viewModel.ArticleHtmlContent, out pureText);
                    if (err.Length > 0)
                    {
                        viewModel.errorMsg = err;
                        ret = View(viewModel);
                        break;
                    }
                    viewModel.ArticleContent = pureText;
                    //article2add.IsDir = viewModel.IsDir ;
                    tblArticle tArticle = new tblArticle();
                    if (viewModel.changeMode == ARTICLE_CHANGE_MODE.CREATE)
                    {
                        viewModel.ArticleId = Guid.NewGuid();
                        err = tArticle.Add(viewModel);
                        err += tArticle.SaveChanges();
                    }
                    else if (viewModel.changeMode == ARTICLE_CHANGE_MODE.EDIT)
                    {
                        err = tArticle.Update(viewModel);
                        err += tArticle.SaveChanges();
                    }
                    else if (viewModel.changeMode == ARTICLE_CHANGE_MODE.REPLY_TO)
                    {
                        // transaction, 1. create replied article 2. change original article to be directory type
                        SASDdbBase db = new SASDdbBase();
                        using (var transaction = db.BeginTransaction())
                        {
                            viewModel.ArticleId = Guid.NewGuid();
                            err = tArticle.Add(viewModel);
                            err += tArticle.SaveChanges();
                            tblArticle tart = new tblArticle();
                            Article replied = tart.GetArticleById(viewModel.BelongToArticleDirId.ToString());
                            replied.IsDir = true;
                            tart.Update(replied);
                            tart.SaveChanges();
                            transaction.Commit();
                        }
                    }
                    if (err.Length > 0)
                        viewModel.errorMsg = err;
                    else
                        viewModel.successMsg = "new article successfully added";
                    //undone !!... notification failed
                    //todo !!...proceed to save article/directory
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
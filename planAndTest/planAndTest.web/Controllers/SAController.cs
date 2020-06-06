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
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Articles(string articleId, string parentDirId)
        {
            articlesViewModel viewModel = new articlesViewModel();
            string err = loadArticle(viewModel.articleId, ref viewModel);
            viewModel.errorMsg = err;
            //viewModel.articleId = articleId;
            //todo !!... full text search for articles

            //todo !!... special layout dir(left top), subject(right top), content(bottom most left), relation link (bottom rightmost)
            return View(viewModel);
        }
        private string loadArticle(string articleId, ref articlesViewModel viewModel)
        {
            string ret = "";
            // load directories
            tblArticle tbart = new tblArticle();
            viewModel.directories = tbart.directoriesByArticleId(articleId);
            // load subjects
            viewModel.subjects = tbart.subjectsByArticleId(articleId);
            return ret;
        }
        [HttpPost]
        public IActionResult Articles(articlesViewModel viewModel)
        {
            IActionResult ret;
            var selectedArticle = Request.Form["selectedArticle"];
            string err = loadArticle(viewModel.articleId, ref viewModel);
            viewModel.errorMsg = err;
            articleEditViewModel aevm;
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
                    aevm.BelongToArticleDirId = Guid.Parse( viewModel.parentDirId);
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
                    aevm = new articleEditViewModel();
                    aevm.ArticleId = Guid.Parse( viewModel.articleId);
                    aevm.BelongToArticleDirId = Guid.Parse(viewModel.parentDirId);
                    aevm.changeMode = ARTICLE_CHANGE_MODE.EDIT;
                    TempData["articleEditViewModel"] = jsonUtl.encodeJson(aevm);
                    ret = RedirectToAction("EditArticle");
                    break;
                case "replyTo":
                    aevm = new articleEditViewModel();
                    aevm.ArticleId = Guid.Parse(viewModel.articleId);
                    aevm.BelongToArticleDirId = Guid.Parse(viewModel.parentDirId);
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
                viewModel = jsonUtl.decodeJson<articleEditViewModel>( tmpvar+"");
            else
                viewModel = new articleEditViewModel();
            //if (!string.IsNullOrWhiteSpace(isDir) && isDir == "1")
            //    viewModel.isDir = 1;
            //else
            //    viewModel.isDir = 0;
            //ViewBag.isDir = viewModel.isDir;
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
            //todo !!... articles, ckeditor, paste base64 image
            object obj = Request.Form;
            if (string.IsNullOrWhiteSpace(viewModel.cmd))
                throw new Exception("cmd null");
            switch (viewModel.cmd)
            {
                case "save":
                    err = checkForm(viewModel);
                    if (err.Length>0)
                    {
                        ViewBag.Error = err;
                        ret = View(viewModel);
                        break;
                    }
                    Article article2add = new Article();
                    article2add.ArticleId = Guid.NewGuid();
                    article2add.ArticleTitle = 
                        viewModel.ArticleTitle;
                    article2add.ArticleHtmlContent = 
                        viewModel.ArticleHtmlContent;
                    string pureText;
                    err = htmlHelper.removeHtmlTags(
                        article2add.ArticleHtmlContent, out pureText);
                    if (err.Length>0)
                    {
                        ViewBag.Error = err;
                        ret = View(viewModel);
                        break;
                    }
                    article2add.ArticleContent = pureText;
                    article2add.IsDir = viewModel.IsDir ;
                    tblArticle tArticle = new tblArticle();
                    err = tArticle.Add(article2add);
                    err += tArticle.SaveChanges();
                    if (err.Length > 0)
                        ViewBag.Error = err;
                    else
                        ViewBag.Message = "new article successfully added";
                    //todo !!...proceed to save article/directory
                    ViewBag.Message = "article/directory saved";                    
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
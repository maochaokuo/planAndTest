using commonLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
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
        public IActionResult Acticles(string articleId)
        {
            articleEditViewModel viewModel = new articleEditViewModel();
            viewModel.articleId = articleId;
            //todo !!... full text search for articles

            //todo !!... special layout dir(left top), subject(right top), content(bottom most left), relation link (bottom rightmost)
            return View(viewModel);
        }
        private string loadArticle(string articleId)
        {
            string ret = "";
            // todo !!... load article
            return ret;
        }
        [HttpPost]
        public IActionResult Acticles(articleEditViewModel viewModel)
        {
            IActionResult ret;
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
                    string BelongToArticleDirId = viewModel.editingArticle.BelongToArticleDirId.ToString();
                    viewModel.editingArticle = new Article();
                    viewModel.editingArticle.BelongToArticleDirId = Guid.Parse(BelongToArticleDirId);
                    ret = RedirectToAction("EditArticle", new { 
                        BelongToArticleDirId = viewModel.editingArticle.BelongToArticleDirId });
                    break;
                case "edit":
                    //if (viewModel.editingArticle == null ||
                    //    viewModel.editingArticle.BelongToArticleDirId == null)
                    //{
                    //    //todo !!... show error message
                    //    ret = View(viewModel);
                    //    break;
                    //}
                    ret = RedirectToAction("EditArticle", new { 
                        articleId = viewModel.editingArticle.ArticleId,
                        BelongToArticleDirId = viewModel.editingArticle.BelongToArticleDirId });
                    break;
                case "replyTo":
                    //todo !!... create directory
                    ret = RedirectToAction("EditArticle", new { isDir = 1,
                        BelongToArticleDirId = viewModel.editingArticle.BelongToArticleDirId });
                    break;
                default:
                    ret=View(viewModel);
                    break;
            }
            //todo !!... 
            TempData["articleEditViewModel"] =jsonUtl.encodeJson( viewModel);
            return ret;
        }
        public IActionResult EditArticle(string isDir)
        {
            articleEditViewModel viewModel = new articleEditViewModel();
            if (!string.IsNullOrWhiteSpace(isDir) && isDir == "1")
                viewModel.isDir = "1";
            else
                viewModel.isDir = "0";
            return View(viewModel);
        }
        private string checkForm(articleEditViewModel viewModel)
        {
            string ret = "";
            if (string.IsNullOrWhiteSpace(viewModel.editingArticle.ArticleTitle))
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
            //
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
                        viewModel.editingArticle.ArticleTitle;
                    article2add.ArticleHtmlContent = 
                        viewModel.editingArticle.ArticleHtmlContent;
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
                    article2add.IsDir = viewModel.isDir=="1";
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
            TempData["articleEditViewModel"] = jsonUtl.encodeJson(viewModel);
            return ret;
        }
    }
}
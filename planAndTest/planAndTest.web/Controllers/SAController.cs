using commonLib;
using Microsoft.AspNetCore.Mvc;
using planAndTest.web.Models.SA;
using SASDdb.entity.Models;
using System;

namespace planAndTest.web.Controllers
{
    public class SAController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Acticles()
        {
            articleEditViewModel viewModel = new articleEditViewModel();
            //todo !!... articles, ckeditor, paste base64 image

            //todo !!... full text search for articles

            //todo !!... special layout dir(left top), subject(right top), content(bottom most left), relation link (bottom rightmost)
            return View(viewModel);
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
                    ret = RedirectToAction("EditArticle");
                    break;
                default:
                    ret=View(viewModel);
                    break;
            }
            //todo !!... 
            TempData["articleEditViewModel"] =jsonUtl.encodeJson( viewModel);
            return ret;
        }
        public IActionResult EditArticle()
        {
            articleEditViewModel viewModel = new articleEditViewModel();

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult EditArticle(articleEditViewModel viewModel)
        {
            //todo !!... edit article
            return View(viewModel);
        }
    }
}
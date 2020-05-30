using Microsoft.AspNetCore.Mvc;
using planAndTest.web.Models.SA;

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
            //todo !!... 
            return View(viewModel);
        }
        public IActionResult Action2()
        {
            return View();
        }
        public IActionResult Action3()
        {
            return View();
        }
    }
}
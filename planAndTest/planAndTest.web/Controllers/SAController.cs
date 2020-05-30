using Microsoft.AspNetCore.Mvc;

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
            //todo !!... articles, ckeditor, paste base64 image

            //todo !!... full text search for articles

            //todo !!... special layout dir(left top), subject(right top), content(bottom most left), relation link (bottom rightmost)
            return View();
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
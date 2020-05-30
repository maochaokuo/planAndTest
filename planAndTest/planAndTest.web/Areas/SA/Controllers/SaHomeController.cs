using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace planAndTest.web.Areas.SA.Controllers
{
    public class SaHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Acticles()
        {
            //todo !!... articles, ckeditor, paste base64 image

            //todo !!... full text search for articles
            return View();
        }
    }
}

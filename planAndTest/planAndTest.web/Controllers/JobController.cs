using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace planAndTest.web.Controllers
{
    public class JobController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult JobProgress()
        {
            //todo !!... keep updating running progress
            return View();
        }
    }
}

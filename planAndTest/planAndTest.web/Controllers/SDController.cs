using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace planAndTest.web.Controllers
{
    public class SDController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
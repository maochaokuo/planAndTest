using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using planAndTest.web.Models.Job;

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
            jProgress viewModel = new jProgress();
            //todo !!... keep updating running progress
            //Response.CompleteAsynca.AddHeader("Refresh", "1");
            viewModel.callIds.Add("call1");
            viewModel.callIds.Add("call2");
            viewModel.callDoneTodays.Add("call3");
            viewModel.callDoneTodays.Add("call4");
            viewModel.callDoneTodays.Add("call5");
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult JobProgress(jProgress viewModel)
        {
            viewModel.callIds.Add("call1");
            viewModel.callIds.Add("call2");
            viewModel.callDoneTodays.Add("call3");
            viewModel.callDoneTodays.Add("call4");
            viewModel.callDoneTodays.Add("call5");
            switch(viewModel.cmd)
            {
                case "hello":
                    break;
                default:
                    break;
            }
            return View(viewModel);
        }
    }
}

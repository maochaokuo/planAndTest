using modelsfwk;
using SASDdb.entity.fwk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace planAndTest.Models.PM
{
    public class projectsViewModel : ViewModelBase
    {
        public string projectName { get; set; }
        public string projectDescription { get; set; }

        public List<project> projects { get; set; }
        public projectsViewModel()
        {
            projects = new List<project>();
        }
    }
}
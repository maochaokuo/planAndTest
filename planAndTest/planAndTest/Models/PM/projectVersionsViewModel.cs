using modelsfwk;
using SASDdb.entity.fwk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace planAndTest.Models.PM
{
    public class projectVersionsViewModel : ViewModelBase
    {
        public string projectId { get; set; }
        public string version { get; set; }
        public List<projectVersion> projectVersions { get; set; }
        public projectVersionsViewModel()
        {
            projectVersions = new List<projectVersion>();
        }
    }
}
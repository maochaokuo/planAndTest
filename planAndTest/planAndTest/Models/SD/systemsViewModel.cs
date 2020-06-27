using models.fwk.SD;
using modelsfwk;
using SASDdb.entity.fwk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace planAndTest.Models.SD
{
    public class systemsViewModel : ViewModelBase
    {
        public string projectId { get; set; }
        public string systemName { get; set; }
        public List<systemDisp> systemList { get; set; }
        public systemsViewModel()
        {
            systemList = new List<systemDisp>();
        }
    }
}
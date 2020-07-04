using SASDdb.entity.fwk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.fwk.SD
{
    public class systemEntityDisp : systemEntity
    {
        public string templateName { get; set; }
        public string parentEntityName { get; set; }
        public string systemName { get; set; }
    }
}

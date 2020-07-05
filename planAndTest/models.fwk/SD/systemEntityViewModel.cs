using modelsfwk;
using SASDdb.entity.fwk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.fwk.SD
{
    public class systemEntityViewModel : ViewModelBase
    {
        public systemEntityDisp editModel { get; set; }
        public List<systemEntityDisp> queryResult { get; set; }
        public systemEntityViewModel()
        {
            editModel = new systemEntityDisp();
            queryResult = new List<systemEntityDisp>();
        }
    }
}

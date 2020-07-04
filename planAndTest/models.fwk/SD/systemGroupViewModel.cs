using modelsfwk;
using SASDdb.entity.fwk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.fwk.SD
{
    public class systemGroupViewModel : ViewModelBase
    {
        public systemGroup editModel { get; set; }
        public List<systemGroupDisp> queryResult { get; set; }
        public systemGroupViewModel()
        {
            editModel = new systemGroup();
            queryResult = new List<systemGroupDisp>();
            //pageStatus = PAGE_STATUS.QUERY;
        }
    }
}

using modelsfwk;
using SASDdb.entity.fwk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.fwk.SD
{
    public class globalEventViewModel: ViewModelBase
    {
        public globalEvent editModel { get; set; }
        public List<globalEvent> queryResult { get; set; }
        public globalEventViewModel()
        {
            editModel = new globalEvent();
            queryResult = new List<globalEvent>();
        }
    }
}

using modelsfwk;
using SASDdb.entity.fwk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.fwk.SD
{
    public class SMstateViewModel : ViewModelBase
    {
        public stateMachineStateDisp editModel { get; set; }
        public List<stateMachineStateDisp> queryResult { get; set; }
        public SMstateViewModel()
        {
            editModel = new stateMachineStateDisp();
            queryResult = new List<stateMachineStateDisp>();
        }
    }
}

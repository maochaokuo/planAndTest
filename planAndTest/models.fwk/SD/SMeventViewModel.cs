using modelsfwk;
using SASDdb.entity.fwk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.fwk.SD
{
    public class SMeventViewModel :ViewModelBase
    {
        public stateMachineEventDisp editModel { get; set; }
        public List<stateMachineEventDisp> queryResult { get; set; }
        public SMeventViewModel()
        {
            editModel = new stateMachineEventDisp();
            queryResult = new List<stateMachineEventDisp>();
        }
    }
}

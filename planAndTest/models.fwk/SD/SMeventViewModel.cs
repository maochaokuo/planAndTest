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
        public stateMachineEvent editModel { get; set; }
        public List<stateMachineEvent> queryResult { get; set; }
        public SMeventViewModel()
        {
            editModel = new stateMachineEvent();
            queryResult = new List<stateMachineEvent>();
        }
    }
}

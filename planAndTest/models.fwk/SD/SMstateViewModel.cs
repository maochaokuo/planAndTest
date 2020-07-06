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
        public stateMachineState editModel { get; set; }
        public List<stateMachineState> queryResult { get; set; }
        public SMstateViewModel()
        {
            editModel = new stateMachineState();
            queryResult = new List<stateMachineState>();
        }
    }
}

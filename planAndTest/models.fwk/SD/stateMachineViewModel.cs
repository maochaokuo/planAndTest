using modelsfwk;
using SASDdb.entity.fwk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.fwk.SD
{
    public class stateMachineViewModel : ViewModelBase
    {
        public stateMachine editModel { get; set; }
        public List<stateMachine> queryResult { get; set; }
        public stateMachineViewModel()
        {
            editModel = new stateMachine();
            queryResult = new List<stateMachine>();
        }
    }
}

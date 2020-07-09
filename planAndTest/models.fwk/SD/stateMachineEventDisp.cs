using SASDdb.entity.fwk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.fwk.SD
{
    public class stateMachineEventDisp : stateMachineEvent
    {
        public string globalEventName { get; set; }
        public string stateMachineName { get; set; }
    }
}

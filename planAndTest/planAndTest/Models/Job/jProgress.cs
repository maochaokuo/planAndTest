using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace planAndTest.Models.Job
{
    public class jProgress
    {
        public string cmd { get; set; }
        public List<string> callIds { get; set; }
        public List<string> callDoneTodays { get; set; }
        public jProgress()
        {
            callIds = new List<string>();
            callDoneTodays = new List<string>();
        }
    }
}

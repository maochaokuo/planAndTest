using System;
using System.Collections.Generic;
using System.Text;

namespace models.calls
{
    public class ClsCallStatusPersistent : 
        clsCallStatus
    {
        public ClsCallStatusPersistent(string callId) :
            base(callId)
        {
        }

        public string callFilepath { get; set; }

        public override string addProgress(string logMsg)
        {
            string ret = "";
            ret = base.addProgress(logMsg);
            if (ret.Length > 0) return ret;
            ret = updateCallfile();
            return ret;
        }

        public string updateCallfile()
        {
            string ret = "";
            //to do !!... write call file with new progress
            return ret;
        }
    }
}

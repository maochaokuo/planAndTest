using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace modelsfwk.calls
{
    public class clsCallBase
    {
        private string _systemName = "";
        public string systemName { 
            get
            {
                //if (string.IsNullOrWhiteSpace(_systemName))
                //    throw new Exception(
                //        "systenName unavailabl!");
                return _systemName;
            }
            set { _systemName = value; }
        }
        public string callId { get; set; }
        private string _serviceName = "";
        public string serviceName { 
            get
            {
                if (string.IsNullOrWhiteSpace(_serviceName))
                    throw new Exception(
                        "serviceName unavailable!");
                return _serviceName;
            }
            set { _serviceName = value; }
        }
        public string methodName { get; set; }
        public DateTime callTime { get; set; }
        public string callTypeName { get; set; }
        public string callPara { get; set; }
        public DateTime returnTime { get; set; }
        public string returnTypeName { get; set; }
        public string returnPara { get; set; }

        public clsCallBase(string callId)
        {
            this.callId = callId;
            SetCallTime();
        }
        public void SetCallTime()
        {
            callTime = DateTime.Now;
        }
        public void SetReturnTime()
        {
            returnTime = DateTime.Now;
        }
    }
}

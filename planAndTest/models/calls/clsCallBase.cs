using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace models.calls
{
    public class clsCallBase
    {
        public string systemName { get; set; }
        public string serviceName { get; set; }
        public string methodName { get; set; }
        public DateTime callTime { get; set; }
        public string callTypeName { get; set; }
        public string callPara { get; set; }
        public DateTime returnTime { get; set; }
        public string returnTypeName { get; set; }
        public string returnPara { get; set; }
    }
}

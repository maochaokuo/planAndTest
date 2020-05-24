using System;
using System.Collections.Generic;
using System.Text;

namespace models.calls
{
    /// <summary>
    /// all status during the call
    /// </summary>
    public class clsCallStatus
    {
        public string statusSummary { get; set; }
        public string systemName { get; set; }
        public string serviceName { get; set; }
        public string methodName { get; set; }
        public DateTime callTime { get; set; }
        public string callTypeName { get; set; }
        public string callPara { get; set; }
        public SortedList<DateTime, clsCallProgress> 
            progressLst { get; set; }
        public DateTime returnTime { get; set; }
        public string returnTypeName { get; set; }
        public string returnPara { get; set; }
    }
}

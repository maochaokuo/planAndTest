using System;
using System.Collections.Generic;
using System.Text;

namespace modelsfwk.calls
{
    /// <summary>
    /// all status during the call
    /// </summary>
    [Serializable]
    public class clsCallStatus
    {
        class DescendedDateComparer : IComparer<DateTime>
        {
            public int Compare(DateTime x, DateTime y)
            {
                // use the default comparer to do the original comparison for datetimes
                int ascendingResult = Comparer<DateTime>.Default.Compare(x, y);
                // turn the result around
                return - ascendingResult;
            }
        }
        public string statusSummary { get; set; }
        public string systemName { get; set; }
        public string callId { get; set; }
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

        public clsCallStatus(string callId)
        {
            progressLst = new SortedList<DateTime, clsCallProgress>(
                new DescendedDateComparer());
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
        public virtual string addProgress(string logMsg)
        {
            string ret = "";
            clsCallProgress ccp = new clsCallProgress
            {
                theProgress = logMsg
            };
            progressLst.Add(DateTime.Now, ccp);
            ccp = null;
            return ret;
        }
    }
}

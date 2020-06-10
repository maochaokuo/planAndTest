using System;

namespace modelsfwk.calls
{
    public class clsMainLoop : clsCallStatus// clsCallBase
    {
        public clsMainLoop(string callId)
            : base(callId)
        {
            serviceName = this.GetType().Name;
            callTypeName = "clsMainLoopInput";
            returnTypeName = "string";
            returnPara = string.Format(@"done at {0}"
, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
        }
    }
}

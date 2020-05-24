using callMission;
using callMission.calls;
using commonLib;
using models.calls;
using System;
using System.Collections.Generic;
using System.Text;

namespace exeMission
{
    public class mainClass2
    {
        protected callExe ce = null;
        protected string callId = "";
        protected clsCallStatus ccs = null;

        public mainClass2(string callId)
        {
            ce = new callExe();
            this.callId = callId;
        }
        protected string initialize(string callId)
        {
            string ret = "";
            try
            {
                string json;
                ret = ce.callId2json(callId, out json);
                ccs = jsonUtl.decodeJson<clsCallStatus>(json);
                if (ccs == null)
                    throw new Exception("fail to proceed with callId "
                        + callId);
            }
            catch(Exception ex)
            {
                Exception inner = ex;
                while (inner.InnerException != null)
                    inner = inner.InnerException;
                ret = $"{ex.Message}\n{ex.StackTrace}";
            }
            return ret;
        }
        public string executeCall()
        {
            string ret = "";
            string returnJson = "";
            try
            {
                ret = invokeService.run<callMission.calls.srvHelloTest>(ccs.systemName,
                    ccs.serviceName, ccs.methodName,
                    ccs.callPara, out returnJson);
                //todo !!... call done, remove call json file
                //add call done json file
            }
            catch(Exception ex)
            {
                Exception inner = ex;
                while (inner.InnerException != null)
                    inner = inner.InnerException;
                ret = $"{ex.Message}\n{ex.StackTrace}";
            }
            return ret;
        }
    }
}

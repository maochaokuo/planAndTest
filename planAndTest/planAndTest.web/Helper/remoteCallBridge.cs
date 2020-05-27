#define RELEASE

using callMission;
using callMission.calls;
using commonLib;
using Hangfire;
using models.calls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace planAndTest.web.Helper
{
    public class remoteCallBridge
    {
        callExe ce = null;
        public remoteCallBridge()
        {
            ce = new callExe();
        }
        public remoteCallBridge(callExe ce)
        {
            this.ce = ce;
        }
        public string clearCalls()
        {
            string ret = "";
            // clearCalls and when to do it
            ret = ce.DeleteAllCalls();
            return ret;
        }
        //public string clearCalldones()
        //{
        //    string ret = "";
        //    //(no need) to do clearCalldones and when to do it
        //    return ret;
        //}
        public string instantCall(string systemName, string
            serviceName, string methodName, string paraJson, out
            string returnJson)
        {
            string ret = "";
            returnJson = "";
#if RELEASE
            try
#endif //RELEASE
            {
                string callId = callExe.genCallId();
                ret = invokeService.run(callId, systemName, serviceName
                    , methodName, paraJson, out returnJson);
                if (ret.Length > 0)
                    throw new Exception(ret);
            }
#if RELEASE
            catch(Exception ex)
            {
                Exception inner = ex;
                while (inner.InnerException != null)
                    inner = inner.InnerException;
                ret = ex.Message + "\n" + ex.StackTrace;
            }
#endif //RELEASE
            return ret;
        }
        public string persistentCall(string systemName, string
            serviceName, string methodName, string paraType
            , string paraJson, string returnType, out string callId
            , out string returnJson)
        {
            // persistentCall
            //1. call status objct
            //2. run exe
            string ret = "";
            returnJson = "";
            callId = callExe.genCallId();
            try
            {
                clsCallStatus ccs = new clsCallStatus(callId);
                ccs.systemName = systemName;
                ccs.callId = callId;
                ccs.serviceName = serviceName;
                ccs.methodName = methodName;
                ccs.callTypeName = paraType;
                ccs.callTime = DateTime.Now;
                ccs.callPara = paraJson;
                ccs.returnTypeName = returnType;
                //ccs.callFilepath = "..."; // todo !!...
                string json = jsonUtl.encodeJson(ccs);
                ret = ce.MakeAcall(callId, json);
                if (ret.Length > 0)
                    throw new Exception(ret);
                dbg.o("before spawn exe");
                // change to use hangire...
                string callIdOut = callId;
                BackgroundJob.Enqueue(() =>
                    ce.spawnEXE(callIdOut, serviceName)
                );
                dbg.o("after spawn exe");
            }
            catch (Exception ex)
            {
                Exception inner = ex;
                while (inner.InnerException != null)
                    inner = inner.InnerException;
                ret = ex.Message + "\n" + ex.StackTrace;
            }
            return ret;
        }
    }
}

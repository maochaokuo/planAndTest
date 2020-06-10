using callMission;
using callMission.calls;
using commonLib;
using modelsfwk.calls;
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
            dbg.o("mainClass2 a");
            ce = new callExe();
            this.callId = callId;
            string ret = initialize(this.callId);
            if (ret.Length > 0)
                throw new Exception(ret);
            dbg.o("mainClass2 b "+ret);
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
                dbg.o("executeCall 1");
                ret = invokeService.run(callId, ccs.systemName,
                    ccs.serviceName, ccs.methodName,
                    ccs.callPara, out returnJson);
                if (ret.Length > 0)
                    throw new Exception(ret);
                ccs.SetReturnTime();
                ccs.returnPara = returnJson;
                string retCallId;
                ret = ce.ReturnAcall(callId, returnJson, out retCallId);
                dbg.o($"executeCall ret={ret} returnJson={returnJson}" +
                    $" retCallId={retCallId}");
                // call done, remove call json file
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

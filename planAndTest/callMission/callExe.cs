using commonLib;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace callMission
{
    public class callExe
    {
        public readonly string EXE_PATH;
        public readonly string DATA_PATH;
        public readonly string CALL_PATH;

        public callExe()
        {
            string envPath = AppDomain.CurrentDomain.BaseDirectory;
            string webProjectPath = envPath.Replace(@"netcoreapp3.1\","");
            webProjectPath = envPath.Replace(@"Debug\", "");
            webProjectPath = envPath.Replace(@"bin\", "");
            string missionPath = webProjectPath.Replace(
                @"planAndTest.web", @"exeMission");
            DATA_PATH = fileUtl.pb(missionPath, "Data");
            CALL_PATH = fileUtl.pb(DATA_PATH, "calls");
            string err = fileUtl.ensureDir(DATA_PATH, "calls");
            if (err.Length>0) throw new Exception(err);
            missionPath += @"bin\Debug\netcoreapp3.1\exeMission.exe";
            EXE_PATH = missionPath;
            Thread.Sleep(0);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="json">serialized call model, depend on service name</param>
        /// <returns></returns>
        public string MakeAcall(string serviceName, string json)
        {
            string ret = "";
            string callTs = fileUtl.genTimeStamp();
            ret = putCallJson(callTs, json);
            if (ret.Length > 0) return ret;

            //todo !!... spawn exeMission.exe, with servicename n callTs
            return ret;
        }
        private string putCallJson(string callTs, string json)
        {
            string ret = "";
            ret = fileUtl.ensureDir(CALL_PATH, callTs);
            if (ret.Length > 0) return ret;
            ret = fileUtl.json2file(fileUtl.pb(CALL_PATH,
                callTs), json);
            if (ret.Length > 0) return ret;
            return ret;
        }
    }
}

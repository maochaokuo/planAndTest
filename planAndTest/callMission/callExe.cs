using commonLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace callMission
{
    public class callExe
    {
        public readonly string EXE_PATH;
        public readonly string DATA_PATH;
        public readonly string CALL_PATH;
        public readonly string CALLDONE_PATH;

        public callExe()
        {
            string envPath = AppDomain.CurrentDomain.BaseDirectory;
            string webProjectPath = envPath.Replace("netcoreapp3.1\\", "");
            webProjectPath = webProjectPath.Replace("Debug\\", "");
            webProjectPath = webProjectPath.Replace("bin\\", "");
            string missionPath = webProjectPath.Replace(
                @"planAndTest.web", @"exeMission");
            DATA_PATH = fileUtl.pb(missionPath, "Data");

            CALL_PATH = fileUtl.pb(DATA_PATH, "calls");
            string err = fileUtl.ensureDir(DATA_PATH, "calls");
            if (err.Length>0) throw new Exception(err);

            CALLDONE_PATH = fileUtl.pb(DATA_PATH, "calldones");
            err = fileUtl.ensureDir(DATA_PATH, "calldones");
            if (err.Length > 0) throw new Exception(err);

            missionPath += @"bin\Debug\netcoreapp3.1\exeMission.exe";
            EXE_PATH = missionPath;
            Thread.Sleep(0);
        }
        public List<string> findCallInprogress()
        {
            List<string> ret = null;
            // todo !!... findCallInprogress
            return ret;
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

            // spawn exeMission.exe, with servicename n callTs
            Process p = new Process();
            p.StartInfo.FileName = EXE_PATH;// "dotnet";
            p.StartInfo.Arguments = serviceName + " " + callTs;
            p.StartInfo.UseShellExecute = true;// false;
            p.StartInfo.CreateNoWindow = true;// false;
            p.Start();
            //p.WaitForExit();

            return ret;
        }
        private string putCallJson(string callTs, string json)
        {
            string ret = "";
            ret = fileUtl.ensureDir(CALL_PATH, callTs);
            if (ret.Length > 0) return ret;
            string jsonFullfilename = fileUtl.pb(fileUtl.pb(CALL_PATH,
                callTs), callTs) + ".json";
            ret = fileUtl.json2file(jsonFullfilename, json);
            if (ret.Length > 0) return ret;
            return ret;
        }
        /// <summary>
        /// 呼叫完成時呼叫
        /// </summary>
        /// <param name="callTs"></param>
        /// <param name="returnJson"></param>
        /// <returns></returns>
        public string ReturnAcall(string callTs, string returnJson)
        {
            string ret = "";
            string callTsPath = fileUtl.pb(CALL_PATH, callTs);
            ret = fileUtl.purgePath(callTsPath, true);
            if (ret.Length > 0) return ret;
            string newfile = fileUtl.genTimeStamp() + ".json";
            ret = fileUtl.json2file(fileUtl.pb(CALL_PATH, newfile), returnJson);
            //todo !!... 少做一件事，移到calldone_path
            return ret;
        }
    }
}

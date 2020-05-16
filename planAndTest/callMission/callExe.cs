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
        protected readonly string CALLDONE_PATH;

        public static string genCallId()
        {
            string ret;
            ret = DateTime.Now.ToString(
                "yyyyMMddHHmmssfff");
            return ret;
        }

        public callExe()
        {
            string envPath = AppDomain.CurrentDomain.BaseDirectory;
            string webProjectPath = envPath.Replace("netcoreapp3.1\\", "");
            webProjectPath = webProjectPath.Replace("Debug\\", "");
            webProjectPath = webProjectPath.Replace("bin\\", "");
            string missionPath = webProjectPath.Replace(
                @"planAndTest.web", @"exeMission");
            DATA_PATH = fileUtl.pb(missionPath, "Data");

            string err = fileUtl.ensureDir(DATA_PATH, "calls");
            if (err.Length>0) throw new Exception(err);
            CALL_PATH = fileUtl.pb(DATA_PATH, "calls");

            CALLDONE_PATH = fileUtl.pb(DATA_PATH, "calldones");
            err = fileUtl.ensureDir(DATA_PATH, "calldones");
            if (err.Length > 0) throw new Exception(err);

            missionPath += @"bin\Debug\netcoreapp3.1\exeMission.exe";
            EXE_PATH = missionPath;
            Thread.Sleep(0);
        }
        public string CALLDONE_PATHtoday(
            out string returnPath)
        {
            string ret = "";
            returnPath = "";
            string calldoneYyyyMM =
                DateTime.Today.ToString("yyyyMM");
            ret = fileUtl.ensureDir(CALLDONE_PATH,
                calldoneYyyyMM);
            if (ret.Length > 0) return ret;

            string pathYyyyMM = fileUtl.pb(CALLDONE_PATH
                , calldoneYyyyMM);

            string calldoneDd =
                DateTime.Today.ToString("dd");
            ret = fileUtl.ensureDir(pathYyyyMM,
                calldoneDd);
            if (ret.Length > 0) return ret;
            returnPath = fileUtl.pb(pathYyyyMM,
                calldoneDd);
            return ret;
        }
        /// <summary>
        /// find all callIds in progress except myself 
        /// </summary>
        /// <param name="exceptCallId"></param>
        /// <returns></returns>
        public List<string> allCallsInprogress(
            string exceptCallId="")
        {
            List<string> ret = null;
            // todo !!... findCallInprogress
            return ret;
        }
        private string DeleteAcall(string callId)
        {
            string ret = "";
            //todo !!... DeleteAcall
            return ret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="json">serialized call model, depend on service name</param>
        /// <returns></returns>
        public string MakeAcall(string callId 
            , string serviceName, string json)
        {
            string ret = "";
            ret = putCallJson(callId, json);
            if (ret.Length > 0) return ret;

            // spawn exeMission.exe, with servicename n callTs
            Process p = new Process();
            p.StartInfo.FileName = EXE_PATH;// "dotnet";
            p.StartInfo.Arguments = callId + " " + serviceName;
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
        /// <param name="oriCallId"></param>
        /// <param name="returnJson"></param>
        /// <returns></returns>
        public string ReturnAcall(string oriCallId
            , string returnJson, out string retCallId)
        {
            string ret = "";
            string callTsPath = fileUtl.pb(CALL_PATH, oriCallId);
            ret = fileUtl.purgePath(callTsPath, true);
            retCallId = genCallId();
            if (ret.Length > 0) return ret;
            string newfile = retCallId + ".json";
            ret = fileUtl.json2file(fileUtl.pb(CALL_PATH, newfile), returnJson);
            //todo !!... 少做一件事，移到calldone_path
            return ret;
        }
        /// <summary>
        /// move to return directory
        /// </summary>
        /// <param name="callTs"></param>
        /// <param name="returnJson"></param>
        /// <returns></returns>
        private string Move2return(string callTs, string returnJson)
        {
            string ret = "";

            //todo !!... delete in progress
            //add return/calldone directory (with date)
            return ret;
        }
    }
}

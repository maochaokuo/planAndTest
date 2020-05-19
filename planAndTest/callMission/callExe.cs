using commonLib;
using models.calls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
//using System.IO;

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
        public static string callId2time(string callId
            , out DateTime outime)
        {
            string ret = "";
            outime = DateTime.MinValue;
            if (callId.Length != 17)
                ret = "invalid callId";
            else
            {
                string timeS = string.Format(
                    @"{0}-{1}-{2} {3}:{4}:{5}.{6}",
                    callId.Substring(0, 4),
                    callId.Substring(4, 2),
                    callId.Substring(6, 2),
                    callId.Substring(8, 2),
                    callId.Substring(10, 2),
                    callId.Substring(12, 2),
                    callId.Substring(14, 3));
                if (!DateTime.TryParse(timeS, out outime))
                    ret = "fail to parse callId";
            }
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
        }
        public string callId2json(string callId
            , out string json)
        {
            string ret = "";
            string callPath = fileUtl.pb(CALL_PATH, callId);
            json = fileUtl.file2string(callPath);
            return ret;
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
            List<string> ret = fileUtl.getAllSubdirs(
                CALL_PATH);
            // findCallInprogress
            return ret;
        }

        private string DeleteAcall(string callId)
        {
            string ret = "";
            string callDir = fileUtl.pb(CALL_PATH, callId);
            ret = fileUtl.deleteDirFiles(callDir);
            if (ret.Length > 0) return ret;
            ret = fileUtl.deleteDir(callDir);
            return ret;
        }
        private string ReadAcall(string callId
            , out clsCallBase callObj)
        {
            string ret = "";
            string callDir = fileUtl.pb(CALL_PATH, callId);
            string callFile = fileUtl.newestFile(callDir);
            string json = fileUtl.file2string( fileUtl.pb(
                callDir, callFile));
            callObj = jsonUtl.decodeJson<clsCallBase>(json);
            return ret;
        }
        /// <summary>
        /// spawn exe mission 
        /// </summary>
        /// <param name="callId"></param>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public string spawnEXE(string callId, string
            serviceName)
        {
            string ret = "";

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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="json">serialized call model, depend on service name</param>
        /// <returns></returns>
        public string MakeAcall(string callId 
            , string json, string serviceName="")
        {
            string ret = "";
            ret = putCallJson(callId, json);
            //if (ret.Length > 0) return ret;

            return ret;
        }
        private string putCallJson(string callTs, string json)
        {
            string ret = "";
            ret = fileUtl.ensureDir(CALL_PATH, callTs);
            if (ret.Length > 0) return ret;
            string jsonFullfilename = fileUtl.pb(fileUtl.pb(CALL_PATH,
                callTs), callTs) + ".json";
            ret = fileUtl.string2file(jsonFullfilename, json);
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
            string ret;
            retCallId = "";
            // get called json/object
            clsCallBase ccb ;
            ret = ReadAcall(oriCallId, out ccb);
            if (ret.Length > 0) return ret;
            //string callTsPath = fileUtl.pb(CALL_PATH, oriCallId);
            //string json = fileUtl.file2string(callTsPath);
            //clsCallBase ccb = jsonUtl.decodeJson<clsCallBase>(json);
            // delete called dir/file
            ret = DeleteAcall(oriCallId);
            if (ret.Length > 0) return ret;
            //ret = fileUtl.purgePath(callTsPath, true);
            retCallId = genCallId();
            if (ret.Length > 0) return ret;
            //產生回傳 update json/object with return info
            ccb.returnPara = returnJson;
            ccb.returnTime = DateTime.Now;
            string json = jsonUtl.encodeJson(ccb);
            string newfile = retCallId + ".json";
            // write to return folder
            string callDonePath;
            ret = CALLDONE_PATHtoday(out callDonePath);
            if (ret.Length > 0) return ret;
            ret = fileUtl.string2file(fileUtl.pb(callDonePath, newfile)
                , json);
            return ret;
        }
    }
}

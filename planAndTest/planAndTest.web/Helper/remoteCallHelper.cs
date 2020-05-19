using callMission;
using commonLib;
using models.calls;
using System;
using System.Collections.Generic;

namespace planAndTest.web.Helper
{
    public class remoteCallHelper
    {
        protected callExe ce = null;

        public remoteCallHelper()
        {
            ce = new callExe();
        }
        /// <summary>
        /// 檢查main loop是否存活
        /// </summary>
        /// <returns></returns>
        public string checkMainLoop()
        {
            string ret = "";
            string newestCallId = fileUtl.newestDir(
                ce.CALL_PATH);
            DateTime dt = DateTime.MinValue;
            ret = callExe.callId2time(newestCallId, 
                out dt);
            if (ret.Length > 0) return ret;
            TimeSpan ts = DateTime.Now - dt;
            if (ts.TotalMinutes>0)
            {
            //todo !!... 若要呼叫，距離上次成功呼叫若太久
            //或上次失敗，則先echo, 等echo back
            }
            return ret;
        }
        /// <summary>
        /// 將main loop執行起來。若前有main loop則終結
        /// </summary>
        /// <returns></returns>
        public string runNewMainLoop()
        {
            string ret = "";
            string callId = callExe.genCallId();
            clsMainLoop cmli = new clsMainLoop(callId);
            cmli.serviceName = "initialize";
            cmli.callTypeName = "object";
            cmli.callPara = null;
            string json = jsonUtl.encodeJson(cmli);
            ret = ce.MakeAcall(callId, json);
            if (ret.Length > 0) return ret;
            ret = ce.spawnEXE(callId, cmli.serviceName);
            return ret;
        }
        /// <summary>
        /// 執行一次呼叫。若成功取回callId
        /// </summary>
        /// <param name="servieID"></param>
        /// <param name="json"></param>
        /// <param name="callId"></param>
        /// <returns></returns>
        public string makeOneCall(string servieID
            , string json, out string callId)
        {
            string ret = "";
            callId = callExe.genCallId();
            //clsMainLoop cml = new clsMainLoop(callId);
            //string json = jsonUtl.encodeJson(cml);
            //  makeOneCall
            ret = ce.MakeAcall(callId, servieID 
                //reflectionUtl.TypeName<clsMainLoop>()
                , json);
            return ret;
        }
        /// <summary>
        /// 由callId取得執行回傳
        /// </summary>
        /// <param name="callId"></param>
        /// <param name="waitTillDone"></param>
        /// <param name="retJson"></param>
        public string callReturn(string callId
            , bool waitTillDone, out string retJson)
        {
            string ret = "";
            retJson = "";
            // todo !!... callReturn
            return ret;
        }
        /// <summary>
        /// 取得所有進行中呼叫清單
        /// </summary>
        /// <param name="allCalls"></param>
        /// <returns></returns>
        public string callsInprogress(
            out List<clsCallBase> allCalls)
        {
            string ret = "";
            allCalls = null;
            //todo !!... callsInprogress
            return ret;
        }
        /// <summary>
        /// 取得完成呼叫的年月清單
        /// </summary>
        /// <param name="doneCallyyyyMMs"></param>
        /// <returns></returns>
        public string callsDoneYYYYMMs(
            out List<string> doneCallyyyyMMs)
        {
            string ret = "";
            doneCallyyyyMMs = null;
            //todo !!... callsDoneYYYYMMs
            return ret;
        }
        /// <summary>
        /// 完成呼叫由年月取得日清單
        /// </summary>
        /// <param name="doneCall1yyyyMM"></param>
        /// <param name="doneCalldays"></param>
        /// <returns></returns>
        public string callsDoneDays(string doneCall1yyyyMM,
            out List<string> doneCalldays)
        {
            string ret = "";
            doneCalldays = null;
            //todo !!... callsDoneDays
            return ret;
        }
        /// <summary>
        /// 完成呼叫由日取得完成呼叫清單
        /// </summary>
        /// <param name="doneCallDay"></param>
        /// <param name="doneCalls"></param>
        /// <returns></returns>
        public string callsDoneList(string doneCallDay,
            out List<clsCallBase> doneCalls)
        {
            string ret = "";
            doneCalls = null;
            //todo !!... callsDoneList
            return ret;
        }
    }
}

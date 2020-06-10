using callMission;
using callMission.calls;
using commonLib;
using modelsfwk.calls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace exeMission
{
    public class mainClass
    {
        protected clsMainLoop cml = null;
        protected callExe ce = null;
        protected Dictionary<string, clsCallStatus> calls = null;
        protected Dictionary<string, Thread> callThreads = null;

        public mainClass(clsMainLoop cml)
        {
            this.cml = cml;
            ce = new callExe();
            //calls = new Dictionary<string, clsCallBase>();
            killInprogressCalls();
        }
        public string startLooping()
        {
            string ret = "";
            ret = theMainLoop( ce.CALL_PATH);
            return ret;
        }
        /// <summary>
        /// kill all in progress call except myself
        /// </summary>
        /// <returns></returns>
        public string killInprogressCalls()
        {
            string ret = "";
            //起來，除了自己之外都先清除
            //所以若再run 一個console, 應該會砍掉前一個
            calls = new Dictionary<string, clsCallStatus>();
            callThreads = new Dictionary<string, Thread>();
            ret = ce.DeleteAllCalls(cml.callId);
            return ret;
        }
        /// <summary>
        /// the main loop
        /// </summary>
        /// <param name="CALL_PATH"></param>
        /// <returns></returns>
        public string theMainLoop(string CALL_PATH)
        {
            //const string CALL_PATH = @"C:\Users\maoch\Desktop\temp\git\planAndTest\planAndTest\exeMission\Data\calls\";
            string ret = "";
            dbg d = new dbg();

            // check thread status, if stopped, move to done
            foreach(KeyValuePair<string, Thread> pair
                in callThreads)
            {
                Thread theThread = pair.Value;
                if (theThread.ThreadState==ThreadState.Stopped)
                {
                    clsCallStatus ccb = null;
                    if (!calls.TryGetValue(pair.Key, out ccb))
                        throw new Exception(
                            "cannot find ccb in collection");
                    string retCallId;
                    ret = ce.ReturnAcall(pair.Key, ccb.returnPara
                        , out retCallId);
                    if (ret.Length > 0) return ret;
                    // the way to get return json
                    calls.Remove(pair.Key);
                    callThreads.Remove(pair.Key);
                }
            }

            //keep looping，不斷去檢查calls目錄有沒有新目錄
            //若有的話，spawn new thread去計算
            //one service call done to call ReturnAcall
            List<string> allCallsUndone = null;
            bool hasMyself = true;
            while (hasMyself )// keep looping for 
            {
                // find all calls undone
                allCallsUndone = ce.allCallsInprogress(
                    cml.callId);

                // loop through the active running calls
                // to find a new call
                hasMyself = false;
                foreach (string callId in allCallsUndone)
                {
                    if (callId == cml.callId)
                    {
                        hasMyself = true;
                        continue;
                    }
                    else if (calls.ContainsKey(callId))
                        continue;
                    // get service name, json by callId
                    string json="";
                    ret = ce.callId2json(callId, out json);
                    if (ret.Length > 0) return ret;
                    clsCallStatus ccb = jsonUtl.decodeJson<
                        clsCallStatus>(json);
                    string serviceName = ccb.serviceName;

                    // execute the new call
                    Thread newCallThread = new Thread(()=>
                        thread1call(callId, serviceName, ref ccb));
                    newCallThread.Start();
                    calls.Add(callId, ccb);
                    callThreads.Add(callId, newCallThread);
                }

                Thread.Sleep(50);//loop once sleep 50ms
            }
            return ret;
        }
        /// <summary>
        /// thread activate content
        /// </summary>
        /// <param name="callId"></param>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        private static string thread1call(string callId
            , string serviceName, ref clsCallStatus ccb)// string callJson)
        {
            string retJson = "";
            string callJson = jsonUtl.encodeJson(ccb);
            retJson =
                invokeService.run(callId, "", serviceName, "", 
                callJson, out retJson);
            ccb.returnPara = retJson;
            ccb.returnTime = DateTime.Now;

            // when done, use callId, notify caller
            //to move to done dir, with returnJson
            return retJson;
        }
    }
}

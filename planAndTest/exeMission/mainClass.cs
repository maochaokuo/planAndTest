using callMission;
using callMission.calls;
using commonLib;
using models.calls;
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
        protected Dictionary<string, clsCallBase> calls = null;
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
            //todo !!... killInprogressCalls
            //起來，除了自己之外都先清除
            //所以若再run 一個console, 應該會砍掉前一個
            calls = new Dictionary<string, clsCallBase>();
            callThreads = new Dictionary<string, Thread>();
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
            string toOutput;

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
                    clsCallBase ccb = jsonUtl.decodeJson<
                        clsCallBase>(json);
                    string serviceName = ccb.serviceName;

                    // execute the new call
                    Thread newCallThread = new Thread(()=>
                        thread1call(callId, serviceName, json));
                    newCallThread.Start();
                    calls.Add(callId, ccb);
                    callThreads.Add(callId, newCallThread);

                    //undone !!... 做完的怎麼辦呢？自己搬到完成且今天目錄

                }
                //string tDir = fileUtl.pb(
                //        CALL_PATH, inputObj.callTs);
                //toOutput = "tDir=" + tDir;
                //d.ot(toOutput);
                //if (!fileUtl.dirExists(tDir))
                //    break;
                //toOutput = "datetime:" + DateTime.Now.ToString();
                //d.ot(toOutput);
                //Console.WriteLine(toOutput);

                Thread.Sleep(50);//
            }
            return ret;
        }
        /// <summary>
        /// 要被用thread方式叫起
        /// </summary>
        /// <param name="callId"></param>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        private static string thread1call(string callId
            , string serviceName, string json )
        {
            string ret = "";
            invokeService.run(serviceName);
            //todo !!... thread1call
            
            // when done, use callId, notify caller
            //to move to done dir, with returnJson
            return ret;
        }
    }
}

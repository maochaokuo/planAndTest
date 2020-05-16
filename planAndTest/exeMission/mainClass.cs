using callMission;
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
        protected callExe ce = new callExe();

        public mainClass(clsMainLoop cml)
        {
            this.cml = cml;
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
            //todo !!...killInprogressCalls
            //起來，除了自己之外都先清除
            //所以若再run 一個console, 應該會砍掉前一個
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
            for (; ; )
            {
                string tDir = fileUtl.pb(
                        CALL_PATH, inputObj.callTs);
                toOutput = "tDir=" + tDir;
                d.ot(toOutput);
                if (!fileUtl.dirExists(tDir))
                    break;
                toOutput = "datetime:" + DateTime.Now.ToString();
                d.ot(toOutput);
                Console.WriteLine(toOutput);
                Thread.Sleep(2000);
            }
            return ret;
        }
    }
}

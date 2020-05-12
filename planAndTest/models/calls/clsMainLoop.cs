using commonLib;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace models.calls
{
    public class clsMainLoop : clsCallBase
    {
        public clsMainLoop()
        {
            serviceName = this.GetType().Name;
            callTypeName = "clsMainLoopInput";
            returnTypeName = "string";
            returnPara = string.Format(@"done at {0}"
, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
        }
        public string theMainLoop(clsMainLoopInput inputObj)
        {
            const string CALL_PATH = @"C:\Users\maoch\Desktop\temp\git\planAndTest\planAndTest\exeMission\Data\calls\";
            string ret = "";
            dbg d = new dbg(CALL_PATH);
            string toOutput;
            for (; ; )
            {
                string tDir = fileUtl.pb(
                        CALL_PATH, inputObj.callTs);
                toOutput = "tDir=" + tDir;
                d.output(toOutput);
                if (!fileUtl.dirExists(tDir))
                    break;
                toOutput = "datetime:" + DateTime.Now.ToString();
                d.output(toOutput);
                Console.WriteLine(toOutput);
                Thread.Sleep(2000);
            }
            return ret;
        }
    }
}

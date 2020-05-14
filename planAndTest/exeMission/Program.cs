using commonLib;
using models.calls;
using System;
using System.IO;

namespace exeMission
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            //StreamWriter sw = new StreamWriter(@"c:\temp\o.t");

            string line = string.Format(@"args num:{0}"
, args.Length);
            dbg d = new dbg(@"C:\Users\maoch\Desktop\temp\git\planAndTest\planAndTest\exeMission\Data\calls\");
            d.output(line);
            for(int i=0; i<args.Length; i++)
            {
                line = string.Format(@"args[{0}]={1}"
, i, args[i]);
                d.output(line);
            }
            //clsMainLoop cml = new clsMainLoop();
            clsMainLoopInput cmli = new clsMainLoopInput();
            cmli.serviceName = args[0];
            cmli.callTs = args[1];
            mainClass mc = new mainClass(cmli);

            string err = mc.startLooping();// cml.theMainLoop(cmli);
            d.output(string.Format(@"exeMission end @{0} with error {1}"
                , DateTime.Now.ToString(), err));
            //todo !!... 現在這已經可以進來了
            //但跑一次就結束，沒法debug
            //應改為keep looping，不斷去檢查calls目錄有沒有新目錄
            //若有的話，spawn new thread去計算
            //one service call done to call ReturnAcall

            //todo !!... console起來，除了自己之外都先清除
            //所以若再run 一個console, 應該會砍掉前一個
        }
    }
}

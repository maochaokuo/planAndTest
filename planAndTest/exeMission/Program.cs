using commonLib;
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
            dbg d = new dbg();
            d.output(line);
            for(int i=0; i<args.Length; i++)
            {
                line = string.Format(@"args[{0}]={1}"
, i, args[i]);
                d.output(line);
            }

            //todo !!... 現在這已經可以進來了
            //但跑一次就結束，沒法debug
            //應改為keep looping，不斷去檢查calls目錄有沒有新目錄
            //若有的話，spawn new thread去計算
            //one service call done to call ReturnAcall
        }
    }
}

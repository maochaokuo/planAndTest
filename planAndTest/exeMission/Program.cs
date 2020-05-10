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
        }
    }
}

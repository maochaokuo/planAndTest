using commonLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exeMission.fwk
{
    class Program
    {
        static void Main(string[] args)
        {

            //            string line = string.Format(@"args num:{0}"
            //, args.Length);
            //            d.output(line);
            //            for(int i=0; i<args.Length; i++)
            //            {
            //                line = string.Format(@"args[{0}]={1}"
            //, i, args[i]);
            //                d.output(line);
            //            }
            //clsMainLoop cml = new clsMainLoop();
            try
            {
                dbg.o("exeMission start");
                string callId;
                if (args.Length > 0)
                {
                    callId = args[0];
                    dbg.o($"callId={callId}");
                    mainClass2 mc2 = new mainClass2(callId);
                    dbg.o($"mainClass2 initialized");
                    string ret = mc2.executeCall();
                    dbg.o($"ret={ret}");
                }
                else
                {
                    dbg.o("callId not specified!");
                }
                dbg.o("exeMission end");
            }
            catch (Exception ex)
            {
                Exception inner = ex;
                while (inner.InnerException != null)
                    inner = inner.InnerException;
                string err = "exception " + inner.Message + "\n" + inner.StackTrace;
                Console.WriteLine(err);
                dbg.o(err);

            }
            //Console.ReadLine();
            //    callId = callExe.genCallId();
            //clsMainLoop cml = new clsMainLoop(callId);
            //cml.callId = callId;
            //if (args.Length >= 2)
            //    cml.serviceName = args[1];
            //else
            //    cml.serviceName = reflectionUtl.TypeName<
            //        clsMainLoop>();
            //mainClass mc = new mainClass(cml);

            //string err = mc.startLooping();// cml.theMainLoop(cmli);
            //d.ot(string.Format(@"exeMission end @{0} with error {1}"
            //    , DateTime.Now.ToString(), err));

        }
    }
}

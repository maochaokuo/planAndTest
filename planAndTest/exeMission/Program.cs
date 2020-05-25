using callMission;
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
                using (var d = new dbg())
                    d.ot("exeMission start");
                string callId;
                if (args.Length > 0)
                {
                    callId = args[0];
                    using (var d = new dbg())
                        d.ot($"callId={callId}");
                    mainClass2 mc2 = new mainClass2(callId);
                    using (var d = new dbg())
                        d.ot($"mainClass2 initialized");
                    string ret = mc2.executeCall();
                    using (var d = new dbg())
                        d.ot($"ret={ret}");
                }
                else
                {
                    Console.WriteLine("callId not specified!");
                }
                using (var d = new dbg())
                    d.ot("exeMission end");
            }
            catch(Exception ex)
            {
                Exception inner = ex;
                while (inner.InnerException != null)
                    inner = inner.InnerException;
                string err = "exception " + inner.Message + "\n" + inner.StackTrace;
                Console.WriteLine(err);
                using (var d = new dbg())
                    d.ot(err);

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

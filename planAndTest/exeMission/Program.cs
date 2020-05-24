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
            dbg d = new dbg();
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
            string callId;
            if (args.Length > 0)
            {
                callId = args[0];
                mainClass2 mc2 = new mainClass2(callId);
                string ret = mc2.executeCall();
            }
            else
            {
                Console.WriteLine("callId not specified!");
            }
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

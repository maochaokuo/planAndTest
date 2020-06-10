using commonLib;
using modelsfwk.calls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace callMission.calls
{
    public class srvHelloTest : srvBase
    {
        public srvHelloTest() : base()
        {
            Thread.Sleep(0);
        }
        //public srvHelloTest(string paraTypeName)
        //    : base(paraTypeName)
        //{
        //    Thread.Sleep(0);
        //}
        /// <summary>
        /// the do call procedure
        /// </summary>
        /// <param name="inputJson">call type in json</param>
        /// <returns>return type in json</returns>
        public override string doCall(string callId, string inputJson)
        {
            //怎麼帶入ClsCallStatusPersistent
            string outputJson;
            //clsHelloTest inOut;
            //inOut = jsonUtl.decodeJson<clsHelloTest>
            //    ( inputJson);
            //outputJson = jsonUtl.encodeJson(inOut);
            for (int i = 1; i <= 7; i++)
                updateProgress(callId, i);
            // to feedback running status
            outputJson = string.Format(
                @"Hello, {0}", inputJson);
            return outputJson;
        }
        private void updateProgress(string callId, int i)
        {
            string sout = $"{i} second(s)";
            ce.addProgress(callId, sout);
            dbg.o(sout);
            Thread.Sleep(999);
        }
    }
}

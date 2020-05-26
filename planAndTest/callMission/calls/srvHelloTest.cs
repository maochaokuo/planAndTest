using commonLib;
using models.calls;
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
        public override string doCall(string inputJson)
        {
            //todo !!... 還是有點麻煩，怎麼帶入ClsCallStatusPersistent
            string outputJson;
            //clsHelloTest inOut;
            //inOut = jsonUtl.decodeJson<clsHelloTest>
            //    ( inputJson);
            outputJson = string.Format(
                @"Hello, {0}", inputJson);
            //outputJson = jsonUtl.encodeJson(inOut);
            Thread.Sleep(999);

            dbg.o("1 second");
            Thread.Sleep(999);
            dbg.o("2 second");
            Thread.Sleep(999);
            dbg.o("3 second");
            Thread.Sleep(999);
            dbg.o("4 second");
            Thread.Sleep(999);
            dbg.o("5 second");
            // todo !!... reconsider here, to feedback running status
            return outputJson;
        }

    }
}

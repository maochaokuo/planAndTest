using System;
using System.Collections.Generic;
using System.Text;

namespace modelsfwk.calls
{
    public class clsHelloTest : clsCallStatus// clsCallBase
    {
        public clsHelloTest(string callId)
            : base(callId)
        {
            //systemName = "system";
            serviceName = this.GetType().Name;// "clsHelloTest";
            callTypeName = "string";
            returnTypeName = "string";
            returnPara = string.Format(@"Hello {0}"
                , callPara);
        }
    }
}

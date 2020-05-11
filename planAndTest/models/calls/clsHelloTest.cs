using System;
using System.Collections.Generic;
using System.Text;

namespace models.calls
{
    public class clsHelloTest : clsCallBase
    {
        public clsHelloTest()
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

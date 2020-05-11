using System;
using System.Collections.Generic;
using System.Text;

namespace models.calls
{
    public class clsMainLoop : clsCallBase
    {
        public clsMainLoop()
        {
            serviceName = this.GetType().Name;
            callTypeName = "clsMainLoopInput";
            returnTypeName = "string";
            returnPara = string.Format(@"done at {0}"
, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
        }
        public string theMainLoop(clsMainLoopInput inputObj)
        {
            string ret = "";


            return ret;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace callMission.calls
{
    public class invokeService
    {
        public static string run(string serviceName
            , string callJson)
        {
            string ret = "";
            object returnVal=null;
            string withNamespace = "callMission.calls."
                + serviceName;
            Type t = Type.GetType(withNamespace);
            MethodInfo method;
            //try
            {
                method = t.GetMethod("doCall");
                object[] param = new[] { (object)callJson };
                returnVal = method.Invoke(t, param);
            }
            //catch (Exception ex)
            //{
            //    ret = ex.Message;
            //}
            //finally
            {
                method = null;
                ((IDisposable)t).Dispose();
                GC.Collect();
            }
            return (string)returnVal;
        }
    }
}

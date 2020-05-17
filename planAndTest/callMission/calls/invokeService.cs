using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace callMission.calls
{
    public class invokeService
    {
        public static string run(string serviceName)
        {
            string ret = "";
            string withNamespace = "callMission.calls."
                + serviceName;
            Type t = Type.GetType(withNamespace);
            MethodInfo method
                = t.GetMethod("doCall");
            object[] param = new[] { (object)serviceName };
            object returnVal = method.Invoke(t, param);
            method = null;
            ((IDisposable)t).Dispose();
            GC.Collect();
            return ret;
        }
    }
}

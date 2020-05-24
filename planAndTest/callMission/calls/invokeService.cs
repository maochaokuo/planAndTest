#define RELEASE

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace callMission.calls
{
    public class invokeService
    {
        public static string run(string systemName, string serviceName
            , string methodName, string callJson, out string returnJson)
        {
            string ret = "";
            object returnVal=null;
            returnJson = "";
            if (string.IsNullOrWhiteSpace(systemName))
                systemName = "calls";
            string withNamespace = $"callMission.{systemName}."
                + serviceName;
            Type t = Type.GetType(withNamespace);
            MethodInfo method;
#if RELEASE
            try
#endif //RELEASE
            {
                if (string.IsNullOrWhiteSpace(methodName))
                    methodName = "doCall";
                method = t.GetMethod(methodName);// "doCall");
                object[] param = new[] { (object)callJson };
                returnVal = method.Invoke(t, param);
                returnJson = (string)returnVal;
            }
#if RELEASE
            catch (Exception ex)
            {
                ret = ex.Message;
            }
            finally
#endif //RELEASE
            {
                method = null;
                ((IDisposable)t).Dispose();
                GC.Collect();
            }
            return ret;
        }
    }
}

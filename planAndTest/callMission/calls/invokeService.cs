//#define RELEASE

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace callMission.calls
{
    public class invokeService
    {
        public static string run<T>(string systemName, string serviceName
            , string methodName, string callJson, out string returnJson)
        {
            string ret = "";
            T t1 = default(T);
            Type objtype = null;
            Object returnVal =null;
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
                t1 = System.Activator.CreateInstance<T>();
                objtype = typeof(T);
                if (string.IsNullOrWhiteSpace(methodName))
                    methodName = "doCall";
                //method = t.GetMethod(methodName);// "doCall");
                method = objtype.GetMethod(methodName);
                //Type objtype = typeof(t);
                Object[] param = new Object[]{ (Object)callJson };
                //String[] param = new[] { (String)callJson };
                //returnVal = method.Invoke(t, param);
                returnVal = method.Invoke(t1, param);

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

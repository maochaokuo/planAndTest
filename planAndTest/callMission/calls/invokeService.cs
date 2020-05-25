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
            //T t1 = default(T);
            //Type objtype = null;
            Object returnVal =null;
            returnJson = "";
            if (string.IsNullOrWhiteSpace(systemName))
                systemName = "calls";
            string withNamespace = $"callMission.{systemName}."
                + serviceName;
            Type ObjType = Type.GetType(withNamespace);
            MethodInfo magicMethod;
#if RELEASE
            try
#endif //RELEASE
            {
                var FacOnj = System.Activator.CreateInstance(ObjType);
                //t1 = System.Activator.CreateInstance<T>();
                //objtype = typeof(T);
                if (string.IsNullOrWhiteSpace(methodName))
                    methodName = "doCall";
                //method = t.GetMethod(methodName);// "doCall");
                magicMethod = ObjType.GetMethod(methodName);
                //Type objtype = typeof(t);
                Object[] param = new Object[]{ (Object)callJson };
                //String[] param = new[] { (String)callJson };
                //returnVal = method.Invoke(t, param);
                returnVal = magicMethod.Invoke(FacOnj, param);

                returnJson = (string)returnVal;
                ((IDisposable)FacOnj).Dispose();
                GC.Collect();
                magicMethod = null;
            }
#if RELEASE
            catch (Exception ex)
            {
                ret = ex.Message;
            }
            finally
#endif //RELEASE
            {
            }
            return ret;
        }
    }
}

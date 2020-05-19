using System;
using System.Collections.Generic;
using System.Text;

namespace commonLib
{
    public class reflectionUtl
    {
        public static string TypeName<T>()
        {
            string ret = "";
            ret = typeof(T).Name;
            //ret = typeof(T).FullName;
            return ret;
        }
        //public static string defaultSanitizer<T>(T record)
        //{
        //    string ret = "";
        //    //Record record = new Record();

        //    PropertyInfo[] properties = typeof(T).GetProperties();
        //    foreach (PropertyInfo property in properties)
        //    {
        //        Type tp = property.PropertyType;
        //        string tyName = tp.Name;
        //        if (tyName == "String")
        //        {
        //            string name = property.Name;
        //            String objStr = property.GetValue(record) + "";
        //            //objStr = Sanitizer.GetSafeHtmlFragment(objStr);
        //            //objStr =System.Web.Security.AntiXss.AntiXssEncoder.HtmlEncode(objStr, false);
        //            try
        //            {
        //                property.SetValue(record, objStr);
        //            }
        //            catch (Exception ex)
        //            {
        //                Exception innerEx = ex;
        //                while (innerEx.InnerException != null)
        //                    innerEx = innerEx.InnerException;
        //                LogHelper lh = new LogHelper("Verify Helper checkDTyyyyMMddHHmmss");
        //                lh.Error(innerEx.Message);
        //            }
        //        }
        //    }
        //    return ret;
        //}
        //public ReturnStd doInvoke<T, T2>(T2 enu, Object[] param)
        //{
        //    T t = default(T);
        //    T2 t2 = default(T2);
        //    string name = "";
        //    Type ObjType = null;
        //    try
        //    {
        //        t = System.Activator.CreateInstance<T>();
        //        t2 = System.Activator.CreateInstance<T2>();
        //        name = Enum.GetName(t2.GetType(), enu);
        //        ObjType = typeof(T);
        //        MethodInfo magicMethod = ObjType.GetMethod(name);
        //        object magicValue = magicMethod.Invoke(t, param);
        //        //清除物件
        //        magicMethod = null;
        //        ((IDisposable)t).Dispose();

        //        GC.Collect();
        //        return (ReturnStd)magicValue;
        //    }
        //    catch (Exception ex)
        //    {
        //        string msg = ex.Message;
        //        Exception innerEx = ex;
        //        while (innerEx.InnerException != null)
        //            innerEx = innerEx.InnerException;
        //        ReturnStd rtn = new ReturnStd(false, innerEx.Message);
        //        clsOPData OPdata = new clsOPData();
        //        OPdata.OplogDesc = string.Format("Class:{0} | Method:{1} | 描述:{2}", ObjType.ToString(), name, "發生例外錯誤");
        //        OPdata.Add("錯誤訊息:" + innerEx.Message);
        //        rtn.SetValue("OPLog", OPdata);
        //        return rtn;
        //    }
        //}
        //public ApiResult<object> doAPIInvoke<T, T2>(T2 enu, Object[] param)
        //{
        //    T t = default(T);
        //    T2 t2 = default(T2);
        //    string name = "";
        //    Type ObjType = null;
        //    try
        //    {
        //        t = System.Activator.CreateInstance<T>();
        //        t2 = System.Activator.CreateInstance<T2>();
        //        name = Enum.GetName(t2.GetType(), enu);
        //        ObjType = typeof(T);
        //        MethodInfo magicMethod = ObjType.GetMethod(name);
        //        object magicValue = magicMethod.Invoke(t, param);
        //        //清除物件
        //        magicMethod = null;
        //        ((IDisposable)t).Dispose();
        //        GC.Collect();
        //        return (ApiResult<object>)magicValue;
        //    }
        //    catch (Exception ex)
        //    {
        //        string msg = ex.Message;

        //        OP_LOG oplog = new OP_LOG(new OPUserInfo() { CardNo = "", FunctionCode = "", FunctionGroup = "", JobFrom = "", OperatorID = "", UserID = "" });

        //        clsOPData OPdata = new clsOPData();
        //        OPdata.OplogDesc = string.Format("Class:{0} | Method:{1} | 描述:{2}", ObjType.ToString(), name, "發生例外錯誤");
        //        OPdata.Add("錯誤訊息:" + ex.Message);

        //        oplog.ADD(OPdata);
        //        oplog.Status = StatusEnum.FailException;
        //        WriteOplog<OP_LOG>(oplog, false);

        //        return new ApiResult<object>(false, ex.Message);
        //    }
        //}
    }
}

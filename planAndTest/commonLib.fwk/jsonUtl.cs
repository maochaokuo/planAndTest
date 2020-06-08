using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace commonLib
{
    public class jsonUtl
    {
        /// <summary>
        /// Json編碼
        /// </summary>
        /// <param name="jsonObj"></param>
        /// <returns></returns>
        public static string encodeJson(object jsonObj)
        {
            return JsonConvert.SerializeObject(jsonObj);
        }
        /// <summary>
        /// 將json針對型態T做轉型，若失敗丟exception
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T decodeJson<T>(string json)
        {
            //JsonSerializerSettings settings = new JsonSerializerSettings();
            //settings.MissingMemberHandling = MissingMemberHandling.Error;

            //T ret = JsonConvert.DeserializeObject<T>(json, settings);
            T ret = JsonConvert.DeserializeObject<T>(json);
            if (ret == null)
                throw new Exception("decodeJson fail: " + typeof(T).FullName);
            return ret;
        }
        public static string encodeXML<T>(T MyObject)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(T));
            //var subReq = new MyObject();
            string xml = "";

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, MyObject);
                    xml = sww.ToString(); // Your XML
                }
            }
            return xml;
        }
    }
}

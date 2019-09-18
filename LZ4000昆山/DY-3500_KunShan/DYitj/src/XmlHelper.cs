using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace AIO.src
{
    public static class XmlHelper
    {
        /// <summary>
        /// 实体类转换成XML格式字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string EntityToXml<T>(T obj)
        {
            using (StringWriter sw = new StringWriter())
            {
                Type t = obj.GetType();
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(sw, obj);
                sw.Close();
                return Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(sw.ToString().Replace("utf-16", "utf-8")));
            }
        }

        /// <summary>
        /// XML字符串转实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strXML"></param>
        /// <returns></returns>
        public static T XmlToEntity<T>(string strXML) where T : class
        {
            try
            {
                using (StringReader sr = new StringReader(strXML))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    return serializer.Deserialize(sr) as T;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}

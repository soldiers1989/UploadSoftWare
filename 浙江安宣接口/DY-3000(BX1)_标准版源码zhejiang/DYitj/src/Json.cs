using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using DYSeriesDataSet.DataModel.Wisdom;

namespace AIO.src
{
    /// <summary>
    /// JSON 操作类 WenJ 2016年4月14日
    /// </summary>
    public class Json
    {

        /// <summary>
        /// 浙江数据上传
        /// </summary>
     
        public string resultCode { get; set; }
        public string message { get; set; }
        public string type { get; set; }
        public ItemDetail[] datalist { get; set; }
     
        public class ItemDetail
        {
            public string itemname { get; set; }
            public string itemcode { get; set; }

            public string str()
            {
                return itemname +","+ itemcode;
            }
        }
        // List转成json  ListToJson(IList<T> list)
        // 对象转换为Json  ObjectToJson(object jsonObject)

        /// <summary>  
        /// List转成json   
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="list"></param>  
        /// <returns></returns>  
        public static string ListToJson<T>(IList<T> list)
        {
            object obj = list[0];
            return ListToJson<T>(list, obj.GetType().Name);
        }

        private static string ListToJson<T>(IList<T> list, string jsonName)
        {
            StringBuilder Json = new StringBuilder();
            if (string.IsNullOrEmpty(jsonName))
                jsonName = list[0].GetType().Name;
            Json.Append("{\"" + jsonName + "\":[");
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    T obj = Activator.CreateInstance<T>();
                    PropertyInfo[] pi = obj.GetType().GetProperties();
                    Json.Append("{");
                    for (int j = 0; j < pi.Length; j++)
                    {
                        Type type;
                        object o = pi[j].GetValue(list[i], null);
                        string v = string.Empty;
                        if (o != null)
                        {
                            type = o.GetType();
                            v = o.ToString();
                        }
                        else
                        {
                            type = typeof(string);
                        }

                        Json.Append("\"" + pi[j].Name.ToString() + "\":" + StringFormat(v, type));

                        if (j < pi.Length - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < list.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]}");
            return Json.ToString();
        }

        /// <summary>   
        /// 对象转换为Json字符串   
        /// </summary>   
        /// <param name="jsonObject">对象</param>   
        /// <returns>Json字符串</returns>   
        public static string ObjectToJson(object jsonObject)
        {
            bool isList = false;
            try
            {
                StringBuilder jsonString = new StringBuilder();
                jsonString.Append("{");
                PropertyInfo[] propertyInfo = jsonObject.GetType().GetProperties();
                for (int i = 0; i < propertyInfo.Length; i++)
                {
                    object objectValue = propertyInfo[i].GetGetMethod().Invoke(jsonObject, null);
                    if (objectValue == null)
                    {
                        continue;
                    }
                    StringBuilder value = new StringBuilder();
                    if (objectValue is DateTime || objectValue is Guid || objectValue is TimeSpan)
                    {
                        value.Append("\"" + objectValue.ToString() + "\"");
                    }
                    else if (objectValue is string)
                    {
                        value.Append("\"" + objectValue.ToString() + "\"");
                    }
                    else if (objectValue is IEnumerable)
                    {
                        if (Wisdom.ISUPLOADSAMPLE)
                        {
                            isList = true;
                            uploadSample.Request details = new uploadSample.Request();
                            details.detailsList = (List<uploadSample.Request.details>)objectValue;
                            value.Append(ListToJson(details.detailsList));
                        }
                        else if (Wisdom.ISUPLOADRESULT)
                        {
                            isList = true;
                            uploadResult.Request details = new uploadResult.Request();
                            details.detailsList = (List<uploadResult.Request.details>)objectValue;
                            value.Append(ListToJson(details.detailsList));
                        }
                        else
                        {
                            value.Append(ObjectToJson((IEnumerable)objectValue));
                        }
                        value.Remove(0, 1);
                        value.Remove(value.Length - 1, 1);
                    }
                    else
                    {
                        value.Append("\"" + objectValue.ToString() + "\"");
                    }
                    if (isList)
                        jsonString.Append(value + ",");
                    else
                        jsonString.Append("\"" + propertyInfo[i].Name + "\":" + value + ",");
                    isList = false;
                }
                return jsonString.ToString().TrimEnd(',') + "}";
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>  
        /// 格式化字符型、日期型、布尔型  
        /// </summary>  
        /// <param name="str"></param>  
        /// <param name="type"></param>  
        /// <returns></returns>  
        private static string StringFormat(string str, Type type)
        {
            if (type != typeof(string) && string.IsNullOrEmpty(str))
            {
                str = "\"" + str + "\"";
            }
            else if (type == typeof(string))
            {
                str = String2Json(str);
                str = "\"" + str + "\"";
            }
            else if (type == typeof(DateTime))
            {
                str = "\"" + str + "\"";
            }
            else if (type == typeof(bool))
            {
                str = str.ToLower();
            }
            else if (type == typeof(byte[]))
            {
                str = "\"" + str + "\"";
            }
            else if (type == typeof(Guid))
            {
                str = "\"" + str + "\"";
            }
            return str;
        }

        /// <summary>  
        /// 过滤特殊字符  
        /// </summary>  
        /// <param name="s"></param>  
        /// <returns></returns>  
        private static string String2Json(String s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s.ToCharArray()[i];
                switch (c)
                {
                    case '\"':
                        sb.Append("\\\""); break;
                    case '\\':
                        sb.Append("\\\\"); break;
                    case '/':
                        sb.Append("\\/"); break;
                    case '\b':
                        sb.Append("\\b"); break;
                    case '\f':
                        sb.Append("\\f"); break;
                    case '\n':
                        sb.Append("\\n"); break;
                    case '\r':
                        sb.Append("\\r"); break;
                    case '\t':
                        sb.Append("\\t"); break;
                    case '\v':
                        sb.Append("\\v"); break;
                    case '\0':
                        sb.Append("\\0"); break;
                    default:
                        sb.Append(c); break;
                }
            }
            return sb.ToString();
        }

    }
}

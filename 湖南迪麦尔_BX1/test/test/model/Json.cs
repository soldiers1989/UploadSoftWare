using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Newtonsoft.Json;

namespace test.model
{
    /// <summary>
    /// JSON 操作类 WenJ 2016年4月14日
    /// </summary>
    public class Json
    {
        /// <summary>
        /// json字符串转entity
        /// </summary>
        /// <typeparam name="T">entity</typeparam>
        /// <param name="jsonString">json字符串</param>
        /// <returns>返回转换后的entity</returns>
        public static T JsonToEntity<T>(string jsonString)
        {
            return (T)JsonConvert.DeserializeObject(jsonString, typeof(T));
        }

        public static string EntityToJson(object obj)
        {
            string strJson = JsonConvert.SerializeObject(obj);
            return strJson;
        }
 
    }
}

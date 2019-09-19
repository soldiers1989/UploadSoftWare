using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test
{
    public class JsonClass
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



        public static object jsonto = new object();
        /// <summary>
        /// entity转json字符串
        /// </summary>
        /// <param name="T">entity</param>
        /// <returns>返回转换后的json字符串</returns>
        public static string EntityToJson(object T)
        {
            lock (jsonto)
            {
                return JsonConvert.SerializeObject(T);
            }

        }
    }
}

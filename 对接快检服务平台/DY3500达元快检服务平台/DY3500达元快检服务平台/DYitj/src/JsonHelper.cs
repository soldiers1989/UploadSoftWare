﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace AIO.src
{
    /// <summary>
    /// json辅助操作类
    /// Create wenj
    /// Time 2017年9月4日
    /// </summary>
    public static class JsonHelper
    {
        public static object tojson = new object();

        /// <summary>
        /// json字符串转entity
        /// </summary>
        /// <typeparam name="T">entity</typeparam>
        /// <param name="jsonString">json字符串</param>
        /// <returns>返回转换后的entity</returns>
        public static T JsonToEntity<T>(string jsonString)
        {
            lock (tojson)
            {
                return (T)JsonConvert.DeserializeObject(jsonString, typeof(T));
            }
            
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

        /// <summary>
        /// 获取json字符串中的指定key的值
        /// </summary>
        /// <param name="jsonString">json字符串</param>
        /// <param name="key">key</param>
        /// <returns>返回值</returns>
        public static string GetJsonValueByKey(string jsonString, string key)
        {
            string rtn = null;
            try
            {
                rtn = JObject.Parse(jsonString)[key].ToString();
            }
            catch (Exception)
            {
                return null;
            }
            return rtn;
        }

     

    }
}
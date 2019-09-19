using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WorkstationModel.UpData
{
    /// <summary>
    /// 下载基础数据
    /// </summary>
    public  class clsDownExamedUnit
    {
        /// <summary>
        /// 获取ResultMsg
        /// </summary>
        /// <param name="json"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static ResultMsg GetJsonResult(string json)
        {
            try
            {
                return (ResultMsg)JsonConvert.DeserializeObject(json, typeof(ResultMsg));
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

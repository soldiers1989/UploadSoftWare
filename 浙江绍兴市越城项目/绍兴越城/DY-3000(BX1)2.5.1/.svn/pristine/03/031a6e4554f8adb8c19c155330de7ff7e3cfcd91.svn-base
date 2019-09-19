using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DYSeriesDataSet.DataSentence.yc
{
    public  class getUrl
    {
        /// <summary>
        /// 上传接口地址
        /// </summary>
        /// <param name="url"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetUrl(string url, int type)
        {
            string rtn = "";
            if (type == 1)
            {
                if (url.Substring(url.Length - 1, 1) == "/")
                {
                    rtn = url + "checkCompany/getMarketInfo";
                }
                else
                {
                    rtn = url + "/checkCompany/getMarketInfo";
                }
            }
            else if (type == 2)
            {
                if (url.Substring(url.Length - 1, 1) == "/")
                {
                    rtn = url + "checkCompany/getOperatorInfo";
                }
                else
                {
                    rtn = url + "/checkCompany/getOperatorInfo";
                }
            }
            else if (type == 3)
            {
                if (url.Substring(url.Length - 1, 1) == "/")
                {
                    rtn = url + "checkCompany/getGoodsInfo";
                }
                else
                {
                    rtn = url + "/checkCompany/getGoodsInfo";
                }
            }
            else if (type == 4)
            {
                if (url.Substring(url.Length - 1, 1) == "/")
                {
                    rtn = url + "checkCompany/getCheckItemInfo";
                }
                else
                {
                    rtn = url + "/checkCompany/getCheckItemInfo";
                }
            }
            else if (type == 5)
            {
                string c = url.Substring(url.Length - 1, 1);
                if (url.Substring(url.Length-1 ,1)=="/")
                {
                    rtn = url + "checkCompany/updateDeviceInfo";
                }
                else
                {
                    rtn = url + "/checkCompany/updateDeviceInfo";
                }
            }
            else if (type == 6)
            {
                if (url.Substring(url.Length - 1, 1) == "/")
                {
                    rtn = url + "checkCompany/uploadCheckData";
                }
                else
                {
                    rtn = url + "/checkCompany/uploadCheckData";
                }
            }
            return rtn;
        }
    }
}

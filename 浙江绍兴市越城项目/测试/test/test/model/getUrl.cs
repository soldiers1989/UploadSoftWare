using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace test.model
{
    public  class getUrl
    {
        public static string GetUrl(string url,int type)
        {
            string rtn = "";
            if(type ==1)
            {
                if (url.LastIndexOf("/") > 0)
                {
                    rtn = url + "checkCompany/getMarketInfo";
                }
                else
                {
                    rtn = url + "/checkCompany/getMarketInfo";
                }
            }
            else if(type ==2)
            {
                if (url.LastIndexOf("/") > 0)
                {
                    rtn = url + "checkCompany/getOperatorInfo";
                }
                else
                {
                    rtn = url + "checkCompany/getOperatorInfo";
                }
            }
            else if (type == 3)
            {
                if (url.LastIndexOf("/") > 0)
                {
                    rtn = url + "checkCompany/getGoodsInfo";
                }
                else
                {
                    rtn = url + "checkCompany/getGoodsInfo";
                }
            }
            else if (type == 4)
            {
                if (url.LastIndexOf("/") > 0)
                {
                    rtn = url + "checkCompany/getCheckItemInfo";
                }
                else
                {
                    rtn = url + "checkCompany/getCheckItemInfo";
                }
            }
            else if (type == 5)
            {
                if (url.LastIndexOf("/") > 0)
                {
                    rtn = url + "checkCompany/updateDeviceInfo";
                }
                else
                {
                    rtn = url + "checkCompany/updateDeviceInfo";
                }
            }
            else if (type == 6)
            {
                if (url.LastIndexOf("/") > 0)
                {
                    rtn = url + "checkCompany/uploadCheckData";
                }
                else
                {
                    rtn = url + "checkCompany/uploadCheckData";
                }
            }
            return rtn;
        }
    }
}

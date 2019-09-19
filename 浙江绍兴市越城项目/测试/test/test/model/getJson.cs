using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace test.model
{
    public class getJson
    {
        public static string getMarketInfo()
        {
            string rtn = "";
            Users u = new Users();
            u.thirdCompanyName = "国研科技有限公司";
            u.thirdCompanyCode = "1";
            rtn= EntityToJson(u);
            return rtn;
        }
        public static string getOperator()
        {
            string rtn = "";
            operate u = new operate();
            u.marketCode = "3";//获取市场返回的编号
            u.thirdCompanyName = "国研科技有限公司";
            u.thirdCompanyCode = "1";
            rtn = EntityToJson(u);
            return rtn;
        }

        public static string updateDevice()
        {
            string rtn = "";

            updateDeviceInfo u = new updateDeviceInfo();
            u.Count  = "1";//获取市场返回的编号
            u.thirdCompanyName = "国研科技有限公司";
            u.thirdCompanyCode = "1";
            List<devices> dd = new List<devices>();
            devices d = new devices();
            d.type = "DY3500";
            d.deviceId = "dy35000001";
            d.factory = "广东达元绿洲食品药品安全科技股份有限公司";
            dd.Add(d);
            u.device = dd;

            rtn = EntityToJson(u);
            return rtn;
        }
        /// <summary>
        /// 上传数据
        /// </summary>
        /// <returns></returns>
        public static string uploadData()
        {
            string rtn = "";
            uploadData u = new uploadData();
            u.thirdCompanyName = "国研科技有限公司";
            u.thirdCompanyCode = "1";
            u.checkCount = "1";

            List<checkInfo> cks = new List<checkInfo>();

            checkInfo ck = new checkInfo();
            ck.amount = "1";
            ck.uuid = "20181109115512";
            ck.deviceId = "dy35000001";
            ck.marketName = "袍江农贸市场";
            ck.operatorName = "毛玉兰";
            //ck.operatorCode = "1";
            ck.stallNo = "02";//必填
            ck.checkDate = "2018-11-09 10:00:01";
            ck.goodsName = "菜豆";
            //ck.amount = "1";
            //ck.unite = "克";
            ck.itemType = "甲醛残留1";
            ck.itemName = "甲醛残留1";
            ck.itemCode = "2";
            ck.value = "阳性";
            ck.checkResult = "不合格";
            cks.Add(ck);
            u.checkInfo = cks;
            rtn = EntityToJson(u);

            return rtn;
        }

        /// <summary>
        /// entity转json字符串
        /// </summary>
        /// <param name="T">entity</param>
        /// <returns>返回转换后的json字符串</returns>
        public static string EntityToJson(object T)
        {
            return JsonConvert.SerializeObject(T);
        }
    }
}


using GPS.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPS.GPSModel
{
     public  class UpHeat
    {

        /// <summary>
        /// 更新心跳包
        /// </summary>
        public static bool UpHeartBeat(string types)
        {
            bool uprtn = false;
            try
            {
                //有网络时
                Global.getFilePath();
                if (Global.IsConnectInternet())
                {
                    string x = "";
                    string y = "";
                    AddressInfo address = AddressInfo.GetAddressByBaiduAPI();
                    if (address != null)
                    {
                        x = address.content.point.x; 
                        y = address.content.point.y;
                    }
                    if(Global.lat !="" && Global.lon !="")
                    {
                        x = Global.lat;
                        y = Global.lon;
                    }
                    string url =Global.ServerAddr ;
                    int index = url.LastIndexOf('/');
                    url += (index == url.Length - 1) ? "iLog/uploadStatus.do" : "iLog/uploadStatus.do";
                    
                    StringBuilder sb = new StringBuilder();
                    string message = AES.Encrypt(Global.FactoryNumber, "DYXX@)!(QPMZA2-5");
                    message = message.Replace('+', '-').Replace('/', '_').Replace("=", "");
                    Console.WriteLine(message);

                    string token = message;
                    heartbeat ht = new heartbeat();
                    ht.status = types;//在线
                    ht.onlineDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    ht.softwareVersion = Global.SoftwareVer ;
                    ht.offlineDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    ht.handwareVersion = Global.HardwareVer ;
                    ht.longitude = x;
                    ht.latitude = y;
                    string json = InterfaceHelper.EntityToJson(ht);
                    sb.AppendFormat("{0}?userToken={1}", url, token);
                    sb.AppendFormat("&results={0}", json);
                    //FileUtils.KLog(sb.ToString(), "发送", 23);
                    string result = InterfaceHelper.HttpsPost(sb.ToString());
                    //FileUtils.KLog(result, "接收", 23);

                    if (result.Contains("msg") || result.Contains("resultCode"))
                    {
                        resultdata rtn = InterfaceHelper.JsonToEntity<resultdata>(result);
                        if (rtn != null)
                        {
                            if (rtn.resultCode == "0X00000")
                            {
                                uprtn = true;
                                Console.WriteLine("数据同步成功！" + rtn.msg);
                            }
                            else
                            {
                                uprtn = false;
                                Console.WriteLine("数据同步失败！失败原因：" + rtn.msg);
                            }
                        }
                        //else
                        //{
                        //    Console.WriteLine("数据同步失败！失败原因：" + result);
                        //}
                    }
                    else
                    {
                        uprtn = false;
                    }
                }
            }
            catch (Exception ex)
            {
                uprtn = false;
            }

            return uprtn;
        }
    }
}

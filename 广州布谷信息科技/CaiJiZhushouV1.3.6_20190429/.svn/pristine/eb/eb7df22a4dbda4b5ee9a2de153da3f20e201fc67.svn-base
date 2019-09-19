using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using WorkstationDAL.Model;
using WorkstationModel.function;

namespace WorkstationModel.UpData
{
    public class ZYUpData
    {
        public static string  UpData(DataTable dt,string url, string user, string pwd)
        {
            
            string responseInfo = "";
            try
            {
                string json = "";
                Result rt = new Result();
                Framsds fras = new Framsds();
                fras.UserName = user;
                fras.UserPassWord = pwd;
                Farmsdata dd = new Farmsdata();
                dd.shebei_code = Global.MachineSerialCode;
                dd.Area = Global.regions;
                dd.LabCode = Global.LaboratoryNum;
                dd.Stations = Global.TestUnitName;
                dd.Manger = Global.NickName;
                dd.Phone = Global.PhotoNum; 
                dd.BoothCode = "1232";
                dd.BoothName = "达元";
                dd.BoothNum = "B10";
                dd.SampleCode = dt.Rows[0]["SampleCode"].ToString();
                dd.SampleName = dt.Rows[0]["SampleName"].ToString();
                dd.DetectionName = dt.Rows[0]["Checkitem"].ToString();
                dd.Grade = "";
                dd.Status = "";
                dd.DetectionValue = dt.Rows[0]["CheckData"].ToString();
                dd.Range = dt.Rows[0]["LimitData"].ToString();
                dd.Result = dt.Rows[0]["Result"].ToString();
                dd.Unit = dt.Rows[0]["Unit"].ToString();
                dd.DetectionDate = dt.Rows[0]["CheckTime"].ToString();
                dd.holeNumber = dt.Rows[0]["HoleNum"].ToString(); 

                List<Farmsdata> framsds = new List<Farmsdata>();
                framsds.Add(dd);
                fras.Farms = framsds;
                rt.FarmsDS = fras;
                json = JsonHelper.EntityToJson(rt);
                FilesRW.KLog(json, "发送", 14);
                responseInfo = HttpPOST(url, json);
                FilesRW.KLog(responseInfo, "接收", 14);
            }
            catch (Exception ex)
            {
                FilesRW.KLog(ex.Message , "数据上传", 14);
            }
            return responseInfo;
        
        }

        public static string HttpPOST(string url, string paramsValue)
        {
            string result = string.Empty;
            try
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(paramsValue);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = byteArray.Length;
                //自定义header
                //CookieContainer cc = new CookieContainer();     //post发送数据携带Cookie身份认证信息
                //cc.Add(new Uri(url), new Cookie("token", PostToken));
                //request.CookieContainer = cc;
                using (Stream newStream = request.GetRequestStream())
                {
                    newStream.Write(byteArray, 0, byteArray.Length); //写入参数 
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();//获取响应

                using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    result = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return result;
        }
    }
}

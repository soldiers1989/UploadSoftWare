using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using WorkstationDAL.Model;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using WorkstationModel.function;
using System.Data;
using WorkstationBLL.Mode;

namespace WorkstationModel.UpData
{
    public class QuickInspectServing
    {
        public static object dtFlag = new object();
        private static StringBuilder sb = new StringBuilder();
        private static clsSetSqlData _bll = new clsSetSqlData();
        public static object urlFlag = new object();

        /// <summary>
        /// 获取服务器url 适用于快检车系统
        /// 1、服务器通讯验证；2、基础数据下载；3、数据上传;4、快检服务登录；5、快检服务检测项目下载
        /// 6、数据上传；7、仪器注册
        /// </summary>
        /// <param name="url">服务器地址</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static string GetServiceURL(string url, int type)
        {
            lock (urlFlag)
            {
                int index = url.LastIndexOf('/');
                //服务器通讯验证
                if (type == 1)
                {
                    url += (index == url.Length - 1) ? "os/java/pub/user/checkUserConnect.shtml" : "/os/java/pub/user/checkUserConnect.shtml";
                }
                //基础数据下载
                else if (type == 2)
                {
                    url += (index == url.Length - 1) ? "os/java/pub/data/downloadBasicData.shtml" : "/os/java/pub/data/downloadBasicData.shtml";
                }
                //数据上传
                else if (type == 3)
                {
                    url += (index == url.Length - 1) ? "os/java/pub/data/upLoadCheckData.shtml" : "/os/java/pub/data/upLoadCheckData.shtml";
                }
                //快检服务平台用户登录
                else if (type == 4)
                {
                    url += (index == url.Length - 1) ? "interfaces/userLogin/login.do" : "/interfaces/userLogin/login.do";
                }
                else if (type == 5)//快检服务平下载检测任务
                {
                    url += (index == url.Length - 1) ? "interfaces/download/basicData.doo" : "/interfaces/download/basicData.do";
                }
                else if (type == 6)//数据上传
                {
                    url += (index == url.Length - 1) ? "interfaces/dataChecking/uploadData.do" : "/interfaces/dataChecking/uploadData.do";
                }
                else if (type == 7)//仪器注册
                {
                    url += (index == url.Length - 1) ? "interfaces/devices/registerDevice.do" : "/interfaces/devices/registerDevice.do";
                }
                else if (type == 8)//食品种类下载
                {
                    url += (index == url.Length - 1) ? "interfaces/download/basicData.do" : "/interfaces/download/basicData.do";
                }
                else if (type == 9)//抽样数量
                {
                    url += (index == url.Length - 1) ? "iSampling/samplingNumber.do" : "/iSampling/samplingNumber.do";
                }
                else if (type == 10)//任务管理数量
                {
                    url += (index == url.Length - 1) ? "interfaces/task/tasksNumber.do" : "/interfaces/task/tasksNumber.do";
                }
                else if (type == 11)//任务管理
                {
                    url += (index == url.Length - 1) ? "interfaces/task/receiveTasks.do" : "/interfaces/task/receiveTasks.do";
                }
                else if (type == 12)//任务管理状态更新
                {
                    url += (index == url.Length - 1) ? "interfaces/task/viewTask.do" : "/interfaces/task/viewTask.do";
                }
                else if (type == 13)//公报消息数量
                {
                    url += (index == url.Length - 1) ? "interfaces/taskMessage/taskMsgNumber.do" : "/interfaces/taskMessage/taskMsgNumber.do";
                }
                else if (type == 14)//公报消息
                {
                    url += (index == url.Length - 1) ? "interfaces/taskMessage/downloadTaskMsg.do" : "/interfaces/taskMessage/downloadTaskMsg.do";
                }
                else if (type == 15)//公报消息状态
                {
                    url += (index == url.Length - 1) ? "interfaces/taskMessage/viewed.do" : "/interfaces/taskMessage/viewed.do";
                }
                else if (type == 16)//抽样任务
                {
                    url += (index == url.Length - 1) ? "iSampling/downloadSampling.do" : "/iSampling/downloadSampling.do";
                }
                return url;
            }
        }
        /// <summary>
        /// 线程加锁
        /// </summary>
        public static object lockpost = new object();
        /// <summary>
        /// POST请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string HttpsPost(string url)
        {
            lock (lockpost)
            {
                string result = string.Empty;
                try
                {
                    HttpWebRequest request = null;

                    if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                    {
                        request = WebRequest.Create(url) as HttpWebRequest;
                        ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                        //request.ProtocolVersion = HttpVersion.Version11;
                        request.ProtocolVersion = HttpVersion.Version10;
                        // 这里设置了协议类型。
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;// (SecurityProtocolType)3072;//SecurityProtocolType.Tls1.2; 
                        request.KeepAlive = false;
                        ServicePointManager.CheckCertificateRevocationList = true;
                        ServicePointManager.DefaultConnectionLimit = 100;
                        ServicePointManager.Expect100Continue = false;
                    }
                    else
                    {
                        request = (HttpWebRequest)WebRequest.Create(url);
                    }

                    request.Method = "POST";    //使用get方式发送数据
                    request.Accept = "*/*";
                    //request.ContentType 
                    request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                    request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
                    request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.91 Safari/537.36";

                    //IDictionary<string, string> parameters = new Dictionary<string, string>();
                    //parameters.Add("userName", "admin");
                    //parameters.Add("password", "F59BD65F7EDAFB087A81D4DCA06C4910");
                    //byte[] data = Encoding.UTF8.GetBytes(builder.ToString());//postdata
                    //Stream newStream = request.GetRequestStream();
                    //newStream.Write(data, 0, data.Length);
                    //newStream.Close();

                    //获取网页响应结果
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream stream = response.GetResponseStream();
                    //client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                    using (StreamReader sr = new StreamReader(stream))
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
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }
        /// <summary>
        /// 快检服务平台用户登录
        /// </summary>
        /// <param name="url"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string QuickTestServerLogin(string url, string user, string pwd, int type)
        {
            lock (dtFlag)
            {
                string err = "";
                string responseInfo = "";
                if(type ==1)
                {
                    try
                    {
                        responseInfo = string.Empty;
                        string uria = GetServiceURL(url, 4);
                        string password = Global.MD5(Global.MD5(pwd));

                        sb.Length = 0;

                        sb.Append(uria);
                        sb.AppendFormat("?{0}={1}", "userName", user);
                        sb.AppendFormat("&{0}={1}", "password", password);
                        sb.AppendFormat("&{0}={1}", "deviceCode", "");
                        sb.AppendFormat("&{0}={1}", "softWareVersion", "DY-3500食品综合分析仪");
                        sb.AppendFormat("&{0}={1}", "place", "");
                        sb.AppendFormat("&{0}={1}", "ip", "");
                        sb.AppendFormat("&{0}={1}", "param1", "");
                        sb.AppendFormat("&{0}={1}", "param2", "");

                        FilesRW.KLog(sb.ToString(), "发送", 1);
                        responseInfo = HttpsPost(sb.ToString());// HttpMath(uria, json, 1, 1);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
               
                
                return responseInfo;
            }
        }
        /// <summary>
        /// 快检服务上传数据
        /// </summary>
        /// <param name="udata"></param>
        /// <param name="url"></param>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static string KJFWUpData(QuickServerResult udata ,string url)
        {
            string responseInfo = "";
            try
            {
                sb.Length = 0;
                sb.Append(url);
                sb.AppendFormat("?userToken={0}", Global.Token);
                string json = JsonHelper.EntityToJson(udata);
                sb.AppendFormat("&result={0}", json);
                FilesRW.KLog(sb.ToString(), "发送", 14);
                responseInfo = HttpsPost(sb.ToString());
                FilesRW.KLog(responseInfo, "接收", 14);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return responseInfo;
        }

        /// <summary>
        /// 上传检测数据
        /// </summary>
        /// <returns></returns>
        public static string UpLoadData(DataTable dt,string url, string user, string pwd)
        {
            string err = "";
            string responseInfo = "";
            try
            {
                responseInfo = string.Empty;
                sb.Length = 0;
                sb.Append(url);
                sb.AppendFormat("?userToken={0}", Global.Token);
                QuickServerResult udata = new QuickServerResult();

                DataTable dts = _bll.GetCompany("b.reg_id=r.rid and r.reg_name = '" + dt.Rows[0]["CheckUnit"].ToString() + "'", "", 0, out err);
                if (dts != null && dts.Rows.Count > 0)
                {
                    udata.regId = dts.Rows[0]["rid"].ToString();//经营户ID
                    udata.regUserName = dts.Rows[0]["ope_shop_name"].ToString();//档口名称
                    udata.regUserId = dts.Rows[0]["bid"].ToString();//档口ID
                   
                }

                dts = _bll.GetCheckItem("detect_item_name='" + dt.Rows[0]["Checkitem"].ToString() + "'", "", 2);//查检测项目ID
                if (dts != null && dts.Rows.Count > 0)
                {
                    udata.itemId = dts.Rows[0]["cid"].ToString();
                }
               
                udata.id = dt.Rows[0]["ChkNum"].ToString().Replace("-","").Trim();//仪器的检测编号
                udata.taskId = "";//dt.Rows[0]["t_id"].ToString();//任务ID为空
                udata.taskName = ""; //dt.Rows[0]["t_task_title"].ToString();//任务主题
                udata.samplingId = "";//dt.Rows[0]["sampling_id"].ToString();//检测任务的样品ID;
                udata.samplingDetailId = "";//dt.Rows[0]["tid"].ToString();//检测任务的tid
                //qsr.itemId = "";//dt.Rows[0]["item_id"].ToString();//检测任务检测项目
                udata.departId = Global.depart_id;// dt.Rows[0]["t_task_departId"].ToString(); 
                udata.departName = Global.d_depart_name;// "天河区食药监局";  //?
                udata.pointId = Global.point_id;// dt.Rows[0]["s_point_id"].ToString();
                udata.pointName = Global.p_point_name;//  检测点
                //qsr.regUserId = ""; //dt.Rows[0]["s_ope_id"].ToString();//档口ID
                //qsr.regUserName = "";//dt.Rows[0]["s_ope_shop_name"].ToString();//档口名称
                //qsr.regId = "";//dt.Rows[0]["s_reg_id"].ToString(); ;//经营户ID
                udata.regName = dt.Rows[0]["CheckUnit"].ToString();//dt.Rows[0]["s_reg_name"].ToString(); //被检单位
                udata.statusFalg = "0";
                udata.remark = "";
                udata.foodId = dt.Rows[0]["SampleCode"].ToString();//dt.Rows[0]["food_id"].ToString(); ;
                udata.foodName = dt.Rows[0]["SampleName"].ToString();
                udata.foodTypeId = dt.Rows[0]["SampleCode"].ToString();// result[i].SampleCode;
                udata.foodTypeName = dt.Rows[0]["SampleCategory"].ToString();
                udata.checkCode = "";//dt.Rows[0]["tid"].ToString(); 
                udata.itemName = dt.Rows[0]["Checkitem"].ToString();
                udata.checkResult = dt.Rows[0]["CheckData"].ToString();
                udata.limitValue ="<"+ dt.Rows[0]["LimitData"].ToString();
                udata.checkAccord = dt.Rows[0]["TestBase"].ToString();
                udata.checkUnit = dt.Rows[0]["Unit"].ToString();
                udata.conclusion = dt.Rows[0]["Result"].ToString();
                udata.auditorName = Global.realname;
                udata.checkUserid = Global.id;
                udata.checkUsername = Global.realname;//result[i].CheckUnitInfo;登录人账号
                udata.checkAccordId = dt.Rows[0]["ChkNum"].ToString().Replace("-","").Trim();
                udata.checkDate = dt.Rows[0]["CheckTime"].ToString().Replace("/","-");
                udata.uploadName = Global.realname; //result[i].CheckUnitInfo;
                udata.auditorId = Global.id;
                udata.uploadId = Global.id;
                udata.uploadDate = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                udata.deviceCompany = "广东达元绿洲食品安全科技股份有限公司";
                udata.deviceId = Global.MachineSerialCode;
                udata.deviceName = dt.Rows[0]["Machine"].ToString();// "DY-3500I食品综合分析仪";
                udata.deviceModel = "分光";//dt.Rows[0]["mokuai"].ToString();//模块 分光、胶体金、干化学
                udata.deviceMethod = "霉抑制率法";
                udata.dataSource = "1";
                udata.param1 = ""; //dt.Rows[0]["param2"].ToString();//预留参数
                udata.dataType = "1";//1表示送检数据

                string json = JsonHelper.EntityToJson(udata); 
                sb.AppendFormat("&result={0}", json);

                FilesRW.KLog(sb.ToString(), "发送", 14);
                responseInfo = HttpsPost(sb.ToString());// HttpMath(uria, json, 1, 1);
                FilesRW.KLog(responseInfo, "接收", 14);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return responseInfo;
        }
    }
    /// <summary>
    /// 解析返回数据
    /// </summary>
    public class ResultData
    {
        public string attributes { get; set; }
        public object obj { get; set; }
        public string resultCode { get; set; }
        public string msg { get; set; }
        public bool success { get; set; }
    }
    public class objdata
    {
        public string rights { get; set; }
        public object user { get; set; }
        public string token { get; set; }

    }
    public class userdata
    {
        public string user_name { get; set; }
        public string id { get; set; }
        public string realname { get; set; }
        public string depart_id { get; set; }
        public string point_id { get; set; }
        public string d_depart_code { get; set; }
        //public string realname { get; set; }
        /// <summary>
        /// 检测点编号
        /// </summary>
        public string p_point_code { get; set; }
        /// <summary>
        /// 检测点名称
        /// </summary>
        public string p_point_name { get; set; }
        /// <summary>
        /// 检测点类型
        /// </summary>
        public string p_point_type { get; set; }
        /// <summary>
        /// 所属机构名称
        /// </summary>
        public string d_depart_name { get; set; }
        /// <summary>
        /// 行政机构ID
        /// </summary>
        public string d_id { get; set; }

    }

    public class QuickServerResult
    {
        public string id { get; set; }
        public string taskId { get; set; }
        public string taskName { get; set; }
        public string samplingId { get; set; }
        public string samplingDetailId { get; set; }
        public string itemId { get; set; }
        public string departId { get; set; }
        public string departName { get; set; }
        public string pointId { get; set; }
        public string pointName { get; set; }
        public string regUserId { get; set; }
        public string regUserName { get; set; }
        public string regId { get; set; }
        public string regName { get; set; }
        public string statusFalg { get; set; }
        public string remark { get; set; }
        public string foodId { get; set; }
        public string foodName { get; set; }
        public string foodTypeId { get; set; }
        public string foodTypeName { get; set; }
        public string checkCode { get; set; }
        public string itemName { get; set; }
        public string checkResult { get; set; }
        public string limitValue { get; set; }
        public string checkAccord { get; set; }
        public string checkUnit { get; set; }
        public string conclusion { get; set; }
        public string auditorName { get; set; }
        public string checkUserid { get; set; }
        public string checkUsername { get; set; }
        public string checkAccordId { get; set; }
        public string checkDate { get; set; }
        public string uploadName { get; set; }
        public string auditorId { get; set; }
        public string uploadId { get; set; }
        public string uploadDate { get; set; }
        public string deviceCompany { get; set; }
        public string deviceId { get; set; }
        public string deviceName { get; set; }
        public string deviceModel { get; set; }
        public string dataSource { get; set; }
        public string deviceMethod { get; set; }
        public string param1 { get; set; }
        public string dataType { get; set; }

    }
    //仪器注册类
    public class zhuce
    {
        public string id { get; set; }
        public string device_name { get; set; }
        public string serial_number { get; set; }
        public string update_date { get; set; }
    }
}

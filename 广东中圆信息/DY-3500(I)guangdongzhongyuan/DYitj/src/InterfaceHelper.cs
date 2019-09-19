using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace AIO.src
{
    /// <summary>
    /// Note：
    /// Creater：wenj
    /// Time：2018/4/16 16:31:07
    /// Company：食安科技
    /// Web Site：http://www.chinafst.cn/
    /// Version：V1.0.0
    /// </summary>
    public static class InterfaceHelper
    {
        private static string responseInfo = string.Empty;
        public static string HttpsPost(string url)
        {
            string result = string.Empty;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "POST";    //使用get方式发送数据
                request.Accept = "*/*";
                request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.91 Safari/537.36";
                //获取网页响应结果
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
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
        /// <summary>
        /// HTTP通用接口;
        /// 如果 ContentType  "application/json"  json类型那么参数就为"{a:1,b:2}" 格式;
        /// 如果 ContentType  "application/x-www-form-urlencoded" 表单类型，那么参数为 a=1&b=2 形式;
        /// </summary>
        /// <param name="url">服务器地址</param>
        /// <param name="paramsContent">报文内容</param>
        /// <param name="methodtype">接口提交类型 1 POST 2 GET 3 SET</param>
        /// <param name="contenttype"></param>
        /// <returns>返回值</returns>
        public static string HttpMath(string url, string paramsContent = "", int methodtype = 1, int contenttype = 1)
        {
            string methodType = string.Empty, contentType = string.Empty;
            responseInfo = string.Empty;
            methodType = methodtype == 1 ? "POST" : methodtype == 2 ? "GET" : "SET";
            contentType = contenttype == 1 ? "application/json" : "application/x-www-form-urlencoded";
            try
            {
                //报文需要转义
                string urlContent = HttpUtility.UrlEncode(paramsContent, Encoding.UTF8);
                byte[] byteArray = Encoding.UTF8.GetBytes(urlContent);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = methodType;
                request.ContentType = contentType;
                request.ContentLength = byteArray.Length;
                //添加header
                //request.Headers.Add("userName", "admin");
                //添加cookie
                //CookieContainer cookie = new CookieContainer();
                //cookie.Add(new Cookie("", "", "", ""));
                using (Stream newStream = request.GetRequestStream())
                {
                    newStream.Write(byteArray, 0, byteArray.Length); //写入参数 
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();//获取响应

                using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    responseInfo = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return responseInfo;
        }

        /// <summary>
        /// 服务器通讯验证
        /// </summary>
        /// <param name="errMsg">异常信息</param>
        /// <returns>返回通讯结果</returns>
        public static string CheckUserCommunication(string url, string user, string pwd)
        {
            responseInfo = string.Empty;
            try
            {
                responseInfo = HttpMath(string.Format("{0}?userName={1}&passWord={2}", GetServiceURL(url, 1), user, Global.MD5(pwd)));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return responseInfo;
        }

        /// <summary>
        /// 数据上传
        /// </summary>
        /// <param name="json">上传Json数据</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static string UploadData(string url, string user, string pwd, string json)
        {
            responseInfo = string.Empty;
            try
            {
                responseInfo = HttpMath(string.Format("{0}?userName={1}&passWord={2}&mode={3}", GetServiceURL(url, 3), user, Global.MD5(pwd), "DY-3000(BX1)"), json);
            }
            catch (Exception ex)
            {
                responseInfo = ex.Message;
            }
            return responseInfo;
        }

        /// <summary>
        /// 数据字典下载通用接口
        /// </summary>
        /// <param name="type">下载类型</param>
        /// <param name="errMsg">异常信息</param>
        /// <param name="model">仪器型号</param>
        /// <returns>返回值</returns>
        public static string DownloadBasicData(string url, string user, string pwd, string type, string model = "")
        {
            responseInfo = string.Empty;
            try
            {
                ////如果是获取抽样任务，则要加上机器码
                //if (type.Equals(Global.GetSampleTask))
                //{
                //    model = "LZ4000-170904001";
                //}
                responseInfo = HttpMath(string.Format("{0}?userName={1}&passWord={2}&mode={3}&type={4}", GetServiceURL(url, 2), user, Global.MD5(pwd), model, type));
            }
            catch (Exception ex)
            {
                responseInfo = ex.Message;
            }

            return responseInfo;
        }

        /// <summary>
        /// 获取服务器url 适用于快检车系统
        /// 1、服务器通讯验证；2、基础数据下载；3、数据上传
        /// </summary>
        /// <param name="url">服务器地址</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static string GetServiceURL(string url, int type)
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
            else if (type == 9)
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
            return url;
        }

    }
}
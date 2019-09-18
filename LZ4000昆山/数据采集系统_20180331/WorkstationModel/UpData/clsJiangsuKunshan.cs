using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkstationDAL.Model;

namespace WorkstationModel.UpData
{
    public class clsJiangsuKunshan
    {
        public static string getKsInfo(string errMsg)
        {
            string info = string.Empty;
            switch (errMsg)
            {
                case "-1001|该检测设备编码未注册，请厂商联系我们公司。":
                    info = "检测设备编码未注册，请联系管理员！";
                    break;
                case "-1001|该编码已存在！":
                    info = "该数据已上传到平台，不支持重复上传。";
                    break;
                case "-1001|请求失败！":
                    info = "令牌获取失败！\r\n\r\nTips：请尝试进入[设置]界面进行[通讯测试]！";
                    break;
                case "-1001|用户名密码不正确":
                    info = "用户名或密码不正确！\r\n\r\nTips1：请核对用户名和密码是否填写正确！\r\nTips2：如果该账号可以正常登录平台，却通讯失败，可以尝试联系亿通技术员协助处理！";
                    break;
                case "-1001|单位编码或名称不对":
                    info = "上传时提交的单位编码或名称与平台登记信息不匹配！\r\n\r\nTips：请联系亿通公司技术员协助处理！";
                    break;
                case "-1001|令牌号已过期，请重新获取":
                    Global.Token = string.Empty;
                    info = "令牌已过期，需要重新获取！\r\n\r\nTips1：请稍后重新上传！\r\nTips2：可以尝试进入[设置]界面进行[通讯测试]提示成功后，然后重新上传！";
                    break;
                case "-1001|经营者摊位号或者身份证号不能同时为空！":
                    info = "经营户签约已到期！\r\n\r\nTips：请登录[苏州监管平台]重新签约！";
                    break;
                case "-1001|抽检的品种编码不正确!":
                    info = "检测品种编码不存在！\r\n\r\nTips：请尝试进入[设置]界面下载数据字典，然后重新上传！";
                    break;
                case "-1001|抽检项目大类编号不正确!":
                    info = "抽检项目大类编号不正确！\r\n\r\nTips：请尝试进入[设置]界面下载数据字典，然后重新上传！";
                    break;
                case "-1001|抽检项目小类编号不正确!":
                    info = "抽检项目小类编号不正确！\r\n\r\nTips：请尝试进入[设置]界面下载数据字典，然后重新上传！";
                    break;
                case "-1001|经营者摊位号或身份证号不对！多个摊位号只需要上传一个即可！":
                    info = "经营户签约过期，建议前往平台续签后再尝试上传。";
                    break;
                default:
                    info = errMsg;
                    break;
            }
            return info;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkstationDAL.Model;

namespace WorkstationModel.report
{
    public class clsReport
    {
        private string path = Environment.CurrentDirectory;


        private string GetHtmlDoc()
        {
            string htmlDoc = string.Empty;
            try
            {
                //string Organization = Global.samplenameadapter[0].Organization;
                string Result = string.Empty;
                //if (_selectedRecords != null && _selectedRecords.Count > 0)
                //{
                //    tlsTtResultSecond md = _selectedRecords[0];
                //    string CheckPlanCode = md.CheckPlanCode;
                //    if (CheckPlanCode.Length > 0)
                //    {
                //        string[] str = CheckPlanCode.Split('-');
                //        CheckPlanCode = str[0];
                //    }
                //    else
                //    {
                //        CheckPlanCode = "/";
                //    }
                //    Result = md.Result;
                //    htmlDoc = System.IO.File.ReadAllText(path + "\\Others\\CheckedReportTitle.txt", System.Text.Encoding.Default);
                //    htmlDoc += string.Format("<div class=\"number\">编号：{0}</div>", md.CheckNo.Length > 0 ? md.CheckNo : "/");
                //    htmlDoc += "<div style=\"float:left; margin-left:40px; margin-top:20px;\">";
                //    htmlDoc += "<table width=\"760\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">";

                //    htmlDoc += "<tbody><tr><th width=\"190\" height=\"76\" class=\"as\">样品名称</th>";
                //    htmlDoc += string.Format("<td width=\"190\" height=\"76\" align=\"center\" class=\"as\">{0}</td>", md.FoodName.Length > 0 ? md.FoodName : "/");
                //    htmlDoc += "<th width=\"190\" height=\"76\" class=\"as\">唯一性单号或样品单号</th>";
                //    htmlDoc += string.Format("<td width=\"190\" height=\"76\" align=\"center\" class=\"as box\">{0}</td></tr>", md.SampleId.Length > 0 ? md.SampleId : "/");

                //    htmlDoc += "<tr><th width=\"190\" height=\"76\" class=\"bs\">采样（抽样）时间</th>";
                //    htmlDoc += string.Format("<td width=\"190\" height=\"76\" align=\"center\" class=\"bs\">{0}</td>", md.CheckStartDate.Length > 0 ? DateTime.Parse(md.CheckStartDate).AddHours(-1).ToString("yyyy-MM-dd HH:mm:ss") : "/");
                //    htmlDoc += "<th width=\"190\" height=\"76\" class=\"bs\">采样（抽样）地点</th>";
                //    htmlDoc += string.Format("<td width=\"190\" height=\"76\" align=\"center\" class=\"bs box\">{0}</td></tr>", md.CheckedCompany.Length > 0 ? md.CheckedCompany.ToString() : "/");

                //    htmlDoc += "<tr><th width=\"190\" height=\"76\" class=\"bs\">区县所</th>";
                //    htmlDoc += string.Format("<td width=\"190\" height=\"76\" align=\"center\" class=\"bs\">{0}</td>", Organization.Length > 0 ? Organization : "/");
                //    htmlDoc += "<th width=\"190\" height=\"76\" class=\"bs\">计划编号</th>";
                //    htmlDoc += string.Format("<td width=\"190\" height=\"76\" align=\"center\" class=\"bs box\">{0}</td></tr>", CheckPlanCode);

                //    htmlDoc += "<tr><th width=\"190\" height=\"76\" class=\"bs\">检测时间</th>";
                //    htmlDoc += string.Format("<td width=\"190\" height=\"76\" align=\"center\" class=\"bs\">{0}</td>", md.CheckStartDate.Length > 0 ? md.CheckStartDate : "/");
                //    htmlDoc += "<th width=\"190\" height=\"76\" class=\"bs\">检测依据</th>";
                //    htmlDoc += string.Format("<td width=\"190\" height=\"76\" align=\"center\" class=\"bs box\">{0}</td></tr>", md.Standard.Length > 0 ? md.Standard : "/");
                //    htmlDoc += "</tbody></table>";

                //    htmlDoc += "<table width=\"760\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tbody><tr>";
                //    htmlDoc += "<th height=\"54\" width=\"152\" class=\"ds\">检测项目</th>";
                //    htmlDoc += "<th height=\"54\" width=\"152\" class=\"ds\">检测仪器</th>";
                //    htmlDoc += "<th height=\"54\" width=\"152\" class=\"ds\">检测结果</th>";
                //    htmlDoc += "<th height=\"54\" width=\"152\" class=\"ds\">限定值</th>";
                //    htmlDoc += "<th height=\"54\" width=\"152\" class=\"ds\" id=\"unique\">检测结论</th></tr>";

                //    htmlDoc += string.Format("<tr><td height=\"54\" align=\"center\" class=\"ds\">{0}</td>", md.CheckTotalItem.Length > 0 ? md.CheckTotalItem : "/");
                //    htmlDoc += string.Format("<td height=\"54\" align=\"center\" class=\"ds\">{0}</td>", md.CheckMachine.Length > 0 ? md.CheckMachine : "/");
                //    htmlDoc += string.Format("<td height=\"54\" align=\"center\" class=\"ds\">{0}</td>", md.CheckValueInfo.Length > 0 ? md.CheckValueInfo + (md.CheckValueInfo.Equals("阳性") || md.CheckValueInfo.Equals("阴性") || md.CheckValueInfo.Equals("合格") || md.CheckValueInfo.Equals("不合格") || md.CheckValueInfo.Equals("无效") || md.CheckValueInfo.Equals("参考国标") ? string.Empty : (" " + md.ResultInfo)) : "/"); // md.ResultInfo
                //    htmlDoc += string.Format("<td height=\"54\" align=\"center\" class=\"ds\">{0}</td>", md.StandValue.Length > 0 ? md.StandValue + " " + (Result.Equals("阳性") || Result.Equals("阴性") || md.StandValue.Equals("合格") || md.StandValue.Equals("参考国标") || md.StandValue.Equals("阴性") || md.StandValue.Equals("阳性") ? string.Empty : (" " + md.ResultInfo)) : "/"); // md.ResultInfo
                //    htmlDoc += string.Format("<td height=\"54\" align=\"center\" class=\"ds\" id=\"unique\">{0}</td></tr>", Result.Equals("阴性") || Result.Equals("合格") ? "合格" : "不合格");

                //    htmlDoc += "</tbody></table>";

                //    htmlDoc += string.Format("<div class=\"forming\"><div class=\"conclusion\">结论：{0}</div>", Result.Equals("阴性") || Result.Equals("合格") ? "合格" : "不合格");
                //    htmlDoc += "<div class=\"img\"><img src=\"dlReport.png\"></div>";
                //    htmlDoc += "<table width=\"760\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\" float:left; margin-top:-54px;font-size:16px;\">";
                //    htmlDoc += "<tbody><tr><td height=\"54\" width=\"380\"></td>";
                //    htmlDoc += string.Format("<td height=\"54\" width=\"380\" style=\"padding-left:40px;\">检测单位：{0}</td>", md.CheckUnitName.Length > 0 ? md.CheckUnitName : "/");
                //    htmlDoc += "</tr></tbody></table>";

                //    htmlDoc += "<table width=\"760\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\" float:left; margin-top:-20px;font-size:16px;\">";
                //    htmlDoc += "<tbody><tr><td height=\"54\" width=\"380\"></td>";
                //    htmlDoc += string.Format("<td height=\"54\" width=\"380\" style=\"padding-left:40px;\">报告时间：{0}</td>", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                //    htmlDoc += "</tr></tbody></table></div>";

                //    htmlDoc += "<div class=\"forming\" style=\"height:54px; margin-top:0; float:left;\"><div class=\"note\"><span class=\"NT\">备注：检测结论为不合格时，应该及时上报。</span></div></div></div>";
                //    htmlDoc += string.Format("<div class=\"personnel\">检测人员：{0}</div>", md.CheckUnitInfo.Length > 0 ? md.CheckUnitInfo : "/");
                //    htmlDoc += "<div class=\"personnel\" style=\"margin-left:190px;\">审核：</div>";
                //    htmlDoc += "<div class=\"personnel\" style=\"margin-left:190px;\">批准：</div>";
                //    htmlDoc += "<div style=\" float:left; width:800px; height:60px;\"></div></div></body></html>";
                //}
            }
            catch (Exception)
            {
                throw;
            }
            return htmlDoc;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WorkstationModel.UpData
{
    public class HXUpLoad
    {
        public static string UpLoadXML(DataTable dt)
        {
            StringBuilder xmlResult = new StringBuilder();
            xmlResult.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            xmlResult.Append("<request>");
            xmlResult.Append("<head>");
            xmlResult.AppendFormat("<data_count>{0}</data_count>", 1);
            xmlResult.Append("</head>");
            xmlResult.Append("<dataset>");
            xmlResult.Append("<data no=\"1\">");
            xmlResult.AppendFormat ("<ID>{0}</ID>",dt.Rows[0]["ChkNum"].ToString().Replace("-","").Trim());
            xmlResult.AppendFormat("<SAMPLE>{0}</SAMPLE>", dt.Rows[0]["SampleName"].ToString());
            xmlResult.AppendFormat("<CHECKITEM>{0}</CHECKITEM>", dt.Rows[0]["Checkitem"].ToString());
            xmlResult.AppendFormat("<CHECKRESULT>{0}</CHECKRESULT>", dt.Rows[0]["Result"].ToString()=="合格"?"1":"2");
            xmlResult.AppendFormat("<CHECKTIME>{0}</CHECKTIME>", dt.Rows[0]["CheckTime"].ToString().Replace("/", "-"));
            xmlResult.AppendFormat("<SENDCOMPANY>{0}</SENDCOMPANY>", dt.Rows[0]["CheckUnit"].ToString());
            xmlResult.AppendFormat("<CHECKVALUE>{0}</CHECKVALUE>", dt.Rows[0]["CheckData"].ToString());
            xmlResult.AppendFormat("<UNIT>{0}</UNIT>", dt.Rows[0]["Unit"].ToString());
            xmlResult.AppendFormat("<CITY>{0}</CITY>", dt.Rows[0]["ProductPlace"].ToString());
            xmlResult.AppendFormat("<SUPPLIER>{0}</SUPPLIER>", dt.Rows[0]["ProcodeCompany"].ToString());
            xmlResult.AppendFormat("<SAMPLENO>{0}</SAMPLENO>", dt.Rows[0]["SampleCode"].ToString());
            xmlResult.AppendFormat("<CHANNEL>{0}</CHANNEL>", dt.Rows[0]["HoleNum"].ToString());
            xmlResult.AppendFormat("<REFINFO>{0}</REFINFO>", dt.Rows[0]["TestBase"].ToString());
            xmlResult.AppendFormat("<REFVALUE>{0}</REFVALUE>", dt.Rows[0]["LimitData"].ToString());
            xmlResult.AppendFormat("<GREATKIND>{0}</GREATKIND>", "食用农产品");
            xmlResult.AppendFormat("<LITTLEKIND>{0}</LITTLEKIND>", "果菜类");
            xmlResult.AppendFormat("<A1>{0}</A1>", "0");
            xmlResult.AppendFormat("<A2>{0}</A2>", "0");
            xmlResult.AppendFormat("<CHECKER>{0}</CHECKER>", dt.Rows[0]["Tester"].ToString());
            xmlResult.AppendFormat("<DEVICE>{0}</DEVICE>", dt.Rows[0]["Machine"].ToString());
            xmlResult.AppendFormat("<LONGITUDE>{0}</LONGITUDE>","0");
            xmlResult.AppendFormat("<LATITUDE>{0}</LATITUDE>", "0");
            xmlResult.Append("</data>");
            xmlResult.Append("</dataset>");
            xmlResult.Append("</request>");

            return xmlResult.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Runtime.InteropServices;

namespace FoodClient.AnHui
{
    public class Global
    {
        /// <summary>
        /// 上传成功的数据条数
        /// </summary>
        public static int UploadCount = 0;

        public static class AnHuiInterface
        {
            public static List<data_dictionary> data_dictionaryList = null;
            public static List<checked_unit> checked_unitList = null;
            public static List<standard_limit> standard_limitList = null;
            public static string userName = ConfigurationManager.AppSettings["AnHui_userName"];
            public static string passWord = ConfigurationManager.AppSettings["AnHui_passWord"];
            public static string interfaceVersion = ConfigurationManager.AppSettings["AnHui_interfaceVersion"];
            public static string instrument = ConfigurationManager.AppSettings["AnHui_instrument"];
            public static string instrumentNo = ConfigurationManager.AppSettings["AnHui_instrumentNo"];
            public static string mac = ConfigurationManager.AppSettings["AnHui_mac"];

            //public static string ServerAddr = ConfigurationManager.AppSettings["AnHui_url"];
            public static string ServerAddr = ConfigurationManager.AppSettings["HeFei_uploadUrl"];
            public static string Chkdanwei = ConfigurationManager.AppSettings["AnHui_instrument"];
            public static string Chkbianhao = instrumentNo = ConfigurationManager.AppSettings["AnHui_instrumentNo"];

            /// <summary>
            /// 安徽省项目 - 字典接口 
            /// </summary>
            /// <returns></returns>
            public static string instrumentDictionaryHandle(clsInstrumentInfoHandle model)
            {
                StringBuilder soap = new StringBuilder();
                soap.AppendFormat("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:ins=\"http://www.zhiyunda.com/service/instrumentDockingService\" xmlns:zyd=\"http://www.zhiyunda.com/zydjcy\">");
                soap.AppendFormat("<soapenv:Header>");
                soap.AppendFormat("<ins:interfaceVersion>{0}</ins:interfaceVersion>", model.interfaceVersion);
                soap.AppendFormat("<ins:key>{0}</ins:key>", model.key);
                soap.AppendFormat("<ins:userName>{0}</ins:userName>", model.userName);
                soap.AppendFormat("</soapenv:Header>");
                soap.AppendFormat("<soapenv:Body>");
                soap.AppendFormat("<zyd:instrumentDictionaryRequest>");
                soap.AppendFormat("<zyd:instrument>{0}</zyd:instrument>", model.instrument);
                soap.AppendFormat("<zyd:instrumentNo>{0}</zyd:instrumentNo>", model.instrumentNo);
                soap.AppendFormat("<zyd:mac>{0}</zyd:mac>", model.mac);
                soap.AppendFormat("<zyd:tableData>data_dictionary:{0};checked_unit:{1};standard_limit:{2};</zyd:tableData>", model.tableData, model.tableData, model.tableData);
                soap.AppendFormat("<zyd:reqType>{0}</zyd:reqType>", "all");
                soap.AppendFormat("</zyd:instrumentDictionaryRequest>");
                soap.AppendFormat("</soapenv:Body>");
                soap.AppendFormat("</soapenv:Envelope>");

                //发起请求
                Uri uri = new Uri(Global.AnHuiInterface.ServerAddr);
                WebRequest webRequest = WebRequest.Create(uri);
                webRequest.ContentType = "text/xml; charset=utf-8";
                webRequest.Method = "POST";
                webRequest.Timeout = 10000;
                using (Stream requestStream = webRequest.GetRequestStream())
                {
                    byte[] paramBytes = Encoding.UTF8.GetBytes(soap.ToString());
                    requestStream.Write(paramBytes, 0, paramBytes.Length);
                }

                //响应
                string rtnStr = string.Empty;
                WebResponse webResponse = webRequest.GetResponse();
                using (StreamReader myStreamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8))
                {
                    rtnStr = myStreamReader.ReadToEnd();
                }

                return rtnStr;
            }

            /// <summary>
            /// 安徽省项目 - 采集接口
            /// </summary>
            /// <returns></returns>
            public static string instrumentInfoHandle(clsInstrumentInfoHandle model)
            {
                StringBuilder soap = new StringBuilder();
                soap.AppendFormat("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:ins=\"http://www.zhiyunda.com/service/instrumentDockingService\" xmlns:zyd=\"http://www.zhiyunda.com/zydjcy\">");
                soap.AppendFormat("<soapenv:Header>");
                soap.AppendFormat("<ins:interfaceVersion>{0}</ins:interfaceVersion>", model.interfaceVersion);
                soap.AppendFormat("<ins:key>{0}</ins:key>", model.key);
                soap.AppendFormat("<ins:userName>{0}</ins:userName>", model.userName);
                soap.AppendFormat("</soapenv:Header>");
                soap.AppendFormat("<soapenv:Body>");
                soap.AppendFormat("<zyd:instrumentInfoRequest>");
                soap.AppendFormat("<zyd:instrumentDockingInfoList>");
                soap.AppendFormat("<zyd:instrument>{0}</zyd:instrument>", model.instrument);
                soap.AppendFormat("<zyd:instrumentNo>{0}</zyd:instrumentNo>", model.instrumentNo);
                soap.AppendFormat("<zyd:gps>{0}</zyd:gps>", model.gps);
                soap.AppendFormat("<zyd:mac>{0}</zyd:mac>", model.mac);
                soap.AppendFormat("<zyd:fTpye>{0}</zyd:fTpye>", model.fTpye);
                soap.AppendFormat("<zyd:fName>{0}</zyd:fName>", model.fName);
                soap.AppendFormat("<zyd:tradeMark>{0}</zyd:tradeMark>", model.tradeMark);
                soap.AppendFormat("<zyd:foodcode>{0}</zyd:foodcode>", model.foodcode);
                soap.AppendFormat("<zyd:proBatch>{0}</zyd:proBatch>", model.proBatch);
                soap.AppendFormat("<zyd:proDate>{0}</zyd:proDate>", model.proDate);
                soap.AppendFormat("<zyd:proSpecifications>{0}</zyd:proSpecifications>", model.proSpecifications);
                soap.AppendFormat("<zyd:manuUnit>{0}</zyd:manuUnit>", model.manuUnit);
                soap.AppendFormat("<zyd:checkedNo>{0}</zyd:checkedNo>", model.checkedNo);
                soap.AppendFormat("<zyd:sampleNo>{0}</zyd:sampleNo>", model.sampleNo);
                soap.AppendFormat("<zyd:checkedUnit>{0}</zyd:checkedUnit>", model.checkedUnit);
                soap.AppendFormat("<zyd:dataNum>{0}</zyd:dataNum>", model.dataNum);
                soap.AppendFormat("<zyd:testPro>{0}</zyd:testPro>", model.testPro);
                soap.AppendFormat("<zyd:quanOrQual>{0}</zyd:quanOrQual>", model.quanOrQual);
                soap.AppendFormat("<zyd:contents>{0}</zyd:contents>", model.contents);
                soap.AppendFormat("<zyd:unit>{0}</zyd:unit>", model.unit);
                soap.AppendFormat("<zyd:testResult>{0}</zyd:testResult>", model.testResult);
                soap.AppendFormat("<zyd:dilutionRa>{0}</zyd:dilutionRa>", model.dilutionRa);
                soap.AppendFormat("<zyd:testRange>{0}</zyd:testRange>", model.testRange);
                soap.AppendFormat("<zyd:standardLimit>{0}</zyd:standardLimit>", model.standardLimit);
                soap.AppendFormat("<zyd:basedStandard>{0}</zyd:basedStandard>", model.basedStandard);
                soap.AppendFormat("<zyd:testPerson>{0}</zyd:testPerson>", model.testPerson);
                soap.AppendFormat("<zyd:testTime>{0}</zyd:testTime>", model.testTime);
                soap.AppendFormat("<zyd:sampleTime>{0}</zyd:sampleTime>", model.sampleTime);
                soap.AppendFormat("<zyd:remark>{0}</zyd:remark>", model.remark);
                soap.AppendFormat("<zyd:reserve1>{0}</zyd:reserve1>", "");
                soap.AppendFormat("<zyd:reserve2>{0}</zyd:reserve2>", "");
                soap.AppendFormat("<zyd:reserve3>{0}</zyd:reserve3>", "");
                soap.AppendFormat("<zyd:reserve4>{0}</zyd:reserve4>", "");
                soap.AppendFormat("<zyd:reserve5>{0}</zyd:reserve5>", "");
                soap.AppendFormat("</zyd:instrumentDockingInfoList>");
                soap.AppendFormat("</zyd:instrumentInfoRequest>");
                soap.AppendFormat("</soapenv:Body>");
                soap.AppendFormat("</soapenv:Envelope>");

                //发起请求
                Uri uri = new Uri(Global.AnHuiInterface.ServerAddr);
                WebRequest webRequest = WebRequest.Create(uri);
                webRequest.ContentType = "text/xml; charset=utf-8";
                webRequest.Method = "POST";
                webRequest.Timeout = 10000;
                using (Stream requestStream = webRequest.GetRequestStream())
                {
                    byte[] paramBytes = Encoding.UTF8.GetBytes(soap.ToString());
                    requestStream.Write(paramBytes, 0, paramBytes.Length);
                }

                //响应
                string rtnStr = string.Empty;
                WebResponse webResponse = webRequest.GetResponse();
                using (StreamReader myStreamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8))
                {
                    rtnStr = myStreamReader.ReadToEnd();
                }
                return rtnStr;
            }

            /// <summary>
            /// MD5加密
            /// </summary>
            /// <param name="password"></param>
            /// <returns></returns>
            public static string md5(string password)
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password);
                bytes = md5.ComputeHash(bytes);
                md5.Clear();

                string ret = "";
                for (int i = 0; i < bytes.Length; i++)
                {
                    ret += Convert.ToString(bytes[i], 16).PadLeft(2, '0');
                }

                return ret.PadLeft(32, '0');
            }

            /// <summary>
            /// 安徽省项目 - 解析数据上传返回的XML
            /// </summary>
            /// <param name="xml"></param>
            /// <returns></returns>
            public static List<string> ParsingUploadXML(string xml)
            {
                List<string> rtnStr = new List<string>();
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(xml);
                string status = string.Empty, description = string.Empty;
                XmlNodeList statusList = xd.GetElementsByTagName("status");
                foreach (XmlNode item in statusList)
                    status = item.InnerText;
                XmlNodeList descriptionList = xd.GetElementsByTagName("description");
                foreach (XmlNode item in descriptionList)
                    description = item.InnerText;
                rtnStr.Add(status);
                rtnStr.Add(description);

                return rtnStr;
            }

            /// <summary>
            /// 安徽省项目 - 解析XML
            /// </summary>
            /// <param name="xml"></param>
            /// <returns></returns>
            public static string ParsingXML(string xml)
            {
                try
                {
                    data_dictionaryList = new List<data_dictionary>();
                    checked_unitList = new List<checked_unit>();
                    standard_limitList = new List<standard_limit>();
                    string status = string.Empty, description = string.Empty, tableUpdateTime = string.Empty;
                    XmlDocument xd = new XmlDocument();
                    xd.LoadXml(xml);
                    XmlNodeList statusList = xd.GetElementsByTagName("status");
                    foreach (XmlNode item in statusList)
                        status = item.InnerText;
                    XmlNodeList descriptionList = xd.GetElementsByTagName("description");
                    foreach (XmlNode item in descriptionList)
                        description = item.InnerText;
                    if (status.Equals("1") && description.Equals("成功"))
                    {
                        //获取更新时间tableUpdateTime
                        XmlNodeList tableUpdateTimeList = xd.GetElementsByTagName("tableUpdateTime");
                        foreach (XmlNode item in tableUpdateTimeList)
                            tableUpdateTime = item.InnerText;
                        #region 更新时间
                        //string[] strTime = tableUpdateTime.Split(';');
                        //if (strTime != null && strTime.Length > 0)
                        //{
                        //    foreach (string item in strTime)
                        //    {
                        //        if (item.Length > 0)
                        //        {
                        //            string str = ":" + DateTime.Now.Year;
                        //            string[] sArray = Regex.Split(item, str, RegexOptions.IgnoreCase);
                        //            if (sArray != null && sArray.Length > 0)
                        //            {
                        //                if (sArray[0].Equals("data_dictionary"))
                        //                {
                        //                    string data_dictionary = DateTime.Now.Year + sArray[1];
                        //                }
                        //                else if (sArray[0].Equals("checked_unit"))
                        //                {
                        //                    string checked_unit = DateTime.Now.Year + sArray[1];
                        //                }
                        //                else if (sArray[0].Equals("standard_limit"))
                        //                {
                        //                    string standard_limit = DateTime.Now.Year + sArray[1];
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        #endregion
                        XmlNodeList updatedataList = xd.GetElementsByTagName("updateData");
                        foreach (XmlNode items in updatedataList)
                        {
                            if (items.Name.Equals("updateData"))
                            {
                                //此处将updateData中的串重新加载到XML来解析
                                string str = items.InnerText;
                                xd = new XmlDocument();
                                xd.LoadXml(str);
                                XmlNodeList table = xd.GetElementsByTagName("table");
                                foreach (XmlNode item in table)
                                {
                                    XmlNodeList data = item.ChildNodes;
                                    string type = data[0].InnerText;
                                    foreach (XmlNode dts in data[2])
                                    {
                                        string strData = dts.InnerText;
                                        if (!strData.Equals(""))
                                        {
                                            string[] strDataList = strData.Split('|');
                                            if (strDataList != null && strDataList.Length > 0)
                                            {
                                                if (type.Equals("data_dictionary"))
                                                {
                                                    data_dictionary data_dictionary = new data_dictionary();
                                                    data_dictionary.id = strDataList[0];
                                                    data_dictionary.codeId = strDataList[1];
                                                    data_dictionary.name = strDataList[2];
                                                    data_dictionary.pid = strDataList[3];
                                                    data_dictionary.remark = strDataList[4];
                                                    data_dictionary.inputdate = strDataList[5];
                                                    data_dictionary.modifydate = strDataList[6];
                                                    data_dictionary.status = strDataList[7];
                                                    data_dictionary.typeNum = strDataList[8];
                                                    data_dictionaryList.Add(data_dictionary);
                                                }
                                                else if (type.Equals("checked_unit"))
                                                {
                                                    checked_unit checked_unit = new checked_unit();
                                                    checked_unit.id = strDataList[0];
                                                    checked_unit.inputdate = strDataList[1];
                                                    checked_unit.modifydate = strDataList[2];
                                                    checked_unit.address = strDataList[3];
                                                    checked_unit.busScope = strDataList[4];
                                                    checked_unit.bussinessId = strDataList[5];
                                                    checked_unit.idCard = strDataList[6];
                                                    checked_unit.linkName = strDataList[7];
                                                    checked_unit.tel = strDataList[8];
                                                    checked_unit.unitName = strDataList[9];
                                                    checked_unit.status = strDataList[10];
                                                    checked_unitList.Add(checked_unit);
                                                }
                                                else if (type.Equals("standard_limit"))
                                                {
                                                    standard_limit standard_limit = new standard_limit();
                                                    standard_limit.id = strDataList[0];
                                                    standard_limit.inputdate = strDataList[1];
                                                    standard_limit.modifydate = strDataList[2];
                                                    standard_limit.decisionBasis = strDataList[3];
                                                    double limit = 0;
                                                    if (double.TryParse(strDataList[4], out limit)) standard_limit.maxLimit = limit;
                                                    else standard_limit.maxLimit = 0;
                                                    if (double.TryParse(strDataList[5], out limit)) standard_limit.minLimit = limit;
                                                    else standard_limit.minLimit = 0;
                                                    standard_limit.testBasis = strDataList[6];
                                                    standard_limit.unit = strDataList[7];
                                                    standard_limit.foodType = strDataList[8];
                                                    standard_limit.testItem = strDataList[9];
                                                    standard_limitList.Add(standard_limit);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        data_dictionaryList = null;
                        checked_unitList = null;
                        standard_limitList = null;
                        return description;
                    }
                    return status;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        /// <summary>
        /// 在指定的字符串列表CnStr中检索符合拼音索引字符串
        /// </summary>
        /// <param name="CnStr">汉字字符串</param>
        /// <returns>相对应的汉语拼音首字母串</returns>
        public static string GetSpellCode(string CnStr)
        {
            string strTemp = "";
            int iLen = CnStr.Length;
            int i = 0;
            for (i = 0; i <= iLen - 1; i++)
            {
                strTemp += GetCharSpellCode(CnStr.Substring(i, 1));
            }
            return strTemp;
        }

        /// <summary>
        /// 得到一个汉字的拼音第一个字母，如果是一个英文字母则直接返回大写字母
        /// </summary>
        /// <param name="CnChar">单个汉字</param>
        /// <returns>单个大写字母</returns>
        private static string GetCharSpellCode(string CnChar)
        {
            long iCnChar;
            byte[] ZW = System.Text.Encoding.Default.GetBytes(CnChar);
            //如果是字母，则直接返回
            if (ZW.Length == 1)
            {
                return CnChar.ToUpper();
            }
            else
            {
                int i1 = (short)(ZW[0]);
                int i2 = (short)(ZW[1]);
                iCnChar = i1 * 256 + i2;
            }
            if ((iCnChar >= 45217) && (iCnChar <= 45252))
            {
                return "A";
            }
            else if ((iCnChar >= 45253) && (iCnChar <= 45760))
            {
                return "B";
            }
            else if ((iCnChar >= 45761) && (iCnChar <= 46317))
            {
                return "C";
            }
            else if ((iCnChar >= 46318) && (iCnChar <= 46825))
            {
                return "D";
            }
            else if ((iCnChar >= 46826) && (iCnChar <= 47009))
            {
                return "E";
            }
            else if ((iCnChar >= 47010) && (iCnChar <= 47296))
            {
                return "F";
            }
            else if ((iCnChar >= 47297) && (iCnChar <= 47613))
            {
                return "G";
            }
            else if ((iCnChar >= 47614) && (iCnChar <= 48118))
            {
                return "H";
            }
            else if ((iCnChar >= 48119) && (iCnChar <= 49061))
            {
                return "J";
            }
            else if ((iCnChar >= 49062) && (iCnChar <= 49323))
            {
                return "K";
            }
            else if ((iCnChar >= 49324) && (iCnChar <= 49895))
            {
                return "L";
            }
            else if ((iCnChar >= 49896) && (iCnChar <= 50370))
            {
                return "M";
            }
            else if ((iCnChar >= 50371) && (iCnChar <= 50613))
            {
                return "N";
            }
            else if ((iCnChar >= 50614) && (iCnChar <= 50621))
            {
                return "O";
            }
            else if ((iCnChar >= 50622) && (iCnChar <= 50905))
            {
                return "P";
            }
            else if ((iCnChar >= 50906) && (iCnChar <= 51386))
            {
                return "Q";
            }
            else if ((iCnChar >= 51387) && (iCnChar <= 51445))
            {
                return "R";
            }
            else if ((iCnChar >= 51446) && (iCnChar <= 52217))
            {
                return "S";
            }
            else if ((iCnChar >= 52218) && (iCnChar <= 52697))
            {
                return "T";
            }
            else if ((iCnChar >= 52698) && (iCnChar <= 52979))
            {
                return "W";
            }
            else if ((iCnChar >= 52980) && (iCnChar <= 53640))
            {
                return "X";
            }
            else if ((iCnChar >= 53689) && (iCnChar <= 54480))
            {
                return "Y";
            }
            else if ((iCnChar >= 54481) && (iCnChar <= 55289))
            {
                return "Z";
            }
            else
                return ("?");
        }

        /// <summary>
        /// 将DataTable转换成实体类
        /// 2016年6月24日 wenj
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> TableToEntity<T>(DataTable dt) where T : new()
        {
            // 定义集合 
            List<T> ts = new List<T>();

            // 获得此模型的类型 
            Type type = typeof(T);
            //定义一个临时变量 
            string tempName = string.Empty;
            //遍历DataTable中所有的数据行  
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                // 获得此模型的公共属性 
                PropertyInfo[] propertys = t.GetType().GetProperties();
                //遍历该对象的所有属性 
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;//将属性名称赋值给临时变量   
                    //检查DataTable是否包含此列（列名==对象的属性名）     
                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter   
                        if (!pi.CanWrite) continue;//该属性不可写，直接跳出   
                        //取值   
                        object value = dr[tempName];
                        //如果非空，则赋给对象的属性   
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                //对象添加到泛型集合中 
                ts.Add(t);
            }
            return ts;
        }

        /// <summary>
        /// ping一个IP或域名是否能ping通
        /// </summary>
        /// <param name="strIpOrDName"></param>
        /// <returns></returns>
        public static bool PingIpOrDomainName(string strIpOrDName)
        {
            try
            {
                Ping objPingSender = new Ping();
                PingOptions objPinOptions = new PingOptions();
                objPinOptions.DontFragment = true;
                string data = string.Empty;
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                int intTimeout = 5000;
                PingReply objPinReply = objPingSender.Send(strIpOrDName, intTimeout, buffer, objPinOptions);
                string strInfo = objPinReply.Status.ToString();
                if (strInfo == "Success") return true;
                else return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(int Description, int ReservedValue);
        /// <summary>
        /// 检查网络是否可以连接互联网
        /// </summary>
        /// <returns></returns>
        public static bool IsConnectInternet()
        {
            int Description = 0;
            return InternetGetConnectedState(Description, 0);
        }

    }
}
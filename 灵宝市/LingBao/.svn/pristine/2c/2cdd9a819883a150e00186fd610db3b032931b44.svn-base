using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorkstationDAL.Basic;
using JH.CommBase;
using System.Data;
using WorkstationDAL.Model;

namespace WorkstationModel.Model
{
    public  class CommonOperation
    {
        /// <summary>
        /// 公共操作类
        /// </summary>
        private CommonOperation()
        {

        }

        /// <summary>
        /// 对已经存在的版本判断
        /// </summary>
        /// <returns></returns>
        public static string ExistVersion()
        {
            string errMsg = string.Empty;
            string sql = "SELECT OPTVALUE FROM TSYSOPT WHERE SYSCODE='030102'";
            object obj = DataBase.GetOneValue(sql, out errMsg);
            if (obj == null)
            {
                return "false";
            }
            else
            {
                return obj.ToString();
            }
        }

        /// <summary>
        /// 获取编号前缀,效率有待改进
        /// </summary>
        /// <param name="format">编号格式</param>
        /// <param name="companyCode">单位编码</param>
        /// <param name="userCode">用户编码</param>
        /// <param name="nLen">长度</param>
        /// <returns></returns>
        public static string GetPreCode(string format, string companyCode, string userCode, out int nLen)
        {
            string areaCode = string.Empty;
            string resultStr = string.Empty;
            string splitStr = "+";
            char[] splitChar = splitStr.ToCharArray();
            string[] OkStr = format.Split(splitChar);
            int LevelNum = OkStr.GetUpperBound(0);
            nLen = 0;
            for (int i = 0; i <= LevelNum; i++)
            {
                if (!OkStr[i].Substring(0, 1).Equals("{"))
                {
                    resultStr += OkStr[i];
                }
                else
                {
                    switch (OkStr[i].Substring(0, 3).ToUpper())
                    {
                        //时间：yy-2位年份，yyyy-4位年份
                        //M-1位月份，MM-2位月份
                        //d-1位日期，dd-2位日期
                        //等同于日期格式变化

                        case "{D:":
                            if (OkStr[i].Length > 4)
                            {
                                //将大写的Y替换成小写的y，将小写的m替换成大写的m，将大写的D替换成小写的d
                                string sinput = OkStr[i].Substring(3, OkStr[i].Length - 4);
                                sinput = sinput.Replace("Y", "y");
                                sinput = sinput.Replace("m", "M");
                                sinput = sinput.Replace("D", "d");
                                resultStr += DateTime.Now.ToString(sinput);
                            }
                            break;

                        case "{N:":
                            if (OkStr[i].Length > 4)
                            {
                                nLen = int.Parse(OkStr[i].Substring(3, OkStr[i].Length - 4));
                            }
                            break;

                        //当前单位编号
                        case "{C}":
                            if (OkStr[i].Length == 3)
                            {
                                resultStr += companyCode;
                            }
                            break;

                        //当前检测员编号
                        case "{U}":
                            if (OkStr[i].Length == 3)
                            {
                                resultStr += userCode;
                            }
                            break;

                        //但前地区编号
                        case "{A}":
                            if (OkStr[i].Length == 3)
                            {
                                resultStr += areaCode;
                            }
                            break;
                    }
                }
            }
            return resultStr;
        }

        /// <summary>
        /// 获取编码字符串
        /// </summary>
        /// <param name="format">格式化</param>
        /// <param name="maxNum"></param>
        /// <param name="companyCode"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public static string GetCodeString(string format, int maxNum, string companyCode, string userCode)
        {
            string areaCode = string.Empty;
            string resultStr = string.Empty;
            string splitStr = "+";
            char[] splitChar = splitStr.ToCharArray();
            string[] OkStr = format.Split(splitChar);
            int LevelNum = OkStr.GetUpperBound(0);
            for (int i = 0; i <= LevelNum; i++)
            {
                if (!OkStr[i].Substring(0, 1).Equals("{"))
                {
                    resultStr = resultStr + OkStr[i];
                }
                else
                {
                    switch (OkStr[i].Substring(0, 3).ToUpper())
                    {
                        //时间：yy-2位年份，yyyy-4位年份
                        //      M-1位月份，MM-2位月份
                        //      d-1位日期，dd-2位日期
                        //等同于日期格式变化
                        case "{D:":
                            if (OkStr[i].Length > 4)
                            {
                                //将大写的Y替换成小写的y，将小写的m替换成大写的m，将大写的D替换成小写的d
                                string sinput = OkStr[i].Substring(3, OkStr[i].Length - 4);
                                sinput = sinput.Replace("Y", "y");
                                sinput = sinput.Replace("m", "M");
                                sinput = sinput.Replace("D", "d");
                                resultStr += DateTime.Now.ToString(OkStr[i].Substring(3, OkStr[i].Length - 4));
                            }
                            break;
                        case "{N:":
                            if (OkStr[i].Length > 4)
                            {
                                resultStr += (maxNum + 1).ToString().PadLeft(int.Parse(OkStr[i].Substring(3, OkStr[i].Length - 4)), '0');
                            }
                            break;
                        //当前单位编号
                        case "{C}":
                            if (OkStr[i].Length == 3)
                            {
                                resultStr += companyCode;
                            }
                            break;
                        //当前检测员编号
                        case "{U}":
                            if (OkStr[i].Length == 3)
                            {
                                resultStr += userCode;
                            }
                            break;
                        //但前地区编号
                        case "{A}":
                            if (OkStr[i].Length == 3)
                            {
                                resultStr += areaCode;
                            }
                            break;
                    }
                }
            }
            return resultStr;
        }

        /// <summary>
        /// 获取仪器信息，每个仪器对应不同的：代码，检测项目，标准，端口
        /// </summary>
        /// <param name="machinModel"></param>
        public static void GetMachineSetting(string machinModel)
        {
            string query = string.Empty;
            DataTable dtbl = null;
            if (string.IsNullOrEmpty(machinModel))
            {
                MessageBox.Show("系统设置中仪器设置错误", "严重错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            machinModel = machinModel.ToUpper();
            string temp = string.Empty;
            //修改这种方式，比较灵活，扩展新仪器的时候不用修改此处 只要按照规则填写
            //如：增加新仪器填写“仪器型号代码:(DY3900DY)，扩展插件:RS232DY3900DY 。
            if (machinModel.IndexOf("DYRSY2") >= 0)//如果是旧版肉类水分仪 特殊处理
            {
                query = "Protocol='RS232水分仪插件' AND MachineModel='DYRSY2'";
            }
            else if (machinModel.IndexOf("0DY") > 0)//如果是新版DY3000(后有DY)系列与旧版DY3000区分
            {
                temp = "DY3000DY";
                query = string.Format("Protocol='RS232{0}' AND MachineModel='DY{1}'", temp, machinModel.Replace("DY", ""));
            }
            else if (machinModel.IndexOf("0LD") > 0)//如果是雷度系列版本如:DY5000LD,DY5500LD
            {
                temp = "DY5000LD";
                query = string.Format("Protocol='RS232{0}' AND MachineModel='{1}'", temp, machinModel.Replace("LD", ""));
            }
            else if (machinModel.Equals("LZ4000LZ"))
            {
                query = string.Format("Protocol='RS232LZ4000TDY' AND MachineModel='LZ4000'");
            }
            else if (machinModel.Equals("LZ4000TLZ"))
            {
                query = string.Format("Protocol='RS232LZ4000TDY' AND MachineModel='LZ4000T'");
            }
            else if (machinModel.Equals("DY-5000食品安全综合分析仪"))
            {
                query = string.Format("Name='{0}'", machinModel);
            }
            else
            {
                query = string.Format("Protocol='RS232{0}' AND MachineModel='{0}'", machinModel);
            }
            clsMachineOpr bll = new clsMachineOpr();
            dtbl = bll.GetAsDataTable(query, "ID", 2);

            if (bll == null || dtbl.Rows.Count <= 0)
            {
                MessageBox.Show("系统设置中仪器设置错误", "严重错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int i = 0;
                //ShareOption.DefaultMachineCode = dtbl.Rows[i]["SysCode"].ToString();
                ShareOption.DefaultCheckItemCode = dtbl.Rows[i]["ChkStdCode"].ToString();
                ShareOption.ComPort = dtbl.Rows[i]["communication"].ToString() ; //"COM" + dtbl.Rows[i]["LinkComNo"].ToString() + ":";
                //ShareOption.DefaultLimitValue = Convert.ToDecimal(dtbl.Rows[i]["TestValue"].ToString());
            }
        }

        /// <summary>
        /// 运行缓存
        /// </summary>
        /// <param name="iType"></param>
        public static void RunExeCache(int iType)
        {
            clsDistrictOpr oprDistrict = new clsDistrictOpr();
            clsUserUnitOpr oprUserUnit = new clsUserUnitOpr();
            clsUserInfoOpr oprUserInfo = new clsUserInfoOpr();
            switch (iType)
            {
                case 1:
                    ShareOption.DtblDistrict = oprDistrict.GetAsDataTable("IsLock=false", "SysCode", 1);
                    break;
                case 2:
                    ShareOption.DtblCheckCompany = oprUserUnit.GetAsDataTable("IsLock=false", "SysCode", 1);
                    break;
                case 3:
                    ShareOption.DtblChecker = oprUserInfo.GetAsDataTable("IsLock=false", "UserCode", 1);
                    break;
                case 10:
                    ShareOption.DtblDistrict = oprDistrict.GetAsDataTable("IsLock=false", "SysCode", 1);
                    ShareOption.DtblCheckCompany = oprUserUnit.GetAsDataTable("IsLock=false", "SysCode", 1);
                    ShareOption.DtblChecker = oprUserInfo.GetAsDataTable("IsLock=false", "UserCode", 1);//检测人
                    break;
            }
            ShareOption.IsRunCache = true;
        }

        /// <summary>
        /// 设置各种标题名称
        /// </summary>
        public static void GetTitleSet()
        {
            DataTable dtbl = null;
            ShareOption.AreaTitle = "所属组织";// "所属市场";
            ShareOption.NameTitle = "受检人/单位";
            ShareOption.DomainTitle = "档口/店面/车牌号";// "档口/店面/车牌号";
            ShareOption.SampleTitle = "样品";//名称 "商品名称";
            try
            {
                clsCheckComTypeOpr bll = new clsCheckComTypeOpr();
                dtbl = bll.GetAsDataTable(string.Empty, "ID", 2);
                object obj = null;
                if (dtbl != null && dtbl.Rows.Count >= 1)
                {
                    obj = dtbl.Rows[0]["AreaTitle"];
                    if (obj != null && obj.ToString().Trim() != string.Empty)
                    {
                        ShareOption.AreaTitle = obj.ToString();
                    }
                    obj = dtbl.Rows[0]["NameTitle"];
                    if (obj != null && obj.ToString().Trim() != string.Empty)
                    {
                        ShareOption.NameTitle = obj.ToString();
                    }
                    obj = dtbl.Rows[0]["DomainTitle"];
                    if (obj != null && obj.ToString().Trim() != string.Empty)
                    {
                        ShareOption.DomainTitle = obj.ToString();
                    }
                    obj = dtbl.Rows[0]["SampleTitle"];
                    if (obj != null && obj.ToString().Trim() != string.Empty)
                    {
                        ShareOption.SampleTitle = obj.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据配置存在错误：" + ex.Message);
            }
        }

        /// <summary>
        /// 连接J2EE接口并下载
        /// </summary>
        /// <param name="stdCode">标准代码</param>
        /// <param name="districtCode">区域编码</param>
        /// <param name="sign">标识</param>
        /// <returns></returns>
        public static DataSet GetJ2EEData(string stdCode, string districtCode, string sign)
        {
            DataSet dst = null;
            //FoodClient.localhost.DataSyncService ws = new FoodClient.localhost.DataSyncService();
            //ws.Url = ShareOption.SysServerUrl;
            //string ret = ws.GetDataDriverBySign(ShareOption.SystemVersion, districtCode, stdCode, ShareOption.SysServerUser, FormsAuthentication.HashPasswordForStoringInConfigFile(ShareOption.SysServerPwd, "MD5").ToString(), sign);

            //if (ret.Length >= 10 && ret.Substring(0, 10).Equals("errorInfo:"))
            //{
            //    throw new Exception("同步数据失败，失败原因：" + ret.Substring(10, ret.Length - 10));
            //}
            //else
            //{
            //    dst = new DataSet();
            //    using (StringReader sr = new StringReader(ret))
            //    {
            //        dst.ReadXml(sr);
            //    }
            //}
            return dst;
        }

        /// <summary>
        /// 新版本接口-数据下载 2016年12月8日 wenj
        /// </summary>
        /// <param name="version">版本：“行政版”或“企业版”</param>
        /// <param name="url">服务器地址</param>
        /// <param name="username">操作用户名</param>
        /// <param name="pwd">md5加密后的用户密码</param>
        /// <param name="sign">下载数据标志，all（全部），FoodClass（样品类别），CheckComTypeOpr（检测点类型），StandardType（检测标准类型），
        ///                     Standard（检测标准），CheckItem（检测项目），CompanyKind（被监管对象，单位类别） District（行政机构），
        ///                     ProduceArea（产品产地），Company（被监管对象），Dealer（被检经营户），CheckPlan（检测点检测任务下载），
        ///                     SelectItem（样品、检测项目和对应标准）</param>
        /// <param name="udate">更新时间，时间格式为：“2016-01-01”或 “2016-01-01 12：00：01”</param>
        /// <returns>XML字符串</returns>
        public static string GetXmlByService(String version, String url, String username, String pwd, String sign, String udate)
        {
            string rtnStr = string.Empty;
            try
            {
                //FoodClient.NewInterface.dataSync ds = new FoodClient.NewInterface.dataSync();
                //ds.Url = url;
                //rtnStr = ds.downLoadBaseData(version, username,
                //    FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5").ToString(), sign, udate);
            }
            catch (Exception)
            {
                return string.Empty;
            }
            return rtnStr;
        }

        /// <summary>
        /// 新版本接口-数据上传 2016年12月12日 wenj
        /// </summary>
        /// <param name="xml">XML字符串</param>
        /// <param name="username">操作用户名</param>
        /// <param name="pwd">加密后操作密码</param>
        /// <param name="companyCode">当前检测点单位编号</param>
        /// <param name="url">上传URL</param>
        /// <returns>XML字符串</returns>
        public static string Upload(String xml, String username, String pwd, String companyCode, String url)
        {
            string rtn = string.Empty;
            try
            {
                //FoodClient.NewInterface.dataSync ds = new FoodClient.NewInterface.dataSync();
                //ds.Url = url;
                //rtn = ds.uploadCheckData(xml, username, FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5").ToString(), companyCode);
            }
            catch (Exception ex)
            {
                rtn = ex.Message;
            }
            return rtn;
        }

        /// <summary>
        /// 旧版本接口，直接返回DataTable，在前台处理数据操作。
        /// 2016年12月7日 wenj
        /// </summary>
        /// <param name="stdCode">标准代码</param>
        /// <param name="districtCode">区域编码</param>
        /// <returns></returns>
        //public static DataTable GetFoodClass(string stdCode, string districtCode)
        //{
        //    string sign = "FoodClass";
        //    //旧版本接口
        //    //DataTable dtbl = GetJ2EEData(stdCode, districtCode, sign).Tables[sign];
        //    DataTable dtbl = null;
        //    FoodClient.NewInterface.dataSync ds = new FoodClient.NewInterface.dataSync();
        //    ds.downLoadBaseData(ShareOption.SystemVersion, "", "", sign, "");
        //    return dtbl;
        //}

        /// <summary>
        /// 下载食品种类
        /// </summary>
        /// <param name="stdCode">标准代码</param>
        /// <param name="districtCode">区域编码</param>
        //public static string DownloadFoodClass(string stdCode, string districtCode)
        //{
        //    string sign = "FoodClass";
        //    DataTable dtbl = null;
        //    dtbl = GetJ2EEData(stdCode, districtCode, sign).Tables[sign];

        //    string delErr = string.Empty;
        //    string err = string.Empty;
        //    StringBuilder sb = new StringBuilder();

            //食品种类
            //clsFoodClassOpr bll = new clsFoodClassOpr();
            //bll.Delete("IsReadOnly=true", out delErr);
            //sb.Append(delErr);
            //if (delErr != string.Empty)
            //{
            //    return delErr;
            //}
            //if (dtbl == null)
            //{
            //    return "暂无样品种类数据";
            //}
            //int len = dtbl.Rows.Count;
            //clsFoodClass foodmodel = new clsFoodClass();
            //for (int i = 0; i < len; i++)
            //{
            //    err = string.Empty;

            //    foodmodel.SysCode = dtbl.Rows[i]["SysCode"].ToString();
            //    foodmodel.StdCode = dtbl.Rows[i]["StdCode"].ToString();
            //    foodmodel.Name = dtbl.Rows[i]["Name"].ToString();
            //    foodmodel.ShortCut = dtbl.Rows[i]["ShortCut"].ToString();
            //    foodmodel.CheckLevel = dtbl.Rows[i]["CheckLevel"].ToString();
            //    foodmodel.CheckItemCodes = dtbl.Rows[i]["CheckItemCodes"].ToString();
            //    foodmodel.CheckItemValue = dtbl.Rows[i]["CheckItemValue"].ToString();
            //    foodmodel.IsReadOnly = Convert.ToBoolean(dtbl.Rows[i]["IsReadOnly"]);
            //    foodmodel.IsLock = Convert.ToBoolean(dtbl.Rows[i]["IsLock"]);
            //    foodmodel.Remark = dtbl.Rows[i]["Remark"].ToString();
            //    bll.Insert(foodmodel, out err);
            //    if (!err.Equals(string.Empty))
            //    {
            //        sb.Append(err);
            //    }
            //}
            //if (sb.Length > 0)
            //{
            //    return sb.ToString();
            //}
            //return string.Format("已经成功下载{0}条样品种类数据", len.ToString());
        //}

        /// <summary>
        /// 下载检测点单位类别
        /// </summary>
        /// <param name="stdCode">标准代码</param>
        /// <param name="districtCode">区域编码</param>
        public static string DownloadCheckComTypeOpr(string stdCode, string districtCode)
        {
            string sign = "CheckComTypeOpr";
            DataTable dtbl = null;
            dtbl = GetJ2EEData(stdCode, districtCode, sign).Tables[sign];

            string delErr = string.Empty;
            string err = string.Empty;
            StringBuilder sb = new StringBuilder();
            clsCheckComTypeOpr bll = new clsCheckComTypeOpr();
            bll.Delete("IsReadOnly=true", out delErr);
            sb.Append(delErr);
            if (delErr != string.Empty)
            {
                return delErr;
            }
            if (dtbl == null)
            {
                return "暂无检测点类型数据";
            }
            int len = dtbl.Rows.Count;
            clsCheckComType model = new clsCheckComType();

            for (int i = 0; i < len; i++)
            {
                err = string.Empty;

                model.TypeName = dtbl.Rows[i]["TypeName"].ToString();
                model.NameCall = dtbl.Rows[i]["NameCall"].ToString();
                model.AreaCall = dtbl.Rows[i]["AreaCall"].ToString();
                model.VerType = dtbl.Rows[i]["VerType"].ToString();
                model.IsReadOnly = Convert.ToBoolean(dtbl.Rows[i]["IsReadOnly"]);
                model.IsLock = Convert.ToBoolean(dtbl.Rows[i]["IsLock"]);
                model.ComKind = dtbl.Rows[i]["ComKind"].ToString();

                //bll.Insert(model, out err);
                if (!err.Equals(string.Empty))
                {
                    sb.Append(err);
                }
            }
            if (sb.Length > 0)
            {
                return sb.ToString();
            }
            return string.Format("已经成功下载{0}条检测点类型数据", len.ToString());
        }

        /// <summary>
        /// 下载检测标准类型
        /// </summary>
        /// <param name="stdCode">标准代码</param>
        /// <param name="districtCode">区域编码</param>
        public static string DownloadStandardType(string stdCode, string districtCode)
        {
            string sign = "StandardType";
            DataTable dtbl = null;
            dtbl = GetJ2EEData(stdCode, districtCode, sign).Tables[sign];
            StringBuilder sb = new StringBuilder();
            string delErr = string.Empty;
            string err = string.Empty;

            //clsStandardTypeOpr bll = new clsStandardTypeOpr();
            //bll.Delete("IsReadOnly=true", out delErr);
            sb.Append(delErr);
            if (delErr != string.Empty)
            {
                return delErr;
            }
            if (dtbl == null)
            {
                return "暂无标准类型数据";
            }
            int len = dtbl.Rows.Count;
            //clsStandardType model = new clsStandardType();

            //for (int i = 0; i < len; i++)
            //{
            //    err = string.Empty;

            //    model.StdName = dtbl.Rows[i]["StdName"].ToString();
            //    model.IsReadOnly = Convert.ToBoolean(dtbl.Rows[i]["IsReadOnly"]);
            //    model.IsLock = Convert.ToBoolean(dtbl.Rows[i]["IsLock"]);
            //    model.Remark = string.Empty;

            //    bll.Insert(model, out err);
            //    if (!err.Equals(string.Empty))
            //    {
            //        sb.Append(err);
            //    }
            //}
            if (sb.Length > 0)
            {
                return sb.ToString();
            }
            return string.Format("已经成功下载{0}条检测标准类别数据", len.ToString());
        }

        /// <summary>
        /// 下载检测标准
        /// </summary>
        /// <param name="stdCode">标准代码</param>
        /// <param name="districtCode">区域编码</param>
        public static string DownloadStandard(string stdCode, string districtCode)
        {
            string sign = "Standard";
            DataTable dtbl = null;
            dtbl = GetJ2EEData(stdCode, districtCode, sign).Tables[sign];
            StringBuilder sb = new StringBuilder();
            string delErr = string.Empty;
            string err = string.Empty;

            //clsStandardOpr bll = new clsStandardOpr();
            //bll.Delete("IsReadOnly=true", out delErr);
            //sb.Append(delErr);
            if (delErr != string.Empty)
            {
                return delErr;
            }
            if (dtbl == null)
            {
                return "暂无检测标准数据";
            }
            int len = dtbl.Rows.Count;
            //clsStandard model = new clsStandard();

            //for (int i = 0; i < len; i++)
            //{
            //    err = string.Empty;
            //    model.SysCode = dtbl.Rows[i]["SysCode"].ToString();
            //    model.StdCode = dtbl.Rows[i]["StdCode"].ToString();
            //    model.StdDes = dtbl.Rows[i]["StdDes"].ToString();
            //    model.ShortCut = dtbl.Rows[i]["ShortCut"].ToString();
            //    model.StdInfo = dtbl.Rows[i]["StdInfo"].ToString();
            //    model.StdType = dtbl.Rows[i]["StdType"].ToString();
            //    model.IsReadOnly = Convert.ToBoolean(dtbl.Rows[i]["IsReadOnly"]);
            //    model.IsLock = Convert.ToBoolean(dtbl.Rows[i]["IsLock"]);
            //    model.Remark = dtbl.Rows[i]["Remark"].ToString();

            //    bll.Insert(model, out err);
            //    if (!err.Equals(string.Empty))
            //    {
            //        sb.AppendLine(err);
            //    }
            //}
            if (sb.Length > 0)
            {
                return sb.ToString();
            }
            return string.Format("已经成功下载{0}条检测标准数据", len.ToString());
        }

        /// <summary>
        /// 下载检测项目
        /// </summary>
        /// <param name="stdCode">标准代码</param>
        /// <param name="districtCode">区域编码</param>
        public static string DownloadCheckItem(string stdCode, string districtCode)
        {
            string sign = "CheckItem";
            DataTable dtbl = null;
            dtbl = GetJ2EEData(stdCode, districtCode, sign).Tables[sign];
            StringBuilder sb = new StringBuilder();
            string delErr = string.Empty;
            string err = string.Empty;
            //clsCheckItemOpr bll = new clsCheckItemOpr();
            //bll.Delete("IsReadOnly=true", out delErr);
            //sb.Append(delErr);
            //clsCheckItem model = new clsCheckItem();
            if (delErr != string.Empty)
            {
                return delErr;
            }
            if (dtbl == null)
            {
                return "暂无检测项目数据";
            }
            int len = dtbl.Rows.Count;
            //for (int i = 0; i < len; i++)
            //{
            //    err = string.Empty;
            //    model.SysCode = dtbl.Rows[i]["SysCode"].ToString();
            //    model.StdCode = dtbl.Rows[i]["StdCode"].ToString();
            //    model.ItemDes = dtbl.Rows[i]["ItemDes"].ToString();
            //    model.CheckType = dtbl.Rows[i]["CheckType"].ToString();
            //    model.StandardCode = dtbl.Rows[i]["StandardCode"].ToString();
            //    model.StandardValue = dtbl.Rows[i]["StandardValue"].ToString();
            //    model.Unit = dtbl.Rows[i]["Unit"].ToString();
            //    model.DemarcateInfo = dtbl.Rows[i]["DemarcateInfo"].ToString();
            //    model.TestValue = dtbl.Rows[i]["TestValue"].ToString();
            //    model.OperateHelp = dtbl.Rows[i]["OperateHelp"].ToString();
            //    model.CheckLevel = dtbl.Rows[i]["CheckLevel"].ToString();
            //    model.IsReadOnly = Convert.ToBoolean(dtbl.Rows[i]["IsReadOnly"]);
            //    model.IsLock = Convert.ToBoolean(dtbl.Rows[i]["IsLock"]);
            //    model.Remark = dtbl.Rows[i]["Remark"].ToString();

            //    bll.Insert(model, out err);
            //    if (!err.Equals(string.Empty))
            //    {
            //        sb.AppendLine(err);
            //    }
            //}
            if (sb.Length > 0)
            {
                return sb.ToString();
            }
            return string.Format("已经成功下载{0}条检测项目数据", len.ToString());
        }

        /// <summary>
        /// 下载行政机构
        /// </summary>
        /// <param name="stdCode">标准代码</param>
        /// <param name="districtCode">区域编码</param>
        public static string DownloadDistrict(string stdCode, string districtCode)
        {
            string sign = "District";
            DataTable dtbl = null;
            dtbl = GetJ2EEData(stdCode, districtCode, sign).Tables[sign];
            StringBuilder sb = new StringBuilder();
            string delErr = string.Empty;
            string err = string.Empty;

            clsDistrictOpr bll = new clsDistrictOpr();
            bll.Delete("IsReadOnly = true", out delErr);
            sb.Append(delErr);
            if (delErr != string.Empty)
            {
                return delErr;
            }
            if (dtbl == null)
            {
                return "暂无行政机构数据";
            }
            int len = dtbl.Rows.Count;

            clsDistrict model = new clsDistrict();

            for (int i = 0; i < len; i++)
            {
                err = string.Empty;
                model.SysCode = dtbl.Rows[i]["SysCode"].ToString();
                model.StdCode = dtbl.Rows[i]["StdCode"].ToString();
                model.Name = dtbl.Rows[i]["Name"].ToString();
                model.ShortCut = dtbl.Rows[i]["ShortCut"].ToString();
                model.DistrictIndex = Convert.ToInt64(dtbl.Rows[i]["DistrictIndex"]);
                model.CheckLevel = dtbl.Rows[i]["CheckLevel"].ToString();
                model.IsLocal = Convert.ToBoolean(dtbl.Rows[i]["IsLocal"]);
                model.IsReadOnly = Convert.ToBoolean(dtbl.Rows[i]["IsReadOnly"]);
                model.IsLock = Convert.ToBoolean(dtbl.Rows[i]["IsLock"]);
                model.Remark = dtbl.Rows[i]["Remark"].ToString();

                bll.Insert(model, out err);
                if (!err.Equals(string.Empty))
                {
                    sb.AppendLine(err);
                }
            }
            if (sb.Length > 0)
            {
                return sb.ToString();
            }
            return string.Format("已经成功下载{0}条行政机构数据", len.ToString());
        }


        /// <summary>
        /// 下载产品产地
        /// </summary>
        /// <param name="stdCode">标准代码</param>
        /// <param name="districtCode">区域编码</param>
        //public static string DownloadProduceArea(string stdCode, string districtCode)
        //{
        //    string sign = "ProduceArea";
        //    DataTable dtbl = null;
        //    dtbl = GetJ2EEData(stdCode, districtCode, sign).Tables[sign];
        //    StringBuilder sb = new StringBuilder();
        //    string delErr = string.Empty;
        //    string err = string.Empty;

        //    //clsProduceAreaOpr bll = new clsProduceAreaOpr();
        //    //bll.Delete("IsReadOnly=true", out delErr);
        //    //sb.Append(delErr);
        //    if (delErr != string.Empty)
        //    {
        //        return delErr;
        //    }
        //    if (dtbl == null)
        //    {
        //        return "暂无产品产地数据";
        //    }
        //    int len = dtbl.Rows.Count;

            //clsProduceArea model = new clsProduceArea();

            //for (int i = 0; i < len; i++)
            //{
            //    err = string.Empty;
            //    model.SysCode = dtbl.Rows[i]["SysCode"].ToString();
            //    model.StdCode = dtbl.Rows[i]["StdCode"].ToString();
            //    model.Name = dtbl.Rows[i]["Name"].ToString();
            //    model.ShortCut = dtbl.Rows[i]["ShortCut"].ToString();
            //    model.DistrictIndex = Convert.ToInt64(dtbl.Rows[i]["DistrictIndex"]);
            //    model.CheckLevel = dtbl.Rows[i]["CheckLevel"].ToString();
            //    model.IsLocal = Convert.ToBoolean(dtbl.Rows[i]["IsLocal"]);
            //    model.IsReadOnly = Convert.ToBoolean(dtbl.Rows[i]["IsReadOnly"]);
            //    model.IsLock = Convert.ToBoolean(dtbl.Rows[i]["IsLock"]);
            //    model.Remark = string.Empty;

            //    bll.Insert(model, out err);
            //    if (!err.Equals(string.Empty))
            //    {
            //        sb.AppendLine(err);
            //    }
           // }
            //if (sb.Length > 0)
            //{
            //    return sb.ToString();
            //}
            //return string.Format("已经成功下载{0}条产品产地数据", len.ToString());
       // }

        /// <summary>
        /// 下载单位类别
        /// </summary>
        /// <param name="stdCode">标准代码</param>
        /// <param name="districtCode">区域编码</param>
        //public static string DownloadCompanyKind(string stdCode, string districtCode)
        //{
        //    string sign = "CompanyKind";
        //    DataTable dtbl = null;
        //    dtbl = GetJ2EEData(stdCode, districtCode, sign).Tables[sign];
        //    StringBuilder sb = new StringBuilder();
        //    string delErr = string.Empty;
        //    string err = string.Empty;

        //    clsCompanyKindOpr bll = new clsCompanyKindOpr();
        //    bll.Delete("IsReadOnly=true", out delErr);
        //    sb.Append(delErr);
        //    if (delErr != string.Empty)
        //    {
        //        return delErr;
        //    }
        //    if (dtbl == null)
        //    {
        //        return "暂无单位类别数据";
        //    }
        //    int len = dtbl.Rows.Count;
        //    clsCompanyKind model = new clsCompanyKind();
        //    for (int i = 0; i < len; i++)
        //    {
        //        err = string.Empty;

        //        model.SysCode = dtbl.Rows[i]["SysCode"].ToString();
        //        model.StdCode = dtbl.Rows[i]["StdCode"].ToString();
        //        model.Name = dtbl.Rows[i]["Name"].ToString();
        //        model.CompanyProperty = dtbl.Rows[i]["CompanyProperty"].ToString();
        //        model.IsReadOnly = Convert.ToBoolean(dtbl.Rows[i]["IsReadOnly"]);
        //        model.IsLock = Convert.ToBoolean(dtbl.Rows[i]["IsLock"]);
        //        model.Remark = dtbl.Rows[i]["Remark"].ToString();
        //        model.Ksign = dtbl.Rows[i]["Ksign"].ToString();

        //        bll.Insert(model, out err);
        //        if (!err.Equals(string.Empty))
        //        {
        //            sb.AppendLine(err);
        //        }
        //    }
        //    if (sb.Length > 0)
        //    {
        //        return sb.ToString();
        //    }
        //    return string.Format("已经成功下载{0}条单位类别数据", len.ToString());
        //}

        /// <summary>
        /// 下载单位信息
        /// </summary>
        /// <param name="stdCode">标准代码</param>
        /// <param name="districtCode">区域编码</param>
        //public static string DownloadCompany(string stdCode, string districtCode)
        //{
        //    string sign = "Company";
        //    DataTable dtbl = GetJ2EEData(stdCode, districtCode, sign).Tables[sign];
        //    StringBuilder sb = new StringBuilder();
        //    string delErr = string.Empty;
        //    string err = string.Empty;
        //    clsCompanyOpr bll = new clsCompanyOpr();
        //    #region
            //if (ShareOption.SystemVersion.Equals(ShareOption.LocalBaseVersion))//行政版
            //{
            //    bll.Delete("IsReadOnly=true", out delErr);
            //}
            //else
            //{
            //    bll.Delete(string.Format("IsReadOnly=true AND Property='{0}'", ShareOption.CompanyProperty1), out delErr);//企业版
            //}
            //不删除生产单位，只删除被检单位
            //bll.Delete(string.Format("IsReadOnly=true AND Property='{0}'", ShareOption.CompanyProperty1), out delErr);
            //#endregion
            //bll.Delete("IsReadOnly=true and TSign<>'本地增'", out delErr);
            //sb.Append(delErr);
            //if (dtbl == null)
            //{
            //    return "暂无被检单位数据";
            //}
            //int len = dtbl.Rows.Count;
            //clsCompany model = new clsCompany();
            //for (int i = 0; i < len; i++)
            //{
            //    #region
                //model.ShortCut = dtbl.Rows[i]["ShortCut"].ToString();
                //model.OtherCodeInfo = dtbl.Rows[i]["OtherCodeInfo"].ToString();
                //model.ShortName = dtbl.Rows[i]["ShortName"].ToString();
                //model.CAllow = dtbl.Rows[i]["LICENSEID"].ToString();
                //model.ISSUEAGENCY = dtbl.Rows[i]["ISSUEAGENCY"].ToString();
                //model.ISSUEDATE = dtbl.Rows[i]["ISSUEDATE"].ToString();
                //model.PERIODSTART = dtbl.Rows[i]["PERIODSTART"].ToString();
                //model.PERIODEND = dtbl.Rows[i]["PERIODEND"].ToString();
                //model.VIOLATENUM = dtbl.Rows[i]["VIOLATENUM"].ToString();
                //model.LONGITUDE = dtbl.Rows[i]["LONGITUDE"].ToString();
                //model.LATITUDE = dtbl.Rows[i]["LATITUDE"].ToString();
                //model.SCOPE = dtbl.Rows[i]["SCOPE"].ToString();
                //model.PUNISH = dtbl.Rows[i]["PUNISH"].ToString(); 
            //    #endregion
            //    err = string.Empty;
            //    model.SysCode = dtbl.Rows[i]["SysCode"].ToString();
            //    model.StdCode = dtbl.Rows[i]["StdCode"].ToString();
            //    model.CompanyID = dtbl.Rows[i]["CompanyID"].ToString();
            //    model.FullName = dtbl.Rows[i]["FullName"].ToString();
            //    model.DisplayName = dtbl.Rows[i]["DisplayName"].ToString();

            //    model.Property = dtbl.Rows[i]["Property"].ToString();
            //    model.KindCode = dtbl.Rows[i]["KindCode"].ToString();
            //    model.RegCapital = Convert.ToInt64(dtbl.Rows[i]["RegCapital"]);
            //    model.Unit = dtbl.Rows[i]["Unit"].ToString();
            //    model.Incorporator = dtbl.Rows[i]["Incorporator"].ToString();
            //    if (!string.IsNullOrEmpty(dtbl.Rows[i]["RegDate"].ToString()))
            //    {
            //        model.RegDate = Convert.ToDateTime(dtbl.Rows[i]["RegDate"]);
            //    }
            //    model.DistrictCode = dtbl.Rows[i]["DistrictCode"].ToString();
            //    model.PostCode = dtbl.Rows[i]["PostCode"].ToString();
            //    model.Address = dtbl.Rows[i]["Address"].ToString();
            //    model.LinkMan = dtbl.Rows[i]["LinkMan"].ToString();
            //    model.LinkInfo = dtbl.Rows[i]["LinkInfo"].ToString();
            //    model.CreditLevel = dtbl.Rows[i]["CreditLevel"].ToString();
            //    model.CreditRecord = dtbl.Rows[i]["CreditRecord"].ToString();
            //    model.ProductInfo = dtbl.Rows[i]["ProductInfo"].ToString();
            //    model.OtherInfo = dtbl.Rows[i]["OtherInfo"].ToString();
            //    model.FoodSafeRecord = dtbl.Rows[i]["FoodSafeRecord"].ToString();
            //    model.CheckLevel = dtbl.Rows[i]["CheckLevel"].ToString();
            //    model.IsReadOnly = Convert.ToBoolean(dtbl.Rows[i]["IsReadOnly"]);
            //    model.IsLock = Convert.ToBoolean(dtbl.Rows[i]["IsLock"]);
            //    model.Remark = dtbl.Rows[i]["Remark"].ToString();
            //    model.TSign = dtbl.Rows[i]["Sign"].ToString();

            //    bll.Insert(model, out err);
            //    if (!err.Equals(string.Empty))
            //    {
            //        sb.AppendLine(err);
            //    }
            //}
            //if (sb.Length > 0)
            //{
            //    return sb.ToString();
            //}
            //return string.Format("已经成功下载{0}条单位信息数据", len.ToString());
        //}

        //public static string DownloadProprietors(string stdCode, string districtCode)
        //{
        //    string sign = "DEALER";
        //    DataTable dtbl = GetJ2EEData(stdCode, districtCode, sign).Tables[sign];
        //    StringBuilder sb = new StringBuilder();
        //    string delErr = string.Empty;
        //    string err = string.Empty;

        //    clsProprietorsOpr bll = new clsProprietorsOpr();
        //    bll.Delete("IsReadOnly=true ", out delErr);
        //    sb.Append(delErr);
        //    if (dtbl == null)
        //    {
        //        return "暂无经营户数据";
        //    }
        //    int len = dtbl.Rows.Count;
        //    clsProprietors model = new clsProprietors();
        //    for (int i = 0; i < len; i++)
        //    {
        //        err = string.Empty;
        //        model.Cdcode = dtbl.Rows[i]["Cdcode"].ToString();
        //        model.Cdbuslicence = dtbl.Rows[i]["Cdbuslicence"].ToString();
        //        model.Ciid = dtbl.Rows[i]["Ciid"].ToString();
        //        model.Ciname = dtbl.Rows[i]["Ciname"].ToString();
        //        model.Cdname = dtbl.Rows[i]["Cdname"].ToString();
        //        model.Cdcardid = dtbl.Rows[i]["Cdcardid"].ToString();
        //        model.DisplayName = dtbl.Rows[i]["DisplayName"].ToString();
        //        model.Property = dtbl.Rows[i]["Property"].ToString();
        //        model.KindCode = dtbl.Rows[i]["KindCode"].ToString();
        //        model.RegCapital = dtbl.Rows[i]["RegCapital"].ToString();
        //        model.Unit = dtbl.Rows[i]["Unit"].ToString();
        //        model.Incorporator = dtbl.Rows[i]["Incorporator"].ToString();
        //        if (!string.IsNullOrEmpty(dtbl.Rows[i]["RegDate"].ToString()))
        //        {
        //            model.RegDate = Convert.ToDateTime(dtbl.Rows[i]["RegDate"]);
        //        }
        //        model.DistrictCode = dtbl.Rows[i]["DistrictCode"].ToString();
        //        model.PostCode = dtbl.Rows[i]["PostCode"].ToString();
        //        model.Address = dtbl.Rows[i]["Address"].ToString();
        //        model.LinkMan = dtbl.Rows[i]["LinkMan"].ToString();
        //        model.LinkInfo = dtbl.Rows[i]["LinkInfo"].ToString();
        //        model.CreditLevel = dtbl.Rows[i]["CreditLevel"].ToString();
        //        model.CreditRecord = dtbl.Rows[i]["CreditRecord"].ToString();
        //        model.ProductInfo = dtbl.Rows[i]["ProductInfo"].ToString();
        //        model.OtherInfo = dtbl.Rows[i]["OtherInfo"].ToString();
        //        model.CheckLevel = dtbl.Rows[i]["CheckLevel"].ToString();
        //        model.FoodSafeRecord = dtbl.Rows[i]["FoodSafeRecord"].ToString();
        //        model.IsLock = Convert.ToBoolean(dtbl.Rows[i]["IsLock"]);
        //        model.IsReadOnly = Convert.ToBoolean(dtbl.Rows[i]["IsReadOnly"]);
        //        model.Remark = dtbl.Rows[i]["Remark"].ToString();

        //        bll.Insert(model, out err);
        //        if (!err.Equals(string.Empty))
        //        {
        //            sb.AppendLine(err);
        //        }
        //    }
        //    if (sb.Length > 0)
        //    {
        //        return sb.ToString();
        //    }
        //    return string.Format("已经成功下载{0}条经营户数据", len.ToString());
        //}

        /// <summary>
        /// 基础数据同步
        /// </summary>
        /// <param name="stdCode">检测点代码</param>
        /// <param name="sumError">错误信息</param>
        //public static string DownloadAll(string stdCode, string districtCode, out string sumError, string sign)
        //{
        //    sumError = string.Empty;
        //    try
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        sb.AppendLine(DownloadFoodClass(stdCode, districtCode));//样品类别

        //        sb.AppendLine(DownloadCheckComTypeOpr(stdCode, districtCode));//检测单位类别

        //        sb.AppendLine(DownloadStandardType(stdCode, districtCode));//检测标准类型

        //        sb.AppendLine(DownloadStandard(stdCode, districtCode));//检测标准

        //        sb.AppendLine(DownloadCheckItem(stdCode, districtCode)); //检测项目

        //        sb.AppendLine(DownloadCompanyKind(stdCode, districtCode)); //单位类别

        //        sb.AppendLine(DownloadDistrict(stdCode, districtCode)); //组织机构

        //        sb.AppendLine(DownloadProduceArea(stdCode, districtCode)); //产地

        //        sb.AppendLine(DownloadCompany(stdCode, districtCode));//单位信息

        //        sb.AppendLine(DownloadProprietors(stdCode, districtCode));//经营户信息

        //        return sb.ToString();

                //#region 注掉
                //检测类别
                //				clsCheckTypeOpr Opr2=new clsCheckTypeOpr();
                //				Opr2.Delete("IsReadOnly=true",out sDelErr);
                //
                //				dt=dsRt.Tables["CheckType"];
                //				for(int i=0;i<dt.Rows.Count;i++)
                //				{
                //					string sErr="";
                //
                //					clsCheckType checkType=new clsCheckType();
                //					checkType.Name=dt.Rows[i]["Name"].ToString();
                //					checkType.IsReadOnly=Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
                //					checkType.IsLock=Convert.ToBoolean(dt.Rows[i]["IsLock"]);
                //					checkType.Remark=dt.Rows[i]["Remark"].ToString();
                //
                //					Opr2.Insert(checkType,out sErr);
                //					if(!sErr.Equals(""))
                //					{
                //						sSumError+=sErr + "\r\n";
                //						continue;
                //					}
                //				}
                //				//检测级别
                //				clsCheckLevelOpr Opr8=new clsCheckLevelOpr();
                //				Opr8.Delete("IsReadOnly=true",out sDelErr);
                //
                //				dt=dsRt.Tables["CheckLevel"];
                //				for(int i=0;i<dt.Rows.Count;i++)
                //				{
                //					string sErr="";
                //
                //					clsCheckLevel checkLevel=new clsCheckLevel();
                //					checkLevel.CheckLevel=dt.Rows[i]["CheckLevel"].ToString();
                //					checkLevel.IsReadOnly=Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
                //					checkLevel.IsLock=Convert.ToBoolean(dt.Rows[i]["IsLock"]);
                //					checkLevel.Remark=dt.Rows[i]["Remark"].ToString();
                //
                //					Opr8.Insert(checkLevel,out sErr);
                //					if(!sErr.Equals(""))
                //					{
                //						sSumError+=sErr + "\r\n";
                //						continue;
                //					}
                //				}		

                //				//信用级别
                //				clsCreditLevelOpr Opr9=new clsCreditLevelOpr();
                //				Opr9.Delete("IsReadOnly=true",out sDelErr);
                //
                //				dt=dsRt.Tables["CreditLevel"];
                //				for(int i=0;i<dt.Rows.Count;i++)
                //				{
                //					string sErr="";
                //
                //					clsCreditLevel creditLevel=new clsCreditLevel();
                //					creditLevel.CreditLevel=dt.Rows[i]["CreditLevel"].ToString();
                //					creditLevel.IsReadOnly=Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
                //					creditLevel.IsLock=Convert.ToBoolean(dt.Rows[i]["IsLock"]);
                //					creditLevel.Remark=dt.Rows[i]["Remark"].ToString();
                //
                //					Opr9.Insert(creditLevel,out sErr);
                //					if(!sErr.Equals(""))
                //					{
                //						sSumError+=sErr + "\r\n";
                //						continue;
                //					}
                //				}			

                //公司属性
                //				clsCompanyPropertyOpr Opr11=new clsCompanyPropertyOpr();
                //				Opr11.Delete("IsReadOnly=true",out sDelErr);
                //
                //				dt=dsRt.Tables["CompanyProperty"];
                //				for(int i=0;i<dt.Rows.Count;i++)
                //				{
                //					string sErr="";
                //
                //					clsCompanyProperty companyProperty=new clsCompanyProperty();
                //					companyProperty.CompanyProperty=dt.Rows[i]["CompanyProperty"].ToString();
                //					companyProperty.IsReadOnly=Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
                //					companyProperty.IsLock=Convert.ToBoolean(dt.Rows[i]["IsLock"]);
                //					companyProperty.Remark=dt.Rows[i]["Remark"].ToString();
                //
                //					Opr11.Insert(companyProperty,out sErr);
                //					if(!sErr.Equals(""))
                //					{
                //						sSumError+=sErr + "\r\n";
                //						continue;
                //					}
                //				}		
                //#endregion

                //#region DotNET接口版本，暂不支持
                //else if (ShareOption.InterfaceType.Equals(ShareOption.InterfaceDotNET))
                //{

                //FoodClient.ForNet.DataDriver ws = new FoodClient.ForNet.DataDriver();
                //ws.Url = ShareOption.SysServerIP;
                //DataSet dsRt = ws.GetDataDriver(ShareOption.SystemVersion, districtCode, stdCode, ShareOption.SysServerID, ShareOption.SysServerPass, out sSumError);
                //if (!sSumError.Equals(""))
                //{
                //    sSumError = "同步数据失败，失败原因：" + sSumError;
                //    return;
                //}
                //DataTable dt = null;
                //string sDelErr = string.Empty;

                ////食品种类
                //clsFoodClassOpr Opr1 = new clsFoodClassOpr();
                //Opr1.Delete("IsReadOnly=true", out sDelErr);
                //dt = dsRt.Tables["FoodClass"];
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string sErr = "";

                //    clsFoodClass foodClass = new clsFoodClass();
                //    foodClass.SysCode = dt.Rows[i]["SysCode"].ToString();
                //    foodClass.StdCode = dt.Rows[i]["StdCode"].ToString();
                //    foodClass.Name = dt.Rows[i]["Name"].ToString();
                //    foodClass.ShortCut = dt.Rows[i]["ShortCut"].ToString();
                //    foodClass.CheckLevel = dt.Rows[i]["CheckLevel"].ToString();
                //    foodClass.CheckItemCodes = dt.Rows[i]["CheckItemCodes"].ToString();
                //    foodClass.CheckItemValue = dt.Rows[i]["CheckItemValue"].ToString();
                //    foodClass.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
                //    foodClass.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
                //    foodClass.Remark = dt.Rows[i]["Remark"].ToString();

                //    Opr1.Insert(foodClass, out sErr);
                //    if (!sErr.Equals(""))
                //    {
                //        sSumError += sErr + "\r\n";
                //        continue;
                //    }
                //}

                ////检测类别
                //clsCheckTypeOpr Opr2 = new clsCheckTypeOpr();
                //Opr2.Delete("IsReadOnly=true", out sDelErr);

                //dt = dsRt.Tables["CheckType"];
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string sErr = "";

                //    clsCheckType checkType = new clsCheckType();
                //    checkType.Name = dt.Rows[i]["Name"].ToString();
                //    checkType.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
                //    checkType.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
                //    checkType.Remark = dt.Rows[i]["Remark"].ToString();

                //    Opr2.Insert(checkType, out sErr);
                //    if (!sErr.Equals(""))
                //    {
                //        sSumError += sErr + "\r\n";
                //        continue;
                //    }
                //}

                ////检测点单位类别
                //clsCheckComTypeOpr Opr18 = new clsCheckComTypeOpr();
                //Opr18.Delete("IsReadOnly=true", out sDelErr);

                //dt = dsRt.Tables["CheckComTypeOpr"];
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string sErr = "";

                //    clsCheckComType checkcomType = new clsCheckComType();
                //    checkcomType.TypeName = dt.Rows[i]["TypeName"].ToString();
                //    checkcomType.NameCall = dt.Rows[i]["NameCall"].ToString();
                //    checkcomType.AreaCall = dt.Rows[i]["AreaCall"].ToString();
                //    checkcomType.VerType = dt.Rows[i]["VerType"].ToString();
                //    checkcomType.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
                //    checkcomType.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
                //    checkcomType.ComKind = dt.Rows[i]["ComKind"].ToString();

                //    Opr18.Insert(checkcomType, out sErr);
                //    if (!sErr.Equals(""))
                //    {
                //        sSumError += sErr + "\r\n";
                //        continue;
                //    }
                //}

                ////检测标准类型
                //clsStandardTypeOpr Opr7 = new clsStandardTypeOpr();
                //Opr7.Delete("IsReadOnly=true", out sDelErr);

                //dt = dsRt.Tables["StandardType"];
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string sErr = "";

                //    clsStandardType standardType = new clsStandardType();
                //    standardType.StdName = dt.Rows[i]["StdName"].ToString();
                //    standardType.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
                //    standardType.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
                //    standardType.Remark = dt.Rows[i]["Remark"].ToString();

                //    Opr7.Insert(standardType, out sErr);
                //    if (!sErr.Equals(""))
                //    {
                //        sSumError += sErr + "\r\n";
                //        continue;
                //    }
                //}

                ////检测标准
                //clsStandardOpr Opr6 = new clsStandardOpr();
                //Opr6.Delete("IsReadOnly=true", out sDelErr);

                //dt = dsRt.Tables["Standard"];
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string sErr = "";

                //    clsStandard standard = new clsStandard();
                //    standard.SysCode = dt.Rows[i]["SysCode"].ToString();
                //    standard.StdCode = dt.Rows[i]["StdCode"].ToString();
                //    standard.StdDes = dt.Rows[i]["StdDes"].ToString();
                //    standard.ShortCut = dt.Rows[i]["ShortCut"].ToString();
                //    standard.StdInfo = dt.Rows[i]["StdInfo"].ToString();
                //    standard.StdType = dt.Rows[i]["StdType"].ToString();
                //    standard.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
                //    standard.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
                //    standard.Remark = dt.Rows[i]["Remark"].ToString();

                //    Opr6.Insert(standard, out sErr);
                //    if (!sErr.Equals(""))
                //    {
                //        sSumError += sErr + "\r\n";
                //        continue;
                //    }
                //}

                ////检测项目
                //clsCheckItemOpr Opr3 = new clsCheckItemOpr();
                //Opr3.Delete("IsReadOnly=true", out sDelErr);

                //dt = dsRt.Tables["CheckItem"];
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string sErr = "";

                //    clsCheckItem checkItem = new clsCheckItem();
                //    checkItem.SysCode = dt.Rows[i]["SysCode"].ToString();
                //    checkItem.StdCode = dt.Rows[i]["StdCode"].ToString();
                //    checkItem.ItemDes = dt.Rows[i]["ItemDes"].ToString();
                //    checkItem.CheckType = dt.Rows[i]["CheckType"].ToString();
                //    checkItem.StandardCode = dt.Rows[i]["StandardCode"].ToString();
                //    checkItem.StandardValue = dt.Rows[i]["StandardValue"].ToString();
                //    checkItem.Unit = dt.Rows[i]["Unit"].ToString();
                //    checkItem.DemarcateInfo = dt.Rows[i]["DemarcateInfo"].ToString();
                //    checkItem.TestValue = dt.Rows[i]["TestValue"].ToString();
                //    checkItem.OperateHelp = dt.Rows[i]["OperateHelp"].ToString();
                //    checkItem.CheckLevel = dt.Rows[i]["CheckLevel"].ToString();
                //    checkItem.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
                //    checkItem.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
                //    checkItem.Remark = dt.Rows[i]["Remark"].ToString();

                //    Opr3.Insert(checkItem, out sErr);
                //    if (!sErr.Equals(""))
                //    {
                //        sSumError += sErr + "\r\n";
                //        continue;
                //    }
                //}

                ////单位类别
                //clsCompanyKindOpr Opr4 = new clsCompanyKindOpr();
                //Opr4.Delete("IsReadOnly=true", out sDelErr);

                //dt = dsRt.Tables["CompanyKind"];
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string sErr = "";

                //    clsCompanyKind companyKind = new clsCompanyKind();
                //    companyKind.SysCode = dt.Rows[i]["SysCode"].ToString();
                //    companyKind.StdCode = dt.Rows[i]["StdCode"].ToString();
                //    companyKind.Name = dt.Rows[i]["Name"].ToString();
                //    companyKind.CompanyProperty = dt.Rows[i]["CompanyProperty"].ToString();
                //    companyKind.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
                //    companyKind.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
                //    companyKind.Remark = dt.Rows[i]["Remark"].ToString();

                //    Opr4.Insert(companyKind, out sErr);
                //    if (!sErr.Equals(""))
                //    {
                //        sSumError += sErr + "\r\n";
                //        continue;
                //    }
                //}

                ////组织机构
                //clsDistrictOpr Opr5 = new clsDistrictOpr();
                //Opr5.Delete("IsReadOnly=true", out sDelErr);

                //dt = dsRt.Tables["District"];
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string sErr = "";

                //    clsDistrict district = new clsDistrict();
                //    district.SysCode = dt.Rows[i]["SysCode"].ToString();
                //    district.StdCode = dt.Rows[i]["StdCode"].ToString();
                //    district.Name = dt.Rows[i]["Name"].ToString();
                //    district.ShortCut = dt.Rows[i]["ShortCut"].ToString();
                //    district.DistrictIndex = Convert.ToInt64(dt.Rows[i]["DistrictIndex"]);
                //    district.CheckLevel = dt.Rows[i]["CheckLevel"].ToString();
                //    district.IsLocal = Convert.ToBoolean(dt.Rows[i]["IsLocal"]);
                //    district.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
                //    district.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
                //    district.Remark = dt.Rows[i]["Remark"].ToString();

                //    Opr5.Insert(district, out sErr);
                //    if (!sErr.Equals(""))
                //    {
                //        sSumError += sErr + "\r\n";
                //        continue;
                //    }
                //}

                ////产地
                //clsProduceAreaOpr Opr15 = new clsProduceAreaOpr();
                //Opr15.Delete("IsReadOnly=true", out sDelErr);

                //dt = dsRt.Tables["ProduceArea"];
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string sErr = "";

                //    clsProduceArea ProduceArea = new clsProduceArea();
                //    ProduceArea.SysCode = dt.Rows[i]["SysCode"].ToString();
                //    ProduceArea.StdCode = dt.Rows[i]["StdCode"].ToString();
                //    ProduceArea.Name = dt.Rows[i]["Name"].ToString();
                //    ProduceArea.ShortCut = dt.Rows[i]["ShortCut"].ToString();
                //    ProduceArea.DistrictIndex = Convert.ToInt64(dt.Rows[i]["DistrictIndex"]);
                //    ProduceArea.CheckLevel = dt.Rows[i]["CheckLevel"].ToString();
                //    ProduceArea.IsLocal = Convert.ToBoolean(dt.Rows[i]["IsLocal"]);
                //    ProduceArea.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
                //    ProduceArea.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
                //    ProduceArea.Remark = dt.Rows[i]["Remark"].ToString();

                //    Opr15.Insert(ProduceArea, out sErr);
                //    if (!sErr.Equals(""))
                //    {
                //        sSumError += sErr + "\r\n";
                //        continue;
                //    }
                //}

                ////检测级别
                //clsCheckLevelOpr Opr8 = new clsCheckLevelOpr();
                //Opr8.Delete("IsReadOnly=true", out sDelErr);

                //dt = dsRt.Tables["CheckLevel"];
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string sErr = "";

                //    clsCheckLevel checkLevel = new clsCheckLevel();
                //    checkLevel.CheckLevel = dt.Rows[i]["CheckLevel"].ToString();
                //    checkLevel.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
                //    checkLevel.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
                //    checkLevel.Remark = dt.Rows[i]["Remark"].ToString();

                //    Opr8.Insert(checkLevel, out sErr);
                //    if (!sErr.Equals(""))
                //    {
                //        sSumError += sErr + "\r\n";
                //        continue;
                //    }
                //}

                ////信用级别
                //clsCreditLevelOpr Opr9 = new clsCreditLevelOpr();
                //Opr9.Delete("IsReadOnly=true", out sDelErr);

                //dt = dsRt.Tables["CreditLevel"];
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string sErr = "";

                //    clsCreditLevel creditLevel = new clsCreditLevel();
                //    creditLevel.CreditLevel = dt.Rows[i]["CreditLevel"].ToString();
                //    creditLevel.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
                //    creditLevel.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
                //    creditLevel.Remark = dt.Rows[i]["Remark"].ToString();

                //    Opr9.Insert(creditLevel, out sErr);
                //    if (!sErr.Equals(""))
                //    {
                //        sSumError += sErr + "\r\n";
                //        continue;
                //    }
                //}

                ////公司属性
                //clsCompanyPropertyOpr Opr11 = new clsCompanyPropertyOpr();
                //Opr11.Delete("IsReadOnly=true", out sDelErr);

                //dt = dsRt.Tables["CompanyProperty"];
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string sErr = "";

                //    clsCompanyProperty companyProperty = new clsCompanyProperty();
                //    companyProperty.CompanyProperty = dt.Rows[i]["CompanyProperty"].ToString();
                //    companyProperty.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
                //    companyProperty.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
                //    companyProperty.Remark = dt.Rows[i]["Remark"].ToString();

                //    Opr11.Insert(companyProperty, out sErr);
                //    if (!sErr.Equals(""))
                //    {
                //        sSumError += sErr + "\r\n";
                //        continue;
                //    }
                //}

                ////公司
                //clsCompanyOpr Opr12 = new clsCompanyOpr();

                //if (ShareOption.SystemVersion.Equals(ShareOption.LocalBaseVersion))
                //{
                //    Opr12.Delete("IsReadOnly=true", out sDelErr);
                //}
                //else
                //{
                //    Opr12.Delete("IsReadOnly=true And Property='" + ShareOption.CompanyProperty1 + "'", out sDelErr);
                //}
                //dt = dsRt.Tables["Company"];
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string sErr = "";

                //    clsCompany company = new clsCompany();
                //    company.SysCode = dt.Rows[i]["SysCode"].ToString();
                //    company.StdCode = dt.Rows[i]["StdCode"].ToString();
                //    company.CompanyID = dt.Rows[i]["CompanyID"].ToString();
                //    company.OtherCodeInfo = dt.Rows[i]["OtherCodeInfo"].ToString();
                //    company.FullName = dt.Rows[i]["FullName"].ToString();
                //    company.ShortName = dt.Rows[i]["ShortName"].ToString();
                //    company.DisplayName = dt.Rows[i]["DisplayName"].ToString();
                //    company.ShortCut = dt.Rows[i]["ShortCut"].ToString();
                //    company.Property = dt.Rows[i]["Property"].ToString();
                //    company.KindCode = dt.Rows[i]["KindCode"].ToString();
                //    company.RegCapital = Convert.ToInt64(dt.Rows[i]["RegCapital"]);
                //    company.Unit = dt.Rows[i]["Unit"].ToString();
                //    company.Incorporator = dt.Rows[i]["Incorporator"].ToString();
                //    company.RegDate = Convert.ToDateTime(dt.Rows[i]["RegDate"]);
                //    company.DistrictCode = dt.Rows[i]["DistrictCode"].ToString();
                //    company.PostCode = dt.Rows[i]["PostCode"].ToString();
                //    company.Address = dt.Rows[i]["Address"].ToString();
                //    company.LinkMan = dt.Rows[i]["LinkMan"].ToString();
                //    company.LinkInfo = dt.Rows[i]["LinkInfo"].ToString();
                //    company.CreditLevel = dt.Rows[i]["CreditLevel"].ToString();
                //    company.CreditRecord = dt.Rows[i]["CreditRecord"].ToString();
                //    company.ProductInfo = dt.Rows[i]["ProductInfo"].ToString();
                //    company.OtherInfo = dt.Rows[i]["OtherInfo"].ToString();
                //    company.FoodSafeRecord = dt.Rows[i]["FoodSafeRecord"].ToString();
                //    company.CheckLevel = dt.Rows[i]["CheckLevel"].ToString();
                //    company.IsReadOnly = Convert.ToBoolean(dt.Rows[i]["IsReadOnly"]);
                //    company.IsLock = Convert.ToBoolean(dt.Rows[i]["IsLock"]);
                //    company.Remark = dt.Rows[i]["Remark"].ToString();

                //    Opr12.Insert(company, out sErr);
                //    if (!sErr.Equals(""))
                //    {
                //        sSumError += sErr + "\r\n";
                //        continue;
                //    }
                //}
                //}
        //        #endregion
        //    }
        //    catch (Exception e)
        //    {
        //        sumError = "同步数据失败，失败原因：" + e.Message;
        //        return string.Empty;
        //    }
        //}


        /// <summary>
        /// 获取系统配置信息
        /// </summary>
        //public static void GetSystemInfo()
        //{
        //    clsSysOptOpr sysBll = new clsSysOptOpr();
        //    try
        //    {
        //        DataTable dtbl = sysBll.GetColumnDataTable(0, "LEN(SysCode)=6 ORDER BY SysCode", "OptValue");
        //        ShareOption.SysTemperature = Convert.ToDecimal(dtbl.Rows[0]["OptValue"]);//索引号为0
        //        ShareOption.SysHumidity = Convert.ToDecimal(dtbl.Rows[1]["OptValue"]);
        //        ShareOption.SysUnit = dtbl.Rows[2]["OptValue"].ToString();
        //        ShareOption.SysAutoLogin = Convert.ToBoolean(dtbl.Rows[3]["OptValue"]);
        //        ShareOption.SysExitPrompt = Convert.ToBoolean(dtbl.Rows[5]["OptValue"]);
        //        ShareOption.AllowHandInputCheckUint = Convert.ToBoolean(dtbl.Rows[7]["OptValue"]);
        //        ShareOption.SysStdCodeSame = Convert.ToBoolean(dtbl.Rows[8]["OptValue"]);
        //        ShareOption.FormatStrMachineCode = dtbl.Rows[9]["OptValue"].ToString();
        //        ShareOption.FormatStandardCode = dtbl.Rows[10]["OptValue"].ToString();
        //        ShareOption.SysTimer1 = Convert.ToDecimal(dtbl.Rows[15]["OptValue"]);
        //        ShareOption.SysTimer2 = Convert.ToDecimal(dtbl.Rows[16]["OptValue"]);
        //        ShareOption.SysTimer3 = Convert.ToDecimal(dtbl.Rows[17]["OptValue"]);
        //        ShareOption.SysTimer4 = Convert.ToDecimal(dtbl.Rows[18]["OptValue"]);
        //        ShareOption.SysTimerEndPlayWav = dtbl.Rows[19]["OptValue"].ToString();

        //        //设置系统版本
        //        ShareOption.SystemVersion = dtbl.Rows[20]["OptValue"].ToString();
        //        ShareOption.SystemTitle = dtbl.Rows[22]["OptValue"].ToString();
        //        //新增
        //        if (dtbl.Rows[28]["OptValue"] != null)//标识是否单机版
        //        {
        //            ShareOption.IsDataLink = Convert.ToBoolean(dtbl.Rows[28]["OptValue"].ToString());
        //        }

        //        if (!ShareOption.IsDataLink)//如果网络版
        //        {
        //            ShareOption.SysServerUrl = dtbl.Rows[12]["OptValue"].ToString();
        //            ShareOption.SysServerUser = dtbl.Rows[13]["OptValue"].ToString();
        //            ShareOption.SysServerPwd = dtbl.Rows[14]["OptValue"].ToString();
        //            ShareOption.InterfaceType = dtbl.Rows[29]["OptValue"].ToString();
        //        }
        //        //2011-11-18新增
        //        ShareOption.ApplicationTag = dtbl.Rows[30]["OptValue"].ToString();//应用类型版本

        //        //新增字段
        //        ShareOption.CurDY3000Tag = dtbl.Rows[31]["OptValue"].ToString();//DY3000版本名称
        //        ShareOption.DY5000Name = dtbl.Rows[32]["OptValue"].ToString();//DY5000版本名称
        //        ShareOption.ProductionUnitNameTag = dtbl.Rows[33]["OptValue"].ToString();//生产单位标签

        //        //2016年12月12日 新增获取检测点编号
        //        DataTable dtUserUnit = new clsUserUnitOpr().GetAsDataTable(string.Format("A.SysCode='{0}'", ShareOption.DefaultUserUnitCode), "", 0);
        //        ShareOption.companyCode = dtUserUnit.Rows[0]["STDCODE"].ToString();
        //    }
        //    catch
        //    {
        //        MessageBox.Show("系统初始化设置错误", "严重错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}


        /// <summary>
        /// 更新App.config网络连接字符
        /// </summary>
        /// <param name="serverURL">配置输入的联网URL</param>
        //public static void writeWebServer(string serverURL)
        //{
        //    //检查程序名字.config文件是否存在
        //    string keyName = string.Empty;
        //    if (ShareOption.InterfaceType.Equals(ShareOption.InterfaceJ2EE))
        //    {
        //        keyName = "FoodClient.localhost.DataSyncService";
        //    }
        //    else
        //    {
        //        keyName = "FoodClient.ForNet.DataDriver";
        //    }
        //    //System.Configuration.ConfigurationManager.AppSettings["InterfaceNameSpace"] = keyName;
        //    //System.Configuration.ConfigurationManager.AppSettings[keyName] = serverURL;

        //    #region
            ////////////////写如下大堆代码其实就是为了更新APP.config一个字段
            //string strFileNameFullPath=Application.ExecutablePath;
            //string strFileNamePath=Application.StartupPath;
            //if (strFileNamePath.Substring(strFileNamePath.Length-1,1)!="\\")
            //{
            //    strFileNamePath=strFileNamePath+"\\";
            //}
            //string strFileName=strFileNameFullPath.Substring(strFileNamePath.Length,strFileNameFullPath.Length-strFileNamePath.Length) + ".Config";
            //if (System.IO.File.Exists(strFileNamePath + strFileName))
            //{
            //    System.Xml.XmlDocument xmlDoc = new XmlDocument();
            //    xmlDoc.Load(strFileNamePath + strFileName);
            //    XmlElement xmlRootElement;
            //    xmlRootElement = xmlDoc.DocumentElement["appSettings"];
            //    if (xmlRootElement.ChildNodes[0].Name == "add")
            //    {
            //        if (ShareOption.InterfaceType.Equals(ShareOption.InterfaceJ2EE))
            //        {
            //            xmlRootElement.ChildNodes[0].Attributes["key"].Value = "FoodClient.localhost.DataSyncService";
            //        }
            //        else if (ShareOption.InterfaceType.Equals(ShareOption.InterfaceDotNET))
            //        {
            //            xmlRootElement.ChildNodes[0].Attributes["key"].Value = "FoodClient.ForNet.DataDriver";
            //        }
            //        xmlRootElement.ChildNodes[0].Attributes["value"].Value = strWebServer;
            //    }
            //    xmlDoc.Save(strFileNamePath + strFileName);
            //}
        //    #endregion
        //}

        /// <summary>
        /// 服务器测试连接
        /// </summary>
        /// <param name="serverIp">连网服务器地址</param>
        /// <param name="user">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns>返回true表示成功，返回false表示失败</returns>
        //public static bool CheckConnection(string serverIp, string user, string pwd)
        //{
        //    if (ShareOption.InterfaceType.Equals(ShareOption.InterfaceJ2EE))
        //    {
        //        FoodClient.localhost.DataSyncService ws = new FoodClient.localhost.DataSyncService();
        //        //DY.WebService.ForJ2EE.DataSyncService ws = new DY.WebService.ForJ2EE.DataSyncService();
        //        ws.Url = serverIp;

        //        string blrtn = ws.CheckConnection(user, FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5").ToString());
        //        if (blrtn.Equals("true"))
        //        {
        //            return true;
        //            //Cursor = Cursors.Default;
        //            //MessageBox.Show(this, "服务器连接正常！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //        else
        //        {
        //            return false;
        //            //Cursor = Cursors.Default;
        //            //MessageBox.Show(this, "服务器无法连接，请重新设置！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //    }
        //    else //if (ShareOption.InterfaceType.Equals(ShareOption.InterfaceDotNET))
        //    {
        //        FoodClient.ForNet.DataDriver ws = new FoodClient.ForNet.DataDriver();
        //        //DY.WebService.ForNet.DataDriver ws = new WebService.ForNet.DataDriver();
        //        ws.Url = serverIp;
        //        string sErr = string.Empty;
        //        bool blrtn = ws.CheckConnection(user, pwd, out sErr);
        //        if (blrtn)
        //        {
        //            return true;
        //            //Cursor = Cursors.Default;
        //            //MessageBox.Show(this, "服务器连接正常！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //        else
        //        {
        //            return false;
        //            //Cursor = Cursors.Default;
        //            //MessageBox.Show(this, "服务器无法连接，请重新设置！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //    }
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DY.FoodClientLib
{
    /// <summary>
    /// 生产单位或者被检单位
    /// </summary>
    public class clsCompanyOpr
    {
        public clsCompanyOpr()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        private StringBuilder sb = new StringBuilder();
        /// <summary>
        /// 部分修改保存
        /// </summary>
        /// <param name="model">对象clsCompany的一个实例参数</param>
        /// <returns></returns>
        public int UpdatePart(clsCompany model, string sOldCode, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                sb.Length = 0;
                sb.Append("UPDATE TCOMPANY SET ");
                sb.AppendFormat("StdCode='{0}',", model.StdCode);
                sb.AppendFormat("CAllow='{0}',", model.CAllow);

                sb.AppendFormat("ISSUEAGENCY='{0}',", model.ISSUEAGENCY);
                sb.AppendFormat("ISSUEDATE='{0}',", model.ISSUEDATE);
                sb.AppendFormat("PERIODSTART='{0}',", model.PERIODSTART);
                sb.AppendFormat("PERIODEND='{0}',", model.PERIODEND);
                sb.AppendFormat("VIOLATENUM='{0}',", model.VIOLATENUM);
                sb.AppendFormat("LONGITUDE='{0}',", model.LONGITUDE);
                sb.AppendFormat("LATITUDE='{0}',", model.LATITUDE);
                sb.AppendFormat("SCOPE='{0}',", model.SCOPE);
                sb.AppendFormat("PUNISH='{0}',", model.PUNISH);

                sb.AppendFormat("CompanyID='{0}',", model.CompanyID);
                sb.AppendFormat("OtherCodeInfo='{0}',", model.OtherCodeInfo);
                sb.AppendFormat("FullName='{0}',", model.FullName);
                sb.AppendFormat("ShortName='{0}',", model.ShortName);
                sb.AppendFormat("DisplayName='{0}',", model.DisplayName);
                sb.AppendFormat("ShortCut='{0}',", model.ShortCut);
                sb.AppendFormat("Property='{0}',", model.Property);
                sb.AppendFormat("KindCode='{0}',", model.KindCode);
                sb.AppendFormat("RegCapital={0},", model.RegCapital);
                sb.AppendFormat("Unit='{0}',", model.Unit);
                sb.AppendFormat("Incorporator='{0}',", model.Incorporator);
                if (model.RegDate != null)
                {
                    sb.AppendFormat("RegDate='{0}',", model.RegDate);
                }
                if (model.RegDate == null)
                {
                    sb.AppendFormat("RegDate=null,", "");
                }
                sb.AppendFormat("DistrictCode='{0}',", model.DistrictCode);
                sb.AppendFormat("PostCode='{0}',", model.PostCode);
                sb.AppendFormat("Address='{0}',", model.Address);
                sb.AppendFormat("LinkMan='{0}',", model.LinkMan);
                sb.AppendFormat("LinkInfo='{0}',", model.LinkInfo);
                sb.AppendFormat("CreditLevel='{0}',", model.CreditLevel);
                sb.AppendFormat("CreditRecord='{0}',", model.CreditRecord);
                sb.AppendFormat("ProductInfo='{0}',", model.ProductInfo);
                sb.AppendFormat("OtherInfo='{0}',", model.OtherInfo);
                sb.AppendFormat("CheckLevel='{0}',", model.CheckLevel);
                sb.AppendFormat("FoodSafeRecord='{0}',", model.FoodSafeRecord);
                sb.AppendFormat("IsReadOnly={0},", model.IsReadOnly);
                sb.AppendFormat("IsLock={0},", model.IsLock);
                sb.AppendFormat("Remark='{0}',", model.Remark);
                sb.AppendFormat("ComProperty='{0}'", model.ComProperty);
                //sb.AppendFormat("TSign='{0}'", model.TSign);
                sb.AppendFormat("  WHERE SysCode='{0}'", sOldCode);
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;
                rtn = 1;
            }
            catch (Exception e)
            {
                //Log.WriteLog("更新clsCompany",e);
                errMsg = e.Message;
            }

            return rtn;
        }

        /// <summary>
        /// 删除 
        /// </summary>
        /// <param name="whereSql"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int Delete(string whereSql, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                sb.Length = 0;
                sb.Append("DELETE FROM TCOMPANY");

                if (!whereSql.Equals(""))
                {
                    sb.Append(" WHERE ");
                    sb.Append(whereSql);
                }
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }

            return rtn;
        }

        /// <summary>
        /// 根据主键编号删除记录
        /// </summary>
        /// <param name="mainkey">主键编号</param>
        /// <returns></returns>
        public int DeleteByPrimaryKey(string mainkey, out string errMsg)
        {
            errMsg = string.Empty;
            if (string.IsNullOrEmpty(mainkey))
            {
                return 0;
            }
            int rtn = 0;
            try
            {
                int len = mainkey.Length;
                string deleteSql = string.Empty;
                deleteSql = string.Format("DELETE FROM TCOMPANY WHERE MID(SysCode,1,{1})='{0}'", mainkey, len);//STDCODE

                bool flag = DataBase.ExecuteCommand(deleteSql, out errMsg);

                //deleteSql = "delete from tResult"
                //    + " where CheckedCompany='" + mainkey + "'"
                //    + " or ProduceCompany='" + mainkey + "'";
                //DataBase.ExecuteCommand(deleteSql, out sErrMsg);
                if (flag)
                {
                    rtn = 1;
                }
            }
            catch (Exception e)
            {
                //Log.WriteLog("通过主键删除clsCompany",e);
                errMsg = e.Message;
            }

            return rtn;
        }

        /// <summary>
        /// 安徽项目
        /// </summary>
        /// <param name="whereSql"></param>
        /// <param name="orderBySql"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable GetAsDataTable_ah(string whereSql, string orderBySql, int type,string tableName)
        {
            string errMsg = string.Empty;
            DataTable dt = null;
            sb.Length = 0;
            try
            {
                if (type == 1)//样品种类
                {
                    sb.Append("SELECT name,codeId,typeNum,pid,remark,status,ID FROM ah_data_dictionary");
                }
                else if (type == 2)//检测项目
                {
                    sb.Append("SELECT name AS ItemDes,codeId AS SysCode FROM ah_data_dictionary");
                }
                if (!whereSql.Equals(""))
                {
                    sb.Append(" WHERE ");
                    sb.Append(whereSql);
                }
                if (!orderBySql.Equals(""))
                {
                    sb.Append(" ORDER BY ");
                    sb.Append(orderBySql);
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { tableName };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables[tableName];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                //Log.WriteLog("查询clsCompany",e);
                errMsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 根据查询串条件查询记录
        /// </summary>
        /// <param name="whereSql">查询条件串,不含Where</param>
        /// <param name="orderBySql">排序串,不含Order</param>
        /// <returns></returns>
        public DataTable GetAsDataTable(string whereSql, string orderBySql, int type)
        {
            string errMsg = string.Empty;
            DataTable dt = null;
            sb.Length = 0;
            try
            {
                if (type == 0)
                {
                    sb.Append("SELECT A.SysCode,A.CompanyID,A.FullName,A.StdCode,A.CAllow,A.ISSUEAGENCY,A.ISSUEDATE,A.PERIODSTART,A.PERIODEND,A.VIOLATENUM,A.LONGITUDE,A.LATITUDE,A.SCOPE,A.PUNISH,A.OtherCodeInfo,");
                    sb.Append("A.ShortName,A.DisplayName,A.ShortCut,A.Property,A.KindCode,A.KindName,A.RegCapital,A.Unit,A.Incorporator,");
                    sb.Append("A.RegDate,A.DistrictCode,C.Name As DistrictName,A.PostCode,A.Address,A.LinkMan,A.LinkInfo,");
                    sb.Append("A.CreditLevel,A.CreditRecord,A.ProductInfo,A.OtherInfo,A.CheckLevel,A.FoodSafeRecord,");
                    sb.Append("A.IsReadOnly,A.IsLock,A.ComProperty,A.Remark,A.TSign");
                    sb.Append(" FROM [SELECT D.SysCode,D.StdCode,D.CompanyID,d.CAllow,d.ISSUEAGENCY,d.ISSUEDATE,d.PERIODSTART,d.PERIODEND,d.VIOLATENUM,d.LONGITUDE,d.LATITUDE,d.SCOPE,d.PUNISH,d.OtherCodeInfo,d.FullName,d.ShortName,d.DisplayName,d.ShortCut,d.Property,d.KindCode,B.Name As KindName,d.RegCapital,d.Unit,d.Incorporator,d.RegDate,d.DistrictCode,d.PostCode,d.Address,d.LinkMan,d.LinkInfo,d.CreditLevel,d.CreditRecord,d.ProductInfo,d.OtherInfo,d.CheckLevel,d.FoodSafeRecord,d.IsReadOnly,d.IsLock,d.Remark,d.TSign,D.ComProperty FROM tCompany As d Left Join tCompanyKind As B  On d.KindCode=B.SysCode]. AS A LEFT JOIN tDistrict AS C ON A.DistrictCode=C.SysCode");
                }
                else if (type == 1)
                {
                    sb.Append(" select fullname,syscode from tCompany  ");
                    //sb.Append(" select fullname,syscode from tCompany  where TSign<>'本地增' union  Select Cdname,Ciid from tProprietors ");
                }
                else if (type == 2)
                {
                    sb.Append("SELECT c.fullname,t.[name] AS CompanyType,d.[name] AS OrganizationName,c.SysCode,c.Incorporator ");
                    sb.Append("FROM (tcompany AS c INNER JOIN tcompanyKind AS t on c.kindcode=t.syscode) ");
                    sb.Append("INNER JOIN tdistrict AS d on c.districtcode = d.syscode");
                }
                else if (type == 3)
                {
                    sb.Append("SELECT FULLNAME,STDCODE,SYSCODE,KINDCODE,DISPLAYNAME,Property,IsReadOnly,DistrictCode FROM tCompany");
                }
                else if (type == 4)//样品种类
                {
                    sb.Append("SELECT name,codeId,typeNum,pid,remark,status,ID FROM ah_data_dictionary");
                }
                else if (type == 5)//检测项目
                {
                    sb.Append("SELECT name AS ItemDes,codeId AS SysCode FROM ah_data_dictionary");
                }
                if (!whereSql.Equals(""))
                {
                    sb.Append(" WHERE ");
                    sb.Append(whereSql);
                }
                if (!orderBySql.Equals(""))
                {
                    sb.Append(" ORDER BY ");
                    sb.Append(orderBySql);
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "Company" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Company"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                //Log.WriteLog("查询clsCompany",e);
                errMsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 插入一条明细记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(clsCompany model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                sb.Length = 0;
                sb.Append("INSERT INTO tCompany");
                sb.Append("(SysCode,StdCode,CAllow,ISSUEAGENCY,ISSUEDATE,PERIODSTART,PERIODEND,VIOLATENUM,LONGITUDE,LATITUDE,SCOPE,PUNISH,CompanyID,OtherCodeInfo,FullName,");
                sb.Append("ShortName,DisplayName,ShortCut,Property,KindCode,RegCapital,Unit,Incorporator,");
                if (model.RegDate != null)
                {
                    sb.Append("RegDate,");
                }
                sb.Append("DistrictCode,PostCode,Address,LinkMan,LinkInfo,");
                sb.Append("CreditLevel,CreditRecord,ProductInfo,OtherInfo,CheckLevel,FoodSafeRecord,IsReadOnly,IsLock,Remark,ComProperty,TSign)");
                sb.Append(" VALUES(");
                sb.AppendFormat("'{0}',", model.SysCode);
                //sb.AppendFormat("'{0}',", model.Ciid);
                sb.AppendFormat("'{0}',", model.StdCode);
                sb.AppendFormat("'{0}',", model.CAllow);

                sb.AppendFormat("'{0}',", model.ISSUEAGENCY);
                sb.AppendFormat("'{0}',", model.ISSUEDATE);
                sb.AppendFormat("'{0}',", model.PERIODSTART);
                sb.AppendFormat("'{0}',", model.PERIODEND);
                sb.AppendFormat("'{0}',", model.VIOLATENUM);
                sb.AppendFormat("'{0}',", model.LONGITUDE);
                sb.AppendFormat("'{0}',", model.LATITUDE);
                sb.AppendFormat("'{0}',", model.SCOPE);
                sb.AppendFormat("'{0}',", model.PUNISH);

                sb.AppendFormat("'{0}',", model.CompanyID);
                sb.AppendFormat("'{0}',", model.OtherCodeInfo);
                sb.AppendFormat("'{0}',", model.FullName);
                sb.AppendFormat("'{0}',", model.ShortName);
                sb.AppendFormat("'{0}',", model.DisplayName);
                sb.AppendFormat("'{0}',", model.ShortCut);
                sb.AppendFormat("'{0}',", model.Property);
                sb.AppendFormat("'{0}',", model.KindCode);
                sb.AppendFormat("{0},", model.RegCapital);
                sb.AppendFormat("'{0}',", model.Unit);
                sb.AppendFormat("'{0}',", model.Incorporator);
                if (model.RegDate != null)
                {
                    sb.AppendFormat("'{0}',", model.RegDate);
                }
                sb.AppendFormat("'{0}',", model.DistrictCode);
                sb.AppendFormat("'{0}',", model.PostCode);
                sb.AppendFormat("'{0}',", model.Address);
                sb.AppendFormat("'{0}',", model.LinkMan);
                sb.AppendFormat("'{0}',", model.LinkInfo);
                sb.AppendFormat("'{0}',", model.CreditLevel);
                sb.AppendFormat("'{0}',", model.CreditRecord);
                sb.AppendFormat("'{0}',", model.ProductInfo);
                sb.AppendFormat("'{0}',", model.OtherInfo);
                sb.AppendFormat("'{0}',", model.CheckLevel);
                sb.AppendFormat("'{0}',", model.FoodSafeRecord);
                sb.AppendFormat("{0},", model.IsReadOnly);
                sb.AppendFormat("{0},", model.IsLock);
                sb.AppendFormat("'{0}',", model.Remark);
                sb.AppendFormat("'{0}',", model.ComProperty);
                sb.AppendFormat("'{0}'", model.TSign);
                sb.Append(")");
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;

                rtn = 1;
            }
            catch (Exception e)
            {
                //Log.WriteLog("添加clsCompany",e);
                errMsg = e.Message;
            }

            return rtn;
        }

        public int GetMaxNO(string prevCode, int lev, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn;

            try
            {
                sb.Length = 0;
                sb.Append("SELECT SYSCODE FROM TCOMPANY WHERE SYSCODE LIKE ");
                sb.Append("'");
                sb.Append(prevCode);
                sb.Append(StringUtil.RepeatChar('_', lev));
                sb.Append("'");
                sb.Append(" ORDER BY SYSCODE DESC");
                Object o = DataBase.GetOneValue(sb.ToString(), out errMsg);
                sb.Length = 0;
                rtn = o == null ? 0 : 
                    Convert.ToInt32(o.ToString().Substring(o.ToString().Length - lev, lev));
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                rtn = -1;
            }

            return rtn;
        }

        public int GetStdCodeMaxNO(string prevCode, int lev, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn;

            try
            {
                sb.Length = 0;
                sb.Append("SELECT Stdcode FROM TCOMPANY WHERE SYSCODE LIKE ");
                sb.Append("'");
                sb.Append(prevCode);
                sb.Append(StringUtil.RepeatChar('_', lev));
                sb.Append("'");
                sb.Append(" ORDER BY Stdcode DESC");
                Object o = DataBase.GetOneValue(sb.ToString(), out errMsg);
                sb.Length = 0;
                //string sql="select Stdcode from tCompany where Stdcode like '" + prevcode 
                //    + StringUtil.RepeatChar('_',lev) + "' order by Stdcode desc";
                //Object o=DataBase.GetOneValue(sql,out errMsg);
                rtn = o == null ? 0 :
                    Convert.ToInt32(o.ToString().Substring(o.ToString().Length - lev, lev));
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                rtn = -1;
            }

            return rtn;
        }
        /// <summary>
        /// 查询被检单位地址
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static string GetCheckUnitAddress(string unit)
        {
            string errMsg = string.Empty;
            if (unit.Equals(""))
            {
                return "";
            }

            try
            {
                string sql = string.Format("Select Address from tCompany where FullName='{0}'", unit);

                Object o = DataBase.GetOneValue(sql, out errMsg);
                if (o == null)
                {
                    return "";
                }
                else
                {
                    return o.ToString();
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
        }
        /// <summary>
        /// 根据被检单位名称获取地址,营业执照,经营性质,联系人,电话,邮编
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<string> GetCompanyByName(string name)
        {
            List<string> strList = new List<string>();
            string sErr = string.Empty;
            if (name.Equals(""))
            {
                for (int i = 0; i < 6; i++)
                {
                    strList.Add("");
                }
                return strList;
            }

            try
            {
                //获取地址
                string sql = string.Format("SELECT Address FROM TCOMPANY WHERE FullName = '{0}'", name);
                Object obj = DataBase.GetOneValue(sql, out sErr);
                strList.Add((obj == null) ? "" : obj.ToString());

                //获取营业执照
                sql = string.Empty; obj = null;
                sql = string.Format("SELECT StdCode FROM TCOMPANY WHERE FullName = '{0}'", name);
                obj = DataBase.GetOneValue(sql, out sErr);
                strList.Add((obj == null) ? "" : obj.ToString());

                //经营性质
                sql = string.Empty; obj = null;
                sql = string.Format("SELECT ComProperty FROM TCOMPANY WHERE FullName = '{0}'", name);
                obj = DataBase.GetOneValue(sql, out sErr);
                strList.Add((obj == null) ? "" : obj.ToString());

                //获取联系人
                sql = string.Empty; obj = null;
                sql = string.Format("SELECT LinkMan FROM TCOMPANY WHERE FullName = '{0}'", name);
                obj = DataBase.GetOneValue(sql, out sErr);
                strList.Add((obj == null) ? "" : obj.ToString());

                //获取联系人电话
                sql = string.Empty; obj = null;
                sql = string.Format("SELECT LinkInfo FROM TCOMPANY WHERE FullName = '{0}'", name);
                obj = DataBase.GetOneValue(sql, out sErr);
                strList.Add((obj == null) ? "" : obj.ToString());

                //邮编
                sql = string.Empty; obj = null;
                sql = string.Format("SELECT PostCode FROM TCOMPANY WHERE FullName = '{0}'", name);
                obj = DataBase.GetOneValue(sql, out sErr);
                strList.Add((obj == null) ? "" : obj.ToString());
            }
            catch (Exception ex)
            {
                sErr = ex.Message;
                return null;
            }
            return strList;
        }

        /// <summary>
        /// 通过ID获取名称
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string NameFromCode(string code)
        {
            string errMsg = string.Empty;
            if (code.Equals(""))
            {
                return "";
            }
            try
            {
                string sql = string.Format("SELECT FULLNAME FROM TCOMPANY WHERE SYSCODE='{0}' ORDER BY SYSCODE", code);
                Object o = DataBase.GetOneValue(sql, out errMsg);
                return o == null ? "" : o.ToString();
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
        }
        /// <summary>
        /// 通过fullname获取门牌号
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string CompanyInfo(string code)
        {
            string errMsg = string.Empty;
            if (code.Equals(""))
            {
                return "";
            }
            try
            {
                string sql = string.Format("select displayname from ( Select fullname,syscode,displayname from tCompany union select Cdname,Ciid,DisplayName from tProprietors ) where fullname='{0}' ", code);
                Object o = DataBase.GetOneValue(sql, out errMsg);
                return o == null ? "" : o.ToString();
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
        }
        /// <summary>
        /// 通过fullname获取门牌号
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string TxtCiidInfo(string code)
        {
            string errMsg = string.Empty;
            if (code.Equals(""))
            {
                return "";
            }
            try
            {
                string sql = string.Format("select displayname from ( Select fullname,syscode,displayname from tCompany union select Cdname,Ciid,DisplayName from tProprietors where fullname='{0}') ", code);
                Object o = DataBase.GetOneValue(sql, out errMsg);
                return o == null ? "" : o.ToString();
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
        }
        /// <summary>
        /// 通过fullname获取名称
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string Namefullname(string code)
        {
            string errMsg = string.Empty;
            if (code.Equals(""))
            {
                return "";
            }

            try
            {
                //string sql = string.Format("SELECT FULLNAME FROM TCOMPANY WHERE SYSCODE='{0}' ORDER BY SYSCODE", code);
                string sql = string.Format("select fullname from (select fullname,syscode from tCompany union  Select Cdname,Ciid from tProprietors ) as T WHERE  fullname='{0}' ", code);
                Object o = DataBase.GetOneValue(sql, out errMsg);
                return o == null ? "" : o.ToString();
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
        }

        /// <summary>
        /// 获取DisplayName
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string DisplayNameFromCode(string code)
        {
            string errMsg = string.Empty;
            if (code.Equals(""))
            {
                return "";
            }

            try
            {
                string sql = string.Format("SELECT DISPLAYNAME FROM TCOMPANY WHERE SYSCODE='{0}' ORDER BY SYSCODE", code);

                Object o = DataBase.GetOneValue(sql, out errMsg);
                return o == null ? "" : o.ToString();
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
        }
        /// <summary>
        /// 标准编码获取名称
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string NameFromStdCode(string code)
        {
            string sErrMsg = string.Empty;
            if (code.Equals(""))
            {
                return "";
            }

            try
            {
                string sql = string.Format("SELECT FULLNAME FROM TCOMPANY WHERE STDCODE='{0}' ORDER BY STDCODE", code);
                Object o = DataBase.GetOneValue(sql, out sErrMsg);
                return o == null ? "" : o.ToString();
            }
            catch (Exception e)
            {
                sErrMsg = e.Message;
                return null;
            }
        }

        public static string GetCompanyName(string name)
        {
            string errMsg = string.Empty;
            if (name.Equals(""))
            {
                return "";
            }

            try
            {
                string sql = string.Format("SELECT FULLNAME FROM TCOMPANY WHERE FULLNAME='{0}' ORDER BY SYSCODE", name);
                Object o = DataBase.GetOneValue(sql, out errMsg);
                return o == null ? "" : o.ToString();
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
        }

        public static string GetCompanyCode(string name)
        {
            string errMsg = string.Empty;
            if (name.Equals(""))
            {
                return "";
            }

            try
            {
                string sql = string.Format("SELECT SysCode FROM TCOMPANY WHERE FULLNAME='{0}' ORDER BY SYSCODE", name);
                Object o = DataBase.GetOneValue(sql, out errMsg);
                return o == null ? "" : o.ToString();
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
        }

        /// <summary>
        /// 通过标准编码获取编码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string CodeFromStdCode(string code)
        {
            string errMsg = string.Empty;
            if (code.Equals(""))
            {
                return "";
            }

            try
            {
                string sql = string.Format("SELECT SYSCODE FROM TCOMPANY WHERE STDCODE='{0}' ORDER BY STDCODE", code);
                Object o = DataBase.GetOneValue(sql, out errMsg);
                return o == null ? "" : o.ToString();
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
        }

        public static string KindCodeFromStdCode(string Stdcode)
        {
            string errMsg = string.Empty;
            if (Stdcode.Equals(""))
            {
                return "";
            }

            try
            {
                string sql = string.Format("SELECT KINDCODE FROM TCOMPANY WHERE STDCODE='{0}' ORDER BY STDCODE", Stdcode);
                Object o = DataBase.GetOneValue(sql, out errMsg);
                return o == null ? "" : o.ToString();
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
        }

        public static string AreaCodeFromStdCode(string stdCode)
        {
            string errMsg = string.Empty;
            if (stdCode.Equals(""))
            {
                return "";
            }

            try
            {
                string sql = string.Format("SELECT DISTRICTCODE FROM TCOMPANY WHERE STDCODE='{0}' ORDER BY STDCODE", stdCode);

                Object o = DataBase.GetOneValue(sql, out errMsg);
                return o == null ? "" : o.ToString();
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
        }

        public static bool CompanyIsExist(string swhere)
        {
            string errMsg = string.Empty;
            if (swhere.Equals(""))
            {
                return false;
            }

            try
            {
                string sql = string.Format("SELECT SYSCODE FROM TCOMPANY WHERE {0}", swhere);
                Object o = DataBase.GetOneValue(sql, out errMsg);
                return o == null ? false : true;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return false;
            }
        }


        public static string InfoFromCode(string code)
        {
            string errMsg = string.Empty;
            if (code.Equals(""))
            {
                return "";
            }

            try
            {
                string sql = string.Format("SELECT TCOMPANYKIND.NAME FROM TCOMPANY,TCOMPANYKIND WHERE tCompany.syscode='{0}' AND TCOMPANY.KINDCODE=TCOMPANYKIND.SYSCODE ORDER BY TCOMPANY.SYSCODE", code);

                object o = DataBase.GetOneValue(sql, out errMsg);
                return o == null ? "" : o.ToString();
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
        }

        public static string StdCodeFromCode(string code)
        {
            string errMsg = string.Empty;
            if (code.Equals(""))
            {
                return "";
            }

            try
            {
                string sql = string.Format("SELECT STDCODE FROM TCOMPANY WHERE SYSCODE='{0}' ORDER BY SYSCODE", code);
                Object o = DataBase.GetOneValue(sql, out errMsg);
                return o == null ? "" : o.ToString();
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
        }

        public DataTable GetAllCompanies()
        {
            //string districtCode = clsUserUnitOpr.GetNameFromCode("DistrictCode", "0001");
            //string sqlWhere = string.Format("Property='被检单位' And IsReadOnly=true And Len(StdCode)=6 AND DistrictCode LIKE '{0}%'", districtCode);
            string sqlWhere = string.Format("Property='被检单位' And IsReadOnly=true");
            DataTable companies = GetAsDataTable(sqlWhere, "DistrictCode ASC,KindCode ASC", 1);
            //string sqlWhere = string.Empty;
            //DataTable companies = GetAsDataTable(sqlWhere, "", 1);
            return companies;
        }

        /// <summary>
        /// 获取所有样品种类
        /// </summary>
        /// <returns></returns>
        ////public DataTable GetAllFoodType(string where,int type)
        ////{
        ////    return GetAsDataTable(where, "", type);
        ////}

        /// <summary>
        ///  安徽项目
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllFoodType(string where, int type, string tableName)
        {
            return GetAsDataTable_ah(where, "", type, tableName);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DYSeriesDataSet
{
    public class clsProprietorsOpr
    {
        public clsProprietorsOpr()
        { 
        
        }

        private StringBuilder sb = new StringBuilder();
        /// <summary>
        /// 部分修改保存
        /// </summary>
        /// <param name="model">对象clsProprietors的一个实例参数</param>
        /// <returns></returns>
        public int UpdatePart(clsProprietors  model, string sOldCode, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                sb.Length = 0;
                sb.Append("UPDATE tProprietors SET ");
                sb.AppendFormat("Cdcode='{0}',", model.Cdcode);
                sb.AppendFormat("Cdbuslicence='{0}',", model.Cdbuslicence);
                sb.AppendFormat("CAllow='{0}',", model.CAllow);
                sb.AppendFormat("Ciid='{0}',", model.Ciid);
                sb.AppendFormat("Ciname='{0}',", model.Ciname);
                sb.AppendFormat("Cdname='{0}',", model.Cdname);
                sb.AppendFormat("Cdcardid='{0}',", model.Cdcardid);
                sb.AppendFormat("DisplayName='{0}',", model.DisplayName);
                sb.AppendFormat("Property='{0}',", model.Property);
                sb.AppendFormat("KindCode='{0}',", model.KindCode);
                sb.AppendFormat("RegCapital='{0}',", model.RegCapital);
                sb.AppendFormat("Unit='{0}',", model.Unit);
                sb.AppendFormat("Incorporator='{0}',", model.Incorporator);
                sb.AppendFormat("RegDate='{0}',", model.RegDate);
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
                sb.AppendFormat("IsLock='{0}',", model.IsLock);
                sb.AppendFormat("IsReadOnly='{0}',", model.IsReadOnly);
                sb.AppendFormat("Remark='{0}',", model.Remark);




                sb.AppendFormat("  WHERE Cdcode='{0}'", sOldCode);

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
                sb.Append("DELETE FROM tProprietors");

                if (!whereSql.Equals(string.Empty))
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
                deleteSql = string.Format("DELETE FROM tProprietors WHERE MID(Cdcode,1,{1})='{0}'", mainkey, len);//STDCODE

                bool flag = DataBase.ExecuteCommand(deleteSql, out errMsg);
                if (flag)
                {
                    rtn = 1;
                }
            }
            catch (Exception e)
            {
                //Log.WriteLog("通过主键删除clsProprietors",e);
                errMsg = e.Message; ;
            }

            return rtn;
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
                    sb.Append("SELECT Cdcode,Cdbuslicence,CAllow,Ciid,Ciname,Cdname,Cdcardid,DisplayName,RegCapital,Unit,Incorporator,RegDate,PostCode,Address,LinkMan,LinkInfo,CreditLevel,CreditRecord,ProductInfo,OtherInfo,CheckLevel,FoodSafeRecord,IsReadOnly,Remark FROM tProprietors  ");
                }
                else if (type == 1)
                {
                    sb.Append("Select Ciname,Cdname,Incorporator,DisplayName from tProprietors ");
                }
                else if (type == 2)
                {
                    sb.Append("SELECT c.fullname,t.[name] AS CompanyType,d.[name] AS OrganizationName,c.SysCode,c.Incorporator ");
                    sb.Append("FROM (tcompany AS c INNER JOIN tcompanyKind AS t on c.kindcode=t.syscode) ");
                    sb.Append("INNER JOIN tdistrict AS d on c.districtcode = d.syscode");
                }
                if (!whereSql.Equals(string.Empty))
                {
                    sb.Append(" WHERE ");
                    sb.Append(whereSql);
                }
                if (!orderBySql.Equals(string.Empty))
                {
                    sb.Append(" ORDER BY ");
                    sb.Append(orderBySql);
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "Proprietors" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Proprietors"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                //Log.WriteLog("查询clsProprietors",e);
                errMsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 插入一条明细记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(clsProprietors model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                sb.Length = 0;
                sb.Append("INSERT INTO tProprietors");
                sb.Append(" (Cdcode,Cdbuslicence,CAllow,Ciid,Ciname,Cdname,Cdcardid,DisplayName,Property,KindCode,RegCapital,Unit,Incorporator,");
                if (model.RegDate != null)
                {
                    sb.Append("RegDate,");
                }
                 sb.Append("DistrictCode,PostCode,Address,LinkMan,LinkInfo,CreditLevel,CreditRecord,ProductInfo,OtherInfo,CheckLevel,");
                sb.Append("FoodSafeRecord,IsLock,IsReadOnly,Remark) ");
                sb.Append(" VALUES(");
                sb.AppendFormat("'{0}',", model.Cdcode);
                sb.AppendFormat("'{0}',", model.Cdbuslicence);
                sb.AppendFormat("'{0}',", model.CAllow );
                sb.AppendFormat("'{0}',", model.Ciid);
                sb.AppendFormat("'{0}',", model.Ciname);
                sb.AppendFormat("'{0}',", model.Cdname);
                sb.AppendFormat("'{0}',", model.Cdcardid);
                sb.AppendFormat("'{0}',", model.DisplayName);
                sb.AppendFormat("'{0}',", model.Property);
                sb.AppendFormat("'{0}',", model.KindCode);
                sb.AppendFormat("'{0}',", model.RegCapital);
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
                sb.AppendFormat("{0},", model.IsLock);
                sb.AppendFormat("{0},", model.IsReadOnly);
                sb.AppendFormat("'{0}'", model.Remark);

                sb.Append(")");
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
                //sb.Append(StringUtil.RepeatChar('_', lev));
                sb.Append("'");
                sb.Append(" ORDER BY SYSCODE DESC");
                Object o = DataBase.GetOneValue(sb.ToString(), out errMsg);
                sb.Length = 0;
                if (o == null)
                {
                    rtn = 0;
                }
                else
                {
                    string rightStr = o.ToString().Substring(o.ToString().Length - lev, lev);
                    rtn = Convert.ToInt32(rightStr);
                }
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
                //sb.Append(StringUtil.RepeatChar('_', lev));
                sb.Append("'");
                sb.Append(" ORDER BY Stdcode DESC");
                Object o = DataBase.GetOneValue(sb.ToString(), out errMsg);
                sb.Length = 0;
                //string sql="select Stdcode from tProprietors where Stdcode like '" + prevcode 
                //    + StringUtil.RepeatChar('_',lev) + "' order by Stdcode desc";
                //Object o=DataBase.GetOneValue(sql,out errMsg);
                if (o == null)
                {
                    rtn = 0;
                }
                else
                {
                    string rightString = o.ToString().Substring(o.ToString().Length - lev, lev);
                    rtn = Convert.ToInt32(rightString);
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                rtn = -1;
            }

            return rtn;
        }

        /// <summary>
        /// 通过ID获取名称
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string NameFromCode(string code)
        {
            string errMsg = string.Empty;
            if (code.Equals(string.Empty))
            {
                return string.Empty;
            }

            try
            {
                string sql = string.Format("SELECT FULLNAME FROM TCOMPANY WHERE SYSCODE='{0}' ORDER BY SYSCODE", code);
                Object o = DataBase.GetOneValue(sql, out errMsg);
                if (o == null)
                {
                    return string.Empty;
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
        /// 获取DisplayName
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string CiidNameFromCode(string code)
        {
            string errMsg = string.Empty;
            if (code.Equals(string.Empty))
            {
                return string.Empty;
            }

            try
            {
                string sql = string.Format("Select Ciname from tProprietors where cdname='{0}'", code);

                Object o = DataBase.GetOneValue(sql, out errMsg);
                if (o == null)
                {
                    return string.Empty;
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
        /// 标准编码获取名称
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string NameFromStdCode(string code)
        {
            string sErrMsg = string.Empty;
            if (code.Equals(string.Empty))
            {
                return string.Empty;
            }

            try
            {
                string sql = string.Format("SELECT FULLNAME FROM TCOMPANY WHERE STDCODE='{0}' ORDER BY STDCODE", code);
                Object o = DataBase.GetOneValue(sql, out sErrMsg);
                if (o == null)
                {
                    return string.Empty;
                }
                else
                {
                    return o.ToString();
                }
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
            if (name.Equals(string.Empty))
            {
                return string.Empty;
            }

            try
            {
                string sql = string.Format("SELECT Cdname FROM tProprietors WHERE Cdname='{0}' ORDER BY Cdname", name);
                Object o = DataBase.GetOneValue(sql, out errMsg);
                if (o == null)
                {
                    return string.Empty;
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

        public static string GetCompanyCode(string name)
        {
            string errMsg = string.Empty;
            if (name.Equals(string.Empty))
            {
                return string.Empty;
            }

            try
            {
                string sql = string.Format("SELECT SysCode FROM TCOMPANY WHERE FULLNAME='{0}' ORDER BY SYSCODE", name);
                Object o = DataBase.GetOneValue(sql, out errMsg);
                if (o == null)
                {
                    return string.Empty;
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
        /// 通过标准编码获取编码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string CodeFromStdCode(string code)
        {
            string errMsg = string.Empty;
            if (code.Equals(string.Empty))
            {
                return string.Empty;
            }

            try
            {
                string sql = string.Format("SELECT SYSCODE FROM TCOMPANY WHERE STDCODE='{0}' ORDER BY STDCODE", code);
                Object o = DataBase.GetOneValue(sql, out errMsg);
                if (o == null)
                {
                    return string.Empty;
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

        public static string KindCodeFromStdCode(string Stdcode)
        {
            string errMsg = string.Empty;
            if (Stdcode.Equals(string.Empty))
            {
                return string.Empty;
            }

            try
            {
                string sql = string.Format("SELECT KINDCODE FROM TCOMPANY WHERE STDCODE='{0}' ORDER BY STDCODE", Stdcode);
                Object o = DataBase.GetOneValue(sql, out errMsg);
                if (o == null)
                {
                    return string.Empty;
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

        public static string AreaCodeFromStdCode(string stdCode)
        {
            string errMsg = string.Empty;
            if (stdCode.Equals(string.Empty))
            {
                return string.Empty;
            }

            try
            {
                string sql = string.Format("SELECT DISTRICTCODE FROM TCOMPANY WHERE STDCODE='{0}' ORDER BY STDCODE", stdCode);

                Object o = DataBase.GetOneValue(sql, out errMsg);
                if (o == null)
                {
                    return string.Empty;
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

        public static bool CompanyIsExist(string swhere)
        {
            string errMsg = string.Empty;
            if (swhere.Equals(string.Empty))
            {
                return false;
            }

            try
            {
                string sql = string.Format("SELECT SYSCODE FROM TCOMPANY WHERE {0}", swhere);
                Object o = DataBase.GetOneValue(sql, out errMsg);
                if (o == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
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
            if (code.Equals(string.Empty))
            {
                return string.Empty;
            }

            try
            {
                string sql = string.Format("SELECT TCOMPANYKIND.NAME FROM TCOMPANY,TCOMPANYKIND WHERE tProprietors.syscode='{0}' AND TCOMPANY.KINDCODE=TCOMPANYKIND.SYSCODE ORDER BY TCOMPANY.SYSCODE", code);

                object o = DataBase.GetOneValue(sql, out errMsg);
                if (o != null)
                {
                    return o.ToString();
                }
                else
                {
                    return string.Empty;
                }
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
            if (code.Equals(string.Empty))
            {
                return string.Empty;
            }

            try
            {
                string sql = string.Format("SELECT STDCODE FROM TCOMPANY WHERE SYSCODE='{0}' ORDER BY SYSCODE", code);
                Object o = DataBase.GetOneValue(sql, out errMsg);
                if (o == null)
                {
                    return string.Empty;
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

        public DataTable GetAllCompanies()
        {
            string districtCode = clsUserUnitOpr.GetNameFromCode("DistrictCode", "0001");
            string sqlWhere = string.Format("Property='被检单位' And IsReadOnly=true And Len(StdCode)=6 AND DistrictCode LIKE '{0}%'", districtCode);
            DataTable companies = GetAsDataTable(sqlWhere, "DistrictCode ASC,KindCode ASC", 1);

            return companies;
        }
    }
}

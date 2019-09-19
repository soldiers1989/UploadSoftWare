using System;
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
                    sb.Append("SELECT A.SysCode,A.StdCode,A.CompanyID,A.OtherCodeInfo,A.FullName,");
                    sb.Append("A.ShortName,A.DisplayName,A.ShortCut,A.Property,A.KindCode,A.KindName,A.RegCapital,A.Unit,A.Incorporator,");
                    sb.Append("A.RegDate,A.DistrictCode,C.Name As DistrictName,A.PostCode,A.Address,A.LinkMan,A.LinkInfo,");
                    sb.Append("A.CreditLevel,A.CreditRecord,A.ProductInfo,A.OtherInfo,A.CheckLevel,A.FoodSafeRecord,");
                    sb.Append("A.IsReadOnly,A.IsLock,A.ComProperty,A.Remark");
                    sb.Append(" FROM [SELECT D.SysCode,D.StdCode,D.CompanyID,d.OtherCodeInfo,d.FullName,d.ShortName,d.DisplayName,d.ShortCut,d.Property,d.KindCode,B.Name As KindName,d.RegCapital,d.Unit,d.Incorporator,d.RegDate,d.DistrictCode,d.PostCode,d.Address,d.LinkMan,d.LinkInfo,d.CreditLevel,d.CreditRecord,d.ProductInfo,d.OtherInfo,d.CheckLevel,d.FoodSafeRecord,d.IsReadOnly,d.IsLock,d.Remark,D.ComProperty FROM tCompany As d Left Join tCompanyKind As B  On d.KindCode=B.SysCode]. AS A LEFT JOIN tDistrict AS C ON A.DistrictCode=C.SysCode");
                }
                else if (type == 1)
                {
                    sb.Append("SELECT FULLNAME,STDCODE,SYSCODE,KINDCODE,DISPLAYNAME FROM tCompany");
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
                sb.Append("(SysCode,StdCode,CompanyID,OtherCodeInfo,FullName,");
                sb.Append("ShortName,DisplayName,ShortCut,Property,KindCode,RegCapital,Unit,Incorporator,");
                if (model.RegDate != null)
                {
                    sb.Append("RegDate,");
                }
                sb.Append("DistrictCode,PostCode,Address,LinkMan,LinkInfo,");
                sb.Append("CreditLevel,CreditRecord,ProductInfo,OtherInfo,CheckLevel,FoodSafeRecord,IsReadOnly,IsLock,Remark,ComProperty)");
                sb.Append(" VALUES(");
                sb.AppendFormat("'{0}',", model.SysCode);
                sb.AppendFormat("'{0}',", model.StdCode);
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
                sb.AppendFormat("'{0}'", model.ComProperty);
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

		public int GetMaxNO(string prevCode,int lev,out string errMsg)
		{
			errMsg=string.Empty; 
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
				Object o=DataBase.GetOneValue(sb.ToString(),out errMsg);
                sb.Length = 0;
				if(o==null)
				{
					rtn=0;
				}
				else
				{
					string rightStr=o.ToString().Substring(o.ToString().Length-lev,lev);
					rtn=Convert.ToInt32(rightStr);
				}
			}
			catch(Exception e)
			{
				errMsg=e.Message;
				rtn=-1;
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
            if (code.Equals(""))
            {
                return "";
            }

            try
            {
                string sql = string.Format("SELECT FULLNAME FROM TCOMPANY WHERE SYSCODE='{0}' ORDER BY SYSCODE", code);
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
                sErrMsg = e.Message;
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
            if (code.Equals(""))
            {
                return "";
            }

            try
            {
                string sql = string.Format("SELECT TCOMPANYKIND.NAME FROM TCOMPANY,TCOMPANYKIND WHERE tCompany.syscode='{0}' AND TCOMPANY.KINDCODE=TCOMPANYKIND.SYSCODE ORDER BY TCOMPANY.SYSCODE", code);

                object o = DataBase.GetOneValue(sql, out errMsg);
                if (o != null)
                {
                    return o.ToString();
                }
                else
                {
                    return "";
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
            if (code.Equals(""))
            {
                return "";
            }

            try
            {
                string sql = string.Format("SELECT STDCODE FROM TCOMPANY WHERE SYSCODE='{0}' ORDER BY SYSCODE", code);
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
	}
}

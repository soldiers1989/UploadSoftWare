using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DYSeriesDataSet.DataSentence.Kjc
{
    public static class kjcCompanyBLL
    {
        private static StringBuilder sql = new StringBuilder();
        private static int rtn = 0;

        public static int Insert(kjcCompany model, out string errMsg)
        {
            sql.Length = rtn = 0;
            errMsg = string.Empty;
            try
            {
                sql.Append("Insert Into kjcCompany ");
                sql.Append("(regId,regName,regCorpName,organizationCode,regAddress,regPost,contactMan,contactPhone,uDate) ");
                sql.AppendFormat("Values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                    model.regId, model.regName, model.regCorpName, model.organizationCode, model.regAddress, model.regPost, model.contactMan, model.contactPhone, model.uDate);
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }

        public static int InsertChildTable(kjcCompany.business model, out string errMsg)
        {
            sql.Length = rtn = 0;
            errMsg = string.Empty;
            try
            {
                sql.Append("Insert Into Company_Business ");
                sql.Append("(cdId,cdName,cdIdNum,ciAddr,cicorpName,ciContMan,ciContType,ciPost,ciName,regId,cdeuslicence,uDate) ");
                sql.AppendFormat("Values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')",
                    model.cdId, model.cdName, model.cdIdNum, model.ciAddr, model.cicorpName, model.ciContMan, model.ciContType, model.ciPost, model.ciName, model.regId, model.cdeuslicence, model.uDate);
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }

        public static int Update(kjcCompany model, out string errMsg)
        {
            sql.Length = rtn = 0;
            errMsg = string.Empty;
            try
            {
                sql.Append("Update kjcCompany Set ");
                sql.AppendFormat("regName='{0}',regCorpName='{1}',contactMan='{2}',contactPhone='{3}',regAddress='{4}',regPost='{5}' ",
                    model.regName, model.regCorpName, model.contactMan, model.contactPhone, model.regAddress, model.regPost);
                //添加where条件
                sql.AppendFormat(" Where regId='{0}'", model.regId);
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }

        public static int DeletedChildTable(string where, out string errMsg)
        {
            sql.Length = rtn = 0;
            errMsg = string.Empty;
            try
            {
                sql.Append("Delete From Company_Business");
                if (where.Length > 0)
                {
                    sql.AppendFormat(" Where {0}", where);
                }
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }

        public static int Deleted(string where, out string errMsg)
        {
            sql.Length = rtn = 0;
            errMsg = string.Empty;
            try
            {
                sql.Append("Delete From kjcCompany");
                if (where.Length > 0)
                {
                    sql.AppendFormat(" Where {0}", where);
                }
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }

        public static DataTable GetDataTable(out string errMsg, int type = 0, string where = "", string orderBy = "")
        {
            errMsg = string.Empty;
            sql.Length = 0;
            DataTable dtbl = null;

            try
            {
                if (type == 0)
                {
                    sql.Append("Select * From kjcCompany");
                }
                else if (type == 1)
                {
                    sql.Append("Select regName AS 被检单位名称,regCorpName AS 法人代表,contactMan AS 联系人,contactPhone AS 联系信息,regAddress AS 公司地址,regId AS 系统编号 From Company");
                }

                //添加where条件
                if (where.Length > 0)
                {
                    sql.AppendFormat(" Where {0}", where);
                }

                //添加orderBy排序模式
                if (orderBy.Length > 0)
                {
                    sql.AppendFormat(" Order By {0}", orderBy);
                }

                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "dtbl" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["dtbl"];
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }

            return dtbl;
        }

        public static string GetColumnStr(string table, string column, string where, out string errMsg)
        {
            string rtnColumn = errMsg = string.Empty;
            sql.Length = 0;
            Object obj = null;

            try
            {
                sql.AppendFormat("Select {0} From {1} Where {2}", column, table, where);
                obj = DataBase.GetOneValue(sql.ToString(), out errMsg);
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }

            return (obj == null || obj == DBNull.Value) ? string.Empty : obj.ToString();
        }


        public static DataTable GetAsDataTable(string where, string orderBy, int type, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dt = null;
            sql.Length = 0;
            try
            {
                if (type == 0)
                {
                    sql.Append("SELECT A.SysCode,A.CompanyID,A.FullName,A.StdCode,A.CAllow,A.ISSUEAGENCY,A.ISSUEDATE,A.PERIODSTART,A.PERIODEND,A.VIOLATENUM,A.LONGITUDE,A.LATITUDE,A.SCOPE,A.PUNISH,A.OtherCodeInfo,");
                    sql.Append("A.ShortName,A.DisplayName,A.ShortCut,A.Property,A.KindCode,A.KindName,A.RegCapital,A.Unit,A.Incorporator,");
                    sql.Append("A.RegDate,A.DistrictCode,C.Name As DistrictName,A.PostCode,A.Address,A.LinkMan,A.LinkInfo,");
                    sql.Append("A.CreditLevel,A.CreditRecord,A.ProductInfo,A.OtherInfo,A.CheckLevel,A.FoodSafeRecord,");
                    sql.Append("A.IsReadOnly,A.IsLock,A.ComProperty,A.Remark,A.TSign");
                    sql.Append(" FROM [SELECT D.SysCode,D.StdCode,D.CompanyID,d.CAllow,d.ISSUEAGENCY,d.ISSUEDATE,d.PERIODSTART,d.PERIODEND,d.VIOLATENUM,d.LONGITUDE,d.LATITUDE,d.SCOPE,d.PUNISH,d.OtherCodeInfo,d.FullName,d.ShortName,d.DisplayName,d.ShortCut,d.Property,d.KindCode,B.Name As KindName,d.RegCapital,d.Unit,d.Incorporator,d.RegDate,d.DistrictCode,d.PostCode,d.Address,d.LinkMan,d.LinkInfo,d.CreditLevel,d.CreditRecord,d.ProductInfo,d.OtherInfo,d.CheckLevel,d.FoodSafeRecord,d.IsReadOnly,d.IsLock,d.Remark,d.TSign,D.ComProperty FROM tCompany As d Left Join tCompanyKind As B  On d.KindCode=B.SysCode]. AS A LEFT JOIN tDistrict AS C ON A.DistrictCode=C.SysCode");
                }
                else if (type == 1)
                {
                    sql.Append(" select fullname,syscode from tCompany where TSign <> '本地增' union select Cdname,Ciid from tProprietors where Ciid in(select syscode from tCompany where TSign <> '本地增') ");
                }
                else if (type == 2)
                {
                    sql.Append("SELECT c.fullname,t.[name] AS CompanyType,d.[name] AS OrganizationName,c.SysCode,c.Incorporator ");
                    sql.Append("FROM (tcompany AS c INNER JOIN tcompanyKind AS t on c.kindcode=t.syscode) ");
                    sql.Append("INNER JOIN tdistrict AS d on c.districtcode = d.syscode");
                }
                else if (type == 3)
                {
                    sql.Append("SELECT FULLNAME,STDCODE,SYSCODE,KINDCODE,DISPLAYNAME,Property,IsReadOnly,DistrictCode FROM tCompany");
                }
                if (!where.Equals(""))
                {
                    sql.Append(" WHERE ");
                    sql.Append(where);
                }
                if (!orderBy.Equals(""))
                {
                    sql.Append(" ORDER BY ");
                    sql.Append(orderBy);
                }
                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "Company" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Company"];
                sql.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
        }

    }
}

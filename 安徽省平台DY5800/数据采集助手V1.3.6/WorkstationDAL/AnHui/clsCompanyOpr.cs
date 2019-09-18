using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using WorkstationDAL.Basic;


namespace WorkstationDAL.AnHui
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

        private StringBuilder sql = new StringBuilder();
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
                sql.Length = 0;
                sql.Append("UPDATE TCOMPANY SET ");
                sql.AppendFormat("StdCode='{0}',", model.StdCode);
                sql.AppendFormat("CAllow='{0}',", model.CAllow);
                sql.AppendFormat("CompanyID='{0}',", model.CompanyID);
                sql.AppendFormat("OtherCodeInfo='{0}',", model.OtherCodeInfo);
                sql.AppendFormat("FullName='{0}',", model.FullName);
                sql.AppendFormat("ShortName='{0}',", model.ShortName);
                sql.AppendFormat("DisplayName='{0}',", model.DisplayName);
                sql.AppendFormat("ShortCut='{0}',", model.ShortCut);
                sql.AppendFormat("Property='{0}',", model.Property);
                sql.AppendFormat("KindCode='{0}',", model.KindCode);
                sql.AppendFormat("RegCapital={0},", model.RegCapital);
                sql.AppendFormat("Unit='{0}',", model.Unit);
                sql.AppendFormat("Incorporator='{0}',", model.Incorporator);
                if (model.RegDate != null)
                    sql.AppendFormat("RegDate='{0}',", model.RegDate);
                if (model.RegDate == null)
                    sql.AppendFormat("RegDate=null,", "");
                sql.AppendFormat("DistrictCode='{0}',", model.DistrictCode);
                sql.AppendFormat("PostCode='{0}',", model.PostCode);
                sql.AppendFormat("Address='{0}',", model.Address);
                sql.AppendFormat("LinkMan='{0}',", model.LinkMan);
                sql.AppendFormat("LinkInfo='{0}',", model.LinkInfo);
                sql.AppendFormat("CreditLevel='{0}',", model.CreditLevel);
                sql.AppendFormat("CreditRecord='{0}',", model.CreditRecord);
                sql.AppendFormat("ProductInfo='{0}',", model.ProductInfo);
                sql.AppendFormat("OtherInfo='{0}',", model.OtherInfo);
                sql.AppendFormat("CheckLevel='{0}',", model.CheckLevel);
                sql.AppendFormat("FoodSafeRecord='{0}',", model.FoodSafeRecord);
                sql.AppendFormat("IsReadOnly={0},", model.IsReadOnly);
                sql.AppendFormat("IsLock={0},", model.IsLock);
                sql.AppendFormat("Remark='{0}',", model.Remark);
                sql.AppendFormat("ComProperty='{0}'", model.ComProperty);
                sql.AppendFormat("  WHERE SysCode='{0}'", sOldCode);
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;
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
                sql.Length = 0;
                sql.Append("DELETE FROM TCOMPANY");
                if (!whereSql.Equals(""))
                {
                    sql.Append(" WHERE ");
                    sql.Append(whereSql);
                }
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;
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
                return 0;
            int rtn = 0;
            try
            {
                int len = mainkey.Length;
                string deleteSql = string.Empty;
                deleteSql = string.Format("DELETE FROM TCOMPANY WHERE MID(SysCode,1,{1})='{0}'", mainkey, len);//STDCODE
                bool flag = DataBase.ExecuteCommand(deleteSql, out errMsg);
                if (flag)
                    rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message; ;
            }
            return rtn;
        }

        /// <summary>
        /// 根据ID删除被检单位
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="sErrMsg"></param>
        /// <returns></returns>
        public int DeleteByID(int ID, out string sErrMsg)
        {
            sErrMsg = string.Empty;
            int rtn = 0;
            try
            {
                string deleteSql = string.Format("DELETE FROM tCompany WHERE ID={0}", ID);
                DataBase.ExecuteCommand(deleteSql, out sErrMsg);
                rtn = 1;
            }
            catch (Exception e)
            {
                sErrMsg = e.Message; ;
            }
            return rtn;
        }


        /// <summary>
        /// 根据查询串条件查询记录
        /// </summary>
        /// <param name="name">查询条件串,不含Where</param>
        /// <param name="orderBySql">排序串,不含Order</param>
        /// <returns></returns>
        public DataTable GetAsDataTable(string name)
        {
            string errMsg = string.Empty;
            DataTable dt = null;
            sql.Length = 0;
            try
            {
                sql.Append("SELECT * FROM tCompany");
                if (!name.Equals(""))
                {
                    sql.Append(" WHERE ");
                    sql.Append(name);
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
                    sql.Append(" select fullname,syscode from tCompany  where TSign<>'本地增' union  Select Cdname,Ciid from tProprietors   ");
                else if (type == 2)
                {
                    sql.Append("SELECT c.fullname,t.[name] AS CompanyType,d.[name] AS OrganizationName,c.SysCode,c.Incorporator ");
                    sql.Append("FROM (tcompany AS c INNER JOIN tcompanyKind AS t on c.kindcode=t.syscode) ");
                    sql.Append("INNER JOIN tdistrict AS d on c.districtcode = d.syscode");
                }
                else if (type == 3)
                    sql.Append("SELECT FULLNAME,STDCODE,SYSCODE,KINDCODE,DISPLAYNAME,Property,IsReadOnly,DistrictCode FROM tCompany");
                else if (type == 4)
                    sql.Append("SELECT FULLNAME FROM tCompany");
                else if (type == 5)
                    sql.Append("SELECT FULLNAME,CompanyID FROM tCompany");
                else if (type == 6)//下载被检单位，判定是否已经存在
                    sql.Append("SELECT * FROM tCompany");
                else if (type == 7)//查询最近一次更新时间
                    sql.Append("Select Top 1 * from tCompany Order By UDate Desc");
                else if (type == 8)//下载检测项目，判定是否已经存在
                    sql.Append("SELECT * FROM tCheckItems");
                else if (type == 9)//下载检测标准，判定是否已经存在
                    sql.Append("SELECT * FROM tStandard");
                else if (type == 10)//下载检测项目，查看关联数据库表tStandardType是否已有记录tTask
                    sql.Append("SELECT * FROM tStandardType");
                else if (type == 11)//任务更新，判定是否已经存在
                    sql.Append("SELECT * FROM tTask");
                else if (type == 12)//任务更新，获取最后一次更新时间
                    sql.Append("SELECT Top 1 * FROM tTask Order By UDate Desc");
                if (!whereSql.Equals(""))
                {
                    sql.Append(" WHERE ");
                    sql.Append(whereSql);
                }
                if (!orderBySql.Equals(""))
                {
                    sql.Append(" ORDER BY ");
                    sql.Append(orderBySql);
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

        public static IList<T> DataTableToIList<T>(DataTable p_DataSet, int p_TableName)
        {
            List<T> list = new List<T>();
            T t = default(T);
            PropertyInfo[] propertypes = null;
            string tempName = string.Empty;
            foreach (DataRow row in p_DataSet.Rows)
            {
                t = Activator.CreateInstance<T>();
                propertypes = t.GetType().GetProperties();
                foreach (PropertyInfo pro in propertypes)
                {
                    tempName = pro.Name;
                    if (p_DataSet.Columns.Contains(tempName))
                    {
                        object value = row[tempName];
                        if (!value.ToString().Equals(""))
                        {
                            pro.SetValue(t, value, null);
                        }
                    }
                }
                list.Add(t);
            }
            return list.Count == 0 ? null : list;
        }

        /// <summary>
        /// 下载|更新 被检单位
        /// </summary>
        /// <param name="model">clsCompany</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int InsertOrUpdate(clsCompany model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sql.Length = 0;
                //根据编码查询是否已存在该数据
                DataTable dt = null;// this.GetAsDataTable("SysCode Like '" + model.SysCode + "'", "", 6);
                //如果存在则修改
                if (dt != null && dt.Rows.Count > 0)
                {
                    //修改
                    sql.AppendFormat("Update tCompany Set SysCode = '{0}',StdCode = '{1}',CAllow = '{2}',", model.SysCode, model.StdCode, model.CAllow);
                    sql.AppendFormat("ISSUEAGENCY = '{0}',ISSUEDATE = '{1}',PERIODSTART = '{2}',PERIODEND = '{3}',", model.ISSUEAGENCY, model.ISSUEDATE, model.PERIODSTART, model.PERIODEND);
                    sql.AppendFormat("VIOLATENUM = '{0}',LONGITUDE = '{1}',LATITUDE = '{2}',SCOPE = '{3}',", model.VIOLATENUM, model.LONGITUDE, model.LATITUDE, model.SCOPE);
                    sql.AppendFormat("PUNISH = '{0}',CompanyID = '{1}',OtherCodeInfo = '{2}',FullName = '{3}',", model.PUNISH, model.CompanyID, model.OtherCodeInfo, model.FullName);
                    sql.AppendFormat("ShortName = '{0}',DisplayName = '{1}',ShortCut = '{2}',Property = '{3}',", model.ShortName, model.DisplayName, model.ShortCut, model.Property);
                    sql.AppendFormat("KindCode = '{0}',RegCapital = '{1}',Unit = '{2}',Incorporator = '{3}',", model.KindCode, model.RegCapital, model.Unit, model.Incorporator);
                    sql.AppendFormat("DistrictCode = '{0}',Address = '{1}',PostCode = '{2}',", model.DistrictCode, model.Address, model.PostCode);
                    sql.AppendFormat("LinkMan = '{0}',LinkInfo = '{1}',CreditLevel = '{2}',CreditRecord = '{3}',", model.LinkMan, model.LinkInfo, model.CreditLevel, model.CreditRecord);
                    sql.AppendFormat("ProductInfo = '{0}',OtherInfo = '{1}',CheckLevel = '{2}',FoodSafeRecord = '{3}',", model.ProductInfo, model.OtherInfo, model.CheckLevel, model.FoodSafeRecord);
                    sql.AppendFormat("IsReadOnly = {0},IsLock = {1},Remark = '{2}',ComProperty = '{3}',", model.IsReadOnly, model.IsLock, model.Remark, model.ComProperty);
                    sql.AppendFormat("TSign = '{0}',UDate= '{1}' Where SysCode = '{2}'", model.TSign, model.UDate, model.SysCode);
                }
                else//不存在则新增
                {
                    sql.Append("INSERT INTO tCompany");
                    sql.Append("(SysCode,StdCode,CAllow,ISSUEAGENCY,ISSUEDATE,PERIODSTART,PERIODEND,VIOLATENUM,LONGITUDE,LATITUDE,SCOPE,PUNISH,CompanyID,OtherCodeInfo,FullName,");
                    sql.Append("ShortName,DisplayName,ShortCut,Property,KindCode,RegCapital,Unit,Incorporator,");
                    //if (model.RegDate != null)
                    //    sb.Append("RegDate,");
                    sql.Append("DistrictCode,PostCode,Address,LinkMan,LinkInfo,");
                    sql.Append("CreditLevel,CreditRecord,ProductInfo,OtherInfo,CheckLevel,FoodSafeRecord,IsReadOnly,IsLock,Remark,ComProperty,TSign,UDate)");
                    sql.Append(" VALUES(");
                    sql.AppendFormat("'{0}',", model.SysCode);
                    sql.AppendFormat("'{0}',", model.StdCode);
                    sql.AppendFormat("'{0}',", model.CAllow);
                    sql.AppendFormat("'{0}',", model.ISSUEAGENCY);
                    sql.AppendFormat("'{0}',", model.ISSUEDATE);
                    sql.AppendFormat("'{0}',", model.PERIODSTART);
                    sql.AppendFormat("'{0}',", model.PERIODEND);
                    sql.AppendFormat("'{0}',", model.VIOLATENUM);
                    sql.AppendFormat("'{0}',", model.LONGITUDE);
                    sql.AppendFormat("'{0}',", model.LATITUDE);
                    sql.AppendFormat("'{0}',", model.SCOPE);
                    sql.AppendFormat("'{0}',", model.PUNISH);
                    sql.AppendFormat("'{0}',", model.CompanyID);
                    sql.AppendFormat("'{0}',", model.OtherCodeInfo);
                    sql.AppendFormat("'{0}',", model.FullName);
                    sql.AppendFormat("'{0}',", model.ShortName);
                    sql.AppendFormat("'{0}',", model.DisplayName);
                    sql.AppendFormat("'{0}',", model.ShortCut);
                    sql.AppendFormat("'{0}',", model.Property);
                    sql.AppendFormat("'{0}',", model.KindCode);
                    sql.AppendFormat("{0},", model.RegCapital);
                    sql.AppendFormat("'{0}',", model.Unit);
                    sql.AppendFormat("'{0}',", model.Incorporator);
                    //if (model.RegDate != null)
                    //    sb.AppendFormat("'{0}',", model.RegDate);
                    sql.AppendFormat("'{0}',", model.DistrictCode);
                    sql.AppendFormat("'{0}',", model.PostCode);
                    sql.AppendFormat("'{0}',", model.Address);
                    sql.AppendFormat("'{0}',", model.LinkMan);
                    sql.AppendFormat("'{0}',", model.LinkInfo);
                    sql.AppendFormat("'{0}',", model.CreditLevel);
                    sql.AppendFormat("'{0}',", model.CreditRecord);
                    sql.AppendFormat("'{0}',", model.ProductInfo);
                    sql.AppendFormat("'{0}',", model.OtherInfo);
                    sql.AppendFormat("'{0}',", model.CheckLevel);
                    sql.AppendFormat("'{0}',", model.FoodSafeRecord);
                    sql.AppendFormat("{0},", model.IsReadOnly);
                    sql.AppendFormat("{0},", model.IsLock);
                    sql.AppendFormat("'{0}',", model.Remark);
                    sql.AppendFormat("'{0}',", model.ComProperty);
                    sql.AppendFormat("'{0}',", model.TSign);
                    sql.AppendFormat("'{0}'", model.UDate);
                    sql.Append(")");
                    //FileUtils.TestLog(sb.ToString());
                }
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }
        /// <summary>
        /// 插入一条明细记录
        /// </summary>
        /// <param name="model">ID>0为修改 ID=0为新增</param>
        /// <returns></returns>
        public int Insert(clsCompany model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sql.Length = 0;
                if (model.ID > 0)
                {
                    //修改
                    sql.AppendFormat("Update tCompany Set SysCode = '{0}',StdCode = '{1}',CAllow = '{2}',", model.SysCode, model.StdCode, model.CAllow);
                    sql.AppendFormat("ISSUEAGENCY = '{0}',ISSUEDATE = '{1}',PERIODSTART = '{2}',PERIODEND = '{3}',", model.ISSUEAGENCY, model.ISSUEDATE, model.PERIODSTART, model.PERIODEND);
                    sql.AppendFormat("VIOLATENUM = '{0}',LONGITUDE = '{1}',LATITUDE = '{2}',SCOPE = '{3}',", model.VIOLATENUM, model.LONGITUDE, model.LATITUDE, model.SCOPE);
                    sql.AppendFormat("PUNISH = '{0}',CompanyID = '{1}',OtherCodeInfo = '{2}',FullName = '{3}',", model.PUNISH, model.CompanyID, model.OtherCodeInfo, model.FullName);
                    sql.AppendFormat("ShortName = '{0}',DisplayName = '{1}',ShortCut = '{2}',Property = '{3}',", model.ShortName, model.DisplayName, model.ShortCut, model.Property);
                    sql.AppendFormat("KindCode = '{0}',RegCapital = '{1}',Unit = '{2}',Incorporator = '{3}',", model.KindCode, model.RegCapital, model.Unit, model.Incorporator);
                    //sb.AppendFormat("RegDate = '{0}',DistrictCode = '{1}',Address = '{2}',PostCode = '{3}',", model.RegDate, model.DistrictCode, model.Address, model.PostCode);
                    sql.AppendFormat("DistrictCode = '{0}',Address = '{1}',PostCode = '{2}',", model.DistrictCode, model.Address, model.PostCode);
                    sql.AppendFormat("LinkMan = '{0}',LinkInfo = '{1}',CreditLevel = '{2}',CreditRecord = '{3}',", model.LinkMan, model.LinkInfo, model.CreditLevel, model.CreditRecord);
                    sql.AppendFormat("ProductInfo = '{0}',OtherInfo = '{1}',CheckLevel = '{2}',FoodSafeRecord = '{3}',", model.ProductInfo, model.OtherInfo, model.CheckLevel, model.FoodSafeRecord);
                    sql.AppendFormat("IsReadOnly = {0},IsLock = {1},Remark = '{2}',ComProperty = '{3}',", model.IsReadOnly, model.IsLock, model.Remark, model.ComProperty);
                    sql.AppendFormat("TSign = '{0}' Where ID = {1}", model.TSign, model.ID);
                }
                else
                {
                    //新增
                    sql.Append("INSERT INTO tCompany");
                    sql.Append("(SysCode,StdCode,CAllow,ISSUEAGENCY,ISSUEDATE,PERIODSTART,PERIODEND,VIOLATENUM,LONGITUDE,LATITUDE,SCOPE,PUNISH,CompanyID,OtherCodeInfo,FullName,");
                    sql.Append("ShortName,DisplayName,ShortCut,Property,KindCode,RegCapital,Unit,Incorporator,");
                    if (model.RegDate != null)
                    {
                        sql.Append("RegDate,");
                    }
                    sql.Append("DistrictCode,PostCode,Address,LinkMan,LinkInfo,");
                    sql.Append("CreditLevel,CreditRecord,ProductInfo,OtherInfo,CheckLevel,FoodSafeRecord,IsReadOnly,IsLock,Remark,ComProperty,TSign)");
                    sql.Append(" VALUES(");
                    sql.AppendFormat("'{0}',", model.SysCode);
                    sql.AppendFormat("'{0}',", model.StdCode);
                    sql.AppendFormat("'{0}',", model.CAllow);
                    sql.AppendFormat("'{0}',", model.ISSUEAGENCY);
                    sql.AppendFormat("'{0}',", model.ISSUEDATE);
                    sql.AppendFormat("'{0}',", model.PERIODSTART);
                    sql.AppendFormat("'{0}',", model.PERIODEND);
                    sql.AppendFormat("'{0}',", model.VIOLATENUM);
                    sql.AppendFormat("'{0}',", model.LONGITUDE);
                    sql.AppendFormat("'{0}',", model.LATITUDE);
                    sql.AppendFormat("'{0}',", model.SCOPE);
                    sql.AppendFormat("'{0}',", model.PUNISH);
                    sql.AppendFormat("'{0}',", model.CompanyID);
                    sql.AppendFormat("'{0}',", model.OtherCodeInfo);
                    sql.AppendFormat("'{0}',", model.FullName);
                    sql.AppendFormat("'{0}',", model.ShortName);
                    sql.AppendFormat("'{0}',", model.DisplayName);
                    sql.AppendFormat("'{0}',", model.ShortCut);
                    sql.AppendFormat("'{0}',", model.Property);
                    sql.AppendFormat("'{0}',", model.KindCode);
                    sql.AppendFormat("{0},", model.RegCapital);
                    sql.AppendFormat("'{0}',", model.Unit);
                    sql.AppendFormat("'{0}',", model.Incorporator);
                    if (model.RegDate != null)
                        sql.AppendFormat("'{0}',", model.RegDate);
                    sql.AppendFormat("'{0}',", model.DistrictCode);
                    sql.AppendFormat("'{0}',", model.PostCode);
                    sql.AppendFormat("'{0}',", model.Address);
                    sql.AppendFormat("'{0}',", model.LinkMan);
                    sql.AppendFormat("'{0}',", model.LinkInfo);
                    sql.AppendFormat("'{0}',", model.CreditLevel);
                    sql.AppendFormat("'{0}',", model.CreditRecord);
                    sql.AppendFormat("'{0}',", model.ProductInfo);
                    sql.AppendFormat("'{0}',", model.OtherInfo);
                    sql.AppendFormat("'{0}',", model.CheckLevel);
                    sql.AppendFormat("'{0}',", model.FoodSafeRecord);
                    sql.AppendFormat("{0},", model.IsReadOnly);
                    sql.AppendFormat("{0},", model.IsLock);
                    sql.AppendFormat("'{0}',", model.Remark);
                    sql.AppendFormat("'{0}',", model.ComProperty);
                    sql.AppendFormat("'{0}'", model.TSign);
                    sql.Append(")");
                }
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }

    }
}

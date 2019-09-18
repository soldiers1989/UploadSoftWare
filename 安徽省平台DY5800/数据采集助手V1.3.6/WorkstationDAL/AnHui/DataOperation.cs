using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using WorkstationDAL.Basic;

namespace WorkstationDAL.AnHui
{
    public static class DataOperation
    {
        private static StringBuilder sql = new StringBuilder();

        public static int Del(string type, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            sql.Length = 0;
            string table = string.Empty;
            try
            {
                if (type.Equals("data_dictionary"))
                {
                    table = "ah_data_dictionary";
                }
                else if (type.Equals("checked_unit"))
                {
                    table = "ah_checked_unit";
                }
                else if (type.Equals("standard_limit"))
                {
                    table = "ah_standard_limit";
                }
                else if (type.Equals("Company"))
                {
                    table = "tCompany";
                }

                sql.AppendFormat("Delete From {0}", table);
                DataBase.ExecuteCommand(sql.ToString());
                sql.Length = 0;
                rtn = 1;
            }
            catch (Exception ex)
            {
                rtn = 0;
                errMsg = ex.Message;
            }
            return rtn;
        }

        public static int Insert(data_dictionary model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            sql.Length = 0;
            try
            {
                sql.Append("Insert Into ah_data_dictionary ");
                sql.Append("(ID,name,codeId,typeNum,pid,remark,status) ");
                sql.AppendFormat("VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                    model.id, model.name, model.codeId, model.typeNum, model.pid, model.remark, model.status);
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;
                rtn = 1;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                rtn = 0;
            }
            return rtn;
        }

        public static int Insert(checked_unit model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            sql.Length = 0;
            try
            {
                sql.Append("Insert Into ah_checked_unit ");
                sql.Append("(ID,bussinessId,unitName,busScope,address,linkName,tel,idCard,status) ");
                sql.AppendFormat("VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                    model.id, model.bussinessId, model.unitName, model.busScope, model.address,
                    model.linkName, model.tel, model.idCard, model.status);
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;
                rtn = 1;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                rtn = 0;
            }
            return rtn;
        }

        public static int Insert(standard_limit model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            sql.Length = 0;
            try
            {
                sql.Append("Insert Into ah_standard_limit ");
                sql.Append("(ID,foodType,testItem,testBasis,decisionBasis,minLimit,maxLimit,unit) ");
                sql.AppendFormat("VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                    model.id, model.foodType, model.testItem, model.testBasis, model.decisionBasis,
                    model.minLimit, model.maxLimit, model.unit);
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;
                rtn = 1;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                rtn = 0;
            }
            return rtn;
        }

        public static DataTable GetAsDataTable(string type, string whereSql)
        {
            string errMsg = string.Empty;
            DataTable dataTable = null;
            sql.Length = 0;
            string table = string.Empty;
            if (type.Equals("data_dictionary"))
            {
                table = "ah_data_dictionary";
            }
            else if (type.Equals("checked_unit"))
            {
                table = "ah_checked_unit";
            }
            else if (type.Equals("standard_limit"))
            {
                table = "ah_standard_limit";
            }
            else
            {
                return null;
            }

            try
            {
                sql.AppendFormat("SELECT * FROM {0}", table);
                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "dataTable" };
                dataTable = DataBase.GetDataSet(cmd, names, out errMsg).Tables["dataTable"];
                sql.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dataTable;
        }

        /// <summary>
        /// 根据名称获取种类
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DataTable GetSampleTypeByName(string name)
        {
            string errMsg = string.Empty;
            DataTable dtbl = null;
            try
            {
                sql.Length = 0;
                string where = name.Length > 0 ? " name = '" + name + "'" : "";
                if (where.Length > 0)
                {
                    sql.AppendFormat("Select ID,name,codeId,typeNum,pid,remark From ah_data_dictionary where {0}  ", where);
                }
                else
                {
                    sql.AppendFormat("Select ID,name,codeId,typeNum,pid,remark From ah_data_dictionary order by ID", where);
                }
               
                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "ah_data_dictionary" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["ah_data_dictionary"];
                sql.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dtbl;
        }

        /// <summary>
        /// 根据名称查询检测项目
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DataTable GetCheckProjectByName(string name)
        {
            string errMsg = string.Empty;
            DataTable dtbl = null;
            try
            {
                sql.Length = 0;
                string where = name.Length > 0 ? "and name = '" + name + "'" : "";
                sql.AppendFormat("Select * from ah_data_dictionary where pid='1' and pid <> '-1' {0}",
                    where);
                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "ah_data_dictionary" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["ah_data_dictionary"];
                sql.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dtbl;
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
                return "";
            try
            {
                string sql = string.Format("SELECT CompanyID FROM TCOMPANY WHERE FULLNAME='{0}' ", code);
                Object o = DataBase.GetOneValue(sql, out errMsg);
                if (o == null)
                    return "";
                else
                    return o.ToString();
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
        }

    }
}

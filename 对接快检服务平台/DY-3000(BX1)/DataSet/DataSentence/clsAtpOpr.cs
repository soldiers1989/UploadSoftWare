﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace DYSeriesDataSet
{
    public class clsAtpOpr
    {
        public clsAtpOpr()
        {
        }

        private StringBuilder sql = new StringBuilder();

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int Update(clsATP model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sql.Length = 0;
                sql.Append("UPDATE tAtp SET ");
                sql.AppendFormat("USER='{0}',", model.Atp_CheckName);
                sql.AppendFormat("RLU='{0}',", model.Atp_RLU);
                sql.AppendFormat("RESULT='{0}',", model.Atp_Result);
                sql.AppendFormat("DATA='{0}',", model.Atp_CheckData);
                sql.AppendFormat("TIME='{0}',", model.Atp_CheckTime);
                sql.AppendFormat("UPPER='{0}',", model.Atp_Upper);
                sql.AppendFormat("LOWER='{0}',", model.Atp_Lower);
                sql.AppendFormat(" WHERE DATA='{0}' AND TIME='{1}' AND USER='{2}'",
                    model.Atp_CheckData, model.Atp_CheckTime, model.Atp_CheckName);
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
        /// 查询全部数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllAsDataTable()
        {
            string errMsg = string.Empty;
            DataTable dt = null;
            sql.Length = 0;
            try
            {
                sql.Append("SELECT * FROM tAtp");
                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "Atp" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Atp"];
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
        /// 插入一条明细记录
        /// </summary>
        /// <param name="model">ID>0为修改 ID=0为新增</param>
        /// <returns></returns>
        public int Insert(clsATP model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sql.Length = 0;
                sql.Append("INSERT INTO tAtp ");
                sql.Append("(ATP_CHECKNAME,ATP_RLU,ATP_RESULT,ATP_CHECKDATA,ATP_CHECKTIME,ATP_UPPER,ATP_LOWER) ");
                sql.Append("VALUES(");
                sql.AppendFormat("'{0}',", model.Atp_CheckName);
                sql.AppendFormat("'{0}',", model.Atp_RLU);
                sql.AppendFormat("'{0}',", model.Atp_Result);
                sql.AppendFormat("'{0}',", model.Atp_CheckData);
                sql.AppendFormat("'{0}',", model.Atp_CheckTime);
                sql.AppendFormat("'{0}',", model.Atp_Upper);
                sql.AppendFormat("'{0}'", model.Atp_Lower);
                sql.Append(")");
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using WorkstationDAL.Basic;

namespace WorkstationModel.Model
{
    public class clsSysOptOpr
    {
        public clsSysOptOpr()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        /// <summary>
        /// 更新命令行参数
        /// </summary>
        /// <param name="cmmdText"></param>
        /// <returns></returns>
        public int UpdateCommand(string cmmdText)
        {
            string sErrMsg = string.Empty;
            DataBase.ExecuteCommand(cmmdText);
            return 0;
        }

        /// <summary>
        /// 部分修改保存
        /// </summary>
        /// <param name="OprObject">对象clsSysOpt的一个实例参数</param>
        /// <returns></returns>
        public int Update(string setSql, string whereSql, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            if (string.IsNullOrEmpty(setSql))
            // if (setSql == null || setSql.Equals(""))
            {
                errMsg = "参数有误！";
                return -1;
            }

            try
            {
                sb.Length = 0;
                sb.Append("UPDATE tSysOpt SET ");
                sb.Append(setSql);

                //string updateSql = "update tSysOpt set " + setSql;
                if (!whereSql.Equals(""))
                {
                    sb.Append(" WHERE ");
                    sb.Append(whereSql);
                    //updateSql += " where " + whereSql;
                }

                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;
                rtn = 1;
            }
            catch (Exception e)
            {
                //Log.WriteLog("更新clsSysOpt",e);
                errMsg = e.Message;
            }
            return rtn;
        }

        /// <summary>
        /// 根据查询串条件查询记录
        /// </summary>
        /// <param name="whereSql">查询条件串,不含Where</param>
        /// <param name="orderBySql">排序串,不含Order</param>
        /// <returns></returns>
        public DataTable GetAsDataTable(string whereSql, string orderBySql)
        {
            string errMsg = string.Empty;
            DataTable dt = null;

            try
            {
                sb.Length = 0;
                sb.Append("SELECT SysCode,OptDes,OptType,OptValue,IsReadOnly,IsDisplay,IsLock,Remark FROM tSysOpt");

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
                string[] sCmd = new string[1] { sb.ToString() };


                string[] sNames = new string[1] { "SysOpt" };

                dt = DataBase.GetDataSet(sCmd, sNames, out errMsg).Tables["SysOpt"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                //Log.WriteLog("查询clsSysOpt",e);
                errMsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 获取指定列指定结果集结果集
        /// 作者：
        /// </summary>
        /// <param name="top">表示前N个记录，小于等于0表示"*"</param>
        /// <param name="strWhere">过滤条件组合串，输入字符串中不能包含where关键字，需要排序时在条件后加 order by</param>
        /// <param name="columns">表示1个或者多个字段字符串，多个字段之间用","隔开如： "ID,Name,Value,...."</param>
        /// <returns>返回结果集</returns>
        public DataTable GetColumnDataTable(int top, string strWhere, params string[] columns)
        {
            return DataBase.GetColumnList("tSysOpt", top, strWhere, columns);
        }
    }
}

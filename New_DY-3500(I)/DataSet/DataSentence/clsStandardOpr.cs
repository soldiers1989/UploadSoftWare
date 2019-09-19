using System;
using System.Data;
using System.Text;

namespace DataSetModel
{
	/// <summary>
	///检测标准
	/// </summary>
    public class clsStandardOpr
    {
        public clsStandardOpr()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        private StringBuilder sb = new StringBuilder();
        /// <summary>
        /// 部分修改保存
        /// </summary>
        /// <param name="model">对象clsStandard的一个实例参数</param>
        /// <returns></returns>
        public int UpdatePart(clsStandard model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                sb.Length = 0;
                sb.Append("UPDATE TSTANDARD SET ");
                sb.AppendFormat(" StdCode='{0}',", model.StdCode);
                sb.AppendFormat("StdDes='{0}',", model.StdDes);
                sb.AppendFormat("ShortCut='{0}',", model.ShortCut);
                sb.AppendFormat("StdInfo='{0}',", model.StdInfo);
                sb.AppendFormat("StdType='{0}',", model.StdType);
                sb.AppendFormat("IsReadOnly={0},", model.IsReadOnly);
                sb.AppendFormat("IsLock={0},", model.IsLock);
                sb.AppendFormat("Remark='{0}' ", model.Remark);
                sb.AppendFormat(" WHERE SysCode='{0}'", model.SysCode);
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;

                rtn = 1;
            }
            catch (Exception e)
            {
                //Log.WriteLog("更新clsStandard",e);
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
                sb.Append(" DELETE FROM tstandard ");

                if (!string.IsNullOrEmpty(whereSql))
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
        /// <param name="%pkname%">主键编号</param>
        /// <returns></returns>
        public int DeleteByPrimaryKey(string mainkey, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                string deleteSql = string.Format("DELETE FROM TSTANDARD WHERE SYSCODE='{0}'", mainkey);
                DataBase.ExecuteCommand(deleteSql, out errMsg);

                //string updateSql = "update tResult set "
                //    + "Standard=''"
                //    + " where Standard='" + mainkey + "' ";
                //DataBase.ExecuteCommand(updateSql, out sErrMsg);

                rtn = 1;
            }
            catch (Exception e)
            {
                //Log.WriteLog("通过主键删除clsStandard",e);
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

            try
            {
                sb.Length = 0;
                if (type == 0)
                {
                    sb.Append("SELECT SysCode,StdCode,StdDes,ShortCut,StdInfo,StdType,IsLock,IsReadOnly,Remark FROM tStandard");
                }
                else if (type == 1)
                {
                    sb.Append("SELECT StdDes,StdCode,SysCode FROM tStandard");
                }
                else if (type == 2)
                {
                    sb.Append("SELECT SysCode,StdDes,StdInfo FROM tStandard");
                }
                else if (type == 3)
                {
                    sb.Append("SELECT * FROM tStandard");
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
                string[] names = new string[1] { "Standard" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Standard"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                //Log.WriteLog("查询clsStandard",e);
                errMsg = e.Message;
            }

            return dt;
        }

        /// <summary>
        /// 插入一条明细记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(clsStandard model, out string errMsg)
        {
          
            errMsg = string.Empty;
            int rtn = 0;
            sb.Length = 0;
            try
            {
                sb.Append("INSERT INTO tStandard(SysCode,StdCode,StdDes,ShortCut,StdInfo,StdType,IsReadOnly,IsLock,Remark)VALUES(");
                sb.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}',{6},{7},'{8}'", model.SysCode, model.StdCode, model.StdDes, model.ShortCut, model.StdInfo, model.StdType, model.IsReadOnly, model.IsLock, model.Remark);
                sb.Append(")");
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;
                rtn = 1;
            }
            catch (Exception e)
            {
                //Log.WriteLog("添加clsStandard",e);
                errMsg = e.Message;
            }
            return rtn;
        }

        /// <summary>
        /// 获取最大编号
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int GetMaxNO(out string errMsg)
        {
            errMsg = string.Empty;
            int rtn;

            try
            {
                string sql = "SELECT SYSCODE FROM TSTANDARD ORDER BY SYSCODE DESC";
                Object o = DataBase.GetOneValue(sql, out errMsg);
                if (o == null)
                {
                    rtn = 0;
                }
                else
                {
                    rtn = Convert.ToInt32(o.ToString());
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
        /// 通过代码获取名称
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetNameFromCode(string code)
        {
            string errMsg = string.Empty;
            if (string.IsNullOrEmpty(code))
            {
                return string.Empty;
            }

            try
            {
                string sql = string.Format("SELECT STDDES FROM TSTANDARD WHERE SYSCODE='{0}' ORDER BY SYSCODE", code);
                object obj = DataBase.GetOneValue(sql, out errMsg);
                if (obj == null || obj == DBNull.Value)
                {
                    return string.Empty;
                }
                else
                {
                    return obj.ToString();
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return string.Empty;
            }
        }

        public string ErrMsg { get; set; }
    }
}

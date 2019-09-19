using System;
using System.Data;
using System.Text;

namespace DYSeriesDataSet
{
    /// <summary>
    /// 检测项目
    /// </summary>
    public class clsCheckItemOpr
    {
        public clsCheckItemOpr()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        private StringBuilder sql = new StringBuilder();

        /// <summary>
        /// 部分修改保存
        /// </summary>
        /// <param name="model">对象clsCheckItem的一个实例参数</param>
        /// <returns></returns>
        public int UpdatePart(clsCheckItem model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            sql.Length = 0;
            try
            {
                sql.Append("UPDATE TCHECKITEM SET ");
                sql.AppendFormat("StdCode='{0}',", model.StdCode);
                sql.AppendFormat("ItemDes='{0}',", model.ItemDes);
                sql.AppendFormat("CheckType='{0}',", model.CheckType);
                sql.AppendFormat("StandardCode='{0}',", model.StandardCode);
                sql.AppendFormat("StandardValue='{0}',", model.StandardValue);
                sql.AppendFormat("Unit='{0}',", model.Unit);
                sql.AppendFormat("DemarcateInfo='{0}',", model.DemarcateInfo);
                sql.AppendFormat("TestValue='{0}',", model.TestValue);
                sql.AppendFormat("OperateHelp='{0}',", model.OperateHelp);
                sql.AppendFormat("CheckLevel='{0}',", model.CheckLevel);
                sql.AppendFormat("IsReadOnly={0},", model.IsReadOnly);
                sql.AppendFormat("IsLock={0},", model.IsLock);
                sql.AppendFormat("Remark='{0}'", model.Remark);
                sql.AppendFormat(" WHERE SysCode='{0}',", model.SysCode);
                //+ "StdCode='" + model.StdCode + "'," 
                //+ "ItemDes='" + model.ItemDes + "',"
                //+ "CheckType='" + model.CheckType + "',"
                //+ "StandardCode='" + model.StandardCode + "',"
                //+ "StandardValue='" + model.StandardValue + "',"
                //+ "Unit='" + model.Unit + "',"
                //+ "DemarcateInfo='" + model.DemarcateInfo + "',"
                //+ "TestValue='" + model.TestValue + "',"
                //+ "OperateHelp='" + model.OperateHelp + "',"
                //+ "CheckLevel='" + model.CheckLevel + "',"
                //+ "IsReadOnly=" + model.IsReadOnly + "," 
                //+ "IsLock=" + model.IsLock + "," 
                //+ "Remark='" + model.Remark + "'" 
                //+ " where SysCode='" + model.SysCode + "' ";
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;

                rtn = 1;
            }
            catch (Exception e)
            {
                //Log.WriteLog("更新clsCheckItem",e);
                errMsg = e.Message;
            }

            return rtn;
        }
        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="whereSql"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int Delete(string whereSql, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            sql.Length = 0;
            try
            {
                sql.Append("DELETE FROM TCHECKITEM");

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
        /// <param name="%pkname%">主键编号</param>
        /// <returns></returns>
        public int DeleteByPrimaryKey(string mainkey, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            sql.Length = 0;
            try
            {
                sql.Append("DELETE FROM TCHECKITEM");
                sql.AppendFormat(" WHERE SYSCODE='{0}'", mainkey);
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;

                rtn = 1;
            }
            catch (Exception e)
            {
                //Log.WriteLog("通过主键删除clsCheckItem",e);
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
            DataTable dtbl = null;
            sql.Length = 0;
            try
            {

                if (type == 0)
                {
                    sql.Append("SELECT SYSCODE,STDCODE,ITEMDES,CHECKTYPE,");
                    sql.Append("StandardCode,StandardValue,Unit,DemarcateInfo,");
                    sql.Append("TestValue,OperateHelp,CheckLevel,IsReadOnly,IsLock,Remark");
                    sql.Append(" FROM TCHECKITEM");
                }
                else if (type == 1)
                {
                    sql.Append("SELECT ItemDes,StdCode,SysCode FROM tcheckitem");//SysCode
                }
                else if (type == 11)
                {
                    sql.Append("SELECT ItemDes,SysCode FROM tcheckitem");//SysCode
                }
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
                string[] names = new string[1] { "CheckItem" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["CheckItem"];
                sql.Length = 0;
            }
            catch (Exception e)
            {
                // Log.WriteLog("查询clsCheckItem",e);
                errMsg = e.Message;
            }

            return dtbl;
        }

        /// <summary>
        /// 联合查询检测标准
        /// </summary>
        /// <param name="whereSql"></param>
        /// <param name="orderBySql"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable GetAsDataTable1(string whereSql, string orderBySql, int type)
        {
            string errMsg = string.Empty;
            DataTable dtbl = null;
            sql.Length = 0;
            try
            {
                //string selectSql=string.Empty;
                if (type == 0)
                {
                    sql.Append("SELECT A.SysCode, A.StdCode, A.ItemDes, A.CheckType, B.StdDes, A.StandardValue,");
                    sql.Append("A.Unit, A.DemarcateInfo, A.TestValue, A.OperateHelp, A.CheckLevel,");
                    sql.Append("A.IsReadOnly, A.IsLock, A.Remark, A.StandardCode");
                    sql.Append(" FROM tCheckItem As A Left Join tStandard As B On A.StandardCode=B.SysCode");
                }
                else if (type == 1)
                {
                    sql.Append("SELECT ITEMDES,STDCODE,SYSCODE FROM TCHECKITEM");
                }

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
                string[] names = new string[1] { "CheckItem" };
                //if(!whereSql.Equals(""))
                //{
                //    sb.Append( " where " + whereSql;
                //}
                //if(!orderBySql.Equals(""))
                //{
                //    selectSql+= " order by " + orderBySql;
                //}
                //string[] sCmd=new string[1];
                //sCmd[0]=selectSql;
                //string[] sNames=new string[1];
                //sNames[0]="CheckItem";
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["CheckItem"];
                sql.Length = 0;
            }
            catch (Exception e)
            {
                //Log.WriteLog("查询clsCheckItem",e);
                errMsg = e.Message;
            }

            return dtbl;
        }

        /// <summary>
        /// 插入一条明细记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(clsCheckItem model, out string sErrMsg)
        {
            sErrMsg = string.Empty;
            int rtn = 0;
            sql.Length = 0;
            try
            {
                sql.Append("INSERT INTO tCheckItem");
                sql.Append("(SysCode,StdCode,ItemDes,CheckType,StandardCode,StandardValue,Unit,");
                sql.Append("DemarcateInfo,TestValue,OperateHelp,CheckLevel,IsReadOnly,IsLock,Remark)");
                sql.Append(" VALUES(");
                sql.AppendFormat("'{0}',", model.SysCode);
                sql.AppendFormat("'{0}',", model.StdCode);
                sql.AppendFormat("'{0}',", model.ItemDes);
                sql.AppendFormat("'{0}',", model.CheckType);
                sql.AppendFormat("'{0}',", model.StandardCode);
                sql.AppendFormat("'{0}',", model.StandardValue);
                sql.AppendFormat("'{0}',", model.Unit);
                sql.AppendFormat("'{0}',", model.DemarcateInfo);
                sql.AppendFormat("'{0}',", model.TestValue);
                sql.AppendFormat("'{0}',", model.OperateHelp);
                sql.AppendFormat("'{0}',", model.CheckLevel);
                sql.AppendFormat("{0},", model.IsReadOnly);
                sql.AppendFormat("{0},", model.IsLock);
                sql.AppendFormat("'{0}'", model.Remark);
                sql.Append(")");
                DataBase.ExecuteCommand(sql.ToString(), out sErrMsg);
                sql.Length = 0;

                rtn = 1;
            }
            catch (Exception e)
            {
                //Log.WriteLog("添加clsCheckItem",e);
                sErrMsg = e.Message;
            }

            return rtn;
        }


        /// <summary>
        /// 获取最大记录号
        /// </summary>
        /// <param name="code"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int GetMaxNO(string code, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn;
            try
            {
                string sql = "SELECT SYSCODE FROM TCHECKITEM ORDER BY SYSCODE DESC";
                Object obj = DataBase.GetOneValue(sql, out errMsg);
                if (obj == null || obj == DBNull.Value)
                {
                    rtn = 0;
                }
                else
                {
                    rtn = Convert.ToInt32(obj.ToString());
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
        /// 标识是否可以删除
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool CanDelete(string code)
        {
            string errMsg = string.Empty;

            try
            {
                string sql = string.Format("SELECT syscode FROM tCheckItem where tCheckItem.syscode='{0}'", code);
                //string sql = string.Format("SELECT tCheckItem.syscode from tCheckItem,tResultItem where tCheckItem.SysCode=tResultItem.CheckItemCode and tCheckItem.syscode='{0}' order by tCheckItem.syscode desc", code);
                Object obj = DataBase.GetOneValue(sql, out errMsg);
                if (obj != null || obj != DBNull.Value)
                {
                    return false;
                }

                //sql = string.Format("SELECT tCheckItem.syscode from tCheckItem,tChkTmpContent where tCheckItem.SysCode=tChkTmpContent.CheckItemCode and tCheckItem.syscode='{0}' order by tCheckItem.syscode desc", code);
                // Object obj2 = DataBase.GetOneValue(sql, out sErrMsg);

                //if (obj2 != null || obj2 != DBNull.Value)
                //{
                //    return false;
                //}

                return true;
                //if (obj == null && obj2 == null)
                //{
                //    return true;
                //}
                //else
                //{
                //    return false;
                //}
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return false;
            }
        }

        /// <summary>
        /// 通过代码获取名称
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetNameFromCode(string code)
        {
            string errMsg = string.Empty;
            if (code.Equals(""))
            {
                return string.Empty;
            }
            try
            {
                string sql = string.Format("SELECT ITEMDES FROM TCHECKITEM WHERE syscode='{0}'order by syscode", code);
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
        /// 通过代码获取单位
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetUnitFromCode(string code)
        {
            string errMsg = string.Empty;
            if (code.Equals(""))
            {
                return string.Empty;
            }

            try
            {
                string sql = string.Format("SELECT UNIT FROM TCHECKITEM WHERE SYSCODE='{0}' ORDER BY SYSCODE", code);
                Object obj = DataBase.GetOneValue(sql, out errMsg);
                if (obj == null)
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
                return null;
            }
        }

        /// <summary>
        /// 获取标准代码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetStandardCode(string code)
        {
            string errMsg = string.Empty;
            if (code.Equals(""))
            {
                return string.Empty;
            }

            try
            {
                string sql = string.Format("SELECT STANDARDCODE FROM TCHECKITEM WHERE SYSCODE='{0}' ORDER BY SYSCODE", code);
                Object obj = DataBase.GetOneValue(sql, out errMsg);
                if (obj == null)
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
                return null;
            }
        }
    }
}

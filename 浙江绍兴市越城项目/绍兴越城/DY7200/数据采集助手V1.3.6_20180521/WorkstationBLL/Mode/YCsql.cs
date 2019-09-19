using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using WorkstationDAL.Basic;
using WorkstationDAL.yc;

namespace WorkstationBLL.Mode
{
    public class YCsql
    {
        StringBuilder sb = new StringBuilder();
        public int deleteItem(string where, string order, out string errMsg)
        {
            errMsg = "";
            int rtn = 0;
            sb.Length = 0;
            try
            {
                sb.Append("delete from ycCheckItem ");

                if (where.Length > 0)
                {
                    sb.AppendFormat("where {0}", where);
                }

                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception ex)
            {
                rtn = 0;
            }
            return rtn;
        }
        public int InsertItem(Items model, out string errMsg)
        {
            errMsg = "";
            int rtn = 0;
            sb.Length = 0;
            try
            {
                sb.Append("insert into ycCheckItem(itemName,itemCode,itemType) values(");
                sb.AppendFormat("'{0}',", model.itemName);
                sb.AppendFormat("'{0}',", model.itemCode);
                sb.AppendFormat("'{0}')", model.itemType);

                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception ex)
            {
                rtn = 0;
            }
            return rtn;
        }
        public int deleteSample(string where, string order, out string errMsg)
        {
            errMsg = "";
            int rtn = 0;
            sb.Length = 0;
            try
            {
                sb.Append("delete from ycSample ");

                if (where.Length > 0)
                {
                    sb.AppendFormat("where {0}", where);
                }

                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception ex)
            {
                rtn = 0;
            }
            return rtn;
        }
        public int InsertSample(samples model, out string errMsg)
        {
            errMsg = "";
            int rtn = 0;
            sb.Length = 0;
            try
            {
                sb.Append("insert into ycSample(goodsCode,goodsName,goodsType) values(");
                sb.AppendFormat("'{0}',", model.goodsCode);
                sb.AppendFormat("'{0}',", model.goodsName);
                sb.AppendFormat("'{0}')", model.goodsType);

                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception ex)
            {
                rtn = 0;
            }
            return rtn;
        }
        public DataTable GetSample(string where ,string order,out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dt = null;
            sb.Length = 0;
            try
            {
                sb.Append("SELECT * FROM ycSample");
                if (!where.Equals(string.Empty))
                {
                    sb.Append(" WHERE ");
                    sb.Append(where);
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "ycSample" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["ycSample"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
        }
        public int InsertOpertor(Operates model, string marketcode, string marketname, out string errMsg)
        {
            errMsg = "";
            int rtn = 0;
            sb.Length = 0;
            try
            {
                sb.Append("insert into ycOperate(stallNo,operatorCode,operatorName,socialCreditCode,marketName,marketCode) values(");
                sb.AppendFormat("'{0}',", model.stallNo);
                sb.AppendFormat("'{0}',", model.operatorCode);
                sb.AppendFormat("'{0}',", model.operatorName);
                sb.AppendFormat("'{0}',", model.socialCreditCode);
                sb.AppendFormat("'{0}',", marketname);
                sb.AppendFormat("'{0}')", marketcode);

                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception ex)
            {
                rtn = 0;
            }
            return rtn;
        }
        public int deleteOpertor(string where, string order, out string errMsg)
        {
            errMsg = "";
            int rtn = 0;
            sb.Length = 0;
            try
            {
                sb.Append("delete from ycOperate ");

                if (where.Length > 0)
                {
                    sb.AppendFormat("where {0}", where);
                }

                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception ex)
            {
                rtn = 0;
            }
            return rtn;
        }
        /// <summary>
        /// 查询越城检测项目
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DataTable GetYCItemTable(string name)
        {
            string errMsg = string.Empty;
            DataTable dt = null;
            sb.Length = 0;
            try
            {
                sb.Append("SELECT * FROM ycCheckItem");
                if (!name.Equals(string.Empty))
                {
                    sb.Append(" WHERE ");
                    sb.Append(name);
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "ycCheckItem" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["ycCheckItem"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
        }
        /// <summary>
        /// 查询越城被检单位
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DataTable GetYCCompanyTable(string name)
        {
            string errMsg = string.Empty;
            DataTable dt = null;
            sb.Length = 0;
            try
            {
                sb.Append("SELECT * FROM ycMarket");
                if (!name.Equals(string.Empty))
                {
                    sb.Append(" WHERE ");
                    sb.Append(name);
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "ycMarket" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["ycMarket"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
        }
        /// <summary>
        /// 查询越城经营户
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DataTable GetYCOpertorTable(string name)
        {
            string errMsg = string.Empty;
            DataTable dt = null;
            sb.Length = 0;
            try
            {
                sb.Append("SELECT * FROM ycOperate");
                if (!name.Equals(string.Empty))
                {
                    sb.Append(" WHERE ");
                    sb.Append(name);
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "ycOperate" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["ycOperate"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
        }
        public DataTable GetYCMarket(string name)
        {
            string errMsg = string.Empty;
            DataTable dt = null;
            sb.Length = 0;
            try
            {
                sb.Append("SELECT * FROM ycMarket");
                if (!name.Equals(string.Empty))
                {
                    sb.Append(" WHERE ");
                    sb.Append(name);
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "ycMarket" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["ycMarket"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
        }

        public int deletemarket(string where, string order, out string errMsg)
        {
            errMsg = "";
            int rtn = 0;
            sb.Length = 0;
            try
            {
                sb.Append("delete from ycMarket ");

                if (where.Length > 0)
                {
                    sb.AppendFormat("where {0}", where);
                }

                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception ex)
            {
                rtn = 0;
            }
            return rtn;
        }
        public int InsertMarket(marketInfo model, out string errMsg)
        {
            errMsg = "";
            int rtn = 0;
            sb.Length = 0;
            try
            {
                sb.Append("insert into ycMarket(marketName,marketCode) values(");
                sb.AppendFormat("'{0}',", model.marketName);
                sb.AppendFormat("'{0}')", model.marketCode);

                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception ex)
            {
                rtn = 0;
            }
            return rtn;
        }
        public int updateOperator(int id,bool sel, out string errMsg)
        {
            errMsg = "";
            int rtn = 0;
            sb.Length = 0;
            try
            {
                sb.AppendFormat("UPDATE ycOperate SET isselect={0} ", sel);
                sb.AppendFormat(" where ID={0}",id);

                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception ex)
            {
                rtn = 0;
            }
            return rtn;
        }
    }
}

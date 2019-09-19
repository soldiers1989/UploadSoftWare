using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DYSeriesDataSet.DataSentence
{
    public class clsDownChkItem
    {
        private StringBuilder sb = new StringBuilder();
        /// <summary>
        /// 插入新的检测项目
        /// </summary>
        /// <param name="name"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public int InDownItem(int id, string name, string code, out string errMsg)
        {
            int rtn = 0;
            errMsg = "";
            try
            {
                sb.Clear();
                sb.Length = 0;
                sb.Append("Insert Into ZJCheckItem (ID,itemName,itemCode) values(");
                sb.Append(id);
                sb.Append(",'");
                sb.Append(name);
                sb.Append("','");
                sb.Append(code);
                sb.Append("')");
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return rtn;
        }
        /// <summary>
        /// 查询检测项目
        /// </summary>
        /// <returns></returns>
        public DataTable GetDownItem(string where, string orderby, out string errMsg)
        {
            DataTable dt = null;
            errMsg = "";
            try
            {
                sb.Length = 0;
                sb.Append("select ID,itemName,itemCode from ZJCheckItem");
                if (where != "")
                {
                    sb.Append(" where ");
                    sb.Append(where);
                }
                if (orderby != "")
                {
                    sb.Append(orderby);
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "ZJCheckitem" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["ZJCheckitem"];
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dt;

        }
        /// <summary>
        /// 删除检测项目表内容
        /// </summary>
        /// <returns></returns>
        public int DelDownItem(out string errMsg)
        {
            int rtn = 0;
            errMsg = "";
            try
            {
                sb.Length = 0;
                sb.AppendFormat("Delete From ZJCheckItem");
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return rtn;

        }
    }
}

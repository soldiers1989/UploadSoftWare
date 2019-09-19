using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DYSeriesDataSet
{
    /// <summary>
    /// 孔位设置
    /// </summary>
    public class HolesSetingOpr
    {
        /// <summary>
        /// 数据插入
        /// </summary>
        /// <param name="model">实体类HolesSetting对象</param>
        /// <returns></returns>
        public bool Insert(HolesSetting model)
        {
            StringBuilder sb = new StringBuilder();//insert into values
            sb.Append("INSERT INTO tHolesSetting(checkItemId,HolesIndex,IsShowOnData,IsDouble,MachineCode)VALUES(");
            sb.AppendFormat("'{0}',", model.CheckItemId);
            sb.AppendFormat("'{0}',", model.HolesIndex);
            sb.AppendFormat("{0},", model.IsShowOnData);
            sb.AppendFormat("{0},", model.IsDouble);
            sb.AppendFormat("'{0}'", model.MachineCode);
            sb.Append(")");
            return DataBase.ExecuteCommand(sb.ToString());
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="model">实体类HolesSetting对象</param>
        /// <returns></returns>
        public bool Update(HolesSetting model)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE FROM tHolesSetting SET ");
            sb.AppendFormat("checkItemId='{0}'", model.CheckItemId);
            sb.AppendFormat(",HolesIndex='{0}'", model.HolesIndex);
            sb.AppendFormat(",IsShowOnData={0}", model.IsShowOnData);
            sb.AppendFormat(",IsDouble={0}", model.IsDouble);
            sb.AppendFormat(",MachineCode='{0}'", model.MachineCode);
            return DataBase.ExecuteCommand(sb.ToString());
        }

        /// <summary>
        /// 删除某个编号记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM tHolesSetting WHERE Id=");
            sb.Append(id);
            return DataBase.ExecuteCommand(sb.ToString());
        }

        /// <summary>
        /// 删除符某些条件的记录
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public bool Delete(string strWhere)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM tHolesSetting ");
            if (strWhere != null && strWhere.Length > 0)
            {
                sb.Append(" WHERE ");
                sb.Append(strWhere);
            }
            return DataBase.ExecuteCommand(sb.ToString());
        }

        /// <summary>
        /// 检测是否存某个条件的记录
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public bool IsExist(string strWhere)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT COUNT(1) FROM tHolesSetting ");
            if (strWhere != null && strWhere.Length > 0)
            {
                sb.Append(" WHERE ");
                sb.Append(strWhere);
            }
            string errMsg = string.Empty;
            object obj = DataBase.GetOneValue(sb.ToString(), out errMsg);
            if (obj != null&&obj!=DBNull.Value)
            {
                return ((int)obj) > 0;
            }
            return false;
        }

        /// <summary>
        /// 获取指列的DataTable结果集
        /// </summary>
        /// <param name="top">前top条记录</param>
        /// <param name="strWhere">过滤条件</param>
        /// <param name="columList">列</param>
        /// <returns></returns>
        public DataTable GetDataTable(int top, string strWhere, params string[] columList)
        {
            //StringBuilder sb = new StringBuilder();
            //sb.Append("SELECT checkItemId,HolesIndex,IsShowOnData,IsDouble FROM tHolesSetting ");
            //if (strWhere != null && strWhere.Length > 0)
            //{
            //    sb.Append(" WHERE ");
            //    sb.Append(strWhere);
            //}

            return DataBase.GetColumnList("tHolesSetting", top, strWhere, columList);
        }
    }
}

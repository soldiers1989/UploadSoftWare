using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace DYSeriesDataSet
{
    public class clsttStandardDecideOpr
    {

        public clsttStandardDecideOpr()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        private StringBuilder sql = new StringBuilder();

        /// <summary>
        /// 部分修改保存
        /// </summary>
        /// <param name="model">对象clsttStandardDecide的一个实例参数</param>
        /// <returns></returns>
        //public int UpdatePart(clsttStandardDecide model, out string errMsg)
        //{
        //    errMsg = string.Empty;
        //    int rtn = 0;
        //    try
        //    {
        //        sb.Length = 0;
        //        sb.Append("UPDATE ttStandDecide SET");
        //        sb.AppendFormat(" TypeName='{0}'", model.TypeName);
        //        sb.AppendFormat(",NameCall='{0}'", model.NameCall);
        //        sb.AppendFormat(",AreaCall='{0}'", model.AreaCall);
        //        sb.AppendFormat(",VerType='{0}'", model.VerType);
        //        sb.AppendFormat(",IsReadOnly={0}", model.IsReadOnly);
        //        sb.AppendFormat(",IsLock={0}", model.IsLock);
        //        sb.AppendFormat(",ComKind='{0}'", model.ComKind);
        //        sb.AppendFormat(",AreaTitle='{0}'", model.AreaTitle);
        //        sb.AppendFormat(",NameTitle='{0}'", model.NameTitle);
        //        sb.AppendFormat(",DomainTitle='{0}'", model.DomainTitle);
        //        sb.AppendFormat(",SampleTitle='{0}'", model.SampleTitle);
        //        sb.Append(" Where ID=");
        //        sb.Append(model.ID);
        //        DataBase.ExecuteCommand(sb.ToString(), out errMsg);
        //        sb.Length = 0;
        //        rtn = 1;
        //    }
        //    catch (Exception e)
        //    {
        //        errMsg = e.Message;
        //    }
        //    return rtn;
        //}


        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="errMsg">输出错误信息</param>
        /// <returns></returns>
        public int Delete(string strWhere, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sql.Length = 0;
                sql.Append("Delete From ttStandDecide ");
                if (strWhere != string.Empty)
                {
                    sql.Append(" Where ");
                    sql.Append(strWhere);
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
        public int DeleteByPrimaryKey(string mainkey, out string sErrMsg)
        {
            sErrMsg = string.Empty;
            int rtn = 0;
            try
            {
                string deleteSql = string.Format("Delete From ttStandDecide Where FtypeNmae={0}", mainkey);
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
        /// 根据ID删除样品
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
                string deleteSql = string.Format("Delete From ttStandDecide Where ID={0}", ID);
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
                string selectSql = string.Empty;
                if (type == 0)
                {
                    sql.Append("Select FtypeNmae,Name,ItemDes,StandardValue,Demarcate,Unit From ttStandDecide ");
                }
                else if (type == 1)
                {
                    sql.Append("Select FtypeNmae,SampleNum,Name,ItemDes,StandardValue,Demarcate,Unit From ttStandDecide ");
                }
                else if (type == 2)//获取样品标准库的最后更新时间
                {
                    sql.Append("Select Top 1 * From ttStandDecide Order By UDate Desc");
                }
                else if (type == 3)//获取检测项目的最后更新时间
                {
                    sql.Append("Select Top 1 * From tCheckItem Order By UDate Desc");
                }
                else if (type == 4)//获取检测标准的最后更新时间
                {
                    sql.Append("Select Top 1 * From tStandard Order By UDate Desc");
                }
                else if (type == 5)
                {
                    //sql.Append("select IIF(s.use_default = '0',s.detect_sign,item.detect_sign) as detect_sign,IIF(s.use_default = '0',s.detect_value_unit,item.detect_value_unit) as detect_value_unit,IIF(s.use_default = '0',s.detect_value,item.detect_value) as detect_value,");
                    //sql.Append("d.std_code,f.food_name,item.detect_item_name from SampleStandard s,DetectItem item,foodlist f,StandardList d");

                    sql.Append("select IIF(s.use_default = '0',s.detect_sign,item.detect_sign) as detect_sign,IIF(s.use_default = '0',s.detect_value_unit,item.detect_value_unit) as detect_value_unit,IIF(s.use_default = '0',s.detect_value,item.detect_value) as detect_value,");
                    sql.Append("f.food_name,item.detect_item_name,item.standard_id from SampleStandard s,DetectItem item,foodlist f");
                }
                else if (type == 6)//查询父ID
                {
                    sql.Append("select parent_id from foodlist ");
                }
                else if (type == 7)//根据父ID查询数据
                {
                    sql.Append("select IIF(s.use_default = '1',s.detect_sign,item.detect_sign) as detect_sign,IIF(s.use_default = '1',s.detect_value_unit,item.detect_value_unit) as detect_value_unit,IIF(s.use_default ='1',");
                    sql.Append("s.detect_value,item.detect_value) as detect_value,d.std_code,f.food_name,item.detect_item_name from SampleStandard s,DetectItem item,foodlist f,StandardList d ");
                }
                else if (type == 8)
                {
                    sql.Append("select s.std_code, d.detect_item_name,d.detect_sign,d.detect_value,d.detect_value_unit from DetectItem d,StandardList s ");
                }
                else if (type == 9)
                {
                    sql.Append("select d.standard_id as standard_id,d.detect_sign as detect_sign,d.detect_value as detect_value,d.detect_value_unit as detect_value_unit,s.std_code as std_code from DetectItem d,StandardList s ");
                }
                else if (type == 10)
                {
                    sql.Append("select * from StandardList ");
                }

                if (!whereSql.Equals(""))
                {
                    sql.Append(" Where ");
                    sql.Append(whereSql);
                }
                if (!orderBySql.Equals(""))
                {
                    sql.Append(" Order By ");
                    sql.Append(orderBySql);
                }
                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "StandDecide" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["StandDecide"];
                sql.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dtbl;
        }

        /// <summary>
        /// 插入||修改一条明细记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(clsttStandardDecide model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                if (model.ID > 0)
                {
                    sql.Length = 0;
                    sql.Append("Update ttStandDecide Set FtypeNmae = '" + model.FtypeNmae);
                    sql.Append("',SampleNum = '" + model.SampleNum);
                    sql.Append("',Name = '" + model.Name);
                    sql.Append("',ItemDes = '" + model.ItemDes);
                    sql.Append("',StandardValue = '" + model.StandardValue);
                    sql.Append("',Demarcate = '" + model.Demarcate);
                    sql.Append("',Unit = '" + model.Unit);
                    sql.Append("' Where ID = " + model.ID);
                }
                else
                {
                    sql.Length = 0;
                    sql.Append("Insert Into ttStandDecide(FtypeNmae,SampleNum,Name,ItemDes,StandardValue,Demarcate,Unit)Values('");
                    sql.Append(model.FtypeNmae);
                    sql.Append("','");
                    sql.Append(model.SampleNum);
                    sql.Append("','");
                    sql.Append(model.Name);
                    sql.Append("','");
                    sql.Append(model.ItemDes);
                    sql.Append("','");
                    sql.Append(model.StandardValue);
                    sql.Append("','");
                    sql.Append(model.Demarcate);
                    sql.Append("','");
                    sql.Append(model.Unit);
                    sql.Append("')");
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
        /// 插入||修改 样品和检测项目对应标准
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int InsertOrUpdate(clsttStandardDecide model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sql.Length = 0;
                DataTable dataTable = new clsTaskOpr().GetSampleByNameOrCode(model.SampleNum, "", true, false, 2);
                //List<clsttStandardDecide> listModel = (List<clsttStandardDecide>)DataTableToIList<clsttStandardDecide>(dataTable, 1);
                //if (dataTable.Rows.Count > 0)
                //{
                //    strB.Append("Update ttStandDecide Set FtypeNmae = '" + model.FtypeNmae);
                //    strB.Append("',SampleNum = '" + model.SampleNum);
                //    strB.Append("',Name = '" + model.Name);
                //    strB.Append("',ItemDes = '" + model.ItemDes);
                //    strB.Append("',StandardValue = '" + model.StandardValue);
                //    strB.Append("',Demarcate = '" + model.Demarcate);
                //    strB.Append("',Unit = '" + model.Unit);
                //    strB.Append("',UDate = '" + model.UDate);
                //    strB.Append("' Where SampleNum = '" + model.SampleNum + "'");
                //}
                //else
                //{
                sql.Append("Insert Into ttStandDecide(FtypeNmae,SampleNum,Name,ItemDes,StandardValue,Demarcate,Unit,UDate)Values('");
                sql.Append(model.FtypeNmae);
                sql.Append("','");
                sql.Append(model.SampleNum);
                sql.Append("','");
                sql.Append(model.Name);
                sql.Append("','");
                sql.Append(model.ItemDes);
                sql.Append("','");
                sql.Append(model.StandardValue);
                sql.Append("','");
                sql.Append(model.Demarcate);
                sql.Append("','");
                sql.Append(model.Unit);
                sql.Append("','");
                sql.Append(model.UDate);
                sql.Append("')");
                //}
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

        public int Update(clsttStandardDecide model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sql.Length = 0;
                sql.Append("Insert Into ttStandDecide(FtypeNmae,SampleNum,Name,ItemDes,StandardValue,Demarcate,Unit)Values('");
                sql.Append(model.FtypeNmae);
                sql.Append("','");
                sql.Append(model.SampleNum);
                sql.Append("','");
                sql.Append(model.Name);
                sql.Append("','");
                sql.Append(model.ItemDes);
                sql.Append("','");
                sql.Append(model.StandardValue);
                sql.Append("','");
                sql.Append(model.Demarcate);
                sql.Append("','");
                sql.Append(model.Unit);
                sql.Append("')");
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace DataSetModel
{
    public class clsttStandardDecideOpr
    {

        public clsttStandardDecideOpr()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        private StringBuilder strB = new StringBuilder();

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
                strB.Length = 0;
                strB.Append("Delete From ttStandDecide ");
                if (strWhere != string.Empty)
                {
                    strB.Append(" Where ");
                    strB.Append(strWhere);
                }
                DataBase.ExecuteCommand(strB.ToString(), out errMsg);
                strB.Length = 0;

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
            strB.Length = 0;
            try
            {
                string selectSql = string.Empty;
                if (type == 0)
                {
                    strB.Append("Select FtypeNmae,Name,ItemDes,StandardValue,Demarcate,Unit From ttStandDecide ");
                }
                else if (type == 1)
                {
                    strB.Append("Select FtypeNmae,SampleNum,Name,ItemDes,StandardValue,Demarcate,Unit From ttStandDecide ");
                }
                else if (type == 2)//获取样品标准库的最后更新时间
                {
                    strB.Append("Select Top 1 * From ttStandDecide Order By UDate Desc");
                }
                else if (type == 3)//获取检测项目的最后更新时间
                {
                    strB.Append("Select Top 1 * From tCheckItem Order By UDate Desc");
                }
                else if (type == 4)//获取检测标准的最后更新时间
                {
                    strB.Append("Select Top 1 * From tStandard Order By UDate Desc");
                }
                else if (type == 5)
                {
                    strB.Append("select IIF(s.use_default = '0',s.detect_sign,item.detect_sign) as detect_sign,IIF(s.use_default = '0',s.detect_value_unit,item.detect_value_unit) as detect_value_unit,IIF(s.use_default = '0',s.detect_value,item.detect_value) as detect_value,");
                    strB.Append("d.std_code,f.food_name,item.detect_item_name from SampleStandard s,DetectItem item,foodlist f,StandardList d");
                }
                else if (type == 6)//查询父ID
                {
                    strB.Append("select parent_id from foodlist ");
                }
                else if (type == 7)//根据父ID查询数据
                {
                    strB.Append("select IIF(s.use_default = '1',s.detect_sign,item.detect_sign) as detect_sign,IIF(s.use_default = '1',s.detect_value_unit,item.detect_value_unit) as detect_value_unit,IIF(s.use_default ='1',");
                    strB.Append("s.detect_value,item.detect_value) as detect_value,d.std_code,f.food_name,item.detect_item_name from SampleStandard s,DetectItem item,foodlist f,StandardList d ");
                }
                else if (type == 8)
                {
                    strB.Append("select s.std_code, d.detect_item_name,d.detect_sign,d.detect_value,d.detect_value_unit from DetectItem d,StandardList s ");
                }

                if (!whereSql.Equals(string.Empty))
                {
                    strB.Append(" Where ");
                    strB.Append(whereSql);
                }
                if (!orderBySql.Equals(string.Empty))
                {
                    strB.Append(" Order By ");
                    strB.Append(orderBySql);
                }
                string[] cmd = new string[1] { strB.ToString() };
                string[] names = new string[1] { "StandDecide" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["StandDecide"];
                strB.Length = 0;
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
                    strB.Length = 0;
                    strB.Append("Update ttStandDecide Set FtypeNmae = '" + model.FtypeNmae);
                    strB.Append("',SampleNum = '" + model.SampleNum);
                    strB.Append("',Name = '" + model.Name);
                    strB.Append("',ItemDes = '" + model.ItemDes);
                    strB.Append("',StandardValue = '" + model.StandardValue);
                    strB.Append("',Demarcate = '" + model.Demarcate);
                    strB.Append("',Unit = '" + model.Unit);
                    strB.Append("' Where ID = " + model.ID);
                }
                else
                {
                    strB.Length = 0;
                    strB.Append("Insert Into ttStandDecide(FtypeNmae,SampleNum,Name,ItemDes,StandardValue,Demarcate,Unit)Values('");
                    strB.Append(model.FtypeNmae);
                    strB.Append("','");
                    strB.Append(model.SampleNum);
                    strB.Append("','");
                    strB.Append(model.Name);
                    strB.Append("','");
                    strB.Append(model.ItemDes);
                    strB.Append("','");
                    strB.Append(model.StandardValue);
                    strB.Append("','");
                    strB.Append(model.Demarcate);
                    strB.Append("','");
                    strB.Append(model.Unit);
                    strB.Append("')");
                }
                DataBase.ExecuteCommand(strB.ToString(), out errMsg);
                strB.Length = 0;
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
                strB.Length = 0;
                DataTable dataTable = new clsTaskOpr().GetSampleByNameOrCode(model.SampleNum, string.Empty, true, false, 2);
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
                strB.Append("Insert Into ttStandDecide(FtypeNmae,SampleNum,Name,ItemDes,StandardValue,Demarcate,Unit,UDate)Values('");
                strB.Append(model.FtypeNmae);
                strB.Append("','");
                strB.Append(model.SampleNum);
                strB.Append("','");
                strB.Append(model.Name);
                strB.Append("','");
                strB.Append(model.ItemDes);
                strB.Append("','");
                strB.Append(model.StandardValue);
                strB.Append("','");
                strB.Append(model.Demarcate);
                strB.Append("','");
                strB.Append(model.Unit);
                strB.Append("','");
                strB.Append(model.UDate);
                strB.Append("')");
                //}
                DataBase.ExecuteCommand(strB.ToString(), out errMsg);
                strB.Length = 0;
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
                        if (!value.ToString().Equals(string.Empty))
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
                strB.Length = 0;
                strB.Append("Insert Into ttStandDecide(FtypeNmae,SampleNum,Name,ItemDes,StandardValue,Demarcate,Unit)Values('");
                strB.Append(model.FtypeNmae);
                strB.Append("','");
                strB.Append(model.SampleNum);
                strB.Append("','");
                strB.Append(model.Name);
                strB.Append("','");
                strB.Append(model.ItemDes);
                strB.Append("','");
                strB.Append(model.StandardValue);
                strB.Append("','");
                strB.Append(model.Demarcate);
                strB.Append("','");
                strB.Append(model.Unit);
                strB.Append("')");
                DataBase.ExecuteCommand(strB.ToString(), out errMsg);
                strB.Length = 0;
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

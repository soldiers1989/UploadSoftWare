using System;
using System.Data;
using System.Text;

namespace DYSeriesDataSet.KjService
{
    /// <summary>
    /// Note：快检服务中心-检测任务数据库操作帮助类
    /// Creater：wenj
    /// Time：2018/5/13 15:09:56
    /// Company：食安科技
    /// Web Site：http://www.chinafst.cn/
    /// Version：V1.0.0
    /// </summary>
    public static class KjTasksOpr
    {
        private static StringBuilder sql = new StringBuilder();

        public static int Insert(DyInterfaceHelper.KjService.DownloadSamplingEntity.ResultItem model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sql.Length = 0;
                sql.Append("Insert Into KJ_Tasks");
                sql.Append("(id,sampling_id,sample_code,food_id,food_name,sample_number,purchase_amount,sample_date,purchase_date,item_id,item_name,");
                sql.Append("origin,supplier,supplier_address,supplier_person,supplier_phone,batch_number,status,recevie_device,ope_shop_name,remark,");
                sql.Append("s_reg_id,s_reg_name,s_ope_shop_code,s_ope_shop_name,t_id,t_task_title,td_id,isReceive,resultType) ");
                sql.Append("VALUES(");
                sql.AppendFormat("'{0}',", model.id);
                sql.AppendFormat("'{0}',", model.sampling_id);
                sql.AppendFormat("'{0}',", model.sample_code);
                sql.AppendFormat("'{0}',", model.food_id);
                sql.AppendFormat("'{0}',", model.food_name);
                sql.AppendFormat("'{0}',", model.sample_number);
                sql.AppendFormat("'{0}',", model.purchase_amount);
                sql.AppendFormat("'{0}',", model.sample_date);
                sql.AppendFormat("'{0}',", model.purchase_date);
                sql.AppendFormat("'{0}',", model.item_id);
                sql.AppendFormat("'{0}',", model.item_name);

                sql.AppendFormat("'{0}',", model.origin);
                sql.AppendFormat("'{0}',", model.supplier);
                sql.AppendFormat("'{0}',", model.supplier_address);
                sql.AppendFormat("'{0}',", model.supplier_person);
                sql.AppendFormat("'{0}',", model.supplier_phone);
                sql.AppendFormat("'{0}',", model.batch_number);
                sql.AppendFormat("'{0}',", model.status);
                sql.AppendFormat("'{0}',", model.recevie_device);
                sql.AppendFormat("'{0}',", model.ope_shop_name);
                sql.AppendFormat("'{0}',", model.remark);

                sql.AppendFormat("'{0}',", model.s_reg_id);
                sql.AppendFormat("'{0}',", model.s_reg_name);
                sql.AppendFormat("'{0}',", model.s_ope_shop_code);
                sql.AppendFormat("'{0}',", model.s_ope_shop_name);
                sql.AppendFormat("'{0}',", model.t_id);
                sql.AppendFormat("'{0}',", model.t_task_title);
                sql.AppendFormat("'{0}',", model.td_id);
                sql.AppendFormat("{0},", model.isReceive);
                sql.AppendFormat("'{0}'", model.resultType);
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

        public static int Update(DyInterfaceHelper.KjService.DownloadSamplingEntity.ResultItem model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sql.Length = 0;
                sql.Append("Update KJ_Tasks set ");
                sql.AppendFormat("sampling_id='{0}',sample_code='{1}',food_id='{2}',food_name='{3}',sample_number='{4}',",
                    model.sampling_id, model.sample_code, model.food_id, model.food_name, model.sample_number);
                sql.AppendFormat("purchase_amount='{0}',sample_date='{1}',purchase_date='{2}',item_id='{3}',item_name='{4}',"
                    , model.purchase_amount, model.sample_date, model.purchase_date, model.item_id, model.item_name);
                sql.AppendFormat("origin='{0}',supplier='{1}',supplier_address='{2}',supplier_person='{3}',supplier_phone='{4}',"
                    , model.origin, model.supplier, model.supplier_address, model.supplier_person, model.supplier_phone);
                sql.AppendFormat("batch_number='{0}',status='{1}',recevie_device='{2}',ope_shop_name='{3}',remark='{4}',"
                    , model.batch_number, model.status, model.recevie_device, model.ope_shop_name, model.remark);
                sql.AppendFormat("s_reg_id='{0}',s_reg_name='{1}',s_ope_shop_code='{2}',s_ope_shop_name='{3}',t_id='{4}',"
                    , model.s_reg_id, model.s_reg_name, model.s_ope_shop_code, model.s_ope_shop_name, model.t_id);
                sql.AppendFormat("t_task_title='{0}',td_id='{1}',isReceive={2},resultType='{3}' Where id='{4}'"
                    , model.t_task_title, model.td_id, model.isReceive, model.resultType, model.id);

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
        /// 删除操作
        /// </summary>
        /// <param name="whereSql"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static int Delete(string whereSql, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                string deleteSql = "DELETE FROM KJ_Tasks";

                if (!whereSql.Equals(""))
                {
                    deleteSql += string.Format(" WHERE {0} ", whereSql);
                }
                DataBase.ExecuteCommand(deleteSql, out errMsg);
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }

        public static DataTable GetAsDataTable(out string errMsg, string whereSql = "", string orderBySql = "", int type = 0)
        {
            DataTable dtbl = null;
            errMsg = string.Empty;
            sql.Length = 0;

            try
            {
                if (type == 0)
                {
                    sql.Append("SELECT * FROM KJ_Tasks");
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
                    sql.Append(" ASC ");
                }

                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "KJ_Tasks" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["KJ_Tasks"];
                sql.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dtbl;
        }

    }
}
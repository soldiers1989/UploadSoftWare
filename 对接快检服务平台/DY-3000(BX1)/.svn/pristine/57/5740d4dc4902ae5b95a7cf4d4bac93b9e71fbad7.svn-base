using System;
using System.Data;
using System.Text;


namespace DYSeriesDataSet
{
    /// <summary>
    /// 食品类别操作类
    /// </summary>
    public class clsTaskOpr
    {
        public clsTaskOpr()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        private StringBuilder sql = new StringBuilder();
        /// <summary>
        /// 更新抽样任务记录
        /// </summary>
        /// <param name="where"></param>
        /// <param name="data"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int UpdateTask(string where, string data, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sql.Length = 0;
                sql.AppendFormat("UPDATE KTask SET mokuai='{0}'", data);
                sql.AppendFormat(" where tid='{0}'", where);
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return rtn;
        }
        /// <summary>
        /// 部分修改保存
        /// </summary>
        /// <param name="model">对象clsTask的一个实例参数</param>
        /// <returns></returns>
        public int UpdatePart(clsTask model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {

                sql.Length = 0;
                sql.AppendFormat("UPDATE tTask SET CPTITLE='{0}'", model.CPTITLE);
                //sb.AppendFormat(",Name='{0}'", model.Name);
                //sb.AppendFormat(",ShortCut='{0}'", model.ShortCut);
                //sb.AppendFormat(",CheckLevel='{0}'", model.CheckLevel);
                //sb.AppendFormat(",CheckItemCodes='{0}'", model.CheckItemCodes);
                //sb.AppendFormat(",CheckItemValue='{0}'", model.CheckItemValue);
                //sb.Append(",IsReadOnly=");
                //sb.Append(model.IsReadOnly);
                //sb.Append(",IsLock=");
                //sb.Append(model.IsLock);
                //sb.AppendFormat(",Remark='{0}'", model.Remark);
                //sb.AppendFormat(",FoodProperty='{0}'", model.FoodProperty);
                sql.AppendFormat(" WHERE CPCODE='{0}'", model.CPCODE);
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
        /// 删除操作 tTask
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
                string deleteSql = "DELETE FROM tTask";

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
                string deleteSql = string.Format("DELETE FROM tTask WHERE CPCODE='{0}' ", mainkey);
                DataBase.ExecuteCommand(deleteSql, out errMsg);

                rtn = 1;
            }
            catch (Exception e)
            {
                //Log.WriteLog("通过主键删除clsTask",e);
                errMsg = e.Message; ;
            }

            return rtn;
        }

        /// <summary>
        /// 根据样品名称|样品编号查找样品
        /// </summary>
        /// <param name="SampleName">样品名称</param>
        /// <param name="Name">项目名称</param>
        /// <param name="IsPreciseQuery">true 精确查找 | false 模糊查找</param>
        /// <returns></returns>
        public DataTable GetSampleByNameOrCode(string SampleName, string Name, bool IsPreciseQuery, bool IsFirst, int type)
        {
            string errMsg = string.Empty;
            DataTable dtbl = null;
            try
            {
                sql.Length = 0;
                if (type == 1)
                {
                    //if (IsFirst)
                    //{
                    //    sb.Append("Select Top 200 ID,FtypeNmae,SampleNum,Name,ItemDes,StandardValue,Demarcate,Unit From ttStandDecide ");
                    //}
                    //else
                    //{
                    sql.Append("Select ID,FtypeNmae,SampleNum,Name,ItemDes,StandardValue,Demarcate,Unit From ttStandDecide ");
                    //}
                    if (!SampleName.Equals("") && !Name.Equals(""))
                    {
                        sql.AppendFormat(" WHERE FtypeNmae Like '%{0}%'", SampleName);
                        sql.AppendFormat(IsPreciseQuery ? " AND Name Like '{0}'" : " AND Name Like '%{0}%'", Name);
                    }
                    else if (SampleName.Equals("") && !Name.Equals(""))
                    {
                        sql.AppendFormat(IsPreciseQuery ? " WHERE Name Like '{0}'" : " WHERE Name Like '%{0}%'", Name);
                    }
                    else if (!SampleName.Equals("") && Name.Equals(""))
                    {
                        sql.AppendFormat(" WHERE FtypeNmae Like '%{0}%'", SampleName);
                    }
                }
                else if (type == 2)
                {
                    sql.AppendFormat("Select Top 1 ID,FtypeNmae,SampleNum,Name,ItemDes,StandardValue,Demarcate,Unit,UDate From ttStandDecide Where SampleNum Like'{0}'", SampleName);
                }
                else if (type == 3)
                {
                    if (SampleName.Equals(""))
                    {
                        sql.Append("select d.cid,d.detect_item_name,s.food_id,f.food_name,sd.std_name,s.detect_value,s.detect_value_unit from DetectItem d,foodlist f,SampleStandard s,StandardList sd ");
                        sql.AppendFormat("where d.detect_item_name='{0}' and s.item_id=d.cid and s.food_id=f.fid and sd.sid=d.standard_id ", Name);
                        sql.Append("and d.checked='1' and d.delete_flag='0' and f.checked='1' and f.delete_flag='0' and s.checked='1' and s.delete_flag='0' and sd.delete_flag='0' ");
                    }
                    else if (!SampleName.Equals(""))
                    {
                        sql.Append("select d.cid,d.detect_item_name,s.food_id,f.food_name,sd.std_name,s.detect_value,s.detect_value_unit from DetectItem d,foodlist f,SampleStandard s,StandardList sd ");
                        sql.AppendFormat("where d.detect_item_name='{0}' and s.item_id=d.cid and s.food_id=f.fid and sd.sid=d.standard_id ", Name);
                        sql.AppendFormat("and d.checked='1' and d.delete_flag='0' and f.checked='1' and f.delete_flag='0' and s.checked='1' and s.delete_flag='0' and sd.delete_flag='0' and f.food_name like '%{0}%' ", SampleName);
                    }
                   
                }
                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "tStandDecide" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["tStandDecide"];
                sql.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dtbl;
        }

        /// <summary>
        /// 根据项目名称查找相关检测项目
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public DataTable GetProjectByName(string Name)
        {
            string errMsg = string.Empty;
            DataTable dtbl = null;
            try
            {
                sql.Length = 0;
                sql.Append("Select SysCode,StdCode,ItemDes,CheckType,StandardCode,StandardValue,Unit,DemarcateInfo From tCheckItem ");
                if (!Name.Equals(""))
                {
                    sql.Append(" Where ItemDes Like '%");
                    sql.Append(Name + "%'");
                }
                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "tCheckItem" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["tCheckItem"];
                sql.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dtbl;
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

            try
            {
                sql.Length = 0;

                if (type == 0)
                {
                    sql.Append("SELECT CPCODE,CPTITLE,CPSDATE,CPEDATE,CPTPROPERTY,");
                    sql.Append("CPFROM,CPEDITOR,CPPORGID,CPPORG,CPEDDATE,CPMEMO,PLANDETAIL,PLANDCOUNT FROM tTask");
                }
                else if (type == 1)
                {
                    sql.Append("SELECT * FROM tTask");
                }
                else if (type == 2)
                {
                    sql.Append("SELECT CPCODE,CPTITLE  FROM tTask");
                }
                if (type == 3)
                {
                    // SELECT CPCODE,CPTITLE,CPSDATE,CPEDATE,CPTPROPERTY,CPFROM,CPEDITOR,CPPORGID,CPPORG,CPEDDATE,PLANDETAIL,
                    //PLANDCOUNT,BAOJINGTIME,(SELECT COUNT(*) FROM ttResultSecond WHERE CheckPlanCode=m.CPCODE ) AS v30,'未完成' as finish
                    // FROM tTask as m where (cdate(BAOJINGTIME)>=#2015-10-26 22:21:23#)
                    sql.Append(" SELECT CPCODE,CPTITLE,CPSDATE,CPEDATE,CPTPROPERTY,CPFROM,CPEDITOR,CPPORGID,CPPORG,CPEDDATE,PLANDETAIL,");
                    sql.Append("PLANDCOUNT,BAOJINGTIME,(SELECT COUNT(*) FROM ttResultSecond WHERE CheckPlanCode=m.CPCODE ) AS v30,'未完成' as finish");
                    sql.Append(",(v30/PLANDCOUNT) as Num  FROM tTask as m");
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
                string[] names = new string[1] { "task" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["task"];
                sql.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dtbl;
        }

        /// <summary>
        /// 根据任务编号查询任务名称
        /// </summary>
        /// <param name="code">编号</param>
        /// <returns></returns>
        public string SearchTaskNameByCode(string code)
        {

            string errMsg = string.Empty;
            DataTable dtbl = null;

            try
            {
                sql.Length = 0;
                sql.Append("SELECT CPTITLE FROM tTask");
                if (!code.Equals(""))
                {
                    sql.Append(" WHERE CPCODE Like '%" + code + "%'");
                }
                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "task" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["task"];
                sql.Length = 0;
            }
            catch (Exception e)
            {
                //Log.WriteLog("查询clsTask",e);
                errMsg = e.Message;
            }
            return "";
        }

        /// <summary>
        /// 插入一条明细记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(clsTask model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sql.Length = 0;
                sql.Append("Insert Into tTask");
                sql.Append("(CPCODE,CPTITLE,CPSDATE,CPEDATE,CPTPROPERTY,CPFROM,CPEDITOR");
                sql.Append(",CPPORGID,CPPORG,CPEDDATE,CPMEMO,PLANDETAIL,BAOJINGTIME,PLANDCOUNT)");
                sql.Append("VALUES(");
                sql.AppendFormat("'{0}',", model.CPCODE);
                sql.AppendFormat("'{0}',", model.CPTITLE);
                sql.AppendFormat("'{0}',", model.CPSDATE);
                sql.AppendFormat("'{0}',", model.CPEDATE);
                sql.AppendFormat("'{0}',", model.CPTPROPERTY);
                sql.AppendFormat("'{0}',", model.CPFROM);
                sql.AppendFormat("'{0}',", model.CPEDITOR);
                sql.AppendFormat("'{0}',", model.CPPORGID);
                sql.AppendFormat("'{0}',", model.CPPORG);
                sql.AppendFormat("'{0}',", model.CPEDDATE);
                sql.AppendFormat("'{0}',", model.CPMEMO);
                sql.AppendFormat("'{0}',", model.PLANDETAIL);
                sql.AppendFormat("'{0}',", model.BAOJINGTIME);
                sql.AppendFormat("'{0}'", model.PLANDCOUNT);
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

        /// <summary>
        /// 任务更新 修改|新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int InsertOrUpdate(clsTask model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sql.Length = 0;
                DataTable dt = null;//new clsCompanyOpr().GetAsDataTable("CPCODE='" + model.CPCODE + "'", "", 11);
                if (dt != null && dt.Rows.Count > 0)
                {
                    sql.AppendFormat("Update tTask Set CPCODE='{0}',CPTITLE='{1}',CPSDATE='{2}',CPEDATE='{3}',CPTPROPERTY='{4}',",
                        model.CPCODE, model.CPTITLE, model.CPSDATE, model.CPEDATE, model.CPTPROPERTY);
                    sql.AppendFormat("CPFROM='{0}',CPEDITOR='{1}',CPPORGID='{2}',CPPORG='{3}',CPEDDATE='{4}',",
                        model.CPFROM, model.CPEDITOR, model.CPPORGID, model.CPPORG, model.CPEDDATE);
                    sql.AppendFormat("CPMEMO='{0}',PLANDETAIL='{1}',PLANDCOUNT='{2}',BAOJINGTIME='{3}',UDate='{4}' Where CPCODE='{5}'",
                        model.CPMEMO, model.PLANDCOUNT, model.PLANDCOUNT, model.BAOJINGTIME, model.UDate, model.CPCODE);
                }
                else
                {
                    sql.Append("Insert Into tTask");
                    sql.Append("(CPCODE,CPTITLE,CPSDATE,CPEDATE,CPTPROPERTY,CPFROM,CPEDITOR");
                    sql.Append(",CPPORGID,CPPORG,CPEDDATE,CPMEMO,PLANDETAIL,BAOJINGTIME,PLANDCOUNT,UDate)");
                    sql.Append("VALUES(");
                    sql.AppendFormat("'{0}',", model.CPCODE);
                    sql.AppendFormat("'{0}',", model.CPTITLE);
                    sql.AppendFormat("'{0}',", model.CPSDATE);
                    sql.AppendFormat("'{0}',", model.CPEDATE);
                    sql.AppendFormat("'{0}',", model.CPTPROPERTY);
                    sql.AppendFormat("'{0}',", model.CPFROM);
                    sql.AppendFormat("'{0}',", model.CPEDITOR);
                    sql.AppendFormat("'{0}',", model.CPPORGID);
                    sql.AppendFormat("'{0}',", model.CPPORG);
                    sql.AppendFormat("'{0}',", model.CPEDDATE);
                    sql.AppendFormat("'{0}',", model.CPMEMO);
                    sql.AppendFormat("'{0}',", model.PLANDETAIL);
                    sql.AppendFormat("'{0}',", model.BAOJINGTIME);
                    sql.AppendFormat("'{0}',", model.PLANDCOUNT);
                    sql.AppendFormat("'{0}'", model.UDate);
                    sql.Append(")");
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
        /// 获取最大值
        /// </summary>
        /// <param name="code"></param>
        /// <param name="lev"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int GetMaxNO(string code, int lev, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn;

            try
            {
                string sql = string.Format("SELECT CPCODE FROM tTask WHERE CPCODE LIKE '{0}' ORDER BY CPCODE DESC", code);
                Object o = DataBase.GetOneValue(sql, out errMsg);
                if (o == null)
                {
                    rtn = 0;
                }
                else
                {
                    string rightStr = o.ToString().Substring(o.ToString().Length - lev, lev);
                    rtn = Convert.ToInt32(rightStr);
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
        public static string NameFromCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return string.Empty;
            }
            string errMsg = string.Empty;


            try
            {
                string sql = string.Format("SELECT CPTITLE FROM tTask WHERE CPCODE='{0}' ORDER BY CPCODE", code);
                Object obj = DataBase.GetOneValue(sql, out errMsg);
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
                return null;
            }
        }

        /// <summary>
        /// 删除抽样任务记录
        /// </summary>
        /// <param name="where"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public string DeleteTast(string where, out string errMsg)
        {
            string rtn = "";
            errMsg = string.Empty;
            try
            {
                sql.Length = 0;
                sql.AppendFormat("DELETE FROM KTask ");

                if (where != "")
                {
                    sql.Append(" where ");
                    sql.Append(where);
                }

                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                rtn = "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return rtn;
        }



        /// <summary>
        /// 通过食品类别获取检测项目编号
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        //public static string CheckItemsFromCode(string code)
        //{
        //    string sErrMsg = string.Empty;
        //    if (code.Equals(string.Empty))
        //    {
        //        return string.Empty;
        //    }

        //    try
        //    {
        //        string sql = string.Format("select CheckItemCodes from tFoodClass where CPCODE='{0}' order by CPCODE", code);
        //        Object o = DataBase.GetOneValue(sql, out sErrMsg);
        //        if (o == null)
        //        {
        //            return string.Empty;
        //        }
        //        else
        //        {
        //            return o.ToString();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        sErrMsg = e.Message;
        //        return null;
        //    }
        //}
        public DataTable GetQtask(string whereSql, string orderBySql, int type)
        {
            string errMsg = string.Empty;
            DataTable dtbl = null;

            try
            {
                StringBuilder sb = new StringBuilder();

                if (type == 0)
                {
                    //sb.Append("select * from KTask ");
                    sb.Append("SELECT taskid,taskname,taskdate,sampleid,sample,itemid,item,CompanyID,CheckCompany,shopkou,Testmokuai,Checktype,s_sampling_no FROM KTask");
                    //sb.Append("CPFROM,CPEDITOR,CPPORGID,CPPORG,CPEDDATE,CPMEMO,PLANDETAIL,PLANDCOUNT FROM tTask");
                }
                else if (type == 1)
                {
                    sb.Append("select * from KTask ");
                    //sb.Append("SELECT tid,sampling_id,sample_code,food_id,food_name,sample_number,purchase_amount,sample_date,purchase_date,item_id,item_name,origin,supplier,supplier_address,supplier_person,supplier_phone,batch_number,status,recevie_device,ope_shop_name,remark,param1,param2,param3,s_id,s_sampling_no,s_sampling_date,s_point_id,s_reg_id,s_reg_name,s_reg_licence,s_reg_link_person,s_reg_link_phone,s_ope_id,s_ope_shop_code,s_ope_shop_name,s_qrcode,s_task_id,s_status,s_place_x,s_place_y,s_sampling_userid,s_sampling_username,s_ope_signature,s_create_by,s_create_date,s_update_by,s_update_date,s_sheet_address,s_param1,s_param2,s_param3,t_id,t_task_code,t_task_title,t_task_content,t_task_detail_pId,t_project_id,t_task_type,t_task_source,t_task_status,t_task_total,t_sample_number,t_task_sdate,t_task_edate,t_task_pdate,t_task_fdate,t_task_departId,t_task_announcer,t_task_cdate,t_remark,t_view_flag,t_file_path,t_delete_flag,t_create_by,t_create_date,t_update_by,t_update_date,td_id,td_task_id,td_detail_code,td_sample_id,td_sample,td_item_id,td_item,td_task_fdate,td_receive_pointid,td_receive_point,td_receive_nodeid,td_receive_node,td_receive_userid,td_receive_username,td_receive_status,td_task_total,td_sample_number,td_remark,mokuai,Checktype FROM KTask");
                    //sb.Append("SELECT tid,sampling_id,sample_code,food_id,food_name,sample_number,purchase_amount,sample_date,purchase_date,item_id,item_name,origin,supplier,supplier_address,supplier_person,supplier_phone,batch_number,status,recevie_device,ope_shop_name,remark,param1,param2,param3,s_id,s_sampling_no,convert(datetime,s_sampling_date) as dt,s_point_id,s_reg_id,s_reg_name,s_reg_licence,s_reg_link_person,s_reg_link_phone,s_ope_id,s_ope_shop_code,s_ope_shop_name,s_qrcode,s_task_id,s_status,s_place_x,s_place_y,s_sampling_userid,s_sampling_username,s_ope_signature,s_create_by,s_create_date,s_update_by,s_update_date,s_sheet_address,s_param1,s_param2,s_param3,t_id,t_task_code,t_task_title,t_task_content,t_task_detail_pId,t_project_id,t_task_type,t_task_source,t_task_status,t_task_total,t_sample_number,t_task_sdate,t_task_edate,t_task_pdate,t_task_fdate,t_task_departId,t_task_announcer,t_task_cdate,t_remark,t_view_flag,t_file_path,t_delete_flag,t_create_by,t_create_date,t_update_by,t_update_date,td_id,td_task_id,td_detail_code,td_sample_id,td_sample,td_item_id,td_item,td_task_fdate,td_receive_pointid,td_receive_point,td_receive_nodeid,td_receive_node,td_receive_userid,td_receive_username,td_receive_status,td_task_total,td_sample_number,td_remark,mokuai FROM KTask"); 

                }
                else if (type == 2)
                {
                    sb.Append("SELECT taskid,taskname,taskdate,sampleid,sample,itemid,item,CompanyID,CheckCompany,shopkou,Testmokuai,Checktype,s_sampling_no FROM KTask");
                }
                if (type == 3)
                {
                    sb.Append("SELECT ID FROM KTask");
                    //// SELECT CPCODE,CPTITLE,CPSDATE,CPEDATE,CPTPROPERTY,CPFROM,CPEDITOR,CPPORGID,CPPORG,CPEDDATE,PLANDETAIL,
                    ////PLANDCOUNT,BAOJINGTIME,(SELECT COUNT(*) FROM ttResultSecond WHERE CheckPlanCode=m.CPCODE ) AS v30,'未完成' as finish
                    //// FROM tTask as m where (cdate(BAOJINGTIME)>=#2015-10-26 22:21:23#)
                    //sb.Append(" SELECT CPCODE,CPTITLE,CPSDATE,CPEDATE,CPTPROPERTY,CPFROM,CPEDITOR,CPPORGID,CPPORG,CPEDDATE,PLANDETAIL,");
                    //sb.Append("PLANDCOUNT,BAOJINGTIME,(SELECT COUNT(*) FROM ttResultSecond WHERE CheckPlanCode=m.CPCODE ) AS v30,'未完成' as finish");
                    //sb.Append(",(v30/PLANDCOUNT) as Num  FROM tTask as m");
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
                    sb.Append(" desc ");
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "KTask" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["KTask"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                //Log.WriteLog("查询clsTask",e);
                errMsg = e.Message;
            }
            return dtbl;
        }
        public string errMsg { get; set; }
    }
}
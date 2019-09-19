﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace DataSetModel.DataSentence
//{
//    class clsTaskOpr
//    {
//    }
//}
using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;


namespace DataSetModel
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
        private StringBuilder sb = new StringBuilder();
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

                sb.Length = 0;
                sb.AppendFormat("UPDATE tTask SET CPTITLE='{0}'", model.CPTITLE);
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
                sb.AppendFormat(" WHERE CPCODE='{0}'", model.CPCODE);
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

                if (!whereSql.Equals(string.Empty))
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
                sb.Length = 0;
                if (type == 1)
                {
                    sb.Append("Select ID,FtypeNmae,SampleNum,Name,ItemDes,StandardValue,Demarcate,Unit From ttStandDecide ");
                    if (!SampleName.Equals(string.Empty) && !Name.Equals(string.Empty))
                    {
                        sb.AppendFormat(" WHERE FtypeNmae Like '%{0}%'", SampleName);
                        sb.AppendFormat(IsPreciseQuery ? " AND Name Like '{0}'" : " AND Name Like '%{0}%'", Name);
                    }
                    else if (SampleName.Equals(string.Empty) && !Name.Equals(string.Empty))
                    {
                        sb.AppendFormat(IsPreciseQuery ? " WHERE Name Like '{0}'" : " WHERE Name Like '%{0}%'", Name);
                    }
                    else if (!SampleName.Equals(string.Empty) && Name.Equals(string.Empty))
                    {
                        sb.AppendFormat(" WHERE FtypeNmae Like '%{0}%'", SampleName);
                    }
                }
                else if (type == 2)
                {
                    sb.AppendFormat("Select Top 1 ID,FtypeNmae,SampleNum,Name,ItemDes,StandardValue,Demarcate,Unit,UDate From ttStandDecide Where SampleNum Like'{0}'", SampleName);
                }
                else if (type == 3)
                {
                    //sb.AppendFormat("select (select food_name from foodlist where fid=s.food_id) as foodname,(select detect_item_name from DetectItem where cid=s.item_id) as itemname, * from SampleStandard  s where item_id in (select cid from DetectItem where detect_item_name like '%{0}%')", Name);
                    sb.AppendFormat("select f.food_name as foodname,d.detect_item_name as itemname, * from DetectItem d,SampleStandard  s ,foodlist f where f.fid=s.food_id and s.item_id=d.cid and d.detect_item_name = '{0}'", Name);
                }
                else if (type == 4)
                {
                    sb.AppendFormat("select (select food_name from foodlist where fid=s.food_id) as foodname,(select detect_item_name from DetectItem where cid=s.item_id) as itemname, * from SampleStandard  s where food_id in (select fid from foodlist where food_name like '%{0}%')", SampleName);
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "tStandDecide" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["tStandDecide"];
               
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
                sb.Length = 0;
                sb.Append("Select SysCode,StdCode,ItemDes,CheckType,StandardCode,StandardValue,Unit,DemarcateInfo From tCheckItem ");
                if (!Name.Equals(string.Empty))
                {
                    sb.Append(" Where ItemDes Like '%");
                    sb.Append(Name + "%'");
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "tCheckItem" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["tCheckItem"];
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dtbl;
        }

        public DataTable getlastoneItem(string where,string ordersql)
        {
            string errMsg = string.Empty;
            DataTable dtbl = null;
            try
            {
                sb.Length = 0;
                sb.AppendFormat(" Select Top {0} ", 1);
                sb.Append(" * From KTask order by ID desc ");
                if (!where.Equals(string.Empty))
                {
                    sb.Append(" WHERE ");
                    sb.Append(where);
                }
                if (!ordersql.Equals(string.Empty))
                {
                    sb.Append(" ORDER BY ");
                    sb.Append(ordersql);
                    sb.Append(" ASC ");
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "task" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["task"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                //Log.WriteLog("查询clsTask",e);
                errMsg = e.Message;
            }
            return dtbl;
        }

        public DataTable GetCheckItem(string whereSql, string orderBySql, int type)
        {
            string errMsg = string.Empty;
            DataTable dtbl = null;
            try
            {
                sb.Length = 0;

                if (type == 0)
                {
                    sb.Append("SELECT mid,device_type_id,item_id,project_type,detect_method,detect_unit,operation_password,food_code,invalid_value,check_hole1,check_hole2,");
                    sb.Append("wavelength,pre_time,dec_time,stdA0,stdA1,stdA2,stdA3,stdB0,stdB1,stdB2,stdB3,stdA,stdB,national_stdmin,national_stdmax,yin_min,yin_max,yang_min,yang_max,yinT,yangT,absX,ctAbsX");
                    sb.Append(",division,parameter,trailingEdgeC,trailingEdgeT,suspiciousMin,suspiciousMax,reserved1,reserved2,reserved3,reserved4,reserved5,m.remark,m.delete_flag,m.create_by,m.create_date,m.update_by,m.update_date,d.detect_item_name as item FROM MachineItem m,DetectItem d where d.cid=item_id and ");


                }
                else if (type == 1)
                {
                    sb.Append("SELECT tid,sampling_id,sample_code,food_id,food_name,sample_number,purchase_amount,sample_date,purchase_date,item_id,item_name,origin,supplier,supplier_address,supplier_person,supplier_phone,batch_number,status,recevie_device,ope_shop_name,remark,param1,param2,param3,s_id,s_sampling_no,s_sampling_date,s_point_id,s_reg_id,s_reg_name,s_reg_licence,s_reg_link_person,s_reg_link_phone,s_ope_id,s_ope_shop_code,s_ope_shop_name,s_qrcode,s_task_id,s_status,s_place_x,s_place_y,s_sampling_userid,s_sampling_username,s_ope_signature,s_create_by,s_create_date,s_update_by,s_update_date,s_sheet_address,s_param1,s_param2,s_param3,t_id,t_task_code,t_task_title,t_task_content,t_task_detail_pId,t_project_id,t_task_type,t_task_source,t_task_status,t_task_total,t_sample_number,t_task_sdate,t_task_edate,t_task_pdate,t_task_fdate,t_task_departId,t_task_announcer,t_task_cdate,t_remark,t_view_flag,t_file_path,t_delete_flag,t_create_by,t_create_date,t_update_by,t_update_date,td_id,td_task_id,td_detail_code,td_sample_id,td_sample,td_item_id,td_item,td_task_fdate,td_receive_pointid,td_receive_point,td_receive_nodeid,td_receive_node,td_receive_userid,td_receive_username,td_receive_status,td_task_total,td_sample_number,td_remark,mokuai,Checktype FROM KTask");
                }
                else if (type == 2)
                {
                    sb.Append("select cid from DetectItem where ");
                }

                if (!whereSql.Equals(string.Empty))
                {
                    //sb.Append(" WHERE ");
                    sb.Append(whereSql);
                }
                if (!orderBySql.Equals(string.Empty))
                {
                    sb.Append(" ORDER BY ");
                    sb.Append(orderBySql);
                    sb.Append(" desc ");
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "machineitem" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["machineitem"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dtbl;
        }

        public DataTable GetCompany(string whereSql, string orderBySql, int type,out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dtbl = null;

            try
            {
                sb.Length = 0;

                if (type == 0)
                {
                    sb.Append("select b.bid as bid,b.reg_id as reg_id,b.ope_shop_name as ope_shop_name,b.ope_shop_code as ope_shop_code,r.rid as rid,r.reg_name,r.link_user,r.reg_address,r.link_phone,r.update_date,r.depart_id  from regulardata r,business b ");
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
                string[] names = new string[1] { "business" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["business"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dtbl;
        }

        public DataTable GetQtask(string whereSql, string orderBySql, int type)
        {
            string errMsg = string.Empty;
            DataTable dtbl = null;

            try
            {
                sb.Length = 0;

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
                string[] names = new string[1] { "task" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["task"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                //Log.WriteLog("查询clsTask",e);
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
                sb.Length = 0;

                if (type == 0)
                {
                    sb.Append("SELECT CPCODE,CPTITLE,CPSDATE,CPEDATE,CPTPROPERTY,");
                    sb.Append("CPFROM,CPEDITOR,CPPORGID,CPPORG,CPEDDATE,CPMEMO,PLANDETAIL,PLANDCOUNT FROM tTask");
                }
                else if (type == 1)
                {
                    sb.Append("SELECT * FROM tTask");
                }
                else if (type == 2)
                {
                    sb.Append("SELECT CPCODE,CPTITLE  FROM tTask");
                }
                if (type == 3)
                {
                    // SELECT CPCODE,CPTITLE,CPSDATE,CPEDATE,CPTPROPERTY,CPFROM,CPEDITOR,CPPORGID,CPPORG,CPEDDATE,PLANDETAIL,
                    //PLANDCOUNT,BAOJINGTIME,(SELECT COUNT(*) FROM ttResultSecond WHERE CheckPlanCode=m.CPCODE ) AS v30,'未完成' as finish
                    // FROM tTask as m where (cdate(BAOJINGTIME)>=#2015-10-26 22:21:23#)
                    sb.Append(" SELECT CPCODE,CPTITLE,CPSDATE,CPEDATE,CPTPROPERTY,CPFROM,CPEDITOR,CPPORGID,CPPORG,CPEDDATE,PLANDETAIL,");
                    sb.Append("PLANDCOUNT,BAOJINGTIME,(SELECT COUNT(*) FROM ttResultSecond WHERE CheckPlanCode=m.CPCODE ) AS v30,'未完成' as finish");
                    sb.Append(",(v30/PLANDCOUNT) as Num  FROM tTask as m");
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
                    sb.Append(" ASC ");
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "task" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["task"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                //Log.WriteLog("查询clsTask",e);
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
                sb.Length = 0;
                sb.Append("SELECT CPTITLE FROM tTask");
                if (!code.Equals(string.Empty))
                {
                    sb.Append(" WHERE CPCODE Like '%" + code + "%'");
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "task" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["task"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                //Log.WriteLog("查询clsTask",e);
                errMsg = e.Message;
            }
            return string.Empty;
        }

        public int UpdateTask(string where, string data, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sb.Length = 0;
                sb.AppendFormat("UPDATE KTask SET mokuai='{0}'", data);
                sb.AppendFormat(" where tid='{0}'", where);
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
                sb.Length = 0;
                sb.Append("Insert Into tTask");
                sb.Append("(CPCODE,CPTITLE,CPSDATE,CPEDATE,CPTPROPERTY,CPFROM,CPEDITOR");
                sb.Append(",CPPORGID,CPPORG,CPEDDATE,CPMEMO,PLANDETAIL,BAOJINGTIME,PLANDCOUNT)");
                sb.Append("VALUES(");
                sb.AppendFormat("'{0}',", model.CPCODE);
                sb.AppendFormat("'{0}',", model.CPTITLE);
                sb.AppendFormat("'{0}',", model.CPSDATE);
                sb.AppendFormat("'{0}',", model.CPEDATE);
                sb.AppendFormat("'{0}',", model.CPTPROPERTY);
                sb.AppendFormat("'{0}',", model.CPFROM);
                sb.AppendFormat("'{0}',", model.CPEDITOR);
                sb.AppendFormat("'{0}',", model.CPPORGID);
                sb.AppendFormat("'{0}',", model.CPPORG);
                sb.AppendFormat("'{0}',", model.CPEDDATE);
                sb.AppendFormat("'{0}',", model.CPMEMO);
                sb.AppendFormat("'{0}',", model.PLANDETAIL);
                sb.AppendFormat("'{0}',", model.BAOJINGTIME);
                sb.AppendFormat("'{0}'", model.PLANDCOUNT);
                sb.Append(")");
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
                sb.Length = 0;
                DataTable dt = new clsCompanyOpr().GetAsDataTable("CPCODE='" + model.CPCODE + "'", string.Empty, 11);
                if (dt != null && dt.Rows.Count > 0)
                {
                    sb.AppendFormat("Update tTask Set CPCODE='{0}',CPTITLE='{1}',CPSDATE='{2}',CPEDATE='{3}',CPTPROPERTY='{4}',",
                        model.CPCODE, model.CPTITLE, model.CPSDATE, model.CPEDATE, model.CPTPROPERTY);
                    sb.AppendFormat("CPFROM='{0}',CPEDITOR='{1}',CPPORGID='{2}',CPPORG='{3}',CPEDDATE='{4}',",
                        model.CPFROM, model.CPEDITOR, model.CPPORGID, model.CPPORG, model.CPEDDATE);
                    sb.AppendFormat("CPMEMO='{0}',PLANDETAIL='{1}',PLANDCOUNT='{2}',BAOJINGTIME='{3}',UDate='{4}' Where CPCODE='{5}'",
                        model.CPMEMO, model.PLANDCOUNT, model.PLANDCOUNT, model.BAOJINGTIME, model.UDate, model.CPCODE);
                }
                else
                {
                    sb.Append("Insert Into tTask");
                    sb.Append("(CPCODE,CPTITLE,CPSDATE,CPEDATE,CPTPROPERTY,CPFROM,CPEDITOR");
                    sb.Append(",CPPORGID,CPPORG,CPEDDATE,CPMEMO,PLANDETAIL,BAOJINGTIME,PLANDCOUNT,UDate)");
                    sb.Append("VALUES(");
                    sb.AppendFormat("'{0}',", model.CPCODE);
                    sb.AppendFormat("'{0}',", model.CPTITLE);
                    sb.AppendFormat("'{0}',", model.CPSDATE);
                    sb.AppendFormat("'{0}',", model.CPEDATE);
                    sb.AppendFormat("'{0}',", model.CPTPROPERTY);
                    sb.AppendFormat("'{0}',", model.CPFROM);
                    sb.AppendFormat("'{0}',", model.CPEDITOR);
                    sb.AppendFormat("'{0}',", model.CPPORGID);
                    sb.AppendFormat("'{0}',", model.CPPORG);
                    sb.AppendFormat("'{0}',", model.CPEDDATE);
                    sb.AppendFormat("'{0}',", model.CPMEMO);
                    sb.AppendFormat("'{0}',", model.PLANDETAIL);
                    sb.AppendFormat("'{0}',", model.BAOJINGTIME);
                    sb.AppendFormat("'{0}',", model.PLANDCOUNT);
                    sb.AppendFormat("'{0}'", model.UDate);
                    sb.Append(")");
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

        public string errMsg { get; set; }
    }
}
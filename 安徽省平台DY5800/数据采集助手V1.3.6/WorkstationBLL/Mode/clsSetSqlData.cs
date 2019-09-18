﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using WorkstationDAL.Basic;
using WorkstationDAL.Model;
using WorkstationDAL.UpLoadData;


namespace WorkstationBLL.Mode
{
    public class clsSetSqlData
    {   
        private StringBuilder _strBd = new StringBuilder();
        private  clsUpdateData up = new clsUpdateData();
        private StringBuilder sb = new StringBuilder();
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

        public DataTable GetCompany(string whereSql, string orderBySql, int type, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dtbl = null;

            try
            {
                sb.Length = 0;

                if (type == 0)
                {
                    sb.Append("select b.bid as bid,b.reg_id as reg_id,b.ope_shop_name as ope_shop_name,b.ope_shop_code as ope_shop_code,r.rid as rid,r.reg_name,r.link_user,r.reg_address,r.link_phone,r.update_date,r.depart_id  from Regulatory r,Business b ");
                }
                else if(type==1)
                {

                    sb.Append("select * from Regulatory ");
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

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="%pkname%">主键编号</param>
        /// <returns></returns>
        public int Delete(string mainkey, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            _strBd.Length = 0;
            try
            {
                _strBd.AppendFormat("DELETE FROM {0}", mainkey);
                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);

                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }

        /// <summary>
        /// 根据条件查询检测记录信息
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public DataTable SearchData(string whereSql)
        {
            string errMsg = string.Empty;
            DataTable dt = null;
            _strBd.Length = 0;
            try
            {
                _strBd.Append("Select * From SetCom");
                //_strBd.Append(whereSql);
                //if (!whereSql.Equals(""))
                //{
                //    _strBd.Append(" Where ");
                //    _strBd.Append(whereSql);
                //}
                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "tSetCom" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["tSetCom"];
                //_strBd.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
        }
        /// <summary>
        /// 保存结果
        /// </summary>
        /// <param name="result"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int ResuInsert(clsSaveResult result, out string errMsg)
        {
            int rtn = 0;
            _strBd.Length = 0;
            errMsg = string.Empty;
            try
            {
                _strBd.Append("INSERT INTO CheckResult ");
                _strBd.Append("(ChkNum,Checkitem,SampleName,CheckData,Unit,CheckTime,CheckUnit,Result,Save,Machine,SampleTime,SampleAddress,DetectUnit,TestBase,LimitData,Tester,StockIn,SampleNum,");
                _strBd.Append("IsUpload,SampleCode,SampleCategory,MachineNum,ProductPlace,ProductDatetime,Barcode,ProcodeCompany,ProduceAddr,ProduceUnit,SendTestDate,NumberUnit,CompanyNeture,DoResult,sampleid,HoleNum,Xiguangdu,BID");
                _strBd.Append(",TID,SampleCategoryCode,ChkCompanyCode,itemCode,ChkMethod");
                _strBd.AppendFormat(")VALUES('{0}',", result.CheckNumber);
                _strBd.AppendFormat("'{0}',",result.Checkitem);
                _strBd.AppendFormat("'{0}',",result.SampleName);
                _strBd.AppendFormat("'{0}',", result.CheckData);
                _strBd.AppendFormat("'{0}',", result.Unit);
                _strBd.AppendFormat("#{0}#,", result.CheckTime);
                _strBd.AppendFormat("'{0}',", result.CheckUnit);
                _strBd.AppendFormat("'{0}',", result.Result);
                _strBd.AppendFormat("'{0}',", "是");
                _strBd.AppendFormat("'{0}',", Global.ChkManchine);
                _strBd.AppendFormat("'{0}',", result.Gettime == "" ? System.DateTime.Now.ToString() : result.Gettime);
                _strBd.AppendFormat("'{0}',", result.Getplace);
                _strBd.AppendFormat("'{0}',", result.detectunit);
                _strBd.AppendFormat("'{0}',", result.Testbase);
                _strBd.AppendFormat("'{0}',", result.LimitData);
                _strBd.AppendFormat("'{0}',", result.Tester);
                _strBd.AppendFormat("'{0}',", result.quantityin);
                _strBd.AppendFormat("'{0}',", result.sampleNum);
                _strBd.AppendFormat("'{0}',", "否");
                _strBd.AppendFormat("'{0}',", result.SampleCode);
                _strBd.AppendFormat("'{0}',", result.SampleType);
                _strBd.AppendFormat("'{0}',", result.MachineCode);
                _strBd.AppendFormat("'{0}',", result.ProductPlace);
                _strBd.AppendFormat("'{0}',", result.ProductDate);
                _strBd.AppendFormat("'{0}',", result.Barcode);
                _strBd.AppendFormat("'{0}',", result.ProductCompany);
                _strBd.AppendFormat("'{0}',", result.ProductPlace);
                _strBd.AppendFormat("'{0}',", result.ProductCompany);
                _strBd.AppendFormat("'{0}',", result.SendDate);
                _strBd.AppendFormat("'{0}',", result.NumberUnit);
                _strBd.AppendFormat("'{0}',", result.CheckUnitNature);
                _strBd.AppendFormat("'{0}',", result.TreatResult);
                _strBd.AppendFormat("'{0}',", result.SampeID );
                _strBd.AppendFormat("'{0}',", result.HoleNumber );
                _strBd.AppendFormat("'{0}',", result.Xiguagndu);
                _strBd.AppendFormat("'{0}',", result.BID);
                _strBd.AppendFormat("'{0}',", result.TaskID);
                _strBd.AppendFormat("'{0}',", result.SampleTypeCode);//样品种类编号
                _strBd.AppendFormat("'{0}',", result.CheckCompanyCode);//被检单位编号
                _strBd.AppendFormat("'{0}',", result.CheckitemCode);//检测项目编号
                _strBd.AppendFormat("'{0}')", result.ChkMethod );//检测方法

                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }

        public DataTable GetResultTable(string whereSQL, string oderby, int type,int count, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dtb1 = null;
            _strBd.Length = 0;
            try
            {
                if (type == 1)
                {
                    _strBd.AppendFormat(" Select Top {0} * From CheckResult t order by ID desc ", count);
                    //_strBd.Append(" ");
                }
                else if(type==2)
                {
                    _strBd.Append("SELECT ");
                    _strBd.Append("ChkNum,Checkitem,SampleName,CheckData,Unit,CheckTime,CheckUnit,Result,Machine,SampleTime,SampleAddress,");
                    _strBd.Append("DetectUnit,TestBase,LimitData,Tester,StockIn,SampleNum,IsUpload,SampleCode,SampleCategory,MachineNum");
                    _strBd.Append(" FROM CheckResult ");
                    //_strBd.Append("CheckResult ");
                }
               
                if (whereSQL.Length > 0)
                {
                    _strBd.Append(" WHERE ");
                    _strBd.Append(whereSQL);
                }
                if (oderby.Length > 0)
                {
                    _strBd.Append(oderby);
                }

                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "CheckResult" };
                dtb1 = DataBase.GetDataSet(cmd, names, out errMsg).Tables["CheckResult"];
                //_strBd.Length = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dtb1;
        }
        /// <summary>
        /// 查询检测结果数据
        /// </summary>
        /// <param name="whereSQL"></param>
        /// <param name="oderby"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string whereSQL,string oderby)
        {
            string errMsg = string.Empty;
            DataTable dtb1 = null;
            _strBd.Length = 0;
            try
            {
                _strBd.Append("SELECT * FROM CheckResult");

                //_strBd.Append("SELECT ");
                //_strBd.Append("ChkNum,Checkitem,SampleName,CheckData,Unit,CheckTime,CheckUnit,Result,Machine,SampleTime,SampleAddress,");
                //_strBd.Append("DetectUnit,TestBase,LimitData,Tester,StockIn,SampleNum,IsUpload,SampleCode,SampleCategory,MachineNum,ID");
                //_strBd.Append(" FROM ");
                //_strBd.Append("CheckResult ");
                if (whereSQL.Length > 0)
                {
                    _strBd.Append(" WHERE ");
                    _strBd.Append(whereSQL);
                }
                if (oderby.Length > 0)
                {
                    _strBd.Append(oderby);
                }
                
                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "CheckResult" };
                dtb1 = DataBase.GetDataSet(cmd, names, out errMsg).Tables["CheckResult"];
                //_strBd.Length = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }  
            return dtb1;
        }
       
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="SR"></param>
        /// <param name="errMsg"></param>
        public void UpdateResult(clsUpdateData SR,out string errMsg)
        {
            errMsg = string.Empty;
            _strBd.Length = 0;
            int rtn = 0;
            try
            {
                _strBd.Append("update CheckResult ");
                _strBd.Append("set SampleTime='");
                _strBd.Append(SR.GetSampTime);
                _strBd.Append("',");
                _strBd.Append("SampleAddress='");
                _strBd.Append(SR.GetSampPlace);
                _strBd.Append("',");
                _strBd.Append("Result='");
                _strBd.Append(SR.result );
                _strBd.Append("',");
                _strBd.Append("Unit='");
                _strBd.Append(SR.unit );
                _strBd.Append("',");
                _strBd.Append("Machine='");
                _strBd.Append(SR.intrument);
                _strBd.Append("',");
                _strBd.Append("TestBase='");
                _strBd.Append(SR.Chktestbase);
                _strBd.Append("',");
                _strBd.Append("Tester='");
                _strBd.Append(SR.ChkPeople);
                _strBd.Append("',");
                _strBd.Append("CheckUnit='");
                _strBd.Append(SR.ChkUnit);
                _strBd.Append("',");
                _strBd.Append("StockIn='");
                _strBd.Append(SR.quantityin );
                _strBd.Append("',");
                _strBd.Append("SampleNum='");
                _strBd.Append(SR.sampleNum);
                _strBd.Append("' where ChkNum='");
                _strBd.Append(Global.bianhao);
                _strBd.Append("' and ");
                _strBd.Append("Checkitem='");
                _strBd.Append(Global.Chkxiangmu);
                _strBd.Append("' and ");
                _strBd.Append("CheckTime=#");
                _strBd.Append(Global.ChkTime);
                _strBd.Append("# and ");
                _strBd.Append("SampleName='");
                _strBd.Append(Global.ChkSample == "" ? " '" : Global.ChkSample+"'");

                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
 
        }
        /// <summary>
        /// 保存串口号
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errMsg"></param>
        public void updateCom(clsSetCom model, out string errMsg)
        {
            //int rtn = 0;
            errMsg = string.Empty;
            _strBd.Length = 0;
            try
            {
                _strBd.Append("update SetCom ");
                _strBd.Append("set ComPort=");              
                _strBd.Append(model.ComPort);
                _strBd.Append(" where ID=1");
                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
                //rtn = 1;
            }
            catch(Exception e)
            {
                errMsg = e.Message;
            }
        }
        /// <summary>
        /// 添加仪器保存到本地数据库
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errMsg"></param>
        public void updateIntrument(clsIntrument addmach, out string errMsg)
        {
            errMsg = string.Empty;
            _strBd.Length = 0;
            try
            {
                _strBd.Append("INSERT INTO Instrument ");
                _strBd.Append("(Name)");
                _strBd.Append("VALUES('");
                _strBd.Append(addmach.Name);
                _strBd.Append("')");            
                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);                
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
        }
        public DataTable SearchIntrument(string whereSQL, string oderby)
        {
            DataTable dt = null;
            string errMsg = string.Empty;
            _strBd.Length = 0;
            try
            {
                _strBd.Append("SELECT *");
                //_strBd.Append("ID,Name");
                _strBd.Append(" FROM ");
                _strBd.Append("Instrument ");
                //_strBd.Append(" WHERE ");
                _strBd.Append(whereSQL);
                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "Instrument" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Instrument"];
                _strBd.Length = 0;

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return  dt;
        }
       
        /// <summary>
        /// 导入Excel数据
        /// </summary>
        /// <param name="result"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int LoadInData(clsSaveResult result, out string errMsg)
        {
            int rtn = 0;
            _strBd.Length = 0;
            errMsg = string.Empty;
            try
            {
                _strBd.Append("INSERT INTO CheckResult ");
                _strBd.Append("(ChkNum,Checkitem,SampleName,CheckData,Unit,CheckTime,CheckUnit,Result,Save,Machine,SampleTime,SampleAddress,TestBase,LimitData,Tester,");
                _strBd.Append("DetectUnit,SampleNum,SampleCategory,NumberUnit,Barcode,ProduceUnit,ProduceAddr,ProcodeCompany,ProductPlace,ProductDatetime,SendTestDate,CompanyNeture,DoResult,IsUpload,StockIn,MachineNum,SampleCode,");
                _strBd.Append("Xiguangdu,HoleNum,ChkCompanyCode,itemCode,SampleCategoryCode,ChkMethod)VALUES('");
          
                _strBd.Append(result.CheckNumber);
                _strBd.Append("','");
                _strBd.Append(result.Checkitem);
                _strBd.Append("','");
                _strBd.Append(result.SampleName);
                _strBd.Append("','");
                _strBd.Append(result.CheckData);
                _strBd.Append("','");
                _strBd.Append(result.Unit);
                _strBd.Append("','");
                _strBd.Append(result.CheckTime);
                _strBd.Append("','");
                _strBd.Append(result.CheckUnit == "" ? Global.CheckedUnit : result.CheckUnit);
                _strBd.Append("','");
                _strBd.Append(result.Result);
                _strBd.Append("','");
                _strBd.Append("是");
                _strBd.Append("','");
                _strBd.Append(result.Instrument);
                _strBd.Append("','");
                _strBd.Append(result.Gettime == "" ? result.Gettime : result.Gettime);
                _strBd.Append("','");
                _strBd.Append(result.Getplace);
                //_strBd.Append("','");
                //_strBd.Append(result.PlanNum);
                _strBd.Append("','");
                _strBd.Append(result.Testbase);
                _strBd.Append("','");
                _strBd.Append(result.LimitData);
                _strBd.Append("','");
                _strBd.Append(result.Tester);
                _strBd.Append("','");
                _strBd.Append(result.detectunit);
                _strBd.Append("','");
                _strBd.Append(result.sampleNum);
                _strBd.AppendFormat("','{0}',", result.SampleType);
                _strBd.AppendFormat("'{0}',", result.numUnit);
                _strBd.AppendFormat("'{0}',", result.Barcode );
                _strBd.AppendFormat("'{0}',", result.productUnit);
                _strBd.AppendFormat("'{0}',", result.productAddr);
                _strBd.AppendFormat("'{0}',", result.ProductCompany);
                _strBd.AppendFormat("'{0}',", result.Addr);
                _strBd.AppendFormat("'{0}',", result.ProductDate );
                _strBd.AppendFormat("'{0}',", result.SendDate );
                _strBd.AppendFormat("'{0}',", result.CheckUnitNature);
                _strBd.AppendFormat("'{0}',", result.TreatResult);
                _strBd.AppendFormat("'{0}',", result.IsUpLoad);
                _strBd.AppendFormat("'{0}',", result.stockin);
                _strBd.AppendFormat("'{0}',", result.IntrumentNum);
                _strBd.AppendFormat("'{0}',", result.SampleCode);
                _strBd.AppendFormat("'{0}',", result.Xiguagndu);
                _strBd.AppendFormat("'{0}',", result.HoleNumber);
                _strBd.AppendFormat("'{0}',", result.CheckCompanyCode);
                _strBd.AppendFormat("'{0}',", result.CheckitemCode);
                _strBd.AppendFormat("'{0}',", result.SampleTypeCode);
                _strBd.AppendFormat("'{0}')", result.ChkMethod);


                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }
        /// <summary>
        /// 查询仪器信息
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetIntrument(string sql, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dtb1 = null;
            _strBd.Length = 0;
            try
            {
                _strBd.Append("SELECT Name,Manufacturer,communication,Isselect,ChkStdCode,Numbering,Protocol,ID FROM Instrument ");
                //_strBd.Append(" FROM ");
                //_strBd.Append("Instrument ");
                _strBd.Append(sql);
                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "Instrument" };
                dtb1 = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Instrument"];
                //_strBd.Length = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dtb1;

        }
        public int insertInstrument(string name,string productor,string comType,string select,string num,string protoco)
        {
            string errMsg = string.Empty;
            _strBd.Length = 0;
            int rtn = 0;
            try
            {
                _strBd.Append("INSERT INTO Instrument ");
                _strBd.Append("(Name,Manufacturer,communication,Isselect,Numbering,Protocol");      
                _strBd.Append(")");
                _strBd.Append("VALUES('");
                _strBd.Append(name);
                _strBd.Append("','");
                _strBd.Append(productor);
                _strBd.Append("','");
                _strBd.Append(comType);
                _strBd.Append("','");
                _strBd.Append(select=="True"?"是":"否");
                _strBd.Append("','");
                _strBd.Append(num);
                _strBd.Append("','");
                _strBd.Append(protoco);
                _strBd.Append("')");
                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }
        /// <summary>
        /// 删除仪器信息
        /// </summary>
        /// <param name="mainkey"></param>
        /// <returns></returns>
        public int deleteInstrument(string mainkey)
        {
            string errMsg = string.Empty;
            int rtn = 0;
            _strBd.Length = 0;
            try
            {
                _strBd.AppendFormat("DELETE FROM Instrument WHERE {0}", mainkey);
                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);

                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="sok"></param>
        /// <param name="Chknum"></param>
        /// <param name="errMsg"></param>
        public void UpdateUserInfo(string uname, string upwd,string utype,string id, out string errMsg)
        {
            errMsg = string.Empty;
            _strBd.Length = 0;
            int rtn = 0;
            try
            {
                _strBd.Append("update UserSet ");
                _strBd.Append("set userlog='");
                _strBd.Append(uname);
                _strBd.Append("',passData='");
                _strBd.Append(upwd);
                //_strBd.Append("'");
                _strBd.Append("',usertype='");
                _strBd.Append(utype);
                _strBd.Append("' where ID=");
                _strBd.Append(Convert.ToInt32(id));

                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }

        }
        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetUser(string sql, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dtb1 = null;
            _strBd.Length = 0;
            try
            {
                _strBd.Append("SELECT userlog,passData,usertype,ID");
                _strBd.Append(" FROM ");
                _strBd.Append("UserSet ");
                _strBd.Append(sql);
                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "UserSet" };
                dtb1 = DataBase.GetDataSet(cmd, names, out errMsg).Tables["UserSet"];
                _strBd.Length = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dtb1;
        }
        /// <summary>
        /// 查询服务器配置信息
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetServer(string sql, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dtb1 = null;
            _strBd.Length = 0;
            try
            {
                _strBd.Append("SELECT isselect,webAddress,name,pd,productor");
                _strBd.Append(" FROM ");
                _strBd.Append("webServer ");
                _strBd.Append(sql);
                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "webServer" };
                dtb1 = DataBase.GetDataSet(cmd, names, out errMsg).Tables["webServer"];
                _strBd.Length = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dtb1;
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="name"></param>
        /// <param name="pw"></param>
        /// <param name="pd"></param>
        /// <returns></returns>
        public int insertServer(string sel ,string addr, string name, string pw,string pd)
        {
            string errMsg = string.Empty;
            _strBd.Length = 0;
            int rtn = 0;
            try
            {
                _strBd.Append("INSERT INTO webServer ");
                _strBd.Append("(isselect,webAddress,name,pd,productor");
                _strBd.Append(")");
                _strBd.Append("VALUES('");
                _strBd.Append(sel=="False"?"否":"是");
                _strBd.Append("','");
                _strBd.Append(addr);
                _strBd.Append("','");
                _strBd.Append(name);
                _strBd.Append("','");
                _strBd.Append(pw);
                _strBd.Append("','");
                _strBd.Append(pd);
                _strBd.Append("')");
                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }
        /// <summary>
        /// 删除仪器信息
        /// </summary>
        /// <param name="mainkey"></param>
        /// <returns></returns>
        public int deleteServer(string mainkey)
        {
            string errMsg = string.Empty;
            int rtn = 0;
            _strBd.Length = 0;
            try
            {
                _strBd.AppendFormat("DELETE FROM webServer WHERE {0}", mainkey);
                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);

                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }
        /// <summary>
        /// 插入日记
        /// </summary>
        /// <param name="T"></param>
        /// <param name="D"></param>
        /// <param name="R"></param>
        /// <returns></returns>
        public int insertDairy(string T, string D, string R)
        {
            string errMsg = string.Empty;
            _strBd.Length = 0;
            int rtn = 0;
            try
            {
                _strBd.Append("INSERT INTO diary ");
                _strBd.Append("(worktime,details,remark");
                _strBd.Append(")");
                _strBd.Append("VALUES('");
                _strBd.Append(T);
                _strBd.Append("','");
                _strBd.Append(D);
                _strBd.Append("','");
                _strBd.Append(R);
                _strBd.Append("')");
                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }
        /// <summary>
        /// 查询操作记录
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetDiary(string sql, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dtb1 = null;
            _strBd.Length = 0;
            try
            {
                _strBd.Append("SELECT worktime,details,remark");
                _strBd.Append(" FROM ");
                _strBd.Append("diary ");
                _strBd.Append(sql);
                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "diary" };
                dtb1 = DataBase.GetDataSet(cmd, names, out errMsg).Tables["diary"];
                _strBd.Length = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dtb1;

        }
        /// <summary>
        /// 保存LZ2000读回的临时数据
        /// </summary>
        /// <param name="save"></param>
        /// <param name="num"></param>
        /// <param name="item"></param>
        /// <param name="data"></param>
        /// <param name="unit"></param>
        /// <param name="result"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public int insertTemp(string save, string num, string item,string data,string unit,string result,string time)
        {
            string errMsg = string.Empty;
            _strBd.Length = 0;
            int rtn = 0;
            try
            {
                _strBd.Append("INSERT INTO TempResult ");
                _strBd.Append("(Save,ChkNum,ChkItem,ChkResult,Unit,Resut,ChkTime");
                _strBd.Append(")");
                _strBd.Append("VALUES('");
                _strBd.Append(save);
                _strBd.Append("','");
                _strBd.Append(num);
                _strBd.Append("','");
                _strBd.Append(item);
                _strBd.Append("','");
                _strBd.Append(data);
                _strBd.Append("','");
                _strBd.Append(unit);
                _strBd.Append("','");
                _strBd.Append(result);
                _strBd.Append("','");
                _strBd.Append(time);
                _strBd.Append("')");
                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }
        /// <summary>
        /// 查询操作记录
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetTempData(string sql, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dtb1 = null;
            _strBd.Length = 0;
            try
            {
                _strBd.Append("SELECT Save,ChkNum,ChkItem,ChkResult,Unit,Resut,ChkTime");
                _strBd.Append(" FROM ");
                _strBd.Append("TempResult ");
                _strBd.Append(sql);
                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "TempResult" };
                dtb1 = DataBase.GetDataSet(cmd, names, out errMsg).Tables["TempResult"];
                _strBd.Length = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dtb1;
        }
        /// <summary>
        /// 保存新增用户
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <param name="Utype"></param>
        /// <returns></returns>
        public int AddUser(string name, string password, string Utype)
        {
          
            string errMsg = string.Empty;
            _strBd.Length = 0;
            int rtn = 0;
            try
            {
                _strBd.Append("INSERT INTO UserSet ");
                _strBd.Append("(userlog,passData,usertype");
                _strBd.Append(")");
                _strBd.Append("VALUES('");
                _strBd.Append(name);
                _strBd.Append("','");
                _strBd.Append(password);
                _strBd.Append("','");
                _strBd.Append(Utype);
                _strBd.Append("')");
                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }
        /// <summary>
        /// 删除登录用户
        /// </summary>
        /// <param name="mainkey"></param>
        /// <returns></returns>
        public int deletetUser(string mainkey)
        {
            string errMsg = string.Empty;
            int rtn = 0;
            _strBd.Length = 0;
            try
            {
                _strBd.AppendFormat("DELETE FROM UserSet WHERE {0}", mainkey);
                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);

                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }
        /// <summary>
        /// 更新临时数据库
        /// </summary>
        /// <param name="SR"></param>
        /// <param name="errMsg"></param>
        public void UpdateTempResult(string sok,string Chknum, out string errMsg)
        {
            errMsg = string.Empty;
            _strBd.Length = 0;
            int rtn = 0;
            try
            {
                _strBd.Append("update TempResult ");
                _strBd.Append("set Save='");
                _strBd.Append(sok);
                _strBd.Append("'");                     
                _strBd.Append("' where ChkNum='");
                _strBd.Append(Chknum);

                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }

        }
        /// <summary>
        /// 查询是否以保存记录
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetResData(string sql, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dtb1 = null;
            _strBd.Length = 0;
            try
            {
                _strBd.Append("SELECT *");
                _strBd.Append(" FROM ");
                _strBd.Append("CheckResult ");
                _strBd.Append(sql);
                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "TempResult" };
                dtb1 = DataBase.GetDataSet(cmd, names, out errMsg).Tables["TempResult"];
                _strBd.Length = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dtb1;

        }
        /// <summary>
        /// 查询数据是否保存
        /// </summary>
        /// <param name="swhere"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetSave(string swhere, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dtb1 = null;
            _strBd.Length = 0;
            try
            {
                _strBd.Append("SELECT * FROM CheckResult");
                if (swhere != "")
                {
                    _strBd.Append(" where ");
                    _strBd.Append(swhere);
                }
               
                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "Result" };
                dtb1 = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Result"];
                _strBd.Length = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dtb1;
        }

        /// <summary>
        /// 判断数据是否保存过
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public bool IsExist(string strWhere)
        {
            string errMsg = string.Empty;
            _strBd.Length = 0;
            _strBd.Append("SELECT * FROM CheckResult ");
            if (strWhere.Length > 0)
            {
                _strBd.Append(" WHERE ");
                _strBd.Append(strWhere);
            }
            object obj = DataBase.GetOneValue(_strBd.ToString(), out errMsg);
            if (errMsg != string.Empty)
            {
                throw new Exception(errMsg);
            }
            if (obj != null && obj != DBNull.Value )
            {
                //if (((int)obj) > 0)
                //{
                //   return true; 
                //}
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// 插入新测试仪器
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <param name="Utype"></param>
        /// <returns></returns>
        public int UpdateIntrument(string name)
        {

            string errMsg = string.Empty;
            _strBd.Length = 0;
            int rtn = 0;
            try
            {
                _strBd.Append("INSERT INTO mProject ");
                _strBd.Append("(TestIntrument");
                _strBd.Append(")");
                _strBd.Append("VALUES('");
                _strBd.Append(name);
                //_strBd.Append("','");
                //_strBd.Append(password);
                //_strBd.Append("','");
                //_strBd.Append(Utype);
                _strBd.Append("')");
                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }
       
        /// <summary>
        /// 更新网络设置
        /// </summary>
        /// <param name="sok"></param>
        /// <param name="Chknum"></param>
        /// <param name="errMsg"></param>
        public void SetWebServerce(string sok, string Chknum, out string errMsg)
        {
            errMsg = string.Empty;
            _strBd.Length = 0;
            int rtn = 0;
            try
            {
                _strBd.Append("update webServer ");
                _strBd.Append("set isselect='");
                _strBd.Append(sok == "True" ? "是" : "否");
                _strBd.Append("'");
                _strBd.Append(" where Name='");
                _strBd.Append(Chknum);
                _strBd.Append("'");
                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
        }
        /// <summary>
        /// 更新仪器数据库
        /// </summary>
        /// <param name="SR"></param>
        /// <param name="errMsg"></param>
        public void SetIntrument(string sok, string Chknum, out string errMsg)
        {
            errMsg = string.Empty;
            _strBd.Length = 0;
            int rtn = 0;
            try
            {
                _strBd.Append("update Instrument ");
                _strBd.Append("set Isselect='");
                _strBd.Append(sok=="True"? "是":"否");
                _strBd.Append("'");
                _strBd.Append(" where Numbering='");
                _strBd.Append(Chknum);
                _strBd.Append("'");
                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
        }

        /// <summary>
        /// 修改仪器名称
        /// </summary>
        /// <param name="sok"></param>
        /// <param name="com"></param>
        /// <param name="name"></param>
        /// <param name="errMsg"></param>
        public void ModifeIntrument(string data, string swhere, out string errMsg)
        {
            errMsg = string.Empty;
            _strBd.Length = 0;
            int rtn = 0;
            try
            {
                _strBd.Append("update Instrument ");
                _strBd.Append("set ");
                _strBd.Append(data);              
                _strBd.Append("'");
                _strBd.Append(" where ID=");
                _strBd.Append( Convert.ToInt32( swhere));
                _strBd.Append("");
                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }

        }
        /// <summary>
        /// 修改仪器名称
        /// </summary>
        /// <param name="sok"></param>
        /// <param name="com"></param>
        /// <param name="name"></param>
        /// <param name="errMsg"></param>
        public void RepairIntrument(string sok, string com,string name,string protoco, out string errMsg)
        {
            errMsg = string.Empty;
            _strBd.Length = 0;
            int rtn = 0;
            try
            {
                _strBd.Append("update Instrument ");
                _strBd.Append("set Manufacturer='");
                _strBd.Append(sok);
                _strBd.Append("'");
                _strBd.Append(",");
                _strBd.Append("communication=");
                _strBd.Append("'");
                _strBd.Append(com);
                _strBd.Append("'");
                _strBd.Append(",");
                _strBd.Append("Protocol='");
                _strBd.Append(protoco);
                _strBd.Append("'");
                _strBd.Append(" where Name='");
                _strBd.Append(name);
                _strBd.Append("'");
                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }

        }
        /// <summary>
        /// 获取样品名称
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetSample(string sql,string orderby, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dtb1 = null;
            _strBd.Length = 0;
            try
            {
                //_strBd.Append("SELECT SysCode,StdCode,Name,ShortCut,CheckLevel,CheckItemCodes");
                //_strBd.Append(",CheckItemValue,IsLock,IsReadOnly,Remark,FoodProperty FROM CheckSample");
                _strBd.Append("SELECT FtypeNmae,SampleNum,Name,ItemDes,StandardValue,Demarcate,Unit,SaveType,UDate,idx");
                _strBd.Append(" FROM tStandSample");
               
                if (!sql.Equals(""))
                {
                    _strBd.Append(" WHERE ");
                    _strBd.Append(sql);
                }
                if (!orderby.Equals(""))
                {
                    _strBd.Append(" ORDER BY ");
                    _strBd.Append(orderby);
                    _strBd.Append(" ASC ");
                }
               
                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "CheckSample" };
                dtb1 = DataBase.GetDataSet(cmd, names, out errMsg).Tables["CheckSample"];
                
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dtb1;
        }
        /// <summary>
        /// 保存基本信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int SaveInformation(string name,out string errMsg)
        {
             errMsg = string.Empty;
            _strBd.Length = 0;
            int rtn = 0;
            try
            {
                _strBd.Append("INSERT INTO BasicInformation (TestUnitName,TestUnitAddr,Tester,DetectUnitName,DetectUnitNature,ProductAddr,ProductCompany)");
                _strBd.AppendFormat("VALUES({0})",name );

                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }
        /// <summary>
        /// 查询被检单位
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="order"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetAHCompany(string sql,string order,out string errMsg)
        {
             errMsg = string.Empty;
            DataTable dtb1 = null;
            _strBd.Length = 0;
            try
            {
                _strBd.Append("SELECT isShows,FullName,Incorporator,Address,LinkMan,LinkInfo,ID FROM tCompany");

                if (!sql.Equals(""))
                {
                    _strBd.Append(" WHERE ");
                    _strBd.Append(sql);
                }
                if (!order.Equals(""))
                {
                    _strBd.Append(" ORDER BY ");
                    _strBd.Append(order);
                    _strBd.Append(" ASC ");
                }

                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "tCompany" };
                dtb1 = DataBase.GetDataSet(cmd, names, out errMsg).Tables["tCompany"];
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dtb1;
        }
        /// <summary>
        /// 获取基本信息
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="orderby"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetInformation(string sql, string orderby, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dtb1 = null;
            _strBd.Length = 0;
            try
            {
                //_strBd.Append("SELECT TestUnitName,TestUnitAddr,DetectUnitName,SampleAddress,QuantityIn,SampleNum,TestBase");
                //_strBd.Append(",SampleTime,Tester,iChecked,ID FROM BasicInformation");
                _strBd.Append("SELECT * FROM BasicInformation");

                if (!sql.Equals(""))
                {
                    _strBd.Append(" WHERE ");
                    _strBd.Append(sql);
                }
                if (!orderby.Equals(""))
                {
                    _strBd.Append(" ORDER BY ");
                    _strBd.Append(orderby);
                    _strBd.Append(" ASC ");
                }

                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "BasicInformation" };
                dtb1 = DataBase.GetDataSet(cmd, names, out errMsg).Tables["BasicInformation"];
             
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dtb1;
        }
        /// <summary>
        /// 查询北海样品数据
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderby"></param>
        /// <param name="typr"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetTestData(string where, string orderby, int type, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dtbl = null;
            try
            {
                if (type == 1)
                {
                    _strBd.Length = 0;
                    _strBd.Append("SELECT * FROM BeiHaiSample");
                }
                else if (type == 2)
                {

                }
                if (where != "")
                {
                    _strBd.Append(" WHERE ");
                    _strBd.Append(where);
                }
                if (orderby != "")
                {
                    _strBd.Append(" ORDER BY ");
                    _strBd.Append(orderby);
                    _strBd.Append(" ASC ");
                }

                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "sample" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["sample"];

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dtbl;
        }
        /// <summary>
        /// 北海插入下载数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int InsertTestSample(TestSamples model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            _strBd.Length = 0;
            try
            {
                _strBd.Append("Insert Into BeiHaiSample(productId,goodsName,operateId,operateName,marketId");
                _strBd.Append(",marketName,samplingPerson,samplingTime,positionAddress,goodsId,IsTest)VALUES(");
                _strBd.AppendFormat("'{0}',", model.productId);
                _strBd.AppendFormat("'{0}',", model.goodsName);
                _strBd.AppendFormat("'{0}',", model.operateId);
                _strBd.AppendFormat("'{0}',", model.operateName);
                _strBd.AppendFormat("'{0}',", model.marketId);
                _strBd.AppendFormat("'{0}',", model.marketName);
                _strBd.AppendFormat("'{0}',", model.samplingPerson);
                _strBd.AppendFormat("'{0}',", model.samplingTime);
                _strBd.AppendFormat("'{0}',", model.positionAddress);
                _strBd.AppendFormat("'{0}',", model.goodsId);
                _strBd.AppendFormat("'{0}')", "否");

                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return rtn;
        }
        /// <summary>
        /// 删除下载的全部数据
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderby"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int DeleteBeiHaiData(string where, string orderby, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            _strBd.Length = 0;
            try
            {
                _strBd.Append("Delete from BeiHaiSample");

                if (where != "")
                {
                    _strBd.Append(" where ");
                    _strBd.Append(where);
                }
                if (orderby != "")
                {
                    _strBd.Append(" order by ");
                    _strBd.Append(orderby);
                    _strBd.Append(" asc ");
                }

                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return rtn;
        }
        public int UpdateBHSample(string data, string where, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            _strBd.Length = 0;
            try
            {
                _strBd.Append("UPDATE BeiHaiSample SET IsTest=");
                _strBd.AppendFormat("'{0}'", data);
                _strBd.AppendFormat(" WHERE ID={0}",where);
                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return rtn;
        }
        /// <summary>
        /// 判断是否存在符合某个条件的记录
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public bool IsExistResult(string strWhere)
        {
            string errMsg = string.Empty;
            _strBd.Length = 0;
            _strBd.Append("SELECT COUNT(1) FROM CheckResult ");
            if (strWhere.Length > 0)
            {
                _strBd.Append(" WHERE ");
                _strBd.Append(strWhere);
            }
            object obj = DataBase.GetOneValue(_strBd.ToString(), out errMsg);
            if (errMsg != string.Empty)
            {
                throw new Exception(errMsg);
            }
            if (obj != null && obj != DBNull.Value)
            {
                return ((int)obj) > 0;
            }
            return false;
        }

        public void UpdatetCompany(string id,string value)
        {
            string errMsg = string.Empty;
            try
            {
                _strBd.Length =0;
                _strBd.Append("update tCompany set ");
                _strBd.AppendFormat("isShows={0}",Convert.ToBoolean(value));
                if(id!="")
                {
                    _strBd.AppendFormat(" where ID={0}", id);
                }

                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
       
            }
            catch (Exception e)
            {
                
            }
        }
        

        /// <summary>
        /// 更新基本信息库
        /// </summary>
        /// <param name="sok"></param>
        /// <param name="Chknum"></param>
        /// <param name="errMsg"></param>
        public void updateBasicIn(string sok, string Chknum, string Addr, string tester, string dunit, string saddr,string productAddr,string productUnit, out string errMsg)
        {
            errMsg = string.Empty;
            _strBd.Length = 0;
            int rtn = 0;
            try
            {
                _strBd.Append("update BasicInformation ");
                _strBd.Append("set iChecked='");
                _strBd.Append(sok == "True" ? "是" : "否");
                _strBd.Append("'");
                _strBd.Append(" where TestUnitName='");
                _strBd.Append(Chknum);
                _strBd.Append("'");
                _strBd.Append(" and ");
                _strBd.AppendFormat("TestUnitAddr='{0}' and ", Addr);
                _strBd.AppendFormat("DetectUnitName='{0}' and ", dunit);
                _strBd.AppendFormat("DetectUnitNature='{0}' and ", saddr);
                _strBd.AppendFormat("Tester='{0}' and ", tester);
                _strBd.AppendFormat("ProductAddr='{0}' and ", productAddr);
                _strBd.AppendFormat("ProductCompany='{0}' ", productUnit);

                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
        }
        /// <summary>
        /// 删除基本信息记录
        /// </summary>
        /// <param name="mainkey"></param>
        /// <returns></returns>
        public int deletetInfo(string mainkey ,out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            _strBd.Length = 0;
            try
            {
                _strBd.AppendFormat("DELETE FROM BasicInformation WHERE {0}", mainkey);
                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }
        public DataTable GetItemID(string sql, string orderby, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dtb1 = null;
            _strBd.Length = 0;
            try
            {
                _strBd.Append("SELECT FtypeNmae,Name,ItemDes,StandardValue,Demarcate,Unit,idx");
                _strBd.Append(" FROM tStandSample");

                if (!sql.Equals(""))
                {
                    _strBd.Append(" WHERE ");
                    _strBd.Append(sql);
                }
                if (!orderby.Equals(""))
                {
                    _strBd.Append(" ORDER BY ");
                    _strBd.Append(orderby);
                    _strBd.Append(" ASC ");
                }

                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "BasicInformation" };
                dtb1 = DataBase.GetDataSet(cmd, names, out errMsg).Tables["BasicInformation"];
                _strBd.Length = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dtb1;
        }
        public DataTable GetDownItemID(string sql, string orderby, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dtb1 = null;
            _strBd.Length = 0;
            try
            {
                if (Global.Platform == "DYBus")
                {
                    _strBd.Append("SELECT ID,sampleName FROM ChkItemStandard");
                }
                else
                {
                    _strBd.Append("SELECT ID,FtypeNmae,idx FROM tStandSample");
                }

                if (!sql.Equals(""))
                {
                    _strBd.Append(" WHERE ");
                    _strBd.Append(sql);
                }
                if (!orderby.Equals(""))
                {
                    _strBd.Append(" ORDER BY ");
                    _strBd.Append(orderby);
                    _strBd.Append(" ASC ");
                }

                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "tStandSample" };
                dtb1 = DataBase.GetDataSet(cmd, names, out errMsg).Tables["tStandSample"];
                _strBd.Length = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dtb1;
        }
        /// <summary>
        /// 下载达元检测项目和标准
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="orderby"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetDownChkItem(string sql, string orderby, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dtb1 = null;
            _strBd.Length = 0;
            try
            {
                if (Global.Platform == "DYBus")
                {
                    _strBd.Append("SELECT sampleName,itemName,standardName,standardValue,checkSign,checkValueUnit");
                    _strBd.Append(" FROM ChkItemStandard");
                }
                else
                {
                    _strBd.Append("SELECT FtypeNmae,Name,ItemDes,StandardValue,Demarcate,Unit");
                    _strBd.Append(" FROM tStandSample");
                }

                if (!sql.Equals(""))
                {
                    _strBd.Append(" WHERE ");
                    _strBd.Append(sql);
                }
                if (!orderby.Equals(""))
                {
                    _strBd.Append(" ORDER BY ");
                    _strBd.Append(orderby);
                    _strBd.Append(" ASC ");
                }

                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "ChkItemStandard" };
                dtb1 = DataBase.GetDataSet(cmd, names, out errMsg).Tables["ChkItemStandard"];
        
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dtb1;
        }

        /// <summary>
        /// 获取检测项目到样品编辑
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="orderby"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetChkItem(string sql, string orderby, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dtb1 = null;
            _strBd.Length = 0;
            try
            {
                _strBd.Append("SELECT FtypeNmae,Name,ItemDes,StandardValue,Demarcate,Unit  FROM tStandSample");
                //_strBd.Append(" FROM tStandSample");

                if (!sql.Equals(""))
                {
                    _strBd.Append(" WHERE ");
                    _strBd.Append(sql);
                }
                if (!orderby.Equals(""))
                {
                    _strBd.Append(" ORDER BY ");
                    _strBd.Append(orderby);
                    _strBd.Append(" ASC ");
                }

                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "tStandSample" };
                dtb1 = DataBase.GetDataSet(cmd, names, out errMsg).Tables["tStandSample"];
                _strBd.Length = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dtb1;
        }

        public void updateunit(string repairdata, int Condition, out string errMsg)
        {
            errMsg = string.Empty;
            _strBd.Length = 0;
           
            try
            {
                _strBd.Append("update BasicInformation ");
                _strBd.Append("set ");
                _strBd.Append(repairdata);
                _strBd.Append(" where ID=");
                _strBd.Append(Condition);              
             
                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
              
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
        }

        public int SaveSample(string Sresult,string condition, int stype, out string errMsg)
        {
            //string errMsg = string.Empty;
            _strBd.Length = 0;
            int rtn = 0;
            try
            {
                if (stype == 0)
                {
                    _strBd.Append("INSERT INTO tStandSample ");
                    _strBd.Append("(FtypeNmae,SampleNum,Name,ItemDes,StandardValue,Demarcate,Unit");
                    _strBd.Append(")");
                    _strBd.Append("VALUES(");
                    _strBd.Append(Sresult);
                    _strBd.Append(")");
                }
                else if (stype == 1)
                {
                    _strBd.Append("update tStandSample ");
                    _strBd.Append("set ");
                    _strBd.Append(Sresult);
                    _strBd.Append(" where idx=");
                    _strBd.Append(condition);    
 
                }
                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }
        /// <summary>
        /// 修改达元的样品检测标准
        /// </summary>
        /// <param name="Sresult"></param>
        /// <param name="condition"></param>
        /// <param name="stype"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int SaveDYSample(string Sresult, string condition, int stype, out string errMsg)
        {
            //string errMsg = string.Empty;
            _strBd.Length = 0;
            int rtn = 0;
            try
            {
                if (Global.Platform == "DYBus")
                {
                    if (stype == 0)
                    {
                        _strBd.Append("INSERT INTO ChkItemStandard ");
                        _strBd.Append("(sampleName,sampleNum,itemName,standardName,standardValue,checkSign,checkValueUnit");
                        _strBd.Append(")");
                        _strBd.Append("VALUES(");
                        _strBd.Append(Sresult);
                        _strBd.Append(")");
                    }
                    else if (stype == 1)
                    {
                        _strBd.Append("update ChkItemStandard ");
                        _strBd.Append("set ");
                        _strBd.Append(Sresult);
                        _strBd.Append(" where ID=");
                        _strBd.Append(condition);

                    }
                }
                else
                {
                    if (stype == 0)
                    {
                        _strBd.Append("INSERT INTO tStandSample ");
                        _strBd.Append("(FtypeNmae,SampleNum,Name,ItemDes,StandardValue,Demarcate,Unit)");
                        _strBd.Append("VALUES(");
                        _strBd.Append(Sresult);
                        _strBd.Append(")");
                    }
                    else if (stype == 1)
                    {
                        _strBd.Append("update tStandSample set ");
                        _strBd.Append(Sresult);
                        _strBd.Append(" where idx=");
                        _strBd.Append(condition);

                    }
                }
                
                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }
        /// <summary>
        /// 查找数据库ID
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="orderby"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable isSaveID(string sql, string orderby, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dtb1 = null;
            _strBd.Length = 0;
            try
            {
                _strBd.Append("SELECT ID FROM CheckResult");

                if (!sql.Equals(""))
                {
                    _strBd.Append(" WHERE ");
                    _strBd.Append(sql);
                }
                if (!orderby.Equals(""))
                {
                    _strBd.Append(" ORDER BY ");
                    _strBd.Append(orderby);
                    _strBd.Append(" ASC ");
                }

                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "CheckResult" };
                dtb1 = DataBase.GetDataSet(cmd, names, out errMsg).Tables["CheckResult"];
                _strBd.Length = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dtb1;
        }

        public void RepairResult(clsUpdateData SR,int wID, out string errMsg)
        {
            errMsg = string.Empty;
            _strBd.Length = 0;
            int rtn = 0;
            try
            {
                _strBd.Append("update CheckResult ");
                _strBd.AppendFormat("set SampleName='{0}',", SR.ChkSample);
                _strBd.AppendFormat("Checkitem='{0}',", SR.Chkxiangmu);
                _strBd.AppendFormat("CheckData='{0}',", SR.result);
                _strBd.AppendFormat("Unit='{0}',", SR.unit);
                _strBd.AppendFormat("TestBase='{0}',", SR.Chktestbase);
                _strBd.AppendFormat("LimitData='{0}',", SR.ChklimitData);
                _strBd.AppendFormat("Machine='{0}',", SR.intrument);
                _strBd.AppendFormat("Result='{0}',", SR.conclusion);
                _strBd.AppendFormat("SampleTime='{0}',", SR.GetSampTime);
                _strBd.AppendFormat("SampleAddress='{0}',", SR.GetSampPlace);
                _strBd.AppendFormat("CheckUnit='{0}',", SR.ChkUnit);
                _strBd.AppendFormat("Tester='{0}',", SR.ChkPeople);
                _strBd.AppendFormat("DetectUnit='{0}',", SR.detectunit);
                _strBd.AppendFormat("CheckTime=#{0}#", DateTime.Parse(SR.ChkTime));
                _strBd.AppendFormat(",SampleNum='{0}',", SR.sampleNum);
                _strBd.AppendFormat("SampleCategory='{0}',", SR.sampletype);
                _strBd.AppendFormat("CompanyNeture='{0}',", SR.CheckCompanyNature);
                _strBd.AppendFormat("ProcodeCompany='{0}',", SR.ProductUnit);
                _strBd.AppendFormat("ProductPlace='{0}',", SR.ProductPlace );
                _strBd.AppendFormat("ProductDatetime='{0}',", SR.ProductDate);
                _strBd.AppendFormat("SendTestDate='{0}',", SR.SendDate);
                _strBd.AppendFormat("DoResult='{0}'", SR.DoResult);

                _strBd.AppendFormat(" where ID={0}", wID);
                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }

        }
        /// <summary>
        /// 修改上传标志
        /// </summary>
        public void SetUploadResult(string uid,out string errMsg)
        {
            errMsg = string.Empty;
            _strBd.Length = 0;
            try
            {
               
                _strBd.AppendFormat("update CheckResult set IsUpload='{0}'", "是");
                _strBd.AppendFormat(" where ID={0}",uid);
               
                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
        }

        public void SetUpLoadData(clsUpdateData SR, out string errMsg)
        {
             errMsg = string.Empty;
            _strBd.Length = 0;
            //int rtn = 0;
            try
            {
                _strBd.Append("update CheckResult ");
                _strBd.Append("set IsUpload='");
                _strBd.Append("是");
                _strBd.Append("'");
                _strBd.Append(" where ");
                _strBd.Append("CheckData='");
                _strBd.Append(SR.result);
                _strBd.Append("' and ");
                _strBd.Append("CheckTime=#");
                _strBd.Append(DateTime.Parse(SR.ChkTime));
                _strBd.Append("# and Machine='");
                _strBd.Append(SR.intrument);
                _strBd.Append("' and SampleName='");
                _strBd.Append(SR.ChkSample);
                _strBd.Append("' and Checkitem='");
                _strBd.Append(SR.Chkxiangmu);
                _strBd.Append("'");

                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
 
        }
        /// <summary>
        /// 查询检测标准
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="orderby"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetStandardValue(string sql, string orderby, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dtb1 = null;
            _strBd.Length = 0;
            try
            {
                _strBd.Append("SELECT FtypeNmae,SampleNum,Name,ItemDes,StandardValue,Demarcate");
                _strBd.Append(",Unit,SaveType,UDate FROM tStandSample");

                if (!sql.Equals(""))
                {
                    _strBd.Append(" WHERE ");
                    _strBd.Append(sql);
                }
                if (!orderby.Equals(""))
                {
                    _strBd.Append(" ORDER BY ");
                    _strBd.Append(orderby);
                    _strBd.Append(" ASC ");
                }

                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "tStandSample" };
                dtb1 = DataBase.GetDataSet(cmd, names, out errMsg).Tables["tStandSample"];
                _strBd.Length = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dtb1;
        }
        /// <summary>
        /// 删除被检单位
        /// </summary>
        /// <returns></returns>
        public int DeleteExamedUnit(string where ,string orderby,out string err)
        {
            int rtn = 0;
            err = string.Empty;
            _strBd.Length = 0;
            try
            {

                _strBd.Append("DELETE FROM ExamedUnit");
                if (where != "")
                {
                    _strBd.Append(" where ");
                    _strBd.Append(where);
                }
                if (orderby != "")
                {
                    _strBd.Append(" order by ");
                    _strBd.Append(orderby);
                }

                DataBase.ExecuteCommand(_strBd.ToString(), out err);
                rtn = 1;
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }

            return rtn;
        }
        public int InExamedUnit(Company Indata,out string err)
        {
            int rtn = 0;
            err = "";
            _strBd.Length = 0;
            try
            {
                _strBd.Append("INSERT INTO ExamedUnit (regId,regName,regAddress,regCorpName,organizationCode");
                _strBd.Append(",regPost,contactMan,contactPhone,uDate)VALUES('");
                _strBd.Append(Indata.regId);
                _strBd.Append("','");
                _strBd.Append(Indata.regName);
                _strBd.Append("','");
                _strBd.Append(Indata.regAddress);
                _strBd.Append("','");
                _strBd.Append(Indata.regCorpName);
                _strBd.Append("','");
                _strBd.Append(Indata.organizationCode );
                _strBd.Append("','");
                _strBd.Append(Indata.regPost );
                _strBd.Append("','");
                _strBd.Append(Indata.contactMan);
                _strBd.Append("','");
                _strBd.Append(Indata.contactPhone);
                _strBd.Append("',#");
                _strBd.Append(Indata.uDate);
                _strBd.Append("#)");
                DataBase.ExecuteCommand(_strBd.ToString(), out err);
                rtn = 1;
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }

            return rtn;
        }
        public DataTable GetResult(string wheresql, string orderby,int type, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dtb1 = null;
            _strBd.Length = 0;
            try
            {
                if(type ==1)
                {
                    _strBd.Append("SELECT * FROM  CheckResult ");
                    //_strBd.Append(",regPost,contactMan,contactPhone,uDate From ExamedUnit");
                }

                if (!wheresql.Equals(""))
                {
                    _strBd.Append(" WHERE ");
                    _strBd.Append(wheresql);
                }
                if (!orderby.Equals(""))
                {
                    _strBd.Append(" ORDER BY ");
                    _strBd.Append(orderby);
                    _strBd.Append(" ASC ");
                }

                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "checkResut" };
                dtb1 = DataBase.GetDataSet(cmd, names, out errMsg).Tables["checkResut"];
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }

            return dtb1;
        }

        /// <summary>
        /// 查询被检单位
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="orderby"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetExamedUnit(string sql, string orderby, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dtb1 = null;
            _strBd.Length = 0;
            try
            {
                _strBd.Append("SELECT ID,regId,regName,regAddress,regCorpName,organizationCode");
                _strBd.Append(",regPost,contactMan,contactPhone,uDate From ExamedUnit");

                if (!sql.Equals(""))
                {
                    _strBd.Append(" WHERE ");
                    _strBd.Append(sql);
                }
                if (!orderby.Equals(""))
                {
                    _strBd.Append(" ORDER BY ");
                    _strBd.Append(orderby);
                    _strBd.Append(" ASC ");
                }

                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "ExamedUnit" };
                dtb1 = DataBase.GetDataSet(cmd, names, out errMsg).Tables["ExamedUnit"];
                
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dtb1;
        }
        /// <summary>
        /// 删除检测项目和标准
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderby"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int DeleteItemStandard(string where, string orderby, out string err)
        {
            int rtn = 0;
            err = string.Empty;
            _strBd.Length = 0;
            try
            {
                _strBd.Append("DELETE FROM ChkItemStandard");
                if (where != "")
                {
                    _strBd.Append(" where ");
                    _strBd.Append(where);
                }
                if (orderby != "")
                {
                    _strBd.Append(" order by ");
                    _strBd.Append(orderby);
                }

                DataBase.ExecuteCommand(_strBd.ToString(), out err);
                rtn = 1;
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }

            return rtn;
        }
        /// <summary>
        /// 插入检测项目和标准
        /// </summary>
        /// <param name="Indata"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int InItemStandard(ItemAndStandard Indata, out string err)
        {
            int rtn = 0;
            err = "";
            _strBd.Length = 0;
            try
            {
                _strBd.Append("INSERT INTO ChkItemStandard (checkId,sampleName,sampleNum,itemName,standardName");
                _strBd.Append(",standardValue,checkSign,checkValueUnit,uDate)VALUES('");
                _strBd.Append(Indata.checkId);
                _strBd.Append("','");
                _strBd.Append(Indata.sampleName);
                _strBd.Append("','");
                _strBd.Append(Indata.sampleNum);
                _strBd.Append("','");
                _strBd.Append(Indata.itemName);
                _strBd.Append("','");
                _strBd.Append(Indata.standardName);
                _strBd.Append("','");
                _strBd.Append(Indata.standardValue);
                _strBd.Append("','");
                _strBd.Append(Indata.checkSign);
                _strBd.Append("','");
                _strBd.Append(Indata.checkValueUnit);
                _strBd.Append("',#");
                _strBd.Append(DateTime.Parse(Indata.uDate));
                _strBd.Append("#)");
                DataBase.ExecuteCommand(_strBd.ToString(), out err);
                rtn = 1;
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }

            return rtn;
        }
        /// <summary>
        /// 查询检测项目和检测标准
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="orderby"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetItemStandard(string sql, string orderby, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dtb1 = null;
            _strBd.Length = 0;
            try
            {
                _strBd.Append("SELECT ID,checkId,sampleName,sampleNum,itemName,standardName");
                _strBd.Append(",standardValue,checkSign,checkValueUnit,uDate From ChkItemStandard");

                if (!sql.Equals(""))
                {
                    _strBd.Append(" WHERE ");
                    _strBd.Append(sql);
                }
                if (!orderby.Equals(""))
                {
                    _strBd.Append(" ORDER BY ");
                    _strBd.Append(orderby);
                    _strBd.Append(" ASC ");
                }

                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "ExamedUnit" };
                dtb1 = DataBase.GetDataSet(cmd, names, out errMsg).Tables["ExamedUnit"];

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dtb1;
        }
        /// <summary>
        /// 删除原有的样品信息
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderby"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int DeleteSample(string where, string orderby, out string err)
        {
            int rtn = 0;
            err = string.Empty;
            _strBd.Length = 0;
            try
            {
                _strBd.Append("DELETE FROM Sample");
                if (where != "")
                {
                    _strBd.Append(" where ");
                    _strBd.Append(where);
                }
                if (orderby != "")
                {
                    _strBd.Append(" order by ");
                    _strBd.Append(orderby);
                }

                DataBase.ExecuteCommand(_strBd.ToString(), out err);
                rtn = 1;
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }

            return rtn;
        }
        public int InSample(Simple Indata, out string err)
        {
            int rtn = 0;
            err = "";
            _strBd.Length = 0;
            try
            {
                _strBd.Append("INSERT INTO Sample (foodId,foodCode,foodName,uDate");
                _strBd.Append(")VALUES('");
                _strBd.Append(Indata.foodId);
                _strBd.Append("','");
                _strBd.Append(Indata.foodCode);
                _strBd.Append("','");
                _strBd.Append(Indata.foodName);              
                _strBd.Append("',#");
                _strBd.Append(DateTime.Parse(Indata.uDate));
                _strBd.Append("#)");
                DataBase.ExecuteCommand(_strBd.ToString(), out err);
                rtn = 1;
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }

            return rtn;
        }
        /// <summary>
        /// 查询检测样品
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="orderby"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetSampleDetail(string sql, string orderby, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dtb1 = null;
            _strBd.Length = 0;
            try
            {
                _strBd.Append("SELECT ID,foodId,foodCode,foodName,uDate");
                _strBd.Append(" From Sample");

                if (!sql.Equals(""))
                {
                    _strBd.Append(" WHERE ");
                    _strBd.Append(sql);
                }
                if (!orderby.Equals(""))
                {
                    _strBd.Append(" ORDER BY ");
                    _strBd.Append(orderby);
                    _strBd.Append(" ASC ");
                }

                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "Sample" };
                dtb1 = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Sample"];

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dtb1;
        }
        /// <summary>
        /// 获取样品种类
        /// </summary>
        /// <returns></returns>
        public DataTable Getsampletype(string sql, string orderby, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dtb1 = null;
            _strBd.Length = 0;
            try
            {
                _strBd.Append("SELECT sampletype,samplename From SampleType");

                if (!sql.Equals(""))
                {
                    _strBd.Append(" WHERE ");
                    _strBd.Append(sql);
                }
                //if (!orderby.Equals(""))
                //{
                //    _strBd.Append(" ORDER BY ");
                //    _strBd.Append(orderby);
                //    _strBd.Append(" ASC ");
                //}
                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "Sample" };
                dtb1 = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Sample"];

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dtb1;
 
        }

        /// <summary>
        /// 获取请求时间
        /// </summary>
        /// <param name="whereSql"></param>
        /// <param name="orderBySql"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetRequestTime(string whereSql, string orderBySql, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dt = null;
            sb.Length = 0;
            try
            {
                //sql.Append("SELECT ID,MachineItem,CheckItem,CheckStandard,Foodtype,Foodtype,MachineTask,MachineNumTask FROM RequestTime");
                sb.Append("SELECT * FROM RequestTime");
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
                string[] names = new string[1] { "RequestTime" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["RequestTime"];
                sb.Length = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dt;
        }

        /// <summary>
        /// 插入请求时间
        /// </summary>
        /// <param name="wheresql"></param>
        /// <param name="type"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int InsertResquestTime(string data, string wheresql, string orderby, int type, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.Length = 0;
                if (type == 1)
                {
                    sql.AppendFormat("INSERT INTO RequestTime(RequestName,UpdateTime) VALUES({0}) ", data);
                }
                else if (type == 2)
                {
                    sql.AppendFormat("INSERT INTO RequestTime(RequestName,UpdateTime) VALUES({0})", data);
                }
                else if (type == 3)
                {
                    sql.AppendFormat("INSERT INTO RequestTime(RequestName,UpdateTime) VALUES({0})", data);
                }
                else if (type == 4)
                {
                    sql.AppendFormat("INSERT INTO RequestTime(RequestName,UpdateTime) VALUES({0})", data);
                }
                else if (type == 5)
                {
                    sql.AppendFormat("INSERT INTO RequestTime(RequestName,UpdateTime) VALUES({0})", data);
                }
                else if (type == 6)
                {
                    sql.AppendFormat("Update RequestTime Set CheckStandard ='{0}'", wheresql);
                }
                else if (type == 7)
                {
                    sql.AppendFormat("Update RequestTime Set Laws ='{0}'", wheresql);
                }
                else if (type == 8)
                {
                    sql.AppendFormat("Update RequestTime Set fooditem ='{0}'", wheresql);
                }
                else if (type == 9)
                {
                    sql.AppendFormat("Update RequestTime Set CheckItem ='{0}'", wheresql);
                }
                else if (type == 10)
                {
                    sql.AppendFormat("Update RequestTime Set SampleItemstd ='{0}'", wheresql);
                }
                if (wheresql != "")
                {
                    sql.AppendFormat(" where {0}", wheresql);
                }
                if (orderby != "")
                {
                    sql.AppendFormat(" order by {0} ", orderby);
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

        public int UpdateRequestTime(string data, string wheresql, string orderby, int type, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                StringBuilder sql = new StringBuilder();
                if (type == 1)//接收任务 任务更新
                {
                    sql.AppendFormat("Update RequestTime Set UpdateTime ='{0}' ", data);
                }
                else if (type == 2)//法律法规
                {
                    sql.AppendFormat("UPDATE RequestTime Set UpdateTime ='{0}'", data);
                }
                else if (type == 3)//样品检测项目标准
                {
                    sql.AppendFormat("Update RequestTime Set UpdateTime ='{0}'", data);
                }
                else if (type == 4)
                {
                    sql.AppendFormat("Update RequestTime Set BuinessTime ='{0}'", wheresql);
                }
                else if (type == 5)
                {
                    sql.AppendFormat("Update RequestTime Set Foodtype ='{0}'", wheresql);
                }
                else if (type == 6)
                {
                    sql.AppendFormat("Update RequestTime Set CheckStandard ='{0}'", wheresql);
                }
                else if (type == 7)
                {
                    sql.AppendFormat("Update RequestTime Set Laws ='{0}'", wheresql);
                }
                else if (type == 8)
                {
                    sql.AppendFormat("Update RequestTime Set fooditem ='{0}'", wheresql);
                }
                else if (type == 9)
                {
                    sql.AppendFormat("Update RequestTime Set CheckItem ='{0}'", wheresql);
                }
                else if (type == 10)
                {
                    sql.AppendFormat("Update RequestTime Set SampleItemstd ='{0}'", wheresql);
                }

                if (wheresql != "")
                {
                    sql.AppendFormat(" where {0}", wheresql);
                }
                if (orderby != "")
                {
                    sql.AppendFormat(" order by {0}", orderby);
                }

                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }
        /// <summary>
        /// 查询经营户
        /// </summary>
        /// <returns></returns>
        public DataTable getregulation(string where, string order, out string errMsg)
        {
            DataTable dt = null;
            errMsg = string.Empty;
            sb.Length = 0;
            try
            {
                sb.Append("select * from Regulatory");
                if (where != string.Empty)
                {
                    sb.Append(" Where ");
                    sb.Append(where);
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "Regulatory" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Regulatory"];
               
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }

            return dt;
        }
        /// <summary>
        /// 更新监管对象
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderby"></param>
        /// <param name="type"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int UpdateRegulatSel(string where,bool value, string orderby,int type, out string errMsg)
        {
            int rtn = 0;
          
            try
            {
                sb.Length = 0;
                sb.Append("UPDATE Regulatory SET ");
                if (type == 1)
                {
                    sb.AppendFormat("IsSelects={0}", value);   
                }

                if (where != "")
                {
                    sb.AppendFormat(" where {0} ",where);
                }
                if (orderby != "")
                {
                    sb.AppendFormat(" order by {0} ", orderby);
                }
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                rtn = 1;
            }
             catch (Exception e)
             {
                 //throw (e);
                 errMsg = e.Message;
             }
            return rtn;

        }

        public int UpdateRegulation(regulator model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sb.Length = 0;
                sb.Append("UPDATE Regulatory SET ");
                //sb.Append("remark,checked,delete_flag,sorting,create_by,create_date,update_by,update_date) ");
                //sb.Append("VALUES(");
                //sb.AppendFormat("'{0}',", model.id);
                sb.AppendFormat("depart_id='{0}',", model.depart_id);
                sb.AppendFormat("reg_name='{0}',", model.reg_name);
                sb.AppendFormat("reg_type='{0}',", model.reg_type);
                sb.AppendFormat("link_user='{0}',", model.link_user);
                sb.AppendFormat("link_phone='{0}',", model.link_phone);
                sb.AppendFormat("link_idcard='{0}',", model.link_idcard);
                sb.AppendFormat("fax='{0}',", model.fax);
                sb.AppendFormat("post='{0}',", model.post);
                sb.AppendFormat("region_id='{0}',", model.region_id);
                sb.AppendFormat("reg_address='{0}',", model.reg_address);
                sb.AppendFormat("place_x='{0}',", model.place_x);
                sb.AppendFormat("place_y='{0}',", model.place_y);
                sb.AppendFormat("remark='{0}',", model.remark);
                sb.AppendFormat("checked='{0}',", model.@checked);
                sb.AppendFormat("delete_flag='{0}',", model.delete_flag);
                sb.AppendFormat("sorting='{0}',", model.sorting);
                sb.AppendFormat("create_by='{0}',", model.create_by);
                sb.AppendFormat("create_date='{0}',", model.create_date);
                sb.AppendFormat("update_by='{0}',", model.update_by);
                sb.AppendFormat("update_date='{0}'", model.update_date);
                sb.AppendFormat(" WHERE rid='{0}'", model.id);

                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }
        public int DeleteRegulation(string where, string order, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sb.Length = 0;
                sb.Append("Delete from Regulatory ");

                if (where != "")
                {
                    sb.AppendFormat(" where '{0}'", where);
                }
                if (order != "")
                {
                    sb.AppendFormat(" order by '{0}' ", order);
                }

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
        /// 查询经营户
        /// </summary>
        /// <param name="sqlwhere"></param>
        /// <param name="order"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable getbusiness(string sqlwhere, string order, out string errMsg)
        {
            DataTable dt = null;
            errMsg = string.Empty;
            sb.Length = 0;
            try
            {
                sb.Append("select * from Business");
                if (sqlwhere != string.Empty)
                {
                    sb.Append(" Where ");
                    sb.Append(sqlwhere);
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "Business" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Business"];
                
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dt;
        }
        /// <summary>
        /// 更新经营户
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int Updatebusiness(Manbusiness model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("UPDATE Business SET ");
                //sql.Append("sorting,create_by,create_date,update_by,update_date) ");
                //sql.Append("VALUES(");
                //sql.AppendFormat("'{0}',", model.id);
                sql.AppendFormat("reg_id='{0}',", model.reg_id);
                sql.AppendFormat("ope_shop_name='{0}',", model.ope_shop_name);
                sql.AppendFormat("ope_shop_code='{0}',", model.ope_shop_code);
                sql.AppendFormat("ope_name='{0}',", model.ope_name);
                sql.AppendFormat("ope_idcard='{0}',", model.ope_idcard);
                sql.AppendFormat("ope_phone='{0}',", model.ope_phone);
                sql.AppendFormat("credit_rating='{0}',", model.credit_rating);
                sql.AppendFormat("monitoring_level='{0}',", model.monitoring_level);
                sql.AppendFormat("qrcode='{0}',", model.qrcode);
                sql.AppendFormat("remark='{0}',", model.remark);
                sql.AppendFormat("checked='{0}',", model.@checked);
                sql.AppendFormat("delete_flag='{0}',", model.delete_flag);
                sql.AppendFormat("sorting='{0}',", model.sorting);
                sql.AppendFormat("create_by='{0}',", model.create_by);
                sql.AppendFormat("create_date='{0}',", model.create_date);
                sql.AppendFormat("update_by='{0}',", model.update_by);
                sql.AppendFormat("update_date='{0}'", model.update_date);
                sql.AppendFormat(" where Bid='{0}'", model.id);

                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
               
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }

        public int DeleteBuiness(string where ,string order,out string errMsg)
        {

            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sb.Length = 0;
                sb.AppendFormat("Delete from Business ");
                if (where != "")
                {
                    sb.Append(" where ");
                    sb.Append(where);
                }
                if (order != "")
                {
                    sb.AppendFormat(" order by '{0}' desc", order);
                }
                
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);

                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }

            return rtn;
        }
        /// <summary>
        /// 插入经营户
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int Insertbusiness(Manbusiness model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("Insert Into Business (Bid,reg_id,ope_shop_name,ope_shop_code,ope_name,ope_idcard,ope_phone,credit_rating,monitoring_level,qrcode,remark,checked,delete_flag,");
                sql.Append("sorting,create_by,create_date,update_by,update_date) ");
                sql.Append("VALUES(");
                sql.AppendFormat("'{0}',", model.id);
                sql.AppendFormat("'{0}',", model.reg_id);
                sql.AppendFormat("'{0}',", model.ope_shop_name);
                sql.AppendFormat("'{0}',", model.ope_shop_code);
                sql.AppendFormat("'{0}',", model.ope_name);
                sql.AppendFormat("'{0}',", model.ope_idcard);
                sql.AppendFormat("'{0}',", model.ope_phone);
                sql.AppendFormat("'{0}',", model.credit_rating);
                sql.AppendFormat("'{0}',", model.monitoring_level);
                sql.AppendFormat("'{0}',", model.qrcode);
                sql.AppendFormat("'{0}',", model.remark);
                sql.AppendFormat("'{0}',", model.@checked);
                sql.AppendFormat("'{0}',", model.delete_flag);
                sql.AppendFormat("'{0}',", model.sorting);
                sql.AppendFormat("'{0}',", model.create_by);
                sql.AppendFormat("'{0}',", model.create_date);
                sql.AppendFormat("'{0}',", model.update_by);
                sql.AppendFormat("'{0}'", model.update_date);
                sql.Append(")");
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }

            return rtn;
        }
        public int InsertRegulation(regulator model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("Insert Into Regulatory (rid,depart_id,reg_name,reg_type,link_user,link_phone,link_idcard,fax,post,region_id,reg_address,place_x,place_y,");
                sql.Append("remark,checked,delete_flag,sorting,create_by,create_date,update_by,update_date) ");
                sql.Append("VALUES(");
                sql.AppendFormat("'{0}',", model.id);
                sql.AppendFormat("'{0}',", model.depart_id);
                sql.AppendFormat("'{0}',", model.reg_name);
                sql.AppendFormat("'{0}',", model.reg_type);
                sql.AppendFormat("'{0}',", model.link_user);
                sql.AppendFormat("'{0}',", model.link_phone);
                sql.AppendFormat("'{0}',", model.link_idcard);
                sql.AppendFormat("'{0}',", model.fax);
                sql.AppendFormat("'{0}',", model.post);
                sql.AppendFormat("'{0}',", model.region_id);
                sql.AppendFormat("'{0}',", model.reg_address);
                sql.AppendFormat("'{0}',", model.place_x);
                sql.AppendFormat("'{0}',", model.place_y);
                sql.AppendFormat("'{0}',", model.remark);
                sql.AppendFormat("'{0}',", model.@checked);
                sql.AppendFormat("'{0}',", model.delete_flag);
                sql.AppendFormat("'{0}',", model.sorting);
                sql.AppendFormat("'{0}',", model.create_by);
                sql.AppendFormat("'{0}',", model.create_date);
                sql.AppendFormat("'{0}',", model.update_by);
                sql.AppendFormat("'{0}'", model.update_date);
                sql.Append(")");
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
              
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }

            return rtn;
        }

        public DataTable getPerson(string sqlwhere, string order, out string errMsg)
        {
            DataTable dt = null;
            errMsg = string.Empty;
            try
            {
                sb.Length = 0;
                sb.Append("select * from Personal");
                if (sqlwhere != string.Empty)
                {
                    sb.Append(" Where ");
                    sb.Append(sqlwhere);
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "Personal" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Personal"];
                
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dt;
        }
        /// <summary>
        /// 更新监管人员信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int UpdatePerson(persons model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("Insert Into Personal (pid,reg_id,name,job_title,idcard,phone,proof_code,proof_sdate,proof_edate,remark,delete_flag,sorting,create_by,");
                sql.Append("create_date,update_by,update_date) ");
                sql.Append("VALUES(");
                sql.AppendFormat("'{0}',", model.id);
                sql.AppendFormat("'{0}',", model.reg_id);
                sql.AppendFormat("'{0}',", model.name);
                sql.AppendFormat("'{0}',", model.job_title);
                sql.AppendFormat("'{0}',", model.idcard);
                sql.AppendFormat("'{0}',", model.phone);
                sql.AppendFormat("'{0}',", model.proof_code);
                sql.AppendFormat("'{0}',", model.proof_sdate);
                sql.AppendFormat("'{0}',", model.proof_edate);
                sql.AppendFormat("'{0}',", model.remark);
                sql.AppendFormat("'{0}',", model.delete_flag);
                sql.AppendFormat("'{0}',", model.sorting);
                sql.AppendFormat("'{0}',", model.create_by);
                sql.AppendFormat("'{0}',", model.create_date);
                sql.AppendFormat("'{0}',", model.update_by);
                sql.AppendFormat("'{0}')", model.create_date);
              
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
              
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }

            return rtn;
        }
        /// <summary>
        /// 插入监管人员信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int InsertPerson(persons model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("Insert Into Personal (pid,reg_id,name,job_title,idcard,phone,proof_code,proof_sdate,proof_edate,remark,delete_flag,sorting,create_by,");
                sql.Append("create_date,update_by,update_date) VALUES(");
             
                sql.AppendFormat("'{0}',", model.id);
                sql.AppendFormat("'{0}',", model.reg_id);
                sql.AppendFormat("'{0}',", model.name);
                sql.AppendFormat("'{0}',", model.job_title);
                sql.AppendFormat("'{0}',", model.idcard);
                sql.AppendFormat("'{0}',", model.phone);
                sql.AppendFormat("'{0}',", model.proof_code);
                sql.AppendFormat("'{0}',", model.proof_sdate);
                sql.AppendFormat("'{0}',", model.proof_edate);
                sql.AppendFormat("'{0}',", model.remark);
                sql.AppendFormat("'{0}',", model.delete_flag);
                sql.AppendFormat("'{0}',", model.sorting);
                sql.AppendFormat("'{0}',", model.create_by);
                sql.AppendFormat("'{0}',", model.create_date);
                sql.AppendFormat("'{0}',", model.update_by);
                sql.AppendFormat("'{0}'", model.create_date);
                sql.Append(")");
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }

            return rtn;
        }

        public int DeletePerson(string where,string order,out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sb.Length = 0;
                sb.Append("delete  from Personal ");
                if(where !="")
                {
                    sb.AppendFormat(" where '{0}' ", where);
                }
                if(order !="")
                {
                    sb.AppendFormat(" order by '{0}' ", order);

                }

                DataBase.ExecuteCommand(sb.ToString(), out errMsg);

                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }

            return rtn;
        }

        public DataTable GetRegulator(string where,string order, int type)
        {
            string errMsg = string.Empty;
            DataTable dt = null;
            sb.Length = 0;
            try
            {
                if (type == 1)
                {
                    sb.Append("select r.IsSelects,r.reg_name,r.link_user,r.reg_address,r.link_phone,r.update_date,r.rid,r.ID,r.reg_type ");
                    sb.Append(" from Regulatory r ");//where b.reg_id=r.rid
                }
                else if (type == 2)
                {
                    sb.Append("SELECT * FROM tCompany");
                }

                if (where!="")
                {
                    sb.Append(" WHERE ");
                    sb.Append(where);
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "Company" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Company"];
           
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
        }

        public DataTable GetDetectItem(string whereSql, string orderBySql, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dt = null;
            sb.Length = 0;
            try
            {
                sb.Append("SELECT * FROM DetectItem");
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
                string[] names = new string[1] { "DetectItem" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["DetectItem"];
               
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dt;
        }
        /// <summary>
        /// 更新检测项目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateDetectItem(CheckItem model)
        {
            int rtn = 0;
            try
            {
                sb.Length = 0;

                sb.AppendFormat("UPDATE DetectItem SET detect_item_name='{0}',", model.detect_item_name);
                sb.AppendFormat("detect_item_typeid='{0}',", model.detect_item_typeid);
                sb.AppendFormat("standard_id='{0}',", model.standard_id);
                sb.AppendFormat("detect_sign='{0}',", model.detect_sign);
                sb.AppendFormat("detect_value='{0}',", model.detect_value);
                sb.AppendFormat("detect_value_unit='{0}',", model.detect_value_unit);
                sb.AppendFormat("checked='{0}',", model.@checked);
                sb.AppendFormat("cimonitor_level='{0}',", model.cimonitor_level);
                sb.AppendFormat("remark='{0}',", model.remark);
                sb.AppendFormat("delete_flag='{0}',", model.delete_flag);
                sb.AppendFormat("create_by='{0}',", model.create_by);
                sb.AppendFormat("create_date='{0}',", model.create_date);
                sb.AppendFormat("update_by='{0}',", model.update_by);
                sb.AppendFormat("update_date='{0}',", model.update_date);
                sb.AppendFormat("t_id='{0}',", model.t_id);
                sb.AppendFormat("t_item_name='{0}',", model.t_item_name);
                sb.AppendFormat("t_sorting='{0}',", model.t_sorting);
                sb.AppendFormat("t_remark='{0}',", model.t_remark);
                sb.AppendFormat("t_delete_flag='{0}',", model.t_delete_flag);
                sb.AppendFormat("t_create_by='{0}',", model.t_create_by);
                sb.AppendFormat("t_create_date='{0}',", model.t_create_date);
                sb.AppendFormat("t_update_by='{0}',", model.t_update_by);
                sb.AppendFormat("t_update_date='{0}'", model.t_update_date);

                sb.AppendFormat(" where cid='{0}'", model.id);

                DataBase.ExecuteCommand(sb.ToString());
              
                rtn = 1;

            }
            catch (Exception ex)
            {
                return rtn = 0;
            }
            return rtn;
        }
        /// <summary>
        /// 保存检测项目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int InsertDetectItem(CheckItem model)
        {
            int rtn = 0;
            try
            {
                sb.Length = 0;
                //sql.Append("");
                sb.Append("INSERT INTO DetectItem (cid ,detect_item_name,detect_item_typeid,standard_id,detect_sign,detect_value,detect_value_unit,checked,cimonitor_level,remark,delete_flag,");
                sb.Append("create_by,create_date,update_by,update_date,t_id,t_item_name,t_sorting,t_remark,t_delete_flag,t_create_by,t_create_date,t_update_by,t_update_date");
                sb.Append(")VALUES(");
                sb.AppendFormat("'{0}',", model.id);
                sb.AppendFormat("'{0}',", model.detect_item_name);
                sb.AppendFormat("'{0}',", model.detect_item_typeid);
                sb.AppendFormat("'{0}',", model.standard_id);
                sb.AppendFormat("'{0}',", model.detect_sign);
                sb.AppendFormat("'{0}',", model.detect_value);
                sb.AppendFormat("'{0}',", model.detect_value_unit);
                sb.AppendFormat("'{0}',", model.@checked);
                sb.AppendFormat("'{0}',", model.cimonitor_level);
                sb.AppendFormat("'{0}',", model.remark);
                sb.AppendFormat("'{0}',", model.delete_flag);
                sb.AppendFormat("'{0}',", model.create_by);
                sb.AppendFormat("'{0}',", model.create_date);
                sb.AppendFormat("'{0}',", model.update_by);
                sb.AppendFormat("'{0}',", model.update_date);
                sb.AppendFormat("'{0}',", model.t_id);
                sb.AppendFormat("'{0}',", model.t_item_name);
                sb.AppendFormat("'{0}',", model.t_sorting);
                sb.AppendFormat("'{0}',", model.t_remark);
                sb.AppendFormat("'{0}',", model.t_delete_flag);
                sb.AppendFormat("'{0}',", model.t_create_by);
                sb.AppendFormat("'{0}',", model.t_create_date);
                sb.AppendFormat("'{0}',", model.t_update_by);
                sb.AppendFormat("'{0}')", model.t_update_date);

                DataBase.ExecuteCommand(sb.ToString());
              
                rtn = 1;

            }
            catch (Exception ex)
            {
                return rtn = 0;
            }


            return rtn;
        }
        /// <summary>
        /// 任务表
        /// </summary>
        /// <param name="whereSql"></param>
        /// <param name="orderBySql"></param>
        /// <param name="type"></param>
        /// <returns></returns>
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
                else if (type == 3)
                {
                    sb.Append("SELECT ID FROM KTask");
                    //// SELECT CPCODE,CPTITLE,CPSDATE,CPEDATE,CPTPROPERTY,CPFROM,CPEDITOR,CPPORGID,CPPORG,CPEDDATE,PLANDETAIL,
                    ////PLANDCOUNT,BAOJINGTIME,(SELECT COUNT(*) FROM ttResultSecond WHERE CheckPlanCode=m.CPCODE ) AS v30,'未完成' as finish
                    //// FROM tTask as m where (cdate(BAOJINGTIME)>=#2015-10-26 22:21:23#)
                    //sb.Append(" SELECT CPCODE,CPTITLE,CPSDATE,CPEDATE,CPTPROPERTY,CPFROM,CPEDITOR,CPPORGID,CPPORG,CPEDDATE,PLANDETAIL,");
                    //sb.Append("PLANDCOUNT,BAOJINGTIME,(SELECT COUNT(*) FROM ttResultSecond WHERE CheckPlanCode=m.CPCODE ) AS v30,'未完成' as finish");
                    //sb.Append(",(v30/PLANDCOUNT) as Num  FROM tTask as m");
                }
                else if(type ==4)
                {
                    sb.Append("SELECT Tests,sample_code,item_name,food_name,s_reg_name,s_ope_shop_code,s_ope_shop_name,s_sampling_date,t_task_title,Checktype,ID FROM KTask");
                }
                else if(type ==5)//查询foodID
                {
                    sb.Append("SELECT food_id,dataType,td_id,s_ope_id,s_ope_shop_name,s_reg_id,t_id,t_task_title,sampling_id,tid FROM KTask");
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
               
            }
            catch (Exception e)
            {
                //Log.WriteLog("查询clsTask",e);
                errMsg = e.Message;
            }
            return dtbl;
        }

        public int InsertKTask(MMtask model)
        {
            int retn = 0;
            try
            {
                sb.Length = 0;
                //sql.Append("INSERT INTO KTask (taskid,taskname,taskdate,sampleid,sample,itemid,item,CompanyID,CheckCompany,shopkou,Testmokuai,s_sampling_no,Checktype)VALUES(");
                sb.Append("INSERT INTO KTask (tid,sampling_id,sample_code,food_id,food_name,sample_number,purchase_amount,sample_date,purchase_date,item_id,item_name,origin,supplier,supplier_address,supplier_person,supplier_phone,batch_number,status,recevie_device,ope_shop_name,remark,param1,param2,param3,s_id,s_sampling_no,s_sampling_date,s_point_id,s_reg_id,s_reg_name,s_reg_licence,s_reg_link_person,s_reg_link_phone,s_ope_id,s_ope_shop_code,s_ope_shop_name,s_qrcode,s_task_id,s_status,s_place_x,s_place_y,s_sampling_userid,s_sampling_username,s_ope_signature,s_create_by,s_create_date,s_update_by,s_update_date,s_sheet_address,s_param1,s_param2,s_param3,t_id,t_task_code,t_task_title,t_task_content,t_task_detail_pId,t_project_id,t_task_type,t_task_source");
                sb.Append(",t_task_status,t_task_total,t_sample_number,t_task_sdate,t_task_edate,t_task_pdate,t_task_fdate,t_task_departId,t_task_announcer,t_task_cdate,t_remark,t_view_flag,t_file_path,t_delete_flag,t_create_by,t_create_date,t_update_by,t_update_date,td_id,td_task_id,td_detail_code,td_sample_id,td_sample,td_item_id,td_item,td_task_fdate,td_receive_pointid,td_receive_point,td_receive_nodeid,td_receive_node,td_receive_userid,td_receive_username,td_receive_status,td_task_total,td_sample_number,td_remark,mokuai,UserName,dataType,IsReceive)VALUES(");
                sb.AppendFormat("'{0}',", model.id);
                sb.AppendFormat("'{0}',", model.sampling_id);
                sb.AppendFormat("'{0}',", model.sample_code);
                sb.AppendFormat("'{0}',", model.food_id);
                sb.AppendFormat("'{0}',", model.food_name);
                sb.AppendFormat("'{0}',", model.sample_number);
                sb.AppendFormat("'{0}',", model.purchase_amount);
                sb.AppendFormat("'{0}',", model.sample_date);
                sb.AppendFormat("'{0}',", model.purchase_date);
                sb.AppendFormat("'{0}',", model.item_id);
                sb.AppendFormat("'{0}',", model.item_name);//被检单位
                sb.AppendFormat("'{0}',", model.origin);
                sb.AppendFormat("'{0}',", model.supplier);
                sb.AppendFormat("'{0}',", model.supplier_address);
                sb.AppendFormat("'{0}',", model.supplier_person);
                sb.AppendFormat("'{0}',", model.supplier_phone);
                sb.AppendFormat("'{0}',", model.batch_number);
                sb.AppendFormat("'{0}',", model.status);
                sb.AppendFormat("'{0}',", model.recevie_device);
                sb.AppendFormat("'{0}',", model.ope_shop_name);
                sb.AppendFormat("'{0}',", model.remark);
                sb.AppendFormat("'{0}',", model.param1);
                sb.AppendFormat("'{0}',", model.param2);
                sb.AppendFormat("'{0}',", model.param3);
                sb.AppendFormat("'{0}',", model.s_id);
                sb.AppendFormat("'{0}',", model.s_sampling_no);
                sb.AppendFormat("'{0}',", model.s_sampling_date);//DateTime.Parse(model.s_sampling_date));//抽样时间
                sb.AppendFormat("'{0}',", model.s_point_id);
                sb.AppendFormat("'{0}',", model.s_reg_id);
                sb.AppendFormat("'{0}',", model.s_reg_name);
                sb.AppendFormat("'{0}',", model.s_reg_licence);
                sb.AppendFormat("'{0}',", model.s_reg_link_person);
                sb.AppendFormat("'{0}',", model.s_reg_link_phone);
                sb.AppendFormat("'{0}',", model.s_ope_id);
                sb.AppendFormat("'{0}',", model.s_ope_shop_code);
                sb.AppendFormat("'{0}',", model.s_ope_shop_name);
                sb.AppendFormat("'{0}',", model.s_qrcode);
                sb.AppendFormat("'{0}',", model.s_task_id);
                sb.AppendFormat("'{0}',", model.s_status);
                sb.AppendFormat("'{0}',", model.s_place_x);
                sb.AppendFormat("'{0}',", model.s_place_y);
                sb.AppendFormat("'{0}',", model.s_sampling_userid);
                sb.AppendFormat("'{0}',", model.s_sampling_username);
                sb.AppendFormat("'{0}',", model.s_ope_signature);
                sb.AppendFormat("'{0}',", model.s_create_by);
                sb.AppendFormat("'{0}',", model.s_create_date);
                sb.AppendFormat("'{0}',", model.s_update_by);
                sb.AppendFormat("'{0}',", model.s_update_date);
                sb.AppendFormat("'{0}',", model.s_sheet_address);
                sb.AppendFormat("'{0}',", model.s_param1);
                sb.AppendFormat("'{0}',", model.s_param2);
                sb.AppendFormat("'{0}',", model.s_param3);
                sb.AppendFormat("'{0}',", model.t_id);
                sb.AppendFormat("'{0}',", model.t_task_code);
                sb.AppendFormat("'{0}',", model.t_task_title);
                sb.AppendFormat("'{0}',", model.t_task_content);
                sb.AppendFormat("'{0}',", model.t_task_detail_pId);
                sb.AppendFormat("'{0}',", model.t_project_id);
                sb.AppendFormat("'{0}',", model.t_task_type);
                sb.AppendFormat("'{0}',", model.t_task_source);
                sb.AppendFormat("'{0}',", model.t_task_status);
                sb.AppendFormat("'{0}',", model.t_task_total);
                sb.AppendFormat("'{0}',", model.t_sample_number);
                sb.AppendFormat("'{0}',", model.t_task_sdate);
                sb.AppendFormat("'{0}',", model.t_task_edate);
                sb.AppendFormat("'{0}',", model.t_task_pdate);
                sb.AppendFormat("'{0}',", model.t_task_fdate);
                sb.AppendFormat("'{0}',", model.t_task_departId);
                sb.AppendFormat("'{0}',", model.t_task_announcer);
                sb.AppendFormat("'{0}',", model.t_task_cdate);
                sb.AppendFormat("'{0}',", model.t_remark);
                sb.AppendFormat("'{0}',", model.t_view_flag);
                sb.AppendFormat("'{0}',", model.t_file_path);
                sb.AppendFormat("'{0}',", model.t_delete_flag);
                sb.AppendFormat("'{0}',", model.t_create_by);
                sb.AppendFormat("'{0}',", model.t_create_date);
                sb.AppendFormat("'{0}',", model.t_update_by);
                sb.AppendFormat("'{0}',", model.t_update_date);
                sb.AppendFormat("'{0}',", model.td_id);
                sb.AppendFormat("'{0}',", model.td_task_id);
                sb.AppendFormat("'{0}',", model.td_detail_code);
                sb.AppendFormat("'{0}',", model.td_sample_id);
                sb.AppendFormat("'{0}',", model.td_sample);
                sb.AppendFormat("'{0}',", model.td_item_id);
                sb.AppendFormat("'{0}',", model.td_item);
                sb.AppendFormat("'{0}',", model.td_task_fdate);
                sb.AppendFormat("'{0}',", model.td_receive_pointid);
                sb.AppendFormat("'{0}',", model.td_receive_point);
                sb.AppendFormat("'{0}',", model.td_receive_nodeid);
                sb.AppendFormat("'{0}',", model.td_receive_node);
                sb.AppendFormat("'{0}',", model.td_receive_userid);
                sb.AppendFormat("'{0}',", model.td_receive_username);
                sb.AppendFormat("'{0}',", model.td_receive_status);
                sb.AppendFormat("'{0}',", model.td_task_total);
                sb.AppendFormat("'{0}',", model.td_sample_number);
                sb.AppendFormat("'{0}',", model.td_remark);
                sb.AppendFormat("'{0}',", model.mokuai);
                sb.AppendFormat("'{0}',", model.username);
                sb.AppendFormat("'{0}',", model.s_personal);
                sb.AppendFormat("'{0}')", "否");

                //sb.AppendFormat("'{0}')", "已接受");

                DataBase.ExecuteCommand(sb.ToString());
                
                retn = 1;
            }
            catch (Exception ex)
            {
                return retn = 0;
            }
            return retn;

        }


        //插入任务管理
        public int InsertTask(ManageTaskTest model, string user, out string errMsg)
        {
            int rtn = 0;
            errMsg = string.Empty;
            try
            {
                sb.Length = 0;
                sb.Append("INSERT INTO ManageTask(t_id,t_task_code,t_task_title,t_task_content,t_task_detail_pId,t_project_id,t_task_type,t_task_source,t_task_status,t_task_total,");
                sb.Append("t_sample_number,t_task_sdate,t_task_edate,t_task_pdate,t_task_fdate,t_task_departId,t_task_announcer,t_task_cdate,t_remark,t_view_flag,t_delete_flag,");
                sb.Append("t_create_by,t_create_date,t_update_by,t_update_date,d_id,d_task_id,d_detail_code,d_sample_id,d_sample,d_item_id,d_item,d_task_fdate,d_receive_pointid,");
                sb.Append("d_receive_point,d_receive_nodeid,d_receive_node,d_receive_userid,d_receive_username,d_receive_status,d_task_total,d_sample_number,d_remark,username)VALUES(");
                sb.AppendFormat("'{0}',", model.t_id);
                sb.AppendFormat("'{0}',", model.t_task_code);
                sb.AppendFormat("'{0}',", model.t_task_title);
                sb.AppendFormat("'{0}',", model.t_task_content);
                sb.AppendFormat("'{0}',", model.t_task_detail_pId);
                sb.AppendFormat("'{0}',", model.t_project_id);
                sb.AppendFormat("'{0}',", model.t_task_type);
                sb.AppendFormat("'{0}',", model.t_task_source);
                sb.AppendFormat("'{0}',", model.t_task_status);
                sb.AppendFormat("'{0}',", model.t_task_total);
                sb.AppendFormat("'{0}',", model.t_sample_number);
                sb.AppendFormat("'{0}',", model.t_task_sdate);
                sb.AppendFormat("'{0}',", model.t_task_edate);
                sb.AppendFormat("'{0}',", model.t_task_pdate);
                sb.AppendFormat("'{0}',", model.t_task_fdate);
                sb.AppendFormat("'{0}',", model.t_task_departId);
                sb.AppendFormat("'{0}',", model.t_task_announcer);
                sb.AppendFormat("'{0}',", model.t_task_cdate);
                sb.AppendFormat("'{0}',", model.t_remark);
                sb.AppendFormat("'{0}',", model.t_view_flag);
                sb.AppendFormat("'{0}',", model.t_delete_flag);
                sb.AppendFormat("'{0}',", model.t_create_by);
                sb.AppendFormat("'{0}',", model.t_create_date);
                sb.AppendFormat("'{0}',", model.t_update_by);
                sb.AppendFormat("'{0}',", model.t_update_date);
                sb.AppendFormat("'{0}',", model.d_id);
                sb.AppendFormat("'{0}',", model.d_task_id);
                sb.AppendFormat("'{0}',", model.d_detail_code);
                sb.AppendFormat("'{0}',", model.d_sample_id);
                sb.AppendFormat("'{0}',", model.d_sample);
                sb.AppendFormat("'{0}',", model.d_item_id);
                sb.AppendFormat("'{0}',", model.d_item);
                sb.AppendFormat("'{0}',", model.d_task_fdate);
                sb.AppendFormat("'{0}',", model.d_receive_pointid);
                sb.AppendFormat("'{0}',", model.d_receive_point);
                sb.AppendFormat("'{0}',", model.d_receive_nodeid);
                sb.AppendFormat("'{0}',", model.d_receive_node);
                sb.AppendFormat("'{0}',", model.d_receive_userid);
                sb.AppendFormat("'{0}',", model.d_receive_username);
                sb.AppendFormat("'{0}',", model.d_receive_status);
                sb.AppendFormat("'{0}',", model.d_task_total);
                sb.AppendFormat("'{0}',", model.d_sample_number);
                sb.AppendFormat("'{0}',", model.d_remark);
                sb.AppendFormat("'{0}')", user);

                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
               
                rtn = 1;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return rtn;
        }

        //查询任务管理
        public DataTable GetTestTask(string where, string orderby, int type, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dt = null;
            sb.Length = 0;
            try
            {
                if (type == 1)
                {
                    sb.Append ("Select * From ManageTask ");
                }
                else if (type == 2)
                {
                    sb.Append("Select t_task_title,t_task_type,t_task_source,d_item,d_sample,d_task_total,d_receive_node,t_create_date,t_task_edate,ID From ManageTask");
                }

                if (where != "")
                {
                    sb.Append(" where ");
                    sb.Append(where);
                }
                if (!orderby.Equals(string.Empty))
                {
                    sb.AppendFormat(" ORDER BY {0} desc", orderby);
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "ManageTask" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["ManageTask"];

            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
        }
        /// <summary>
        /// 更新食品种类
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public int Updatefoodtype(foodtype mode)
        {
            int rtn = 0;
            try
            {
                sb.Length = 0;
                sb.AppendFormat("UPDATE  foodlist SET food_name='{0}',", mode.food_name);
                sb.AppendFormat("food_name_en='{0}',", mode.food_name_en);
                sb.AppendFormat("food_name_other='{0}',", mode.food_name_other);
                sb.AppendFormat("parent_id='{0}',", mode.parent_id);
                sb.AppendFormat("cimonitor_level='{0}',", mode.cimonitor_level);
                sb.AppendFormat("sorting='{0}',", mode.sorting);
                sb.AppendFormat("checked='{0}',", mode.@checked);
                sb.AppendFormat("delete_flag='{0}',", mode.delete_flag);
                sb.AppendFormat("create_by='{0}',", mode.create_by);
                sb.AppendFormat("create_date='{0}',", mode.create_date);
                sb.AppendFormat("update_by='{0}',", mode.update_by);
                sb.AppendFormat("update_date='{0}',", mode.update_date);
                sb.AppendFormat("isFood='{0}'", mode.isFood);
                sb.AppendFormat(" where fid='{0}'", mode.id);

                DataBase.ExecuteCommand(sb.ToString());

                rtn = 1;
            }
            catch (Exception ex)
            {
                return rtn = 0;
            }
            return rtn;
        }
        /// <summary>
        /// 插入食品种类
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public int Insertfoodtype(foodtype mode)
        {
            int rtn = 0;
            try
            {
                sb.Length = 0;
                sb.Append("insert into  foodlist(fid,food_name,food_name_en,food_name_other,parent_id,cimonitor_level,sorting,checked,delete_flag,create_by,create_date,update_by,update_date,isFood)VALUES(");
                sb.AppendFormat("'{0}',", mode.id);
                sb.AppendFormat("'{0}',", mode.food_name);
                sb.AppendFormat("'{0}',", mode.food_name_en);
                sb.AppendFormat("'{0}',", mode.food_name_other);
                sb.AppendFormat("'{0}',", mode.parent_id);
                sb.AppendFormat("'{0}',", mode.cimonitor_level);
                sb.AppendFormat("'{0}',", mode.sorting);
                sb.AppendFormat("'{0}',", mode.@checked);
                sb.AppendFormat("'{0}',", mode.delete_flag);
                sb.AppendFormat("'{0}',", mode.create_by);
                sb.AppendFormat("'{0}',", mode.create_date);
                sb.AppendFormat("'{0}',", mode.update_by);
                sb.AppendFormat("'{0}',", mode.update_date);
                sb.AppendFormat("'{0}')", mode.isFood);

                DataBase.ExecuteCommand(sb.ToString());
                sb.Length = 0;
                rtn = 1;
            }
            catch (Exception ex)
            {
                return rtn = 0;
            }
            return rtn;

        }
        public DataTable Getfoodtype(string wheresb, string orderBysb, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dt = null;
            sb.Length = 0;
            try
            {
                sb.Append("SELECT * FROM foodlist");
                if (!wheresb.Equals(string.Empty))
                {
                    sb.Append(" WHERE ");
                    sb.Append(wheresb);
                }
                if (!orderBysb.Equals(string.Empty))
                {
                    sb.Append(" ORDER BY ");
                    sb.Append(orderBysb);
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "foodlist" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["foodlist"];
                sb.Length = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dt;
        }
        public DataTable GetSamplestd(string wheresql, string orderby, int type, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dt = null;
            sb.Length = 0;
            try
            {
                if (type == 1)
                {
                    sb.Append("select f.food_name as foodname, d.detect_item_name as itemname,std.detect_sign,std.detect_value,std.detect_value_unit,std.update_date");
                    sb.Append(" from SampleStandard std ,foodlist f,DetectItem ");
                    sb.Append("d where f.fid=std.food_id and d.cid=std.item_id ");

                    //sb.Append("select std.food_id,std.item_id,std.detect_sign,std.detect_value,std.detect_value_unit,std.update_date,f.food_name as");
                    //sb.Append(" foodname,d.detect_item_name as itemname,sd.std_code as standard from SampleStandard std ,foodlist f,DetectItem ");
                    //sb.Append("d,StandardList sd where f.fid=std.food_id and d.cid=std.item_id and sd.sid=d.standard_id");
                }
                else if (type == 2)
                {
                    sb.Append("select f.food_name as foodname, d.detect_item_name as itemname,std.detect_sign,std.detect_value,std.detect_value_unit,std.update_date");
                    sb.Append(" from SampleStandard std ,foodlist f,DetectItem d ");
                }
                if (!wheresql.Equals(string.Empty))
                {
                    //sb.Append(" WHERE ");
                    sb.Append(wheresql);
                }
                if (!orderby.Equals(string.Empty))
                {
                    sb.Append(" order by ");
                    sb.Append(orderby);
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "SampleStandard" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["SampleStandard"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
        }
        public DataTable GetSampleStand(string whereSql, string orderBySql, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dt = null;
            sb.Length = 0;
            try
            {
                sb.Append("SELECT * FROM SampleStandard");
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
                string[] names = new string[1] { "SampleStandard" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["SampleStandard"];
               
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dt;
        }
        /// <summary>
        /// 更新样品检测项目标准
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateSampleStandard(fooditem model)
        {
            int retn = 0;
            try
            {
                sb.Length = 0;
                sb.AppendFormat("UPDATE SampleStandard SET food_id='{0}',", model.food_id);
                sb.AppendFormat("food_id1='{0}',", model.food_id1);
                sb.AppendFormat("item_id='{0}',", model.item_id);
                sb.AppendFormat("detect_sign='{0}',", model.detect_sign);
                sb.AppendFormat("detect_value='{0}',", model.detect_value);
                sb.AppendFormat("detect_value_unit='{0}',", model.detect_value_unit);
                sb.AppendFormat("remark='{0}',", model.remark);
                sb.AppendFormat("use_default='{0}',", model.use_default);
                sb.AppendFormat("checked='{0}',", model.@checked);
                sb.AppendFormat("delete_flag='{0}',", model.delete_flag);
                sb.AppendFormat("create_by='{0}',", model.create_by);
                sb.AppendFormat("create_date='{0}',", model.create_date);
                sb.AppendFormat("update_by='{0}',", model.update_by);
                sb.AppendFormat("update_date='{0}'", model.update_date);

                sb.AppendFormat(" where sid='{0}'", model.id);

                DataBase.ExecuteCommand(sb.ToString());
                retn = 1;
            }
            catch (Exception ex)
            {
                return retn = 0;
            }
            return retn;
        }
        /// <summary>
        /// 样品标准
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int InsertSampleStandard(fooditem model)
        {
            int retn = 0;
            try
            {
                sb.Length = 0;
                sb.Append("INSERT INTO SampleStandard (sid,food_id,item_id,detect_sign,detect_value,detect_value_unit,remark,use_default,checked,delete_flag,create_by,create_date,update_by,update_date)VALUES(");
                sb.AppendFormat("'{0}',", model.id);
                sb.AppendFormat("'{0}',", model.food_id);   
                sb.AppendFormat("'{0}',", model.item_id);
                sb.AppendFormat("'{0}',", model.detect_sign);
                sb.AppendFormat("'{0}',", model.detect_value);
                sb.AppendFormat("'{0}',", model.detect_value_unit);
                sb.AppendFormat("'{0}',", model.remark);
                sb.AppendFormat("'{0}',", model.use_default);
                sb.AppendFormat("'{0}',", model.@checked);
                sb.AppendFormat("'{0}',", model.delete_flag);
                sb.AppendFormat("'{0}',", model.create_by);
                sb.AppendFormat("'{0}',", model.create_date);
                sb.AppendFormat("'{0}',", model.update_by);
                sb.AppendFormat("'{0}')", model.update_date);

                DataBase.ExecuteCommand(sb.ToString());
               
                retn = 1;

            }
            catch (Exception ex)
            {
                return retn = 0;
            }
            return retn;
        }
        /// <summary>
        /// 导出数据到Excel
        /// </summary>
        /// <returns></returns>
        public DataTable ExportExcel(string whereSql, string orderBySql, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dt = null;
            sb.Length = 0;
            try
            {
                sb.Append("SELECT ChkNum as 检测编号,Checkitem as 检测项目,SampleName as 样品名称,CheckData as 检测结果,Unit as 单位,CheckTime as 检测时间,");
                sb.Append("Result as 结论,TestBase as 检测依据,LimitData as 标准值,Machine as 检测仪器,CheckUnit as 被检单位,DetectUnit as 检测单位,Tester as 检测员,");
                sb.Append("SampleTime as 采样时间,SampleAddress as 采样地点,SampleNum as 检测数量,IsUpload as 已上传,SampleCode as 样品编号,SampleCategory as 样品种类,sampleid as 样品ID,");
                sb.Append("MachineNum as 仪器编号,ProductPlace as 产地,ProductDatetime as 生产日期,Barcode as 条形码,ProduceUnit as 生产单位,ProcodeCompany as 生产企业,ProduceAddr as 产地地址,");
                sb.Append("SendTestDate as 送检日期,NumberUnit as 数量单位,CompanyNeture as 被检单位性质,DoResult as 处理结果,HoleNum as 通道号,StockIn as 进货数量,Xiguangdu as 吸光度,");
                sb.Append("ChkCompanyCode as 检测单位编号,itemCode as 检测项目编号,SampleCategoryCode as 样品种类编号,ChkMethod as 检测方法 ");
                sb.Append(" FROM CheckResult ");
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
                string[] names = new string[1] { "CheckResult" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["CheckResult"];

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dt;
        }

    }
}

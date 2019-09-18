using System;
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
        private DataTable dt = null;
        private string err = "";
        /// <summary>
        /// 插入串口数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        //public int Insert(clsSetCom model, out string errMsg)
        //{
        //    _strBd.Length = 0;
        //    errMsg = string.Empty;
        //    int rtn = 0;
        //    try
        //    {
        //        _strBd.Append("INSERT INTO tSetCom ");
        //        _strBd.Append("(ID,ComBaud,ComDataBit,ComParity,ComStopBit,");
        //        _strBd.Append("ComPort)");
        //        _strBd.Append("VALUES('");
        //        _strBd.Append("1");
        //        _strBd.Append("','");
        //        _strBd.Append(model.ComBaud);
        //        _strBd.Append("','");
        //        _strBd.Append(model.ComDataBit);
        //        _strBd.Append("','");
        //        _strBd.Append(model.ComParity);
        //        _strBd.Append("','");
        //        _strBd.Append(model.ComStopBit);
        //        _strBd.Append("','");
        //        _strBd.Append(model.ComPort);
        //        _strBd.Append("')");

        //        DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
        //        rtn = 1;
        //    }
        //    catch (Exception e)
        //    {
        //        errMsg = e.Message;
        //    }
        //    return rtn;
        //}
        /// <summary>
        /// 删除所有数据
        /// </summary>
        /// <param name="errMsg"></param>
        //public void DeleteAll(out string errMsg)
        //{
        //    _strBd.Length = 0;
        //    _strBd.Append("DELETE FROM tResult");
        //    DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
        //    _strBd.Length = 0;
        //}
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
            //string errMsg = string.Empty;
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
                dt = DataBase.GetDataSet(cmd, names, out err).Tables["tSetCom"];
                //_strBd.Length = 0;
            }
            catch (Exception e)
            {
                                      err = e.Message;
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
                _strBd.Append("IsUpload,SampleCode,SampleCategory,MachineNum,ProductPlace,ProductDatetime,Barcode,ProcodeCompany,ProduceAddr,ProduceUnit,SendTestDate,NumberUnit,CompanyNeture,DoResult,sampleid,HoleNum,BID");
                _strBd.Append(",MarketType,DABH,PositionNo,jingyinghuName,SubItemCode,SubItemName,QuickCheckItemCode,QuickCheckSubItemCode,QuickCheckUnitName,QuickCheckUnitId,ReviewIs,CheckCompanyCode,retester,beizhu)");
                _strBd.AppendFormat("VALUES('{0}',", result.CheckNumber);
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
                _strBd.AppendFormat("'{0}',", result.BID);
                _strBd.AppendFormat("'{0}',", result.markettype);
                _strBd.AppendFormat("'{0}',", result.sfz);
                _strBd.AppendFormat("'{0}',", result.stallnum);
                _strBd.AppendFormat("'{0}',", result.Operators);
                _strBd.AppendFormat("'{0}',", result.SampleCode);
                _strBd.AppendFormat("'{0}',", result.SampleName);
                _strBd.AppendFormat("'{0}',", result.Checkitemcode);
                _strBd.AppendFormat("'{0}',", result.CheckItemSmallcode);
                _strBd.AppendFormat("'{0}',", result.detectunit );
                _strBd.AppendFormat("'{0}',", result.detectunitNo);
                _strBd.AppendFormat("'{0}',", result.Retest);
                _strBd.AppendFormat("'{0}',", result.CheckCompanycode);
                _strBd.AppendFormat("'{0}',", result.retester);
                _strBd.AppendFormat("'{0}')", result.Beizhu);

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
                _strBd.Append("MarketType,DABH,PositionNo,jingyinghuName,SubItemCode,SubItemName,QuickCheckItemCode,QuickCheckSubItemCode,QuickCheckUnitName,QuickCheckUnitId,ReviewIs,CheckCompanyCode,retester,beizhu)");
                _strBd.Append("VALUES('");
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
                _strBd.AppendFormat("'{0}',", result.markettype);
                _strBd.AppendFormat("'{0}',", result.sfz);
                _strBd.AppendFormat("'{0}',", result.stallnum);
                _strBd.AppendFormat("'{0}',", result.Operators);
                _strBd.AppendFormat("'{0}',", result.SampleCode);
                _strBd.AppendFormat("'{0}',", result.SampleName);
                _strBd.AppendFormat("'{0}',", result.Checkitemcode);
                _strBd.AppendFormat("'{0}',", result.CheckItemSmallcode);
                _strBd.AppendFormat("'{0}',", result.detectunit);
                _strBd.AppendFormat("'{0}',", result.detectunitNo);
                _strBd.AppendFormat("'{0}',", result.Retest);
                _strBd.AppendFormat("'{0}',", result.CheckCompanycode);
                _strBd.AppendFormat("'{0}',", result.retester);
                _strBd.AppendFormat("'{0}')", result.Beizhu);

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
                _strBd.Append("SELECT IsUpload FROM CheckResult");
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
                _strBd.Append("SELECT ID,sampleName FROM ChkItemStandard");
                //_strBd.Append(" FROM ChkItemStandard");

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
                _strBd.Append("SELECT sampleName,itemName,standardName,standardValue,checkSign,checkValueUnit");
                _strBd.Append(" FROM ChkItemStandard");

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
                //_strBd.Length = 0;
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
                _strBd.AppendFormat("CheckTime=#{0}#,", DateTime.Parse(SR.ChkTime));
                //_strBd.AppendFormat(",SampleNum='{0}',", SR.sampleNum);
                //_strBd.AppendFormat("SampleCategory='{0}',", SR.sampletype);
                //_strBd.AppendFormat("CompanyNeture='{0}',", SR.CheckCompanyNature);
                //_strBd.AppendFormat("ProcodeCompany='{0}',", SR.ProductUnit);
                //_strBd.AppendFormat("ProductPlace='{0}',", SR.ProductPlace );
                //_strBd.AppendFormat("ProductDatetime='{0}',", SR.ProductDate);
                //_strBd.AppendFormat("SendTestDate='{0}',", SR.SendDate);
                //_strBd.AppendFormat("DoResult='{0}',", SR.DoResult);
                _strBd.AppendFormat("SubItemCode='{0}',", SR.ChkSampleCode);
                _strBd.AppendFormat("SampleCode='{0}',", SR.ChkSampleCode);
                _strBd.AppendFormat("retester='{0}',", SR.Retester);
                _strBd.AppendFormat("CheckCompanyCode='{0}',", SR.chkcompanycode);
                _strBd.AppendFormat("DABH='{0}',", SR.Shenfenzheng);
                _strBd.AppendFormat("QuickCheckUnitId='{0}',", SR.jigoubianhao);
                _strBd.AppendFormat("QuickCheckItemCode='{0}',", SR.ChkitemCode);
                _strBd.AppendFormat("QuickCheckSubItemCode='{0}',", SR.ChkitemsmallCode);
                _strBd.AppendFormat("ReviewIs='{0}',", SR.retest );
                _strBd.AppendFormat("jingyinghuName='{0}',", SR.Jingyinghu );
                _strBd.AppendFormat("PositionNo='{0}',", SR.stallnum );
                _strBd.AppendFormat("MarketType='{0}',", SR.markettype);
                _strBd.AppendFormat("beizhu='{0}'", SR.beizhu);

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
                _strBd.Append("update CheckResult ");
                _strBd.AppendFormat("set IsUpload='{0}'","是");
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
        /// 查询检测项目名称编号
        /// </summary>
        /// <param name="where"></param>
        /// <param name="MsgErr"></param>
        /// <returns></returns>
        public DataTable  GetKSItemcode(string where, out string MsgErr)
        {
            int rtn = 0;
            MsgErr = "";
            dt = null;
            try
            {
                MsgErr = "";
                _strBd.Length = 0;
                _strBd.Append("select * from KSItems ");
                if (where != "")
                {
                    _strBd.Append(" where ");
                    _strBd.Append(where);
                }
                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "KSItems" };
                dt = DataBase.GetDataSet(cmd, names, out MsgErr).Tables["KSItems"];
            }
            catch (Exception ex)
            {
                MsgErr = ex.Message;
            }
            return dt;
        }
        /// <summary>
        /// 删除检测项目
        /// </summary>
        /// <param name="MsgErr"></param>
        /// <returns></returns>
        public int DeleteItem(string where , out string MsgErr)
        {
            MsgErr = "";
            int rtn = 0;
            try
            {
                _strBd.Length = 0;
                _strBd.Append("DELETE FROM KSItems ");
                if(where !="")
                {
                    _strBd.Append(" where ");
                    _strBd.Append(where);
                }

                DataBase.ExecuteCommand(_strBd.ToString(), out MsgErr);
                rtn = 1;
            }
            catch (Exception ex)
            {
                MsgErr = ex.Message;
            }
            return rtn;
        }
        /// <summary>
        /// 昆山插入检测项目
        /// </summary>
        /// <param name="MsgErr"></param>
        /// <returns></returns>
        public int InKSItem(KunShanEntity.CheckItem model,out string MsgErr)
        {
            MsgErr = "";
            int rtn = 0;
            try
            {
                _strBd.Length = 0;
                _strBd.Append("INSERT INTO KSItems (IId,ItemCode,ItemName,SubItemCode,SubItemName,UpdateDate)VALUES(");
                _strBd.AppendFormat("'{0}',", model.Id);
                _strBd.AppendFormat("'{0}',", model.ItemCode );
                _strBd.AppendFormat("'{0}',", model.ItemName);
                _strBd.AppendFormat("'{0}',", model.SubItemCode);
                _strBd.AppendFormat("'{0}',", model.SubItemName);
                _strBd.AppendFormat("'{0}')", model.UpdateDate);

                DataBase.ExecuteCommand(_strBd.ToString(), out MsgErr);
                rtn = 1;
            }
            catch (Exception ex)
            {
                MsgErr = ex.Message;
            }
            return rtn;
        }
        /// <summary>
        /// 删除昆山样品
        /// </summary>
        /// <returns></returns>
        public int DeleteKSsample(string where, out string MsgErr)
        {
            MsgErr = "";
            int rtn = 0;
            try
            {
                _strBd.Length = 0;
                _strBd.Append("Delete from KSSamples ");
                if(where !="")
                {
                    _strBd.Append(" where ");
                    _strBd.Append(where);
                }
                DataBase.ExecuteCommand(_strBd.ToString(), out MsgErr);
                rtn = 1;
            }
            catch(Exception ex)
            {
                MsgErr = ex.Message;
            }
            return rtn;
        }
        /// <summary>
        /// 插入昆山样品信息
        /// </summary>
        /// <param name="MsgErr"></param>
        /// <returns></returns>
        public int InKSSamples(KunShanEntity.SalesItem model,out string MsgErr)
        {
            MsgErr = "";
            int rtn = 0;
            try
            {
                _strBd.Length = 0;
                _strBd.Append("INSERT INTO KSSamples (sId,ItemCode,ItemName,SubItemCode,SubItemName,UpdateDate)VALUES(");
                _strBd.AppendFormat("'{0}',",model.Id);
                _strBd.AppendFormat("'{0}',", model.ItemCode );
                _strBd.AppendFormat("'{0}',", model.ItemName );
                _strBd.AppendFormat("'{0}',", model.SubItemCode );
                _strBd.AppendFormat("'{0}',", model.SubItemName );
                _strBd.AppendFormat("'{0}')", model.UpdateDate );

                DataBase.ExecuteCommand(_strBd.ToString(), out MsgErr);
                rtn = 1;
            }
            catch (Exception ex)
            {
                MsgErr = ex.Message;
            }
            return rtn;
        }
        /// <summary>
        /// 查询昆山样品信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetKSsample(string where,string orderby, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dtb1 = null;
            _strBd.Length = 0;
            try
            {
                _strBd.Append("SELECT * FROM KSSamples ");

                if (!where.Equals(""))
                {
                    _strBd.Append(" WHERE ");
                    _strBd.Append(where);
                }
                if (!orderby.Equals(""))
                {
                    _strBd.Append(" ORDER BY ");
                    _strBd.Append(orderby);
                    _strBd.Append(" ASC ");
                }

                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "KSSamples" };
                dtb1 = DataBase.GetDataSet(cmd, names, out errMsg).Tables["KSSamples"];

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
        /// 查询主体信息
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderby"></param>
        /// <param name="MsgErr"></param>
        /// <returns></returns>
        public DataTable GetAreaMarket(string where, string orderby, out string MsgErr)
        {
            DataTable dtb1 = null;
            MsgErr = "";
            _strBd.Length = 0;
            try
            {
                _strBd.Append("SELECT * From KSAreaSignContact");

                if (!where.Equals(""))
                {
                    _strBd.Append(" WHERE ");
                    _strBd.Append(where);
                }
                if (!orderby.Equals(""))
                {
                    _strBd.Append(" ORDER BY ");
                    _strBd.Append(orderby);
                    _strBd.Append(" ASC ");
                }
                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "KSAreaMarket" };
                dtb1 = DataBase.GetDataSet(cmd, names, out MsgErr).Tables["KSAreaMarket"];
            }
            catch(Exception ex)
            {
                MsgErr = ex.Message;
            }
            return dtb1;
        }

        /// <summary>
        /// 昆山经营户信息
        /// </summary>
        /// <returns></returns>
        public int InKSAreaSignContact(KunShanEntity.SignContact model,out string MsgErr)
        {
            MsgErr = "";
            int rtn = 0;
            try
            {
                _strBd.Length = 0;
                _strBd.Append("INSERT INTO KSAreaSignContact (LicenseNo,MarketName,MarketRef,Abbreviation,PositionDistrictName,PositionNo,DABH,Contactor,ContactTel)VALUES(");
                _strBd.AppendFormat("'{0}',", model.LicenseNo);
                _strBd.AppendFormat("'{0}',", model.MarketName);
                _strBd.AppendFormat("'{0}',", model.MarketRef);
                _strBd.AppendFormat("'{0}',", model.Abbreviation);
                _strBd.AppendFormat("'{0}',", model.PositionDistrictName);
                _strBd.AppendFormat("'{0}',", model.PositionNo);
                _strBd.AppendFormat("'{0}',", model.DABH);
                _strBd.AppendFormat("'{0}',", model.Contactor);
                _strBd.AppendFormat("'{0}')", model.ContactTel);

                DataBase.ExecuteCommand(_strBd.ToString(), out MsgErr);

                rtn = 1;
            }
            catch(Exception ex)
            {
                MsgErr = ex.Message;
            }
            return rtn;
        }
        /// <summary>
        /// 插入主体经营户信息
        /// </summary>
        /// <returns></returns>
        public int InAreaMarket(KunShanEntity.AreaMarket model, out string MsgErr)
        {
            MsgErr = "";
            int rtn = 0;
            try
            {
                _strBd.Length = 0;
                _strBd.Append("INSERT INTO KSAreaMarket (LicenseNo,MarketName,MarketRef,Abbreviation)VALUES(");
                _strBd.AppendFormat("'{0}',", model.LicenseNo);
                _strBd.AppendFormat("'{0}',", model.MarketName);
                _strBd.AppendFormat("'{0}',", model.MarketRef);
                _strBd.AppendFormat("'{0}')", model.Abbreviation);

                DataBase.ExecuteCommand(_strBd.ToString(), out MsgErr);

                rtn = 1;
            }
            catch (Exception ex)
            {
                MsgErr = ex.Message;
            }
            return rtn;
        }
        /// <summary>
        /// 删除主体信息
        /// </summary>
        /// <param name="where"></param>
        /// <param name="MsgErr"></param>
        /// <returns></returns>
        public int DeleteAreaMarket(string where, out string MsgErr)
        {
            MsgErr = "";
            int rtn = 0;
            try
            {
                _strBd.Length = 0;
                _strBd.Append("DELETE FROM KSAreaMarket ");
                if (where != "")
                {
                    _strBd.Append(" where ");
                    _strBd.Append(where );
                }

                DataBase.ExecuteCommand(_strBd.ToString(), out MsgErr);

                rtn = 1;
            }
            catch (Exception ex)
            {
                MsgErr = ex.Message;
            }
            return rtn;
        }
        public DataTable GetKSAreaMarket(string where ,out string MsgErr)
        {
            DataTable dtb1 = null;
           
            _strBd.Length = 0;
           
            _strBd.Append("SELECT * From KSAreaMarket");

            if (!where.Equals(""))
            {
                _strBd.Append(" WHERE ");
                _strBd.Append(where);
            }
               
            string[] cmd = new string[1] { _strBd.ToString() };
            string[] names = new string[1] { "KSAreaMarket" };
            dtb1 = DataBase.GetDataSet(cmd, names, out MsgErr).Tables["KSAreaMarket"];
           
            return dtb1;
        }
        /// <summary>
        /// 删除经营户信息
        /// </summary>
        /// <param name="where"></param>
        /// <param name="MsgErr"></param>
        /// <returns></returns>
        public int DeleteKSAreaSignContact(string where, out string MsgErr)
        {
            MsgErr = "";
            int rtn = 0;
            try
            {
                _strBd.Length = 0;
                _strBd.Append("DELETE FROM KSAreaSignContact ");
                if (where != "")
                {
                    _strBd.Append(" where ");
                    _strBd.Append(where);
                }

                DataBase.ExecuteCommand(_strBd.ToString(), out MsgErr);

                rtn = 1;
            }
            catch (Exception ex)
            {
                MsgErr = ex.Message;
            }
            return rtn;
        }
    }
}

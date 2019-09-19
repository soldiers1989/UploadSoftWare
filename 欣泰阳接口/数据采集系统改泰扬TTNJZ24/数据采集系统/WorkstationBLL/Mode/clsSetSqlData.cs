using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using WorkstationDAL.Basic;
using WorkstationDAL.Model;



namespace WorkstationBLL.Mode
{
    public class clsSetSqlData
    {
        
        private StringBuilder _strBd = new StringBuilder();
        /// <summary>
        /// 插入串口数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int Insert(clsSetCom model, out string errMsg)
        {
            _strBd.Length = 0;
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                _strBd.Append("INSERT INTO tSetCom ");
                _strBd.Append("(ID,ComBaud,ComDataBit,ComParity,ComStopBit,");
                _strBd.Append("ComPort)");
                _strBd.Append("VALUES('");
                _strBd.Append("1");
                _strBd.Append("','");
                _strBd.Append(model.ComBaud);
                _strBd.Append("','");
                _strBd.Append(model.ComDataBit);
                _strBd.Append("','");
                _strBd.Append(model.ComParity);
                _strBd.Append("','");
                _strBd.Append(model.ComStopBit);
                _strBd.Append("','");
                _strBd.Append(model.ComPort);
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
        /// 删除所有数据
        /// </summary>
        /// <param name="errMsg"></param>
        public void DeleteAll(out string errMsg)
        {
            _strBd.Length = 0;
            _strBd.Append("DELETE FROM tResult");
            DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
            _strBd.Length = 0;
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
                _strBd.Append("Select * From ");
                _strBd.Append(whereSql);
                //if (!whereSql.Equals(""))
                //{
                //    _strBd.Append(" Where ");
                //    _strBd.Append(whereSql);
                //}
                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "tSetCom" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["tSetCom"];
                _strBd.Length = 0;
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
                _strBd.Append("(ChkNum,Checkitem,SampleName,CheckData,Unit,");
                _strBd.Append("CheckTime,CheckUnit,Result,Save,Machine,SampleTime,SampleAddress,DetectUnit,TestBase,LimitData,Tester,StockIn,SampleNum,HodeNum)");
                _strBd.Append("VALUES('");
                _strBd.Append(result.Gridnum);
                _strBd.Append("','");
                _strBd.Append(result.Checkitem);
                _strBd.Append("','");
                _strBd.Append(result.SampleName);
                _strBd.Append("','");
                _strBd.Append(result.CheckData);
                _strBd.Append("','");
                _strBd.Append(result.Unit);
                _strBd.Append("',#");
                _strBd.Append(result.CheckTime);
                _strBd.Append("#,'");
                _strBd.Append(result.CheckUnit == "" ? Global.CheckedUnit : result.CheckUnit);
                _strBd.Append("','");
                _strBd.Append(result.Result);
                _strBd.Append("','");
                _strBd.Append("是" );
                _strBd.Append("','");
                _strBd.Append(Global.ChkManchine);
                _strBd.Append("','");
                _strBd.Append(result.Gettime == "" ? "" : result.Gettime);
                _strBd.Append("','");
                _strBd.Append(result.Getplace);
                _strBd.Append("','");
                _strBd.Append(result.detectunit);
                _strBd.Append("','");
                _strBd.Append(result.Testbase);
                _strBd.Append("','");
                _strBd.Append(result.LimitData);
                _strBd.Append("','");
                _strBd.Append(result.Tester);
                _strBd.Append("','");
                _strBd.Append(result.quantityin );
                _strBd.Append("','");
                _strBd.Append(result.sampleNum);
                _strBd.Append("','");
                _strBd.Append(result.holeSize);
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
        /// 查询数据显示在LZ-2000测试界面上
        /// </summary>
        /// <param name="whereSQL"></param>
        /// <param name="oderby"></param>
        /// <returns></returns>
        public DataTable GetResultShow(string whereSQL, string oderby)
        {
            string errMsg = string.Empty;
            DataTable dtb1 = null;
            _strBd.Length = 0;
            try
            {
                _strBd.Append("SELECT ");
                _strBd.Append("Save,ChkNum,Checkitem,CheckData,Unit,Result,CheckTime");
                //_strBd.Append("PlanNum,TestBase,LimitData,Tester,Retester,Manager");
                _strBd.Append(" FROM ");
                _strBd.Append("CheckResult");
                _strBd.Append(" WHERE ");
                _strBd.Append(whereSQL);
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
                _strBd.Append("SELECT ");
                _strBd.Append("ChkNum,Checkitem,SampleName,CheckData,Unit,CheckTime,CheckUnit,Result,Machine,SampleTime,SampleAddress,");
                _strBd.Append("DetectUnit,TestBase,LimitData,Tester,StockIn,SampleNum,IsUp");
                _strBd.Append(" FROM ");
                _strBd.Append("CheckResult");
                _strBd.Append(" WHERE ");
                _strBd.Append(whereSQL);
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
        clsUpdateData up = new clsUpdateData();
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
        public int deleteSD(string mainkey)
        {
            string errMsg = string.Empty;
            int rtn = 0;
            _strBd.Length = 0;
            try
            {
                _strBd.AppendFormat("DELETE FROM CheckResult WHERE {0}", mainkey);
                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);

                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return rtn;
        }
        public int insertDatagrid(string  mainkey)
        {
            string errMsg = string.Empty;
            _strBd.Length = 0;
            int rtn = 0;
            try
            {
                _strBd.Append("INSERT INTO CheckResult ");
                _strBd.Append("(ChkNum,Checkitem,SampleName,CheckData,Unit,CheckTime,CheckUnit,Result,Machine,GetSampltTime,");
                _strBd.Append("GetSampleAddress,DetectUnit,TestBase,LimitData,Tester,StockIn,SampleNum");
                _strBd.Append(")");
                _strBd.Append("VALUES('");
                _strBd.Append(mainkey);
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
        public DataTable GetResultData(string sql, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dtb1 = null;
            _strBd.Length = 0;
            try
            {
                _strBd.Append("SELECT ChkNum,Checkitem,CheckData,CheckTime,Result");                
                _strBd.Append(" FROM ");
                _strBd.Append("CheckResult");
                _strBd.Append(sql);
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
                _strBd.Append("(Checkitem,SampleName,CheckData,Unit,");
                _strBd.Append("CheckTime,CheckUnit,Result,Save,Machine,SampleTime,SampleAddress,TestBase,LimitData,Tester,DetectUnit,SampleNum)");
                _strBd.Append("VALUES('");
                //_strBd.Append(result.Gridnum);
                //_strBd.Append("','");
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
                _strBd.Append("True");
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
                _strBd.Append("SELECT Name,Manufacturer,communication,Isselect,ChkStdCode,Numbering,Protocol");
                _strBd.Append(" FROM ");
                _strBd.Append("Instrument ");
                _strBd.Append(sql);
                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "Instrument" };
                dtb1 = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Instrument"];
                _strBd.Length = 0;
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
                _strBd.Append("SELECT userlog,passData,usertype");
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
        /// 获取新建方案
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetProject(string sql, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dtb1 = null;
            _strBd.Length = 0;
            try
            {
                _strBd.Append("SELECT *");
                _strBd.Append(" FROM ");
                _strBd.Append("mProject ");
                _strBd.Append(sql);
                string[] cmd = new string[1] { _strBd.ToString() };
                string[] names = new string[1] { "mProject" };
                dtb1 = DataBase.GetDataSet(cmd, names, out errMsg).Tables["mProject"];
                _strBd.Length = 0;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dtb1;

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
                _strBd.Append("SELECT SysCode,StdCode,Name,ShortCut,CheckLevel,CheckItemCodes");
                _strBd.Append(",CheckItemValue,IsLock,IsReadOnly,Remark,FoodProperty FROM CheckSample");
               
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
                _strBd.Length = 0;
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
        public int SaveInformation(string name)
        {
            string errMsg = string.Empty;
            _strBd.Length = 0;
            int rtn = 0;
            try
            {
                _strBd.Append("INSERT INTO BasicInformation ");
                _strBd.Append("(TestUnitName,TestUnitAddr,DetectUnitName,SampleAddress,Tester,iChecked");
                _strBd.Append(")");
                _strBd.Append("VALUES('");
                _strBd.Append(name);               
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
                _strBd.Append("SELECT TestUnitName,TestUnitAddr,DetectUnitName,SampleAddress,QuantityIn,SampleNum,TestBase");
                _strBd.Append(",SampleTime,Tester,iChecked,ID FROM BasicInformation");

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
        public void updateBasicIn(string sok, string Chknum, string Addr, string tester, string dunit, string saddr, out string errMsg)
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
                _strBd.Append("TestUnitAddr='");
                _strBd.Append(Addr);
                _strBd.Append("' and ");
                _strBd.Append("DetectUnitName='");
                _strBd.Append(dunit);
                _strBd.Append("' and ");
                _strBd.Append("SampleAddress='");
                _strBd.Append(saddr);
                _strBd.Append("' and ");
                _strBd.Append("Tester='");
                _strBd.Append(tester);
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
        /// 删除基本信息记录
        /// </summary>
        /// <param name="mainkey"></param>
        /// <returns></returns>
        public int deletetInfo(string mainkey)
        {
            string errMsg = string.Empty;
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

        public void updateunit(string repairdata, int Condition, out string errMsg)
        {
            errMsg = string.Empty;
            _strBd.Length = 0;
            int rtn = 0;
            try
            {
                _strBd.Append("update BasicInformation ");
                _strBd.Append("set ");
                _strBd.Append(repairdata);
                _strBd.Append(" where ID=");
                _strBd.Append(Condition);              
             
                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
                rtn = 1;
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
                _strBd.Append("set SampleName='");
                _strBd.Append(SR.ChkSample);
                _strBd.Append("',");
                _strBd.Append("Checkitem='");
                _strBd.Append(SR.Chkxiangmu);
                _strBd.Append("',");
                _strBd.Append("CheckData='");
                _strBd.Append(SR.result );
                _strBd.Append("',");
                _strBd.Append("Unit='");
                _strBd.Append(SR.unit);
                _strBd.Append("',");
                _strBd.Append("TestBase='");
                _strBd.Append(SR.Chktestbase);
                _strBd.Append("',");
                _strBd.Append("LimitData='");
                _strBd.Append(SR.ChklimitData);
                _strBd.Append("',");
                _strBd.Append("Machine='");
                _strBd.Append(SR.intrument);
                _strBd.Append("',");
                _strBd.Append("Result='");
                _strBd.Append(SR.conclusion );
                _strBd.Append("',");
                _strBd.Append("SampleTime='");
                _strBd.Append(SR.GetSampTime);
                _strBd.Append("',");
                _strBd.Append("SampleAddress='");
                _strBd.Append(SR.GetSampPlace);
                _strBd.Append("',");
                _strBd.Append("CheckUnit='");
                _strBd.Append(SR.ChkUnit);
                _strBd.Append("',");
                _strBd.Append("Tester='");
                _strBd.Append(SR.ChkPeople);
                _strBd.Append("',");
                _strBd.Append("DetectUnit='");
                _strBd.Append(SR.detectunit);
                _strBd.Append("',");
                _strBd.Append("CheckTime=#");
                _strBd.Append(DateTime.Parse(SR.ChkTime));
                _strBd.Append("#");
                _strBd.Append(",SampleNum='");
                _strBd.Append(SR.sampleNum);
                _strBd.Append("'");
                _strBd.Append(" where ID=");
                _strBd.Append(wID);
                //_strBd.Append("");

                DataBase.ExecuteCommand(_strBd.ToString(), out errMsg);
                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
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
                _strBd.Append("set IsUp='");
                _strBd.Append("是");
                _strBd.Append("'");
                _strBd.Append(" where ");
                _strBd.Append("CheckData='");
                _strBd.Append(SR.result);
                _strBd.Append("' and ");
                _strBd.Append("CheckTime=#");
                _strBd.Append(DateTime.Parse(SR.ChkTime));
                _strBd.Append("#");
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
    }
}

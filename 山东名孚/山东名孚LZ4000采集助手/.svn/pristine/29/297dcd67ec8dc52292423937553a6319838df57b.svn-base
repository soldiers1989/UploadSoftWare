using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using WorkstationDAL.Basic;

namespace WorkstationDAL.Model
{
    public class clsSqlExecute
    {
        private StringBuilder _strBd = new StringBuilder();
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

        public int ResuInsert(clsSaveResult result, out string errMsg)
        {
            int rtn = 0;
            _strBd.Length = 0;
            errMsg = string.Empty;
            try
            {
                _strBd.Append("INSERT INTO CheckResult ");
                _strBd.Append("(GridNum,Checkitem,SampleName,CheckData,Unit,");
                _strBd.Append("CheckTime,CheckUnit)");
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
                _strBd.Append("','");
                _strBd.Append(result.CheckTime);
                _strBd.Append("','");
                _strBd.Append(result.CheckUnit);
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
    }
}

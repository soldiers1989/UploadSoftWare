using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DYSeriesDataSet.DataModel;
using System.Data;

namespace DYSeriesDataSet.DataSentence
{
    public class clsSaveItems
    {
        private StringBuilder sb = new StringBuilder();

        /// <summary>
        /// 删除所有数据
        /// </summary>
        /// <param name="errMsg"></param>
        public  int DeleteAllItem(out string errMsg)
        {
            int rtn=0;
            try
            {
                sb.Length = 0;
                sb.Append("DELETE FROM JSItems");
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;
                rtn = 1;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return rtn;
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int Insert(clsItem model, out string errMsg)
        {
            int rtn = 0;
            sb.Length=0;
            errMsg = string.Empty;
           
            try
            {
                sb.Append("INSERT INTO JSItems (Itemid,name,pid,hierarchy)VALUES('");
                sb.Append(model.id);
                sb.Append("','");
                sb.Append(model.name);
                sb.Append("','");
                sb.Append(model.pid );
                sb.Append("','");
                sb.Append(model.hierarchy);
                sb.Append("')");
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                rtn = 1;
 
            }
            catch(Exception ex)
            {
                errMsg = ex.Message;
            }

            return rtn;

        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="whereSql"></param>
        /// <param name="orderBySql"></param>
        /// <returns></returns>
        public DataTable GetAllItem(string whereSql, string orderBySql, out string errMsg)
        {          
            DataTable dt = null;
            sb.Length = 0;
            try
            {
                sb.Append("SELECT ID,Itemid,name,pid,hierarchy FROM JSItems");
                if (whereSql != "")
                {
                    sb.Append(" where ");
                    sb.Append(whereSql);
                }
                if (orderBySql != "")
                {
                    sb.Append(" order by ");
                    sb.Append(orderBySql);
                }

                string[] cmd = new string[1]{ sb.ToString() };
                string[] names = new string[1] { "JSItems" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["JSItems"];
                sb.Length = 0;

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dt;
        }
        /// <summary>
        /// 删除下载样品库
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int DeleteSTDSample(out string errMsg)
        {
            int rtn = 0;
            try
            {
                sb.Length = 0;
                sb.Append("DELETE FROM STDSample");
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;
                rtn = 1;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return rtn;
        }
        /// <summary>
        /// 插入样品库
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int InsertSTDSample(clsSTDSample model, out string errMsg)
        {
            int rtn = 0;
            sb.Length = 0;
            errMsg = string.Empty;

            try
            {
                sb.Append("INSERT INTO STDSample (sampleNum,stallNumber,productName,category,enterpriseId,foodTypeId,isDetection,enterpriseName,createTime)VALUES('");
                sb.Append(model.sampleNum);
                sb.Append("','");
                sb.Append(model.stallNumber);
                sb.Append("','");
                sb.Append(model.productName);
                sb.Append("','");
                sb.Append(model.category);
                sb.Append("','");
                sb.Append(model.enterpriseId);
                sb.Append("','");
                sb.Append(model.foodTypeId);
                sb.Append("','");
                sb.Append(model.isDetection);
                sb.Append("','");
                sb.Append(model.enterpriseName);
                sb.Append("','");
                sb.Append(model.createtime);
                sb.Append("')");
                //sb.AppendFormat("'{0}','{1}','{2}','{3}','{4}')", model.id, model.name, model.typeLevel, model.typeLevelName,model.hierarchy);
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
        /// 查询下载的样品库
        /// </summary>
        /// <param name="whereSql"></param>
        /// <param name="orderBySql"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetSTDsample(string whereSql, string orderBySql, out string errMsg)
        {
            DataTable dt = null;
            sb.Length = 0;
            try
            {
                sb.Append("SELECT ID,sampleNum,stallNumber,productName,category,enterpriseId,foodTypeId,isDetection,enterpriseName,createTime FROM STDSample");
                if (whereSql != "")
                {
                    sb.Append(" where ");
                    sb.Append(whereSql);
                }
                if (orderBySql != "")
                {
                    sb.Append(" order by ");
                    sb.Append(orderBySql);
                }

                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "STDSample" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["STDSample"];
                sb.Length = 0;

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dt;
        }
        /// <summary>
        /// 删除样品分类表
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int DeleteAllSample(out string errMsg)
        {
            int rtn = 0;
            try
            {
                sb.Length = 0;
                sb.Append("DELETE FROM JSSample");
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;
                rtn = 1;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return rtn;
        }
        /// <summary>
        /// 插入样品分类数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int InsertSample(clsSample model, out string errMsg)
        {
            int rtn = 0;
            sb.Length = 0;
            errMsg = string.Empty;

            try
            {
                sb.Append("INSERT INTO JSSample (sampleid,name,typeLevel,typeLevelName,hierarchy)VALUES('");
                sb.Append(model.id);
                sb.Append("','");
                sb.Append(model.name);
                sb.Append("','");
                sb.Append(model.typeLevel);
                sb.Append("','");
                sb.Append(model.typeLevelName);
                sb.Append("','");
                sb.Append(model.hierarchy);
                sb.Append("')");
                //sb.AppendFormat("'{0}','{1}','{2}','{3}','{4}')", model.id, model.name, model.typeLevel, model.typeLevelName,model.hierarchy);
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
        /// 查询样品分类表
        /// </summary>
        /// <param name="whereSql"></param>
        /// <param name="orderBySql"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable Getdownsample(string whereSql, string orderBySql, out string errMsg)
        {
            DataTable dt = null;
            sb.Length = 0;
            try
            {
                sb.Append("SELECT ID,sampleid,name,typeLevel,typeLevelName,hierarchy FROM JSSample");
                if (whereSql != "")
                {
                    sb.Append(" where ");
                    sb.Append(whereSql);
                }
                if (orderBySql != "")
                {
                    sb.Append(" order by ");
                    sb.Append(orderBySql);
                }

                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "JSSample" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["JSSample"];
                //sb.Length = 0;

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dt;
        }
        /// <summary>
        /// 删除原有的摊位数据
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int DeleteAllStall(out string errMsg)
        {
            int rtn = 0;
            try
            {
                sb.Length = 0;
                sb.Append("DELETE FROM JSstall");
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;
                rtn = 1;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return rtn;
        }
        /// <summary>
        /// 插入摊位数据
        /// </summary>
        /// <param name="sqldata"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int InsertStall(string sqldata, out string errMsg)
        {
            int rtn = 0;
            sb.Length = 0;
            errMsg = string.Empty;

            try
            {
                sb.Append("INSERT INTO JSstall (stallNumber,enterpriseName,unitName)VALUES('");
                sb.Append(sqldata);  
                sb.Append("')");
                //sb.AppendFormat("'{0}','{1}','{2}','{3}','{4}')", model.id, model.name, model.typeLevel, model.typeLevelName,model.hierarchy);
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
        /// 查询摊位
        /// </summary>
        /// <param name="whereSql"></param>
        /// <param name="orderBySql"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable Getdownstall(string whereSql, string orderBySql, out string errMsg)
        {
            DataTable dt = null;
            sb.Length = 0;
            try
            {
                sb.Append("SELECT stallNumber,enterpriseName,unitName FROM JSstall");
                if (whereSql != "")
                {
                    sb.Append(" where ");
                    sb.Append(whereSql);
                }
                if (orderBySql != "")
                {
                    sb.Append(" order by ");
                    sb.Append(orderBySql);
                }

                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "JSstall" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["JSstall"];
                sb.Length = 0;

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dt;
        }
        public int DeleteAllCompany(out string errMsg)
        {
            int rtn = 0;
            try
            {
                sb.Length = 0;
                sb.Append("DELETE FROM company");
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;
                rtn = 1;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return rtn;
        }
        /// <summary>
        /// 插入被检单位
        /// </summary>
        /// <param name="sqldata"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int InsertCompany(JSCompany model, out string errMsg)
        {
            int rtn = 0;
            sb.Length = 0;
            errMsg = string.Empty;

            try
            {
                sb.Append("INSERT INTO company (comid,name,type,legalPerson,legalPersonContact,address,creditLevel,licenceNumber)VALUES('");
                sb.Append(model.id);
                sb.Append("','");
                sb.Append(model.name);
                sb.Append("','");
                sb.Append(model.type );
                sb.Append("','");
                sb.Append(model.legalPerson);
                sb.Append("','");
                sb.Append(model.legalPersonContact);
                sb.Append("','");
                sb.Append(model.address);
                sb.Append("','");
                sb.Append(model.creditLevel);
                sb.Append("','");
                sb.Append(model.licenceNumber);
                sb.Append("')");
                //sb.AppendFormat("'{0}','{1}','{2}','{3}','{4}')", model.id, model.name, model.typeLevel, model.typeLevelName,model.hierarchy);
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
        /// 查询被检单位
        /// </summary>
        /// <param name="whereSql"></param>
        /// <param name="orderBySql"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetdownCompany(string whereSql, string orderBySql, out string errMsg)
        {
            DataTable dt = null;
            sb.Length = 0;
            try
            {
                sb.Append("SELECT comid,name,type,legalPerson,legalPersonContact,address,creditLevel,licenceNumber FROM company");
                if (whereSql != "")
                {
                    sb.Append(" where ");
                    sb.Append(whereSql);
                }
                if (orderBySql != "")
                {
                    sb.Append(" order by ");
                    sb.Append(orderBySql);
                }

                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "company" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["company"];
                sb.Length = 0;

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return dt;
        }
    }
}

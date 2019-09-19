using System;
using System.Data;
using System.Text;
using System.Windows;
using DYSeriesDataSet;
using DYSeriesDataSet.DataModel;
using DYSeriesDataSet.DataModel.Wisdom;

namespace AIO.src
{
    public class WisdomCls : Window
    {
        public WisdomCls()
        {
            //2016年4月16日 wenj
        }

        private static StringBuilder sql = new StringBuilder();

        /// <summary>
        /// 新增一条快检单号
        /// </summary>
        /// <returns></returns>
        public static int Insert()
        {
            int rtn = 0;
            if (Wisdom.GETSAMPLE_RESPONSE != null)
            {
                getsample.Response model = Wisdom.GETSAMPLE_RESPONSE;
                try
                {
                    sql.Length = 0;
                    sql.Append("INSERT INTO TB_SAMPLE ");
                    sql.Append("(SAMPLENUM,SAMPDATE,SAMPCOMPANY,SCOMPADDR,SCOMPCONT,SCOMPPHON,SAMPPERSON, ");
                    sql.Append("FOODNAME,BARCODE,BSAMPCOMPANY,BSCOMPADDR,BSCOMPPHON,BSCOMPCONT,BRAND,PRODATE,MODEL, ");
                    sql.Append("BATCHNUM,SHELFLIFE,PROCOMPANY,PROCOMPADDR,PROCOMPPHON,DEVICEID) VALUES(");
                    sql.AppendFormat("'{0}',", model.sampleid);
                    sql.AppendFormat("'{0}',", model.sampleDate);
                    sql.AppendFormat("'{0}',", model.sampleDept);
                    sql.AppendFormat("'{0}',", model.scompAddr);
                    sql.AppendFormat("'{0}',", model.scompCont);
                    sql.AppendFormat("'{0}',", model.scompPhon);
                    sql.AppendFormat("'{0}',", model.sampPerson);
                    sql.AppendFormat("'{0}',", model.productName);
                    sql.AppendFormat("'{0}',", model.barCode);
                    sql.AppendFormat("'{0}',", model.bsampCompany);
                    sql.AppendFormat("'{0}',", model.bscompAddr);
                    sql.AppendFormat("'{0}',", model.bscompPhon);
                    sql.AppendFormat("'{0}',", model.bscompCont);
                    sql.AppendFormat("'{0}',", model.brand);
                    sql.AppendFormat("'{0}',", model.prodate);
                    sql.AppendFormat("'{0}',", model.model);
                    sql.AppendFormat("'{0}',", model.batchNum);
                    sql.AppendFormat("'{0}',", model.shelfLife);
                    sql.AppendFormat("'{0}',", model.proCompany);
                    sql.AppendFormat("'{0}',", model.procompAddr);
                    sql.AppendFormat("'{0}',", model.procompPhon);
                    sql.AppendFormat("'{0}')", Global.DeviceID);
                    DataBase.ExecuteCommand(sql.ToString());
                    rtn = 1;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return rtn;
        }

        /// <summary>
        /// 修改一条快检单号
        /// </summary>
        /// <returns></returns>
        public static int Update()
        {
            int rtn = 0;
            if (Wisdom.GETSAMPLE_RESPONSE != null)
            {
                getsample.Response model = Wisdom.GETSAMPLE_RESPONSE;
                try
                {
                    sql.Append("UPDATE TB_SAMPLE SET ");
                    sql.AppendFormat("SAMPDATE='{0}',SAMPCOMPANY='{1}',SAMPPERSON='{2}',",
                        model.sampleDate, model.sampleDept, model.sampPerson);
                    sql.AppendFormat("FOODNAME='{0}',BARCODE='{1}',BSAMPCOMPANY='{2}',BSCOMPADDR='{3}',BSCOMPPHON='{4}',BSCOMPCONT='{5}'  Where SAMPLENUM='{6}'",
                         model.productName, model.barCode, model.bsampCompany, model.bscompAddr, model.bscompPhon, model.bscompCont, Global.DeviceID, model.sampleid);

                    DataBase.ExecuteCommand(sql.ToString());
                    rtn = 1;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return rtn;
        }

        public static int InsertSample(clsTB_SAMPLING model)
        {
            int rtn = 0;
            sql.Length = 0;
            try
            {
                sql.Append("INSERT INTO TB_SAMPLING ");
                sql.Append("(SAMPLINGNO,SAMPLINGDATE,CKOCODE,CKONAME,CDCODE,CDNAME,CDBUSLICENCE,");
                sql.Append("CONTACTPERSON,PHONE,USERNAME,USERID,SAMPLENO,SAMPLENAME,CKICOUNT,CKTAKECOUNT,CKTAKEDATE,");
                sql.Append("CKPCONAME,SUPPLIER,SUPPADDR,SUPPCONTACT,SUPPPHONE,UDATE) VALUES(");
                sql.AppendFormat("'{0}',", model.SAMPLINGNO);
                sql.AppendFormat("'{0}',", model.SAMPLINGDATE);
                sql.AppendFormat("'{0}',", model.CKOCODE);
                sql.AppendFormat("'{0}',", model.CKONAME);
                sql.AppendFormat("'{0}',", model.CDCODE);
                sql.AppendFormat("'{0}',", model.CDNAME);
                sql.AppendFormat("'{0}',", model.CDBUSLICENCE);
                sql.AppendFormat("'{0}',", model.CONTACTPERSON);
                sql.AppendFormat("'{0}',", model.PHONE);
                sql.AppendFormat("'{0}',", model.USERNAME);
                sql.AppendFormat("'{0}',", model.USERID);
                sql.AppendFormat("'{0}',", model.SAMPLENO);
                sql.AppendFormat("'{0}',", model.SAMPLENAME);
                sql.AppendFormat("'{0}',", model.CKICOUNT);
                sql.AppendFormat("'{0}',", model.CKTAKECOUNT);
                sql.AppendFormat("'{0}',", model.CKTAKEDATE);
                sql.AppendFormat("'{0}',", model.CKPCONAME);
                sql.AppendFormat("'{0}',", model.SUPPLIER);
                sql.AppendFormat("'{0}',", model.SUPPADDR);
                sql.AppendFormat("'{0}',", model.SUPPCONTACT);
                sql.AppendFormat("'{0}',", model.SUPPPHONE);
                sql.AppendFormat("'{0}')", model.UDATE);
                DataBase.ExecuteCommand(sql.ToString());
                rtn = 1;
                sql.Length = 0;
            }
            catch (Exception ex)
            {
                sql.Length = 0;
                throw ex;
            }
            return rtn;
        }

        public static DataTable GetSample(string whereSql, string orderBySql, int type)
        {
            string errMsg = string.Empty;
            DataTable dt = null;
            sql.Length = 0;
            try
            {

                sql.Append("SELECT * FROM TB_SAMPLING ");
                if (!whereSql.Equals(string.Empty))
                {
                    sql.Append(" WHERE ");
                    sql.Append(whereSql);
                }
                if (!orderBySql.Equals(string.Empty))
                {
                    sql.Append(" ORDER BY ID DESC");
                    sql.Append(orderBySql);
                }
                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "Result" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Result"];
                sql.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
        }

        /// <summary>
        /// 根据条件查询快检单号
        /// </summary>
        /// <param name="whereSql">where条件</param>
        /// <param name="orderBySql">orderBy条件</param>
        /// <param name="type">类型：1查询全部，2按条件查询</param>
        /// <returns></returns>
        public static DataTable GetAsDataTable(string whereSql, string orderBySql, int type)
        {
            string errMsg = string.Empty;
            DataTable dt = null;
            sql.Length = 0;
            try
            {

                sql.Append("SELECT * FROM TB_SAMPLE");
                if (type == 2)
                {
                    if (!whereSql.Equals(string.Empty))
                    {
                        sql.Append(" WHERE ");
                        sql.Append(whereSql);
                    }
                    if (!orderBySql.Equals(string.Empty))
                    {
                        sql.Append(" ORDER BY ID DESC");
                        sql.Append(orderBySql);
                    }
                }

                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "Result" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Result"];
                sql.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
        }

    }
}
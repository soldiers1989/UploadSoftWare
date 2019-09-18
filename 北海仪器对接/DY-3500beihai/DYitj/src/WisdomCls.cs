using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using DYSeriesDataSet;
using System.Data;
using DYSeriesDataSet.DataModel;

namespace AIO.src
{
    public class WisdomCls : Window
    {
        public WisdomCls()
        {
            //2016年4月16日 wenj
        }

        private static StringBuilder _strB = new StringBuilder();

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
                    _strB.Length = 0;
                    _strB.Append("INSERT INTO TB_SAMPLE ");
                    _strB.Append("(SAMPLENUM,SAMPDATE,SAMPCOMPANY,SCOMPADDR,SCOMPCONT,SCOMPPHON,SAMPPERSON, ");
                    _strB.Append("FOODNAME,BARCODE,BSAMPCOMPANY,BSCOMPADDR,BSCOMPPHON,BSCOMPCONT,BRAND,PRODATE,MODEL, ");
                    _strB.Append("BATCHNUM,SHELFLIFE,PROCOMPANY,PROCOMPADDR,PROCOMPPHON,DEVICEID) VALUES(");
                    _strB.AppendFormat("'{0}',", model.sampleid);
                    _strB.AppendFormat("'{0}',", model.sampleDate);
                    _strB.AppendFormat("'{0}',", model.sampleDept);
                    _strB.AppendFormat("'{0}',", model.scompAddr);
                    _strB.AppendFormat("'{0}',", model.scompCont);
                    _strB.AppendFormat("'{0}',", model.scompPhon);
                    _strB.AppendFormat("'{0}',", model.sampPerson);
                    _strB.AppendFormat("'{0}',", model.productName);
                    _strB.AppendFormat("'{0}',", model.barCode);
                    _strB.AppendFormat("'{0}',", model.bsampCompany);
                    _strB.AppendFormat("'{0}',", model.bscompAddr);
                    _strB.AppendFormat("'{0}',", model.bscompPhon);
                    _strB.AppendFormat("'{0}',", model.bscompCont);
                    _strB.AppendFormat("'{0}',", model.brand);
                    _strB.AppendFormat("'{0}',", model.prodate);
                    _strB.AppendFormat("'{0}',", model.model);
                    _strB.AppendFormat("'{0}',", model.batchNum);
                    _strB.AppendFormat("'{0}',", model.shelfLife);
                    _strB.AppendFormat("'{0}',", model.proCompany);
                    _strB.AppendFormat("'{0}',", model.procompAddr);
                    _strB.AppendFormat("'{0}',", model.procompPhon);
                    _strB.AppendFormat("'{0}')", Wisdom.DeviceID);
                    DataBase.ExecuteCommand(_strB.ToString());
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
                    _strB.Append("UPDATE TB_SAMPLE SET ");
                    _strB.AppendFormat("SAMPDATE='{0}',SAMPCOMPANY='{1}',SAMPPERSON='{2}',",
                        model.sampleDate, model.sampleDept, model.sampPerson);
                    _strB.AppendFormat("FOODNAME='{0}',BARCODE='{1}',BSAMPCOMPANY='{2}',BSCOMPADDR='{3}',BSCOMPPHON='{4}',BSCOMPCONT='{5}'  Where SAMPLENUM='{6}'",
                         model.productName, model.barCode, model.bsampCompany, model.bscompAddr, model.bscompPhon, model.bscompCont, Wisdom.DeviceID, model.sampleid);

                    DataBase.ExecuteCommand(_strB.ToString());
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
            _strB.Length = 0;
            try
            {
                _strB.Append("INSERT INTO TB_SAMPLING ");
                _strB.Append("(SAMPLINGNO,SAMPLINGDATE,CKOCODE,CKONAME,CDCODE,CDNAME,CDBUSLICENCE,");
                _strB.Append("CONTACTPERSON,PHONE,USERNAME,USERID,SAMPLENO,SAMPLENAME,CKICOUNT,CKTAKECOUNT,CKTAKEDATE,");
                _strB.Append("CKPCONAME,SUPPLIER,SUPPADDR,SUPPCONTACT,SUPPPHONE,UDATE) VALUES(");
                _strB.AppendFormat("'{0}',", model.SAMPLINGNO);
                _strB.AppendFormat("'{0}',", model.SAMPLINGDATE);
                _strB.AppendFormat("'{0}',", model.CKOCODE);
                _strB.AppendFormat("'{0}',", model.CKONAME);
                _strB.AppendFormat("'{0}',", model.CDCODE);
                _strB.AppendFormat("'{0}',", model.CDNAME);
                _strB.AppendFormat("'{0}',", model.CDBUSLICENCE);
                _strB.AppendFormat("'{0}',", model.CONTACTPERSON);
                _strB.AppendFormat("'{0}',", model.PHONE);
                _strB.AppendFormat("'{0}',", model.USERNAME);
                _strB.AppendFormat("'{0}',", model.USERID);
                _strB.AppendFormat("'{0}',", model.SAMPLENO);
                _strB.AppendFormat("'{0}',", model.SAMPLENAME);
                _strB.AppendFormat("'{0}',", model.CKICOUNT);
                _strB.AppendFormat("'{0}',", model.CKTAKECOUNT);
                _strB.AppendFormat("'{0}',", model.CKTAKEDATE);
                _strB.AppendFormat("'{0}',", model.CKPCONAME);
                _strB.AppendFormat("'{0}',", model.SUPPLIER);
                _strB.AppendFormat("'{0}',", model.SUPPADDR);
                _strB.AppendFormat("'{0}',", model.SUPPCONTACT);
                _strB.AppendFormat("'{0}',", model.SUPPPHONE);
                _strB.AppendFormat("'{0}')", model.UDATE);
                DataBase.ExecuteCommand(_strB.ToString());
                rtn = 1;
                _strB.Length = 0;
            }
            catch (Exception ex)
            {
                _strB.Length = 0;
                throw ex;
            }
            return rtn;
        }

        public static DataTable GetSample(string whereSql, string orderBySql, int type)
        {
            string errMsg = string.Empty;
            DataTable dt = null;
            _strB.Length = 0;
            try
            {

                _strB.Append("SELECT * FROM TB_SAMPLING ");
                if (!whereSql.Equals(string.Empty))
                {
                    _strB.Append(" WHERE ");
                    _strB.Append(whereSql);
                }
                if (!orderBySql.Equals(string.Empty))
                {
                    _strB.Append(" ORDER BY ID DESC");
                    _strB.Append(orderBySql);
                }
                string[] cmd = new string[1] { _strB.ToString() };
                string[] names = new string[1] { "Result" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Result"];
                _strB.Length = 0;
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
            _strB.Length = 0;
            try
            {

                _strB.Append("SELECT * FROM TB_SAMPLE");
                if (type == 2)
                {
                    if (!whereSql.Equals(string.Empty))
                    {
                        _strB.Append(" WHERE ");
                        _strB.Append(whereSql);
                    }
                    if (!orderBySql.Equals(string.Empty))
                    {
                        _strB.Append(" ORDER BY ID DESC");
                        _strB.Append(orderBySql);
                    }
                }

                string[] cmd = new string[1] { _strB.ToString() };
                string[] names = new string[1] { "Result" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Result"];
                _strB.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
        }


    }
}

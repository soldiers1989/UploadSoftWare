using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JH.CommBase;
using System.Data;
using System.IO;
using WorkstationModel.Model;
using WorkstationBLL.Mode;


namespace WorkstationModel.Instrument
{
    public class clsDY6400 : CommBase
    {
        /// <summary>
        /// 第三代数据列表
        /// </summary>
        public DataTable checkDtbl;

        private clsSetSqlData sql = new clsSetSqlData();
        // private string errMsg = string.Empty;
        private bool IsCreatDT = false;
        private StringBuilder sbShow = new StringBuilder();
        private StringBuilder strWhere = new StringBuilder();
        //private clsResultOpr resultBll = new clsResultOpr();
        private byte[] txBuffer = new byte[5];


        /// <summary>
        /// 构造函数
        /// </summary>
        public clsDY6400()
        {
            if (!IsCreatDT)
            {
                checkDtbl = new DataTable("checkDtbl");
                DataColumn dataCol;
                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "已保存";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);//int,string
                dataCol.ColumnName = "样品名称";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测项目";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测结果";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "单位";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测依据";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "标准值";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测仪器";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "结论";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测单位";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "采样时间";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "采样地点";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "被检单位";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测员";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测时间";
                checkDtbl.Columns.Add(dataCol);
               
                IsCreatDT = true;
            }
        }

        /// <summary>
        /// 数据读取
        /// </summary>
        public void ReadHistory()
        {
            checkDtbl.Clear();
            sbShow.Length = 0;
            txBuffer[0] = 0x6a;
            txBuffer[1] = 0x5b;
            txBuffer[2] = 0xa4;
            txBuffer[3] = 0x69;
            //txBuffer[4] = 0x04;
            try
            {
                Send(txBuffer);
            }
            catch (CommPortException e)
            {
                throw new CommPortException(e.Message);
                // MessageBox.Show(e.Message, "错误");
            }
        }

        /// <summary>
        /// 清理缓存数据,其实是删除仪器数据
        /// </summary>
        public void ClearCache()
        {
            txBuffer[0] = 0x6a;
            txBuffer[1] = 0x5d;
            txBuffer[2] = 0xa2;
            txBuffer[3] = 0x69;
            //try
            //{
            //    Send(txBuffer);
            //}
            //catch (CommPortException e)
            //{
            //    throw new CommPortException(e.Message);
            //    // MessageBox.Show(e.Message, "错误");
            //}
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="rec"></param>
        protected override void OnRxChar(byte rec)
        {
            sbShow.Append(rec.ToString("X2"));
            string temp = sbShow.ToString();
            if (temp.IndexOf("6A9999999999999999") >= 0)//表示数据采集完成
            {
                if (temp.Equals("6A9999999999999999"))
                {
                    clsMessageNotify.Instance().SendMessage(clsMessageNotify.NotifyInfo.ReadDY6400Data, "NO");
                    return;
                }
                int index = temp.Length % 18;
                if (index != 0)
                {
                    throw new FormatException("不是有效数据");
                }

                int len = temp.Length / 18;//数据条数
                if (len <= 1)
                {
                    throw new FormatException("暂无数据");
                }
                string id = string.Empty;//编号
                string item = string.Empty;//检测项目
                string sdate = string.Empty;//日期
                string scheck = string.Empty;//检测值
                string itemNum = string.Empty;
                float checkValue = 0f;
                DataRow dr;
                for (int i = 0; i < len; i++)
                {
                    string tt = temp.Substring(2 + i * 18, 14);

                    itemNum = tt.Substring(4, 2);//检测样品类别
                    switch (itemNum)
                    {
                        case "01": item = "猪肉"; break;
                        case "02": item = "牛肉"; break;
                        case "03": item = "羊肉"; break;
                        case "04": item = "鸡肉"; break;
                        case "05": item = "其它"; break;
                        default: item = ""; break;
                    }
                    if (item == "")//其他无效数据过虑
                    {
                        continue;
                    }
                    scheck = tt.Substring(0, 4);
                    checkValue = (float)(Convert.ToInt32(scheck) * 0.1);

                    sdate = "20" + tt.Substring(6, 2) + "-" + tt.Substring(8, 2) + "-" + tt.Substring(10, 2);
                    id = tt.Substring(12, 2);

                    strWhere.AppendFormat(" CheckData='{0}'", checkValue);
                    strWhere.AppendFormat(" AND Checkitem='{0}'", item);
                    strWhere.AppendFormat(" AND CheckTime=#{0}#", sdate);

                    dr = checkDtbl.NewRow();
                    dr["已保存"] = sql.IsExist(strWhere.ToString());
                    dr["样品名称"] = "";
                    dr["检测项目"] = item;
                    dr["检测结果"] = checkValue;
                    dr["单位"] = "";
                    dr["检测依据"] = "";
                    dr["标准值"] = "";
                    dr["检测仪器"] = "";
                    dr["结论"] = "";
                    dr["检测单位"] = "";
                    dr["采样时间"] = "";
                    dr["采样地点"] = "";
                    dr["被检单位"] = "";
                    dr["检测员"] = "";
                    dr["检测时间"] = sdate;
                    
                    strWhere.Length = 0;
                    checkDtbl.Rows.Add(dr);
                }
                if (checkDtbl.Rows.Count > 0)
                {
                    if (clsMessageNotify.Instance() != null)
                    {
                        clsMessageNotify.Instance().SendMessage(clsMessageNotify.NotifyInfo.ReadDY6400Data, "OK");
                    }
                }
                else
                {
                    throw new FormatException("暂无数据");
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using JH.CommBase;

namespace DY.FoodClientLib
{
    public class clsDY6400 : CommBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public clsDY6400()
        {
            if (!IsCreatDT)
            {
                checkDtbl = new DataTable("checkDtbl");
                DataColumn dataCol;
                /////////////////////////新增
                dataCol = new DataColumn();
                dataCol.DataType = typeof(bool);
                dataCol.ColumnName = "已保存";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(int);
                dataCol.ColumnName = "编号";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(decimal);
                dataCol.ColumnName = "检测值";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "种类";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测时间";
                checkDtbl.Columns.Add(dataCol);

                IsCreatDT = true;
            }
        }

        protected override CommBaseSettings CommSettings()
        {
            CommBaseSettings cs = new CommBaseSettings();
            cs.SetStandard(ShareOption.ComPort, 9600, Handshake.none, Parity.none);
            return cs;
        }

        /// <summary>
        /// 第三代数据列表
        /// </summary>
        public DataTable checkDtbl;

       // private string errMsg = string.Empty;
        private bool IsCreatDT = false;
        private StringBuilder sbShow = new StringBuilder();
        private StringBuilder strWhere = new StringBuilder();
        private clsResultOpr resultBll = new clsResultOpr();
        private byte[] txBuffer = new byte[5];

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
                    MessageNotify.Instance().SendMessage(MessageNotify.NotifyInfo.ReadDY6400Data, "NO");
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

                    strWhere.Append(" checkMachine='003'");
                    strWhere.AppendFormat(" AND MachineSampleNum='{0}'", id);
                    strWhere.AppendFormat(" AND MachineItemName='{0}'", item);
                    strWhere.AppendFormat(" AND CheckStartDate=#{0}#", sdate);
                    dr = checkDtbl.NewRow();
                    dr["编号"] = id;
                    dr["检测值"] = checkValue;
                    dr["种类"] = item;
                    dr["检测时间"] = sdate;
                    dr["已保存"] = resultBll.IsExist(strWhere.ToString());
                    strWhere.Length = 0;
                    checkDtbl.Rows.Add(dr);
                }
                if (checkDtbl.Rows.Count > 0)
                {
                    if (MessageNotify.Instance() != null)
                    {
                        MessageNotify.Instance().SendMessage(MessageNotify.NotifyInfo.ReadDY6400Data, "OK");
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

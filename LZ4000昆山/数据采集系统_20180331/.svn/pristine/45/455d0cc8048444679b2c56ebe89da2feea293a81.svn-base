using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using JH.CommBase;
using WorkstationModel.Model;

namespace WorkstationModel.Instrument
{
    public class clsDY8120:CommBase
    {
        /// <summary>
        /// 第三代数据列表
        /// </summary>
        public DataTable checkDtbl;
        private bool IsCreatDT = false;
        private StringBuilder sbShow = new StringBuilder();
        private StringBuilder strWhere = new StringBuilder();
        private clsResultOpr resultBll = new clsResultOpr();
        List<byte> rxBuffer = new List<byte>();
        private byte[] txBuffer;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public clsDY8120()
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
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "编号";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(decimal);
                dataCol.ColumnName = "检测值";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "项目";
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
        /// 数据读取
        /// </summary>
        public void ReadHistory(DateTime dtStart, DateTime dtEnd)
        {
            checkDtbl.Clear();
            string data = "S" + dtStart.ToString("yy/MM/ddHH:mm:ss") + dtEnd.ToString("yy/MM/ddHH:mm:ss") + "E";
            txBuffer = Encoding.Default.GetBytes(data); //StringUtil.HexString2ByteArray(data);
            try
            {
                Send(txBuffer);
            }
            catch (CommPortException e)
            {
                throw new CommPortException(e.Message);
            }
        }

        Encoding chs = Encoding.GetEncoding("gb2312");

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="rec"></param>
        protected override void OnRxChar(byte rec)
        {
            rxBuffer.Add(rec);
            //byte[] bArry = rxBuffer.ToArray();
            //string text = chs.GetString(bArry);//全部所有字符串
            string item = string.Empty;//检测项目
            string checkTime = string.Empty;//检测时间
            string id = string.Empty;
            string data = string.Empty;
            int count = 0;//检测记录条数
            int index = 0;

            // byte[] bArryItem = { rxBuffer[0], rxBuffer[1] };
            item = rxBuffer[0].ToString(); //chs.GetString(bArryItem, 0, 1);//第一段检测项目代码
            index = rxBuffer.IndexOf(0x0d);//去掉项目
            rxBuffer.RemoveRange(0, index + 1);

            index = rxBuffer.IndexOf(0x0d);//文件名标识索引
            byte[] bFileId = null;
            for (int i = 0; i < index; i++)
            {
                bFileId[i] = rxBuffer[i];
            }
            id = chs.GetString(bFileId);
            rxBuffer.RemoveRange(0, index + 1);//去文件名区段

            index = rxBuffer.IndexOf(0x0d);//文件时间索引,也就检测时间
            byte[] bCheckTime = null;
            for (int i = 0; i < index; i++)
            {
                bCheckTime[i] = rxBuffer[i];
            }
            checkTime = "20" + chs.GetString(bCheckTime);
            rxBuffer.RemoveRange(0, index + 1);

            index = rxBuffer.IndexOf(0x0d);//稀释倍数
            rxBuffer.RemoveRange(0, index + 1);//去稀释倍数区段

            index = rxBuffer.IndexOf(0x0d);//检测数据个数区段
            byte[] bDataCount = { rxBuffer[0] };
            count = Convert.ToInt32(chs.GetString(bDataCount));
            rxBuffer.RemoveRange(0, index + 1);//去掉检测数据个数区段

            DataRow dr;
            for (int j = 0; j < count; j++)//如果有多条的情况
            {
                index = rxBuffer.IndexOf(0x0d);//第一条检测数据
                data = string.Empty;
                byte[] bCheckData = null;
                for (int i = 0; i < index; i++)
                {
                    bCheckData[i] = rxBuffer[i];
                }
                data = chs.GetString(bCheckData);
                rxBuffer.RemoveRange(0, index + 1);//去掉第i条

                strWhere.Append(" checkMachine='020'");
                strWhere.AppendFormat(" AND MachineSampleNum='{0}'", id);
                strWhere.AppendFormat(" AND MachineItemName='{0}'", item);
                strWhere.AppendFormat(" AND CheckStartDate=#{0}#", checkTime);
                dr = checkDtbl.NewRow();
                dr["编号"] = id;
                dr["检测值"] = data;
                dr["项目"] = item;
                dr["检测时间"] = checkTime;
                dr["已保存"] = resultBll.IsExist(strWhere.ToString());
                strWhere.Length = 0;
                checkDtbl.Rows.Add(dr);//加入数据列表
            }
            if (checkDtbl.Rows.Count > 0)
            {
                if (clsMessageNotify.Instance() != null)
                {
                    clsMessageNotify.Instance().SendMessage(clsMessageNotify.NotifyInfo.ReadDY8120Data, "OK");
                }
            }
            else
            {
                throw new FormatException("暂无数据");
            }
        }

        private string getCheckItem(string itemCode)
        {
            string ret = string.Empty;
            switch (itemCode)
            {
                case "1": ret = "胭脂红"; break;
                case "2": ret = ""; break;
                case "3": ret = ""; break;
                case "4": ret = ""; break;
                case "5": ret = ""; break;
                case "6": ret = ""; break;
                case "7": ret = ""; break;
                case "8": ret = ""; break;
                case "9": ret = ""; break;
                case "10": ret = ""; break;
                case "11": ret = ""; break;
                case "12": ret = ""; break;
            }
            return ret;
        }

    }
}

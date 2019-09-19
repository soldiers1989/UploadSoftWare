using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorkstationModel.Model;
using System.Collections;
using WorkstationDAL.Model;
using System.Data;
using System.Collections;
using WorkstationBLL.Mode;

namespace WorkstationModel.Instrument
{
    public class clsTY16: JH.CommBase.CommBase
    {
        public static DataTable DataReadTable = null;
        private ArrayList arrl = new ArrayList();
        private int rd = 0;
        private bool m_IsCreatedDataTable = false;
        private StringBuilder strWhere = new StringBuilder();
    
        private string SChkTime = "";
        private string groundone = "";
        private string groundtwo = "";
        private string groundthree = "";
        private string groundfour = "";
        private string groundfire = "";
        private string groundsix = "";
        private string groundseven = "";
        private string groundeight = "";
        private DataTable cdt = null;
        private string[,] unitInfo = new string[1, 4];
        private clsSetSqlData sqlSet = new clsSetSqlData();

        public clsTY16()
        { 
            //初始化表格对象
            if (!m_IsCreatedDataTable)
            {
                DataReadTable = new DataTable("checkDtbl");//去掉Static

                DataColumn dataCol;
                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "已保存";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);//int,string
                dataCol.ColumnName = "样品名称";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测项目";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测结果";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "单位";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测依据";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "标准值";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测仪器";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "结论";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测时间";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测单位";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测数量";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "通道号";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "采样时间";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "采样地点";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "被检单位";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测员";
                DataReadTable.Columns.Add(dataCol);

                m_IsCreatedDataTable = true;
            }
            try
            {
                string err = "";
                cdt = sqlSet.GetInformation("", "", out err);
                if (cdt != null)
                {
                    if (cdt.Rows.Count > 0)
                    {
                        for (int n = 0; n < cdt.Rows.Count; n++)
                        {
                            if (cdt.Rows[n][9].ToString() == "是")
                            {
                                unitInfo[0, 0] = cdt.Rows[n][2].ToString();
                                unitInfo[0, 1] = cdt.Rows[n][3].ToString();
                                unitInfo[0, 2] = cdt.Rows[n][8].ToString();
                                unitInfo[0, 3] = cdt.Rows[n][0].ToString();//检测单位
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        public void SendToInstrument(string data)
        {
            byte[] dataSent = clsStringUtil.HexString2ByteArray(data);  
            try
            {
                Send(dataSent);
            }
            catch (JH.CommBase.CommPortException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        protected override CommBaseSettings CommSettings()
        {
            CommBaseSettings cs = new CommBaseSettings();
            cs.SetStandard(WorkstationDAL.Model.clsShareOption.ComPort, 9600, Handshake.none, Parity.none);//9600
            return cs;
        }

        /// <summary>
        /// 接收返回数据
        /// </summary>
        /// 根据发送数据解析接收数据
        /// <param name="c"></param>
        protected override void OnRxChar(byte c)
        {
            arrl.Add(c);
            rd = rd + 1;
            string data = "";
            if (Global.SendToTYData == "1BBF")
            {
                //握手数据
                if (rd == 2)
                {
                    byte[] tbytes = new byte[2];
                    for (int i = 0; i < arrl.Count; i++)
                    {
                        tbytes[i] = Convert.ToByte(arrl[i]);
                        data = data + tbytes[i].ToString("X2");
                    }
                    if (data == "1B01" || data == "1B02")
                    {
                        rd = 0;
                        arrl.Clear();
                        DataReadTable.Clear();
                        //发送消息给主界面
                        if (MessageNotification.GetInstance() != null)
                        {
                            MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadTY16, data);
                        }
                    }
                }
            }
            if (Global.SendToTYData == "1BB1")
            {
                //读取第一组数据
                if (rd == 75)
                {
                    byte[] tbytes = new byte[6];
                    tbytes[0] = Convert.ToByte(arrl[8]);
                    tbytes[1] = Convert.ToByte(arrl[6]);
                    tbytes[2] = Convert.ToByte(arrl[5]);
                    tbytes[3] = Convert.ToByte(arrl[4]);
                    tbytes[4] = Convert.ToByte(arrl[3]);
                    tbytes[5] = Convert.ToByte(arrl[2]);

                    int i1 = Int32.Parse(Convert.ToString(tbytes[0], 16), System.Globalization.NumberStyles.HexNumber);
                    int i2 = Int32.Parse(Convert.ToString(tbytes[1], 16), System.Globalization.NumberStyles.HexNumber);
                    int i3 = Int32.Parse(Convert.ToString(tbytes[2], 16), System.Globalization.NumberStyles.HexNumber);
                    int i4 = Int32.Parse(Convert.ToString(tbytes[3], 16), System.Globalization.NumberStyles.HexNumber);
                    int i5 = Int32.Parse(Convert.ToString(tbytes[4], 16), System.Globalization.NumberStyles.HexNumber);
                    int i6 = Int32.Parse(Convert.ToString(tbytes[5], 16), System.Globalization.NumberStyles.HexNumber);
                    //检测时间
                    SChkTime = "20" + i1.ToString() + "/" + i2.ToString() + "/" + i3.ToString() + " " + i4.ToString()
                        + ":" + i5.ToString() + ":" + i6.ToString();
                    //判断哪个通道
                    byte[] bt = new byte[1];
                    bt[0] = Convert.ToByte(arrl[9]);
                    string IstestNum = Convert.ToString(bt[0], 2);
                    //给二进制数补0
                    if (IstestNum.Length < 8)
                    {
                        for (int k = IstestNum.Length; k < 8; k++)
                        {
                            IstestNum = "0" + IstestNum;
                        }
                    }

                    int h = 0;

                    //for循环读取 
                    for (int j = 15; j < 75; j++)
                    {
                        groundone = IsShiLiu(Convert.ToString(Convert.ToInt32(arrl[j].ToString()), 16)) + IsShiLiu(Convert.ToString(Convert.ToInt32(arrl[j + 1].ToString()), 16))
                                                + IsShiLiu(Convert.ToString(Convert.ToInt32(arrl[j + 2].ToString()), 16)) + IsShiLiu(Convert.ToString(Convert.ToInt32(arrl[j + 3].ToString()), 16));
                        string hole = IstestNum.Substring(h, 1);
                        h = h + 1;
                        if (hole == "1")
                        {
                            uint num = uint.Parse(groundone, System.Globalization.NumberStyles.AllowHexSpecifier);

                            byte[] floatVals = BitConverter.GetBytes(num);

                            float f = BitConverter.ToSingle(floatVals, 0);

                            double fd = Math.Round(f, 2);
                            DataToTable(Convert.ToString(fd), SChkTime, h.ToString());
                        }
                        j = j + 7;
                    }
                    
                   
                    rd = 0;
                    arrl.Clear();
                   //读下一组数据
                    if (MessageNotification.GetInstance() != null)
                    {
                        MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadTY16, "JX");
                    }            
                }
            }
            if (Global.SendToTYData == "24")
            {
                //读取第二组数据
                if (rd == 73)
                {
                    byte[] tbytes = new byte[6];
                    tbytes[0] = Convert.ToByte(arrl[6]);
                    tbytes[1] = Convert.ToByte(arrl[4]);
                    tbytes[2] = Convert.ToByte(arrl[3]);
                    tbytes[3] = Convert.ToByte(arrl[2]);
                    tbytes[4] = Convert.ToByte(arrl[1]);
                    tbytes[5] = Convert.ToByte(arrl[0]);

                    int i1 = Int32.Parse(Convert.ToString(tbytes[0], 16), System.Globalization.NumberStyles.HexNumber);
                    int i2 = Int32.Parse(Convert.ToString(tbytes[1], 16), System.Globalization.NumberStyles.HexNumber);
                    int i3 = Int32.Parse(Convert.ToString(tbytes[2], 16), System.Globalization.NumberStyles.HexNumber);
                    int i4 = Int32.Parse(Convert.ToString(tbytes[3], 16), System.Globalization.NumberStyles.HexNumber);
                    int i5 = Int32.Parse(Convert.ToString(tbytes[4], 16), System.Globalization.NumberStyles.HexNumber);
                    int i6 = Int32.Parse(Convert.ToString(tbytes[5], 16), System.Globalization.NumberStyles.HexNumber);
                    //检测时间
                    SChkTime = "20" + i1.ToString() + "/" + i2.ToString() + "/" + i3.ToString() + " " + i4.ToString()
                        + ":" + i5.ToString() + ":" + i6.ToString();
                    //判断哪个通道
                    byte[] bt = new byte[1];
                    bt[0] = Convert.ToByte(arrl[7]);
                    string IstestNum = Convert.ToString(bt[0], 2);
                    //给二进制数补0
                    if (IstestNum.Length < 8)
                    {
                        for (int k = IstestNum.Length; k < 8; k++)
                        {
                            IstestNum = "0" + IstestNum;
                        }
                    }

                    int h = 0;

                    //for循环读取 
                    for (int j = 13; j < 73; j++)
                    {
                        groundone = IsShiLiu(Convert.ToString(Convert.ToInt32(arrl[j].ToString()), 16)) + IsShiLiu(Convert.ToString(Convert.ToInt32(arrl[j + 1].ToString()), 16))
                                                + IsShiLiu(Convert.ToString(Convert.ToInt32(arrl[j + 2].ToString()), 16)) + IsShiLiu(Convert.ToString(Convert.ToInt32(arrl[j + 3].ToString()), 16));
                        string hole = IstestNum.Substring(h, 1);
                        h = h + 1;
                        if (hole == "1")
                        {
                            uint num = uint.Parse(groundone, System.Globalization.NumberStyles.AllowHexSpecifier);

                            byte[] floatVals = BitConverter.GetBytes(num);

                            float f = BitConverter.ToSingle(floatVals, 0);

                            double fd = Math.Round(f, 2);
                            DataToTable(Convert.ToString(fd), SChkTime, h.ToString());
                        }
                        j = j + 7;
                    }
                    rd = 0;
                    arrl.Clear();                               
                    //发送消息给主界面
                    if (MessageNotification.GetInstance() != null)
                    {
                        MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadTY16, "JX");
                    }          
                }
                else if (rd == 2 && arrl[0].ToString() == "27" && arrl[1].ToString() == "254")
                {
                    //所有数据传送完成
                    rd = 0;
                    arrl.Clear();
                    //发送消息给主界面
                    if (MessageNotification.GetInstance() != null)
                    {
                        MessageNotification.GetInstance().OnDataRead(MessageNotification.NotificationInfo.ReadTY16, "ReadOver");
                    }                    
                }
            }
        }
        

        private string IsShiLiu(string data)
        {
            string rt = "";
            if (data == "0")
            {
                rt = "00";

            }
            else if (data.Length == 1)
            {
                rt = "0" + data;
            }
            else
            {
                rt = data;
            }
            return rt;
        }
        private void DataToTable( string checkValue, string time,string holes)
        {
            DataRow dr;
            dr = DataReadTable.NewRow();
            //dr["已保存"] = "否";
            dr["样品名称"] = "";
            dr["检测项目"] = "农药残留";
            dr["检测结果"] = checkValue;
            dr["单位"] = "%";//检测值
            dr["检测依据"] = "GB/T 5009.199-2003";
            dr["标准值"] = "50";
            dr["检测仪器"] = Global.ChkManchine;
            if (Convert.ToDouble(checkValue) < 50)
            {
                dr["结论"] = "合格";
            }
            else
            {
                dr["结论"] = "不合格";
            }            
            dr["检测单位"] = unitInfo[0, 3];
            dr["检测数量"] = "";
            dr["通道号"] = holes;
            dr["采样时间"] =System.DateTime.Now.ToString();
            dr["采样地点"] = unitInfo[0, 1];
            dr["被检单位"] = unitInfo[0, 0];
            dr["检测员"] = unitInfo[0,2];
            dr["检测时间"] = time;

            strWhere.Length = 0;
            strWhere.AppendFormat(" CheckData='{0}'", checkValue);
            strWhere.AppendFormat(" AND Checkitem='{0}'", "农药残留");
            strWhere.AppendFormat(" AND CheckTime=#{0}#", DateTime.Parse(time.Replace("-", "/")));
            dr["已保存"] = (sqlSet.IsExist(strWhere.ToString()) == true ? "是" : "否");

            DataReadTable.Rows.Add(dr);
        }
    }
}

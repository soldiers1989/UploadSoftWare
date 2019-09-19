using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using JH.CommBase;
using WorkstationModel.Model;

namespace WorkstationModel.Instrument
{
    public class clsDYRS2:CommBase
    {
        ///// <summary>
        ///// 第二代数据表
        ///// </summary>
        //private DataTable dtResultV2 = new DataTable("dtResultV2");

        /// <summary>
        /// 第三代数据表
        /// </summary>
        public DataTable checkDtbl;

        private string errMsg = string.Empty;
        private bool IsCreatDT = false;

        private StringBuilder sbShow = new StringBuilder();
        private StringBuilder sbResult = new StringBuilder();
        private StringBuilder strWhere = new StringBuilder();
        private clsResultOpr resultBll = new clsResultOpr();

        // private string settingFileName = "Data\\DYRSY2.Xml";
        private int tRecordNum = 0;
        private int rRecordNum = 0;
        private int cRecordNum = 0;
        private byte[] txBuffer = new byte[5];
        private bool IsNewVersion = false;

         /// <summary>
        /// 构造函数
        /// </summary>
        public clsDYRS2()
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

        protected override CommBaseSettings CommSettings()
        {
            CommBaseSettings cs = new CommBaseSettings();
            //string path = AppDomain.CurrentDomain.BaseDirectory + settingFileName;
            //FileInfo file = new FileInfo(path);
            //if (file.Exists)
            //{
            //    cs = CommBase.CommBaseSettings.LoadFromXML(file.OpenRead());
            //}
            //else
            //{
            cs.SetStandard(ShareOption.ComPort, 9600, Handshake.none, Parity.none);
            //}
            return cs;
        }
        /// <summary>
        /// 第二代数据读取
        /// </summary>
        public void ReadHistory()
        {
            // dtResultV2.Clear();
            checkDtbl.Clear();
            sbShow.Length = 0;
            sbResult.Length = 0;
            tRecordNum = 0;
            rRecordNum = 0;
            cRecordNum = 0;
            txBuffer[0] = 0x01;
            txBuffer[1] = 0x55;
            txBuffer[2] = 0x61;
            txBuffer[3] = 0x62;
            txBuffer[4] = 0x04;
            try
            {
                Send(txBuffer);
            }
            catch (CommPortException e)
            {
                MessageBox.Show(e.Message.ToString(), "错误");
            }
        }
        /// <summary>
        /// 读取判断版本
        /// </summary>
        public void NReadVersion()
        {
            sbShow.Length = 0;
            sbResult.Length = 0;
            tRecordNum = 0;
            rRecordNum = 0;
            cRecordNum = 0;
            txBuffer[0] = 0x01;
            txBuffer[1] = 0x56;
            txBuffer[2] = 0x41;
            txBuffer[3] = 0x41;
            txBuffer[4] = 0x04;

            try
            {
                Send(txBuffer);
            }
            catch (CommPortException e)
            {
                MessageBox.Show(e.Message.ToString(), "错误");
            }
        }

        /// <summary>
        /// 第三代
        /// </summary>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        public void NReadHistory(DateTime dtStart, DateTime dtEnd)
        {
            checkDtbl.Clear();
            string sendStr = string.Empty;
            sbShow.Length = 0;
            sbResult.Length = 0;
            int intStartYear = 0;
            int intEndYear = 0;
            if (dtStart.Year < 2000 || dtStart.Year > 2099)
            {
                MessageBox.Show("起始时间不能小于2000年,大于2099年！", "错误");
            }
            else
            {
                intStartYear = (dtStart.Year - 2000);
            }
            if (dtEnd.Year < 2000 || dtEnd.Year > 2099)
            {
                MessageBox.Show("结束时间不能小于2000年,大于2099年！", "错误");
            }
            else
            {
                intEndYear = (dtEnd.Year - 2000);
            }
            if (dtEnd < dtStart)
            {
                MessageBox.Show("结束时间不能小于起始时间！", "错误");
            }
            sendStr = "R" + intStartYear.ToString("00") + dtStart.ToString("MMdd") + intEndYear.ToString("00") + dtEnd.ToString("MMdd");

            byte[] sendBuffer = new byte[17];
            sendBuffer = String2ByteArray(sendStr);
            sbShow.Length = 0;
            tRecordNum = 0;
            rRecordNum = 0;
            cRecordNum = 0;
            try
            {
                Send(sendBuffer);
            }
            catch (CommPortException e)
            {
                MessageBox.Show(e.Message.ToString(), "错误");
            }
        }

        /// <summary>
        /// 设置仪器时间
        /// </summary>
        /// <param name="dtSet"></param>
        public void NSetTime(DateTime dtSet)
        {
            string sendStr = string.Empty;
            sbShow.Length = 0;
            sbResult.Length = 0;
            int intYear = 0;
            if (dtSet.Year < 2000 || dtSet.Year > 2099)
            {
                MessageBox.Show("设置系统时间不能小于2000年,大于2099年！", "错误");
            }
            else
            {
                intYear = (dtSet.Year - 2000);
            }
            sendStr = "S" + intYear.ToString("00") + dtSet.ToString("MMddHHmmss");
            byte[] sendBuffer = new byte[17];
            sendBuffer = String2ByteArray(sendStr);
            sbShow.Length = 0;
            tRecordNum = 0;
            rRecordNum = 0;
            cRecordNum = 0;
            try
            {
                Send(sendBuffer);
            }
            catch (CommPortException e)
            {
                MessageBox.Show(e.Message.ToString(), "错误");
            }
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="rec"></param>
        protected override void OnRxChar(byte rec)
        {
            sbShow.Append(rec.ToString("X2"));
            if (rec.ToString("X2") == "04")
            {
                if (IsNewVersion)
                {
                    string temp = sbShow.ToString();
                    if (temp.Substring(0, 4) == "0145")
                    {
                        doWithResult(temp);
                        sbResult.Length = 0;
                    }
                    else if (temp.Substring(0, 4) == "0144")
                    {
                        sbResult.Append(temp);
                    }
                    else if (temp.Substring(0, 2) == "44")
                    {
                        sbResult.Append("01");
                        sbResult.Append(temp);
                    }
                }
                else
                {
                    this.doWithResult(sbShow.ToString());
                }
                sbShow.Length = 0;
            }
        }


        /// <summary>
        /// 处理结果
        /// </summary>
        /// <param name="input"></param>
        private void doWithResult(string input)
        {
            int id = 0;
            int itype = 0;
            float checkValue = 0;
            string dtTest;
            string tempStr = string.Empty;
            if (input.Length > 0)
            {
                Match m = Regex.Match(input, "[^0-9a-fA-F]");
                if (m.Success || input.Length % 2 != 0 || input.Length / 2 < 7 || input.Substring(input.Length - 2, 2) != "04")
                {
                    errMsg = "错误:不是一个有效完整的数据！请重新读取";
                    MessageBox.Show(errMsg, "错误");
                }
                else
                {
                    string item = string.Empty;
                    DataRow dr;

                    //得到第一个命令头
                    if (input.Substring(0, 2) == "56" || input.Substring(0, 4) == "0156")
                    {
                        IsNewVersion = true;
                        //FoodClient.frmMain.formAutoTakeSFY.IsNewVersion = true;
                        //FoodClient.frmMain.formAutoTakeSFY.SetState();
                        if (clsMessageNotify.Instance() != null)
                        {
                            //表示读取版本
                            clsMessageNotify.Instance().SendMessage(clsMessageNotify.NotifyInfo.ReadSFYData, "V");
                        }
                        return;
                    }
                    if (input.Substring(0, 4) == "0144" && IsNewVersion)
                    {
                        if (input.Length % 50 != 0)
                        {
                            errMsg = "错误:不是一个有效完整的数据！请重新读取";
                            MessageBox.Show(errMsg, "错误");
                        }
                        else
                        {
                            tRecordNum = input.Length / 50;
                            for (int i = 0; i <= tRecordNum - 1; i++)
                            {
                                tempStr = HexString2AsiiString(input.Substring(i * 50 + 4, 40));
                                id = int.Parse(tempStr.Substring(0, 4));
                                checkValue = (float)(int.Parse(tempStr.Substring(4, 3)) * 0.1);
                                itype = int.Parse(tempStr.Substring(7, 1));


                                dtTest = "20" + tempStr.Substring(8, 2) + "-" + tempStr.Substring(10, 2) + "-" + tempStr.Substring(12, 2) + " ";

                                dr = checkDtbl.NewRow();
                                dr["已保存"] = id;
                                dr["检测结果"] = checkValue;
                                item = string.Empty;

                                strWhere.Length = 0;
                                switch (itype)
                                {
                                    case 1:
                                        item = "猪肉";
                                        break;
                                    case 2:
                                        item = "牛肉";
                                        break;
                                    case 3:
                                        item = "羊肉";
                                        break;
                                    case 4:
                                        item = "鸡肉";
                                        break;
                                    case 5:
                                        item = "其它";
                                        break;
                                }
                                dr["种类"] = item;
                                dr["检测时间"] = dtTest;

                                ///新增
                                strWhere.Append(" checkMachine='002'");
                                strWhere.AppendFormat(" AND MachineSampleNum='{0}'", id);
                                strWhere.AppendFormat(" AND MachineItemName='{0}'", item);
                                strWhere.AppendFormat(" AND CheckStartDate=#{0}#", dtTest);

                                dr["已保存"] = resultBll.IsExist(strWhere.ToString());
                                strWhere.Length = 0;
                                checkDtbl.Rows.Add(dr);
                            }
                            //FoodClient.frmMain.formAutoTakeSFY.ShowResult(dtResultV3);
                            if (clsMessageNotify.Instance() != null)
                            {
                                clsMessageNotify.Instance().SendMessage(clsMessageNotify.NotifyInfo.ReadSFYData, "V");//表示读取版本
                            }
                        }
                    }
                    else if (!IsNewVersion)//第二代
                    {
                        tRecordNum = int.Parse(HexString2AsiiString(input.Substring(2, 6)));
                        if (input.Length / 2 != tRecordNum * 19 + 7)
                        {
                            errMsg = "错误:不是一个有效完整的数据！请重新读取";
                            MessageBox.Show(errMsg, "错误");
                        }
                        else
                        {
                            for (int i = 0; i <= tRecordNum - 1; i++)
                            {
                                tempStr = input.Substring(8 + i * 38, 38);
                                if (tempStr.Substring(0, 2) != "1E" || tempStr.Substring(14, 2) != "20" || tempStr.Substring(18, 2) != "20" || tempStr.Substring(36, 2) != "20")
                                {
                                    cRecordNum++;
                                }
                                else
                                {
                                    id = int.Parse(HexString2AsiiString(tempStr.Substring(2, 6)));
                                    checkValue = (float)(int.Parse(HexString2AsiiString(tempStr.Substring(8, 6))) * 0.1);
                                    itype = int.Parse(HexString2AsiiString(tempStr.Substring(16, 2)));
                                    dtTest = HexString2AsiiString(tempStr.Substring(20, 16));
                                    dtTest = "20" + dtTest.Substring(0, 2) + "-" + dtTest.Substring(3, 2) + "-" + dtTest.Substring(6, 2) + " 00:00:00";

                                    // myDr = dtResultV2.NewRow();
                                    dr = checkDtbl.NewRow();

                                    dr["已保存"] = "否";
                                    dr["检测结果"] = checkValue;
                                    item = string.Empty;
                                    switch (itype)
                                    {
                                        case 1:
                                            item = "猪肉";
                                            break;
                                        case 2:
                                            item = "牛肉";
                                            break;
                                        case 3:
                                            item = "羊肉";
                                            break;
                                        case 4:
                                            item = "鸡肉";
                                            break;
                                        case 5:
                                            item = "其它";
                                            break;
                                    }
                                    dr["种类"] = item;
                                    dr["检测时间"] = DateTime.Now;
                                    strWhere.Append(" checkMachine='002'");
                                    strWhere.AppendFormat(" AND MachineSampleNum='{0}'", id);
                                    strWhere.AppendFormat(" AND MachineItemName='{0}'", item);
                                    dr["已保存"] = resultBll.IsExist(strWhere.ToString());
                                    strWhere.Length = 0;

                                    //dtResultV2.Rows.Add(myDr);
                                    checkDtbl.Rows.Add(dr);
                                    rRecordNum++;
                                }
                            }
                            //FoodClient.frmMain.formAutoTakeSFY.ShowResult(dtResultV2);
                            if (clsMessageNotify.Instance() != null)
                            {
                                clsMessageNotify.Instance().SendMessage(clsMessageNotify.NotifyInfo.ReadSFYData, "");//表示读取版本
                            }
                        }
                    }
                    else
                    {
                        errMsg = "错误:不是一个有效的数据！请重新读取" + input.ToString();
                        MessageBox.Show(errMsg, "错误");
                    }
                }
            }
            else
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("该时间段内没有检测数据！", "错误");
                //FoodClient.frmMain.formAutoTakeSFY.ShowResult(dtResultV3);
            }
        }

        private string HexString2AsiiString(string input)
        {
            string ret = string.Empty;
            int len = input.Length / 2;
            byte[] by = new byte[len];
            for (int i = 0; i < len; i++)
            {
                ret = input.Substring(i * 2, 2);
                by[i] = byte.Parse(ret, System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            ret = Encoding.ASCII.GetString(by, 0, len);
            return ret;
        }

        private byte[] String2ByteArray(string input)
        {
            int len = input.Length + 4;
            byte[] by = new byte[len];
            by[0] = 0x01;
            int b = 0;
            byte[] b1 = new byte[1];
            for (int i = 0; i < input.Length; i++)
            {
                b1 = Encoding.ASCII.GetBytes(input.Substring(i, 1));
                by[i + 1] = b1[0];
                b += by[i + 1];
            }
            byte a = byte.Parse(b.ToString("X2").Substring(b.ToString().Length - 2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            byte c = Convert.ToByte(0xFF - a + 0x01);
            b1 = Encoding.ASCII.GetBytes(c.ToString("X2").Substring(0, 1));
            by[len - 3] = b1[0];
            b1 = Encoding.ASCII.GetBytes(c.ToString("X2").Substring(1, 1));
            by[len - 2] = b1[0];
            by[len - 1] = 0x04;
            return by;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using JH.CommBase;

namespace DY.FoodClientLib
{
    public class clsDY6200 : CommBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public clsDY6200()
        {
            if (!IsCreatDT)
            {
                checkDtbl = new DataTable("checkDtbl");
                DataColumn dataCol;

                ////////////新增
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
                dataCol.ColumnName = "单位";
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
            cs.sendTimeoutMultiplier =100;
            cs.sendTimeoutConstant = 1000;
            cs.rxLowWater = 256;
            cs.rxHighWater = 256;
            cs.txWhenRxXoff = false;
            return cs;
        }

        /// <summary>
        /// 数据列表
        /// </summary>
        public DataTable checkDtbl;
        private bool IsCreatDT = false;
        private StringBuilder sbShow = new StringBuilder();
        private StringBuilder strWhere = new StringBuilder();
        private clsResultOpr resultBll = new clsResultOpr();
        private byte[] txBuffer = new byte[4];
        private const int dataLen = 26;//每个数据包的有效长度
        private DateTime dtStart, dtEnd;
        private bool isReadByTime=false;//指示是否按日期时间读取

        /// <summary>
        /// 数据读取
        /// </summary>
        public void ReadHistory(DateTime dtstart, DateTime dtend)
        {
            isReadByTime = true;
            dtStart = dtstart;
            dtEnd = dtend;
            ReadHistory();
        }

        /// <summary>
        /// 数据读取
        /// </summary>
        public void ReadHistory()
        {
            checkDtbl.Clear();
            sbShow.Length = 0;
            //getdata();
            //先发验证信息信息
            txBuffer[0] = 0x6A;
            txBuffer[1] = 0x5A;
            txBuffer[2] = 0xA5;
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
        void getdata()
        {
            txBuffer[0] = 0x6a;
            txBuffer[1] = 0x5b;
            txBuffer[2] = 0xa4;
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
        /// 清理缓存数据,删除仪器数据
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

        ///// <summary>
        ///// 接收数据
        ///// </summary>
        ///// <param name="rec"></param>
        //protected override void OnRxChar(byte rec)
        //{
        //    sbShow.Append(rec.ToString("X2"));
        //    string temp = sbShow.ToString();
        //    int ind = temp.IndexOf("6A9999999999999999");
        //    if (ind >= 0)//表示数据采集完成
        //    {
        //        if (temp == "6A5AA5696A9999999999999999")
        //        {
        //            getdata();
        //            sbShow.Length = 0;
        //            return;
        //        }

        //        if (temp.Equals("6A9999999999999999"))
        //        {
        //            throw new FormatException("暂无数据");
        //        }
        //        //01 减肥酚酞  02亚硝酸盐  03硝酸盐  04余氯  05蛋白质  06人血白蛋白  07重金属汞
        //        // 6A 010101010101 0100 D208 02 03 121024150221 ff ff ff ff ... 020202020202...
        //        string strdata = string.Empty;

        //        strdata = temp.Substring(2, ind-2);//取出有效数据位。去掉起始码和结束码

        //        //int index1 = str.IndexOf("020202020202");
        //        //int index2 = str.IndexOf("030303030303");
        //        //int index3 = str.IndexOf("040404040404");
        //        //int index4 = str.IndexOf("050505050505");
        //        //int index5 = str.IndexOf("060606060606");
        //        //int index6 = str.IndexOf("070707070707");
        //        //string str1 = str.Substring(12, index1);//第一个检测项目
        //        //string str2 = str.Substring(12 + index1, index2);//第二个检测项目
        //        //string str3 = str.Substring(12 + index2, index3);//第三个检测项目
        //        //string str4 = str.Substring(12 + index3, index4);
        //        //string str5 = str.Substring(12 + index4, index5);
        //        //string str6 = str.Substring(12 + index5, index6);
        //        //string str7 = str.Substring(12 + index6, str.IndexOf("6A9999999999999999"));
        //        //doDataResult(str1);
        //        //doDataResult(str2);
        //        //doDataResult(str3);
        //        //doDataResult(str4);
        //        //doDataResult(str5);
        //        //doDataResult(str6);
        //        //doDataResult(str7);
        //        string[] itemArry = new string[] { "减肥酚酞", "亚硝酸盐", "硝酸盐", "余氯", "蛋白质", "人血白蛋白", "重金属汞" };
        //        int itemLen = itemArry.Length;
        //        int[] indArry = new int[itemLen];
        //        string[] dataArry = new string[itemLen];
        //        for (int i = 0; i < itemLen; i++)
        //        {
        //            indArry[i] = strdata.IndexOf(string.Format("0{0}0{0}0{0}0{0}0{0}0{0}", (i + 1).ToString()));
        //        }

        //        int subIndex = 0;//临时位置索引
        //        for (int i = 0; i < itemLen; i++)
        //        {
        //            subIndex = indArry[i] + 12;
        //            dataArry[i] = string.Empty;

        //            if (i < itemLen - 1)
        //            {
        //                if (subIndex < indArry[i + 1])//表明有检测数据
        //                {
        //                    dataArry[i] = strdata.Substring(subIndex, indArry[i + 1]);
        //                }
        //            }
        //            else //最后一项特殊处理
        //            {
        //                if (subIndex + 18 < strdata.Length)
        //                {
        //                    dataArry[i] = strdata.Substring(subIndex, strdata.Length - 18);//减去尾标识长度
        //                }
        //            }
        //        }
        //        for (int i = 0; i < itemLen; i++)
        //        {
        //            if (dataArry[i].Length > 0)
        //            {
        //                doDataResult(dataArry[i], itemArry[i]);
        //            }
        //        }

        //        if (checkDtbl.Rows.Count > 0)
        //        {
        //            if (MessageNotify.Instance() != null)
        //            {
        //                MessageNotify.Instance().SendMessage(MessageNotify.NotifyInfo.ReadDY6200Data, "OK");
        //            }
        //        }
        //        else
        //        {
        //            throw new FormatException("暂无数据");
        //        }
        //    }
        //}

        /// <summary>
        /// 处理数据格式,组织有效的数据显示格式
        /// </summary>
        /// <param name="input"></param>
        /// <param name="item"></param>
        private void doDataResult(string input, string item)
        {
            string id = string.Empty;//编号
            //string item = string.Empty;//检测项目
            string itemNum = string.Empty;//项目编号
            string sdate = string.Empty;//日期
            string scheck = string.Empty;//检测值
            string result = string.Empty;//检测结论
            float checkValue = 0f;
            DataRow dr;
            int dataLen = 32;//一条数据的长度
            int len = input.Length / dataLen;
            for (int j = 0; j < len; j++)
            {
                id = StringUtil.ConvertString(input.Substring(2, 2) + input.Substring(0, 2), 16, 10);
                scheck = input.Substring(6, 2) + input.Substring(4, 2);
                //if (scheck.Equals("0000"))//无效检测值,先采取过滤的方式。2012-06-08修改为不过虑
                //{
                //    continue;
                //}
                checkValue = (float)(Convert.ToInt32(scheck, 16) * 0.01);
                //第5位为保留位
                result = input.Substring(10, 2);//01合格，02不合格,03可疑，04超范围

                sdate = "20" + input.Substring(12, 2) + "-" + input.Substring(14, 2) + "-" + input.Substring(16, 2);//年月日
                sdate += input.Substring(18, 2) + ":" + input.Substring(20, 2) + input.Substring(22, 2);//时分秒

                if (isReadByTime)
                {
                    DateTime dt = Convert.ToDateTime(sdate);
                    if (dt < dtStart || dt > dtEnd.Date.AddDays(1).AddSeconds(-1))//如果不在指定时间范围内
                    {
                        continue;
                    }
                }
                strWhere.Append(" checkMachine='004'");
                strWhere.AppendFormat(" AND MachineSampleNum='{0}'", id);
                strWhere.AppendFormat(" AND MachineItemName='{0}'", item);
                strWhere.AppendFormat(" AND CheckStartDate=#{0}#", sdate);
                dr = checkDtbl.NewRow();

                dr["编号"] = id;
                dr["检测值"] = checkValue;
                dr["单位"] = "mg/kg";
                dr["项目"] = item;
                dr["检测时间"] = sdate;
                dr["已保存"] = resultBll.IsExist(strWhere.ToString());
                strWhere.Length = 0;
                checkDtbl.Rows.Add(dr);
            }
        }

        /// <summary>
        /// 接收数据,以前的版本
        /// </summary>
        /// <param name="rec"></param>
        protected override void OnRxChar(byte rec)
        {
            sbShow.Append(rec.ToString("X2"));
            string temp = sbShow.ToString();
            int ind = temp.IndexOf("6A9999999999999999");
           
            //if (temp.IndexOf("6A9999999999999999") >= 0) //表示数据采集完成
            if (ind >= 0)//表示数据采集完成
            {
                if (temp == "6A5AA5696A9999999999999999")//测试通过
                {
                    getdata();
                    sbShow.Length = 0;
                    return;
                }
                string text = temp.Substring(0, ind);
                int index = text.Length % dataLen;
                if (index != 0)
                {
                    //throw new FormatException("不是有效数据");
                    if (MessageNotify.Instance() != null)
                    {
                        MessageNotify.Instance().SendMessage(MessageNotify.NotifyInfo.ReadDY6200Data, "不是有效数据");
                        return;
                    }
                }

                int len = text.Length / dataLen;//数据条数
                if (len <1)
                {
                    //throw new FormatException("暂无数据");
                    if (MessageNotify.Instance() != null)
                    {
                        MessageNotify.Instance().SendMessage(MessageNotify.NotifyInfo.ReadDY6200Data, "数据已清空或暂无数据");
                        return;
                    }
                }
                string id = string.Empty;//编号
                string item = string.Empty;//检测项目
                string itemNum = string.Empty;//项目编号
                string sdate = string.Empty;//日期
                string scheck = string.Empty;//检测值
                string result = string.Empty;//检测结论

                float checkValue = 0f;
                DataRow dr;
                string tt = string.Empty;

                // 01 减肥酚酞     02亚硝酸盐    03硝酸盐   04余氯  05蛋白质  06人血白蛋白   07重金属汞

                for (int i = 0; i < len; i++)
                {
                    tt = text.Substring(2 + i * dataLen, dataLen - 2);//去掉标识头取出有效数据位

                    id = StringUtil.ConvertString(tt.Substring(2, 2) + tt.Substring(0, 2), 16, 10);

                    scheck = tt.Substring(6, 2) + tt.Substring(4, 2);//检测值
                    //if (scheck.Equals("0000"))//无效检测值,先采取过滤的方式
                    //{
                    //    continue; 
                    //}
                    checkValue = (float)(Convert.ToInt32(scheck, 16) * 0.01);

                    itemNum = tt.Substring(8, 2);//检测项目类别
                    switch (itemNum)
                    {
						case "01": item = "酚酞"; break;   //原为[减肥酚酞]
						case "02": item = "亚硝酸盐"; break;
						case "03": item = "硝酸盐"; break;
						case "04": item = "余氯"; break;

                        case "05": item = "蛋白质"; break;
                        case "06": item = "人血白蛋白"; break;
                        case "07": item = "过氧化物"; break;
                        default: item = ""; break;
                    }
                    if (item == "")//其他无效数据过虑
                    {
                        continue;
                    }
                    result = tt.Substring(10, 2);//01合格，02不合格,03可疑，04超范围

                    sdate = "20" + tt.Substring(12, 2) + "-" + tt.Substring(14, 2) + "-" + tt.Substring(16, 2);//年月日
                    sdate =sdate+" "+tt.Substring(18, 2) + ":" + tt.Substring(20, 2)+":" + tt.Substring(22, 2);//时分秒

                    if (isReadByTime)
                    {
                        DateTime dt = Convert.ToDateTime(sdate);
                        if (dt < dtStart || dt > dtEnd)//如果不在指定时间范围内
                        {
                            continue;
                        }
                    }
                    strWhere.Append(" checkMachine='004'");
                    strWhere.AppendFormat(" AND MachineSampleNum='{0}'", id);
                    strWhere.AppendFormat(" AND MachineItemName='{0}'", item);
                    strWhere.AppendFormat(" AND CheckStartDate=#{0}#", sdate);
                    dr = checkDtbl.NewRow();

                    dr["编号"] = id;
                    dr["检测值"] = checkValue;
                    dr["单位"] = "mg/kg";
                    dr["项目"] = item;
                    dr["检测时间"] = sdate;
                    dr["已保存"] = resultBll.IsExist(strWhere.ToString());
                    strWhere.Length = 0;
                    checkDtbl.Rows.Add(dr);
                }
                if (checkDtbl.Rows.Count > 0)
                {
                    if (MessageNotify.Instance() != null)
                    {
                        MessageNotify.Instance().SendMessage(MessageNotify.NotifyInfo.ReadDY6200Data, "OK");
                        return;
                    }
                }
                else
                {
                    if (MessageNotify.Instance() != null)
                    {
                        MessageNotify.Instance().SendMessage(MessageNotify.NotifyInfo.ReadDY6200Data, "暂无有效数据");
                        return;
                    }
                    //throw new FormatException("暂无数据");
                }
            }
        }
    }
}

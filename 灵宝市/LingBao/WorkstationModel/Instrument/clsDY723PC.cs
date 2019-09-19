using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using JH.CommBase;
using WorkstationModel.Model;

namespace WorkstationModel.Instrument
{
    public class clsDY723PC:CommBase 
    {
        /// <summary>
        ///  构造检测数据集结果
        /// </summary>
        public DataTable checkDtbl = null;

        private readonly clsResultOpr resultBll = new clsResultOpr();

        private bool IsCreatDT = false;
        private bool isSaved = false;
        private StringBuilder strWhere = new StringBuilder();

        private DateTime dtstart;
        private DateTime dtend;
        private string dataPath = "\\foodCheck\\checkData.xml";
        private List<byte> byList = new List<byte>();
        private string recData = string.Empty;

        public clsDY723PC()
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
            cs.SetStandard(ShareOption.ComPort, 9600, Handshake.none, Parity.none); //Parity.odd;
            clsDY723PC.CommBaseSettings set = cs;
            set.autoReopen = true;

            return cs;
        }
        /// <summary>
        /// 需要根据协议修改
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void ReadHistory(DateTime start, DateTime end, int mode)
        {
            checkDtbl.Clear();
            //byList.Clear();
            dtstart = start;
            dtend = end;
            //if (System.Configuration.ConfigurationManager.AppSettings["WinceDataPath"] != null)
            //{
            //    dataPath = System.Configuration.ConfigurationManager.AppSettings["WinceDataPath"];
            //}
            if (mode == 1)//如果是串通讯模式
            {
                string send = "D" + start.ToString("yyyy-MM-dd") + "D" + end.ToString("yyyy-MM-dd");
                if (dataPath.IndexOf("test_info.xml") >= 0)
                {
                    ///如果是读取厂家提供的应用程序,多一个标识头和一标识尾
                    send = "%D" + start.ToString("yyyy-MM-dd") + "D" + end.ToString("yyyy-MM-dd") + "~";
                }
                ASCIIEncoding asciicode = new ASCIIEncoding();
                byte[] tx = asciicode.GetBytes(send);
                Send(tx);
            }
            else if (mode == 2)//如果是FTP形式
            {
                GetXmlFile();
            }
        }


        /// <summary>
        /// 读取到数据
        /// </summary>
        /// <param name="bty"></param>
        protected override void OnRxChar(byte bty)
        {
            byList.Add(bty);
            int len = byList.Count;
            if (len >= 6)//FINISH,取最后结束符 
            {
                //byte[] btyArry = byList.ToArray();
                //byList.CopyTo(btyArry);
                recData = Encoding.GetEncoding("gb2312").GetString(byList.ToArray());

                if (recData.IndexOf("FINISH") >= 0)//数据接收完成
                {
                    recData = recData.Replace("FINISH", "");//去掉尾标识

                    if (recData.IndexOf('R') < 0)
                    {
                        clsMessageNotify.Instance().SendMessage(clsMessageNotify.NotifyInfo.ReadDY723PCData, "暂无符合条件数据");
                    }
                    doDataString(recData);

                    //处理完成之后交给界面
                    if (clsMessageNotify.Instance() != null)
                    {
                        clsMessageNotify.Instance().SendMessage(clsMessageNotify.NotifyInfo.ReadDY723PCData, "OK");
                    }
                    recData = string.Empty;
                    byList.Clear();
                    return;
                }
            }
        }

        /// <summary>
        /// 假如都是中文串的情况下
        /// </summary>
        /// <param name="input"></param>
        private void doDataString(string input)
        {
            if (input.IndexOf('R') < 0)
            {
                return;
            }
            string num = string.Empty;
            string item = string.Empty;
            string checkValue = string.Empty;
            string unit = string.Empty;
            string time = string.Empty;
            string holes = string.Empty;
            string checkResult = string.Empty;
            string abs = string.Empty;
            string temp = string.Empty;
            int head = 0;
            int end = 0;
            //	 R，样本号，孔位，项目名称，abs，检测值，单位，结论，日期时间，附加校验码，FINISH 
            input = input.Remove(0, 1);//去掉第一个R

            string[] arry = input.Split('R');
            for (int i = 0; i < arry.Length; i++)
            {
                temp = arry[i];//去掉标位
                head = 1;
                end = temp.IndexOf(",", head);
                num = temp.Substring(head, end - head);

                head = end + 1;
                end = temp.IndexOf(",", head);
                holes = temp.Substring(head, end - head);//孔位

                head = end + 1;
                end = temp.IndexOf(",", head);
                item = temp.Substring(head, end - head);//项目名称


                head = end + 1;
                end = temp.IndexOf(",", head);
                abs = temp.Substring(head, end - head);//abs

                head = end + 1;
                end = temp.IndexOf(",", head);
                checkValue = temp.Substring(head, end - head);//检测值

                head = end + 1;
                end = temp.IndexOf(",", head);
                unit = temp.Substring(head, end - head);//单位

                head = end + 1;
                end = temp.IndexOf(",", head);
                checkResult = temp.Substring(head, end - head);//结论

                head = end + 1;
                end = temp.IndexOf(",", head);
                time = temp.Substring(head, end - head);//检测时间

                //head = end + 1;
                //end = temp.IndexOf(",", head);
                //time += " " + temp.Substring(head, end - head);
                DateTime dt = Convert.ToDateTime(time);
                // if (dt >= dtstart.Date && dt <= dtend.Date.AddDays(1).AddSeconds(-1))
                {
                    addNewHistoryData(num, item, holes, abs, checkValue, checkResult, unit, time);
                }
            }
        }

        /// <summary>
        /// 构造旧DY5000系列 行数据
        /// </summary>
        /// <param name="num">样品号</param>
        /// <param name="item"></param>
        /// <param name="holes"></param>
        /// <param name="abs"></param>
        /// <param name="checkValue"></param>
        /// <param name="checkResult"></param>
        /// <param name="unit"></param>
        /// <param name="time"></param>
        private void addNewHistoryData(string num, string item, string holes, string abs,
            string checkValue, string checkResult, string unit, string time)
        {
            //DataRow dr = checkDtbl.NewRow();
            //dr["样本号"] = num;
            //dr["项目"] = item;
            //dr["孔位"] = holes;
            ////dr["abs"] = abs;
            //dr["检测值"] = checkValue;//定量结果
            //// dr["定性结果"] = checkResult;
            //dr["单位"] = unit;
            //dr["检测时间"] = time;

            DataRow dr;
            dr = checkDtbl.NewRow();
            dr["已保存"] = "否";
            dr["样品名称"] = num;
            dr["检测项目"] = item;
            dr["检测结果"] = checkValue;
            dr["单位"] = unit;//检测值
            dr["检测依据"] = "";
            dr["标准值"] = "";
            dr["检测仪器"] = "";
            dr["结论"] = "";
            dr["采样时间"] = System.DateTime.Now.ToString();
            dr["采样地点"] = "";
            dr["被检单位"] = "";
            dr["检测员"] = "";
            dr["检测时间"] = time;
            checkDtbl.Rows.Add(dr);

            strWhere.Length = 0;
            strWhere.Append(" checkMachine='021'");//给它编写的检测仪器代码
            strWhere.AppendFormat(" AND HolesNum='{0}' ", holes);
            strWhere.AppendFormat(" AND MachineSampleNum='{0}'", num);//仪器内部样品编号
            strWhere.AppendFormat(" AND MachineItemName='{0}'", item);
            strWhere.AppendFormat(" AND CheckStartDate='{0}'", time);
            isSaved = resultBll.IsExist(strWhere.ToString());
            strWhere.Length = 0;

            dr["已保存"] = isSaved;
            checkDtbl.Rows.Add(dr);
        }
        /// <summary>
        /// 通过FTP方式获取wince检测数据
        /// </summary>
        private void GetXmlFile()
        {
            //string ftpIp = System.Configuration.ConfigurationManager.AppSettings["WinceFtpIp"];//wince FTP IP
            //string path = "ftp://" + ftpIp + dataPath;
            string tblName = "checkData";
            string num = "checkId";
            string item = "itemName";
            string checkValue = "result";//result //
            string unit = "unit";
            string time = "checkTime";
            string holes = "channel";
            string checkResult = "conclusion";
            string abs = "abs";

            if (dataPath.IndexOf("test_info.xml") >= 0)//如果是厂家提供的应用程序，检测数据文件放于wince根目录下
            {
                tblName = "sample_info";
                num = "no";
                item = "item";
                checkValue = "conc";
                unit = "unit";
                time = "time";
                holes = "channel";
                checkResult = "tof";
                abs = "abs";
            }
            DataSet dst = new DataSet();
            DataTable dtbl = new DataTable(tblName);

            try
            {
                //dst.ReadXml(path);
                dtbl = dst.Tables[tblName];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            DataRow[] rows = dtbl.Select(string.Format("{2}>=#{0}# AND {2}<=#{1}#", dtstart.Date, dtend.Date.AddDays(1).AddSeconds(-1), time));

            if (rows.Length < 1)
            {
                clsMessageNotify.Instance().SendMessage(clsMessageNotify.NotifyInfo.ReadDY723PCData, "暂无符合条件数据");
                return;
            }
            foreach (DataRow dr in rows)
            {
                addNewHistoryData(dr[num].ToString(), dr[item].ToString(), dr[holes].ToString(), dr[abs].ToString(),
            dr[checkValue].ToString(), dr[checkResult].ToString(), dr[unit].ToString(), dr[time].ToString());
            }

            //处理完成传回数据
            if (clsMessageNotify.Instance() != null)
            {
                clsMessageNotify.Instance().SendMessage(clsMessageNotify.NotifyInfo.ReadDY723PCData, "OK");//bty.ToString("X2")
            }
        }

    }
}

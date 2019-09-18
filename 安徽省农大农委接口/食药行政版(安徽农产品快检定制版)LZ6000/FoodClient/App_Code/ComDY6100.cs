using System;
using System.IO;
using System.Text;
using JH.CommBase;
using System.Windows.Forms;


namespace DY.FoodClientLib
{
    /// <summary>
    /// DY6100仪器通信设置
    /// 即ATP手持式荧光检测仪
    /// </summary>
    public class ComDY6100 : CommBase
    {
        private int[] array = new int[0x4e20];
        private bool bBusyImport = false;
        private int iCount = 0;
        private int iEndadd = 0;
        private int iNumber;
        private int iSerial;//串口号
        private int iStartadd = 0;

        //private int iProcent = 0;
        //private int iProcentOutput = 0;
        //private int iCountOutput = 0;
        //private int iWaitingTime;
        //private int iBuff;
        private StringBuilder sb;
        private string strContent;

        public ComDY6100()
        {
            sb = new StringBuilder();
        }

         private static string settingFileName = "Data\\DY3000.Xml";

        /// <summary>
        /// 重写参数设置
        /// </summary>
        /// <returns></returns>
        protected override CommBaseSettings CommSettings()
        {
            CommBaseSettings cs = new CommBaseSettings();
            string path = AppDomain.CurrentDomain.BaseDirectory + settingFileName;
            FileInfo f = new FileInfo(path);
            if (f.Exists)
            {
                cs = CommBase.CommBaseSettings.LoadFromXML(f.OpenRead());
            }
            else
             {
            cs.SetStandard(ShareOption.ComPort, 9600, Handshake.none, Parity.even);
            clsDY3000.CommBaseSettings s = cs;
            s.autoReopen = false;
            s.sendTimeoutMultiplier = 0;
            s.sendTimeoutConstant = 0;
            s.rxLowWater = 0;
            s.rxHighWater = 0;
            s.rxQueue = 0;
            s.txQueue = 0;
            s.checkAllSends = true;
            }
            return cs;
        }

        /// <summary>
        /// 接收到数据
        /// </summary>
        /// <param name="by"></param>
        protected override void OnRxChar(byte by)
        {
            //FoodClient.frmMain.formMain.frmAutoLD.SetChar(c.ToString("X2"));
            //string s = c.ToString("X2");
            //frmMain.formAutoTakeDY3000.SetChar(c.ToString("X2"));

            int[] iNumArray = new int[2];
            iEndadd = iNumber * 0x13;
            iEndadd = (iEndadd + iStartadd) - 1;
            try
            {
                while (iCount < iEndadd)//port.BytesToRead > 0
                {
                    //iBuff = port.ReadByte();
                    array[iCount] = by;// iBuff;
                    //if (bBusyImport)
                    //{
                    //    iCountOutput = iCount;
                    //    iProcent = iEndadd / 100;
                    //    iCountOutput = iCount % iProcent;
                    //    if (iCountOutput == 0)
                    //    {
                    //        iProcentOutput++;
                    //        //UpdateUI(btnImport, progressBar1, iProcentOutput);
                    //    }
                    //}
                    iCount++;
                }
                if (bBusyImport)
                {
                    if (iCount >= iEndadd)//读到最后
                    {
                        readArray();
                        //SaveToFile();//格式化
                        GetFormatData();
                        bBusyImport = false;
                        ClearArray();
                        sendCode(0x45); //end

                        //port.Write("E");
                        // Console.WriteLine("if (iCount == iEndadd)");
                    }
                }
            }
            catch
            {
                sendCode(0x45); //end
                base.Close();
                //port.Write("E");
                //port.Close();
                //MessageBox.Show(sMessageErrReadingData);
            }
        }

        public void sendCode(byte by)
        {
            base.Send(by);
        }
        /// <summary>
        /// 直接发送字符串
        /// </summary>
        /// <param name="code"></param>
        public void sendCode(string code)
        {
            byte[] by = StringToByteArray(code);
            base.Send(by);
        }

        /// <summary>
        /// 把字符串转化字节数组
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <returns></returns>
        private byte[] StringToByteArray(string input)
        {
            byte[] bs = System.Text.Encoding.Default.GetBytes(input);//Default 编码可能会存在问题
            return bs;
        }
        /// <summary>
        /// 清理数组
        /// </summary>
        private void ClearArray()
        {
            int b = 0;
            iStartadd = 0;
            array.Initialize();
            while (iCount > b)
            {
                array[b] = 0;
                b++;
            }
            iCount = 0;
            //iProcentOutput = 0;
            //iProcent = 0;
        }

        /// <summary>
        /// 发送结果条数
        /// </summary>
        public void GetResultNR()
        {
            sendCode(0x4e);
            //port.Write("N");
        }
        /// <summary>
        /// 获取串口号
        /// </summary>
        public void GetSerialNr()
        {
            sendCode(0x47);
            //port.Write("G");
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        private void readArray()
        {
            for (int i = 0; i <= iCount; i++)
            {
                //strContent = strContent + array[i];
                //strContent = strContent + "; ";
                sb.Append(array[i]);
                sb.Append(";");

                if ((array[i] == 110) & (i <= 6))//结果条数
                {
                    i++;
                    i++;
                    iNumber = array[i];
                    iNumber = iNumber << 8;
                    i--;
                    iNumber += array[i];
                    i--;
                }

                //获取串口号
                if ((array[i] == 0x67) & (i <= 10))//103
                {
                    i++;
                    i++;
                    iSerial = array[i];
                    iSerial = iSerial << 8;
                    i--;
                    iSerial += array[i];
                    iStartadd = i + 2;
                    i--;
                }
                iEndadd = i;
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        public void GetData()
        {
            sb.Length = 0;
            if (!bBusyImport)
            {
                bBusyImport = false;
                ClearArray();
                //iWaitingTime = 300;
                GetResultNR();
                readArray();
                GetSerialNr();//可以去掉
                System.Threading.Thread.Sleep(500);
                readArray();
               // while (iNumber != 0)
                {
                    base.Send(0x52);//send "R"
                    bBusyImport = true;

                }
                //if (iNumber != 0)//有数据
                //{
                //    ImportData();
                //    //Thread.Sleep(0x3e8);
                //    bBusyImport = true;
                //    RecieveData();//接收数据
                //}
                //else
                //{
                //    //没有数据
                //    //MessageBox.Show(sMessageNoResults);
                //}

            }
            else //数据操作有误
            {
                //MessageBox.Show(sMessageDuringImport, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //private void ImportData()
        //{
        //    try
        //    {
        //        if (base.Online)//port.IsOpen
        //        {
        //            base.Send(0x52);//一个字节一个字节发送到pc
        //            // port.Write("R");
        //        }
        //        else
        //        {
        //            base.Send(0x45);
        //            //port.Write("F");
        //            ImportData();
        //        }
        //    }
        //    catch
        //    {
        //        //MessageBox.Show(sMessageErrReadingData);
        //    }
        //}
        //private void RecieveData()
        //{

        //}

        /// <summary>
        /// 清除仪器内部数据
        /// </summary>
        public void deleteResults()
        {
            base.Send(0x44);//D
        }

        /// <summary>
        /// 最后格式化字符串,输出
        /// 数据格式：
        /// 仪器、测试号、结论、测试值、下限、上限、分组号、检测编点、时间、星期、日期
        /// </summary>
        /// <returns></returns>
        public void GetFormatData()
        {
            int b = 0;
            int x = iStartadd + 1;
            strContent = sb.ToString();
            string strData = string.Empty;//数据

            //strPath = txtFileName.Text;
            //StreamWriter sw = new StreamWriter(strPath, false);

            if (strContent != "")
            {
                try
                {
                    x = iStartadd + 2;

                    // sw.WriteLine("SERIAL-NR, TEST-NR, RESULT, RLU, DOWN, UP, GROUP, PROG, TIME, DAY, DATE,");

                    string strDay;//星期
                    string strResult;//结论

                    while (b < iNumber)
                    {
                        strDay = string.Empty;
                        strResult = string.Empty;
                        strData = string.Empty;

                        int iResultNr = array[x];
                        iResultNr = iResultNr << 8;
                        x--;
                        iResultNr += array[x];

                        x += 3;
                        int iRLU = array[x];
                        iRLU = iRLU << 8;
                        x--;
                        iRLU += array[x];
                        x += 3;
                        int iDown = array[x];
                        iDown = iDown << 8;
                        x--;
                        iDown += array[x];
                        x += 3;
                        int iUp = array[x];
                        iUp = iUp << 8;
                        x--;
                        iUp += array[x];
                        x += 2;
                        int iGroup = array[x];//分组号
                        x++;
                        int iProg = array[x];//检测点号
                        x++;
                        int iSec = array[x];
                        x++;
                        int iMin = array[x];
                        x++;
                        int iHours = array[x];
                        x++;

                        //星期
                        //switch (array[x])
                        //{
                        //    case 1:
                        //        strDay = "Monday";
                        //        break;

                        //    case 2:
                        //        strDay = "Tuesday";
                        //        break;

                        //    case 3:
                        //        strDay = "Wednesday";
                        //        break;

                        //    case 4:
                        //        strDay = "Thursday";
                        //        break;

                        //    case 5:
                        //        strDay = "Friday";
                        //        break;

                        //    case 6:
                        //        strDay = "Saturday";
                        //        break;

                        //    case 7:
                        //        strDay = "Sonday";
                        //        break;

                        //    default:
                        //        strDay = "EMPTY";
                        //        break;
                        //}

                        x++;
                        int iDate = array[x];

                        x++;
                        int iMonth = array[x];
                        x++;
                        int iYear = array[x];
                        iYear = 0x7d0 + iYear;
                        x++;
                        if (iRLU <= iDown)
                        {
                            strResult = "pass";//合格
                        }
                        else if ((iRLU > iDown) & (iRLU <= iUp))
                        {
                            strResult = "warning";//不合格
                        }
                        else //错误
                        {
                            strResult = "error";
                        }

                        strData = (((((((strData + (iSerial).ToString() + ", ") + (iResultNr) + ", ") + strResult + ", ") + (iRLU) + ", ") + (iDown) + ", ") + (iUp) + ", ") + (iGroup) + ", ") + (iProg) + ", ";

                        //   sw.WriteLine(string.Concat(new object[] { strData, iHours, ":", iMin, ":", iSec, ", ", strDay, ", ", iYear, ".", iMonth, ".", iDate }));
                        b++;
                        x += 3;
                    }
                    //FoodClient.frmMain.formMain.frmAutoDY6100.SetChar(strData);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            // sw.Close();

        }
    }
}

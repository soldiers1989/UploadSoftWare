using System;
using System.IO;
using System.Text;
using JH.CommBase;
using System.Windows.Forms;


namespace DY.FoodClientLib
{
    /// <summary>
    /// DY6100����ͨ������
    /// ��ATP�ֳ�ʽӫ������
    /// </summary>
    public class ComDY6100 : CommBase
    {
        private int[] array = new int[0x4e20];
        private bool bBusyImport = false;
        private int iCount = 0;
        private int iEndadd = 0;
        private int iNumber;
        private int iSerial;//���ں�
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
        /// ��д��������
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
        /// ���յ�����
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
                    if (iCount >= iEndadd)//�������
                    {
                        readArray();
                        //SaveToFile();//��ʽ��
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
        /// ֱ�ӷ����ַ���
        /// </summary>
        /// <param name="code"></param>
        public void sendCode(string code)
        {
            byte[] by = StringToByteArray(code);
            base.Send(by);
        }

        /// <summary>
        /// ���ַ���ת���ֽ�����
        /// </summary>
        /// <param name="input">������ַ���</param>
        /// <returns></returns>
        private byte[] StringToByteArray(string input)
        {
            byte[] bs = System.Text.Encoding.Default.GetBytes(input);//Default ������ܻ��������
            return bs;
        }
        /// <summary>
        /// ��������
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
        /// ���ͽ������
        /// </summary>
        public void GetResultNR()
        {
            sendCode(0x4e);
            //port.Write("N");
        }
        /// <summary>
        /// ��ȡ���ں�
        /// </summary>
        public void GetSerialNr()
        {
            sendCode(0x47);
            //port.Write("G");
        }
        /// <summary>
        /// ��ȡ����
        /// </summary>
        private void readArray()
        {
            for (int i = 0; i <= iCount; i++)
            {
                //strContent = strContent + array[i];
                //strContent = strContent + "; ";
                sb.Append(array[i]);
                sb.Append(";");

                if ((array[i] == 110) & (i <= 6))//�������
                {
                    i++;
                    i++;
                    iNumber = array[i];
                    iNumber = iNumber << 8;
                    i--;
                    iNumber += array[i];
                    i--;
                }

                //��ȡ���ں�
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
        /// ��ȡ����
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
                GetSerialNr();//����ȥ��
                System.Threading.Thread.Sleep(500);
                readArray();
               // while (iNumber != 0)
                {
                    base.Send(0x52);//send "R"
                    bBusyImport = true;

                }
                //if (iNumber != 0)//������
                //{
                //    ImportData();
                //    //Thread.Sleep(0x3e8);
                //    bBusyImport = true;
                //    RecieveData();//��������
                //}
                //else
                //{
                //    //û������
                //    //MessageBox.Show(sMessageNoResults);
                //}

            }
            else //���ݲ�������
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
        //            base.Send(0x52);//һ���ֽ�һ���ֽڷ��͵�pc
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
        /// ��������ڲ�����
        /// </summary>
        public void deleteResults()
        {
            base.Send(0x44);//D
        }

        /// <summary>
        /// ����ʽ���ַ���,���
        /// ���ݸ�ʽ��
        /// ���������Ժš����ۡ�����ֵ�����ޡ����ޡ�����š�����㡢ʱ�䡢���ڡ�����
        /// </summary>
        /// <returns></returns>
        public void GetFormatData()
        {
            int b = 0;
            int x = iStartadd + 1;
            strContent = sb.ToString();
            string strData = string.Empty;//����

            //strPath = txtFileName.Text;
            //StreamWriter sw = new StreamWriter(strPath, false);

            if (strContent != "")
            {
                try
                {
                    x = iStartadd + 2;

                    // sw.WriteLine("SERIAL-NR, TEST-NR, RESULT, RLU, DOWN, UP, GROUP, PROG, TIME, DAY, DATE,");

                    string strDay;//����
                    string strResult;//����

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
                        int iGroup = array[x];//�����
                        x++;
                        int iProg = array[x];//�����
                        x++;
                        int iSec = array[x];
                        x++;
                        int iMin = array[x];
                        x++;
                        int iHours = array[x];
                        x++;

                        //����
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
                            strResult = "pass";//�ϸ�
                        }
                        else if ((iRLU > iDown) & (iRLU <= iUp))
                        {
                            strResult = "warning";//���ϸ�
                        }
                        else //����
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

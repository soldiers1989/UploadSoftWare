using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using com.lvrenyang;
using DYSeriesDataSet;

namespace AIO.xaml.ATP
{
    /// <summary>
    /// AtpWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AtpWindow : Window
    {
        public AtpWindow()
        {
            InitializeComponent();
        }

        private Synoxo.USBHidDevice.DeviceManagement MyDeviceManagement = new Synoxo.USBHidDevice.DeviceManagement();
        private List<clsATP> _atpLsit = new List<clsATP>();
        private clsAtpOpr _atpBll = new clsAtpOpr();
        private bool _isRead = false;
        private List<tlsTtResultSecond> _selectedRecords = null;
        private QueryThread _queryThread = null;
        private int count = 0;
        private string logType = "AtpWindow-error";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                FindTheHid();
                _queryThread = new QueryThread(this);
                _queryThread.Start();
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("初始化UI时出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_readData_Click(object sender, RoutedEventArgs e)
        {
            ReadAtpData();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            if (_atpLsit.Count > 0 && _isRead)
            {
                bool isSaveOk = true;
                int result = 0;
                try
                {
                    for (int i = 0; i < _atpLsit.Count; i++)
                    {
                        String err = String.Empty;
                        result = _atpBll.Insert(_atpLsit[i], out err);
                        if (!err.Equals("") || result == 0)
                        {
                            MessageBox.Show("保存数据失败！ 错误信息：" + err);
                            isSaveOk = false;
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    FileUtils.OprLog(6, logType, ex.ToString());
                    MessageBox.Show("保存失败！ " + ex.Message);
                    isSaveOk = false;
                    return;
                }
                finally
                {
                    if (isSaveOk)
                    {
                        MessageBox.Show("成功保存 " + _atpLsit.Count + " 条数据！");
                        _isRead = false;
                    }
                }
            }
            else
            {
                MessageBox.Show("请先读取数据!");
            }
        }

        /// <summary>
        /// 查看数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_selectData_Click(object sender, RoutedEventArgs e)
        {
            _isRead = false;
            _atpLsit = new List<clsATP>();
            DataTable data = _atpBll.GetAllAsDataTable();
            if (data != null)
            {
                _atpLsit = (List<clsATP>)IListDataSet.DataTableToIList<clsATP>(data, 1);
                this.DataGridRecord.DataContext = _atpLsit;
            }
        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_upload_Click(object sender, RoutedEventArgs e)
        {
            if (null == _selectedRecords || _selectedRecords.Count == 0)
            {
                MessageBox.Show("请选择上传条目！", "操作提示");
                return;
            }
            try
            {
                DataTable dt = ListToDataTable();
                Message msg = new Message();
                msg.what = MsgCode.MSG_UPLOAD;
                msg.obj1 = Global.samplenameadapter[0];
                msg.table = dt;
                Global.updateThread.SendMessage(msg, _queryThread);
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
            }
        }

        private DataTable ListToDataTable()
        {
            string[] PropertyName = new string[43];
            int PropertyNum = 0;
            tlsTtResultSecond Rs = new tlsTtResultSecond();
            Type t = Rs.GetType();

            foreach (PropertyInfo pi in t.GetProperties())
            {
                PropertyName[PropertyNum] = pi.Name;
                PropertyNum++;
            }
            DataSet dto = IListDataSet.ToDataSet<tlsTtResultSecond>(_selectedRecords, PropertyName);
            return dto.Tables[0];
        }

        private void ReadAtpData()
        {
            try
            {
                //获取总数据数
                count = getCount();
                int leny = 0, lenz = 0, index = 0;
                _atpLsit = new List<clsATP>();
                if (count > 0)
                {
                    //每条数据三条记录，取余
                    lenz = count % 3;
                    if (count >= 3)
                        leny = count / 3;
                    else
                        leny = count;
                    if (lenz > 0)
                        leny++;
                    for (int i = 0; i < leny; i++)
                    {
                        String cmd = Global.ATP.getCmd(index), str = "";
                        index += 3;
                        byte[] data = ReadAndWriteToDevice(cmd);
                        if (data[0] == 0xff && data[2] == 0x31 && data[3] == 0x01)
                        {
                            i--;
                            continue;
                            //count = (data[4] << 8) | data[5];
                        }
                        List<byte[]> dataList = Global.ATP.getByteList(data);
                        if (dataList.Count > 0)
                        {
                            for (int j = 0; j < dataList.Count; j++)
                            {
                                byte[] btlist = dataList[j];
                                clsATP model = new clsATP();
                                model.Atp_CheckName = LoginWindow._userAccount.UserName;
                                String strC = "", str1 = btlist[1].ToString("X2"), str2 = btlist[2].ToString("X2"), str3 = btlist[3].ToString("X2");
                                if (!str1.Equals("00") && !str1.Equals("FE"))
                                    strC += str1;
                                if (!str2.Equals("00"))
                                    strC += str2;
                                if (!str3.Equals("00"))
                                    strC += str3;
                                if (strC.Equals(""))
                                    strC = "0";

                                model.Atp_RLU = Int32.Parse(strC, System.Globalization.NumberStyles.HexNumber).ToString();
                                model.Atp_Upper = Convert.ToString((btlist[14] + btlist[15]), 10);
                                model.Atp_Lower = Convert.ToString((btlist[16]), 10);
                                if (int.Parse(model.Atp_RLU, 0) > int.Parse(model.Atp_Upper, 0))
                                    str = "超标";
                                else if (int.Parse(model.Atp_RLU, 0) < int.Parse(model.Atp_Lower, 0))
                                    str = "警告";
                                else if (int.Parse(model.Atp_RLU, 0) >= int.Parse(model.Atp_Lower, 0) && int.Parse(model.Atp_RLU, 0) <= int.Parse(model.Atp_Upper, 0))
                                    str = "通过";
                                model.Atp_Result = str;
                                model.Atp_CheckData = "20" + Convert.ToString(btlist[5], 10) + "-"
                                    + int.Parse(Convert.ToString(btlist[6], 10)).ToString("D2")
                                    + "-" + int.Parse(Convert.ToString(btlist[7], 10)).ToString("D2");
                                //model.Atp_CheckTime = int.Parse(Convert.ToString(btlist[8], 10)).ToString("D2") + ":"
                                //    + int.Parse(Convert.ToString(btlist[9], 10)).ToString("D2")
                                //    + ":" + int.Parse(Convert.ToString(btlist[17], 10)).ToString("D2");
                                model.Atp_CheckData += " " + int.Parse(Convert.ToString(btlist[8], 10)).ToString("D2") + ":"
                                    + int.Parse(Convert.ToString(btlist[9], 10)).ToString("D2")
                                    + ":" + int.Parse(Convert.ToString(btlist[17], 10)).ToString("D2");
                                if (btlist[5] != 0)
                                    _atpLsit.Add(model);
                            }
                        }
                    }
                    if (_atpLsit.Count > 0)
                    {
                        _atpLsit.Sort(delegate(clsATP x, clsATP y)
                        {
                            return y.Atp_CheckData.CompareTo(x.Atp_CheckData);
                        });
                        this.DataGridRecord.DataContext = _atpLsit;
                        _isRead = true;
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("读取数据时发生异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        /// <summary>
        /// 读取下位机返回的数据
        /// </summary>
        /// <returns></returns>
        private byte[] ReadAndWriteToDevice(String cmd)
        {
            int len = 64;
            byte[] inputdatas = new byte[len];
            try
            {
                byte[] outdatas = new byte[len];
                outdatas[0] = 0x55;
                outdatas[1] = 0x2;
                outdatas[2] = 0x1;
                outdatas[3] = 0x00;
                byte[] inputs = Global.ATP.StringToBytes(cmd, new string[] { ",", " " }, 16);
                if (inputs != null && inputs.Length > 0)
                    outdatas = inputs;
                System.Windows.Forms.Application.DoEvents();
                this.MyDeviceManagement.InputAndOutputReports(0, false, outdatas, ref inputdatas, 100);
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                throw ex;
            }
            return inputdatas;
        }

        /// <summary>
        /// 获取总记录数
        /// cmd = "0xFF 0x08 0x31 0x01 0x00 0x00 0x3A 0xFE"
        /// </summary>
        /// <returns></returns>
        private int getCount()
        {
            int len = 0;
            try
            {
                byte[] bt = new byte[8];
                bt[0] = 0xFF;
                bt[1] = 0x08;
                bt[2] = 0x31;
                bt[3] = 0x01;
                bt[4] = 0x00;
                bt[5] = 0x00;
                bt[6] = 0x3A;
                bt[7] = 0xFE;
                byte[] outdatas = new byte[64];
                MyDeviceManagement.WriteReport(0, false, bt, ref outdatas, 100);
                while (MyDeviceManagement.ReadReport(0, false, bt, ref outdatas, 100))
                {
                    if (outdatas[2] == 0x31 && outdatas[3] == 0x01)
                    {
                        len = outdatas[5];
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                throw ex;
            }
            return len;
        }

        ///  <summary>
        ///  用VID和PID查找HID设备
        ///  </summary>
        ///  <returns>
        ///   True： 找到设备
        ///  </returns>
        private bool FindTheHid()
        {
            string strvid = "0483", strpid = "5750";
            int myVendorID = 0x03EB, myProductID = 0x2013, vid = 0, pid = 0;
            try
            {
                vid = Convert.ToInt32(strvid, 16);
                pid = Convert.ToInt32(strpid, 16);
                myVendorID = vid;
                myProductID = pid;
                if (this.MyDeviceManagement.findHidDevices(ref myVendorID, ref myProductID))
                {
                    Global.ATP.getCommunication();
                    return true;
                }
            }
            catch (SystemException ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                throw ex;
            }
            return false;
        }

        private class QueryThread : ChildThread
        {
            AtpWindow wnd;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;

            public QueryThread(AtpWindow wnd)
            {
                this.wnd = wnd;
                uiHandleMessageDelegate = new UIHandleMessageDelegate(UIHandleMessage);
            }

            protected override void HandleMessage(Message msg)
            {
                base.HandleMessage(msg);
                //接收到消息之后进行处理，这里属于子线程，处理一些费时的查询操作
                switch (msg.what)
                {
                    case MsgCode.MSG_RECORD_INIT:
                        //wnd._records = RecordHelper.ReadRecord(wnd._strRecordsDir);
                        break;

                    case MsgCode.MSG_RECORD_QUERY:
                        // 查询记录
                        //wnd._filteredRecords = wnd.FilterRecords();
                        break;
                    default:
                        break;
                }
                try
                {
                    wnd.Dispatcher.Invoke(uiHandleMessageDelegate, msg);
                }
                catch (Exception ex)
                {
                    FileUtils.OprLog(6, wnd.logType, ex.ToString());
                    Console.WriteLine(ex.Message);
                }
            }

            // 这个函数是通过代理调用的。根据消息类别，将数据更新到UI。这里处理的是不费时的操作。
            private void UIHandleMessage(Message msg)
            {
                switch (msg.what)
                {
                    case MsgCode.MSG_UPLOAD:
                        wnd.LabelInfo.Content = msg.result ? "提示：成功上传 " + Global.UploadSCount + " 条数据！" : "提示：上传失败！";
                        break;
                    default:
                        break;
                }
            }
        }

        private void DataGridRecord_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                _selectedRecords = new List<tlsTtResultSecond>();
                for (int i = 0; i < DataGridRecord.SelectedItems.Count; i++)
                {
                    clsATP model = (clsATP)DataGridRecord.SelectedItems[i];
                    tlsTtResultSecond record = new tlsTtResultSecond();
                    record.CheckUnitInfo = record.Organizer = model.Atp_CheckName;
                    record.CheckValueInfo = model.Atp_RLU;
                    String str = model.Atp_Result;
                    if (str.Equals("通过"))
                    {
                        str = "合格";
                    }
                    else if (str.Equals("超标"))
                    {
                        str = "不合格";
                    }
                    else
                    {
                        str = "合格";
                    }
                    CheckPointInfo CPoint = Global.samplenameadapter[0];
                    record.CheckPlace = CPoint.CheckPointName;
                    record.CheckPlaceCode = CPoint.CheckPlaceCode;
                    record.Result = str;
                    record.ResultType = record.CheckMethod = "ATP";
                    record.CheckStartDate = model.Atp_CheckData;
                    record.CheckMachine = "手持式 ATP 荧光检测仪";
                    record.CheckNo = DateTime.Now.ToString("yyyyMMddHHmmss");
                    if (!str.Equals("警告"))
                    {
                        _selectedRecords.Add(record);
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
            }
        }

    }
}
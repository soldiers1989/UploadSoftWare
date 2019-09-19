using AIO.xaml.Dialog;
using AIO.xaml.Print;
using DYSeriesDataSet;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using AIO.src;
using DYSeriesDataSet.DataModel;
using DY.Process;
using DYSeriesDataSet.DataSentence;

namespace AIO.xaml.Main
{
    /// <summary>
    /// DataManagementWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DataManagementWindow : Window
    {
        public DataManagementWindow()
        {
            InitializeComponent();
        }

        private MsgThread _msgThread;
        private  tlsttResultSecondOpr _bll = new tlsttResultSecondOpr();
        private StringBuilder sb = new StringBuilder();

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 标准管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonManagementSample_Click(object sender, RoutedEventArgs e)
        {
            //ManagementSample window = new ManagementSample()
            //{
            //    ShowInTaskbar = false,
            //    Owner = this
            //};
            //window.Show();
            if (Global.teststandbtn == true)
            {
                TestStandard window = new TestStandard()
                {
                    ShowInTaskbar = false,
                    Owner = this
                };
                window.Show();
            }
            else
            {
                MessageBox.Show("请在平台设置访问权限","提示");
            }
        }

        /// <summary>
        /// 被检单位管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonManagementCompany_Click(object sender, RoutedEventArgs e)
        {
            if (Global.checkcompanuy == true)
            {
                Kcompany window = new Kcompany()
                {
                    ShowInTaskbar = false,
                    Owner = this
                };
                window.Show();

            }
            else 
            {
                MessageBox.Show("请在平台设置访问权限","提示");
            }
        }
        /// <summary>
        /// 检测记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonRecord_Click(object sender, RoutedEventArgs e)
        {
            if (Global.testdatabtn == true)
            {
                RecordWindow window = new RecordWindow();
                window.ComboBoxUser.Text = LoginWindow._userAccount.UserName;
                window.ShowInTaskbar = false; window.Owner = this; window.Show();
            }
            else
            {
                MessageBox.Show("请在系统平台设置访问权限","提示");
            }
        }

        /// <summary>
        /// 任务查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonTask_Click(object sender, RoutedEventArgs e)
        {
            //Task();
            TaskDisplay window = new TaskDisplay()
            {
                ShowInTaskbar = false,
                Owner = this
            };
            window.Show();
        }

        /// <summary>
        /// 下载任务
        /// </summary>
        /// <param name="stdCode">标准代码</param>
        /// <param name="districtCode">区域编码</param>
        private string DownloadTask(string TaskTemp)
        {
            string delErr = string.Empty;
            string err = string.Empty;
            StringBuilder sb = new StringBuilder();
            DataSet dataSet = new DataSet();
            DataTable dtbl = new DataTable();
            using (StringReader sr = new StringReader(TaskTemp))
            {
                dataSet.ReadXml(sr);
            }
            int len = 0;
            if (!TaskTemp.Equals("<NewDataSet>\r\n</NewDataSet>"))
            {
                if (dataSet != null)
                {
                    len = dataSet.Tables[0].Rows.Count;
                    dtbl = dataSet.Tables[0];
                }
                //任务
                clsTaskOpr bll = new clsTaskOpr();
                //bll.Delete(string.Empty, out delErr);
                sb.Append(delErr);
                if (len == 0)
                {
                    return string.Empty;
                }
                clsTask Tst = new clsTask();
                for (int i = 0; i < len; i++)
                {
                    err = string.Empty;
                    Tst.CPCODE = dtbl.Rows[i]["CPCODE"].ToString();
                    Tst.CPTITLE = dtbl.Rows[i]["CPTITLE"].ToString();
                    Tst.CPSDATE = dtbl.Rows[i]["CPSDATE"].ToString();
                    Tst.CPEDATE = dtbl.Rows[i]["CPEDATE"].ToString();
                    Tst.CPTPROPERTY = dtbl.Rows[i]["CPTPROPERTY"].ToString();
                    Tst.CPFROM = dtbl.Rows[i]["CPFROM"].ToString();
                    Tst.CPEDITOR = dtbl.Rows[i]["CPEDITOR"].ToString();
                    Tst.CPPORGID = dtbl.Rows[i]["CPPORGID"].ToString();
                    Tst.CPPORG = dtbl.Rows[i]["CPPORG"].ToString();
                    Tst.CPEDDATE = dtbl.Rows[i]["CPEDDATE"].ToString();
                    Tst.CPMEMO = dtbl.Rows[i]["CPMEMO"].ToString();
                    Tst.PLANDETAIL = dtbl.Rows[i]["PLANDETAIL"].ToString();
                    Tst.PLANDCOUNT = dtbl.Rows[i]["PLANDCOUNT"].ToString();
                    Tst.BAOJINGTIME = dtbl.Rows[i]["BAOJINGTIME"].ToString();
                    bll.Insert(Tst, out err);
                    if (!err.Equals(string.Empty))
                    {
                        sb.Append(err);
                    }
                }
                if (sb.Length > 0)
                {
                    return sb.ToString();
                }
            }
            return string.Format("已经成功下载{0}条样品种类数据", len.ToString());
        }

        #region  检测任务下载线程
        class MsgThread : ChildThread
        {
            DataManagementWindow wnd;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;

            public MsgThread(DataManagementWindow wnd)
            {
                this.wnd = wnd;
                uiHandleMessageDelegate = new UIHandleMessageDelegate(UIHandleMessage);
            }

            protected override void HandleMessage(Message msg)
            {
                base.HandleMessage(msg);
                try
                {
                    wnd.Dispatcher.Invoke(uiHandleMessageDelegate, msg);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            protected void UIHandleMessage(Message msg)
            {
                switch (msg.what)
                {
                    case MsgCode.MSG_DownTask:
                        if (!string.IsNullOrEmpty(msg.DownLoadTask))
                        {
                            try
                            {
                                wnd.DownloadTask(msg.DownLoadTask);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("下载数据错误,或者服务链接不正常，请联系管理员!");
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        private void BtnReportSearch_Click(object sender, RoutedEventArgs e)
        {
            ReportWindow window = new ReportWindow()
            {
                ShowInTaskbar = false,
                Owner = this
            };
            window.ShowDialog();
        }

        /// <summary>
        /// 食品安全法律法规
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFalvFagui_Click(object sender, RoutedEventArgs e)
        {
            //FalvFaguiWindow window = new FalvFaguiWindow()
            //{
            //    ShowInTaskbar = false,
            //    Owner = this
            //};
            //window.ShowDialog();
            if (Global.lawsbtn == true)
            {
                LawsShow window = new LawsShow()
                {
                    ShowInTaskbar = false,
                    Owner = this
                };
                window.ShowDialog();
            }
            else 
            {
                MessageBox.Show("请在系统平台设置访问权限");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //如果接口类型为上传到广东智慧平台的话，则不显示任务更新模块
            if (Global.InterfaceType.Equals("ZH"))
            {
                Zjs.Visibility = Visibility.Collapsed;
            }
        }
        /// <summary>
        /// 检测项目下载展示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDownItem_Click(object sender, RoutedEventArgs e)
        {
            if (Global.testitembtn == true)
            {
                ShowTestItem window = new ShowTestItem()
                {
                    ShowInTaskbar = false,
                    Owner = this
                };
                window.ShowDialog();
            }
            else
            {
                MessageBox.Show("请在平台设置访问权限");
            }
        }
        /// <summary>
        /// 仪器检测项目下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonMachineItem_Click(object sender, RoutedEventArgs e)
        {
            if (Global.machineitembtn == true)
            {
                MachineItem window = new MachineItem()
                {
                    ShowInTaskbar = false,
                    Owner = this
                };
                window.ShowDialog();
            }
            else
            {
                MessageBox.Show("请在平台设置访问权限");
            }

        }
        /// <summary>
        /// 食品种类下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSampleType_Click(object sender, RoutedEventArgs e)
        {
            if (Global.foodtypebtn == true)
            {
                SampleType window = new SampleType()
                {
                    ShowInTaskbar = false,
                    Owner = this
                };
                window.ShowDialog();
            }
            else
            {
                MessageBox.Show("请在平台设置访问权限");
            }
        }
        /// <summary>
        /// 快检服务检测项目标准下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCheckstand_Click(object sender, RoutedEventArgs e)
        {
            if (Global.samplestdbtn == true)
            {
                CheckSampleStand window = new CheckSampleStand()
                {
                    ShowInTaskbar = false,
                    Owner = this
                };
                window.ShowDialog();
            }
            else
            {
                MessageBox.Show("请在平台设置访问权限");
            }

        }
        /// <summary>
        /// 更新基础数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnUpdateData.IsEnabled = false;
                if (MessageBox.Show("是否更新基础数据", "提示", MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    PercentProcess process = new PercentProcess()
                    {
                        BackgroundWork = downdata,
                        MessageInfo = "正在更新数据"
                    };
                    process.Start();
                }
                 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                btnUpdateData.IsEnabled = true;
            }
            finally
            {
                btnUpdateData.IsEnabled = true;
            }
        }

        private void downdata(Action<int> percent)
        {
            percent(0);
            try
            {
                int len = 0;
               
                //float percentage1 = (float)95 / (float)len, percentage2 = 0;
                //int count = (int)percentage1 + 5;
                
                string webUrl = Global.samplenameadapter[0].url;//服务器地址
                string username = Global.samplenameadapter[0].user;//用户名
                string password = Global.samplenameadapter[0].pwd;//密码
                DataTable dt = null;
                string err = "";
                string lasttime = "";
               
                #region 国家标准更新
                dt = _bll.GetRequestTime("", "", out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    lasttime = dt.Rows[0]["CheckStandard"].ToString();
                }

                string stdaddr = InterfaceHelper.GetServiceURL(webUrl, 8);//地址
                sb.Length = 0;
                sb.Append(stdaddr);
                sb.AppendFormat("?userToken={0}", Global.Token);
                sb.AppendFormat("&type={0}", "standard");
                sb.AppendFormat("&serialNumber={0}", Global.MachineNum);
                sb.AppendFormat("&lastUpdateTime={0}", lasttime == "" ? "2000-01-01 00:00:01" : lasttime);

                string stdtype = InterfaceHelper.HttpsPost(sb.ToString());
                if (stdtype.Length > 0)
                {
                    ResultData Jresult = JsonHelper.JsonToEntity<ResultData>(stdtype);
                    standardlist obj = JsonHelper.JsonToEntity<standardlist>(Jresult.obj.ToString());
                    if (obj.standard.Count > 0)
                    {
                        standard sd = new standard();
                        for (int i = 0; i < obj.standard.Count; i++)
                        {
                            int rt = 0;
                            sb.Length = 0;
                            sb.AppendFormat("sid='{0}' ", obj.standard[i].id);

                            dt = _bll.GetStandard(sb.ToString(), "", out err);
                            sd = obj.standard[i];

                            if (dt != null && dt.Rows.Count > 0)
                            {
                                _bll.UpdateStandard(sd);
                            }
                            rt = _bll.InsertStandard(sd);
                            if (rt == 1)
                            {
                                Global.Gitem = Global.Gitem + 1;
                            }
                        }
                    }
                }
                //_bll.UpdateRequestTime(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 6, out err);
                #endregion
                percent(20);
                #region 法律法规更新
                string lw = InterfaceHelper.GetServiceURL(webUrl, 8);//地址 msg.str1 + "iSampling/updateStatus.do";
                Global.Gitem = 0;

                dt = _bll.GetRequestTime("", "", out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    lasttime = dt.Rows[0]["Laws"].ToString();//获取上一次获取时间
                }
                sb.Length = 0;
                sb.Append(lw);
                sb.AppendFormat("?userToken={0}", Global.Token);
                sb.AppendFormat("&type={0}", "laws");
                sb.AppendFormat("&serialNumber={0}", Global.MachineNum);
                sb.AppendFormat("&lastUpdateTime={0}", lasttime == "" ? "2000-01-01 00:00:01" : lasttime);
                sb.AppendFormat("&pageNumber={0}", "");

                string law = InterfaceHelper.HttpsPost(sb.ToString());
                ResultData lawlist = JsonHelper.JsonToEntity<ResultData>(law);

                if (lawlist.msg == "操作成功" && lawlist.success == true)
                {
                    Lawlist zdata = JsonHelper.JsonToEntity<Lawlist>(lawlist.obj.ToString());
                    if (zdata.laws.Count > 0)
                    {
                        law lws = new law();
                        for (int i = 0; i < zdata.laws.Count; i++)
                        {
                            int w = 0;
                            sb.Length = 0;
                            sb.AppendFormat("wid='{0}' ", zdata.laws[i].id);

                            dt = _bll.GetLaws(sb.ToString(), "", out err);

                            lws = zdata.laws[i];
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                _bll.UpdateLaws(lws);
                            }
                            w = _bll.InsertLaws(lws);
                            if (w == 1)
                            {
                                Global.Gitem = Global.Gitem + 1;
                            }
                        }
                    }
                }
                //_bll.UpdateRequestTime(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 7, out err);
                #endregion
                percent(35);
                #region 仪器检测项目更新
                string CItemAddr = InterfaceHelper.GetServiceURL(webUrl, 5);//地址
                dt = _bll.GetRequestTime("", "", out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    lasttime = dt.Rows[0]["MachineItem"].ToString();
                }

                _bll.DeleteBaseData("", "", 1, out err);//删除原来所欲数据

                sb.Length = 0;
                Global.Gitem = 0;
                sb.Append(CItemAddr);
                sb.AppendFormat("?userToken={0}", Global.Token);
                sb.AppendFormat("&type={0}", "deviceItem");
                sb.AppendFormat("&serialNumber={0}", Global.MachineNum);
                sb.AppendFormat("&lastDateTime={0}", lasttime == "" ? "2000-01-01 00:00:01" : lasttime);//lastUpdateTime

                string Citemlist = InterfaceHelper.HttpsPost(sb.ToString());
                if (Citemlist.Length > 0)
                {
                    ResultData Jresult = JsonHelper.JsonToEntity<ResultData>(Citemlist);
                    DeviceCheckItem obj = JsonHelper.JsonToEntity<DeviceCheckItem>(Jresult.obj.ToString());
                    if (obj.deviceItem.Count > 0)
                    {
                        deviceItem DI = new deviceItem();
                        for (int i = 0; i < obj.deviceItem.Count; i++)
                        {
                            int rt = 0;
                            DataTable mdt = _bll.GetMachineSave("mid='" + obj.deviceItem[i].id + "'", "", out err);
                            if (mdt != null && mdt.Rows.Count > 0)//记录存在就更新
                            {
                                DI = obj.deviceItem[i];

                                rt = _bll.UpdateMachineItem(DI, out err);
                                if (rt == 1)
                                {
                                    Global.Gitem = Global.Gitem + 1;
                                }
                            }
                            else
                            {
                                DI = obj.deviceItem[i];

                                rt = _bll.IsertMachineItem(DI);
                                if (rt == 1)
                                {
                                    Global.Gitem = Global.Gitem + 1;
                                }
                            }
                        }
                    }
                }
                //更新请求时间
                //_bll.UpdateRequestTime(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 1, out err);
                #endregion
                percent(45);
                #region 检测项目更新
                dt = _bll.GetRequestTime("", "", out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    lasttime = dt.Rows[0]["CheckItem"].ToString();
                }
                _bll.DeleteBaseData("", "", 2, out err);//删除原来所欲数据

                string ItemAddr = InterfaceHelper.GetServiceURL(webUrl, 5);//地址
                sb.Length = 0;
                sb.Append(ItemAddr);
                sb.AppendFormat("?userToken={0}", Global.Token);
                sb.AppendFormat("&type={0}", "item");
                sb.AppendFormat("&serialName={0}", "");
                sb.AppendFormat("&lastDateTime={0}", lasttime == "" ? "2000-01-01 00:00:01" : lasttime);
                string itemlist = InterfaceHelper.HttpsPost(sb.ToString());

                if (itemlist.Length > 0)
                {
                    ResultData Jitem = JsonHelper.JsonToEntity<ResultData>(itemlist);
                    detectitem obj = JsonHelper.JsonToEntity<detectitem>(Jitem.obj.ToString());
                    if (obj.detectItem.Count > 0)
                    {
                        Global.Gitem = 0;
                        CheckItem CI = new CheckItem();
                        for (int i = 0; i < obj.detectItem.Count; i++)
                        {
                            int rt = 0;
                            sb.ToString();
                            sb.AppendFormat("cid='{0}' ", obj.detectItem[i].id);

                            dt = _bll.GetDetectItem(sb.ToString(), "", out err);

                            CI = obj.detectItem[i];
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                rt = _bll.UpdateDetectItem(CI);
                                if (rt == 1)
                                {
                                    Global.Gitem = Global.Gitem + 1;
                                }
                            }
                            else
                            {
                                rt = _bll.InsertDetectItem(CI);
                                if (rt == 1)
                                {
                                    Global.Gitem = Global.Gitem + 1;
                                }
                            }
                        }
                    }
                }
                //更新请求时间
                //_bll.UpdateRequestTime(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 9, out err);
                #endregion
                percent(65);
                #region 食品种类更新
                string saddr = InterfaceHelper.GetServiceURL(webUrl, 8);//地址
                dt = _bll.GetRequestTime("", "", out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    lasttime = dt.Rows[0]["Foodtype"].ToString();
                }

                _bll.DeleteBaseData("", "", 3, out err);//删除原来所欲数据

                sb.Length = 0;
                sb.Append(saddr);
                sb.AppendFormat("?userToken={0}", Global.Token);
                sb.AppendFormat("&type={0}", "food");
                sb.AppendFormat("&serialNumber={0}", Global.MachineNum);
                sb.AppendFormat("&lastDateTime={0}", lasttime == "" ? "2000-01-01 00:00:01" : lasttime);//lastUpdateTime

                string stype = InterfaceHelper.HttpsPost(sb.ToString());
                if (stype.Length > 0)
                {
                    ResultData Jresult = JsonHelper.JsonToEntity<ResultData>(stype);
                    sampletype obj = JsonHelper.JsonToEntity<sampletype>(Jresult.obj.ToString());
                    if (obj.food.Count > 0)
                    {
                        foodtype ft = new foodtype();
                        for (int i = 0; i < obj.food.Count; i++)
                        {
                            int rt = 0;
                            sb.Length = 0;
                            sb.AppendFormat("fid='{0}' ", obj.food[i].id);

                            dt = _bll.Getfoodtype(sb.ToString(), "", out err);

                            ft = obj.food[i];
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                rt = _bll.Updatefoodtype(ft);
                                if (rt == 1)
                                {
                                    Global.Gitem = Global.Gitem + 1;
                                }
                            }
                            else
                            {
                                rt = _bll.Insertfoodtype(ft);
                                if (rt == 1)
                                {
                                    Global.Gitem = Global.Gitem + 1;
                                }
                            }
                        }
                    }
                }
                //_bll.UpdateRequestTime(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 5, out err);
                #endregion
                percent(75);
                #region 检测样品项目标准
                string SD = InterfaceHelper.GetServiceURL(webUrl, 8);//地址 msg.str1 + "iSampling/updateStatus.do";
                Global.Gitem = 0;
                dt = _bll.GetRequestTime("", "", out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    lasttime = dt.Rows[0]["SampleItemstd"].ToString();//获取上一次获取时间
                }

                _bll.DeleteBaseData("", "", 4, out err);//删除原来所欲数据

                sb.Length = 0;
                sb.Append(SD);
                sb.AppendFormat("?userToken={0}", Global.Token);
                sb.AppendFormat("&type={0}", "foodItem");
                sb.AppendFormat("&serialNumber={0}", Global.MachineNum);
                sb.AppendFormat("&lastDateTime={0}", lasttime == "" ? "2000-01-01 00:00:01" : lasttime);//lastUpdateTime
                sb.AppendFormat("&pageNumber={0}", "");

                string std = InterfaceHelper.HttpsPost(sb.ToString());
                ResultData sslist = JsonHelper.JsonToEntity<ResultData>(std);
                if (sslist.msg == "操作成功" && sslist.success == true)
                {
                    fooditemlist flist = JsonHelper.JsonToEntity<fooditemlist>(sslist.obj.ToString());
                    if (flist.foodItem.Count > 0)
                    {
                        fooditem fi = new fooditem();
                        for (int i = 0; i < flist.foodItem.Count; i++)
                        {
                            int w = 0;
                            sb.Length = 0;
                            sb.AppendFormat("sid='{0}' ", flist.foodItem[i].id);
                            dt = _bll.GetSampleStand(sb.ToString(), "", out err);

                            fi = flist.foodItem[i];
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                w = _bll.UpdateSampleStandard(fi);
                                if (w == 1)
                                {
                                    Global.Gitem = Global.Gitem + 1;
                                }
                            }
                            else
                            {
                                w = _bll.InsertSampleStandard(fi);
                                if (w == 1)
                                {
                                    Global.Gitem = Global.Gitem + 1;
                                }
                            }

                        }
                    }
                }
                //_bll.UpdateRequestTime(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 10, out err);
                #endregion

                #region 被检单位跟新
                string rtndata = string.Empty;
                ResultData resultd = null;
                int item = 0;
                string reqtime = string.Empty;

                dt = _bll.GetRequestTime("", "", out err);//获取请求时间
                if (dt != null && dt.Rows.Count > 0)
                {
                    reqtime = dt.Rows[0]["BuinessTime"].ToString();
                }
                _bll.DeleteBaseData("","",5,out err);
                sb.Length = 0;
                string url = InterfaceHelper.GetServiceURL(webUrl, 8);
                //下载监管对象
                sb.Append(url);
                sb.AppendFormat("?userToken={0}", Global.Token);
                sb.AppendFormat("&type={0}", "regulatory");
                sb.AppendFormat("&serialName={0}", Global.MachineNum);
                sb.AppendFormat("&lastDateTime={0}", reqtime == "" ? "2000-01-01 00:00:01" : reqtime);
                sb.AppendFormat("&pageNumber={0}", "");
                rtndata = InterfaceHelper.HttpsPost(sb.ToString());
                if (rtndata.Length > 0)
                {
                    resultd = JsonHelper.JsonToEntity<ResultData>(rtndata);
                    regulatorylist regu = JsonHelper.JsonToEntity<regulatorylist>(resultd.obj.ToString());
                    if (regu.regulatory.Count > 0)
                    {
                        for (int i = 0; i < regu.regulatory.Count; i++)
                        {
                            regulator rg = regu.regulatory[i];
                            sb.Length = 0;
                            sb.AppendFormat("rid='{0}' ", rg.id);

                            dt = _bll.getregulation(sb.ToString(), "", out err);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                _bll.UpdateRegulation(rg, out err);
                                item = item + 1;
                            }
                            else
                            {
                                _bll.InsertRegulation(rg, out err);
                                item = item + 1;
                            }
                        }
                    }
                }
                percent(85);
                //经营户下载
                _bll.DeleteBaseData("", "", 6, out err);

                sb.Length = 0;
                sb.Append(url);
                sb.AppendFormat("?userToken={0}", Global.Token);
                sb.AppendFormat("&type={0}", "business");
                sb.AppendFormat("&serialName={0}", Global.MachineNum);
                sb.AppendFormat("&lastDateTime={0}", reqtime == "" ? "2000-01-01 00:00:01" : reqtime);//lastUpdateTime  增量更新
                sb.AppendFormat("&pageNumber={0}", "");
                rtndata = InterfaceHelper.HttpsPost(sb.ToString());

                if (rtndata.Length > 0)
                {
                    resultd = JsonHelper.JsonToEntity<ResultData>(rtndata);
                    businesslist regu = JsonHelper.JsonToEntity<businesslist>(resultd.obj.ToString());
                    if (regu.business.Count > 0)
                    {
                        for (int i = 0; i < regu.business.Count; i++)
                        {
                            Manbusiness rg = regu.business[i];

                            sb.Length = 0;
                            sb.AppendFormat("bid='{0}' ", rg.id);

                            dt = _bll.getbusiness(sb.ToString(), "", out err);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                _bll.Updatebusiness(rg, out err);
                            }
                            else
                            {
                                _bll.Insertbusiness(rg, out err);
                            }
                        }
                    }
                }
                //监管对象人员
                _bll.DeleteBaseData("", "", 7, out err);
                percent(95);

                sb.Length = 0;
                sb.Append(url);
                sb.AppendFormat("?userToken={0}", Global.Token);
                sb.AppendFormat("&type={0}", "personnel");
                sb.AppendFormat("&serialName={0}", Global.MachineNum);
                sb.AppendFormat("&lastDateTime={0}", reqtime == "" ? "2000-01-01 00:00:01" : reqtime);
                sb.AppendFormat("&pageNumber={0}", "");
                rtndata = InterfaceHelper.HttpsPost(sb.ToString());

                if (rtndata.Length > 0)
                {
                    resultd = JsonHelper.JsonToEntity<ResultData>(rtndata);
                    personlist regu = JsonHelper.JsonToEntity<personlist>(resultd.obj.ToString());
                    if (regu.personnel.Count > 0)
                    {
                        for (int i = 0; i < regu.personnel.Count; i++)
                        {
                            persons rg = regu.personnel[i];
                            sb.Length = 0;
                            sb.AppendFormat("pid='{0}' ", rg.id);
                            dt = _bll.getPerson(sb.ToString(), "", out err);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                _bll.UpdatePerson(rg, out err);
                            }
                            else
                            {
                                _bll.InsertPerson(rg, out err);
                            }
                        }
                    }
                }
                //_bll.UpdateRequestTime(System.DateTime.Now.AddSeconds(-5).ToString("yyyy-MM-dd HH:mm:ss"), 4, out err);//更新请求时间
                #endregion
                percent(100);

                MessageBox.Show("数据更新完成");
            }
            catch (Exception ex)
            {
                percent(100);
                MessageBox.Show(ex.Message);   
            }
           
        }

    }
}
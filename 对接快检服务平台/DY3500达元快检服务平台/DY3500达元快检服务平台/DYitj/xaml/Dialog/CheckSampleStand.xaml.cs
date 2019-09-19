﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using DYSeriesDataSet.DataSentence;
using com.lvrenyang;
using DYSeriesDataSet;
using AIO.src;
using DYSeriesDataSet.DataModel;
using DY.Process;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// CheckSampleStand.xaml 的交互逻辑
    /// </summary>
    public partial class CheckSampleStand : Window
    {
        private MsgThread _msgThread;
        private KJFWBaseData _kdb = new KJFWBaseData();
        private DataTable dt = null;
        private string err = "";
        private StringBuilder sb = new StringBuilder();
        private tlsttResultSecondOpr _bll = new tlsttResultSecondOpr();

        public CheckSampleStand()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Global.ResetSampleStd == true)
            {
                btnUpdateAllData.Visibility = Visibility.Visible;
            }
            else
            {
                btnUpdateAllData.Visibility = Visibility.Collapsed;
            }

            dt=_kdb.GetSamplestd("","",1,out err);
            if (dt != null && dt.Rows.Count > 0)
            {
                List<Ssamplestd> ItemNames = (List<Ssamplestd>)IListDataSet.DataTableToIList<Ssamplestd>(dt, 1);
                if (ItemNames.Count > 0)
                    this.DataGridRecord.DataContext = ItemNames;
            }
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private string itemstr = "";
        private void analyse(string item)
        {
            if (item == "")
                return;
            itemstr = item;
            PercentProcess process = new PercentProcess()
            {
                BackgroundWork = downdatas,
                MessageInfo = "正在更新数据"
            };
            process.Start();
            MessageBox.Show("数据下载成功！", "提示");
        }
        private void downdatas(Action<int> percent)
        {
            float p = 0;
            float sp = 0;
            int showp = 0;
            percent(0);
            fooditemlist flist = JsonHelper.JsonToEntity<fooditemlist>(itemstr);
            if (flist.foodItem.Count > 0)
            {
                fooditem fi = new fooditem();
                p = 0;
                p = (float)100 / (float)flist.foodItem.Count;

                for (int i = 0; i < flist.foodItem.Count; i++)
                {
                    sp = sp + p;
                    showp = (int)sp;
                    percent(showp);

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
            percent(100);
        }

        class MsgThread : ChildThread
        {
            CheckSampleStand wnd;
            bool _checkedDown = true;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;

            public MsgThread(CheckSampleStand wnd)
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
                    case MsgCode.MSG_SampleStand:
                        if (msg.result == true)
                        {
                            wnd.analyse(msg.responseInfo);
                            string err = "";
                            wnd._bll.DeleteBaseData(" delete_flag='1'", "", 4, out err);
                            MessageBox.Show(wnd ,"检测样品标准下载成功，共下载"+Global.Gitem +"条数据！");
                        }
                        wnd.btnDownlaw.IsEnabled = true;
                        break;

                    default:
                        break;
                }
            }
        }

        private void btnDownlaw_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _msgThread = new MsgThread(this);
                _msgThread.Start();
                btnDownlaw.IsEnabled = false;
                Message msg = new Message()
                {
                    what = MsgCode.MSG_SampleStand,
                    str1 = Global.samplenameadapter[0].url,
                    str2 = Global.samplenameadapter[0].user,
                    str3 = Global.samplenameadapter[0].pwd,
                };

                Global.workThread.SendMessage(msg, _msgThread);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sb.Length = 0;

                if (textBoxName.Text.Trim() != "" && textBoxSampleName.Text.Trim()!="")
                {
                    sb.AppendFormat(" and f.food_name like '%{0}%' and d.detect_item_name like '%{1}%'", textBoxSampleName.Text.Trim(), textBoxName.Text.Trim());
                }
                else if (textBoxName.Text.Trim() != "")
                {
                    sb.AppendFormat(" and d.detect_item_name like '%{0}%'", textBoxName.Text.Trim());
                }
                else if (textBoxSampleName.Text.Trim() != "")
                {
                    sb.AppendFormat(" and f.food_name like '%{0}%'", textBoxSampleName.Text.Trim());
                }

                dt = _kdb.GetSamplestd(sb.ToString(), "", 1, out err);
                this.DataGridRecord.DataContext = null;
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<Ssamplestd> ItemNames = (List<Ssamplestd>)IListDataSet.DataTableToIList<Ssamplestd>(dt, 1);
                    if (ItemNames.Count > 0)
                        this.DataGridRecord.DataContext = ItemNames;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DataGridRecord_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }
        /// <summary>
        /// 数据重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateAllData_Click(object sender, RoutedEventArgs e)
        {
            btnUpdateAllData.IsEnabled = false;
            PercentProcess process = new PercentProcess()
            {
                BackgroundWork = downdata,
                MessageInfo = "正在更新数据"
            };
            process.Start();
            MessageBox.Show("共成功下载" + Global.Gitem + "条数据");
            btnUpdateAllData.IsEnabled = true;
        }
        private void downdata(Action<int> percent)
        {
            string lasttime = "";
            try
            {
                string str1 = Global.samplenameadapter[0].url;
                string SD = InterfaceHelper.GetServiceURL(str1, 8);//地址 msg.str1 + "iSampling/updateStatus.do";
                Global.Gitem = 0;

                //dt = _bll.GetRequestTime("", "", out err);
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    lasttime = dt.Rows[0]["SampleItemstd"].ToString();//获取上一次获取时间
                //}
                float p = 0;
                float sp = 0;
                int showp = 0;
                percent(0);
                sb.Length = 0;
                sb.Append(SD);
                sb.AppendFormat("?userToken={0}", Global.Token);
                sb.AppendFormat("&type={0}", "foodItem");
                sb.AppendFormat("&serialNumber={0}", Global.MachineNum);
                sb.AppendFormat("&lastDateTime={0}", lasttime == "" ? "2000-01-01 00:00:01" : lasttime);
                sb.AppendFormat("&pageNumber={0}", "");
                FileUtils.KLog(sb.ToString(), "发送", 9);

                string std = InterfaceHelper.HttpsPost(sb.ToString());
                FileUtils.KLog(std, "接收", 9);
                ResultData sslist = JsonHelper.JsonToEntity<ResultData>(std);
                if (sslist.msg == "操作成功" && sslist.success == true)
                {
                    
                    fooditemlist flist = JsonHelper.JsonToEntity<fooditemlist>(sslist.obj.ToString());
                    if (flist.foodItem.Count > 0)
                    {
                        _bll.DeleteBaseData("","",4,out err);
                        p = 0;
                        p = (float)100 / (float)flist.foodItem.Count;
                        fooditem fi = new fooditem();

                        for (int i = 0; i < flist.foodItem.Count; i++)
                        {
                            sp = sp + p;
                            showp = (int)sp;
                            percent(showp);
                            int w = 0;
                            sb.Length = 0;
                            sb.AppendFormat("sid='{0}' ", flist.foodItem[i].id);

                            //dt = _bll.GetSampleStand(sb.ToString(), "", out err);

                            fi = flist.foodItem[i];
                            //if (dt != null && dt.Rows.Count > 0)
                            //{
                            //    w = _bll.UpdateSampleStandard(fi);
                            //    if (w == 1)
                            //    {
                            //        Global.Gitem = Global.Gitem + 1;
                            //    }
                            //}
                            //else
                            //{
                                w = _bll.InsertSampleStandard(fi);
                                if (w == 1)
                                {
                                    Global.Gitem = Global.Gitem + 1;
                                }
                            //}
                        }
                    }
                }
                _bll.DeleteBaseData(" delete_flag='1'", "", 4, out err);
                percent(100);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

using com.lvrenyang;
using DYSeriesDataSet;
using DYSeriesDataSet.DataModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// KJTaskwindow.xaml 的交互逻辑
    /// </summary>
    public partial class KJTaskwindow : Window
    {
        private MsgThread _msgThread;
        private clsTaskOpr _Tskbll = new clsTaskOpr();
        public KJTaskwindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //SearchTask();
                //LabelInfo.Visibility = Visibility.Collapsed;
                uploadTask();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// 更新任务
        /// </summary>
        private void uploadTask()
        {
            this.LabelInfo.Content = "信息:正在更新任务";
            //this.btnTaskUpdate.Content = "正在更新···";
            this.btnTaskUpdate.IsEnabled = false;
            
            this.btnTaskUpdate.FontSize = 16;
            try
            {
                _msgThread = new MsgThread(this);
                _msgThread.Start();
                Message msg = new Message()
                {
                    what = MsgCode.MSG_DownTask,
                    str1 = Global.KjServer.KjServerAddr,
                    str2 = Global.KjServer.Kjuser_name,
                    str3 = Global.KjServer.Kjpassword,
                };
            
                Global.workThread.SendMessage(msg, _msgThread);
            }
            catch (Exception ex)
            {
                MessageBox.Show("出现异常!\n" + ex.Message);
            }
        }
        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 任务更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTaskUpdate_Click(object sender, RoutedEventArgs e)
        {
            uploadTask();
        }

        private void DataGridRecord_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex();
            DataGridRow dataGridRow=e.Row;
            tlsTrTask dataRow = e.Row.Item as tlsTrTask;
            if (dataRow.Checktype == "已检测")
            {
                dataGridRow.Background = Brushes.YellowGreen;
            }
            else
            {
                dataGridRow.Background = Brushes.White;
            }
        }

        private void SearchTask()
        {
            DataGridRecord.ItemsSource = null;
            string stime = DateTime.Now.ToString("yyyy-MM-dd");
            StringBuilder sb= new StringBuilder();
            sb.Length = 0;
            sb.AppendFormat("UserName='{0}' and CheckType is null or (UserName='{1}' and CheckType='已检测' and s_sampling_date like '%{2}%')", Global.KjServer.Kjuser_name, Global.KjServer.Kjuser_name, stime);
            DataTable dt = _Tskbll.GetQtask(sb.ToString(), " CheckType,s_sampling_date ", 1);
            if (dt != null && dt.Rows.Count > 0)
            {
                List<tlsTrTask> Items = (List<tlsTrTask>)IListDataSet.DataTableToIList<tlsTrTask>(dt, 1);
                if (Items.Count > 0)
                    this.DataGridRecord.ItemsSource = Items;//(Items != null && Items.Count > 0) ? Items : null;这写法有问题
            }
        }

        /// <summary>
        /// 双击选择任务检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridRecord_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            selecttask();
        }

        class MsgThread : ChildThread
        {
            KJTaskwindow wnd;
            bool _checkedDown = true;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;

            public MsgThread(KJTaskwindow wnd)
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
                    //case MsgCode.MSG_RECEIVETASK:
                    //    if (msg.result == true)
                    //    {
                    //        MessageBox.Show("任务接收成功");
                    //        wnd.SearchTask();
                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show("任务接收失败，失败原因：" + msg.responseInfo);
                    //    }
                    //    break;
                    case MsgCode.MSG_DownTask:
                        wnd.btnTaskUpdate.IsEnabled = true;

                        wnd.btnTaskUpdate.FontSize = 24;
                        wnd.SearchTask();

                        wnd.LabelInfo.Visibility = Visibility.Visible;
                        wnd.LabelInfo.Content = "下载" + msg.responseInfo + "条数据！ ";
                        break;
                    case MsgCode.MSG_SweepCode :
                        wnd.btndown.IsEnabled = true;
                        wnd.SearchTask();
                        wnd.LabelInfo.Visibility = Visibility.Visible;
                        wnd.LabelInfo.Content = "下载" + msg.responseInfo + "条数据！";
                        break;
                    //case MsgCode.MSG_OBJECTASK:
                    //    wnd.btnoject.IsEnabled = true;
                    //    if (msg.result == true)
                    //    {
                    //        wnd.DeleteTask(2, Global.samplingnumRecive);
                    //        MessageBox.Show("任务拒收成功");
                    //        wnd.SearchTask();
                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show("任务拒收失败，失败原因：" + msg.responseInfo);
                    //    }

                        //break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// 检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            selecttask();
        }
        /// <summary>
        /// 选择检测任务事件
        /// </summary>
        private void selecttask()
        {
            Global.ManuTest = false;
            tlsTrTask tlsTrTask;
            try
            {
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    //判断是否重检
                    ArrayList retest = new ArrayList();
                    retest.Clear();
                    for (int i = 0; i < DataGridRecord.SelectedItems.Count; i++)
                    {
                        tlsTrTask = (tlsTrTask)DataGridRecord.SelectedItems[i];
                        if (retest.Count == 0)
                        {
                            retest.Add(tlsTrTask.Checktype);
                        }
                        if (tlsTrTask.Checktype == "已检测")
                        {
                            if (Global.redetectionbtn == true)
                            {
                                if (MessageBox.Show("本次选择的检测任务有 已检测 ，是否重检", "提示", MessageBoxButton.YesNoCancel, MessageBoxImage.Question) != MessageBoxResult.Yes)
                                {
                                    return;
                                }
                                break;
                            }
                            else
                            {
                                MessageBox.Show("请在平台上设置重检权限", "提示");
                                return;
                            }

                        }
                    }
                    //判断是否同一个模块
                    ArrayList Mokuailist = new ArrayList();
                    Mokuailist.Clear();
                    for (int j = 0; j < DataGridRecord.SelectedItems.Count; j++)
                    {
                        tlsTrTask = (tlsTrTask)DataGridRecord.SelectedItems[j];
                        if (Mokuailist.Count == 0)
                        {
                            Mokuailist.Add(tlsTrTask.mokuai);
                        }
                        else
                        {
                            if (!Mokuailist.Contains(tlsTrTask.mokuai))
                            {
                                MessageBox.Show("只能选择相同的分光光度、或胶体金、或干化学");
                                return;
                            }
                        }
                    }
                    //判断是否同一个检测项目
                    ArrayList Itemlist = new ArrayList();
                    Itemlist.Clear();
                    for (int j = 0; j < DataGridRecord.SelectedItems.Count; j++)
                    {
                        tlsTrTask = (tlsTrTask)DataGridRecord.SelectedItems[j];

                        if (Itemlist.Count == 0)
                        {
                            Itemlist.Add(tlsTrTask.item_name);
                        }
                        else
                        {
                            if (!Itemlist.Contains(tlsTrTask.item_name))
                            {
                                MessageBox.Show("只能选择相同的检测项目");
                                return;
                            }
                        }
                    }
                    Global.TestCompany = new string[DataGridRecord.SelectedItems.Count];//被检单位
                    //加载样品
                    Global.Testsample = new string[DataGridRecord.SelectedItems.Count, 2];
                    for (int j = 0; j < DataGridRecord.SelectedItems.Count; j++)
                    {
                        tlsTrTask = (tlsTrTask)DataGridRecord.SelectedItems[j];
                        Global.Testsample[j, 0] = tlsTrTask.food_name;
                        Global.TestCompany[j] = tlsTrTask.s_reg_name;
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat(" mokuai='{0}'", tlsTrTask.mokuai);
                        sb.AppendFormat(" and item_name='{0}'", tlsTrTask.item_name);
                        sb.AppendFormat(" and food_name='{0}'", tlsTrTask.food_name);
                        sb.AppendFormat(" and s_reg_name='{0}'", tlsTrTask.s_reg_name);
                        sb.AppendFormat(" and t_task_title='{0}'", tlsTrTask.t_task_title);
                        sb.AppendFormat(" and tid='{0}'", tlsTrTask.tid);
                        sb.AppendFormat(" and s_sampling_date='{0}'", tlsTrTask.s_sampling_date);

                        DataTable dt = _Tskbll.GetQtask(sb.ToString(), "", 3);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            Global.Testsample[j, 1] = dt.Rows[0]["ID"].ToString();
                        }
                    }
                    if (Mokuailist[0].ToString() == "胶体金")
                    {
                        if (Global.Testsample.GetLength(0) > 4)
                        {
                            MessageBox.Show("胶体金检测只有4通道，不能选择样品大于4个，请重选", "提示");
                            return;
                        }
                    }
                    else if (Mokuailist[0].ToString() == "干化学")
                    {
                        if (Global.Testsample.GetLength(0) > 4)
                        {
                            MessageBox.Show("干化学检测只有4通道，不能选择样品大于4个,请重选", "提示");
                            return;
                        }
                    }
                    else if (Mokuailist[0].ToString() == "分光光度")
                    {
                        if (Global.Testsample.GetLength(0) > 16)
                        {
                            MessageBox.Show("分光光度检测只有16通道，不能选择样品大于16个,请重选", "提示");
                            return;
                        }
                    }

                    ArrayList list = new ArrayList();
                    bool feguan = false, jiti = false, gahua = false;
                    list.Clear();
                    for (int j = 0; j < DataGridRecord.SelectedItems.Count; j++)
                    {
                        tlsTrTask = (tlsTrTask)DataGridRecord.SelectedItems[j];
                        if (!list.Contains(tlsTrTask.Testmokuai))
                        {
                            list.Add(tlsTrTask.Testmokuai);
                        }
                        else
                        {
                            MessageBox.Show("请选择相同模块和相同检测项目下的检测任务", "操作提示");
                            return;
                        }

                        bool isCheckName = false;
                        if (tlsTrTask.mokuai == "分光光度")
                        {
                            //先从分光光度模块中对比检测项目，如果没有继续和胶体金和干化学模块对比
                            for (int i = 0; i < Global.fgdItems.Count; i++)
                            {
                                //对比项目名称，一致的话就进行检测
                                DYFGDItemPara FGDitem = Global.fgdItems[i];
                                if (tlsTrTask.item_name.Equals(FGDitem.Name))
                                {
                                    feguan = true;
                                    isCheckName = true;
                                    //进入分光光度项目选择界面
                                    Global.videoType = "fgd";
                                    FgdWindow window = new FgdWindow();
                                    window._itemname = tlsTrTask.item_name;
                                    window._sample = tlsTrTask.food_name;
                                    window._Company = tlsTrTask.s_reg_name;
                                    window._title = tlsTrTask.t_task_title;
                                    Global.Kitem = tlsTrTask.item_name;
                                    Global.Ksample = tlsTrTask.food_name;
                                    Global.Kmarket = tlsTrTask.s_reg_name;
                                    window.ShowInTaskbar = false;
                                    window.Owner = this;
                                    window.ShowDialog();
                                    SearchTask();
                                    //Global.NT.IsStartInterface = true;
                                    return;
                                }
                            }
                        }
                        else if (tlsTrTask.mokuai == "胶体金")
                        {
                            //和胶体金模块的检测项目对比
                            for (int i = 0; i < Global.jtjItems.Count; i++)
                            {
                                DYJTJItemPara JTJitem = Global.jtjItems[i];
                                if (tlsTrTask.item_name.Equals(JTJitem.Name))
                                {
                                    jiti = true;
                                    isCheckName = true;
                                    //进入胶体金项目选择界面
                                    Global.videoType = "jtj";
                                    JtjWindow window = new JtjWindow();
                                    window._itemname = tlsTrTask.item_name;
                                    window._sample = tlsTrTask.food_name;
                                    window._Company = tlsTrTask.s_reg_name;
                                    window._title = tlsTrTask.t_task_title;
                                    Global.Kitem = tlsTrTask.item_name;
                                    Global.Ksample = tlsTrTask.food_name;
                                    Global.Kmarket = tlsTrTask.s_reg_name;
                                    window.ShowInTaskbar = false;
                                    window.Owner = this;
                                    window.ShowDialog();
                                    SearchTask();
                                    //Global.NT.IsStartInterface = true;
                                    return;
                                }
                            }
                        }
                        else if (tlsTrTask.mokuai == "干化学")
                        {
                            //和干化学模块的检测项目对比
                            for (int i = 0; i < Global.gszItems.Count; i++)
                            {
                                DYGSZItemPara GSZitem = Global.gszItems[i];
                                if (tlsTrTask.item_name.Equals(GSZitem.Name))
                                {
                                    gahua = true;
                                    isCheckName = true;
                                    //进入胶体金项目选择界面
                                    Global.videoType = "gsz";
                                    GszWindow window = new GszWindow();
                                    window._itemname = tlsTrTask.item_name;
                                    window._sample = tlsTrTask.food_name;
                                    window._Company = tlsTrTask.s_reg_name;
                                    window._title = tlsTrTask.t_task_title;
                                    Global.Kitem = tlsTrTask.item_name;
                                    Global.Ksample = tlsTrTask.food_name;
                                    Global.Kmarket = tlsTrTask.s_reg_name;
                                    window.ShowInTaskbar = false;
                                    window.Owner = this;
                                    window.ShowDialog();
                                    SearchTask();
                                    //Global.NT.IsStartInterface = true;
                                    return;
                                }
                            }
                        }
                        //对比所有的检测项目名称都不一致时提示是否新建
                        if (!isCheckName)
                        {
                            MessageBox.Show("本地未找到检测项目【" + tlsTrTask.item + "】！是否立即创建？");
                            //itemIndex += 1;
                            //labelMsg.Content = "本地未找到检测项目【" + item.JCXMNAME + "】！请尽快联系供应商升级检测项目!";
                            ////MessageBox.Show(this, "本地未找到检测项目【" + item.JCXMNAME + "】！\r\n\r\n请尽快联系供应商升级检测项目!", "系统提示");
                            //return;

                            //Global.NT.IsStartInterface = false;
                            if (MessageBox.Show("本地未找到检测项目【" + tlsTrTask.item + "】！是否立即创建？",
                                "系统提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                FgdEditItemWindow window = new FgdEditItemWindow();
                                //window._ntProductName = tlsTrTask.item;
                                window.ShowInTaskbar = false;
                                window.Owner = this;
                                window.ShowDialog();
                                //Global.NT.IsStartInterface = true;
                            }
                            //else
                            //{
                            //    Global.NT.IsStartInterface = true;
                            //}
                        }

                        //TaskDetailedWindow window = new TaskDetailedWindow();
                        //window.GetValues(tlsTrTask);
                        //window.ShowInTaskbar = false;
                        //window.Owner = this;
                        //window.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("请选择需要的检测项目", "提示");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 扫码下载抽样单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btndown_Click(object sender, RoutedEventArgs e)
        {
            btndown.IsEnabled = false;
            LabelInfo.Visibility = Visibility.Collapsed;
            try 
            {
                _msgThread = new MsgThread(this);
                _msgThread.Start();
                Message msg = new Message()
                {
                    what = MsgCode.MSG_SweepCode,
                    str1 = Global.KjServer.KjServerAddr,
                    str2 = Global.KjServer.Kjuser_name,
                    str3 = Global.KjServer.Kjpassword,
                    str4 =txtTaskID.Text.Trim(),
                };
                Global.workThread.SendMessage(msg, _msgThread);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                btndown.IsEnabled = true;
            }
        }
    }
}

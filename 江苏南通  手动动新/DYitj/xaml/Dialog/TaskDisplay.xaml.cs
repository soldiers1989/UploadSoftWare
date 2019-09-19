using AIO.xaml.Dialog;
using com.lvrenyang;
using DYSeriesDataSet;
using DYSeriesDataSet.DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using System.Windows.Media;
using System.Windows.Controls.Primitives;
using System.Collections;

namespace AIO
{
    /// <summary>
    /// TaskDisplay.xaml 的交互逻辑
    /// </summary>
    public partial class TaskDisplay : Window
    {
        private clsTaskOpr _Tskbll = new clsTaskOpr();
        private MsgThread _msgThread;
        private bool _isShowBox = false;
        private List<tlsTrTask> _selectedRecords = null;

        public TaskDisplay()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            uploadTask();
            //this.DataGridRecord.LoadingRow += new EventHandler<DataGridRowEventArgs>(this.DataGridRecord_LoadingRow);
            btnDeleted.Visibility = Global.IsDELETED ? Visibility.Visible : Visibility.Collapsed;
            //SearchTask();
            //uploadTask();
            //searchNTtask();
            
            //datagridItem();
        }
        /// <summary>
        /// 查询南通检测任务信息
        /// </summary>
        private void searchNTtask()
        {
            this.DataGridRecord.ItemsSource = null;
            DataTable dt = _Tskbll.GetNTtask("", "ID", 1);//"istest='否'"
            if (dt != null && dt.Rows.Count > 0)
            {
                List<tlsTrTask> Items = (List<tlsTrTask>)IListDataSet.DataTableToIList<tlsTrTask>(dt, 1);
                this.DataGridRecord.ItemsSource = (Items != null && Items.Count > 0) ? Items : null;
            }
            else
                this.DataGridRecord.ItemsSource = null;

            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, new Action(datagridItem));
        }

        private void SearchTask()
        {
            this.DataGridRecord.ItemsSource = null;
            DataTable dt = _Tskbll.GetAsDataTable(string.Empty, string.Empty, 1);
            if (dt != null && dt.Rows.Count > 0)
            {
                List<tlsTrTask> Items = (List<tlsTrTask>)IListDataSet.DataTableToIList<tlsTrTask>(dt, 1);
                this.DataGridRecord.ItemsSource = (Items != null && Items.Count > 0) ? Items : null;
            }
            else
                this.DataGridRecord.ItemsSource = null;
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DataGridRecord_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        /// <summary>
        /// 双击查看任务详情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridRecord_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        //    Global.ismore = false;
        //    Global.deviceHole.HoleCount = 16;
        //    Global.NTCheckMassage = null;
        //    tlsTrTask tlsTrTask;
        //    if (DataGridRecord.SelectedItems.Count > 0)
        //    {
        //        tlsTrTask = (tlsTrTask)DataGridRecord.SelectedItems[0];
        //        for (int i = 0; i < Global.fgdItems.Count; i++)
        //        {
        //            //对比项目名称，一致的话就进行检测
        //            DYFGDItemPara FGDitem = Global.fgdItems[i];
        //            if (tlsTrTask.ItemName.Equals(FGDitem.Name))
        //            {
        //                //进入分光光度项目选择界面
        //                FgdWindow window = new FgdWindow();
        //                window.ItemName = tlsTrTask.ItemName;
        //                window.samplename = tlsTrTask.SampleName;
        //                Global._selSampleID = tlsTrTask.SampleID;
        //                Global._selSamplename=tlsTrTask.SampleName;
        //                Global._itemname = tlsTrTask.ItemName;
        //                Global._tasktime = tlsTrTask.tasktime;


        //                window.ShowInTaskbar = false;
        //                window.Owner = this;
        //                window.ShowDialog();

        //                return;
        //            }

        //        }
        //        // //和胶体金模块的检测项目对比
        //        for (int i = 0; i < Global.jtjItems.Count; i++)
        //        {
        //            DYJTJItemPara JTJitem = Global.jtjItems[i];
        //            if (tlsTrTask.ItemName.Equals(JTJitem.Name))
        //            {

        //                //进入胶体金项目选择界面

        //                JtjWindow window = new JtjWindow();
        //                window.ItemName = tlsTrTask.ItemName;
        //                window.samplename = tlsTrTask.SampleName;
        //                Global._selSampleID = tlsTrTask.SampleID;
        //                Global._selSamplename = tlsTrTask.SampleName;
        //                Global._itemname = tlsTrTask.ItemName;
        //                Global._tasktime = tlsTrTask.tasktime;

        //                window.ShowInTaskbar = false;
        //                window.Owner = this;
        //                window.ShowDialog();
        //                //Global.NT.IsStartInterface = true;
        //                return;
        //            }
        //        }
        //        //和干化学模块的检测项目对比
        //        for (int i = 0; i < Global.gszItems.Count; i++)
        //        {
        //            DYGSZItemPara GSZitem = Global.gszItems[i];
        //            if (tlsTrTask.ItemName.Equals(GSZitem.Name))
        //            {

        //                //进入胶体金项目选择界面

        //                GszWindow window = new GszWindow();
        //                window.ItemName = tlsTrTask.ItemName;
        //                window.samplename = tlsTrTask.SampleName;
        //                Global._selSampleID = tlsTrTask.SampleID;
        //                Global._selSamplename = tlsTrTask.SampleName;
        //                Global._itemname = tlsTrTask.ItemName;
        //                Global._tasktime = tlsTrTask.tasktime;

        //                window.ShowInTaskbar = false;
        //                window.Owner = this;
        //                window.ShowDialog();
        //                //Global.NT.IsStartInterface = true;
        //                return;
        //            }
        //        }
        //        MessageBox.Show("请到项目选择界面新建检测项目","提示");
        //        //if (MessageBox.Show("本地软件未检测到该检测项目，请新增方可检测", "操作提示", MessageBoxButton.YesNoCancel, MessageBoxImage.Information) == MessageBoxResult.Yes) ;
        //        //{
        //        //    AddOrUpdateSample addsample = new AddOrUpdateSample();
        //        //    addsample._sampleName = tlsTrTask.SampleName;
        //        //    addsample._projectName = tlsTrTask.ItemName;
        //        //    addsample.ShowDialog();
 
        //        //}
               

        //    }      

        }

        private void btnViewDetailed_Click(object sender, RoutedEventArgs e)
        {
            ShowTask();
        }

        private void ShowTask()
        {
            tlsTrTask tlsTrTask;
            try
            {
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    tlsTrTask = (tlsTrTask)DataGridRecord.SelectedItems[0];
                    TaskDetailedWindow window = new TaskDetailedWindow();
                    window.GetValues(tlsTrTask);
                    window.ShowInTaskbar = false;
                    window.Owner = this;
                    window.ShowDialog();
                }
                else
                {
                    MessageBox.Show("未选择任何任务!", "操作提示");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 任务更新 2017年2月25日 wenj 新版本接口
        /// </summary>
        private void uploadTask()
        {
            try
            {
                int icount = 0;
                this.LabelInfo.Content = "信息:正在更新任务";
                this.btnTaskUpdate.Content = "正在更新···";
                this.btnTaskUpdate.IsEnabled = false;
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("<?xml version='1.0' encoding='UTF-8'?><RESULT><BODY><JCRY>{0}</JCRY></BODY></RESULT>", Global.LoggonNmae);
                WebReference.Testplaninfo web = new WebReference.Testplaninfo();
                string Task = web.getTestPlanInfoByJcry(sb.ToString());
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(Task);
                NTInformation.RESULT result = new NTInformation.RESULT();
                string tasktime = string.Empty;
                XmlNodeList xList = xd.GetElementsByTagName("HEAD");
                foreach (XmlNode xn in xList)
                {
                    XmlNodeList childList = xn.ChildNodes;
                    foreach (XmlNode item in childList)
                    {
                        if (item.Name.Equals("XMLRESULT"))
                        {
                            result.XMLRESULT = item.InnerText;
                        }
                        else if (item.Name.Equals("XMLMESSAGE"))
                        {
                            result.XMLMESSAGE = item.InnerText;
                        }
                        else if (item.Name.Equals("SENDDATE"))
                        {
                            tasktime = item.InnerText;
                        }
                    }
                }
                string err = "";
                //解析Body数据
                NTInformation.TESTPLANINFO resultbody = new NTInformation.TESTPLANINFO();
                XmlNodeList bodyList = xd.GetElementsByTagName("TESTPLANINFO");
                foreach (XmlNode bd in bodyList)
                {
                    clsTask model = new clsTask();
                    XmlNodeList body = bd.ChildNodes;
                    string sampleid = string.Empty, samplename = string.Empty, itemid = string.Empty, itemname = string.Empty;
                    foreach (XmlNode item in body)
                    {
                        if (item.Name.Equals("SAMPLEUUID"))
                        {
                            model.sampleid = item.InnerText;
                        }
                        else if (item.Name.Equals("SAMPLENAME"))
                        {
                            model.samplename = item.InnerText;
                        }
                        else if (item.Name.Equals("JCXMID"))
                        {
                            model.itemid = item.InnerText;
                        }
                        else if (item.Name.Equals("JCXMNAME"))
                        {
                            model.itemname = item.InnerText;
                        }         
                    }
                    model.istest = "否";
                    model.tasktime = tasktime;
                    //查询是否保存过数据
                    sb.Length =0;
                    sb.Clear();
                    sb.Append("SampleID='");
                    sb.Append(model.sampleid);
                    sb.Append("' and SampleName='");
                    sb.Append(model.samplename);
                    sb.Append("' and ItemID='");
                    sb.Append(model.itemid );
                    sb.Append("' and ItemName='");
                    sb.Append(model.itemname );
                    //sb.Append("' and tasktime='");
                    //sb.Append(model.tasktime);
                    sb.Append("'");

                    DataTable dt= _Tskbll.GetNTtask(sb.ToString(),"",1);
                    if (dt != null && dt.Rows.Count > 0)
                    { 

                    }
                    else 
                    {
                        _Tskbll.InsertNTtask(model, out err);
                        icount = icount + 1;
                    }
                }

                this.LabelInfo.Content = "共下载"+icount+"条数据";
                this.btnTaskUpdate.Content = "任务更新";
                this.btnTaskUpdate.IsEnabled = true;
                MessageBox.Show("检测任务信息下载成功，共下载" + icount + "条数据");
                searchNTtask();
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //_isShowBox = true;
            //this.btnTaskUpdate.FontSize = 16;
            //try
            //{
            //    _msgThread = new MsgThread(this);
            //    _msgThread.Start();
            //    Message msg = new Message()
            //    {
            //        what = MsgCode.MSG_DownTask,
            //        str1 = Global.samplenameadapter[0].url,
            //        str2 = Global.samplenameadapter[0].user,
            //        str3 = Global.samplenameadapter[0].pwd
            //    };
            //    msg.args.Enqueue(Global.samplenameadapter[0].pointNum);
            //    msg.args.Enqueue(Global.samplenameadapter[0].pointName);
            //    msg.args.Enqueue(Global.samplenameadapter[0].pointType);
            //    msg.args.Enqueue(Global.samplenameadapter[0].orgName);
            //    Global.workThread.SendMessage(msg, _msgThread);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("出现异常!\n" + ex.Message);
            //}
        }

        private void datagridItem()
        {
            List<tlsTrTask> Items = new List<tlsTrTask>();
            //获取单元行
            try
            {
                bool isexit = false;
                for (int i = 0; i < DataGridRecord.Items.Count; i++)
                {
                    Items.Add((tlsTrTask)DataGridRecord.Items[i]);
                    var row = DataGridRecord.ItemContainerGenerator.ContainerFromItem(DataGridRecord.Items[i]) as DataGridRow;
                    if (row != null)
                    {
                        if (Items[i].istest.Equals("否"))
                        {
                            row.Background = new SolidColorBrush(Colors.LightCoral);
                        }
                        //isexit = false;
                        //for (int j = 0; j < Global.fgdItems.Count; j++)
                        //{
                        //    //对比项目名称，一致的话就进行检测
                        //    DYFGDItemPara FGDitem = Global.fgdItems[j];
                        //    if (Items[i].ItemName.Equals(FGDitem.Name))
                        //    {
                        //        isexit = true;
                        //        continue;
                        //    }
                        //}
                        //if (isexit == false)
                        //{
                        //    for (int j = 0; j < Global.jtjItems.Count;j++)
                        //    {
                        //        DYJTJItemPara JTJitem = Global.jtjItems[j];
                        //        if (Items[i].ItemName.Equals(JTJitem.Name))
                        //        {
                        //            isexit = true;
                        //            continue;
                        //        }
                        //    }
                        //}

                        //if (isexit == false)
                        //{
                        //    for (int j = 0; j < Global.gszItems.Count; j++)
                        //    {
                        //        DYGSZItemPara GSZitem = Global.gszItems[j];
                        //        if (Items[i].ItemName.Equals(GSZitem.Name))
                        //        {
                        //            isexit = true;
                        //            continue;
                        //        }
                        //    }
                        //}

                        //if(isexit ==false)
                        //{
                        //    row.Background = new SolidColorBrush(Colors.Red);
                        //}

                        //if (Items[i].SampleID.Equals("已上传"))
                        //    row.Background = new SolidColorBrush(Colors.Green);
                        //else if (Items[i].SampleName.Equals("上传后有修改"))
                        //    row.Background = new SolidColorBrush(Colors.Yellow);
                        //获取单元格
                        //DataRowView drv = DataGridRecord.Items[i] as DataRowView;
                        //if (Items[i].Result != null && Items[i].Result.Equals("不合格"))
                        //{
                        //DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(row);
                        //DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(9);
                        //cell.Background = new SolidColorBrush(Colors.Red);
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            //_selectedRecords = new List<clsNTtaskresult>();
            //for (int i = 0; i < DataGridRecord.SelectedItems.Count; i++)
            //{
            //    clsNTtaskresult record = (clsNTtaskresult)DataGridRecord.SelectedItems[i];
            //    _selectedRecords.Add(record);
            //}
            //for(int i=1;i<DataGridRecord.Items.Count;i++)
            //{
            //    DataRowView selectItem = DataGridRecord.Items[i] as DataRowView;
            //    if (selectItem == null)
            //    {
            //        continue;
            //    }
            //    string d= selectItem.Row[i].ToString();
            
            //    //string dd = selectItem["SampleID"].ToString();
            //}
            
        }
        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T childContent = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                childContent = v as T;
                if (childContent == null)
                {
                    childContent = GetVisualChild<T>(v);
                }
                if (childContent != null)
                {
                    break;
                }
            }
            return childContent;
        }
        private void btnTaskUpdate_Click(object sender, RoutedEventArgs e)
        {
            
            uploadTask();
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
                dataSet.ReadXml(sr);
            int len = 0;
            if (!TaskTemp.Equals("<NewDataSet>\r\n</NewDataSet>"))
            {
                if (dataSet != null)
                {
                    len = dataSet.Tables[0].Rows.Count;
                    dtbl = dataSet.Tables[0];
                }
                //bll.Delete(string.Empty, out delErr);
                sb.Append(delErr);
                if (len == 0)
                    return string.Empty;
                clsTask model = new clsTask();
                for (int i = 0; i < len; i++)
                {
                    err = string.Empty;
                    try
                    {
                        model.CPCODE = dtbl.Rows[i]["CPCODE"].ToString();
                        model.CPTITLE = dtbl.Rows[i]["CPTITLE"].ToString();
                        model.CPSDATE = dtbl.Rows[i]["CPSDATE"].ToString();
                        model.CPEDATE = dtbl.Rows[i]["CPEDATE"].ToString();
                        model.CPTPROPERTY = dtbl.Rows[i]["CPTPROPERTY"].ToString();
                        model.CPFROM = dtbl.Rows[i]["CPFROM"].ToString();
                        model.CPEDITOR = dtbl.Rows[i]["CPEDITOR"].ToString();
                        model.CPPORGID = dtbl.Rows[i]["CPPORGID"].ToString();
                        model.CPPORG = dtbl.Rows[i]["CPPORG"].ToString();
                        model.CPEDDATE = dtbl.Rows[i]["CPEDDATE"].ToString();
                        model.CPMEMO = dtbl.Rows[i]["CPMEMO"].ToString();
                        model.PLANDETAIL = dtbl.Rows[i]["PLANDETAIL"].ToString();
                        model.PLANDCOUNT = dtbl.Rows[i]["PLANDCOUNT"].ToString();
                        model.BAOJINGTIME = dtbl.Rows[i]["BAOJINGTIME"].ToString();
                        model.UDate = dtbl.Rows[i]["UDATE"].ToString();
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                    _Tskbll.InsertOrUpdate(model, out err);
                    if (!err.Equals(string.Empty))
                        sb.Append(err);
                }
                if (sb.Length > 0)
                    return sb.ToString();
            }
            this.btnTaskUpdate.FontSize = 24;
            this.btnTaskUpdate.Content = "任务更新";
            this.btnTaskUpdate.IsEnabled = true;
            SearchTask();
            return string.Format("已经成功下载{0}条样品种类数据", len.ToString());
        }

        class MsgThread : ChildThread
        {
            TaskDisplay wnd;
            bool _checkedDown = true;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;

            public MsgThread(TaskDisplay wnd)
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
                        if (!string.IsNullOrEmpty(msg.DownLoadTask) && wnd._isShowBox)
                        {
                            try
                            {
                                wnd.DownloadTask(msg.DownLoadTask);
                            }
                            catch (Exception e)
                            {
                                _checkedDown = false;
                                wnd.btnTaskUpdate.FontSize = 24;
                                wnd.btnTaskUpdate.Content = "任务更新";
                                wnd.btnTaskUpdate.IsEnabled = true;
                                wnd.LabelInfo.Content = "信息:任务更新失败";
                                MessageBox.Show("任务更新失败!请联系管理员!\n" + e.Message, "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            }
                            finally
                            {
                                if (_checkedDown)
                                {
                                    wnd._isShowBox = false;
                                    wnd.LabelInfo.Content = "信息:任务更新完成";
                                    MessageBox.Show("任务更新完成!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                }
                            }
                        }
                        else if (wnd._isShowBox)
                        {
                            wnd._isShowBox = _checkedDown = false;
                            wnd.LabelInfo.Content = "信息:任务更新失败";
                            wnd.btnTaskUpdate.FontSize = 24;
                            wnd.btnTaskUpdate.Content = "任务更新";
                            wnd.btnTaskUpdate.IsEnabled = true;
                            MessageBox.Show("任务更新失败,或者服务链接不正常，请联系管理员!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            //Console.WriteLine("任务更新失败,或者服务链接不正常，请联系管理员!");
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void btnDeleted_Click(object sender, RoutedEventArgs e)
        {
            string sErr = string.Empty, str = string.Empty;
            if (MessageBox.Show("确定要清空所有任务吗?\n注意：任务一旦清空将不可恢复，请慎重选择。", "操作提示",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _Tskbll.Delete(string.Empty, out sErr);
                str = sErr.Equals(string.Empty) ? "已成功清理所有任务!" : ("清理任务时出现错误!\n异常：" + sErr);
                SearchTask();
                MessageBox.Show(str, "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }
       
        private void DataGridRecord_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedRecords = new List<tlsTrTask>();
            for (int i = 0; i < DataGridRecord.SelectedItems.Count; i++)
            {
                tlsTrTask record = (tlsTrTask)DataGridRecord.SelectedItems[i];
                _selectedRecords.Add(record);
            }
        }
        /// <summary>
        /// 检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btncheck_Click(object sender, RoutedEventArgs e)
        {
            Global.NTCheckMassage = null;
            Global.ismore = true;//进入多项检测
            tlsTrTask tlsTrTask;
            //判断是否选择相同的检测项目
            if (DataGridRecord.SelectedItems.Count > 0)
            {
                ArrayList Itemlist = new ArrayList();
                Itemlist.Clear();
                for (int j = 0; j < DataGridRecord.SelectedItems.Count; j++)
                {
                    tlsTrTask = (tlsTrTask)DataGridRecord.SelectedItems[j];

                    if (Itemlist.Count == 0)
                    {
                        Itemlist.Add(tlsTrTask.ItemName);
                    }
                    else
                    {
                        if (!Itemlist.Contains(tlsTrTask.ItemName))
                        {
                            MessageBox.Show("请选择相同的检测项目进行测试");
                            return;
                        }
                    }
                }
                if (Itemlist.Count > 16)
                {
                    MessageBox.Show("不能选择大于16个样品进行测试");
                    return;
                }
                Global.NTCheckMassage = new string[DataGridRecord.SelectedItems.Count, 6];
                for (int j = 0; j < DataGridRecord.SelectedItems.Count; j++)
                {
                    tlsTrTask = (tlsTrTask)DataGridRecord.SelectedItems[j];
                    Global.NTCheckMassage[j, 0] = tlsTrTask.ItemName;
                    Global.NTCheckMassage[j, 1] = tlsTrTask.SampleName;
                    Global.NTCheckMassage[j, 2] = tlsTrTask.SampleID;
                    Global.NTCheckMassage[j, 3] = tlsTrTask.tasktime;
                }

                for (int j = 0; j < DataGridRecord.SelectedItems.Count; j++)
                {
                    tlsTrTask = (tlsTrTask)DataGridRecord.SelectedItems[j];
                    //Global.NTCheckMassage[j, 0] = tlsTrTask.ItemName;
                    //Global.NTCheckMassage[j, 1] = tlsTrTask.SampleName;
                    //Global.NTCheckMassage[j, 2] = tlsTrTask.SampleID;
                    //Global.NTCheckMassage[j, 3] = tlsTrTask.tasktime;

                    for (int i = 0; i < Global.fgdItems.Count; i++)
                    {
                        //对比项目名称，一致的话就进行检测
                        DYFGDItemPara FGDitem = Global.fgdItems[i];
                        if (tlsTrTask.ItemName.Equals(FGDitem.Name))
                        {
                            //进入分光光度项目选择界面
                            FgdWindow window = new FgdWindow();
                            window.ItemName = tlsTrTask.ItemName;
                            window.samplename = tlsTrTask.SampleName;
                            //Global._selSampleID = tlsTrTask.SampleID;
                            //Global._selSamplename = tlsTrTask.SampleName;
                            //Global._itemname = tlsTrTask.ItemName;
                            //Global._tasktime = tlsTrTask.tasktime;
                            window.ShowInTaskbar = false;
                            window.Owner = this;
                            window.ShowDialog();

                            return;
                        }

                    }
                    // //和胶体金模块的检测项目对比
                    for (int i = 0; i < Global.jtjItems.Count; i++)
                    {
                        DYJTJItemPara JTJitem = Global.jtjItems[i];
                        if (tlsTrTask.ItemName.Equals(JTJitem.Name))
                        {
                            //进入胶体金项目选择界面
                            JtjWindow window = new JtjWindow();
                            window.ItemName = tlsTrTask.ItemName;
                            window.samplename = tlsTrTask.SampleName;
                            //Global._selSampleID = tlsTrTask.SampleID;
                            //Global._selSamplename = tlsTrTask.SampleName;
                            //Global._itemname = tlsTrTask.ItemName;
                            //Global._tasktime = tlsTrTask.tasktime;

                            window.ShowInTaskbar = false;
                            window.Owner = this;
                            window.ShowDialog();
                            //Global.NT.IsStartInterface = true;
                            return;
                        }
                    }
                    //和干化学模块的检测项目对比
                    for (int i = 0; i < Global.gszItems.Count; i++)
                    {
                        DYGSZItemPara GSZitem = Global.gszItems[i];
                        if (tlsTrTask.ItemName.Equals(GSZitem.Name))
                        {
                           
                            //进入胶体金项目选择界面
                            GszWindow window = new GszWindow();
                            window.ItemName = tlsTrTask.ItemName;
                            window.samplename = tlsTrTask.SampleName;
                            //Global._selSampleID = tlsTrTask.SampleID;
                            //Global._selSamplename = tlsTrTask.SampleName;
                            //Global._itemname = tlsTrTask.ItemName;
                            //Global._tasktime = tlsTrTask.tasktime;

                            window.ShowInTaskbar = false;
                            window.Owner = this;
                            window.ShowDialog();
                            //Global.NT.IsStartInterface = true;
                            return;
                        }
                    }
                }
                MessageBox.Show("请到项目选择界面新建检测项目", "提示");
            }
        }
    }
}
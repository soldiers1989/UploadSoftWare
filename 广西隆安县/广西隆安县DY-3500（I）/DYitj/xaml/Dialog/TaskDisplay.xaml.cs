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
using AIO.src;
using DYSeriesDataSet.DataSentence.Kjc;
using System.Windows.Media;

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

        public TaskDisplay()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //this.DataGridRecord.LoadingRow += new EventHandler<DataGridRowEventArgs>(this.DataGridRecord_LoadingRow);
            //btnDeleted.Visibility = Global.IsDELETED ? Visibility.Visible : Visibility.Collapsed;
            //SearchTask();
            //uploadTask();
            SearchTask();
        }

        private void SearchTask()
        {
            this.DataGridRecord.ItemsSource = null;
            //DataTable dt = _Tskbll.GetAsDataTable(string.Empty, string.Empty, 1);
            DataTable dt = _Tskbll.GetAsDataTable(string.Empty, string.Empty, 4);
            if (dt != null && dt.Rows.Count > 0)
            {
                //List<tlsTrTask> Items = (List<tlsTrTask>)IListDataSet.DataTableToIList<tlsTrTask>(dt, 1);
                List<downsample> Items = (List<downsample>)IListDataSet.DataTableToIList<downsample>(dt, 1);
                this.DataGridRecord.ItemsSource = Items;
                if (DataGridRecord.Items.Count > 0)
                {
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, new Action(ProcessRows));
                   
                }
            }
            else
                this.DataGridRecord.ItemsSource = null;
        }
        private void ProcessRows()
        {

            List<downsample> Items = new List<downsample>();
            //获取单元行
            try
            {
                for (int i = 0; i < DataGridRecord.Items.Count; i++)
                {
                    Items.Add((downsample)DataGridRecord.Items[i]);
                    var row = DataGridRecord.ItemContainerGenerator.ContainerFromItem(DataGridRecord.Items[i]) as DataGridRow;
                    if (row != null)
                    {
                        if (Items[i].isTest.Equals("是"))
                            row.Background = new SolidColorBrush(Colors.Red);
                     
                        ////获取单元格
                        //DataRowView drv = DataGridRecord.Items[i] as DataRowView;
                        //if (Items[i].Result != null && Items[i].Result.Equals("不合格"))
                        //{
                        //    DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(row);
                        //    DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(9);
                        //    cell.Background = new SolidColorBrush(Colors.Red);
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
            //tlsTrTask tlsTrTask;
            //try
            //{
            //    if (DataGridRecord.SelectedItems.Count > 0)
            //    {
            //        tlsTrTask = (tlsTrTask)DataGridRecord.SelectedItems[0];
            //        TaskDetailedWindow window = new TaskDetailedWindow();
            //        window.GetValues(tlsTrTask);
            //        window.ShowInTaskbar = false;
            //        window.Owner = this;
            //        window.ShowDialog();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
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
                if (DatePickerDateFrom.SelectedDate==null )
                {
                    MessageBox.Show("请选择起始时间！", "操作提示");
                    return ;
                }
                if (DatePickerDateTo.SelectedDate == null)
                {
                    MessageBox.Show("请选择结束时间！", "操作提示");
                    return;
                }

                this.LabelInfo.Content = "信息:正在更新任务";
                this.btnTaskUpdate.Content = "正在更新···";
                this.btnTaskUpdate.IsEnabled = false;
                _isShowBox = true;
                this.btnTaskUpdate.FontSize = 16;

                _msgThread = new MsgThread(this);
                _msgThread.Start();
                DateTime from = DatePickerDateFrom.SelectedDate != null ? (DateTime)DatePickerDateFrom.SelectedDate : new DateTime();
                DateTime to = DatePickerDateTo.SelectedDate != null ? (DateTime)DatePickerDateTo.SelectedDate : new DateTime();
                Message msg = new Message()
                {
                    what = MsgCode.MSG_DownTask,
                    str1 = Global.samplenameadapter[0].url,
                    str2 = Global.samplenameadapter[0].user,
                    str3 = Global.samplenameadapter[0].pwd,
                    str4 = Global.samplenameadapter[0].serial,
                    str5 = Global.samplenameadapter[0].mechineType,
                    str6 = from.ToString("yyyy-MM-dd"),
                    str7 = to.ToString ("yyyy-MM-dd"),
                    str8 = txtSampleNo.Text.Trim(),
                };
                //msg.args.Enqueue(Global.samplenameadapter[0].pointNum);
                //msg.args.Enqueue(Global.samplenameadapter[0].pointName);
                //msg.args.Enqueue(Global.samplenameadapter[0].pointType);
                //msg.args.Enqueue(Global.samplenameadapter[0].orgName);
                Global.workThread.SendMessage(msg, _msgThread);
            }
            catch (Exception ex)
            {
                MessageBox.Show("出现异常!\n" + ex.Message);
            }
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
        private void DownloadTask(string TaskTemp)
        {
            string err = string.Empty;
            StringBuilder sb = new StringBuilder();
            int len = 0;
            _Tskbll.Delete(string.Empty, out err);
            try
            {
                if (Global.InterfaceType.Equals("KJC"))//快检车任务
                {
                    ResultMsg msgResult = JsonHelper.JsonToEntity<ResultMsg>(TaskTemp);
                    if (msgResult.resultCode.Equals("success1"))
                    {
                        List<kjcCheckPlan> models = JsonHelper.JsonToEntity<List<kjcCheckPlan>>(msgResult.result.ToString());
                        if (models != null && models.Count > 0)
                        {
                            len = models.Count;
                            clsTask model = new clsTask();
                            for (int i = 0; i < len; i++)
                            {
                                model.CPCODE = models[i].cpdCode;
                                model.CPTITLE = models[i].cpTitle;
                                model.CPSDATE = models[i].cpsDate;
                                model.CPEDATE = models[i].cpeDate;
                                model.CPTPROPERTY = models[i].cptProperty;
                                model.CPFROM = models[i].cpFrom;
                                model.CPEDITOR = models[i].cpEditor;
                                model.CPPORGID = models[i].cpPorgId;
                                model.CPPORG = models[i].cpPorg;
                                model.CPEDDATE = models[i].cpedDate;
                                model.CPMEMO = models[i].memo;
                                model.PLANDETAIL = string.Format("检测项目：{0}；样品种类：{1}", models[i].cpdciTem, models[i].cpdfName);
                                model.PLANDCOUNT = models[i].cpdpCcount.ToString();
                                model.BAOJINGTIME = models[i].alarmTime;
                                model.UDate = models[i].udate;
                                _Tskbll.InsertOrUpdate(model, out err);
                                if (!err.Equals(string.Empty))
                                    sb.Append(err);
                            }
                        }
                    }
                }
                else
                {
                    //达元平台任务
                    string delErr = string.Empty;
                    DataSet dataSet = new DataSet();
                    DataTable dtbl = new DataTable();
                    using (StringReader sr = new StringReader(TaskTemp))
                        dataSet.ReadXml(sr);
                    if (!TaskTemp.Equals("<NewDataSet>\r\n</NewDataSet>"))
                    {
                        if (dataSet != null)
                        {
                            len = dataSet.Tables[0].Rows.Count;
                            dtbl = dataSet.Tables[0];
                        }
                        
                        sb.Append(delErr);
                        if (len == 0)
                        {
                            MessageBox.Show("暂时没有任务需要更新！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
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
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("下载任务时出现异常！\r\n" + ex.Message, "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                btnTaskUpdate.FontSize = 24;
                btnTaskUpdate.Content = "任务更新";
                btnTaskUpdate.IsEnabled = true;
                SearchTask();
            }
        }

        private void downTask(string data)
        {
            string err="";
            int icount = 0;
            downsample sd = new downsample();
            List<SampleInfo> infoList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SampleInfo>>(data);
            if (infoList != null && infoList.Count > 0)
            {
                for (int i = 0; i < infoList.Count; i++)
                {
                    sd.cdIdNum = infoList[i].cdIdNum;
                    sd.cdName = infoList[i].cdName;
                    sd.regName = infoList[i].regName;
                    sd.regBusLicence = infoList[i].regBusLicence;
                    sd.regContactPerson = infoList[i].regContactPerson;

                    List<detailsample> Dsample = Newtonsoft.Json.JsonConvert.DeserializeObject<List<detailsample>>(infoList[i].detailModels.ToString());
                    if (Dsample != null && Dsample.Count > 0)
                    {
                        sd.foodName = Dsample[0].foodName;
                        sd.foodType = Dsample[0].foodType;
                        sd.nominalSourcePlace = Dsample[0].nominalSourcePlace;
                        sd.productionPlace = Dsample[0].productionPlace;
                        sd.sampleNO = Dsample[0].sampleNO;
                        sd.supplier = Dsample[0].supplier;
                        sd.supplierContact = Dsample[0].supplierContact;
                        sd.supplierPhone = Dsample[0].supplierPhone;
                        sd.takeInDate = Dsample[0].takeInDate;
                        sd.takeInNumber = Dsample[0].takeInNumber;

                        List<checkItemList> Ditem = Newtonsoft.Json.JsonConvert.DeserializeObject<List<checkItemList>>(Dsample[0].checkItemsModels.ToString());
                        if (Ditem != null && Ditem.Count > 0)
                        {
                            sd.checkAccord = Ditem[0].checkAccord;
                            sd.checkItem = Ditem[0].checkItem;
                        }
                    }
                    //插入数据库
                    _Tskbll.InsertTask(sd, out err);
                    icount = icount + 1;
                }
            }

            LabelInfo.Content = "信息:任务下载成功！";
            MessageBox.Show("任务下载成功！\r\n共成功下载" + icount + "条数据！");
            SearchTask();
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
                        if(msg.result ==true )
                        {
                            if(msg.responseInfo !="")
                            {
                                wnd.downTask(msg.responseInfo);
                            }
                            else
                            {
                                wnd.LabelInfo.Content = "信息:任务更新完成！";
                                MessageBox.Show("任务更新失败！\r\n失败原因：" + msg.errMsg, "操作提示", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                        else
                        {
                            wnd.LabelInfo.Content = "信息:任务更新失败";
                            MessageBox.Show("任务更新失败！\r\n失败原因："+msg.errMsg, "操作提示", MessageBoxButton.OK, MessageBoxImage.Information);
                            
                        }
                        wnd.btnTaskUpdate.Content = "任务更新";
                        wnd.btnTaskUpdate.IsEnabled = true;
                        wnd.btnTaskUpdate.FontSize = 24;

                        //if (!string.IsNullOrEmpty(msg.DownLoadTask) && wnd._isShowBox)
                        //{
                        //    try
                        //    {
                        //        wnd.DownloadTask(msg.DownLoadTask);
                        //    }
                        //    catch (Exception e)
                        //    {
                        //        _checkedDown = false;
                        //        wnd.btnTaskUpdate.FontSize = 24;
                        //        wnd.btnTaskUpdate.Content = "任务更新";
                        //        wnd.btnTaskUpdate.IsEnabled = true;
                        //        wnd.LabelInfo.Content = "信息:任务更新失败";
                        //        MessageBox.Show("任务更新失败!请联系管理员!\n" + e.Message, "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        //    }
                        //    finally
                        //    {
                        //        if (_checkedDown)
                        //        {
                        //            wnd._isShowBox = false;
                        //            wnd.LabelInfo.Content = "信息:任务更新完成";
                        //            MessageBox.Show("任务更新完成!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        //        }
                        //    }
                        //}
                        //else if (wnd._isShowBox)
                        //{
                        //    wnd._isShowBox = _checkedDown = false;
                        //    wnd.LabelInfo.Content = "信息:任务更新失败";
                        //    wnd.btnTaskUpdate.FontSize = 24;
                        //    wnd.btnTaskUpdate.Content = "任务更新";
                        //    wnd.btnTaskUpdate.IsEnabled = true;
                        //    MessageBox.Show("任务更新失败,或者服务链接不正常，请联系管理员!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        //    //Console.WriteLine("任务更新失败,或者服务链接不正常，请联系管理员!");
                        //}
                        break;
                    default:
                        break;
                }
            }
        }

        private void btnDeleted_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataGridRecord.SelectedItems.Count == 0)
                {
                    MessageBox.Show("请选择需要删除的记录！", "操作提示", MessageBoxButton.OK, MessageBoxImage.Question);
                    return;
                }
                string err = "";
                for (int i = 0; i < DataGridRecord.SelectedItems.Count; i++)
                {
                    downsample items = (downsample)DataGridRecord.SelectedItems[i];
                    _Tskbll.DeleteTask("ID="+items.ID , "", out err);
                }
                SearchTask();
                MessageBox.Show("删除成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           

        }

    }
}
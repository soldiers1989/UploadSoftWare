using com.lvrenyang;
using DYSeriesDataSet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using DYSeriesDataSet.DataSentence;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// AddSample.xaml 的交互逻辑
    /// </summary>
    public partial class ManagementSample : Window
    {
        public ManagementSample()
        {
            InitializeComponent();
        }
        private MsgThread _msgThread;
        private bool _IsFirst = true;
        private clsTaskOpr _clsTaskOpr = new clsTaskOpr();
        private clsttStandardDecideOpr _clsttStandardDecideOpr = new clsttStandardDecideOpr();
        private List<Ssamplestd> _ItemNames = new List<Ssamplestd>();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //btnDeleted.Visibility = btnCleanRepeatSample.Visibility = Global.IsDELETED.Equals("Y") ? Visibility.Visible : Visibility.Collapsed;
            SearchSample();
        }

        /// <summary>
        /// 查询样品
        /// </summary>
        private void SearchSample()
        {
            this.DataGridRecord.DataContext = null;
            DataTable dataTable = _clsTaskOpr.GetSampleByNameOrCode(textBoxSampleName.Text.Trim(), textBoxName.Text.Trim(), false, _IsFirst, 1);
            if (dataTable != null)
            {
                List<clsttStandardDecide> ItemNames = (List<clsttStandardDecide>)IListDataSet.DataTableToIList<clsttStandardDecide>(dataTable, 1);
                if (ItemNames != null && ItemNames.Count > 0)
                {
                    this.DataGridRecord.DataContext = ItemNames;
                    _IsFirst = false;
                    if (_ItemNames.Count <= 0)
                    { }
                        //_ItemNames = ItemNames;
                }
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void SaveSample()
        {
            if (DataGridRecord.Items.Count > 0)
            {
                bool check = true;
                int result = 1;
                string err = string.Empty;
                clsttStandardDecide model = new clsttStandardDecide();
                try
                {
                    foreach (clsttStandardDecide item in DataGridRecord.Items.SourceCollection)
                    {
                        check = checkValue(item);
                        if (!check)
                            break;
                    }
                    if (check)
                    {
                        foreach (clsttStandardDecide item in DataGridRecord.Items.SourceCollection)
                        {
                            model.ID = item.ID;
                            model.FtypeNmae = item.FtypeNmae;
                            model.SampleNum = item.SampleNum;
                            model.Name = item.Name;
                            model.ItemDes = item.ItemDes;
                            model.StandardValue = item.StandardValue;
                            model.Demarcate = item.Demarcate;
                            model.Unit = item.Unit;
                            _clsttStandardDecideOpr.Insert(model, out err);
                        }
                    }
                    else
                    {
                        MessageBox.Show("信息不完整，请填写完整的国家检测标准信息!", "操作提示");
                    }
                }
                catch (Exception exception)
                {
                    result = 0;
                    MessageBox.Show("操作出错!" + exception.ToString());
                }
                finally
                {
                    if (result == 1)
                        MessageBox.Show("保存成功!", "操作提示");
                }
            }
        }

        /// <summary>
        /// 检测完整性
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private bool checkValue(clsttStandardDecide model)
        {
            if (model.FtypeNmae == null)
                return false;
            else if (model.FtypeNmae.Trim().Equals(string.Empty))
                return false;

            if (model.SampleNum == null)
                return false;
            else if (model.SampleNum.Trim().Equals(string.Empty))
                return false;

            if (model.Name == null)
                return false;
            else if (model.Name.Trim().Equals(string.Empty))
                return false;

            if (model.ItemDes == null)
                return false;
            else if (model.ItemDes.Trim().Equals(string.Empty))
                return false;

            if (model.StandardValue == null)
                return false;
            else if (model.StandardValue.Trim().Equals(string.Empty))
                return false;

            if (model.Demarcate == null)
                return false;
            else if (model.Demarcate.Trim().Equals(string.Empty))
                return false;

            if (model.Unit == null)
                return false;
            else if (model.Unit.Trim().Equals(string.Empty))
                return false;

            return true;
        }

        /// <summary>
        /// 删除数据||删除表格列
        /// </summary>
        private void Deleted()
        {
            if (DataGridRecord.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("确定要删除所选的国家检测标准吗?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    clsttStandardDecide record;
                    int result = 0;
                    string err = string.Empty;
                    try
                    {
                        for (int i = 0; i < DataGridRecord.SelectedItems.Count; i++)
                        {
                            if (DataGridRecord.SelectedItems[i].ToString().Equals("{NewItemPlaceholder}"))
                            {
                                break;
                            }
                            record = (clsttStandardDecide)DataGridRecord.SelectedItems[i];
                            if (record.ID > 0)
                            {
                                if (_clsttStandardDecideOpr.DeleteByID(record.ID, out err) == 1)
                                    result += 1;
                            }
                        }
                        if (result > 0)
                        {
                            MessageBox.Show("成功删除 " + result + " 条国家检测标准!", "操作提示");
                        }
                    }
                    catch (Exception Exception)
                    {
                        MessageBox.Show("删除失败!出现异常\n" + Exception.ToString());
                    }
                    finally
                    {
                        SearchSample();
                    }
                }
            }
            else
            {
                MessageBox.Show("未选择任何国家检测标准!", "操作提示");
            }
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Deleted();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        /// <summary>
        /// 修改样品
        /// </summary>
        private void Update()
        {
            try
            {
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    if (!DataGridRecord.SelectedItems[0].ToString().Equals("{NewItemPlaceholder}"))
                    {
                        clsttStandardDecide _decide = (clsttStandardDecide)DataGridRecord.SelectedItems[0];
                        AddOrUpdate("update", _decide);
                    }
                }
                else
                {
                    MessageBox.Show("未选择任何国家检测标准!", "操作提示");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:\n" + ex.Message);
            }
        }

        /// <summary>
        /// 删除选中行的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miDelete_Click(object sender, RoutedEventArgs e)
        {
            Deleted();
        }

        /// <summary>
        /// 修改选中行的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miUpload_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddOrUpdate("add", null);
        }

        /// <summary>
        /// 新增||修改样品
        /// </summary>
        /// <param name="type">add为新增||update为修改</param>
        private void AddOrUpdate(string type, clsttStandardDecide model)
        {
            try
            {
                AddOrUpdateSample addWindow = new AddOrUpdateSample();
                if (type.Equals("add"))
                {
                    addWindow.Title = "新增国家检测标准";
                }
                else
                {
                    addWindow.Title = "修改国家检测标准";
                    addWindow.btnSave.Content = "修改";
                    addWindow._decide = model;
                }
                //addWindow._ItemNames = _ItemNames;
                addWindow.ShowDialog();
                SearchSample();
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:\n" + ex.Message);
            }
        }

        private void DataGridRecord_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Update();
            try
            {
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    if (!DataGridRecord.SelectedItems[0].ToString().Equals("{NewItemPlaceholder}"))
                    {
                        clsttStandardDecide _decide = (clsttStandardDecide)DataGridRecord.SelectedItems[0];
                        AddOrUpdate("update", _decide);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:\n" + ex.Message);
            }
        }

        private void textBoxSampleName_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchSample();
        }

        private void textBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchSample();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchSample();
        }

        private void DataGridRecord_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void textBoxName_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Global.IsProject = true;
            SearchSample searchSample = new SearchSample();
            try
            {
                searchSample.ShowDialog();
                if (!Global.projectName.Equals(string.Empty))
                {
                    this.textBoxName.Text = Global.projectName;
                    SearchSample();
                }
                Global.projectName = string.Empty;//还原项目名称的值
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:\n" + ex.Message);
            }
        }

        private void btnCleanRepeatSample_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable dataTable = _clsTaskOpr.GetSampleByNameOrCode(string.Empty, string.Empty, false, false, 1);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    List<clsttStandardDecide> ItemNames = (List<clsttStandardDecide>)IListDataSet.DataTableToIList<clsttStandardDecide>(dataTable, 1);
                    if (ItemNames != null && ItemNames.Count > 0)
                    {
                        int delNum = 0;
                        IDictionary<string, clsttStandardDecide> dicItems = new Dictionary<string, clsttStandardDecide>();
                        string err = String.Empty;
                        for (int i = 0; i < ItemNames.Count; i++)
                        {
                            //key=样品名称+项目名称+标准名称     //+标准值+判定符号+单位
                            string key = ItemNames[i].FtypeNmae + "_" + ItemNames[i].Name + "_" + ItemNames[i].ItemDes;
                            //+ "_" + ItemNames[i].StandardValue + "_" + ItemNames[i].Demarcate + "_" + ItemNames[i].Unit;
                            if (!dicItems.ContainsKey(key))
                            {
                                dicItems.Add(key, ItemNames[i]);
                            }
                            else
                            {
                                if (_clsttStandardDecideOpr.DeleteByID(ItemNames[i].ID, out err) == 1)
                                {
                                    delNum += 1;
                                }
                            }
                        }
                        if (delNum > 0)
                        {
                            MessageBox.Show("成功清理 " + delNum + " 条重复国家检测标准!");
                        }
                        else
                        {
                            MessageBox.Show("暂无重复国家检测标准!");
                        }
                        SearchSample();
                    }
                }
                else
                {
                    MessageBox.Show("暂无国家检测标准需要清理!", "操作提示");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常：\n" + ex.Message);
            }
        }

        private void btnDeleted_Click(object sender, RoutedEventArgs e)
        {
            string sErr = string.Empty, str = string.Empty;
            if (MessageBox.Show("确定要清空所有国家检测标准吗?\n注意：一旦清空将不可恢复，请慎重选择。", "操作提示",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _clsttStandardDecideOpr.Delete(string.Empty, out sErr);
                str = sErr.Equals(string.Empty) ? "已成功清理所有国家检测标准!" : ("清理国家检测标准时出错!\n异常：" + sErr);
                SearchSample();
                MessageBox.Show(str, "操作提示");
            }
        }
        /// <summary>
        /// 检测标准下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownStand_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _msgThread = new MsgThread(this);
                _msgThread.Start();

                Message msg = new Message()
                {
                    what = MsgCode.MSG_STANDARD,
                    str1 = Global.samplenameadapter[0].url,
                    str2 = Global.samplenameadapter[0].user,
                    str3 = Global.samplenameadapter[0].pwd
                };

                Global.workThread.SendMessage(msg, _msgThread);
            }
            catch (Exception ex)
            {
                MessageBox.Show("出现异常!\n" + ex.Message);
            }
        }

        class MsgThread : ChildThread
        {
            ManagementSample  wnd;
            bool _checkedDown = true;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;

            public MsgThread(ManagementSample wnd)
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
                    case MsgCode.MSG_STANDARD:
                        if (msg.result == true)
                        {
                            MessageBox.Show("共成功下载"+" "+"检测项目");
                        }
                        break;
                    //case MsgCode.MSG_DownTask:
                        //if (msg.responseInfo != "0" && wnd._isShowBox)
                        //{
                        //    wnd._isShowBox = false;
                        //    wnd.LabelInfo.Content = "信息:任务更新完成";
                        //    wnd.btnTaskUpdate.Content = "任务更新";
                        //    wnd.btnTaskUpdate.FontSize = 24;
                        //    MessageBox.Show("任务更新完成,共下载" + msg.responseInfo + "条数据！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        //    wnd.btnTaskUpdate.IsEnabled = true;
                        //}
                        //wnd.btnTaskUpdate.IsEnabled = true;
                        //wnd.btnTaskUpdate.Content = "任务更新";
                        //wnd.LabelInfo.Content = "信息:任务更新完成";
                        //wnd.btnTaskUpdate.FontSize = 24;
                        //wnd.SearchTask();
                        //MessageBox.Show("任务更新完成,共下载" + msg.responseInfo + "条数据！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                        break;
                    default:
                        break;
                }
            }
        }

        private void btnreturn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AIO.xaml.Main;
using com.lvrenyang;

namespace AIO.xaml.KjService
{
    /// <summary>
    /// CheckTasks.xaml 的交互逻辑
    /// </summary>
    public partial class CheckTasks : Window
    {
        /// <summary>
        /// 仪器检测任务
        /// </summary>
        public CheckTasks()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 信息验证是否成功
        /// </summary>
        private bool ValidationSuccessful = false;
        private string errMsg = string.Empty;
        private List<DyInterfaceHelper.KjService.DownloadSamplingEntity.ResultItem> _selectModels = null;

        private MsgThread _msgThread;
        class MsgThread : ChildThread
        {
            CheckTasks wnd;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;

            public MsgThread(CheckTasks wnd)
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            protected void UIHandleMessage(Message msg)
            {
                switch (msg.what)
                {
                    case MsgCode.MSG_DOWNLOADSAMPLING:
                        wnd.ShowSamplelingTasks();
                        wnd.RefreshInfo(false);
                        if (msg.errMsg.Length > 0)
                        {
                            wnd.LabelInfo.Content = msg.errMsg;
                        }
                        break;

                    //更新任务接收状态，如果接收，则写入本地数据库，拒收则删除显示不写入数据库
                    case MsgCode.MSG_UPDATESTATUS:
                        wnd.ShowSamplelingTasks();
                        if (msg.errMsg.Length > 0)
                        {
                            MessageBox.Show(wnd, msg.errMsg, "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        break;

                    //通讯测试
                    case MsgCode.MSG_CHECK_CONNECTION:
                        if (msg.result)
                        {
                            wnd.LabelInfo.Content = "用户信息验证成功！";
                            //用户信息验证成功后，验证仪器注册信息
                            wnd.KjDeviceLogin();
                        }
                        else
                        {
                            wnd.ValidationSuccessful = false;
                            wnd.LabelInfo.Content = "用户信息验证失败！";
                        }
                        break;

                    //仪器注册
                    case MsgCode.MSG_REGISTERDEVICE:
                        if (msg.result)
                        {
                            wnd.ValidationSuccessful = true;
                            wnd.LabelInfo.Content = "仪器注册信息验证成功！";
                            wnd.Btn_Refresh_Click(null, null);
                        }
                        else
                        {
                            wnd.ValidationSuccessful = false;
                            wnd.LabelInfo.Content = "仪器注册信息验证失败！";
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _msgThread = new MsgThread(this);
            _msgThread.Start();
            DataGridRecord.Height = SystemParameters.WorkArea.Height - 270;
            //首先进行通讯测试，测试成功后进行仪器注册以检测基础信息的完整性
            KjServerTest();
        }

        /// <summary>
        /// 通讯测试
        /// </summary>
        private void KjServerTest()
        {
            LabelInfo.Content = "正在验证用户信息···";
            Message msg = new Message()
            {
                what = MsgCode.MSG_CHECK_CONNECTION,
                str1 = Global.KjServer.Kjuser_name,
                str2 = Global.KjServer.Kjpassword
            };
            Global.workThread.SendMessage(msg, _msgThread);
        }

        /// <summary>
        /// 仪器注册
        /// </summary>
        private void KjDeviceLogin()
        {
            LabelInfo.Content = "正在验证仪器注册信息···";
            Message msg = new Message()
            {
                what = MsgCode.MSG_REGISTERDEVICE,
                str1 = Global.KjServer.Kjseries,
                str2 = Global.Mac
            };
            Global.workThread.SendMessage(msg, _msgThread);
        }

        /// <summary>
        /// 主界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_GoBack_Click(object sender, RoutedEventArgs e)
        {
            Global.KjServer._selectCheckTasks = null;
            MainWindow window = new MainWindow();
            window.ShowInTaskbar = false;
            window.Owner = this;
            window.ShowDialog();
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            if (sender != null && e != null)
            {
                if (!ValidationSuccessful)
                {
                    if (MessageBox.Show("基础信息验证失败，是否进入设置界面验证？", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        IntoSettingWindow();
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else
            {
                LabelInfo.Content = "信息验证失败，请进入设置界面进行验证！";
            }
            try
            {
                Message msg = new Message()
                {
                    what = MsgCode.MSG_DOWNLOADSAMPLING
                };
                Global.workThread.SendMessage(msg, _msgThread);
                RefreshInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("出现异常!\n" + ex.Message);
            }
        }

        private void RefreshInfo(bool request = true)
        {
            if (request)
            {
                LabelInfo.Content = "正在获取数据···";
                Btn_Refresh.Content = "正在加载";
                Btn_Refresh.IsEnabled = false;
            }
            else
            {
                Btn_Refresh.Content = "刷  新";
                Btn_Refresh.IsEnabled = true;
            }
        }

        private void DataGridRecord_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridRecord.SelectedItems.Count > 0)
            {
                bool isReceive = true, isCheck = false;
                _selectModels = new List<DyInterfaceHelper.KjService.DownloadSamplingEntity.ResultItem>();
                for (int i = 0; i < DataGridRecord.SelectedItems.Count; i++)
                {
                    _selectModels.Add((DyInterfaceHelper.KjService.DownloadSamplingEntity.ResultItem)DataGridRecord.SelectedItems[i]);
                    if (!_selectModels[i].isReceive)//未接收
                    {
                        isReceive = false;
                    }
                    else
                    {
                        isCheck = true;
                    }
                }

                //如果选中的任务集合中有未接收的数据，则启用接收和拒收
                miReceive.IsEnabled = Btn_Receive.IsEnabled = !isReceive;
                miRefuse.IsEnabled = Btn_Refuse.IsEnabled = !isReceive;
                //如果选中的任务集合中都没有接收任务则不启用开始检测按钮
                miStartCheck.IsEnabled = Btn_StartCheck.IsEnabled = isCheck;
            }
            else
            {
                _selectModels = null;
            }
        }

        /// <summary>
        /// 查看详情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Show_Click(object sender, RoutedEventArgs e)
        {
            ShowDetails();
        }

        /// <summary>
        /// 双击查看详情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridRecord_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowDetails();
        }

        private void ShowDetails()
        {
            if (_selectModels == null)
            {
                MessageBox.Show("未选择任何数据！");
                return;
            }

            Global.KjServer._selectCheckTasks = _selectModels;
            ShowCheckTasks window = new ShowCheckTasks();
            window.ShowInTaskbar = false;
            window.Owner = this;
            window.ShowDialog();
        }

        private void DataGridRecord_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void ShowSamplelingTasks()
        {
            LabelInfo.Content = "更新成功！";
            DataGridRecord.DataContext = Global.KjServer.samplingEntity.result;
        }

        /// <summary>
        /// 接收
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Receive_Click(object sender, RoutedEventArgs e)
        {
            UpdateStatus();
        }

        /// <summary>
        /// 拒收
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Refuse_Click(object sender, RoutedEventArgs e)
        {
            UpdateStatus(false);
        }

        /// <summary>
        /// 接收 | 拒收任务
        /// </summary>
        /// <param name="isReceive">true 接收，false 拒收</param>
        private void UpdateStatus(bool isReceive = true)
        {
            try
            {
                if (_selectModels == null)
                {
                    MessageBox.Show("未选择任何数据！");
                    return;
                }

                if (MessageBox.Show("确定要" + (isReceive ? "[接收]" : "[拒收]") + "当前选择的任务吗?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    int continueCount = 0;
                    string sdId = string.Empty, recevieStatus = isReceive ? "1" : "2";
                    for (int i = 0; i < _selectModels.Count; i++)
                    {
                        //需要略过已经接收的任务
                        if (_selectModels[i].isReceive)
                        {
                            continueCount++;
                            continue;
                        }
                        sdId += (sdId.Length > 0 ? "," : "") + _selectModels[i].id;
                    }

                    //如果选择的任务中都已经接收了，则不进行下一步
                    if (sdId.Length == 0)
                    {
                        MessageBox.Show("所选数据全部已经被接收!\r\n无法再次进行[接收]或[拒收]操作！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    //如果选择的任务中有已经接收的，则弹出提示，让用户选择略过继续操作还是取消操作
                    else if (continueCount > 0 &&
                        MessageBox.Show("所选数据中有已经被接收的任务！\r\n点击[是]略过已接收任务，点击[否]取消当前操作！",
                        "系统提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                    {
                        return;
                    }

                    LabelInfo.Content = "正在更新" + (isReceive ? "[接收]" : "[拒收]") + "状态···";
                    Message msg = new Message()
                    {
                        what = MsgCode.MSG_UPDATESTATUS,
                        str1 = sdId,
                        str2 = Global.DeviceID,
                        str3 = recevieStatus,
                        str4 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    };
                    Global.workThread.SendMessage(msg, _msgThread);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("出现异常!\n" + ex.Message);
            }
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("确定要退出系统吗?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                CFGUtils.SaveConfig("FgIsStart", "0");
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// 开始检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_StartCheck_Click(object sender, RoutedEventArgs e)
        {
            if (_selectModels == null)
            {
                MessageBox.Show("未选择任何数据！");
                return;
            }

            Global.KjServer._selectCheckTasks = new List<DyInterfaceHelper.KjService.DownloadSamplingEntity.ResultItem>();
            //以选择的第一个项目为准
            Global.KjServer._selectCheckTasks.Add(_selectModels[0]);
            for (int i = 1; i < _selectModels.Count; i++)
            {
                //相同的项目ADD
                if (_selectModels[i].item_name.Equals(Global.KjServer._selectCheckTasks[0].item_name))
                {
                    Global.KjServer._selectCheckTasks.Add(_selectModels[i]);
                }
            }

            //如果还没有指定检测模块，则轮询一个出来
            if (_selectModels[0].resultType == null || _selectModels[0].resultType.Length == 0)
            {
                _selectModels[0].resultType = Global.KjServer.GetResultType(_selectModels[0].item_name, 0);
                if (_selectModels[0].resultType.Length == 0)
                {
                    MessageBox.Show("暂时没有找到匹配的检测模块，请确认基础数据已更新至最新！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }

            if (_selectModels[0].resultType.Equals("分光光度"))
            {
                for (int i = 0; i < Global.fgdItems.Count; i++)
                {
                    //对比项目名称，一致的话就进行检测
                    DYFGDItemPara FGDitem = Global.fgdItems[i];
                    if (_selectModels[0].item_name.Equals(FGDitem.Name))
                    {
                        bool isdz = false;
                        int holeIdx = 0;
                        for (int j = 0; j < _selectModels.Count; j++)
                        {
                            //所选样品超过通道数时，剩下部分忽略
                            if (j >= FGDitem.Hole.Length) break;

                            //如果需要对照，第一通道需要显示对照样
                            if (!isdz && ((0 == FGDitem.Method && Double.MinValue == FGDitem.ir.RefDeltaA) || (1 == FGDitem.Method && Double.MinValue == FGDitem.sc.RefA)))
                            {
                                FGDitem.Hole[j].SampleName = "对照样";
                                FGDitem.Hole[j].CompanyName = string.Empty;
                                FGDitem.Hole[j].TaskName = string.Empty;
                                FGDitem.Hole[j].TaskCode = string.Empty;
                                isdz = true;
                                holeIdx = j + 1;
                            }

                            FGDitem.Hole[holeIdx].SampleName = _selectModels[j].food_name;
                            FGDitem.Hole[holeIdx].CompanyName = _selectModels[j].s_reg_name;
                            FGDitem.Hole[holeIdx].TaskName = _selectModels[j].t_task_title;
                            FGDitem.Hole[holeIdx].TaskCode = _selectModels[j].id;
                            holeIdx++;
                        }
                        //进入分光光度项目选择界面
                        FgdSelChannelWindow window = new FgdSelChannelWindow();
                        window._item = FGDitem;
                        window.ShowInTaskbar = false;
                        window.Owner = this;
                        window.ShowDialog();
                        return;
                    }
                }
            }
            else if (_selectModels[0].resultType.Equals("胶体金"))
            {

            }
            else if (_selectModels[0].resultType.Equals("干化学"))
            {

            }

            //Global.KjServer._selectModels = _selectModels;

            ////先从分光光度模块中对比检测项目，如果没有继续和胶体金和干化学模块对比
            //for (int i = 0; i < Global.fgdItems.Count; i++)
            //{
            //    //对比项目名称，一致的话就进行检测
            //    DYFGDItemPara FGDitem = Global.fgdItems[i];
            //    if (_selectModel.item_name.Equals(FGDitem.Name))
            //    {
            //        for (int j = 0; j < FGDitem.Hole.Length; j++)
            //        {
            //            FGDitem.Hole[j].SampleName = _selectModel.food_name;
            //            FGDitem.Hole[j].CompanyName = _selectModel.s_reg_name;
            //            FGDitem.Hole[j].TaskName = _selectModel.t_task_title;
            //        }
            //        //进入分光光度项目选择界面
            //        FgdSelChannelWindow window = new FgdSelChannelWindow();
            //        window._item = FGDitem;
            //        window.ShowInTaskbar = false;
            //        window.Owner = this;
            //        window.ShowDialog();
            //        return;
            //    }
            //}

            ////和胶体金模块的检测项目对比
            //for (int i = 0; i < Global.jtjItems.Count; i++)
            //{
            //    DYJTJItemPara JTJitem = Global.jtjItems[i];
            //    if (_selectModel.item_name.Equals(JTJitem.Name))
            //    {
            //        //进入胶体金项目选择界面
            //        JtjWindow window = new JtjWindow();
            //        window.ShowInTaskbar = false;
            //        window.Owner = this;
            //        window.ShowDialog();
            //        return;
            //    }
            //}

            ////和干化学模块的检测项目对比
            //for (int i = 0; i < Global.gszItems.Count; i++)
            //{
            //    DYGSZItemPara GSZitem = Global.gszItems[i];
            //    if (_selectModel.item_name.Equals(GSZitem.Name))
            //    {
            //        //进入胶体金项目选择界面
            //        GszWindow window = new GszWindow();
            //        window.ShowInTaskbar = false;
            //        window.Owner = this;
            //        window.ShowDialog();
            //        return;
            //    }
            //}

        }

        /// <summary>
        /// 数据管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_DataMsg_Click(object sender, RoutedEventArgs e)
        {
            DataManagementWindow window = new DataManagementWindow();
            window.ShowInTaskbar = false;
            window.Owner = this;
            window.ShowDialog();
        }

        /// <summary>
        /// 功能选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmb_Method_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Cmb_Method.SelectedIndex == 0)
            {
                return;
            }
            //手动检测
            else if (Cmb_Method.SelectedIndex == 1)
            {
                Global.KjServer._selectCheckTasks = null;
                MainWindow window = new MainWindow();
                window.ShowInTaskbar = false;
                window.Owner = this;
                window.ShowDialog();
            }
            //数据管理
            else if (Cmb_Method.SelectedIndex == 2)
            {
                DataManagementWindow window = new DataManagementWindow();
                window.ShowInTaskbar = false;
                window.Owner = this;
                window.ShowDialog();
            }
            //设置
            else if (Cmb_Method.SelectedIndex == 3)
            {
                IntoSettingWindow();
            }
            Cmb_Method.SelectedIndex = 0;
        }

        private void IntoSettingWindow()
        {
            SettingsWindow window = new SettingsWindow();
            window.ShowInTaskbar = false;
            window.Owner = this;
            window.ShowDialog();
            KjServerTest();
        }

    }
}
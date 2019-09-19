using AIO.xaml.Dialog;
using com.lvrenyang;
using DYSeriesDataSet;
using DYSeriesDataSet.DataModel;
using AIO.src;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Collections;
using System.Windows.Threading;
using System.Windows.Media;
using DY.Process;

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
        private DispatcherTimer _GetTaskTimer = null;
        private DispatcherTimer _showlabel = null;
        private StringBuilder sb = new StringBuilder();
        public UserAccount _userconfig = null;
        private string err = "";
        private int gotimes = 0;

        public TaskDisplay()
        {
            InitializeComponent();   
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Global.IsConnectInternet())
                {
                    MessageBox.Show(this, "设备网络异常，请检查网络！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                    SettingsWindow sw = new SettingsWindow();
                    sw.Noregister = true;
                    sw.ShowDialog();
                }

                LabelInfo.Visibility = Visibility.Collapsed;
                LabelInfo.Content = "信息:共成功下载0条数据！ " + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                SearchItem();//查询检测项目

                //btnDeleted.Visibility = Global.IsDELETED ? Visibility.Visible : Visibility.Collapsed;

                ServerLogon();
                Resigiter();

                Global.sysupdatebtn = true;
                if (_GetTaskTimer == null)
                {
                    _GetTaskTimer = new DispatcherTimer();
                    _GetTaskTimer.Interval = TimeSpan.FromSeconds(10);
                    _GetTaskTimer.Tick += _GetTaskTimer_Tick;
                    _GetTaskTimer.Start();
                }
                SearchTask();
                Style styleRight = new Style(typeof(TextBlock));
                Setter setRight = new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center); styleRight.Setters.Add(setRight);
                foreach (DataGridColumn c in DataGridRecord.Columns)
                {
                    DataGridTextColumn tc = c as DataGridTextColumn;
                    if (tc != null)
                    {
                        tc.ElementStyle = styleRight;
                    }
                }
                //显示label定时器
                _showlabel = new DispatcherTimer();
                _showlabel.Interval = TimeSpan.FromSeconds(10);
                _showlabel.Tick += new EventHandler(_showlabel_Tick);
                _showlabel.Start();

                functionbtn();//功能按钮
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        /// <summary>
        /// 数据更新
        /// </summary>
        private void UpdateBaseData()
        {
            PercentProcess process = new PercentProcess()
            {
                BackgroundWork = downdata,
                MessageInfo = "正在更新数据"
            };
            process.Start();
        }
        /// <summary>
        /// 下载数据
        /// </summary>
        /// <param name="percent"></param>
        private void downdata(Action<int> percent)
        {

        }
        /// <summary>
        /// 功能按钮权限设置
        /// </summary>
        private void functionbtn()
        {
            //检测按钮
            if (Global.Detectionbtn == true)
            {
                ButtonSel.Visibility  =Visibility.Visible ;
            }
            else 
            {
                ButtonSel.Visibility = Visibility.Collapsed;
            }
            //数据中心
            if (Global.DataCenterbtn == true)
            {
                btnDataCentre.Visibility = Visibility.Visible;
            }
            else
            {
                btnDataCentre.Visibility = Visibility.Collapsed;
            }
            //任务接收
            if (Global.Receivetask == true)
            {
                //btnReceive.Visibility = Visibility.Visible;
            }
            else
            {
                btnReceive.Visibility = Visibility.Collapsed ;
            }
            //任务拒收
            if (Global.Refusetask == true)
            {
                btnoject.Visibility = Visibility.Visible;
            }
            else
            {
                btnoject.Visibility = Visibility.Collapsed ;
            }
            //手动测试
            if (Global.ShoudongTest == true)
            {
                BtnManutTest.Visibility = Visibility.Visible;
            }
            else
            {
                BtnManutTest.Visibility = Visibility.Collapsed;
            }
            //任务管理
            if (Global.ManageTask == true)
            {
                btnTaskManage.Visibility = Visibility.Visible;
            }
            else
            {
                btnTaskManage.Visibility = Visibility.Collapsed ;
            }

        }
        private  void _showlabel_Tick(object sender, EventArgs e)
        {
            LabelInfo.Visibility = Visibility.Collapsed;
            
        }
        /// <summary>
        /// 仪器登录
        /// </summary>
        private void ServerLogon()
        {
            if (Global.samplenameadapter[0].url.Length == 0)
            {
                MessageBox.Show("检测到未设置服务器登录地址，请到系统设置中设置","操作提示");
                SettingsWindow sw = new SettingsWindow();
                sw.IsServerNull = true;
                sw.ShowDialog();
                return;
            }
            if (Global.samplenameadapter[0].user.Length == 0)
            {
                MessageBox.Show("检测到未设置服务器登录用户名，请到系统设置中设置", "操作提示");
                SettingsWindow sw = new SettingsWindow();
                sw.IsServerNull = true;
                sw.ShowDialog();
                return;
            }
            if (Global.samplenameadapter[0].pwd.Length == 0)
            {
                MessageBox.Show("检测到未设置服务器登录密码，请到系统设置中设置", "操作提示");
                SettingsWindow sw = new SettingsWindow();
                sw.IsServerNull = true;
                sw.ShowDialog();
                return;
            }
            try
            {
                ResultData Jresult = null;
                objdata obj = null;
                string rtn = InterfaceHelper.QuickTestServerLogin(Global.samplenameadapter[0].url, Global.samplenameadapter[0].user, Global.samplenameadapter[0].pwd, 1);
                FileUtils.KLog(rtn,"接收",1);
                if (rtn.Contains("msg") || rtn.Contains("success") || rtn.Contains("resultCode") || rtn.Contains("obj"))
                {
                    Jresult = JsonHelper.JsonToEntity<ResultData>(rtn);
                    obj = JsonHelper.JsonToEntity<objdata>(Jresult.obj.ToString());
                    if (Jresult.msg == "操作成功" && Jresult.success == true)
                    {
                        Global.Token = obj.token;
                        sysRights(obj.rights);//权限解析
                        userdata ud = JsonHelper.JsonToEntity<userdata>(obj.user.ToString());
                        Global.d_depart_name = ud.d_depart_name;
                        Global.depart_id = ud.depart_id;
                        Global.p_point_name = ud.p_point_name;
                        Global.point_id = ud.point_id;
                        Global.user_name = ud.user_name;
                        Global.id = ud.id;
                        Global.realname = ud.realname;
                        Global.pointNum = ud.p_point_code;
                        Global.pointName = ud.p_point_name;
                        Global.pointType = ud.p_point_type;
                        Global.orgName = ud.d_depart_name;
                        Global.orgID = ud.d_id;
                    }
                    else
                    {
                        MessageBox.Show("用户登录失败，请查看系统设置，失败原因：" + Jresult.msg, "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                        SettingsWindow sw = new SettingsWindow();
                        sw.Noregister = true;
                        sw.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("用户登录失败，请查看系统设置" , "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    SettingsWindow sw = new SettingsWindow();
                    sw.Noregister = true;
                    sw.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 仪器注册
        /// </summary>
        private void Resigiter()
        {
            try
            {
                string Rpassword = Global.MD5(Global.MD5(Global.samplenameadapter[0].pwd));
                string RAddr = InterfaceHelper.GetServiceURL(Global.samplenameadapter[0].url, 7);//地址
                sb.Length = 0;
                sb.Append(RAddr);
                sb.AppendFormat("?userToken={0}", Global.Token);
                sb.AppendFormat("&series={0}", Global.MachineModel);
                sb.AppendFormat("&mac={0}", Global.GetMACComputer());
                sb.AppendFormat("&param1={0}", "");
                sb.AppendFormat("&param2={0}", "");
                sb.AppendFormat("&param3={0}", "");
                FileUtils.KLog(sb.ToString(), "发送", 2);
                string Rlist = InterfaceHelper.HttpsPost(sb.ToString());
                FileUtils.KLog(Rlist, "接收", 2);

                if (Rlist.Contains("msg") || Rlist.Contains("success") || Rlist.Contains("resultCode") || Rlist.Contains("obj"))
                {
                    ResultData Zresult = JsonHelper.JsonToEntity<ResultData>(Rlist);
                    if (Zresult.success == true)
                    {
                        zhuce zdata = JsonHelper.JsonToEntity<zhuce>(Zresult.obj.ToString());
                        Global.MachineNum = zdata.serial_number;
                        CFGUtils.SaveConfig("IntrumentSeriersNum", Global.MachineNum);//保存系列号到本地文件
                        Global.isresige = true;
                    }
                    else
                    {
                        MessageBox.Show("仪器注册失败，请联系管理员");
                        Global.isresige = false;
                        //btnTaskUpdate.IsEnabled = false;
                        SettingsWindow sw = new SettingsWindow();
                        sw.Noregister = true;
                        sw.ShowDialog();
                       
                    }
                }
                else
                {
                    MessageBox.Show("仪器注册失败，失败原因：" + Rlist,"系统提示",MessageBoxButton.OK,MessageBoxImage.Error  );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
        /// <summary>
        /// 系统权限
        /// </summary>
        /// <param name="right"></param>
        private void sysRights(string right)
        {
            string[] Aright = right.Split(',');
            for (int j = 0; j < Aright.Length; j++)
            {
                if (Aright[j] == "1301-1")//检测
                {
                    Global.Detectionbtn = true;
                }
                else if (Aright[j] == "1301-2")//重检
                {
                    Global.redetectionbtn = true;
                }
                else if (Aright[j] == "1303")//数据中心
                {
                    Global.DataCenterbtn = true;
                }
                else if (Aright[j] == "1304")//拒绝任务
                {
                    Global.Refusetask = true;
                }
                else if (Aright[j] == "1305")//接收任务
                {
                    Global.Receivetask = true;
                }
                else if (Aright[j] == "1306")//设置
                {
                    Global.Setting = true;
                }
                else if (Aright[j] == "1303-1")//检测项目
                {
                    Global.testitembtn = true;
                }
                else if (Aright[j] == "1303-2")//检测标准
                {
                    Global.teststandbtn = true;
                }
                else if (Aright[j] == "1303-3")//食品种类
                {
                    Global.foodtypebtn = true;
                }
                else if (Aright[j] == "1303-4")//仪器检测项目
                {
                    Global.machineitembtn = true;
                }
                else if (Aright[j] == "1303-5")//样品检测标准
                {
                    Global.samplestdbtn = true;
                }
                else if (Aright[j] == "1303-6")//法律法规
                {
                    Global.lawsbtn = true;
                }
                else if (Aright[j] == "1303-7")//检测数据、记录
                {
                    Global.testdatabtn = true;
                }
                else if (Aright[j] == "1306-1")//系统升级
                {
                    Global.sysupdatebtn = true;
                }
                else if (Aright[j] == "1303-8")//被检单位
                {
                    Global.checkcompanuy = true;
                }
                else if (Aright[j] == "1328")//手动检测
                {
                    Global.ShoudongTest = true;
                }
                else if (Aright[j] == "1303-9")//打印
                {
                    Global.print = true;
                }
                else if (Aright[j] == "1303-10")//上传
                {
                    Global.Uploadd = true;
                }
                else if (Aright[j] == "1303-11")//编辑
                {
                    Global.edition = true;
                }
                else if (Aright[j] == "1303-12")//导入
                {
                    Global.Input = true;
                }
                else if (Aright[j] == "1303-13")//导出
                {
                    Global.output = true;
                }
                else if (Aright[j] == "1303-14")//生成报告
                {
                    Global.GenerateReport = true;
                }
                else if (Aright[j] == "1303-15")//打印报告
                {
                    Global.PrintReport = true;
                }
                else if (Aright[j] == "1328-1")//视频教程
                {
                    Global.vedioTV = true;
                }
                else if (Aright[j] == "1328-2")//操作说明
                {
                    Global.Instruction = true;
                }
                else if (Aright[j] == "1303-16")//被检单位重置
                {
                    Global.ResetCompany = true;
                }
                else if (Aright[j] == "1303-17")//检测项目重置
                {
                    Global.ResetItem = true;
                }
                else if (Aright[j] == "1303-18")//检测标准重置
                {
                    Global.ResetStandard = true;
                }
                else if (Aright[j] == "1303-19")//食品种类重置
                {
                    Global.ResetSampleType = true;
                }
                else if (Aright[j] == "1303-110")//仪器检测项目重置
                {
                    Global.ResetMachineItem = true;
                }
                else if (Aright[j] == "1303-111")//样品检测标准重置
                {
                    Global.ResetSampleStd = true;
                }
                else if (Aright[j] == "1303-112")//法律法规重置
                {
                    Global.ResetLaws = true;
                }
                else if (Aright[j] == "1377")//任务管理
                {
                    Global.ManageTask = true;
                }
                
            }
        }
        /// <summary>
        /// 定时接收检测任务数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  void _GetTaskTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                gotimes = gotimes + 1;
                if(gotimes ==300)//50分钟重新登录一次
                {
                    ServerLogon();
                    gotimes = 0;
                }
                Global.TasknumTime = CFGUtils.GetConfig("tasknumtime", "");
                //抽样任务
                string rtn = InterfaceHelper.QuickTestServerLogin(Global.samplenameadapter[0].url, Global.samplenameadapter[0].user, Global.samplenameadapter[0].pwd, 2);
                FileUtils.KLog(rtn, "接收", 3);
                if (rtn.Contains("msg") || rtn.Contains("success") || rtn.Contains("resultCode") || rtn.Contains("obj"))
                {
                    ResultData Jresult = JsonHelper.JsonToEntity<ResultData>(rtn);

                    if (Jresult.msg == "操作成功" && Jresult.success == true)
                    {
                        tasknum obj = JsonHelper.JsonToEntity<tasknum>(Jresult.obj.ToString());
                        labelMessage.Content = obj.count;
                        if (obj.count != "0")
                        {
                            labelMessage.Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {
                        MessageBox.Show("仪器任务数量接收失败，失败原因：" + Jresult.msg);
                    }
                }
                else
                {
                    MessageBox.Show("仪器任务数量接收失败，失败原因：" + rtn);
                }

                //检测任务
                //rtn = InterfaceHelper.QuickTestServerLogin(Global.samplenameadapter[0].url, Global.samplenameadapter[0].user, Global.samplenameadapter[0].pwd, 3);
                //FileUtils.KLog(rtn, "接收", 17);
                //if (rtn.Contains("msg") || rtn.Contains("success") || rtn.Contains("resultCode") || rtn.Contains("obj"))
                //{
                //    ResultData Jresult = JsonHelper.JsonToEntity<ResultData>(rtn);
                //    if (Jresult.msg == "操作成功" && Jresult.success == true)
                //    {
                //        ManageTask obj = JsonHelper.JsonToEntity<ManageTask>(Jresult.obj.ToString());
                //        if (obj != null && obj.tasks.Count > 0)
                //        {
                //            labelTaskManage.Content = obj.tasks[0].tasksNumber;
                //            ImgTaskInfo.Visibility = Visibility.Visible;
                //            labelTaskManage.Visibility = Visibility.Visible;
                //        }
                //    }
                //    else
                //    {
                //        MessageBox.Show("任务管理数量接收失败，失败原因：" + Jresult.msg);
                //    }

                //}
                //else
                //{
                //    MessageBox.Show("任务管理数量接收失败，失败原因：" + rtn);
                //}

                //消息公告

                //rtn = InterfaceHelper.QuickTestServerLogin(Global.samplenameadapter[0].url, Global.samplenameadapter[0].user, Global.samplenameadapter[0].pwd, 4);
                //FileUtils.KLog(rtn, "接收", 20);
                //if (rtn.Contains("msg") || rtn.Contains("success") || rtn.Contains("resultCode") || rtn.Contains("obj"))
                //{
                //    ResultData Jresult = JsonHelper.JsonToEntity<ResultData>(rtn);
                //    if (Jresult.msg == "操作成功" && Jresult.success == true)
                //    {
                //        ManagegonggaoNum objg = JsonHelper.JsonToEntity<ManagegonggaoNum>(Jresult.obj.ToString());
                //        if (objg != null && objg.unread!="0")
                //        {
                //            labelgonggao.Content = objg.unread;
                //            ImggonggaoInfo.Visibility = Visibility.Visible;
                //            labelgonggao.Visibility = Visibility.Visible;
                //        }
                //    }
                //    else
                //    {
                //        MessageBox.Show("任务管理数量接收失败，失败原因：" + Jresult.msg);
                //    }

                //}
                //else
                //{
                //    MessageBox.Show("任务管理数量接收失败，失败原因：" + rtn);
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        /// <summary>
        /// 查询检测项目
        /// </summary>
        private void SearchItem()
        {
            DataTable dt = null;
            //分光度
            dt = _Tskbll.GetCheckItem("project_type='分光光度'", "", 0);
            if (dt != null && dt.Rows.Count > 0)
            {
                Global.FGItem= (List<deviceItem>)IListDataSet.DataTableToIList<deviceItem>(dt, 1);
            }
            //胶体金
            dt = _Tskbll.GetCheckItem("project_type='胶体金'", "", 0);
            if (dt != null && dt.Rows.Count > 0)
            {
                Global.JTItem = (List<KJFWJTItem>)IListDataSet.DataTableToIList<KJFWJTItem>(dt, 1);
            }
            //干化学
            dt = _Tskbll.GetCheckItem("project_type='干化学'", "", 0);
            if (dt != null && dt.Rows.Count > 0)
            {
                Global.GHItem = (List<KJFWGHXItem>)IListDataSet.DataTableToIList<KJFWGHXItem>(dt, 1);
            }
            //if (Global.FGItem.Count > 0)
            //{
            //    string sd = Global.FGItem[0].project_type;
            //}
        }

        private void SearchTask()
        {
            DataGridRecord.ItemsSource = null;
            string stime = DateTime.Now.ToString("yyyy-MM-dd");
            sb.Length = 0;
            sb.AppendFormat("UserName='{0}' and CheckType is null or (UserName='{1}' and CheckType='已检测' and s_sampling_date like '%{2}%')",Global.samplenameadapter[0].user,Global.samplenameadapter[0].user, stime);
            DataTable dt = _Tskbll.GetQtask(sb.ToString(), " CheckType,s_sampling_date ", 1);
            if (dt != null && dt.Rows.Count > 0)
            {
                List<tlsTrTask> Items = (List<tlsTrTask>)IListDataSet.DataTableToIList<tlsTrTask>(dt, 1);
                if (Items.Count > 0)
                    this.DataGridRecord.ItemsSource = Items;//(Items != null && Items.Count > 0) ? Items : null;这写法有问题
            }
        }
        private void DeleteTask(int type, string TaskID)
        {
            
            try
            {
                sb.Length = 0;
                if (type == 1)
                {
                  
                    sb.AppendFormat("tid='{0}'", TaskID);
                }
                else if (type == 2)
                {
                    sb.Append("IsReceive='拒收'");
                }

               
                _Tskbll.DeleteTast(sb.ToString(), out err);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
 
        }
        /// <summary>
        /// 修改控件测试行的颜色
        /// </summary>
        private void datagridItem()
        {
            List<tlsTrTask> Items = new List<tlsTrTask>();
            //获取单元行
            try
            {
                for (int i = 0; i < DataGridRecord.Items.Count; i++)
                {
                    Items.Add((tlsTrTask)DataGridRecord.Items[i]);
                    var row = DataGridRecord.ItemContainerGenerator.ContainerFromItem(DataGridRecord.Items[i]) as DataGridRow;
                    if (row != null)
                    {
                        if (Items[i].Checktype.Equals("已检测"))
                        {
                            row.Background = new SolidColorBrush(Colors.YellowGreen);
                        }
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
           
            if (MessageBox.Show("确定要退出系统吗?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Environment.Exit(0);
            }
        }

        private void DataGridRecord_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;

            DataGridRow dataGridRow = e.Row;
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

        /// <summary>
        /// 双击检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridRecord_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (Global.isresige == true)
            {
                selecttask();
            }
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
        /// 更新任务
        /// </summary>
        private void uploadTask()
        {
            this.LabelInfo.Content = "信息:正在更新任务";
            this.btnTaskUpdate.Content = "正在更新···";
            this.btnTaskUpdate.IsEnabled = false;
            _isShowBox = true;
            this.btnTaskUpdate.FontSize = 16;
            try
            {
                _msgThread = new MsgThread(this);
                _msgThread.Start();
                Message msg = new Message()
                {
                    what = MsgCode.MSG_DownTask,
                    str1 = Global.samplenameadapter[0].url,
                    str2 = Global.samplenameadapter[0].user,
                    str3 = Global.samplenameadapter[0].pwd
                };
                msg.args.Enqueue(Global.samplenameadapter[0].pointNum);
                msg.args.Enqueue(Global.samplenameadapter[0].pointName);
                msg.args.Enqueue(Global.samplenameadapter[0].pointType);
                msg.args.Enqueue(Global.samplenameadapter[0].orgName);
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
                    case MsgCode.MSG_RECEIVETASK:
                        if (msg.result == true)
                        {
                            MessageBox.Show("任务接收成功");
                            wnd.SearchTask();
                        }
                        else
                        {
                            MessageBox.Show("任务接收失败，失败原因："+msg.responseInfo );
                        }
                        break;
                    case MsgCode.MSG_DownTask:
                        wnd.btnTaskUpdate.IsEnabled = true;
                 
                        wnd.btnTaskUpdate.FontSize = 24;
                        wnd.SearchTask();
                        
                        wnd.LabelInfo.Visibility = Visibility.Visible;
                        wnd.LabelInfo.Content = "下载" + msg.responseInfo + "条数据！ ";
                        break;
                    case MsgCode.MSG_OBJECTASK:
                        wnd.btnoject.IsEnabled = true;
                        if (msg.result == true)
                        {
                            wnd.DeleteTask(2, Global.samplingnumRecive);
                            MessageBox.Show("任务拒收成功");
                            wnd.SearchTask();
                        }
                        else
                        {
                            MessageBox.Show("任务拒收失败，失败原因："+msg.responseInfo);
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
                                MessageBox.Show("请在平台上设置重检权限","提示");
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
                            Mokuailist.Add(tlsTrTask.mokuai );
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
                    Itemlist.Clear ();
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
                    Global.Testsample = new string[DataGridRecord.SelectedItems.Count,2];
                    for (int j = 0; j < DataGridRecord.SelectedItems.Count; j++)
                    {
                        tlsTrTask = (tlsTrTask)DataGridRecord.SelectedItems[j];
                        Global.Testsample[j,0]=tlsTrTask.food_name;
                        Global.TestCompany[j] = tlsTrTask.s_reg_name;
                        sb.Length = 0;
                        sb.AppendFormat(" mokuai='{0}'", tlsTrTask.mokuai);
                        sb.AppendFormat(" and item_name='{0}'", tlsTrTask.item_name);
                        sb.AppendFormat(" and food_name='{0}'", tlsTrTask.food_name);
                        sb.AppendFormat(" and s_reg_name='{0}'", tlsTrTask.s_reg_name);
                        sb.AppendFormat(" and t_task_title='{0}'", tlsTrTask.t_task_title);
                        sb.AppendFormat(" and tid='{0}'", tlsTrTask.tid);
                        sb.AppendFormat(" and s_sampling_date='{0}'", tlsTrTask.s_sampling_date);

                        DataTable dt= _Tskbll.GetQtask(sb.ToString(), "", 3);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            Global.Testsample[j, 1] = dt.Rows[0]["ID"].ToString();
                        }
                    }
                    if (Mokuailist[0].ToString() == "胶体金")
                    {
                        if (Global.Testsample.GetLength(0) > 4)
                        {
                            MessageBox.Show("胶体金检测只有4通道，不能选择样品大于4个，请重选","提示");
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
                            MessageBox.Show("请选择相同模块和相同检测项目下的检测任务","操作提示");
                            return;
                        }

                        bool isCheckName = false;
                        if (tlsTrTask.mokuai == "分光光度")
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


                            //先从分光光度模块中对比检测项目，如果没有继续和胶体金和干化学模块对比
                            //for (int i = 0; i < Global.fgdItems.Count; i++)
                            //{
                            //    //对比项目名称，一致的话就进行检测
                            //    DYFGDItemPara FGDitem = Global.fgdItems[i];
                            //    if (tlsTrTask.item_name.Equals(FGDitem.Name))
                            //    {
                            //        feguan = true;
                            //        isCheckName = true;
                            //        //进入分光光度项目选择界面
                            //        Global.videoType = "fgd";
                            //        FgdWindow window = new FgdWindow();
                            //        window._itemname = tlsTrTask.item_name;
                            //        window._sample = tlsTrTask.food_name;
                            //        window._Company = tlsTrTask.s_reg_name;
                            //        window._title = tlsTrTask.t_task_title;
                            //        Global.Kitem = tlsTrTask.item_name;
                            //        Global.Ksample = tlsTrTask.food_name;
                            //        Global.Kmarket = tlsTrTask.s_reg_name;
                            //        window.ShowInTaskbar = false;
                            //        window.Owner = this;
                            //        window.ShowDialog();
                            //        SearchTask();
                            //        //Global.NT.IsStartInterface = true;
                            //        return;
                            //    }
                            //}
                        }
                        else if (tlsTrTask.mokuai == "胶体金")
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
                            ////和胶体金模块的检测项目对比
                            //for (int i = 0; i < Global.jtjItems.Count; i++)
                            //{
                            //    DYJTJItemPara JTJitem = Global.jtjItems[i];
                            //    if (tlsTrTask.item_name.Equals(JTJitem.Name))
                            //    {
                            //        jiti = true;
                            //        isCheckName = true;
                            //        //进入胶体金项目选择界面
                            //        Global.videoType = "jtj";
                            //        JtjWindow window = new JtjWindow();
                            //        window._itemname = tlsTrTask.item_name;
                            //        window._sample = tlsTrTask.food_name;
                            //        window._Company = tlsTrTask.s_reg_name;
                            //        window._title = tlsTrTask.t_task_title;
                            //        Global.Kitem = tlsTrTask.item_name;
                            //        Global.Ksample = tlsTrTask.food_name;
                            //        Global.Kmarket = tlsTrTask.s_reg_name;
                            //        window.ShowInTaskbar = false;
                            //        window.Owner = this;
                            //        window.ShowDialog();
                            //        SearchTask();
                            //        //Global.NT.IsStartInterface = true;
                            //        return;
                            //    }
                            //}
                        }
                        else if (tlsTrTask.mokuai == "干化学")
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
                            ////和干化学模块的检测项目对比
                            //for (int i = 0; i < Global.gszItems.Count; i++)
                            //{
                            //    DYGSZItemPara GSZitem = Global.gszItems[i];
                            //    if (tlsTrTask.item_name.Equals(GSZitem.Name))
                            //    {
                            //        gahua = true;
                            //        isCheckName = true;
                            //        //进入胶体金项目选择界面
                            //        Global.videoType = "gsz";
                            //        GszWindow window = new GszWindow();
                            //        window._itemname = tlsTrTask.item_name;
                            //        window._sample = tlsTrTask.food_name;
                            //        window._Company = tlsTrTask.s_reg_name;
                            //        window._title = tlsTrTask.t_task_title;
                            //        Global.Kitem = tlsTrTask.item_name;
                            //        Global.Ksample = tlsTrTask.food_name;
                            //        Global.Kmarket = tlsTrTask.s_reg_name;
                            //        window.ShowInTaskbar = false;
                            //        window.Owner = this;
                            //        window.ShowDialog();
                            //        SearchTask();
                            //        //Global.NT.IsStartInterface = true;
                            //        return;
                            //    }
                            //}
                        }
                        //对比所有的检测项目名称都不一致时提示是否新建
                        //if (!isCheckName)
                        //{
                        //    MessageBox.Show("本地未找到检测项目【" + tlsTrTask.item + "】！是否立即创建？");
                        //    //itemIndex += 1;
                        //    //labelMsg.Content = "本地未找到检测项目【" + item.JCXMNAME + "】！请尽快联系供应商升级检测项目!";
                        //    ////MessageBox.Show(this, "本地未找到检测项目【" + item.JCXMNAME + "】！\r\n\r\n请尽快联系供应商升级检测项目!", "系统提示");
                        //    //return;

                        //    //Global.NT.IsStartInterface = false;
                        //    if (MessageBox.Show("本地未找到检测项目【" + tlsTrTask.item + "】！是否立即创建？",
                        //        "系统提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        //    {
                        //        FgdEditItemWindow window = new FgdEditItemWindow();
                        //        //window._ntProductName = tlsTrTask.item;
                        //        window.ShowInTaskbar = false;
                        //        window.Owner = this;
                        //        window.ShowDialog();
                        //        //Global.NT.IsStartInterface = true;
                        //    }
                        //    //else
                        //    //{
                        //    //    Global.NT.IsStartInterface = true;
                        //    //}
                        //}
                       
                        //TaskDetailedWindow window = new TaskDetailedWindow();
                        //window.GetValues(tlsTrTask);
                        //window.ShowInTaskbar = false;
                        //window.Owner = this;
                        //window.ShowDialog();
                    }  
                }
                else
                {
                    MessageBox.Show("请选择需要的检测项目","提示");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSel_Click(object sender, RoutedEventArgs e)
        {
            if (Global.isresige == true)
            {
                selecttask();
            }
        }
        /// <summary>
        /// 任务接受
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReceive_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Global.samplingnumRecive = "";
                tlsTrTask tlsTrTask;
                if (DataGridRecord.SelectedItems.Count <1)
                {
                    MessageBox.Show("请选择需要接收的任务");
                    return;
                }
                for (int i = 0; i < DataGridRecord.SelectedItems.Count; i++)
                {
                    tlsTrTask = (tlsTrTask)DataGridRecord.SelectedItems[i];
                    //StringBuilder sb = new StringBuilder();
                    sb.Length = 0;
                    sb.AppendFormat("sample_code='{0}' and ", tlsTrTask.sample_code);
                    sb.AppendFormat("food_name='{0}' and ", tlsTrTask.food_name);
                    sb.AppendFormat("s_reg_name='{0}' and ", tlsTrTask.s_reg_name);
                    sb.AppendFormat("s_ope_shop_code='{0}' and ", tlsTrTask.s_ope_shop_code);
                    sb.AppendFormat("s_sampling_date='{0}'", tlsTrTask.s_sampling_date);

                    DataTable dt = _Tskbll.GetQtask(sb.ToString(), "", 1);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (Global.samplingnumRecive == "")
                        {
                            Global.samplingnumRecive =Global.samplingnumRecive+ dt.Rows[0]["tid"].ToString();
                        }
                        else
                        {
                            Global.samplingnumRecive = Global.samplingnumRecive +","+ dt.Rows[0]["tid"].ToString();
                        }
                        
                    }
                }
              

                _msgThread = new MsgThread(this);
                _msgThread.Start();
                Message msg = new Message()
                {
                    what = MsgCode.MSG_RECEIVETASK,
                    str1 = Global.samplenameadapter[0].url,
                    str2 = Global.samplenameadapter[0].user,
                    str3 = Global.samplenameadapter[0].pwd
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
        /// <summary>
        /// 任务拒收
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnoject_Click(object sender, RoutedEventArgs e)
        {
            if (Global.isresige == true)
            {
                if (DataGridRecord.SelectedItems.Count < 1)
                {
                    MessageBox.Show("请选择需要拒收的任务");
                    return;
                }

                tlsTrTask tlsTrTask;
                if (DataGridRecord.SelectedItems.Count < 1)
                {
                    MessageBox.Show("请选择需要接收的任务");
                    return;
                }
                Global.ObjectSampling = "";
                for (int i = 0; i < DataGridRecord.SelectedItems.Count; i++)
                {
                   
                    tlsTrTask = (tlsTrTask)DataGridRecord.SelectedItems[i];

                    if (tlsTrTask.Checktype == "已检测")
                    {
                        continue;
                    }
                    if (Global.ObjectSampling == "")
                    {
                        Global.ObjectSampling = Global.ObjectSampling + tlsTrTask.tid;
                    }
                    else
                    {
                        Global.ObjectSampling = Global.ObjectSampling + "," + tlsTrTask.tid;
                    }

                    //StringBuilder sb = new StringBuilder();
                    //sb.Length = 0;
                    //sb.AppendFormat("sample_code='{0}' and ", tlsTrTask.sample_code);
                    //sb.AppendFormat("food_name='{0}' and ", tlsTrTask.food_name);
                    //sb.AppendFormat("s_reg_name='{0}' and ", tlsTrTask.s_reg_name);
                    //sb.AppendFormat("s_ope_shop_code='{0}' and ", tlsTrTask.s_ope_shop_code);
                    //sb.AppendFormat("s_sampling_date='{0}'", tlsTrTask.s_sampling_date);

                    //DataTable dt = _Tskbll.GetQtask(sb.ToString(), "", 1);
                    //if (dt != null && dt.Rows.Count > 0)
                    //{
                    //    if (Global.samplingnumRecive == "")
                    //    {
                    //        Global.samplingnumRecive = Global.samplingnumRecive + dt.Rows[0]["tid"].ToString();
                    //    }
                    //    else
                    //    {
                    //        Global.samplingnumRecive = Global.samplingnumRecive + "," + dt.Rows[0]["tid"].ToString();
                    //    }

                    //}
                }


                //tlsTrTask = (tlsTrTask)DataGridRecord.SelectedItems[0];
                ////StringBuilder sb = new StringBuilder();
                //sb.Length = 0;
                //sb.AppendFormat("tid='{0}' and ", tlsTrTask.tid);
                //sb.AppendFormat("sample_code='{0}' and ", tlsTrTask.sample_code);
                //sb.AppendFormat("food_name='{0}' and ", tlsTrTask.food_name);
                //sb.AppendFormat("s_reg_name='{0}' and ", tlsTrTask.s_reg_name);
                //sb.AppendFormat("s_ope_shop_code='{0}' and ", tlsTrTask.s_ope_shop_code);
                //sb.AppendFormat("s_sampling_date='{0}'", tlsTrTask.s_sampling_date);

                //DataTable dt = _Tskbll.GetQtask(sb.ToString(), "", 1);
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    Global.samplingnumRecive = dt.Rows[0]["tid"].ToString();
                //}
                try
                {
                    _msgThread = new MsgThread(this);
                    _msgThread.Start();
                    Message msg = new Message()
                    {
                        what = MsgCode.MSG_OBJECTASK,
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
        }

        private void Image_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MessageBox.Show("单击鼠标左键");
        }

        /// <summary>
        /// 数据中心
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDataCentre_Click(object sender, RoutedEventArgs e)
        {
            if (Global.isresige == true)
            {
                AIO.xaml.Main.DataManagementWindow window = new AIO.xaml.Main.DataManagementWindow()
                {
                    ShowInTaskbar = false,
                    Owner = this
                };
                window.Show();
            }
        }
        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSet_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow window = new SettingsWindow()
            {
                ShowInTaskbar = false,
                Owner = this
            };
            window.ShowDialog();
        }
        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeform_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (MessageBox.Show("确定要退出系统吗?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Environment.Exit(0);
            }
        }
        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sysSet_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //if (Global.isresige == true)
            //{
            //    if (Global.Setting == true)
            //    {
                    SettingsWindow window = new SettingsWindow()
                    {
                        ShowInTaskbar = false,
                        Owner = this
                    };
                    window.ShowDialog();
            //    }
            //    else
            //    {
            //        MessageBox.Show("请在平台设置设置权限", "操作提示");
            //    }
            //}
        }
        /// <summary>
        /// 任务更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rmessage_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                if (Global.isresige == true)
                {
                    //任务接受徐
                    if (Global.Receivetask == true)
                    {
                        labelMessage.Visibility = Visibility.Collapsed;
                        uploadTask();
                    }
                    else
                    {
                        MessageBox.Show("请在平台设置任务接收权限", "操作提示");
                    }
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
        /// <summary>
        /// 手动测试入口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnManutTest_Click(object sender, RoutedEventArgs e)
        {
            if (Global.isresige == true)
            {
                if (Global.ShoudongTest == true)
                {
                    Global.ManuTest = true;
                    MainWindow window = new MainWindow()
                    {
                        _userconfig = _userconfig,
                        ShowInTaskbar = false,
                        Owner = this,
                    };
                    window.ShowDialog();
                }
                else
                {
                    MessageBox.Show("请在平台设置访问权限", "提示");
                }
            }
        }

        private void DataGridRecord_LoadingRow_1(object sender, DataGridRowEventArgs e)
        {

        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Repair_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridRecord.SelectedItems.Count < 1)
            {
                MessageBox.Show("请选择需要修改检测模块的数据");
                return;
            }
            try
            {
                string err = "";
                tlsTrTask tlsTrTask;
                tlsTrTask = (tlsTrTask)DataGridRecord.SelectedItems[0];
                RepairModel window = new RepairModel();
                window.model = tlsTrTask.mokuai;
                window.item = tlsTrTask.item_name;
                window.SampleNum = tlsTrTask.sample_code;
                window.SampleTime = tlsTrTask.getsampledate;
                window.ShowDialog();
                if (Global.modelkuai != "")
                {
                    _Tskbll.UpdateTask(tlsTrTask.tid, Global.modelkuai, out err);
                    SearchTask();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void rmessage_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }
        /// <summary>
        /// 任务管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTaskManage_Click(object sender, RoutedEventArgs e)
        {
            TaskManageWindow window = new TaskManageWindow();
            window.ShowDialog();
            ImgTaskInfo.Visibility = Visibility.Collapsed;
            labelTaskManage.Visibility = Visibility.Collapsed;
        }
        /// <summary>
        /// 消息公告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMessageBulletin_Click(object sender, RoutedEventArgs e)
        {
            MessageBulletinWindow window = new MessageBulletinWindow();
            window.ShowDialog();
            labelgonggao.Visibility = Visibility.Collapsed;
            ImggonggaoInfo.Visibility = Visibility.Collapsed;
        }

    }
}
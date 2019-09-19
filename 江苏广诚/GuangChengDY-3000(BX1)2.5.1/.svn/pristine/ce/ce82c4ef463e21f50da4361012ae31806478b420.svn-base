using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using AIO.src;
using AIO.xaml.Dialog;
using AIO.xaml.KjService;
using com.lvrenyang;
using DYSeriesDataSet;

namespace AIO
{
    /// <summary>
    /// SelChannelWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FgdSelChannelWindow : Window
    {
        public DYFGDItemPara _item = null;
        private Brush _borderBrushSelected = new SolidColorBrush(Color.FromRgb(224, 67, 67));
        private Brush _borderBrushNormal = new SolidColorBrush(Color.FromRgb(0x00, 0x7C, 0xC2));
        private int _SelIndex = -1;
        private int _checkNum = 0;
        private clsTaskOpr _clsTaskOpr = new clsTaskOpr();
        private DataTable _Tc = new DataTable();
        private string logType = "FgdSelChannelWindow-error";

        public FgdSelChannelWindow()
        {
            InitializeComponent();
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (null == _item)
            {
                MessageBox.Show("项目异常");
                this.Close();
            }
            try
            {
                //进入检测界面前停止电池状态获取
                CFGUtils.SaveConfig("FgIsStart", "1");
                System.Console.WriteLine(CFGUtils.GetConfig("FgIsStart", "0"));

                labelTitle.Content = _item.Name + "  检测通道选择";
                UpdateHoleWaveIdx();
                ShowAllChannel();
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            try
            {
                for (i = 0; i < Global.deviceHole.HoleCount; ++i)
                {
                    if (_item.Hole[i].Use) break;
                }
                if (Global.deviceHole.HoleCount == i)
                {
                    MessageBox.Show("请至少选择一个检测孔", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }
                //进入检测界面前停止电池状态获取
                CFGUtils.SaveConfig("FgIsStart", "1");
                //System.Console.WriteLine(CFGUtils.GetConfig("FgIsStart", "0"));

                List<TextBox> listSampleName = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "SampleName");
                List<TextBox> listDiShuOrBeiShu = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "DiShuOrBeiShu");
                List<TextBox> listTask = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "TaskName");
                List<TextBox> listCompany = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Company");
                List<TextBox> listProduceCompany = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "ProduceCompany");
                List<TextBox> listSampleid = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Sampleid");
                List<TextBox> listproject = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "project");
                List<TextBox> liststall = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "stall");
                List<TextBox> listSampleType = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "SampleType");

                bool IsContrast = false;
                //bool IsProject = false;
                //bool IsCompany = false;
                //bool ISsampleType = false;
                //bool IsStall = false;
                //string warmingUser = "";

                for (i = 0; i < Global.deviceHole.HoleCount; ++i)
                {
                    if (_item.Hole[i].Use)
                    {
                        //如果检测项目需要对照且还没有对照时
                        if (!IsContrast)
                        {
                            if ((0 == _item.Method && Double.MinValue == _item.ir.RefDeltaA) || (1 == _item.Method && Double.MinValue == _item.sc.RefA))
                                listSampleName[i].Text = "对照样";
                            else
                                IsContrast = true;
                        }

                        if (IsContrast)
                        {
                            if (listSampleName[i].Text.Trim().Length == 0)
                            {
                                MessageBox.Show(this, "样品名称不能为空!\n\n可手工输入或双击选择样品", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                listSampleName[i].Focus();
                                return;
                            }

                            if (Global.InterfaceType.Equals("ZH"))// && LoginWindow._userAccount.CheckSampleID)
                            {
                                if (listSampleid != null && listSampleid[i].Text.Trim().Length == 0)
                                {
                                    MessageBox.Show(this, "快检单号不能为空!\n\n可手工输入或双击选择被检单位", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                    listSampleid[i].Focus();
                                    return;
                                }
                            }
                        }

                        if (listSampleType[i].Text.Trim().Length == 0)
                        {
                            //if (!ISsampleType)
                            //{
                            //    warmingUser = warmingUser.Length==0 ? warmingUser = "样品种类" : warmingUser + "、样品种类";
                            //    ISsampleType = true;
                            if (MessageBox.Show("样品种类为空将不能上传数据，是否需继续？\r\n\r\n可手工输入或双击选择样品种类", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) != MessageBoxResult.Yes)
                            {
                                listSampleType[i].Focus();
                                //ISsampleType = true;
                                return;
                            }
                                //else
                                //{
                                //    ISsampleType = true;
                                //}
                            //}
                        }
                       
                        if (listproject[i].Text.Trim().Length == 0)
                        {
                            //if (!IsProject)
                            //{
                            //    warmingUser = warmingUser.Length == 0 ? warmingUser = "项目部" : warmingUser + "、项目部";
                            //    IsProject = true;
                            if (MessageBox.Show("项目部为空将不能上传数据，是否需继续？\r\n\r\n可到设置界面选择项目部", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) != MessageBoxResult.Yes)
                            {
                                listproject[i].Focus();
                                //IsProject = true;
                                return;
                            }
                                //else
                                //{
                                //    IsProject = true;
                                //}
                            //}
                        }
                        if (listCompany[i].Text.Trim().Length == 0)
                        {
                            //if (!IsCompany)
                            //{
                            //    warmingUser = warmingUser.Length == 0 ? warmingUser = "被检单位" : warmingUser + "、被检单位";
                            //    IsCompany = true;
                            if (MessageBox.Show("被检单位为空将不能上传数据，是否需继续？\r\n\r\n可双击选择被检单位", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) != MessageBoxResult.Yes)
                            {
                                listCompany[i].Focus();
                                //IsCompany = true;
                                return;
                            }
                                //else
                                //{
                                //    IsCompany = true;
                                //}
                            //}
                        }

                        if (liststall[i].Text.Trim().Length == 0)
                        {
                            //if (!IsStall)
                            //{
                            //    warmingUser = warmingUser.Length == 0 ? warmingUser = "摊位号" : warmingUser + "、摊位号";
                            //    IsStall = true;

                            if (MessageBox.Show("摊位号为空将不能上传数据，是否需继续？\r\n\r\n可双击选择摊位号", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) != MessageBoxResult.Yes)
                            {
                                liststall[i].Focus();
                                //IsStall = true;
                                return;
                            }
                                //else
                                //{
                                //    IsStall = true;
                                //}
                                
                            //}
                        }

                       
                        //if (Global.InterfaceType.Equals("KJ") && listTask[i].Text.Trim().Length == 0)
                        //{
                        //    MessageBox.Show("检测任务不能为空!\r\n可手工输入或双击选择检测任务", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        //    listTask[i].Focus();
                        //    return;
                        //}

                        //甘肃 被检单位&生产单位不能为空
                        if (Global.InterfaceType.Equals("GS") || Global.InterfaceType.Equals("KJ"))
                        {
                            if (listCompany[i].Text.Trim().Length == 0)
                            {
                                MessageBox.Show(this, "被检单位不能为空!\n\n可手工输入或双击选择被检单位", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                listCompany[i].Focus();
                                return;
                            }
                            if (Global.InterfaceType.Equals("GS") && listProduceCompany[i].Text.Trim().Length == 0)
                            {
                                MessageBox.Show(this, "生产单位不能为空!\n\n可手工输入或双击选择被检单位", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                listProduceCompany[i].Focus();
                                return;
                            }
                        }

                        IsContrast = true;

                        _item.Hole[i].SampleName = listSampleName[i].Text.Trim();
                        _item.Hole[i].SampleId = listSampleid != null && listSampleid.Count > 0 ? listSampleid[i].Text.Trim() : string.Empty;
                        _item.Hole[i].SampleTypeName = listSampleType != null && listSampleType.Count > 0 ? listSampleType[i].Text.Trim() : string.Empty;//样品种类
                        
                        //反应液滴数OR稀释倍数
                        if ((_item.Method == 1) || (_item.Method == 4))
                            _item.Hole[i].DishuOrBeishu = Global.StringConvertDouble(listDiShuOrBeiShu[i].Text.Trim());
                        //任务主题
                        if (listTask != null && listTask.Count > 0)
                        {
                            _item.Hole[i].TaskCode = listTask[i].DataContext.ToString();
                            _item.Hole[i].TaskName = listTask[i].Text.Trim();
                        }
                        else
                        {
                            _item.Hole[i].TaskName = string.Empty;
                            _item.Hole[i].TaskCode = string.Empty;
                        }
                        //被检单位&生产单位
                        _item.Hole[i].CompanyName = listCompany != null && listCompany.Count > 0 ? listCompany[i].Text.Trim() : "";
                        _item.Hole[i].CompanyCode  = listCompany != null && listCompany.Count > 0 ? listCompany[i].DataContext .ToString ().Trim() : "";
                        _item.Hole[i].ProduceCompany = listProduceCompany != null && listProduceCompany.Count > 0 ? listProduceCompany[i].Text.Trim() : string.Empty;

                        //项目部
                        _item.Hole[i].projectName = listproject != null ? listproject[i].Text.Trim() : "";
                        _item.Hole[i].projectCode  = listproject != null ? listproject[i].DataContext.ToString ().Trim() : "";

                        //摊位号
                        _item.Hole[i].stall = liststall != null ? liststall[i].Text.Trim() : "";
                        _item.Hole[i].StallCode = liststall != null ? liststall[i].DataContext.ToString().Trim() : "";

                        if (!listSampleName[i].Text.Trim().Equals("对照样") && Global.CheckItemAndFoodIsNull(_item.Name, listSampleName[i].Text.Trim()))
                        {
                            if (MessageBox.Show(string.Format("检测到样品[{0}]没有对应检测标准，是否立即添加检测标准？\r\n\r\n备注：没有对应检测标准时，可能无法得到准确的检测结果！", _item.Hole[i].SampleName), "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                AddOrUpdateSample swindow = new AddOrUpdateSample();
                                try
                                {
                                    swindow.textBoxName.Text = swindow._projectName = _item.Name;
                                    swindow._sampleName = _item.Hole[i].SampleName;
                                    swindow._addOrUpdate = "ADD";
                                    swindow.ShowDialog();
                                    return;
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(string.Format("新增样品时出现异常：{0}", ex.Message));
                                    return;
                                }
                            }
                        }
                    }
                }
               
                 //if(warmingUser.Length >0 )
                 //{
                 //    if (MessageBox.Show(warmingUser+"为空将不能上传数据，是否需继续？\r\n\r\n可双击选择", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) != MessageBoxResult.Yes)
                 //    {
                 //        return;
                 //    }
                 //}
                //Global.stopwatch.Start();//启动代码运行计数器计算运行时间
                //Global.stopwatch.Stop();
                //string runTime = Global.stopwatch.ElapsedMilliseconds.ToString();
                //Console.Write(runTime);
                if(Global.Token =="")
                {
                    if(MessageBox.Show("未进行通信测试将导致数据上传失败！！！是否前往？","操作提示",MessageBoxButton.YesNo ,MessageBoxImage.Warning )==MessageBoxResult.Yes )
                    {
                        SettingsWindow windowset = new SettingsWindow();
                        windowset.ShowDialog();
                    }
                }
              

                FgdMeasureWindow window = new FgdMeasureWindow
                {
                    _item = _item
                };
                if (0 == _item.Method)
                {
                    if (Double.MinValue == _item.ir.RefDeltaA)
                        MessageBox.Show(this, "当前检测项目需要进行对照测试!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (1 == _item.Method)
                {
                    if (Double.MinValue == _item.sc.RefA)
                        MessageBox.Show(this, "当前检测项目需要进行对照测试!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                window.ShowInTaskbar = false;
                window.Owner = this;
                window.ShowDialog();

                //检测完后如果已经对照则清除第一个孔的
                for (i = 0; i < Global.deviceHole.HoleCount; ++i)
                {
                    if (_item.Hole[i].Use)
                    {
                        listSampleName[i].Text = listSampleName[i].Text.Trim().Equals("对照样") ? listSampleName[i + 1].Text : listSampleName[i].Text;
                    }
                }

            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            //标记COM3未使用
            CFGUtils.SaveConfig("FgIsStart", "0");

            System.Console.WriteLine(CFGUtils.GetConfig("FgIsStart", "0"));
            this.Close();
        }

        private void UpdateHoleWaveIdx()
        {
            try
            {
                List<int> waveIdx = Global.deviceHole.GetHoleWaveIdx(_item.Wave);
                for (int i = 0; i < waveIdx.Count; ++i)
                {
                    _item.Hole[i].WaveIndex = waveIdx[i];
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void ShowAllChannel()
        {
            try
            {
                for (int i = 0; i < Global.deviceHole.HoleCount; ++i)
                {
                    UIElement element = GenerateChlBriefLayout(i, string.Empty);
                    WrapPanelChannel.Children.Add(element);
                    if (_item.Hole[i].WaveIndex >= 0)
                    {
                        //2015年9月10日 wenj 修改进入检查通道是全部不选
                        _item.Hole[i].Use = false;
                    }
                    else
                    {
                        _item.Hole[i].Use = false;
                        element.Visibility = System.Windows.Visibility.Collapsed;
                    }
                }

                List<Border> borderList = UIUtils.GetChildObjects<Border>(WrapPanelChannel, "border");
                if (null == borderList)
                    return;

                for (int i = 0; i < borderList.Count; ++i)
                {
                    if (_item.Hole[i].Use)
                        borderList[i].BorderBrush = _borderBrushSelected;
                    else
                        borderList[i].BorderBrush = _borderBrushNormal;
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void ShowBorder(object sender, MouseButtonEventArgs e, string type)
        {
            try
            {
                List<Border> borderList = UIUtils.GetChildObjects<Border>(WrapPanelChannel, "border");
                List<TextBox> tbSampleNameList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "SampleName");
                List<TextBox> tbTaskList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "TaskName");
                List<TextBox> tbCompanyList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Company");
                List<TextBox> tbProduceCompanyList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "ProduceCompany");
                List<TextBox> tbSampleidList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Sampleid");
                List<TextBox> tbProIndexList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "ProIndex");
                List<TextBox> tbDiShuOrBeiShuList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "DiShuOrBeiShu");

                List<TextBox> tbprojectList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "project");
                List<TextBox> tbstallList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "stall");
                List<TextBox> listSampleType = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "SampleType");

                for (int i = 0; i < borderList.Count; ++i)
                {
                    if (type.Equals("border"))
                    {
                        if (borderList[i] == sender)
                        {
                            if (!_item.Hole[i].Use)
                            {
                                Global.SelIndex = _SelIndex = i;
                                borderList[i].BorderBrush = _borderBrushSelected;
                                _item.Hole[i].Use = true;
                                _checkNum += 1;
                            }
                            else
                            {
                                borderList[i].BorderBrush = _borderBrushNormal;
                                _item.Hole[i].Use = false;
                                _checkNum -= 1;
                            }
                            if (_item.Hole[i].Use)
                            {
                                Global.SelIndex = _SelIndex = i;
                            }
                            break;
                        }
                    }
                    else if (type.Equals("SampleName"))
                    {
                        if (tbSampleNameList[i] == sender)
                        {
                            if (!_item.Hole[i].Use)
                            {
                                Global.SelIndex = _SelIndex = i;
                                borderList[i].BorderBrush = _borderBrushSelected;
                                _item.Hole[i].Use = true;
                            }
                            if (_item.Hole[i].Use)
                            {
                                Global.SelIndex = _SelIndex = i;
                            }
                            if (!Global.sampleName.Equals(string.Empty))
                            {
                                tbSampleNameList[i].Text = Global.sampleName;
                            }
                            break;
                        }
                    }
                    else if (type.Equals("SampleType"))
                    {
                        if (sender == listSampleType[i])
                        {
                            
                            if (!_item.Hole[i].Use)
                            {
                                borderList[i].BorderBrush = _borderBrushSelected;
                                _item.Hole[i].Use = true;
                            }
                            if (_item.Hole[i].Use)
                            {
                                Global.SelIndex = _SelIndex = i;
                            }
                            if (!Global.projectName.Equals(string.Empty))
                                listSampleType[i].Text = Global.projectName;
                            if (!Global.projectUnit.Equals(string.Empty))
                                listSampleType[i].DataContext = Global.projectUnit;
                                
                            break;
                        }
                        //else
                        //    borderList[i].BorderBrush = _borderBrushNormal;
                    }
                    else if (type.Equals("TaskName"))
                    {
                        if (tbTaskList[i] == sender)
                        {
                            if (!_item.Hole[i].Use)
                            {
                                Global.SelIndex = _SelIndex = i;
                                borderList[i].BorderBrush = _borderBrushSelected;
                                _item.Hole[i].Use = true;
                            }
                            if (Global.InterfaceType.Equals("KJ"))
                            {
                                if (Global.KjServer._selectReceiveTasks != null)
                                {
                                    tbTaskList[i].Text = Global.KjServer._selectReceiveTasks.t_task_title;
                                    tbTaskList[i].DataContext = Global.KjServer._selectReceiveTasks.t_id;
                                    tbSampleNameList[i].Text = Global.KjServer._selectReceiveTasks.d_sample;
                                }
                            }
                            else if (!Global.TaskName.Equals(string.Empty))
                            {
                                tbTaskList[i].Text = Global.TaskName;
                                tbTaskList[i].DataContext = Global.TaskCode;
                            }
                            break;
                        }
                    }
                    else if (type.Equals("project"))//项目部
                    {
                        if (sender == tbprojectList[i])
                        {
                            
                            if (!_item.Hole[i].Use)
                            {
                               
                                borderList[i].BorderBrush = _borderBrushSelected;
                                _item.Hole[i].Use = true;
                            }
                            if (_item.Hole[i].Use)
                            {
                                Global.SelIndex = _SelIndex = i;
                            }
                            if (Global.departName != "")
                            {
                                tbprojectList[i].Text = Global.departName;
                                tbprojectList[i].DataContext = Global.departIDCode;
                                tbCompanyList[i].Text = "";
                                tbCompanyList[i].DataContext = "";
                                tbstallList[i].Text = "";
                                tbstallList[i].DataContext = "";
                                Global.departName = "";
                                Global.departIDCode = "";

                            }
                        }
                        //else
                        //    borderList[i].BorderBrush = _borderBrushNormal;

                    }
                    else if (type.Equals("stall"))//摊位号
                    {
                        if (sender == tbstallList[i])
                        {
                           
                            if (!_item.Hole[i].Use)
                            {
                                borderList[i].BorderBrush = _borderBrushSelected;
                                _item.Hole[i].Use = true;
                            }
                            if (_item.Hole[i].Use)
                            {
                                Global.SelIndex = _SelIndex = i;
                            }
                            if (Global.stallNum != "")
                            {
                                tbstallList[i].Text = Global.stallNum;
                                tbstallList[i].DataContext = Global.stallIDCode;
                                Global.stallNum = "";
                                Global.stallIDCode = "";
                            }

                        }
                        //else
                        //    borderList[i].BorderBrush = _borderBrushNormal;
                    }
                    else if (type.Equals("Company"))
                    {
                        if (tbCompanyList[i] == sender)
                        {
                           
                            if (!_item.Hole[i].Use)
                            {
                                borderList[i].BorderBrush = _borderBrushSelected;
                                _item.Hole[i].Use = true;
                            }
                            if (_item.Hole[i].Use)
                            {
                                Global.SelIndex = _SelIndex = i;
                            }
                            if (!Global.CompanyName.Equals(string.Empty))
                            {
                                tbCompanyList[i].Text = Global.CompanyName;
                                tbCompanyList[i].DataContext  = Global.CompanyID;
                                tbstallList[i].Text = "";
                                tbstallList[i].DataContext = "";
                                Global.CompanyName = "";
                                Global.CompanyID = "";
                                Global.stallNum = "";
                                Global.stallIDCode = "";
                            }
                                
                            break;
                        }
                    }
                    else if (type.Equals("ProduceCompany"))
                    {
                        if (sender == tbProduceCompanyList[i])
                        {
                            if (!_item.Hole[i].Use)
                            {
                                Global.SelIndex = _SelIndex = i;
                                borderList[i].BorderBrush = _borderBrushSelected;
                                _item.Hole[i].Use = true;
                            }
                            break;
                        }
                    }
                    else if (type.Equals("DiShuOrBeiShu"))
                    {
                        if (sender == tbDiShuOrBeiShuList[i])
                        {
                            if (!_item.Hole[i].Use)
                            {
                                Global.SelIndex = _SelIndex = i;
                                borderList[i].BorderBrush = _borderBrushSelected;
                                _item.Hole[i].Use = true;
                            }
                            break;
                        }
                    }
                    else if (type.Equals("Sampleid"))
                    {
                        if (tbSampleidList[i] == sender)
                        {
                            if (!_item.Hole[i].Use)
                            {
                                Global.SelIndex = _SelIndex = i;
                                borderList[i].BorderBrush = _borderBrushSelected;
                                _item.Hole[i].Use = true;
                            }
                            if (Wisdom.GETSAMPLECODE.Length > 0)
                            {
                                tbSampleidList[i].Text = Wisdom.GETSAMPLECODE;
                                tbCompanyList[i].Text = Global.CompanyName;
                                if (Global.IsSelectSampleName)
                                {
                                    DataTable dataTable = _clsTaskOpr.GetSampleByNameOrCode(Global.sampleName, _item.Name, true, true, 1);
                                    if (dataTable != null && dataTable.Rows.Count > 0)
                                    {
                                        tbSampleNameList[i].Text = Global.sampleName;
                                    }
                                    else
                                    {
                                        if (MessageBox.Show(this, "当前样品名称在本地数据库中未匹配，是否新建该样品？", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                        {
                                            AddOrUpdateSample addWindow = new AddOrUpdateSample();
                                            try
                                            {
                                                addWindow.textBoxName.Text = addWindow._projectName = _item.Name;
                                                addWindow._sampleName = Global.sampleName;
                                                addWindow.ShowDialog();
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show(this, ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                            }
                                        }
                                        tbSampleNameList[i].Text = Global.sampleName;
                                    }
                                }
                                Global.CompanyName = Global.sampleName = Wisdom.GETSAMPLECODE = string.Empty;
                            }
                            break;
                        }
                    }
                }
                if (_checkNum == Global.deviceHole.HoleCount)
                {
                    this.CheckBoxSelAll.IsChecked = true;
                    this.cb_SelAll.IsChecked = true;
                }
                else if (_checkNum < Global.deviceHole.HoleCount)
                {
                    this.CheckBoxSelAll.IsChecked = false;
                    this.cb_SelAll.IsChecked = true;
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowBorder(sender, e, "border");
        }

        private UIElement GenerateChlBriefLayout(int channel, string name)
        {
            Border border = new Border()
            {
                Width = 205,
                Margin = new Thickness(2),
                BorderThickness = new Thickness(5),
                CornerRadius = new CornerRadius(10),
                BorderBrush = _borderBrushNormal,
                Background = Brushes.AliceBlue,
                Name = "border"
            };
            border.MouseDown += Border_MouseDown;

            StackPanel stackPanel = new StackPanel()
            {
                Width = 205,
                Name = "stackPanel"
            };
            Grid grid = new Grid()
            {
                Width = 205,
                Height = 40
            };
            Label labelChannel = new Label()
            {
                FontSize = 20,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Content = " 检测通道 " + (channel + 1)
            };

            //检测人员
            WrapPanel wrapPannelDetectPeople = new WrapPanel()
            {
                Width = 205,
                Height = 30
            };
            Label labelDetectPeople = new Label()
            {
                Width = 93,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "检测人员",
                FlowDirection = System.Windows.FlowDirection.RightToLeft,
                VerticalContentAlignment = VerticalAlignment.Center
            };
            Label textBoxDetectPeople = new Label()
            {
                Width = 95,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = LoginWindow._userAccount != null ? LoginWindow._userAccount.UserName : string.Empty,
                VerticalContentAlignment = VerticalAlignment.Center
            };

            //样品名称
            WrapPanel wrapPannelSampleName = new WrapPanel()
            {
                Width = 205,
                Height = 30
            };
            Label labelX_SampleName = new Label()
            {
                Margin = new Thickness(0, 0, 0, 0),
                Width = 18,
                FontSize = 15,
                Content = "*",
                Foreground = new SolidColorBrush(Colors.Red),
                FlowDirection = System.Windows.FlowDirection.RightToLeft,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            wrapPannelSampleName.Children.Add(labelX_SampleName);

            Label labelSampleName = new Label()
            {
                Width = 75,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "样品名称",
                FlowDirection = System.Windows.FlowDirection.RightToLeft,
                VerticalContentAlignment = VerticalAlignment.Center
            };
            TextBox textBoxSampleName = new TextBox()
            {
                Width = 95,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Text = name.Equals(string.Empty) ? _item.Hole[channel].SampleName : name,
                VerticalContentAlignment = VerticalAlignment.Center,
                Name = "SampleName",
                Tag = channel,
                ToolTip = "双击可查询样品名称"
            };
            textBoxSampleName.MouseDoubleClick += textBoxSampleName_MouseDoubleClick;
            textBoxSampleName.PreviewMouseDown += textBoxSampleName_PreviewMouseDown;

            WrapPanel wrapPannelSampleType = new WrapPanel
            {
                Width = 205,
                Height = 30
            };

            Label labelX_SampleType = new Label()
            {
                Margin = new Thickness(0, 0, 0, 0),
                Width = 18,
                FontSize = 15,
                Content = "*",
                Foreground = new SolidColorBrush(Colors.Red),
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            wrapPannelSampleType.Children.Add(labelX_SampleType);

            Label labelSampleType = new Label
            {
                Width = 75,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "样品种类",
                FlowDirection = System.Windows.FlowDirection.RightToLeft,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            wrapPannelSampleType.Children.Add(labelSampleType);
            TextBox textBoxSampleType = new TextBox
            {
                Width = 95,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Text = _item.Hole[channel].SampleTypeName,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                Name = "SampleType",
                ToolTip = "双击可查询样品种类"
            };
            wrapPannelSampleType.Children.Add(textBoxSampleType);

            textBoxSampleType.MouseDoubleClick += textBoxSampleType_MouseDoubleClick;
            textBoxSampleType.PreviewMouseDown += textBoxSampleType_PreviewMouseDown;

            //稀释倍数|反应液滴数
            WrapPanel wrapPanelCount = null;
            Label labelCount = null;
            TextBox textBoxDiShuOrBeiShu = null;
            if ((_item.Method == 1) || (_item.Method == 4))
            {
                wrapPanelCount = new WrapPanel()
                {
                    Width = 205,
                    Height = 30
                };

                labelCount = new Label()
                {
                    Width = 93,
                    Height = 26,
                    Margin = new Thickness(0, 2, 0, 0),
                    FontSize = 15,
                    Content = (_item.Method == 1) ? "稀释倍数" : "反应液滴数",
                    FlowDirection = System.Windows.FlowDirection.RightToLeft,
                    VerticalContentAlignment = VerticalAlignment.Center
                };
                textBoxDiShuOrBeiShu = new TextBox()
                {
                    Width = 95,
                    Height = 26,
                    Margin = new Thickness(0, 2, 0, 0),
                    FontSize = 15,
                    Text = (_item.Method == 1) ? "1" : string.Empty,//_item.sc.CCA.ToString() (_item.Method == 1) ? _item.sc.CCA.ToString() : string.Empty,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Name = "DiShuOrBeiShu"
                };
                textBoxDiShuOrBeiShu.TextChanged += textBoxDiShuOrBeiShu_TextChanged;
                textBoxDiShuOrBeiShu.PreviewMouseDown += textBoxDiShuOrBeiShu_PreviewMouseDown;
            }
            //项目部
            WrapPanel wrapPannelProject = new WrapPanel()
            {
                Width = 205,
                Height = 30
            };
            Label labelX_Project = new Label()
            {
                Margin = new Thickness(0, 0, 0, 0),
                Width = 18,
                FontSize = 15,
                Content = "*",
                Foreground = new SolidColorBrush(Colors.Red),
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            wrapPannelProject.Children.Add(labelX_Project);

            Label labelProject = new Label()
            {
                Width = 75,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "项目部",
                FlowDirection = System.Windows.FlowDirection.RightToLeft,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            wrapPannelProject.Children.Add(labelProject);
            TextBox textBoxProject = new TextBox()
            {
                Width = 95,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                Name = "project",
                ToolTip = "双击可查询项目部",
                Text = _item.Hole[channel].projectName ,
                DataContext = _item.Hole[channel].projectCode ,
                IsReadOnly = true,
            };
            //textBoxProject.PreviewMouseDoubleClick += textBoxProject_MouseDoubleClick;
            textBoxProject.PreviewMouseDown += textBoxProject_PreviewMouseDown;
            wrapPannelProject.Children.Add(textBoxProject);

            //任务主题
            WrapPanel wrapPannelTask = null;
            Label labelTaskName = null;
            TextBox textBoxTaskName = null;
            if (Global.InterfaceType.Equals("DY") || Global.InterfaceType.Equals("GS") || Global.InterfaceType.Equals("KJ"))
            {
                wrapPannelTask = new WrapPanel()
                {
                    Width = 205,
                    Height = 30
                };

                Label labelX_TaskName = new Label()
                {
                    Margin = new Thickness(0, 0, 0, 0),
                    Width = 18,
                    FontSize = 15,
                    Content = "",//Global.InterfaceType.Equals("KJ") ? "*" : "",
                    Foreground = new SolidColorBrush(Colors.Red),
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Center
                };
                wrapPannelTask.Children.Add(labelX_TaskName);

                labelTaskName = new Label()
                {
                    Width = 75,
                    Height = 26,
                    Margin = new Thickness(0, 2, 0, 0),
                    FontSize = 15,
                    Content = "任务主题",
                    FlowDirection = System.Windows.FlowDirection.RightToLeft,
                    VerticalContentAlignment = VerticalAlignment.Center
                };
                textBoxTaskName = new TextBox()
                {
                    Width = 95,
                    Height = 26,
                    Margin = new Thickness(0, 2, 0, 0),
                    FontSize = 15,
                    Text = _item.Hole[channel].TaskName,
                    DataContext = _item.Hole[channel].TaskCode,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Name = "TaskName",
                    ToolTip = "双击可查询检测任务"
                };
                textBoxTaskName.MouseDoubleClick += textBoxTaskName_MouseDoubleClick;
                textBoxTaskName.PreviewMouseDown += textBoxTaskName_PreviewMouseDown;
            }

            //被检单位
            WrapPanel wrapPannelCompany = new WrapPanel()
            {
                Width = 205,
                Height = 30
            };
            Label labelX_Company = new Label()
            {
                Margin = new Thickness(0, 0, 0, 0),
                Width = 18,
                FontSize = 15,
                Content =  "*" ,
                Foreground = new SolidColorBrush(Colors.Red),
                FlowDirection = System.Windows.FlowDirection.RightToLeft,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            wrapPannelCompany.Children.Add(labelX_Company);

            Label labelCompany = new Label()
            {
                Width = 75,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "被检单位",
                FlowDirection = System.Windows.FlowDirection.RightToLeft,
                VerticalContentAlignment = VerticalAlignment.Center
            };
            TextBox textBoxCompany = new TextBox()
            {
                Width = 95,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Text = _item.Hole[channel].CompanyName,
                DataContext = _item.Hole[channel].CompanyCode,
                VerticalContentAlignment = VerticalAlignment.Center,
                Name = "Company",
                ToolTip = "双击可查询被检单位",
                IsReadOnly = true,
            };
            textBoxCompany.MouseDoubleClick += textBoxCompany_MouseDoubleClick;
            textBoxCompany.PreviewMouseDown += textBoxCompany_PreviewMouseDown;

            //生产单位
            WrapPanel wrapPannelProduceCompany = new WrapPanel()
            {
                Width = 205,
                Height = 30
            };

            Label labelX_ProduceCompany = new Label()
            {
                Margin = new Thickness(0, 0, 0, 0),
                Width = 18,
                FontSize = 15,
                Content = "*",
                Foreground = new SolidColorBrush(Colors.Red),
                FlowDirection = System.Windows.FlowDirection.RightToLeft,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            wrapPannelProduceCompany.Children.Add(labelX_ProduceCompany);

            Label labelProduceCompany = new Label()
            {
                Width = 75,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "生产单位",
                FlowDirection = System.Windows.FlowDirection.RightToLeft,
                VerticalContentAlignment = VerticalAlignment.Center
            };
            TextBox textBoxProduceCompany = new TextBox()
            {
                Width = 95,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Text = _item.Hole[channel].ProduceCompany,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                Name = "ProduceCompany"
            };
            textBoxProduceCompany.PreviewMouseDown += textBoxProduceCompany_PreviewMouseDown;

            //摊位号
            WrapPanel wrapStall = new WrapPanel()
            {
                Width = 205,
                Height = 30
            };
            Label labelX_Stall = new Label()
            {
                Margin = new Thickness(0, 0, 0, 0),
                Width = 18,
                FontSize = 15,
                Content = "*",
                Foreground = new SolidColorBrush(Colors.Red),
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            wrapStall.Children.Add(labelX_Stall);

            Label labelStall = new Label()
            {
                Width = 75,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "摊位号",
                FlowDirection = System.Windows.FlowDirection.RightToLeft,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            wrapStall.Children.Add(labelStall);

            TextBox textBoxStall = new TextBox()
            {
                Width = 95,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Text = _item.Hole[channel].stall ,
                DataContext= _item.Hole[channel].StallCode ,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                Name = "stall",
                ToolTip = "双击可查询摊位号",
                IsReadOnly = true,
            };
            textBoxStall.PreviewMouseDoubleClick  += textBoxStall_MouseDoubleClick;
            textBoxStall.PreviewMouseDown += textBoxStall_PreviewMouseDown;
            wrapStall.Children.Add(textBoxStall);

            //快检单号
            WrapPanel wrapSampleid = null;
            if (Global.InterfaceType.Equals("ZH"))
            {
                wrapSampleid = new WrapPanel()
                {
                    Width = 205,
                    Height = 30
                };
                Label labelX_Sampleid = new Label()
                {
                    Margin = new Thickness(0, 0, 0, 0),
                    Width = 18,
                    FontSize = 15,
                    Content = "*",
                    Foreground = new SolidColorBrush(Colors.Red),
                    FlowDirection = System.Windows.FlowDirection.RightToLeft,
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Center
                };
                wrapSampleid.Children.Add(labelX_Sampleid);

                Label labelSampleid = new Label()
                {
                    Width = 75,
                    Height = 26,
                    Margin = new Thickness(0, 2, 0, 0),
                    FontSize = 15,
                    Content = "快检单号",
                    FlowDirection = System.Windows.FlowDirection.RightToLeft,
                    VerticalContentAlignment = VerticalAlignment.Center
                };
                wrapSampleid.Children.Add(labelSampleid);

                TextBox tbSampleid = new TextBox()
                {
                    Width = 95,
                    Height = 26,
                    Margin = new Thickness(0, 2, 0, 0),
                    FontSize = 15,
                    Text = _item.Hole[channel].SampleId,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Name = "Sampleid",
                    ToolTip = "双击可查询所有快检单号",
                    IsReadOnly = true
                };
                tbSampleid.MouseDoubleClick += tbSampleid_MouseDoubleClick;
                tbSampleid.PreviewMouseDown += tbSampleid_PreviewMouseDown;
                wrapSampleid.Children.Add(tbSampleid);
            }

            grid.Children.Add(labelChannel);
            wrapPannelDetectPeople.Children.Add(labelDetectPeople);
            wrapPannelDetectPeople.Children.Add(textBoxDetectPeople);
            wrapPannelSampleName.Children.Add(labelSampleName);
            wrapPannelSampleName.Children.Add(textBoxSampleName);
            
            wrapPannelCompany.Children.Add(labelCompany);
            wrapPannelCompany.Children.Add(textBoxCompany);
            stackPanel.Children.Add(grid);
            stackPanel.Children.Add(wrapPannelDetectPeople);
            stackPanel.Children.Add(wrapPannelSampleName);
            stackPanel.Children.Add(wrapPannelSampleType);
            stackPanel.Children.Add(wrapPannelProject);

            if ((_item.Method == 1) || (_item.Method == 4))
            {
                wrapPanelCount.Children.Add(labelCount);
                wrapPanelCount.Children.Add(textBoxDiShuOrBeiShu);
                stackPanel.Children.Add(wrapPanelCount);
            }

            if (Global.InterfaceType.Equals("DY") || Global.InterfaceType.Equals("GS") || Global.InterfaceType.Equals("KJ"))
            {
                wrapPannelTask.Children.Add(labelTaskName);
                wrapPannelTask.Children.Add(textBoxTaskName);
                stackPanel.Children.Add(wrapPannelTask);
            }

            stackPanel.Children.Add(wrapPannelCompany);
            if (Global.InterfaceType.Equals("GS"))
            {
                wrapPannelProduceCompany.Children.Add(labelProduceCompany);
                wrapPannelProduceCompany.Children.Add(textBoxProduceCompany);
                stackPanel.Children.Add(wrapPannelProduceCompany);
            }
            if (Global.InterfaceType.Equals("ZH")) stackPanel.Children.Add(wrapSampleid);
            stackPanel.Children.Add(wrapStall);

            border.Child = stackPanel;
            return border;
        }

        private void textBoxStall_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //throw new NotImplementedException();
            Global.stallNum = "";
            Global.stallIDCode = "";
            ShowBorder(sender, e, "stall");
        }

        private  void textBoxProject_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Global.departName = "";
            Global.departIDCode = "";
            ShowBorder(sender, e, "project");
        }

        /// <summary>
        /// 双击样品种类文本框先选中当前border
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxSampleType_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            Global.projectName = Global.projectUnit = string.Empty;
            //SearchFoodTypeWindow window = new SearchFoodTypeWindow();
            //window.ShowDialog();
            GCFoodTypeWindow window = new GCFoodTypeWindow();
            window.ShowDialog();
            ShowBorder(sender, e, "SampleType");
            Global.projectName = Global.projectUnit = string.Empty;
        }

        private void textBoxSampleType_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowBorder(sender, e, "SampleType");
        }

        private void textBoxStall_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            List<TextBox> tbCompanyList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Company");
            if (tbCompanyList[_SelIndex].Text.Trim() == "")
            {
                MessageBox.Show("请选择被检单位再选择摊位号！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            GCStallWindow window = new GCStallWindow();
            window.Companyname = tbCompanyList[_SelIndex].Text.Trim();
            window.ShowDialog();
            ShowBorder(sender, e, "stall");
        }
        private void textBoxProject_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Global.departName = "";
            Global.departIDCode = "";
            GCProjectWindow window = new GCProjectWindow();
            window.ShowDialog();
            ShowBorder(sender, e, "project");
        }

        private void textBoxProduceCompany_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowBorder(sender, e, "ProduceCompany");
        }

        private void textBoxDiShuOrBeiShu_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowBorder(sender, e, "DiShuOrBeiShu");
        }

        private void tbSampleid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowBorder(sender, e, "Sampleid");
        }

        private void tbSampleid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Wisdom.GETSAMPLECODE = String.Empty;
            Global.sampleName = String.Empty;
            Global.CompanyName = String.Empty;
            SearchSampleidWindow windowCompany = new SearchSampleidWindow
            {
                ShowInTaskbar = false,
                Owner = this
            };
            windowCompany.ShowDialog();
            ShowBorder(sender, e, "Sampleid");
        }

        private void textBoxCompany_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowBorder(sender, e, "Company");
        }

        private void textBoxTaskName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowBorder(sender, e, "TaskName");
        }

        private void textBoxSampleName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowBorder(sender, e, "SampleName");
        }

        /// <summary>
        /// 样子当前样品和检测项目是否在本地数据库中有对应标准
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxSampleName_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                TextBox textBox = sender as TextBox;
                string val = textBox.Text.Trim();
                if (val.Length == 0) return;

                if (Global.CheckItemAndFoodIsNull(_item.Name, val))
                {
                    if (MessageBox.Show(string.Format("检测到样品[{0}]没有对应检测标准，是否立即添加检测标准？\r\n\r\n备注：没有对应检测标准时，可能无法得到准确的检测结果！",
                        val), "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        AddOrUpdateSample swindow = new AddOrUpdateSample();
                        try
                        {
                            swindow.textBoxName.Text = swindow._projectName = _item.Name;
                            swindow._sampleName = val;
                            swindow._addOrUpdate = "ADD";
                            swindow.ShowDialog();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
            }
        }

        private void CheckBoxSelAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool isChecked = (bool)CheckBoxSelAll.IsChecked;
                List<Border> listBorder = UIUtils.GetChildObjects<Border>(WrapPanelChannel, "border");
                _checkNum = isChecked ? listBorder.Count : 0;
                for (int i = 0; i < listBorder.Count; ++i)
                {
                    if (isChecked)
                    {
                        listBorder[i].BorderBrush = _borderBrushSelected;
                        _item.Hole[i].Use = true;
                    }
                    else
                    {
                        listBorder[i].BorderBrush = _borderBrushNormal;
                        _item.Hole[i].Use = false;
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void textBoxDiShuOrBeiShu_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox textBox = sender as TextBox;
                TextChange[] change = new TextChange[e.Changes.Count];
                e.Changes.CopyTo(change, 0);
                int offset = change[0].Offset;
                if (change[0].AddedLength > 0)
                {
                    double num = 0;
                    if (!Double.TryParse(textBox.Text, out num))
                    {
                        textBox.Text = textBox.Text.Remove(offset, change[0].AddedLength);
                        textBox.Select(offset, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
            }
        }

        /// <summary>
        /// 双击样品名称文本框先选中当前border
        /// 然后弹出样品小精灵
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxSampleName_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //弹出样品小精灵
            ShowSample();
            //先选中当前border
            ShowBorder(sender, e, "SampleName");
            Global.sampleName = string.Empty;
        }

        /// <summary>
        /// 样品小精灵
        /// </summary>
        private void ShowSample()
        {
            Global.IsProject = false;
            SearchSample searchSample = new SearchSample
            {
                _projectName = this._item.Name
            };
            searchSample.ShowDialog();
        }

        /// <summary>
        /// 双击任务主题文本框先选中当前border
        /// 然后弹出检测任务列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxTaskName_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Global.InterfaceType.Equals("KJ"))
            {
                SearchReceiveTasks window = new SearchReceiveTasks
                {
                    itemName = _item.Name
                };
                window.ShowDialog();
            }
            else
            {
                SearchTaskWindow searchTaskWindow = new SearchTaskWindow();
                searchTaskWindow.ShowDialog();
            }
            ShowBorder(sender, e, "TaskName");
            Global.TaskName = string.Empty;
            Global.TaskCode = string.Empty;
            Global.KjServer._selectReceiveTasks = null;
        }

        private void textBoxCompany_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Global.InterfaceType.Equals("KJ"))
            {
                SearchCompanys window = new SearchCompanys();
                window.ShowDialog();
            }
            else
            {
                List<TextBox> tbprojectList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "project");
               
                if (tbprojectList[_SelIndex].Text.Trim() == "")
                {
                    MessageBox.Show("请选择项目部再选择被检单位！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                SearchCompanyWindow windowCompany = new SearchCompanyWindow();
                windowCompany.projectName = tbprojectList[_SelIndex].Text.Trim();
                windowCompany.ShowDialog();
            }
            ShowBorder(sender, e, "Company");
            Global.CompanyName = string.Empty;
            Global.CompanyID = "";
            Global.KjServer._selectCompany = null;
            Global.KjServer._selectBusiness = null;
        }

        private void cb_SelAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool isChecked = (bool)cb_SelAll.IsChecked;
                if (lb_selAll.Content.Equals("全选"))
                {
                    lb_selAll.Content = "全不选";
                }
                else
                {
                    lb_selAll.Content = "全选";
                }
                List<Border> listBorder = UIUtils.GetChildObjects<Border>(WrapPanelChannel, "border");
                _checkNum = isChecked ? listBorder.Count : 0;
                for (int i = 0; i < listBorder.Count; ++i)
                {
                    if (isChecked)
                    {
                        listBorder[i].BorderBrush = _borderBrushSelected;
                        _item.Hole[i].Use = true;
                    }
                    else
                    {
                        listBorder[i].BorderBrush = _borderBrushNormal;
                        _item.Hole[i].Use = false;
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

    }
}
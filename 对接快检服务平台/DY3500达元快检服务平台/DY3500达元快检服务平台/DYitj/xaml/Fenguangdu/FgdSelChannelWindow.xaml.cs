using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using AIO.xaml.Dialog;
using com.lvrenyang;
using DYSeriesDataSet;
using DYSeriesDataSet.DataModel;

namespace AIO
{
    /// <summary>
    /// SelChannelWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FgdSelChannelWindow : Window
    {
        #region 全局变量
        public deviceItem _item = null;
        public DYFGDItemPara _itemo = null;
        public int testhole = 0;
        private Brush _borderBrushSelected = new SolidColorBrush(Color.FromRgb(224, 67, 67));
        private Brush _borderBrushNormal = new SolidColorBrush(Color.FromRgb(0x00, 0x7C, 0xC2));
        private int _SelIndex = -1;
        private int _checkNum = 0;
        private clsTaskOpr _clsTaskOpr = new clsTaskOpr();
        private DataTable _Tc = new DataTable();
        private bool InRepair = false;
        /// <summary>
        /// 接口若为省智慧平台则显示快检单号
        /// </summary>
        private Boolean InterfaceType = (Global.InterfaceType.Equals("ZH") || Global.InterfaceType.Equals("ALL")) ? true : false;
        /// <summary>
        /// 是否是甘肃地区
        /// </summary>
        private Boolean EachDistrict = Global.EachDistrict.Equals("GS") ? true : false;
        private bool isduizhao = false;
        public int selHole = 0;

        #endregion

        public FgdSelChannelWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Global.ManuTest == true)
            {
                if (null == _item)
                {
                    MessageBox.Show(this, "项目异常", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Close();
                }
                labelTitle.Content = "分光模块  " + _item.item;
                UpdateHoleWaveIdx();
                ShowAllChannelo();
            }
            else
            {
                isduizhao = false;
                if (null == _item)
                {
                    MessageBox.Show(this, "项目异常", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Close();
                }
                if ("抑制率法" == _item.detect_method)
                {
                    if (Double.MinValue == _item.ir.RefDeltaA &&( _item.RefDeltaA==null||_item.RefDeltaA==""))
                        isduizhao = true;//MessageBox.Show(this, "当前检测项目需要进行对照测试!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    else
                    {
                        DateTime t1 = DateTime.Now;
                        DateTime t2 = DateTime.Parse(_item.SaveTime);

                        TimeSpan t3 = t1 - t2;  //两个时间相减 。默认得到的是两个的时间差  格式如：365.10:35:25 
                        double getHours = t3.TotalHours; //将这个天数转换成小时, 返回值是double类型的  
                        if (getHours > 24)
                        {
                            isduizhao = true;
                        }
                        else
                        {
                            _item.ir.RefDeltaA =double.Parse(_item.RefDeltaA);
                        }
                    }
                }
                else if ("标准曲线法" == _item.detect_method)
                {
                    if (Double.MinValue == _item.sc.RefA && (_item.RefDeltaA == null || _item.RefDeltaA == ""))
                        isduizhao = true; //MessageBox.Show(this, "当前检测项目需要进行对照测试!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    else
                    {
                        DateTime t1 = DateTime.Now;
                        DateTime t2 = DateTime.Parse(_item.SaveTime);

                        TimeSpan t3 = t1 - t2;  //两个时间相减 。默认得到的是两个的时间差  格式如：365.10:35:25 
                        double getHours = t3.TotalHours; //将这个天数转换成小时, 返回值是double类型的  
                        if (getHours > 24)
                        {
                            isduizhao = true;
                        }
                        else
                        {
                            _item.sc.RefA = double.Parse(_item.RefDeltaA);
                        }
                    }
                }
                if (isduizhao == true)
                {
                    testhole = testhole + 1;
                }
                labelTitle.Content ="分光模块  "+ _item.item;
                UpdateHoleWaveIdx();
                ShowAllChannel(testhole);
            }
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (Global.ManuTest == true)
            {
                // 是否选择了检测孔
                int i = 0;
                try
                {
                    for (i = 0; i < Global.deviceHole.HoleCount; ++i)
                    {
                        if (_item.Hole[i].Use)
                            break;
                    }
                    if (Global.deviceHole.HoleCount == i)
                    {
                        MessageBox.Show(this, "请至少选择一个检测孔!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    List<TextBox> listSampleName = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "SampleName");
                    List<TextBox> listDiShuOrBeiShu = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "DiShuOrBeiShu");
                    List<TextBox> listTask = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "TaskName");
                    List<TextBox> listCompany = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Company");
                    List<TextBox> listProduceCompany = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "ProduceCompany");
                    List<TextBox> listSampleid = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Sampleid");
                    List<Label> holelist = UIUtils.GetChildObjects<Label>(WrapPanelChannel, "LabelHole");
                    List<TextBox> listOperator = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Operator");

                    bool IsContrast = false;
                    for (i = 0; i < Global.deviceHole.HoleCount; ++i)
                    {
                        if (_item.Hole[i].Use)
                        {
                            #region 必填验证
                            //如果检测项目需要对照且还没有对照时
                            if (!IsContrast)
                            {
                                if ((_item.detect_method == "抑制率法" && Double.MinValue == _item.ir.RefDeltaA) || (_item.detect_method == "标准曲线法" && Double.MinValue == _item.sc.RefA))
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
                                if (listCompany[i].Text.Trim().Length == 0)
                                {
                                    if (MessageBox.Show(this, "被检单位为空，将无法上传检测数据！！\n\n是否继续？", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                                    {
                                        listCompany[i].Focus();
                                        return;
                                    }
                                }
                                if (EachDistrict)
                                {
                                    if (listCompany[i].Text.Trim().Length == 0)
                                    {
                                        MessageBox.Show(this, "被检单位不能为空!\n\n可手工输入或双击选择被检单位", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                        listCompany[i].Focus();
                                        return;
                                    }
                                    if (listProduceCompany[i].Text.Trim().Length == 0)
                                    {
                                        MessageBox.Show(this, "生产单位不能为空!\n\n可手工输入或双击选择被检单位", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                        listProduceCompany[i].Focus();
                                        return;
                                    }
                                }
                                if (InterfaceType && LoginWindow._userAccount.CheckSampleID)
                                {
                                    if (listSampleid != null && listSampleid[i].Text.Trim().Length == 0)
                                    {
                                        MessageBox.Show(this, "快检单号不能为空!\n\n可手工输入或双击选择被检单位", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                        listSampleid[i].Focus();
                                        return;
                                    }
                                }
                            }
                            IsContrast = true;
                            #endregion
                            _item.Hole[i].SampleName = listSampleName[i].Text.Trim();
                            _item.Hole[i].SampleId = listSampleid.Count > 0 ? listSampleid[i].Text.Trim() : string.Empty;
                            //反应液滴数OR稀释倍数
                            if ((_item.detect_method == "标准曲线法") || (_item.detect_method == "系数法"))
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

                            _item.Hole[i].Operator = listOperator != null && listOperator.Count > 0 ? listOperator[i].Text.Trim() : "";
                            _item.Hole[i].OperatorID = listSampleid != null && listSampleid.Count > 0 ? listSampleid[i].Text.Trim() : "";

                            //被检单位
                            _item.Hole[i].CompanyName = listCompany[i].Text.Trim();
                            //生产单位
                            if (EachDistrict)
                                _item.Hole[i].ProduceCompany = listProduceCompany[i].Text.Trim();
                        }
                    }
                    //Global.SerializeToFile(Global.fgdItems, Global.fgdItemsFile);
                    FgdMeasureWindow window = new FgdMeasureWindow()
                    {
                        _item = _item
                    };
                    if (_item.detect_method == "抑制率法")
                    {
                        if (Double.MinValue == _item.ir.RefDeltaA)
                        {
                            MessageBox.Show(this, "当前检测项目需要进行对照测试!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {
                            DateTime t1 = DateTime.Now;
                            DateTime t2 = DateTime.Parse(_item.SaveTime);

                            TimeSpan t3 = t1 - t2;  //两个时间相减 。默认得到的是两个的时间差  格式如：365.10:35:25 
                            double getHours = t3.TotalHours; //将这个天数转换成小时, 返回值是double类型的  
                            if(getHours>24)
                            {
                                MessageBox.Show(this, "当前检测项目需要进行对照测试!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                                _item.ir.RefDeltaA = Double.MinValue;
                            }
                        }
                            
                    }
                    else if (_item.detect_method == "标准曲线法")
                    {
                        if (Double.MinValue == _item.sc.RefA)
                            MessageBox.Show(this, "当前检测项目需要进行对照测试!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        else
                        {
                            DateTime t1 = DateTime.Now;
                            DateTime t2 = DateTime.Parse(_item.SaveTime);

                            TimeSpan t3 = t1 - t2;  //两个时间相减 。默认得到的是两个的时间差  格式如：365.10:35:25 
                            double getHours = t3.TotalHours; //将这个天数转换成小时, 返回值是double类型的  
                            if (getHours > 24)
                            {
                                MessageBox.Show(this, "当前检测项目需要进行对照测试!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                                _item.sc.RefA = Double.MinValue;
                            }
                        }
                    }
                    window.ShowInTaskbar = false; window.Owner = this; window.ShowDialog();
                    if (Global.ManuTest == false )
                    {
                        this.Close();
                    }
                    //检测完后如果已经对照则清除第一个孔的
                    //for (i = 0; i < Global.deviceHole.HoleCount; ++i)
                    //{
                    //    if (_item.Hole[i].Use)
                    //    {
                    //        listSampleName[i].Text = (_item.detect_method == "抑制率法" && Double.MinValue != _item.ir.RefDeltaA) ||
                    //            (_item.detect_method == "标准曲线法" && Double.MinValue != _item.sc.RefA) ? string.Empty : "对照样";
                    //        break;
                    //    }
                    //}
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "出现异常! - 异常代码[ButtonNext]\r\n异常信息：" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
 
            }
            else 
            {
                // 是否选择了检测孔
                //Global.deviceHole.HoleCount = testhole;
                int i = 0;
                try
                {
                    for (i = 0; i < Global.deviceHole.HoleCount; ++i)//Global.deviceHole.HoleCount
                    {
                        if (_item.Hole[i].Use)
                            break;
                    }
                    if (Global.deviceHole.HoleCount == i)
                    {
                        MessageBox.Show(this, "请至少选择一个检测孔!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    List<TextBox> listSampleName = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "SampleName");
                    List<TextBox> listDiShuOrBeiShu = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "DiShuOrBeiShu");
                    List<TextBox> listTask = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "TaskName");
                    List<TextBox> listCompany = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Company");
                    List<TextBox> listProduceCompany = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "ProduceCompany");
                    List<TextBox> listSampleid = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Sampleid");
                    List<Label> holelist = UIUtils.GetChildObjects<Label>(WrapPanelChannel, "LabelHole");
                    bool IsContrast = false;
                    
                    for (i = 0; i < Global.deviceHole.HoleCount; ++i) //Global.deviceHole.HoleCount testhole
                    {
                        if (_item.Hole[i].Use)
                        {
                            #region 必填验证
                            //如果检测项目需要对照且还没有对照时
                            if (!IsContrast)
                            {
                                if (("抑制率法" == _item.detect_method && Double.MinValue == _item.ir.RefDeltaA) || ("标准曲线法" == _item.detect_method && Double.MinValue == _item.sc.RefA))
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
                                //禅城区被检单位为必填
                                if (Global.EachDistrict.Equals("CC"))
                                {
                                    if (listCompany[i].Text.Trim().Length == 0)
                                    {
                                        MessageBox.Show(this, "被检单位不能为空!\n\n可手工输入或双击选择被检单位", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                        listCompany[i].Focus();
                                        return;
                                    }
                                }
                                if (EachDistrict)
                                {
                                    if (listCompany[i].Text.Trim().Length == 0)
                                    {
                                        MessageBox.Show(this, "被检单位不能为空!\n\n可手工输入或双击选择被检单位", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                        listCompany[i].Focus();
                                        return;
                                    }
                                    if (listProduceCompany[i].Text.Trim().Length == 0)
                                    {
                                        MessageBox.Show(this, "生产单位不能为空!\n\n可手工输入或双击选择被检单位", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                        listProduceCompany[i].Focus();
                                        return;
                                    }
                                }
                                if (InterfaceType && LoginWindow._userAccount.CheckSampleID)
                                {
                                    if (listSampleid != null && listSampleid[i].Text.Trim().Length == 0)
                                    {
                                        MessageBox.Show(this, "快检单号不能为空!\n\n可手工输入或双击选择被检单位", "操作提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                        listSampleid[i].Focus();
                                        return;
                                    }
                                }
                            }
                            IsContrast = true;
                            #endregion
                            _item.Hole[i].SampleName = listSampleName[i].Text.Trim();
                            _item.Hole[i].SampleId = listSampleid.Count > 0 ? listSampleid[i].Text.Trim() : string.Empty;
                            //反应液滴数OR稀释倍数
                            if ((_item.detect_method == "标准曲线法") || (_item.detect_method == "系数法"))
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
                            //被检单位
                            _item.Hole[i].CompanyName = listCompany[i].Text.Trim();
                            //生产单位
                            if (EachDistrict)
                                _item.Hole[i].ProduceCompany = listProduceCompany[i].Text.Trim();
                        }
                    }
                    //Global.SerializeToFile(Global.fgdItems, Global.fgdItemsFile);
                    FgdMeasureWindow window = new FgdMeasureWindow()
                    {
                        _item = _item,
                        selHole=testhole
                    };
                    if ("抑制率法" == _item.detect_method)
                    {
                        if (Double.MinValue == _item.ir.RefDeltaA)
                            MessageBox.Show(this, "当前检测项目需要进行对照测试!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        else
                        {
                            DateTime t1 = DateTime.Now;
                            DateTime t2 = DateTime.Parse(_item.SaveTime);

                            TimeSpan t3 = t1 - t2;  //两个时间相减 。默认得到的是两个的时间差  格式如：365.10:35:25 
                            double getHours = t3.TotalHours; //将这个天数转换成小时, 返回值是double类型的  
                            if (getHours > 24)
                            {
                                MessageBox.Show(this, "当前检测项目需要进行对照测试!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                                _item.ir.RefDeltaA = Double.MinValue;
                            }
                        }
                    }
                    else if ("标准曲线法" == _item.detect_method)
                    {
                        if (Double.MinValue == _item.sc.RefA)
                            MessageBox.Show(this, "当前检测项目需要进行对照测试!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        else
                        {
                            DateTime t1 = DateTime.Now;
                            DateTime t2 = DateTime.Parse(_item.SaveTime);

                            TimeSpan t3 = t1 - t2;  //两个时间相减 。默认得到的是两个的时间差  格式如：365.10:35:25 
                            double getHours = t3.TotalHours; //将这个天数转换成小时, 返回值是double类型的  
                            if (getHours > 24)
                            {
                                MessageBox.Show(this, "当前检测项目需要进行对照测试!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                                _item.sc.RefA = Double.MinValue;
                            }
                        }
                    }
                    window.ShowInTaskbar = false; window.Owner = this; window.ShowDialog();
                    this.Close();
                    //检测完后如果已经对照则清除第一个孔的
                    //for (i = 0; i < Global.deviceHole.HoleCount; ++i)
                    //{
                    //    if (_item.Hole[i].Use)
                    //    {
                    //        listSampleName[i].Text = (_item.detect_method == "抑制率法" && Double.MinValue != _item.ir.RefDeltaA) ||
                    //            (_item.detect_method == "标准曲线法" && Double.MinValue != _item.sc.RefA) ? string.Empty : "对照样";
                    //        break;
                    //    }
                    //}
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "出现异常! - 异常代码[ButtonNext]\r\n异常信息：" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
          
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            Global.WaitingWindowIsClose = true;
            this.Close();
        }

        private void UpdateHoleWaveIdx()
        {
            try
            {
                if (Global.ManuTest == true)
                {
                    List<int> waveIdx = Global.deviceHole.GetHoleWaveIdx(int.Parse(_item.wavelength));//_item.Wave
                    for (int i = 0; i < waveIdx.Count; ++i)
                    {
                        _item.Hole[i].WaveIndex = waveIdx[i];
                    }
                }
                else 
                {
                    List<int> waveIdx = Global.deviceHole.GetHoleWaveIdx(int.Parse(_item.wavelength));//_item.Wave
                    for (int i = 0; i < waveIdx.Count; ++i)
                    {
                        _item.Hole[i].WaveIndex = waveIdx[i];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "出现异常! - 异常代码[UpdateHoleWaveIdx]\r\n异常信息：" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ShowAllChannelo()
        {
            //int sampleNum = item.SampleNum;
            try
            {
                for (int i = 0; i < Global.deviceHole.HoleCount; ++i)
                {
                    UIElement element = oGenerateChlBriefLayout(i, string.Empty);
                    WrapPanelChannel.Children.Add(element);
                    if (_item.Hole[i].WaveIndex >= 0)
                    {
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
                MessageBox.Show(this, "出现异常! - 异常代码[ShowAllChannel]\r\n异常信息：" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowAllChannel(int selhole)
        {
            //int sampleNum = item.SampleNum;

            try
            {
                for (int i = 0; i < Global.deviceHole.HoleCount; ++i)//Global.deviceHole.HoleCount
                {
                    _item.Hole[i].HoleNumber = (i+1).ToString();
                    UIElement element = GenerateChlBriefLayout(i, string.Empty);
                    if (i < testhole)
                    {
                        if (isduizhao == true && i > 0)
                        {
                            Global.iscanzhao = true;
                            _item.Hole[i].tID = Global.Testsample[i - 1, 1];//记录检测任务ID判断数据是否保存过
                        }
                        else
                        {
                            _item.Hole[i].tID = Global.Testsample[i, 1];//记录检测任务ID判断数据是否保存过
                        }
                    }
                    WrapPanelChannel.Children.Add(element);
                    if (i < testhole)
                    {
                        if (_item.Hole[i].WaveIndex >= 0)
                        {
                         
                            _item.Hole[i].Use = true;
                        }
                        else
                        {
                            _item.Hole[i].Use = false;
                            element.Visibility = System.Windows.Visibility.Collapsed;
                        }
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
                MessageBox.Show(this, "出现异常! - 异常代码[ShowAllChannel]\r\n异常信息：" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                List<TextBox> tbSampleidList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Sampleid");
                List<TextBox> tbProduceCompanyList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "ProduceCompany");
                List<TextBox> tbDiShuOrBeiShuList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "DiShuOrBeiShu");
                List<Label> holelist = UIUtils.GetChildObjects<Label>(WrapPanelChannel, "LabelHole");
                List<TextBox> listOperator = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Operator");

                for (int i = 0; i < borderList.Count; ++i)
                {
                    if (Global.ManuTest == true)
                    {
                        if (type.Equals("border"))
                        {
                            if (borderList[i] == sender)
                            {
                                if (!_item.Hole[i].Use)
                                {
                                    borderList[i].BorderBrush = _borderBrushSelected;
                                    Global.SelIndex = _SelIndex = i;
                                    _item.Hole[i].Use = true;
                                    _checkNum += 1;
                                }
                                else
                                {
                                    borderList[i].BorderBrush = _borderBrushNormal;
                                    _item.Hole[i].Use = false;
                                    _checkNum -= 1;
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
                                if (!Global.sampleName.Equals(string.Empty))
                                {
                                    tbSampleNameList[i].Text = Global.sampleName;
                                    Global.sampleName = string.Empty;
                                }
                                //e=null时为扫描枪设备或其他方式录入需要将被检单位自动关联
                                if (e == null)
                                {
                                    if (!Global.CompanyName.Equals(string.Empty))
                                    {
                                        tbCompanyList[i].Text = Global.CompanyName;
                                        Global.CompanyName = string.Empty;
                                    }
                                }
                                break;
                            }
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
                                if (!Global.TaskName.Equals(string.Empty))
                                {
                                    tbTaskList[i].Text = Global.TaskName;
                                    tbTaskList[i].DataContext = Global.TaskCode;
                                    Global.TaskName = Global.TaskCode = string.Empty;
                                }
                                if (Global.Other.Length > 0)
                                {
                                    tbSampleNameList[i].Text = Global.GetTaskSampleName();
                                    Global.Other = string.Empty;
                                }
                                break;
                            }
                        }
                        else if (type.Equals("Company"))
                        {
                            if (tbCompanyList[i] == sender)
                            {
                                if (!_item.Hole[i].Use)
                                {
                                    Global.SelIndex = _SelIndex = i;
                                    borderList[i].BorderBrush = _borderBrushSelected;
                                    _item.Hole[i].Use = true;
                                }
                                if (!Global.CompanyName.Equals(string.Empty))
                                {
                                    tbCompanyList[i].Text = Global.CompanyName;
                                    Global.CompanyName = string.Empty;
                                    listOperator[i].Text = "";
                                    tbSampleidList[i].Text = "";
                                }
                                break;
                            }
                        }
                        else if (type.Equals("Operator"))//经营户
                        {
                            if (sender == listOperator[i])
                            {
                                if (!_item.Hole[i].Use)
                                {
                                    _SelIndex = i;
                                    borderList[i].BorderBrush = _borderBrushSelected;
                                    _item.Hole[i].Use = true;
                                }

                                if (Global.OpertorName != "")
                                {
                                    listOperator[i].Text = Global.OpertorName; //Global.KjServer._selectCompany.reg_name + "-" + Global.KjServer._selectBusiness.ope_shop_code;
                                    tbSampleidList[i].Text = Global.OpertorID;
                                }
                                Global.OpertorName = "";
                                Global.OpertorID = "";
                               
                                return;
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
                        else if (type.Equals("LabelHole"))//通道号
                        {
                            if (holelist[i] == sender)
                            {
                                holelist[i].Content = " 检测通道 " + Global.RepairHole;

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
                                        DataTable dataTable = _clsTaskOpr.GetSampleByNameOrCode(Global.sampleName, _item.item, true, true, 1);
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
                                                    addWindow.textBoxName.Text = addWindow._projectName = _item.item;
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
                    else
                    {
                        if (type.Equals("border"))
                        {
                            if (borderList[i] == sender)
                            {
                                if (!_item.Hole[i].Use)
                                {
                                    borderList[i].BorderBrush = _borderBrushSelected;
                                    Global.SelIndex = _SelIndex = i;
                                    if (InRepair == false)
                                    {
                                        _item.Hole[i].Use = true;
                                    }
                                    _checkNum += 1;
                                }
                                else
                                {
                                    borderList[i].BorderBrush = _borderBrushNormal;
                                    _item.Hole[i].Use = false;
                                    _checkNum -= 1;
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
                                    if (InRepair == false)
                                    {
                                        _item.Hole[i].Use = true;
                                    }
                                }
                                if (!Global.sampleName.Equals(string.Empty))
                                {
                                    tbSampleNameList[i].Text = Global.sampleName;
                                    Global.sampleName = string.Empty;
                                }
                                //e=null时为扫描枪设备或其他方式录入需要将被检单位自动关联
                                if (e == null)
                                {
                                    if (!Global.CompanyName.Equals(string.Empty))
                                    {
                                        tbCompanyList[i].Text = Global.CompanyName;
                                        Global.CompanyName = string.Empty;
                                    }
                                }
                                break;
                            }
                        }
                        else if (type.Equals("TaskName"))
                        {
                            if (tbTaskList[i] == sender)
                            {
                                if (!_item.Hole[i].Use)
                                {
                                    Global.SelIndex = _SelIndex = i;
                                    borderList[i].BorderBrush = _borderBrushSelected;
                                    if (InRepair == false)
                                    {
                                        _item.Hole[i].Use = true;
                                    }
                                }
                                if (!Global.TaskName.Equals(string.Empty))
                                {
                                    tbTaskList[i].Text = Global.TaskName;
                                    tbTaskList[i].DataContext = Global.TaskCode;
                                    Global.TaskName = Global.TaskCode = string.Empty;
                                }
                                if (Global.Other.Length > 0)
                                {
                                    tbSampleNameList[i].Text = Global.GetTaskSampleName();
                                    Global.Other = string.Empty;
                                }
                                break;
                            }
                        }
                        else if (type.Equals("Company"))
                        {
                            if (tbCompanyList[i] == sender)
                            {
                                if (!_item.Hole[i].Use)
                                {
                                    Global.SelIndex = _SelIndex = i;
                                    borderList[i].BorderBrush = _borderBrushSelected;
                                    if (InRepair == false)
                                    {
                                        _item.Hole[i].Use = true;
                                    }
                                }
                                if (!Global.CompanyName.Equals(string.Empty))
                                {
                                    tbCompanyList[i].Text = Global.CompanyName;
                                    Global.CompanyName = string.Empty;
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
                                    if (InRepair == false)
                                    {
                                        _item.Hole[i].Use = true;
                                    }
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
                                    if (InRepair == false)
                                    {
                                        _item.Hole[i].Use = true;
                                    }
                                }
                                break;
                            }
                        }
                        else if (type.Equals("LabelHole"))//通道号
                        {
                            if (holelist[i] == sender)
                            {
                                holelist[i].Content = " 检测通道 " + Global.RepairHole;

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
                                    if (InRepair == false)
                                    {
                                        _item.Hole[i].Use = true;
                                    }
                                }
                                if (Wisdom.GETSAMPLECODE.Length > 0)
                                {
                                    tbSampleidList[i].Text = Wisdom.GETSAMPLECODE;
                                    tbCompanyList[i].Text = Global.CompanyName;
                                    if (Global.IsSelectSampleName)
                                    {
                                        DataTable dataTable = _clsTaskOpr.GetSampleByNameOrCode(Global.sampleName, _item.item, true, true, 1);
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
                                                    addWindow.textBoxName.Text = addWindow._projectName = _item.item;
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
                MessageBox.Show(this, "异常(ShowBorder):\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowBorder(sender, e, "border");
        }

        private UIElement oGenerateChlBriefLayout(int channel, string name)
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
                Height = 40,
            };
            Label labelChannel = new Label()
            {
                FontSize = 20,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Content = " 检测通道 " + (channel + 1),
                Name = "LabelHole",
            };
            //labelChannel.MouseDoubleClick +=new MouseButtonEventHandler(labelChannel_MouseDoubleClick);

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
                ToolTip = "双击可查询样品名称",
                IsReadOnly = Global.EachDistrict.Equals("CC") ? true : false
            };
            if (Global.IsSelectSampleName)
            {
                textBoxSampleName.TextChanged += textBoxSampleName_TextChanged;
                textBoxSampleName.KeyDown += textBoxSampleName_KeyDown;
            }
            textBoxSampleName.MouseDoubleClick += textBoxSampleName_MouseDoubleClick;
            textBoxSampleName.PreviewMouseDown += textBoxSampleName_PreviewMouseDown;
            if (Global.EachDistrict.Equals("CC")) textBoxSampleName.IsReadOnly = true;

            //稀释倍数|反应液滴数
            WrapPanel wrapPanelCount = null;
            Label labelCount = null;
            TextBox textBoxDiShuOrBeiShu = null;
            if ((_item.detect_method == "标准曲线法") || (_item.detect_method == "系数法"))
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
                    Content = (_item.detect_method == "标准曲线法") ? "稀释倍数" : "反应液滴数",
                    FlowDirection = System.Windows.FlowDirection.RightToLeft,
                    VerticalContentAlignment = VerticalAlignment.Center
                };
                textBoxDiShuOrBeiShu = new TextBox()
                {
                    Width = 95,
                    Height = 26,
                    Margin = new Thickness(0, 2, 0, 0),
                    FontSize = 15,
                    Text = (_item.detect_method == "标准曲线法") ? _item.sc.CCA.ToString() : string.Empty,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Name = "DiShuOrBeiShu"
                };
                textBoxDiShuOrBeiShu.TextChanged += textBoxDiShuOrBeiShu_TextChanged;
                textBoxDiShuOrBeiShu.PreviewMouseDown += textBoxDiShuOrBeiShu_PreviewMouseDown;
            }

            //任务主题
            WrapPanel wrapPannelTask = null;
            Label labelTaskName = null;
            TextBox textBoxTaskName = null;
            if (Global.InterfaceType.Equals("DY"))
            {
                wrapPannelTask = new WrapPanel()
                {
                    Width = 205,
                    Height = 30
                };

                labelTaskName = new Label()
                {
                    Width = 93,
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
                    Text = _item.Hole[0].TaskName,
                    DataContext = _item.Hole[0].TaskCode,
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
                Content = "*",
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
                Text = _item.Hole[0].CompanyName,
                VerticalContentAlignment = VerticalAlignment.Center,
                Name = "Company",
                ToolTip = "双击可查询被检单位",
                IsReadOnly = Global.EachDistrict.Equals("CC") ? true : false
            };
            textBoxCompany.MouseDoubleClick += textBoxCompany_MouseDoubleClick;
            textBoxCompany.PreviewMouseDown += textBoxCompany_PreviewMouseDown;

            //经营户
            WrapPanel wrapOperator = new WrapPanel()
            {
                Width = 205,
                Height = 30
            };

            Label labelX_Operator = new Label()
            {
                Margin = new Thickness(0, 0, 0, 0),
                Width = 18,
                FontSize = 15,
                Content = Global.InterfaceType.Equals("GS") || Global.InterfaceType.Equals("KJ") ? " " : "",
                Foreground = new SolidColorBrush(Colors.Red),
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            wrapOperator.Children.Add(labelX_Operator);

            Label labelOperator = new Label()
            {
                Width = 75,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "经营户",
                FlowDirection = System.Windows.FlowDirection.RightToLeft,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            wrapOperator.Children.Add(labelOperator);
            TextBox textBoxOperator = new TextBox()
            {
                Width = 95,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Text = _item.Hole[channel].Operator,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                Name = "Operator",
                ToolTip = "双击可查询经营户",
                IsReadOnly = true,
            };
            textBoxOperator.MouseDoubleClick += textBoxOperator_MouseDoubleClick;
            textBoxOperator.PreviewMouseDown += textBoxOperator_PreviewMouseDown;
            wrapOperator.Children.Add(textBoxOperator);

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
                Text = _item.Hole[0].ProduceCompany,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                Name = "ProduceCompany"
            };
            textBoxProduceCompany.PreviewMouseDown += textBoxProduceCompany_PreviewMouseDown;

            //快检单号
            WrapPanel wrapSampleid = null;
          
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
                    Text = _item.Hole[0].SampleId,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Name = "Sampleid",
                    ToolTip = "双击可查询所有快检单号",
                    IsReadOnly = true
                };
                tbSampleid.MouseDoubleClick += tbSampleid_MouseDoubleClick;
                tbSampleid.PreviewMouseDown += tbSampleid_PreviewMouseDown;
                wrapSampleid.Children.Add(tbSampleid);
                wrapSampleid.Visibility = Visibility.Collapsed;

            grid.Children.Add(labelChannel);
            wrapPannelDetectPeople.Children.Add(labelDetectPeople);
            wrapPannelDetectPeople.Children.Add(textBoxDetectPeople);
            wrapPannelSampleName.Children.Add(labelSampleName);
            wrapPannelSampleName.Children.Add(textBoxSampleName);
            if ((_item.detect_method == "标准曲线法") || (_item.detect_method == "系数法"))
            {
                wrapPanelCount.Children.Add(labelCount);
                wrapPanelCount.Children.Add(textBoxDiShuOrBeiShu);
            }
            if (Global.InterfaceType.Equals("DY"))
            {
                wrapPannelTask.Children.Add(labelTaskName);
                wrapPannelTask.Children.Add(textBoxTaskName);
            }
            wrapPannelCompany.Children.Add(labelCompany);
            wrapPannelCompany.Children.Add(textBoxCompany);
            wrapPannelProduceCompany.Children.Add(labelProduceCompany);
            wrapPannelProduceCompany.Children.Add(textBoxProduceCompany);

            stackPanel.Children.Add(grid);
            stackPanel.Children.Add(wrapPannelDetectPeople);
            stackPanel.Children.Add(wrapPannelSampleName);
            if ((_item.detect_method == "标准曲线法") || (_item.detect_method == "系数法")) stackPanel.Children.Add(wrapPanelCount);
            //if (Global.InterfaceType.Equals("DY")) stackPanel.Children.Add(wrapPannelTask);
            if (Global.ManuTest == false)
            {
                //if (Global.InterfaceType.Equals("DY")) 
                stackPanel.Children.Add(wrapPannelTask);
            }
            stackPanel.Children.Add(wrapPannelCompany);

            stackPanel.Children.Add(wrapOperator);

            if (EachDistrict) stackPanel.Children.Add(wrapPannelProduceCompany);
            stackPanel.Children.Add(wrapSampleid);

            border.Child = stackPanel;
            return border;
        }
        private void textBoxOperator_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowBorder(sender, e, "Operator");
        }

        private void textBoxOperator_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string marketname = "";
            List<Border> borderList = UIUtils.GetChildObjects<Border>(WrapPanelChannel, "border");
            List<TextBox> tbCompanyList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Company");
            List<TextBox> OpertorList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Operator");
            if (borderList == null)
                return;
            for (int i = 0; i < borderList.Count; ++i)
            {
                if (sender == OpertorList[i])
                {
                    marketname = tbCompanyList[i].Text.Trim();
                }
            }

            SearchOperatorWindow window = new SearchOperatorWindow();
            window.marketname = marketname;
            window.ShowDialog();
            ShowBorder(sender, e, "Operator");
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
                Content = " 检测通道 " + (channel + 1),
                Name="LabelHole",
            };
            labelChannel.MouseDoubleClick += new MouseButtonEventHandler(labelChannel_MouseDoubleClick);

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
                Text ="",//Global.Testsample[channel,0].ToString(), //name.Equals(string.Empty) ? _item.Hole[channel].SampleName : name,
                VerticalContentAlignment = VerticalAlignment.Center,
                Name = "SampleName",
                Tag = channel,
                ToolTip = "双击可查询样品名称",
                IsReadOnly = Global.EachDistrict.Equals("CC") ? true : false
            };
            if (channel < testhole)
            {
                if (isduizhao == true && channel > 0)
                {
                    textBoxSampleName.Text = Global.Testsample[channel - 1, 0].ToString();
                }
                else if (isduizhao == true && channel == 0)
                {
                    textBoxSampleName.Text = "对照样";
                }
                else
                {
                    textBoxSampleName.Text = Global.Testsample[channel, 0].ToString();
                }
            }
            if (Global.IsSelectSampleName)
            {
                textBoxSampleName.TextChanged += textBoxSampleName_TextChanged;
                textBoxSampleName.KeyDown += textBoxSampleName_KeyDown;
            }
            textBoxSampleName.MouseDoubleClick += textBoxSampleName_MouseDoubleClick;
            textBoxSampleName.PreviewMouseDown += textBoxSampleName_PreviewMouseDown;
            if (Global.EachDistrict.Equals("CC")) textBoxSampleName.IsReadOnly = true;

            //稀释倍数|反应液滴数
            WrapPanel wrapPanelCount = null;
            Label labelCount = null;
            TextBox textBoxDiShuOrBeiShu = null;
            if ((_item.detect_method == "标准曲线法") || (_item.detect_method == "系数法"))
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
                    Content = (_item.detect_method  == "标准曲线法") ? "稀释倍数:" : "反应液滴数",
                    FlowDirection = System.Windows.FlowDirection.RightToLeft,
                    VerticalContentAlignment = VerticalAlignment.Center
                };
                textBoxDiShuOrBeiShu = new TextBox()
                {
                    Width = 95,
                    Height = 26,
                    Margin = new Thickness(0, 2, 0, 0),
                    FontSize = 15,
                    Text = (_item.detect_method == "标准曲线法") ? _item.sc.CCA.ToString() : string.Empty,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Name = "DiShuOrBeiShu"
                };
                textBoxDiShuOrBeiShu.TextChanged += textBoxDiShuOrBeiShu_TextChanged;
                textBoxDiShuOrBeiShu.PreviewMouseDown += textBoxDiShuOrBeiShu_PreviewMouseDown;
            }

            //任务主题
            WrapPanel wrapPannelTask = null;
            Label labelTaskName = null;
            TextBox textBoxTaskName = null;
            if (Global.InterfaceType.Equals("DY"))
            {
                wrapPannelTask = new WrapPanel()
                {
                    Width = 205,
                    Height = 30,
                    Visibility = Global.IsSweepCode?Visibility.Collapsed :Visibility.Visible ,
                };

                labelTaskName = new Label()
                {
                    Width = 93,
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
                    Text = _item.Hole[0].TaskName,
                    DataContext = _item.Hole[0].TaskCode,
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
                Height = 30,
                Visibility = Global.IsSweepCode ? Visibility.Collapsed : Visibility.Visible,
            };
            Label labelX_Company = new Label()
            {
                Margin = new Thickness(0, 0, 0, 0),
                Width = 18,
                FontSize = 15,
                Content = Global.EachDistrict.Equals("GS") || Global.EachDistrict.Equals("CC") ? "*" : "",
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
                Text = _item.Hole[0].CompanyName,
                VerticalContentAlignment = VerticalAlignment.Center,
                Name = "Company",
                ToolTip = "双击可查询被检单位",
                IsReadOnly = Global.EachDistrict.Equals("CC") ? true : false
            };
            textBoxCompany.MouseDoubleClick += textBoxCompany_MouseDoubleClick;
            textBoxCompany.PreviewMouseDown += textBoxCompany_PreviewMouseDown;
            if (channel < testhole)
            {
                //if (isduizhao == true && channel > 0)
                //{
                //    textBoxCompany.Text = Global.TestCompany[channel - 1].ToString();
                //}
                //else if (isduizhao == true && channel == 0)
                //{
                //    textBoxCompany.Text = _item.Hole[0].CompanyName;
                //}
                //else
                //{
                //    textBoxCompany.Text = Global.TestCompany[channel].ToString();
                //}
            }

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
                Text = _item.Hole[0].ProduceCompany,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                Name = "ProduceCompany"
            };
            textBoxProduceCompany.PreviewMouseDown += textBoxProduceCompany_PreviewMouseDown;

            //快检单号
            WrapPanel wrapSampleid = null;
            if (InterfaceType)
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
                    Text = _item.Hole[0].SampleId,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Name = "Sampleid",
                    ToolTip = "双击可查询所有快检单号",
                    IsReadOnly = true
                };
                tbSampleid.MouseDoubleClick += tbSampleid_MouseDoubleClick;
                tbSampleid.PreviewMouseDown += tbSampleid_PreviewMouseDown;
                wrapSampleid.Children.Add(tbSampleid);
            }

            WrapPanel wrapPanelBarCodeA = new WrapPanel()
            {
                Width = 205,
                Height = 30,
            };
            Label labelX_BarCodeA = new Label()
            {
                Margin = new Thickness(0, 0, 0, 0),
                Width = 18,
                FontSize = 15,
                Content = "*",
                Foreground = new SolidColorBrush(Colors.Red),
                FlowDirection = System.Windows.FlowDirection.RightToLeft,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            wrapPanelBarCodeA.Children.Add(labelX_BarCodeA);

            Label labelBarCodeA = new Label()
            {
                Width = 75,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "输入条码",
                FlowDirection = System.Windows.FlowDirection.RightToLeft,
                VerticalContentAlignment = VerticalAlignment.Center
            };
            wrapPanelBarCodeA.Children.Add(labelBarCodeA);

            TextBox tbBarCodeA = new TextBox()
            {
                Width = 95,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Text = _item.Hole[0].SampleId,
                VerticalContentAlignment = VerticalAlignment.Center,
                Name = "BarCodeA",
                ToolTip = "输入条码",
                IsReadOnly = true
            };
            wrapPanelBarCodeA.Children.Add(tbBarCodeA);


            WrapPanel wrapPanelBarCodeB = new WrapPanel()
            {
                Width = 205,
                Height = 30,
            };
            Label labelX_BarCodeB= new Label()
            {
                Margin = new Thickness(0, 0, 0, 0),
                Width = 18,
                FontSize = 15,
                Content = "*",
                Foreground = new SolidColorBrush(Colors.Red),
                FlowDirection = System.Windows.FlowDirection.RightToLeft,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center
            };
            wrapPanelBarCodeB.Children.Add(labelX_BarCodeB);

            Label labelBarCodeB = new Label()
            {
                Width = 75,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Content = "输出条码",
                FlowDirection = System.Windows.FlowDirection.RightToLeft,
                VerticalContentAlignment = VerticalAlignment.Center
            };
            wrapPanelBarCodeB.Children.Add(labelBarCodeB);

            TextBox tbBarCodeB = new TextBox()
            {
                Width = 95,
                Height = 26,
                Margin = new Thickness(0, 2, 0, 0),
                FontSize = 15,
                Text = _item.Hole[0].SampleId,
                VerticalContentAlignment = VerticalAlignment.Center,
                Name = "BarCodeB",
                ToolTip = "输出条码",
                IsReadOnly = true,
            };
            wrapPanelBarCodeB.Children.Add(tbBarCodeB);
            //加载
            if (Global.IsSweepCode)
            {
                if (channel < testhole)
                {
                    if (isduizhao == true && channel > 0)
                    {
                        tbBarCodeA.Text = Global.Testsample[channel - 1, 2].ToString();
                        tbBarCodeB.Text = Global.Testsample[channel - 1, 3].ToString();
                        //textBoxSampleName.Text = Global.Testsample[channel - 1, 0].ToString();
                    }
                    else if (isduizhao == true && channel == 0)
                    {
                        //textBoxSampleName.Text = "对照样";
                    }
                    else
                    {
                        tbBarCodeA.Text = Global.Testsample[channel, 2].ToString();
                        tbBarCodeB.Text = Global.Testsample[channel, 3].ToString();
                        //textBoxSampleName.Text = Global.Testsample[channel, 0].ToString();
                    }
                }
            }
            grid.Children.Add(labelChannel);
            wrapPannelDetectPeople.Children.Add(labelDetectPeople);
            wrapPannelDetectPeople.Children.Add(textBoxDetectPeople);
            wrapPannelSampleName.Children.Add(labelSampleName);
            wrapPannelSampleName.Children.Add(textBoxSampleName);
            if ((_item.detect_method  == "标准曲线法") || (_item.detect_method =="系数法"))
            {
                wrapPanelCount.Children.Add(labelCount);
                wrapPanelCount.Children.Add(textBoxDiShuOrBeiShu);
            }
            if (Global.InterfaceType.Equals("DY"))
            {
                wrapPannelTask.Children.Add(labelTaskName);
                wrapPannelTask.Children.Add(textBoxTaskName);
            }
            wrapPannelCompany.Children.Add(labelCompany);
            wrapPannelCompany.Children.Add(textBoxCompany);
            wrapPannelProduceCompany.Children.Add(labelProduceCompany);
            wrapPannelProduceCompany.Children.Add(textBoxProduceCompany);

            stackPanel.Children.Add(grid);
            stackPanel.Children.Add(wrapPannelDetectPeople);
            stackPanel.Children.Add(wrapPannelSampleName);

            if (Global.IsSweepCode)
            {
                stackPanel.Children.Add(wrapPanelBarCodeA);
                stackPanel.Children.Add(wrapPanelBarCodeB);
            }

            if ((_item.detect_method == "标准曲线法") || (_item.detect_method =="系数法")) stackPanel.Children.Add(wrapPanelCount);
            if (Global.InterfaceType.Equals("DY")) stackPanel.Children.Add(wrapPannelTask);
            stackPanel.Children.Add(wrapPannelCompany);
            if (EachDistrict) stackPanel.Children.Add(wrapPannelProduceCompany);
            if (InterfaceType) stackPanel.Children.Add(wrapSampleid);

            border.Child = stackPanel;
            return border;
        }
        /// <summary>
        /// 修改通道号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labelChannel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            InRepair = false;
            Global.RepairHole = "";
            string d= ((Label)sender).Content.ToString().Trim();
            int s = int.Parse(d.Substring(4, d.Length - 4));

            RepairHole window = new RepairHole();
            window._MoKuai = "分光";
            window.HoleFullName = d;
            window.ShowDialog();
            ShowBorder(sender, e, "LabelHole");
            if (Global.RepairHole != "")
            {
                InRepair = true ;
                int n = int.Parse(Global.RepairHole);
                exchangehole(s-1, n-1);
            }
        }
        /// <summary>
        /// 对换通道的数据
        /// </summary>
        private void exchangehole(int oldhole,int newhole)
        {
            try
            {
                List<Border> borderList = UIUtils.GetChildObjects<Border>(WrapPanelChannel, "border");
                List<TextBox> tbSampleNameList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "SampleName");
                List<TextBox> tbTaskList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "TaskName");
                List<TextBox> tbCompanyList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Company");
                List<TextBox> tbSampleidList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "Sampleid");
                List<TextBox> tbProduceCompanyList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "ProduceCompany");
                List<TextBox> tbDiShuOrBeiShuList = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "DiShuOrBeiShu");
                List<Label> holelist = UIUtils.GetChildObjects<Label>(WrapPanelChannel, "LabelHole");
                _item.Hole[newhole].Use = true;
                _item.Hole[oldhole].Use = false;
                _item.Hole[newhole].tID = _item.Hole[oldhole].tID;
                _item.Hole[oldhole].HoleNumber = (newhole + 1).ToString();
                tbSampleNameList[newhole].Text=tbSampleNameList[oldhole].Text;
                tbTaskList[newhole].Text =tbTaskList[oldhole].Text;
                tbCompanyList[newhole].Text =tbCompanyList[oldhole].Text;
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "异常(ShowBorder):\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void tbSampleid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowBorder(sender, e, "Sampleid");
        }

        private void textBoxProduceCompany_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowBorder(sender, e, "ProduceCompany");
        }

        private void textBoxCompany_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowBorder(sender, e, "Company");
        }

        private void textBoxTaskName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowBorder(sender, e, "TaskName");
        }

        private void textBoxDiShuOrBeiShu_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowBorder(sender, e, "DiShuOrBeiShu");
        }

        private void textBoxSampleName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowBorder(sender, e, "SampleName");
        }

        /// <summary>
        /// 双击快检单号文本框先选中当前border
        /// 然后弹出快检单号选择界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                MessageBox.Show(this, "异常(CheckBoxSelAll_Click):\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private List<string> AddStringToList(List<string> list, string content)
        {
            if (null == list)
                list = new List<string>();

            try
            {
                if (!string.Empty.Equals(content.Trim()))
                {
                    if (!list.Contains(content))
                    {
                        list.Add(content);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "异常(AddStringToList):\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return list;
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
                MessageBox.Show(this, "异常(textBoxDiShuOrBeiShu_TextChanged):\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowWaitingWindow()
        {
            WaitingWindow window = new WaitingWindow()
            {
                ShowInTaskbar = false,
                Owner = this
            };
            window.Show();
        }

        private string sampleCode = string.Empty;
        private void textBoxSampleName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tbs = sender as TextBox;
            try
            {
                string[] sampleCodes = tbs.Text.Trim().Split('：');
                if (sampleCodes != null && sampleCodes.Length == 1) sampleCodes = tbs.Text.Trim().Split(':');
                if (sampleCodes != null && sampleCodes.Length >= 3)
                {
                    int index = sampleCodes.Length;
                    if (sampleCodes[index - 2].Equals("ID"))
                    {
                        string[] strs = sampleCodes[index - 1].Split('.');
                        if (strs != null && strs.Length == 2)
                        {
                            try
                            {
                                ShowWaitingWindow();
                                tbs.Text = strs[0];
                                sampleCode = tbs.Text;

                                List<clsTB_SAMPLING> models = null;
                                if (sampleCode.Length > 0)
                                {
                                    models = Global.Checkcar.GetSampling(sampleCode);
                                    if (models != null && models.Count > 0)
                                    {
                                        Global.sampleName = tbs.Text = models[0].SAMPLENAME;
                                        Global.CompanyName = models[0].CKONAME;
                                        ShowBorder(sender, null, "SampleName");
                                        return;
                                    }
                                }
                                sampleCode = Global.LoadSamplingData(Global.samplenameadapter[0].user,
                                        Global.samplenameadapter[0].pwd, sampleCode, Global.samplenameadapter[0].url);
                                if (sampleCode.Length > 0)
                                {
                                    DataSet dataSet = new DataSet();
                                    DataTable dataTable = new DataTable();
                                    using (StringReader sr = new StringReader(sampleCode))
                                    {
                                        dataSet.ReadXml(sr);
                                    }
                                    dataTable = dataSet.Tables["SamplingClass"];
                                    if (dataTable != null && dataTable.Rows.Count > 0)
                                    {
                                        Global.sampleName = tbs.Text = dataTable.Rows[0]["SAMPLENAME"].ToString();
                                        Global.CompanyName = dataTable.Rows[0]["CKONAME"].ToString();
                                        ShowBorder(sender, null, "SampleName");
                                        int rtn = Global.Checkcar.Insert(dataTable);
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                Global.WaitingWindowIsClose = true;
                            }
                            finally { Global.WaitingWindowIsClose = true; }
                        }
                    }
                }
            }
            catch (Exception)
            {
                tbs.Text = string.Empty;
            }
        }

        private void textBoxSampleName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                try
                {
                    ShowWaitingWindow();

                    TextBox tbs = sender as TextBox;
                    sampleCode = tbs.Text.Trim();

                    List<clsTB_SAMPLING> models = null;
                    if (sampleCode.Length > 0)
                    {
                        models = Global.Checkcar.GetSampling(sampleCode);
                        if (models != null && models.Count > 0)
                        {
                            Global.sampleName = tbs.Text = models[0].SAMPLENAME;
                            Global.CompanyName = models[0].CKONAME;
                            ShowBorder(sender, null, "SampleName");
                            return;
                        }
                    }
                    sampleCode = Global.LoadSamplingData(Global.samplenameadapter[0].user,
                            Global.samplenameadapter[0].pwd, sampleCode, Global.samplenameadapter[0].url);
                    if (sampleCode.Length > 0)
                    {
                        DataSet dataSet = new DataSet();
                        DataTable dataTable = new DataTable();
                        using (StringReader sr = new StringReader(sampleCode))
                        {
                            dataSet.ReadXml(sr);
                        }
                        dataTable = dataSet.Tables["SamplingClass"];
                        if (dataTable != null && dataTable.Rows.Count > 0)
                        {
                            Global.sampleName = tbs.Text = dataTable.Rows[0]["SAMPLENAME"].ToString();
                            Global.CompanyName = dataTable.Rows[0]["CKONAME"].ToString();
                            ShowBorder(sender, null, "SampleName");
                            int rtn = Global.Checkcar.Insert(dataTable);
                        }
                    }
                    ShowBorder(sender, null, "SampleName");
                }
                catch (Exception)
                {
                    Global.WaitingWindowIsClose = true;
                }
                finally { Global.WaitingWindowIsClose = true; }
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
            ShowSample();
            //先选中当前border
            ShowBorder(sender, e, "SampleName");
        }

        /// <summary>
        /// 样品小精灵
        /// </summary>
        private void ShowSample()
        {
            Global.IsProject = false;
            SearchSample searchSample = new SearchSample()
            {
                ShowInTaskbar = false,
                Owner = this,
                //_projectName = this._item.item 
            };
            if (Global.ManuTest == true)
            {
                searchSample._projectName = this._item.item;
            }
            else
            {
                searchSample._projectName = this._item.item;
            }
            
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
            SearchTaskWindow searchTask = new SearchTaskWindow
            {
                ShowInTaskbar = false,
                Owner = this
            };
            searchTask.ShowDialog();
            //先选中当前border
            ShowBorder(sender, e, "TaskName");
        }

        private void textBoxCompany_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SearchCompanyWindow windowCompany = new SearchCompanyWindow
            {
                ShowInTaskbar = false,
                Owner = this
            };
            windowCompany.ShowDialog();
            ShowBorder(sender, e, "Company");
        }

        private void cb_SelAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool isChecked = (bool)cb_SelAll.IsChecked;
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
                MessageBox.Show(this, "异常(CheckBoxSelAll_Click):\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
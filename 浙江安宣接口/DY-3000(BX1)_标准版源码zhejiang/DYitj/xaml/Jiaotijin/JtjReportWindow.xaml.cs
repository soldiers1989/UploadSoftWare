using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using AIO.src;
using com.lvrenyang;
using DYSeriesDataSet;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.Charts;
using Microsoft.Research.DynamicDataDisplay.Charts.Axes;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Microsoft.Research.DynamicDataDisplay.PointMarkers;

namespace AIO
{
    /// <summary>
    /// JtjReportWindow.xaml 的交互逻辑
    /// </summary>
    public partial class JtjReportWindow : Window
    {

        #region 全局变量
        public List<byte[]> _datas;
        private static Double[] _tValue;
        private static List<Double[]> _tValues;
        public List<byte[]> _listGrayValues;
        public DYJTJItemPara _item = null;
        public JtjMeasureWindow.HelpBox[] _helpBoxes = null;
        public List<String> _listDetectResult = null;
        public List<String> _listStrRecord = null;
        public List<TextBox> _RecordValue = null;
        private String[] _methodToString = { "定性消线法", "定性比色法", "定量法(T)", "定量法(T/C)" };
        private Brush _borderBrushNormal = new SolidColorBrush(Color.FromRgb(0x00, 0x7C, 0xC2));
        private DateTime _date = DateTime.Now;
        private MsgThread _msgThread;
        private List<TextBox> _listJudmentValue = null;
        private Int32 _HoleNumber = 1;
        private String[,] _CheckValue;
        private Int32 _AllNumber = 0;
        private List<ChartPlotter> _plotters = null;
        private List<HorizontalDateTimeAxis> _dateAxis = null;
        private List<double> _ValueC = new List<double>(), _ValueT = new List<double>(), _Peak = new List<double>();
        private List<TextBox> _listCheckResult = null;
        private List<TextBox> _listResult = null;
        private String _textBoxDetectResult = "textBoxDetectResult";
        private String _textJugmentResult = "textJugmentResult";
        private DispatcherTimer _DataTimer = null;
        //private double[] oldData;
        private tlsttResultSecondOpr _resultTable = new tlsttResultSecondOpr();
        private bool IsUpLoad = false;
        private List<int> _idList = null;
        private string logType = "JtjReportWindow-error";
        #endregion

        public JtjReportWindow()
        {
            InitializeComponent();
            _msgThread = new MsgThread(this);
            _msgThread.Start();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Global.IsShowValue)
                {
                    LabelInfo_Copy.Visibility = Visibility.Visible;
                }
                if (null == _item)
                    return;

                _listDetectResult = new List<String>();
                _listStrRecord = new List<String>();
                _listJudmentValue = new List<TextBox>();
                _listCheckResult = new List<TextBox>();
                _listResult = new List<TextBox>();
                _plotters = new List<ChartPlotter>();
                _dateAxis = new List<HorizontalDateTimeAxis>();
                int sampleNum = _item.SampleNum;
                for (int i = 0; i < Global.deviceHole.SxtCount; ++i)
                {
                    if (_item.Hole[i].Use)
                    {
                        UIElement element = GenerateResultLayout(i, String.Format("{0:D5}", sampleNum), _item.Hole[i].SampleName);
                        WrapPanelChannel.Children.Add(element);
                        _plotters.Add(UIUtils.GetChildObject<ChartPlotter>(element, "chartPlotter"));
                        _dateAxis.Add(UIUtils.GetChildObject<HorizontalDateTimeAxis>(element, "dateAxis"));
                        _listCheckResult.Add(UIUtils.GetChildObject<TextBox>(element, _textBoxDetectResult));
                        _listResult.Add(UIUtils.GetChildObject<TextBox>(element, _textJugmentResult));
                        WrapPanelChannel.Width += 355;
                        sampleNum++;
                        _CheckValue = new String[_HoleNumber, 22];
                        _HoleNumber++;
                    }
                    else
                    {
                        _listCheckResult.Add(null);
                        _listResult.Add(null);
                        _plotters.Add(new ChartPlotter());
                        _dateAxis.Add(new HorizontalDateTimeAxis());
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(2, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }

            ShowResult();

            if (_DataTimer == null)
            {
                _DataTimer = new DispatcherTimer();
                _DataTimer.Interval = TimeSpan.FromSeconds(0.5);
                _DataTimer.Tick += new EventHandler(SaveAndUpload);
                _DataTimer.Start();
            }
            _msgThread = new MsgThread(this);
            _msgThread.Start();
        }

        /// <summary>
        /// 延迟一秒保存和上传数据
        /// 非强制上传权限的用户给予提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAndUpload(object sender, EventArgs e)
        {
            Save();
            if (LoginWindow._userAccount.UpDateNowing)
                Upload();
            else Global.IsStartUploadTimer = true;
            ButtonPrint.IsEnabled = true;
            ButtonUpdate.IsEnabled = true;
            ButtonPrev.IsEnabled = true;
            Btn_ShowDatas.IsEnabled = true;
            _DataTimer.Stop();
            _DataTimer = null;
        }

        private string msgStr = string.Empty;
        /// <summary>
        /// 计算结果
        /// </summary>
        private void CalcResult()
        {
            int num = 0, index = 0;
            int sampleNum = _item.SampleNum;
            try
            {
                for (int i = 0; i < Global.deviceHole.SxtCount; ++i)
                {
                    if (_item.Hole[i].Use)
                    {
                        string[] UnqualifiedValue = new string[4];
                        UnqualifiedValue = TestResultConserve.UnqualifiedOrQualified("0", _item.Hole[i].SampleName, _item.Name);
                        Double value = 0, x = 0;
                        if (_item.Method == 2 || _item.Method == 3)
                        {
                            //a+bx+cxx+dxxx
                            if (_item.Method == 2)//x=T
                            {
                                x = _ValueT[index];
                                value = _item.dlt.A0 + _item.dlt.A1 * x + _item.dlt.A2 * x * x
                                   + _item.dlt.A3 * x * x * x;
                                if (_item.dlt.B0 > 0)
                                {
                                    value = value * _item.dlt.B0;
                                }
                            }
                            else if (_item.Method == 3)//x=T/C
                            {
                                x = _ValueT[index] / _ValueC[index];
                                value = _item.dlt.A0 + _item.dlt.A1 * x + _item.dlt.A2 * x * x
                                   + _item.dlt.A3 * x * x * x;
                                if (_item.dltc.B0 > 0)
                                {
                                    value = value * _item.dltc.B0;
                                }
                            }

                        }
                        //定性消线
                        if (_item.Method == 0)
                        {
                            LabelInfo_Copy.Content += string.Format("C{0}:{1}   T{0}:{2}   CTAbs:{3}   AbsX:{4}", (index + 1), _ValueC[index].ToString("F4"), _ValueT[index].ToString("F4"),
                                System.Math.Abs(_ValueC[index] - _ValueT[index]).ToString("F4"), _item.dxbs.Abs);
                            if (_ValueC[index] <= _item.InvalidC)
                            {
                                _listCheckResult[i].Text = "检测无效";
                                _listResult[i].Text = "检测无效";
                                UnqualifiedValue[0] = "检测无效";
                            }
                            else if (_ValueT[index] < _item.dxxx.PlusT)
                            {
                                _listCheckResult[i].Text = "阳性";
                                _listResult[i].Text = "不合格";
                                //_listResult[i].Text = string.Format("不合格(C:{0} T:{1})", Math.Round(_ValueC[index], 2), Math.Round(_ValueT[index], 2));
                                UnqualifiedValue[0] = "不合格";
                            }
                            else if (_ValueT[index] >= _item.dxxx.MinusT)
                            {
                                _listCheckResult[i].Text = "阴性";
                                _listResult[i].Text = "合格";
                                //_listResult[i].Text = string.Format("合格(C:{0} T:{1})", Math.Round(_ValueC[index], 2), Math.Round(_ValueT[index], 2));
                                UnqualifiedValue[0] = "合格";
                            }
                            //可疑数据有填写的时候
                            else if (_item.dxxx.SuspiciousMin > 0 && _item.dxxx.SuspiciousMax > 0)
                            {
                                if (_ValueT[index] >= _item.dxxx.SuspiciousMin && _ValueT[index] <= _item.dxxx.SuspiciousMax)
                                {
                                    _listCheckResult[i].Text = "可疑";
                                    _listResult[i].Text = "可疑";
                                    //_listResult[i].Text = string.Format("可疑(C:{0} T:{1})", Math.Round(_ValueC[index], 2), Math.Round(_ValueT[index], 2));
                                    UnqualifiedValue[0] = "可疑";
                                }
                            }
                        }
                        //2016年10月13日 wenj 
                        //新版本判定方法：Abs（C-T）≥Abs时 C＞T阳性 C＜T阴性；Abs（C-T）＜Abs时 SexIdx=0阴性 else 阳性
                        else if (_item.Method == 1)
                        {
                            double ctAbs = System.Math.Abs(_ValueC[index] - _ValueT[index]);
                            LabelInfo_Copy.Content += string.Format("C{0}:{1}   T{0}:{2}   CTAbs:{3}   AbsX:{4}", (index + 1), _ValueC[index].ToString("F4"), _ValueT[index].ToString("F4"), ctAbs.ToString("F4"), _item.dxbs.Abs);
                            //综合C值<15则检测无效
                            if (_ValueC[index] > 15)
                            {
                                if (ctAbs >= _item.dxbs.Abs)
                                {
                                    if (_ValueC[index] >= _ValueT[index])
                                    {
                                        _listCheckResult[i].Text = "阳性";
                                        _listResult[i].Text = "不合格";
                                        UnqualifiedValue[0] = "不合格";
                                    }
                                    else
                                    {
                                        _listCheckResult[i].Text = "阴性";
                                        _listResult[i].Text = "合格";
                                        UnqualifiedValue[0] = "合格";
                                    }
                                }
                                else
                                {
                                    if (_item.dxbs.SetIdx == 0)
                                    {
                                        _listCheckResult[i].Text = "阴性";
                                        _listResult[i].Text = "合格";
                                        UnqualifiedValue[0] = "合格";
                                    }
                                    else
                                    {
                                        _listCheckResult[i].Text = "阳性";
                                        _listResult[i].Text = "不合格";
                                        UnqualifiedValue[0] = "不合格";
                                    }
                                }
                            }
                            else
                            {
                                _listCheckResult[i].Text = "检测无效";
                                _listResult[i].Text = "检测无效";
                                UnqualifiedValue[0] = "检测无效";
                            }
                            #region 旧判定方法：定性比色 T>C 阴性
                            //if (_ValueC[index] <= _item.InvalidC)
                            //{
                            //    _listCheckResult[i].Text = "检测无效";
                            //    _listResult[i].Text = "检测无效";
                            //    UnqualifiedValue[0] = "不合格";
                            //}
                            //else if (_ValueT[index] > _ValueC[index])
                            //{
                            //    _listCheckResult[i].Text = "阴性";
                            //    _listResult[i].Text = "合格";
                            //    UnqualifiedValue[0] = "合格";
                            //}
                            //else
                            //{
                            //    _listCheckResult[i].Text = "阳性";
                            //    _listResult[i].Text = "不合格";
                            //    UnqualifiedValue[0] = "不合格";
                            //}
                            #endregion
                        }
                        //定量（T） a+bx+cxx+dxxx  < PPB 阴性 x=T
                        else if (_item.Method == 2)
                        {
                            if (_ValueC[index] <= _item.InvalidC)
                            {
                                _listCheckResult[i].Text = "检测无效";
                                _listResult[i].Text = "检测无效";
                                UnqualifiedValue[0] = "检测无效";
                            }
                            else if (value < _item.dlt.Limit)
                            {
                                _listCheckResult[i].Text = "阴性";
                                _listResult[i].Text = "合格";
                                UnqualifiedValue[0] = "合格";
                            }
                            else
                            {
                                _listCheckResult[i].Text = "阳性";
                                _listResult[i].Text = "不合格";
                                UnqualifiedValue[0] = "不合格";
                            }
                        }
                        //定量（T/C）:a+bx+cxx+dxxx  < PPB 阴性 x=T/C
                        else if (_item.Method == 3)
                        {
                            if (_ValueC[index] <= _item.InvalidC)
                            {
                                _listCheckResult[i].Text = "检测无效";
                                _listResult[i].Text = "检测无效";
                                UnqualifiedValue[0] = "检测无效";
                            }
                            else if (value < _item.dltc.Limit)
                            {
                                _listCheckResult[i].Text = "阴性";
                                _listResult[i].Text = "合格";
                                UnqualifiedValue[0] = "合格";
                            }
                            else
                            {
                                _listCheckResult[i].Text = "阳性";
                                _listResult[i].Text = "不合格";
                                UnqualifiedValue[0] = "不合格";
                            }
                        }

                        int number = num > 0 ? (i - num) : i;
                        _CheckValue[number, 0] = String.Format("{0:D2}", (i + 1));
                        _CheckValue[number, 1] = "胶体金";
                        _CheckValue[number, 2] = _item.Name;
                        _CheckValue[number, 3] = _methodToString[_item.Method];
                        _CheckValue[number, 4] = _listCheckResult[i].Text;
                        _CheckValue[number, 5] = _item.Unit.Length == 0 ? UnqualifiedValue[4] : _item.Unit;
                        _CheckValue[number, 6] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        _CheckValue[number, 7] = LoginWindow._userAccount.UserName;
                        if (!string.IsNullOrEmpty(_item.Hole[i].SampleName))
                            _CheckValue[number, 8] = _item.Hole[i].SampleName;
                        else
                            _CheckValue[number, 8] = string.Empty;

                        _CheckValue[number, 9] = Convert.ToString(UnqualifiedValue[0]);
                        _CheckValue[number, 10] = Convert.ToString(UnqualifiedValue[2]);
                        _CheckValue[number, 11] = String.Format("{0:D5}", sampleNum++);
                        _CheckValue[number, 12] = Convert.ToString(UnqualifiedValue[1]);
                        if (_item.Hole[i].TaskName != null)
                            _CheckValue[number, 13] = _item.Hole[i].TaskName;
                        else
                            _CheckValue[number, 13] = string.Empty;
                        if (!string.IsNullOrEmpty(_item.Hole[i].CompanyName))
                            _CheckValue[number, 14] = _item.Hole[i].CompanyName;
                        else
                            _CheckValue[number, 14] = string.Empty;
                        _CheckValue[number, 15] = curveDatas[i];
                        _CheckValue[(num > 0 ? (i - num) : i), 16] = _item.Hole[i].SampleTypeCode;//样品种类编号
                        _CheckValue[(num > 0 ? (i - num) : i), 17] = _item.testPro;//检测项目编号
                        _CheckValue[(num > 0 ? (i - num) : i), 18] = "1";//检测结果类型 1，定量，2定性 分光光度都为1
                        _CheckValue[(num > 0 ? (i - num) : i), 19] = string.Empty; //检测结果编号 dataNum
                        _CheckValue[(num > 0 ? (i - num) : i), 20] = string.IsNullOrEmpty(_item.Hole[i].SampleId) ? string.Empty : _item.Hole[i].SampleId;
                        _CheckValue[(num > 0 ? (i - num) : i), 21] = _item.Hole[i].ProduceCompany;//生产单位
                        index++;
                    }
                    else
                    {
                        num += 1;
                        index++;
                        _listStrRecord.Add(null);
                        _listDetectResult.Add(null);
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(2, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void ShowResult()
        {
            //格式化数据
            FormattingDatas();
            //绘制曲线并定位C.T值
            DrawingCurve();
            //计算结果
            CalcResult();
        }

        /// <summary>
        /// 存储曲线数据
        /// </summary>
        private List<string> curveDatas = new List<string>();

        /// <summary>
        /// 绘制曲线并定位C.T值
        /// </summary>
        private void DrawingCurve()
        {
            string curveData = string.Empty;
            for (int i = 0; i < _tValues.Count; i++)
            {
                _ValueC.Add(0); _ValueT.Add(0); _Peak.Add(0);
                if (_tValues[i] != null)
                {
                    int CMaxIndex1 = 0, CMaxIndex2 = 0, TMaxIndex1 = 0, TMaxIndex2 = 0;
                    double[] datas = _tValues[i];
                    int length = datas.Length,
                        pointCNum = _item.dxxx.pointCNum <= 0 ? 5 : _item.dxxx.pointCNum,
                        pointTNum = _item.dxxx.pointTNum <= 0 ? 4 : _item.dxxx.pointTNum,
                        //beforeAreaTNum = _item.dxxx.beforeAreaTNum <= 0 ? 4 : _item.dxxx.beforeAreaTNum,
                        //afterAreaTNum = _item.dxxx.afterAreaTNum <= 0 ? 5 : _item.dxxx.afterAreaTNum,
                        CMinIndex = 0, TMinIndex = 0,
                        CCount = 0, TCount = 0;
                    double slope = 0;
                    double difference = 0;
                    double efficientPoint = 0;
                    #region 绘制曲线
                    try
                    {
                        DateTime[] dates = new DateTime[length];
                        int[] numberOpen = new int[length];
                        for (int j = 0; j < length; ++j)
                        {
                            DateTime dt = Convert.ToDateTime("01/01/000" + (j + 1));
                            dates[j] = dt;
                            numberOpen[j] = (int)datas[j];
                            curveData += curveData.Length == 0 ? datas[j].ToString() : string.Format("|{0}", datas[j]);
                        }
                        curveDatas.Add(curveData);

                        var datesDataSource = new EnumerableDataSource<DateTime>(dates);
                        datesDataSource.SetXMapping(x => _dateAxis[i < 2 ? i : 0].ConvertToDouble(x));

                        var numberOpenDataSource = new EnumerableDataSource<int>(numberOpen);
                        numberOpenDataSource.SetYMapping(y => y);

                        CompositeDataSource compositeDataSource1 = new
                        CompositeDataSource(datesDataSource, numberOpenDataSource);

                        _plotters[i < 2 ? i : 0].AddLineGraph(compositeDataSource1,
                                            new Pen(Brushes.Red, 2),
                                            new CirclePointMarker { Size = 2.0, Fill = Brushes.Blue },
                                            null);
                        _plotters[i < 2 ? i : 0].Viewport.FitToView();
                    }
                    catch (Exception ex)
                    {
                        FileUtils.OprLog(2, logType, ex.ToString());
                    }
                    #endregion

                    //先从第1个点到第25点，确定到C值最小点。判断条件如下：
                    //条件1：从第1点开始，用序号大的值减序号小的值小于0（X_(i+1)-X_i<0），出现至少5个点，呈现连续下降趋势，直至（X_(i+1)-X_i>0）得出波谷最小值（X_i）。
                    //条件2：从最小值（X_i）开始，用序号大的值减序号小的值大于0（X_(i+1)-X_i>0），呈现连续上升趋势，直至出现连续5个数满足。
                    //条件3：最小值X_i的坐标要大于8 （X_i> 8），小于20范围内（X_i< 20）

                    for (int j = 8; j < length; j++)
                    {
                        #region 求CMinIndex
                        if (CMinIndex == 0)
                        {
                            //当前点的左右都为上升趋势，相邻两点可以相等，但不允许相邻三个点相等，且范围在8~20之间
                            try
                            {
                                if ((datas[j - 1] >= datas[j] && datas[j] <= datas[j + 1]) && j >= 6 && j <= 20 &&
                                    (datas[j - 1] < datas[j - 2] && datas[j + 1] < datas[j + 2]))
                                {
                                    CCount++;
                                    //以当前坐标为原点，向左右扩散验证上升沿
                                    int cidx = 2;
                                    for (int k = j; k < length; k++)
                                    {
                                        try
                                        {
                                            if (datas[j - cidx] > datas[j] && datas[j] < datas[j + cidx])
                                            {
                                                CCount++;
                                                //连续pointCNum个连续上升沿，确定C波谷坐标
                                                if (CCount >= pointCNum)
                                                {
                                                    _ValueC[i] = CMinIndex = j;
                                                    //往前遍历取TMaxIndex1 最大6
                                                    for (int c1 = j - CCount; c1 > 0; c1--)
                                                    {
                                                        if (datas[c1 - 1] <= datas[c1] || j - c1 >= 6)
                                                        {
                                                            CMaxIndex1 = c1;
                                                            break;
                                                        }
                                                    }

                                                    //往后遍历取TMaxIndex2 最大8
                                                    for (int c2 = j + TCount; c2 < length; c2++)
                                                    {
                                                        if (datas[c2] >= datas[c2 + 1] || c2 - j >= 8)
                                                        {
                                                            CMaxIndex2 = c2;
                                                            break;
                                                        }
                                                    }

                                                    j = 20;
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                CCount = 0;
                                                break;
                                            }
                                            cidx++;
                                        }
                                        catch (Exception ex)
                                        {
                                            FileUtils.OprLog(2, logType, ex.ToString());
                                            CCount = 0;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    CCount = 0;
                                }
                            }
                            catch (Exception ex)
                            {
                                FileUtils.OprLog(2, logType, ex.ToString());
                                continue;
                            }
                        }
                        #endregion

                        if (j < 20) continue;

                        #region 求TMinIndex
                        if (CMinIndex > 0 && TMinIndex == 0)
                        {
                            try
                            {
                                //当前点的左右都为上升趋势，相邻两点可以相等，但不允许相邻三个点相等，且T坐标要落在 C坐标 + （20±5）这个区间
                                if ((datas[j - 1] >= datas[j] && datas[j] <= datas[j + 1]) &&
                                    (datas[j - 1] < datas[j - 2] && datas[j + 1] < datas[j + 2]) &&
                                    j >= (CMinIndex + 15) && j <= (CMinIndex + 25))
                                {
                                    TCount++;
                                    //以当前坐标为原点，向左右扩散验证上升沿
                                    int tidx = 2;
                                    for (int k = j; k < length; k++)
                                    {
                                        try
                                        {
                                            //满足以T波谷为原点的左右上升沿
                                            if (datas[j - tidx] > datas[j] && datas[j] < datas[j + tidx])
                                            {
                                                TCount++;
                                                //连续pointTNum个连续上升沿，确定T波谷坐标，同时确定左右两边同时满足上升沿的最大波峰
                                                if (TCount >= pointTNum)
                                                {
                                                    TMinIndex = j;
                                                    //往前遍历取TMaxIndex1 最大6
                                                    for (int t1 = j - TCount; t1 > 0; t1--)
                                                    {
                                                        if (datas[t1 - 1] <= datas[t1] || j - t1 >= 6)
                                                        {
                                                            TMaxIndex1 = t1;
                                                            break;
                                                        }
                                                    }
                                                    //往后遍历取TMaxIndex2 最大5
                                                    for (int t2 = j + TCount; t2 < length; t2++)
                                                    {
                                                        if (datas[t2] >= datas[t2 + 1] || t2 - j >= 5)
                                                        {
                                                            TMaxIndex2 = t2;
                                                            break;
                                                        }
                                                    }
                                                    break;
                                                    #region
                                                    ////连续5个点上升沿(往后的上升沿最大取5)
                                                    //if (datas[j - tidx - 1] >= datas[j - tidx] && datas[j + tidx + 1] >= datas[j + tidx])
                                                    //{
                                                    //    TMinIndex = j;
                                                    //    TMaxIndex1 = j - tidx - 1;
                                                    //    TMaxIndex2 = j + tidx + 1;
                                                    //    break;
                                                    //}
                                                    ////连续4个点上升沿
                                                    //else
                                                    //{
                                                    //    TMinIndex = j;
                                                    //    TMaxIndex1 = j - tidx;
                                                    //    TMaxIndex2 = j + tidx;
                                                    //    break;
                                                    //}
                                                    #endregion
                                                }
                                                tidx++;
                                            }
                                            else
                                            {
                                                tidx = 2;
                                                TCount = 0;
                                                break;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            FileUtils.OprLog(2, logType, ex.ToString());
                                            TCount = 0;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    TCount = 0;
                                }
                            }
                            catch (Exception ex)
                            {
                                FileUtils.OprLog(2, logType, ex.ToString());
                                continue;
                            }
                        }
                        else
                        {
                            break;
                        }
                        #endregion
                    }

                    #region C值面积
                    //面积6个点(3~6)，斜率8个点(3~8)，有效点8个(连续上升/下降)，差值（波峰(C往左边推取最大值)-波谷）
                    if (CMinIndex > 0)
                    {
                        //比例，面积和差值各40 斜率和有效点各10
                        //scope为有效范围，坐标0为最小范围，坐标1为最大范围
                        int[] scope = new int[2];
                        scope[0] = 3; scope[1] = 8;
                        double standard = 0, proportion = 0;

                        //原始曲线面积
                        standard = 5000;//面积标准为5000
                        proportion = 0.4;//面积权重40%
                        int cmax1 = CMinIndex - CMaxIndex1 > 6 ? CMinIndex - 6 : CMaxIndex1;
                        int cmax2 = CMaxIndex2 - CMinIndex > 6 ? CMinIndex + 6 : CMaxIndex2;
                        double area = Global.JtjCheck.GetArea(datas, CMinIndex, cmax1, cmax2);
                        text.AppendText("C (面积:" + Math.Round(area, 2));
                        area = area > standard ? standard : area;
                        area = area / standard * proportion;
                        text.AppendText(string.Format(" 占比:{0}%；", Math.Round(area, 2) * 100));

                        //规则曲线面积
                        //double rulesArea = Global.JtjCheck.GetRulesArea(datas, TMinIndex, TMaxIndex1, TMaxIndex2);

                        //斜率 以波谷T为原点往C值方向遍历，当出现X_(i-1)<=X_i时，即找到波峰值坐标T_i
                        standard = 200;//斜率标准为100
                        proportion = 0.25;//斜率权重25%
                        slope = Global.JtjCheck.GetSlope(datas, CMinIndex, scope);
                        text.AppendText(string.Format("    C斜率:{0}", Math.Round(slope, 2)));
                        slope = slope > standard ? standard : slope;
                        slope = slope / standard * proportion;
                        text.AppendText(string.Format(" 占比:{0}%；", Math.Round(slope, 2) * 100));

                        //差值 以波谷T为原点往C值方向遍历，找到波峰的值
                        standard = 2000;//差值标准为2000
                        proportion = 0.25;//差值权重25%
                        difference = Global.JtjCheck.GetDifference(datas, CMinIndex, "C");
                        text.AppendText(string.Format("C差值:{0}", Math.Round(difference, 2)));
                        difference = difference > standard ? standard : difference;
                        difference = difference / standard * proportion;
                        text.AppendText(string.Format(" 占比:{0}%；", Math.Round(difference, 2) * 100));

                        //有效点 有效范围<=8，以波谷T为原点往C值方向遍历，寻找有效点
                        standard = 8;//有效点标准为8
                        proportion = 0.1;//有效点权重10%
                        efficientPoint = Global.JtjCheck.GetEfficientPoint(datas, CMinIndex, scope);
                        text.AppendText(string.Format("    C有效点:{0}", Math.Round(efficientPoint, 2)));
                        efficientPoint = efficientPoint > standard ? standard : efficientPoint;
                        efficientPoint = efficientPoint / standard * proportion;
                        text.AppendText(string.Format(" 占比:{0}%；", Math.Round(efficientPoint, 2) * 100));

                        _ValueC[i] = (area + slope + difference + efficientPoint) * 100;
                        text.AppendText(string.Format(" C最终结果:{0}%)", Math.Round(_ValueC[i], 2)));
                    }
                    #endregion

                    #region T值面积
                    //面积6个点(3~6)，斜率8个点(3~8)，有效点8个(连续上升/下降)，差值（波峰(T往C方向推取最大值)-波谷）
                    if (TMinIndex > 0)
                    {
                        //比例，面积比重大点35%，斜率和差值各25%，点数占15%

                        //scope为有效范围，坐标0为最小范围，坐标1为最大范围
                        int[] scope = new int[2];
                        scope[0] = 3; scope[1] = 8;
                        double standard = 0, proportion = 0;

                        //原始曲线面积
                        standard = 5000;//面积标准为5000
                        proportion = 0.4;//面积权重40%
                        double area = Global.JtjCheck.GetArea(datas, TMinIndex, TMaxIndex1, TMaxIndex2);
                        text.AppendText("\r\n\r\nT(面积:" + Math.Round(area, 2));
                        area = area > standard ? standard : area;
                        area = area / standard * proportion;
                        text.AppendText(string.Format(" 占比:{0}%；", Math.Round(area, 2) * 100));

                        //规则曲线面积
                        //double rulesArea = Global.JtjCheck.GetRulesArea(datas, TMinIndex, TMaxIndex1, TMaxIndex2);

                        //斜率 以波谷T为原点往C值方向遍历，当出现X_(i-1)<=X_i时，即找到波峰值坐标T_i
                        standard = 200;//斜率标准为100
                        proportion = 0.25;//斜率权重25%
                        slope = Global.JtjCheck.GetSlope(datas, TMinIndex, scope);
                        text.AppendText(string.Format("T斜率:{0}", Math.Round(slope, 2)));
                        slope = slope > standard ? standard : slope;
                        slope = slope / standard * proportion;
                        text.AppendText(string.Format(" 占比:{0}%；", Math.Round(slope, 2) * 100));

                        //差值 以波谷T为原点往C值方向遍历，找到波峰的值
                        standard = 2000;//差值标准为2000
                        proportion = 0.25;//差值权重25%
                        difference = Global.JtjCheck.GetDifference(datas, TMinIndex, "T");
                        text.AppendText(string.Format("T差值:{0}", Math.Round(difference, 2)));
                        difference = difference > standard ? standard : difference;
                        difference = difference / standard * proportion;
                        text.AppendText(string.Format(" 占比:{0}%；", Math.Round(difference, 2) * 100));

                        //有效点 有效范围<=8，以波谷T为原点往C值方向遍历，寻找有效点
                        standard = 8;//有效点标准为8
                        proportion = 0.1;//有效点权重10%
                        efficientPoint = Global.JtjCheck.GetEfficientPoint(datas, TMinIndex, scope);
                        text.AppendText(string.Format("T有效点:{0}", Math.Round(efficientPoint, 2)));
                        efficientPoint = efficientPoint > standard ? standard : efficientPoint;
                        efficientPoint = efficientPoint / standard * proportion;
                        text.AppendText(string.Format(" 占比:{0}%；", Math.Round(efficientPoint, 2) * 100));

                        _ValueT[i] = (area + slope + difference + efficientPoint) * 100;
                        text.AppendText(string.Format(" T最终结果:{0}%)", Math.Round(_ValueT[i], 2) * 100));
                    }
                    #endregion
                }
                else
                {
                    curveDatas.Add(string.Empty);
                }
            }

            #region 计算面积

            ////计算面积
            //if (TMinIndex > 0)
            //{
            //    //totalArea总面积，upArea上部分面积，beforeArea前面部分面积，afterArea后面部分面积
            //    double area = 0, totalArea = 0, upArea = 0, beforeArea = 0, afterArea = 0;

            //    //总面积宽度
            //    int totalWidth = System.Math.Abs(TMaxIndex1 - TMaxIndex2);

            //    //总面积
            //    totalArea = (datas[TMaxIndex1] > datas[TMaxIndex2] ?
            //        (datas[TMaxIndex1] - datas[TMinIndex]) : (datas[TMaxIndex2] - datas[TMinIndex])) * totalWidth;

            //    //上部分面积
            //    upArea = System.Math.Abs(datas[TMaxIndex1] - datas[TMaxIndex2]) * totalWidth * 0.5;

            //    //前面部分面积
            //    for (int j = TMaxIndex1; j < TMinIndex; j++)
            //    {
            //        beforeArea += (datas[j] - datas[j + 1]) * 0.5 + (datas[j + 1] - datas[TMinIndex]);
            //        #region
            //        //continue;
            //        ////如果两个点之间的差值<=1的话，当前点的面积不计算，直接往前再推一个点计算面积
            //        //if (datas[j] - datas[j + 1] > 1)
            //        //{
            //        //    beforeArea += (datas[j] - datas[j + 1]) * 0.5 + (datas[j + 1] - datas[TMinIndex]);
            //        //}
            //        //else
            //        //{
            //        //    //往前推一个点的时候需要判断是否是上升沿，如果不是，则不进行面积累加计算，并且总面积也需要减少一个点的面积
            //        //    if (datas[TMaxIndex1 - 1] - datas[TMaxIndex1] > 0)
            //        //    {
            //        //        beforeArea += (datas[TMaxIndex1 - 1] - datas[TMaxIndex1]) * 0.5 + (datas[TMaxIndex1] - datas[TMinIndex]);
            //        //    }
            //        //    else
            //        //    {
            //        //        //总面积减少一个点的计算
            //        //        totalArea = (datas[TMaxIndex1] > datas[TMaxIndex2] ?
            //        //            (datas[TMaxIndex1] - datas[TMinIndex]) : (datas[TMaxIndex2] - datas[TMinIndex])) * (totalWidth - 1);
            //        //        //上部分面积减少一个点的计算
            //        //        upArea = System.Math.Abs(datas[TMaxIndex1] - datas[TMaxIndex2]) * (totalWidth - 1) * 0.5;
            //        //    }
            //        //}
            //        #endregion
            //    }

            //    //后面部分面积
            //    for (int j = TMinIndex; j < TMaxIndex2; j++)
            //    {
            //        afterArea += (datas[j + 1] - datas[j]) * 0.5 + (datas[j] - datas[TMinIndex]);
            //        #region
            //        //continue;
            //        ////如果两个点之间的差值<=1的话，当前点的面积不计算，直接往前再推一个点计算面积
            //        //if (datas[j + 1] - datas[j] > 1)
            //        //{
            //        //    afterArea += (datas[j + 1] - datas[j]) * 0.5 + (datas[j] - datas[TMinIndex]);
            //        //}
            //        //else
            //        //{
            //        //    //往后推一个点的时候需要判断是否是上升沿，如果不是，则不进行面积累加计算，并且总面积也需要减少一个点的面积
            //        //    if (datas[TMaxIndex2 + 1] - datas[TMaxIndex2] > 0)
            //        //    {
            //        //        afterArea += (datas[TMaxIndex2 + 1] - datas[TMaxIndex2]) * 0.5 + (datas[TMaxIndex2] - datas[TMinIndex]);
            //        //    }
            //        //    else
            //        //    {
            //        //        //总面积减少一个点的计算
            //        //        totalArea = (datas[TMaxIndex1] > datas[TMaxIndex2] ?
            //        //            (datas[TMaxIndex1] - datas[TMinIndex]) : (datas[TMaxIndex2] - datas[TMinIndex])) * (totalWidth - 1);
            //        //        //上部分面积减少一个点的计算
            //        //        upArea = System.Math.Abs(datas[TMaxIndex1] - datas[TMaxIndex2]) * (totalWidth - 1) * 0.5;
            //        //    }
            //        //}
            //        #endregion
            //    }

            //    area = totalArea - upArea - beforeArea - afterArea;
            //    FileUtils.TestLog(string.Format("总面积：{0}；上部分面积：{1}；前面部分面积：{2}；后面部分面积：{3}；T值面积：{4}；\r\n通道：{5}；TMin：{6}；TMax1：{7}；TMax2：{8}；",
            //        totalArea, upArea, beforeArea, afterArea, area, i + 1, TMinIndex, TMaxIndex1, TMaxIndex2));

            //    //强拉成规则波形的面积
            //    double area2 = 0, totalArea2 = 0, upArea2 = 0, beforeArea2 = 0, afterArea2 = 0;
            //    int totalWidth2 = System.Math.Abs(TMaxIndex1 - TMaxIndex2) / 2;
            //    totalArea2 = Math.Abs((datas[TMaxIndex1] + datas[TMaxIndex2]) / 2 - datas[TMinIndex]) * totalWidth2;
            //    int idx = 1;
            //    for (int j = TMinIndex; j < TMaxIndex2; j++)
            //    {
            //        beforeArea2 += ((datas[TMinIndex + idx] + datas[TMinIndex - idx]) / 2 - datas[TMinIndex]) / 2;
            //        beforeArea2 += (datas[TMinIndex + idx - 1] + datas[TMinIndex - idx + 1]) / 2 - datas[TMinIndex];
            //        idx++;
            //    }
            //    area2 = (totalArea2 - upArea2 - beforeArea2 - afterArea2) * 2;

            //    _ValueT[i] = Math.Log10((area2 + area) / 2) * 10;

            //    FileUtils.TestLog(string.Format("规则波形面积：{0}；最后得出的面积：{1}；Log(T)：{2}", area2, (area2 + area) / 2, _ValueT[i]));
            //}

            #endregion

            #region 2016年12月25日 计算斜率
            //for (int i = 0; i < _tValues.Count; i++)
            //{
            //    _ValueC.Add(0); _ValueT.Add(0); _Peak.Add(0);
            //    if (_tValues[i] != null)
            //    {
            //        double[] datas = _tValues[i];
            //        int length = datas.Length,
            //            pointCNum = _item.dxxx.pointCNum <= 0 ? 5 : _item.dxxx.pointCNum,
            //            pointTNum = _item.dxxx.pointTNum <= 0 ? 5 : _item.dxxx.pointTNum,
            //            CMinIndex = 0, TMinIndex = 0,
            //            CCount = 0, TCount = 0;
            //        //先从第1个点到第25点，确定到C值最小点。判断条件如下：
            //        //条件1：从第1点开始，用序号大的值减序号小的值小于0（X_(i+1)-X_i<0），出现至少5个点，呈现连续下降趋势，直至（X_(i+1)-X_i>0）得出波谷最小值（X_i）。
            //        //条件2：从最小值（X_i）开始，用序号大的值减序号小的值大于0（X_(i+1)-X_i>0），呈现连续上升趋势，直至出现连续5个数满足。
            //        //条件3：最小值X_i的坐标要大于8 （X_i> 8），小于20范围内（X_i< 20）
            //        for (int j = 8 - pointCNum; j < length; j++)
            //        {
            //            if (CMinIndex == 0)
            //            {
            //                if ((datas[j] - datas[j - 1]) < 0)
            //                {
            //                    CCount++;
            //                    //连续pointCNum个下降沿，且满足X_(i+1)-X_i>0时
            //                    if (CCount >= pointCNum && (datas[j] - datas[j + 1]) < 0)
            //                    {
            //                        CCount = 0;
            //                        for (int k = j; k < length - 1; k++)
            //                        {
            //                            if ((datas[k] - datas[k + 1]) < 0)
            //                            {
            //                                CCount++;
            //                                //需再满足连续pointCNum个上升沿，且坐标范围在8~20之间，确定C值坐标
            //                                if (CCount >= pointCNum && j > 8 && j < 20)
            //                                {
            //                                    CMinIndex = j;
            //                                    break;
            //                                }
            //                            }
            //                        }
            //                    }
            //                }
            //                else
            //                {
            //                    CCount = 0;
            //                }
            //            }

            //            //T_MIN范围20~40
            //            if (j < 20) continue;
            //            if (TMinIndex == 0 && CMinIndex > 0)
            //            {
            //                if ((datas[j] - datas[j - 1]) < 0)
            //                {
            //                    TCount++;
            //                    //连续pointTNum个下降沿，且满足X_(i+1)-X_i>0时
            //                    if (TCount >= pointTNum && (datas[j] - datas[j + 1]) < 0)
            //                    {
            //                        TCount = 0;
            //                        for (int k = j; k < length - 1; k++)
            //                        {
            //                            if ((datas[k] - datas[k + 1]) < 0)
            //                            {
            //                                TCount++;
            //                                //需再满足连续pointTNum个上升沿，且T坐标要落在 C坐标 + （20±3）这个区间
            //                                if (TCount >= pointTNum && j > (CMinIndex + 17) && j < (CMinIndex + 23))
            //                                {
            //                                    TMinIndex = j;
            //                                    break;
            //                                }
            //                            }
            //                        }
            //                    }
            //                }
            //                else
            //                {
            //                    TCount = 0;
            //                }
            //                if (TMinIndex > 0) break;
            //            }
            //        }

            //        if (CMinIndex > 0)
            //        {
            //            //计算C值斜率 K=(C_5-C_MIN)/5值  
            //            double K = (datas[CMinIndex + 5] - datas[CMinIndex]) / 5;
            //            _ValueC[i] = System.Math.Log(K, 2);

            //            int TMaxIndex = 0;
            //            //计算T值斜率 
            //            //以T坐标为原点，往前C坐标方向遍历，当出现X_(i-1)-X_i＜0时，即找到波峰值坐标T_i
            //            if (TMinIndex > 0)
            //            {
            //                for (int j = TMinIndex; j > 0; j--)
            //                {
            //                    //如果没有找到T值，直接判定为阳性
            //                    if ((datas[j - 1] - datas[j]) < 0)
            //                    {
            //                        TMaxIndex = j;
            //                        K = 0;
            //                        //①当T_i-T_MAX≥8，则直接取T_8坐标的X_8值，计算斜率K=(X_8-X_MIN)/8值
            //                        if ((TMinIndex - TMaxIndex) >= 8)
            //                        {
            //                            K = (datas[TMinIndex - 8] - datas[TMinIndex]) / 8;
            //                        }
            //                        //②当T_i-T_MAX＜8，则直接取T_i坐标的X_i值
            //                        else if ((TMinIndex - TMaxIndex) < 8)
            //                        {
            //                            K = (datas[j] - datas[TMinIndex]) / (TMinIndex - TMaxIndex);
            //                        }
            //                        _ValueT[i] = System.Math.Log(K, 2);
            //                        break;
            //                    }
            //                }
            //            }
            //        }

            //        //绘制曲线
            //        DateTime[] dates = new DateTime[length];
            //        int[] numberOpen = new int[length];
            //        for (int j = 0; j < length; ++j)
            //        {
            //            DateTime dt = Convert.ToDateTime("01/01/000" + (j + 1));
            //            dates[j] = dt;
            //            numberOpen[j] = (int)datas[j];
            //        }

            //        var datesDataSource = new EnumerableDataSource<DateTime>(dates);
            //        datesDataSource.SetXMapping(x => _dateAxis[i < 2 ? i : 0].ConvertToDouble(x));

            //        var numberOpenDataSource = new EnumerableDataSource<int>(numberOpen);
            //        numberOpenDataSource.SetYMapping(y => y);

            //        CompositeDataSource compositeDataSource1 = new
            //        CompositeDataSource(datesDataSource, numberOpenDataSource);

            //        _plotters[i < 2 ? i : 0].AddLineGraph(compositeDataSource1,
            //                            new Pen(Brushes.Red, 2),
            //                            new CirclePointMarker { Size = 2.0, Fill = Brushes.Blue },
            //                            null);
            //        _plotters[i < 2 ? i : 0].Viewport.FitToView();
            //    }
            //}
            #endregion

            //}
            //catch (Exception ex)
            //{
            //    FileUtils.Log("JtjReportWindow-Curve:" + ex.Message + "\r\n\r\n详细信息:" + ex.ToString());
            //    MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            //}

            #region 旧版本代码 2016年12月23日
            //try
            //{
            //    for (int i = 0; i < _tValues.Count; i++)
            //    {
            //        _ValueC.Add(0); _ValueT.Add(0); _Peak.Add(0);
            //        if (_tValues[i] != null)
            //        {
            //            double[] datas = _tValues[i];
            //            int length = datas.Length;//, radius = datas.Length / 2;
            //            int CIndex = 0, TIndex = 0;

            //            //C值波谷坐标 10-30
            //            for (int j = 0; j < length; ++j)
            //            {
            //                if (j == 0) j = 10;
            //                if (j > 20) break;

            //                if (_ValueC[i] == 0)
            //                {
            //                    _ValueC[i] = datas[j];
            //                    CIndex = j;
            //                }

            //                if (_ValueC[i] > datas[j])
            //                {
            //                    _ValueC[i] = datas[j];
            //                    CIndex = j;
            //                }
            //            }

            //            //T值波谷坐标 25-45
            //            for (int j = 0; j < length; ++j)
            //            {
            //                if (j == 0) j = 30;
            //                if (j > 40) break;

            //                if (_ValueT[i] == 0)
            //                {
            //                    _ValueT[i] = datas[j];
            //                    TIndex = j;
            //                }

            //                if (_ValueT[i] > datas[j])
            //                {
            //                    _ValueT[i] = datas[j];
            //                    TIndex = j;
            //                }
            //            }

            //            double IDX = 0;
            //            //C和T之间的波峰
            //            for (int j = 0; j < length; j++)
            //            {
            //                if (j == 0) j = CIndex;
            //                if (j >= TIndex) break;

            //                if (_Peak[i] == 0)
            //                {
            //                    _Peak[i] = datas[j];
            //                    IDX = j;
            //                }

            //                if (_Peak[i] < datas[j])
            //                {
            //                    _Peak[i] = datas[j];
            //                    IDX = j;
            //                }
            //            }

            //            int index = 5;
            //            if (datas.Length - TIndex <= index) TIndex = datas.Length - index - 1;
            //            FileUtils.jtjLog("波谷C  " + datas[CIndex] + "  C坐标  " + CIndex + "  波谷T  " + datas[TIndex] + "  T坐标  " + TIndex
            //                 + "  波峰  " + _Peak[i] + "  波峰坐标  " + IDX + "  C值  " + (_Peak[i] - datas[CIndex]) + "  T值  " + (_Peak[i] - datas[TIndex]));

            //            //重新计算C值和T值
            //            //C=peak-valuec;T=peak-valuet
            //            _ValueC[i] = 0; _ValueT[i] = 0; 
            //            _ValueC[i] = _Peak[i] - datas[CIndex];
            //            _ValueT[i] = _Peak[i] - datas[TIndex];


            //            #region
            //            //_ValueC[i] = 0; _ValueT[i] = 0;

            //            //if (CIndex - index <= 0 || CIndex + index >= datas.Length - 1)
            //            //    _ValueC[i] = 0;
            //            //else
            //            //    _ValueC[i] = (datas[CIndex - index] + datas[CIndex + index]) / 2 - datas[CIndex];

            //            //if (TIndex - index <= 0 || TIndex + index > datas.Length - 1)
            //            //    _ValueT[i] = 0;
            //            //else
            //            //    _ValueT[i] = (datas[TIndex - index] + datas[TIndex + index]) / 2 - datas[TIndex];

            //            //if (_ValueC[i]==0|| _ValueT[i]==0)
            //            //{
            //            //    string str = string.Empty;
            //            //}
            //            //FileUtils.jtjLog("T:C：" + _ValueT[i] / _ValueC[i] * 100);

            //            //确定C范围
            //            //int[] cIndex = new int[2];
            //            //if (CIndex > 0)
            //            //{
            //            //    for (int j = length; j >= 1; j--)
            //            //    {
            //            //        if (j == length) j = CIndex;
            //            //        if (System.Math.Abs(data[0] - data[j]) > 0.5) cIndex[0] = j;
            //            //        else break;
            //            //    }
            //            //    for (int j = 0; j < length; j++)
            //            //    {
            //            //        if (j == 0) j = CIndex;
            //            //        if (System.Math.Abs(data[0] - data[j]) < 0.5) cIndex[1] = cIndex[1] == 0 ? j - 1 : cIndex[1];
            //            //        if (j > radius) break;
            //            //    }
            //            //}

            //            //确定T范围
            //            //int[] tIndex = new int[2];
            //            //if (TIndex > 0)
            //            //{
            //            //    for (int j = length; j >= 1; j--)
            //            //    {
            //            //        if (j == length) j = TIndex;
            //            //        if (System.Math.Abs(data[0] - data[j]) > 0.5) tIndex[0] = j;
            //            //        else break;
            //            //    }
            //            //    for (int j = 0; j < length; j++)
            //            //    {
            //            //        if (j == 0) j = TIndex;
            //            //        if (System.Math.Abs(data[length - 1] - data[j]) < 0.5) tIndex[1] = tIndex[1] == 0 ? j - 1 : tIndex[1];
            //            //    }
            //            //}

            //            //求曲线面积
            //            //_ValueC[i] = 0; _ValueT[i] = 0;
            //            //for (int j = 0; j < length; j++)
            //            //{
            //            //    if (j == 0 && cIndex[0] > 0) j = cIndex[0];
            //            //    if (tIndex[1] > 0 && j > tIndex[1]) break;
            //            //    if (j >= cIndex[0] && j <= cIndex[1]) _ValueC[i] += System.Math.Abs(data[0] - data[j]);
            //            //    if (j >= tIndex[0] && j <= tIndex[1]) _ValueT[i] += System.Math.Abs(data[length - 1] - data[j]);
            //            //}
            //            #endregion

            //            //优化显示的曲线
            //            //double[] values = new double[length];
            //            //for (int j = 0; j < length; j++)
            //            //{
            //            //    values[i] = datas[i];
            //            //}
            //            //double[][] dbs = new double[2][];
            //            //dbs[0] = values;
            //            //dbs[1] = new double[length];
            //dbs = CurveSmoothing.FFT(dbs, 1);
            //dbs = CurveSmoothing.FLT(dbs);
            //dbs = CurveSmoothing.FFT(dbs, -1);
            //values = CurveSmoothing.SUM(dbs);

            //            //绘制曲线
            //            DateTime[] dates = new DateTime[length];
            //            int[] numberOpen = new int[length];
            //            for (int j = 0; j < length; ++j)
            //            {
            //                DateTime dt = Convert.ToDateTime("01/01/000" + (j + 1));
            //                dates[j] = dt;
            //                numberOpen[j] = (int)datas[j];
            //            }

            //            var datesDataSource = new EnumerableDataSource<DateTime>(dates);
            //            datesDataSource.SetXMapping(x => _dateAxis[i < 2 ? i : 0].ConvertToDouble(x));

            //            var numberOpenDataSource = new EnumerableDataSource<int>(numberOpen);
            //            numberOpenDataSource.SetYMapping(y => y);

            //            CompositeDataSource compositeDataSource1 = new
            //            CompositeDataSource(datesDataSource, numberOpenDataSource);

            //            _plotters[i < 2 ? i : 0].AddLineGraph(compositeDataSource1,
            //                                new Pen(Brushes.Red, 2),
            //                                new CirclePointMarker { Size = 2.0, Fill = Brushes.Blue },
            //                                null);
            //            _plotters[i < 2 ? i : 0].Viewport.FitToView();
            //        }
            //    }
            //    LabelInfo_Copy.Content = "C1:" + _ValueC[0] + "; T1:" + _ValueT[0] + "   C2:" + _ValueC[1] + "; T2:" + _ValueT[1];
            //    //FileUtils.jtjLog(LabelInfo_Copy.Content.ToString());
            //}
            //catch (Exception ex)
            //{
            //    FileUtils.Log("JtjReportWindow-Curve:" + ex.Message + "\r\n\r\n详细信息:" + ex.ToString());
            //    MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            //}
            #endregion
        }

        /// <summary>
        /// 格式化数据
        /// </summary>
        private void FormattingDatas()
        {
            try
            {
                _tValues = new List<double[]>();
                foreach (byte[] datas in _datas)
                {
                    if (datas != null && datas.Length > 160)
                    {
                        int length = datas.Length / 2, index = 0;
                        double[] data = new double[length];
                        for (int i = 0; i < length; i++)
                        {
                            data[i] = datas[index + 1] * 256 + datas[index];
                            index += 2;
                        }

                        //从原始数据87~89个数据点，截取第20点到第65个点这一段
                        //int[] idx = CurveSmoothing.FindUsefulData(data);
                        string strdt = string.Empty;
                        #region 测试数据
                        //string str = "5469|6329|7112|7801|8382|8835|9249|9696|10069|10308|10422|10390|10185|9852|9504|9224|9010|8804|8591|8391|8229|8103|8007|7943|7891|7839|7787|7732|7687|7636|7547|7413|7243|7055|6880|6777|6789|6910|7099|7295|7448|7544|7591|7602|7584|7549|7515|7490|7484|7492|7473|7378|7206|7009|6848|6779|6819|6926|7063|7233|7399|7523|7608|7658|7678|7700|7746|7818|7912|8037|8225|8572|9157|10112|11453|12653|13274|13240|12542|11436|10366|9558|8888|8185|7409|6561|5636|4694|3808|2987|21635469|6329|7112|7801|8382|8835|9249|9696|10069|10308|10422|10390|10185|9852|9504|9224|9010|8804|8591|8391|8229|8103|8007|7943|7891|7839|7787|7732|7687|7636|7547|7413|7243|7055|6880|6777|6789|6910|7099|7295|7448|7544|7591|7602|7584|7549|7515|7490|7484|7492|7473|7378|7206|7009|6848|6779|6819|6926|7063|7233|7399|7523|7608|7658|7678|7700|7746|7818|7912|8037|8225|8572|9157|10112|11453|12653|13274|13240|12542|11436|10366|9558|8888|8185|7409|6561|5636|4694|3808|2987|2163";
                        //string[] strList = str.Split('|');
                        //double[] db = new double[strList.Length];
                        //for (int i = 0; i < strList.Length; i++)
                        //{
                        //    //_tValues[0][x] = double.Parse(strList[i]);
                        //    db[i] = double.Parse(strList[i]);
                        //}

                        //data = db;


                        //for (int i = 0; i < data.Length; i++)
                        //{
                        //    strdt += strdt.Length == 0 ? data[i].ToString() : "|" + data[i];
                        //}

                        //FileUtils.TestLog(strdt);
                        #endregion

                        int[] idx = new int[2];
                        idx[0] = 20; idx[1] = 65;
                        if (idx != null && data.Length > 85)
                        {
                            int len = idx[1] - idx[0];
                            _tValue = new double[len];
                            Array.ConstrainedCopy(data, idx[0], _tValue, 0, len);
                            strdt = string.Empty;
                            for (int i = 0; i < _tValue.Length; i++)
                            {
                                strdt += strdt.Length == 0 ? _tValue[i].ToString() : "|" + _tValue[i];
                            }

                            //FileUtils.TestLog(strdt);

                            #region
                            //原始数据
                            //oldData = new double[len];
                            //for (int i = 0; i < len; i++)
                            //    oldData[i] = _tValue[i];

                            //double[] value = new double[_tValue.Length + 20];
                            //for (int i = 0; i < 10; i++)
                            //{
                            //    value[i] = _tValue[0];
                            //    value[_tValue.Length + 10 + i] = _tValue[_tValue.Length - 1];
                            //}
                            //Array.ConstrainedCopy(_tValue, 0, value, 10, _tValue.Length);
                            //_tValue = new double[value.Length];
                            //_tValue = value;
                            //len = _tValue.Length;

                            //_tValue = CurveSmoothing.BaselineCorrect(_tValue);
                            //double[] values = new double[_tValue.Length - 1];
                            //Array.ConstrainedCopy(_tValue, 1, values, 0, values.Length);
                            //_tValue = new double[values.Length];
                            //for (int i = 0; i < values.Length; i++)
                            //    _tValue[i] = values[i];

                            //double[][] dbs = new double[2][];
                            //dbs[0] = _tValue;
                            //dbs[1] = new double[len];
                            //dbs = CurveSmoothing.FFT(dbs, 1);
                            //dbs = CurveSmoothing.FLT(dbs);
                            //dbs = CurveSmoothing.FFT(dbs, -1);
                            //_tValue = CurveSmoothing.SUM(dbs);

                            //_tValue = CurveSmoothing.ZB(_tValue);
                            #endregion
                        }
                        else
                        {
                            _tValue = new double[45];
                            for (int i = 0; i < _tValue.Length; i++)
                            {
                                _tValue[i] = 10000;
                            }
                        }
                        _tValues.Add(_tValue);
                    }
                    else
                    {
                        _tValues.Add(null);
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(2, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _tValues = null;
            UpdateItem();
            _msgThread.Stop();
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void Save()
        {
            if (Global.InterfaceType.Equals("AH"))
            {
                _AllNumber = TestResultConserve.ResultConserveAH(_CheckValue);
            }
            //if (Global.InterfaceType.Length == 0 || Global.InterfaceType.Equals("DY") || Global.InterfaceType.Equals("ZH") || Global.InterfaceType.Equals("GS"))
            else
            {
                _AllNumber = TestResultConserve.ResultConserve(_CheckValue);
            }
        }

        private List<PrintHelper.Report> GenerateReports()
        {
            List<PrintHelper.Report> reports = new List<PrintHelper.Report>();
            try
            {
                DataTable dt = _resultTable.GetAsDataTable(string.Empty, string.Empty, 6, _AllNumber);
                if (dt != null && dt.Rows.Count > 0)
                {
                    IDictionary<string, List<tlsTtResultSecond>> dic = new Dictionary<string, List<tlsTtResultSecond>>();
                    List<tlsTtResultSecond> dtList = Global.TableToEntity<tlsTtResultSecond>(dt);
                    if (dtList.Count > 0)
                    {
                        for (int i = 0; i < dtList.Count; i++)
                        {
                            if (!dic.ContainsKey(dtList[i].CheckedCompany))
                            {
                                List<tlsTtResultSecond> rs = new List<tlsTtResultSecond>();
                                rs.Add(dtList[i]);
                                dic.Add(dtList[i].CheckedCompany, rs);
                            }
                            else
                            {
                                dic[dtList[i].CheckedCompany].Add(dtList[i]);
                            }
                        }
                        foreach (var item in dic)
                        {
                            List<tlsTtResultSecond> models = item.Value;
                            PrintHelper.Report model = new PrintHelper.Report();
                            model.ItemName = _item.Name;
                            model.ItemCategory = models[0].ResultType; 
                            model.User = LoginWindow._userAccount.UserName;
                            model.ContrastValue = models[0].ContrastValue;
                            model.Unit = _item.Unit;
                            model.Judgment = _item.Hole[0].SampleName;
                            model.Date = _date.ToString("yyyy-MM-dd HH:mm:ss");
                            model.Company = item.Key;
                            for (int i = 0; i < models.Count; i++)
                            {
                                model.SampleName.Add(item.Value[i].FoodName);
                                model.SampleNum.Add(String.Format("{0:D5}", item.Value[i].SampleCode));
                                model.JudgmentTemp.Add(item.Value[i].Result);
                                model.Result.Add(item.Value[i].CheckValueInfo);
                            }
                            reports.Add(model);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(1, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
            return reports;
        }

        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<byte> data = new List<byte>();
                List<PrintHelper.Report> reports = GenerateReports();
                foreach (PrintHelper.Report report in reports)
                    data.AddRange(report.GeneratePrintBytes());
                byte[] buffer = new byte[data.Count];
                data.CopyTo(buffer);
                Global.IsStartGetBattery = false;
                Message msg = new Message();
                msg.what = MsgCode.MSG_PRINT;
                msg.str1 = Global.strPRINTPORT;
                msg.data = buffer;
                msg.arg1 = 0;
                msg.arg2 = buffer.Length;
                Global.printThread.SendMessage(msg, _msgThread);
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(2, logType, ex.ToString());
                MessageBox.Show("打印失败!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private UIElement GenerateResultLayout(int channel, string sampleNum, string sampleName)
        {
            Border border = new Border();
            border.Width = 350;
            border.Margin = new Thickness(2);
            border.BorderThickness = new Thickness(5);
            border.BorderBrush = _borderBrushNormal;
            border.CornerRadius = new CornerRadius(10);
            border.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            border.Name = "border";

            StackPanel stackPanel = new StackPanel();
            stackPanel.Width = 350;
            stackPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            stackPanel.Name = "stackPanel";

            Grid grid = new Grid();
            grid.Width = 350;
            grid.Height = 40;
            grid.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;

            Label label = new Label();
            label.FontSize = 20;
            label.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            label.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            label.Content = " 检测通道" + (channel + 1);

            Canvas canvas = new Canvas();
            canvas.Width = 345;
            canvas.Height = 200;
            canvas.Background = Brushes.Gray;
            canvas.Name = "canvas";
            canvas.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;

            ChartPlotter plotter = new ChartPlotter();
            plotter.Width = 345;
            plotter.Height = 200;
            plotter.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            plotter.MouseDoubleClick += new MouseButtonEventHandler(plotter_MouseDoubleClick);
            plotter.Name = "chartPlotter";

            HorizontalAxis horizontalAxis = new HorizontalAxis();
            horizontalAxis.Name = "horizontalAxis";

            HorizontalDateTimeAxis dateAxis = new HorizontalDateTimeAxis();
            dateAxis.Name = "dateAxis";

            VerticalAxis verticalAxis = new VerticalAxis();
            verticalAxis.Name = "verticalAxis";

            VerticalIntegerAxis countAxis = new VerticalIntegerAxis();
            countAxis.Name = "countAxis";

            VerticalAxisTitle arialy = new VerticalAxisTitle();
            arialy.Content = "y";

            HorizontalAxisTitle arialx = new HorizontalAxisTitle();
            arialx.Content = "x";

            canvas.Children.Add(plotter);
            canvas.Children.Add(dateAxis);
            canvas.Children.Add(verticalAxis);
            canvas.Children.Add(countAxis);
            canvas.Children.Add(arialy);
            canvas.Children.Add(arialx);

            WrapPanel wrapPannelSampleNum = new WrapPanel();
            wrapPannelSampleNum.Width = 345;
            wrapPannelSampleNum.Height = 30;

            Label labelSampleNum = new Label();
            labelSampleNum.Width = 85;
            labelSampleNum.Height = 26;
            labelSampleNum.Margin = new Thickness(0, 2, 0, 0);
            labelSampleNum.FontSize = 15;
            labelSampleNum.Content = " 样品编号:";
            labelSampleNum.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            TextBox textBoxSampleNum = new TextBox();
            textBoxSampleNum.Width = 255;
            textBoxSampleNum.Height = 26;
            textBoxSampleNum.Margin = new Thickness(0, 2, 0, 2);
            textBoxSampleNum.FontSize = 15;
            textBoxSampleNum.Text = string.Empty + sampleNum;
            textBoxSampleNum.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            textBoxSampleNum.IsReadOnly = true;

            WrapPanel wrapPannelSampleName = new WrapPanel();
            wrapPannelSampleName.Width = 345;
            wrapPannelSampleName.Height = 30;

            Label labelSampleName = new Label();
            labelSampleName.Width = 85;
            labelSampleName.Height = 26;
            labelSampleName.Margin = new Thickness(0, 2, 0, 0);
            labelSampleName.FontSize = 15;
            labelSampleName.Content = " 样品名称:";
            labelSampleName.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            TextBox textBoxSampleName = new TextBox();
            textBoxSampleName.Width = 255;
            textBoxSampleName.Height = 26;
            textBoxSampleName.Margin = new Thickness(0, 2, 0, 2);
            textBoxSampleName.FontSize = 15;
            textBoxSampleName.Text = string.Empty + _item.Hole[channel].SampleName;
            textBoxSampleName.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            textBoxSampleName.IsReadOnly = true;

            WrapPanel wrapPannelRGBValue = new WrapPanel();
            wrapPannelRGBValue.Width = 180;
            wrapPannelRGBValue.Height = 30;

            Label labelRGBValue = new Label();
            labelRGBValue.Width = 85;
            labelRGBValue.Height = 26;
            labelRGBValue.Margin = new Thickness(0, 2, 0, 0);
            labelRGBValue.FontSize = 15;
            labelRGBValue.Content = " 灰度值:";
            labelRGBValue.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            TextBox textBoxRGBValue = new TextBox();
            textBoxRGBValue.Width = 90;
            textBoxRGBValue.Height = 26;
            textBoxRGBValue.Margin = new Thickness(0, 2, 0, 2);
            textBoxRGBValue.FontSize = 15;
            textBoxRGBValue.Text = string.Empty;
            textBoxRGBValue.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            textBoxRGBValue.IsReadOnly = true;
            textBoxRGBValue.Name = "textBoxRGBValue";

            WrapPanel wrapPannelDetectResult = new WrapPanel();
            wrapPannelDetectResult.Width = 345;
            wrapPannelDetectResult.Height = 30;

            Label labelDetectResult = new Label();
            labelDetectResult.Width = 85;
            labelDetectResult.Height = 26;
            labelDetectResult.Margin = new Thickness(0, 2, 0, 0);
            labelDetectResult.FontSize = 15;
            labelDetectResult.Content = " 检测结果:";
            labelDetectResult.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            TextBox textBoxDetectResult = new TextBox();
            textBoxDetectResult.Width = 255;
            textBoxDetectResult.Height = 26;
            textBoxDetectResult.Margin = new Thickness(0, 2, 0, 2);
            textBoxDetectResult.FontSize = 15;
            textBoxDetectResult.Text = string.Empty;
            textBoxDetectResult.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            textBoxDetectResult.IsReadOnly = true;
            textBoxDetectResult.Name = "textBoxDetectResult";

            //判定结果
            WrapPanel wrapJudgemtn = new WrapPanel();
            wrapJudgemtn.Width = 345;
            wrapJudgemtn.Height = 30;

            Label labelJudgment = new Label();
            labelJudgment.Width = 85;
            labelJudgment.Height = 26;
            labelJudgment.Margin = new Thickness(0, 2, 0, 0);
            labelJudgment.FontSize = 15;
            labelJudgment.Content = " 判定结果:";
            labelJudgment.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            TextBox textJugmentResult = new TextBox();
            textJugmentResult.Width = 255;
            textJugmentResult.Height = 26;
            textJugmentResult.Margin = new Thickness(0, 2, 0, 2);
            textJugmentResult.FontSize = 15;
            textJugmentResult.Text = string.Empty;
            textJugmentResult.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            textJugmentResult.Name = "textJugmentResult";
            //判定标准值
            WrapPanel wrapStandValue = new WrapPanel();
            wrapStandValue.Width = 180;
            wrapStandValue.Height = 30;

            Label labelStandValue = new Label();
            labelStandValue.Width = 85;
            labelStandValue.Height = 26;
            labelStandValue.Margin = new Thickness(0, 2, 0, 0);
            labelStandValue.FontSize = 15;
            labelStandValue.Content = " 标准值:";
            labelStandValue.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            TextBox textStandValue = new TextBox();
            textStandValue.Width = 90;
            textStandValue.Height = 26;
            textStandValue.Margin = new Thickness(0, 2, 0, 2);
            textStandValue.FontSize = 15;
            textStandValue.Text = "1.00";
            textStandValue.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            textStandValue.Name = "textStandValue";

            grid.Children.Add(label);
            wrapPannelSampleNum.Children.Add(labelSampleNum);
            wrapPannelSampleNum.Children.Add(textBoxSampleNum);
            wrapPannelSampleName.Children.Add(labelSampleName);
            wrapPannelSampleName.Children.Add(textBoxSampleName);

            //wrapPannelRGBValue.Children.Add(labelRGBValue);
            //wrapPannelRGBValue.Children.Add(textBoxRGBValue);
            wrapPannelDetectResult.Children.Add(labelDetectResult);
            wrapPannelDetectResult.Children.Add(textBoxDetectResult);
            wrapJudgemtn.Children.Add(labelJudgment);

            wrapJudgemtn.Children.Add(textJugmentResult);
            wrapStandValue.Children.Add(labelStandValue);
            wrapStandValue.Children.Add(textStandValue);
            stackPanel.Children.Add(grid);
            stackPanel.Children.Add(canvas);

            stackPanel.Children.Add(wrapPannelSampleNum);
            stackPanel.Children.Add(wrapPannelSampleName);
            //stackPanel.Children.Add(wrapPannelRGBValue);
            stackPanel.Children.Add(wrapPannelDetectResult);
            stackPanel.Children.Add(wrapJudgemtn);

            //stackPanel.Children.Add(wrapStandValue);
            border.Child = stackPanel;
            return border;
        }

        private void plotter_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ChartPlotter chart = sender as ChartPlotter;
            Point p = e.GetPosition(this).ScreenToData(chart.Transform);
        }

        private void UpdateItem()
        {
            // 更新样品编号
            try
            {
                for (int i = 0; i < Global.deviceHole.SxtCount; ++i)
                {
                    if (_item.Hole[i].Use)
                    {
                        _item.SampleNum++;
                    }
                }
                Global.SerializeToFile(Global.jtjItems, Global.jtjItemsFile);
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(2, logType, ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        /// <summary>
        /// 更新上传后的检测数据状态
        /// </summary>
        private void UpdateUpload()
        {
            string error = string.Empty;
            try
            {
                for (int i = 0; i < _idList.Count; i++)
                    _resultTable.UpdateUpload(_idList[i], out error);
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(2, logType, ex.ToString());
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (!error.Equals(string.Empty))
                    MessageBox.Show("出现异常!\n" + error);
                _idList = null;
            }
        }

        class MsgThread : ChildThread
        {
            JtjReportWindow wnd;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;

            public MsgThread(JtjReportWindow wnd)
            {
                this.wnd = wnd;
                uiHandleMessageDelegate = new UIHandleMessageDelegate(UIHandleMessage);
            }

            protected override void HandleMessage(Message msg)
            {
                try
                {
                    wnd.Dispatcher.Invoke(uiHandleMessageDelegate, msg);
                }
                catch (Exception ex)
                {
                    FileUtils.OprLog(2, wnd.logType, ex.ToString());
                    Console.WriteLine(ex.Message);
                }
            }

            private void UIHandleMessage(Message msg)
            {
                switch (msg.what)
                {
                    case MsgCode.MSG_PRINT:
                        if (!msg.result)
                            wnd.LabelInfo.Content = "打印失败，请检查打印端口是否正确。";
                        break;

                    case MsgCode.MSG_UPLOAD:
                        if (msg.result)
                        {
                            wnd.LabelInfo.Content = "成功上传 " + Global.UploadSCount + " 条数据!";
                            wnd.IsUpLoad = true;
                            Global.IsStartUploadTimer = false;
                        }
                        else
                        {
                            if (Global.UploadSCount > 0 || Global.UploadFCount > 0)
                            {
                                wnd.LabelInfo.Content = "数据上传:成功" + Global.UploadSCount + "条；失败" + Global.UploadFCount + "条";
                                wnd.IsUpLoad = false;
                            }
                            else
                            {
                                wnd.LabelInfo.Content = "数据上传失败！";
                            }
                            if (msg.outError.Length > 0)
                            {
                                MessageBox.Show(wnd, msg.outError, "系统提示");
                            }
                            Global.IsStartUploadTimer = true;
                        }
                        break;

                    default:
                        break;
                }
            }
        }
        class JTJResult
        {
            public const int JTJF_STAT_UNKNOWN = 0x00;
            public const int JTJF_STAT_PLUS = 0x01;
            public const int JTJF_STAT_MINUS = 0x02;
            public const int JTJF_STAT_INVALID = 0x03;
            public const int JTJF_STAT_Lee = 0x04;
            public int result = JTJF_STAT_INVALID;
            public double density = 0; //如果是定量，需要用到浓度值
        }

        private JTJResult CalJTJFResult(byte CValue, byte TValue, DYJTJItemPara mItem)
        {
            JTJResult jtjResult = new JTJResult();
            if (CValue <= mItem.InvalidC)
            {   // C 线不显色为无效
                jtjResult.result = JTJResult.JTJF_STAT_INVALID;
            }
            else
            {
                switch (mItem.Method)
                {   // 定量
                    case DYJTJItemPara.METHOD_DXXX:
                        {
                            double a = TValue;
                            double f = mItem.dlt.A0 + mItem.dlt.A1 * a + mItem.dlt.A2 * a * a + mItem.dlt.A3 * a * a * a;
                            jtjResult.density = f = mItem.dlt.B0 * f + mItem.dlt.B1;

                            if (TValue <= mItem.dxxx.PlusT)
                                jtjResult.result = JTJResult.JTJF_STAT_PLUS;
                            else if (TValue > mItem.dxxx.MinusT)
                                jtjResult.result = JTJResult.JTJF_STAT_MINUS;
                        }
                        break;
                    case DYJTJItemPara.METHOD_DXBS:
                        {
                            double a = TValue;
                            double f = mItem.dlt.A0 + mItem.dlt.A1 * a + mItem.dlt.A2 * a * a + mItem.dlt.A3 * a * a * a;
                            jtjResult.density = f = mItem.dlt.B0 * f + mItem.dlt.B1;
                            if (TValue < CValue)// 阳性
                                jtjResult.result = JTJResult.JTJF_STAT_PLUS;
                            else
                                jtjResult.result = JTJResult.JTJF_STAT_MINUS;
                        }
                        break;
                    case DYJTJItemPara.METHOD_DLT:
                        {
                            double a = TValue;
                            double f = mItem.dlt.A0 + mItem.dlt.A1 * a + mItem.dlt.A2 * a * a + mItem.dlt.A3 * a * a * a;
                            jtjResult.density = f = mItem.dlt.B0 * f + mItem.dlt.B1;
                            if (f > mItem.dlt.Limit)
                                jtjResult.result = JTJResult.JTJF_STAT_PLUS;
                            else
                                jtjResult.result = JTJResult.JTJF_STAT_MINUS;
                        }
                        break;

                    case DYJTJItemPara.METHOD_DLTC:
                        {
                            double a = CValue == 0 ? 1 : TValue * 1.0 / CValue;
                            double f = mItem.dltc.A0 + mItem.dltc.A1 * a + mItem.dltc.A2 * a * a + mItem.dltc.A3 * a * a * a;
                            jtjResult.density = f = mItem.dltc.B0 * f + mItem.dltc.B1;
                            if (f > mItem.dltc.Limit)
                                jtjResult.result = JTJResult.JTJF_STAT_PLUS;
                            else
                                jtjResult.result = JTJResult.JTJF_STAT_MINUS;
                        }

                        break;
                }
            }
            return jtjResult;
        }

        string JTJFResultStatToStr(JTJResult jtjResult)
        {
            string str = string.Empty;
            switch (jtjResult.result)
            {
                case JTJResult.JTJF_STAT_PLUS:
                    str = "阳性";
                    break;
                case JTJResult.JTJF_STAT_MINUS:
                    str = "阴性";
                    break;
                case JTJResult.JTJF_STAT_INVALID:

                    str = "无效";
                    break;
                case JTJResult.JTJF_STAT_Lee:

                    str = "有效";
                    break;
                default:
                    str = "错误";
                    break;
            }
            return str;
        }

        private byte getMinByte(byte[] data)
        {
            byte b = 255;
            for (int i = 0; i < data.Length; ++i)
                if (b > data[i])
                    b = data[i];
            return b;
        }

        private byte getMaxByte(byte[] data)
        {
            byte b = 0;
            for (int i = 0; i < data.Length; ++i)
                if (b < data[i])
                    b = data[i];
            return b;
        }

        private int weitiaoCT(byte[] data, int offset, int nWidth)
        {
            int lastOffset = offset;
            int startOffset = offset - nWidth / 2;
            int endOffset = offset + nWidth / 2;
            if (startOffset <= 0 || endOffset >= data.Length)
                return lastOffset;
            int maxValueIdx = startOffset;
            int maxValue = data[maxValueIdx];
            for (int i = startOffset; i < endOffset; ++i)
            {
                if (maxValue < data[i])
                {
                    maxValue = data[i];
                    maxValueIdx = i;
                }
            }
            lastOffset = maxValueIdx + nWidth / 2;

            return lastOffset;
        }

        #region-- UpDataNowing
        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            //Window1 ww = new Window1();
            //ww._tValues = oldData;
            //ww.Show();
            if (IsUpLoad)
            {
                MessageBox.Show("当前数据已上传!", "系统提示");
                return;
            }
            Upload();
        }

        /// <summary>
        /// 30s上传超时
        /// </summary>
        private void UploadTimeOut(object sender, EventArgs e)
        {
            LabelInfo.Content = "正在上传...";
            if (LabelInfo.Content.Equals("正在上传..."))
            {
                LabelInfo.Content = "上传超时，请检测连接设置！";
                _DataTimer.Stop();
                _msgThread.Stop();
            }
        }

        private bool UploadCheck()
        {
            if (!Global.IsConnectInternet())
            {
                Global.IsStartUploadTimer = true;
                MessageBox.Show(this, "设备无法连接到互联网，请检查网络！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                this.LabelInfo.Content = "无网络连接";
                return false;
            }

            if (Global.InterfaceType.Equals("DY"))
            {
                if (Global.samplenameadapter == null || Global.samplenameadapter.Count == 0)
                {
                    if (MessageBox.Show("还未进行服务器通讯测试，可能导致数据上传失败！\r\n是否前往【设置】界面进行通讯测试？", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        SettingsWindow window = new SettingsWindow();
                        window.ShowDialog();
                    }
                    else
                    {
                        LabelInfo.Content = "取消上传";
                        return false;
                    }
                }
            }

            if (Global.InterfaceType.Equals("ZH"))
            {
                if (Wisdom.DeviceID.Length == 0 || Wisdom.USER.Length == 0 || Wisdom.PASSWORD.Length == 0)
                {
                    if (MessageBox.Show("【无法上传】 - 服务器链接配置异常，是否立即前往【设置】界面进行配置？", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        SettingsWindow window = new SettingsWindow()
                        {
                            DeviceIdisNull = false
                        };
                        window.ShowDialog();
                    }
                    else
                    {
                        LabelInfo.Content = "取消上传";
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 上传
        /// </summary>
        private void Upload()
        {
            if (!UploadCheck()) return;

            try
            {
                LabelInfo.Content = "正在上传...";
                _resultTable = new tlsttResultSecondOpr();
                DataTable dt = _resultTable.GetAsDataTable(string.Empty, string.Empty, 6, _AllNumber);
                if (dt != null || dt.Rows.Count > 0)
                {
                    if (Global.InterfaceType.Equals("DY"))
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dt.Rows[i]["CKCKNAMEUSID"] = Global.samplenameadapter[0].UploadUserUUID;
                        }
                    }
                }
                else
                {
                    Global.IsStartUploadTimer = false;
                    LabelInfo.Content = "暂无需要上传的数据";
                    return;
                }

                Message msg = new Message();
                msg.what = MsgCode.MSG_UPLOAD;
                msg.obj1 = Global.samplenameadapter[0];
                msg.table = dt;
                //获取服务器地址信息
                if (Global.samplenameadapter.Count > 0)
                {
                    CheckPointInfo CPoint = Global.samplenameadapter[0];
                    msg.str1 = CPoint.ServerAddr;
                    msg.str2 = CPoint.RegisterID;
                    msg.str3 = CPoint.RegisterPassword;

                }
                //if (Global.InterfaceType.Equals("AH") || Global.InterfaceType.Equals("ZH"))
                //{
                //    if (dt != null && dt.Rows.Count > 0)
                //    {
                //        List<tlsTtResultSecond> dtList = Global.TableToEntity<tlsTtResultSecond>(dt);
                //        msg.selectedRecords = dtList;
                //    }
                //}
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<tlsTtResultSecond> dtList = Global.TableToEntity<tlsTtResultSecond>(dt);
                    msg.selectedRecords = dtList;
                }
                Global.updateThread.SendMessage(msg, _msgThread);
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(2, logType, ex.ToString());
                MessageBox.Show("上传时出现异常！\r\n异常信息：" + ex.Message, "系统提示");
            }
        }

        #endregion

        private void Btn_ShowDatas_Click(object sender, RoutedEventArgs e)
        {
            RecordWindow window = new RecordWindow()
            {
                ShowInTaskbar = false,
                Owner = this,
                _CheckItemName = _item.Name
            };
            window.Show();
        }

    }
}
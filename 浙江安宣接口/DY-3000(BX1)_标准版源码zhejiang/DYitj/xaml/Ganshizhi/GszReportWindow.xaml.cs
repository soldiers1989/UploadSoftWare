using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
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
    /// GszReportWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GszReportWindow : Window
    {

        #region 全局变量
        public byte[] _data;
        public List<byte[]> _datas;
        public List<byte[]> _listRGBValues;
        //private static double[][] _rgbValues;
        private static Double[] _tValue;
        private static List<Double[]> _tValues;
        private List<double> _ValueC = new List<double>(), _ValueT = new List<double>(), _Peak = new List<double>();
        public DYGSZItemPara _item = null;
        public GszMeasureWindow.HelpBox[] _helpBoxes = null;
        public static List<string> _listDetectResult = null;
        public static List<string> _listStrRecord;
        private string[] _methodToString = { "定性法", "定量法" };
        Brush _borderBrushNormal = new SolidColorBrush(Color.FromRgb(0x00, 0x7C, 0xC2));
        DateTime _date = DateTime.Now;
        MsgThread _msgThread;
        //List<TextBox> _listJudmentValue = null;
        private string[,] _CheckValue;
        private int _HoleNumber = 1;
        int _AllNumber = 0;
        public List<TextBox> _RecordValue = null;
        private DispatcherTimer _DataTimer = null;
        private tlsttResultSecondOpr _resultTable = new tlsttResultSecondOpr();
        private bool IsUpLoad = false;
        private List<ChartPlotter> _plotters = null;
        private List<HorizontalDateTimeAxis> _dateAxis = null;
        private string logType = "GszReportWindow-error";
        #endregion

        public GszReportWindow()
        {
            InitializeComponent();
            _msgThread = new MsgThread(this);
            _msgThread.Start();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (null == _item)
                return;
            try
            {
                _listDetectResult = new List<string>();
                _listStrRecord = new List<string>();
                _plotters = new List<ChartPlotter>();
                _dateAxis = new List<HorizontalDateTimeAxis>();
                int sampleNum = _item.SampleNum;
                WrapPanelChannel.Width = 0;
                // 添加布局
                for (int i = 0; i < Global.deviceHole.SxtCount; ++i)
                {
                    if (_item.Hole[i].Use)
                    {
                        UIElement element = GenerateResultLayout(i, String.Format("{0:D5}", sampleNum), _item.Hole[i].SampleName);
                        WrapPanelChannel.Children.Add(element);
                        _plotters.Add(UIUtils.GetChildObject<ChartPlotter>(element, "chartPlotter"));
                        _dateAxis.Add(UIUtils.GetChildObject<HorizontalDateTimeAxis>(element, "dateAxis"));
                        WrapPanelChannel.Width += 355;
                        sampleNum++;
                        _CheckValue = new string[_HoleNumber, 22];
                        _HoleNumber++;
                    }
                }
                // 显示结果的时候，把字符串生成
                NewShowResult();
                Result();
                if (_DataTimer == null)
                {
                    _DataTimer = new DispatcherTimer();
                    _DataTimer.Interval = TimeSpan.FromSeconds(1.5);
                    _DataTimer.Tick += new EventHandler(SaveAndUpload);
                    _DataTimer.Start();
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(3, logType, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        private string[] CalcValue(string[] values, string val)
        {
            string[] Qualifide = values;
            if (_item.dx.ComparePlus == 0)
            {
                if (Convert.ToDouble(val) >= _item.dx.PlusT)
                {
                    Qualifide[0] = "不合格";
                }
                else if (Convert.ToDouble(val) < _item.dx.MinusT)
                {
                    Qualifide[0] = "合格";
                }
                else
                {
                    Qualifide[0] = "可疑";
                }
            }
            else if (_item.dx.ComparePlus == 1)
            {
                if (Convert.ToDouble(val) < _item.dx.PlusT)
                {
                    Qualifide[0] = "不合格";
                }
                else if (Convert.ToDouble(val) >= _item.dx.MinusT)
                {
                    Qualifide[0] = "合格";
                }
                else
                {
                    Qualifide[0] = "可疑";
                }
            }

            Qualifide[3] = _item.dx.ComparePlus == 0 ? "≥" : "＜";
            return Qualifide;
        }


        private void Result()
        {
            int num = 0, idx = -1;
            try
            {
                int sampleNum = _item.SampleNum;
                for (int i = 0; i < Global.deviceHole.SxtCount; ++i)
                {
                    if (_item.Hole[i].Use)
                    {
                        idx++;
                        string[] UnqualifiedValue = new string[4];
                        string str = String.Format("{0:F}", _dataCT[i] / 60);
                        UnqualifiedValue = TestResultConserve.UnqualifiedOrQualified(str, _item.Hole[i].SampleName, _item.Name);
                        UnqualifiedValue = CalcValue(UnqualifiedValue, str);
                        //检测结果
                        List<TextBox> listCheckValue = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "textBoxDetectResult");
                        //判定结果
                        List<TextBox> listCheckResult = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "textJugmentResult");
                        //标准值
                        List<TextBox> listStandValue = UIUtils.GetChildObjects<TextBox>(WrapPanelChannel, "textStandValue");

                        listCheckValue[idx].Text = UnqualifiedValue[0].Equals("合格") ? "阴性" : (UnqualifiedValue[0].Equals("不合格") ? "阳性" : "可疑");
                        listCheckResult[idx].Text = UnqualifiedValue[0];
                        listStandValue[idx].Text = UnqualifiedValue[2];
                        _CheckValue[(num > 0 ? (i - num) : i), 0] = String.Format("{0:D2}", (i + 1));
                        _CheckValue[(num > 0 ? (i - num) : i), 1] = "干化学";
                        _CheckValue[(num > 0 ? (i - num) : i), 2] = _item.Name;
                        _CheckValue[(num > 0 ? (i - num) : i), 3] = _methodToString[_item.Method];
                        _CheckValue[(num > 0 ? (i - num) : i), 4] = listCheckValue[idx].Text;
                        _CheckValue[(num > 0 ? (i - num) : i), 5] = _item.Unit.Length == 0 ? UnqualifiedValue[4] : _item.Unit;
                        _CheckValue[(num > 0 ? (i - num) : i), 6] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        _CheckValue[(num > 0 ? (i - num) : i), 7] = LoginWindow._userAccount.UserName;
                        if (!string.IsNullOrEmpty(_item.Hole[i].SampleName))
                            _CheckValue[(num > 0 ? (i - num) : i), 8] = _item.Hole[i].SampleName;
                        else
                            _CheckValue[(num > 0 ? (i - num) : i), 8] = "";
                        _CheckValue[(num > 0 ? (i - num) : i), 9] = Convert.ToString(UnqualifiedValue[0]);
                        _CheckValue[(num > 0 ? (i - num) : i), 10] = Convert.ToString(UnqualifiedValue[2]);
                        _CheckValue[(num > 0 ? (i - num) : i), 11] = String.Format("{0:D5}", sampleNum++);
                        _CheckValue[(num > 0 ? (i - num) : i), 12] = Convert.ToString(UnqualifiedValue[1]);
                        if (_item.Hole[i].TaskName != null)
                            _CheckValue[(num > 0 ? (i - num) : i), 13] = _item.Hole[i].TaskName;
                        else
                            _CheckValue[(num > 0 ? (i - num) : i), 13] = "";
                        if (!string.IsNullOrEmpty(_item.Hole[i].CompanyName))
                            _CheckValue[(num > 0 ? (i - num) : i), 14] = _item.Hole[i].CompanyName;
                        else
                            _CheckValue[(num > 0 ? (i - num) : i), 14] = "";
                        _CheckValue[(num > 0 ? (i - num) : i), 15] = curveDatas[i];
                        _CheckValue[(num > 0 ? (i - num) : i), 16] = _item.Hole[i].SampleTypeCode;//样品种类编号
                        _CheckValue[(num > 0 ? (i - num) : i), 17] = _item.testPro;//检测项目编号
                        _CheckValue[(num > 0 ? (i - num) : i), 18] = _item.Method == 0 ? "2" : "1";//检测结果类型 1，定量，2定性
                        _CheckValue[(num > 0 ? (i - num) : i), 19] = ""; //检测结果编号 dataNum
                        _CheckValue[(num > 0 ? (i - num) : i), 20] = string.IsNullOrEmpty(_item.Hole[i].SampleId) ? string.Empty : _item.Hole[i].SampleId;
                        _CheckValue[(num > 0 ? (i - num) : i), 21] = _item.Hole[i].ProduceCompany;
                    }
                    else
                    {
                        num += 1;
                        _listStrRecord.Add(null);
                        _listDetectResult.Add(null);
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(3, logType, ex.ToString());
                MessageBox.Show(ex.Message);
            }
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
            btn_upload.IsEnabled = true;
            ButtonPrev.IsEnabled = true;
            Btn_ShowDatas.IsEnabled = true;
            _DataTimer.Stop();
            _DataTimer = null;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UpdateItem();
            _msgThread.Stop();
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private List<PrintHelper.Report> GenerateReports()
        {
            List<PrintHelper.Report> reports = new List<PrintHelper.Report>();
            try
            {
                DataTable dt = _resultTable.GetAsDataTable("", "", 6, _AllNumber);
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
                            model.ItemCategory = "干化学";
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
                FileUtils.OprLog(3, logType, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        private UIElement GenerateResultLayout(int channel, string sampleNum, string sampleName)
        {
            Border border = new Border();
            border.Width = 350;
            //border.Height = 440;
            border.Margin = new Thickness(2);
            border.BorderThickness = new Thickness(5);
            border.BorderBrush = _borderBrushNormal;
            border.CornerRadius = new CornerRadius(10);
            border.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            border.Name = "border";

            StackPanel stackPanel = new StackPanel();
            stackPanel.Width = 350;
            //stackPanel.Height = 420;
            stackPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            stackPanel.Name = "stackPanel";

            Grid grid = new Grid();
            grid.Width = 350;
            grid.Height = 40;
            grid.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;

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
            wrapPannelSampleNum.Width = 180;
            wrapPannelSampleNum.Height = 30;

            Label labelSampleNum = new Label();
            labelSampleNum.Width = 85;
            labelSampleNum.Height = 26;
            labelSampleNum.Margin = new Thickness(0, 2, 0, 0);
            labelSampleNum.FontSize = 15;
            labelSampleNum.Content = " 样品编号:";
            labelSampleNum.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            TextBox textBoxSampleNum = new TextBox();
            textBoxSampleNum.Width = 90;
            textBoxSampleNum.Height = 26;
            textBoxSampleNum.Margin = new Thickness(0, 2, 0, 2);
            textBoxSampleNum.FontSize = 15;
            textBoxSampleNum.Text = "" + sampleNum;
            textBoxSampleNum.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            textBoxSampleNum.IsReadOnly = true;

            WrapPanel wrapPannelSampleName = new WrapPanel();
            wrapPannelSampleName.Width = 180;
            wrapPannelSampleName.Height = 30;

            Label labelSampleName = new Label();
            labelSampleName.Width = 85;
            labelSampleName.Height = 26;
            labelSampleName.Margin = new Thickness(0, 2, 0, 0);
            labelSampleName.FontSize = 15;
            labelSampleName.Content = " 样品名称:";
            labelSampleName.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            TextBox textBoxSampleName = new TextBox();
            textBoxSampleName.Width = 90;
            textBoxSampleName.Height = 26;
            textBoxSampleName.Margin = new Thickness(0, 2, 0, 2);
            textBoxSampleName.FontSize = 15;
            textBoxSampleName.Text = _item.Hole[channel].SampleName;
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
            labelRGBValue.Content = " T值:";
            labelRGBValue.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            TextBox textBoxRGBValue = new TextBox();
            textBoxRGBValue.Width = 90;
            textBoxRGBValue.Height = 26;
            textBoxRGBValue.Margin = new Thickness(0, 2, 0, 2);
            textBoxRGBValue.FontSize = 15;
            textBoxRGBValue.Text = "";
            textBoxRGBValue.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            textBoxRGBValue.IsReadOnly = true;
            textBoxRGBValue.Name = "textBoxRGBValue";

            WrapPanel wrapPannelDetectResult = new WrapPanel();
            wrapPannelDetectResult.Width = 180;
            wrapPannelDetectResult.Height = 30;

            Label labelDetectResult = new Label();
            labelDetectResult.Width = 85;
            labelDetectResult.Height = 26;
            labelDetectResult.Margin = new Thickness(0, 2, 0, 0);
            labelDetectResult.FontSize = 15;
            labelDetectResult.Content = " 检测结果:";
            labelDetectResult.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            TextBox textBoxDetectResult = new TextBox();
            textBoxDetectResult.Width = 90;
            textBoxDetectResult.Height = 26;
            textBoxDetectResult.Margin = new Thickness(0, 2, 0, 2);
            textBoxDetectResult.FontSize = 15;
            textBoxDetectResult.Text = "";
            textBoxDetectResult.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            textBoxDetectResult.IsReadOnly = true;
            textBoxDetectResult.Name = "textBoxDetectResult";

            //判定结果
            WrapPanel wrapJudgemtn = new WrapPanel();
            wrapJudgemtn.Width = 180;
            wrapJudgemtn.Height = 30;

            Label labelJudgment = new Label();
            labelJudgment.Width = 85;
            labelJudgment.Height = 26;
            labelJudgment.Margin = new Thickness(0, 2, 0, 0);
            labelJudgment.FontSize = 15;
            labelJudgment.Content = " 判定结果:";
            labelJudgment.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            TextBox textJugmentResult = new TextBox();
            textJugmentResult.Width = 90;
            textJugmentResult.Height = 26;
            textJugmentResult.Margin = new Thickness(0, 2, 0, 2);
            textJugmentResult.FontSize = 15;
            textJugmentResult.Text = "";
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
            textStandValue.Text = _item.dx.PlusT.ToString();
            textStandValue.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            textStandValue.Name = "textStandValue";

            grid.Children.Add(label);
            wrapPannelSampleNum.Children.Add(labelSampleNum);
            wrapPannelSampleNum.Children.Add(textBoxSampleNum);
            wrapPannelSampleName.Children.Add(labelSampleName);
            wrapPannelSampleName.Children.Add(textBoxSampleName);

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
            stackPanel.Children.Add(wrapPannelDetectResult);
            stackPanel.Children.Add(wrapJudgemtn);

            stackPanel.Children.Add(wrapStandValue);
            border.Child = stackPanel;
            return border;
        }

        #region 曲线
        internal class BugInfo
        {
            public DateTime date;
            public int numberOpen;
            public int numberClosed;

            public BugInfo(DateTime date, int numberOpen, int numberClosed)
            {
                this.date = date;
                this.numberOpen = numberOpen;
                this.numberClosed = numberClosed;
            }
        }

        private static List<BugInfo> LoadBugInfo(string fileName)
        {
            var result = new List<BugInfo>();
            FileStream fs = new FileStream(fileName, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                string[] pieces = line.Split(':');
                DateTime d = DateTime.Parse(pieces[0]);
                int numopen = int.Parse(pieces[1]);
                int numclosed = int.Parse(pieces[2]);
                BugInfo bi = new BugInfo(d, numopen, numclosed);
                result.Add(bi);
            }
            sr.Close();
            fs.Close();
            return result;
        }

        private void plotter_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ChartPlotter chart = sender as ChartPlotter;
            Point p = e.GetPosition(this).ScreenToData(chart.Transform);
        }
        #endregion

        private List<double> _values = new List<double>();

        private void NewShowResult()
        {
            FormattingDatas();
            DrawingCurve();
        }

        private static double[] _dataCT;
        /// <summary>
        /// 格式化数据
        /// </summary>
        private void FormattingDatas()
        {
            try
            {
                _tValues = new List<double[]>();
                _dataCT = new double[_datas.Count];
                int ctIdx = -1;
                foreach (byte[] datas in _datas)
                {
                    ctIdx++;
                    if (datas != null && datas.Length > 160)
                    {
                        int length = datas.Length / 2, index = 0;
                        double[] data = new double[length];
                        for (int i = 0; i < length; i++)
                        {
                            data[i] = datas[index + 1] * 256 + datas[index];
                            if (i > 19 && i < 79)
                                _dataCT[ctIdx] += data[i];
                            index += 2;
                        }

                        int[] idx = new int[2];
                        idx[0] = 20; idx[1] = 65;
                        if (idx != null && data.Length > 85)
                        {
                            int len = idx[1] - idx[0];
                            _tValue = new double[len];
                            Array.ConstrainedCopy(data, idx[0], _tValue, 0, len);
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
                        _dataCT[ctIdx] = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(3, logType, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 存储曲线数据
        /// </summary>
        private List<string> curveDatas = new List<string>();

        /// <summary>
        /// 绘制曲线
        /// </summary>
        private void DrawingCurve()
        {
            try
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
                            pointCNum = 3,//_item.dxxx.pointCNum <= 0 ? 5 : _item.dxxx.pointCNum,
                            pointTNum = 3,//_item.dxxx.pointTNum <= 0 ? 4 : _item.dxxx.pointTNum,
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
                        catch (Exception) { }
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
                                            catch (Exception)
                                            {
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
                                catch (Exception) { continue; }
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
                                            catch (Exception)
                                            {
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
                                catch (Exception) { continue; }
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
                            //text.AppendText("C (面积:" + Math.Round(area, 2));
                            area = area > standard ? standard : area;
                            area = area / standard * proportion;
                            //text.AppendText(string.Format(" 占比:{0}%；", Math.Round(area, 2) * 100));

                            //规则曲线面积
                            //double rulesArea = Global.JtjCheck.GetRulesArea(datas, TMinIndex, TMaxIndex1, TMaxIndex2);

                            //斜率 以波谷T为原点往C值方向遍历，当出现X_(i-1)<=X_i时，即找到波峰值坐标T_i
                            standard = 200;//斜率标准为100
                            proportion = 0.25;//斜率权重25%
                            slope = Global.JtjCheck.GetSlope(datas, CMinIndex, scope);
                            //text.AppendText(string.Format("    C斜率:{0}", Math.Round(slope, 2)));
                            slope = slope > standard ? standard : slope;
                            slope = slope / standard * proportion;
                            //text.AppendText(string.Format(" 占比:{0}%；", Math.Round(slope, 2) * 100));

                            //差值 以波谷T为原点往C值方向遍历，找到波峰的值
                            standard = 2000;//差值标准为2000
                            proportion = 0.25;//差值权重25%
                            difference = Global.JtjCheck.GetDifference(datas, CMinIndex, "C");
                            //text.AppendText(string.Format("C差值:{0}", Math.Round(difference, 2)));
                            difference = difference > standard ? standard : difference;
                            difference = difference / standard * proportion;
                            //text.AppendText(string.Format(" 占比:{0}%；", Math.Round(difference, 2) * 100));

                            //有效点 有效范围<=8，以波谷T为原点往C值方向遍历，寻找有效点
                            standard = 8;//有效点标准为8
                            proportion = 0.1;//有效点权重10%
                            efficientPoint = Global.JtjCheck.GetEfficientPoint(datas, CMinIndex, scope);
                            //text.AppendText(string.Format("    C有效点:{0}", Math.Round(efficientPoint, 2)));
                            efficientPoint = efficientPoint > standard ? standard : efficientPoint;
                            efficientPoint = efficientPoint / standard * proportion;
                            //text.AppendText(string.Format(" 占比:{0}%；", Math.Round(efficientPoint, 2) * 100));

                            _ValueC[i] = (area + slope + difference + efficientPoint) * 100;
                            //text.AppendText(string.Format(" C最终结果:{0}%)", Math.Round(_ValueC[i], 2)));
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
                            //text.AppendText("\r\n\r\nT(面积:" + Math.Round(area, 2));
                            area = area > standard ? standard : area;
                            area = area / standard * proportion;
                            //text.AppendText(string.Format(" 占比:{0}%；", Math.Round(area, 2) * 100));

                            //规则曲线面积
                            //double rulesArea = Global.JtjCheck.GetRulesArea(datas, TMinIndex, TMaxIndex1, TMaxIndex2);
                            //斜率 以波谷T为原点往C值方向遍历，当出现X_(i-1)<=X_i时，即找到波峰值坐标T_i
                            standard = 200;//斜率标准为100
                            proportion = 0.25;//斜率权重25%
                            slope = Global.JtjCheck.GetSlope(datas, TMinIndex, scope);
                            //text.AppendText(string.Format("T斜率:{0}", Math.Round(slope, 2)));
                            slope = slope > standard ? standard : slope;
                            slope = slope / standard * proportion;
                            //text.AppendText(string.Format(" 占比:{0}%；", Math.Round(slope, 2) * 100));

                            //差值 以波谷T为原点往C值方向遍历，找到波峰的值
                            standard = 2000;//差值标准为2000
                            proportion = 0.25;//差值权重25%
                            difference = Global.JtjCheck.GetDifference(datas, TMinIndex, "T");
                            //text.AppendText(string.Format("T差值:{0}", Math.Round(difference, 2)));
                            difference = difference > standard ? standard : difference;
                            difference = difference / standard * proportion;
                            //text.AppendText(string.Format(" 占比:{0}%；", Math.Round(difference, 2) * 100));

                            //有效点 有效范围<=8，以波谷T为原点往C值方向遍历，寻找有效点
                            standard = 8;//有效点标准为8
                            proportion = 0.1;//有效点权重10%
                            efficientPoint = Global.JtjCheck.GetEfficientPoint(datas, TMinIndex, scope);
                            //text.AppendText(string.Format("T有效点:{0}", Math.Round(efficientPoint, 2)));
                            efficientPoint = efficientPoint > standard ? standard : efficientPoint;
                            efficientPoint = efficientPoint / standard * proportion;
                            //text.AppendText(string.Format(" 占比:{0}%；", Math.Round(efficientPoint, 2) * 100));
                            _ValueT[i] = (area + slope + difference + efficientPoint) * 100;
                            //text.AppendText(string.Format(" T最终结果:{0}%)", Math.Round(_ValueT[i], 2) * 100));
                        }
                        #endregion
                    }
                    else
                    {
                        curveDatas.Add("");
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(3, logType, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 更新样品编号
        /// </summary>
        private void UpdateItem()
        {
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
                FileUtils.OprLog(3, logType, ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        class MsgThread : ChildThread
        {
            GszReportWindow wnd;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;
            public MsgThread(GszReportWindow wnd)
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
                    FileUtils.OprLog(3, wnd.logType, ex.ToString());
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

        class GSZResult
        {
            public const int GSZF_STAT_UNKNOWN = 0x00;
            public const int GSZF_STAT_PLUS = 0x01;
            public const int GSZF_STAT_MINUS = 0x02;
            public const int GSZF_STAT_INVALID = 0x03;
            public int result = GSZF_STAT_INVALID;
            public double density = 0;
        }

        private void btn_upload_Click(object sender, RoutedEventArgs e)
        {
            if (IsUpLoad)
            {
                MessageBox.Show("当前数据已上传!", "系统提示");
                return;
            }
            Upload();
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

        private void Upload()
        {
            if (!UploadCheck()) return;

            try
            {
                LabelInfo.Content = "正在上传...";
                DataTable dt = _resultTable.GetAsDataTable("", "", 6, _AllNumber);
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
                FileUtils.OprLog(3, logType, ex.ToString());
                MessageBox.Show("上传时出现异常！\r\n异常信息：" + ex.Message, "系统提示");
            }
        }

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
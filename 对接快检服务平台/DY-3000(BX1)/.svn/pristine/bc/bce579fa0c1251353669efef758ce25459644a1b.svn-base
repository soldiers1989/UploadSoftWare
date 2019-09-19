using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using AIO.src;
using com.lvrenyang;
using DYSeriesDataSet;
using Microsoft.Windows.Controls;

namespace AIO.xaml.Record
{
    /// <summary>
    /// StatisticalAnalysisWindow.xaml 的交互逻辑
    /// Create wenj
    /// Time 2017年6月9日
    /// </summary>
    public partial class StatisticalAnalysisWindow : Window
    {
        public StatisticalAnalysisWindow()
        {
            InitializeComponent();
            this.DtStart.DisplayDateEnd = DateTime.Now;
            this.DtEnd.DisplayDateStart = DateTime.Now;
        }

        private string errMsg = string.Empty;
        private DataTable dtbl = null;
        private List<tlsTtResultSecond> modelList = null;

        /// <summary>
        /// 合格率
        /// </summary>
        private List<KeyValuePair<string, int>> PercentOfPassList = null;
        /// <summary>
        /// 检测方法
        /// </summary>
        private List<KeyValuePair<string, int>> CheckMethodList = null;
        /// <summary>
        /// 样品统计
        /// </summary>
        private List<KeyValuePair<string, int>> SamplesList = null;
        /// <summary>
        /// 检测项目
        /// </summary>
        private List<KeyValuePair<string, int>> CheckItemList = null;
        /// <summary>
        /// 检测日期趋势
        /// </summary>
        private List<KeyValuePair<string, int>> DateTrendList = null;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lineChart2.Visibility = lineChart3.Visibility = Visibility.Collapsed;
            Search();

            //检测类别
            List<string> comboList = new List<string>();
            comboList.Add("全部"); comboList.Add("分光光度"); comboList.Add("胶体金"); comboList.Add("干化学"); comboList.Add("重金属");
            ComboBoxCategory.ItemsSource = comboList;
        }

        private StringBuilder where = new StringBuilder();
        private void DtStart_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (DtStart.SelectedDate != null)
            {
                this.DtEnd.DisplayDateStart = (DateTime)DtStart.SelectedDate;
                Search();
            }
        }

        private void DtEnd_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (DtEnd.SelectedDate != null)
            {
                Search();
            }
        }

        private void Search()
        {
            where.Length = 0;
            string _strDateFrom = string.Empty;
            string _strDateTo = string.Empty;
            try
            {
                DateTime from = DtStart.SelectedDate != null ? (DateTime)DtStart.SelectedDate : new DateTime();
                DateTime to = DtEnd.SelectedDate != null ? (DateTime)DtEnd.SelectedDate : new DateTime();
                if (DtStart.SelectedDate != null && DtEnd.SelectedDate != null)
                {
                    _strDateFrom = from.ToString("yyyy-MM-dd 00:00:00");
                    _strDateTo = to.ToString("yyyy-MM-dd 23:59:59");
                    where.AppendFormat(" CheckStartDate between '{0}' and  '{1}'", _strDateFrom, _strDateTo);
                }
                else if (DtStart.SelectedDate != null && DtEnd.SelectedDate == null)
                {
                    _strDateFrom = from.ToString("yyyy-MM-dd 00:00:00");
                    where.AppendFormat(" CheckStartDate between '{0}' and  '{1}'", _strDateFrom, "9999-01-01 00:00:00");
                }
                else if (DtStart.SelectedDate == null && DtEnd.SelectedDate != null)
                {
                    _strDateTo = to.ToString("yyyy-MM-dd 23:59:59");
                    where.AppendFormat(" CheckStartDate between '{0}' and  '{1}'", "1000-01-01 00:00:00", _strDateTo);
                }

                //检测项目名称
                string val = TxtCheckItem.Text.Trim();
                if (val.Length > 0 && !val.Equals("全部"))
                {
                    where.AppendFormat(" {0} CheckTotalItem Like '%{1}%'", (where.Length == 0 ? "" : "And"), val);
                }

                //检测地点 关联被检单位
                val = TxtCheckAddress.Text.Trim();
                if (val.Length > 0)
                {
                    DataTable dt = SqlHelper.GetDataTable("tCompany", string.Format("Address Like '%{0}%'", val), out errMsg);
                    val = string.Empty;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string str = string.Empty;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            str = string.Format("'{0}'", dt.Rows[0]["FullName"]);
                            val = val.Length == 0 ? str :
                                string.Format("{0},{1}", val, str);
                        }
                    }
                    where.AppendFormat(" {0} CheckedCompany in ({1})", (where.Length == 0 ? "" : "And"), val);
                }

                //样品名称
                val = TxtFoodName.Text.Trim();
                if (val.Length > 0)
                {
                    where.AppendFormat(" {0} FoodName Like '%{1}%'", (where.Length == 0 ? "" : "And"), val);
                }

                //检测类别
                val = ComboBoxCategory.Text.Trim();
                if (val.Length > 0 && !val.Equals("全部"))
                {
                    where.AppendFormat(" {0} ResultType Like '{1}'", (where.Length == 0 ? "" : "And"), val);
                }

                //检测方法
                val = ComboBoxMethod.Text.Trim();
                if (val.Length > 0 && !val.Equals("全部"))
                {
                    where.AppendFormat(" {0} CheckMethod Like '{1}'", (where.Length == 0 ? "" : "And"), val);
                }

                where.AppendFormat(" {0} Order By CheckStartDate", (where.Length == 0 ? "1=1" : ""));
                dtbl = SqlHelper.GetDataTable("ttResultSecond", where.ToString(), out errMsg);
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    modelList = (List<tlsTtResultSecond>)IListDataSet.DataTableToIList<tlsTtResultSecond>(dtbl);
                    LbCount.Content = string.Format("检测总数：{0}", (modelList != null ? modelList.Count : 0));
                }
                else
                {
                    modelList = null;
                    LbCount.Content = "";
                    LbQualified.Content = "";
                    LbUnqualified.Content = "";
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, "statisticalAnalysis-error", ex.ToString());
                MessageBox.Show("加载数据时出现异常，请联系管理员！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                modelList = null;
            }
            ShowColumnChart(modelList);
        }

        private void ShowColumnChart(List<tlsTtResultSecond> models)
        {
            if (models == null)
            {
                columnChart.DataContext = pieChart.DataContext = areaChart.DataContext =
                    barChart.DataContext = lineChart.DataContext = null;
                return;
            }

            PercentOfPassList = new List<KeyValuePair<string, int>>();
            IDictionary<string, int> dicPercentOfPassList = new Dictionary<string, int>();

            CheckMethodList = new List<KeyValuePair<string, int>>();
            IDictionary<string, int> dicCheckMethodList = new Dictionary<string, int>();

            SamplesList = new List<KeyValuePair<string, int>>();
            IDictionary<string, int> dicSamplesList = new Dictionary<string, int>();

            CheckItemList = new List<KeyValuePair<string, int>>();
            IDictionary<string, int> dicCheckItemList = new Dictionary<string, int>();

            DateTrendList = new List<KeyValuePair<string, int>>();
            //日检测量
            IDictionary<string, int> dicDateTrendList = new Dictionary<string, int>();
            //日检测合格量
            IDictionary<string, int> dicQualifiedDateTrendList = new Dictionary<string, int>();
            //日检测不合格量
            IDictionary<string, int> dicNoQualifiedDateTrendList = new Dictionary<string, int>();

            string key = string.Empty;
            for (int i = 0; i < models.Count; i++)
            {
                //合格率
                key = models[i].Result;
                if (!dicPercentOfPassList.ContainsKey(key))
                    dicPercentOfPassList.Add(key, 1);
                else
                    dicPercentOfPassList[key] += 1;

                //检测方法
                key = models[i].CheckMethod;
                if (!dicCheckMethodList.ContainsKey(key))
                    dicCheckMethodList.Add(key, 1);
                else
                    dicCheckMethodList[key] += 1;

                //样品
                key = models[i].FoodName;
                if (!dicSamplesList.ContainsKey(key))
                    dicSamplesList.Add(key, 1);
                else
                    dicSamplesList[key] += 1;

                //检测项目
                key = models[i].CheckTotalItem;
                if (!dicCheckItemList.ContainsKey(key))
                    dicCheckItemList.Add(key, 1);
                else
                    dicCheckItemList[key] += 1;

                //日期
                key = models[i].CheckStartDate;
                DateTime dt = DateTime.Now;
                bool ishg = models[i].Result.Equals("合格") ? true : false;
                if (DateTime.TryParse(key, out dt))
                    key = dt.ToString("yyyy-MM-dd");
                else
                    key = "other";
                if (!dicDateTrendList.ContainsKey(key))
                {
                    dicDateTrendList.Add(key, 1);
                    dicQualifiedDateTrendList.Add(key, ishg ? 1 : 0);
                    dicNoQualifiedDateTrendList.Add(key, ishg ? 0 : 1);
                }
                else
                {
                    dicDateTrendList[key] += 1;
                    dicQualifiedDateTrendList[key] += ishg ? 1 : 0;
                    dicQualifiedDateTrendList[key] += ishg ? 0 : 1;
                }
            }

            //合格率
            foreach (var item in dicPercentOfPassList)
            {
                PercentOfPassList.Add(new KeyValuePair<string, int>(string.Format("{0}({1})", item.Key, item.Value), item.Value));
            }

            //检测方法
            foreach (var item in dicCheckMethodList)
            {
                CheckMethodList.Add(new KeyValuePair<string, int>(item.Key, item.Value));
            }

            //样品
            foreach (var item in dicSamplesList)
            {
                SamplesList.Add(new KeyValuePair<string, int>(item.Key, item.Value));
            }

            //检测项目
            foreach (var item in dicCheckItemList)
            {
                CheckItemList.Add(new KeyValuePair<string, int>(item.Key, item.Value));
            }

            //日期趋势
            foreach (var item in dicDateTrendList)
            {
                DateTrendList.Add(new KeyValuePair<string, int>(item.Key, item.Value));
            }

            //设置柱状图
            columnChart.DataContext = CheckMethodList;

            //饼图
            pieChart.DataContext = PercentOfPassList;

            // 区域报表图
            areaChart.DataContext = SamplesList;

            //横向柱状图
            barChart.DataContext = CheckItemList;

            //折线图
            lineChart1.ItemsSource = dicDateTrendList;
            lineChart2.ItemsSource = dicQualifiedDateTrendList;
            lineChart3.ItemsSource = dicNoQualifiedDateTrendList;

            int Qualified = 0;
            try { Qualified = dicPercentOfPassList["合格"]; }
            catch (Exception) { Qualified = 0; }
            int count = models.Count;
            LbQualified.Content = string.Format("合格数：{0}   合格率占比：{1}", Qualified, ((double)Qualified / count).ToString("P"));
            LbUnqualified.Content = string.Format("不合格数：{0}   不合格率占比：{1}", (count - Qualified), ((double)(count - Qualified) / count).ToString("P"));
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CboxAll_Click(object sender, RoutedEventArgs e)
        {
            lineChart1.Visibility = (bool)CboxAll.IsChecked ? Visibility.Visible : Visibility.Collapsed;
        }

        private void CboxQualified_Click(object sender, RoutedEventArgs e)
        {
            lineChart2.Visibility = (bool)CboxQualified.IsChecked ? Visibility.Visible : Visibility.Collapsed;
        }

        private void CboxUnqualified_Click(object sender, RoutedEventArgs e)
        {
            lineChart3.Visibility = (bool)CboxUnqualified.IsChecked ? Visibility.Visible : Visibility.Collapsed;
        }

        private void TxtCheckItem_DropDownOpened(object sender, EventArgs e)
        {

            try
            {
                ComboBox comboBoxPort = (ComboBox)sender;
                if (comboBoxPort.Items.Count == 0)
                {
                    comboBoxPort.Items.Add("全部");
                    if (modelList != null && modelList.Count > 0)
                    {
                        IDictionary<string, string> dic = new Dictionary<string, string>();
                        for (int i = 0; i < modelList.Count; i++)
                        {
                            if (!dic.ContainsKey(modelList[i].CheckTotalItem))
                            {
                                dic.Add(modelList[i].CheckTotalItem, "");
                                comboBoxPort.Items.Add(modelList[i].CheckTotalItem);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void TxtCheckAddress_DropDownOpened(object sender, EventArgs e)
        {
            try
            {
                ComboBox comboBoxPort = (ComboBox)sender;
                comboBoxPort.Items.Clear();
                if (modelList != null && modelList.Count > 0)
                {
                    DataTable dtbl = SqlHelper.GetDataTable("tCompany", string.Format("FullName Like '%{0}%'", comboBoxPort.Text.Trim()), out errMsg);
                    if (dtbl != null && dtbl.Rows.Count > 0)
                    {
                        List<clsCompany> models = (List<clsCompany>)IListDataSet.DataTableToIList<clsCompany>(dtbl);
                        IDictionary<string, string> dic = new Dictionary<string, string>();
                        for (int i = 0; i < models.Count; i++)
                        {
                            if (models[i].Address == null) continue;

                            if (!dic.ContainsKey(models[i].Address))
                            {
                                dic.Add(models[i].Address, "");
                                comboBoxPort.Items.Add(models[i].Address);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void TxtFoodName_DropDownOpened(object sender, EventArgs e)
        {
            try
            {
                ComboBox comboBoxPort = (ComboBox)sender;
                comboBoxPort.Items.Clear();
                if (modelList != null && modelList.Count > 0)
                {
                    IDictionary<string, string> dic = new Dictionary<string, string>();
                    for (int i = 0; i < modelList.Count; i++)
                    {
                        if (!dic.ContainsKey(modelList[i].FoodName))
                        {
                            dic.Add(modelList[i].FoodName, "");
                            comboBoxPort.Items.Add(modelList[i].FoodName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void TxtCheckAddress_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
        }

        private void TxtFoodName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
        }

        private void ComboBoxCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxCategory.SelectedValue != null)
            {
                ComboBoxCategory.Text = ComboBoxCategory.SelectedValue.ToString();
                setItems();
                Search();
            }
        }

        private void setItems()
        {
            IList<string> comboList = new List<string>();
            comboList.Add("全部");

            //分光光度
            if (ComboBoxCategory.SelectedIndex == 0 || ComboBoxCategory.SelectedIndex == 1)
            {
                comboList.Add("------分光光度------");
                comboList.Add("抑制率法"); comboList.Add("标准曲线法"); comboList.Add("动力学法"); comboList.Add("系数法");
            }
            //胶体金
            if (ComboBoxCategory.SelectedIndex == 0 || ComboBoxCategory.SelectedIndex == 2)
            {
                comboList.Add("------胶体金------");
                comboList.Add("定性消线法"); comboList.Add("定性比色法"); comboList.Add("定量法(T)"); comboList.Add("定量法(T/C)");
            }
            //干化学
            if (ComboBoxCategory.SelectedIndex == 0 || ComboBoxCategory.SelectedIndex == 3)
            {
                comboList.Add("------干化学------");
                comboList.Add("定性法"); comboList.Add("定量法");
            }
            ComboBoxMethod.ItemsSource = comboList;
            ComboBoxMethod.SelectedIndex = 0;
        }

        private void TxtCheckItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TxtCheckItem.SelectedValue != null)
            {
                TxtCheckItem.Text = TxtCheckItem.SelectedValue.ToString();
                Search();
            }
        }

        private void ComboBoxMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxMethod.SelectedValue != null)
            {
                ComboBoxMethod.Text = ComboBoxMethod.SelectedValue.ToString();
                Search();
            }
        }

    }
}
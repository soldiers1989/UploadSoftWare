using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using com.lvrenyang;
using DYSeriesDataSet;
using Visifire.Charts;
using System.Windows.Input;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// DataAnalysis.xaml 的交互逻辑
    /// </summary>
    public partial class DataAnalysis : Window
    {
        public DataAnalysis()
        {
            InitializeComponent();
        }

        private bool _IsFirst = false;
        /// <summary>
        /// 本次统计总数
        /// </summary>
        private int _totalCount = 0;
        /// <summary>
        /// 本次统计合格数
        /// </summary>
        private int _qualifiedCount = 0;
        /// <summary>
        /// 本次统计不合格数
        /// </summary>
        private int _unqualifiedCount = 0;
        private int _otherCount = 0;
        /// <summary>
        /// 本次统计的数据集合
        /// </summary>
        public DataTable _dataTable = null;
        private List<tlsTtResultSecond> _qualifiedList = new List<tlsTtResultSecond>();
        private List<tlsTtResultSecond> _unqualifiedList = new List<tlsTtResultSecond>();
        private List<tlsTtResultSecond> _otherList = new List<tlsTtResultSecond>();
        private int _qualified = 0;
        private int _unqualified = 0;
        private int _other = 0;
        private List<DataList> _dataList = new List<DataList>();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_dataTable != null && _dataTable.Rows.Count > 0)
            {
                List<tlsTtResultSecond> ItemList = new List<tlsTtResultSecond>();
                try
                {
                    ItemList = (List<tlsTtResultSecond>)IListDataSet.DataTableToIList<tlsTtResultSecond>(_dataTable, 1);
                    if (ItemList.Count > 0)
                    {
                        for (int i = 0; i < ItemList.Count; i++)
                        {
                            if (ItemList[i].Result != null)
                            {
                                if (ItemList[i].Result.Trim().Equals("合格"))
                                {
                                    _qualifiedList.Add(ItemList[i]);
                                    _qualified++;
                                }
                                else if (ItemList[i].Result.Trim().Equals("不合格"))
                                {
                                    _unqualified++;
                                    _unqualifiedList.Add(ItemList[i]);
                                }
                                else
                                {
                                    _other++;
                                    _otherList.Add(ItemList[i]);
                                }
                            }
                            else
                            {
                                _other++;
                                _otherList.Add(ItemList[i]);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            Assembly();
        }

        private void Assembly()
        {
            //设置ComboBox默认值
            List<string> comboList = new List<string>
            {
                "全部数据",
                "合格",
                "不合格",
                "其他"
            };
            ComboBoxMethod.ItemsSource = comboList;
            comboList = new List<string>
            {
                "数据详情",
                "图形报表"
            };
            cb_Type.ItemsSource = comboList;
            comboList = new List<string>
            {
                "竖状显示",
                "水平显示",
                "饼状图表"
            };
            ComboBoxType.ItemsSource = comboList;

            //给dataList赋值
            DataList data = new DataList()
            {
                Type = "合格",
                Count = _qualified
            };
            _dataList.Add(data);
            data = new DataList()
            {
                Type = "不合格",
                Count = _unqualified
            };
            _dataList.Add(data);
            data = new DataList()
            {
                Type = "其他",
                Count = _other
            };
            _dataList.Add(data);
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void InitializeData(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                _dataTable = dataTable;
                List<tlsTtResultSecond> ItemList = new List<tlsTtResultSecond>();
                try
                {
                    ItemList = (List<tlsTtResultSecond>)IListDataSet.DataTableToIList<tlsTtResultSecond>(_dataTable, 1);
                    if (ItemList.Count > 0)
                    {
                        for (int i = 0; i < ItemList.Count; i++)
                        {
                            if (ItemList[i].Result != null)
                            {
                                if (ItemList[i].Result.Trim().Equals("合格"))
                                    _qualifiedCount += 1;
                                else if (ItemList[i].Result.Trim().Equals("不合格"))
                                    _unqualifiedCount += 1;
                                else
                                    _otherCount += 1;
                            }
                            else
                                _otherCount += 1;
                        }
                    }
                    _totalCount = dataTable.Rows.Count;
                    double percent = Convert.ToDouble(_qualifiedCount) / Convert.ToDouble(_totalCount);
                    string result = string.Format("{0:0.00%}", percent);
                    this.labeltotalCount.Content = "本次数据统计共 " + _totalCount + " 条数据";
                    this.labelqualifiedCount.Content = "合格数: " + _qualifiedCount;
                    this.labelunqualifiedCount.Content = "不合格数: " + _unqualifiedCount;
                    this.labelotherCount.Content = "其他: " + _otherCount;
                    this.labelCount.Content = "合格率: " + result;
                    DataGridRecord.DataContext = ItemList;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                DataGridRecord.DataContext = null;
        }

        private void DataGridRecord_LoadingRow(object sender, System.Windows.Controls.DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void ComboBoxMethod_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                if (!ComboBoxMethod.SelectedValue.ToString().Equals(string.Empty) && _IsFirst)
                {
                    DataGridRecord.DataContext = null;
                    string strType = ComboBoxMethod.SelectedValue.ToString();
                    if (_dataTable != null && _dataTable.Rows.Count > 0)
                    {
                        if (strType.Equals("全部数据"))
                            DataGridRecord.DataContext = _dataTable;
                        else if (strType.Equals("合格"))
                            DataGridRecord.DataContext = _qualifiedList;
                        else if (strType.Equals("不合格"))
                            DataGridRecord.DataContext = _unqualifiedList;
                        else
                            DataGridRecord.DataContext = _otherList;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:\n" + ex.Message);
            }
        }

        /// <summary>
        /// 选择报表显示模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_Type_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                if (!cb_Type.SelectedValue.ToString().Equals(string.Empty) && _IsFirst)
                {
                    string strType = cb_Type.SelectedValue.ToString();
                    if (strType.Equals("数据详情"))
                    {
                        DataGridRecord.Visibility = Visibility.Visible;
                        LayoutRoot.Visibility = Visibility.Collapsed;
                        DataGridRecord.DataContext = _dataTable;
                        ComboBoxMethod.Visibility = Visibility.Visible;
                        ComboBoxType.Visibility = Visibility.Collapsed;
                    }
                    else if (strType.Equals("图形报表"))
                    {
                        DataGridRecord.Visibility = Visibility.Collapsed;
                        LayoutRoot.Visibility = Visibility.Visible;
                        ComboBoxMethod.Visibility = Visibility.Collapsed;
                        ComboBoxType.Visibility = Visibility.Visible;
                        ComboBoxType.Margin = new Thickness(5, 0, 0, 0);
                        CreateChart(string.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:\n" + ex.Message);
            }
        }

        Chart mychart = new Chart();
        /// <summary>
        /// 创建图形报表
        /// </summary>
        private void CreateChart(String type)
        {
            mychart = new Chart()
            {
                Width = 400,
                Height = 400
            };
            Title title = new Title()
            {
                Text = "报表数据统计分析"
            };
            mychart.Titles.Add(title);
            //统计项
            DataSeries ds = new DataSeries()
            {
                //图表类型类型
                RenderAs = RenderAs.StackedColumn,
                //显示标注
                LabelStyle = LabelStyles.OutSide,
                LabelEnabled = true
            };
            //遍历添加统计结果
            foreach (DataList data in _dataList)
                ds.DataPoints.Add(new DataPoint() { AxisXLabel = data.Type, YValue = data.Count });

            //foreach (DataPoint dt in ds.DataPoints)
            //    dt.MouseMove += new EventHandler<MouseEventArgs>(dt_MouseMove);

            if (type.Equals("竖状显示"))
            {
                ds.RenderAs = RenderAs.StackedColumn;
            }
            else if (type.Equals("水平显示"))
            {
                ds.RenderAs = RenderAs.Bar;
            }
            else if (type.Equals("饼状图表"))
            {
                ds.RenderAs = RenderAs.Pie;
            }
            mychart.View3D = true;
            mychart.Series.Add(ds);
            LayoutRoot.Children.Add(mychart);
        }

        private void dt_MouseMove(object sender, MouseEventArgs e)
        {
            MessageBox.Show((sender as DataPoint).AxisXLabel);
        }

        private class DataList
        {
            string type;
            public string Type
            {
                get { return type; }
                set { type = value; }
            }

            int count;
            public int Count
            {
                get { return count; }
                set { count = value; }
            }
        }

        private void ComboBoxMethod_LostFocus(object sender, RoutedEventArgs e)
        {
            _IsFirst = true;
        }

        private void cb_Type_LostFocus(object sender, RoutedEventArgs e)
        {
            _IsFirst = true;
        }

        private void ComboBoxType_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                if (!ComboBoxType.SelectedValue.ToString().Equals(string.Empty) && _IsFirst)
                {
                    string strType = ComboBoxType.SelectedValue.ToString();
                    LayoutRoot.Children.Remove(mychart);
                    CreateChart(strType);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:\n" + ex.Message);
            }
        }

    }
}

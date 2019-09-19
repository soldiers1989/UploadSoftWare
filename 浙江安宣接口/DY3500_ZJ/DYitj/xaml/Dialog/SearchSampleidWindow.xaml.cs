using AIO.src;
using DYSeriesDataSet;
using System;
using System.Data;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// SearchSampleidWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SearchSampleidWindow : Window
    {
        public SearchSampleidWindow()
        {
            InitializeComponent();
        }

        private string sampleId = string.Empty;

        private DataTable _dataTable = null;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Search(string.Empty, string.Empty, 1);
        }

        private void SearchCompany_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Wisdom.DeviceID.Length == 0)
                {
                    if (MessageBox.Show("设备唯一码未设置，是否立即设置仪器唯一码?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        SettingsWindow window = new SettingsWindow()
                        {
                            DeviceIdisNull = false
                        };
                        window.ShowDialog();
                    }
                    else
                    {
                        return;
                    }
                }

                sampleId = tb_SAMPLENUM.Text.Trim();
                if (sampleId.Length == 0)
                    return;

                String whereSql = sampleId.Length > 0 ? "SAMPLENUM = '" + sampleId + "'" : string.Empty,
                    orderBy = string.Empty;
                if (!Search(whereSql, orderBy, 2))
                {
                    if (MessageBox.Show("本地未找到数据，是否检索云平台是否有该快检单号?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        SearchCloud();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 根据快检单号检索云平台是否有数据
        /// </summary>
        private void SearchCloud()
        {
            try
            {
                Wisdom.GETSAMPLE_REQUEST = new DYSeriesDataSet.getsample.Request()
                {
                    deviceid = Wisdom.DeviceID,
                    sampleid = tb_SAMPLENUM.Text.Trim()
                };
                String json = Wisdom.HttpPostRequest(Wisdom.GETSAMPLE);
                JavaScriptSerializer js = new JavaScriptSerializer();
                getsample.Response model = js.Deserialize<getsample.Response>(json);
                if (model.result.Equals("0"))
                {
                    model.sampleid = sampleId;
                    Wisdom.GETSAMPLE_RESPONSE = model;
                    Insert();
                }
                else
                {
                    if (MessageBox.Show("未在云平台上检索到该快检单号，是否手动添加快检单号?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        AddSampleid();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddSampleid()
        {
            try
            {
                AddSampleidWindow window = new AddSampleidWindow()
                {
                    ShowInTaskbar = false,
                    Owner = this,
                    sampleid = tb_SAMPLENUM.Text.Trim()
                };
                window.ShowDialog();
                Search(string.Empty, string.Empty, 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 新增一条快检单号
        /// </summary>
        private int Insert()
        {
            int rtn = 0;
            try
            {
                rtn = WisdomCls.Insert();
                if (rtn == 1)
                {
                    sampleId = tb_SAMPLENUM.Text.Trim();
                    String whereSql = "SAMPLENUM = '" + sampleId + "'",
                        orderBy = string.Empty;
                    int type = 2;
                    Search(whereSql, orderBy, type);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return rtn;
        }

        /// <summary>
        /// 查询本地数据 本地有数据返回true，没有则返回false
        /// </summary>
        private Boolean Search(String whereSql, String orderBySql, int type)
        {
            this.DataGridRecord.ItemsSource = null;
            try
            {
                type = (whereSql.Length == 0 && orderBySql.Length == 0) ? 1 : 2;
                _dataTable = WisdomCls.GetAsDataTable(whereSql, orderBySql, type);
                if (_dataTable != null && _dataTable.Rows.Count > 0)
                {
                    this.DataGridRecord.ItemsSource = _dataTable.DefaultView;
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return false;
        }

        private void Choose_Click(object sender, RoutedEventArgs e)
        {
            Selected();
        }

        private void DataGridRecord_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Selected();
        }

        private void Selected()
        {
            try
            {
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    Wisdom.GETSAMPLECODE = (DataGridRecord.SelectedItem as DataRowView).Row["SAMPLENUM"].ToString();
                    Global.sampleName = (DataGridRecord.SelectedItem as DataRowView).Row["FOODNAME"].ToString();
                    Global.CompanyName = (DataGridRecord.SelectedItem as DataRowView).Row["BSAMPCOMPANY"].ToString();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("未选择快检单号!", "操作提示");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:\n" + ex.Message);
            }
        }

        private void DataGridRecord_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            AddSampleid();
        }
    }
}

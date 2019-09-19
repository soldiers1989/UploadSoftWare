using System;
using System.Data;
using System.Text;
using System.Windows;
using com.lvrenyang;
using DYSeriesDataSet;

namespace AIO
{
    /// <summary>
    /// WpfTaskFinishShow.xaml 的交互逻辑
    /// </summary>
    public partial class WpfTaskFinishShow : Window
    {
        clsTaskOpr _Tskbll;

        public WpfTaskFinishShow()
        {
            InitializeComponent();
            _Tskbll = new clsTaskOpr();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //(cdate(BAOJINGTIME)>=#2015-10-26 22:21:23#)
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                stringBuilder.AppendFormat("  (cdate(BAOJINGTIME)< #{0}#)", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                DataTable dt = _Tskbll.GetAsDataTable(stringBuilder.ToString(), "", 3);
                DataView nev = new DataView();
                nev = dt.DefaultView;
                nev.RowFilter = "Num <1";
                this.DataGridRecord.ItemsSource = nev;
            }
            catch (Exception ex)
            {
                FileUtils.Log("WpfTaskFinishShow-Window_Loaded:" + ex.Message + "\r\n\r\n详细信息:" + ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DataGridRecord_LoadingRow(object sender, System.Windows.Controls.DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

    }
}
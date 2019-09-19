using System;
using System.Windows;
using System.Windows.Controls;
using com.lvrenyang;
using DYSeriesDataSet;
using DYSeriesDataSet.DataModel;

namespace AIO.xaml.Print
{
    /// <summary>
    /// UpdateReportWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UpdateReportWindow : Window
    {
        public UpdateReportWindow()
        {
            InitializeComponent();
        }

        private tlsttResultSecondOpr _resultTable = new tlsttResultSecondOpr();
        private int _id = 0;
        private bool _isUpdate = false;
        private string logType = "print-error";

        /// <summary>
        /// 初始化界面的值
        /// </summary>
        /// <param name="report"></param>
        public void GetValues(clsReport report)
        {
            if (report != null)
            {
                try
                {
                    _id = report.ID;
                    if (report.Title != null && !report.Title.Equals(""))
                        this.textBoxTitle.Text = report.Title;
                    this.textBoxCheckUnitName.Text = report.CheckUnitName;
                    this.textBoxTrademark.Text = report.Trademark;
                    this.textBoxSpecifications.Text = report.Specifications;
                    this.textBoxProductionDate.Text = report.ProductionDate;
                    this.textBoxQualityGrade.Text = report.QualityGrade;
                    this.textBoxTaskSource.Text = report.TaskSource;
                    this.textBoxCheckedCompanyName.Text = report.CheckedCompanyName;
                    this.textBoxCheckedCompanyPhone.Text = report.CheckedCompanyPhone;
                    this.textBoxProductionUnitsName.Text = report.ProductionUnitsName;
                    this.textBoxProductionUnitsPhone.Text = report.ProductionDate;
                    this.textBoxStandard.Text = report.Standard;
                    this.textBoxSamplingData.Text = report.SamplingData;
                    this.textBoxSampleNum.Text = report.SampleNum;
                    this.textBoxSamplingCode.Text = report.SamplingCode;
                    this.textBoxSampleArrivalData.Text = report.SampleArrivalData;
                    this.textBoxNote.Text = report.Notes;
                    if (report.reportList != null && report.reportList.Count > 0)
                        DataGridRecord.DataContext = report.reportList;
                    else
                        DataGridRecord.DataContext = null;
                }
                catch (Exception ex)
                {
                    FileUtils.OprLog(6, logType, ex.ToString());
                    MessageBox.Show("异常(GetValues):\n" + ex.Message);
                }
            }
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            if (_isUpdate)
            {
                if (MessageBox.Show("是否保存当前内容?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    btnSave_Click(null, null);
                }
                else
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void DataGridRecord_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (this.textBoxTitle.Text.Trim().ToString().Equals(""))
            {
                this.textBoxTitle.Focus();
                MessageBox.Show("请填写报表标题！");
                return;
            }
            Save();
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            string error = "";
            clsReport report = new clsReport();
            try
            {
                report.ID = _id;
                report.Title = this.textBoxTitle.Text.Trim();
                report.Trademark = this.textBoxTrademark.Text.Trim();
                report.CheckUnitName = this.textBoxCheckUnitName.Text.Trim();
                report.Specifications = this.textBoxSpecifications.Text.Trim();
                report.ProductionDate = this.textBoxProductionDate.Text.Trim();
                report.QualityGrade = this.textBoxQualityGrade.Text.Trim();
                report.TaskSource = this.textBoxTaskSource.Text.Trim();
                report.CheckedCompanyName = this.textBoxCheckedCompanyName.Text.Trim();
                report.CheckedCompanyPhone = this.textBoxCheckedCompanyPhone.Text.Trim();
                report.ProductionUnitsName = this.textBoxProductionUnitsName.Text.Trim();
                report.ProductionUnitsPhone = this.textBoxProductionUnitsPhone.Text.Trim();
                report.Standard = this.textBoxStandard.Text.Trim();
                report.SamplingData = this.textBoxSamplingData.Text.Trim();
                report.SampleNum = this.textBoxSampleNum.Text.Trim();
                report.SamplingCode = this.textBoxSamplingCode.Text.Trim();
                report.SampleArrivalData = this.textBoxSampleArrivalData.Text.Trim();
                report.Notes = this.textBoxNote.Text.Trim();
                report.IsDeleted = "N";
                _resultTable.Update(report, out error);
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("出现异常!\n" + ex.Message.ToString());
            }
            finally
            {
                if (error.Equals(""))
                {
                    if (MessageBox.Show("保存成功！是否返回【报表界面】？！", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("保存失败！\n" + error);
                }
            }
        }

        /// <summary>
        /// 禁止输入非数字字符
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckNum(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            TextChange[] change = new TextChange[e.Changes.Count];
            e.Changes.CopyTo(change, 0);
            int offset = change[0].Offset;
            if (change[0].AddedLength > 0)
            {
                long num = 0;
                if (!long.TryParse(textBox.Text, out num))
                {
                    textBox.Text = textBox.Text.Remove(offset, change[0].AddedLength);
                    textBox.Select(offset, 0);
                }
            }
        }

        private void textBoxSampleNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckNum(sender, e);
            _isUpdate = true;
        }

        private void textBoxCheckedCompanyPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckNum(sender, e);
            _isUpdate = true;
        }

        private void textBoxProductionUnitsPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckNum(sender, e);
            _isUpdate = true;
        }

        private void dtpDateTime_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.textBoxSampleArrivalData.Text = dtpDateTime.Text;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _isUpdate = false;
        }

        private void textBoxCheckUnitName_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void textBoxTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void textBoxStandard_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void textBoxCheckedCompanyName_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void textBoxProductionUnitsName_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void textBoxQualityGrade_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void textBoxSpecifications_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void textBoxProductionDate_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void textBoxTrademark_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void textBoxSamplingData_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void textBoxSamplingCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void textBoxSampleArrivalData_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void textBoxTaskSource_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

    }
}
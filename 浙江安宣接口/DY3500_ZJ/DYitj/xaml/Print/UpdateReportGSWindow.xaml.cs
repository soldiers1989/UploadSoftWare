using System;
using System.Windows;
using DYSeriesDataSet.DataModel;
using DYSeriesDataSet;

namespace AIO.xaml.Print
{
    /// <summary>
    /// UpdateReportGSWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UpdateReportGSWindow : Window
    {
        public UpdateReportGSWindow()
        {
            InitializeComponent();
        }

        int _id = 0;

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public void GetValues(clsReportGS report) 
        {
            if (report != null)
            {
                try
                {
                    _id = report.ID;
                    if (report.Title != null && !report.Title.Equals(string.Empty))
                        this.textBoxTitle.Text = report.Title;
                    this.textBoxFoodName.Text = report.FoodName; ;
                    this.textBoxFoodType.Text = report.FoodType;
                    this.textBoxCheckedCompanyName.Text = report.CheckedCompanyName;
                    this.textBoxCheckedCompanyAddress.Text = report.CheckedCompanyAddress;
                    this.textBoxCheckedCompanyPhone.Text = report.CheckedCompanyPhone;
                    this.textBoxLabelProducerName.Text = report.LabelProducerName;
                    this.textBoxLabelProducerAddress.Text = report.LabelProducerAddress;
                    this.textBoxLabelProducerPhone.Text = report.LabelProducerPhone;
                    this.textBoxSamplingData.Text = report.SamplingData;
                    this.textBoxSamplingPerson.Text = report.SamplingPerson;
                    this.textBoxSampleNum.Text = report.SampleNum;
                    //this.textBoxSamplingBase.Text = report.SamplingOrderCode;
                    this.textBoxStandard.Text = report.Standard;
                    this.textBoxTInspectionConclusion.Text = report.InspectionConclusion;
                    if (report.reportList != null && report.reportList.Count > 0)
                        DataGridRecord.DataContext = report.reportList;
                    else
                        DataGridRecord.DataContext = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("异常(GetValues):\n" + ex.Message);
                }
            }
        }

        private void DataGridRecord_LoadingRow(object sender, System.Windows.Controls.DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void dtpDateTime_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.textBoxSamplingData.Text = dtpDateTime.Text;
        }

        /// <summary>
        /// 保存报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string error = string.Empty;
            clsReportGS report = new clsReportGS();
            try
            {
                report.ID = _id;
                report.Title = this.textBoxTitle.Text.Trim();
                report.FoodName = this.textBoxFoodName.Text.Trim();
                report.FoodType = this.textBoxFoodType.Text.Trim();
                report.ProductionDate = this.textBoxProductionDate.Text.Trim();
                report.CheckedCompanyName = this.textBoxCheckedCompanyName.Text.Trim();
                report.CheckedCompanyAddress = this.textBoxCheckedCompanyAddress.Text.Trim();
                report.CheckedCompanyPhone = this.textBoxCheckedCompanyPhone.Text.Trim();
                report.CheckedCompanyName = this.textBoxCheckedCompanyName.Text.Trim();
                report.CheckedCompanyPhone = this.textBoxCheckedCompanyPhone.Text.Trim();
                report.LabelProducerName = this.textBoxLabelProducerName.Text.Trim();
                report.LabelProducerAddress = this.textBoxLabelProducerAddress.Text.Trim();
                report.LabelProducerPhone = this.textBoxLabelProducerPhone.Text.Trim();
                report.SamplingData = this.textBoxSamplingData.Text.Trim();
                report.SamplingPerson = this.textBoxSamplingPerson.Text.Trim();
                report.SampleNum = this.textBoxSampleNum.Text.Trim();
                report.SamplingOrderCode = this.textBoxSamplingOrderCode.Text.Trim();
                report.Standard = this.textBoxStandard.Text.Trim();
                report.InspectionConclusion = this.textBoxTInspectionConclusion.Text.Trim();
                new tlsttResultSecondOpr().Update(report, out error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("出现异常!\n" + ex.Message.ToString());
            }
            finally
            {
                if (error.Equals(string.Empty))
                {
                    if (MessageBox.Show("保存成功!是否返回【报表界面】?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("保存失败!\n" + error);
                }
            }
        }

        private void datePickerProductionDate_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (!datePickerProductionDate.Text.Equals(string.Empty))
            {
                this.textBoxProductionDate.Text = datePickerProductionDate.Text.Trim();
            }
        }
    }
}

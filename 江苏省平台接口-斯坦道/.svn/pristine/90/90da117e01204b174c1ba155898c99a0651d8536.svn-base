using DYSeriesDataSet;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AIO
{
    public delegate void ChangeTextHandler();
    /// <summary>
    /// ItemResultEdit.xaml 的交互逻辑
    /// </summary>
    public partial class ItemResultEdit : Window
    {
        public ChangeTextHandler _changeTextEnvet;
        private string _IsUpload = string.Empty;
        private int _ID = 0;
        private tlsttResultSecondOpr _bll = new tlsttResultSecondOpr();

        public ItemResultEdit()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string dt = clsCompanyOpr.NameFromCode(tb_CheckedCompany.Text);
            tb_CheckedCompanyCode.Text = dt;
            tb_CheckNo.IsReadOnly = true;
            tb_TaskNumber.IsReadOnly = true;
            tb_CheckTotalItem.IsReadOnly = true;
            tb_Standard.IsReadOnly = true;
            tb_ResultInfo.IsReadOnly = true;
            tb_FoodName.IsReadOnly = true;
            tb_SampleCode.IsReadOnly = true;
            tb_CheckStartDate.IsReadOnly = true;
            tb_StandValue.IsReadOnly = true;
            tb_Organizer.IsReadOnly = true;
            tb_APRACategory.IsReadOnly = true;
            tb_CheckedCompany.IsReadOnly = true;
            tb_CheckedCompanyCode.IsReadOnly = true;
            tb_UpLoader.IsReadOnly = true;
            tb_UploadDate.IsReadOnly = true;
            tb_TakeDate.IsReadOnly = true;
            tb_DateManufacture.IsReadOnly = true;
            tReportDeliTime.IsReadOnly = true;
            tCheckMachine.IsReadOnly = true;
            tCheckMachineModel.IsReadOnly = true;
            tMachineCompany.IsReadOnly = true;
            if (Global.IsUpdateChekcedValue)
            {
                tb_CheckValueInfo.IsReadOnly = false;//检测值
                tb_CheckValueInfo.IsEnabled = true;
                tb_Result.IsReadOnly = false;//结论
                tb_Result.IsEnabled = true;
                tb_CheckNo.IsReadOnly = false;//检测编号
                tb_CheckNo.IsEnabled = true;
                tb_TaskNumber.IsReadOnly = false;//任务编号
                tb_TaskNumber.IsEnabled = true;
                tb_CheckTotalItem.IsReadOnly = false;//检测项目
                tb_CheckTotalItem.IsEnabled = true;
                tb_Standard.IsReadOnly = false;//检测依据
                tb_Standard.IsEnabled = true;
                tb_CheckUnitName.IsReadOnly = false;//检测单位
                tb_CheckUnitName.IsEnabled = true;
                tb_APRACategory.IsReadOnly = false;//检测单位类别
                tb_APRACategory.IsEnabled = true;
                tb_CheckedCompany.IsReadOnly = false;//被检单位
                tb_CheckedCompany.IsEnabled = true;
                tb_CheckedCompanyCode.IsReadOnly = false;//被检单位编号
                tb_CheckedCompanyCode.IsEnabled = true;
                tb_CheckStartDate.IsReadOnly = false;//检测时间
                tb_CheckStartDate.IsEnabled = true;
                tb_Organizer.IsReadOnly = false;//抽样人
                tb_Organizer.IsEnabled = true;
                tb_FoodName.IsReadOnly = false;//样品名称
                tb_FoodName.IsEnabled = true;
                tb_SampleCode.IsReadOnly = false;//样品编号
                tb_SampleCode.IsEnabled = true;
                tb_StandValue.IsReadOnly = false;//标准值
                tb_StandValue.IsEnabled = true;
                tb_ResultInfo.IsReadOnly = false;//检测值单位
                tb_ResultInfo.IsEnabled = true;
                tb_UpLoader.IsReadOnly = false;//基层上传人
                tb_UpLoader.IsEnabled = true;
                

            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string ourEgg = string.Empty;
            tlsTtResultSecond model = new tlsTtResultSecond();
            try
            {
                model.CheckNo = tb_CheckNo.Text.Trim();//检测编号
                model.CheckStartDate = this.tb_CheckStartDate.Text.Trim();
                model.ReportDeliTime = tReportDeliTime.Text.Trim();//检测报告送达时间
                model.ProceResults = tProceResults.Text.Trim();//处理结果
                model.CheckPlaceCode = tCheckPlaceCode.Text.Trim();//行政机构编号
                model.CheckPlace = tCheckPlace.Text.Trim();//行政机构名称
                model.TakeDate = tb_TakeDate.Text.Trim();//抽检日期
                model.DateManufacture = tb_DateManufacture.Text.Trim();//生产日期            
                model.SamplingPlace = tb_SamplingPlace.Text.Trim();//抽样地点
                model.CheckUnitName = tb_CheckUnitName.Text.Trim();//检测单位
                model.CheckType = cb_CheckType.Text.Trim();//检测类别
                model.CheckedComDis = tCheckedComDis.Text.Trim();//档口/车牌号/门牌号
                model.APRACategory = tb_APRACategory.Text.Trim();//检测单位类别
                model.CheckedCompanyCode = tb_CheckedCompanyCode.Text.Trim();//被检对象编号
                model.IsUpload = _IsUpload;//是否上传
                model.ID = _ID;
                model.CheckValueInfo = tb_CheckValueInfo.Text.Trim();//检测值
                model.Result = tb_Result.Text.Trim();//结论
                model.CheckNo = tb_CheckNo.Text.Trim();//检测编号
                model.CheckPlanCode = tb_TaskNumber.Text.Trim();//任务编号
                model.CheckTotalItem = tb_CheckTotalItem.Text.Trim();//检测项目
                model.Standard = tb_Standard.Text.Trim();//检测依据
                model.CheckUnitName = tb_CheckUnitName.Text.Trim();//检测单位
                model.APRACategory = tb_APRACategory.Text.Trim();//检测单位类别
                model.CheckedCompany = tb_CheckedCompany.Text.Trim();//被检单位
                model.CheckedCompanyCode = tb_CheckedCompanyCode.Text.Trim();//被检单位编号
                model.CheckStartDate = tb_CheckStartDate.Text.Trim();//检测时间
                model.Organizer = tb_Organizer.Text.Trim();//抽样人
                model.FoodName = tb_FoodName.Text.Trim();//样品名称
                model.SampleCode = tb_SampleCode.Text.Trim();//样品编号
                model.StandValue = tb_StandValue.Text.Trim();//标准值
                model.ResultInfo = tb_ResultInfo.Text.Trim();//检测值单位
                model.UpLoader = tb_UpLoader.Text.Trim();//基层上传人
                model.IsReconsider = (bool)tIsReconsider.IsChecked;//是否提出复议(是、否)
                model.ReconsiderTime = tReconsiderTime.Text;
                model.ProduceCompany = tb_ProduceCompany.Text.Trim();//生产单位
                _bll.UpdatePart(model, out ourEgg);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存数据时出现异常!\r\n异常信息：" + (ourEgg.Equals(string.Empty) ? ex.Message : ourEgg));
            }
            finally
            {
                if (ourEgg.Equals(string.Empty))
                {
                    if (MessageBox.Show("保存成功!\r\n是否返回【检测记录查询】界面?!", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        this.Close();
                }
                else
                    MessageBox.Show("保存数据时出现异常!\r\n异常信息：" + ourEgg);
            }
        }

        internal void GetValue(tlsTtResultSecond model)
        {
            tb_CheckNo.Text = model.CheckNo;//检测编号
            tb_TaskNumber.Text = model.CheckPlanCode;//任务编号
            tb_CheckTotalItem.Text = model.CheckTotalItem;//检测项目
            tb_Standard.Text = model.Standard;//检测依据
            tb_ResultInfo.Text = model.ResultInfo;//检测值单位
            tb_FoodName.Text = model.FoodName;//被检样品名称
            tb_SampleCode.Text = model.SampleCode;//样品编号
            tb_CheckValueInfo.Text = model.CheckValueInfo;//检测值
            tb_StandValue.Text = model.StandValue;//检测值标准
            tb_Result.Text = model.Result;//检测结论
            tb_DateManufacture.Text = model.DateManufacture;//生产日期
            tb_TakeDate.Text = model.TakeDate;//抽检日期
            tb_Organizer.Text = model.Organizer;//抽样人
            tb_CheckUnitName.Text = model.CheckUnitName;
            tb_SamplingPlace.Text = model.SamplingPlace;//抽样地点
            cb_CheckType.Text = model.CheckType;//检测类别
            tb_CheckedCompany.Text = model.CheckedCompany;//被检对象名称
            tb_CheckedCompanyCode.Text = model.CheckedCompanyCode;//被检对象编码
            tCheckedComDis.Text = model.CheckedComDis;//档口/门牌/车牌
            tb_UpLoader.Text = model.UpLoader;//基层上传人
            tb_UploadDate.Text = string.Empty;//基层上传时间
            tb_CheckStartDate.Text = model.CheckStartDate;//检测时间
            tCheckPlace.Text = model.CheckPlace;//行政机构名称
            tCheckPlaceCode.Text = model.CheckPlaceCode;//行政机构编码
            tCheckMachine.Text = model.CheckMachine;//检测设备
            tCheckMachineModel.Text = model.CheckMachineModel;//检测设备型号
            tMachineCompany.Text = model.MachineCompany;//设备生产厂家
            tProceResults.Text = model.ProceResults;//处理结果
            tb_APRACategory.Text = model.APRACategory;//检测单位类别
            tReportDeliTime.Text = model.ReportDeliTime;//检测报告送达时间
            tIsReconsider.IsChecked = model.IsReconsider;//是否提出复议(是、否)
            tReconsiderTime.Text = model.ReconsiderTime;//提出复议时间
            tb_ProduceCompany.Text = model.ProduceCompany;//生产单位
            _IsUpload = model.IsUpload;
            _ID = model.ID;
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            if (_changeTextEnvet != null)
                _changeTextEnvet();
            this.Close();
        }

        private void datePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            tReportDeliTime.Text = datePicker1.DisplayDate.ToString("yyyy-MM-dd ") + DateTime.Now.ToString("HH:mm:ss");
        }

        private void datePicker3_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            tb_DateManufacture.Text = datePicker3.DisplayDate.ToString("yyyy-MM-dd ") + DateTime.Now.ToString("HH:mm:ss"); ;
        }

        private void datePicker4_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            tb_TakeDate.Text = datePicker4.DisplayDate.ToString("yyyy-MM-dd ") + DateTime.Now.ToString("HH:mm:ss");
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cb_CheckType.SelectedIndex == 1)
                cb_CheckType.Text = "送检";
            if (cb_CheckType.SelectedIndex == 0)
                cb_CheckType.Text = "抽检";
        }

        /// <summary>
        /// 点击查看相关法律法规
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_View_Click(object sender, RoutedEventArgs e)
        {
            string pdfUrl = string.Empty, showStr = "未找到相关法律法规!";
            if (!tb_Standard.Text.Trim().Equals(string.Empty))
            {
                try
                {
                    pdfUrl = Global.PdfAddress + tb_Standard.Text.Trim() + ".pdf";
                    System.Diagnostics.Process.Start(pdfUrl);
                }
                catch (Exception)
                {
                    MessageBox.Show(showStr, "系统提示");
                }
            }
            else
            {
                MessageBox.Show(showStr, "系统提示");
            }
        }

        private void datePicker2_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            tReconsiderTime.Text = datePicker2.DisplayDate.ToString("yyyy-MM-dd ") + DateTime.Now.ToString("HH:mm:ss");
        }

    }
}

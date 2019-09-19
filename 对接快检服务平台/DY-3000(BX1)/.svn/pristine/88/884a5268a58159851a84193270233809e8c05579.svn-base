using System.Windows;

namespace AIO.xaml.Record
{
    /// <summary>
    /// PrintPreview.xaml 的交互逻辑
    /// </summary>
    public partial class PrintPreview : Window
    {
        public PrintPreview()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 恢复默认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Return_Click(object sender, RoutedEventArgs e)
        {
            Cb_IsInstrumentName.IsChecked = true;
            Cb_IsCompany.IsChecked = true;
            Cb_IsItemCategory.IsChecked = true;
            Cb_IsDate.IsChecked = true;
            Cb_IsUnit.IsChecked = true;
            Cb_IsUser.IsChecked = true;
            Cb_IsReviewers.IsChecked = true;
            Cb_IsPrintQR.IsChecked = true;
        }

        /// <summary>
        /// 保存格式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            PrintHelper.PrintInstrumentName = (bool)Cb_IsInstrumentName.IsChecked;
            PrintHelper.PrintCompany = (bool)Cb_IsCompany.IsChecked;
            PrintHelper.PrintItemCategory = (bool)Cb_IsItemCategory.IsChecked;
            PrintHelper.PrintDate = (bool)Cb_IsDate.IsChecked;
            PrintHelper.PrintUnit = (bool)Cb_IsUnit.IsChecked;
            PrintHelper.PrintUser = (bool)Cb_IsUser.IsChecked;
            PrintHelper.PrintReviewers = (bool)Cb_IsReviewers.IsChecked;
            Global.PrintQrCode = PrintHelper.PrintQR = (bool)Cb_IsPrintQR.IsChecked;
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Cb_IsInstrumentName.IsChecked = PrintHelper.PrintInstrumentName;
            Cb_IsCompany.IsChecked = PrintHelper.PrintCompany;
            Cb_IsItemCategory.IsChecked = PrintHelper.PrintItemCategory;
            Cb_IsDate.IsChecked = PrintHelper.PrintDate;
            Cb_IsUnit.IsChecked = PrintHelper.PrintUnit;
            Cb_IsUser.IsChecked = PrintHelper.PrintUser;
            Cb_IsReviewers.IsChecked = PrintHelper.PrintReviewers;
            Cb_IsPrintQR.IsChecked = PrintHelper.PrintQR;
        }

    }
}
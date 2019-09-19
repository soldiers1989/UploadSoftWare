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
            Cb_IsPrintCode.IsChecked = true;

            PrintHelper.IsInstrumentName = (bool)Cb_IsInstrumentName.IsChecked;
            PrintHelper.IsCompany = (bool)Cb_IsCompany.IsChecked;
            PrintHelper.IsItemCategory = (bool)Cb_IsItemCategory.IsChecked;
            PrintHelper.IsDate = (bool)Cb_IsDate.IsChecked;
            PrintHelper.IsUnit = (bool)Cb_IsUnit.IsChecked;
            PrintHelper.IsUser = (bool)Cb_IsUser.IsChecked;
            PrintHelper.IsReviewers = (bool)Cb_IsReviewers.IsChecked;
            //Global.IsPrintCode = PrintHelper.IsPrintCode = (bool)Cb_IsPrintCode.IsChecked;
            PrintHelper.IsPrintCode = (bool)Cb_IsPrintCode.IsChecked;

            Global.Printing[0].PrintInstrumentName = PrintHelper.IsInstrumentName;
            Global.Printing[0].PrintCompany = PrintHelper.IsCompany;
            Global.Printing[0].PrintItemCategory = PrintHelper.IsItemCategory;
            Global.Printing[0].PrintDate = PrintHelper.IsDate;
            Global.Printing[0].PrintUnit = PrintHelper.IsUnit;
            Global.Printing[0].PrintUser = PrintHelper.IsUser;
            Global.Printing[0].PrintReviewers = PrintHelper.IsReviewers;
            Global.Printing[0].PrintQR = PrintHelper.IsPrintCode;

            Global.SerializeToFile(Global.Printing, Global.PrintViewFile);
        }

        /// <summary>
        /// 保存格式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            PrintHelper.IsInstrumentName = (bool)Cb_IsInstrumentName.IsChecked;
            PrintHelper.IsCompany = (bool)Cb_IsCompany.IsChecked;
            PrintHelper.IsItemCategory = (bool)Cb_IsItemCategory.IsChecked;
            PrintHelper.IsDate = (bool)Cb_IsDate.IsChecked;
            PrintHelper.IsUnit = (bool)Cb_IsUnit.IsChecked;
            PrintHelper.IsUser = (bool)Cb_IsUser.IsChecked;
            PrintHelper.IsReviewers = (bool)Cb_IsReviewers.IsChecked;
            PrintHelper.IsPrintCode = (bool)Cb_IsPrintCode.IsChecked;
            PrintHelper.isCompany = (bool)Cb_IsCompany.IsChecked;

            if (Global.Printing != null && Global.Printing.Count > 0)
            {
                Global.Printing[0].PrintInstrumentName = PrintHelper.IsInstrumentName;
                Global.Printing[0].PrintCompany = PrintHelper.IsCompany;
                Global.Printing[0].PrintItemCategory = PrintHelper.IsItemCategory;
                Global.Printing[0].PrintDate = PrintHelper.IsDate;
                Global.Printing[0].PrintUnit = PrintHelper.IsUnit;
                Global.Printing[0].PrintUser = PrintHelper.IsUser;
                Global.Printing[0].PrintReviewers = PrintHelper.IsReviewers;
                Global.Printing[0].PrintQR = PrintHelper.IsPrintCode;
       
            }
            else
            {
                Prints pp = new Prints();

                pp.PrintInstrumentName = PrintHelper.IsInstrumentName;
                pp.PrintCompany = PrintHelper.IsCompany;
                pp.PrintItemCategory = PrintHelper.IsItemCategory;
                pp.PrintDate = PrintHelper.IsDate;
                pp.PrintUnit = PrintHelper.IsUnit;
                pp.PrintUser = PrintHelper.IsUser;
                pp.PrintReviewers = PrintHelper.IsReviewers;
                pp.PrintQR = PrintHelper.IsPrintCode;

                Global.Printing.Add(pp);
            }
            Global.SerializeToFile(Global.Printing, Global.PrintViewFile);

            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Cb_IsInstrumentName.IsChecked = PrintHelper.IsInstrumentName;
            //Cb_IsCompany.IsChecked = PrintHelper.IsCompany;
            //Cb_IsItemCategory.IsChecked = PrintHelper.IsItemCategory;
            //Cb_IsDate.IsChecked = PrintHelper.IsDate;
            //Cb_IsUnit.IsChecked = PrintHelper.IsUnit;
            //Cb_IsUser.IsChecked = PrintHelper.IsUser;
            //Cb_IsReviewers.IsChecked = PrintHelper.IsReviewers;
            if (Global.Printing != null && Global.Printing.Count > 0)
            {
                //PrintHelper.PrintInstrumentName = Global.Printing[0].PrintInstrumentName;
                //PrintHelper.PrintCompany = Global.Printing[0].PrintCompany;
                //PrintHelper.PrintItemCategory = Global.Printing[0].PrintItemCategory;
                //PrintHelper.PrintDate = Global.Printing[0].PrintDate;
                //PrintHelper.PrintUnit = Global.Printing[0].PrintUnit;
                //PrintHelper.PrintUser = Global.Printing[0].PrintUser;
                //PrintHelper.PrintReviewers = Global.Printing[0].PrintReviewers;
                //Global.PrintQrCode = PrintHelper.PrintQR = Global.Printing[0].PrintQR;

                Cb_IsInstrumentName.IsChecked = PrintHelper.IsInstrumentName = Global.Printing[0].PrintInstrumentName;
                Cb_IsCompany.IsChecked = PrintHelper.IsCompany = Global.Printing[0].PrintCompany;
                Cb_IsItemCategory.IsChecked = PrintHelper.IsItemCategory = Global.Printing[0].PrintItemCategory;
                Cb_IsDate.IsChecked = PrintHelper.IsDate = Global.Printing[0].PrintDate;
                Cb_IsUnit.IsChecked = PrintHelper.IsUnit = Global.Printing[0].PrintUnit;
                Cb_IsUser.IsChecked = PrintHelper.IsUser = Global.Printing[0].PrintUser;
                Cb_IsReviewers.IsChecked = PrintHelper.IsReviewers = Global.Printing[0].PrintReviewers;
                Cb_IsPrintCode.IsChecked = PrintHelper.IsPrintCode = Global.Printing[0].PrintQR;
            }
        }

    }
}
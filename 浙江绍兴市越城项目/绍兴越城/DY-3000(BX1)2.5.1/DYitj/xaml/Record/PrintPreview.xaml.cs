using System.Windows;

namespace AIO.xaml.Record
{
    /// <summary>
    /// PrintPreview.xaml 的交互逻辑
    /// </summary>
    public partial class PrintPreview : Window
    {
        public Prints _Print= new Prints();
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
            PrintHelper.PrintInstrumentName = (bool)Cb_IsInstrumentName.IsChecked;
            PrintHelper.PrintCompany = (bool)Cb_IsCompany.IsChecked;
            PrintHelper.PrintItemCategory = (bool)Cb_IsItemCategory.IsChecked;
            PrintHelper.PrintDate = (bool)Cb_IsDate.IsChecked;
            PrintHelper.PrintUnit = (bool)Cb_IsUnit.IsChecked;
            PrintHelper.PrintUser = (bool)Cb_IsUser.IsChecked;
            PrintHelper.PrintReviewers = (bool)Cb_IsReviewers.IsChecked;
            Global.PrintQrCode = PrintHelper.PrintQR = (bool)Cb_IsPrintQR.IsChecked;

            Global.Printing[0].PrintInstrumentName = PrintHelper.PrintInstrumentName;
            Global.Printing[0].PrintCompany = PrintHelper.PrintCompany;
            Global.Printing[0].PrintItemCategory = PrintHelper.PrintItemCategory;
            Global.Printing[0].PrintDate = PrintHelper.PrintDate;
            Global.Printing[0].PrintUnit = PrintHelper.PrintUnit;
            Global.Printing[0].PrintUser = PrintHelper.PrintUser;
            Global.Printing[0].PrintReviewers = PrintHelper.PrintReviewers;
            Global.Printing[0].PrintQR = PrintHelper.PrintQR;

            Global.SerializeToFile(Global.Printing, Global.PrintViewFile);
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
            PrintHelper.PrintQR = (bool)Cb_IsPrintQR.IsChecked;
            if (Global.Printing != null && Global.Printing.Count > 0)
            {
                Global.Printing[0].PrintInstrumentName = PrintHelper.PrintInstrumentName;
                Global.Printing[0].PrintCompany = PrintHelper.PrintCompany;
                Global.Printing[0].PrintItemCategory = PrintHelper.PrintItemCategory;
                Global.Printing[0].PrintDate = PrintHelper.PrintDate;
                Global.Printing[0].PrintUnit = PrintHelper.PrintUnit;
                Global.Printing[0].PrintUser = PrintHelper.PrintUser;
                Global.Printing[0].PrintReviewers = PrintHelper.PrintReviewers;
                Global.Printing[0].PrintQR = PrintHelper.PrintQR;
            }
            else
            {
                Prints pp = new Prints();

                pp.PrintInstrumentName = PrintHelper.PrintInstrumentName;
                pp.PrintCompany = PrintHelper.PrintCompany;
                pp.PrintItemCategory = PrintHelper.PrintItemCategory;
                pp.PrintDate = PrintHelper.PrintDate;
                pp.PrintUnit = PrintHelper.PrintUnit;
                pp.PrintUser = PrintHelper.PrintUser;
                pp.PrintReviewers = PrintHelper.PrintReviewers;
                pp.PrintQR = PrintHelper.PrintQR;

                Global.Printing.Add(pp);
            }
            Global.SerializeToFile(Global.Printing, Global.PrintViewFile);

            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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

                Cb_IsInstrumentName.IsChecked = PrintHelper.PrintInstrumentName = Global.Printing[0].PrintInstrumentName; 
                Cb_IsCompany.IsChecked = PrintHelper.PrintCompany = Global.Printing[0].PrintCompany;
                Cb_IsItemCategory.IsChecked = PrintHelper.PrintItemCategory= Global.Printing[0].PrintItemCategory;
                Cb_IsDate.IsChecked = PrintHelper.PrintDate = Global.Printing[0].PrintDate;
                Cb_IsUnit.IsChecked = PrintHelper.PrintUnit = Global.Printing[0].PrintUnit;
                Cb_IsUser.IsChecked = PrintHelper.PrintUser = Global.Printing[0].PrintUser;
                Cb_IsReviewers.IsChecked = PrintHelper.PrintReviewers = Global.Printing[0].PrintReviewers;
                Cb_IsPrintQR.IsChecked = PrintHelper.PrintQR = Global.Printing[0].PrintQR;
            }
        }

    }
}
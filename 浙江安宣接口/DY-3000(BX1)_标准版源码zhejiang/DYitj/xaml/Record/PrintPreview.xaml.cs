﻿using System.Windows;

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
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Cb_IsInstrumentName.IsChecked = PrintHelper.IsInstrumentName;
            Cb_IsCompany.IsChecked = PrintHelper.IsCompany;
            Cb_IsItemCategory.IsChecked = PrintHelper.IsItemCategory;
            Cb_IsDate.IsChecked = PrintHelper.IsDate;
            Cb_IsUnit.IsChecked = PrintHelper.IsUnit;
            Cb_IsUser.IsChecked = PrintHelper.IsUser;
            Cb_IsReviewers.IsChecked = PrintHelper.IsReviewers;
        }

    }
}
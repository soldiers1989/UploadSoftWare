using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// ErrorMsg.xaml 的交互逻辑
    /// </summary>
    public partial class ErrorMsg : Window
    {
        public ErrorMsg()
        {
            InitializeComponent();
        }

        private string errorContent = string.Empty;
        private string errorMsg = string.Empty;

        public void Show(string msg, string content) 
        {
            errorMsg = msg;
            errorContent = content;
            Txt_ErrorInfo.AppendText(msg);
            ShowInTaskbar = false;
            ShowDialog();
        }

        private void Btn_ErrorDetail_Click(object sender, RoutedEventArgs e)
        {
            if (Btn_ErrorDetail.Content.Equals("详细信息"))
            {
                Btn_ErrorDetail.Content = "收起";
                Height = 300;
                Txt_ErrorInfo.Document.Blocks.Clear();
                Txt_ErrorInfo.AppendText(string.Format("异常详细信息：{0}", errorContent));
            }
            else
            {
                Btn_ErrorDetail.Content = "详细信息";
                Height = 200;
                Txt_ErrorInfo.Document.Blocks.Clear();
                Txt_ErrorInfo.AppendText(errorMsg);
            }
        }

        private void Btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
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
    /// NoDryGlueSetWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NoDryGlueSetWindow : Window
    {
        public NoDryGlueSetWindow()
        {
            InitializeComponent();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("名称：{0}\r\n", txtName.Text.Trim());
                sb.AppendFormat("日期：{0}\r\n", txtTime.Text.Trim());

                if (txtWeight.Text.Trim().Length >0)
                {
                    sb.AppendFormat("重量：{0}  电话：{1}\r\n", txtWeight.Text.Trim(), txtPhone.Text.Trim());
                }
                else
                {
                    sb.AppendFormat("重量：{0}        电话：{1}\r\n", "", txtPhone.Text.Trim());
                }

                sb.AppendFormat("经营者：{0}\r\n", txtOpetor.Text.Trim());
                sb.AppendFormat("产地：{0}\r\n", txtAddr.Text.Trim());
                sb.Append("- - - - - - - - - - - - - - - - - - - - - - - - - - - -\n");
                sb.Append("方式：\r\n");
                if (checkSelf.IsChecked == true)
                {
                    sb.Append("☑ 自检合格\r\n");
                }
                else
                {
                    sb.Append("□ 自检合格\r\n");//☑☑
                }
                if (checkCommissioTesti.IsChecked == true)
                {
                    sb.Append("☑ 委托检测合格\r\n");
                }
                else
                {
                    sb.Append("□ 委托检测合格\r\n");
                }
                if (checkSelfCommitment.IsChecked==true )
                {
                    sb.Append("☑ 自我承诺合格\r\n");
                }
                else
                {
                    sb.Append("□ 自我承诺合格\r\n");
                }
                if (checkInternalQuality.IsChecked ==true )
                {
                    sb.Append("☑ 内部质量控制合格\r\n");
                }
                else
                {
                    sb.Append("□ 内部质量控制合格\r\n");
                }

                AIO.src.xprinter.PrintModel pmTitle = new AIO.src.xprinter.PrintModel();
                pmTitle.FontFamily = "宋体";
                pmTitle.FontSize = Convert.ToInt32("14");
                pmTitle.IsBold = true;

                pmTitle.Text = new System.IO.StringReader("\n" + txtTile.Text.Trim ());//首行需要空行,某些打印机首行不为空时会出现“首行乱码问题”

                AIO.src.xprinter.PrintModel pmContent2 = new AIO.src.xprinter.PrintModel();
                pmContent2.FontFamily = "宋体";
                pmContent2.FontSize = Convert.ToInt32("9");
                pmContent2.IsBold = false;
                pmContent2.Text = new System.IO.StringReader(sb.ToString());

                AIO.src.xprinter.PrintModel[] pms = new AIO.src.xprinter.PrintModel[] { pmTitle, pmContent2 };
                //二维码
                string QrCode = "";

                if(chkQRCode.IsChecked ==true )
                {
                    QrCode = "检测结果：" + txtResult.Text.Trim() + "\r\n检测人：" + txtUser.Text.Trim () + "\r\n检测时间：" + txtTime.Text.Trim();
                }
               
                if (AIO.src.xprinter.TicketPrinterHelper.Print(pms, QrCode))
                {
                    MessageBox.Show("打印成功", "操作提示", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("打印失败！请检查驱动！", "操作提示", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message );
            }

        }
    }
}

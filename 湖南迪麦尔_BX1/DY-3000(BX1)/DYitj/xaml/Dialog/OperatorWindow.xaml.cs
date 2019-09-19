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
    /// OperatorWindow.xaml 的交互逻辑
    /// </summary>
    public partial class OperatorWindow : Window
    {
        public OperatorWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Global.OperatorInfo = "";
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TxtOperatorName.Text.Trim().Length== 0)
                {
                    if(MessageBox.Show("经营者为空，合格证打印将不显示！\r\n是否继续？","操作提示",MessageBoxButton.YesNo ,MessageBoxImage.Question )!=MessageBoxResult.Yes )
                    {
                        return;
                    }
                }
                if (TxtWeight.Text.Trim().Length == 0)
                {
                    if (MessageBox.Show("重量为空，合格证打印将不显示！\r\n是否继续？", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                    {
                        return;
                    }
                }
                if (TxtPhone.Text.Trim().Length == 0)
                {
                    if (MessageBox.Show("电话为空，合格证打印将不显示！\r\n是否继续？", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                    {
                        return;
                    }
                }
                if (TxtProductAddr.Text.Trim().Length == 0)
                {
                    if (MessageBox.Show("产地为空，合格证打印将不显示！\r\n是否继续？", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                    {
                        return;
                    }
                }

                Global.OperatorInfo = TxtOperatorName.Text.Trim() + ',' + TxtWeight.Text.Trim() + ',' + TxtPhone.Text.Trim() + ',' + TxtProductAddr.Text.Trim();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message );
            }

        }

    }
}

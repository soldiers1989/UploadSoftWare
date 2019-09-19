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
    /// RepairHole.xaml 的交互逻辑
    /// </summary>
    public partial class RepairHole : Window
    {
        private int[] FenGuang=new int[]{1,2,3,4,5,7,8,9,10,11,12,13,14,15,16};
        private int[] JiaoTiJin = new int[] {1,2,3,4 };
        public string _MoKuai = "";
        public string HoleFullName = "";

        public RepairHole()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_MoKuai == "分光")
            {
                for (int i = 0; i < Global.deviceHole.HoleCount; i++)
                {
                    cmbHoleSelect.Items.Add(i+1);
                }
               
            }
            else
            {
                for (int i = 0; i < JiaoTiJin.GetLength(0); i++)
                {
                    cmbHoleSelect.Items.Add(i+1);
                }
            }
            cmbHoleSelect.SelectedIndex =int.Parse( HoleFullName.Substring(4, HoleFullName.Length - 4))-1;
            
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Global.RepairHole = HoleFullName.Substring(4, HoleFullName.Length - 4).Trim();
            this.Close();
        }
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Global.RepairHole = cmbHoleSelect.Text;
            this.Close();
        }
    }
}

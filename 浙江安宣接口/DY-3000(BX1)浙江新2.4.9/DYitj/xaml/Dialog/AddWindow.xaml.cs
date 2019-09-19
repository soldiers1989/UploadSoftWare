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
using DYSeriesDataSet;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// AddWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddWindow : Window
    {
        public AddWindow()
        {
            InitializeComponent();
        }
        clsttStandardDecideOpr bll = new clsttStandardDecideOpr();

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            clsttStandardDecide model = new clsttStandardDecide();
            string err = string.Empty;
            model.FtypeNmae = FtypeNmae.Text;
            model.SampleNum = SampleNum.Text;
            model.Name = Name.Text;
            model.ItemDes = ItemDes.Text;
            model.StandardValue = StandardValue.Text;
            model.Demarcate = Demarcate.Text;
            model.Unit = Unit.Text;
            err = string.Empty;
            bll.Insert(model, out err);
        }

    }
}

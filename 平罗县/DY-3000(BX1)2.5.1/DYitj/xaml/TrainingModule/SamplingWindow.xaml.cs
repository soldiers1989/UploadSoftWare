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

namespace AIO.xaml.TrainingModule
{
    /// <summary>
    /// SamplingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SamplingWindow : Window
    {
        public SamplingWindow()
        {
            InitializeComponent();
        }

        private void TxtName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DataGridRecord_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void DataGridRecord_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void miShow_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Show_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
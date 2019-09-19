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
    /// CountryDataSearchShow.xaml 的交互逻辑
    /// </summary>
    public partial class CountryDataSearchShow : Window
    {
        public CountryDataSearchShow()
        {
            InitializeComponent();
        }
        public List<QuickContent> models = null;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (models != null)
            {
                try
                {
                    txt.AppendText(models[models.Count - 1].CONTENT);
                    Lb1.Content = string.Format("{0} ： {1}", models[0].NAME, models[0].CONTENT);
                    Lb2.Content = string.Format("{0} ： {1}", models[1].NAME, models[1].CONTENT);
                    Lb3.Content = string.Format("{0} ： {1}", models[2].NAME, models[2].CONTENT);
                    Lb4.Content = string.Format("{0} ： {1}", models[3].NAME, models[3].CONTENT);
                    Lb5.Content = string.Format("{0} ： {1}", models[4].NAME, models[4].CONTENT);
                    Lb6.Content = string.Format("{0} ： {1}", models[5].NAME, models[5].CONTENT);
                    Lb7.Content = string.Format("{0} ： {1}", models[6].NAME, models[6].CONTENT);
                    Lb8.Content = string.Format("{0} ： {1}", models[7].NAME, models[7].CONTENT);
                    Lb9.Content = string.Format("{0} ： {1}", models[8].NAME, models[8].CONTENT);
                    Lb10.Content = string.Format("{0} ： {1}", models[9].NAME, models[9].CONTENT);
                    Lb11.Content = string.Format("{0} ： {1}", models[10].NAME, models[10].CONTENT);
                    Lb12.Content = string.Format("{0} ： {1}", models[11].NAME, models[11].CONTENT);
                    Lb13.Content = string.Format("{0} ： {1}", models[12].NAME, models[12].CONTENT);
                }
                catch (Exception)
                {
                    //MessageBox.Show("查看详情时出现异常！");
                    //this.Close();
                }
            }
        }

    }
}
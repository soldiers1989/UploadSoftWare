using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// SearchItem.xaml 的交互逻辑
    /// </summary>
    public partial class SearchItem : Window
    {
        public SearchItem()
        {
            InitializeComponent();
        }

        private void DataGridRecord_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private List<Items> basicItems = new List<Items>();
        private List<Items> items = null;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //分光
            foreach (var item in Global.fgdItems)
            {
                Items model = new Items();
                model.Name = item.Name;
                model.Type = "分光光度";
                model.Ver = item.Ver;
                basicItems.Add(model);
            }

            //胶体金
            foreach (var item in Global.jtjItems)
            {
                 Items model = new Items();
                model.Name = item.Name;
                model.Type = "胶体金";
                model.Ver = item.Ver;
                basicItems.Add(model);
            }

            //干化学
            foreach (var item in Global.gszItems)
            {
                Items model = new Items();
                model.Name = item.Name;
                model.Type = "干化学";
                model.Ver = item.Ver;
                basicItems.Add(model);
            }

            DataGridRecord.ItemsSource = basicItems;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string val = txt.Text.Trim();
            if (val.Length == 0)
            {
                DataGridRecord.ItemsSource = basicItems;
                return;
            }

            items = new List<Items>();
            foreach (var item in basicItems)
            {
                if (item.Name.IndexOf(val) >= 0)
                {
                    items.Add(item);
                }
            }

            DataGridRecord.ItemsSource = items;
        }

        private class Items 
        {
            private string name = string.Empty;

            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            private string type = string.Empty;

            public string Type
            {
                get { return type; }
                set { type = value; }
            }
            private string ver = "";
            public string Ver
            {
                get { return ver; }
                set { ver = value; }
            }

        }
        /// <summary>
        /// 项目导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputItem_Click(object sender, RoutedEventArgs e)
        {
            DataGridRecord.ItemsSource = null;
            basicItems.Clear();
            ImportItems window= new ImportItems();
            window.ShowDialog();
            //分光
            foreach (var item in Global.fgdItems)
            {
                Items model = new Items();
                model.Name = item.Name;
                model.Type = "分光光度";
                model.Ver = item.Ver;
                basicItems.Add(model);
            }

            //胶体金
            foreach (var item in Global.jtjItems)
            {
                Items model = new Items();
                model.Name = item.Name;
                model.Type = "胶体金";
                model.Ver = item.Ver;
                basicItems.Add(model);
            }

            //干化学
            foreach (var item in Global.gszItems)
            {
                Items model = new Items();
                model.Name = item.Name;
                model.Type = "干化学";
                model.Ver = item.Ver;
                basicItems.Add(model);
            }

            DataGridRecord.ItemsSource = basicItems;
        }

        
        private void labelclose_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void labelclose_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            labelclose.Foreground = new SolidColorBrush(Colors.ForestGreen);
        }

        private void labelclose_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            labelclose.Foreground = new SolidColorBrush(Colors.Red );
        }

    }
}
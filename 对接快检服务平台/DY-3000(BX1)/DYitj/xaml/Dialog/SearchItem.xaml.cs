using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

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
                basicItems.Add(model);
            }

            //胶体金
            foreach (var item in Global.jtjItems)
            {
                 Items model = new Items();
                model.Name = item.Name;
                model.Type = "胶体金";
                basicItems.Add(model);
            }

            //干化学
            foreach (var item in Global.gszItems)
            {
                Items model = new Items();
                model.Name = item.Name;
                model.Type = "干化学";
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
        }

    }
}
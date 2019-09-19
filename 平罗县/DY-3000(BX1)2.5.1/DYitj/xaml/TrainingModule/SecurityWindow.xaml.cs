using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AIO.xaml.Dialog;

namespace AIO.xaml.TrainingModule
{
    /// <summary>
    /// SecurityWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SecurityWindow : Window
    {
        public SecurityWindow()
        {
            InitializeComponent();
        }

        public string type = "实验安全";
        private string path = string.Empty;
        private string[] files = null;
        private List<Files> BasicModels = null;
        private List<Files> newModel = null;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            path = string.Format(@"{0}\KnowledgeBbase\{1}\", Environment.CurrentDirectory, type);
            if (!Directory.Exists(path))
            {
                MessageBox.Show("文件路径不存在，如需使用请联系管理员！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Lb_Title.Content = string.Format("{0}培训模块", type);
            files = Directory.GetFiles(path);
            if (files != null && files.Length > 0)
            {
                BasicModels = new List<Files>();
                Files model = null;
                for (int i = 0; i < files.Length; i++)
                {
                    try
                    {
                        model = new Files();
                        model.Path = files[i];
                        string[] fs = model.Path.Split('\\');
                        model.Name = fs[fs.Length - 1];
                        model.Type = type;
                        BasicModels.Add(model);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                DataGridRecord.DataContext = BasicModels;
            }
        }

        private void TxtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (BasicModels == null || BasicModels.Count == 0) return;

            string val = TxtName.Text.Trim();
            if (val.Length == 0)
            {
                DataGridRecord.DataContext = BasicModels;
                return;
            }

            newModel = new List<Files>();
            for (int i = 0; i < BasicModels.Count; i++)
            {
                if (BasicModels[i].Name.IndexOf(val) >= 0)
                {
                    newModel.Add(BasicModels[i]);
                }
            }
            DataGridRecord.DataContext = newModel;
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btn_Show_Click(object sender, RoutedEventArgs e)
        {
            ShowValue();
        }

        private void miShow_Click(object sender, RoutedEventArgs e)
        {
            ShowValue();
        }

        private void DataGridRecord_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowValue();
        }

        private void ShowValue()
        {
            if (DataGridRecord.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择需要查看的培训资料！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            Files fs = (Files)DataGridRecord.SelectedItems[0];
            System.Diagnostics.Process.Start(fs.Path);
        }

        private void DataGridRecord_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void Btn_Update_Click(object sender, RoutedEventArgs e)
        {
            Btn_Update.IsEnabled = false;
            TimerWindow window = new TimerWindow();
            window.lb_NTTimer.Content = "正在获取更新数据，请稍等···";
            window.ShowInTaskbar = false;
            window.ShowDialog();
            MessageBox.Show("已更新为最新数据！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            Btn_Update.IsEnabled = true;
        }

    }

    public class Files
    {
        private string _Name = string.Empty;
        /// <summary>
        /// 文件名
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _Type = string.Empty;
        /// <summary>
        /// 类型
        /// </summary>
        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        private string _Path = string.Empty;
        /// <summary>
        /// 文件路径
        /// </summary>
        public string Path
        {
            get { return _Path; }
            set { _Path = value; }
        }
    }

}
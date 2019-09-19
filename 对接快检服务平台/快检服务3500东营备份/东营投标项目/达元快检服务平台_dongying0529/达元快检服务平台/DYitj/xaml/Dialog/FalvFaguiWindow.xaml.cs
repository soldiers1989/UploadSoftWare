using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// FalvFaguiWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FalvFaguiWindow : Window
    {
        public FalvFaguiWindow()
        {
            InitializeComponent();
        }

        private string _path = Environment.CurrentDirectory + "\\Standards";
        private List<ClsStandard> _standardList;
        private MsgThread _msgThread;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void Load()
        {
            _standardList = new List<ClsStandard>();
            try
            {
                string[] fileNames = Directory.GetFiles(_path);
                string name;
                ClsStandard model;
                for (int i = 0; i < fileNames.Length; i++)
                {
                    name = fileNames[i];
                    if (name.IndexOf(_path) >= 0)
                        name = name.Remove(0, _path.Length + 1);
                    model = new ClsStandard()
                    {
                        Name = name
                    };
                    _standardList.Add(model);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }

            this.DataGridRecord.DataContext = _standardList;
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    System.Windows.Forms.OpenFileDialog file = new System.Windows.Forms.OpenFileDialog();
            //    if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //    {
            //        string path = file.FileName;
            //        FileInfo fi = new FileInfo(path);
            //        string[] name = path.Split('\\');
            //        fi.CopyTo(_path + "\\" + name[name.Length - 1], true);
            //        MessageBox.Show("新增成功", "系统提示");
            //        Load();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("新增法律法规时出现异常!\r\n\r\n异常信息" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Deleted();
        }

        /// <summary>
        /// 查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            Show();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void Search()
        {
            string name = textName.Text.Trim();
            if (name.Length == 0) this.DataGridRecord.DataContext = _standardList;

            List<ClsStandard> lists = new List<ClsStandard>();
            ClsStandard model;
            for (int i = 0; i < _standardList.Count; i++)
            {
                model = _standardList[i];
                if (model.Name.IndexOf(name) >= 0)
                {
                    lists.Add(model);
                }
            }
            this.DataGridRecord.DataContext = lists;
        }

        /// <summary>
        /// 右键-查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miShow_Click(object sender, RoutedEventArgs e)
        {
            Show();
        }

        /// <summary>
        /// 右键-删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miDelete_Click(object sender, RoutedEventArgs e)
        {
            Deleted();
        }

        private void textName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search();
        }

        private void DataGridRecord_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void DataGridRecord_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Show();
        }

        private new void Show()
        {
            try
            {
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    ClsStandard model = (ClsStandard)DataGridRecord.SelectedItems[0];
                    if (model != null && model.Name.Length > 0)
                    {
                        string fPatn = @"" + _path + "\\" + model.Name;
                        //打开指定路径的文件
                        System.Diagnostics.Process.Start(fPatn);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Deleted()
        {
            MessageBoxResult mr = MessageBox.Show("确定要删除当前选中的法律法规吗？\r\n\r\n提示：删除后无法恢复！", "重要提示", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (mr == MessageBoxResult.No)
                return;

            try
            {
                if (DataGridRecord.SelectedItems.Count > 0)
                {
                    int delCount = 0;
                    string err = string.Empty;
                    for (int i = 0; i < DataGridRecord.SelectedItems.Count; i++)
                    {
                        try
                        {
                            ClsStandard model = (ClsStandard)DataGridRecord.SelectedItems[i];
                            if (model != null && model.Name.Length > 0)
                            {
                                string fPatn = @"" + _path + "\\" + model.Name;
                                if (File.Exists(fPatn))
                                {
                                    File.Delete(fPatn);
                                    delCount++;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            err = err.Length > 0 ? "\r\n\r\n" : ex.Message;
                        }
                    }

                    if (err.Length == 0 && delCount > 0)
                    {
                        MessageBox.Show(string.Format("成功删除 {0} 条法律法规文件！", delCount), "系统提示");
                    }
                    else
                    {
                        if (delCount == 0)
                        {
                            MessageBox.Show(err.Length > 0 ? "删除文件失败！\r\n\r\n异常信息：" + err : "删除文件失败！", "Error");
                        }
                        else
                        {
                            MessageBox.Show(string.Format("成功删除 {0} 条法律法规文件！\r\n\r\n但还是出现了点小问题，异常信息：" + err, delCount), "Error");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Load();
            }
        }

        private class ClsStandard
        {
            private string name;

            public string Name
            {
                get { return name; }
                set { name = value; }
            }
        }

        /// <summary>
        /// 恢复默认法律法规文件
        /// 将Standards_Back拷贝到Standards文件中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mr = MessageBox.Show("确定要恢复到初始状态吗？\r\n\r\n提示：恢复初始状态可能会删除您新增的一些文件！", "重要提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (mr == MessageBoxResult.No)
                return;

            try
            {
                string[] fileNames;
                //1、删除现有文件夹中的所有文件
                fileNames = Directory.GetFiles(_path);
                if (fileNames != null && fileNames.Length > 0)
                {
                    for (int i = 0; i < fileNames.Length; i++)
                    {
                        if (File.Exists(fileNames[i]))
                        {
                            File.Delete(fileNames[i]);
                        }
                    }
                }

                //2、将备份文件夹中的文件拷贝到现有的目录下
                string path = @"" + Environment.CurrentDirectory + "\\Standards_Back";
                fileNames = Directory.GetFiles(path);
                if (fileNames != null && fileNames.Length > 0)
                {
                    int addCount = 0;
                    for (int i = 0; i < fileNames.Length; i++)
                    {
                        FileInfo fi = new FileInfo(fileNames[i]);
                        string[] name = fileNames[i].Split('\\');
                        fi.CopyTo(_path + "\\" + name[name.Length - 1], true);
                        addCount++;
                    }
                    MessageBox.Show("恢复成功！\r\n\r\n已成功恢复到初始状态！", "系统提示");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("恢复默认时出现异常！\r\n\r\n异常信息：" + ex.Message, "系统提示",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                textName.Text = string.Empty;
                Load();
            }
        }
        /// <summary>
        /// 下载法律法规
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _msgThread = new MsgThread(this);
                _msgThread.Start();
                Message msg = new Message()
                {
                    what = MsgCode.MSG_DownLaw,
                    str1 = Global.samplenameadapter[0].url,
                    str2 = Global.samplenameadapter[0].user,
                    str3 = Global.samplenameadapter[0].pwd
                };

                Global.workThread.SendMessage(msg, _msgThread);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        class MsgThread : ChildThread
        {
            FalvFaguiWindow wnd;
            bool _checkedDown = true;
            private delegate void UIHandleMessageDelegate(Message msg);
            private UIHandleMessageDelegate uiHandleMessageDelegate;

            public MsgThread(FalvFaguiWindow wnd)
            {
                this.wnd = wnd;
                uiHandleMessageDelegate = new UIHandleMessageDelegate(UIHandleMessage);
            }

            protected override void HandleMessage(Message msg)
            {
                base.HandleMessage(msg);
                try
                {
                    wnd.Dispatcher.Invoke(uiHandleMessageDelegate, msg);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            protected void UIHandleMessage(Message msg)
            {
                switch (msg.what)
                {
                    case MsgCode.MSG_DownLaw:
                        if (msg.result == true)
                        {
                            MessageBox.Show("法律法规下载成功");
                        }
                        break;

                    default:
                        break;
                }
            }
        }

    }
}
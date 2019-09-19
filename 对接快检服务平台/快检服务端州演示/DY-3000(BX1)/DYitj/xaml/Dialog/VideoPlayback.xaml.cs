using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using com.lvrenyang;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// VideoPlayback.xaml 的交互逻辑
    /// </summary>
    public partial class VideoPlayback : Window
    {

        private string _pathMedia = "";
        private List<string> _fileNames = new List<string>();
        public string _ItemName = string.Empty;

        public VideoPlayback()
        {
            InitializeComponent();
            _pathMedia = Global.VideoAddress;
        }
        public int playIndex = -1;
        //将视频目录下的视频名称添加到ListView中  
        private void AddItemToListView()
        {
            try
            {
                this.mediaElement1.LoadedBehavior = MediaState.Manual;
                string[] files = Directory.GetFiles(_pathMedia);
                if (files.Length == 0)
                {
                    MessageBox.Show("暂时没有可播放的视频源！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    this.Close();
                    return;
                }
                string fileName = string.Empty;
                for (int i = 0; i < files.Length; i++)
                {
                    string[] str = files[i].Split(new char[] { '\\' });
                    fileName = files[i].Substring(files[i].LastIndexOf('\\') + 1);
                    //如果有检测项目，则查找对应的播放文件
                    if (_ItemName.Length > 0)
                    {
                        if (str[str.Length - 1].Split('.')[0].Equals(_ItemName))
                        {
                            playIndex = i;
                            this.listView1.Items.Add(fileName);
                        }
                    }
                    //如果没有项目名称，则加载全部播放文件。
                    else
                    {
                        if (fileName.IndexOf(".mp4") > 0 || fileName.IndexOf(".MP4") > 0)
                        {
                            this.listView1.Items.Add(fileName);
                        }
                    }
                }
                if (this.listView1.Items==null||this.listView1.Items.Count==0)
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        if (files[i].Substring(files[i].LastIndexOf('\\') + 1).IndexOf(".mp4") > 0 || files[i].Substring(files[i].LastIndexOf('\\') + 1).IndexOf(".MP4") > 0)
                        {
                            this.listView1.Items.Add(files[i].Substring(files[i].LastIndexOf('\\') + 1));
                        }
                    }
                }
                playIndex = playIndex == -1 || playIndex > files.Length ? 0 : playIndex;
                this.mediaElement1.Source = new Uri(files[playIndex]);
                this.mediaElement1.Play();
            }
            catch (Exception ex)
            {
                FileUtils.Log("VideoPlayback-AddItemToListView:" + ex.Message + "\r\n\r\n详细信息:" + ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        //窗体加载时调用视频，进行播放  
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AddItemToListView();
            this.Topmost = true;
            this.ShowInTaskbar = false;
            //MediaElementControl();
        }

        private void mediaElement1_MediaEnded(object sender, RoutedEventArgs e)
        {
            try
            {
                //获取当前播放视频的名称（格式为：xxx.wmv）  
                string path = this.mediaElement1.Source.LocalPath;
                string currentfileName = path.Substring(path.LastIndexOf('\\') + 1);

                //对比名称列表，如果相同，则播放下一个，如果播放的是最后一个，则从第一个重新开始播放  
                for (int i = 0; i < _fileNames.Count; i++)
                {
                    if (currentfileName == _fileNames[i])
                    {
                        if (i == _fileNames.Count - 1)
                        {
                            this.mediaElement1.Source = new Uri(_pathMedia + "//" + _fileNames[0]);
                            this.mediaElement1.Play();
                        }
                        else
                        {
                            this.mediaElement1.Source = new Uri(_pathMedia + "//" + _fileNames[i + 1]);
                            this.mediaElement1.Play();
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.Log("VideoPlayback-mediaElement1_MediaEnded:" + ex.Message + "\r\n\r\n详细信息:" + ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        //播放列表选择时播放对应视频  
        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string fileName = this.listView1.SelectedValue.ToString();
                this.mediaElement1.Source = new Uri(_pathMedia + "//" + fileName);
                this.mediaElement1.Play();
            }
            catch (Exception ex)
            {
                FileUtils.Log("VideoPlayback-listView1_SelectionChanged:" + ex.Message + "\r\n\r\n详细信息:" + ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        //停止播放视频 
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            this.mediaElement1.Stop();
        }

        //播放、暂停按钮 
        private void btnSuspended_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (btnSuspended.Content.ToString() == "播 放")
                {
                    mediaElement1.Play();
                    btnSuspended.Content = "暂 停";
                    mediaElement1.ToolTip = "Click to Pause";
                }
                else
                {
                    mediaElement1.Pause();
                    btnSuspended.Content = "播 放";
                    mediaElement1.ToolTip = "Click to Play";
                }
            }
            catch (Exception ex)
            {
                FileUtils.Log("VideoPlayback-btnSuspended_Click:" + ex.Message + "\r\n\r\n详细信息:" + ex.ToString());
                MessageBox.Show("出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
            }
        }

        //快退
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            mediaElement1.Position = mediaElement1.Position - TimeSpan.FromSeconds(10);
        }

        //快进  
        private void btnForward_Click(object sender, RoutedEventArgs e)
        {
            mediaElement1.Position = mediaElement1.Position + TimeSpan.FromSeconds(10);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (this != null)
            {
                Global.IsPlayer = false;
                this.Close();
            }
        }

    }
}
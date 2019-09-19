using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// VideoPlayback.xaml 的交互逻辑
    /// </summary>
    public partial class VideoPlayback : Window
    {

        private string _pathMedia = string.Empty;
        //存储播放列表中视频的名称  
        private List<string> _fileNames = new List<string>();

        public VideoPlayback()
        {
            InitializeComponent();
            InitPath();
            AddItemToListView();
        }

        //获取视频目录  
        private void InitPath()
        {
            _pathMedia = Global.VideoAddress;
        }

        //将视频目录下的视频名称添加到ListView中  
        private void AddItemToListView()
        {
            try
            {
                string[] files = Directory.GetFiles(_pathMedia);
                foreach (string file in files)
                {
                    string[] str = file.Split(new char[] { '\\' });
                    if (Global.videoType.Equals("fgd") && str[str.Length - 1].Equals("分光度.mp4"))
                    {
                        this.listView1.Items.Add(file.Substring(file.LastIndexOf('\\') + 1));
                    }
                    else if (Global.videoType.Equals("jtj") && str[str.Length - 1].Equals("胶体金.mp4"))
                    {
                        this.listView1.Items.Add(file.Substring(file.LastIndexOf('\\') + 1));
                    }
                    else if (Global.videoType.Equals("ghx") && str[str.Length - 1].Equals("干化学.mp4"))
                    {
                        this.listView1.Items.Add(file.Substring(file.LastIndexOf('\\') + 1));
                    }
                    else if (Global.videoType.Equals("zjs") && str[str.Length - 1].Equals("重金属.mp4"))
                    {
                        this.listView1.Items.Add(file.Substring(file.LastIndexOf('\\') + 1));
                    }
                    else if (Global.videoType.Equals("all"))
                    {
                        this.listView1.Items.Add(file.Substring(file.LastIndexOf('\\') + 1));
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("视频播放失败!视频存放路径设置不正确!");
            }
        }

        //窗体加载时调用视频，进行播放  
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Topmost = true;
            this.ShowInTaskbar = false;
            MediaElementControl();
        }

        private void MediaElementControl()
        {
            try
            {
                this.mediaElement1.LoadedBehavior = MediaState.Manual;
                string player = string.Empty;
                string[] files = Directory.GetFiles(_pathMedia);

                foreach (string file in files)
                {
                    string[] str = file.Split(new char[] { '\\' });
                    if (Global.videoType.Equals("fgd") && str[str.Length - 1].Equals("分光度.mp4"))
                    {
                        player = file;
                        _fileNames.Add(file.Substring(file.LastIndexOf('\\') + 1));
                    }
                    else if (Global.videoType.Equals("jtj") && str[str.Length - 1].Equals("胶体金.mp4"))
                    {
                        player = file;
                        _fileNames.Add(file.Substring(file.LastIndexOf('\\') + 1));
                    }
                    else if (Global.videoType.Equals("ghx") && str[str.Length - 1].Equals("干化学.mp4"))
                    {
                        player = file;
                        _fileNames.Add(file.Substring(file.LastIndexOf('\\') + 1));
                    }
                    else if (Global.videoType.Equals("zjs") && str[str.Length - 1].Equals("重金属.mp4"))
                    {
                        player = file;
                        _fileNames.Add(file.Substring(file.LastIndexOf('\\') + 1));
                    }
                    else if (Global.videoType.Equals("all"))
                    {
                        player = string.Empty;
                        _fileNames.Add(file.Substring(file.LastIndexOf('\\') + 1));
                    }
                }
                if (!player.Equals(string.Empty))
                {
                    this.mediaElement1.Source = new Uri(player);
                }
                else
                {
                    this.mediaElement1.Source = new Uri(files[0]);
                }

                this.mediaElement1.Play();
            }
            catch (Exception)
            {
                //MessageBox.Show(Exception.ToString());
            }
        }

        //视频播放结束事件  
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
                MessageBox.Show("异常:\n" + ex.Message);
            }
        }

        //播放列表选择时播放对应视频  
        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string fileName = this.listView1.SelectedValue.ToString();
            this.mediaElement1.Source = new Uri(_pathMedia + "//" + fileName);
            this.mediaElement1.Play();
        }

        //返回按钮  
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            this.mediaElement1.Position = this.mediaElement1.Position + TimeSpan.FromSeconds(20);
        }

        //停止播放视频 
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            this.mediaElement1.Stop();
        }

        //播放、暂停按钮 
        private void btnSuspended_Click(object sender, RoutedEventArgs e)
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

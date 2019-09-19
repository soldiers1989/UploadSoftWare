using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AIO.src;
using AxAPlayer3Lib;

namespace AIO.xaml.Dialog
{
    public partial class APlayerForm : Form
    {
        public APlayerForm()
        {
            InitializeComponent();
            _Player = this.axPlayer;
            _PlayerTime = new Timer();
            _PlayerTime.Tick += new EventHandler(PlayerTime_Tick);
            _PlayerTime.Interval = 200;
            _PlayerTime.Enabled = true;
            this.FormClosing += APlayerForm_FormClosing;
        }

        private AxPlayer _Player = null;//播放器
        private Timer _PlayerTime = null;
        private int _Position = 0;//位置(毫秒)
        private int _Duration = 0;//长度(毫秒)
        private int _Volume = 0;
        private PlayHelp.Scrol_State _ScrollChange = PlayHelp.Scrol_State.None;//是否滑动滑块
        private PlayHelp ph = new PlayHelp();
        private string ServicePath = @"http://rj.chinafst.cn:8099/KnowledgeBbase/";
        public bool IsPlayServiceVideo = false;
        public string _ItemName = string.Empty;

        private void APlayerForm_Load(object sender, EventArgs e)
        {
            LoadPlayer();
        }

        public void LoadPlayer()
        {
            try
            {
                listView.Items.Clear();
                string[] files = Directory.GetFiles(Global.VideoAddress);
                if (files == null || files.Length == 0)
                {
                    MessageBox.Show("暂时没有可以播放的视频文件！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.Close();
                    return;
                }

                ListViewItem li = null;
                for (int i = 0; i < files.Length; i++)
                {
                    li = new ListViewItem();
                    string[] str = files[i].Split(new char[] { '\\' });
                    //如果有检测项目，则查找对应的播放文件
                    if (_ItemName.Length > 0)
                    {
                        if (str[str.Length - 1].Split('.')[0].Equals(_ItemName))
                        {
                            li.Text = _ItemName;
                            li.SubItems.Add(files[i]);
                            listView.Items.Add(li);
                        }
                    }
                    //如果没有项目名称，则加载全部播放文件。
                    else
                    {
                        li.Text = str[str.Length - 1].Split('.')[0];
                        li.SubItems.Add(files[i]);
                        listView.Items.Add(li);
                    }
                }

                if (listView.Items.Count <= 0) return;
                if (listView.Items.Count > 1) Btn_NextPage.Enabled = Btn_UpPage.Enabled = true;

                listView.Items[0].Selected = true;
                ListViewItem item = listView.Items[0];
                string path = item.SubItems[1].Text;
                _Player.Open(path);
                _Duration = _Player.GetDuration();
                this.trackBar.Maximum = _Duration;
                _Position = _Player.GetPosition();
                _Volume = _Player.GetVolume();
                Lb_AllTime.Text = ph.GetTime(_Duration);
                Global.IsPlayer = true;
                this.Text = string.Format("{0} - 视频教程", item.SubItems[0].Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("加载播放组件时出现异常！\r\n\r\n异常信息[{0}]", ex.Message), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void APlayerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global.IsPlayer = false;
            _PlayerTime.Stop();
            _PlayerTime.Dispose();
            _Player.Dispose();
        }

        private void PlayerTime_Tick(object sender, EventArgs e)
        {
            try
            {
                PlayHelp.PLAY_STATE state = (PlayHelp.PLAY_STATE)_Player.GetState();
                if (state == PlayHelp.PLAY_STATE.PS_PLAY || state == PlayHelp.PLAY_STATE.PS_PAUSED)
                {
                    _ScrollChange = PlayHelp.Scrol_State.NormalMove;
                    _Position = _Player.GetPosition();
                    _Duration = _Player.GetDuration();
                    this.trackBar.Maximum = _Duration;
                    this.trackBar.Value = _Position;
                    Lb_NowTime.Text = ph.GetTime(_Position);
                    Lb_AllTime.Text = ph.GetTime(_Duration);
                    Btn_Player.Text = "暂停";
                }
                else
                {
                    _Position = 0;
                    this.trackBar.Value = 0;
                    Btn_Player.Text = "播放";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("播放视频时出现异常！\r\n\r\n异常信息[{0}]", ex.Message), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_UpPage_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView.SelectedItems.Count > 0 && listView.Items.Count > 0)
                {
                    int index = listView.SelectedItems[0].Index;
                    if (index > 0)
                    {
                        listView.SelectedItems.Clear();
                        listView.Items[index - 1].Selected = true;
                        listView_Click(null, null);
                    }
                    else
                    {
                        listView.SelectedItems.Clear();
                        listView.Items[listView.Items.Count - 1].Selected = true;
                        listView_Click(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("播放视频出现异常！\r\n\r\n异常信息[{0}]", ex.Message), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_Player_Click(object sender, EventArgs e)
        {
            try
            {
                if (Btn_Player.Text == "播放")
                {
                    _Player.Play();
                    _PlayerTime.Start();
                    Btn_Player.Text = "暂停";
                }
                else
                {
                    _PlayerTime.Stop();
                    _Player.Pause();
                    Btn_Player.Text = "播放";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("播放视频出现异常！\r\n\r\n异常信息[{0}]", ex.Message), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_NextPage_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView.SelectedItems.Count > 0 && listView.Items.Count > 0)
                {
                    int index = listView.SelectedItems[0].Index;
                    if (index < listView.Items.Count - 1)
                    {
                        listView.SelectedItems.Clear();
                        listView.Items[index + 1].Selected = true;
                        listView_Click(null, null);
                    }
                    else
                    {
                        listView.SelectedItems.Clear();
                        listView.Items[0].Selected = true;
                        listView_Click(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("播放视频出现异常！\r\n\r\n异常信息[{0}]", ex.Message), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listView_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView.SelectedItems.Count > 0)
                {
                    ListViewItem item = listView.SelectedItems[0];
                    string path = item.SubItems[1].Text;

                    _Player.Open(path);
                    _Duration = _Player.GetDuration();
                    this.trackBar.Maximum = _Duration;
                    _Position = _Player.GetPosition();
                    _Volume = _Player.GetVolume();
                    Lb_AllTime.Text = ph.GetTime(_Duration);
                    this.Text = string.Format("{0} - 视频教程", item.SubItems[0].Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("播放视频出现异常！\r\n\r\n异常信息[{0}]", ex.Message), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void trackBar_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    _ScrollChange = PlayHelp.Scrol_State.MoveBegin;
                    _Volume = _Player.GetVolume();
                    _PlayerTime.Stop();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("播放视频出现异常！\r\n\r\n异常信息[{0}]", ex.Message), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void trackBar_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    _ScrollChange = PlayHelp.Scrol_State.MoveEnd;
                    _Player.SetVolume(_Volume);
                    _PlayerTime.Start();
                    _Player.Play();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("播放视频出现异常！\r\n\r\n异常信息[{0}]", ex.Message), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            try
            {
                if (sender != null && _ScrollChange == PlayHelp.Scrol_State.MoveBegin)
                {
                    TrackBar tb = (TrackBar)sender;
                    _Player.SetVolume(0);
                    _Player.Pause();
                    _Player.SetPosition(tb.Value);
                    Lb_NowTime.Text = ph.GetTime(tb.Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("播放视频出现异常！\r\n\r\n异常信息[{0}]", ex.Message), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

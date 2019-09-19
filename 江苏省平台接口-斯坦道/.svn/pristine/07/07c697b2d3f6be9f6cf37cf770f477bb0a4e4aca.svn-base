using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;

namespace AutoUpdate
{
    /// <summary>
    /// UpdateMain 的摘要说明。
    /// </summary>
    public class UpdateMain : Form
    {
        #region 必需的设计器变量。
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ColumnHeader chFileName;
        private System.Windows.Forms.ColumnHeader chVersion;
        private System.Windows.Forms.ColumnHeader chProgress;
        private System.Windows.Forms.ListView lvUpdateList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lbState;
        private System.Windows.Forms.ProgressBar pbDownFile;
        private System.Windows.Forms.Button btnFinish;
        private Label label2;
        private Label label5;
        private LinkLabel linkLabel1;
        private Label label3;
        private CheckBox checkBox1;
        private Label label4;
        private Label label6;
        private Label label7;
        private Panel panel_updateNotes;
        private RichTextBox txt_updateNotes;
        private Button btn_showUpdateNotes;
        #endregion

        #region Windows 窗体设计器生成的代码
        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateMain));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lvUpdateList = new System.Windows.Forms.ListView();
            this.chFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chProgress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pbDownFile = new System.Windows.Forms.ProgressBar();
            this.lbState = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnFinish = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel_updateNotes = new System.Windows.Forms.Panel();
            this.txt_updateNotes = new System.Windows.Forms.RichTextBox();
            this.btn_showUpdateNotes = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel_updateNotes.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(8, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(96, 240);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(120, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "提示";
            // 
            // lvUpdateList
            // 
            this.lvUpdateList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chFileName,
            this.chVersion,
            this.chProgress});
            this.lvUpdateList.Location = new System.Drawing.Point(116, 57);
            this.lvUpdateList.Name = "lvUpdateList";
            this.lvUpdateList.Size = new System.Drawing.Size(280, 120);
            this.lvUpdateList.TabIndex = 6;
            this.lvUpdateList.UseCompatibleStateImageBehavior = false;
            this.lvUpdateList.View = System.Windows.Forms.View.Details;
            // 
            // chFileName
            // 
            this.chFileName.Text = "组件名称";
            this.chFileName.Width = 125;
            // 
            // chVersion
            // 
            this.chVersion.Text = "版本号";
            this.chVersion.Width = 83;
            // 
            // chProgress
            // 
            this.chProgress.Text = "进度";
            this.chProgress.Width = 50;
            // 
            // pbDownFile
            // 
            this.pbDownFile.ForeColor = System.Drawing.Color.Green;
            this.pbDownFile.Location = new System.Drawing.Point(117, 213);
            this.pbDownFile.Name = "pbDownFile";
            this.pbDownFile.Size = new System.Drawing.Size(274, 17);
            this.pbDownFile.TabIndex = 5;
            // 
            // lbState
            // 
            this.lbState.Location = new System.Drawing.Point(117, 189);
            this.lbState.Name = "lbState";
            this.lbState.Size = new System.Drawing.Size(274, 16);
            this.lbState.TabIndex = 4;
            this.lbState.Text = "点击“更新”开始升级任务";
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnNext.Location = new System.Drawing.Point(236, 251);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(80, 24);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = "更新(&U)";
            this.toolTip.SetToolTip(this.btnNext, "点击进行更新操作");
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(324, 251);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 24);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消(&C)";
            this.toolTip.SetToolTip(this.btnCancel, "点击取消更新任务");
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnFinish
            // 
            this.btnFinish.Location = new System.Drawing.Point(148, 251);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(80, 24);
            this.btnFinish.TabIndex = 3;
            this.btnFinish.Text = "完成(&F)";
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.Silver;
            this.label2.Location = new System.Drawing.Point(115, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(292, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "--------------------------------------------------";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label5.Location = new System.Drawing.Point(2, 284);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(227, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "©广东达元绿洲食品安全科技股份有限公司";
            this.toolTip.SetToolTip(this.label5, "点击打开“食安科技”官网");
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(247, 212);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(137, 12);
            this.linkLabel1.TabIndex = 15;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "http://www.chinafst.cn";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(186, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "已升级到最新版本！";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(188, 139);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(108, 16);
            this.checkBox1.TabIndex = 17;
            this.checkBox1.Text = "立即运行新版本";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(323, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "©食安科技";
            // 
            // label6
            // 
            this.label6.ForeColor = System.Drawing.Color.Silver;
            this.label6.Location = new System.Drawing.Point(115, 232);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(292, 16);
            this.label6.TabIndex = 19;
            this.label6.Text = "-------------------------------------------------";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(167, 134);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(165, 16);
            this.label7.TabIndex = 20;
            this.label7.Text = "正在获取版本信息，请稍等。";
            // 
            // panel_updateNotes
            // 
            this.panel_updateNotes.Controls.Add(this.txt_updateNotes);
            this.panel_updateNotes.Location = new System.Drawing.Point(115, 51);
            this.panel_updateNotes.Name = "panel_updateNotes";
            this.panel_updateNotes.Size = new System.Drawing.Size(284, 126);
            this.panel_updateNotes.TabIndex = 21;
            this.panel_updateNotes.Visible = false;
            // 
            // txt_updateNotes
            // 
            this.txt_updateNotes.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txt_updateNotes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_updateNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_updateNotes.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_updateNotes.ForeColor = System.Drawing.Color.Green;
            this.txt_updateNotes.Location = new System.Drawing.Point(0, 0);
            this.txt_updateNotes.Name = "txt_updateNotes";
            this.txt_updateNotes.ReadOnly = true;
            this.txt_updateNotes.Size = new System.Drawing.Size(284, 126);
            this.txt_updateNotes.TabIndex = 1;
            this.txt_updateNotes.Text = string.Empty;
            this.txt_updateNotes.Visible = false;
            // 
            // btn_showUpdateNotes
            // 
            this.btn_showUpdateNotes.Enabled = false;
            this.btn_showUpdateNotes.Location = new System.Drawing.Point(119, 251);
            this.btn_showUpdateNotes.Name = "btn_showUpdateNotes";
            this.btn_showUpdateNotes.Size = new System.Drawing.Size(109, 24);
            this.btn_showUpdateNotes.TabIndex = 22;
            this.btn_showUpdateNotes.Text = "查看更新说明(&S)";
            this.toolTip.SetToolTip(this.btn_showUpdateNotes, "点击查看更新内容说明/更新文件列表");
            this.btn_showUpdateNotes.Visible = false;
            this.btn_showUpdateNotes.Click += new System.EventHandler(this.btn_showUpdateNotes_Click);
            // 
            // UpdateMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(416, 301);
            this.Controls.Add(this.btn_showUpdateNotes);
            this.Controls.Add(this.panel_updateNotes);
            this.Controls.Add(this.lvUpdateList);
            this.Controls.Add(this.lbState);
            this.Controls.Add(this.pbDownFile);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateMain";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "食安科技 - 自动升级服务 Ver_1.0";
            this.Load += new System.EventHandler(this.FrmUpdate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel_updateNotes.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private ToolTip toolTip;
        private System.ComponentModel.IContainer components;
        private bool isExit = false;
        private string updateUrl = string.Empty;
        private string tempUpdatePath = string.Empty;
        private XmlFiles updaterXmlFiles = null;
        private int availableUpdate = 0;
        private bool isUpdate = false;
        private string mainAppExe = string.Empty;
        private int failureCount = 0;
        private string errorList = string.Empty;
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        public UpdateMain()
        {
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();
            //
            // TODO: 在 InitializeComponent 调用后添加任何构造函数代码
            //
        }

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new UpdateMain());
        }

        private void FrmUpdate_Load(object sender, System.EventArgs e)
        {
            lvUpdateList.Visible = false;
            lbState.Visible = false;
            pbDownFile.Visible = false;
            linkLabel1.Visible = false;
            label3.Visible = false;
            checkBox1.Visible = false;
            label4.Visible = false;
            btnFinish.Visible = false;
            btnNext.Enabled = false;

            timer.Interval = 100;
            timer.Tick += new EventHandler(update_Tick);
            timer.Enabled = false;
            timer.Start();
        }

        private void update_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            timer.Dispose();
            if (!IsConnectInternet())
            {
                label1.Visible = true;
                label7.Text = "无法连接到互联网！";
                MessageBox.Show(this, "无法连接到互联网，请检查网络！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string localXmlFile = Application.StartupPath + "\\UpdateList.xml";
            string serverXmlFile = string.Empty;
            try
            {
                //从本地读取更新配置文件信息
                updaterXmlFiles = new XmlFiles(localXmlFile);
            }
            catch
            {
                MessageBox.Show(this, "读取配置文件出错，请联系管理员！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            //获取服务器地址
            updateUrl = updaterXmlFiles.GetNodeValue("//Url");
            AppUpdater appUpdater = new AppUpdater();
            appUpdater.UpdaterUrl = updateUrl + "/UpdateList.xml";
            //与服务器连接,下载更新配置文件
            try
            {
                tempUpdatePath = Environment.GetEnvironmentVariable("Temp") + "\\" + "_" + updaterXmlFiles.FindNode("//Application").Attributes["applicationId"].Value + "_" + "y" + "_" + "x" + "_" + "m" + "_" + "\\";
                appUpdater.DownAutoUpdateFile(tempUpdatePath);
            }
            catch
            {
                MessageBox.Show(this, "与服务器连接失败，请求超时！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            //获取更新文件列表
            Hashtable htUpdateFile = new Hashtable();
            serverXmlFile = tempUpdatePath + "\\UpdateList.xml";
            if (!File.Exists(serverXmlFile))
            {
                label1.Visible = true;
                label7.Text = "当前版本已经是最新版本！";
                label7.Visible = true;
                MessageBox.Show(this, "当前版本已经是最新版本！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }
            availableUpdate = appUpdater.CheckForUpdate(serverXmlFile, localXmlFile, out htUpdateFile);
            if (availableUpdate > 0)
            {
                //获取更新内容说明
                this.txt_updateNotes.Visible = true;
                this.panel_updateNotes.Visible = true;
                this.btn_showUpdateNotes.Enabled = true;
                this.btn_showUpdateNotes.Text = "查看更新列表(&S)";
                string[] updateNotes = appUpdater.GetUpdateNotes(serverXmlFile);
                if (updateNotes != null)
                {
                    foreach (string note in updateNotes)
                        this.txt_updateNotes.AppendText(note + "\r\n");
                }
                else
                {
                    this.txt_updateNotes.AppendText("暂时没有版本更新说明！");
                }

                for (int i = 0; i < htUpdateFile.Count; i++)
                {
                    string[] fileArray = (string[])htUpdateFile[i];
                    lvUpdateList.Items.Add(new ListViewItem(fileArray));
                }
                label1.Visible = true;
                label1.Text = "发现新版本";
                lvUpdateList.Visible = true;
                lbState.Visible = true;
                pbDownFile.Visible = true;
                label7.Visible = false;
                btnNext.Enabled = true;
            }
            else
            {
                label1.Visible = true;
                label7.Text = "当前版本已经是最新版本！";
                label7.Visible = true;
                MessageBox.Show(this, "当前版本已经是最新版本！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            if (isUpdate)
            {
                isExit = true;
                DialogResult dr = MessageBox.Show("确定取消更新操作，并退出升级服务吗？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                if (dr == DialogResult.Yes)
                {
                    this.Close();
                    Application.ExitThread();
                    Application.Exit();
                }
                isExit = false;
            }
            else
            {
                this.Close();
                Application.ExitThread();
                Application.Exit();
            }
        }

        private void btnNext_Click(object sender, System.EventArgs e)
        {
            update();
        }

        private void KillProcesses()
        {
            //获取所有进程
            Process[] ps = Process.GetProcesses();
            for (int i = 0; i < ps.Length; i++)
            {
                if (ps[i].ProcessName.StartsWith("DY-Detector"))
                {
                    ps[i].Kill();
                }
            }
        }

        private void update()
        {
            this.label1.Text = "正在更新";
            this.btnNext.Enabled = false;
            this.panel_updateNotes.Visible = false;
            this.btn_showUpdateNotes.Text = "查看更新说明(&S)";
            if (availableUpdate > 0)
            {
                Thread threadDown = new Thread(new ThreadStart(DownUpdateFile));
                threadDown.IsBackground = true;
                threadDown.Start();
            }
            else
            {
                MessageBox.Show(this, "当前版本已经是最新版本！", "更新提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnNext.Enabled = true;
            }
        }

        private void DownUpdateFile()
        {
            isUpdate = true;
            CheckForIllegalCrossThreadCalls = false;
            failureCount = 0;
            this.Cursor = Cursors.WaitCursor;
            mainAppExe = updaterXmlFiles.GetNodeValue("//EntryPoint");
            Process[] allProcess = Process.GetProcesses();
            foreach (Process p in allProcess)
            {
                if (p.ProcessName.ToLower() + ".exe" == mainAppExe.ToLower())
                {
                    for (int i = 0; i < p.Threads.Count; i++)
                        p.Threads[i].Dispose();
                    p.Kill();
                }
            }
            WebClient webClient = new WebClient();
            for (int i = 0; i < this.lvUpdateList.Items.Count; i++)
            {
                while (isExit) { }
                string updateFile = lvUpdateList.Items[i].Text.Trim();
                string updateFileUrl = updateUrl + lvUpdateList.Items[i].Text.Trim();
                long fileLength = 0;
                WebRequest webReq;
                WebResponse webRes;
                try
                {
                    webReq = WebRequest.Create(updateFileUrl);
                    webReq.ContentType = "GET";
                    webRes = webReq.GetResponse();
                    fileLength = webRes.ContentLength;
                }
                catch (Exception)
                {
                    string[] files = updateFileUrl.Split('/');
                    if (files != null && files.Length > 0)
                        files[files.Length - 1] = string.Format("[{0}]", files[files.Length - 1]);
                    errorList += errorList.Length > 0 ? "\r\n" + files[files.Length - 1] : files[files.Length - 1];
                    failureCount++;
                    continue;
                }

                lbState.Text = string.Format("正在下载更新文件({0}/{1})，请稍后···", i + 1, lvUpdateList.Items.Count);
                pbDownFile.Value = 0;
                pbDownFile.Maximum = (int)fileLength;

                try
                {
                    Stream srm = webRes.GetResponseStream();
                    StreamReader srmReader = new StreamReader(srm);
                    byte[] bufferbyte = new byte[fileLength];
                    int allByte = (int)bufferbyte.Length;
                    int startByte = 0;
                    while (fileLength > 0)
                    {
                        try
                        {
                            Application.DoEvents();
                            int downByte = srm.Read(bufferbyte, startByte, allByte);
                            if (downByte == 0) { break; };
                            startByte += downByte;
                            allByte -= downByte;
                            pbDownFile.Value += downByte;

                            float part = (float)startByte / 1024;
                            float total = (float)bufferbyte.Length / 1024;
                            int percent = Convert.ToInt32((part / total) * 100);
                            this.lvUpdateList.Items[i].SubItems[2].Text = percent.ToString() + "%";
                        }
                        catch (Exception) { }
                    }

                    string tempPath = tempUpdatePath + updateFile;
                    CreateDirtory(tempPath);
                    FileStream fs = new FileStream(tempPath, FileMode.OpenOrCreate, FileAccess.Write);
                    fs.Write(bufferbyte, 0, bufferbyte.Length);
                    srm.Close();
                    srmReader.Close();
                    fs.Close();
                }
                catch (WebException ex)
                {
                    MessageBox.Show(this, "更新文件下载失败！\r\n\r\n异常信息：" + ex.Message.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            UpdateFile();
            InvalidateControl();
            if (failureCount > 0)
                MessageBox.Show(this, "有 " + failureCount + " 个文件更新失败！\r\n更新失败列表：\r\n" + errorList + "\r\n\r\n请联系软件供应商反馈此情况！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (errorLog.Length > 0)
                MessageBox.Show(this, string.Format("更新过程中出现了点小问题，请将此界面截图反馈给软件服务供应商，谢谢！\r\n\r\n异常信息：{0}", errorLog), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="path"></param>
        private void CreateDirtory(string path)
        {
            if (!File.Exists(path))
            {
                string[] dirArray = path.Split('\\');
                string temp = string.Empty;
                for (int i = 0; i < dirArray.Length - 1; i++)
                {
                    temp += dirArray[i].Trim() + "\\";
                    if (!Directory.Exists(temp))
                        Directory.CreateDirectory(temp);
                }
            }
        }

        private string errorLog = string.Empty;

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="objPath"></param>
        public void CopyFile(string sourcePath, string objPath)
        {
            if (!Directory.Exists(objPath))
            {
                Directory.CreateDirectory(objPath);
            }
            string[] files = Directory.GetFiles(sourcePath);
            for (int i = 0; i < files.Length; i++)
            {
                try
                {
                    string[] childfile = files[i].Split('\\');
                    File.Copy(files[i], objPath + @"\" + childfile[childfile.Length - 1], true);
                }
                catch (Exception ex)
                {
                    errorLog += string.Format("[{0}]", ex.Message);
                }
            }
            string[] dirs = Directory.GetDirectories(sourcePath);
            for (int i = 0; i < dirs.Length; i++)
            {
                try
                {
                    string[] childdir = dirs[i].Split('\\');
                    CopyFile(dirs[i], objPath + @"\" + childdir[childdir.Length - 1]);
                }
                catch (Exception ex)
                {
                    errorLog += string.Format("[{0}]", ex.Message);
                }
            }
        }

        private void btnFinish_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    System.Diagnostics.Process.Start(Environment.CurrentDirectory + "\\DY-Detector.exe");
                }
                this.Close();
                this.Dispose();
            }
            catch (Exception)
            {
                if (checkBox1.Checked) Process.Start(mainAppExe);
            }
            finally
            {
                Application.ExitThread();
                Application.Exit();
            }
        }

        /// <summary>
        /// 重绘UI
        /// </summary>
        private void InvalidateControl()
        {
            //仪器配置一般，需要延迟3S等待覆盖操作完成
            for (int i = 1; i <= 100; i++)
            {
                lbState.Text = string.Format("正在完成最后操作({0}/{1})，请稍等···", i, 100);
                Thread.Sleep(30);
            }

            label1.Text = "更新成功";
            //完成按钮
            btnFinish.Location = btnCancel.Location;
            btnFinish.Visible = true;
            //更新按钮
            btnNext.Visible = false;
            //取消按钮
            btnCancel.Visible = false;
            //列表
            lvUpdateList.Visible = false;
            lbState.Visible = false;
            pbDownFile.Visible = false;

            linkLabel1.Visible = true;
            label3.Visible = true;
            checkBox1.Visible = true;
            label4.Visible = true;
        }

        /// <summary>
        /// 更新文件
        /// </summary>
        private void UpdateFile()
        {
            try
            {
                //覆盖旧文件
                CopyFile(tempUpdatePath, Directory.GetCurrentDirectory());
                System.IO.Directory.Delete(tempUpdatePath, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "升级出错！\r\n\r\n异常信息：" + ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 判断主应用程序是否正在运行
        /// </summary>
        /// <returns></returns>
        private bool IsMainAppRun()
        {
            string mainAppExe = updaterXmlFiles.GetNodeValue("//EntryPoint");
            bool isRun = false;
            Process[] allProcess = Process.GetProcesses();
            foreach (Process p in allProcess)
            {
                if (p.ProcessName.ToLower() + ".exe" == mainAppExe.ToLower())
                {
                    isRun = true;
                }
            }
            return isRun;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel1.Text);
        }

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(int Description, int ReservedValue);
        /// <summary>
        /// 检查网络是否可以连接互联网
        /// </summary>
        /// <returns></returns>
        public static bool IsConnectInternet()
        {
            int Description = 0;
            return InternetGetConnectedState(Description, 0);
        }

        private void btn_showUpdateNotes_Click(object sender, EventArgs e)
        {
            if (panel_updateNotes.Visible)
            {
                this.panel_updateNotes.Visible = false;
                this.btn_showUpdateNotes.Text = "查看更新说明(&S)";
            }
            else
            {
                this.panel_updateNotes.Visible = true;
                this.btn_showUpdateNotes.Text = "查看更新列表(&S)";
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.chinafst.cn");
        }

        private class MD5
        {
            /// <summary>
            /// MD5加密（包含key和iv向量）
            /// </summary>
            /// <param name="data">需要加密的数据</param>
            /// <param name="KEY_64">key len=8</param>
            /// <param name="IV_64">iv向量 len=8</param>
            /// <returns>返回加密后的字符串</returns>
            public static string MD564_KEY(string data)
            {
                string KEY_64 = "1@3$5^7*", IV_64 = "8&6%4#2!";
                try
                {
                    byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);
                    byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);
                    DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                    int i = cryptoProvider.KeySize;
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);
                    StreamWriter sw = new StreamWriter(cst);
                    sw.Write(data);
                    sw.Flush();
                    cst.FlushFinalBlock();
                    sw.Flush();
                    return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }

            /// <summary>
            /// MD5解密（需要用key和iv向量）
            /// </summary>
            /// <param name="data">需要解密的数据</param>
            /// <param name="KEY_64">密钥</param>
            /// <param name="IV_64">向量</param>
            /// <returns>返回解密后的数据</returns>
            public static string DMD564_KEY(string data)
            {
                string KEY_64 = "1@3$5^7*", IV_64 = "8&6%4#2!";
                try
                {
                    byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);
                    byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);
                    byte[] byEnc;
                    byEnc = Convert.FromBase64String(data); //把需要解密的字符串转为8位无符号数组
                    DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                    MemoryStream ms = new MemoryStream(byEnc);
                    CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
                    StreamReader sr = new StreamReader(cst);
                    return sr.ReadToEnd();
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }

    }
}
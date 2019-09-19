using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using DY.FoodClientLib;

namespace FoodClient
{
    /// <summary>
    /// frmAbout 的摘要说明。
    /// </summary>
    public class FrmAbout : System.Windows.Forms.Form
    {
        #region 窗体
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblCompanyName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblSystemName;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblVersion;
        private Label lblServicePhone;
        private Label label3;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label2;
        private TextBox txtWebSite;
        private Label label5;
        private Label lblZipcode;
        private System.ComponentModel.Container components = null;
        #endregion

        public FrmAbout()
        {
            InitializeComponent();
        }

        #region Windows 窗体设计器生成的代码
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
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAbout));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.lblSystemName = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblServicePhone = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtWebSite = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblZipcode = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(9, 235);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(488, 10);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 252);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(401, 40);
            this.label1.TabIndex = 2;
            this.label1.Text = "警告：本计算机程序受著作权法和国际公约的保护，未经授权擅自复制或传播本程序的部分或全部，可能受到严厉的民事及刑事制裁，并将在法律许可的范围内受到最大可能的起诉。";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(408, 255);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 24);
            this.button1.TabIndex = 3;
            this.button1.Text = "确定(&O)";
            this.button1.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCompanyName.Location = new System.Drawing.Point(168, 70);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(182, 16);
            this.lblCompanyName.TabIndex = 5;
            this.lblCompanyName.Text = "广州达元食品安全技术有限公司";
            // 
            // lblSystemName
            // 
            this.lblSystemName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSystemName.Location = new System.Drawing.Point(170, 17);
            this.lblSystemName.Name = "lblSystemName";
            this.lblSystemName.Size = new System.Drawing.Size(314, 16);
            this.lblSystemName.TabIndex = 6;
            this.lblSystemName.Text = "达元食品安全检测工作站系统";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(103, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "公司主页：";
            // 
            // lblAddress
            // 
            this.lblAddress.Location = new System.Drawing.Point(171, 154);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(208, 41);
            this.lblAddress.TabIndex = 9;
            this.lblAddress.Text = "广州市天河区珠江新城金穗路6-20号\r\n星汇国际大厦西塔10楼1003室";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Location = new System.Drawing.Point(9, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(88, 219);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(168, 44);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(95, 12);
            this.lblVersion.TabIndex = 14;
            this.lblVersion.Text = "Version 1.0.0.0";
            // 
            // lblServicePhone
            // 
            this.lblServicePhone.AutoSize = true;
            this.lblServicePhone.Location = new System.Drawing.Point(170, 96);
            this.lblServicePhone.Name = "lblServicePhone";
            this.lblServicePhone.Size = new System.Drawing.Size(83, 12);
            this.lblServicePhone.TabIndex = 15;
            this.lblServicePhone.Text = "400-696-696-8";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(103, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "系统名称：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(103, 44);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 17;
            this.label8.Text = "系统版本：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(103, 70);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 18;
            this.label9.Text = "公司名称：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(103, 96);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 19;
            this.label10.Text = "客服热线：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(103, 154);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 20;
            this.label11.Text = "公司地址：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(355, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 21;
            this.label2.Text = "版权所有";
            // 
            // txtWebSite
            // 
            this.txtWebSite.BackColor = System.Drawing.SystemColors.Control;
            this.txtWebSite.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtWebSite.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtWebSite.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txtWebSite.Location = new System.Drawing.Point(171, 121);
            this.txtWebSite.Name = "txtWebSite";
            this.txtWebSite.Size = new System.Drawing.Size(163, 14);
            this.txtWebSite.TabIndex = 22;
            this.txtWebSite.Text = "http://www.dayuangz.com/";
            this.txtWebSite.Click += new System.EventHandler(this.txtWebSite_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(103, 199);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 23;
            this.label5.Text = "邮政编码：";
            // 
            // lblZipcode
            // 
            this.lblZipcode.AutoSize = true;
            this.lblZipcode.Location = new System.Drawing.Point(172, 199);
            this.lblZipcode.Name = "lblZipcode";
            this.lblZipcode.Size = new System.Drawing.Size(0, 12);
            this.lblZipcode.TabIndex = 24;
            // 
            // FrmAbout
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(496, 295);
            this.Controls.Add(this.lblZipcode);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtWebSite);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblServicePhone);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblSystemName);
            this.Controls.Add(this.lblCompanyName);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAbout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "关于本软件";
            this.Load += new System.EventHandler(this.frmAbout_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void frmAbout_Load(object sender, System.EventArgs e)
        {
            //lblSystemName.Text = frmMain.formMain.Text;
            bindData();
        }

        /// <summary>
        /// 绑定初始化数据
        /// 对应该数据表SysOpt 03打头的行
        /// </summary>
        private void bindData()
        {
            clsSysOptOpr bll = new clsSysOptOpr();
            DataTable dtbl = bll.GetColumnDataTable(0, "Len(SysCode)=6 AND OptType='03'", "OptValue");
            if (dtbl != null && dtbl.Rows.Count > 0)
            {
                //lblVersion.Text = dtbl.Rows[0]["OptValue"].ToString() + dtbl.Rows[1]["OptValue"].ToString();//版本名称+版本号
                //lblVersion.Text = "单机版V3.0.0.3";
                //txtVersionNum.Text = dtbl.Rows[1]["OptValue"].ToString();
                lblSystemName.Text = dtbl.Rows[2]["OptValue"].ToString();
                lblCompanyName.Text = dtbl.Rows[3]["OptValue"].ToString();
                lblServicePhone.Text = dtbl.Rows[4]["OptValue"].ToString();
                txtWebSite.Text = " " + dtbl.Rows[5]["OptValue"].ToString();
                lblAddress.Text = dtbl.Rows[6]["OptValue"].ToString();
                lblZipcode.Text = dtbl.Rows[7]["OptValue"].ToString();
            }
        }
        //private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        //{
        //    System.Diagnostics.Process.Start("IEXPLORE.EXE", ((LinkLabel)sender).Text);
        //}

        /// <summary>
        ///点击打开连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWebSite_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("IEXPLORE.EXE", txtWebSite.Text);
        }
    }
}

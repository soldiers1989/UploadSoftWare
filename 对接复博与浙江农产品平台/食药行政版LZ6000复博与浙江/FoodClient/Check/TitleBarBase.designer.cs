using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace FoodClient
{
	partial class TitleBarBase : Form 
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.lblDeviceName = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelRightBorder2 = new System.Windows.Forms.Panel();
            this.panelLeftBorder2 = new System.Windows.Forms.Panel();
            this.panelBottomBorder = new System.Windows.Forms.Panel();
            this.panelTop = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelTitleBar.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(202)))), ((int)(((byte)(187)))));
            this.panelTitleBar.BackgroundImage = global::FoodClient.Properties.Resources.TitleBar_1;
            this.panelTitleBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelTitleBar.Controls.Add(this.lblDeviceName);
            this.panelTitleBar.Controls.Add(this.btnClose);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(612, 26);
            this.panelTitleBar.TabIndex = 111;
            this.panelTitleBar.DoubleClick += new System.EventHandler(this.OnTitleBarDoubleClick);
            this.panelTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnTitleBarMouseDown);
            this.panelTitleBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnTitleBarMouseMove);
            this.panelTitleBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnTitleBarMouseUp);
            // 
            // lblDeviceName
            // 
            this.lblDeviceName.AutoSize = true;
            this.lblDeviceName.BackColor = System.Drawing.Color.Transparent;
            this.lblDeviceName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDeviceName.Location = new System.Drawing.Point(4, 5);
            this.lblDeviceName.Name = "lblDeviceName";
            this.lblDeviceName.Size = new System.Drawing.Size(88, 16);
            this.lblDeviceName.TabIndex = 113;
            this.lblDeviceName.Text = "DeviceName";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnClose.BackgroundImage = global::FoodClient.Properties.Resources.Close;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnClose.Location = new System.Drawing.Point(589, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(20, 20);
            this.btnClose.TabIndex = 110;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelRightBorder2
            // 
            this.panelRightBorder2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(151)))), ((int)(((byte)(128)))));
            this.panelRightBorder2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRightBorder2.Location = new System.Drawing.Point(614, 0);
            this.panelRightBorder2.Name = "panelRightBorder2";
            this.panelRightBorder2.Size = new System.Drawing.Size(2, 396);
            this.panelRightBorder2.TabIndex = 13;
            // 
            // panelLeftBorder2
            // 
            this.panelLeftBorder2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(151)))), ((int)(((byte)(128)))));
            this.panelLeftBorder2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeftBorder2.Location = new System.Drawing.Point(0, 0);
            this.panelLeftBorder2.Name = "panelLeftBorder2";
            this.panelLeftBorder2.Size = new System.Drawing.Size(2, 396);
            this.panelLeftBorder2.TabIndex = 14;
            // 
            // panelBottomBorder
            // 
            this.panelBottomBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(151)))), ((int)(((byte)(128)))));
            this.panelBottomBorder.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottomBorder.Location = new System.Drawing.Point(2, 393);
            this.panelBottomBorder.Name = "panelBottomBorder";
            this.panelBottomBorder.Size = new System.Drawing.Size(612, 3);
            this.panelBottomBorder.TabIndex = 15;
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(151)))), ((int)(((byte)(128)))));
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(2, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(612, 2);
            this.panelTop.TabIndex = 16;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(151)))), ((int)(((byte)(128)))));
            this.panel1.Controls.Add(this.panelTitleBar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(612, 26);
            this.panel1.TabIndex = 17;
            // 
            // TitleBarBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 396);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelBottomBorder);
            this.Controls.Add(this.panelLeftBorder2);
            this.Controls.Add(this.panelRightBorder2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TitleBarBase";
            this.Text = "TitleBarBase";
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panelTitleBar;
		private System.Windows.Forms.Button btnClose;
		private Panel panelRightBorder2;
		private Panel panelLeftBorder2;
        private Panel panelBottomBorder;
		private Panel panelTop;
		private Panel panel1;
        protected Label lblDeviceName;
	}
}
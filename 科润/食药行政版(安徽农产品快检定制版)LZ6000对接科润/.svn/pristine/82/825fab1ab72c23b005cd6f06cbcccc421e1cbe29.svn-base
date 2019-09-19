namespace DY.Process
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ProcessForm : Form
    {
        private IContainer components;
        private Label labelInfor;
        private Panel panel1;
        private ProgressBar progressBar1;

        public ProcessForm()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.labelInfor = new Label();
            this.panel1 = new Panel();
            this.progressBar1 = new ProgressBar();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.labelInfor.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.labelInfor.AutoSize = true;
            this.labelInfor.Font = new Font("宋体", 11f, FontStyle.Bold);
            this.labelInfor.Location = new Point(12, 8);
            this.labelInfor.Name = "labelInfor";
            this.labelInfor.Size = new Size(0x62, 15);
            this.labelInfor.TabIndex = 2;
            this.labelInfor.Text = "正在进行...";
            this.panel1.BackColor = SystemColors.Control;
            this.panel1.BorderStyle = BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.labelInfor);
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x24f, 0x4a);
            this.panel1.TabIndex = 1;
            this.progressBar1.ForeColor = SystemColors.GradientActiveCaption;
            this.progressBar1.Location = new Point(11, 0x1a);
            this.progressBar1.MarqueeAnimationSpeed = 15;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new Size(0x233, 0x1f);
            this.progressBar1.Style = ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 3;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x251, 0x4c);
            base.Controls.Add(this.panel1);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "ProcessForm";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "ProcessForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
        }

        public string MessageInfo
        {
            set
            {
                this.labelInfor.Text = value;
            }
        }

        public ProgressBarStyle ProcessStyle
        {
            set
            {
                this.progressBar1.Style = value;
            }
        }

        public int ProcessValue
        {
            set
            {
                this.progressBar1.Value = value;
            }
        }
    }
}


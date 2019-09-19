using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace FoodClient
{
	/// <summary>
	/// frmSplash ��ժҪ˵����
	/// </summary>
	public class frmSplash : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Timer timSplash;
		private System.ComponentModel.IContainer components;

		public frmSplash()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
		}

		/// <summary>
		/// ������������ʹ�õ���Դ��
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.timSplash = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// timSplash
			// 
			this.timSplash.Enabled = true;
			this.timSplash.Interval = 1;
			this.timSplash.Tick += new System.EventHandler(this.timSplash_Tick);
			// 
			// frmSplash
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackgroundImage = global::FoodClient.Properties.Resources.Standalone;
			this.ClientSize = new System.Drawing.Size(600, 329);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.KeyPreview = true;
			this.Name = "frmSplash";
			this.Opacity = 0D;
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "frmSplash";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.frmSplash_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSplash_KeyDown);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmSplash_MouseDown);
			this.ResumeLayout(false);

		}
		#endregion

        private void frmSplash_Load(object sender, System.EventArgs e)
		{
			this.Opacity=0;
		}

		private void frmSplash_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.Hide();
			this.Dispose(); 
		}

		private void frmSplash_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			this.Hide();
			this.Dispose(); 
		}
        
		private int i=0;
		private void timSplash_Tick(object sender, System.EventArgs e)
		{
			if (this.Opacity<=0.2)
			{
				this.Opacity=this.Opacity+0.05;
			}
			else if(this.Opacity<0.7&&this.Opacity>0.2)
			{
				this.Opacity=this.Opacity+0.01;
			}
			else if(this.Opacity<1&&this.Opacity>0.7)
			{
				this.Opacity=this.Opacity+0.05;
			}
			else
			{  
				if (i<50)//i<5
				{
					this.timSplash.Interval=100;
					i++;
				}
				else
				{
					this.Hide();
					this.Dispose(); 
				}
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FoodClient
{
	public partial class TitleBarBase : Form
	{
		public TitleBarBase()
		{
			InitializeComponent();
		}

		protected string TitleBarText
		{
			set
			{
				lblDeviceName.Text = value;
			}
		}

		private bool m_IsMouseDown = false;
		private Point m_FormLocation;     //form的location
		private Point m_MouseOffset;      //鼠标的按下位置 


		protected virtual void OnTitleBarDoubleClick(object sender, EventArgs e)
		{
			if (this.WindowState != FormWindowState.Maximized)
			{
				this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
				this.WindowState = FormWindowState.Maximized;
			}
			else
			{
				this.WindowState = FormWindowState.Normal;
			}
		}

		private void OnTitleBarMouseDown(object sender, MouseEventArgs e)
		{
			try
			{
				if (e.Button == MouseButtons.Left)
				{
					m_IsMouseDown = true;
					m_FormLocation = this.Location;
					m_MouseOffset = Control.MousePosition;
				}
			}
			catch (Exception) { }
		}

		private void OnTitleBarMouseMove(object sender, MouseEventArgs e)
		{
			try
			{
				int x = 0;
				int y = 0;
				if (m_IsMouseDown)
				{
					Point pt = Control.MousePosition;
					x = m_MouseOffset.X - pt.X;
					y = m_MouseOffset.Y - pt.Y;

					this.Location = new Point(m_FormLocation.X - x, m_FormLocation.Y - y);
				}
			}
			catch (Exception) { }
		}

		private void OnTitleBarMouseUp(object sender, MouseEventArgs e)
		{
			try
			{
				m_IsMouseDown = false;
			}
			catch (Exception) { }
		}

		protected void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}


	}
}

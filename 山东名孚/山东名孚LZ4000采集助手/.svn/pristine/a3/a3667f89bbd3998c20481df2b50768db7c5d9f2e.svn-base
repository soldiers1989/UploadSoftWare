using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WorkstationUI.function
{
    public partial class ucShopmall : UserControl
    {
        public ucShopmall()
        {
            InitializeComponent();
            //连接
            string url = "https://tianhelvzhou.tmall.com/";
            webBrowser1.Navigate(url);//显示网页
        }

        private void ucShopmall_Load(object sender, EventArgs e)
        {
            
        }

        private void labelclose_Click(object sender, EventArgs e)
        {
            webBrowser1.Stop();
            this.Dispose();
            //this.Hide();
            //MainForm mf = new MainForm();
            //mf.Mainpanel.Visible =false;            
        }
        //后退
        private void labelback_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void labelclose_MouseEnter(object sender, EventArgs e)
        {
            labelclose.ForeColor = Color.Red;
        }

        private void labelclose_MouseLeave(object sender, EventArgs e)
        {
            labelclose.ForeColor = Color.White;
        }

        private void labelback_MouseEnter(object sender, EventArgs e)
        {
            labelback.ForeColor = Color.Red;
        }

        private void labelback_MouseLeave(object sender, EventArgs e)
        {
            labelback.ForeColor = Color.White;
        }
        //防止弹窗；
        private void webBrowser1_NewWindow(object sender, CancelEventArgs e)
        {
            //防止弹窗；
            e.Cancel = true;
            string url = this.webBrowser1.StatusText;
            this.webBrowser1.Url = new Uri(url);

        }

        private void labelBrowser_MouseEnter(object sender, EventArgs e)
        {
            labelBrowser.ForeColor = Color.Red;
        }

        private void labelBrowser_MouseLeave(object sender, EventArgs e)
        {
            labelBrowser.ForeColor = Color.White;
        }

        private void panelBrowser_MouseClick(object sender, MouseEventArgs e)
        {         
            //调用系统默认的浏览器  
            System.Diagnostics.Process.Start("explorer.exe", "https://tianhelvzhou.tmall.com/");     
        }

        private void labelBrowser_Click(object sender, EventArgs e)
        {
            //调用系统默认的浏览器  
            System.Diagnostics.Process.Start("explorer.exe", "https://tianhelvzhou.tmall.com/"); 
        }
        //后退
        private void panelBack_MouseClick(object sender, MouseEventArgs e)
        {
            webBrowser1.GoBack();
        }
        //private void webBrowser1_NewWindow(object sender, CancelEventArgs e)
        //{
        //    //防止弹窗；
        //    e.Cancel = true;
        //    string url = this.webBrowser1.StatusText;
        //    this.webBrowser1.Url = new Uri(url);
        //}
    }
}

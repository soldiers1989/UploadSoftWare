using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace WorkstationUI.function
{
    public partial class reportform : Form
    {
        public reportform()
        {
            InitializeComponent();
        }
        public string HtmlUrl = string.Empty;

        private void reportform_Load(object sender, EventArgs e)
        {
            string FEATURE_BROWSER_EMULATION = @"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION";
            string FEATURE_DOCUMENT_COMPATIBLE_MODE = @"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_DOCUMENT_COMPATIBLE_MODE";
            string exename = "WebBrowserTest.exe";
            using (RegistryKey regkey1 = Registry.CurrentUser.CreateSubKey(FEATURE_BROWSER_EMULATION))
            using (RegistryKey regkey2 = Registry.CurrentUser.CreateSubKey(FEATURE_DOCUMENT_COMPATIBLE_MODE))
            {
                regkey1.SetValue(exename, 8000, RegistryValueKind.DWord);
                regkey2.SetValue(exename, 80000, RegistryValueKind.DWord);
                regkey1.Close();
                regkey2.Close();
            }

            //因为是使用WebBrowser对象进行打印HTML文件，所以无法控制页面设置，需要使用注册表修改一些内容
            //修改注册表，取消页眉、页角项目
            RegistryKey hklm = Registry.CurrentUser;
            RegistryKey software = hklm.OpenSubKey(@"Software\Microsoft\Internet Explorer\PageSetup".ToUpper(), true);
            object A = (object)string.Empty;
            object B = (object)"0.5";
            object C = (object)"0";
            software.SetValue("header", A);
            software.SetValue("footer", A);
            software.SetValue("margin_bottom", B);
            software.SetValue("margin_left", C);
            software.SetValue("margin_right", C);
            software.SetValue("margin_top", C);
            software.SetValue("Shrink_To_Fit", "yes");
            webBrowser1.Url = new Uri(HtmlUrl);
            webBrowser1.ScriptErrorsSuppressed = false;
            //webBrowser1.ScrollBarsEnabled = false;
            if (webBrowser1.ReadyState != WebBrowserReadyState.Complete) return;
            Size szb = new Size(webBrowser1.Document.Body.OffsetRectangle.Width,
                webBrowser1.Document.Body.OffsetRectangle.Height);
            Size sz = webBrowser1.Size;

            int xbili = (int)((float)sz.Width / (float)szb.Width * 100);//水平方向缩小比例
            int ybili = (int)((float)sz.Height / (float)szb.Height * 100);//垂直方向缩小比例
            webBrowser1.Document.Body.Style = "zoom:" + xbili.ToString() + "%";
            webBrowser1.Invalidate();
        }
        const int WM_NCHITTEST = 0x0084;
        const int HT_CAPTION = 2;
        protected override void WndProc(ref Message Msg)
        {
            //禁止双击最大化
            if (Msg.Msg == 0x0112 && Msg.WParam.ToInt32() == 61490) return;
            if (Msg.Msg == WM_NCHITTEST)
            {
                //允许拖动窗体移动
                Msg.Result = new IntPtr(HT_CAPTION);
                return;
            }
            base.WndProc(ref Msg);
        }
        private void labelClose_Click(object sender, EventArgs e)
        {
            webBrowser1.Dispose();
            this.Close();
        }

        private void labelClose_MouseEnter(object sender, EventArgs e)
        {
            labelClose.ForeColor = Color.Red;
        }

        private void labelClose_MouseLeave(object sender, EventArgs e)
        {
            labelClose.ForeColor = Color.White;
        }
        //打印预览
        private void panelPrintPreview_MouseClick(object sender, MouseEventArgs e)
        {
            webBrowser1.ShowPrintPreviewDialog();
            webBrowser1.Invalidate();
        }

        private void panelPrint_MouseClick(object sender, MouseEventArgs e)
        {
            webBrowser1.ShowPrintDialog();
            webBrowser1.Invalidate();
        }
        
        private void labelPrint_MouseEnter(object sender, EventArgs e)
        {
            labelPrint.ForeColor = Color.Red;
        }

        private void labelPreviewPrint_MouseEnter(object sender, EventArgs e)
        {
            labelPreviewPrint.ForeColor = Color.Red;
        }

        private void labelPrint_MouseLeave(object sender, EventArgs e)
        {
            labelPrint.ForeColor = Color.White;
        }

        private void labelPreviewPrint_MouseLeave(object sender, EventArgs e)
        {
            labelPreviewPrint.ForeColor = Color.White;
        }
        //打印
        private void labelPrint_Click(object sender, EventArgs e)
        {
            webBrowser1.ShowPrintDialog();
            webBrowser1.Invalidate();
        }
        //打印预览
        private void labelPreviewPrint_Click(object sender, EventArgs e)
        {
            webBrowser1.ShowPrintPreviewDialog();
            webBrowser1.Invalidate();
        }

    }
}

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;
using com.lvrenyang;

namespace AIO.xaml.Print
{
    public partial class CheckReportForm : Form
    {
        public CheckReportForm()
        {
            InitializeComponent();
            this.Width = 900;
            this.Height = int.Parse(System.Windows.SystemParameters.WorkArea.Height.ToString());
        }

        // 定义dgSetPage委托进行打印时的选项设置
        public delegate void dgSetPage();
        //定义dgFileDelete 委托进行打印完成后，删除填充后的模板文件
        public delegate void dgFileDelete();

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("User32.dll", EntryPoint = "FindWindowEx")]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpClassName, string lpWindowName);
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        //定义SendMessage方法内使用的鼠标单击常量
        const int BM_CLICK = 0xF5;
        public string HtmlUrl = string.Empty;
        public bool IsLoadPdf = false;

        private void CheckReportForm_Load(object sender, EventArgs e)
        {
            if (IsLoadPdf)
            {
                this.WindowState = FormWindowState.Minimized;
                this.Width = this.Height = 0;
            }

            try
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
            catch (Exception ex)
            {
                FileUtils.OprLog(6, "", ex.ToString());
                MessageBox.Show("加载打印资源出现异常！\r\n\r\n请重新操作，若多次出现异常请联系管理员！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            webBrowser1.ShowPrintDialog();
            webBrowser1.Invalidate();
        }

        private void btn_PrintPreview_Click(object sender, EventArgs e)
        {
            webBrowser1.ShowPrintPreviewDialog();
            webBrowser1.Invalidate();
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            webBrowser1.Dispose();
            this.Close();
        }
    }
}
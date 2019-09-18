using System;
using System.Windows.Forms;

namespace WorkstationUI.Basic
{
    public partial class BasicNavigation : UserControl
    {
        public string BasicNgtitle = string.Empty;
        private static BasicNavigation instance;
        /// <summary>
        /// 返回一个该控件的实例。如果之前该控件已经被创建，直接返回已创建的控件。
        /// 此处采用单键模式对控件实例进行缓存，避免因界面切换重复创建和销毁对象。
        /// </summary>
        public static BasicNavigation Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BasicNavigation();
                }
                return instance;
            }
        }
        private static BasicNavigation mainform;
        /// <summary>
        /// 返回一个该控件的实例。如果之前该控件已经被创建，直接返回已创建的控件。
        /// 此处采用单键模式对控件实例进行缓存，避免因界面切换重复创建和销毁对象。
        /// </summary>
        public static BasicNavigation Mainform
        {
            get
            {
                if (mainform == null)
                {
                    mainform = new BasicNavigation();
                }
                return mainform;
            }
        }
        public BasicNavigation()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        private void BasicNavigation_Load(object sender, EventArgs e)
        {
            
            this.LbTitle.Text = BasicNgtitle;
        }

        private void PnlBackGround_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
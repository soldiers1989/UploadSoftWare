using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FoodClientTools
{
    /// <summary>
    /// 配置工作站软件工具,主要是修改数据库相关表字段
    /// 作者：陈国利 2011-06-12
    /// </summary>
    public partial class FrmMain : Form
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗口加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }
        /// <summary>
        /// 窗口关闭
        /// </summary>
        private void winClose()
        {
            this.Close();
            Application.Exit();
        }

        /// <summary>
        /// 环境参数配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnvirnomentForm_Click(object sender, EventArgs e)
        {
            FrmEnvirnomentSet envie = new FrmEnvirnomentSet();
            //envie.MdiParent = this;
            envie.ShowDialog(this);
        }

        /// <summary>
        /// 仪器参数配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MachineParamSet_Click(object sender, EventArgs e)
        {
            FrmMachineList mlist = new FrmMachineList();
            //mlist.MdiParent = this;
            mlist.ShowDialog(this);
        }

        /// <summary>
        /// 连接参数配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConectParamSet_Click(object sender, EventArgs e)
        {
            FrmConectSet conect = new FrmConectSet();
            //conect.MdiParent = this;
            conect.ShowDialog(this);
        }

        /// <summary>
        /// 退出窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            winClose();
        }

        /// <summary>
        /// 重叠
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        //private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    LayoutMdi(MdiLayout.ArrangeIcons);
        //}
        
        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        /// <summary>
        /// 版本信息配置 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyRightTSMI_Click(object sender, EventArgs e)
        {
            FrmVersionCopyright copyRight = new FrmVersionCopyright();
            //copyRight.MdiParent = this;
            copyRight.ShowDialog(this);
        }

        /// <summary>
        /// 系统参数配置 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SystemParamTSMI_Click(object sender, EventArgs e)
        {
            FrmSystemSet system = new FrmSystemSet();
           // system.MdiParent = this;
            system.ShowDialog(this);
        }

        /// <summary>
        /// 配置标签名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbSetTagName_Click(object sender, EventArgs e)
        {
            FrmCheckComTypeEdit tagSet = new FrmCheckComTypeEdit();
            tagSet.ShowDialog(this);
        }
        /// <summary>
        /// 数据整理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbDataMng_Click(object sender, EventArgs e)
        {
            FrmDataMng frmdata = new FrmDataMng();
            frmdata.ShowDialog(this);
        }

        /// <summary>
        /// 去掉最大化时的Icon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuStrip_ItemAdded(object sender, ToolStripItemEventArgs e)
        {
            if (e.Item.Text.Equals(""))
            {
                menuStrip.Items.RemoveAt(0);
            }
        }

     
    }
}

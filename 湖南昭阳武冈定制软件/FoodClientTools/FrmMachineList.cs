using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DY.FoodClientLib;

namespace FoodClientTools
{
    public partial class FrmMachineList : Form
    {
        //private bool IsChanged = false;
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmMachineList()
        {
            InitializeComponent();
            MessageNotify.Instance().OnMsgNotifyEvent += OnNotifyEvent; 
        }
        /// <summary>
        /// 数据库操作层类
        /// </summary>
        private readonly clsMachineOpr bll=new clsMachineOpr();


        protected void OnNotifyEvent(object sender, MessageNotify.NotifyEventArgs e)
        {
            if (e.Code == MessageNotify.NotifyInfo.InfoAdd && e.Message.Equals("001"))
            {
                //控件数据重新绑定
                showInfoList();
            }
        }   
        /// <summary>
        /// 窗体加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMachinList_Load(object sender, EventArgs e)
        {
            showInfoList();
        }

  
        /// <summary>
        /// 绑定数据列表
        /// </summary>
        private void showInfoList()
        {
            DataTable dtbl = bll.GetColumnDataTable(0,"SysCode<>'' Order by OrderId ASC", null);//.GetAsDataTable("", "OrderId ASC", 0);
            dgMachinList.DataSource = dtbl;

            dgMachinList.Columns["SysCode"].HeaderText = "编号";
            dgMachinList.Columns["MachineName"].HeaderText = "仪器名称";
            dgMachinList.Columns["ShortCut"].HeaderText = "快捷编码";
            dgMachinList.Columns["MachineModel"].HeaderText = "型号";
            dgMachinList.Columns["Company"].HeaderText = "生产厂商";
            dgMachinList.Columns["Protocol"].HeaderText = "所用插件";
            dgMachinList.Columns["LinkComNo"].HeaderText = "使用端口";
            dgMachinList.Columns["IsSupport"].HeaderText = "是否默认";
            dgMachinList.Columns["TestValue"].HeaderText = "检测值";
            dgMachinList.Columns["TestSign"].HeaderText = "检测符号";
            dgMachinList.Columns["LinkStdCode"].HeaderText = "所用标准";
            dgMachinList.Columns["IsShow"].HeaderText = "是否启用";//新增字段
            dgMachinList.Columns["OrderId"].HeaderText = "排序编号";//新增字段
            dgMachinList.Columns["SysCode"].Width = 60;
            dgMachinList.Columns["MachineName"].Width = 200;
            dgMachinList.Columns["LinkComNo"].Width = 80;
            dgMachinList.Columns["IsSupport"].Width = 70;
            dgMachinList.Columns["IsShow"].Width = 70;
            dgMachinList.Columns["OrderId"].Width = 80;

         
            //dgMachinList.Columns["SysCode"].Visible = false;
            dgMachinList.Columns["ShortCut"].Visible = false;
            dgMachinList.Columns["TestSign"].Visible = false;
            dgMachinList.Columns["LinkStdCode"].Visible = false;
        }

        /// <summary>
        /// 双击行时弹出修改对话框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgMachinList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            object obj = dgMachinList.CurrentRow.Cells["SysCode"].Value;
            string code = string.Empty;
            if (obj != null)
            {
                code = obj.ToString();
                selectRowEidt(code);
            }
        }

        /// <summary>
        /// 编辑指定行
        /// </summary>
        private void selectRowEidt(string code)
        {
            FrmMachineMng mng = new FrmMachineMng(code);
            mng.ShowDialog();
            //IsChanged = true;
        }

        /// <summary>
        /// 关闭
        /// </summary>
        private void winClose()
        {
            WindowState=FormWindowState.Normal;
            //if (IsChanged)
            //{
            //    MessageBox.Show("所配置操作，将在下次重启应用程序时起作用", "系统提示");
            //}
            MessageNotify.Instance().OnMsgNotifyEvent -= OnNotifyEvent; 
            this.Dispose();
        }

        /// <summary>
        /// 关闭窗口事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnEixt_Click(object sender, EventArgs e)
        {
            winClose();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            winClose();
            //base.OnClosing(e);
        }

        /// <summary>
        /// 编辑行按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnEdit_Click(object sender, EventArgs e)
        {
            object obj = dgMachinList.CurrentRow.Cells["SysCode"].Value;
            string code = string.Empty;
            if (obj != null)
            {
                code = obj.ToString();
                selectRowEidt(code);
            }
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnAdd_Click(object sender, EventArgs e)
        {
            selectRowEidt(string.Empty);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnDelete_Click(object sender, EventArgs e)
        {
            object obj = dgMachinList.CurrentRow.Cells["SysCode"].Value;
            string code = string.Empty;
            string sErrmsg = string.Empty;
            if (obj != null)
            {
                code = obj.ToString();
                bll.Delete(code,out sErrmsg);
                showInfoList();
                //IsChanged = true;
            }
        }
    }
}

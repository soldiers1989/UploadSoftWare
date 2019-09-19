using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FoodClient
{
    public partial class FrmMsg : Form
    {
        private string msg = string.Empty;
        private int count = 0;
        private float fontWidth = 0.8f;

        /// <summary>
        /// 重载构造函数
        /// </summary>
        /// <param name="showMsg">显示在窗口的消息</param>
        public FrmMsg(string showMsg)
        {
            InitializeComponent();
            msg = showMsg;
            lblMsg.AutoSize = true;
            lblMsg.Text = showMsg;
           
            int len = showMsg.Length;
            if (len > 0)
            {
                fontWidth = (float)lblMsg.Width / len;//每个字符的宽度
            }

            winRedraw();
        }

        /// <summary>
        ///重设置窗口大小
        /// </summary>
        private void winRedraw()
        {
            //窗口初始值
            int width = 300;
            int height = 200;

            int lblNum = msg.Length; //Label内容长度
            int rowNum = 60;  //每行显示的字数
            int colNum = 10;//列数
            int rowHeight = 15;//每行的高度
            rowNum = lblNum+5;
            if (lblNum % rowNum == 0)
            {
                colNum = lblNum / rowNum;
            }
            else
            {
                colNum = (lblNum / rowNum) + 1;
            }

            lblMsg.AutoSize = false;    //设置AutoSize
            int Xwidth = (int)(fontWidth * rowNum);//设置显示宽度
            int Yheight = rowHeight * colNum; //设置显示宽度

            width = Xwidth + 60;//字符串本身宽度+边距
            height = Yheight + 100;

            //重设窗口大小
            this.ClientSize = new Size(width, height);
            lblMsg.Width = Xwidth;
            lblMsg.Height = Yheight;

            //设置按钮位置,X为中间,高底距底部50
            this.btnOK.Location = new System.Drawing.Point(Xwidth / 2, height - 50);
        }

        /// <summary>
        /// 窗口关闭
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            timer1.Enabled = false;
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        /// <summary>
        /// 窗口加载时开始计时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMsg_Load(object sender, EventArgs e)
        {
            count = 0;
            this.timer1.Enabled = true;
        }
        /// <summary>
        /// Timer事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            count++;
            if (count >= 20)//超过2秒。因为timer步长是：100毫秒
            {
                this.DialogResult = DialogResult.OK;
                timer1.Enabled = false;
                this.Dispose();
            }
        }
    }
}

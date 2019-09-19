﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorkstationBLL.Mode;
using WorkstationModel.Model;
using System.Drawing.Drawing2D;
using WorkstationDAL.Model;

namespace WorkstationUI
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        private clsdiary dy = new clsdiary();
        private clsSetSqlData sql = new clsSetSqlData();
        private void Login_Load(object sender, EventArgs e)
        {
            //Test.clsDrawForm.Paint(sender, e);
//#if DEBUG
            this.tbx_user.Text = "admin";
            this.Txt_Password.Text = "admin";
//#endif
           
            Txt_Password.Select(Txt_Password.Text.Length, 0);//鼠标定位到密码栏最后面
            
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (tbx_user.Text == string.Empty)
            {
                MessageBox.Show("用户名不能为空","提示");
                return;
 
            }
            if(Txt_Password.Text ==string.Empty) 
            {
                MessageBox.Show("密码不能为空","提示");
                return;
            }
            //根据输入的用户名查询
            string err = string.Empty;
            StringBuilder sb = new StringBuilder();
            //displaytable.Clear();
            sb.Clear();
            sb.Append("where userlog=");
            sb.Append("'");
            sb.Append(tbx_user.Text.Trim());
            sb.Append("'");
            sb.Append(" order by ID");

            DataTable dt = sql.GetUser(sb.ToString(), out err);
            if (dt.Rows.Count < 1)
            {
                MessageBox.Show("此用户不存在，请输入正确用户！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.tbx_user.Select(0, this.tbx_user.Text.Length);
                this.tbx_user.Focus();
                return;
            }
            else if (dt.Rows.Count > 0)
            {
                if (!dt.Rows[0][1].ToString().Equals(Txt_Password.Text.Trim()))
                {
                    MessageBox.Show("密码错误，请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Txt_Password.Text = "";
                    this.Txt_Password.Focus();
                    return;
                }
            }
            Global.userlog = tbx_user.Text.Trim();
            this.Hide();
            this.ShowIcon = false;
            MainForm window = new MainForm();
            dy.savediary(DateTime.Now.ToString(),"系统登录","成功");
            window.Show();
        }

        private void Login_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (tbx_user.Text == string.Empty)
                {
                    MessageBox.Show("用户名不能为空", "提示");
                    return;

                }
                if (Txt_Password.Text == string.Empty)
                {
                    MessageBox.Show("密码不能为空", "提示");
                    return;
                }
                //根据输入的用户名查询
                string err = string.Empty;
                StringBuilder sb = new StringBuilder();
                //displaytable.Clear();
                sb.Clear();
                sb.Append("where userlog=");
                sb.Append("'");
                sb.Append(tbx_user.Text.Trim());
                sb.Append("'");
                sb.Append(" order by ID");

                DataTable dt = sql.GetUser(sb.ToString(), out err);
                if (dt.Rows.Count < 1)
                {
                    MessageBox.Show("此用户不存在，请输入正确用户！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.tbx_user.Select(0, this.tbx_user.Text.Length);
                    this.tbx_user.Focus();
                    return;
                }
                else if (dt.Rows.Count > 0)
                {
                    if (!dt.Rows[0][1].ToString().Equals(Txt_Password.Text.Trim()))
                    {
                        MessageBox.Show("密码错误，请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Txt_Password.Text = "";
                        this.Txt_Password.Focus();
                        return;
                    }
                }
                Global.userlog = tbx_user.Text.Trim();
                this.Hide();
                this.ShowIcon = false;
                MainForm window = new MainForm();
                dy.savediary(DateTime.Now.ToString(), "系统登录", "成功");
                window.Show();
            }
        }
        
        private void Txt_Password_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (tbx_user.Text == string.Empty)
                {
                    MessageBox.Show("用户名不能为空", "提示");
                    return;

                }
                if (Txt_Password.Text == string.Empty)
                {
                    MessageBox.Show("密码不能为空", "提示");
                    return;
                }
                //根据输入的用户名查询
                string err = string.Empty;
                StringBuilder sb = new StringBuilder();
                //displaytable.Clear();
                sb.Clear();
                sb.Append("where userlog=");
                sb.Append("'");
                sb.Append(tbx_user.Text.Trim());
                sb.Append("'");
                sb.Append(" order by ID");

                DataTable dt = sql.GetUser(sb.ToString(), out err);
                if (dt.Rows.Count < 1)
                {
                    MessageBox.Show("此用户不存在，请输入正确用户！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.tbx_user.Select(0, this.tbx_user.Text.Length);
                    this.tbx_user.Focus();
                    return;
                }
                else if (dt.Rows.Count>0)
                {
                    if (!dt.Rows[0][1].ToString().Equals(Txt_Password.Text.Trim()))
                    {
                        MessageBox.Show("密码错误，请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Txt_Password.Text ="";
                        this.Txt_Password.Focus();
                        return;
                    }
                }
                Global.userlog = tbx_user.Text.Trim();
                this.Hide ();
                this.ShowIcon = false;
                MainForm window = new MainForm();
                window.Show();
            }
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labelClose_Click(object sender, EventArgs e)
        {
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
        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelClose_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();       
        }

        private void Type(Control sender, int p_1, double p_2)
        {
            GraphicsPath oPath = new GraphicsPath();
            oPath.AddClosedCurve(
                new Point[] {
            new Point(0, sender.Height / p_1),
            new Point(sender.Width / p_1, 0), 
            new Point(sender.Width - sender.Width / p_1, 0), 
            new Point(sender.Width, sender.Height / p_1),
            new Point(sender.Width, sender.Height - sender.Height / p_1), 
            new Point(sender.Width - sender.Width / p_1, sender.Height), 
            new Point(sender.Width / p_1, sender.Height),
            new Point(0, sender.Height - sender.Height / p_1) },

                (float)p_2);

            sender.Region = new Region(oPath);
        }

        private void Login_Resize(object sender, EventArgs e)
        {
            //重绘时使用双缓存解决闪烁问题
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            this.Refresh();
            SetWindowRegion();
        }
        private void SetWindowRegion()
        {
            System.Drawing.Drawing2D.GraphicsPath FormPath;
            FormPath = new System.Drawing.Drawing2D.GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            FormPath = GetRoundedRectPath(rect, 21);
            this.Region = new Region(FormPath);
        }
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = radius;
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();

            // 左上角
            path.AddArc(arcRect, 180, 90);

            // 右上角
            arcRect.X = rect.Right - diameter;
            path.AddArc(arcRect, 270, 90);

            // 右下角
            arcRect.Y = rect.Bottom - diameter;
            path.AddArc(arcRect, 0, 90);

            // 左下角
            arcRect.X = rect.Left;
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();//闭合曲线
            return path;
        }

        private void tbx_user_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (tbx_user.Text == string.Empty)
                {
                    MessageBox.Show("用户名不能为空", "提示");
                    return;

                }
                if (Txt_Password.Text == string.Empty)
                {
                    MessageBox.Show("密码不能为空", "提示");
                    return;
                }
                //根据输入的用户名查询
                string err = string.Empty;
                StringBuilder sb = new StringBuilder();
                //displaytable.Clear();
                sb.Clear();
                sb.Append("where userlog=");
                sb.Append("'");
                sb.Append(tbx_user.Text.Trim());
                sb.Append("'");
                sb.Append(" order by ID");

                DataTable dt = sql.GetUser(sb.ToString(), out err);
                if (dt.Rows.Count < 1)
                {
                    MessageBox.Show("此用户不存在，请输入正确用户！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.tbx_user.Select(0, this.tbx_user.Text.Length);
                    this.tbx_user.Focus();
                    return;
                }
                else if (dt.Rows.Count > 0)
                {
                    if (!dt.Rows[0][1].ToString().Equals(Txt_Password.Text.Trim()))
                    {
                        MessageBox.Show("密码错误，请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Txt_Password.Text = "";
                        this.Txt_Password.Focus();
                        return;
                    }
                }
                Global.userlog = tbx_user.Text.Trim();
                this.Hide();
                this.ShowIcon = false;
                MainForm window = new MainForm();
                window.Show();
            }
        }
   }
}


using System.Windows.Forms;
using DY.FoodClientLib;

namespace FoodClient
{
    /// <summary>
    /// frmLogin 的摘要说明。
    /// </summary>
    public class frmLogin : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.Label lblPassWord;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtLoginID;
        private System.ComponentModel.Container components = null;
        private string tag;

        /// <summary>
        /// 构造函数
        /// </summary>
        public frmLogin()
        {
            InitializeComponent();
            tag = this.Tag.ToString();
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.lblUserID = new System.Windows.Forms.Label();
            this.lblPassWord = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtLoginID = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblUserID
            // 
            this.lblUserID.BackColor = System.Drawing.Color.Transparent;
            this.lblUserID.Location = new System.Drawing.Point(263, 102);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(42, 21);
            this.lblUserID.TabIndex = 4;
            this.lblUserID.Text = "用户：";
            this.lblUserID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPassWord
            // 
            this.lblPassWord.BackColor = System.Drawing.Color.Transparent;
            this.lblPassWord.Location = new System.Drawing.Point(263, 133);
            this.lblPassWord.Name = "lblPassWord";
            this.lblPassWord.Size = new System.Drawing.Size(42, 21);
            this.lblPassWord.TabIndex = 5;
            this.lblPassWord.Text = "密码：";
            this.lblPassWord.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(308, 133);
            this.txtPassword.MaxLength = 12;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(147, 21);
            this.txtPassword.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.LightGreen;
            this.btnOK.Location = new System.Drawing.Point(309, 175);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(56, 24);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.LightGreen;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(400, 175);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(56, 24);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtLoginID
            // 
            this.txtLoginID.Location = new System.Drawing.Point(309, 102);
            this.txtLoginID.MaxLength = 50;
            this.txtLoginID.Name = "txtLoginID";
            this.txtLoginID.Size = new System.Drawing.Size(146, 21);
            this.txtLoginID.TabIndex = 0;
            // 
            // frmLogin
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.BackColor = System.Drawing.Color.SeaGreen;
            this.BackgroundImage = global::FoodClient.Properties.Resources.Login_Logo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(493, 283);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtLoginID);
            this.Controls.Add(this.lblPassWord);
            this.Controls.Add(this.lblUserID);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "LOGIN";
            this.Text = "用户登录";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            if (tag.Equals("LOGIN"))
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            else if (tag.Equals("RELOGIN"))
            {
                this.Close();
            }
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            //检验输入的值是否合法
            //必须输入的是否已输入
            if (this.txtLoginID.Text.Trim().Equals(""))
            {
                MessageBox.Show(this, "请输入登录名!");
                this.txtLoginID.Focus();
                return;
            }

            //非法字符检查
            Control posControl;
            if (RegularCheck.HaveSpecChar(this, out posControl))
            {
                MessageBox.Show(this, "输入中有非法字符,请检查!");
                posControl.Focus();
                return;
            }

            //非字符型检查
            clsUserInfo oldUser = null;
            if (this.Tag.Equals("RELOGIN"))
            {
                oldUser = CurrentUser.GetInstance().UserInfo;
            }

            clsUserInfoOpr opr = new clsUserInfoOpr();
            clsUserInfo user = opr.GetInfo("LoginID='" + this.txtLoginID.Text.Trim() + "'");
            if (user == null)
            {
                MessageBox.Show("没有此用户，请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtLoginID.Select(0, this.txtLoginID.Text.Length);
                this.txtLoginID.Focus();
                return;
            }

            CurrentUser.GetInstance().UserInfo = user;

            if (!user.PassWord.Equals(this.txtPassword.Text.Trim()))
            {
                MessageBox.Show("您录入的密码不正确，请重新录入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtPassword.Text = "";
                this.txtPassword.Focus();
                return;
            }
            if (user.IsLock)
            {
                MessageBox.Show("没有此用户，请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtLoginID.Select(0, this.txtLoginID.Text.Length);
                this.txtLoginID.Focus();
                return;
            }

            if (this.Tag.Equals("RELOGIN") && oldUser != null && !user.UserCode.Equals(oldUser.UserCode))
            {
                //这里借用DialogResult这个返回值来标识是从“切换用户”窗体返回的。
                this.DialogResult = DialogResult.Yes;
            }

            CurrentUser.GetInstance().Unit = clsUserUnitOpr.GetNameFromCode(user.UnitCode);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frmLogin_Load(object sender, System.EventArgs e)
        {

#if DEBUG
            this.txtLoginID.Text = "admin";
            this.txtPassword.Text = "admin";
#endif

            if (this.Tag.Equals("LOGIN"))
            {
                this.Text = "用户登录";
            }
            else if (this.Tag.Equals("RELOGIN"))
            {
                this.Text = "切换用户";
                this.txtLoginID.Text = "";
                this.txtPassword.Text = "";
            }
        }

    }
}

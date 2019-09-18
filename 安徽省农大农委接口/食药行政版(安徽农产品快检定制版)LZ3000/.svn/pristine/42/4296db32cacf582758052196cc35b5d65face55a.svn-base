using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DY.FoodClientLib;


namespace FoodClientTools
{
    public partial class FrmConectSet : Form
    {
        private readonly clsSysOptOpr bll = new clsSysOptOpr();

        public FrmConectSet()
        {
            InitializeComponent();
        }

        private void FrmConectSet_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            bindData();
         
        }

        /// <summary>
        /// 绑定初始化数据
        /// 对应该数据表SysOpt 0204打头的行
        /// </summary>
        private void bindData()
        {
            DataTable dtbl = bll.GetColumnDataTable(0, "Len(SysCode)=6 AND OptType='0202' OR SysCode='030109'", "OptValue");//OR SysCode IN('030109','030110')"
              bool isEnabled =true;
            if (dtbl != null && dtbl.Rows.Count > 0)
            {
                txtServerIP.Text = dtbl.Rows[0]["OptValue"].ToString();
                txtUserName.Text = dtbl.Rows[1]["OptValue"].ToString();
                txtPassword.Text = dtbl.Rows[2]["OptValue"].ToString();
                isEnabled = Convert.ToBoolean(dtbl.Rows[3]["OptValue"].ToString());
                //chbIsDataLink.Checked = isEnabled;
                //cmbInterface.SelectedItem = dtbl.Rows[4]["OptValue"].ToString();
            }
          
            btnConectTest.Enabled = !isEnabled;
            txtPassword.Enabled = !isEnabled;
            txtServerIP.Enabled = !isEnabled;
            txtUserName.Enabled = !isEnabled; 
        }

        /// <summary>
        /// 保存操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!checkEmpty())
            {
                return;
            }
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("UPDATE tSysOpt SET OptValue='{0}' WHERE SysCode='020201'", txtServerIP.Text);
                bll.UpdateCommand(sb.ToString());
                sb.Length = 0;

                sb.AppendFormat("UPDATE tSysOpt SET OptValue='{0}' WHERE SysCode='020202'", txtUserName.Text);
                bll.UpdateCommand(sb.ToString());
                sb.Length = 0;

                sb.AppendFormat("UPDATE tSysOpt SET OptValue='{0}' WHERE SysCode='020203'", txtPassword.Text);
                bll.UpdateCommand(sb.ToString());
                sb.Length = 0;

                //sb.AppendFormat("UPDATE tSysOpt SET OptValue='{0}' WHERE SysCode='030109'", chbIsDataLink.Checked.ToString());
                //bll.UpdateCommand(sb.ToString());
                //sb.Length = 0;

                //sb.AppendFormat("UPDATE tSysOpt SET OptValue='{0}' WHERE SysCode='030110'", cmbInterface.SelectedItem.ToString());
                //bll.UpdateCommand(sb.ToString());
                //sb.Length = 0;
                MessageBox.Show("操作成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// <summary>
        /// 联网测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConectTest_Click(object sender, EventArgs e)
        {
            if (!checkEmpty())
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            //DY.WebService.DataSend send = null;
            try
            {
                //send = new DY.WebService.DataSend();
                //send.UserId = userName;
                //send.Password = pwd;
                //send.URL = ip;
                //string blrtn = send.CheckConection();
                bool flag = CheckConnection(ip, userName, pwd);
                //if (blrtn.Equals("true"))
                if(flag)
                {
                    MessageBox.Show(this, "服务器连接正常！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show(this, "服务器无法连接，请重新设置！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //if (send != null)
                //{
                //    send.Close();
                //    send = null;
                //}
                Cursor = Cursors.Default;
            }
        }

        private string ip = string.Empty;
        private string userName = string.Empty;
        private string pwd = string.Empty;

        /// <summary>
        /// 检测是否为空
        /// </summary>
        /// <returns></returns>
        private bool checkEmpty()
        {
            ip = txtServerIP.Text;
            userName = txtUserName.Text;
            pwd = txtServerIP.Text;
            if (string.IsNullOrEmpty(ip))
            {
                MessageBox.Show("服务器IP或者网址不能为空", "系统提示");
                txtServerIP.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(userName))
            {
                MessageBox.Show("服务器认证用户名不能为空", "系统提示");
                txtUserName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(pwd))
            {
                MessageBox.Show("服务器认证用户名密码不能为空", "系统提示");
                txtPassword.Focus();
                return false;
            }
            return true;
        }
        //private void chbIsDataLink_CheckedChanged(object sender, EventArgs e)
        //{
        //    bool isEnabled=!chbIsDataLink.Checked;
        //    lblInterface.Enabled = isEnabled;
        //    cmbInterface.Enabled = isEnabled;
        //    btnConectTest.Enabled = isEnabled;
        //    txtPassword.Enabled = isEnabled;
        //    txtServerIP.Enabled = isEnabled;
        //    txtUserName.Enabled = isEnabled; 
        //}

        /// <summary>
        /// 服务器测试连接
        /// </summary>
        /// <param name="serverIp">连网服务器地址</param>
        /// <param name="user">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public  bool CheckConnection(string serverIp, string user, string pwd)
        {
            if (ShareOption.InterfaceType.Equals(ShareOption.InterfaceJ2EE))
            {
                //FoodClient.localhost.DataSyncService ws=new FoodClient.localhost.DataSyncService();
                DY.WebService.ForJ2EE.DataSyncService ws = new DY.WebService.ForJ2EE.DataSyncService();
                ws.Url = serverIp;

                string blrtn = ws.CheckConnection(user, System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5").ToString());
                if (blrtn.Equals("true"))
                {
                    return true;
                    //Cursor = Cursors.Default;
                    //MessageBox.Show(this, "服务器连接正常！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    return false;
                    //Cursor = Cursors.Default;
                    //MessageBox.Show(this, "服务器无法连接，请重新设置！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else //if (ShareOption.InterfaceType.Equals(ShareOption.InterfaceDotNET))
            {
               // FoodClient.ForNet.DataDriver ws = new FoodClient.ForNet.DataDriver();
                DY.WebService.ForNet.DataDriver ws = new DY.WebService.ForNet.DataDriver();
                ws.Url = serverIp;
                string err = string.Empty;
                bool blrtn = ws.CheckConnection(user, pwd, out err);
                if (blrtn)
                {
                    return true;
                    //Cursor = Cursors.Default;
                    //MessageBox.Show(this, "服务器连接正常！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    return false;
                    //Cursor = Cursors.Default;
                    //MessageBox.Show(this, "服务器无法连接，请重新设置！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FoodClient.AnHui;
using System.Configuration;

namespace FoodClient.Basic
{
    public partial class frmNewInterface : TitleBarBase
    {
        public frmNewInterface()
        {
            InitializeComponent();
        }

        private void frmNewInterface_Load(object sender, EventArgs e)
        {
            lblDeviceName.Text = "新接口服务器配置";
            FoodClient.AnHui.Global.WebAddr = System.Configuration.ConfigurationManager.AppSettings["WebAddr"];
            FoodClient.AnHui.Global.UserName  = System.Configuration.ConfigurationManager.AppSettings["UnitID"];
            FoodClient.AnHui.Global.Password = System.Configuration.ConfigurationManager.AppSettings["UnitPwd"];
            FoodClient.AnHui.Global.UnitName = System.Configuration.ConfigurationManager.AppSettings["UnitName"];

            string nn= System.Configuration.ConfigurationManager.AppSettings["DoubleWebserverice"];
            if (nn == "1")
            {
                Global.DoubleWeb = true;
                checkBoxDouble.Checked = true;
            }
            else
            {
                Global.DoubleWeb = false;
                checkBoxDouble.Checked = false ;
            }

            string dd = System.Configuration.ConfigurationManager.AppSettings["setInterface"];
            if (dd == "xin")
            {
                Global.setInterface = true ;
                chkboxSelect.Checked = true  ;
            }
            else
            {
                Global.setInterface = false ;
                chkboxSelect.Checked = false ;
            }

            txtWebServerice.Text = FoodClient.AnHui.Global.WebAddr;
            txtChkUnitID.Text = FoodClient.AnHui.Global.UserName;
            txtPwd.Text = FoodClient.AnHui.Global.Password;
            txtUnitName.Text = FoodClient.AnHui.Global.UnitName;
        }
        /// <summary>
        /// 通信测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!Global.IsConnectInternet())
            {
                MessageBox.Show(this, "设备无法连接到互联网，请检查网络！", "系统提示");
                return;
            }
            if (txtWebServerice.Text.Trim() == "")
            {
                MessageBox.Show("服务器地址不能为空");
                return;
            }
            if (txtChkUnitID.Text.Trim() == "")
            {
                MessageBox.Show("检测单位ID不能为空");
                return;
            }
            if (txtPwd.Text.Trim() == "")
            {
                MessageBox.Show("密码不能为空");
                return;
            }

            string err = "";
            try
            {
                int index = txtWebServerice.Text.Trim().LastIndexOf('/');
                if (index == txtWebServerice.Text.Trim().Length - 1)
                {
                    Global.WebAddr = txtWebServerice.Text.Trim();
                }
                else
                {
                    Global.WebAddr = txtWebServerice.Text.Trim()+"/";
                }
                
                Global.UserName = txtChkUnitID.Text.Trim();
                Global.Password = txtPwd.Text.Trim();
                Global.UnitName = txtUnitName.Text.Trim();

                string unit = "<T_farmsDTS>"
                              + "<Farms>" 
                               + "<FARMID>" + Global.UserName + "</FARMID >"
                               + "<PWD>" + Global.Password + "</PWD> "
                                + "<NAMES>" + Global.UnitName + "</NAMES>"
                                + "<REGIONID>" + "" + "</REGIONID>"
                                + "<ADDRE>" + "" + "</ADDRE>"
                                 + "<FARMURL>" + "" + "</FARMURL>"
                                 + "<LINKMAN>" + "" + "</LINKMAN>"
                                 + "<TELEPHONE>" + "" + "</TELEPHONE>"
                                  + "<BUILDTIME>" + "" + "</BUILDTIME>"
                                 + "<USERID>"+""+"</USERID>"
                                + "<GROUPID>"+""+"</GROUPID>"
                             + "</Farms>"
                          + "</T_farmsDTS>";
                string rtn= shandong.ShanDongUpData.PostResquestHttp(unit, out err);
                if (rtn.Length > 0)
                {
                    MessageBox.Show(rtn);
                    //shandong.ResultData result = shandong.ShanDongUpData.JsonToEntity<shandong.ResultData>(rtn);
                    //if (result.message == "成功" && result.status == "0")
                    //{
                    //    MessageBox.Show("通信测试成功！\r\n检测单位正确！","提示",MessageBoxButtons.OK ,MessageBoxIcon.Information );
                    //}
                    //else if (result.message == "成功" && result.status != "0")
                    //{
                    //    MessageBox.Show("通信测试成功！\r\n检测单位不正确！错误信息为：" + result.data , "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                    //else
                    //{
                    //    MessageBox.Show("通信失败，失败原因："+result.data  , "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                }
                else
                {
                    MessageBox.Show("通信失败，失败原因", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //写入配置文件
                Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                //服务器名
                string str = ConfigurationManager.AppSettings["WebAddr"];
                if (str != null)
                    cfa.AppSettings.Settings["WebAddr"].Value = Global.WebAddr;
                else
                    cfa.AppSettings.Settings.Add("WebAddr", Global.WebAddr);
                cfa.Save();
                ////设置用户名
                str = ConfigurationManager.AppSettings["UnitID"];
                if (str != null)
                    cfa.AppSettings.Settings["UnitID"].Value = Global.UserName;
                else
                    cfa.AppSettings.Settings.Add("UnitID", Global.UserName);
                cfa.Save();
                ////设置密码
                str = ConfigurationManager.AppSettings["UnitPwd"];
                if (str != null)
                    cfa.AppSettings.Settings["UnitPwd"].Value = Global.Password;
                else
                    cfa.AppSettings.Settings.Add("UnitPwd", Global.Password);
                cfa.Save();
                //检测单位名称
                str = ConfigurationManager.AppSettings["UnitName"];
                if (str != null)
                    cfa.AppSettings.Settings["UnitName"].Value = Global.UnitName;
                else
                    cfa.AppSettings.Settings.Add("UnitName", Global.UnitName);
                cfa.Save();
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chkboxSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (chkboxSelect.Checked == true)
            {
                Global.setInterface = true;
                //写入配置文件
                Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                //服务器名
                string str = ConfigurationManager.AppSettings["setInterface"];
                if (str != null)
                    cfa.AppSettings.Settings["setInterface"].Value = "xin";
                else
                    cfa.AppSettings.Settings.Add("setInterface", "xin");
                cfa.Save();
                ConfigurationManager.RefreshSection("appSettings");
            }
            else
            {
                Global.setInterface = false;
                //写入配置文件
                Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                //服务器名
                string str = ConfigurationManager.AppSettings["setInterface"];
                if (str != null)
                    cfa.AppSettings.Settings["setInterface"].Value = "jiu";
                else
                    cfa.AppSettings.Settings.Add("setInterface", "jiu");
                cfa.Save();
                ConfigurationManager.RefreshSection("appSettings");
            }
          
        }

        private void checkBoxDouble_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDouble.Checked == true)
            {
                Global.DoubleWeb = true;
                //写入配置文件
                Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                //服务器名
                string str = ConfigurationManager.AppSettings["DoubleWebserverice"];
                if (str != null)
                    cfa.AppSettings.Settings["DoubleWebserverice"].Value = "1";
                else
                    cfa.AppSettings.Settings.Add("DoubleWebserverice", "1");
                cfa.Save();
                ConfigurationManager.RefreshSection("appSettings");
            }
            else
            {
                Global.DoubleWeb = false;
                //写入配置文件
                Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                //服务器名
                string str = ConfigurationManager.AppSettings["DoubleWebserverice"];
                if (str != null)
                    cfa.AppSettings.Settings["DoubleWebserverice"].Value = "0";
                else
                    cfa.AppSettings.Settings.Add("DoubleWebserverice", "0");
                cfa.Save();
                ConfigurationManager.RefreshSection("appSettings");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using WorkstationDAL.Model;
using WorkstationModel.UpData;
using WorkstationDAL.UpLoadData;

namespace WorkstationUI.function
{
    public partial class frmServer : Form
    {
        public frmServer()
        {
            InitializeComponent();
        }

        private void labelClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmServer_Load(object sender, EventArgs e)
        {
            Txt_Url.Text = ConfigurationManager.AppSettings["ServerAddr"];//服务器地址
            Txt_User.Text = ConfigurationManager.AppSettings["ServerName"];//用户名
            Txt_PassWord.Text = ConfigurationManager.AppSettings["ServerPassword"];//密码
            txtDetectUnit.Text = ConfigurationManager.AppSettings["UpDetectUnit"];//检测单位
            txtDetectUnitNo.Text = ConfigurationManager.AppSettings["UpDetectUnitNo"];//检测单位编号
            //txtUserName.Text = ConfigurationManager.AppSettings["IntrumentNum"];//用户昵称
            txtOrganize.Text = ConfigurationManager.AppSettings["Organize"];//机构名称
            txtOrganizeNo.Text = ConfigurationManager.AppSettings["OrganizeNo"];//机构编号
            txtDetectType.Text = ConfigurationManager.AppSettings["ChkUnitType"];//检测类型
            txtUserName.Text = ConfigurationManager.AppSettings["NickName"];//用户昵称
            if (ConfigurationManager.AppSettings["UpUnit"] == "xianshi")//UpUnit
            {
                btnChkUnitUp.Visible = true;
            }
        }
        /// <summary>
        /// 通信测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            Global.ServerAdd = Txt_Url.Text.Trim();
            Global.ServerName = Txt_User.Text.Trim();
            Global.ServerPassword = Txt_PassWord.Text.Trim();
            Global.DetectUnit = txtDetectUnit.Text.Trim();//检测单位
            Global.DetectUnitNo = txtDetectUnitNo.Text.Trim();//检测站编号         
            //通信测试
            Communication();
            this.Close();
        }
        /// <summary>
        /// 通信测试
        /// </summary>
        private void Communication()
        { 
            try
            {
                string err = "";
                //保存信息
                Global.ServerAdd = Txt_Url.Text.Trim();
                Global.ServerName = Txt_User.Text.Trim();
                Global.ServerPassword = Txt_PassWord.Text.Trim();
                Global.DetectUnit = txtDetectUnit.Text.Trim();//检测单位
                Global.DetectUnitNo = txtDetectUnitNo.Text.Trim();//检测站编号
                Global.OrganizeName = txtOrganize.Text.Trim();
                Global.OrganizeNo = txtOrganizeNo.Text.Trim();
                Global.DetectUnitType = txtDetectType.Text.Trim();
                Global.NickName = txtUserName.Text.Trim();
                //Global.pointID = Global.ServiceConnect.pointId;//检测点ID

                //Global.UploadType = cmbUpType.Text;//上传类型
                //Global.IntrumentNum = txtUserName.Text;//设备类型
                //保存服务器配置信息到配置文件
                Configuration cf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                //服务器IP设置
                string str = ConfigurationManager.AppSettings["ServerAddr"];
                if (str != null)
                {
                    cf.AppSettings.Settings["ServerAddr"].Value = Global.ServerAdd;
                }
                else
                {
                    cf.AppSettings.Settings.Add("ServerAddr", Global.ServerAdd);
                }
                //服务器名
                string strname = ConfigurationManager.AppSettings["ServerName"];
                if (strname != null)
                {
                    cf.AppSettings.Settings["ServerName"].Value = Global.ServerName;
                }
                else
                {
                    cf.AppSettings.Settings.Add("ServerName", Global.ServerName);
                }
                //服务器密码
                string pwd = ConfigurationManager.AppSettings["ServerPassword"];
                if (pwd != null)
                {
                    cf.AppSettings.Settings["ServerPassword"].Value = Global.ServerPassword;
                }
                else
                {
                    cf.AppSettings.Settings.Add("ServerPassword", Global.ServerPassword);
                }
                //检测站名称
                string unitname = ConfigurationManager.AppSettings["UpDetectUnit"];
                if (unitname != null)
                {
                    cf.AppSettings.Settings["UpDetectUnit"].Value = Global.DetectUnit;
                }
                else
                {
                    cf.AppSettings.Settings.Add("UpDetectUnit", Global.DetectUnit);
                }
                //检测站编号
                string unitno = ConfigurationManager.AppSettings["UpDetectUnitNo"];
                if (unitno != null)
                {
                    cf.AppSettings.Settings["UpDetectUnitNo"].Value = Global.DetectUnitNo;
                }
                else
                {
                    cf.AppSettings.Settings.Add("UpDetectUnitNo", Global.DetectUnitNo);
                }
                //机构名称
                string Org = ConfigurationManager.AppSettings["Organize"];
                if (Org != null)
                {
                    cf.AppSettings.Settings["Organize"].Value = Global.OrganizeName;
                }
                else
                {
                    cf.AppSettings.Settings.Add("Organize", Global.OrganizeName);
                }
                //机构编号
                string OrgNo = ConfigurationManager.AppSettings["OrganizeNo"];
                if (OrgNo != null)
                {
                    cf.AppSettings.Settings["OrganizeNo"].Value = Global.OrganizeNo;
                }
                else
                {
                    cf.AppSettings.Settings.Add("OrganizeNo", Global.OrganizeNo);
                }
                //检测站类型
                string DecUnitType = ConfigurationManager.AppSettings["ChkUnitType"];
                if (DecUnitType != null)
                {
                    cf.AppSettings.Settings["ChkUnitType"].Value = Global.DetectUnitType;
                }
                else
                {
                    cf.AppSettings.Settings.Add("ChkUnitType", Global.DetectUnitType);
                }
                //用户昵称
                string UName = ConfigurationManager.AppSettings["NickName"];
                if (UName != null)
                {
                    cf.AppSettings.Settings["NickName"].Value = Global.NickName;
                }
                else
                {
                    cf.AppSettings.Settings.Add("NickName", Global.NickName);
                }
                //检测点ID
                string pointID = ConfigurationManager.AppSettings["pointID"];
                if (pointID != null)
                {
                    cf.AppSettings.Settings["pointID"].Value = Global.pointID;
                }
                else
                {
                    cf.AppSettings.Settings.Add("pointID", Global.pointID);
                }

                cf.Save();
                ConfigurationManager.RefreshSection("appSettings");
                //string data = InterfaceHelper.CheckUserCommunication(out err);
                //SettingServiceConnect(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        /// <summary>
        /// 解析通信返回数据
        /// </summary>
        /// <param name="json"></param>
        private void SettingServiceConnect(string json)
        {
            ResultMsg msgResult = null;
            try
            {
                msgResult = JsonHelper.JsonToEntity<ResultMsg>(json);
                if (msgResult == null)
                {
                    MessageBox.Show("验证失败!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (msgResult.resultCode.Equals("success1"))
                {
                    checkUserConnect user = JsonHelper.JsonToEntity<checkUserConnect>(msgResult.result.ToString());//(checkUserConnect)JsonConvert.DeserializeObject(msgResult.result.ToString(), typeof(checkUserConnect));
                    if (user != null)
                    {
                        Global.ServiceConnect = user;
                        txtOrganize.Text = Global.ServiceConnect.orgName; //机构名称
                        txtOrganizeNo.Text = Global.ServiceConnect.orgNum;//机构编号
                        txtDetectUnit.Text = Global.ServiceConnect.pointName;//检测点名称
                        txtDetectUnitNo.Text = Global.ServiceConnect.pointNum;//检测点编号
                        txtDetectType.Text = Global.ServiceConnect.pointType;//检测点类型
                        txtUserName.Text = Global.ServiceConnect.nickName;//用户昵称
                        //保存信息
                        Global.ServerAdd = Txt_Url.Text.Trim();
                        Global.ServerName = Txt_User.Text.Trim();
                        Global.ServerPassword = Txt_PassWord.Text.Trim();
                        Global.DetectUnit = txtDetectUnit.Text.Trim();//检测单位
                        Global.DetectUnitNo = txtDetectUnitNo.Text.Trim();//检测站编号
                        Global.OrganizeName = txtOrganize.Text.Trim();
                        Global.OrganizeNo = txtOrganizeNo.Text.Trim();
                        Global.DetectUnitType = txtDetectType.Text.Trim();
                        Global.NickName = txtUserName.Text.Trim();
                        Global.pointID = Global.ServiceConnect.pointId;//检测点ID

                        //Global.UploadType = cmbUpType.Text;//上传类型
                        //Global.IntrumentNum = txtUserName.Text;//设备类型
                        //保存服务器配置信息到配置文件
                        Configuration cf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        //服务器IP设置
                        string str = ConfigurationManager.AppSettings["ServerAddr"];
                        if (str != null)
                        {
                            cf.AppSettings.Settings["ServerAddr"].Value = Global.ServerAdd;
                        }
                        else
                        {
                            cf.AppSettings.Settings.Add("ServerAddr", Global.ServerAdd);
                        }
                        //服务器名
                        string strname = ConfigurationManager.AppSettings["ServerName"];
                        if (strname != null)
                        {
                            cf.AppSettings.Settings["ServerName"].Value = Global.ServerName;
                        }
                        else
                        {
                            cf.AppSettings.Settings.Add("ServerName", Global.ServerName);
                        }
                        //服务器密码
                        string pwd = ConfigurationManager.AppSettings["ServerPassword"];
                        if (pwd != null)
                        {
                            cf.AppSettings.Settings["ServerPassword"].Value = Global.ServerPassword;
                        }
                        else
                        {
                            cf.AppSettings.Settings.Add("ServerPassword", Global.ServerPassword);
                        }
                        //检测站名称
                        string unitname = ConfigurationManager.AppSettings["UpDetectUnit"];
                        if (unitname != null)
                        {
                            cf.AppSettings.Settings["UpDetectUnit"].Value = Global.DetectUnit;
                        }
                        else
                        {
                            cf.AppSettings.Settings.Add("UpDetectUnit", Global.DetectUnit);
                        }
                        //检测站编号
                        string unitno = ConfigurationManager.AppSettings["UpDetectUnitNo"];
                        if (unitno != null)
                        {
                            cf.AppSettings.Settings["UpDetectUnitNo"].Value = Global.DetectUnitNo;
                        }
                        else
                        {
                            cf.AppSettings.Settings.Add("UpDetectUnitNo", Global.DetectUnitNo);
                        }
                        //机构名称
                        string Org = ConfigurationManager.AppSettings["Organize"];
                        if (Org != null)
                        {
                            cf.AppSettings.Settings["Organize"].Value = Global.OrganizeName;
                        }
                        else
                        {
                            cf.AppSettings.Settings.Add("Organize", Global.OrganizeName);
                        }
                        //机构编号
                        string OrgNo = ConfigurationManager.AppSettings["OrganizeNo"];
                        if (OrgNo != null)
                        {
                            cf.AppSettings.Settings["OrganizeNo"].Value = Global.OrganizeNo;
                        }
                        else
                        {
                            cf.AppSettings.Settings.Add("OrganizeNo", Global.OrganizeNo);
                        }
                        //检测站类型
                        string DecUnitType = ConfigurationManager.AppSettings["ChkUnitType"];
                        if (DecUnitType != null)
                        {
                            cf.AppSettings.Settings["ChkUnitType"].Value = Global.DetectUnitType;
                        }
                        else
                        {
                            cf.AppSettings.Settings.Add("ChkUnitType", Global.DetectUnitType);
                        }
                        //用户昵称
                        string UName = ConfigurationManager.AppSettings["NickName"];
                        if (UName != null)
                        {
                            cf.AppSettings.Settings["NickName"].Value = Global.NickName;
                        }
                        else
                        {
                            cf.AppSettings.Settings.Add("NickName", Global.NickName);
                        }
                        //检测点ID
                        string pointID = ConfigurationManager.AppSettings["pointID"];
                        if (pointID != null)
                        {
                            cf.AppSettings.Settings["pointID"].Value = Global.pointID;
                        }
                        else
                        {
                            cf.AppSettings.Settings.Add("pointID", Global.pointID);
                        }

                        cf.Save();
                        ConfigurationManager.RefreshSection("appSettings");

                        MessageBox.Show("通信测试成功!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
                else
                {
                    MessageBox.Show(msgResult.resultDescripe, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
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
        /// 同步检测单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChkUnitUp_Click(object sender, EventArgs e)
        {
            frmUpUnitData frm = new frmUpUnitData();
            frm.ShowDialog();
        }
    }
}

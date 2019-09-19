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
using WorkstationModel.Model;
using WorkstationModel.beihai;
using WorkstationModel.function;

namespace WorkstationUI.function
{
    public partial class frmServer : Form
    {
        private clsdiary dy = new clsdiary();
        private StringBuilder sb = new StringBuilder();
        private string err = "";
        private string rtn = "";
        private bool isrepair = false;
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
            if (Global.Platform != "DYKJFW" && Global.Platform != "DYBus")
            {
                btnRegiter.Visible = false;
                txtOrganize.Visible = false;
                txtOrganizeNo.Visible = false;
                txtDetectUnit.Visible = false;
                txtDetectUnitNo.Visible = false;
                txtDetectType.Visible = false;
                txtUserName.Visible = false;
                txtMachineSerial.Visible = false;
                txtmodel.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
                label9.Visible = false;
                label11.Visible = false;

            }
            

            txtOrganize.Text = ConfigurationManager.AppSettings["Organize"];//机构名称
            txtOrganizeNo.Text = ConfigurationManager.AppSettings["OrganizeNo"];//检测单位编号
            txtDetectUnit.Text = ConfigurationManager.AppSettings["UpDetectUnit"];//检测单位l
            txtDetectUnitNo.Text = ConfigurationManager.AppSettings["UpDetectUnitNo"];//检测单位编号
            txtDetectType.Text = ConfigurationManager.AppSettings["ChkUnitType"];//检测站类型
            txtUserName.Text = ConfigurationManager.AppSettings["NickName"];//检测站类型
            txtMachineSerial.Text = ConfigurationManager.AppSettings["IntrumentSeriersNum"];//仪器系列号
            txtmodel.Text = ConfigurationManager.AppSettings["InstrumentNameModel"]; //仪器型号
            Txt_Url.Text = ConfigurationManager.AppSettings["ServerAddr"];//服务器地址
            Txt_User.Text = ConfigurationManager.AppSettings["ServerName"];//用户名
            Txt_PassWord.Text = ConfigurationManager.AppSettings["ServerPassword"];//密码
            //Global.DetectUnitNo = ConfigurationManager.AppSettings["UpDetectUnitNo"];//检测单位编号
            //txtUserName.Text = ConfigurationManager.AppSettings["IntrumentNum"];//用户昵称
            //txtOrganizeNo.Text = ConfigurationManager.AppSettings["OrganizeNo"];//机构编号
            //txtDetectType.Text = ConfigurationManager.AppSettings["ChkUnitType"];//检测类型
            //txtUserName.Text = ConfigurationManager.AppSettings["NickName"];//用户昵称
            if (ConfigurationManager.AppSettings["UpUnit"] == "xianshi")//UpUnit
            {
                btnChkUnitUp.Visible = true;
            }
            dy.savediary(DateTime.Now.ToString(), "进入服务器配置", "成功");
        }
        /// <summary>
        /// 通信测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCommunicate_Click(object sender, EventArgs e)
        {
            //if(isrepair == true)
            //{
            //    MessageBox.Show("服务地址已修改，请重启软件！","提示");
            //    return;
            //}
            BtnCommunicate.Enabled = false;
            string ReturnMessage = "";
            try
            {
                Global.ServerAdd = Txt_Url.Text.Trim();
                Global.ServerName = Txt_User.Text.Trim();
                Global.ServerPassword = Txt_PassWord.Text.Trim();
                //Global.DetectUnit = txtDetectUnit.Text.Trim();//检测单位
                //Global.DetectUnitNo = txtDetectUnitNo.Text.Trim();//检测站编号   
                if (Global.linkNet() == false)
                {
                    MessageBox.Show("无法连接到互联网，请检查网络连接！", "系统提示");
                    BtnCommunicate.Enabled = true;
                    return;
                }
                if (Global.Platform == "DYBus")
                {
                    Communication();
                }
                else if (Global.Platform == "DYKJFW")
                {
                    KJFWcommunicate();
                }
                else if (Global.Platform == "hexian")//鹤县
                {
                    HXConmunicate();
                    
                }
                //Global.ServerAdd = Txt_Url.Text.Trim();
                //Global.ServerName = Txt_User.Text.Trim();
                //Global.ServerPassword = Txt_PassWord.Text.Trim();
                //Global.DetectUnit = txtDetectUnit.Text.Trim();//检测单位
                 
                //ReturnMessage=clsHpptPost.BeihaiCommunicateTest(Global.ServerAdd, Global.ServerName, Global.ServerPassword, 1, out err);
                //if (ReturnMessage.Contains("status"))
                //{
                //    clsCommunication com = JsonHelper.JsonToEntity<clsCommunication>(ReturnMessage);
                //    if (com.status == "1")
                //    {
                //        Global.CheckUnitcode = com.unit;
                //        txtOrganizeNo.Text = Global.CheckUnitcode;
                //        MessageBox.Show("通信测试成功！","系统提示",MessageBoxButtons.OK ,MessageBoxIcon.Information);
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("通信测试失败，请检查设置！","提示");
                //}
              
                // 保存 applicationSettings 范围的设置
                //string configFileName = AppDomain.CurrentDomain.BaseDirectory + "数据采集系统.exe.config";
                //System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                //doc.Load(configFileName);
                //string configString = @"configuration/applicationSettings/WorkstationUI.Properties.Settings/setting[@name='数据采集系统_WebReference_PutService']/value";
                //System.Xml.XmlNode configNode = doc.SelectSingleNode(configString);
                //if (configNode != null)
                //{
                //    configNode.InnerText = Global.ServerAdd;
                //    doc.Save(configFileName);
                //    // 刷新应用程序设置，这样下次读取时才能读到最新的值。
                //    Properties.Settings.Default.Reload();
                //}

                BtnCommunicate.Enabled = true ;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"通信测试",MessageBoxButtons.OK ,MessageBoxIcon.Warning );
                BtnCommunicate.Enabled = true;
            }
        }
        /// <summary>
        /// 鹤县通信测试
        /// </summary>
        private void HXConmunicate()
        {
            if (Global.ServerAdd=="")
            {
                MessageBox.Show("服务器地址不能为空","通信测试",MessageBoxButtons.OK ,MessageBoxIcon.Warning );
                BtnCommunicate.Enabled = true;
                return;
            }
            if (Global.ServerName == "")
            {
                MessageBox.Show("服务器用户名不能为空", "通信测试", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BtnCommunicate.Enabled = true;
                return;
            }
            if (Global.ServerPassword  == "")
            {
                MessageBox.Show("服务器密码不能为空", "通信测试", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BtnCommunicate.Enabled = true;
                return;
            }
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
            cf.Save();

            ConfigurationManager.RefreshSection("appSettings");
            //保存 applicationSettings 范围的设置
            string configFileName = AppDomain.CurrentDomain.BaseDirectory + "数据采集系统.exe.config";
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(configFileName);
            string configString = @"configuration/applicationSettings/WorkstationUI.Properties.Settings/setting[@name='数据采集系统_com_tainot_foodsafe_wireDataService']/value";
            System.Xml.XmlNode configNode = doc.SelectSingleNode(configString);
            if (configNode != null)
            {
                configNode.InnerText = Global.ServerAdd;
                doc.Save(configFileName);
                // 刷新应用程序设置，这样下次读取时才能读到最新的值。
                Properties.Settings.Default.Reload();
            }

            Global.Ticket = "";
            com.tainot.foodsafe.wireDataService wds = new com.tainot.foodsafe.wireDataService();
            Global.Ticket = wds.GetTicket(Global.ServerName, Global.ServerPassword);

            if (Global.Ticket != "")
            {
                MessageBox.Show("通信测试成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("通信测试失败！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// 
        /// 快检服务通信测试
        /// </summary>
        private void KJFWcommunicate()
        {
            rtn=QuickInspectServing.QuickTestServerLogin(Global.ServerAdd, Global.ServerName, Global.ServerPassword, 1);
            FilesRW.KLog(rtn, "接收", 1);
            if (rtn.Contains("success") || rtn.Contains("msg"))
            {
                ResultData Jresult = JsonHelper.JsonToEntity<ResultData>(rtn);
                if (Jresult.msg == "操作成功" && Jresult.success == true)
                {
                    objdata obj = JsonHelper.JsonToEntity<objdata>(Jresult.obj.ToString());
                    Global.Token = obj.token;

                    userdata ud = JsonHelper.JsonToEntity<userdata>(obj.user.ToString());
                    txtOrganize.Text = ud.d_depart_name;
                    txtOrganizeNo.Text = ud.d_depart_code;
                    txtDetectUnit.Text = ud.p_point_name;
                    txtDetectUnitNo.Text = ud.p_point_code;//检测单位编号
                    txtDetectType.Text = ud.p_point_type;//检测点类型
                    txtUserName.Text = ud.realname;//用户昵称
                    Global.d_depart_name = ud.d_depart_name;
                    Global.depart_id = ud.depart_id;
                    Global.p_point_name = ud.p_point_name;
                    Global.point_id = ud.point_id;
                    Global.user_name = ud.user_name;
                    Global.id = ud.id;
                    Global.realname = ud.realname;
                    Global.pointName = ud.p_point_name;
                    //Global.orgName = ud.d_depart_name;

                    //保存服务器配置信息到配置文件
                    Configuration cf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    //机构名称
                    string Org = ConfigurationManager.AppSettings["Organize"];
                    if (Org != null)
                    {
                        cf.AppSettings.Settings["Organize"].Value = Global.d_depart_name;
                    }
                    else
                    {
                        cf.AppSettings.Settings.Add("Organize", Global.d_depart_name);
                    }

                    //机构编号
                    string OrgNo = ConfigurationManager.AppSettings["OrganizeNo"];
                    if (OrgNo != null)
                    {
                        cf.AppSettings.Settings["OrganizeNo"].Value = Global.depart_id;
                    }
                    else
                    {
                        cf.AppSettings.Settings.Add("OrganizeNo", Global.depart_id);
                    }

                    //检测站名称
                    string unitname = ConfigurationManager.AppSettings["UpDetectUnit"];
                    if (unitname != null)
                    {
                        cf.AppSettings.Settings["UpDetectUnit"].Value = Global.p_point_name;
                    }
                    else
                    {
                        cf.AppSettings.Settings.Add("UpDetectUnit", Global.p_point_name);
                    }
                    //检测站编号
                    string unitno = ConfigurationManager.AppSettings["UpDetectUnitNo"];
                    if (unitno != null)
                    {
                        cf.AppSettings.Settings["UpDetectUnitNo"].Value = Global.point_id;
                    }
                    else
                    {
                        cf.AppSettings.Settings.Add("UpDetectUnitNo", Global.point_id);
                    }

                    //检测站类型
                    string DecUnitType = ConfigurationManager.AppSettings["ChkUnitType"];
                    if (DecUnitType != null)
                    {
                        cf.AppSettings.Settings["ChkUnitType"].Value = txtDetectType.Text.Trim();
                    }
                    else
                    {
                        cf.AppSettings.Settings.Add("ChkUnitType", txtDetectType.Text.Trim());
                    }
                    //用户昵称
                    string UName = ConfigurationManager.AppSettings["NickName"];
                    if (UName != null)
                    {
                        cf.AppSettings.Settings["NickName"].Value = Global.realname;
                    }
                    else
                    {
                        cf.AppSettings.Settings.Add("NickName", Global.realname);
                    }

                    string Mmodel = ConfigurationManager.AppSettings["InstrumentNameModel"];
                    if (Mmodel != null)
                    {
                        cf.AppSettings.Settings["NickName"].Value = txtmodel.Text.Trim();
                    }
                    else
                    {
                        cf.AppSettings.Settings.Add("NickName", txtmodel.Text.Trim());
                    }

                    cf.Save();
                    ConfigurationManager.RefreshSection("appSettings");

                    MessageBox.Show("通信测试成功！","系统提示",MessageBoxButtons.OK ,MessageBoxIcon.Information );
                }
                else
                {
                    MessageBox.Show("通信失败，失败原因：" + Jresult.msg);
                }
            }
            else
            {
                MessageBox.Show("通信失败，失败原因：" + rtn);
            }
           
        }

        /// <summary>
        /// 快检车通信信息
        /// </summary>
        private void Communication()
        {
            try
            {
                string err = "";
                string data = InterfaceHelper.CheckUserCommunication(out err);
                SettingServiceConnect(data);
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
        /// <summary>
        /// 仪器注册
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegiter_Click(object sender, EventArgs e)
        {
            try
            {
                Global.MachineSerialCode=txtmodel.Text.Trim();
                string RAddr = QuickInspectServing.GetServiceURL(Global.ServerAdd, 7);//地址
                sb.Length = 0;
                sb.Append(RAddr);
                sb.AppendFormat("?userToken={0}", Global.Token);
                sb.AppendFormat("&series={0}", Global.MachineSerialCode);//DY-3500(I)
                sb.AppendFormat("&mac={0}", Global.GetMACComputer());
                sb.AppendFormat("&param1={0}", "");
                sb.AppendFormat("&param2={0}", "");
                sb.AppendFormat("&param3={0}", "");
                string Rlist = QuickInspectServing.HttpsPost(sb.ToString());
                ResultData Zresult = JsonHelper.JsonToEntity<ResultData>(Rlist);
                if (Zresult.success  == true )
                {
                    zhuce zdata = JsonHelper.JsonToEntity<zhuce>(Zresult.obj.ToString());
                    Global.MachineSerialCode = zdata.serial_number;
                    txtMachineSerial.Text = Global.MachineSerialCode;
                    MessageBox.Show("仪器注册成功！\r\n仪器系列号：" + Global.MachineSerialCode);
                }
                else
                {
                    MessageBox.Show("仪器注册失败！\r\n失败原因：" + Zresult.msg);
                }
                //保存服务器配置信息到配置文件
                Configuration cf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                //仪器系列号
                string pointID = ConfigurationManager.AppSettings["IntrumentSeriersNum"];
                if (pointID != null)
                {
                    cf.AppSettings.Settings["IntrumentSeriersNum"].Value = Global.MachineSerialCode;
                }
                else
                {
                    cf.AppSettings.Settings.Add("IntrumentSeriersNum", Global.MachineSerialCode);
                }



                cf.Save();
                ConfigurationManager.RefreshSection("appSettings");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Txt_Url_KeyUp(object sender, KeyEventArgs e)
        {
            isrepair = true;
        }

      
    }
}

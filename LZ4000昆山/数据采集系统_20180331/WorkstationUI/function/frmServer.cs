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

namespace WorkstationUI.function
{
    public partial class frmServer : Form
    {
        private clsdiary dy = new clsdiary();
        private string err = "";
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
            txtOrganizeNo.Text = ConfigurationManager.AppSettings["UpDetectUnitNo"];//检测单位编号
            Global.DetectUnit = ConfigurationManager.AppSettings["UpDetectUnit"];//检测单位
            Global.DetectUnitNo = ConfigurationManager.AppSettings["UpDetectUnitNo"];//检测单位编号
            //txtUserName.Text = ConfigurationManager.AppSettings["IntrumentNum"];//用户昵称
            txtOrganize.Text = ConfigurationManager.AppSettings["Organize"];//机构名称
            //txtOrganizeNo.Text = ConfigurationManager.AppSettings["OrganizeNo"];//机构编号
            txtDetectType.Text = ConfigurationManager.AppSettings["ChkUnitType"];//检测类型
            txtUserName.Text = ConfigurationManager.AppSettings["NickName"];//用户昵称
            txtManufacture.Text = ConfigurationManager.AppSettings["InstrumManufact"];//检测设备厂家
            txtMachineModel.Text = ConfigurationManager.AppSettings["MachineModel"];//设备型号
            txtMachineSerial.Text = ConfigurationManager.AppSettings["MachineSerial"];//设备系列号

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
            try
            {
                BtnCommunicate.Enabled = false;
                if (Global.linkNet() == false)
                {
                    MessageBox.Show("无法连接到互联网，请检查网络连接！", "系统提示");
                    BtnCommunicate.Enabled = true;
                    return;
                }

                if (Txt_Url.Text.Trim()=="")
                {
                    MessageBox.Show("服务器地址不能为空","提示");
                    BtnCommunicate.Enabled = true;
                    return;
                }
                if (Txt_User.Text.Trim() == "")
                {
                    MessageBox.Show("用户名不能为空", "提示");
                    BtnCommunicate.Enabled = true;
                    return;
                }
                if (Txt_PassWord.Text.Trim() == "")
                {
                    MessageBox.Show("密码不能为空", "提示");
                    BtnCommunicate.Enabled = true;
                    return;
                }
                if (txtManufacture.Text.Trim() == "")
                {
                    MessageBox.Show("设备厂家不能为空", "提示");
                    BtnCommunicate.Enabled = true;
                    return;
                }
                if (txtMachineModel.Text.Trim() == "")
                {
                    MessageBox.Show("设备型号不能为空", "提示");
                    BtnCommunicate.Enabled = true;
                    return;
                }
                if (txtMachineSerial.Text.Trim() == "")
                {
                    MessageBox.Show("设备系列号不能为空", "提示");
                    BtnCommunicate.Enabled = true;
                    return;
                }

                Global.ServerAdd = Txt_Url.Text.Trim();
                Global.ServerName = Txt_User.Text.Trim();
                Global.ServerPassword = Txt_PassWord.Text.Trim();
                //Global.DetectUnit = txtDetectUnit.Text.Trim();//检测单位

                string address=Txt_Url.Text.Trim();
                //if (address.Substring(address.Length - 1, 1) == "/")
                //{
                //    address = address + "sDataInfrace.asmx";
                //}
                //else
                //{
                //    address = address + "/sDataInfrace.asmx";
                //}
                // 保存 applicationSettings 范围的设置
                if (address != "")
                {
                    string configFileName = AppDomain.CurrentDomain.BaseDirectory + "数据采集系统.exe.config";
                    System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                    doc.Load(configFileName);
                    string configString = @"configuration/applicationSettings/WorkstationUI.Properties.Settings/setting[@name='数据采集系统_com_szscgl_ncp_sDataInfrace']/value";
                    System.Xml.XmlNode configNode = doc.SelectSingleNode(configString);
                    if (configNode != null)
                    {
                        configNode.InnerText = address;
                        doc.Save(configFileName);
                        // 刷新应用程序设置，这样下次读取时才能读到最新的值。
                        Properties.Settings.Default.Reload();
                    }
                }
                KunShanEntity.GetTokenRequest.webService getTokenRequest = new KunShanEntity.GetTokenRequest.webService();
                getTokenRequest.request = new KunShanEntity.GetTokenRequest.Request();
                getTokenRequest.request.name = Global.ServerName;
                getTokenRequest.request.password = Global.MD5(Global.ServerPassword);
                string  response = XmlHelper.EntityToXml<KunShanEntity.GetTokenRequest.webService>(getTokenRequest);
                //签到 获取令牌
                com.szscgl.ncp.sDataInfrace webserver = new com.szscgl.ncp.sDataInfrace();
                string ReturnMessage = webserver.checkIn(response);
                KunShanEntity.GetTokenResponse.webService getTokenResponse = XmlHelper.XmlToEntity<KunShanEntity.GetTokenResponse.webService>(ReturnMessage);
                if (getTokenResponse != null && getTokenResponse.response.error.Length == 0 && getTokenResponse.response.tokenNo.Length > 0)
                {
                    Global.Token = getTokenResponse.response.tokenNo; 
                }
                else if (getTokenResponse.response.error.Length > 0)
                {
                    MessageBox.Show("通信失败：" + getTokenResponse.response.error);
                    //errMsg = getTokenResponse.response.error;
                }
                //获取市场信息
                if (getTokenResponse.response.error.Length == 0)
                {
                    KunShanEntity.QueryMarketRequest.webService getQueryMarket = new KunShanEntity.QueryMarketRequest.webService();
                    getQueryMarket.request = new KunShanEntity.QueryMarketRequest.Request();
                    getQueryMarket.request.name = Global.ServerName;
                    getQueryMarket.request.password = Global.MD5(Global.ServerPassword);
                    response = XmlHelper.EntityToXml<KunShanEntity.QueryMarketRequest.webService>(getQueryMarket);
                    //System.Console.WriteLine(string.Format("QueryMarket-Request:{0}", response));
                    string rtnweb = webserver.QueryMarket(response);
                    KunShanEntity.QueryMarketResponse.Response market = null;
                    market = JsonHelper.JsonToEntity<KunShanEntity.QueryMarketResponse.Response>(rtnweb.Replace("[", "").Replace("]", ""));
                    if (market != null)
                    {
                        txtDetectUnit.Text = market.MarketName;
                        txtDetectUnitNo.Text = market.LicenseNo;
                        Global.DetectUnit=market.MarketName;
                        Global.DetectUnitNo =market.LicenseNo;
                        Global.communicate = "通信测试";
                        MessageBox.Show("通讯成功！", "系统提示",MessageBoxButtons.OK ,MessageBoxIcon.Information);
                    }
 
                }
                Communication();
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


                //this.Close();
                BtnCommunicate.Enabled = true ;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                BtnCommunicate.Enabled = true;
            }
        }
        /// <summary>
        /// 保存信息
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
                Global.IntrumManifacture = txtManufacture.Text.Trim();//设备厂家
                Global.MachineModel = txtMachineModel.Text.Trim();//设备型号
                Global.MachineSerial = txtMachineSerial.Text.Trim();//设备系列号

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
                //设备厂家
                string MachineMacfacure = ConfigurationManager.AppSettings["InstrumManufact"];
                if (MachineMacfacure != null)
                {
                    cf.AppSettings.Settings["InstrumManufact"].Value = Global.IntrumManifacture;
                }
                else
                {
                    cf.AppSettings.Settings.Add("InstrumManufact", Global.IntrumManifacture);
                }
                //设备型号
                string MachineModel = ConfigurationManager.AppSettings["MachineModel"];
                if (MachineMacfacure != null)
                {
                    cf.AppSettings.Settings["MachineModel"].Value = Global.MachineModel ;
                }
                else
                {
                    cf.AppSettings.Settings.Add("MachineModel", Global.MachineModel);
                }
                //设备系列号
                string MachineSerial = ConfigurationManager.AppSettings["MachineSerial"];
                if (MachineMacfacure != null)
                {
                    cf.AppSettings.Settings["MachineSerial"].Value = Global.MachineSerial ;
                }
                else
                {
                    cf.AppSettings.Settings.Add("MachineSerial", Global.MachineSerial);
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

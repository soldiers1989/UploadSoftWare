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
using WorkstationDAL.yc;
using WorkstationBLL.Mode;
using DY.Process;

namespace WorkstationUI.function
{
    public partial class frmServer : Form
    {
        private clsdiary dy = new clsdiary();
        private StringBuilder sb = new StringBuilder();
        private string err = "";
        private string rtn = "";
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
            txtProductor.Text = ConfigurationManager.AppSettings["InstrumManufact"];

            //Global.DetectUnitNo = ConfigurationManager.AppSettings["UpDetectUnitNo"];//检测单位编号
            //txtUserName.Text = ConfigurationManager.AppSettings["IntrumentNum"];//用户昵称
            //txtOrganizeNo.Text = ConfigurationManager.AppSettings["OrganizeNo"];//机构编号
            //txtDetectType.Text = ConfigurationManager.AppSettings["ChkUnitType"];//检测类型
            //txtUserName.Text = ConfigurationManager.AppSettings["NickName"];//用户昵称
            if (ConfigurationManager.AppSettings["UpUnit"] == "xianshi")//UpUnit
            {
                btnChkUnitUp.Visible = true;
            }
            //dy.savediary(DateTime.Now.ToString(), "进入服务器配置", "成功");
        }
        /// <summary>
        /// 通信测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCommunicate_Click(object sender, EventArgs e)
        {
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

                YCcommunicate();

                BtnCommunicate.Enabled = true ;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                BtnCommunicate.Enabled = true;
            }
        }
        private void YCcommunicate()
        {
            if (Txt_Url.Text.Trim().Length ==0)
            {
                MessageBox.Show("服务器地址不能为空！","提示",MessageBoxButtons.OK ,MessageBoxIcon.Warning );
            }
            if (Txt_User.Text.Trim().Length==0)
            {
                MessageBox.Show("用户名不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (Txt_PassWord.Text.Trim().Length == 0)
            {
                MessageBox.Show("密码不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (txtMachineSerial.Text.Trim().Length == 0)
            {
                MessageBox.Show("设备唯一码不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (txtmodel.Text.Trim().Length == 0)
            {
                MessageBox.Show("设备型号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (txtProductor.Text.Trim().Length == 0)
            {
                MessageBox.Show("设备厂家不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            Global.ServerAdd = Txt_Url.Text.Trim();
            Global.ServerName = Txt_User.Text.Trim();
            Global.ServerPassword = Txt_PassWord.Text.Trim();
            Global.MachineSerialCode = txtMachineSerial.Text.Trim();//设备唯一码
            Global.MachineModel = txtmodel.Text.Trim();//设备型号
            Global.MachineProductor = txtProductor.Text.Trim();//设备厂家

            string url = getUrl.GetUrl(Txt_Url.Text.Trim(), 5);
            updateDeviceInfo u = new updateDeviceInfo();
            u.Count = "1";//数量
            u.thirdCompanyName = Txt_User.Text.Trim();
            u.thirdCompanyCode = Txt_PassWord.Text.Trim();
            List<devices> dd = new List<devices>();
            devices d = new devices();
            d.type = txtmodel.Text.Trim();//设备型号
            d.deviceId = txtMachineSerial.Text.Trim();//设备唯一码
            d.factory = txtProductor.Text.Trim();
            dd.Add(d);
            u.device = dd;
            string data = JsonHelper.EntityToJson(u);

            string rtn = Global.ycHttpMath("POST", url, data);
            if (rtn.Contains("msg") || rtn.Contains("status"))
            {
                resultdata results = JsonHelper.JsonToEntity<resultdata>(rtn);
                if (results != null)
                {
                    if (results.status == "00")//上传成功
                    {
                        MessageBox.Show("通信测试成功！","提示",MessageBoxButtons.OK ,MessageBoxIcon.Information  );
                    }
                    else if (results.status == "01")//无此检测公司
                    {
                        MessageBox.Show("通信测试失败！\r\n无此检测公司", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                    }
                    else if (results.status == "02")//字段错误
                    {
                        MessageBox.Show("通信测试失败！\r\n字段错误", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("通信测试失败！\r\n失败原因：" + results.status, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("通信测试失败！\r\n失败原因：" + rtn, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("通信测试失败！\r\n失败原因：" + rtn, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            //保存服务器配置信息到配置文件
            Configuration cf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            //服务器IP地址设置
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

            //设备唯一码
            string mserial = ConfigurationManager.AppSettings["IntrumentSeriersNum"];
            if (mserial != null)
            {
                cf.AppSettings.Settings["IntrumentSeriersNum"].Value = Global.ServerPassword;
            }
            else
            {
                cf.AppSettings.Settings.Add("IntrumentSeriersNum", Global.ServerPassword);
            }
            //设备型号
            string mModel = ConfigurationManager.AppSettings["InstrumentNameModel"];
            if (mModel != null)
            {
                cf.AppSettings.Settings["InstrumentNameModel"].Value = Global.MachineModel;
            }
            else
            {
                cf.AppSettings.Settings.Add("InstrumentNameModel", Global.MachineModel);
            }
            //设备厂家
            string mProductor = ConfigurationManager.AppSettings["InstrumManufact"];
            if (mProductor != null)
            {
                cf.AppSettings.Settings["InstrumManufact"].Value = Global.MachineProductor;
            }
            else
            {
                cf.AppSettings.Settings.Add("InstrumManufact", Global.MachineProductor);
            }
            cf.Save();
            ConfigurationManager.RefreshSection("appSettings");
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
        /// <summary>
        /// 下载检测项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Txt_Url.Text.Trim().Length == 0)
                {
                    MessageBox.Show("服务器地址不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (Txt_User.Text.Trim().Length == 0)
                {
                    MessageBox.Show("用户名不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (Txt_PassWord.Text.Trim().Length == 0)
                {
                    MessageBox.Show("密码不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (txtMachineSerial.Text.Trim().Length == 0)
                {
                    MessageBox.Show("设备唯一码不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (txtmodel.Text.Trim().Length == 0)
                {
                    MessageBox.Show("设备型号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (txtProductor.Text.Trim().Length == 0)
                {
                    MessageBox.Show("设备厂家不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                //Global.ServerAdd = Txt_Url.Text.Trim();
                //Global.ServerName = Txt_User.Text.Trim();
                //Global.ServerPassword = Txt_PassWord.Text.Trim();
                //Global.MachineSerialCode = txtMachineSerial.Text.Trim();//设备唯一码
                //Global.MachineModel = txtmodel.Text.Trim();//设备型号
                //Global.MachineProductor = txtProductor.Text.Trim();//设备厂家
                btnDownItem.Enabled = false;
                string err="";
                string urlI = getUrl.GetUrl(Txt_Url.Text.Trim(), 4);
                Users uI = new Users();
                uI.thirdCompanyName = Txt_User.Text.Trim();
                uI.thirdCompanyCode = Txt_PassWord.Text.Trim();
                string dataI = JsonHelper.EntityToJson(uI);
                string resultI = Global.ycHttpMath("POST", urlI, dataI);
                if (resultI.Contains("msg") || resultI.Contains("status"))
                {
                    resultItem results = JsonHelper.JsonToEntity<resultItem>(resultI);
                    if (results != null)
                    {
                        if (results.status == "00")//上传成功
                        {
                            saveItem(results.checkItem.ToString());
                        }
                        else if (results.status == "01")//无此检测公司
                        {
                            MessageBox.Show("通信测试失败！\r\n无此检测公司", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else if (results.status == "02")//字段错误
                        {
                            MessageBox.Show("通信测试失败！\r\n字段错误", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("通信测试失败！\r\n失败原因：" + results.status, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("通信测试失败！\r\n失败原因" + resultI, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("通信测试失败！\r\n失败原因" + resultI, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                MessageBox.Show("检测项目下载成功！\r\n共成功下载" + itemCount + "条检测项目数据！", "提示",MessageBoxButtons.OK ,MessageBoxIcon.Information);
                btnDownItem.Enabled = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message );
                btnDownItem.Enabled = true ;
            }
        }
        private string itemInfo = "";
        public void saveItem(string items)
        {
            try
            {
                itemInfo = items;
                PercentProcess process = new PercentProcess()
                {
                    BackgroundWork = this.saveitems,
                    MessageInfo = "正在下载项目信息"
                };
                //process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(backgroundWorkerCompleted);
                process.Start();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private int itemCount = 0;

        private void saveitems(Action<int> percent)
        {
            string err = "";
            List<Items> resultm = JsonsItem.anaylise(itemInfo);
            if (resultm != null && resultm.Count > 0)
            {
                float p = 0;
                float sp = 0;
                int showp = 0;
                percent(5);
                p = (float)95 / (float)resultm.Count;
                YCsql _ycSql = new YCsql();
                _ycSql.deleteItem("", "", out err);
               
                for (int i = 0; i < resultm.Count; i++)
                {
                    sp = sp + p;
                    showp = (int)sp;
                    percent(showp);
                    //插入数据库
                    _ycSql.InsertItem(resultm[i], out err);
                    itemCount = itemCount + 1;
                }
                percent(100);
            }
            else
            {
                percent(100);
            }

          
        }

        private void btnSample_Click(object sender, EventArgs e)
        {
            try
            {
                if (Txt_Url.Text.Trim().Length == 0)
                {
                    MessageBox.Show("服务器地址不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (Txt_User.Text.Trim().Length == 0)
                {
                    MessageBox.Show("用户名不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (Txt_PassWord.Text.Trim().Length == 0)
                {
                    MessageBox.Show("密码不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (txtMachineSerial.Text.Trim().Length == 0)
                {
                    MessageBox.Show("设备唯一码不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (txtmodel.Text.Trim().Length == 0)
                {
                    MessageBox.Show("设备型号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (txtProductor.Text.Trim().Length == 0)
                {
                    MessageBox.Show("设备厂家不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                btnSample.Enabled = false;

                string urlS = getUrl.GetUrl(Txt_Url.Text.Trim(), 3);
                Users uS = new Users();
                uS.thirdCompanyName = Txt_User.Text.Trim();
                uS.thirdCompanyCode = Txt_PassWord.Text.Trim ();
                string dataS = JsonHelper.EntityToJson(uS);
                string resultS = Global.ycHttpMath("POST", urlS, dataS);
                if (resultS.Contains("msg") || resultS.Contains("status"))
                {
                    resultSample results = JsonHelper.JsonToEntity<resultSample>(resultS);
                    if (results != null)
                    {
                        if (results.status == "00")//上传成功
                        {
                            saveSample(results.goodsInfo.ToString ());
                            
                        }
                        else if (results.status == "01")//无此检测公司
                        {
                            MessageBox.Show("通信测试失败！\r\n无此检测公司", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else if (results.status == "02")//字段错误
                        {
                            MessageBox.Show("通信测试失败！\r\n字段错误", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("通信测试失败！\r\n失败原因：" + results.status, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("通信测试失败！\r\n失败原因：" + resultS, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("通信测试失败！\r\n失败原因：" + resultS, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                MessageBox.Show( "样品信息下载成功！\r\n共成功下载" + sampleCount + "条样品信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                btnSample.Enabled = true ;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message );
                btnSample.Enabled = true;
            }
        }
        private string sampleInfo = "";
        public void saveSample(string samples)
        {
            try
            {
                sampleInfo = samples;
                PercentProcess process = new PercentProcess()
                {
                    BackgroundWork = this.saveSamples,
                    MessageInfo = "正在下载样品信息"
                };
                //process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(backgroundWorkerCompleted);
                process.Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private int sampleCount = 0;
        private void saveSamples(Action<int> percent)
        {
            string err = "";
            YCsql _ycSql = new YCsql();
            _ycSql.deleteSample ("", "", out  err);
            List<samples> resultm = JsonsItem.analyseSample(sampleInfo);
            
            sampleCount = 0;
            if (resultm != null && resultm.Count > 0)
            {
                float p = 0;
                float sp = 0;
                int showp = 0;
                percent(5);
                p = (float)95 / (float)resultm.Count;

                for (int i = 0; i < resultm.Count; i++)
                {
                    sp = sp + p;
                    showp = (int)sp;
                    percent(showp);
                    sampleCount = sampleCount + 1;
                    //插入数据库
                    _ycSql.InsertSample(resultm[i], out err);
                }
                percent(100);
            }
            else
            {
                percent(100);
            }
        }

    }
}

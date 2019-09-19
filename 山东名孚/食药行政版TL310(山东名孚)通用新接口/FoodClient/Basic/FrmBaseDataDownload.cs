using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.Security;
using System.Windows.Forms;
using DY.FoodClientLib;
using DY.Process;
using FoodClient.AnHui;
using System.Text.RegularExpressions;

namespace FoodClient
{
    /// <summary>
    /// 基础数据同步
    /// </summary>
    public partial class FrmBaseDataDownload : TitleBarBase
    {
        public FrmBaseDataDownload()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 检测点编号
        /// </summary>
        private string stdCode = string.Empty;
        /// <summary>
        /// 行政机构编号
        /// </summary>
        private string districtCode = string.Empty;

        /// <summary>
        /// 窗口加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmBaseDataDownload_Load(object sender, EventArgs e)
        {
            //string userCode = ShareOption.DefaultUserUnitCode;
            //if (ShareOption.SystemVersion.Equals(ShareOption.EnterpriseVersion))//如果是企业版
            //{
            //    stdCode = clsUserUnitOpr.GetNameFromCode("CompanyId", userCode);
            //}
            //else
            //{
            //    stdCode = clsUserUnitOpr.GetStdCode(userCode);
            //}

            //districtCode = clsUserUnitOpr.GetNameFromCode("DistrictCode", userCode);

            try
            {
                TitleBarText = "基础数据同步";
            //    tb_Url.Text = ConfigurationManager.AppSettings["AnHui_url"];                
            //    tb_userName.Text = ConfigurationManager.AppSettings["AnHui_userName"];
            //    tb_passWord.Text = ConfigurationManager.AppSettings["AnHui_passWord"];
            //    tb_instrument.Text = ConfigurationManager.AppSettings["AnHui_instrument"];
            //    tb_instrumentNo.Text = ConfigurationManager.AppSettings["AnHui_instrumentNo"];
            //    tb_interfaceVersion.Text = ConfigurationManager.AppSettings["AnHui_interfaceVersion"];

            //    TestMoreMethodGetMac.MoreMethodGetMAC getmac = new TestMoreMethodGetMac.MoreMethodGetMAC();
            //    List<string> rtnList = new List<string>();
            //    rtnList.Add(getmac.GetMacAddressByNetworkInformation());
            //    if (rtnList != null && rtnList.Count > 0)
            //    {
            //        Dictionary<string, string> dicMac = new Dictionary<string, string>();
            //        List<string> macList = new List<string>();
            //        foreach (string mac in rtnList)
            //        {
            //            if (!dicMac.ContainsKey(mac))
            //            {
            //                dicMac.Add(mac, mac);
            //                macList.Add(mac);
            //            }
            //        }
            //        tb_mac.DataSource = macList;
            //        if (tb_mac.DataSource != null)
            //            tb_mac.SelectedIndex = 0;
            //    }
            //    string thismac = ConfigurationManager.AppSettings["AnHui_mac"];
            //    if (thismac.Length > 0)
            //        tb_mac.Text = thismac;
            }
            catch (Exception)
            {

            }
            //
            tb_Url.Text = Global.AnHuiInterface.ServerAddr;
            tb_userName.Text = Global.AnHuiInterface.userName;
            tb_passWord.Text = Global.AnHuiInterface.passWord;
            tb_instrument.Text = Global.AnHuiInterface.instrument;
            tb_instrumentNo.Text = Global.AnHuiInterface.instrumentNo;
            txtbarcode.Text = Global.AnHuiInterface.barcode;
        }

        /// <summary>
        /// 全部下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAllDownload_Click(object sender, EventArgs e)
        {
            if (!ValidateCheckPointInfo())
            {
                lblTip.Text = string.Empty;
                Cursor = Cursors.Default;
                return;
            }

            lblTip.Text = string.Empty;
            Cursor = Cursors.Default;

            if (string.IsNullOrEmpty(ShareOption.SysServerIP) || string.IsNullOrEmpty(ShareOption.SysServerID))
            {
                MessageBox.Show(this, "联网用户名和密码不能为空，请先到选项菜单中设置服务器地址与登录ID！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (stdCode.Equals(string.Empty))
            {
                MessageBox.Show(this, "未设置默认的检测单位，请设置！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            //基础数据网络同步更新
            if (MessageBox.Show(this, "全部基础数据同步需要较长时间，真要执行同步吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            Cursor = Cursors.WaitCursor;
            EnableDownload(false);

            PercentProcess process = new PercentProcess();
            process.BackgroundWork = this.DownloadAllProcess;
            process.MessageInfo = "正在执行中";
            process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(backgroundWorkerCompleted);
            process.Start();

        }

        private void DownloadAllProcess(Action<int> percent)
        {
            percent(0);
            //string error = string.Empty;
            //string ret = CommonOperation.DownloadAll(stdCode, districtCode, out error, "all",out p);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(CommonOperation.DownloadFoodClass(stdCode, districtCode));//样品类别
            percent(10);
            sb.AppendLine(CommonOperation.DownloadCheckComTypeOpr(stdCode, districtCode));//检测单位类别
            percent(20);
            sb.AppendLine(CommonOperation.DownloadStandardType(stdCode, districtCode));//检测标准类型
            percent(30);
            sb.AppendLine(CommonOperation.DownloadStandard(stdCode, districtCode));//检测标准
            percent(40);
            sb.AppendLine(CommonOperation.DownloadCheckItem(stdCode, districtCode)); //检测项目
            percent(50);
            sb.AppendLine(CommonOperation.DownloadCompanyKind(stdCode, districtCode)); //单位类别
            percent(60);
            sb.AppendLine(CommonOperation.DownloadDistrict(stdCode, districtCode)); //组织机构
            percent(70);
            sb.AppendLine(CommonOperation.DownloadProduceArea(stdCode, districtCode)); //产地
            percent(80);
            sb.AppendLine(CommonOperation.DownloadCompany(stdCode, districtCode));//单位信息
            percent(90);
            sb.AppendLine(CommonOperation.DownloadProprietors(stdCode, districtCode));//经营户信息
            percent(100);
            MessageBox.Show(sb.ToString());

            //if (error.Equals(string.Empty))
            //{
            //    //frmMain.IsLoadCheckedComSel = false;
            //    //frmMain.IsLoadCheckedUpperComSel = false;
            //    MessageBox.Show(this, ret, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);// "基础数据同步成功！"
            //}
            //else
            //{
            //    MessageBox.Show(this, "基础数据同步失败！错误信息：\r\n" + error, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void backgroundWorkerCompleted(object sender, BackgroundWorkerEventArgs e)
        {
            if (e.BackGroundException == null)
            {
                //groupBox1.Enabled = true;
                EnableDownload(true);

                Cursor = Cursors.Default;
                //MessageBox.Show("执行完毕");
            }
            else
            {
                //groupBox1.Enabled = true;
                EnableDownload(true);
                Cursor = Cursors.Default;
                MessageBox.Show("异常:" + e.BackGroundException.Message);
            }
        }

        /// <summary>
        /// 下载食品类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFoodClass_Click(object sender, EventArgs e)
        {
            if (!ValidateCheckPointInfo())
            {
                lblTip.Text = string.Empty;
                Cursor = Cursors.Default;
                return;
            }

            lblTip.Text = string.Empty;
            Cursor = Cursors.Default;

            try
            {
                //groupBox1.Enabled = false;
                EnableDownload(false);
                string ret = CommonOperation.DownloadFoodClass(stdCode, districtCode);
                MessageBox.Show(ret);//"成功下载{0}条数据" , 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //groupBox1.Enabled = true;
            EnableDownload(true);
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// 下载检测单位类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckComTypeOpr_Click(object sender, EventArgs e)
        {
            if (!ValidateCheckPointInfo())
            {
                lblTip.Text = string.Empty;
                Cursor = Cursors.Default;
                return;
            }

            lblTip.Text = string.Empty;
            Cursor = Cursors.Default;

            try
            {
                //	groupBox1.Enabled = false;
                EnableDownload(false);
                string ret = CommonOperation.DownloadCheckComTypeOpr(stdCode, districtCode);
                MessageBox.Show(ret);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //groupBox1.Enabled = true;
            EnableDownload(true);
            Cursor = Cursors.Default;
        }


        private void EnableDownload(bool enabled)
        {
            btnFoodClass.Enabled = enabled;
            btnDistrict.Enabled = enabled;
            btnCheckComTypeOpr.Enabled = enabled;
            btnProduceArea.Enabled = enabled;
            btnStandardType.Enabled = enabled;
            btnCompanyKind.Enabled = enabled;
            btnAllDownload.Enabled = enabled;
        }



        /// <summary>
        /// 检测标准类别,检测标准，检测项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStandardType_Click(object sender, EventArgs e)
        {
            if (!ValidateCheckPointInfo())
            {
                lblTip.Text = string.Empty;
                Cursor = Cursors.Default;
                return;
            }

            lblTip.Text = string.Empty;
            Cursor = Cursors.Default;

            try
            {
                //groupBox1.Enabled = false;
                EnableDownload(false);
                string ret = CommonOperation.DownloadStandardType(stdCode, districtCode) + "\r\n";
                ret += CommonOperation.DownloadStandard(stdCode, districtCode) + "\r\n";
                ret += CommonOperation.DownloadCheckItem(stdCode, districtCode);
                MessageBox.Show(ret);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //groupBox1.Enabled = true;
            EnableDownload(true);
            Cursor = Cursors.Default;
        }

        ///// <summary>
        ///// 下载检测标准
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void btnStandard_Click(object sender, EventArgs e)
        //{
        //    Cursor = Cursors.WaitCursor;
        //    try
        //    {
        //        groupBox1.Enabled = false;
        //        string ret = CommonOperation.DownloadStandard(stdCode, districtCode);
        //        MessageBox.Show(ret);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    groupBox1.Enabled = true;
        //    Cursor = Cursors.Default;
        //}

        ///// <summary>
        ///// 下载检测项目
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void btnCheckItem_Click(object sender, EventArgs e)
        //{
        //    Cursor = Cursors.WaitCursor;
        //    try
        //    {
        //        groupBox1.Enabled = false;
        //        string ret = CommonOperation.DownloadCheckItem(stdCode, districtCode);
        //        MessageBox.Show(ret);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    groupBox1.Enabled = true;
        //    Cursor = Cursors.Default;
        //}

        /// <summary>
        /// 下载行政
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDistrict_Click(object sender, EventArgs e)
        {
            if (!ValidateCheckPointInfo())
            {
                lblTip.Text = string.Empty;
                Cursor = Cursors.Default;
                return;
            }

            lblTip.Text = string.Empty;
            Cursor = Cursors.Default;

            //groupBox1.Enabled = false;
            EnableDownload(false);

            //PercentProcess process = new PercentProcess();
            //process.BackgroundWork = this.DownloadDistrictProcess;
            //process.MessageInfo = "正在执行中";
            //process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(backgroundWorkerCompleted);
            //process.Start();

            try
            {
                string ret = CommonOperation.DownloadDistrict(stdCode, districtCode);
                MessageBox.Show(ret);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //groupBox1.Enabled = true;
            EnableDownload(true);
            Cursor = Cursors.Default;
        }


        //void DownloadDistrictProcess(Action<int> percent)
        //{
        //    try
        //    {
        //        percent(0);
        //        string ret = CommonOperation.DownloadDistrict(stdCode, districtCode);
        //        percent(100);
        //        MessageBox.Show(ret);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        /// <summary>
        /// 下载产品产地
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProduceArea_Click(object sender, EventArgs e)
        {
            if (!ValidateCheckPointInfo())
            {
                lblTip.Text = string.Empty;
                Cursor = Cursors.Default;
                return;
            }

            lblTip.Text = string.Empty;
            Cursor = Cursors.Default;

            //groupBox1.Enabled = false;
            EnableDownload(false);


            PercentProcess process = new PercentProcess();
            process.BackgroundWork = this.DownloadProduceAreaProcess;
            process.MessageInfo = "正在执行中";
            process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(backgroundWorkerCompleted);
            process.Start();

        }

        void DownloadProduceAreaProcess(Action<int> percent)
        {
            try
            {
                percent(0);
                string ret = string.Empty;
                //CommonOperation.DownloadProduceArea(stdCode, districtCode);
                string sign = "ProduceArea";
                DataTable dtbl = null;
                dtbl = CommonOperation.GetJ2EEData(stdCode, districtCode, sign).Tables[sign];
                percent(10);
                if (dtbl == null)
                {
                    percent(100);
                    ret = "暂无数据";
                    MessageBox.Show(ret);
                    return;// "暂无数据";
                }
                int len = dtbl.Rows.Count;

                StringBuilder sb = new StringBuilder();
                string delErr = string.Empty;
                string err = string.Empty;

                clsProduceAreaOpr bll = new clsProduceAreaOpr();
                bll.Delete("IsReadOnly=true", out delErr);
                percent(12);
                sb.Append(delErr);

                clsProduceArea model = new clsProduceArea();
                int k = 1;
                k = (len / (100 - 12) + 1);
                for (int i = 0; i < len; i++)
                {
                    err = string.Empty;
                    model.SysCode = dtbl.Rows[i]["SysCode"].ToString();
                    model.StdCode = dtbl.Rows[i]["StdCode"].ToString();
                    model.Name = dtbl.Rows[i]["Name"].ToString();
                    model.ShortCut = dtbl.Rows[i]["ShortCut"].ToString();
                    model.DistrictIndex = Convert.ToInt64(dtbl.Rows[i]["DistrictIndex"]);
                    model.CheckLevel = dtbl.Rows[i]["CheckLevel"].ToString();
                    model.IsLocal = Convert.ToBoolean(dtbl.Rows[i]["IsLocal"]);
                    model.IsReadOnly = Convert.ToBoolean(dtbl.Rows[i]["IsReadOnly"]);
                    model.IsLock = Convert.ToBoolean(dtbl.Rows[i]["IsLock"]);
                    model.Remark = string.Empty;

                    bll.Insert(model, out err);
                    if (!err.Equals(string.Empty))
                    {
                        sb.AppendLine(err);
                        //throw new Exception(err);
                        //continue;
                    }
                    percent(i / k + 12);
                }
                if (sb.Length > 0)
                {
                    ret = sb.ToString();
                }
                ret = string.Format("已经成功下载{0}条产品产地数据", len.ToString());
                percent(100);
                MessageBox.Show(ret);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 下载被检单位类别,被检单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCompanyKind_Click(object sender, EventArgs e)
        {
            if (!ValidateCheckPointInfo())
            {
                lblTip.Text = string.Empty;
                Cursor = Cursors.Default;
                return;
            }

            lblTip.Text = string.Empty;
            Cursor = Cursors.Default;

            try
            {
                //groupBox1.Enabled = false;
                EnableDownload(false);

                string ret = CommonOperation.DownloadCompanyKind(stdCode, districtCode) + "\r\n";
                ret += CommonOperation.DownloadCompany(stdCode, districtCode) + "\r\n";
                ret += CommonOperation.DownloadProprietors(stdCode, districtCode);
                MessageBox.Show(ret);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //	groupBox1.Enabled = true;
            EnableDownload(true);

            Cursor = Cursors.Default;
        }

        protected override void OnTitleBarDoubleClick(object sender, EventArgs e)
        {

        }


        public bool ValidateCheckPointInfo()
        {
            Cursor = Cursors.WaitCursor;
            EnableDownload(false);

            lblTip.Text = "正在连接服务器验证检测点信息,请等待......";
            Refresh();
            clsUserUnitOpr userUnit = new clsUserUnitOpr();
            DataTable userTable = userUnit.GetAsDataTable(string.Empty, string.Empty, 3);

            if (userTable.Rows.Count == 0)
            {
                MessageBox.Show(this, "未设置检测点信息,请先到[基础数据]=>[终端信息设置]中设置相关信息!", "未设置", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string standardCode = userTable.Rows[0]["StdCode"].ToString();
            string fullName = userTable.Rows[0]["FullName"].ToString();
            string checkPointType = userTable.Rows[0]["ShortCut"].ToString();
            string districtCode = userTable.Rows[0]["DistrictCode"].ToString();

            try
            {
                DataSet serverData = GetServerData();
                string where = string.Format(" StdCode='{0}' And FullName='{1}' And DistrictCode='{2}' And ShortCut='{3}'", standardCode, fullName, districtCode, checkPointType);

                DataRow[] userUnitRow = serverData.Tables["Com_UserUnit"].Select(where);

                if (userUnitRow != null && userUnitRow.Length <= 0)
                {
                    MessageBox.Show(this, "检测点信息不正确,请先到[基础数据]=>[终端信息设置]中设置相关信息!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show(this, "服务器地址不正确,请先设置!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            EnableDownload(true);
            return true;
        }

        private DataSet GetServerData()
        {
            DataSet serverData = new DataSet("UpdateRecord");

            if (!ShareOption.IsDataLink)//如果是网络版
            {
                if (ShareOption.InterfaceType.Equals(ShareOption.InterfaceJ2EE))
                {
                    localhost.DataSyncService ws = new localhost.DataSyncService();
                    ws.Url = ShareOption.SysServerIP;
                    string rt = ws.GetPartDataDriver(ShareOption.SystemVersion, ShareOption.SysServerID, FormsAuthentication.HashPasswordForStoringInConfigFile(ShareOption.SysServerPass, "MD5").ToString());
                    if (rt.Substring(0, 10).Equals("errorInfo:"))
                    {
                        MessageBox.Show(this, "无法正常连接远程服务器，错误原因为：" + rt.Substring(10, rt.Length - 10));
                        Cursor = Cursors.Default;
                        Close();
                    }
                    else
                    {
                        serverData.ReadXml(new System.IO.StringReader(rt));
                    }
                }
            }

            return serverData;

        }

        /// <summary>
        /// 安徽省项目，下载被检单位
        /// </summary>
        /// <param name="percent"></param>
        private void DownloadCompany(Action<int> percent)
        {
            percent(0);
            Global.UploadCount = 0;
            string error = string.Empty;
            try
            {
                clsInstrumentInfoHandle model = new clsInstrumentInfoHandle();
                model.interfaceVersion = Global.AnHuiInterface.interfaceVersion;
                model.userName = Global.AnHuiInterface.userName;
                model.instrument = Global.AnHuiInterface.instrument;
                model.passWord = Global.AnHuiInterface.passWord;
                model.instrumentNo = model.userName + Global.AnHuiInterface.instrumentNo;
                model.mac = Global.AnHuiInterface.mac;
                model.key = Global.AnHuiInterface.md5(model.userName + model.passWord + model.instrumentNo);
                model.tableData = "";
                string xml = Global.AnHuiInterface.instrumentDictionaryHandle(model);
                percent(3);
                string strRequest = Global.AnHuiInterface.ParsingXML(xml);
                percent(4);
                if (strRequest.Equals("1"))
                {
                    //被检企业表
                    List<checked_unit> checked_unitList = Global.AnHuiInterface.checked_unitList;
                    if (checked_unitList != null && checked_unitList.Count > 0)
                    {
                        Dictionary<string, string> dicErr = new Dictionary<string, string>();
                        string strErr = string.Empty;
                        string err = string.Empty;
                        DataOperation.Del("Company", out err);
                        percent(5);
                        err = string.Empty;
                        float percentage1 = (float)95 / (float)checked_unitList.Count, percentage2 = 0;
                        int count = (int)percentage1 + 5;
                        foreach (checked_unit item in checked_unitList)
                        {
                            clsCompany company = new clsCompany();
                            company.FullName = item.unitName;
                            company.SysCode = DateTime.Now.Millisecond.ToString();
                            company.StdCode = item.id;
                            company.CAllow = item.bussinessId;
                            company.CompanyID = item.id;
                            company.ShortName = item.unitName;
                            company.DisplayName = item.unitName;
                            company.Property = item.busScope;
                            company.Address = item.address;
                            company.LinkMan = item.linkName;
                            company.LinkInfo = item.tel;
                            err = string.Empty;
                            new clsCompanyOpr().Insert(company, out err);
                            Global.UploadCount += (err.Length == 0) ? 1 : 0;
                            if (err.Length > 0)
                            {
                                if (!dicErr.ContainsKey(err))
                                {
                                    dicErr.Add(err, err);
                                    strErr += (err.Length == 0) ? "被检企业下载异常信息：" + err : err;
                                }
                            }

                            if (count < 100)
                            {
                                percent(count);
                                percentage2 += percentage1;
                                if (percentage2 > 1)
                                {
                                    count += (int)percentage2;
                                    percentage2 = 0;
                                }
                            }
                            else
                            {
                                count = 100;
                            }
                        }
                        error += strErr;
                    }
                }
                else
                {
                    MessageBox.Show(strRequest, "Error");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("下载数据时出现异常！\r\n异常信息：" + ex.Message, "error");
                return;
            }
            finally 
            {
                percent(100);
            }

            if (Global.UploadCount > 0 && error.Length == 0)
            {
                MessageBox.Show("数据已全部下载完成！本次共下载了 " + Global.UploadCount + " 条数据！", "操作提示");
            }
            else if (Global.UploadCount > 0 && error.Length > 0)
            {
                MessageBox.Show("数据下载时出了点小问题！本次共下载了 " + Global.UploadCount + " 条数据！\r\n\r\n异常信息：" + error, "操作提示");
            }
            else if (Global.UploadCount == 0 && error.Length == 0)
            {
                MessageBox.Show("无可下载的数据！", "系统提示");
            }
        }

        /// <summary>
        /// 被检企业下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_downLoadCompany_Click(object sender, EventArgs e)
        {
            setValues();
            if (!Global.PingIpOrDomainName(ConfigurationManager.AppSettings["AnHui_IP"]))
            {
                MessageBox.Show(this, "服务器地址链接不通，请检查网络或咨询服务器平台管理员！");
                return;
            }
            PercentProcess process = new PercentProcess();
            process.BackgroundWork = this.DownloadCompany;
            process.MessageInfo = "正在执行中";
            process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(backgroundAnHuiWorkerCompleted);
            process.Start();
        }

        private void backgroundAnHuiWorkerCompleted(object sender, BackgroundWorkerEventArgs e)
        {
            if (e.BackGroundException == null)
            {
                AnHuiEnabled(true);
                Cursor = Cursors.Default;
            }
            else
            {
                AnHuiEnabled(true);
                Cursor = Cursors.Default;
                MessageBox.Show("异常:" + e.BackGroundException.Message);
            }
        }

        private void AnHuiEnabled(bool Enabled) 
        {
            tb_Url.Enabled = Enabled;
            tb_userName.Enabled = Enabled;
            tb_passWord.Enabled = Enabled;
            tb_instrument.Enabled = Enabled;
            tb_instrumentNo.Enabled = Enabled;
            tb_mac.Enabled = Enabled;
            tb_interfaceVersion.Enabled = Enabled;
            btn_downLoadCompany.Enabled = Enabled;
            btn_downLoadDataDictionary.Enabled = Enabled;
            btn_downLoadAllDatas.Enabled = Enabled;
        }

        private void DownloadDataDictionary(Action<int> percent)
        {
            percent(0);
            //数据字典下载- 安徽
            Global.UploadCount = 0;
            string error = string.Empty;
            //字典下载
            try
            {
                clsInstrumentInfoHandle model = new clsInstrumentInfoHandle();
                model.interfaceVersion = Global.AnHuiInterface.interfaceVersion;
                model.userName = Global.AnHuiInterface.userName;
                model.instrument = Global.AnHuiInterface.instrument;
                model.passWord = Global.AnHuiInterface.passWord;
                model.instrumentNo = model.userName + Global.AnHuiInterface.instrumentNo;
                model.mac = Global.AnHuiInterface.mac;
                model.key = Global.AnHuiInterface.md5(model.userName + model.passWord + model.instrumentNo);
                model.tableData = "";
                string xml = Global.AnHuiInterface.instrumentDictionaryHandle(model);

                percent(3);
                string strRequest = Global.AnHuiInterface.ParsingXML(xml);
                percent(4);
                if (strRequest.Equals("1"))
                {
                    //数据字典表下载
                    List<data_dictionary> data_dictionaryList = Global.AnHuiInterface.data_dictionaryList;
                    if (data_dictionaryList != null && data_dictionaryList.Count > 0)
                    {
                        Dictionary<string, string> dicErr = new Dictionary<string, string>();
                        string strErr = string.Empty;
                        string err = string.Empty;
                        DataOperation.Del("data_dictionary", out err);
                        err = string.Empty;
                        percent(5);
                        float percentage1 = (float)95 / (float)data_dictionaryList.Count, percentage2 = 0;
                        int count = (int)percentage1 + 5;
                        foreach (data_dictionary item in data_dictionaryList)
                        {
                            DataOperation.Insert(item, out err);
                            Global.UploadCount += (err.Length == 0) ? 1 : 0;
                            if (err.Length > 0)
                            {
                                if (!dicErr.ContainsKey(err))
                                {
                                    dicErr.Add(err, err);
                                    strErr += (err.Length == 0) ? "数据字典下载异常信息：" + err : err;
                                }
                            }
                            if (count < 100)
                            {
                                percent(count);
                                percentage2 += percentage1;
                                if (percentage2 > 1)
                                {
                                    count += (int)percentage2;
                                    percentage2 = 0;
                                }
                            }
                            else
                            {
                                count = 100;
                            }
                        }
                        error += strErr;
                    }
                }
                else
                {
                    MessageBox.Show(strRequest, "Error");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("下载数据时出现异常！\r\n异常信息：" + ex.Message, "error");
                return;
            }
            finally 
            {
                percent(100);
            }

            if (Global.UploadCount > 0 && error.Length == 0)
            {
                MessageBox.Show("数据已全部下载完成！本次共下载了 " + Global.UploadCount + " 条数据！", "操作提示");
            }
            else if (Global.UploadCount > 0 && error.Length > 0)
            {
                MessageBox.Show("数据下载时出了点小问题！本次共下载了 " + Global.UploadCount + " 条数据！\r\n\r\n异常信息：" + error, "操作提示");
            }
            else if (Global.UploadCount == 0 && error.Length == 0)
            {
                MessageBox.Show("无可下载的数据！", "系统提示");
            }
        }

        /// <summary>
        /// 数据字典下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_downLoadDataDictionary_Click(object sender, EventArgs e)
        {
            setValues();
            if (!Global.PingIpOrDomainName(ConfigurationManager.AppSettings["AnHui_IP"]))
            {
                MessageBox.Show(this, "服务器地址链接不通，请检查网络或咨询服务器平台管理员！");
                return;
            }
            PercentProcess process = new PercentProcess();
            process.BackgroundWork = this.DownloadDataDictionary;
            process.MessageInfo = "正在执行中";
            process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(backgroundAnHuiWorkerCompleted);
            process.Start();
        }

        /// <summary>
        /// 安徽项目，全部数据下载
        /// </summary>
        /// <param name="percent"></param>
        private void DownloadAllDatas(Action<int> percent)
        {
            percent(0);
            Global.UploadCount = 0;
            int data_dictionary = 0, checked_unit = 0, standard_limit = 0;
            string error = string.Empty;
            //字典下载
            try
            {
                clsInstrumentInfoHandle model = new clsInstrumentInfoHandle();
                model.interfaceVersion = Global.AnHuiInterface.interfaceVersion;
                model.userName = Global.AnHuiInterface.userName;
                model.instrument = Global.AnHuiInterface.instrument;
                model.passWord = Global.AnHuiInterface.passWord;
                model.instrumentNo = model.userName + Global.AnHuiInterface.instrumentNo;
                model.mac = Global.AnHuiInterface.mac;
                model.key = Global.AnHuiInterface.md5(model.userName + model.passWord + model.instrumentNo);
                model.tableData = "";
                string xml = Global.AnHuiInterface.instrumentDictionaryHandle(model);
                percent(4);

                string strRequest = Global.AnHuiInterface.ParsingXML(xml);
                percent(5);
                if (strRequest.Equals("1"))
                {
                    //数据字典表
                    List<data_dictionary> data_dictionaryList = Global.AnHuiInterface.data_dictionaryList;
                    //被检企业表
                    List<checked_unit> checked_unitList = Global.AnHuiInterface.checked_unitList;
                    //标准限量表
                    List<standard_limit> standard_limitList = Global.AnHuiInterface.standard_limitList;
                    float percentage1 = (float)95 / (float)(data_dictionaryList.Count +
                        checked_unitList.Count + standard_limitList.Count), percentage2 = 0;
                    int count = (int)percentage1 + 5;

                    if (data_dictionaryList != null && data_dictionaryList.Count > 0)
                    {
                        Dictionary<string, string> dicErr = new Dictionary<string, string>();
                        string strErr = string.Empty;
                        string err = string.Empty;
                        DataOperation.Del("data_dictionary", out err);
                        err = string.Empty;
                        percent(count + 1);
                        foreach (data_dictionary item in data_dictionaryList)
                        {
                            DataOperation.Insert(item, out err);
                            data_dictionary += (err.Length == 0) ? 1 : 0;
                            if (err.Length > 0)
                            {
                                if (!dicErr.ContainsKey(err))
                                {
                                    dicErr.Add(err, err);
                                    strErr += (err.Length == 0) ? "数据字典下载异常信息：" + err : err;
                                }
                            }

                            if (count < 100)
                            {
                                percent(count);
                                percentage2 += percentage1;
                                if (percentage2 > 1)
                                {
                                    count += (int)percentage2;
                                    percentage2 = 0;
                                }
                            }
                            else
                            {
                                count = 100;
                            }
                        }
                        error += strErr;
                    }

                    if (checked_unitList != null && checked_unitList.Count > 0)
                    {
                        Dictionary<string, string> dicErr = new Dictionary<string, string>();
                        string strErr = string.Empty;
                        string err = string.Empty;
                        DataOperation.Del("Company", out err);
                        err = string.Empty;
                        percent(count + 1);
                        foreach (checked_unit item in checked_unitList)
                        {
                            clsCompany company = new clsCompany();
                            company.FullName = item.unitName;
                            company.SysCode = DateTime.Now.Millisecond.ToString();
                            company.StdCode = item.id;
                            company.CAllow = item.bussinessId;
                            company.CompanyID = item.id;
                            company.ShortName = item.unitName;
                            company.DisplayName = item.unitName;
                            company.Property = item.busScope;
                            company.Address = item.address;
                            company.LinkMan = item.linkName;
                            company.LinkInfo = item.tel;
                            err = string.Empty;
                            new clsCompanyOpr().Insert(company, out err);
                            checked_unit += (err.Length == 0) ? 1 : 0;
                            if (err.Length > 0)
                            {
                                if (!dicErr.ContainsKey(err))
                                {
                                    dicErr.Add(err, err);
                                    strErr += (err.Length == 0) ? "被检企业下载异常信息：" + err : err;
                                }
                            }

                            if (count < 100)
                            {
                                percent(count);
                                percentage2 += percentage1;
                                if (percentage2 > 1)
                                {
                                    count += (int)percentage2;
                                    percentage2 = 0;
                                }
                            }
                            else
                            {
                                count = 100;
                            }
                        }
                        error += strErr;
                    }

                    if (standard_limitList != null && standard_limitList.Count > 0)
                    {
                        Dictionary<string, string> dicErr = new Dictionary<string, string>();
                        string strErr = string.Empty;
                        string err = string.Empty;
                        DataOperation.Del("standard_limit", out err);
                        err = string.Empty;
                        percent(count + 1);
                        foreach (standard_limit item in standard_limitList)
                        {
                            DataOperation.Insert(item, out err);
                            standard_limit += (err.Length == 0) ? 1 : 0;
                            if (err.Length > 0)
                            {
                                if (!dicErr.ContainsKey(err))
                                {
                                    dicErr.Add(err, err);
                                    strErr += (err.Length == 0) ? "标准限量下载异常信息：" + err : err;
                                }
                            }

                            if (count < 100)
                            {
                                percent(count);
                                percentage2 += percentage1;
                                if (percentage2 > 1)
                                {
                                    count += (int)percentage2;
                                    percentage2 = 0;
                                }
                            }
                            else
                            {
                                count = 100;
                            }
                        }
                        error += strErr;
                    }
                }
                else
                {
                    MessageBox.Show(strRequest, "Error");
                    return;
                }
                Global.UploadCount = data_dictionary + checked_unit + standard_limit;
            }
            catch (Exception ex)
            {
                MessageBox.Show("下载数据时出现异常！\r\n异常信息：" + ex.Message, "error");
                return;
            }
            finally 
            {
                percent(100);
            }

            if (Global.UploadCount > 0 && error.Length == 0)
            {
                MessageBox.Show("数据已全部下载完成！本次共下载了 " + Global.UploadCount + " 条数据！\r\n\r\n" +
                "数据字典 " + data_dictionary + " 条；\r\n被检企业 " + checked_unit + " 条；\r\n检测标准 " +
                standard_limit + " 条；", "操作提示");
            }
            else if (Global.UploadCount > 0 && error.Length > 0)
            {
                MessageBox.Show("数据下载时出了点小问题！本次共下载了 " + Global.UploadCount + " 条数据！\r\n\r\n " +
                    "数据字典 " + data_dictionary + " 条；\r\n被检企业 " + checked_unit + " 条；\r\n检测标准 " +
                standard_limit + " 条；\r\n\r\n异常信息：" + error, "操作提示");
            }
            else if (Global.UploadCount == 0 && error.Length == 0)
            {
                MessageBox.Show("无可下载的数据！", "系统提示");
            }
        }

        private void setValues() 
        {
            Global.AnHuiInterface.ServerAddr = tb_Url.Text.Trim();
            Global.AnHuiInterface.userName = tb_userName.Text.Trim();
            Global.AnHuiInterface.passWord = tb_passWord.Text.Trim();
            Global.AnHuiInterface.instrument = tb_instrument.Text.Trim();//检测单位
            Global.AnHuiInterface.instrumentNo = tb_instrumentNo.Text.Trim();//单位编号
            Global.AnHuiInterface.mac = tb_mac.Text.Trim();
            Global.AnHuiInterface.interfaceVersion = tb_interfaceVersion.Text.Trim();
        }

        /// <summary>
        /// 全部数据下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_downLoadAllDatas_Click(object sender, EventArgs e)
        {
            setValues();
            if (!Global.PingIpOrDomainName(ConfigurationManager.AppSettings["AnHui_IP"]))
            {
                MessageBox.Show(this, "服务器地址链接不通，请检查网络或咨询服务器平台管理员！");
                return;
            }
            PercentProcess process = new PercentProcess();
            process.BackgroundWork = this.DownloadAllDatas;
            process.MessageInfo = "正在执行中";
            process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(backgroundAnHuiWorkerCompleted);
            process.Start();
        }

        private void FrmBaseDataDownload_FormClosing(object sender, FormClosingEventArgs e)
        {
            #region
            //if (Global.AnHuiInterface.ServerAddr.Length == 0)
            //{
            //    MessageBox.Show("服务器地址不能为空!", "系统提示");
            //    tb_Url.Focus();
            //    return;
            //}
            //if (Global.AnHuiInterface.userName.Length == 0)
            //{
            //    MessageBox.Show("用户名不能为空!", "系统提示");
            //    tb_userName.Focus();
            //    return;
            //}
            //if (Global.AnHuiInterface.passWord.Length == 0)
            //{
            //    MessageBox.Show("密码不能为空!", "系统提示");
            //    tb_passWord.Focus();
            //    return;
            //}
            //if (Global.AnHuiInterface.instrument.Length == 0)
            //{
            //    MessageBox.Show("仪器型号不能为空!", "系统提示");
            //    tb_instrument.Focus();
            //    return;
            //}
            //if (Global.AnHuiInterface.instrumentNo.Length == 0)
            //{
            //    MessageBox.Show("仪器编号不能为空!", "系统提示");
            //    tb_instrumentNo.Focus();
            //    return;
            //}
            //if (Global.AnHuiInterface.mac.Length == 0)
            //{
            //    MessageBox.Show("MAC地址不能为空!", "系统提示");
            //    tb_mac.Focus();
            //    return;
            //}
            //if (Global.AnHuiInterface.interfaceVersion.Length == 0)
            //{
            //    MessageBox.Show("接口版本号不能为空!", "系统提示");
            //    tb_interfaceVersion.Focus();
            //    return;
            //}
            #endregion

            #region @zh 2011/11/17 comment
            //setValues();

            ////设置服务器地址
            //Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //string str = ConfigurationManager.AppSettings["AnHui_url"];
            //if (str != null)
            //    cfa.AppSettings.Settings["AnHui_url"].Value = Global.AnHuiInterface.ServerAddr;
            //else
            //    cfa.AppSettings.Settings.Add("AnHui_url", Global.AnHuiInterface.ServerAddr);

            ////设置用户名
            //str = ConfigurationManager.AppSettings["AnHui_userName"];
            //if (str != null)
            //    cfa.AppSettings.Settings["AnHui_userName"].Value = Global.AnHuiInterface.userName;
            //else
            //    cfa.AppSettings.Settings.Add("AnHui_userName", Global.AnHuiInterface.userName);

            ////设置密码
            //str = ConfigurationManager.AppSettings["AnHui_passWord"];
            //if (str != null)
            //    cfa.AppSettings.Settings["AnHui_passWord"].Value = Global.AnHuiInterface.passWord;
            //else
            //    cfa.AppSettings.Settings.Add("AnHui_passWord", Global.AnHuiInterface.passWord);

            ////设置仪器型号
            //str = ConfigurationManager.AppSettings["AnHui_instrument"];
            //if (str != null)
            //    cfa.AppSettings.Settings["AnHui_instrument"].Value = Global.AnHuiInterface.instrument;
            //else
            //    cfa.AppSettings.Settings.Add("AnHui_instrument", Global.AnHuiInterface.instrument);

            ////设置仪器编号
            //str = ConfigurationManager.AppSettings["AnHui_instrumentNo"];
            //if (str != null)
            //    cfa.AppSettings.Settings["AnHui_instrumentNo"].Value = Global.AnHuiInterface.instrumentNo;
            //else
            //    cfa.AppSettings.Settings.Add("AnHui_instrumentNo", Global.AnHuiInterface.instrumentNo);

            ////设置MAC地址
            //str = ConfigurationManager.AppSettings["AnHui_mac"];
            //if (str != null)
            //    cfa.AppSettings.Settings["AnHui_mac"].Value = Global.AnHuiInterface.mac;
            //else
            //    cfa.AppSettings.Settings.Add("AnHui_mac", Global.AnHuiInterface.mac);

            ////设置接口版本号
            //str = ConfigurationManager.AppSettings["AnHui_interfaceVersion"];
            //if (str != null)
            //    cfa.AppSettings.Settings["AnHui_interfaceVersion"].Value = Global.AnHuiInterface.interfaceVersion;
            //else
            //    cfa.AppSettings.Settings.Add("AnHui_interfaceVersion", Global.AnHuiInterface.interfaceVersion);

            //cfa.Save();
            ////刷新配置文件节点
            //ConfigurationManager.RefreshSection("appSettings");
            #endregion
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {                        
            string url = tb_Url.Text.Trim();
             string user=tb_userName.Text.Trim();
            string pwd=tb_passWord.Text.Trim();
            if(url.Length == 0) {                
                return;
            }
            string Pattern = @"^((http|https)://)(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,4})*(/[a-zA-Z0-9\&%_\./-~-]*)?";
            Regex r = new Regex(Pattern);
            Match m = r.Match(url);
            if (!m.Success)
            {
                MessageBox.Show("请输入合法的url（以http://或https://开头）！");
                return;
            }
            //
            Global.AnHuiInterface.ServerAddr = url;
            Global.AnHuiInterface.userName = user;
            Global.AnHuiInterface.passWord = pwd;
            Global.AnHuiInterface.instrumentNo = tb_instrumentNo.Text.Trim();
            Global.AnHuiInterface.instrument = tb_instrument.Text.Trim();
            Global.AnHuiInterface.barcode = txtbarcode.Text.Trim();

            //写入配置文件
            Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //服务器名
            string str = ConfigurationManager.AppSettings["HeFei_uploadUrl"];
            if (str != null)
                cfa.AppSettings.Settings["HeFei_uploadUrl"].Value = Global.AnHuiInterface.ServerAddr;
            else
                cfa.AppSettings.Settings.Add("HeFei_uploadUrl", Global.AnHuiInterface.ServerAddr);
            cfa.Save();
            ////设置用户名
            str = ConfigurationManager.AppSettings["AnHui_userName"];
            if (str != null)
                cfa.AppSettings.Settings["AnHui_userName"].Value = Global.AnHuiInterface.userName;
            else
                cfa.AppSettings.Settings.Add("AnHui_userName", Global.AnHuiInterface.userName);
            cfa.Save();
            ////设置密码
            str = ConfigurationManager.AppSettings["AnHui_passWord"];
            if (str != null)
                cfa.AppSettings.Settings["AnHui_passWord"].Value = Global.AnHuiInterface.passWord;
            else
                cfa.AppSettings.Settings.Add("AnHui_passWord", Global.AnHuiInterface.passWord);
            cfa.Save();
            //条形码
            str = ConfigurationManager.AppSettings["shangdongbarcode"];
            if (str != null)
                cfa.AppSettings.Settings["shangdongbarcode"].Value = Global.AnHuiInterface.barcode;
            else
                cfa.AppSettings.Settings.Add("shangdongbarcode", Global.AnHuiInterface.barcode);
            cfa.Save();

            //检测单位
            str = ConfigurationManager.AppSettings["AnHui_instrument"];
            if (str != null)
                cfa.AppSettings.Settings["AnHui_instrument"].Value = Global.AnHuiInterface.instrument;
            else
                cfa.AppSettings.Settings.Add("AnHui_instrument", Global.AnHuiInterface.instrument);
            cfa.Save();
            //检测单位编号
            str = ConfigurationManager.AppSettings["AnHui_instrumentNo"];
            if (str != null)
                cfa.AppSettings.Settings["AnHui_instrumentNo"].Value = Global.AnHuiInterface.instrumentNo;
            else
                cfa.AppSettings.Settings.Add("AnHui_instrumentNo", Global.AnHuiInterface.instrumentNo);
            cfa.Save();
            ConfigurationManager.RefreshSection("appSettings");
            Close();
        }

        private void tb_mac_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
﻿using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DY.FoodClientLib;
using FoodClient.AnHui;

namespace FoodClient
{
    public partial class FrmAutoTakeDY : TitleBarBase
    {
        public FrmAutoTakeDY()
        {
            InitializeComponent();
        }

        #region 全局变量
        private string _tag = string.Empty;
        private string _standardCode = string.Empty;
        private decimal _readSet = -1;
        private int _intRowSel = 0;
        private string _produceComSelectedValue = string.Empty;
        private string _checkedComSelectedValue = string.Empty;
        /// <summary>
        /// 样品种类
        /// </summary>
        private string _foodType = string.Empty;
        /// <summary>
        /// 检测项目编号
        /// </summary>
        private string _checkItemsCode = string.Empty;
        private string _upperComSelectedValue = string.Empty;
        private string _producePlaceSelectValue = string.Empty;
        protected string _foodSelectedValue = string.Empty;
        protected string _checkItemCode = string.Empty;
        protected string _sign = string.Empty;
        protected decimal _dTestValue = 0;
        protected string _checkUnit = string.Empty;
        protected string _machineCode = string.Empty;
        protected string[,] _checkItems;
        protected string[,] _checkItems62;
        protected string[,] _checkItems72;
        protected readonly clsResultOpr _resultBll = new clsResultOpr();
        //时间控件
        private string _CBOYear = string.Empty;
        private string _CBOMouth = string.Empty;
        private string _CBODay = string.Empty;
        #endregion

        /// <summary>
        /// DY系列自动检测
        /// 构造函数
        /// </summary>
        /// <param name="codeTag">表示不同的DY系列仪器，包括DY1000,DY2000,DY3000</param>
        public FrmAutoTakeDY(string codeTag)
        {
            InitializeComponent();
            _tag = codeTag;
            c1FlexGrid1.Styles.Normal.Border.Style = C1.Win.C1FlexGrid.BorderStyleEnum.Raised;
            BindCompanies();
        }

        private void FrmAutoTakeDY_Load(object sender, EventArgs e)
        {
            //this.txtCheckNo.Enabled = ShareOption.SysStdCodeSame ? false : true;
            cmbnumunit.SelectedIndex = 0;
            cmbCompany.Text = string.Empty;
            txtCompanyInfo.Text = string.Empty;
            cmbUpperCompany.Text = string.Empty;

            if (_tag != "DY6600")
            {
                cmbCqeck.Visible = false;
                label2.Visible = false;
            }
            else
            {
                cmbCqeck.Visible = true;
                label2.Visible = true;
            }

            cmbCheckerVal.SelectedIndex = 0;
            if (cmbCompany != null && cmbCompany.Items.Count >0)
                cmbCompany.SelectedIndex = 0;

            txtSentCompany.Text = clsCompanyOpr.GetCheckUnitAddress(cmbCompany.Text);

            if (!ShareOption.IsRunCache)
            {
                CommonOperation.RunExeCache(10);
            }

            if (ShareOption.DtblCheckCompany != null)
            {
                comcheckUnit.DataSource = ShareOption.DtblCheckCompany.DataSet;
                comcheckUnit.DataMember = "UserUnit";
                comcheckUnit.DisplayMember = "FullName";
                comcheckUnit.ValueMember = "SysCode";
                comcheckUnit.Columns["StdCode"].Caption = "编号";
                comcheckUnit.Columns["FullName"].Caption = "检测单位";
                comcheckUnit.Columns["SysCode"].Caption = "系统编号";
            }
            comcheckUnit.SelectedValue = FrmMain.formMain.checkUnitCode;
        }

        /// <summary>
        /// 初始化绑定
        /// </summary>
        protected void BindInit()
        {
            this.TitleBarText = this.Text;
            this.lblParent.Text = ShareOption.AreaTitle + "：";
            this.lblName.Text = ShareOption.NameTitle + "：";
            this.lblDomain.Text = ShareOption.DomainTitle + "：";
            this.lblSample.Text = ShareOption.SampleTitle + "名称：";
            this.lblProduceTag.Text = ShareOption.ProductionUnitNameTag + "：";
            this.style1 = c1FlexGrid1.Styles.Add("style1");
            this.style1.ForeColor = Color.Red;
            this.styleNormal = c1FlexGrid1.Styles.Add("styleNormal");
            this.styleNormal.ForeColor = Color.Black;
            this.dtStart.Value = DateTime.Now.Date;
            this.dtEnd.Value = DateTime.Now;
            _machineCode = ShareOption.DefaultMachineCode;
            _readSet = ShareOption.DefaultLimitValue;
            if (_checkItems != null)
            {
                for (int i = 0; i < _checkItems.GetLength(0); i++)
                {
                    if (_checkItems[i, 1].ToString() != "-1")
                    {
                        _checkItemCode = _checkItems[i, 1].ToString();
                        _standardCode = clsCheckItemOpr.GetStandardCode(_checkItems[i, 1].ToString());
                        break;
                    }
                }
            }
            if (_checkItems62 != null)
            {
                for (int i = 0; i < _checkItems62.GetLength(0); i++)
                {
                    if (_checkItems62[i, 1].ToString() != "-1")
                    {
                        _checkItemCode = _checkItems62[i, 1].ToString();
                        _standardCode = clsCheckItemOpr.GetStandardCode(_checkItems62[i, 1].ToString());
                        break;
                    }
                }
            }
            if (_checkItems72 != null)
            {
                for (int i = 0; i < _checkItems72.GetLength(0); i++)
                {
                    if (_checkItems72[i, 1].ToString() != "-1")
                    {
                        _checkItemCode = _checkItems72[i, 1].ToString();
                        _standardCode = clsCheckItemOpr.GetStandardCode(_checkItems72[i, 1].ToString());
                        break;
                    }
                }
            }
            if (!ShareOption.IsRunCache)
            {
                CommonOperation.RunExeCache(10);
            }
            if (ShareOption.DtblCheckCompany != null)
            {
                this.cmbCheckUnit.DataSource = ShareOption.DtblCheckCompany.DataSet;
                this.cmbCheckUnit.DataMember = "UserUnit";
                this.cmbCheckUnit.DisplayMember = "FullName";
                this.cmbCheckUnit.ValueMember = "SysCode";
                this.cmbCheckUnit.Columns["StdCode"].Caption = "编号";
                this.cmbCheckUnit.Columns["FullName"].Caption = "检测单位";
                this.cmbCheckUnit.Columns["SysCode"].Caption = "系统编号";
            }
            if (ShareOption.DtblChecker != null)
            {
                DataSet dstChecker = ShareOption.DtblChecker.DataSet;
                this.cmbChecker.DataSource = dstChecker.Copy();
                this.cmbChecker.DataMember = "UserInfo";
                this.cmbChecker.DisplayMember = "Name";
                this.cmbChecker.ValueMember = "UserCode";
                this.cmbChecker.Columns["Name"].Caption = "检测人";
                this.cmbChecker.Columns["UserCode"].Caption = "系统编号";

                this.cmbAssessor.DataSource = dstChecker.Copy();// ds103;
                this.cmbAssessor.DataMember = "UserInfo";
                this.cmbAssessor.DisplayMember = "Name";
                this.cmbAssessor.ValueMember = "UserCode";
                this.cmbAssessor.Columns["Name"].Caption = "审核人";
                this.cmbAssessor.Columns["UserCode"].Caption = "系统编号";

                this.cmbOrganizer.DataSource = dstChecker.Copy();// ds104;
                this.cmbOrganizer.DataMember = "UserInfo";
                this.cmbOrganizer.DisplayMember = "Name";
                this.cmbOrganizer.ValueMember = "UserCode";
                this.cmbOrganizer.Columns["Name"].Caption = "编制人";
                this.cmbOrganizer.Columns["UserCode"].Caption = "系统编号";
            }
            this.cmbResult.Enabled = false;
            this.txtCheckValueInfo.Enabled = false;
            this.txtResultInfo.Enabled = false;
            this.txtStandard.Enabled = false;
            this.cmbCheckUnit.Enabled = true;
            //this.cmbChecker.Enabled = false;
            this.txtStandValue.Enabled = false;
            string syscode = FrmMain.formMain.getNewSystemCode(true);
            this.txtSysID.Text = syscode;
            if (ShareOption.SysStdCodeSame)
                this.txtCheckNo.Text = syscode;

            this.cmbCheckType.SelectedIndex = 0;
            this.txtUnit.Text = ShareOption.SysUnit;
            this.txtSampleUnit.Text = ShareOption.SysUnit;
            this.dtpTakeDate.Value = DateTime.Today;
            this.cmbCheckItem.Text = clsCheckItemOpr.GetNameFromCode(_checkItemCode);
            this.txtStandard.Text = clsStandardOpr.GetNameFromCode(_standardCode);
            this.dtpCheckStartDate.Value = DateTime.Now;
            this.cmbCheckUnit.SelectedValue = FrmMain.formMain.checkUnitCode;
            this.cmbCheckUnit.Text = clsUserUnitOpr.GetNameFromCode(FrmMain.formMain.checkUnitCode);
            this.cmbChecker.SelectedValue = FrmMain.formMain.userCode;
            this.cmbChecker.Text = clsUserInfoOpr.NameFromCode(FrmMain.formMain.userCode);
            //2016年2月24日 wenj 对抽样人和核准人默认选择当前登录用户
            this.cmbOrganizer.SelectedValue = FrmMain.formMain.userCode;
            this.cmbOrganizer.Text = clsUserInfoOpr.NameFromCode(FrmMain.formMain.userCode);
            this.cmbAssessor.SelectedValue = FrmMain.formMain.userCode;
            this.cmbAssessor.Text = clsUserInfoOpr.NameFromCode(FrmMain.formMain.userCode);
            string title = this.Text.Replace("自动检测", "");
            this.lblPrompt.Text = "当前使用仪器设备为：" + title + "\r\n";
            if (ShareOption.SystemVersion == ShareOption.EnterpriseVersion)
            {
                this.cmbUpperCompany.Text = clsUserUnitOpr.GetNameFromCode(ShareOption.DefaultUserUnitCode);
                _upperComSelectedValue = clsCompanyOpr.CodeFromStdCode(clsUserUnitOpr.GetStdCode(ShareOption.DefaultUserUnitCode));
            }
            this.cmbCheckedCompany.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            //如果允许手工录入经营户信息
            if (ShareOption.AllowHandInputCheckUint)
            {
                if (ShareOption.SystemVersion == ShareOption.LocalBaseVersion)
                    this.cmbUpperCompany.Enabled = true;
                else
                    this.cmbUpperCompany.Enabled = false;
                this.txtCompanyInfo.Enabled = true;
            }
            else
            {
                this.cmbUpperCompany.Enabled = false;
                this.txtCompanyInfo.Enabled = false;
            }
            #region @zh 2016/11/21 合肥
            //界面调整
            //txtSentCompany.Text ="";
            lblName.Text = "被检单位：";       //受检人/单位
            lblProduceTag.Text = "检测单位：";     //供应商
            txtSentCompany.Enabled = true;
            cmbProduceCompany.Text = Global.AnHuiInterface.instrument;//检测单位
            txtChkUnitNum.Text = Global.AnHuiInterface.instrumentNo;//检测站编号
            #endregion
        }

        /// <summary>
        /// 检测输入的值是否有效
        /// </summary>
        /// <returns></returns>
        private bool checkValue()
        {
            try
            {
                if (_foodType.Length == 0)
                {
                    MessageBox.Show(this, "请选择样品种类！");
                    return false;
                }
                //if (_checkItemsCode.Length == 0)
                //{
                //    MessageBox.Show(this, "请选锁定上传的检测项目标识！");
                //    return false;
                //}
                if (cmbCompany.Text.Trim().Length == 0)
                {
                    MessageBox.Show(this, "受检检单位不能为空!");
                    cmbCompany.Focus();
                    return false;
 
                }
                if (txtSentCompany.Text.Trim().Length == 0)
                {
                    MessageBox.Show(this, "产地单位不能为空!");
                    txtSentCompany.Focus();
                    return false;
                }
                //if (Global.AnHuiInterface.userName.Length == 0)
                //{
                //    MessageBox.Show(this, "用户名不能为空！\r\n请到基础数据同步界面设置！");
                //    return false;
                //}
                //if (Global.AnHuiInterface.passWord.Length == 0)
                //{
                //    MessageBox.Show(this, "密码不能为空！\r\n请到基础数据同步界面设置！");
                //    return false;
                //}
                //if (Global.AnHuiInterface.instrument.Length == 0)
                //{
                //    MessageBox.Show(this, "仪器型号不能为空！\r\n请到基础数据同步界面设置！");
                //    return false;
                //}
                //if (Global.AnHuiInterface.instrumentNo.Length == 0)
                //{
                //    MessageBox.Show(this, "仪器编号不能为空！\r\n请到基础数据同步界面设置！");
                //    return false;
                //}
                //if (Global.AnHuiInterface.mac.Length == 0)
                //{
                //    MessageBox.Show(this, "MAC地址不能为空！\r\n请到基础数据同步界面设置！");
                //    return false;
                //}
                //if (Global.AnHuiInterface.interfaceVersion.Length == 0)
                //{
                //    MessageBox.Show(this, "接口版本号不能为空！\r\n请到基础数据同步界面设置！");
                //    return false;
                //}
                if (cmbFoodType.ValueMember.ToString().Length == 0)
                {
                    MessageBox.Show(this, "请选择样品种类！");
                    return false;
                }

                if (!this.txtProduceDate.Text.Trim().Equals(""))
                {
                    DateTime dti = new DateTime();
                    if (DateTime.TryParse(txtProduceDate.Text.Trim(), out  dti))
                    {
                        if (this.txtProduceDate.Text.Trim().Length >= 8)
                        {
                            dti = Convert.ToDateTime(this.txtProduceDate.Text.Trim());
                        }
                        if (this.txtProduceDate.Text.Trim().Length <= 7)
                        {
                            MessageBox.Show(this, "生产日期无效，请重新填写!");
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "生产日期无效，请重新填写!");
                        return false;
                    }
                }
                if (this.txtCheckNo.Text.Trim().Equals(string.Empty))
                {
                    MessageBox.Show(this, "检测编号必须输入!");
                    txtCheckNo.Focus();
                    return false;
                }
                if (_foodSelectedValue.Equals(string.Empty))
                {
                    MessageBox.Show(this, ShareOption.SampleTitle + "必须输入!");
                    this.cmbFood.Text = string.Empty;
                    this.cmbFood.Focus();
                    return false;
                }
                if (ShareOption.AllowHandInputCheckUint)
                {
                    if (_upperComSelectedValue.Equals(string.Empty))
                    {
                        MessageBox.Show(this, ShareOption.AreaTitle + "必须输入!");
                        this.cmbUpperCompany.Text = string.Empty;
                        this.cmbUpperCompany.Focus();
                        return false;
                    }
                    if (cmbCheckedCompany.Text.Trim().Equals(string.Empty))
                    {
                        MessageBox.Show(this, ShareOption.NameTitle + "必须输入!");
                        this.cmbCheckedCompany.Text = string.Empty;
                        this.cmbCheckedCompany.Focus();
                        return false;
                    }
                }
                else
                {
                    string companyName = this.cmbCompany.Text.Trim();
                    string companyCiname = this.cmbUpperCompany.Text.Trim();
                    //if (companyName.Equals(string.Empty))
                    //{
                    //    MessageBox.Show(this, ShareOption.NameTitle + "必须输入!");
                    //    this.cmbCompany.Focus();
                    //    return false;
                    //}
                    string name = clsCompanyOpr.GetCompanyName(this.cmbCompany.Text.Trim());
                    string sename = clsProprietorsOpr.GetCompanyName(this.cmbCompany.Text.Trim());
                    if (name == string.Empty && sename == string.Empty)
                    {
                        MessageBox.Show(this, "录入的" + ShareOption.NameTitle + "不存在,请重新录入!");
                        this.cmbCompany.SelectAll();
                        this.cmbCompany.Focus();
                        return false;
                    }
                    if (_checkedComSelectedValue == string.Empty)
                    {
                        _checkedComSelectedValue = clsCompanyOpr.GetCompanyCode(companyName);
                    }
                }
                //if (this.cmbCheckUnit.Text.Trim().Equals(""))
                //{
                //    MessageBox.Show(this, "检测单位必须输入!");
                //    this.cmbCheckUnit.Focus();
                //    return false;
                //}
                //if (this.txtChkUnitNum.Text.Trim().Equals(""))
                //{
                //    MessageBox.Show(this, "检测单位编号必须输入!");
                //    this.cmbCheckUnit.Focus();
                //    return false;
                //}
                if (this.cmbChecker.SelectedValue == null)
                {
                    MessageBox.Show(this, "检测人必须输入!");
                    this.cmbChecker.Text = string.Empty;
                    this.cmbChecker.Focus();
                    return false;
                }
                if (this.cmbResult.Text.Trim().Equals(string.Empty))
                {
                    if (!this.txtCheckValueInfo.Text.Trim().EndsWith("性"))
                    {
                        MessageBox.Show(this, "结论必须输入!");
                        this.cmbResult.Focus();
                        return false;
                    }
                }

                //如果是工商版本同时为不合格的时候
                if (ShareOption.ApplicationTag == ShareOption.ICAppTag && this.cmbResult.Text.Trim() == "不合格")
                {
                    if (this.txtProduceDate.Text.Trim().Equals(string.Empty))
                    {
                        MessageBox.Show(this, "检测结果不合格，生产日期必须输入!");
                        this.txtProduceDate.Focus();
                        return false;
                    }
                    if (this.cmbProduceCompany.Text.Trim().Equals(""))
                    {
                        MessageBox.Show(this, string.Format("检测结果不合格，{0}必须输入!", this.lblProduceTag.Text.Trim()));
                        this.cmbProduceCompany.Focus();
                        return false;
                    }
                    if (this.cmbProducePlace.Text.Equals(""))
                    {
                        MessageBox.Show(this, "检测结果不合格,产品产地必须输入");
                        this.cmbProducePlace.Focus();
                        return false;
                    }
                    if (this.txtImportNum.Text.Trim().Equals(string.Empty))
                    {
                        MessageBox.Show(this, "检测结果不合格,进货数量必须输入");
                        this.txtImportNum.Focus();
                        return false;
                    }
                    if (this.txtSaveNum.Text.Trim().Equals(string.Empty))
                    {
                        MessageBox.Show(this, "检测结果不合格,库存数量必须输入");
                        this.txtSaveNum.Focus();
                        return false;
                    }
                }

                //非法字符检查
                Control posControl;
                if (RegularCheck.HaveSpecChar(this, out posControl))
                {
                    MessageBox.Show(this, "输入中有非法字符,请检查!");
                    posControl.Focus();
                    return false;
                }

                //非字符型检查
                if (!StringUtil.IsValidNumber(this.txtSampleBaseNum.Text.Trim()))
                {
                    MessageBox.Show(this, "抽样基数必须为整数!");
                    this.txtSampleBaseNum.Focus();
                    return false;
                }
                if (!StringUtil.IsNumeric(this.txtSampleNum.Text.Trim()))
                {
                    MessageBox.Show(this, "抽样数量必须为数字!");
                    this.txtSampleNum.Focus();
                    return false;
                }
                if (!StringUtil.IsNumeric(this.txtImportNum.Text.Trim()))
                {
                    MessageBox.Show(this, "进货数量必须为数字!");
                    this.txtImportNum.Focus();
                    return false;
                }
                if (!StringUtil.IsNumeric(this.txtSaleNum.Text.Trim()))
                {
                    MessageBox.Show(this, "销售数量必须为数字!");
                    this.txtSaleNum.Focus();
                    return false;
                }
                if (!StringUtil.IsNumeric(this.txtPrice.Text.Trim()))
                {
                    MessageBox.Show(this, "单价必须为数字!");
                    this.txtPrice.Focus();
                    return false;
                }
                if (!StringUtil.IsNumeric(this.txtSaveNum.Text.Trim()))
                {
                    MessageBox.Show(this, "库存数量必须为数字!");
                    this.txtSaveNum.Focus();
                    return false;
                }
                if (dtpTakeDate.Value > dtpCheckStartDate.Value)
                {
                    MessageBox.Show(this, "抽样日期不能超过检测开始时间!");
                    dtpTakeDate.Focus();
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常!\n" + ex.Message);
            }
            return true;
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            if (checkValue())
            {
                clsResult model = new clsResult();
                string err = string.Empty;
                try
                {
                    string syscode = FrmMain.formMain.getNewSystemCode(true);
                    this.txtSysID.Text = syscode;
                    model = new clsResult();
                    model.SysCode = txtSysID.Text.Trim();
                    if (ShareOption.SysStdCodeSame)
                    {
                        model.CheckNo = syscode.Trim();
                        this.txtCheckNo.Text = syscode.Trim();
                    }
                    else
                        model.CheckNo = this.txtCheckNo.Text.Trim();
                    if (_resultBll.GetRecCount(" CheckNo='" + model.CheckNo + "'", out err) > 0)
                    {
                        MessageBox.Show(this, "检测编号已经存在请换一个编号!");
                        txtCheckNo.Focus();
                        return;
                    }
                    
                    model.ResultType = ShareOption.ResultType5;
                    model.StdCode = txtbarcode.Text.Trim();//txtStdCode.Text.Trim();
                    model.SampleCode = txtSampleCode.Text.Trim();
                    model.CheckedCompany = _checkedComSelectedValue;
                    model.CheckedCompanyName = cmbCompany.Text.Trim();//检测单位
                    model.CheckedComDis = txtCompanyInfo.Text.Trim();
                    model.CheckPlaceCode = clsUserUnitOpr.GetNameFromCode("DistrictCode", ShareOption.DefaultUserUnitCode);
                    model.FoodCode = _foodSelectedValue;
                    //string produceDate = txtProduceDate.Text;
                   // if (!string.IsNullOrEmpty(produceDate))
                    model.ProduceDate = dtpCheckStartDate.Value;// Convert.ToDateTime(produceDate); //dtpProduceDate.Value;
                    model.ProduceCompany = _produceComSelectedValue;
                    model.ProducePlace = _producePlaceSelectValue;
                    model.SentCompany = txtSentCompany.Text.Trim();
                    model.Provider = txtProvider.Text.Trim();
                    model.TakeDate = dtpTakeDate.Value;
                    model.CheckStartDate = dtpCheckStartDate.Value;
                    //string d = model.CheckStartDate.ToString("yyyy-MM-dd");
                    model.ImportNum = txtImportNum.Text.Trim().Equals("") ? string.Empty : Convert.ToDouble(txtImportNum.Text.Trim()).ToString();
                    model.SaveNum = txtSaveNum.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToDouble(txtSaveNum.Text.Trim()).ToString();
                    model.Unit = txtUnit.Text.Trim();
                    model.SampleBaseNum = txtSampleBaseNum.Text.Trim().Equals(string.Empty) ? "null" : txtSampleBaseNum.Text.Trim();
                    model.SampleNum = txtchknum.Text; //txtSampleNum.Text.Trim().Equals(string.Empty) ? "null" : txtSampleNum.Text.Trim();
                    model.SampleUnit = cmbnumunit.Text; //txtSampleUnit.Text.Trim();//数量单位
                    model.SampleLevel = txtSampleLevel.Text.Trim();
                    model.SampleModel = txtSampleModel.Text.Trim();
                    model.SampleState = txtSampleState.Text.Trim();
                    model.Standard = _standardCode;
                    model.CheckMachine = _machineCode;
                    model.CheckTotalItem = _checkItemCode;
                    model.CheckValueInfo = txtCheckValueInfo.Text.Trim();
                    model.StandValue = txtStandValue.Text.Trim();
                    model.Result = cmbResult.Text.Trim();
                    model.ResultInfo = txtResultInfo.Text.Trim();
                    model.UpperCompany = _upperComSelectedValue.ToString();
                    model.UpperCompanyName = cmbProduceCompany.Text.Trim();
                    model.OrCheckNo = txtOrCheckNo.Text.Trim();
                    model.CheckType = cmbCheckType.Text;
                    model.CheckUnitCode = comcheckUnit.SelectedValue == null ? string.Empty : comcheckUnit.SelectedValue.ToString(); //txtChkUnitNum.Text; //cmbCheckUnit.SelectedValue == null ? string.Empty : cmbCheckUnit.SelectedValue.ToString();
                    model.Checker = cmbChecker.SelectedValue == null ? string.Empty : cmbChecker.SelectedValue.ToString();
                    model.Assessor = cmbAssessor.SelectedValue == null ? string.Empty : cmbAssessor.SelectedValue.ToString();
                    model.Organizer = cmbOrganizer.SelectedValue == null ? string.Empty : cmbOrganizer.SelectedValue.ToString();
                    model.Remark = txtRemark.Text.Trim();
                    model.CheckPlanCode = txtCheckPlanCode.Text.Trim();
                    model.SaleNum = txtSaleNum.Text.Trim().Equals(string.Empty) ? "null" : txtSaleNum.Text.Trim();
                    model.Price = txtPrice.Text.Trim().Equals(string.Empty) ? "null" : txtPrice.Text.Trim();
                    model.CheckederVal = cmbCheckerVal.Text.ToString();
                    model.IsSentCheck = cmbIsSentCheck.Text.ToString();
                    model.CheckederRemark = txtCheckerRemark.Text.ToString();
                    model.Notes = txtNotes.Text.ToString();
                    model.CheckMachine = _machineCode;
                    model.HolesNum = lblHolesNum.Text;
                    model.MachineItemName = lblMachineItemName.Text;
                    model.MachineSampleNum = lblMachineSampleNum.Text;
                    model.fTpye = _foodType;
                    model.testPro = _checkItemsCode;
                    model.dataNum = txtCheckNo.Text;
                    model.checkedUnit = "0";
                    //model.barcode = txtbarcode.Text;

                    _resultBll.Insert(model, out err);
                    if (!err.Equals(string.Empty))
                    {
                        MessageBox.Show(this, "数据库操作出错！");
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        c1FlexGrid1.Rows[c1FlexGrid1.RowSel]["已保存"] = true;//更新保存状态
                        AddEmpty();
                        setTitleColor(Color.Black);
                        FrmMsg frm = new FrmMsg("检测记录已保存！");  //弹出对话框
                        frm.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("异常!\n" + ex.Message);
                }
            }
        }

        /// <summary>
        /// 添加完后清空
        /// </summary>
        private void AddEmpty()
        {
            try
            {
                clsResult model = new clsResult();
                string syscode = FrmMain.formMain.getNewSystemCode(true);
                model.SysCode = syscode;
                model.CheckNo = ShareOption.SysStdCodeSame ? syscode : string.Empty;
                model.StdCode = string.Empty;
                model.SampleCode = model.CheckNo;
                model.CheckedCompany = _checkedComSelectedValue;
                model.CheckedCompanyName = cmbCheckedCompany.Text.Trim();
                model.CheckedComDis = txtCompanyInfo.Text.Trim();
                model.CheckPlaceCode = string.Empty;
                model.FoodCode = _foodSelectedValue;
                model.ProduceCompany = _produceComSelectedValue;
                model.ProducePlace = _producePlaceSelectValue;
                model.SentCompany = string.Empty;
                model.Provider = string.Empty;
                model.TakeDate = DateTime.Today;
                model.CheckStartDate = DateTime.Today;
                model.ImportNum = string.Empty;
                model.SaveNum = string.Empty;
                model.Unit = ShareOption.SysUnit;
                model.SampleBaseNum = string.Empty;
                model.SampleNum = string.Empty;
                model.SampleUnit = ShareOption.SysUnit;
                model.SampleLevel = string.Empty;
                model.SampleModel = string.Empty;
                model.SampleState = string.Empty;
                model.Standard = string.Empty;
                model.CheckTotalItem = string.Empty;
                model.CheckValueInfo = string.Empty;
                model.StandValue = txtStandValue.Text;
                model.Result = string.Empty;
                model.ResultInfo = txtResultInfo.Text;
                model.UpperCompany = _upperComSelectedValue.ToString();
                model.UpperCompanyName = cmbCompany.Text.Trim();
                model.OrCheckNo = string.Empty;
                model.CheckType = "抽检";
                model.CheckUnitCode = FrmMain.formMain.checkUnitCode;
                model.Checker = FrmMain.formMain.userCode;
                model.Assessor = string.Empty;
                model.Organizer = string.Empty;
                model.Remark = string.Empty;
                model.CheckPlanCode = string.Empty;
                model.SaleNum = string.Empty;
                model.Price = string.Empty;
                model.CheckederVal = string.Empty;
                model.IsSentCheck = string.Empty;
                model.CheckederRemark = string.Empty;
                model.Notes = string.Empty;
                setValue(model);
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常!\n" + ex.Message);
            }
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="model"></param>
        protected void setValue(clsResult model)
        {
            txtSysID.Text = model.SysCode;
            txtCheckNo.Text = model.CheckNo;
            txtStdCode.Text = model.StdCode;
            txtSampleCode.Text = model.SampleCode;
            _checkedComSelectedValue = model.CheckedCompany;
            cmbCheckedCompany.Text = model.CheckedCompanyName;
            txtCompanyInfo.Text = model.CheckedComDis;
            _foodSelectedValue = model.FoodCode;
            DateTime? tempdt = model.ProduceDate;
            if (tempdt != null)
                txtProduceDate.Text = Convert.ToDateTime(tempdt).ToString("yyyy-MM-dd");
            _produceComSelectedValue = model.ProduceCompany;
            _producePlaceSelectValue = model.ProducePlace;
            //txtSentCompany.Text = model.SentCompany;
            txtProvider.Text = model.Provider;
            dtpTakeDate.Value = model.TakeDate;
            dtpCheckStartDate.Value = model.CheckStartDate;
            txtImportNum.Text = model.ImportNum;
            txtUnit.Text = model.Unit;
            txtSaveNum.Text = model.SaveNum;
            txtSampleBaseNum.Text = model.SampleBaseNum.ToString();
            txtSampleNum.Text = model.SampleNum.ToString();
            txtSampleUnit.Text = model.SampleUnit;
            txtSampleLevel.Text = model.SampleLevel;
            txtSampleModel.Text = model.SampleModel;
            txtSampleState.Text = model.SampleState;
            _machineCode = ShareOption.DefaultMachineCode;
            txtCheckValueInfo.Text = model.CheckValueInfo;
            txtStandValue.Text = model.StandValue;
            cmbResult.Text = model.Result;
            txtResultInfo.Text = model.ResultInfo;
            _upperComSelectedValue = model.UpperCompany;
            cmbUpperCompany.Text = model.UpperCompanyName;
            txtOrCheckNo.Text = model.OrCheckNo;
            cmbCheckType.SelectedIndex = 0;
            txtRemark.Text = model.Remark;
            txtCheckPlanCode.Text = model.CheckPlanCode;
            txtSaleNum.Text = model.SaleNum.Equals("null") ? string.Empty : model.SaleNum.ToString();
            txtPrice.Text = model.Price.Equals("null") ? string.Empty : model.Price.ToString();
            switch (model.CheckederVal)
            {
                case "":
                    cmbCheckerVal.SelectedIndex = 2;
                    break;
                case "无异议":
                    cmbCheckerVal.SelectedIndex = 0;
                    break;
                case "有异议":
                    cmbCheckerVal.SelectedIndex = 1;
                    break;
            }
            switch (model.IsSentCheck)
            {
                case "":
                    cmbIsSentCheck.SelectedIndex = 2;
                    break;
                case "否":
                    cmbIsSentCheck.SelectedIndex = 0;
                    break;
                case "是":
                    cmbIsSentCheck.SelectedIndex = 1;
                    break;
            }
            txtCheckerRemark.Text = model.CheckederRemark;
            txtNotes.Text = model.Notes;
        }

        /// <summary>
        /// 改变自动赋值标题颜色
        /// </summary>
        /// <param name="col"></param>
        private void setTitleColor(Color col)
        {
            //改变标题颜色
            label5.ForeColor = col;//检测项目
            lblSuppresser.ForeColor = col;//检测值
            lblResult.ForeColor = col;//检测结论
            lblReferStandard.ForeColor = col;//检测依据
            label30.ForeColor = col;//标准值
            label32.ForeColor = col;//检测值单位
            label10.ForeColor = col;//检测时间
        }

        /// <summary>
        /// 判断检测值状态
        /// </summary>
        /// <param name="testValue"></param>
        private void checkValueState(decimal testValue)
        {
            if (txtCheckValueInfo.Text.Trim().Length == 0)
                txtCheckValueInfo.Text = "0";

            //仅适用于合肥农残检测，其他的检测项目肯定有问题的。
            if (!_sign.Equals("≤")) _sign = "≤";
            if (!_checkUnit.Equals("%")) _checkUnit = "%";
            if (int.Parse(txtStandValue.Text.Trim()) <= 0) txtStandValue.Text = "40";
            
            if (!txtCheckValueInfo.Text.Trim().EndsWith("性"))
            {
                decimal checkValue = Decimal.Parse(txtCheckValueInfo.Text);
                switch (_sign)
                {
                    case "<":
                        cmbResult.Text = checkValue >= testValue ? "不合格" : "合格";
                        break;
                    case "≤":
                        cmbResult.Text = checkValue > testValue ? "不合格" : "合格";
                        break;
                    case ">":
                        cmbResult.Text = checkValue <= testValue ? "不合格" : "合格";
                        break;
                    case "≥":
                        cmbResult.Text = checkValue < testValue ? "不合格" : "合格";
                        break;
                }
                //如果是工商版本同时是不合格的时候
                //产品产地、生产单位、生产日期、进货数量、库存数量 是必录项
                if (ShareOption.ApplicationTag.Equals(ShareOption.ICAppTag) && cmbResult.Text.Trim().Equals("不合格"))
                {
                    lblPerImportNumTag.Visible = true;
                    lblPerProduceComTag.Visible = true;
                    lblPerProduceDateTag.Visible = true;
                    lblPerProduceTag.Visible = true;
                    lblPerSaveNumTag.Visible = true;
                }
            }
        }

        /// <summary>
        /// 点击数据列表事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void c1FlexGrid1_Click(object sender, System.EventArgs e)
        {
            if (c1FlexGrid1.RowSel < 1)
                return;

            if (c1FlexGrid1.RowSel == _intRowSel)
                return;

            object obj = c1FlexGrid1.Rows[c1FlexGrid1.RowSel]["已保存"];
            if (obj != null && Convert.ToBoolean(obj))
            {
                MessageBox.Show("此数据已经保存过");
                return;
            }

            if (c1FlexGrid1.RowSel >= 1)
            {
                _intRowSel = c1FlexGrid1.RowSel;
                CheckRowState();
                if (_tag != "DYRSY2" && _tag != "DY6400")//两款肉类水分仪除外
                {
                    if (_checkItems == null)
                        return;

                    string strItem = lblMachineItemName.Text;
                    if (_tag.Equals("DY6100")) strItem = "洁净度";
                    string siom = string.Empty;
                    if (strItem.Contains("-"))
                        siom = strItem.Substring(0, strItem.IndexOf('-'));
                    string[,] temparr;
                    bool IsExist = false;
                    temparr = _checkItems;
                    if (siom == "金标法")
                        temparr = _checkItems62;
                    if (siom == "干化学法")
                        temparr = _checkItems72;
                    for (int i = 0; i < temparr.GetLength(0); i++)
                    {
                        if (strItem.Equals(temparr[i, 0]))
                        {
                            IsExist = true;
                            if (temparr[i, 1].Equals("-1"))
                            {
                                _checkItemCode = string.Empty;
                                _standardCode = string.Empty;
                                _sign = string.Empty;
                                _dTestValue = 0;
                                _checkUnit = string.Empty;
                                cmbCheckItem.Text = string.Empty;
                                txtStandard.Text = string.Empty;
                                txtStandValue.Text = string.Empty;
                                cmbResult.Text = string.Empty;
                                MessageBox.Show(this, "该仪器的这个检测功能项尚未对应检测项目，无法进行下去，请到选项中设置！");
                                return;
                            }
                            else
                            {
                                _checkItemCode = temparr[i, 1].ToString();
                                _standardCode = clsCheckItemOpr.GetStandardCode(temparr[i, 1].ToString());
                            }
                        }
                    }
                    if (!IsExist)
                    {
                        MessageBox.Show(this, "该仪器不存在该检测功能项，可能尚未设定过，请与系统管理员联系！");
                        _checkItemCode = string.Empty;
                        _standardCode = string.Empty;
                        _sign = string.Empty;
                        _dTestValue = 0;
                        _checkUnit = string.Empty;
                        cmbCheckItem.Text = string.Empty;
                        txtStandard.Text = string.Empty;
                        txtStandValue.Text = string.Empty;
                        cmbResult.Text = string.Empty;
                        return;
                    }
                    if (!_tag.Equals("DY6100"))
                    {
                        cmbCheckItem.Text = clsCheckItemOpr.GetNameFromCode(_checkItemCode);
                        txtResultInfo.Text = clsCheckItemOpr.GetUnitFromCode(_checkItemCode);
                        _standardCode = clsCheckItemOpr.GetStandardCode(_checkItemCode);
                        txtStandard.Text = clsStandardOpr.GetNameFromCode(_standardCode);
                        txtStandValue.Text = clsStandardOpr.GetStandValueFromCode(_standardCode);

                        //txtResultInfo.Text = clsCheckItemOpr.GetUnitFromCode(cmbCheckItem.SelectedValue.ToString());
                        
                        _dTestValue = Convert.ToDecimal(txtStandValue.Text);
                        _sign = clsStandardOpr.GetSignValueFromCode(_standardCode);
                    }
                    dtpCheckStartDate.Value = DateTime.Parse(c1FlexGrid1.Rows[_intRowSel]["检测时间"].ToString());
                    dtpTakeDate.Value = dtpCheckStartDate.Value;
                    return;
                }
                if (!string.IsNullOrEmpty(_foodSelectedValue))
                {
                    string[] result = clsFoodClassOpr.ValueFromCode(_foodSelectedValue, _checkItemCode);
                    if (_tag.Equals("DY6100"))
                    {
                        result[0] = "<";
                        result[1] = c1FlexGrid1.Rows[_intRowSel]["上限值"].ToString();
                        result[2] = "RLU";
                    }
                    //_sign = result[0];
                    try
                    {
                        _dTestValue = Convert.ToDecimal(result[1]);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("无效的检测值(如NA,---.-,****等无法转换成整数或小数的字符串),请选择有效的检测结果!", "无效检测值", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    _checkUnit = result[2];
                    if (_sign.Equals("-1") && _dTestValue == 0 && _checkUnit.Equals("-1"))
                    {
                        _foodSelectedValue = string.Empty;
                        cmbFood.Text = string.Empty;
                        txtStandValue.Text = string.Empty;
                        cmbResult.Text = string.Empty;
                        _sign = string.Empty;
                        _dTestValue = 0;
                        _checkUnit = string.Empty;
                    }
                    else
                    {
                        //txtStandValue.Text = _dTestValue.ToString();
                    }
                }
                try
                {
                    if (txtCheckValueInfo.Text.EndsWith("性"))
                    {
                        txtCheckValueInfo.Text = txtCheckValueInfo.Text;
                        if (txtCheckValueInfo.Text == "阴性")
                            cmbResult.Text = "合格";
                        if (txtCheckValueInfo.Text == "阳性")
                            cmbResult.Text = "不合格";
                    }
                    else
                    {
                        txtCheckValueInfo.Text = Decimal.Parse(txtCheckValueInfo.Text).ToString();
                    }
                    if (!txtCheckValueInfo.Text.EndsWith("性"))
                        checkValueState(_dTestValue);
                    setTitleColor(Color.Blue);  //改变标题颜色
                    FrmMsg frm = new FrmMsg("检测数据已自动赋值"); //弹出对话框
                    frm.ShowDialog(this);
                }
                catch (Exception)
                {
                    txtCheckValueInfo.Text = string.Empty;
                    cmbResult.Text = string.Empty;
                    MessageBox.Show("这是错误或空白检测记录，无法采集数据。请选择正常检测记录！");
                }
            }
        }

        #region 下拉窗口
        private void cmbFood_BeforeOpen(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                //if (!_checkItemCode.Equals(string.Empty))
                //{
                frmFoodSelect frm = new frmFoodSelect(_checkItemCode, _foodSelectedValue);
                clsFoodClassOpr foodBll = new clsFoodClassOpr();
                //DataTable dt = foodBll.GetAsDataTable("IsLock=false And IsReadOnly=true and CheckItemCodes like '%{" + _checkItemCode + ":%'", "SysCode", 0);
                DataTable dt = foodBll.GetAsDataTable("IsLock=false And IsReadOnly=true", "SysCode", 0);
                if (dt.Rows.Count > 0)
                {
                    frm.ShowDialog(this);
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        _foodSelectedValue = frm.sNodeTag;
                        cmbFood.Text = frm.sNodeName;
                        //_sign = frm.sSign;
                        //if (_tag.Equals("DY6100"))
                        //{
                        //    _sign = "<";
                        //    frm.sUnit = "RLU";
                        //    frm.sValue = txtStandValue.Text.Length > 0 ? txtStandValue.Text : frm.sValue;
                        //}
                        //_dTestValue = Convert.ToDecimal(frm.sValue);
                        //_checkUnit = frm.sUnit;
                        //txtStandValue.Text = frm.sValue;
                        if (txtCheckValueInfo.Text != string.Empty)
                        {
                            checkValueState(_dTestValue);//自动关联合格或不合格
                        }
                        #region @zh 2016/11/12 合肥
                        //@zh 获取所对应的样品的样品种类
                        //string sysCode = _foodSelectedValue.Substring(0, _foodSelectedValue.Length - 5);
                        string sysCode = _foodSelectedValue.Substring(0, 10);
                        dt = foodBll.GetAsDataTable("IsLock=false And IsReadOnly=true and SysCode= '" + sysCode + "'", "SysCode", 0);
                        if (dt.Rows.Count > 0)
                        {
                            textFoodType.Text = dt.Rows[0]["Name"].ToString();
                            _foodType = sysCode;
                        }
                        #endregion
                    }
                }
                else
                {
                    MessageBox.Show(this, "该检测项目无对应检测样品！");
                }
                //}
                //else
                //{
                //    if (!_machineCode.Equals(string.Empty))
                //    {
                //        MessageBox.Show(this, "没有设置仪器所对应的检测项目，请到选项中设置！");
                //    }
                //    else
                //    {
                //        MessageBox.Show(this, "没有选择检测项目！");
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常!\n" + ex.Message);
            }
            e.Cancel = true;
            btnOK.Focus();
        }

        #region 无效方法
        private void cmbCheckedCompany_BeforeOpen(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if (ShareOption.AllowHandInputCheckUint)
            //{
            //    if (!upperComSelectedValue.Equals(""))
            //    {
            //        FrmMain.formCheckedComSelect = new frmCheckedComSelect("", upperComSelectedValue);
            //        FrmMain.formCheckedComSelect.Tag = "Checked";
            //    }
            //    else
            //    {
            //        MessageBox.Show(this, string.Format("请先选择{0}！",lblParent.Text));
            //        e.Cancel = true;
            //        return;
            //    }
            //}
            //else
            //{
            //    if (!FrmMain.IsLoadCheckedComSel)
            //    {
            //        FrmMain.formCheckedComSelect = new frmCheckedComSelect("", checkedComSelectedValue);
            //        FrmMain.formCheckedComSelect.Tag = "Checked";
            //    }
            //    else
            //    {
            //        FrmMain.formCheckedComSelect.Tag = "Checked";
            //        FrmMain.formCheckedComSelect.SetFormValues("", checkedComSelectedValue);
            //    }
            //}
            //FrmMain.formCheckedComSelect.ShowDialog(this);
            //if (FrmMain.formCheckedComSelect.DialogResult == DialogResult.OK)
            //{
            //    this.checkedComSelectedValue = FrmMain.formCheckedComSelect.sNodeTag;
            //    this.cmbCheckedCompany.Text = FrmMain.formCheckedComSelect.sNodeName;
            //    if (!ShareOption.AllowHandInputCheckUint)
            //    {
            //        if (FrmMain.formCheckedComSelect.sParentCompanyName.Equals(""))
            //        {
            //            this.cmbUpperCompany.Text = "";
            //            this.upperComSelectedValue = "";
            //        }
            //        else
            //        {
            //            this.cmbUpperCompany.Text = FrmMain.formCheckedComSelect.sParentCompanyName.ToString();
            //            this.upperComSelectedValue = FrmMain.formCheckedComSelect.sParentCompanyTag.ToString();
            //        }
            //    }
            //    if (FrmMain.formCheckedComSelect.sNodeCompanyInfo.Equals(""))
            //    {
            //        this.txtCompanyInfo.Text = "";
            //    }
            //    else
            //    {
            //        this.txtCompanyInfo.Text = FrmMain.formCheckedComSelect.sNodeCompanyInfo.ToString();
            //    }
            //}
            //FrmMain.formCheckedComSelect.Hide();
            //e.Cancel = true;
        }
        #endregion

        private void cmbCheckType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            switch (cmbCheckType.SelectedIndex)
            {
                case 0:
                    txtSentCompany.Enabled = false;
                    txtOrCheckNo.Enabled = false;
                    break;
                case 1:
                    txtOrCheckNo.Enabled = false;
                    txtSentCompany.Enabled = true;
                    break;
                case 2:
                    txtSentCompany.Enabled = false;
                    txtOrCheckNo.Enabled = true;
                    break;
                case 3:
                    txtOrCheckNo.Enabled = true;
                    txtSentCompany.Enabled = true;
                    break;
            }
        }

        private void cmbProduceCompany_BeforeOpen(object sender, CancelEventArgs e)
        {
            //frmCheckedComSelect frm = new frmCheckedComSelect(string.Empty, _produceComSelectedValue);
            //frm.Tag = "Produce";
            //frm.ShowDialog(this);
            //if (frm.DialogResult == DialogResult.OK)
            //{
            //    _produceComSelectedValue = frm.sNodeTag;
            //    cmbProduceCompany.Text = frm.sNodeName;
            //}
            //else
            //{
            //    _produceComSelectedValue = string.Empty;
            //    cmbProduceCompany.Text = string.Empty;
            //}
            //e.Cancel = true;
        }

        private void cmbProducePlace_BeforeOpen(object sender, CancelEventArgs e)
        {
            frmProduceAreaSelect frm = new frmProduceAreaSelect(_producePlaceSelectValue);
            frm.Tag = "ProducePlace";
            frm.ShowDialog(this);
            if (frm.DialogResult == DialogResult.OK)
            {
                _producePlaceSelectValue = frm.sNodeTag;
                cmbProducePlace.Text = frm.sNodeName;
            }
            else
            {
                _producePlaceSelectValue = string.Empty;
                cmbProducePlace.Text = string.Empty;
            }
            e.Cancel = true;
        }

        private void cmbUpperCompany_BeforeOpen(object sender, CancelEventArgs e)
        {
            if (!FrmMain.IsLoadCheckedUpperComSel)
            {
                FrmMain.formCheckedUpperComSelect = new frmCheckedComSelect(string.Empty, _upperComSelectedValue);
                FrmMain.formCheckedUpperComSelect.Tag = "UpperChecked";
            }
            else
            {
                FrmMain.formCheckedUpperComSelect.Tag = "UpperChecked";
                FrmMain.formCheckedUpperComSelect.SetFormValues(string.Empty, _upperComSelectedValue);
            }
            FrmMain.formCheckedUpperComSelect.ShowDialog(this);
            if (FrmMain.formCheckedUpperComSelect.DialogResult == DialogResult.OK)
            {
                if (_upperComSelectedValue.Equals(string.Empty) || (!_upperComSelectedValue.Equals(FrmMain.formCheckedUpperComSelect.sNodeTag)))
                {
                    _upperComSelectedValue = FrmMain.formCheckedUpperComSelect.sNodeTag;
                    cmbUpperCompany.Text = FrmMain.formCheckedUpperComSelect.sNodeName;
                    _checkedComSelectedValue = string.Empty;
                    cmbCheckedCompany.Text = string.Empty;
                    txtCompanyInfo.Text = string.Empty;
                }
                else
                {
                    _upperComSelectedValue = FrmMain.formCheckedUpperComSelect.sNodeTag;
                    cmbUpperCompany.Text = FrmMain.formCheckedUpperComSelect.sNodeName;
                }
            }
            FrmMain.formCheckedUpperComSelect.Hide();
            e.Cancel = true;
        }

        private void cmbCheckedCompany_LostFocus(object sender, EventArgs e)
        {
            if ((!_checkedComSelectedValue.Equals(string.Empty)) && cmbCheckedCompany.ComboStyle == C1.Win.C1List.ComboStyleEnum.DropdownCombo)
            {
                string strComName = clsCompanyOpr.NameFromCode(_checkedComSelectedValue);
                if (!cmbCheckedCompany.Text.Trim().Equals(strComName))
                {
                    _checkedComSelectedValue = string.Empty;
                }
            }
        }

        //private void dtpProduceDate_ValueChanged(object sender, EventArgs e)
        //{
        //    txtProduceDate.Text = dtpProduceDate.Value.ToString("yyyy-MM-dd");
        //}
        #endregion

        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            winClose();
        }

        /// <summary>
        /// 窗口关闭事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            winClose();
        }

        /// <summary>
        /// 窗口关闭事件
        /// </summary>
        protected virtual void winClose()
        {
            this.Dispose();
        }
        private clsCompanyOpr clsCompanyOpr = new clsCompanyOpr();
        private void BindCompanies()
        {
            //cmbCompany.DataSource = FrmMain.CompanyTable;
            cmbCompany.DataSource = clsCompanyOpr.GetAllCompanies();
            cmbCompany.DisplayMember = "FullName";
            cmbCompany.ValueMember = "SysCode";

            //样品种类
            cmbFoodType.DataSource = clsCompanyOpr.GetAllFoodType("pid <> '-1' AND remark LIKE '食品分类'", 1, "FoodType");
            cmbFoodType.DisplayMember = "name";
            cmbFoodType.ValueMember = "codeId";


        }

        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DataRowView)cmbCompany.SelectedItem) != null)
            {
                
                _checkedComSelectedValue = ((DataRowView)cmbCompany.SelectedItem)["SysCode"].ToString();
                cmbCompany.Text = ((DataRowView)cmbCompany.SelectedItem)["FullName"].ToString();
                txtSentCompany.Text = clsCompanyOpr.GetCheckUnitAddress(cmbCompany.Text);
                txtCompanyInfo.Text = clsCompanyOpr.CompanyInfo(cmbCompany.Text);
                cmbUpperCompany.Text = clsProprietorsOpr.CiidNameFromCode(cmbCompany.Text);
                if (cmbUpperCompany.Text.Equals(""))
                    cmbUpperCompany.Text = cmbCompany.Text;
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (!FrmMain.IsLoadCheckedComSel)
            {
                FrmMain.formCheckedComSelect = new frmCheckedCompany("", _checkedComSelectedValue);
                FrmMain.formCheckedComSelect.Tag = "Checked";
            }
            else
            {
                FrmMain.formCheckedComSelect.Tag = "Checked";
            }
            FrmMain.formCheckedComSelect.ShowDialog(this);
            if (FrmMain.formCheckedComSelect.DialogResult == DialogResult.OK)
            {
                cmbCompany.Text = FrmMain.formCheckedComSelect.sNodeName;
                _checkedComSelectedValue = FrmMain.formCheckedComSelect.sNodeTag;
                cmbUpperCompany.Text = FrmMain.formCheckedComSelect.sParentCompanyName;
                txtCompanyInfo.Text = FrmMain.formCheckedComSelect.sDisplayName;
            }
            FrmMain.formCheckedComSelect.Hide();
        }

        private void dtpProduceDate_ValueChanged(object sender, EventArgs e)
        {
            txtProduceDate.Text = dtpProduceDate.Value.ToString("yyyy-MM-dd");
        }

        private void dtpProduceDate_DropDown(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProduceDate.Text))
                txtProduceDate.Text = dtpProduceDate.Value.ToString("yyyy-MM-dd");
        }

        #region 无效方法
        ///// <summary>
        ///// 提供子类提供重写的方法-窗体加载时
        ///// </summary>
        //protected virtual void BindCheckItem()
        //{
        //    CommonOperation.GetMachineSetting(tag);
        //    //实化串口对象
        //    //port = new SerialPort();
        //    //port.PortName = ShareOption.ComPort.Replace(":", "");
        //    //port.BaudRate = 9600;
        //    //port.StopBits = StopBits.One;
        //    //port.Parity = Parity.None;
        //    //checkItems = StringUtil.GetDY3000DYAry(ShareOption.DefaultCheckItemCode);
        //    //clsDY3000DY.strCheckItems = checkItems;
        //}

        /// <summary>
        /// 点击行事件的时候，检查数据可用性,给子类重写
        /// </summary>
        protected virtual void CheckRowState()
        {

        }
        //private void c1FlexGrid1_AfterSort(object sender, C1.Win.C1FlexGrid.SortColEventArgs e)
        //{
        //    if (c1FlexGrid1 != null && c1FlexGrid1.Rows.Count > 1)
        //    {
        //        for (int i = 1; i < c1FlexGrid1.Rows.Count; i++)
        //        {
        //            if (c1FlexGrid1.Rows[i]["数据可疑性"].ToString().Equals("是"))
        //            {
        //                c1FlexGrid1.SetCellStyle(i, 2, style1);
        //            }
        //            else
        //            {
        //                c1FlexGrid1.SetCellStyle(i, 2, styleNormal);
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// 读取历史数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnReadHistory_Click(object sender, System.EventArgs e)
        {

        }
        /// <summary>
        /// 清理数据列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void cmbCompany_Leave(object sender, EventArgs e)
        {

        }
        #endregion

        private void txtUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.txtUnit.ToString().Equals(""))
                this.txtSampleUnit.Text = this.txtUnit.Text.Trim();
        }

        private void txtSampleAmount_TextChanged(object sender, EventArgs e)
        {
            double price = 0, sampleAmount = 0;
            if (double.TryParse(txtPrice.Text.Trim(), out price) && double.TryParse(txtSampleAmount.Text.Trim(), out sampleAmount))
            {
                txtSampleNum.Text = (sampleAmount / price).ToString("f2");
            }
        }

        private void txtImportNum_TextChanged(object sender, EventArgs e)
        {
            double ImportNum = 0, SaveNum = 0;
            if (double.TryParse(txtImportNum.Text.Trim(), out ImportNum) && double.TryParse(txtSaveNum.Text.Trim(), out SaveNum))
            {
                txtSaleNum.Text = (ImportNum - SaveNum).ToString("f2");
            }
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            double price = 0, sampleAmount = 0;
            if (double.TryParse(txtPrice.Text.Trim(), out price) && double.TryParse(txtSampleAmount.Text.Trim(), out sampleAmount))
            {
                txtSampleNum.Text = (sampleAmount / price).ToString("f2");
            }
        }

        private void txtSaveNum_TextChanged(object sender, EventArgs e)
        {
            double SaveNum = 0, ImportNum = 0;
            if (double.TryParse(txtSaveNum.Text.Trim(), out SaveNum) && double.TryParse(txtImportNum.Text.Trim(), out ImportNum))
            {
                txtSaleNum.Text = (ImportNum - SaveNum).ToString("f2");
            }
        }

        private void cmbFoodType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DataRowView)cmbFoodType.SelectedItem) != null)
            {
                _foodType = ((DataRowView)cmbFoodType.SelectedItem)["codeId"].ToString();
                cmbFoodType.Text = ((DataRowView)cmbFoodType.SelectedItem)["name"].ToString();
            }
        }

        private void cmbCheckItemAnHui_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DataRowView)cmbCheckItemAnHui.SelectedItem) != null)
            {
                _checkItemsCode = ((DataRowView)cmbCheckItemAnHui.SelectedItem)["codeId"].ToString();
                cmbCheckItemAnHui.Text = ((DataRowView)cmbCheckItemAnHui.SelectedItem)["name"].ToString();
            }
        }

        private void txtCheckNo_TextChanged(object sender, EventArgs e)
        {
            txtSampleCode.Text = txtCheckNo.Text.Trim();
        }

        private void cmbCheckItem_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = clsCompanyOpr.GetAllFoodType("pid <> '-1' AND remark LIKE '检测项目' AND name like '%"
                + cmbCheckItem.Text.Substring(0, 1) + "%'", 1, "CheckItem");
            if (dt == null || dt.Rows.Count == 0)
            {
                dt = clsCompanyOpr.GetAllFoodType("pid <> '-1' AND remark LIKE '检测项目'", 1, "CheckItem");
            }
            //检测项目
            cmbCheckItemAnHui.DataSource = dt;
            cmbCheckItemAnHui.DisplayMember = "name";
            cmbCheckItemAnHui.ValueMember = "codeId";
        }

        private void txtCheckValueInfo_TextChanged(object sender, EventArgs e)
        {
            if (txtCheckValueInfo.Text.Trim().Length == 0)
                txtCheckValueInfo.Text = "0";
            if (txtStandValue.Text.Trim().Length == 0) txtStandValue.Text = "40";

            decimal checkValue = Decimal.Parse(txtCheckValueInfo.Text);
            decimal dTestValue = Decimal.Parse(txtStandValue.Text);
            switch ("≤")
            {
                case "<":
                    if (checkValue >= dTestValue)
                        cmbResult.Text = "不合格";
                    else
                        cmbResult.Text = "合格";
                    break;
                case "＜":
                    if (checkValue >= dTestValue)
                        cmbResult.Text = "不合格";
                    else
                        cmbResult.Text = "合格";
                    break;
                case "≤":
                    if (checkValue > dTestValue)
                        cmbResult.Text = "不合格";
                    else
                        cmbResult.Text = "合格";
                    break;
                case ">":
                    if (checkValue <= dTestValue)
                        cmbResult.Text = "不合格";
                    else
                        cmbResult.Text = "合格";
                    break;
                case "＞":
                    if (checkValue <= dTestValue)
                        cmbResult.Text = "不合格";
                    else
                        cmbResult.Text = "合格";
                    break;
                case "≥":
                    if (checkValue < dTestValue)
                        cmbResult.Text = "不合格";
                    else
                        cmbResult.Text = "合格";
                    break;
            }
        }

        private void cmbProduceCompany_TextChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 读取全部数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnReadAllData_Click(object sender, EventArgs e)
        {



        }

    }
}
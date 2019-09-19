using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DY.FoodClientLib;

namespace FoodClient
{
    public partial class FrmAutoTakeDY : Form
    {

        public FrmAutoTakeDY()
        {
            InitializeComponent();
        }

        /// <summary>
        /// DY系列自动检测
        /// 构造函数
        /// </summary>
        /// <param name="codeTag">表示不同的DY系列仪器，包括DY1000,DY2000,DY3000</param>
        public FrmAutoTakeDY(string codeTag)
        {
            InitializeComponent();
            _tag = codeTag;
        }

        #region 自定义变量
        private string _tag = string.Empty;
        private string _standardCode = string.Empty;
        private decimal _readSet = -1;
        private int _intRowSel = 0;
        private string _produceComSelectedValue = string.Empty;
        private string _checkedComSelectedValue = string.Empty;
        private string _upperComSelectedValue = string.Empty;
        private string _producePlaceSelectValue = string.Empty;

        protected string _foodSelectedValue = string.Empty;
        protected string _checkItemCode = string.Empty;
        protected string _sign = string.Empty;
        protected decimal _dTestValue = 0;
        protected string _checkUnit = string.Empty;
        protected string _machineCode = string.Empty;
        protected string[,] _checkItems;
        protected readonly clsResultOpr _resultBll = new clsResultOpr();
        #endregion

        /// <summary>
        /// 初始化绑定
        /// </summary>
       protected void BindInit()
        {
            lblParent.Text = ShareOption.AreaTitle + "：";
            lblName.Text = ShareOption.NameTitle + "：";
            lblDomain.Text = ShareOption.DomainTitle + "：";
            lblSample.Text = ShareOption.SampleTitle + "名称：";
            this.lblProduceTag.Text = ShareOption.ProductionUnitNameTag + "：";

            style1 = c1FlexGrid1.Styles.Add("style1");
            style1.ForeColor = Color.Red; ;
            styleNormal = c1FlexGrid1.Styles.Add("styleNormal");
            styleNormal.ForeColor = Color.Black;

            dtStart.Value = DateTime.Now.Date;
            dtEnd.Value = DateTime.Now;

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

            if (!ShareOption.IsRunCache)
            {
                CommonOperation.RunExeCache(10);
            }

            if (ShareOption.DtblCheckCompany != null)
            {
                cmbCheckUnit.DataSource = ShareOption.DtblCheckCompany.DataSet;
                cmbCheckUnit.DataMember = "UserUnit";
                cmbCheckUnit.DisplayMember = "FullName";
                cmbCheckUnit.ValueMember = "SysCode";
                cmbCheckUnit.Columns["StdCode"].Caption = "编号";
                cmbCheckUnit.Columns["FullName"].Caption = "检测单位";
                cmbCheckUnit.Columns["SysCode"].Caption = "系统编号";
            }

            if (ShareOption.DtblChecker != null)
            {
                DataSet dstChecker = ShareOption.DtblChecker.DataSet;
                cmbChecker.DataSource = dstChecker.Copy();
                cmbChecker.DataMember = "UserInfo";
                cmbChecker.DisplayMember = "Name";
                cmbChecker.ValueMember = "UserCode";
                cmbChecker.Columns["Name"].Caption = "检测人";
                cmbChecker.Columns["UserCode"].Caption = "系统编号";

                cmbAssessor.DataSource = dstChecker.Copy();// ds103;
                cmbAssessor.DataMember = "UserInfo";
                cmbAssessor.DisplayMember = "Name";
                cmbAssessor.ValueMember = "UserCode";
                cmbAssessor.Columns["Name"].Caption = "审核人";
                cmbAssessor.Columns["UserCode"].Caption = "系统编号";

                cmbOrganizer.DataSource = dstChecker.Copy();// ds104;
                cmbOrganizer.DataMember = "UserInfo";
                cmbOrganizer.DisplayMember = "Name";
                cmbOrganizer.ValueMember = "UserCode";
                cmbOrganizer.Columns["Name"].Caption = "编制人";
                cmbOrganizer.Columns["UserCode"].Caption = "系统编号";
            }
           
            cmbResult.Enabled = false;
            txtCheckValueInfo.Enabled = false;
            txtResultInfo.Enabled = false;
            txtStandard.Enabled = false;
            cmbCheckUnit.Enabled = false;
            cmbChecker.Enabled = false;
            txtStandValue.Enabled = false;

            string syscode = FrmMain.formMain.getNewSystemCode(true);
            txtSysID.Text = syscode;
            if (ShareOption.SysStdCodeSame)
            {
                txtCheckNo.Text = syscode;
            }

            cmbCheckType.SelectedIndex = 0;
            dtpProduceDate.Value = DateTime.Today;
            txtProduceDate.Text = dtpProduceDate.Value.ToString("yyyy-MM-dd");

            txtUnit.Text = ShareOption.SysUnit;
            txtSampleUnit.Text = ShareOption.SysUnit;
            dtpTakeDate.Value = DateTime.Today;
            cmbCheckItem.Text = clsCheckItemOpr.GetNameFromCode(_checkItemCode);
            txtStandard.Text = clsStandardOpr.GetNameFromCode(_standardCode);
            dtpCheckStartDate.Value = DateTime.Today;
            cmbCheckUnit.SelectedValue = FrmMain.formMain.checkUnitCode;
            cmbCheckUnit.Text = clsUserUnitOpr.GetNameFromCode(FrmMain.formMain.checkUnitCode);

            cmbChecker.SelectedValue = FrmMain.formMain.userCode;
            cmbChecker.Text = clsUserInfoOpr.NameFromCode(FrmMain.formMain.userCode);
            string title = this.Text.Replace("自动检测", "");
            lblPrompt.Text = "当前使用仪器设备为：" + title + "\r\n";

            if (ShareOption.SystemVersion == ShareOption.EnterpriseVersion)
            {
                cmbUpperCompany.Text = clsUserUnitOpr.GetNameFromCode(ShareOption.DefaultUserUnitCode);
                _upperComSelectedValue = clsCompanyOpr.CodeFromStdCode(clsUserUnitOpr.GetStdCode(ShareOption.DefaultUserUnitCode));
            }
            cmbCheckedCompany.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;

            //如果允许手工录入经营户信息
            if (ShareOption.AllowHandInputCheckUint)
            {
                if (ShareOption.SystemVersion == ShareOption.LocalBaseVersion)
                {
                    cmbUpperCompany.Enabled = true;
                }
                else
                {
                    cmbUpperCompany.Enabled = false;
                }
                txtCompanyInfo.Enabled = true;
            }
            else
            {
                cmbUpperCompany.Enabled = false;
                txtCompanyInfo.Enabled = false;
            }
        }

       private void btnOK_Click(object sender, System.EventArgs e)
       {
            #region 检验输入的值是否合法
           if (txtCheckNo.Text.Equals(string.Empty))
           {
               MessageBox.Show(this, "检测编号必须输入!");
               txtCheckNo.Focus();
               return;
           }
           if (_foodSelectedValue.Equals(string.Empty))
           {
               MessageBox.Show(this, ShareOption.SampleTitle + "必须输入!");
               cmbFood.Text = string.Empty;
               cmbFood.Focus();
               return;
           }
           if (ShareOption.AllowHandInputCheckUint)
           {
               if (_upperComSelectedValue.Equals(string.Empty))
               {
                   MessageBox.Show(this, ShareOption.AreaTitle + "必须输入!");
                   cmbUpperCompany.Text = string.Empty;
                   cmbUpperCompany.Focus();
                   return;
               }
               if (cmbCheckedCompany.Text.Equals(string.Empty))
               {
                   MessageBox.Show(this, ShareOption.NameTitle + "必须输入!");
                   cmbCheckedCompany.Text = string.Empty;
                   cmbCheckedCompany.Focus();
                   return;
               }
           }
           else
           {
               if (_checkedComSelectedValue.Equals(string.Empty))
               {
                   MessageBox.Show(this, ShareOption.NameTitle + "必须输入!");
                   cmbCheckedCompany.Text = string.Empty;
                   cmbCheckedCompany.Focus();
                   return;
               }
           }
            //if (cmbCheckUnit.SelectedValue == null)
            //{
            //    MessageBox.Show(this, "检测单位必须输入!");
            //    cmbCheckUnit.Text = string.Empty;
            //    cmbCheckUnit.Focus();
            //    return;
            //}
           if (cmbCheckUnit.Text == "")
           {
               MessageBox.Show(this, "检测单位必须输入!");
               cmbCheckUnit.Focus();
               return;
           }
            if (cmbChecker.SelectedValue == null)
            {
                MessageBox.Show(this, "检测人必须输入!");
                cmbChecker.Text = string.Empty;
                cmbChecker.Focus();
                return;
            }
            if (cmbResult.Text.Equals(string.Empty))
            {
                MessageBox.Show(this, "结论必须输入!");
                cmbResult.Focus();
                return;
            }

            //如果是工商版本同时为不合格的时候
            if (ShareOption.ApplicationTag == ShareOption.ICAppTag && cmbResult.Text == "不合格")
            {
                if (txtProduceDate.Text.Equals(string.Empty))
                {
                    MessageBox.Show(this, "检测结果不合格，生产日期必须输入!");
                    txtProduceDate.Focus();
                    return;
                }
                 //if (cmbProduceCompany.SelectedValue == null)
                if(cmbProduceCompany.Text=="")
                {
                    MessageBox.Show(this, string.Format("检测结果不合格，{0}必须输入!", lblProduceTag.Text));
                    cmbProduceCompany.Focus();
                    return;
                }
                //if (cmbProducePlace.SelectedValue == null)
                if (cmbProducePlace.Text == "") 
                {
                    MessageBox.Show(this, "检测结果不合格,产品产地必须输入");
                    cmbProducePlace.Focus();
                    return;
                }
                if (txtImportNum.Text.Equals(string.Empty))
                {
                    MessageBox.Show(this, "检测结果不合格,进货数量必须输入");
                    txtImportNum.Focus();
                    return;
                }
                if (txtSaveNum.Text.Equals(string.Empty))
                {
                    MessageBox.Show(this, "检测结果不合格,库存数量必须输入");
                    txtSaveNum.Focus();
                    return;
                }
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
            if (!StringUtil.IsValidNumber(txtSampleBaseNum.Text.Trim()))
            {
                MessageBox.Show(this, "抽样基数必须为整数!");
                txtSampleBaseNum.Focus();
                return;
            }
            if (!StringUtil.IsNumeric(txtSampleNum.Text.Trim()))
            {
                MessageBox.Show(this, "抽样数量必须为数字!");
                txtSampleNum.Focus();
                return;
            }
            if (!StringUtil.IsNumeric(txtImportNum.Text.Trim()))
            {
                MessageBox.Show(this, "进货数量必须为数字!");
                txtImportNum.Focus();
                return;
            }
            if (!StringUtil.IsNumeric(txtSaleNum.Text.Trim()))
            {
                MessageBox.Show(this, "销售数量必须为数字!");
                txtSaleNum.Focus();
                return;
            }
            if (!StringUtil.IsNumeric(txtPrice.Text.Trim()))
            {
                MessageBox.Show(this, "单价必须为数字!");
                txtPrice.Focus();
                return;
            }
            if (!StringUtil.IsNumeric(txtSaveNum.Text.Trim()))
            {
                MessageBox.Show(this, "库存数量必须为数字!");
                txtSaveNum.Focus();
                return;
            }
            if (dtpTakeDate.Value > dtpCheckStartDate.Value)
            {
                MessageBox.Show(this, "抽样日期不能超过检测开始时间!");
                dtpTakeDate.Focus();
                return;
            }
            #endregion

            clsResult model = new clsResult();
            string err = string.Empty;
            string syscode = FrmMain.formMain.getNewSystemCode(true);
            txtSysID.Text = syscode;

            model = new clsResult();
            model.SysCode = txtSysID.Text.Trim();

            if (ShareOption.SysStdCodeSame)
            {
                model.CheckNo = syscode;
                txtCheckNo.Text = syscode;
            }
            if (_resultBll.GetRecCount(" CheckNo='" + syscode + "'", out err) > 0)
            {
                MessageBox.Show(this, "检测编号已经存在请换一个编号!");
                txtCheckNo.Focus();
                return;
            }
            model.ResultType = ShareOption.ResultType5;
            model.StdCode = txtStdCode.Text.Trim();
            model.SampleCode = txtSampleCode.Text.Trim();
            model.CheckedCompany = _checkedComSelectedValue;
            model.CheckedCompanyName = cmbCheckedCompany.Text.Trim();
            model.CheckedComDis = txtCompanyInfo.Text.Trim();
            model.CheckPlaceCode = clsUserUnitOpr.GetNameFromCode("DistrictCode", ShareOption.DefaultUserUnitCode);
            model.FoodCode = _foodSelectedValue;
 
            string produceDate = txtProduceDate.Text;
            if (!string.IsNullOrEmpty(produceDate))
            {
                model.ProduceDate = Convert.ToDateTime(produceDate); //dtpProduceDate.Value;
            }

            model.ProduceCompany = _produceComSelectedValue;
            model.ProducePlace = _producePlaceSelectValue;
            model.SentCompany = txtSentCompany.Text.Trim();
            model.Provider = txtProvider.Text.Trim();
            model.TakeDate = dtpTakeDate.Value;
            model.CheckStartDate = dtpCheckStartDate.Value;
            if (txtImportNum.Text.Trim().Equals(""))
            {
                model.ImportNum = string.Empty;
            }
            else
            {
                model.ImportNum = Convert.ToDouble(txtImportNum.Text.Trim()).ToString();
            }
            if (txtSaveNum.Text.Trim().Equals(string.Empty))
            {
                model.SaveNum = string.Empty;
            }
            else
            {
                model.SaveNum = Convert.ToDouble(txtSaveNum.Text.Trim()).ToString();
            }
            model.Unit = txtUnit.Text.Trim();
            if (txtSampleBaseNum.Text.Trim().Equals(string.Empty))
            {
                model.SampleBaseNum = "null";
            }
            else
            {
                model.SampleBaseNum = txtSampleBaseNum.Text.Trim();
            }
            if (txtSampleNum.Text.Trim().Equals(string.Empty))
            {
                model.SampleNum = "null";
            }
            else
            {
                model.SampleNum = txtSampleNum.Text.Trim();
            }
            model.SampleUnit = txtSampleUnit.Text.Trim();
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
            model.UpperCompanyName = cmbUpperCompany.Text.Trim();
            model.OrCheckNo = txtOrCheckNo.Text.Trim();
            model.CheckType = cmbCheckType.Text;

            if (cmbCheckUnit.SelectedValue == null)
            {
                model.CheckUnitCode = string.Empty;
            }
            else
            {
                model.CheckUnitCode = cmbCheckUnit.SelectedValue.ToString();
            }

            if (cmbChecker.SelectedValue == null)
            {
                model.Checker = string.Empty;
            }
            else
            {
                model.Checker = cmbChecker.SelectedValue.ToString();
            }
            if (cmbAssessor.SelectedValue == null)
            {
                model.Assessor = string.Empty;
            }
            else
            {
                model.Assessor = cmbAssessor.SelectedValue.ToString();
            }
            if (cmbOrganizer.SelectedValue == null)
            {
                model.Organizer = string.Empty;
            }
            else
            {
                model.Organizer = cmbOrganizer.SelectedValue.ToString();
            }
            model.Remark = txtRemark.Text.Trim();
            model.CheckPlanCode = txtCheckPlanCode.Text.Trim();
            if (txtSaleNum.Text.Trim().Equals(string.Empty))
            {
                model.SaleNum = "null";
            }
            else
            {
                model.SaleNum = txtSaleNum.Text.Trim();
            }
            if (txtPrice.Text.Trim().Equals(string.Empty))
            {
                model.Price = "null";
            }
            else
            {
                model.Price = txtPrice.Text.Trim();
            }
            model.CheckederVal = cmbCheckerVal.Text.ToString();
            model.IsSentCheck = cmbIsSentCheck.Text.ToString();
            model.CheckederRemark = txtCheckerRemark.Text.ToString();
            model.Notes = txtNotes.Text.ToString();
            model.CheckMachine = _machineCode;
            model.HolesNum = lblHolesNum.Text;
            model.MachineItemName = lblMachineItemName.Text;
            model.MachineSampleNum = lblMachineSampleNum.Text;

            _resultBll.Insert(model, out err);

            if (!err.Equals(string.Empty))
            {
                MessageBox.Show(this, "数据库操作出错！");
                DialogResult = DialogResult.OK;
                // Close();
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

        /// <summary>
        /// 添加完后清空
        /// </summary>
       private void AddEmpty()
        {
            clsResult model = new clsResult();
            string syscode = FrmMain.formMain.getNewSystemCode(true);
            model.SysCode = syscode;

            if (ShareOption.SysStdCodeSame)
            {
                model.CheckNo = syscode;
            }
            else
            {
                model.CheckNo = string.Empty;
            }

            model.StdCode = string.Empty;
            model.SampleCode = string.Empty;
            model.CheckedCompany = _checkedComSelectedValue;
            model.CheckedCompanyName = cmbCheckedCompany.Text.Trim();
            model.CheckedComDis = txtCompanyInfo.Text.Trim();
            model.CheckPlaceCode = string.Empty;
            model.FoodCode = _foodSelectedValue;
            // model.ProduceDate = DateTime.Today.AddDays(-1);
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
            model.UpperCompanyName = cmbUpperCompany.Text.Trim();
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
            {
                dtpProduceDate.Value = Convert.ToDateTime(tempdt);
                txtProduceDate.Text = dtpProduceDate.Value.ToString("yyyy-MM-dd");
            }
            _produceComSelectedValue = model.ProduceCompany;
            _producePlaceSelectValue = model.ProducePlace;
            txtSentCompany.Text = model.SentCompany;
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
            if (model.SaleNum == "null")
            {
                txtSaleNum.Text = string.Empty;
            }
            else
            {
                txtSaleNum.Text = model.SaleNum.ToString();
            }
            if (model.Price == "null")
            {
                txtPrice.Text = string.Empty;
            }
            else
            {
                txtPrice.Text = model.Price.ToString();
            }
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
            decimal checkValue = Decimal.Parse(txtCheckValueInfo.Text);
            switch (_sign)
            {
                case "<":
                    if (checkValue >= testValue)
                    {
                        cmbResult.Text = "不合格";
                    }
                    else
                    {
                        cmbResult.Text = "合格";
                    }
                    break;
                case "≤":
                    if (checkValue > testValue)
                    {
                        cmbResult.Text = "不合格";
                    }
                    else
                    {
                        cmbResult.Text = "合格";
                    }
                    break;
                case ">":
                    if (checkValue <= testValue)
                    {
                        cmbResult.Text = "不合格";
                    }
                    else
                    {
                        cmbResult.Text = "合格";
                    }
                    break;
                case "≥":
                    if (checkValue < testValue)
                    {
                        cmbResult.Text = "不合格";
                    }
                    else
                    {
                        cmbResult.Text = "合格";
                    }
                    break;
            }
            //如果是工商版本同时是不合格的时候
            //产品产地、生产单位、生产日期、进货数量、库存数量 是必录项
            if (ShareOption.ApplicationTag == ShareOption.ICAppTag && cmbResult.Text == "不合格")
            {
                lblPerImportNumTag.Visible = true;
                lblPerProduceComTag.Visible = true;
                lblPerProduceDateTag.Visible = true;
                lblPerProduceTag.Visible = true;
                lblPerSaveNumTag.Visible = true;
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
            {
                return;
            }
            if (c1FlexGrid1.RowSel == _intRowSel)
            {
                return;
            }
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

                if (_tag != "DYRSY2"&&_tag!="DY6400")//两款肉类水分仪除外
                {
                    if (_checkItems == null)
                    {
                        return;
                    }
                    string strItem = lblMachineItemName.Text;
                    bool IsExist = false;
                  
                    for (int i = 0; i < _checkItems.GetLength(0); i++)
                    {
                        if (strItem.Equals(_checkItems[i, 0]))
                        {
                            IsExist = true;
                            if (_checkItems[i, 1].Equals("-1"))
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
                                MessageBox.Show(this, "该仪器的这个检测功能项尚未对应检测项目，无法进行下去，请到选项中设置仪器！");
                                return;
                            }
                            else
                            {
                                _checkItemCode = _checkItems[i, 1].ToString();
                                _standardCode = clsCheckItemOpr.GetStandardCode(_checkItems[i, 1].ToString());
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
                    cmbCheckItem.Text = clsCheckItemOpr.GetNameFromCode(_checkItemCode);
                    txtResultInfo.Text = clsCheckItemOpr.GetUnitFromCode(_checkItemCode);
                    txtStandard.Text = clsStandardOpr.GetNameFromCode(_standardCode);

                    dtpCheckStartDate.Value = DateTime.Parse(c1FlexGrid1.Rows[_intRowSel]["检测时间"].ToString());
                    dtpTakeDate.Value = dtpCheckStartDate.Value;
                }


                if (!string.IsNullOrEmpty(_foodSelectedValue))
                {
                    string[] result = clsFoodClassOpr.ValueFromCode(_foodSelectedValue, _checkItemCode);
                    _sign = result[0];
                    _dTestValue = Convert.ToDecimal(result[1]);
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
                        txtStandValue.Text = _dTestValue.ToString();
                    }
                }

                try
                {
                    txtCheckValueInfo.Text = Decimal.Parse(txtCheckValueInfo.Text).ToString();
                    checkValueState(_dTestValue);

                    setTitleColor(Color.Blue);  //改变标题颜色

                    FrmMsg frm = new FrmMsg("检测数据已自动赋值"); //弹出对话框
                    frm.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    txtCheckValueInfo.Text = string.Empty;
                    cmbResult.Text = string.Empty;
                    MessageBox.Show("这是错误或空白检测记录，无法采集数据。请选择正常检测记录！" + ex.Message);
                }
            }
        }

        #region 下拉窗口
        private void cmbFood_BeforeOpen(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!_checkItemCode.Equals(string.Empty))
            {
                frmFoodSelect frm = new frmFoodSelect(_checkItemCode, _foodSelectedValue);
                frm.ShowDialog(this);
                if (frm.DialogResult == DialogResult.OK)
                {
                    _foodSelectedValue = frm.sNodeTag;
                    cmbFood.Text = frm.sNodeName;
                    _sign = frm.sSign;
                    _dTestValue = Convert.ToDecimal(frm.sValue);
                    _checkUnit = frm.sUnit;
                    txtStandValue.Text = frm.sValue;
                    if (txtCheckValueInfo.Text != string.Empty)
                    {
                        checkValueState(_dTestValue);//自动关联合格或不合格
                    }
                }
            }
            else
            {
                if (!_machineCode.Equals(string.Empty))
                {
                    MessageBox.Show(this, "没有设置仪器所对应的检测项目，请到选项中设置仪器！");
                }
                else
                {
                    MessageBox.Show(this, "没有选择检测项目！");
                }
            }
            e.Cancel = true;
        }
        private void cmbCheckedCompany_BeforeOpen(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ShareOption.AllowHandInputCheckUint)
            {
                if (!_upperComSelectedValue.Equals(""))
                {
                    FrmMain.formCheckedComSelect = new frmCheckedComSelect("", _upperComSelectedValue);
                    FrmMain.formCheckedComSelect.Tag = "Checked";
                }
                else
                {
                    MessageBox.Show(this, string.Format("请先选择{0}！",lblParent.Text));
                    e.Cancel = true;
                    return;
                }
            }
            else
            {
                if (!FrmMain.IsLoadCheckedComSel)
                {
                    FrmMain.formCheckedComSelect = new frmCheckedComSelect("", _checkedComSelectedValue);
                    FrmMain.formCheckedComSelect.Tag = "Checked";
                }
                else
                {
                    FrmMain.formCheckedComSelect.Tag = "Checked";
                    FrmMain.formCheckedComSelect.SetFormValues("", _checkedComSelectedValue);
                }
            }
            FrmMain.formCheckedComSelect.ShowDialog(this);
            if (FrmMain.formCheckedComSelect.DialogResult == DialogResult.OK)
            {
                this._checkedComSelectedValue = FrmMain.formCheckedComSelect.sNodeTag;
                this.cmbCheckedCompany.Text = FrmMain.formCheckedComSelect.sNodeName;
                if (!ShareOption.AllowHandInputCheckUint)
                {
                    if (FrmMain.formCheckedComSelect.sParentCompanyName.Equals(""))
                    {
                        this.cmbUpperCompany.Text = "";
                        this._upperComSelectedValue = "";
                    }
                    else
                    {
                        this.cmbUpperCompany.Text = FrmMain.formCheckedComSelect.sParentCompanyName.ToString();
                        this._upperComSelectedValue = FrmMain.formCheckedComSelect.sParentCompanyTag.ToString();

                    }
                }
                if (FrmMain.formCheckedComSelect.sNodeCompanyInfo.Equals(""))
                {
                    this.txtCompanyInfo.Text = "";
                }
                else
                {
                    this.txtCompanyInfo.Text = FrmMain.formCheckedComSelect.sNodeCompanyInfo.ToString();
                }
            }
            FrmMain.formCheckedComSelect.Hide();
            e.Cancel = true;


        }

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
            frmCheckedComSelect frm = new frmCheckedComSelect(string.Empty, _produceComSelectedValue);
            frm.Tag = "Produce";
            frm.ShowDialog(this);
            if (frm.DialogResult == DialogResult.OK)
            {
                _produceComSelectedValue = frm.sNodeTag;
                cmbProduceCompany.Text = frm.sNodeName;
            }
            else
            {
                _produceComSelectedValue = string.Empty;
                cmbProduceCompany.Text = string.Empty;
            }
            e.Cancel = true;
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

        private void dtpProduceDate_ValueChanged(object sender, EventArgs e)
        {
            txtProduceDate.Text = dtpProduceDate.Value.ToString("yyyy-MM-dd");
        }
        #endregion

        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
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
    }
}

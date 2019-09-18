using System;
using System.Windows;
using DYSeriesDataSet;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// AddOrUpdateCompany.xaml 的交互逻辑
    /// </summary>
    public partial class AddOrUpdateCompany : Window
    {
        public AddOrUpdateCompany()
        {
            InitializeComponent();
        }

        public clsCompany _clsCompany = new clsCompany();
        private clsCompanyOpr _clsCompanybll = new clsCompanyOpr();
        private bool _isUpdate = false, _isClose = true;

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_clsCompany != null && _clsCompany.ID > 0)
                {
                    this.textBoxFullName.Text = _clsCompany.FullName ?? string.Empty;
                    this.textBoxSysCode.Text = _clsCompany.SysCode ?? string.Empty;
                    this.textBoxStdCode.Text = _clsCompany.StdCode ?? string.Empty;
                    this.textBoxCAllow.Text = _clsCompany.CAllow ?? string.Empty;
                    this.textBoxISSUEAGENCY.Text = _clsCompany.ISSUEAGENCY ?? string.Empty;
                    this.textBoxISSUEDATE.Text = _clsCompany.ISSUEDATE ?? string.Empty;
                    this.textBoxPERIODSTART.Text = _clsCompany.PERIODSTART ?? string.Empty;
                    this.textBoxVIOLATENUM.Text = _clsCompany.VIOLATENUM ?? string.Empty;
                    this.textBoxLONGITUDE.Text = _clsCompany.LONGITUDE ?? string.Empty;
                    this.textBoxPERIODEND.Text = _clsCompany.PERIODEND ?? string.Empty;
                    this.textBoxLATITUDE.Text = _clsCompany.LATITUDE ?? string.Empty;
                    this.textBoxSCOPE.Text = _clsCompany.SCOPE ?? string.Empty;
                    this.textBoxPUNISH.Text = _clsCompany.PUNISH ?? string.Empty;
                    this.textBoxCompanyID.Text = _clsCompany.CompanyID ?? string.Empty;
                    this.textBoxOtherCodeInfo.Text = _clsCompany.OtherCodeInfo ?? string.Empty;
                    this.textBoxShortName.Text = _clsCompany.ShortName ?? string.Empty;
                    this.textBoxDisplayName.Text = _clsCompany.DisplayName ?? string.Empty;
                    this.textBoxProperty.Text = _clsCompany.Property ?? string.Empty;
                    this.textBoxKindCode.Text = _clsCompany.KindCode ?? string.Empty;
                    this.textBoxRegCapital.Text = _clsCompany.RegCapital.ToString();
                    this.textBoxUnit.Text = _clsCompany.Unit ?? string.Empty;
                    this.textBoxIncorporator.Text = _clsCompany.Incorporator ?? string.Empty;
                    this.textBoxRegDate.Text = _clsCompany.RegDate != null ? _clsCompany.RegDate.ToString() : string.Empty;
                    this.textBoxDistrictCode.Text = _clsCompany.DistrictCode ?? string.Empty;
                    this.textBoxAddress.Text = _clsCompany.Address ?? string.Empty;
                    this.textBoxPostCode.Text = _clsCompany.PostCode ?? string.Empty;
                    this.textBoxLinkMan.Text = _clsCompany.LinkMan ?? string.Empty;
                    this.textBoxLinkInfo.Text = _clsCompany.LinkInfo ?? string.Empty;
                    this.textBoxCreditLevel.Text = _clsCompany.CreditLevel ?? string.Empty;
                    this.textBoxCreditRecord.Text = _clsCompany.CreditRecord ?? string.Empty;
                    this.textBoxProductInfo.Text = _clsCompany.ProductInfo ?? string.Empty;
                    this.textBoxOtherInfo.Text = _clsCompany.OtherInfo ?? string.Empty;
                    this.textBoxCheckLevel.Text = _clsCompany.CheckLevel ?? string.Empty;
                    this.textBoxFoodSafeRecord.Text = _clsCompany.FoodSafeRecord ?? string.Empty;
                    this.textBoxIsReadOnly.Text = _clsCompany.IsReadOnly ? "是" : "否";
                    this.textBoxIsLock.Text = _clsCompany.IsLock ? "是" : "否";
                    this.textBoxRemark.Text = _clsCompany.Remark ?? string.Empty;
                    this.textBoxComProperty.Text = _clsCompany.ComProperty ?? string.Empty;
                    this.textBoxTSign.Text = _clsCompany.TSign ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("出现异常！\r\b异常信息：" + ex.Message, "系统提示");
            }
            finally { _isUpdate = false; }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            clsCompany model = new clsCompany();
            string err = string.Empty;
            if (CheckSave())
            {
                _isClose = true;
                try
                {
                    if (_clsCompany != null && _clsCompany.ID > 0)
                    {
                        model.ID = _clsCompany.ID;
                        this.btnSave.Content = "修改";
                    }
                    model.FullName = this.textBoxFullName.Text.Trim();
                    //2015年11月12日 系统编码改为获取当前系统时间
                    //model.SysCode = this.textBoxSysCode.Text.Trim();
                    model.SysCode = DateTime.Now.Millisecond.ToString();
                    //营业执照为空
                    //model.StdCode = this.textBoxStdCode.Text.Trim();
                    model.StdCode = string.Empty;
                    //许可证号为空
                    //model.CAllow = this.textBoxCAllow.Text.Trim();
                    model.CAllow = string.Empty;
                    model.ISSUEAGENCY = this.textBoxISSUEAGENCY.Text.Trim();
                    model.ISSUEDATE = this.textBoxISSUEDATE.Text.Trim();
                    model.PERIODSTART = this.textBoxPERIODSTART.Text.Trim();
                    model.VIOLATENUM = this.textBoxVIOLATENUM.Text.Trim();
                    model.LONGITUDE = this.textBoxLONGITUDE.Text.Trim();
                    model.PERIODEND = this.textBoxPERIODEND.Text.Trim();
                    model.LATITUDE = this.textBoxLATITUDE.Text.Trim();
                    model.SCOPE = this.textBoxSCOPE.Text.Trim();
                    model.PUNISH = this.textBoxPUNISH.Text.Trim();
                    model.CompanyID = this.textBoxCompanyID.Text.Trim();
                    model.OtherCodeInfo = this.textBoxOtherCodeInfo.Text.Trim();
                    model.ShortName = this.textBoxShortName.Text.Trim();
                    model.DisplayName = this.textBoxDisplayName.Text.Trim();
                    model.Property = this.textBoxProperty.Text.Trim();
                    model.KindCode = this.textBoxKindCode.Text.Trim();
                    //model.RegCapital = this.textBoxRegCapital.Text.Trim();
                    model.Unit = this.textBoxUnit.Text.Trim();
                    model.Incorporator = this.textBoxIncorporator.Text.Trim();
                    //model.RegDate = this.textBoxRegDate.Text.Trim();
                    model.DistrictCode = this.textBoxDistrictCode.Text.Trim();
                    model.Address = this.textBoxAddress.Text.Trim();
                    model.PostCode = this.textBoxPUNISH.Text.Trim();
                    model.LinkMan = this.textBoxLinkMan.Text.Trim();
                    model.LinkInfo = this.textBoxLinkInfo.Text.Trim();
                    model.CreditLevel = this.textBoxCreditLevel.Text.Trim();
                    model.CreditRecord = this.textBoxCreditRecord.Text.Trim();
                    model.ProductInfo = this.textBoxProductInfo.Text.Trim();
                    model.OtherInfo = this.textBoxOtherInfo.Text.Trim();
                    model.CheckLevel = this.textBoxCheckLevel.Text.Trim();
                    model.FoodSafeRecord = this.textBoxFoodSafeRecord.Text.Trim();
                    //model.IsReadOnly = this.textBoxIsReadOnly.Text.Trim();
                    //model.IsLock = this.textBoxIsLock.Text.Trim();
                    model.Remark = this.textBoxRemark.Text.Trim();
                    model.ComProperty = this.textBoxComProperty.Text.Trim();
                    model.TSign = this.textBoxTSign.Text.Trim();
                    err = string.Empty;
                    _clsCompanybll.Insert(model, out err);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("保存失败!\n出现异常：" + ex.Message);
                }
                finally
                {
                    if (err.Equals(string.Empty))
                    {
                        _isUpdate = false;
                        if (btnSave.Content.Equals("保存"))
                        {
                            if (MessageBox.Show("保存成功!\n是否继续添加被检单位?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                this.textBoxFullName.Text = string.Empty;
                                this.textBoxSysCode.Text = string.Empty;
                                this.textBoxStdCode.Text = string.Empty;
                                this.textBoxCAllow.Text = string.Empty;
                                this.textBoxISSUEAGENCY.Text = string.Empty;
                                this.textBoxISSUEDATE.Text = string.Empty;
                                this.textBoxPERIODSTART.Text = string.Empty;
                                this.textBoxVIOLATENUM.Text = string.Empty;
                                this.textBoxLONGITUDE.Text = string.Empty;
                                this.textBoxPERIODEND.Text = string.Empty;
                                this.textBoxLATITUDE.Text = string.Empty;
                                this.textBoxSCOPE.Text = string.Empty;
                                this.textBoxPUNISH.Text = string.Empty;
                                this.textBoxCompanyID.Text = string.Empty;
                                this.textBoxOtherCodeInfo.Text = string.Empty;
                                this.textBoxShortName.Text = string.Empty;
                                this.textBoxDisplayName.Text = string.Empty;
                                this.textBoxProperty.Text = string.Empty;
                                this.textBoxKindCode.Text = string.Empty;
                                this.textBoxRegCapital.Text = string.Empty;
                                this.textBoxUnit.Text = string.Empty;
                                this.textBoxIncorporator.Text = string.Empty;
                                this.textBoxRegDate.Text = string.Empty;
                                this.textBoxDistrictCode.Text = string.Empty;
                                this.textBoxAddress.Text = string.Empty;
                                this.textBoxPostCode.Text = string.Empty;
                                this.textBoxLinkMan.Text = string.Empty;
                                this.textBoxLinkInfo.Text = string.Empty;
                                this.textBoxCreditLevel.Text = string.Empty;
                                this.textBoxCreditRecord.Text = string.Empty;
                                this.textBoxProductInfo.Text = string.Empty;
                                this.textBoxOtherInfo.Text = string.Empty;
                                this.textBoxCheckLevel.Text = string.Empty;
                                this.textBoxFoodSafeRecord.Text = string.Empty;
                                this.textBoxIsReadOnly.Text = string.Empty;
                                this.textBoxIsLock.Text = string.Empty;
                                this.textBoxRemark.Text = string.Empty;
                                this.textBoxComProperty.Text = string.Empty;
                                this.textBoxTSign.Text = string.Empty;
                                this.Title = "新增被检单位";
                                this.btnSave.Content = "保存";
                                _clsCompany = new clsCompany();
                                _isClose = false;
                                _isUpdate = false;
                            }
                            else
                            {
                                _isClose = true;
                                _isUpdate = false;
                                try { this.Close(); }
                                catch (Exception) { }
                            }
                        }
                        else
                        {
                            MessageBox.Show("保存成功！", "系统提示");
                            _isClose = true;
                            _isUpdate = false;
                            try { this.Close(); }
                            catch (Exception) { }
                        }
                    }
                    else
                    {
                        MessageBox.Show("保存失败!\r\n异常信息：" + err.ToString());
                    }
                }
            }
            else
            {
                _isClose = false;
            }
        }

        /// <summary>
        /// 验证信息完整性
        /// </summary>
        /// <returns></returns>
        private bool CheckSave()
        {
            if (this.textBoxFullName.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("被检单位名称不能为空!");
                textBoxFullName.Focus();
                return false;
            }
            return true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_isUpdate)
            {
                if (MessageBox.Show("是否保存当前内容?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    btnSave_Click(null, null);
                }
                else
                {
                    _isClose = true;
                }
            }
            else
            {
                _isClose = true;
            }

            if (!_isClose)
            {
                e.Cancel = true;
            }
        }

        private void textBoxFullName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void textBoxAddress_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void textBoxLinkMan_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void textBoxLinkInfo_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

        private void textBoxIncorporator_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            _isUpdate = true;
        }

    }
}

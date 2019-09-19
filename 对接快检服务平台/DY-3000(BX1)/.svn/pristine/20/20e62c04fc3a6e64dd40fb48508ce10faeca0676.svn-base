using System;
using System.Windows;
using com.lvrenyang;
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
        public String _type = String.Empty;
        private clsCompanyOpr _clsCompanybll = new clsCompanyOpr();
        private bool _isUpdate = false;
        private bool _isClose = true;
        private string logType = "AddOrUpdateCompany-error";

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
                    this.textBoxFullName.Text = _clsCompany.FullName != null ? _clsCompany.FullName : "";
                    this.textBoxSysCode.Text = _clsCompany.SysCode != null ? _clsCompany.SysCode : "";
                    this.textBoxStdCode.Text = _clsCompany.StdCode != null ? _clsCompany.StdCode : "";
                    this.textBoxCAllow.Text = _clsCompany.CAllow != null ? _clsCompany.CAllow : "";
                    this.textBoxISSUEAGENCY.Text = _clsCompany.ISSUEAGENCY != null ? _clsCompany.ISSUEAGENCY : "";
                    this.textBoxISSUEDATE.Text = _clsCompany.ISSUEDATE != null ? _clsCompany.ISSUEDATE : "";
                    this.textBoxPERIODSTART.Text = _clsCompany.PERIODSTART != null ? _clsCompany.PERIODSTART : "";
                    this.textBoxVIOLATENUM.Text = _clsCompany.VIOLATENUM != null ? _clsCompany.VIOLATENUM : "";
                    this.textBoxLONGITUDE.Text = _clsCompany.LONGITUDE != null ? _clsCompany.LONGITUDE : "";
                    this.textBoxPERIODEND.Text = _clsCompany.PERIODEND != null ? _clsCompany.PERIODEND : "";
                    this.textBoxLATITUDE.Text = _clsCompany.LATITUDE != null ? _clsCompany.LATITUDE : "";
                    this.textBoxSCOPE.Text = _clsCompany.SCOPE != null ? _clsCompany.SCOPE : "";
                    this.textBoxPUNISH.Text = _clsCompany.PUNISH != null ? _clsCompany.PUNISH : "";
                    this.textBoxCompanyID.Text = _clsCompany.CompanyID != null ? _clsCompany.CompanyID : "";
                    this.textBoxOtherCodeInfo.Text = _clsCompany.OtherCodeInfo != null ? _clsCompany.OtherCodeInfo : "";
                    this.textBoxShortName.Text = _clsCompany.ShortName != null ? _clsCompany.ShortName : "";
                    this.textBoxDisplayName.Text = _clsCompany.DisplayName != null ? _clsCompany.DisplayName : "";
                    this.textBoxProperty.Text = _clsCompany.Property != null ? _clsCompany.Property : "";
                    this.textBoxKindCode.Text = _clsCompany.KindCode != null ? _clsCompany.KindCode : "";
                    this.textBoxRegCapital.Text = _clsCompany.RegCapital.ToString();
                    this.textBoxUnit.Text = _clsCompany.Unit != null ? _clsCompany.Unit : "";
                    this.textBoxIncorporator.Text = _clsCompany.Incorporator != null ? _clsCompany.Incorporator : "";
                    this.textBoxRegDate.Text = _clsCompany.RegDate != null ? _clsCompany.RegDate.ToString() : "";
                    this.textBoxDistrictCode.Text = _clsCompany.DistrictCode != null ? _clsCompany.DistrictCode : "";
                    this.textBoxAddress.Text = _clsCompany.Address != null ? _clsCompany.Address : "";
                    this.textBoxPostCode.Text = _clsCompany.PostCode != null ? _clsCompany.PostCode : "";
                    this.textBoxLinkMan.Text = _clsCompany.LinkMan != null ? _clsCompany.LinkMan : "";
                    this.textBoxLinkInfo.Text = _clsCompany.LinkInfo != null ? _clsCompany.LinkInfo : "";
                    this.textBoxCreditLevel.Text = _clsCompany.CreditLevel != null ? _clsCompany.CreditLevel : "";
                    this.textBoxCreditRecord.Text = _clsCompany.CreditRecord != null ? _clsCompany.CreditRecord : "";
                    this.textBoxProductInfo.Text = _clsCompany.ProductInfo != null ? _clsCompany.ProductInfo : "";
                    this.textBoxOtherInfo.Text = _clsCompany.OtherInfo != null ? _clsCompany.OtherInfo : "";
                    this.textBoxCheckLevel.Text = _clsCompany.CheckLevel != null ? _clsCompany.CheckLevel : "";
                    this.textBoxFoodSafeRecord.Text = _clsCompany.FoodSafeRecord != null ? _clsCompany.FoodSafeRecord : "";
                    this.textBoxIsReadOnly.Text = _clsCompany.IsReadOnly ? "是" : "否";
                    this.textBoxIsLock.Text = _clsCompany.IsLock ? "是" : "否";
                    this.textBoxRemark.Text = _clsCompany.Remark != null ? _clsCompany.Remark : "";
                    this.textBoxComProperty.Text = _clsCompany.ComProperty != null ? _clsCompany.ComProperty : "";
                    this.textBoxTSign.Text = _clsCompany.TSign != null ? _clsCompany.TSign : "";
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("初始化UI时出现异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
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

            if (checkSave())
            {
                _isClose = true;
                try
                {
                    if (_clsCompany != null && _clsCompany.ID > 0)
                    {
                        model.ID = _clsCompany.ID;
                    }
                    model.FullName = this.textBoxFullName.Text.Trim();
                    //2015年11月12日 系统编码改为获取当前系统时间
                    //model.SysCode = this.textBoxSysCode.Text.Trim();
                    model.SysCode = DateTime.Now.Millisecond.ToString();
                    //营业执照为空
                    //model.StdCode = this.textBoxStdCode.Text.Trim();
                    model.StdCode = "";
                    //许可证号为空
                    //model.CAllow = this.textBoxCAllow.Text.Trim();
                    model.CAllow = "";
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
                    FileUtils.OprLog(6, logType, ex.ToString());
                    MessageBox.Show("保存数据时发生异常!\r\n\r\n异常信息:" + ex.Message, "系统提示");
                }
                finally
                {
                    if (err.Equals(""))
                    {
                        _isUpdate = false;
                        if (_type.Equals("ADD"))
                        {
                            if (MessageBox.Show("保存成功!\n是否继续添加被检单位?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                this.textBoxFullName.Text = "";
                                this.textBoxSysCode.Text = "";
                                this.textBoxStdCode.Text = "";
                                this.textBoxCAllow.Text = "";
                                this.textBoxISSUEAGENCY.Text = "";
                                this.textBoxISSUEDATE.Text = "";
                                this.textBoxPERIODSTART.Text = "";
                                this.textBoxVIOLATENUM.Text = "";
                                this.textBoxLONGITUDE.Text = "";
                                this.textBoxPERIODEND.Text = "";
                                this.textBoxLATITUDE.Text = "";
                                this.textBoxSCOPE.Text = "";
                                this.textBoxPUNISH.Text = "";
                                this.textBoxCompanyID.Text = "";
                                this.textBoxOtherCodeInfo.Text = "";
                                this.textBoxShortName.Text = "";
                                this.textBoxDisplayName.Text = "";
                                this.textBoxProperty.Text = "";
                                this.textBoxKindCode.Text = "";
                                this.textBoxRegCapital.Text = "";
                                this.textBoxUnit.Text = "";
                                this.textBoxIncorporator.Text = "";
                                this.textBoxRegDate.Text = "";
                                this.textBoxDistrictCode.Text = "";
                                this.textBoxAddress.Text = "";
                                this.textBoxPostCode.Text = "";
                                this.textBoxLinkMan.Text = "";
                                this.textBoxLinkInfo.Text = "";
                                this.textBoxCreditLevel.Text = "";
                                this.textBoxCreditRecord.Text = "";
                                this.textBoxProductInfo.Text = "";
                                this.textBoxOtherInfo.Text = "";
                                this.textBoxCheckLevel.Text = "";
                                this.textBoxFoodSafeRecord.Text = "";
                                this.textBoxIsReadOnly.Text = "";
                                this.textBoxIsLock.Text = "";
                                this.textBoxRemark.Text = "";
                                this.textBoxComProperty.Text = "";
                                this.textBoxTSign.Text = "";
                                _clsCompany = new clsCompany();
                                textBoxFullName.Focus();
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
                            MessageBox.Show("编辑成功！", "操作提示");
                            _isUpdate = false;
                            try { this.Close(); }
                            catch (Exception ex) { FileUtils.OprLog(6, logType, ex.ToString()); }
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
        private bool checkSave()
        {
            try
            {
                if (this.textBoxFullName.Text.Trim().Equals(""))
                {
                    MessageBox.Show("被检单位名称不能为空！");
                    textBoxFullName.Focus();
                    return false;
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
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
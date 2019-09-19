using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DYSeriesDataSet.DataSentence.Kjc;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// AddOrUpdateKjcCompany.xaml 的交互逻辑
    /// </summary>
    public partial class AddOrUpdateKjcCompany : Window
    {
        public AddOrUpdateKjcCompany()
        {
            InitializeComponent();
        }

        public kjcCompany model = null;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (model != null)
            {
                Title = "编辑 - 被检单位";
                tb_regName.Text = model.regName;
                tb_regCorpName.Text = model.regCorpName;
                tb_contactMan.Text = model.contactMan;
                tb_contactPhone.Text = model.contactPhone;
                tb_regAddress.Text = model.regAddress;
                tb_regPost.Text = model.regPost;
            }
            else
            {
                Title = "新增 - 被检单位";
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //kjcCompanyBLL
            if (!CheckInfo()) return;
            string errMsg = string.Empty;
            if (model != null)//修改
            {
                kjcCompany kjcModel = model;
                kjcModel.regName = tb_regName.Text.Trim();
                kjcModel.regCorpName = tb_regCorpName.Text.Trim();
                kjcModel.contactMan = tb_contactMan.Text.Trim();
                kjcModel.contactPhone = tb_contactPhone.Text.Trim();
                kjcModel.regAddress = tb_regAddress.Text.Trim();
                kjcModel.regPost = tb_regPost.Text.Trim();
                kjcCompanyBLL.Update(kjcModel, out errMsg);
                if (errMsg.Length == 0)
                {
                    MessageBox.Show("修改成功！");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("修改失败！\r\n异常信息：" + errMsg, "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                kjcCompany kjcModel = new kjcCompany();
                kjcModel.regId = Global.GETGUID();
                kjcModel.regName = tb_regName.Text.Trim();
                kjcModel.regCorpName = tb_regCorpName.Text.Trim();
                kjcModel.contactMan = tb_contactMan.Text.Trim();
                kjcModel.contactPhone = tb_contactPhone.Text.Trim();
                kjcModel.regAddress = tb_regAddress.Text.Trim();
                kjcModel.regPost = tb_regPost.Text.Trim();
                kjcCompanyBLL.Insert(kjcModel, out errMsg);
                if (errMsg.Length == 0)
                {
                    MessageBox.Show("新增成功！");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("新增失败！\r\n异常信息：" + errMsg, "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool CheckInfo() 
        {
            if (tb_regName.Text.Trim().Length == 0)
            {
                MessageBox.Show("公司名称不能为空！");
                return false;
            }
            if (tb_contactMan.Text.Trim().Length == 0)
            {
                MessageBox.Show("联系人不能为空！");
                return false;
            }
            if (tb_contactPhone.Text.Trim().Length == 0)
            {
                MessageBox.Show("联系信息不能为空！");
                return false;
            }
            return true;
        }

    }
}
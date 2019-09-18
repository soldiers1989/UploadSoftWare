using System;
using System.Collections.Generic;
using System.Windows;

namespace AIO
{
    /// <summary>
    /// LoginNameEdit.xaml 的交互逻辑
    /// </summary>
    public partial class LoginNameEdit : Window
    {
        public UserAccount _item = null;

        public LoginNameEdit()
        {
            InitializeComponent();
        }

        public IDictionary<String, String> _strDic = new Dictionary<String, String>();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (null != _item)
            {
                // 已经有项目了，要提前显示这些项目。
                TextBoxName.Text = _item.UserName;
                if (_item.UserName.Equals("admin"))
                {
                    TextBoxName.IsReadOnly = true;
                    guanliquanxian.IsEnabled = false;
                }
                else
                {
                    guanliquanxian.IsEnabled = false;
                }
                TextBoxPassword.Text = _item.UserPassword;
                fengguang.IsChecked = _item.UIFaceOne;
                jiaotijin.IsChecked = _item.UIFaceTwo;
                ganhuaxue.IsChecked = _item.UIFaceThree;
                zhongjinshu.IsChecked = _item.UIFaceFour;
                checkBoxMicrobial.IsChecked = _item.UIFaceFive;
                guanliquanxian.IsChecked = _item.Create;
                //qiangzhi.IsChecked = _item.UpDateNowing;
                //昆山项目强制上传不可去勾选
                qiangzhi.IsChecked = true;
                qiangzhi.IsEnabled = false;
                cbyzkjdh.IsChecked = _item.CheckSampleID;
                if (_strDic.Count > 0)
                    _strDic.Remove(_item.UserName);
            }
            else
            {
                Lb_Title.Content = "新建用户";
            }
            skpZjs.Height = (Global.deviceHole.HmCount == 0) ? 0 : 41;
            GridJtj.Height = (Global.deviceHole.SxtCount == 0) ? 0 : 41;
            skpGhx.Height = (Global.deviceHole.SxtCount == 0) ? 0 : 41;
            skpWsw.Height = Global.IsEnableWswOrAtp ? 41 : 0;
            Lb_WswOrATP.Content = Global.IsWswOrAtp.Equals("WSW") ? "微生物模块" : "ATP模块";
            if (Global.InterfaceType.Equals("ZH"))
                yzkjdh.Visibility = Visibility.Visible;
        }

        public void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (CheckedUser())
            {
                // 将数据保存到文件
                try
                {
                    if (null == _item)
                    {// 这属于新添加
                        _item = new UserAccount();
                        Global.userAccounts.Add(_item);
                    }
                    _item.UserName = TextBoxName.Text;
                    _item.UserPassword = TextBoxPassword.Text;
                    _item.UIFaceOne = Convert.ToBoolean(fengguang.IsChecked);
                    _item.UIFaceTwo = Convert.ToBoolean(jiaotijin.IsChecked);
                    _item.UIFaceThree = Convert.ToBoolean(ganhuaxue.IsChecked);
                    _item.UIFaceFour = Convert.ToBoolean(zhongjinshu.IsChecked);
                    _item.UIFaceFive = Convert.ToBoolean(checkBoxMicrobial.IsChecked);
                    _item.Create = Convert.ToBoolean(guanliquanxian.IsChecked);
                    _item.UpDateNowing = Convert.ToBoolean(qiangzhi.IsChecked);
                    _item.CheckSampleID = Convert.ToBoolean(cbyzkjdh.IsChecked);
                    Global.SerializeToFile(Global.userAccounts, Global.userAccountsFile);
                    if (sender != null && e != null)
                        this.Close();
                }
                catch (Exception exception)
                {
                    MessageBox.Show("出错啦!" + exception.Message);
                }
            }
        }

        private bool CheckedUser()
        {
            String strName = TextBoxName.Text.Trim();

            if (_strDic.Count > 0 && _strDic.ContainsKey(strName))
            {
                MessageBox.Show("用户名【" + strName + "】已经存在,请重新输入!", "操作提示");
                return false;
            }

            if (!strName.Equals("260905"))
            {
                if (strName.Equals(string.Empty))
                {
                    TextBoxName.Clear();
                    TextBoxName.Focus();
                    MessageBox.Show("用户名不能为空!", "操作提示");
                    return false;
                }
                else if (TextBoxPassword.Text.Trim().Equals(string.Empty))
                {
                    TextBoxPassword.Clear();
                    TextBoxPassword.Focus();
                    MessageBox.Show("密码不能为空!", "操作提示");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("不能创建该用户!", "操作提示");
                return false;
            }
            return true;
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBoxName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            String str = TextBoxName.Text.Trim();
            if (str.Length > 12)
            {
                TextBoxName.Text = str.Remove(12);
                MessageBox.Show("用户名长度不能超过12个字符!", "操作提示");
                TextBoxName.Select(str.Length, 0);
            }
        }

        private void TextBoxPassword_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            String str = TextBoxPassword.Text.Trim();
            if (str.Length > 18)
            {
                TextBoxPassword.Text = str.Remove(18);
                MessageBox.Show("用户名长度不能超过18个字符!", "操作提示");
                TextBoxPassword.Select(str.Length, 0);
            }
        }

    }
}
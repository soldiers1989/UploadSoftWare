using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AIO
{
    /// <summary>
    /// UserCountWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UserAccountWindow : Window
    {
        private static string _UsernameLable = "UserLable01";
        private static string _PrassWord = "PrassWord01";
        private static string _ForceUpdata = "ForceUpdata01";
        public static UserAccount _UseNowAccount;
        public DataTable _TempDatable = new DataTable();
        private int _ReviseIndex = 0;
        //private List<UserAccount> UseNowAccountToDisplay = null;

        public UserAccountWindow()
        {
            InitializeComponent();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            UserAccount account = new UserAccount();
            account.Create = false;
            account.UserName = TextBoxUserName.Text;
            account.UserPassword = TextBoxUserPassword.Text;
            account.UIFaceOne = Fenguang.IsChecked.Value;
            account.UIFaceTwo = jiaotijin.IsChecked.Value;
            account.UIFaceThree = ganhuaxue.IsChecked.Value;
            account.UIFaceFour = zhongjinshu.IsChecked.Value;
            account.UpDateNowing = compulsion.IsChecked.Value;
            account.test = "001";
            if ("".Equals(account.UserName) || "".Equals(account.UserPassword))
            {
                MessageBox.Show("请输入账户名和密码");
            }
            else
            {
                Global.userAccounts.Add(account);
                //Global.userAccounts.RemoveRange(2,3);
                Global.SerializeToFile(Global.userAccounts, Global.userAccountsFile);
                MessageBox.Show("添加成功");
                this.Close();
            }
        }
        private void ButtonRevise_Click(object sender, RoutedEventArgs e)
        {
            UserAccount account = Global.userAccounts[_ReviseIndex];
            account.Create = CheckAdmin.IsChecked.Value;
            account.UserName = TextBoxUserName.Text;
            account.UserPassword = TextBoxUserPassword.Text;
            account.UIFaceOne = Fenguang.IsChecked.Value;
            account.UIFaceTwo = jiaotijin.IsChecked.Value;
            account.UIFaceThree = ganhuaxue.IsChecked.Value;
            account.UIFaceFour = zhongjinshu.IsChecked.Value;
            account.UpDateNowing = compulsion.IsChecked.Value;
            account.test = "001";
            if ("".Equals(account.UserName) || "".Equals(account.UserPassword))
            {
                MessageBox.Show("请输入账户名和密码");
            }
            else
            {
                Global.SerializeToFile(Global.userAccounts, Global.userAccountsFile);
                MessageBox.Show("修改成功");
            }
            Refurbish();
        }
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            UserAccount account = Global.userAccounts[_ReviseIndex];
            if (TextBoxUserName.Text != "admin" && account.Create != true)
            {
                Global.userAccounts.RemoveAt(_ReviseIndex);

                Global.SerializeToFile(Global.userAccounts, Global.userAccountsFile);
                MessageBox.Show("删除成功！");
            }
            else
            {
                MessageBox.Show("超级权限不能删除！");
            }
            //Refurbish();
        }
        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Refurbish()
        {
            //DataTable dt = new DataTable();
            //dt.Columns.Add("UserName");
            //dt.Columns.Add("UserPassword");
            //dt.Columns.Add("UIFaceOne");
            //dt.Columns.Add("UIFaceTwo");
            //dt.Columns.Add("UIFaceThree");
            //dt.Columns.Add("UIFaceFour");
            //dt.Columns.Add("UpDateNowing");
            //foreach (UserAccount Use in Global.userAccounts)
            //{
            //    DataRow dtrow1 = dt.NewRow();

            //    dtrow1["UserName"] = Use.UserName;
            //    dtrow1["UserPassword"] = Use.UserPassword;
            //    dtrow1["UIFaceOne"] = Use.UIFaceOne;
            //    dtrow1["UIFaceTwo"] = Use.UIFaceTwo;
            //    dtrow1["UIFaceThree"] = Use.UIFaceThree;
            //    dtrow1["UIFaceFour"] = Use.UIFaceFour;
            //    dtrow1["UpDateNowing"] = Use.UpDateNowing;
            //    dt.Rows.Add(dtrow1);
            //}
            //TempDatable = dt;

            //DataGridRecord.ItemsSource = TempDatable.DefaultView;

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("UserName");
            dt.Columns.Add("UserPassword");
            dt.Columns.Add("UIFaceOne");
            dt.Columns.Add("UIFaceTwo");
            dt.Columns.Add("UIFaceThree");
            dt.Columns.Add("UIFaceFour");
            dt.Columns.Add("UpDateNowing");
            foreach (UserAccount Use in Global.userAccounts)
            {
                DataRow dtrow1 = dt.NewRow();
                dtrow1["UserName"] = Use.UserName;
                dtrow1["UserPassword"] = Use.UserPassword;
                dtrow1["UIFaceOne"] = Use.UIFaceOne;
                dtrow1["UIFaceTwo"] = Use.UIFaceTwo;
                dtrow1["UIFaceThree"] = Use.UIFaceThree;
                dtrow1["UIFaceFour"] = Use.UIFaceFour;
                dtrow1["UpDateNowing"] = Use.UpDateNowing;
                dt.Rows.Add(dtrow1);
            }
            _TempDatable = dt;
            DataGridRecord.ItemsSource = dt.DefaultView;
            button1.IsEnabled = false;
        }

        private void DataGridRecord_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Ot = DataGridRecord.SelectedIndex;
            TextBoxUserName.Text = _TempDatable.Rows[Ot][0].ToString();
            TextBoxUserPassword.Text = _TempDatable.Rows[Ot][1].ToString();
            if (_TempDatable.Rows[Ot][0].ToString() == "admin")
            {
                CheckAdmin.IsEnabled = false;
            }
            Fenguang.IsChecked = Convert.ToBoolean(_TempDatable.Rows[Ot][2]);
            jiaotijin.IsChecked = Convert.ToBoolean(_TempDatable.Rows[Ot][3]);
            ganhuaxue.IsChecked = Convert.ToBoolean(_TempDatable.Rows[Ot][4]);
            zhongjinshu.IsChecked = Convert.ToBoolean(_TempDatable.Rows[Ot][5]);
            compulsion.IsChecked = Convert.ToBoolean(_TempDatable.Rows[Ot][6]);
            _ReviseIndex = Ot;
            button1.IsEnabled = true;
            ButtonAdd.IsEnabled = false;
        }

        private UIElement GenerateGetUserName(string channel, string sampleNum, bool sampleName)
        {
            //UIElement UIDisplay = GenerateGetUserName(UseNowAccount.UserName, UseNowAccount.UserPassword, UseNowAccount.UpDateNowing);
            //StackPanelResultdisplay.Children.Add(UIDisplay);
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            Label labelChannel = new Label();
            labelChannel.Content = channel;
            labelChannel.FontSize = 20;
            labelChannel.Width = 100;
            labelChannel.Name = _UsernameLable;
            Label labelSampleNum = new Label();
            labelSampleNum.Content = sampleNum;
            labelSampleNum.FontSize = 20;
            labelSampleNum.Width = 100;
            labelSampleNum.Name = _PrassWord;
            CheckBox labelSampleName = new CheckBox();
            labelSampleName.Content = sampleName;
            labelSampleName.FontSize = 20;
            labelSampleName.Width = 135;
            labelSampleName.Name = _ForceUpdata;
            //Label labelUnit = new Label();
            //labelUnit.Content = unit;
            //labelUnit.FontSize = 20;
            //labelUnit.Width = 100;
            stackPanel.Children.Add(labelChannel);
            stackPanel.Children.Add(labelSampleNum);
            stackPanel.Children.Add(labelSampleName);
            //stackPanel.Children.Add(labelUnit);
            return stackPanel;
        }

        private void DataGridRecord_MouseLeave(object sender, MouseEventArgs e)
        {
            //ReviseIndex = DataGridRecord.SelectedIndex;
            ButtonAdd.IsEnabled = true;
        }

    }
}

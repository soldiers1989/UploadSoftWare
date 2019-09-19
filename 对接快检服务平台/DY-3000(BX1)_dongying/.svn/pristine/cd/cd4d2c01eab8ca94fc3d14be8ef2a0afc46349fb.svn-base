using System.Windows;
using AIO.xaml.Dialog;
using AIO.xaml.KjService;
using AIO.xaml.Print;
using System;
using DYSeriesDataSet;
using System.Collections.Generic;

namespace AIO.xaml.Main
{
    /// <summary>
    /// DataManagementWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DataManagementWindow : Window
    {
        public DataManagementWindow()
        {
            InitializeComponent();
            this.Loaded += DataManagementWindow_Loaded;
        }

        private void DataManagementWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //如果接口类型为上传到广东智慧平台的话，则不显示任务更新模块
            wp_rwjs.Visibility = Global.InterfaceType.Equals("ZH") ? Visibility.Collapsed : Visibility.Visible;
            //是否启用知识库
            Bd_zsk.Visibility = Global.EnableKnowledgeBase ? Visibility.Visible : Visibility.Collapsed;
            //Lb_Tasks.Content = Global.InterfaceType.Equals("KJ") ? "抽样任务" : "任务查看";
            lb_Report.Content = Global.InterfaceType.Equals("KJ") ? "公告查看" : "报表查看";
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 样品管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonManagementSample_Click(object sender, RoutedEventArgs e)
        {
            ManagementSample window = new ManagementSample
            {
                //FoodCategoriesWindow window = new FoodCategoriesWindow();
                ShowInTaskbar = false,
                Owner = this
            };
            window.Show();
        }

        /// <summary>
        /// 被检单位管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonManagementCompany_Click(object sender, RoutedEventArgs e)
        {
            ManagementCompany window = new ManagementCompany
            {
                ShowInTaskbar = false,
                Owner = this
            };
            window.Show();
        }

        /// <summary>
        /// 检测记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonRecord_Click(object sender, RoutedEventArgs e)
        {
            RecordWindow window = new RecordWindow();
            window.ComboBoxUser.Text = LoginWindow._userAccount.UserName;
            window.ShowInTaskbar = false;
            window.Owner = this;
            window.Show();
        }

        /// <summary>
        /// 任务查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonTask_Click(object sender, RoutedEventArgs e)
        {
            if (Global.InterfaceType.Equals("KJ"))
            {
                ReceiveTasks window = new ReceiveTasks
                {
                    ShowInTaskbar = false,
                    Owner = this
                };
                window.Show();
            }
            else
            {
                TaskDisplay window = new TaskDisplay
                {
                    ShowInTaskbar = false,
                    Owner = this
                };
                window.Show();
            }
        }

        private void btnReportSearch_Click(object sender, RoutedEventArgs e)
        {
            if (Global.InterfaceType.Equals("KJ"))
            {
                TaskMsg window = new TaskMsg
                {
                    ShowInTaskbar = false,
                    Owner = this
                };
                window.ShowDialog();
            }
            else
            {
                ReportWindow window = new ReportWindow
                {
                    ShowInTaskbar = false,
                    Owner = this
                };
                window.ShowDialog();
            }
        }

        /// <summary>
        /// 知识库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_KnowledgeBaseWindow_Click(object sender, RoutedEventArgs e)
        {
            KnowledgeBaseWindow window = new KnowledgeBaseWindow
            {
                ShowInTaskbar = false,
                Owner = this
            };
            window.ShowDialog();
        }
        /// <summary>
        /// 样品下载管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_SampleWindow_Click(object sender, RoutedEventArgs e)
        {

            SampleTypeWindow window = new SampleTypeWindow();
            window.ShowDialog();
        }
        /// <summary>
        /// 检测项目下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_itemWindow_Click(object sender, RoutedEventArgs e)
        {
           

            ItemWindow window = new ItemWindow();
            window.ShowDialog();
        }

        private void btnReSelf_Click(object sender, RoutedEventArgs e)
        {
            btnReSelf.IsEnabled = false;
            tlsttResultSecondOpr bll = new tlsttResultSecondOpr();
            List<string> sqlList = new List<string>();
            string sql = string.Empty, err = string.Empty;
            int rtn = 0;
            //创建检测项目表
            sql = "create table DetectItem(ID integer identity(1,1) PRIMARY KEY)";
            rtn = bll.DataBaseRepair(sql, out err);
            sqlList.Add("alter table DetectItem add cid varchar(255)");
            sqlList.Add("alter table DetectItem add detect_item_name varchar(255)");
            sqlList.Add("alter table DetectItem add detect_item_typeid varchar(255)");
            sqlList.Add("alter table DetectItem add standard_id varchar(255)");
            sqlList.Add("alter table DetectItem add detect_sign varchar(255)");
            sqlList.Add("alter table DetectItem add detect_value varchar(255)");
            sqlList.Add("alter table DetectItem add detect_value_unit varchar(255)");
            sqlList.Add("alter table DetectItem add checked varchar(255)");
            sqlList.Add("alter table DetectItem add cimonitor_level varchar(255)");
            sqlList.Add("alter table DetectItem add remark varchar(255)");
            sqlList.Add("alter table DetectItem add delete_flag varchar(255)");
            sqlList.Add("alter table DetectItem add create_by varchar(255)");
            sqlList.Add("alter table DetectItem add create_date varchar(255)");
            sqlList.Add("alter table DetectItem add update_by varchar(255)");
            sqlList.Add("alter table DetectItem add update_date varchar(255)");
            sqlList.Add("alter table DetectItem add t_id varchar(255)");
            sqlList.Add("alter table DetectItem add t_item_name varchar(255)");
            sqlList.Add("alter table DetectItem add t_sorting varchar(255)");
            sqlList.Add("alter table DetectItem add t_remark varchar(255)");
            sqlList.Add("alter table DetectItem add t_delete_flag varchar(255)");
            sqlList.Add("alter table DetectItem add t_create_by varchar(255)");
            sqlList.Add("alter table DetectItem add t_create_date varchar(255)");
            sqlList.Add("alter table DetectItem add t_update_by varchar(255)");
            sqlList.Add("alter table DetectItem add t_update_date varchar(255)");

            //create KJ_Sample 添加表
            sql = "create table foodlist(ID integer identity(1,1) PRIMARY KEY)";
            rtn = bll.DataBaseRepair(sql, out err);
            sqlList.Add("alter table foodlist add fid varchar(255)");
            sqlList.Add("alter table foodlist add food_name varchar(255)");
            sqlList.Add("alter table foodlist add food_name_en varchar(255)");
            sqlList.Add("alter table foodlist add food_name_other varchar(255)");
            sqlList.Add("alter table foodlist add parent_id varchar(255)");
            sqlList.Add("alter table foodlist add cimonitor_level varchar(255)");
            sqlList.Add("alter table foodlist add sorting varchar(255)");
            sqlList.Add("alter table foodlist add checked varchar(255)");
            sqlList.Add("alter table foodlist add delete_flag varchar(255)");
            sqlList.Add("alter table foodlist add create_by varchar(255)");
            sqlList.Add("alter table foodlist add create_date varchar(255)");
            sqlList.Add("alter table foodlist add update_by varchar(255)");
            sqlList.Add("alter table foodlist add update_date varchar(255)");
            sqlList.Add("alter table foodlist add isFood varchar(255)");

            //时间表
            sql = "";
            sql = "create table RequestTime(ID integer identity(1,1) PRIMARY KEY)";
            rtn = bll.DataBaseRepair(sql, out err);
            sqlList.Add("alter table RequestTime add RequestName varchar(255)");
            sqlList.Add("alter table RequestTime add UpdateTime varchar(255)");

            rtn = bll.DataBaseRepair(sql, out err);

            if (sqlList != null && sqlList.Count > 0)
            {
                for (int i = 0; i < sqlList.Count; i++)
                {
                    bll.DataBaseRepair(sqlList[i], out err);
                }
            }
            MessageBox.Show("自检成功","系统提示");
            btnReSelf.IsEnabled = true ;
        }

    }
}
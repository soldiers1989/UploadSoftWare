using System.Windows;
using AIO.xaml.Dialog;
using AIO.xaml.KjService;
using AIO.xaml.Print;

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
            window.ShowDialog();
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
            window.ShowDialog();
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
            window.ShowDialog();
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
                window.ShowDialog();
            }
            else
            {
                TaskDisplay window = new TaskDisplay
                {
                    ShowInTaskbar = false,
                    Owner = this
                };
                window.ShowDialog();
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

    }
}
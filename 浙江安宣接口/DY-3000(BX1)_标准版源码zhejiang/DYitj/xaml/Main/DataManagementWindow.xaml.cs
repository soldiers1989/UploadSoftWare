using System.Windows;
using AIO.xaml.Dialog;
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
            if (Global.InterfaceType.Equals("DY") || Global.InterfaceType.Equals("GS"))
            {
                Bd_jcrw.Visibility = Visibility.Visible;
                lb_jcrw1.Visibility = lb_jcrw2.Visibility = Visibility.Visible;
            }
            else
            {
                Bd_jcrw.Visibility = Visibility.Collapsed;
                lb_jcrw1.Visibility = lb_jcrw2.Visibility = Visibility.Collapsed;
            }

            if (Global.EnableKnowledgeBase)
            {
                Bd_zsk.Visibility = Visibility.Visible;
                lb_zsk1.Visibility = Visibility.Visible;
            }
            else
            {
                Bd_zsk.Visibility = Visibility.Collapsed;
                lb_zsk1.Visibility = Visibility.Collapsed;
            }
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
            ManagementSample window = new ManagementSample();
            //FoodCategoriesWindow window = new FoodCategoriesWindow();
            window.ShowInTaskbar = false;
            window.Owner = this;
            window.Show();
        }

        /// <summary>
        /// 被检单位管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonManagementCompany_Click(object sender, RoutedEventArgs e)
        {
            ManagementCompany window = new ManagementCompany();
            window.ShowInTaskbar = false;
            window.Owner = this;
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
            TaskDisplay window = new TaskDisplay();
            window.ShowInTaskbar = false;
            window.Owner = this;
            window.Show();
        }

        private void btnReportSearch_Click(object sender, RoutedEventArgs e)
        {
            ReportWindow window = new ReportWindow();
            window.ShowInTaskbar = false;
            window.Owner = this;
            window.ShowDialog();
        }

        /// <summary>
        /// 知识库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_KnowledgeBaseWindow_Click(object sender, RoutedEventArgs e)
        {
            KnowledgeBaseWindow window = new KnowledgeBaseWindow();
            window.ShowInTaskbar = false;
            window.Owner = this;
            window.ShowDialog();
        }

    }
}
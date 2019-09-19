using System.Windows;
using AIO.xaml.Dialog;

namespace AIO.xaml.Main
{
    /// <summary>
    /// KnowledgeBaseWindow.xaml 的交互逻辑
    /// </summary>
    public partial class KnowledgeBaseWindow : Window
    {
        public KnowledgeBaseWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lb_gps1.Visibility = lb_gps2.Visibility = gps.Visibility = Global.EnableGPS ? Visibility.Visible : Visibility.Collapsed;
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 食品标准
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_StandardRepository_Click(object sender, RoutedEventArgs e)
        {
            StandardRepositoryWindow window = new StandardRepositoryWindow();
            window.ShowInTaskbar = false;
            window.Owner = this;
            window.Show();
        }

        /// <summary>
        /// 法律法规
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_LawsAndRegulations_Click(object sender, RoutedEventArgs e)
        {
            LawsAndRegulationsWindow window = new LawsAndRegulationsWindow();
            window.ShowInTaskbar = false;
            window.Owner = this;
            window.Show();
        }

        /// <summary>
        /// 培训模块
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTrainingModule_Click(object sender, RoutedEventArgs e)
        {
            TrainingModuleWindow window = new TrainingModuleWindow();
            window.ShowInTaskbar = false;
            window.Owner = this;
            window.Show();
        }

        /// <summary>
        /// 国家局数据库查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_gjjsjcx_Click(object sender, RoutedEventArgs e)
        {
            CountryDataSearch window = new CountryDataSearch();
            window.ShowInTaskbar = false;
            window.Owner = this;
            window.Show();
        }

        /// <summary>
        /// 地图定位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Gps_Click(object sender, RoutedEventArgs e)
        {
            TraceTestWindow window = new TraceTestWindow();
            window.title = "gps";
            window.ShowDialog();

            GpsForm gps = new GpsForm();
            gps.ShowDialog();
        }

    }
}
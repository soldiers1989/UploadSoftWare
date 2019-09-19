using System.Windows;
using AIO.xaml.TrainingModule;

namespace AIO.xaml.Main
{
    /// <summary>
    /// TrainingModuleWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TrainingModuleWindow : Window
    {
        public TrainingModuleWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 抽样培训
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSampling_Click(object sender, RoutedEventArgs e)
        {
            //SamplingWindow window = new SamplingWindow();
            //window.ShowInTaskbar = false;
            //window.Owner = this;
            //window.Show();
            SecurityWindow window = new SecurityWindow
            {
                type = "抽样操作",
                ShowInTaskbar = false,
                Owner = this
            };
            window.Show();
        }

        /// <summary>
        /// 实验安全
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSecurity_Click(object sender, RoutedEventArgs e)
        {
            SecurityWindow window = new SecurityWindow
            {
                type = "实验安全",
                ShowInTaskbar = false,
                Owner = this
            };
            window.Show();
        }

        /// <summary>
        /// 仪器操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnInstrumentOperation_Click(object sender, RoutedEventArgs e)
        {
            //InstrumentOperationWindow window = new InstrumentOperationWindow();
            //window.ShowInTaskbar = false;
            //window.Owner = this;
            //window.Show();
            SecurityWindow window = new SecurityWindow
            {
                type = "仪器操作",
                ShowInTaskbar = false,
                Owner = this
            };
            window.Show();
        }

        /// <summary>
        /// 试剂操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReagentOperation_Click(object sender, RoutedEventArgs e)
        {
            //ReagentOperationWindow window = new ReagentOperationWindow();
            //window.ShowInTaskbar = false;
            //window.Owner = this;
            //window.Show();
            SecurityWindow window = new SecurityWindow
            {
                type = "试剂操作",
                ShowInTaskbar = false,
                Owner = this
            };
            window.Show();
        }

        /// <summary>
        /// 食安法培训
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFoodSecurityAct_Click(object sender, RoutedEventArgs e)
        {
            //FoodSecurityActWindow window = new FoodSecurityActWindow();
            //window.ShowInTaskbar = false;
            //window.Owner = this;
            //window.Show();
            SecurityWindow window = new SecurityWindow
            {
                type = "食安法",
                ShowInTaskbar = false,
                Owner = this
            };
            window.Show();
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnReagentOperation1_Click(object sender, RoutedEventArgs e)
        {
            SecurityWindow window = new SecurityWindow
            {
                type = "政策解读",
                ShowInTaskbar = false,
                Owner = this
            };
            window.Show();
        }

        private void BtnInstrumentOperation1_Click(object sender, RoutedEventArgs e)
        {
            SecurityWindow window = new SecurityWindow
            {
                type = "基层执法",
                ShowInTaskbar = false,
                Owner = this
            };
            window.Show();
        }

    }
}
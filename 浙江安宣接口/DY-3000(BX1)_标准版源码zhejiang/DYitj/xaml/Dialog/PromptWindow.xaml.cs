using System;
using System.Windows;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// PromptWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PromptWindow : Window
    {
        public PromptWindow()
        {
            InitializeComponent();
        }

        public String _HintStr = "";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Topmost = true;
            this.ShowInTaskbar = false;
            TextBoxHintStr.Text = _HintStr;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Global.IsOpenPrompt = false;
        }

    }
}
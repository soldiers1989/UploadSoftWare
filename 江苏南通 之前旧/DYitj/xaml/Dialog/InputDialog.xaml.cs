using System.Windows;

namespace AIO
{
    /// <summary>
    /// InputDialog.xaml 的交互逻辑
    /// </summary>
    public partial class InputDialog : Window
    {
        private bool _result = false;

        public InputDialog(string hint)
        {
            InitializeComponent();
            LabelTitle.Content = hint;
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            _result = true;
            this.Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            _result = false;
            this.Close();
        }

        public string GetInput()
        {
            return TextBoxInput.Text;
        }

        public bool GetResult()
        {
            return _result;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TextBoxInput.Text = string.Empty;
            TextBoxInput.Focus();
        }

        private void TextBoxInput_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                _result = true;
                this.Close();
            }
        }
    }
}

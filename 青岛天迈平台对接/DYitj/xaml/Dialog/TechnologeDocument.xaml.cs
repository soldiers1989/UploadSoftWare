using System;
using System.IO;
using System.Windows;
using System.Windows.Documents;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// TechnologeDocument.xaml 的交互逻辑
    /// </summary>
    public partial class TechnologeDocument : Window
    {
        public TechnologeDocument()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 文件路径 存储检测项目名称，传递过来后组成完整的路径
        /// </summary>
        public string path = string.Empty;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (FileStream stream = File.OpenRead(path))
            {
                TextRange documentTextRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                string dataFormat = DataFormats.Text;
                string ext = System.IO.Path.GetExtension(path);
                if (String.Compare(ext, ".xaml", true) == 0)
                {
                    dataFormat = DataFormats.Xaml;
                }
                else if (String.Compare(ext, ".rtf", true) == 0)
                {
                    dataFormat = DataFormats.Rtf;
                }
                documentTextRange.Load(stream, dataFormat);
            }
        }

    }
}
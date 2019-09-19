using System.IO;
using System.Windows;
using ClassLibrary.com.lvrenyang;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// ShowViewerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ShowViewerWindow : Window
    {
        public ShowViewerWindow()
        {
            InitializeComponent();
        }

        public string filename = @"F:\Desktop File\正在处理\2017年6月\安徽投标项目\检测项目操作说明\操作流程说明\吊白块.doc";
        public static bool officeFileOpen_Status = false;
        System.Windows.Xps.Packaging.XpsDocument xpsDoc;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string xpsFilePath = System.Environment.CurrentDirectory + "\\Data\\xpsData.xps";
            var convertResults = OfficeToXps.ConvertToXps(filename, ref xpsFilePath);
            switch (convertResults.Result)
            {
                case ConversionResult.OK:
                    xpsDoc = new System.Windows.Xps.Packaging.XpsDocument(xpsFilePath, FileAccess.ReadWrite);
                    docViewer.Document = xpsDoc.GetFixedDocumentSequence();
                    officeFileOpen_Status = true;
                    break;

                case ConversionResult.InvalidFilePath:
                    // Handle bad file path or file missing
                    break;
                case ConversionResult.UnexpectedError:
                    // This should only happen if the code is modified poorly
                    break;
                case ConversionResult.ErrorUnableToInitializeOfficeApp:
                    // Handle Office 2007 (Word | Excel | PowerPoint) not installed
                    break;
                case ConversionResult.ErrorUnableToOpenOfficeFile:
                    // Handle source file being locked or invalid permissions
                    break;
                case ConversionResult.ErrorUnableToAccessOfficeInterop:
                    // Handle Office 2007 (Word | Excel | PowerPoint) not installed
                    break;
                case ConversionResult.ErrorUnableToExportToXps:
                    // Handle Microsoft Save As PDF or XPS Add-In missing for 2007
                    break;
            }
        }
    }
}

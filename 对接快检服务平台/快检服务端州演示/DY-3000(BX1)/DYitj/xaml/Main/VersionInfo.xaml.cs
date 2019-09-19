using System;
using System.Windows;
using System.Xml;
using AIO.src;

namespace AIO.xaml.Main
{
    /// <summary>
    /// VersionInfo.xaml 的交互逻辑
    /// </summary>
    public partial class VersionInfo : Window
    {
        public VersionInfo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 本地配置文件信息
        /// </summary>
        private XmlFiles updaterXmlFiles = null;
        private string Line = "\r\n";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //读取升级配置文件查看当前软件版本信息
            try
            {
                string localXmlFile = Environment.CurrentDirectory + "\\UpdateList.xml";
                //从本地读取更新配置文件信息
                updaterXmlFiles = new XmlFiles(localXmlFile);
                if (updaterXmlFiles != null)
                {
                    string val = string.Empty;

                    //获取版本日志记录
                    XmlNodeList newNodeList = updaterXmlFiles.GetNodeList("AutoUpdater/UpdateNotes");
                    if (newNodeList.Count > 0)
                    {
                        for (int i = 0; i < newNodeList.Count; i++)
                        {
                            val = newNodeList.Item(i).Attributes["Note"].Value.Trim();
                            if (val.Trim().Length == 0) continue;
                            Txt_SoftwareInfo.AppendText(val + Line);
                        }
                    }

                    //获取版本发布日期
                    val = updaterXmlFiles.GetNodeValue("//LastUpdateTime");
                    Txt_SoftwareInfo.AppendText(string.Format("版本发布日期：{0}", val));
                }
            }
            catch (Exception)
            {
                Txt_SoftwareInfo.AppendText("配置文件损坏，无法获取软件版本信息！");
            }
        }

        private void Btn_Closed_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
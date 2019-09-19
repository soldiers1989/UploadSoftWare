using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Net;
using System.Xml;

namespace AutoUpdate
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public partial class FrmMain : System.Windows.Forms.Form
	{
        private static string serverIP = string.Empty;
        private static string serverID = string.Empty;
        private static string serverPwd = string.Empty;


        public FrmMain()
        {
            InitializeComponent();
        }
	
		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>

        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                MessageBox.Show("必须从系统中运行升级程序！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            else
            {
                serverIP = args[0];
                serverID = args[1];
                if (args[2].ToString() == "''")
                {
                    serverPwd = "";
                }
                else
                {
                    serverPwd = args[2].ToString();
                }
            }
            Application.Run(new FrmMain());

        }


        /// <summary>
        /// 点击升级按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonStart_Click(object sender, System.EventArgs e)
        {
            ProgressBar.Value = 0;
            LabelTips.Text = "查找升级服务器...";
            string text1 = Application.StartupPath;
            if (text1.Substring(text1.Length - 1, 1) != "\\")
            {
                text1 = text1 + "\\";
            }
            string strAdoCon = string.Format("Provider=SQLOLEDB;Initial Catalog=FoodSafeSystem;Password={0};User ID={1};Data Source={2};Persist Security Info=True;", serverPwd, serverID, serverIP);
            string strSQL = "Select * From tFss_AutoUpdate";
            OleDbConnection AdoConn = null;
            OleDbCommand AdoCmd = null;
            OleDbDataAdapter AdoAdapter;
            DataTable dt = null;
            int prosval = 0;
            try
            {
                AdoConn = new OleDbConnection(strAdoCon);
                AdoConn.Open();
                AdoCmd = new OleDbCommand(strSQL, AdoConn);
                AdoAdapter = new OleDbDataAdapter();
                AdoAdapter.SelectCommand = AdoCmd;
                AdoAdapter.SelectCommand.Connection = AdoConn;
                dt = new DataTable();
                AdoAdapter.Fill(dt);
                AdoAdapter.Dispose();
                AdoConn.Close();
            }
            catch
            {
                MessageBox.Show("找不到升级服务器", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            ProgressBar.Value = 25;
            prosval = 70 / dt.Rows.Count;
            LabelTips.Text = "读取升级文件列表...";
            System.Xml.XmlDocument xmlDoc = new XmlDocument();
            string strHttpPath, strAppPath, strVer, strMemo;
            try
            {
                xmlDoc.Load(text1 + "FilePatchList.xml");
            }
            catch
            {
                xmlDoc.LoadXml("<FilePatchList PatchTime=\"" + System.DateTime.Now.ToString() + "\"></FilePatchList>");
            }

            LabelTips.Text = "开始升级...";
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                strVer = dt.Rows[i].ItemArray[1].ToString();
                strHttpPath = dt.Rows[i].ItemArray[2].ToString();
                strAppPath = dt.Rows[i].ItemArray[3].ToString();
                strMemo = dt.Rows[i].ItemArray[4].ToString();
                Application.DoEvents();
                //判断是否要更新文件
                if (CanPath(xmlDoc, strAppPath, strHttpPath, strVer))
                {
                    LabelTips.Text = "开始升级：" + strMemo;
                    if (DownLoad(strHttpPath, strAppPath, strMemo))
                    {
                        LabelTips.Text = "升级" + strMemo + "文件成功！";
                    }
                    else
                    {
                        LabelTips.Text = "升级" + strMemo + "文件失败！";
                    }
                }
                ProgressBar.Value = ProgressBar.Value + prosval;
            }
            dt.Clear();
            xmlDoc.Save(Application.StartupPath + "\\FilePatchList.xml");
            LabelTips.Text = "升级信息请察看文件FilePatchList.xml";
            ProgressBar.Value = 100;
        }

        private bool DownLoad(string strHttpPath, string strAppPath, string strMemo)
        {
            WebRequest HttpReq;
            FileStream AppFile = null;
            try
            {
                HttpReq = WebRequest.Create(strHttpPath);
                HttpReq.Method = "GET";
                WebResponse HttpResPonse = HttpReq.GetResponse();
                try
                {

                    if (strAppPath.StartsWith("\\"))
                    {
                        AppFile = new FileStream(Application.StartupPath + strAppPath, FileMode.Create);
                    }
                    else
                    {
                        try
                        {
                            AppFile = new FileStream(strAppPath, FileMode.Create);
                        }
                        catch
                        {
                            int iLastIndex;
                            string strDir;
                            iLastIndex = strAppPath.LastIndexOf("\\");
                            strDir = strAppPath.Substring(0, iLastIndex);
                            System.IO.Directory.CreateDirectory(strDir);
                            AppFile = new FileStream(strAppPath, FileMode.Create);

                        }

                    }
                    CopyData(HttpResPonse.GetResponseStream(), AppFile);
                }
                catch
                {
                    return false;
                }
                finally
                {
                    if (HttpResPonse != null)
                    {
                        HttpResPonse.GetResponseStream().Close();
                    }
                    if (AppFile != null)
                    {
                        AppFile.Close();
                    }
                }
            }
            catch
            {
                return false;

            }
            return true;
        }
 

		private void CopyData(Stream FromStream, Stream ToStream)
		{
    		int intBytesRead;
			const int intSize = 4096;
			byte[]  bytes=new byte[intSize];
			intBytesRead = FromStream.Read(bytes, 0, intSize);
			while(intBytesRead > 0)
			{
				ToStream.Write(bytes, 0, intBytesRead);
				intBytesRead = FromStream.Read(bytes, 0, intSize);
			}
		}

		private bool CanPath(System.Xml.XmlDocument xmlDoc,String strFileName, String strHttp, String strVer) 
		{
			XmlElement xmlRootElement;
			XmlElement xmlNewElement;
			int  iLoop=0;
			string  strOldFileName="";
			string strOldVer="";
			xmlRootElement = xmlDoc.DocumentElement;
			for(iLoop = 0;iLoop<= xmlRootElement.ChildNodes.Count - 1;iLoop++)
			{
				if( xmlRootElement.ChildNodes[iLoop].Name == "File")
				{
					try
					{
						strOldFileName = xmlRootElement.ChildNodes[iLoop].Attributes["File"].Value;
						strOldVer = xmlRootElement.ChildNodes[iLoop].Attributes["Ver"].Value;
					}
					catch
					{
						strOldFileName = "";
						strOldVer = "";
					}
				}
				if(strOldFileName == strFileName)
				{
					if(strOldVer.Equals(strVer))
					{
		                xmlRootElement.ChildNodes[iLoop].RemoveAll();
						break ;
					}
					else
					{
						return false;
					}
				}
			}
			xmlNewElement = xmlDoc.CreateElement("File");
			xmlNewElement.SetAttribute("File", strFileName);
			xmlNewElement.SetAttribute("Http", strHttp);
			xmlNewElement.SetAttribute("Ver", strVer);
			xmlNewElement.SetAttribute("PatchTime", System.DateTime.Now.ToString());
			xmlRootElement.AppendChild(xmlNewElement);
			return true;
		}

		private void ButtonExit_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}

	}
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace AutoUpdate_CreateXml
{
    public partial class CreateXml : Form
    {
        public CreateXml()
        {
            InitializeComponent();
        }

        private string path = Application.StartupPath;

        private void CreateXml_Load(object sender, EventArgs e)
        {
            List<string> cbList = new List<string>();
            cbList.Add("请选择");
            Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
            if (cfa != null && cfa.AppSettings.Settings.Count > 0)
            {
                for (int i = 0; i < cfa.AppSettings.Settings.Count; i++)
                {
                    cbList.Add(cfa.AppSettings.Settings.AllKeys[i].ToString());
                }
                comboBox1.DataSource = cbList;
            }
            comboBox1.SelectedIndex = 0;
            loadXml();
        }

        List<Model> modelList = new List<Model>();

        private void loadXml()
        {
            try
            {
                list = new List<string>();
                list = getPath(path);
                modelList = new List<Model>();
                Model model = null;
                string fileName = string.Empty;
                for (int i = 0; i < list.Count; i++)
                {
                    fileName = list[i].Remove(0, path.Length + 1);
                    string[] names = fileName.Split('.');
                    if (names[names.Length - 1].Equals("pdb") ||
                        names[names.Length - 1].Equals("bak") ||
                        fileName.Equals("web.config") ||
                        fileName.Equals("AutoUpdate_CreateXml.exe") ||
                        fileName.Equals("AutoUpdate_CreateXml.exe.config") ||
                        fileName.IndexOf("vshost.exe") > 0)
                        continue;

                    model = new Model();
                    model.IsUpdate = names[names.Length - 1].Equals("Mdb") ||
                        names[names.Length - 1].Equals("dat") ||
                        fileName.Equals("DY-Detector.exe.config") ? false : true;
                    model.Name = fileName;
                    modelList.Add(model);
                }

                dataGridView1.DataSource = modelList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("出现异常！\r\n\r\n异常信息：" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text.Trim().Equals("请选择"))
                {
                    MessageBox.Show("请选择仪器型号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    comboBox1.Focus();
                    return;
                }
                else if (textBox1.Text.Trim().Equals("80"))
                {
                    MessageBox.Show("请输入端口号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Focus();
                    return;
                }
                else if (textBox2.Text.Trim().Equals("1.0.0."))
                {
                    MessageBox.Show("请输入版本号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox2.Focus();
                    return;
                }
                tb_xml.Text = string.Empty;
                tb_xml.AppendText("<?xml version=\"1.0\" encoding=\"gb2312\"?>\r\n");
                tb_xml.AppendText("<AutoUpdater>\r\n");
                tb_xml.AppendText("  <description>" + comboBox1.Text.Trim() + " autoUpdate</description>\r\n");
                tb_xml.AppendText("  <Updater>\r\n");
                tb_xml.AppendText("    <Url>http://rj.chinafst.cn:" + textBox1.Text.Trim() + "/</Url>\r\n");
                tb_xml.AppendText("    <LastUpdateTime>" + DateTime.Now.ToString("yyyy-MM-dd") + "</LastUpdateTime>\r\n");
                tb_xml.AppendText("  </Updater>\r\n");
                tb_xml.AppendText("  <Application applicationId=\"DY-Detector\">\r\n");
                tb_xml.AppendText("    <EntryPoint>DY-Detector.exe</EntryPoint>\r\n");
                tb_xml.AppendText("    <Location>.</Location>\r\n");
                tb_xml.AppendText("    <Version>" + textBox2.Text.Trim() + "</Version>\r\n");
                tb_xml.AppendText("  </Application>\r\n");
                tb_xml.AppendText("  <Files>\r\n");

                if (modelList != null && modelList.Count > 0)
                {
                    for (int i = 0; i < modelList.Count; i++)
                    {
                        string ver = textBox2.Text.Trim();
                        ver = modelList[i].IsUpdate ? ver : "1.0.0.0";
                        tb_xml.AppendText("    <File Ver=\"" + ver + "\" Name=\"" + modelList[i].Name + "\" />\r\n");
                    }
                }
                else
                {
                    List<string> paths = getPath(path);
                    if (paths != null)
                    {
                        for (int i = 0; i < paths.Count; i++)
                        {
                            string fileName = paths[i];
                            fileName = fileName.Remove(0, path.Length + 1);
                            tb_xml.AppendText("    <File Ver=\"" + textBox2.Text.Trim() + "\" Name=\"" + fileName + "\" />\r\n");
                        }
                    }
                }

                tb_xml.AppendText("  </Files>\r\n");
                tb_xml.AppendText("  <UpdateNotes>\r\n");
                tb_xml.AppendText("    <UpdateNote Note=\"" + comboBox1.Text.Trim() + "食品综合分析仪_Ver X.X.X\"/>\r\n");
                tb_xml.AppendText("    <UpdateNote Note=\"版本更新说明：\"/>\r\n");
                tb_xml.AppendText("    <UpdateNote Note=\"  \"/>\r\n");
                tb_xml.AppendText("    <UpdateNote Note=\"- 1.XXXXXXXX。\"/>\r\n");
                tb_xml.AppendText("    <UpdateNote Note=\"- 2.XXXXXXXX。\"/>\r\n");
                tb_xml.AppendText("    <UpdateNote Note=\"  \"/>\r\n");
                tb_xml.AppendText("    <UpdateNote Note=\"  \"/>\r\n");
                tb_xml.AppendText("    <UpdateNote Note=\"新版本更新预告：\"/>\r\n");
                tb_xml.AppendText("    <UpdateNote Note=\"- 1.XXXXXXXX。\"/>\r\n");
                tb_xml.AppendText("    <UpdateNote Note=\"- 2.XXXXXXXX。\"/>\r\n");
                tb_xml.AppendText("    <UpdateNote Note=\"  \"/>\r\n");
                tb_xml.AppendText("    <UpdateNote Note=\"  \"/>\r\n");
                tb_xml.AppendText("    <UpdateNote Note=\"  \"/>\r\n");
                tb_xml.AppendText("    <UpdateNote Note=\"备注：新版本更新预告内容仅供参考。\"/>\r\n");
                tb_xml.AppendText("  </UpdateNotes>\r\n");
                tb_xml.AppendText("</AutoUpdater>\r\n");
                tabControl1.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("出现异常！\r\n\r\n异常信息：" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private class Model
        {
            private bool _isUpdate;

            public bool IsUpdate
            {
                get { return _isUpdate; }
                set { _isUpdate = value; }
            }
            private string _name;

            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }
        }

        private static List<string> list = new List<string>();
        public static List<string> getPath(string path)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                FileInfo[] fil = dir.GetFiles();
                DirectoryInfo[] dii = dir.GetDirectories();
                foreach (FileInfo f in fil)
                {
                    list.Add(f.FullName);//添加文件的路径到列表
                }

                //获取子文件夹内的文件列表，递归遍历
                foreach (DirectoryInfo d in dii)
                {
                    getPath(d.FullName);
                    //list.Add(d.FullName);//添加文件夹的路径到列表
                }
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void tb_path_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                //tb_path.Text = open.FileName;
            }

            string path = @"C:\A\B\C\D\E\1.txt ";
            DirectoryInfo info = new DirectoryInfo(path);
            string ePath = info.Parent.FullName;//E文件夹路径 C:\A\B\C\D\E
            string dPath = info.Parent.Parent.FullName;//D文件夹路径 C:\A\B\C\D
            string cPath = info.Parent.Parent.Parent.FullName;//C文件夹路径 C:\A\B\C
            string rootpath = info.Root.FullName;//根目录; C:\

            Console.WriteLine(ePath);
            Console.WriteLine(dPath);
            Console.WriteLine(cPath);
            Console.WriteLine(rootpath);
            Console.Read();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tb_xml.Text.Trim().Length == 0)
            {
                MessageBox.Show("请先生成XML格式文档！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(tb_xml.Text.Trim());
                xmlDoc.Save(path + "\\UpdateList.xml");
                MessageBox.Show("生成XML文件成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception)
            {
                MessageBox.Show("自动生成XML文件失败，请手动填写！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value = ConfigurationManager.AppSettings[comboBox1.Text.Trim()];
            if (value != null)
            {
                textBox1.Text = value;
            }
            else
            {
                textBox1.Text = "80";
            }
        }

    }
}
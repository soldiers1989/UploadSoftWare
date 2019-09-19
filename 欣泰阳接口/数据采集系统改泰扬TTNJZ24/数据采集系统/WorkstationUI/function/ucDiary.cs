using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using WorkstationBLL.Mode;

namespace WorkstationUI.function
{
    public partial class ucDiary : UserControl
    {
        private static readonly string dataName = "Diary\\Diary.txt";
        private static readonly string PathString = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, dataName);
        public ucDiary()
        {
            InitializeComponent();
        }
        private  clsSetSqlData sql = new clsSetSqlData();
        private bool isCreateTable = false;
        private DataTable displaytable = null;
        private void ucDiary_Load(object sender, EventArgs e)
        {
            initable();

            string err = string.Empty;
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("order by ID");
                DataTable dt= sql.GetDiary(sb.ToString(),out err);
                if (dt != null)
                {
                    for(int i=0;i<dt.Rows.Count;i++)
                    {
                        AddItemToTable(dt.Rows[i][0].ToString(),dt.Rows[i][1].ToString(),dt.Rows[i][2].ToString());
                    }
                }
                CheckDatas.DataSource = displaytable;
                CheckDatas.Columns[0].Width = 200;
                CheckDatas.Columns[1].Width = 400;
               // CheckDatas.Columns[0].Width = 200;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
            }
        }
        /// <summary>
        /// 建表
        /// </summary>
        private void initable()
        {
            if (isCreateTable == false)
            {
                displaytable = new DataTable("diary");
                DataColumn dataCol;

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "日期时间";
                displaytable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "详情";
                displaytable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "备注";
                displaytable.Columns.Add(dataCol);
            }
            isCreateTable = true;
        }
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="macf"></param>
        /// <param name="communic"></param>
        private void AddItemToTable(string t, string d, string r)
        {
            DataRow dr;
            dr = displaytable.NewRow();
            dr["日期时间"] = t;
            dr["详情"] = d;
            dr["备注"] = r;

            displaytable.Rows.Add(dr);
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            //StreamReader SReader = new StreamReader(PathString, Encoding.Default);                    //创建StreamReader对象
            //tbxDiary.Text = SReader.ReadToEnd();                                                       //读取全部数据

        }

        private void btnOutData_Click(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;// Application.StartupPath;
            string fold = "dataBackup";
            string fullPath = path + fold;
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
            saveFileDialog1.InitialDirectory = fullPath;
            saveFileDialog1.FileName = DateTime.Now.ToString("yyyyMMdd") + ".txt";
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.Filter = "备份文件(*.txt)|*.txt|All files (*.*)|*.*";
            DialogResult dlg = saveFileDialog1.ShowDialog(this);
            if (dlg == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = File.AppendText(saveFileDialog1.FileName))//创建或打开一个文件用于写入 
                    {
                        for (int i=0; i < CheckDatas.Rows.Count;i++ )
                        {
                            sw.WriteLine(CheckDatas.Rows[i].Cells[0].Value.ToString()+"  " + CheckDatas.Rows[i].Cells[1].Value.ToString()+ "  "+ CheckDatas.Rows[i].Cells[2].Value.ToString());//把字符串写入文本流
                        }
                            
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("数据导出失败！" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                MessageBox.Show("文件导出成功！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnfind_Click(object sender, EventArgs e)
        {
            string err = string.Empty;
            displaytable.Clear();
            CheckDatas.DataSource = null;
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.Append("where worktime>=#");
                sb.Append(DTPstart.Value.ToString("yyyy/MM/dd 00:00:00"));
                sb.Append("#");
                sb.Append(" AND worktime<=#");
                sb.Append(DTPend.Value.ToString("yyyy/MM/dd 23:59:00"));
                sb.Append("#");
                sb.Append(" order by ID");
                DataTable dt = sql.GetDiary(sb.ToString(), out err);
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AddItemToTable(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString());
                    }
                }
                CheckDatas.DataSource = displaytable;
                CheckDatas.Columns[0].Width = 200;
                CheckDatas.Columns[1].Width = 400;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }
    }
}

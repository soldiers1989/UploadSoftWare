using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using DY.FoodClientLib.Model;
//using FoodClient.App_Code;

namespace FoodClient.Query
{
    public partial class ExprotForm : Form
    {
        public ExprotForm()
        {
            InitializeComponent();
        }

        public IList<clsReportSZ.ExportReport> _exprotList = new List<clsReportSZ.ExportReport>();

        private void ExprotForm_Load(object sender, EventArgs e)
        {
            if (_exprotList != null && _exprotList.Count > 0)
            {
                List<clsReportSZ.ExportReport> exprotList = new List<clsReportSZ.ExportReport>();
                int Number = 0;
                foreach (clsReportSZ.ExportReport item in _exprotList)
                {
                    exprotList.Add(item);
                }
                //对sysCode排序
                exprotList.Sort(delegate(clsReportSZ.ExportReport a, clsReportSZ.ExportReport b) { return a.SysCode.CompareTo(b.SysCode); });
                //重置序号
                List<clsReportSZ.ExportReport> repList = new List<clsReportSZ.ExportReport>();
                foreach (clsReportSZ.ExportReport exp in exprotList)
                {
                    Number++;
                    exp.Number = Number.ToString();
                    repList.Add(exp);
                }
                dataGridView_SZ.DataSource = repList;
            }
            Export();
        }

        private void Export()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Execl files (*.xls)|*.xls";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "导出到 Excel";
            saveFileDialog.ShowDialog();
            Stream myStream = null;
            StreamWriter sw = null;
            string strError = "";
            try
            {
                myStream = saveFileDialog.OpenFile();
                sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding("gb2312"));
                string str = "";

                for (int i = 0; i < dataGridView_SZ.ColumnCount; i++)
                {
                    if (i > 0)
                    {
                        str += "\t";
                    }
                    str += dataGridView_SZ.Columns[i].HeaderText;
                }
                sw.WriteLine(str);
                for (int j = 0; j < dataGridView_SZ.Rows.Count; j++)
                {
                    string tempStr = "";
                    for (int k = 0; k < dataGridView_SZ.Columns.Count; k++)
                    {
                        if (k > 0)
                        {
                            tempStr += "\t";
                        }
                        tempStr += dataGridView_SZ.Rows[j].Cells[k].Value.ToString();
                    }

                    sw.WriteLine(tempStr);
                }
                sw.Close();
                myStream.Close();
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                if (!ex.Message.Equals("索引超出了数组界限。"))
                {
                    MessageBox.Show("导出失败！\n异常信息：" + ex.Message);
                }
            }
            finally
            {
                if (sw != null)
                    sw.Close();
                if (myStream != null)
                    myStream.Close();
                if (strError.Equals(string.Empty))
                {
                    MessageBox.Show("导出成功！", "操作提示");
                }
                this.Close();
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorkstationBLL.Mode;
using WorkstationDAL.Model;

namespace WorkstationUI.function
{
    public partial class FrmBHSample : Form
    {
        private DataTable dt = null;
        private clsSetSqlData sql = new clsSetSqlData();
        private StringBuilder sb = new StringBuilder();
        private DataTable Searchtable = null;
        private bool m_IsCreatedDataTable = false;
        private string err = "";
        public FrmBHSample()
        {
            InitializeComponent();
        }

        private void FrmBHSample_Load(object sender, EventArgs e)
        {
            BHtable();
            BSearchSample("", "IsTest='否'");
        }
        private void BHtable()
        {
            if (!m_IsCreatedDataTable)
            {
                Searchtable = new DataTable("sample");
                DataColumn dataCol;
                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "样品名称";
                Searchtable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "样品ID";
                Searchtable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "被检单位";
                Searchtable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "经营户";
                Searchtable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "采样时间";
                Searchtable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "采样地点";
                Searchtable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "采样人";
                Searchtable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "已测试";
                Searchtable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "ID";
                Searchtable.Columns.Add(dataCol);

                m_IsCreatedDataTable = true;
            }
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderby"></param>
        private void BSearchSample(string where, string orderby)
        {
            try
            {
                dt = sql.GetTestData(where, orderby, 1, out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Searchtable.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        BHaddtable(dt.Rows[i]["goodsName"].ToString(), dt.Rows[i]["productId"].ToString(), dt.Rows[i]["marketName"].ToString()
                            , dt.Rows[i]["operateName"].ToString(), dt.Rows[i]["samplingTime"].ToString(), dt.Rows[i]["positionAddress"].ToString()
                            , dt.Rows[i]["samplingPerson"].ToString(), dt.Rows[i]["IsTest"].ToString(), dt.Rows[i]["ID"].ToString());
                    }
                    CheckDatas.DataSource = Searchtable;
                    for (int j = 0; j < CheckDatas.Rows.Count; j++)
                    {
                        if (CheckDatas.Rows[j].Cells["已测试"].Value.ToString() == "是")
                        {
                            CheckDatas.Rows[j].DefaultCellStyle.BackColor = Color.YellowGreen;
                        }
                    }
                }
                else
                {
                    CheckDatas.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 加载数据到表
        /// </summary>
        private void BHaddtable(string sample, string sampleID, string checkunit, string person, string gettime, string getplace, string getperson, string istest, string id)
        {
            DataRow dr;
            dr = Searchtable.NewRow();
            dr["样品名称"] = sample;
            dr["样品ID"] = sampleID;
            dr["被检单位"] = checkunit;
            dr["经营户"] = person;
            dr["采样时间"] = gettime;
            dr["采样地点"] = getplace;
            dr["采样人"] = getperson;
            dr["已测试"] = istest;
            dr["ID"] = id;
            Searchtable.Rows.Add(dr);
        }
        private void labelClose_MouseEnter(object sender, EventArgs e)
        {
            labelClose.ForeColor = Color.Red;
        }

        private void labelClose_MouseLeave(object sender, EventArgs e)
        {
            labelClose.ForeColor = Color.White;
        }

        private void labelClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                sb.Length = 0;
                sb.AppendFormat("goodsName like '%{0}%'",textBoxSample.Text.Trim());
                BSearchSample(sb.ToString(), "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CheckDatas_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            for (int j = 0; j < CheckDatas.Rows.Count; j++)
            {
                if (CheckDatas.Rows[j].Cells["已测试"].Value.ToString() == "是")
                {
                    CheckDatas.Rows[j].DefaultCellStyle.BackColor = Color.YellowGreen;
                }
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            Global.Bsample = CheckDatas.SelectedRows[0].Cells["样品名称"].Value.ToString();
            Global.BsampleID = CheckDatas.SelectedRows[0].Cells["样品ID"].Value.ToString();
            Global.BcheckCommpany = CheckDatas.SelectedRows[0].Cells["被检单位"].Value.ToString();
            Global.BID = CheckDatas.SelectedRows[0].Cells["ID"].Value.ToString();
            this.Close();
        }

        private void CheckDatas_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Global.Bsample = CheckDatas.SelectedRows[0].Cells["样品名称"].Value.ToString();
            Global.BsampleID = CheckDatas.SelectedRows[0].Cells["样品ID"].Value.ToString();
            Global.BcheckCommpany = CheckDatas.SelectedRows[0].Cells["被检单位"].Value.ToString();
            Global.BID = CheckDatas.SelectedRows[0].Cells["ID"].Value.ToString();

            this.Close();
        }
    }
}

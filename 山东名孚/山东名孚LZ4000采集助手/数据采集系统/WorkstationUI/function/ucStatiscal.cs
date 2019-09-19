using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorkstationBLL.Mode;
using System.Windows.Forms.DataVisualization.Charting;

namespace WorkstationUI.function
{
    public partial class ucStatiscal : UserControl
    {
        public ucStatiscal()
        {
            InitializeComponent();
        }

        clsSetSqlData sql = new clsSetSqlData();
        private DataTable DataReadTable = null;
        private bool m_IsCreatedDataTable = false;

        /// <summary>
        /// 初始化表格
        /// </summary>
        private void iniTable()
        {
            if (!m_IsCreatedDataTable)
            {
                DataReadTable = new DataTable("checkResult");//去掉Static
                DataColumn dataCol;
                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测编号";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);//int,string
                dataCol.ColumnName = "检测项目";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);//int,string
                dataCol.ColumnName = "样品名称";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测结果";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "单位";//检测值
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "结论";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测时间";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测仪器";
                DataReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "被检单位";
                DataReadTable.Columns.Add(dataCol);
                m_IsCreatedDataTable = true;
                m_IsCreatedDataTable = true;
            }
        }
        //查询
        private void btnfind_Click(object sender, EventArgs e)
        {
            btnfind.Enabled = false;
            DataReadTable.Clear();
            CheckDatas.DataSource = null;

            //根据用户输入的条件组合查询语句，将原来的String 换成StringBuilder对象,提高代码性能
            StringBuilder sb = new StringBuilder();
            sb.Append("CheckTime>=#");
            sb.Append(dTStart.Value.ToString("yyyy/MM/dd"));//yyyy-MM-dd
            sb.Append("#");
            sb.Append(" AND CheckTime<=#");

            sb.Append(dTPEnd.Value.ToString("yyyy/MM/dd"));
            sb.Append(" 23:59:59#");
            if (!string.IsNullOrEmpty(cmbResult.Text.Trim()) && cmbResult.Text.Trim() != "请选择...")
            {
                sb.AppendFormat("AND Result='{0}'", cmbResult.Text.Trim());
            }
            if (!string.IsNullOrEmpty(cmbTestItem.Text.Trim()) && cmbTestItem.Text.Trim() != "请选择...")
            {
                sb.AppendFormat("AND Checkitem='{0}'", cmbTestItem.Text.Trim());
            }

            if (!string.IsNullOrEmpty(cmbSample.Text.Trim()) && cmbSample.Text.Trim() != "请选择...")
            {
                sb.AppendFormat("AND SampleName='{0}'", cmbSample.Text.Trim());
            }
            sb.Append(" ORDER BY ID");
            DataTable dtb = sql.GetDataTable(sb.ToString(), "");
            if (dtb != null)
            {
                for (int i = 0; i < dtb.Rows.Count; i++)
                {

                    TableNewRow(dtb.Rows[i]["ChkNum"].ToString(), dtb.Rows[i]["Checkitem"].ToString(), dtb.Rows[i]["SampleName"].ToString(), dtb.Rows[i]["CheckData"].ToString(), dtb.Rows[i]["Unit"].ToString(),
                       dtb.Rows[i]["CheckTime"].ToString(), dtb.Rows[i]["CheckUnit"].ToString(), dtb.Rows[i]["Result"].ToString(), dtb.Rows[i]["Machine"].ToString());
                }

                CheckDatas.DataSource = DataReadTable;
                CheckDatas.Columns[6].Width = 160;
            }
            //CmbgetItemName();//获取检测项目
            //CmbgetSampleName();//获取样品名称
            generateChart();//生成柱行图

            btnfind.Enabled = true;
        }
        private void TableNewRow(string num, string item, string SampleName, string chkdata, string Unit, string CheckTime, string CheckUnit, string Result, string machine)
        {
            DataRow dr;
            dr = DataReadTable.NewRow();
            dr["检测编号"] = num;
            dr["检测项目"] = item;
            dr["样品名称"] = SampleName;
            dr["检测结果"] = chkdata;
            dr["单位"] = Unit;
            dr["结论"] = Result;
            dr["检测时间"] = CheckTime;
            dr["检测仪器"] = machine;
            dr["被检单位"] = CheckUnit;
            DataReadTable.Rows.Add(dr);
        }

        private void ucStatiscal_Load(object sender, EventArgs e)
        {
            iniTable();
            dTStart.Text = DateTime.Now.AddDays(-DateTime.Now.Day + 1).ToString(); 
            SearchDatabase();//查询数据

            CmbgetItemName();//获取检测项目
            CmbgetSampleName();//获取样品名称
           
            generateChart();//生成柱行图
           
           
        }

        private void btnStatical_Click(object sender, EventArgs e)
        {
            if (DataReadTable == null)
            {
                MessageBox.Show("请先查询数据，在统计", "操作提示");
                return;
            }
            //CmbgetItemName();//获取检测项目
            //CmbgetSampleName();//获取样品名称
            generateChart();//生成柱行图
           
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        private void SearchDatabase()
        {
            DataReadTable.Clear();
            CheckDatas.DataSource = null;

            //根据用户输入的条件组合查询语句，将原来的String 换成StringBuilder对象,提高代码性能
            StringBuilder sb = new StringBuilder();
            sb.Append("CheckTime>=#");
            sb.Append(dTStart.Value.ToString("yyyy/MM/dd"));//yyyy-MM-dd
            sb.Append("#");
            sb.Append(" AND CheckTime<=#");
            //sb.Append(dTPEnd.Value.Year.ToString());
            //sb.Append("-");
            //sb.Append(dTPEnd.Value.Month.ToString("MM"));
            //sb.Append("-");
            //sb.Append(dTPEnd.Value.Day.ToString("dd"));
            sb.Append(dTPEnd.Value.ToString("yyyy/MM/dd"));
            sb.Append(" 23:59:59#");
            if (!string.IsNullOrEmpty(cmbResult.Text.Trim()) && cmbResult.Text.Trim()!="请选择...")
            {
                sb.AppendFormat("AND Result='%{0}%'", cmbResult.Text.Trim());
            }
            if (!string.IsNullOrEmpty(cmbTestItem.Text.Trim()) && cmbTestItem.Text.Trim() != "请选择...")
            {
                sb.AppendFormat("AND Checkitem='%{0}%'", cmbTestItem.Text.Trim());
            }

            sb.Append(" ORDER BY ID");
            DataTable dtb = sql.GetDataTable(sb.ToString(), "");
            if (dtb != null)
            {
                for (int i = 0; i < dtb.Rows.Count; i++)
                {
                    //TableNewRow(dtb.Rows[i][0].ToString(), dtb.Rows[i][1].ToString(), dtb.Rows[i][2].ToString(), dtb.Rows[i][3].ToString(), dtb.Rows[i][4].ToString(),
                    //   dtb.Rows[i][5].ToString(), dtb.Rows[i][6].ToString(), dtb.Rows[i][7].ToString(), dtb.Rows[i][8].ToString());
                    TableNewRow(dtb.Rows[i]["ChkNum"].ToString(), dtb.Rows[i]["Checkitem"].ToString(), dtb.Rows[i]["SampleName"].ToString(), dtb.Rows[i]["CheckData"].ToString(), dtb.Rows[i]["Unit"].ToString(),
                      dtb.Rows[i]["CheckTime"].ToString(), dtb.Rows[i]["CheckUnit"].ToString(), dtb.Rows[i]["Result"].ToString(), dtb.Rows[i]["Machine"].ToString());
                }

                CheckDatas.DataSource = DataReadTable;
                CheckDatas.Columns[6].Width = 160;
                
            }
        }
       
        /// <summary>
        /// 生成柱行图
        /// </summary>
        private void generateChart()
        {
            //cmbTestItem.SelectedIndex = 0;
            //cmbSample.SelectedIndex = 0;

            int sam = 0;//统计同一列样品数有多少个
            int Itm = 0;//统计同一列检测项目有多少个
            string[,] Sanum = new string[cmbSample.Items.Count, 2];
            string[,] Itemnum = new string[cmbTestItem.Items.Count, 2];
            //样品数查找
            for (int j = 1; j < cmbSample.Items.Count; j++)
            {
                sam = 0;
                for (int n = 0; n < DataReadTable.Rows.Count; n++)
                {
                    string d = cmbSample.Items[j].ToString();
                    string s = DataReadTable.Rows[n]["样品名称"].ToString();
                    if (cmbSample.Items[j].ToString() == DataReadTable.Rows[n]["样品名称"].ToString())// CheckDatas.Rows[n].Cells[2].Value.ToString()
                    {
                        sam = sam + 1;
                    }
                }
                Sanum[j-1, 0] = cmbSample.Items[j].ToString();//名称
                Sanum[j-1, 1] = sam.ToString();//个数
            }
            //检测项目数查找
            for (int j = 1; j < cmbTestItem.Items.Count; j++)
            {
                Itm=0;
                for (int n = 0; n < DataReadTable.Rows.Count; n++)
                {
                    if (cmbTestItem.Items[j].ToString() == DataReadTable.Rows[n]["检测项目"].ToString())
                    {
                        Itm = Itm + 1;
                    }
                }             
                Itemnum[j-1, 0] = cmbTestItem.Items[j].ToString();//名称
                Itemnum[j-1, 1] = Itm.ToString();//个数
            }
            //合格和不合格数
            int ok = 0, NG = 0, All = 0;

            for (int i = 0; i < DataReadTable.Rows.Count; i++)
            {
                string d = DataReadTable.Rows[i]["结论"].ToString();
                if (DataReadTable.Rows[i]["结论"].ToString() == "阴性" || DataReadTable.Rows[i]["结论"].ToString() == "合格")
                {
                    ok = ok + 1;
                }
                else if (DataReadTable.Rows[i]["结论"].ToString() == "阳性" || DataReadTable.Rows[i]["结论"].ToString() == "不合格")
                {
                    NG = NG + 1;
                }
                else
                {
                    All = All + 1;
                }
            }
            //检测结果
            List<string> xData = new List<string>() { "合格", "不合格", "其他" };
            List<int> yData = new List<int>() { ok, NG, All };

            //样品数
            //chart1.Titles.Add("柱型图统计分析");
            List<string> SamData = new List<string>() { };
            List<int> Sampnum = new List<int>() { };
            for (int i = 0; i < Sanum.GetLength(0)-1; i++)
            {
                SamData.Add(Sanum[i, 0]);
                Sampnum.Add((Convert.ToInt32(Sanum[i, 1])));
            }
            //检测项目
            List<string> ItemData = new List<string>() { };
            List<int> ChkItemnum = new List<int>() { };
            for (int i = 0; i < Itemnum.GetLength(0) - 1; i++)
            {
                //int f = Sanum.GetLength(0);
                ItemData.Add(Itemnum[i, 0]);
                ChkItemnum.Add((Convert.ToInt32(Itemnum[i, 1])));
            }
            //柱状图显示检测项目数

            chart1.Series[0].Points.Clear();
            chart1.Series[0].ChartType = SeriesChartType.Column;
            chart1.Legends[0].Enabled = false;//不显示图例
            chart1.Legends[0].BackColor = Color.Transparent;
            chart1.ChartAreas[0].BackColor = Color.Transparent;//设置背景为透明
            chart1.ChartAreas[0].Area3DStyle.Enable3D = true;//设置3D效果
            chart1.ChartAreas[0].Area3DStyle.PointDepth = chart1.ChartAreas[0].Area3DStyle.PointGapDepth = 50;//设置一下深度，看起来舒服点……     
            chart1.ChartAreas[0].Area3DStyle.WallWidth = 0;//设置墙的宽度为0；
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;//不显示网格线
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.Series[0].Label = "#VAL";//设置标签文本 (在设计期通过属性窗口编辑更直观)
            chart1.Series[0].IsValueShownAsLabel = true ;//显示标签
            chart1.Series[0].CustomProperties = "DrawingStyle=Cylinder, PointWidth=0.5";//设置为圆柱形 (在设计期通过属性窗口编辑更直观)
            chart1.Series[0].Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;//设置调色板
            //数据
            //chart1.Series[0].Points.AddXY(xData,yData）;
            chart1.Series[0].Points.Clear();
            //chart1.Series[0].Points.AddXY("合格", ok);
            //chart1.Series[0].Points.AddXY("不合格", NG);
            //chart1.Series[0].Points.AddXY("其他", All);
            chart1.Series[0].Points.DataBindXY(ItemData, ChkItemnum);
            chart1.ChartAreas[0].AxisX.Interval = 1;//隔1显示
            chart1.ChartAreas[0].AxisX.IntervalOffset = 1;
            chart1.Series[0].Name = "检测结果";

            //chart1.Series[0].Ax
            //饼型图 显示检测结论 
            //double[] yValues = { ok, NG, All };
            //string[] xValue = { "合格", "不合格", "总数" };
            chart2.Series[0].ChartType = SeriesChartType.Pie;
            chart2.Legends[0].BackColor = Color.Transparent;//去掉标签的底色       
            chart2.ChartAreas[0].BackColor = Color.Transparent;//设置背景为透明
            chart2.ChartAreas[0].Area3DStyle.Enable3D = true;//设置3D效果
            //chart2.Series[0]["PieLabelStyle"] = "Outside";//将文字移到外侧
            //chart2.Series[0]["PieLineColor"] = "Black";//绘制黑色的连线
            chart2.Series[0].IsValueShownAsLabel = true;//显示标签
            chart2.ChartAreas[0].Area3DStyle.PointDepth = chart2.ChartAreas[0].Area3DStyle.PointGapDepth = 80;//设置一下深度，看起来舒服点……           
            if (NG == 0)
            {
                List<string> RData = new List<string>() { "合格", "其他" };
                List<int> ZData = new List<int>() { ok, All };
                chart2.Series[0].Points.DataBindXY(RData, ZData);
            }
            else if(ok==0)
            {
                List<string> RData = new List<string>() { "不合格", "其他" };
                List<int> ZData = new List<int>() { NG , All };
                chart2.Series[0].Points.DataBindXY(RData, ZData);
            }
            else 
            {
                chart2.Series[0].Points.DataBindXY(xData, yData);
            }
            
            //chart2.Titles.Add("饼型图统计分析");
            chart2.Series[0].Name = "检测结果";

            //折线图
            //List<string> xData = new List<string>() { "A", "B", "C", "D" };
            //List<int> yData = new List<int>() { 10, 20, 30, 40 };
            chart3.Series[0].ChartType = SeriesChartType.Line;
            chart3.Legends[0].BackColor = Color.Transparent;//去掉标签的底色
            chart3.ChartAreas[0].AxisY.MajorGrid.Enabled = false;//不显示网格线
            chart3.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart3.Series[0].IsValueShownAsLabel = true;//显示标签
            chart3.ChartAreas[0].AxisX.Interval = 1;//隔1显示
            //线条颜色
            //chart1.Series[0].Color = Color.Green;
            //线条粗细
            chart3.Series[0].BorderWidth = 3;
            //标记点边框颜色      
            chart3.Series[0].MarkerBorderColor = Color.Black;
            //标记点边框大小
            chart3.Series[0].MarkerBorderWidth = 3;
            //标记点中心颜色
            chart3.Series[0].MarkerColor = Color.Red;
            //标记点大小
            chart3.Series[0].MarkerSize = 8;
            //标记点类型     
            chart3.Series[0].MarkerStyle = MarkerStyle.Circle;
            //将文字移到外侧
            //chart3.Series[0]["PieLabelStyle"] = "Outside";
            //绘制黑色的连线
            //chart3.Series[0]["PieLineColor"] = "Black";
            chart3.Series[0].Points.DataBindXY(SamData, Sampnum);
            //chart3.Titles.Add("折线图统计分析");
            chart3.Series[0].Name = "检测结果";
        }
        //选择日期查询
        private void dTPEnd_ValueChanged(object sender, EventArgs e)
        {
            DataReadTable.Clear();
            CheckDatas.DataSource = null;

            //根据用户输入的条件组合查询语句，将原来的String 换成StringBuilder对象,提高代码性能
            StringBuilder sb = new StringBuilder();
            sb.Append("CheckTime>=#");
            sb.Append(dTStart.Value.ToString("yyyy/MM/dd"));//yyyy-MM-dd
            sb.Append("#");
            sb.Append(" AND CheckTime<=#");
            //sb.Append(dTPEnd.Value.Year.ToString());
            //sb.Append("-");
            //sb.Append(dTPEnd.Value.Month.ToString("MM"));
            //sb.Append("-");
            //sb.Append(dTPEnd.Value.Day.ToString("dd"));
            sb.Append(dTPEnd.Value.ToString("yyyy/MM/dd"));
            sb.Append(" 23:59:59#");
            if (!string.IsNullOrEmpty(cmbResult.Text.Trim()) && cmbResult.Text.Trim() != "请选择...")
            {
                sb.AppendFormat("AND Result='{0}'", cmbResult.Text.Trim());
            }
            if (!string.IsNullOrEmpty(cmbTestItem.Text.Trim()) && cmbTestItem.Text.Trim()!="请选择...")
            {
                sb.AppendFormat("AND Checkitem='{0}'", cmbTestItem.Text.Trim());
            }
            //if (!string.IsNullOrEmpty(cmbTestUnit.Text.Trim()))
            //{
            //    sb.AppendFormat("AND CheckUnit='%{0}%'", cmbTestUnit.Text.Trim());
            //}
            if (!string.IsNullOrEmpty(cmbSample.Text.Trim()) && cmbSample.Text.Trim() != "请选择...")
            {
                sb.AppendFormat("AND SampleName='{0}'", cmbSample.Text.Trim());
            }
            sb.Append(" ORDER BY ID");
            DataTable dtb = sql.GetDataTable(sb.ToString(), "");
            if (dtb != null)
            {
                for (int i = 0; i < dtb.Rows.Count; i++)
                {
                    
                    //TableNewRow(dtb.Rows[i][0].ToString(), dtb.Rows[i][1].ToString(), dtb.Rows[i][2].ToString(), dtb.Rows[i][3].ToString(), dtb.Rows[i][4].ToString(),
                    //   dtb.Rows[i][5].ToString(), dtb.Rows[i][6].ToString(), dtb.Rows[i][7].ToString(), dtb.Rows[i][8].ToString());
                    TableNewRow(dtb.Rows[i]["ChkNum"].ToString(), dtb.Rows[i]["Checkitem"].ToString(), dtb.Rows[i]["SampleName"].ToString(), dtb.Rows[i]["CheckData"].ToString(), dtb.Rows[i]["Unit"].ToString(),
                      dtb.Rows[i]["CheckTime"].ToString(), dtb.Rows[i]["CheckUnit"].ToString(), dtb.Rows[i]["Result"].ToString(), dtb.Rows[i]["Machine"].ToString());
                }

                CheckDatas.DataSource = DataReadTable;
                CheckDatas.Columns[6].Width = 160;
            }
            CmbgetItemName();//获取检测项目
            CmbgetSampleName();//获取样品名称
            generateChart();//生成柱行图
           
        }
        //获取检测项目到combox
        private void CmbgetItemName()
        {
            List<string> lsName = new List<string>();
            lsName.Clear();
            cmbTestItem.Items.Clear();
            cmbTestItem.Items.Add("请选择...");
            
            for (int i = 0; i < DataReadTable.Rows.Count; i++)
            {
                string name = DataReadTable.Rows[i]["检测项目"].ToString();//this.CheckDatas.Rows[i].Cells[1].Value.ToString()
                if (lsName.Contains(name))
                {
                    continue;
                }
                else
                {
                    lsName.Add(name);
                    cmbTestItem.Items.Add(name);
                }
            }
            //cmbTestItem.DataSource = lsName;
           
        }
        //获取样品名称添加到combox中
        private void CmbgetSampleName()
        {
            List<string> lsName = new List<string>();
            lsName.Clear();
            cmbSample.Items.Clear();
            cmbSample.Items.Add("请选择...");
           
            for (int i = 0; i < DataReadTable.Rows.Count; i++)
            {
                string name = DataReadTable.Rows[i]["样品名称"].ToString();
                if (lsName.Contains(name))
                {
                    continue;
                }
                else
                {
                    lsName.Add(name);
                    cmbSample.Items.Add(name);
                }
            }
            //cmbSample.DataSource = lsName;
           
        }
        /// <summary>
        /// 检测结论选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbResult_SelectedValueChanged(object sender, EventArgs e)
        {
            DataReadTable.Clear();
            CheckDatas.DataSource = null;

            //根据用户输入的条件组合查询语句，将原来的String 换成StringBuilder对象,提高代码性能
            StringBuilder sb = new StringBuilder();
            sb.Append("CheckTime>=#");
            sb.Append(dTStart.Value.ToString("yyyy/MM/dd"));//yyyy-MM-dd
            sb.Append("#");
            sb.Append(" AND CheckTime<=#");
           
            sb.Append(dTPEnd.Value.ToString("yyyy/MM/dd"));
            sb.Append(" 23:59:59#");
            if (!string.IsNullOrEmpty(cmbResult.Text.Trim()) && cmbResult.Text.Trim() != "请选择...")
            {
                sb.AppendFormat("AND Result='{0}'", cmbResult.Text.Trim());
            }
            if (!string.IsNullOrEmpty(cmbTestItem.Text.Trim()) && cmbTestItem.Text.Trim() != "请选择...")
            {
                sb.AppendFormat("AND Checkitem='{0}'", cmbTestItem.Text.Trim());
            }
            
            if (!string.IsNullOrEmpty(cmbSample.Text.Trim()) && cmbSample.Text.Trim() != "请选择...")
            {
                sb.AppendFormat("AND SampleName='{0}'", cmbSample.Text.Trim());
            }
            sb.Append(" ORDER BY ID");
            DataTable dtb = sql.GetDataTable(sb.ToString(), "");
            if (dtb != null)
            {
                for (int i = 0; i < dtb.Rows.Count; i++)
                {
                    
                    //TableNewRow(dtb.Rows[i][0].ToString(), dtb.Rows[i][1].ToString(), dtb.Rows[i][2].ToString(), dtb.Rows[i][3].ToString(), dtb.Rows[i][4].ToString(),
                    //   dtb.Rows[i][5].ToString(), dtb.Rows[i][6].ToString(), dtb.Rows[i][7].ToString(), dtb.Rows[i][8].ToString());
                    TableNewRow(dtb.Rows[i]["ChkNum"].ToString(), dtb.Rows[i]["Checkitem"].ToString(), dtb.Rows[i]["SampleName"].ToString(), dtb.Rows[i]["CheckData"].ToString(), dtb.Rows[i]["Unit"].ToString(),
                      dtb.Rows[i]["CheckTime"].ToString(), dtb.Rows[i]["CheckUnit"].ToString(), dtb.Rows[i]["Result"].ToString(), dtb.Rows[i]["Machine"].ToString());
                }

                CheckDatas.DataSource = DataReadTable;
                CheckDatas.Columns[6].Width = 160;
            }
            //CmbgetItemName();//获取检测项目
            //CmbgetSampleName();//获取样品名称
            generateChart();//生成柱行图
        }

        private void cmbTestItem_SelectedValueChanged(object sender, EventArgs e)
        {
            DataReadTable.Clear();
            CheckDatas.DataSource = null;

            //根据用户输入的条件组合查询语句，将原来的String 换成StringBuilder对象,提高代码性能
            StringBuilder sb = new StringBuilder();
            sb.Append("CheckTime>=#");
            sb.Append(dTStart.Value.ToString("yyyy/MM/dd"));//yyyy-MM-dd
            sb.Append("#");
            sb.Append(" AND CheckTime<=#");

            sb.Append(dTPEnd.Value.ToString("yyyy/MM/dd"));
            sb.Append(" 23:59:59#");
            if (!string.IsNullOrEmpty(cmbResult.Text.Trim()) && cmbResult.Text.Trim() != "请选择...")
            {
                sb.AppendFormat("AND Result='{0}'", cmbResult.Text.Trim());
            }
            if (!string.IsNullOrEmpty(cmbTestItem.Text.Trim()) && cmbTestItem.Text.Trim() != "请选择...")
            {
                sb.AppendFormat("AND Checkitem='{0}'", cmbTestItem.Text.Trim());
            }

            if (!string.IsNullOrEmpty(cmbSample.Text.Trim()) && cmbSample.Text.Trim() != "请选择...")
            {
                sb.AppendFormat("AND SampleName='{0}'", cmbSample.Text.Trim());
            }
            sb.Append(" ORDER BY ID");
            DataTable dtb = sql.GetDataTable(sb.ToString(), "");
            if (dtb != null)
            {
                for (int i = 0; i < dtb.Rows.Count; i++)
                {

                    //TableNewRow(dtb.Rows[i][0].ToString(), dtb.Rows[i][1].ToString(), dtb.Rows[i][2].ToString(), dtb.Rows[i][3].ToString(), dtb.Rows[i][4].ToString(),
                    //   dtb.Rows[i][5].ToString(), dtb.Rows[i][6].ToString(), dtb.Rows[i][7].ToString(), dtb.Rows[i][8].ToString());
                    TableNewRow(dtb.Rows[i]["ChkNum"].ToString(), dtb.Rows[i]["Checkitem"].ToString(), dtb.Rows[i]["SampleName"].ToString(), dtb.Rows[i]["CheckData"].ToString(), dtb.Rows[i]["Unit"].ToString(),
                      dtb.Rows[i]["CheckTime"].ToString(), dtb.Rows[i]["CheckUnit"].ToString(), dtb.Rows[i]["Result"].ToString(), dtb.Rows[i]["Machine"].ToString());
                }

                CheckDatas.DataSource = DataReadTable;
                CheckDatas.Columns[6].Width = 160;
            }
            //CmbgetItemName();//获取检测项目
            //CmbgetSampleName();//获取样品名称
            generateChart();//生成柱行图
        }

        private void cmbSample_SelectedValueChanged(object sender, EventArgs e)
        {
            DataReadTable.Clear();
            CheckDatas.DataSource = null;

            //根据用户输入的条件组合查询语句，将原来的String 换成StringBuilder对象,提高代码性能
            StringBuilder sb = new StringBuilder();
            sb.Append("CheckTime>=#");
            sb.Append(dTStart.Value.ToString("yyyy/MM/dd"));//yyyy-MM-dd
            sb.Append("#");
            sb.Append(" AND CheckTime<=#");

            sb.Append(dTPEnd.Value.ToString("yyyy/MM/dd"));
            sb.Append(" 23:59:59#");
            if (!string.IsNullOrEmpty(cmbResult.Text.Trim()) && cmbResult.Text.Trim() != "请选择...")
            {
                sb.AppendFormat("AND Result='{0}'", cmbResult.Text.Trim());
            }
            if (!string.IsNullOrEmpty(cmbTestItem.Text.Trim()) && cmbTestItem.Text.Trim() != "请选择...")
            {
                sb.AppendFormat("AND Checkitem='{0}'", cmbTestItem.Text.Trim());
            }

            if (!string.IsNullOrEmpty(cmbSample.Text.Trim()) && cmbSample.Text.Trim() != "请选择...")
            {
                sb.AppendFormat("AND SampleName='{0}'", cmbSample.Text.Trim());
            }
            sb.Append(" ORDER BY ID");
            DataTable dtb = sql.GetDataTable(sb.ToString(), "");
            if (dtb != null)
            {
                for (int i = 0; i < dtb.Rows.Count; i++)
                {

                    //TableNewRow(dtb.Rows[i][0].ToString(), dtb.Rows[i][1].ToString(), dtb.Rows[i][2].ToString(), dtb.Rows[i][3].ToString(), dtb.Rows[i][4].ToString(),
                    //   dtb.Rows[i][5].ToString(), dtb.Rows[i][6].ToString(), dtb.Rows[i][7].ToString(), dtb.Rows[i][8].ToString());
                    TableNewRow(dtb.Rows[i]["ChkNum"].ToString(), dtb.Rows[i]["Checkitem"].ToString(), dtb.Rows[i]["SampleName"].ToString(), dtb.Rows[i]["CheckData"].ToString(), dtb.Rows[i]["Unit"].ToString(),
                      dtb.Rows[i]["CheckTime"].ToString(), dtb.Rows[i]["CheckUnit"].ToString(), dtb.Rows[i]["Result"].ToString(), dtb.Rows[i]["Machine"].ToString());
                }

                CheckDatas.DataSource = DataReadTable;
                CheckDatas.Columns[6].Width = 160;
            }
            //CmbgetItemName();//获取检测项目
            //CmbgetSampleName();//获取样品名称
            generateChart();//生成柱行图
        }

        private void dTStart_ValueChanged(object sender, EventArgs e)
        {
            DataReadTable.Clear();
            CheckDatas.DataSource = null;

            //根据用户输入的条件组合查询语句，将原来的String 换成StringBuilder对象,提高代码性能
            StringBuilder sb = new StringBuilder();
            sb.Append("CheckTime>=#");
            sb.Append(dTStart.Value.ToString("yyyy/MM/dd"));//yyyy-MM-dd
            sb.Append("#");
            sb.Append(" AND CheckTime<=#");
            //sb.Append(dTPEnd.Value.Year.ToString());
            //sb.Append("-");
            //sb.Append(dTPEnd.Value.Month.ToString("MM"));
            //sb.Append("-");
            //sb.Append(dTPEnd.Value.Day.ToString("dd"));
            sb.Append(dTPEnd.Value.ToString("yyyy/MM/dd"));
            sb.Append(" 23:59:59#");
            if (!string.IsNullOrEmpty(cmbResult.Text.Trim()) && cmbResult.Text.Trim() != "请选择...")
            {
                sb.AppendFormat("AND Result='{0}'", cmbResult.Text.Trim());
            }
            if (!string.IsNullOrEmpty(cmbTestItem.Text.Trim()) && cmbTestItem.Text.Trim() != "请选择...")
            {
                sb.AppendFormat("AND Checkitem='{0}'", cmbTestItem.Text.Trim());
            }
            //if (!string.IsNullOrEmpty(cmbTestUnit.Text.Trim()))
            //{
            //    sb.AppendFormat("AND CheckUnit='%{0}%'", cmbTestUnit.Text.Trim());
            //}
            if (!string.IsNullOrEmpty(cmbSample.Text.Trim()) && cmbSample.Text.Trim() != "请选择...")
            {
                sb.AppendFormat("AND SampleName='{0}'", cmbSample.Text.Trim());
            }
            sb.Append(" ORDER BY ID");
            DataTable dtb = sql.GetDataTable(sb.ToString(), "");
            if (dtb != null)
            {
                for (int i = 0; i < dtb.Rows.Count; i++)
                {

                    //TableNewRow(dtb.Rows[i][0].ToString(), dtb.Rows[i][1].ToString(), dtb.Rows[i][2].ToString(), dtb.Rows[i][3].ToString(), dtb.Rows[i][4].ToString(),
                    //   dtb.Rows[i][5].ToString(), dtb.Rows[i][6].ToString(), dtb.Rows[i][7].ToString(), dtb.Rows[i][8].ToString());
                    TableNewRow(dtb.Rows[i]["ChkNum"].ToString(), dtb.Rows[i]["Checkitem"].ToString(), dtb.Rows[i]["SampleName"].ToString(), dtb.Rows[i]["CheckData"].ToString(), dtb.Rows[i]["Unit"].ToString(),
                      dtb.Rows[i]["CheckTime"].ToString(), dtb.Rows[i]["CheckUnit"].ToString(), dtb.Rows[i]["Result"].ToString(), dtb.Rows[i]["Machine"].ToString());
                }

                CheckDatas.DataSource = DataReadTable;
                CheckDatas.Columns[6].Width = 160;
            }
            CmbgetItemName();//获取检测项目
            CmbgetSampleName();//获取样品名称
            generateChart();//生成柱行图
        }

       
    }
}

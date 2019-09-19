using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using DY.FoodClientLib;

namespace FoodClient
{
	/// <summary>
	///查询汇总查询条件
	/// </summary>
	public class frmCollection : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.DateTimePicker dateTimePicker2;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
        private System.ComponentModel.Container components = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public frmCollection()
        {
            InitializeComponent();
        }
        private string querystring;

        /// <summary>
        /// 查询条件
        /// </summary>
        public string QueryString
        {
            get { return querystring; }
            set { querystring=value;}
        }

		#region Windows 窗体设计器生成的代码

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCollection));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.dateTimePicker2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(4, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 160);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "汇总条件";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(70, 132);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(171, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "公斤";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "计量单位：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(70, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "广州市农产品质量检测检测站";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "填报单位：";
            // 
            // comboBox1
            // 
            this.comboBox1.Items.AddRange(new object[] {
            ShareOption.SampleTitle+"种类",
            "被检单位",
            "被检单位类型"});
            this.comboBox1.Location = new System.Drawing.Point(106, 74);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(110, 20);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.Text = ShareOption.SampleTitle + "种类";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(42, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 14);
            this.label6.TabIndex = 5;
            this.label6.Text = "汇总类型：";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CustomFormat = "yyyy年MM月dd日";
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(106, 47);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(110, 21);
            this.dateTimePicker2.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(42, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 14);
            this.label5.TabIndex = 4;
            this.label5.Text = "结束日期：";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy年MM月dd日";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(106, 20);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(110, 21);
            this.dateTimePicker1.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(42, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 14);
            this.label4.TabIndex = 3;
            this.label4.Text = "开始日期：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(130, 174);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(64, 24);
            this.button1.TabIndex = 1;
            this.button1.Text = "汇总(&P)";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(218, 174);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(64, 24);
            this.button2.TabIndex = 2;
            this.button2.Text = "取消(&X)";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmCollection
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(288, 206);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCollection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "汇总统计条件";
            this.Load += new System.EventHandler(this.frmCollection_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

        private void frmCollection_Load(object sender, System.EventArgs e)
        {
            this.dateTimePicker1.Value = DateTime.Today.AddMonths(-1);
            this.dateTimePicker2.Value = DateTime.Today;
            this.comboBox1.Enabled = false;
            this.label2.Text = CurrentUser.GetInstance().Unit.ToString();
        }
		private void button2_Click(object sender, System.EventArgs e)
		{
            this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

        private void button1_Click(object sender, System.EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(" CheckStartDate>= #");
            sb.Append(dateTimePicker1.Value.Date.ToString());
            sb.Append("# AND CheckStartDate<=#");
            sb.Append(dateTimePicker2.Value.Date.ToString("yyyy-MM-dd"));
            sb.Append(" 23:59:59# ");
            querystring = sb.ToString();
            this.DialogResult = DialogResult.OK;

            #region 注掉 2011-06-20修改

            //if (this.dateTimePicker2.Value < this.dateTimePicker1.Value)
            //{
            //    MessageBox.Show("错误：终止日期不能小于起始日期");
            //    return;
            //}
            //else
            //{
            //    //此处读取5次数据库，需要改进
            //    string strDateQuery = " CheckStartDate>= #" + this.dateTimePicker1.Value.Date.ToString() + "# And CheckStartDate<=#" + this.dateTimePicker2.Value.Date.ToString("yyyy-MM-dd") + " 23:59:59# ";

            //    string strSQL = "SELECT T1.FoodCode, B.Name, T1.Count1 FROM [Select FoodCode,Count(FoodCode) As Count1 From tResult WHERE " + strDateQuery + "	GROUP BY FoodCode]. AS T1 LEFT JOIN tFoodClass AS B ON T1.FoodCode = B.SysCode ORDER BY T1.FoodCode";
            //    DataTable dt1 = DataBase.ExecuteDataTable(strSQL);

            //    strSQL = "Select FoodCode,Count(FoodCode) From tResult Where Result='不合格' And " + strDateQuery + " GROUP BY FoodCode ORDER BY FoodCode";
            //    DataTable dt2 = DataBase.ExecuteDataTable(strSQL);

            //    //去掉销毁字段 2011-06-20
            //    //strSQL = "Select FoodCode,Count(FoodCode) From tResult Where Result='不合格' And Remark Like '*销毁*' And " + strDateQuery + " GROUP BY FoodCode ORDER BY FoodCode";
            //    //DataTable dt3 = DataBase.ExecuteDataTable(strSQL);

            //    //检测仪器结果集
            //    strSQL = "Select FoodCode,Count(FoodCode) From tResult Where (ResultType='检测仪手动' Or ResultType='检测仪自动') And " + strDateQuery + " GROUP BY FoodCode ORDER BY FoodCode";
            //    DataTable dt4 = DataBase.ExecuteDataTable(strSQL);

            //    //其他检测手段结果集
            //    strSQL = "Select FoodCode,Count(FoodCode) From tResult Where ResultType<>'检测仪手动' AND ResultType<>'检测仪自动' And " + strDateQuery + " GROUP BY FoodCode ORDER BY FoodCode";
            //    DataTable dt5 = DataBase.ExecuteDataTable(strSQL);


            //    DataTable dt6 = new DataTable("ColTable");
            //    dt6.Columns.Add(ShareOption.SampleTitle, typeof(string));//0
            //    dt6.Columns.Add("抽检数量", typeof(int));//1
            //    dt6.Columns.Add("检测仪器检测数量", typeof(int));//2
            //    dt6.Columns.Add("其他手段检测数量", typeof(int));//3
            //    dt6.Columns.Add("不合格数量", typeof(int));//4
            //    dt6.Columns.Add("合格率(%)", typeof(float));//typeof(Decimal)
            //    dt6.Columns.Add("不合格率(%)", typeof(float));//typeof(Decimal)
            //    //dt6.Columns.Add("销毁数量", typeof(int));

            //    int num1 = 0;//抽检数量
            //    int num2 = 0;//不合格数
            //    int num3 = 0;
            //    int num4 = 0;
            //    int tNum1 = 0;//抽检总数
            //    int tNum2 = 0;//仪检测数据总数
            //    int tNum3 = 0;//其他检测仪器数量总数
            //    int tNum4 = 0;//不合数量总数
            //    object findvalue = null;
            //    DataRow drfind = null;
            //    DataRow ndr = null;
            //    DataColumn[] pkcol = new DataColumn[1];//对象不需要重复new 非必要更不要放在for内部使用

            //    foreach (DataRow dr in dt1.Rows)
            //    {
            //        num2 = 0;
            //        ndr = dt6.NewRow();
            //        ndr[0] = dr[1].ToString();
            //        num1 = Convert.ToInt32(dr[2]);
            //        ndr[1] = num1.ToString();
            //        findvalue = dr[0].ToString();


            //        pkcol[0] = dt4.Columns[0];
            //        dt4.PrimaryKey = pkcol;

            //        drfind = dt4.Rows.Find(findvalue);
            //        if (drfind != null)
            //        {
            //            num3 = Convert.ToInt32(drfind[1]);
            //            ndr[2] = num3.ToString();
            //            drfind = null;
            //        }
            //        else
            //        {
            //            ndr[2] = 0;
            //        }

            //        //DataColumn[] PrimaryKeyColumns1 = new DataColumn[1];
            //        //PrimaryKeyColumns1[0] = dt5.Columns[0];
            //        //dt5.PrimaryKey = PrimaryKeyColumns1;

            //        pkcol[0] = dt5.Columns[0];
            //        dt5.PrimaryKey = pkcol;
            //        drfind = dt5.Rows.Find(findvalue);
            //        if (drfind != null)
            //        {
            //            num4 = Convert.ToInt32(drfind[1]);
            //            ndr[3] = num4.ToString();
            //            drfind = null;
            //        }
            //        else
            //        {
            //            ndr[3] = 0;
            //        }

            //        //DataColumn[] PrimaryKeyColumns3 = new DataColumn[1];
            //        //PrimaryKeyColumns3[0] = dt2.Columns[0];
            //        //dt2.PrimaryKey = PrimaryKeyColumns3;

            //        pkcol[0] = dt2.Columns[0];
            //        dt2.PrimaryKey = pkcol;
            //        drfind = dt2.Rows.Find(findvalue);
            //        if (drfind != null)
            //        {
            //            num2 = Convert.ToInt32(drfind[1]);
            //            ndr[4] = num2.ToString();
            //            drfind = null;
            //        }
            //        else
            //        {
            //            num2 = 0;
            //            ndr[4] = 0;
            //        }

            //        ndr[5] = ((float)((num1 - num2) / num1) * 100).ToString("##0.00");//合格率
            //        ndr[6] = ((float)num2 / num1 * 100).ToString("##0.00");//不合格率

            //        tNum4 += num2;//不合格数
            //        tNum1 += num1;//总数
            //        tNum2 += num3;//仪器
            //        tNum3 += num4;//其他

            //        //去掉销毁字段
            //        ////DataColumn[] PrimaryKeyColumns4 = new DataColumn[1];
            //        ////PrimaryKeyColumns4[0] = dt3.Columns[0];
            //        ////dt3.PrimaryKey = PrimaryKeyColumns4;
            //        //pkcol[0] = dt3.Columns[0];
            //        //dt3.PrimaryKey = pkcol;
            //        //drfind = dt3.Rows.Find(findvalue);
            //        //if (drfind != null)
            //        //{
            //        //    ndr[7] = Convert.ToInt32(drfind[1]);
            //        //    drfind = null;
            //        //}
            //        //else
            //        //{
            //        //    ndr[7] = 0;
            //        //}
            //        dt6.Rows.Add(ndr);
            //    }
              
            //    if (dt6.Rows.Count > 0)
            //    {
            //        //新建最后一行，用于统计和计算
            //        ndr = dt6.NewRow();
            //        float temp = ((float)tNum4 / (float)tNum1) * 100;//不合格率统计
            //        ndr[0] = "合计/平均";
            //        ndr[1] = tNum1.ToString();//加上ToString()不会装箱
            //        ndr[2] = tNum2.ToString();
            //        ndr[3] = tNum3.ToString();
            //        ndr[4] = tNum4.ToString();
            //        ndr[5] = ((float)(tNum1 - tNum4) / tNum1 * 100).ToString("##0.00");
            //        ndr[6] = temp.ToString("##0.00");
            //        dt6.Rows.Add(ndr);

            //        this.c1FlexGrid1.DataSource = dt6;
            //        this.c1FlexGrid1.AutoSizeCols();
            //    }
            //    PrintOperation.PreviewGrid(this.c1FlexGrid1, "汇总报表", null);
            //}
            #endregion
        }

      
	}
}

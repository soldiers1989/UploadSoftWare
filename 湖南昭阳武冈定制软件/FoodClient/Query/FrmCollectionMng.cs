using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text;
using System.Data;
using System.Threading;
using DY.FoodClientLib;

namespace FoodClient
{
	/// <summary>
	/// frmPesticideMeasure ��ժҪ˵����
	/// </summary>
    public class FrmCollectionMng : System.Windows.Forms.Form
    {
        #region �ؼ�˽�б���
        private C1.Win.C1Command.C1Command c1Command5;
        private C1.Win.C1Command.C1Command cmdPrint;
        private C1.Win.C1Command.C1CommandLink c1CommandLink6;
        private C1.Win.C1Command.C1Command cmdExit;
        private C1.Win.C1Command.C1CommandLink c1CommandLink5;
        private C1.Win.C1Command.C1CommandLink c1CommandLink4;
        private C1.Win.C1Command.C1CommandDock c1CommandDock1;
        private C1.Win.C1Command.C1ToolBar c1ToolBar1;
        private C1.Win.C1Command.C1CommandHolder c1CommandHolder1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid1;
        private C1.Win.C1Command.C1Command c1Command1;
        private C1.Win.C1Command.C1Command cmdQuery;
 
        private C1.Win.C1Command.C1CommandLink c1CommandLink7;
        private C1.Win.C1Command.C1CommandLink c1CommandLink9;
        private C1.Win.C1Command.C1CommandMenu c1CommandMenu1;
        private C1.Win.C1Command.C1CommandLink c1CommandLink12;
        // private C1.Win.C1Command.C1Command cmdAdd01;
        private C1.Win.C1Command.C1CommandLink c1CommandLink13;
        private System.ComponentModel.IContainer components;

        private System.Windows.Forms.ToolTip toolTip1;
       // private C1.Win.C1Command.C1Command c1Command3;
        private C1.Win.C1Command.C1Command c1Command4;
        private C1.Win.C1Command.C1Command c1Command6;
        private C1.Win.C1Command.C1Command c1Command7;
        private C1.Win.C1Command.C1CommandMenu c1CommandMenu2;
        private C1.Win.C1Command.C1CommandLink c1CommandLink14;
        private C1.Win.C1Command.C1CommandLink c1CommandLink15;
        private C1.Win.C1Report.C1Report c1Report1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private C1.Win.C1Command.C1CommandLink c1CommandLink1;
        #endregion

        private FrmProgressBar prBar = null;
        private delegate bool IncreaseHandle(int nValue);
        private IncreaseHandle myIncrease = null;

        /// <summary>
        /// �����ⲿ����
        /// </summary>
        private string queryString;//��ϲ�ѯ����

        /// <summary>
        /// ���캯��
        /// </summary>
        public FrmCollectionMng()
        {
            InitializeComponent();
            //queryType = selectType;
            //resultBll = new clsResultOpr();
        }

        #region Windows ������������ɵĴ���
        /// <summary>
        /// ������������ʹ�õ���Դ��
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
        /// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
        /// �˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCollectionMng));
            this.c1Command5 = new C1.Win.C1Command.C1Command();
            this.cmdPrint = new C1.Win.C1Command.C1Command();
            this.c1CommandLink6 = new C1.Win.C1Command.C1CommandLink();
            this.cmdExit = new C1.Win.C1Command.C1Command();
            this.c1CommandLink5 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink4 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandDock1 = new C1.Win.C1Command.C1CommandDock();
            this.c1ToolBar1 = new C1.Win.C1Command.C1ToolBar();
            this.c1CommandHolder1 = new C1.Win.C1Command.C1CommandHolder();
            this.c1Command1 = new C1.Win.C1Command.C1Command();
            this.cmdQuery = new C1.Win.C1Command.C1Command();
            this.c1CommandMenu1 = new C1.Win.C1Command.C1CommandMenu();
            this.c1Command4 = new C1.Win.C1Command.C1Command();
            this.c1Command6 = new C1.Win.C1Command.C1Command();
            this.c1Command7 = new C1.Win.C1Command.C1Command();
            this.c1CommandMenu2 = new C1.Win.C1Command.C1CommandMenu();
            this.c1CommandLink14 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink15 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink1 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink7 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink9 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink12 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandLink13 = new C1.Win.C1Command.C1CommandLink();
            this.panel1 = new System.Windows.Forms.Panel();
            this.c1FlexGrid1 = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.c1Report1 = new C1.Win.C1Report.C1Report();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandDock1)).BeginInit();
            this.c1CommandDock1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Report1)).BeginInit();
            this.SuspendLayout();
            // 
            // c1Command5
            // 
            this.c1Command5.Name = "c1Command5";
            this.c1Command5.Text = "-";
            // 
            // cmdPrint
            // 
            this.cmdPrint.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrint.Image")));
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Shortcut = System.Windows.Forms.Shortcut.CtrlP;
            this.cmdPrint.Text = "��ӡ���м�¼";
            this.cmdPrint.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdPrint_Click);
            this.cmdPrint.Select += new System.EventHandler(this.cmdPrint_Select);
            // 
            // c1CommandLink6
            // 
            this.c1CommandLink6.Command = this.cmdExit;
            // 
            // cmdExit
            // 
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Shortcut = System.Windows.Forms.Shortcut.CtrlK;
            this.cmdExit.Text = "�˳�";
            this.cmdExit.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdExit_Click);
            this.cmdExit.Select += new System.EventHandler(this.cmdExit_Select);
            // 
            // c1CommandLink5
            // 
            this.c1CommandLink5.Command = this.cmdPrint;
            this.c1CommandLink5.Text = "��ӡ���ܽ��";
            // 
            // c1CommandLink4
            // 
            this.c1CommandLink4.Command = this.c1Command5;
            // 
            // c1CommandDock1
            // 
            this.c1CommandDock1.Controls.Add(this.c1ToolBar1);
            this.c1CommandDock1.Id = 2;
            this.c1CommandDock1.Location = new System.Drawing.Point(0, 0);
            this.c1CommandDock1.Name = "c1CommandDock1";
            this.c1CommandDock1.Size = new System.Drawing.Size(632, 24);
            // 
            // c1ToolBar1
            // 
            this.c1ToolBar1.CommandHolder = this.c1CommandHolder1;
            this.c1ToolBar1.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.c1CommandLink1,
            this.c1CommandLink7,
            this.c1CommandLink9,
            this.c1CommandLink5,
            this.c1CommandLink4,
            this.c1CommandLink6});
            this.c1ToolBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.c1ToolBar1.Location = new System.Drawing.Point(0, 0);
            this.c1ToolBar1.Movable = false;
            this.c1ToolBar1.Name = "c1ToolBar1";
            this.c1ToolBar1.ShowToolTips = false;
            this.c1ToolBar1.Size = new System.Drawing.Size(632, 24);
            this.c1ToolBar1.Text = "c1ToolBar1";
            // 
            // c1CommandHolder1
            // 
            this.c1CommandHolder1.Commands.Add(this.c1Command1);
            this.c1CommandHolder1.Commands.Add(this.cmdQuery);
            this.c1CommandHolder1.Commands.Add(this.c1Command5);
            this.c1CommandHolder1.Commands.Add(this.cmdPrint);
            this.c1CommandHolder1.Commands.Add(this.cmdExit);
            this.c1CommandHolder1.Commands.Add(this.c1CommandMenu1);
            this.c1CommandHolder1.Commands.Add(this.c1Command4);
            this.c1CommandHolder1.Commands.Add(this.c1Command6);
            this.c1CommandHolder1.Commands.Add(this.c1Command7);
            this.c1CommandHolder1.Commands.Add(this.c1CommandMenu2);
            this.c1CommandHolder1.Owner = this;
            this.c1CommandHolder1.SmoothImages = false;
            // 
            // c1Command1
            // 
            this.c1Command1.Name = "c1Command1";
            this.c1Command1.Text = "-";
            // 
            // cmdQuery
            // 
            this.cmdQuery.Image = ((System.Drawing.Image)(resources.GetObject("cmdQuery.Image")));
            this.cmdQuery.Name = "cmdQuery";
            this.cmdQuery.Text = "��ѯ";
            this.cmdQuery.Click += new C1.Win.C1Command.ClickEventHandler(this.cmdQuery_Click);
            this.cmdQuery.Select += new System.EventHandler(this.cmdQuery_Select);
            // 
            // c1CommandMenu1
            // 
            this.c1CommandMenu1.Name = "c1CommandMenu1";
            // 
            // c1Command4
            // 
            this.c1Command4.Image = ((System.Drawing.Image)(resources.GetObject("c1Command4.Image")));
            this.c1Command4.Name = "c1Command4";
            this.c1Command4.Text = "ת��ΪExcel";
            this.c1Command4.Click += new C1.Win.C1Command.ClickEventHandler(this.SaveAsExcel_Click);
            // 
            // c1Command6
            // 
            this.c1Command6.Image = ((System.Drawing.Image)(resources.GetObject("c1Command6.Image")));
            this.c1Command6.Name = "c1Command6";
            this.c1Command6.Text = "ת��ΪXML";
            this.c1Command6.Click += new C1.Win.C1Command.ClickEventHandler(this.SaveAsXML_Click);
            // 
            // c1Command7
            // 
            this.c1Command7.Name = "c1Command7";
            // 
            // c1CommandMenu2
            // 
            this.c1CommandMenu2.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.c1CommandLink14,
            this.c1CommandLink15});
            this.c1CommandMenu2.Image = ((System.Drawing.Image)(resources.GetObject("c1CommandMenu2.Image")));
            this.c1CommandMenu2.Name = "c1CommandMenu2";
            this.c1CommandMenu2.Text = "ת��";
            this.c1CommandMenu2.Select += new System.EventHandler(this.c1CommandMenu2_Select);
            // 
            // c1CommandLink14
            // 
            this.c1CommandLink14.Command = this.c1Command4;
            // 
            // c1CommandLink15
            // 
            this.c1CommandLink15.Command = this.c1Command6;
            // 
            // c1CommandLink1
            // 
            this.c1CommandLink1.Command = this.cmdQuery;
            // 
            // c1CommandLink7
            // 
            this.c1CommandLink7.Command = this.c1Command1;
            // 
            // c1CommandLink9
            // 
            this.c1CommandLink9.Command = this.c1CommandMenu2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.c1FlexGrid1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(632, 301);
            this.panel1.TabIndex = 10;
            // 
            // c1FlexGrid1
            // 
            this.c1FlexGrid1.AllowEditing = false;
            this.c1FlexGrid1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1FlexGrid1.AutoResize = false;
            this.c1FlexGrid1.ColumnInfo = "0,0,0,40,0,0,Columns:";
            this.c1FlexGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1FlexGrid1.Location = new System.Drawing.Point(0, 0);
            this.c1FlexGrid1.Name = "c1FlexGrid1";
            this.c1FlexGrid1.Rows.Count = 1;
            this.c1FlexGrid1.Rows.DefaultSize = 18;
            this.c1FlexGrid1.Rows.MinSize = 20;
            this.c1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1FlexGrid1.Size = new System.Drawing.Size(632, 301);
            this.c1FlexGrid1.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("c1FlexGrid1.Styles"));
            this.c1FlexGrid1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 325);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(632, 32);
            this.panel2.TabIndex = 11;
            // 
            // c1Report1
            // 
            this.c1Report1.ReportDefinition = resources.GetString("c1Report1.ReportDefinition");
            this.c1Report1.ReportName = "WorkSheetReport";
            // 
            // FrmCollectionMng
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(632, 357);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.c1CommandDock1);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmCollectionMng";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "����������ͳ��";//���������ͳ��
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmCollectionMng_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandDock1)).EndInit();
            this.c1CommandDock1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Report1)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// �򿪽���������
        /// </summary>
        private void ShowProcessBar()
        {
            prBar = new FrmProgressBar();
            // Init increase event
            myIncrease = new IncreaseHandle(prBar.Increase);
            prBar.ShowDialog();
            prBar = null;
        }

        /// <summary>
        /// ��ʼ���߳�
        /// </summary>
        private void ThreadFun()
        {
            MethodInvoker mi = new MethodInvoker(ShowProcessBar);
            this.BeginInvoke(mi);
            Thread.Sleep(500);//Sleep a while to show window
            bool blnIncreased = false;
            object objReturn = null;
            do
            {
                System.Threading.Thread.Sleep(10);
                objReturn = this.Invoke(this.myIncrease, new object[] { 2 });
                blnIncreased = (bool)objReturn;
            }
            while (blnIncreased);
        }

        /// <summary>
        /// �������ʱ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmCollectionMng_Load(object sender, System.EventArgs e)
        {
            //style1 = c1FlexGrid1.Styles.Add("style1");
            //style1.BackColor = pnlNoEligible.BackColor;
            queryString = string.Empty;
            refreshGrid(queryString);
        }

        /// <summary>
        /// ��ѯɸѡ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdQuery_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            //frmResultQuery query = new frmResultQuery();
            frmCollection query = new frmCollection();
            DialogResult dr = query.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                queryString = query.QueryString;
                refreshGrid(queryString);
            }
        }

        /// <summary>
        /// �˳��ر�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            this.queryString = null;
            this.Close();
        }

        /// <summary>
        /// �������ݰ󶨿ؼ�
        /// </summary>
        /// <param name="queryStr"></param>
        private void refreshGrid(string queryStr)
        {
            if (string.IsNullOrEmpty(queryStr))//Ĭ��״̬����ʾ�ս����
            {
                return;
            }
            Cursor.Current = Cursors.WaitCursor;

            Thread thdSub = new Thread(new ThreadStart(ThreadFun));
            thdSub.Start();
            
            //FrmMsg msg = new FrmMsg("���ڻ���ͳ�ƣ����Ժ򣮣���");
            //msg.Show();

            //�˴���ȡ5�����ݿ⣬��Ҫ�Ľ�
            string dateQuery = queryStr;

            //if (string.IsNullOrEmpty(queryStr))//Ĭ��״̬��
            //{
            //    sb.Append(" CheckStartDate>= #");
            //    sb.Append(DateTime.Now.AddMonths(-1).Date.ToString("yyyy-MM-dd"));
            //    sb.Append(" 00:00:00#");
            //    sb.Append(" And CheckStartDate<=#");
            //    sb.Append(DateTime.Now.Date.ToString("yyyy-MM-dd"));
            //    sb.Append(" 23:59:59#");
            //    strDateQuery = sb.ToString();
            //    sb.Length = 0;
            //}
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT T1.FoodCode, B.Name, T1.Count1 FROM [SELECT FoodCode,COUNT(FoodCode) AS Count1 FROM tResult WHERE ");
            sb.Append(dateQuery);
            sb.Append("	GROUP BY FoodCode]. AS T1 LEFT JOIN tFoodClass AS B ON T1.FoodCode = B.SysCode ORDER BY T1.FoodCode");


            DataTable dt1 = DataBase.ExecuteDataTable(sb.ToString());
            sb.Length = 0;

            sb.Append(" SELECT FoodCode,COUNT(FoodCode) FROM tResult WHERE Result='���ϸ�'");
            sb.Append(" AND ");
            sb.Append(dateQuery);
            sb.Append(" GROUP BY FoodCode ORDER BY FoodCode");
            DataTable dt2 = DataBase.ExecuteDataTable(sb.ToString());
            sb.Length = 0;

            //������������
            sb.Append("SELECT FoodCode,COUNT(FoodCode) FROM tResult WHERE (ResultType='������ֶ�' Or ResultType='������Զ�') ");
            sb.Append(" AND ");
            sb.Append(dateQuery);
            sb.Append(" GROUP BY FoodCode ORDER BY FoodCode");
            DataTable dt4 = DataBase.ExecuteDataTable(sb.ToString());
            sb.Length = 0;

            //��������ֶν����
            sb.Append("SELECT FoodCode,COUNT(FoodCode) FROM tResult WHERE ResultType<>'������ֶ�' AND ResultType<>'������Զ�'");
            sb.Append(" AND ");
            sb.Append(dateQuery);
            sb.Append(" GROUP BY FoodCode ORDER BY FoodCode");
            DataTable dt5 = DataBase.ExecuteDataTable(sb.ToString());
            sb.Length = 0;

            DataTable dt6 = new DataTable("ColTable");
            dt6.Columns.Add(ShareOption.SampleTitle, typeof(string));//0
            dt6.Columns.Add("�������", typeof(int));//1
            dt6.Columns.Add("��������������", typeof(int));//2
            dt6.Columns.Add("�����ֶμ������", typeof(int));//3
            dt6.Columns.Add("���ϸ�����", typeof(int));//4
            dt6.Columns.Add("�ϸ���(%)", typeof(float));//typeof(Decimal)
            dt6.Columns.Add("���ϸ���(%)", typeof(float));//typeof(Decimal)

            int num1 = 0;//�������
            int num2 = 0;//���ϸ���
            int num3 = 0;//�Ǽ������
            int num4 = 0;
            int tNum1 = 0;//�������
            int tNum2 = 0;//�Ǽ����������
            int tNum3 = 0;//�������������������
            int tNum4 = 0;//������������
            object findvalue = null;
            DataRow drfind = null;
            DataRow ndr = null;
            DataColumn[] pkcol = new DataColumn[1];//������Ҫ�ظ�new �Ǳ�Ҫ����Ҫ����for�ڲ�ʹ��

            foreach (DataRow dr in dt1.Rows)
            {
                num2 = 0;
                ndr = dt6.NewRow();
                ndr[0] = dr[1].ToString();
                num1 = Convert.ToInt32(dr[2]);
                ndr[1] = num1.ToString();
                findvalue = dr[0].ToString();


                pkcol[0] = dt4.Columns[0];
                dt4.PrimaryKey = pkcol;

                drfind = dt4.Rows.Find(findvalue);
                if (drfind != null)
                {
                    num3 = Convert.ToInt32(drfind[1]);
                    ndr[2] = num3.ToString();
                    drfind = null;
                }
                else
                {
                    ndr[2] = 0;
                }
                pkcol[0] = dt5.Columns[0];
                dt5.PrimaryKey = pkcol;
                drfind = dt5.Rows.Find(findvalue);
                if (drfind != null)
                {
                    num4 = Convert.ToInt32(drfind[1]);
                    ndr[3] = num4.ToString();
                    drfind = null;
                }
                else
                {
                    ndr[3] = 0;
                }

                pkcol[0] = dt2.Columns[0];
                dt2.PrimaryKey = pkcol;
                drfind = dt2.Rows.Find(findvalue);
                if (drfind != null)
                {
                    num2 = Convert.ToInt32(drfind[1]);
                    ndr[4] = num2.ToString();
                    drfind = null;
                }
                else
                {
                    num2 = 0;
                    ndr[4] = 0;
                }

                ndr[5] = ((float)((num1 - num2) / num1) * 100).ToString("##0.00");//�ϸ���
                ndr[6] = ((float)num2 / num1 * 100).ToString("##0.00");//���ϸ���
                
                tNum4 += num2;//���ϸ���
                tNum1 += num1;//����
                tNum2 += num3;//����
                tNum3 += num4;//����
                dt6.Rows.Add(ndr);
            }

            if (dt6.Rows.Count > 0)
            {
                //�½����һ�У�����ͳ�ƺͼ���
                ndr = dt6.NewRow();
                float temp = ((float)tNum4 / (float)tNum1) * 100;//���ϸ���ͳ��
                ndr[0] = "�ϼ�/ƽ��";
                ndr[1] = tNum1.ToString();//����ToString()����װ��
                ndr[2] = tNum2.ToString();
                ndr[3] = tNum3.ToString();
                ndr[4] = tNum4.ToString();
                ndr[5] = ((float)(tNum1 - tNum4) / tNum1 * 100).ToString("##0.00");
                ndr[6] = temp.ToString("##0.00");
                dt6.Rows.Add(ndr);

                this.c1FlexGrid1.DataSource = dt6;
            }

            c1FlexGrid1.AutoSizeRows();
            c1FlexGrid1.AutoSizeCols();

            
            //msg.Close();
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPrint_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            PrintOperation.PreviewGrid(c1FlexGrid1, "���������ͳ��", null);
            refreshGrid(string.Empty);
        }

        private void c1CommandMenu2_Select(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(c1ToolBar1, c1CommandMenu2.Text);
        }

        private void cmdQuery_Select(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(c1ToolBar1, this.cmdQuery.Text);
        }

        private void c1CommandMenu1_Select(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(c1ToolBar1, this.c1CommandMenu1.Text);
        }

        private void cmdPrint_Select(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(c1ToolBar1, this.cmdPrint.Text);
        }

        private void cmdExit_Select(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(c1ToolBar1, this.cmdExit.Text);
        }


        /// <summary>
        /// ����Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAsExcel_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            this.saveFileDialog1.InitialDirectory = Application.StartupPath;
            this.saveFileDialog1.CheckPathExists = true;
            this.saveFileDialog1.DefaultExt = "xls";
            this.saveFileDialog1.Filter = "Excel�ļ�(*.xls)|*.xls|All files (*.*)|*.*";
            DialogResult dr = this.saveFileDialog1.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                try
                {
                    c1FlexGrid1.SaveExcel(this.saveFileDialog1.FileName, this.Text, C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells);
                }
                catch
                {
                    MessageBox.Show("ת��Excel�ļ�ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                MessageBox.Show("ת��Excel�ļ��ɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// ����XML�ļ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAsXML_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            this.saveFileDialog1.InitialDirectory = Application.StartupPath;
            this.saveFileDialog1.CheckPathExists = true;
            this.saveFileDialog1.DefaultExt = "xml";
            this.saveFileDialog1.Filter = "Xml�ļ�(*.xml)|*.xml|All files (*.*)|*.*";
            DialogResult dr = this.saveFileDialog1.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                try
                {
                    c1FlexGrid1.WriteXml(this.saveFileDialog1.FileName);
                }
                catch
                {
                    MessageBox.Show("ת��XML�ļ�ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                MessageBox.Show("ת��XML�ļ��ɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

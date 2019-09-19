using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorkstationDAL.Model;
using WorkstationModel.UpData;
using WorkstationModel.function;
using WorkstationDAL.UpLoadData;
using WorkstationBLL.Mode;


namespace WorkstationUI.function
{
    public partial class ucTaskSample : UserControl
    {
        private StringBuilder sb = new StringBuilder();
        private clsSetSqlData sql = new clsSetSqlData();
        private DataTable dt = null;
        private string err = "";
        public ucTaskSample()
        {
            InitializeComponent();
        }

        private void ucTaskSample_Load(object sender, EventArgs e)
        {
            dt = sql.GetQtask("", "s_sampling_date", 4);
            if (dt != null && dt.Rows.Count > 0)
            {
                CheckDatas.DataSource = dt;
                IniDataGrid();
            }
        }
        private void IniDataGrid()
        {
            CheckDatas.Columns["Tests"].Visible = false;
            CheckDatas.Columns["Tests"].HeaderText = "已选择";
            CheckDatas.Columns["sample_code"].HeaderText = "抽样单编号";
            CheckDatas.Columns["item_name"].HeaderText = "检测项目";
            CheckDatas.Columns["food_name"].HeaderText = "样品名称";
            CheckDatas.Columns["s_reg_name"].HeaderText = "被检单位";
            CheckDatas.Columns["s_ope_shop_code"].HeaderText = "档口号";
            CheckDatas.Columns["s_ope_shop_name"].HeaderText = "档口名称";
            CheckDatas.Columns["s_sampling_date"].HeaderText = "抽样时间";
            CheckDatas.Columns["t_task_title"].HeaderText = "任务名称";
            CheckDatas.Columns["Checktype"].HeaderText = "状态";
            CheckDatas.Columns["ID"].Visible = false;

            
        }

        
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                BtnUpdate.Enabled = false;
                int f = 0;
                string reqtime = "";
                dt = sql.GetRequestTime("RequestName='BuinessTime'", "", out err);//获取请求时间
                if (dt != null && dt.Rows.Count > 0)
                {
                    //reqtime = dt.Rows[0]["UpdateTime"].ToString();
                    sql.UpdateRequestTime(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "RequestName='BuinessTime'", "", 1, out err);
                }
                else
                {
                    sql.InsertResquestTime("'BuinessTime','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'", "", "", 1, out err);
                }

                string ruld = QuickInspectServing.GetServiceURL(Global.ServerAdd, 16);
                sb.Length = 0;
                sb.Append(ruld);
                sb.AppendFormat("?userToken={0}", Global.Token);
                sb.AppendFormat("&type={0}", "recevieSampling");
                sb.AppendFormat("&serialNumber={0}", Global.MachineSerialCode);//DY-3500_20171028101105
                sb.AppendFormat("&lastUpdateTime={0}", reqtime == "" ? "2000-01-01 00:00:01" : reqtime);//System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                sb.AppendFormat("&param1={0}", "");
                sb.AppendFormat("&param2={0}", "");
                sb.AppendFormat("&param3={0}", "");
                FilesRW.KLog(sb.ToString(), "发送", 4);
                string tasklist = QuickInspectServing.HttpsPost(sb.ToString());
                FilesRW.KLog(tasklist, "接收", 4);
                if (tasklist.Contains("msg") && tasklist.Contains("success"))
                {
                    ResultData Mresult = JsonHelper.JsonToEntity<ResultData>(tasklist);

                    Mtask mt = JsonHelper.JsonToEntity<Mtask>(Mresult.obj.ToString());
                    MMtask mk = new MMtask();

                    for (int i = 0; i < mt.result.Count; i++)
                    {
                        sb.Length = 0;
                        sb.AppendFormat("tid='{0}'", mt.result[i].id);
                        DataTable Istaksave = sql.GetQtask(sb.ToString(), "", 1);
                        if (Istaksave != null && Istaksave.Rows.Count > 0)
                        {
                            continue;
                        }
                        else
                        {
                            mk = mt.result[i];
                            mk.mokuai = "荧光检测";
                            mk.username = Global.ServerName;

                            sql.InsertKTask(mk);
                            f = f + 1;

                            //更新任务状态   边保存边更新  接收
                            int index = Global.ServerAdd.LastIndexOf('/');
                            string addr = (index == Global.ServerAdd.Length - 1) ? Global.ServerAdd : Global.ServerAdd + "/";

                            string RcAddr = addr + "iSampling/updateStatus.do";
                            sb.Length = 0;
                            sb.Append(RcAddr);
                            sb.AppendFormat("?userToken={0}", Global.Token);
                            sb.AppendFormat("&sdId={0}", mt.result[i].id);
                            sb.AppendFormat("&recevieSerialNumber={0}", Global.MachineSerialCode);
                            sb.AppendFormat("&recevieStatus={0}", "1");
                            sb.AppendFormat("&param1={0}", "");
                            sb.AppendFormat("&param2={0}", "");
                            sb.AppendFormat("&param3={0}", "");
                            FilesRW.KLog(sb.ToString(), "发送", 16);
                            string upd = QuickInspectServing.HttpsPost(sb.ToString());
                            FilesRW.KLog(sb.ToString(), "接收", 16);
                            ResultData ut = JsonHelper.JsonToEntity<ResultData>(upd);
                        }
                    }
                }
                MessageBox.Show("共成功下载 "+f+" 条抽样任务！");
                dt = sql.GetQtask("", "", 4);
                if (dt != null && dt.Rows.Count > 0)
                {

                    CheckDatas.DataSource = dt;
                    IniDataGrid();
                }
                BtnUpdate.Enabled = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"抽样任务更新",MessageBoxButtons.OK ,MessageBoxIcon.Error );
                BtnUpdate.Enabled = true;
            }
           
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                btnSearch.Enabled = false;
                sb.Length = 0;
                sb.AppendFormat("s_sampling_date like '{0}'",dTStart.Value.ToString("yyy-MM-dd"));
                CheckDatas.DataSource = dt;

                dt = sql.GetQtask("", "s_sampling_date", 4);
                if (dt != null && dt.Rows.Count > 0)
                {
                    CheckDatas.DataSource = dt;
                    IniDataGrid();
                }
                btnSearch.Enabled = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                btnSearch.Enabled = true;
            }
  
        }

        private void CheckDatas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (CheckDatas.Rows[e.RowIndex].Cells["Tests"].Value.ToString() == "False")
                {
                    CheckDatas.Rows[e.RowIndex].Cells["Tests"].Value = true;
                }
                else
                {
                    CheckDatas.Rows[e.RowIndex].Cells["Tests"].Value = false;
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            frmTask frm = new frmTask();
            frm.ShowDialog();
        }

    }
}

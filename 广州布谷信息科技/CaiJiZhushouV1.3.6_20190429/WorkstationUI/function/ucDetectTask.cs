using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorkstationBLL.Mode;
using WorkstationModel.UpData;
using WorkstationDAL.Model;
using WorkstationModel.function;
using WorkstationDAL.UpLoadData;

namespace WorkstationUI.function
{
    public partial class ucDetectTask : UserControl
    {
        private string err = "";
        private StringBuilder sb = new StringBuilder();
        private clsSetSqlData sql = new clsSetSqlData();
        private DataTable dt = null;
        private ResultData resultd = null;
        public ucDetectTask()
        {
            InitializeComponent();
        }

        private void ucDetectTask_Load(object sender, EventArgs e)
        {
            dt = sql.GetTestTask("", "t_create_date", 2, out err);
            if(dt!=null && dt.Rows.Count>0)
            {
                CheckDatas.DataSource = dt;
                IniDataGridd();
            }
        }



        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                BtnUpdate.Enabled = false;
                string reqtime = string.Empty;
                dt = sql.GetRequestTime("RequestName='TaskManageTime'", "", out err);//获取请求时间
                if (dt != null && dt.Rows.Count > 0)
                {
                    //reqtime = dt.Rows[0]["UpdateTime"].ToString();
                    sql.UpdateRequestTime(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "RequestName='TaskManageTime'", "", 1, out err);
                }
                else
                {
                    sql.InsertResquestTime("'TaskManageTime','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'", "", "", 1, out err);
                }
                int count = 0;
                sb.Length = 0;
                string url = QuickInspectServing.GetServiceURL(Global.ServerAdd, 11);
                sb.Append(url);
                sb.AppendFormat("?userToken={0}", Global.Token);
                sb.AppendFormat("&lastUpdateTime={0}", reqtime == "" ? "2000-01-01 00:00:01" : reqtime);
                sb.AppendFormat("&pageNumber={0}", "");

                FilesRW.KLog(sb.ToString(), "发送", 18);
                string rtndata = QuickInspectServing.HttpsPost(sb.ToString());
                FilesRW.KLog(rtndata, "接收", 18);

                if (rtndata.Contains("success") && rtndata.Contains("msg"))
                {
                      resultd = JsonHelper.JsonToEntity<ResultData>(rtndata);
                      if (resultd.success == true && resultd.msg == "操作成功")
                      {
                          ManageTaskn mt = JsonHelper.JsonToEntity<ManageTaskn>(resultd.obj.ToString());
                          if (mt != null && mt.tasks.Count > 0)
                          {
                              for (int i = 0; i < mt.tasks.Count; i++)
                              {
                                  ManageTaskTest mtt = mt.tasks[i];
                                  sql.InsertTask(mtt, Global.ServerName , out err);
                                  count = count + 1;
                              }
                              MessageBox.Show("任务更新完成，共成功下载 " + count + " 条任务！");
                          }
                      }
                }
                dt = sql.GetTestTask("", "t_create_date", 2, out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    CheckDatas.DataSource = dt;
                    IniDataGridd();
                }
                BtnUpdate.Enabled = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message );
                BtnUpdate.Enabled = true;
            }
           
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                sb.Length = 0;
                CheckDatas.DataSource = null;
                sb.AppendFormat("t_create_date like '{0}'", dTStart.Value.ToString("yyyy-MM-dd"));

                dt = sql.GetTestTask(sb.ToString(), "t_create_date", 2, out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    CheckDatas.DataSource = dt;
                    IniDataGridd();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 初始化表格
        /// </summary>
        private void IniDataGridd()
        {
            CheckDatas.Columns["t_task_title"].HeaderText = "任务名称";
            CheckDatas.Columns["t_task_type"].HeaderText = "任务类型";
            CheckDatas.Columns["t_task_source"].HeaderText = "任务来源";
            CheckDatas.Columns["d_item"].HeaderText = "检测项目";
            CheckDatas.Columns["d_sample"].HeaderText = "样品名称";
            CheckDatas.Columns["d_task_total"].HeaderText = "批次";
            CheckDatas.Columns["d_receive_node"].HeaderText = "接收检测点";
            CheckDatas.Columns["t_create_date"].HeaderText = "创建时间";
            CheckDatas.Columns["t_task_edate"].HeaderText = "结束日期";
            CheckDatas.Columns["ID"].Visible = false;
        }
    }
}

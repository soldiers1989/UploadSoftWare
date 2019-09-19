using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using DY.FoodClientLib;
using DY.Process;

namespace FoodClient
{
    /// <summary>
    /// 基础数据同步
    /// </summary>
    public partial class FrmBaseDataDownload : Form
    {
        public FrmBaseDataDownload()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 检测点编号
        /// </summary>
        private string stdCode = string.Empty;
        /// <summary>
        /// 行政机构编号
        /// </summary>
        private string districtCode = string.Empty;

        /// <summary>
        /// 窗口加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmBaseDataDownload_Load(object sender, EventArgs e)
        {
            string userCode = ShareOption.DefaultUserUnitCode;
            if (ShareOption.SystemVersion.Equals(ShareOption.EnterpriseVersion))//如果是企业版
            {
                stdCode = clsUserUnitOpr.GetNameFromCode("CompanyId", userCode);
            }
            else
            {
                stdCode = clsUserUnitOpr.GetStdCode(userCode);
            }

            districtCode = clsUserUnitOpr.GetNameFromCode("DistrictCode", userCode);
        }

        /// <summary>
        /// 全部下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAllDownload_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ShareOption.SysServerIP) || string.IsNullOrEmpty(ShareOption.SysServerID))
            {
                MessageBox.Show(this, "联网用户名和密码不能为空，请先到选项菜单中设置服务器地址与登录ID！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (stdCode.Equals(string.Empty))
            {
                MessageBox.Show(this, "未设置默认的检测单位，请设置！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            //基础数据网络同步更新
            if (MessageBox.Show(this, "全部基础数据同步需要较长时间，真要执行同步吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            Cursor = Cursors.WaitCursor;
            groupBox1.Enabled = false;

            PercentProcess process = new PercentProcess();
            process.BackgroundWork = this.DownloadAllProcess;
            process.MessageInfo = "正在执行中";
            process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(backgroundWorkerCompleted);
            process.Start();

        }

        private void DownloadAllProcess(Action<int> percent)
        {
            percent(0);
            //string error = string.Empty;
            //string ret = CommonOperation.DownloadAll(stdCode, districtCode, out error, "all",out p);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(CommonOperation.DownloadFoodClass(stdCode, districtCode));//样品类别
            percent(10);
            sb.AppendLine(CommonOperation.DownloadCheckComTypeOpr(stdCode, districtCode));//检测单位类别
            percent(20);
            sb.AppendLine(CommonOperation.DownloadStandardType(stdCode, districtCode));//检测标准类型
            percent(30);
            sb.AppendLine(CommonOperation.DownloadStandard(stdCode, districtCode));//检测标准
            percent(40);
            sb.AppendLine(CommonOperation.DownloadCheckItem(stdCode, districtCode)); //检测项目
            percent(50);
            sb.AppendLine(CommonOperation.DownloadCompanyKind(stdCode, districtCode)); //单位类别
            percent(60);
            sb.AppendLine(CommonOperation.DownloadDistrict(stdCode, districtCode)); //组织机构
            percent(70);
            sb.AppendLine(CommonOperation.DownloadProduceArea(stdCode, districtCode)); //产地
            percent(80);
            sb.AppendLine(CommonOperation.DownloadCompany(stdCode, districtCode));//单位信息
            percent(100);
            MessageBox.Show(sb.ToString());

            //if (error.Equals(string.Empty))
            //{
            //    //frmMain.IsLoadCheckedComSel = false;
            //    //frmMain.IsLoadCheckedUpperComSel = false;
            //    MessageBox.Show(this, ret, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);// "基础数据同步成功！"
            //}
            //else
            //{
            //    MessageBox.Show(this, "基础数据同步失败！错误信息：\r\n" + error, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

       private void backgroundWorkerCompleted(object sender, BackgroundWorkerEventArgs e)
        {
            if (e.BackGroundException == null)
            {
                groupBox1.Enabled = true;
                Cursor = Cursors.Default;
                //MessageBox.Show("执行完毕");
            }
            else
            {
                groupBox1.Enabled = true;
                Cursor = Cursors.Default;
                MessageBox.Show("异常:" + e.BackGroundException.Message);
            }
        }
     
        /// <summary>
        /// 下载食品类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFoodClass_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                groupBox1.Enabled = false;
                string ret = CommonOperation.DownloadFoodClass(stdCode, districtCode);
                MessageBox.Show(ret);//"成功下载{0}条数据" , 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            groupBox1.Enabled = true;
            Cursor = Cursors.Default;
        }
        /// <summary>
        /// 下载检测单位类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckComTypeOpr_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                groupBox1.Enabled = false;
                string ret = CommonOperation.DownloadCheckComTypeOpr(stdCode, districtCode);
                MessageBox.Show(ret);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            groupBox1.Enabled = true;
            Cursor = Cursors.Default;
        }
        /// <summary>
        /// 检测标准类别,检测标准，检测项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStandardType_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                groupBox1.Enabled = false;
                string ret = CommonOperation.DownloadStandardType(stdCode, districtCode)+"\r\n";
                ret += CommonOperation.DownloadStandard(stdCode, districtCode) + "\r\n";
                ret += CommonOperation.DownloadCheckItem(stdCode, districtCode);
                MessageBox.Show(ret);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            groupBox1.Enabled = true;
            Cursor = Cursors.Default;
        }

        ///// <summary>
        ///// 下载检测标准
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void btnStandard_Click(object sender, EventArgs e)
        //{
        //    Cursor = Cursors.WaitCursor;
        //    try
        //    {
        //        groupBox1.Enabled = false;
        //        string ret = CommonOperation.DownloadStandard(stdCode, districtCode);
        //        MessageBox.Show(ret);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    groupBox1.Enabled = true;
        //    Cursor = Cursors.Default;
        //}

        ///// <summary>
        ///// 下载检测项目
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void btnCheckItem_Click(object sender, EventArgs e)
        //{
        //    Cursor = Cursors.WaitCursor;
        //    try
        //    {
        //        groupBox1.Enabled = false;
        //        string ret = CommonOperation.DownloadCheckItem(stdCode, districtCode);
        //        MessageBox.Show(ret);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    groupBox1.Enabled = true;
        //    Cursor = Cursors.Default;
        //}

        /// <summary>
        /// 下载行政
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDistrict_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            groupBox1.Enabled = false;

            //PercentProcess process = new PercentProcess();
            //process.BackgroundWork = this.DownloadDistrictProcess;
            //process.MessageInfo = "正在执行中";
            //process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(backgroundWorkerCompleted);
            //process.Start();

            try
            {
                string ret = CommonOperation.DownloadDistrict(stdCode, districtCode);
                MessageBox.Show(ret);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            groupBox1.Enabled = true;
            Cursor = Cursors.Default;
        }
      

        //void DownloadDistrictProcess(Action<int> percent)
        //{
        //    try
        //    {
        //        percent(0);
        //        string ret = CommonOperation.DownloadDistrict(stdCode, districtCode);
        //        percent(100);
        //        MessageBox.Show(ret);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        /// <summary>
        /// 下载产品产地
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProduceArea_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            groupBox1.Enabled = false;

            PercentProcess process = new PercentProcess();
            process.BackgroundWork = this.DownloadProduceAreaProcess;
            process.MessageInfo = "正在执行中";
            process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(backgroundWorkerCompleted);
            process.Start();

        }
        void DownloadProduceAreaProcess(Action<int> percent)
        {
            try
            {
                percent(0);
                string ret = string.Empty;
                //CommonOperation.DownloadProduceArea(stdCode, districtCode);
                string sign = "ProduceArea";
                DataTable dtbl = null;
                dtbl = CommonOperation.GetJ2EEData(stdCode, districtCode, sign).Tables[sign];
                percent(10);
                if (dtbl == null)
                {
                    percent(100);
                    ret = "暂无数据";
                    MessageBox.Show(ret);
                    return;// "暂无数据";
                }
                int len = dtbl.Rows.Count;

                StringBuilder sb = new StringBuilder();
                string delErr = string.Empty;
                string err = string.Empty;

                clsProduceAreaOpr bll = new clsProduceAreaOpr();
                bll.Delete("IsReadOnly=true", out delErr);
                percent(12);
                sb.Append(delErr);

                clsProduceArea model = new clsProduceArea();
                int k = 1;
                k = (len / (100 - 12) + 1);
                for (int i = 0; i < len; i++)
                {
                    err = string.Empty;
                    model.SysCode = dtbl.Rows[i]["SysCode"].ToString();
                    model.StdCode = dtbl.Rows[i]["StdCode"].ToString();
                    model.Name = dtbl.Rows[i]["Name"].ToString();
                    model.ShortCut = dtbl.Rows[i]["ShortCut"].ToString();
                    model.DistrictIndex = Convert.ToInt64(dtbl.Rows[i]["DistrictIndex"]);
                    model.CheckLevel = dtbl.Rows[i]["CheckLevel"].ToString();
                    model.IsLocal = Convert.ToBoolean(dtbl.Rows[i]["IsLocal"]);
                    model.IsReadOnly = Convert.ToBoolean(dtbl.Rows[i]["IsReadOnly"]);
                    model.IsLock = Convert.ToBoolean(dtbl.Rows[i]["IsLock"]);
                    model.Remark = string.Empty;

                    bll.Insert(model, out err);
                    if (!err.Equals(string.Empty))
                    {
                        sb.AppendLine(err);
                        //throw new Exception(err);
                        //continue;
                    }
                    percent(i / k + 12);
                }
                if (sb.Length > 0)
                {
                    ret = sb.ToString();
                }
                ret = string.Format("已经成功下载{0}条产品产地数据", len.ToString());
                percent(100);
                MessageBox.Show(ret);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 下载被检单位类别,被检单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCompanyKind_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                groupBox1.Enabled = false;
                string ret = CommonOperation.DownloadCompanyKind(stdCode, districtCode) + "\r\n";
                ret += CommonOperation.DownloadCompany(stdCode, districtCode);
                MessageBox.Show(ret);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            groupBox1.Enabled = true;
            Cursor = Cursors.Default;
        }

        ///// <summary>
        ///// 下载被检单位
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void btnCompany_Click(object sender, EventArgs e)
        //{
        //    Cursor = Cursors.WaitCursor;
        //    try
        //    {
        //        groupBox1.Enabled = false;
        //        string ret = CommonOperation.DownloadCompany(stdCode, districtCode);
        //        MessageBox.Show(ret);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    groupBox1.Enabled = true;
        //    Cursor = Cursors.Default;
        //}
    }
}

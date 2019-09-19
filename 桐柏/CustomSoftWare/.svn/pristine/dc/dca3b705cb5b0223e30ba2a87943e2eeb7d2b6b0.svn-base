using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DY.FoodClientLib.Model;

namespace FoodClient.Test
{
    public partial class BaseInfosForm : Form
    {
        public BaseInfosForm()
        {
            InitializeComponent();
        }

        private void BaseInfosForm_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            BindingList<clsBaseInfos> _BindingList = new BindingList<clsBaseInfos>(getList());
            this.dataGridView.DataSource = _BindingList;
            this.dataGridView.MultiSelect = false;
        }

        private IList<clsBaseInfos> getList()
        {
            IList<clsBaseInfos> list = new List<clsBaseInfos>();
            clsBaseInfos model = new clsBaseInfos();
            model.ID = 1;
            model.TITLE = "市食品药品监管局采取五项措施确保第119届广交会圆满完成";
            model.PDATE = "2016-5-9";
            model.AUTHOR = "食品监管执法分局 蒋一芸";
            model.PUBLISHER = "蒋一芸";
            model.STATUSES = "未读";
            model.CONTENT = "2016年5月5日，为确保第119届广交会在广州顺利召开。市食品药品监管局采取五项措施，狠抓广交会期间食品安全监工作，确保展会期间食品安全秩序良好，避免发生食品安全事故。";
            model.CARNAME = "DY-9900智能多功能食品安全快检车";
            model.INFORTYPE = "通知文告";
            model.EDATE = "2016-5-9";
            model.SDATE = "2016-5-9";
            model.VNUM = "0";
            list.Add(model);

            model = new clsBaseInfos();
            model.ID = 2;
            model.TITLE = "市食品药品监管局召开保化监管工作暨风险分析研讨会";
            model.PDATE = "2016-5-6";
            model.AUTHOR = "保健食品化妆品安全监管处 肖昌稳";
            model.PUBLISHER = "肖昌稳";
            model.STATUSES = "已读";
            model.CONTENT = "近日，市食品药品监管局召开保健食品化妆品监管工作暨风险分析研讨会,市局有关处室、各区局、直属分局，市药品检验所、食品检验所、审评认证中心等单位分管领导及业务骨干共80人参加会议";
            model.CARNAME = "DY-9900智能多功能食品安全快检车";
            model.INFORTYPE = "通知文告";
            model.EDATE = "2016-5-6";
            model.SDATE = "2016-5-6";
            model.VNUM = "1";
            list.Add(model);

            //model = new clsBaseInfos();
            //model.ID = 3;
            //model.TITLE = "标题";
            //model.PDATE = "发布时间";
            //model.AUTHOR = "作者";
            //model.PUBLISHER = "发布人";
            //model.STATUSES = "状态";
            //model.CONTENT = "信息内容";
            //model.CARNAME = "接受检测车或仪器";
            //model.INFORTYPE = "信息类别";
            //model.EDATE = "编辑时间";
            //model.SDATE = "固顶时间";
            //model.VNUM = "浏览次数";
            list.Add(model);

            return list;
        }

        private void btn_updateBaseInfos_Click(object sender, EventArgs e)
        {
            MessageBox.Show("暂无通知可更新！", "操作提示");
        }

        private void btn_ToView_Click(object sender, EventArgs e)
        {
            edit();
        }

        private void tsb_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void edit()
        {
            if (dataGridView.SelectedRows.Count <= 0 && dataGridView.SelectedRows.Count <= 0)
            {
                MessageBox.Show("当前未选择任何数据！");
                return;
            }

            try
            {
                if (dataGridView.DataSource != null)
                {
                    clsBaseInfos model = new clsBaseInfos();
                    model.TITLE = this.dataGridView.CurrentRow.Cells["TITLE"].Value.ToString();
                    model.PDATE = this.dataGridView.CurrentRow.Cells["PDATE"].Value.ToString();
                    model.AUTHOR = this.dataGridView.CurrentRow.Cells["AUTHOR"].Value.ToString();
                    model.PUBLISHER = this.dataGridView.CurrentRow.Cells["PUBLISHER"].Value.ToString();
                    model.STATUSES = this.dataGridView.CurrentRow.Cells["STATUSES"].Value.ToString();
                    model.CONTENT = this.dataGridView.CurrentRow.Cells["CONTENT"].Value.ToString();
                    model.CARNAME = this.dataGridView.CurrentRow.Cells["CARNAME"].Value.ToString();
                    model.INFORTYPE = this.dataGridView.CurrentRow.Cells["INFORTYPE"].Value.ToString();
                    model.EDATE = this.dataGridView.CurrentRow.Cells["EDATE"].Value.ToString();
                    model.SDATE = this.dataGridView.CurrentRow.Cells["SDATE"].Value.ToString();
                    model.VNUM = this.dataGridView.CurrentRow.Cells["VNUM"].Value.ToString();

                    ShowBaseInfo window = new ShowBaseInfo();
                    window._model = model;
                    window.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常！\n" + ex.Message);
            }
        }

        private void dataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            edit();
        }
    }
}

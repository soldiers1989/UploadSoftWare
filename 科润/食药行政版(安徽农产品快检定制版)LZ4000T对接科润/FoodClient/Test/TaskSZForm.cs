using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DY.FoodClientLib.Model;

namespace FoodClient.Test
{
    public partial class TaskSZForm : Form
    {
        public TaskSZForm()
        {
            InitializeComponent();
        }

        private void TaskSZForm_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            BindingList<clsTask> _BindingList = new BindingList<clsTask>(getList());
            this.dataGridView.DataSource = _BindingList;
            this.dataGridView.MultiSelect = false;
        }

        private IList<clsTask> getList() 
        {
            IList<clsTask> list = new List<clsTask>();
            clsTask model = new clsTask();
            model.ID = 1;
            model.CPTITLE = "农残检测";
            model.CPCODE = "DY20160513NC0001";
            model.CPSDATE = "2016-05-13";
            model.CPEDATE = "2016-05-16";
            model.CPTPROPERTY = "抽检";
            model.CPFROM = "食药局";
            model.CPPORG = "福田分局";
            model.CPPORGID = "GZFOODCODE05079477";
            model.CPEDITOR = "福田分局用户";
            model.CPEDDATE = "2016-05-13";
            model.PLANDETAIL = "检测项目：农药残留菊酯类 (抽检20份)；农药残留（农标）(抽检20份)；农药残留（国标）(抽检20份)；抽检样品：果蔬类食品及副食产品";
            model.CPMEMO = "暂无备注";
            model.PLANDCOUNT = "60";
            model.BAOJINGTIME = "2016-05-16";
            list.Add(model);

            model = new clsTask();
            model.ID = 2;
            model.CPTITLE = "劣质油检测";
            model.CPCODE = "DY20160513LZY0001";
            model.CPSDATE = "2016-05-12";
            model.CPEDATE = "2016-05-15";
            model.CPTPROPERTY = "抽检";
            model.CPFROM = "食药局";
            model.CPPORG = "福田分局";
            model.CPPORGID = "GZFOODCODE05079477";
            model.CPEDITOR = "福田分局用户";
            model.CPEDDATE = "2016-05-12";
            model.PLANDETAIL = "检测项目：食用油酸价 (抽检20份)；聚甘油脂肪酸酯 (抽检20份)；抽检样品：超市、个体户等中的食用油及其副产品";
            model.CPMEMO = "暂无备注";
            model.PLANDCOUNT = "40";
            model.BAOJINGTIME = "2016-05-15";
            list.Add(model);

            model = new clsTask();
            model.ID = 3;
            model.CPTITLE = "非法添加检测";
            model.CPCODE = "DY20160513FFTJ0001";
            model.CPSDATE = "2016-05-13";
            model.CPEDATE = "2016-05-16";
            model.CPTPROPERTY = "抽检";
            model.CPFROM = "食药局";
            model.CPPORG = "福田分局";
            model.CPPORGID = "GZFOODCODE05079477";
            model.CPEDITOR = "福田分局用户";
            model.CPEDDATE = "2016-05-13";
            model.PLANDETAIL = "检测项目：吊白块 (抽检10份)；甲醛 (抽检10份)；亚硝酸盐 (抽检10份)；面粉增白剂 (抽检10份)；抽检样品：常见食品随机抽取";
            model.CPMEMO = "暂无备注";
            model.PLANDCOUNT = "40";
            model.BAOJINGTIME = "2016-05-16";
            list.Add(model);

            return list;
        }

        /// <summary>
        /// 更新任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_updateTask_Click(object sender, EventArgs e)
        {
            MessageBox.Show("暂无新任务可更新！", "操作提示");
        }

        /// <summary>
        /// 编辑查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ToView_Click(object sender, EventArgs e)
        {
            edit();
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsb_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 上报任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Upload_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count <= 0 && dataGridView.SelectedRows.Count <= 0)
            {
                MessageBox.Show("当前未选择任何数据！");
                return;
            }

            MessageBox.Show("任务上报成功！", "操作提示");
        }

        private void dataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            edit();
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
                    clsTask model = new clsTask();
                    model.CPTITLE = this.dataGridView.CurrentRow.Cells["CPTITLE"].Value.ToString();
                    model.CPCODE = this.dataGridView.CurrentRow.Cells["CPCODE"].Value.ToString();
                    model.CPSDATE = this.dataGridView.CurrentRow.Cells["CPSDATE"].Value.ToString();
                    model.CPEDATE = this.dataGridView.CurrentRow.Cells["CPEDATE"].Value.ToString();
                    model.CPTPROPERTY = this.dataGridView.CurrentRow.Cells["CPTPROPERTY"].Value.ToString();
                    model.CPFROM = this.dataGridView.CurrentRow.Cells["CPFROM"].Value.ToString();
                    model.CPPORG = this.dataGridView.CurrentRow.Cells["CPPORG"].Value.ToString();
                    model.CPPORGID = this.dataGridView.CurrentRow.Cells["CPPORGID"].Value.ToString();
                    model.CPEDITOR = this.dataGridView.CurrentRow.Cells["CPEDITOR"].Value.ToString();
                    model.CPEDDATE = this.dataGridView.CurrentRow.Cells["CPEDDATE"].Value.ToString();
                    model.PLANDETAIL = this.dataGridView.CurrentRow.Cells["PLANDETAIL"].Value.ToString();
                    model.CPMEMO = this.dataGridView.CurrentRow.Cells["CPMEMO"].Value.ToString();
                    model.PLANDCOUNT = this.dataGridView.CurrentRow.Cells["PLANDCOUNT"].Value.ToString();
                    model.BAOJINGTIME = this.dataGridView.CurrentRow.Cells["BAOJINGTIME"].Value.ToString();

                    ShowTask window = new ShowTask();
                    window._clsTask = model;
                    window.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常！\n" + ex.Message);
            }
        }

    }
}

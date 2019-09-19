using System;
using System.Windows;
using com.lvrenyang;
using DYSeriesDataSet.DataModel;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// TaskDetailedWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TaskDetailedWindow : Window
    {
        public TaskDetailedWindow()
        {
            InitializeComponent();
        }

        private string logType = "TaskDetailedWindow-error";

        /// <summary>
        /// 初始化界面的值
        /// </summary>
        /// <param name="task">tlsTrTask</param>
        internal void GetValues(tlsTrTask task)
        {
            try
            {
                tCPTITLE.Text = task.CPTITLE;//任务主题
                tCPCODE.Text = task.CPCODE;//任务编号
                tCPSDATE.Text = task.CPSDATE;//开始时间
                tCPEDATE.Text = task.CPEDATE;//结束时间
                tCPTPROPERTY.Text = task.CPTPROPERTY;//任务性质
                tCPFROM.Text = task.CPFROM;//任务来源
                tCPPORG.Text = task.CPPORG;//机构名称
                tCPPORGID.Text = task.CPPORGID;//机构id
                tCPEDITOR.Text = task.CPEDITOR;//编制人
                tCPEDDATE.Text = task.CPEDDATE;//编制时间
                tPLANDETAIL.Text = task.PLANDETAIL;//任务内容
                tCPMEMO.Text = task.CPMEMO;//备注
                tPLANDCOUNT.Text = task.PLANDCOUNT;//计划抽检数量
                tBAOJINGTIME.Text = task.BAOJINGTIME;//报警时间
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
            }
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
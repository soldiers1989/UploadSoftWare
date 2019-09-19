using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using DYSeriesDataSet;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// BarRepairTaskWindow.xaml 的交互逻辑
    /// </summary>
    public partial class BarRepairTaskWindow : Window
    {
        public string model = string.Empty;//模块
        public string item = string.Empty;//检测项目
        public string SampleID = string.Empty;//样品ID
        public string SampleTime = string.Empty;//采样时间
        public string samplename = string.Empty;//样品名称
        private bool isSelect = false;

        public BarRepairTaskWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Global.modelkuai = "";
            cmbmokuai.Items.Clear();
            cmbmokuai.Items.Add("分光光度");
            cmbmokuai.Items.Add("胶体金");
            cmbmokuai.Items.Add("干化学");
            cmbmokuai.Text = model;
        
            txtItemName.Text = item;
            txtSameName.Text = samplename;
            txtGetSampleTime.Text = SampleTime;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Global.modelkuai = cmbmokuai.Text.Trim();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message );
            }
        }

        private void cmbmokuai_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isSelect)
            {
                tlsttResultSecondOpr _bll = new tlsttResultSecondOpr();
                bool isExit = false;
                string err = "";
                string selValue = cmbmokuai.SelectedValue.ToString();
                if (selValue == "分光光度")
                {

                    DataTable mdt = _bll.GetModel(txtItemName.Text.Trim(), "", out err);
                    if (mdt != null && mdt.Rows.Count > 0)
                    {
                        if (mdt.Rows[0]["project_type"].ToString() == "分光光度")
                        {
                            isExit = true;
                        }
                    }
                    if (isExit == false)
                    {
                        MessageBox.Show("请选择包含该检测项目的检测模块", "操作提示");
                        Global.modelkuai = model;
                        if (model == "胶体金")
                        {
                            cmbmokuai.SelectedIndex = 1;
                        }
                        else if (model == "干化学")
                        {
                            cmbmokuai.SelectedIndex = 2;
                        }
                    }
                }
                else if (selValue == "胶体金")
                {

                    DataTable mdt = _bll.GetModel(txtItemName.Text.Trim(), "", out err);
                    if (mdt != null && mdt.Rows.Count > 0)
                    {
                        if (mdt.Rows[0]["project_type"].ToString() == "胶体金")
                        {
                            isExit = true;
                        }
                    }
                    if (isExit == false)
                    {
                        MessageBox.Show("请选择包含该检测项目的检测模块", "操作提示");
                        Global.modelkuai = model;
                        if (model == "分光光度")
                        {
                            cmbmokuai.SelectedIndex = 0;
                        }
                        else if (model == "干化学")
                        {
                            cmbmokuai.SelectedIndex = 2;

                        }
                    }
                }
                else if (selValue == "干化学")
                {
                    DataTable mdt = _bll.GetModel(txtItemName.Text.Trim(), "", out err);
                    if (mdt != null && mdt.Rows.Count > 0)
                    {
                        if (mdt.Rows[0]["project_type"].ToString() == "干化学")
                        {
                            isExit = true;
                        }
                    }
                    if (isExit == false)
                    {
                        MessageBox.Show("请选择包含该检测项目的检测模块", "操作提示");
                        Global.modelkuai = model;
                        if (model == "分光光度")
                        {
                            cmbmokuai.SelectedIndex = 0;
                        }
                        else if (model == "胶体金")
                        {
                            cmbmokuai.SelectedIndex = 1;
                        }
                    }
                }
            }
        }

        private void cmbmokuai_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isSelect = true;
        }

    
    }
}

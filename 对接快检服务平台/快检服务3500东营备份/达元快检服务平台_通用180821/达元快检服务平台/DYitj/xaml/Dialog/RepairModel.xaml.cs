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
    /// RepairModel.xaml 的交互逻辑
    /// </summary>
    public partial class RepairModel : Window
    {
        public string model = string.Empty;//模块
        public string item = string.Empty;//检测项目
        public string SampleNum = string.Empty;//抽样编号
        public string SampleTime = string.Empty;//采样时间
        private bool isSelect = false;

        public RepairModel()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Global.modelkuai = "";
            CmbModel.Items.Clear();
            CmbModel.Items.Add("分光光度");
            CmbModel.Items.Add("胶体金");
            CmbModel.Items.Add("干化学");
            CmbModel.Text = model;
            txtGetTime.Text = SampleTime;
            txtGetNum.Text = SampleNum;
            txtItem.Text = item;

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 判断选择的模块是否包含该检测项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isSelect == true)
            {
                tlsttResultSecondOpr _bll = new tlsttResultSecondOpr();
                bool isExit = false;
                string err = "";
                string selValue = CmbModel.SelectedValue.ToString();
                if (selValue == "分光光度")
                {
                    
                    //for (int i = 0; i < Global.fgdItems.Count; i++)
                    //{
                    //    if (Global.fgdItems[i].Name == txtItem.Text.Trim())
                    //    {
                    //        Global.modelkuai = selValue;
                    //        isExit = true;
                    //    }
                    //}
                    DataTable mdt = _bll.GetModel(txtItem.Text.Trim(), "", out err);
                    if (mdt != null && mdt.Rows.Count > 0)
                    {
                        if (mdt.Rows[0]["project_type"].ToString() == "分光光度")
                        {
                            isExit = true;
                        }
                    }
                    if (isExit == false)
                    {
                        MessageBox.Show("请选择包含该检测项目的检测模块");
                        Global.modelkuai = model;
                        if (model == "胶体金")
                        {
                            CmbModel.SelectedIndex = 1;
                        }
                        else if (model == "干化学")
                        {
                            CmbModel.SelectedIndex = 2;
                        }
                    }
                }
                else if (selValue == "胶体金")
                {
                    //for (int i = 0; i < Global.jtjItems.Count; i++)
                    //{
                    //    if (Global.jtjItems[i].Name == txtItem.Text.Trim())
                    //    {
                    //        Global.modelkuai = selValue;
                    //        isExit = true;
                    //    }
                    //}
                    DataTable mdt = _bll.GetModel(txtItem.Text.Trim(), "", out err);
                    if (mdt != null && mdt.Rows.Count > 0)
                    {
                        if (mdt.Rows[0]["project_type"].ToString() == "胶体金")
                        {
                            isExit = true;
                        }
                    }
                    if (isExit == false)
                    {
                        MessageBox.Show("请选择包含该检测项目的检测模块");
                        Global.modelkuai = model;
                        if (model == "分光光度")
                        {
                            CmbModel.SelectedIndex = 0;
                        }
                        else if (model == "干化学")
                        {
                            CmbModel.SelectedIndex = 2;
                            
                        }
                    }
                }
                else if (selValue == "干化学")
                {
                    //for (int i = 0; i < Global.gszItems.Count; i++)
                    //{
                    //    if (Global.gszItems[i].Name == txtItem.Text.Trim())
                    //    {
                    //        Global.modelkuai = selValue;
                    //        isExit = true;
                    //    }
                    //}
                    DataTable mdt = _bll.GetModel(txtItem.Text.Trim(), "", out err);
                    if (mdt != null && mdt.Rows.Count > 0)
                    {
                        if (mdt.Rows[0]["project_type"].ToString() == "干化学")
                        {
                            isExit = true;
                        }
                    }
                    if (isExit == false)
                    {
                        MessageBox.Show("请选择包含该检测项目的检测模块");
                        Global.modelkuai = model;
                        if (model == "分光光度")
                        {
                            CmbModel.SelectedIndex = 0;
                        }
                        else if (model == "胶体金")
                        {
                            CmbModel.SelectedIndex = 1;
                        }
                    }
                }
            }
        }

        //private void CmbModel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    isSelect = true;
        //}

        //private void CmbModel_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    isSelect = true;
        //}

        private void CmbModel_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isSelect = true;
        }
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDetermine_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

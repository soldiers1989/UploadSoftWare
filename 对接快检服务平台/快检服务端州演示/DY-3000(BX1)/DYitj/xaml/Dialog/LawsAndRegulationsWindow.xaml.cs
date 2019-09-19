﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AIO.src;
using AIO.xaml.Print;
using com.lvrenyang;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// LawsAndRegulationsWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LawsAndRegulationsWindow : Window
    {
        public LawsAndRegulationsWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 当前页数
        /// </summary>
        private int _pageIndex = 1;
        /// <summary>
        /// 总记录数
        /// </summary>
        private int _datasum = 0;
        /// <summary>
        /// 总页数
        /// </summary>
        private int _pageSum = 0;
        /// <summary>
        /// 每页显示数
        /// </summary>
        private int _pageSize = 100;
        /// <summary>
        /// 是否重新计算分页
        /// </summary>
        private bool _IsCalcPage = true;
        /// <summary>
        /// 初次进入界面事件不执行查询
        /// </summary>
        private bool _IsFirstLoad = true;

        private List<LawsAndRegulations> BasicLawsAndRegulations = null;
        private List<LawsAndRegulations> LawsAndRegulationsList = null;
        private List<LawsAndRegulations> _SelectLawsAndRegulations = null;

        private string errMsg = string.Empty;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                #region 读取Excel数据
                
                //DataTable dtbl = ExcelHelper.ImportExcel(string.Format("{0}\\Data\\Data.xls", Environment.CurrentDirectory), 1);
                //if (dtbl != null && dtbl.Rows.Count > 0)
                //{
                //    BasicLawsAndRegulations = (List<LawsAndRegulations>)IListDataSet.DataTableToIList<LawsAndRegulations>(dtbl);
                //    List<LawsAndRegulations> models = new List<LawsAndRegulations>();
                //    for (int i = 0; i < BasicLawsAndRegulations.Count; i++)
                //    {
                //        if (BasicLawsAndRegulations[i].Name.Length > 0)
                //        {
                //            models.Add(BasicLawsAndRegulations[i]);
                //        }
                //    }
                //    BasicLawsAndRegulations = models;
                //    DataGridRecord.DataContext = BasicLawsAndRegulations;
                //}
                #endregion
                LoadStandard();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("出现异常！\r\n\r\n异常信息：{0}", ex.Message), "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadStandard()
        {
            string whereSql = string.Empty, val = string.Empty;

            if (!(bool)CboxGJFL.IsChecked)
            {
                val = CboxGJFL.Content.ToString();
                whereSql += whereSql.Length == 0 ? string.Format("Type <> '{0}'", val) : string.Format(" And Type <> '{0}'", val);
            }

            if (!(bool)CboxGJFG.IsChecked)
            {
                val = CboxGJFG.Content.ToString();
                whereSql += whereSql.Length == 0 ? string.Format("Type <> '{0}'", val) : string.Format(" And Type <> '{0}'", val);
            }

            if (!(bool)CboxDFFG.IsChecked)
            {
                val = CboxDFFG.Content.ToString();
                whereSql += whereSql.Length == 0 ? string.Format("Type <> '{0}'", val) : string.Format(" And Type <> '{0}'", val);
            }

            if (!(bool)CboxXGFG.IsChecked)
            {
                val = CboxXGFG.Content.ToString();
                whereSql += whereSql.Length == 0 ? string.Format("Type <> '{0}'", val) : string.Format(" And Type <> '{0}'", val);
            }

            if (!(bool)CboxFGDT.IsChecked)
            {
                val = CboxFGDT.Content.ToString();
                whereSql += whereSql.Length == 0 ? string.Format("Type <> '{0}'", val) : string.Format(" And Type <> '{0}'", val);
            }

            if (!(bool)CboxNewUse.IsChecked)
            {
                val = CboxNewUse.Content.ToString();
                whereSql += whereSql.Length == 0 ? string.Format("State <> '{0}'", val) : string.Format(" And State <> '{0}'", val);
            }

            if (!(bool)CboxTheUse.IsChecked)
            {
                val = CboxTheUse.Content.ToString();
                whereSql += whereSql.Length == 0 ? string.Format("State <> '{0}'", val) : string.Format(" And State <> '{0}'", val);
            }

            if (!(bool)CboxFailure.IsChecked)
            {
                val = CboxFailure.Content.ToString();
                whereSql += whereSql.Length == 0 ? string.Format("State <> '{0}'", val) : string.Format(" And State <> '{0}'", val);
            }

            whereSql = whereSql.Length > 0 ? string.Format("({0})", whereSql) : "";

            val = TxtName.Text.Trim();
            if (val.Length > 0)
            {
                whereSql += whereSql.Length == 0 ? string.Format("Name Like '%{0}%'", val) : string.Format(" And Name Like '%{0}%'", val);
            }

            //获取分页信息
            GetPageInfo(whereSql);

            DataTable dtbl = SqlHelper.GetDtblByPage("LawsAndRegulations", whereSql, "ReleaseTime DESC", _pageSize, _pageIndex);
            if (dtbl != null && dtbl.Rows.Count > 0)
            {
                DataGridRecord.DataContext = dtbl;
                labelCount.Content = string.Format("共{0}条数据,当前第{1}页,共{2}页", _datasum, _pageIndex, _pageSum);
            }
            else
            {
                DataGridRecord.DataContext = null;
                labelCount.Content = "";
            }
            _IsFirstLoad = false;
        }

        /// <summary>
        /// 获取分页信息
        /// </summary>
        private void GetPageInfo(string sql)
        {
            if (!_IsCalcPage) return;

            //获取分页信息
            DataTable dtbl = SqlHelper.GetDataTable("", "", out errMsg, "Select Count(ID) AS PageSun From LawsAndRegulations Where " + (sql.Length == 0 ? "1=1" : sql));
            if (dtbl != null && dtbl.Rows.Count > 0)
            {
                _datasum = int.Parse(dtbl.Rows[0]["PageSun"].ToString());
                //判定总页数
                if (_datasum % _pageSize == 0)
                    _pageSum = _datasum / _pageSize;
                else
                    _pageSum = _datasum / _pageSize + 1;
            }
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            if (pdfwindow != null) pdfwindow.Close();
            this.Close();
        }

        private void Btn_Show_Click(object sender, RoutedEventArgs e)
        {
            ShowContent();
        }

        private void Btn_Update_Click(object sender, RoutedEventArgs e)
        {
            Btn_Update.IsEnabled = false;
            TimerWindow window = new TimerWindow();
            window.lb_NTTimer.Content = "正在获取更新数据，请稍等···";
            window.ShowInTaskbar = false;
            window.ShowDialog();
            //Thread.Sleep(2000);
            //window.Close();
            MessageBox.Show("已更新为最新数据！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            Btn_Update.IsEnabled = true;
        }

        private void DataGridRecord_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void DataGridRecord_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridRecord.SelectedItems.Count == 0) return;
            _SelectLawsAndRegulations = new List<LawsAndRegulations>();

            for (int i = 0; i < DataGridRecord.SelectedItems.Count; i++)
            {
                LawsAndRegulations model = new LawsAndRegulations();
                model.Name = (DataGridRecord.SelectedItem as DataRowView).Row["Name"].ToString();
                model.Type = (DataGridRecord.SelectedItem as DataRowView).Row["Type"].ToString();
                model.ReleaseUnit = (DataGridRecord.SelectedItem as DataRowView).Row["ReleaseUnit"].ToString();
                model.ReleaseNum = (DataGridRecord.SelectedItem as DataRowView).Row["ReleaseNum"].ToString();
                model.ReleaseTime = (DataGridRecord.SelectedItem as DataRowView).Row["ReleaseTime"].ToString();
                model.ImplementationTime = (DataGridRecord.SelectedItem as DataRowView).Row["ImplementationTime"].ToString();
                model.FailureTime = (DataGridRecord.SelectedItem as DataRowView).Row["FailureTime"].ToString();
                model.State = (DataGridRecord.SelectedItem as DataRowView).Row["State"].ToString();
                model.Path = (DataGridRecord.SelectedItem as DataRowView).Row["Path"].ToString();
                model.Notes = (DataGridRecord.SelectedItem as DataRowView).Row["Notes"].ToString();
                _SelectLawsAndRegulations.Add(model);
            }
        }

        private void DataGridRecord_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowContent();
        }

        private void miShow_Click(object sender, RoutedEventArgs e)
        {
            ShowContent();
        }

        private string pdfUrl = "http://fst.chinafst.cn:9002/laws/pdf/";
        private CheckReportForm pdfwindow = null;

        private void ShowContent()
        {
            if (_SelectLawsAndRegulations.Count == 0)
            {
                MessageBox.Show("请选择需要查看的标准！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            try
            {
                //string path = string.Format(@"{0}\KnowledgeBbase\法律法规\{1}", Environment.CurrentDirectory, _SelectLawsAndRegulations[0].Path);
                //Process.Start(path);

                string path = pdfUrl + "法律法规/" + _SelectLawsAndRegulations[0].Path;
                if (pdfwindow != null) { pdfwindow.Close(); }
                pdfwindow = new CheckReportForm();
                pdfwindow.HtmlUrl = path;
                pdfwindow.IsLoadPdf = true;
                pdfwindow.ShowInTaskbar = false;
                pdfwindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("加载法律法规时出现异常！\r\n异常信息：{0}", ex.Message), "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TxtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_IsFirstLoad) return;
            if (TxtName.Text.Trim().Length > 0)
            {
                _IsCalcPage = true;
                LoadStandard();
            }
        }

        private void CboxGJFL_Click(object sender, RoutedEventArgs e)
        {
            if (_IsFirstLoad) return;
            _IsCalcPage = true;
            LoadStandard();
        }

        private void CboxGJFG_Click(object sender, RoutedEventArgs e)
        {
            if (_IsFirstLoad) return;
            _IsCalcPage = true;
            LoadStandard();
        }

        private void CboxDFFG_Click(object sender, RoutedEventArgs e)
        {
            if (_IsFirstLoad) return;
            _IsCalcPage = true;
            LoadStandard();
        }

        private void CboxNewUse_Click(object sender, RoutedEventArgs e)
        {
            if (_IsFirstLoad) return;
            _IsCalcPage = true;
            LoadStandard();
        }

        private void CboxTheUse_Click(object sender, RoutedEventArgs e)
        {
            if (_IsFirstLoad) return;
            _IsCalcPage = true;
            LoadStandard();
        }

        private void CboxXGFG_Click(object sender, RoutedEventArgs e)
        {
            if (_IsFirstLoad) return;
            _IsCalcPage = true;
            LoadStandard();
        }

        private void CboxFGDT_Click(object sender, RoutedEventArgs e)
        {
            if (_IsFirstLoad) return;
            _IsCalcPage = true;
            LoadStandard();
        }

        private void SearchLawsAndRegulations()
        {
            if (BasicLawsAndRegulations == null || BasicLawsAndRegulations.Count == 0) return;
            LawsAndRegulationsList = new List<LawsAndRegulations>();

            //法律法规名称
            string val = TxtName.Text.Trim();
            bool IsName = val.Length > 0 ? true : false;
            //国家法律
            bool IsGJFL = (bool)CboxGJFL.IsChecked;
            //国家法规
            bool IsGJFG = (bool)CboxGJFG.IsChecked;
            //地方法规
            bool IsDFFG = (bool)CboxDFFG.IsChecked;
            //相关法规
            bool IsXGFG = (bool)CboxXGFG.IsChecked;
            //法规动态
            bool IsFGDT = (bool)CboxFGDT.IsChecked;
            //现行
            bool IsNewUse = (bool)CboxNewUse.IsChecked;
            //即将实施
            bool IsTheUse = (bool)CboxTheUse.IsChecked;

            for (int i = 0; i < BasicLawsAndRegulations.Count; i++)
            {
                if (IsName && BasicLawsAndRegulations[i].Name.IndexOf(TxtName.Text.Trim()) < 0)
                {
                    continue;
                }

                val = BasicLawsAndRegulations[i].Type;

                if (!IsGJFL && val.IndexOf(CboxGJFL.Content.ToString()) >= 0)
                {
                    continue;
                }

                if (!IsGJFG && val.IndexOf(CboxGJFG.Content.ToString()) >= 0)
                {
                    continue;
                }

                if (!IsDFFG && val.IndexOf(CboxDFFG.Content.ToString()) >= 0)
                {
                    continue;
                }

                if (!IsXGFG && val.IndexOf(CboxXGFG.Content.ToString()) >= 0)
                {
                    continue;
                }

                if (!IsFGDT && val.IndexOf(CboxFGDT.Content.ToString()) >= 0)
                {
                    continue;
                }

                val = BasicLawsAndRegulations[i].State;
                if (!IsNewUse && val.IndexOf(CboxNewUse.Content.ToString()) >= 0)
                {
                    continue;
                }

                if (!IsTheUse && val.IndexOf(CboxTheUse.Content.ToString()) >= 0)
                {
                    continue;
                }

                LawsAndRegulationsList.Add(BasicLawsAndRegulations[i]);
            }

            DataGridRecord.DataContext = LawsAndRegulationsList;
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (BasicLawsAndRegulations != null && BasicLawsAndRegulations.Count > 0)
            {
                string path = string.Empty;
                int count = 0;
                for (int i = 0; i < BasicLawsAndRegulations.Count; i++)
                {
                    try
                    {
                        path = string.Format(@"{0}\KnowledgeBbase\法律法规\{1}", Environment.CurrentDirectory, BasicLawsAndRegulations[i].Path);
                        if (!File.Exists(path))
                        {
                            count++;
                            //FileUtils.OprLog(6, "法律法规", string.Format("{0}:{1}", count, BasicLawsAndRegulations[i].Path));
                            FileUtils.ErrorLog("法律法规", string.Format("{0}", BasicLawsAndRegulations[i].Path));
                            Label.Content = count.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        FileUtils.OprLog(6, "法律法规", string.Format("Error:{0}\r\nPath:{1}", ex.Message, path));
                        count++;
                        Label.Content = count.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHomePage_Click(object sender, RoutedEventArgs e)
        {
            if (_IsFirstLoad) return;
            _pageIndex = 1;
            _IsFirstLoad = true;
            textBoxPage.Text = _pageIndex.ToString();
            _IsCalcPage = false;
            LoadStandard();
        }

        /// <summary>
        /// 末页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEndPage_Click(object sender, RoutedEventArgs e)
        {
            if (_IsFirstLoad) return;
            _pageIndex = _pageSum;
            _IsFirstLoad = true;
            textBoxPage.Text = _pageIndex.ToString();
            _IsCalcPage = false;
            LoadStandard();
        }

        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpPage_Click(object sender, RoutedEventArgs e)
        {
            if (_IsFirstLoad) return;
            if (_pageIndex > 1)
            {
                _pageIndex--;
            }
            _IsFirstLoad = true;
            textBoxPage.Text = _pageIndex.ToString();
            _IsCalcPage = false;
            LoadStandard();
        }

        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (_IsFirstLoad) return;
            if (_pageIndex < _pageSum)
            {
                _pageIndex++;
            }
            _IsFirstLoad = true;
            textBoxPage.Text = _pageIndex.ToString();
            _IsCalcPage = false;
            LoadStandard();
        }

        /// <summary>
        /// 跳转页数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxPage_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_IsFirstLoad) return;
            if (!int.TryParse(textBoxPage.Text.Trim(), out _pageIndex))
            {
                textBoxPage.Text = "1";
                return;
            }
            if (_pageIndex > _pageSum)
            {
                _pageIndex = _pageSum;
                textBoxPage.Text = _pageIndex + "";
            }
            else if (_pageIndex < 0)
            {
                _pageIndex = 0;
                textBoxPage.Text = "0";
            }
            _IsCalcPage = false;
            LoadStandard();
        }

        /// <summary>
        /// 每页显示条数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtPageSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_IsFirstLoad) return;
            if (!int.TryParse(TxtPageSize.Text.Trim(), out _pageSize))
            {
                TxtPageSize.Text = "100";
                return;
            }

            _pageIndex = 1;
            textBoxPage.Text = "1";
            _IsCalcPage = true;
            LoadStandard();
        }

    }

    #region
    public class LawsAndRegulations
    {
        private string _Name = string.Empty;
        /// <summary>
        /// 法规名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _Type = string.Empty;
        /// <summary>
        /// 法规类型
        /// </summary>
        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        private string _ReleaseUnit = string.Empty;
        /// <summary>
        /// 发布单位
        /// </summary>
        public string ReleaseUnit
        {
            get { return _ReleaseUnit; }
            set { _ReleaseUnit = value; }
        }

        private string _ReleaseTime = string.Empty;
        /// <summary>
        /// 发布时间
        /// </summary>
        public string ReleaseTime
        {
            get
            {
                try
                {
                    return _ReleaseTime.Length > 0 ? DateTime.Parse(_ReleaseTime).ToString("yyyy-MM-dd") : _ReleaseTime;
                }
                catch (Exception)
                {
                    return _ReleaseTime;
                }
            }
            set { _ReleaseTime = value; }
        }

        private string _ReleaseNum = string.Empty;
        /// <summary>
        /// 发布文号
        /// </summary>
        public string ReleaseNum
        {
            get { return _ReleaseNum; }
            set { _ReleaseNum = value; }
        }

        private string _ImplementationTime = string.Empty;
        /// <summary>
        /// 实施时间
        /// </summary>
        public string ImplementationTime
        {
            get
            {
                try
                {
                    return _ImplementationTime.Length > 0 ? DateTime.Parse(_ImplementationTime).ToString("yyyy-MM-dd") : _ImplementationTime;
                }
                catch (Exception)
                {
                    return _ImplementationTime;
                }
            }
            set { _ImplementationTime = value; }
        }

        private string _FailureTime = string.Empty;
        /// <summary>
        /// 失效日期
        /// </summary>
        public string FailureTime
        {
            get { return _FailureTime; }
            set { _FailureTime = value; }
        }

        private string _State = string.Empty;
        /// <summary>
        /// 状态
        /// </summary>
        public string State
        {
            get { return _State; }
            set { _State = value; }
        }

        private string _Notes = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        public string Notes
        {
            get { return _Notes; }
            set { _Notes = value; }
        }

        private string _Path = string.Empty;
        /// <summary>
        /// 文件路径
        /// </summary>
        public string Path
        {
            get { return _Path; }
            set { _Path = value; }
        }
    }
    #endregion
}

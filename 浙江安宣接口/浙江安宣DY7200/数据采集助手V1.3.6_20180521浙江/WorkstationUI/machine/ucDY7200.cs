﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorkstationUI.Basic;
using WorkstationDAL.Model;
using WorkstationBLL.Mode;
using WorkstationDAL.Basic;
using System.IO.Ports;
using WorkstationModel.Model;
using WorkstationModel.Instrument;
using WorkstationModel.UpData;
using WorkstationUI.function;
using System.Threading;
using System.Web.Script.Serialization;


namespace WorkstationUI.machine
{
    public partial class ucDY7200 : BasicContent
    {
        private clsSetSqlData sqlSet = new clsSetSqlData();
        private clsSaveResult resultdata = new clsSaveResult();
        private DataTable cdt = null;
        private ComboBox cmbAdd = new ComboBox();
        private ComboBox cmbChkItem = new ComboBox();//检测项目
        private ComboBox cmbSample = new ComboBox();//样品名称
        private ComboBox cmbChkUnit = new ComboBox();//检测单位
        private ComboBox cmbDetectUnit = new ComboBox();//被检单位
        private ComboBox cmbGetSampleAddr = new ComboBox();//采样地址
        private ComboBox cmbChker = new ComboBox();//检测员
        private ComboBox cmbDetectUnitNature = new ComboBox();//被检单位性质
        private ComboBox cmbProductCompany = new ComboBox();//生产单位
        private ComboBox cmbProductAddr = new ComboBox();//产地
        private StringBuilder strWhere = new StringBuilder();
        private delegate void InvokeDelegate(DataTable dtbl);
        private delegate void InvokeBtn(Button btn);
        //private clsDY7200 dy7200 = new clsDY7200();
        private DataTable dt=null ;
        private int isave = 0;//保存的记录数
        private DY7200 dy7200s = new DY7200();
        private string err = "";
        private Thread Serialrecive;
        public ucDY7200()
        {
            InitializeComponent();
        }

        private void ucDY7200_Load(object sender, EventArgs e)
        {
            //禁止双击列名排序
            LbTitle.Text = Global.ChkManchine;
            BtnReadHis.Enabled = false;
            BtnClear.Enabled = false;
            btnDatsave.Enabled = false;
            btnadd.Enabled = false;
           
            //btnadd.Visible = false;
            string err = string.Empty;
           
            string[] Port = SerialPort.GetPortNames();
            if (Port.Length == 0)
            {
                cmbCOMbox.Items.Add("没有COM口");
            }
            foreach (string c in SerialPort.GetPortNames())
            {
                cmbCOMbox.Items.Add(c);
            }
            cmbCOMbox.SelectedIndex = 0;

            cmbAdd.Visible = false;
            //cmbAdd.Items.Add("请选择...");
            //cmbAdd.Items.Add("输入");
            cmbAdd.Items.Add("以下相同");
            cmbAdd.Items.Add("删除");
            cmbAdd.KeyUp += cmbAdd_KeyUp;
            cmbAdd.SelectedIndexChanged += cmbAdd_SelectedIndexChanged;
            CheckDatas.Controls.Add(cmbAdd);

            //检测项目
            cmbChkItem.Visible = false;
            //cmbChkItem.Items.Add("请选择...");
            cmbChkItem.Items.Add("以下相同");
            cmbChkItem.Items.Add("删除");
            //cmbChkItem.SelectedIndex = 0;
            cmbChkItem.KeyUp += cmbChkItem_KeyUp;
            cmbChkItem.SelectedIndexChanged += cmbChkItem_SelectedIndexChanged;
            CheckDatas.Controls.Add(cmbChkItem);

            //样品名称
            cmbSample.Visible = false;
            //cmbSample.Items.Add("请选择...");
            cmbSample.Items.Add("以下相同");
            cmbSample.Items.Add("删除");
            //cmbSample.DropDownStyle = ComboBoxStyle.DropDownList;
            //cmbSample.SelectedIndex = 0;
            cmbSample.MouseClick += cmbSample_MouseClick;
            cmbSample.KeyUp += cmbSample_KeyUp;
            cmbSample.SelectedIndexChanged += cmbSample_SelectedIndexChanged;
            CheckDatas.Controls.Add(cmbSample);
            //检测单位
            cmbChkUnit.Items.Add("以下相同");
            cmbChkUnit.Items.Add("删除");
            cmbChkUnit.Visible = false;
            //cmbChkUnit.SelectedIndexChanged += cmbChkUnit_SelectedIndexChanged;
            cmbChkUnit.MouseClick += cmbChkUnit_MouseClick;
            cmbChkUnit.KeyUp += cmbChkUnit_KeyUp;
            CheckDatas.Controls.Add(cmbChkUnit);
            //被检单位              
            cmbDetectUnit.Items.Add("以下相同");
            cmbDetectUnit.Items.Add("删除");
            cmbDetectUnit.Visible = false;
            cmbDetectUnit.SelectedIndexChanged += cmbDetectUnit_SelectedIndexChanged;
            //cmbDetectUnit.MouseClick += cmbDetectUnit_MouseClick;
            cmbDetectUnit.KeyUp += cmbDetectUnit_KeyUp;
            CheckDatas.Controls.Add(cmbDetectUnit);
            //采样地址
            cmbGetSampleAddr.Items.Add("以下相同");
            cmbGetSampleAddr.Items.Add("删除");
            cmbGetSampleAddr.Visible = false;
            cmbGetSampleAddr.SelectedIndexChanged += cmbGetSampleAddr_SelectedIndexChanged;
            //cmbGetSampleAddr.MouseClick += cmbGetSampleAddr_MouseClick;
            cmbGetSampleAddr.KeyUp += cmbGetSampleAddr_KeyUp;
            CheckDatas.Controls.Add(cmbGetSampleAddr);
            //检测员
            cmbChker.Items.Add("以下相同");
            cmbChker.Items.Add("删除");
            cmbChker.Visible = false;
            //cmbChker.MouseClick += cmbChker_MouseClick;
            cmbChker.SelectedIndexChanged += cmbChker_SelectedIndexChanged;
            cmbChker.KeyUp += cmbChker_KeyUp;
            CheckDatas.Controls.Add(cmbChker);
            //产地
            cmbProductAddr.Items.Add("以下相同");
            cmbProductAddr.Items.Add("删除");
            cmbProductAddr.Visible = false;
            //cmbProductAddr.MouseClick += cmbProductAddr_MouseClick;
            cmbProductAddr.SelectedIndexChanged += cmbProductAddr_SelectedIndexChanged;
            cmbProductAddr.KeyUp += cmbProductAddr_KeyUp;
            CheckDatas.Controls.Add(cmbProductAddr);
            //生产单位
            cmbProductCompany.Items.Add("以下相同");
            cmbProductCompany.Items.Add("删除");
            cmbProductCompany.Visible = false;
            //cmbProductCompany.MouseClick += cmbProductCompany_MouseClick;
            cmbProductCompany.SelectedIndexChanged += cmbProductCompany_SelectedIndexChanged;
            cmbProductCompany.KeyUp += cmbProductCompany_KeyUp;
            CheckDatas.Controls.Add(cmbProductCompany);
            //被检单位性质
            cmbDetectUnitNature.Items.Add("以下相同");
            cmbDetectUnitNature.Items.Add("删除");
            cmbDetectUnitNature.Visible = false;
            cmbDetectUnitNature.MouseClick += cmbDetectUnitNature_MouseClick;
            cmbDetectUnitNature.KeyUp += cmbDetectUnitNature_KeyUp;
            CheckDatas.Controls.Add(cmbDetectUnitNature);

            string itemcode = string.Empty;

            if (Global.ChkManchine != "")
            {
                strWhere.Clear();
                strWhere.AppendFormat("WHERE Name='{0}'", Global.ChkManchine);
                DataTable dt = sqlSet.GetIntrument(strWhere.ToString(), out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    itemcode = dt.Rows[0][4].ToString();
                }
            }
            if (Global.Platform == "DYBus")//快检车
            {
                cdt = sqlSet.GetExamedUnit("", "", out err);
                if (cdt != null && cdt.Rows.Count > 0)
                {
                    for (int n = 0; n < cdt.Rows.Count; n++)
                    {
                        cmbDetectUnit.Items.Add(cdt.Rows[n]["regName"].ToString());//被检单位
                        cmbGetSampleAddr.Items.Add(cdt.Rows[n]["regAddress"].ToString());//采样地址
                        dy7200s.unitInfo[0, 1] = cdt.Rows[n]["regName"].ToString();
                        dy7200s.unitInfo[0, 3] = cdt.Rows[n]["regAddress"].ToString();
                    }
                }
            }
            else if (Global.Platform == "DYKJFW")//快检服务
            {
                cdt = sqlSet.GetRegulator("", "", 1);
                if (cdt != null && cdt.Rows.Count > 0)
                {
                    for (int n = 0; n < cdt.Rows.Count; n++)
                    {
                        if (cdt.Rows[n]["IsSelects"].ToString() == "True")
                        {
                            //dy7200s.unitInfo[0, 0] = cdt.Rows[n]["TestUnitName"].ToString();//检测单位
                            dy7200s.unitInfo[0, 1] = cdt.Rows[n]["reg_name"].ToString();//被检单位
                            //dy7200s.unitInfo[0, 2] = cdt.Rows[n]["Tester"].ToString();//检测人

                            dy7200s.unitInfo[0, 3] = cdt.Rows[n]["reg_type"].ToString();//被检单位性质
                            //dy7200s.unitInfo[0, 4] = cdt.Rows[n]["ProductAddr"].ToString();//产地
                            //dy7200s.unitInfo[0, 5] = cdt.Rows[n]["ProductCompany"].ToString();//生产单位
                        }
                        else
                        {
                            string conm = cdt.Rows[n]["IsSelects"].ToString();
                            string d = conm;
                        }
                    }
                }

            }
            else
            {
                cdt = sqlSet.GetInformation("", "", out err);

                if (cdt != null && cdt.Rows.Count > 0)
                {
                    for (int n = 0; n < cdt.Rows.Count; n++)
                    {
                        if (cdt.Rows[n]["iChecked"].ToString() == "是")
                        {
                            dy7200s.unitInfo[0, 0] = cdt.Rows[n]["TestUnitName"].ToString();//检测单位
                            dy7200s.unitInfo[0, 1] = cdt.Rows[n]["DetectUnitName"].ToString();//被检单位
                            dy7200s.unitInfo[0, 2] = cdt.Rows[n]["Tester"].ToString();//检测人
                           
                            dy7200s.unitInfo[0, 3] = cdt.Rows[n]["DetectUnitNature"].ToString();//被检单位性质
                            dy7200s.unitInfo[0, 4] = cdt.Rows[n]["ProductAddr"].ToString();//产地
                            dy7200s.unitInfo[0, 5] = cdt.Rows[n]["ProductCompany"].ToString();//生产单位
                        }
                        cmbChkUnit.Items.Add(cdt.Rows[n]["TestUnitName"].ToString());//检测单位
                        cmbDetectUnit.Items.Add(cdt.Rows[n]["DetectUnitName"].ToString());//被检单位
                        cmbDetectUnitNature.Items.Add(cdt.Rows[n]["DetectUnitNature"]);//被检单位性质
                        //cmbGetSampleAddr.Items.Add(cdt.Rows[n]["SampleAddress"].ToString());//采样地址
                        cmbChker.Items.Add(cdt.Rows[n]["Tester"].ToString());//检测员
                        cmbProductCompany.Items.Add(cdt.Rows[n]["ProductCompany"].ToString());//生产单位
                        cmbProductAddr.Items.Add(cdt.Rows[n]["ProductAddr"].ToString());//产地
                    }
                }
            }
            

            MessageNotification.GetInstance().DataRead += NotificationEventHandler;
        }

        //首先加载进获取系统PAI函数的引用：
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "GetDoubleClickTime")]
        public extern static int GetDoubleClickTime();//重写系统API函数获取鼠标双击的有效间隔
        private DateTime dtCmbLastClick = DateTime.MinValue;//存储两次单击的时间间隔
        /// <summary>
        /// combox双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSample_MouseClick(object sender, MouseEventArgs e)
        {
            if (DateTime.Now - dtCmbLastClick < new TimeSpan(0, 0, 0, 0, GetDoubleClickTime()))
            {
                // 双击事件处理方式
                try
                {
                    FrmSearchSample window = new FrmSearchSample();
                    window._item = CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测项目"].Value.ToString().Trim();
                    window.ShowDialog();
                    if (Global.iSampleName == "")
                    {
                        return;
                    }
                    CheckDatas.CurrentCell.Value = cmbSample.Text = Global.iSampleName;
                    string sql = "FtypeNmae='" + Global.iSampleName + "'" + " and  Name='" + CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测项目"].Value.ToString().Trim() + "'";
                    DataTable dt = sqlSet.GetChkItem(sql, "", out err);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测依据"].Value = dt.Rows[0][2].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
            else
            {
                dtCmbLastClick = DateTime.Now;
            }
        }

        /// <summary>
        /// 获取输入值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbAdd_KeyUp(object sender, KeyEventArgs e)
        {
            CheckDatas.CurrentCell.Value = cmbAdd.Text;
        }
        /// <summary>
        /// 选择给定的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbChkItem.Visible = false;
            cmbSample.Visible = false;
            if (((ComboBox)sender).Text == "删除")
            {
                CheckDatas.CurrentCell.Value = "";
                cmbAdd.Visible = false;
            }
            else if (((ComboBox)sender).Text == "以下相同")
            {
                for (int i = CheckDatas.CurrentCell.RowIndex; i < CheckDatas.Rows.Count; i++)
                {
                    CheckDatas.Rows[i].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = CheckDatas.CurrentCell.Value.ToString();
                }
                cmbAdd.Visible = false;
            }
            else if (((ComboBox)sender).Text == "输入")
            {
                cmbAdd.Text = "";
                CheckDatas.CurrentCell.Value = "";
            }
            else
            {
                CheckDatas.CurrentCell.Value = ((ComboBox)sender).Text;
                cmbAdd.Visible = false;
            }
        }
        //检测项目
        private void cmbChkItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSample.Visible = false;
            cmbAdd.Visible = false;
            if (cmbChkItem.Text == "以下相同")
            {
                cmbChkItem.Visible = false;
                for (int k = CheckDatas.CurrentCell.RowIndex; k < CheckDatas.Rows.Count; k++)
                {
                    CheckDatas.Rows[k].Cells[2].Value = CheckDatas.CurrentCell.Value.ToString();
                }
            }
            else if (cmbChkItem.Text == "删除")
            {
                CheckDatas.CurrentCell.Value = "";
                cmbChkItem.Visible = false;
            }
            else
            {
                CheckDatas.CurrentCell.Value = cmbChkItem.Text;
                cmbChkItem.Visible = false;
            }
        }

        private void cmbChkItem_KeyUp(object sender, KeyEventArgs e)
        {
            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = cmbChkItem.Text;
        }

        private void cmbSample_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbAdd.Visible = false;
            cmbChkItem.Visible = false;
            string err = string.Empty;

            if (Global.ChkManchine == "DY-6600手持执法快检通")
            {
                if (cmbSample.Text == "以下相同")
                {
                    for (int j = CheckDatas.CurrentCell.RowIndex; j < CheckDatas.Rows.Count; j++)
                    {

                        CheckDatas.Rows[j].Cells[1].Value = CheckDatas.CurrentCell.Value.ToString();

                        string sql = "sampleName='" + cmbSample.Text + "'" + " and  itemName='" + CheckDatas.Rows[j].Cells[2].Value.ToString() + "'";

                        DataTable dt = sqlSet.GetItemStandard(sql, "", out err);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测依据"].Value = dt.Rows[0][5].ToString();
                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["标准值"].Value = dt.Rows[0][6].ToString();
                            string symbol = dt.Rows[0][7].ToString();
                            switch (symbol)
                            {
                                case "≤":
                                    {
                                        if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) < Decimal.Parse(dt.Rows[0][6].ToString()) ||
                                            Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) == Decimal.Parse(dt.Rows[0][6].ToString()))
                                        {
                                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "合格";
                                        }
                                        else
                                        {
                                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "不合格";
                                        }
                                    }
                                    break;
                                case "<":
                                    {
                                        if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) < Decimal.Parse(dt.Rows[0][6].ToString()))
                                        {
                                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "合格";
                                        }
                                        else
                                        {
                                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "不合格";
                                        }
                                    }
                                    break;
                                case "≥":
                                    {
                                        if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) > Decimal.Parse(dt.Rows[0][6].ToString()) ||
                                            Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) == Decimal.Parse(dt.Rows[0][6].ToString()))
                                        {
                                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "合格";
                                        }
                                        else
                                        {
                                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "不合格";
                                        }
                                    }
                                    break;

                                default:
                                    break;
                            }
                        }
                    }
                }
                else if (cmbSample.Text == "删除")
                {
                    CheckDatas.CurrentCell.Value = "";
                    cmbSample.Visible = false;
                }
                else
                {
                    CheckDatas.CurrentCell.Value = cmbSample.Text;

                    string sql = "sampleName='" + cmbSample.Text + "'" + " and  itemName='" + CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[2].Value.ToString() + "'";

                    DataTable dt = sqlSet.GetItemStandard(sql, "", out err);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测依据"].Value = dt.Rows[0][5].ToString();
                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["标准值"].Value = dt.Rows[0][6].ToString();
                        string symbol = dt.Rows[0][7].ToString();
                        switch (symbol)
                        {
                            case "≤":
                                {
                                    if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) < Decimal.Parse(dt.Rows[0][6].ToString()) ||
                                        Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) == Decimal.Parse(dt.Rows[0][6].ToString()))
                                    {
                                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "合格";
                                    }
                                    else
                                    {
                                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "不合格";
                                    }
                                }
                                break;
                            case "<":
                                {
                                    if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) < Decimal.Parse(dt.Rows[0][6].ToString()))
                                    {
                                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "合格";
                                    }
                                    else
                                    {
                                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "不合格";
                                    }
                                }
                                break;
                            case "≥":
                                {
                                    if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) > Decimal.Parse(dt.Rows[0][6].ToString()) ||
                                        Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) == Decimal.Parse(dt.Rows[0][6].ToString()))
                                    {
                                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "合格";
                                    }
                                    else
                                    {
                                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "不合格";
                                    }
                                }
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            else
            {
                if (cmbSample.Text == "以下相同")
                {
                    try
                    {
                        for (int i = CheckDatas.CurrentCell.RowIndex; i < CheckDatas.Rows.Count; i++)
                        {
                            CheckDatas.Rows[i].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = CheckDatas.CurrentCell.Value.ToString();
                        }
                        cmbSample.Visible = false;
                       
                       
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }
                }
                else if (cmbSample.Text == "删除")
                {
                    CheckDatas.CurrentCell.Value = "";
                    cmbSample.Visible = false;
                }
                else
                {
                    try
                    {
                        CheckDatas.CurrentCell.Value = cmbSample.Text;
                        cmbSample.Visible = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }
                }
            }
        }
        private void cmbSample_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == 13)
            {
                try
                {
                    string err = string.Empty;
                    string sql = "FtypeNmae='" + cmbSample.Text + "'" + " and  Name='" + CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[2].Value.ToString() + "'";

                    DataTable dt = sqlSet.GetChkItem(sql, "", out err);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测依据"].Value = dt.Rows[0][2].ToString();
                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["标准值"].Value = dt.Rows[0][3].ToString();
                            string symbol = dt.Rows[0][4].ToString();
                            switch (symbol)
                            {
                                case "≤":
                                    {
                                        if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) < Decimal.Parse(dt.Rows[0][3].ToString()) ||
                                            Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) == Decimal.Parse(dt.Rows[0][3].ToString()))
                                        {
                                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "合格";
                                        }
                                        else
                                        {
                                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "不合格";
                                        }
                                    }
                                    break;
                                case "<":
                                    {
                                        if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) < Decimal.Parse(dt.Rows[0][3].ToString()))
                                        {
                                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "合格";
                                        }
                                        else
                                        {
                                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "不合格";
                                        }
                                    }
                                    break;
                                case "≥":
                                    {
                                        if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) > Decimal.Parse(dt.Rows[0][3].ToString()) ||
                                            Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) == Decimal.Parse(dt.Rows[0][3].ToString()))
                                        {
                                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "合格";
                                        }
                                        else
                                        {
                                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "不合格";
                                        }
                                    }
                                    break;

                                default:
                                    break;
                            }
                        }
                        else
                        {
                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测依据"].Value = "GB/T 5009.199-2003";
                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["标准值"].Value = "50";
                            if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) < 50)
                            {
                                CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "合格";
                            }
                            else
                            {
                                CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "不合格";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
            else
            {
                CheckDatas.CurrentCell.Value = cmbSample.Text;
            }
        }

        //检测单位按键弹起事件
        private void cmbChkUnit_KeyUp(object sender, KeyEventArgs e)
        {
            CheckDatas.CurrentCell.Value = cmbChkUnit.Text.Trim();
        }
        //检测单位单击事件
        private void cmbChkUnit_MouseClick(object sender, MouseEventArgs e)
        {

        }
        //检测单位选择事件
        private void cmbChkUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).Text == "删除")
            {
                cmbChkUnit.Text = "";
                CheckDatas.CurrentCell.Value = "";
                cmbChkUnit.Visible = false;
            }
            else if (((ComboBox)sender).Text == "以下相同")
            {
                for (int i = CheckDatas.CurrentCell.RowIndex; i < CheckDatas.Rows.Count; i++)
                {
                    CheckDatas.Rows[i].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = CheckDatas.CurrentCell.Value.ToString();
                }
                cmbChkUnit.Text = "";
                cmbChkUnit.Visible = false;
            }
            else if (((ComboBox)sender).Text == "输入")
            {
                cmbChkUnit.Text = "";
                CheckDatas.CurrentCell.Value = "";
            }
            else
            {
                CheckDatas.CurrentCell.Value = ((ComboBox)sender).Text;
                cmbChkUnit.Visible = false;
            }
        }

        //被检单位键盘弹起事件
        private void cmbDetectUnit_KeyUp(object sender, KeyEventArgs e)
        {
            CheckDatas.CurrentCell.Value = cmbDetectUnit.Text.Trim(); ;
        }
        //被检单位单击事件
        private void cmbDetectUnit_MouseClick(object sender, MouseEventArgs e)
        {

        }
        //被检单位选择事件
        private void cmbDetectUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).Text == "删除")
            {
                cmbDetectUnit.Text = "";
                CheckDatas.CurrentCell.Value = "";
                cmbDetectUnit.Visible = false;
            }
            else if (((ComboBox)sender).Text == "以下相同")
            {
                for (int i = CheckDatas.CurrentCell.RowIndex; i < CheckDatas.Rows.Count; i++)
                {
                    CheckDatas.Rows[i].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = CheckDatas.CurrentCell.Value.ToString();
                }
                cmbDetectUnit.Text = "";
                cmbDetectUnit.Visible = false;
            }
            else if (((ComboBox)sender).Text == "输入")
            {
                cmbDetectUnit.Text = "";
                CheckDatas.CurrentCell.Value = "";
                cmbDetectUnit.Visible = false;
            }
            else
            {
                CheckDatas.CurrentCell.Value = ((ComboBox)sender).Text;
                cmbDetectUnit.Visible = false;
            }
        }

        //采样地址选择事件
        private void cmbGetSampleAddr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).Text == "删除")
            {
                CheckDatas.CurrentCell.Value = "";
                cmbGetSampleAddr.Text = "";
                cmbGetSampleAddr.Visible = false;
            }
            else if (((ComboBox)sender).Text == "以下相同")
            {
                for (int i = CheckDatas.CurrentCell.RowIndex; i < CheckDatas.Rows.Count; i++)
                {
                    CheckDatas.Rows[i].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = CheckDatas.CurrentCell.Value.ToString();
                }
                cmbGetSampleAddr.Text = "";
                cmbGetSampleAddr.Visible = false;
            }
            else if (((ComboBox)sender).Text == "输入")
            {
                cmbGetSampleAddr.Text = "";
                CheckDatas.CurrentCell.Value = "";
                cmbGetSampleAddr.Visible = false;
            }
            else
            {
                CheckDatas.CurrentCell.Value = ((ComboBox)sender).Text;
                cmbGetSampleAddr.Visible = false;
            }
        }
        //采样地址弹起事件
        private void cmbGetSampleAddr_KeyUp(object sender, KeyEventArgs e)
        {
            CheckDatas.CurrentCell.Value = cmbGetSampleAddr.Text;
        }
        //采样地址单击事件
        private void cmbGetSampleAddr_MouseClick(object sender, MouseEventArgs e)
        {

        }

        //检测员按键弹起事件
        private void cmbChker_KeyUp(object sender, KeyEventArgs e)
        {
            CheckDatas.CurrentCell.Value = cmbChker.Text;
        }
        //检测员选择事件
        private void cmbChker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).Text == "删除")
            {
                CheckDatas.CurrentCell.Value = "";
                cmbChker.Text = "";
                cmbChker.Visible = false;
            }
            else if (((ComboBox)sender).Text == "以下相同")
            {
                for (int i = CheckDatas.CurrentCell.RowIndex; i < CheckDatas.Rows.Count; i++)
                {
                    CheckDatas.Rows[i].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = CheckDatas.CurrentCell.Value.ToString();
                }
                cmbChker.Text = "";
                cmbChker.Visible = false;
            }
            else if (((ComboBox)sender).Text == "输入")
            {
                cmbChker.Text = "";
                CheckDatas.CurrentCell.Value = "";
                cmbChker.Visible = false;
            }
            else
            {
                CheckDatas.CurrentCell.Value = ((ComboBox)sender).Text;
                cmbChker.Visible = false;
            }
        }
        //检测员单击事件
        private void cmbChker_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void cmbProductCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void cmbProductCompany_MouseClick(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void cmbProductAddr_KeyUp(object sender, KeyEventArgs e)
        {
            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = cmbProductAddr.Text;
        }

        private void cmbProductCompany_KeyUp(object sender, KeyEventArgs e)
        {
            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = cmbProductCompany.Text;
        }

        private void cmbDetectUnitNature_KeyUp(object sender, KeyEventArgs e)
        {
            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = cmbDetectUnitNature.Text;
        }

        private void cmbDetectUnitNature_MouseClick(object sender, MouseEventArgs e)
        {
            
        }
        /// <summary>
        /// 打开串口、通信测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnlinkcom_Click(object sender, EventArgs e)
        {
            btnlinkcom.Enabled = false;
            WorkstationDAL.Model.clsShareOption.ComPort = cmbCOMbox.Text;
            try
            {
                if (btnlinkcom.Text == "连接设备")
                {
                    //Serialrecive = new Thread(dy7200s.comn_DataReceived(null,null));
                    string Pdata = dy7200s.IniSearialport(WorkstationDAL.Model.clsShareOption.ComPort, "9600");
                    if (Pdata == "OK")
                    {
                        dy7200s.communicate();

                        //txtlink.Text = "已创建连接";
                        //btnlinkcom.Text = "断开连接";
                    }
                }
                else if (btnlinkcom.Text == "断开设备")
                {
                    string Pdata = dy7200s.IniSearialport(WorkstationDAL.Model.clsShareOption.ComPort, "9600");//判断打开串口后关闭
                    if (Pdata == "OK")
                    {
                        btnlinkcom.Text = "连接设备";
                        txtlink.Text = "未连接";
                        btnlinkcom.Enabled = true;
                        BtnReadHis.Enabled = false;
                        
                    }
                }

            }
            catch (JH.CommBase.CommPortException ex)
            {
                MessageBox.Show(ex.Message);
                btnlinkcom.Enabled = true;
            }
            //btnlinkcom.Enabled = false;

            //WorkstationDAL.Model.clsShareOption.ComPort = cmbCOMbox.Text;

            //if (btnlinkcom.Text == "连接设备")
            //{
            //    try
            //    {
            //        if (!dy7200.Online)
            //        {
            //            dy7200.Open();
            //            dy7200.communicate();//通信测试
            //            //txtlink.Text = "已连接设备";
            //            //btnlinkcom.Text = "断开设备";
            //            //MessageBox.Show("仪器连接成功", "提示",MessageBoxButtons.OK ,MessageBoxIcon.Information);
            //        }
            //        else
            //        {
            //            //curDY3000DY.Close();
            //            MessageBox.Show("串口已打开", "提示");
            //        }
            //    }
            //    catch (JH.CommBase.CommPortException ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //        return;
            //    }
            //}
            //else if (btnlinkcom.Text == "断开设备")
            //{
            //    dy7200.Close();
            //    txtlink.Text = "未连接";
            //    btnlinkcom.Text = "连接设备";
            //}
            //btnlinkcom.Enabled = true;
        }

        /// <summary>
        /// 数据读取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void BtnReadHis_Click(object sender, EventArgs e)
        {
            try
            {
                if (DTPStart.Value > DTPEnd.Value)
                {
                    MessageBox.Show("起始日期不能大于结束日期", "系统提示");
                    return;
                }
                BtnReadHis.Enabled = false;
                cmbAdd.Visible = false;
                cmbChkItem.Visible = false;
                cmbSample.Visible = false;
                cmbChkUnit.Visible = false;
                cmbChker.Visible = false;
                cmbDetectUnit.Visible = false;
                cmbGetSampleAddr.Visible = false;
                cmbDetectUnitNature.Visible = false;
                cmbProductCompany.Visible = false;
                cmbProductAddr.Visible = false;

                if (DY7200.comn.IsOpen == true)
                {
                    dy7200s.ReadHistory(DTPStart.Value.Date, DTPEnd.Value);
                }
                else
                {
                    MessageBox.Show("请通信测试再读取数据", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cursor = Cursors.Default;
                    BtnReadHis.Enabled = true;
                    BtnClear.Enabled = true;
                }

                //if (dy7200.Online)
                //{
                //    dy7200.ReadHistory(DTPStart.Value.Date, DTPEnd.Value);
                //}
                //else
                //{
                //    MessageBox.Show("请通信测试再读取数据", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    Cursor = Cursors.Default;
                //    BtnReadHis.Enabled = true;
                //    BtnClear.Enabled = true;
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                BtnReadHis.Enabled = true;
            }

        }

        private void cmbProductAddr_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 清除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void BtnClear_Click(object sender, EventArgs e)
        {
            CheckDatas.DataSource = null;
            cmbAdd.Visible = false;
            cmbChkItem.Visible = false;
            cmbSample.Visible = false;
            cmbChkUnit.Visible = false;
            cmbChker.Visible = false;
            cmbDetectUnit.Visible = false;
            cmbGetSampleAddr.Visible = false;
            cmbDetectUnitNature.Visible = false;
            cmbProductCompany.Visible = false;
            cmbProductAddr.Visible = false;
            dy7200s.DataReadTable.Clear();
        }

        /// <summary>
        /// 检测数据保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnDatsave_Click(object sender, EventArgs e)
        {
            //isave = 0;
            int iok = 0;
            string chk = "";
            string err = string.Empty;
            if (CheckDatas==null || CheckDatas.Rows.Count == 0)
            {
                MessageBox.Show("请读取数据再保存","系统提示");
                return;
            }
           
            btnDatsave.Enabled = false;
            cmbAdd.Visible = false;
            cmbChkItem.Visible = false;
            cmbSample.Visible = false;
            cmbChkUnit.Visible = false;
            cmbChker.Visible = false;
            cmbDetectUnit.Visible = false;
            cmbGetSampleAddr.Visible = false;
            cmbDetectUnitNature.Visible = false;
            cmbProductCompany.Visible = false;
            cmbProductAddr.Visible = false;
            isave = 0;
            int fsave = 0;
            int osave = 0;
            try
            {
                if (CheckDatas.Rows.Count > 0)
                {
                    for (int i = 0; i < CheckDatas.Rows.Count; i++)
                    {
                        if (CheckDatas.Rows[i].Cells["已保存"].Value.ToString() != "是" && CheckDatas.Rows[i].Cells["结论"].Value.ToString().Trim() != "无效" && CheckDatas.Rows[i].Cells["样品名称"].Value.ToString().Trim() != "")
                        {
                            string samplecode = "";
                            //查询样品编号
                            cdt = sqlSet.Getfoodtype("food_name='" + CheckDatas.Rows[i].Cells["样品名称"].Value.ToString().Trim() + "'", "", out err);
                            if (cdt != null && cdt.Rows.Count > 0)
                            {
                                samplecode = cdt.Rows[0]["fid"].ToString();
                            }
                            resultdata.Save = "是";
                            //resultdata.Gridnum = CheckDatas.Rows[i].Cells[1].Value.ToString();
                            resultdata.CheckNumber = Global.GUID("N", 1);
                            resultdata.SampleName = CheckDatas.Rows[i].Cells["样品名称"].Value.ToString().Trim();
                            resultdata.SampleCode = samplecode;
                            resultdata.Checkitem = CheckDatas.Rows[i].Cells["检测项目"].Value.ToString().Trim();
                            DataTable dtas = sqlSet.GetZJItem("itemName='" + resultdata.Checkitem + "'", "", out err);//查询检测项目编号
                            if (dtas != null && dtas.Rows.Count > 0)
                            {
                                resultdata.CheckitemCode = dtas.Rows[0]["itemCode"].ToString();
                            }
                            else
                            {
                                resultdata.CheckitemCode = "";
                            }

                            resultdata.CheckData = CheckDatas.Rows[i].Cells["检测结果"].Value.ToString().Trim();
                            resultdata.Unit = CheckDatas.Rows[i].Cells["单位"].Value.ToString().Trim();
                            resultdata.Testbase = CheckDatas.Rows[i].Cells["检测依据"].Value.ToString().Trim();
                            resultdata.LimitData = CheckDatas.Rows[i].Cells["标准值"].Value.ToString().Trim();//标准值
                            resultdata.Instrument = CheckDatas.Rows[i].Cells["检测仪器"].Value.ToString().Trim();//检测仪器
                            resultdata.Result = CheckDatas.Rows[i].Cells["结论"].Value.ToString().Trim() == "阴性" || CheckDatas.Rows[i].Cells["结论"].Value.ToString().Trim() == "合格" ? "合格" : "不合格";
                            resultdata.detectunit = CheckDatas.Rows[i].Cells["检测单位"].Value.ToString().Trim();//检测单位
                            resultdata.Gettime = CheckDatas.Rows[i].Cells["采样时间"].Value.ToString().Trim();//采样时间
                            //resultdata.Getplace = CheckDatas.Rows[i].Cells["采样地点"].Value.ToString().Trim();
                            resultdata.CheckUnit = CheckDatas.Rows[i].Cells["被检单位"].Value.ToString().Trim();
                            //resultdata.CheckUnitNature = CheckDatas.Rows[i].Cells["被检企业性质"].Value.ToString().Trim();
                            resultdata.Tester = CheckDatas.Rows[i].Cells["检测员"].Value.ToString().Trim();
                            chk = CheckDatas.Rows[i].Cells["检测时间"].Value.ToString().Replace("-", "/").Trim();
                            resultdata.CheckTime = DateTime.Parse(chk);
                            //resultdata.SampleType = CheckDatas.Rows[i].Cells["样品种类"].Value.ToString().Trim();//样品种类
                            resultdata.sampleNum = CheckDatas.Rows[i].Cells["检测数量"].Value.ToString().Trim();//检测样品数量
                            //resultdata.ProductPlace = CheckDatas.Rows[i].Cells["产地"].Value.ToString().Trim();
                            resultdata.ProductCompany = CheckDatas.Rows[i].Cells["生产单位"].Value.ToString().Trim();
                            //resultdata.ProductDate = CheckDatas.Rows[i].Cells["生产日期"].Value.ToString().Trim();
                            resultdata.SendDate = CheckDatas.Rows[i].Cells["送检日期"].Value.ToString().Trim();
                            resultdata.TreatResult = CheckDatas.Rows[i].Cells["处理结果"].Value.ToString().Trim();
                            resultdata.HoleNumber = "1";
                            resultdata.MachineCode = "DY-7200(I)";
                            //resultdata.TaskID = CheckDatas.Rows[i].Cells[24].Value.ToString().Trim();//抽样单ID

                            iok = sqlSet.ResuInsert(resultdata, out err);
                            if (iok == 1)
                            {
                                isave = isave + 1;
                                CheckDatas.Rows[i].Cells[0].Value = "是";
                            }
                            else
                            {
                                fsave = fsave + 1;
                            }
                        }
                        else
                        {
                            osave = osave + 1;
                        }
                    }
                    if (isave == 0 && fsave==0)
                    {
                        MessageBox.Show("数据已成功保存过！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else 
                    {
                        MessageBox.Show("数据保存成功，共保存" + isave + "条数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                btnDatsave.Enabled = true;
                MessageBox.Show(ex.Message, "Error");
            }
            btnDatsave.Enabled = true;
        }
        /// <summary>
        /// 数据上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnadd_Click(object sender, EventArgs e)
        {
            if (Global.linkNet() == false)
            {
                MessageBox.Show("无法连接到互联网，请检查网络连接！", "系统提示");
                btnadd.Enabled = true;
                return;
            }

            if (CheckDatas.Rows.Count < 1)
            {
                MessageBox.Show("没有检测数据上传", "提示");
                return;
            }

            if (Global.ServerAdd.Length == 0)
            {
                MessageBox.Show("服务器地址不能为空", "提示");
                return;
            }
            if (Global.ServerName.Length == 0)
            {
                MessageBox.Show("用户名不能为空", "提示");
                return;
            }
            if (Global.ServerPassword.Length == 0)
            {
                MessageBox.Show("密码不能为空", "提示");
                return;
            }

            string err = "";
            int upok = 0;
            int NG = 0;
            string cregid = "";
            string ogranco = "";

            if (isave == 0)
            {
                MessageBox.Show("请保存数据再上传","系统提示",MessageBoxButtons.OK ,MessageBoxIcon.Warning);
                return;
            }

            DialogResult tishi = MessageBox.Show("共有" + isave  + "条数据是否上传", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (tishi == DialogResult.No)
            {
                return;
            }
            btnadd.Enabled = false;
            cmbAdd.Visible = false;
            cmbChkItem.Visible = false;
            cmbSample.Visible = false;
            cmbChkUnit.Visible = false;
            cmbChker.Visible = false;
            cmbDetectUnit.Visible = false;
            cmbGetSampleAddr.Visible = false;
            cmbDetectUnitNature.Visible = false;
            cmbProductCompany.Visible = false;
            cmbProductAddr.Visible = false;
            string errstr = "";
            try
            {
                if (Global.Platform == "DYBus")//达元快检车平台
                {
                    for (int i = 0; i < CheckDatas.Rows.Count; i++)
                    {
                   
                        //查询数据是否已上传
                        strWhere.Length = 0;
                        strWhere.AppendFormat("CheckTime=#{0}# and ", CheckDatas.Rows[i].Cells["检测时间"].Value.ToString().Replace("-", "/").Trim());
                        strWhere.AppendFormat("CheckData='{0}' and ", CheckDatas.Rows[i].Cells["检测结果"].Value.ToString().Trim());
                        strWhere.AppendFormat("Machine='{0}' and ", Global.ChkManchine);
                        strWhere.AppendFormat("SampleName='{0}' and ", CheckDatas.Rows[i].Cells["样品名称"].Value.ToString().Replace("\0\0", "").Trim());
                        strWhere.AppendFormat("Checkitem='{0}' ", CheckDatas.Rows[i].Cells["检测项目"].Value.ToString().Trim());

                        DataTable dup = sqlSet.GetSave(strWhere.ToString(), out err);
                        if (dup != null && dup.Rows.Count > 0)
                        {
                            if (dup.Rows[0]["IsUpload"].ToString() == "是" || CheckDatas.Rows[i].Cells["结论"].Value.ToString().Trim() == "无效")
                            {
                                continue;
                            }
                        }
                        //查询被检单位
                        DataTable dtcompany = sqlSet.GetExamedUnit("regName='" + CheckDatas.Rows[i].Cells["被检单位"].Value.ToString().Trim() + "'", "", out err);
                        if (dtcompany != null && dtcompany.Rows.Count > 0)
                        {
                            cregid = dtcompany.Rows[0]["regId"].ToString();
                            ogranco = dtcompany.Rows[0]["organizationCode"].ToString();
                        }

                        string samplecode = "";
                        //查询样品编号
                        DataTable ds = sqlSet.GetSampleDetail("foodName='" + CheckDatas.Rows[i].Cells["样品名称"].Value.ToString().Trim() + "'", "", out err);
                        //DataTable ds = sqlSet.GetItemStandard("sampleName='" + CheckDatas.Rows[i].Cells[1].Value.ToString().Trim() + "' and itemName='" +
                        //    CheckDatas.Rows[i].Cells[2].Value.ToString().Trim() + "'", "", out err);
                        if (ds != null && ds.Rows.Count > 0)
                        {
                            samplecode = ds.Rows[0]["foodCode"].ToString();
                        }
                        
                        clsUpLoadCheckData upDatas = new clsUpLoadCheckData();
                        upDatas.result = new List<clsUpLoadCheckData.results>();
                        clsUpLoadCheckData.results model = new clsUpLoadCheckData.results();
                        model.sysCode = Global.GUID();
                        model.foodName = CheckDatas.Rows[i].Cells["样品名称"].Value.ToString().Trim();
                        model.foodCode = samplecode == "" ? "0000100310002" : samplecode;
                        model.foodType = CheckDatas.Rows[i].Cells["样品种类"].Value.ToString().Trim() == "" ? "蔬菜" : CheckDatas.Rows[i].Cells["样品种类"].Value.ToString().Trim();
                        model.sampleNo = "";
                        model.planCode = "";
                        model.checkPId = Global.pointID;
                        DateTime dt = DateTime.Parse(CheckDatas.Rows[i].Cells["检测时间"].Value.ToString().Replace("/", "-").Trim());
                        model.checkDate = dt.ToString("yyyy-MM-dd HH:mm:ss"); //DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        model.checkAccord = CheckDatas.Rows[i].Cells["检测依据"].Value.ToString().Trim();
                        model.checkItemName = CheckDatas.Rows[i].Cells["检测项目"].Value.ToString().Trim();
                        model.checkDevice = Global.ChkManchine; //CheckDatas.Rows[i].Cells[6].Value.ToString();
                        model.regId = cregid;
                        model.ckcName = CheckDatas.Rows[i].Cells["被检单位"].Value.ToString().Trim();
                        model.cdId = "";
                        model.ckcCode = ogranco;
                        model.checkResult = CheckDatas.Rows[i].Cells["检测结果"].Value.ToString().Trim();
                        model.checkUnit = CheckDatas.Rows[i].Cells["单位"].Value.ToString().Trim();
                        model.limitValue = "<" + CheckDatas.Rows[i].Cells["标准值"].Value.ToString().Trim() + CheckDatas.Rows[i].Cells["标准值"].Value.ToString().Trim();
                        model.checkConclusion = CheckDatas.Rows[i].Cells["结论"].Value.ToString().Trim() == "阴性" || CheckDatas.Rows[i].Cells["结论"].Value.ToString().Trim() == "合格" ? "合格" : "不合格 ";
                        model.dataStatus = 1;
                        model.dataSource = 0;
                        model.checkUser = CheckDatas.Rows[i].Cells["检测员"].Value.ToString().Trim();
                        model.dataUploadUser = CheckDatas.Rows[i].Cells["检测员"].Value.ToString().Trim();
                        model.deviceCompany = "广东达元";
                        model.deviceModel = Global.ChkManchine.Substring(0, 7);
                        upDatas.result.Add(model);
                        string json = JsonHelper.EntityToJson(upDatas);
                        string rtn = InterfaceHelper.UploadChkData(json, out err);
                        ResultMsg msgResult = null;
                        msgResult = JsonHelper.JsonToEntity<ResultMsg>(rtn);
                        if (msgResult.resultCode.Equals("success1"))
                        {
                            upok = upok + 1;
                            CheckDatas.Rows[i].DefaultCellStyle.ForeColor = Color.Green;//上传成功变绿色
                            clsUpdateData ud = new clsUpdateData();
                            ud.result = CheckDatas.Rows[i].Cells["检测结果"].Value.ToString().Trim();
                            ud.ChkTime = CheckDatas.Rows[i].Cells["检测时间"].Value.ToString().Trim();
                            ud.intrument = CheckDatas.Rows[i].Cells["检测仪器"].Value.ToString().Trim();
                            ud.ChkSample = CheckDatas.Rows[i].Cells["样品名称"].Value.ToString().Trim();
                            ud.Chkxiangmu = CheckDatas.Rows[i].Cells["检测项目"].Value.ToString().Trim();
                            sqlSet.SetUpLoadData(ud, out err);
                        }
                    }
                }
                else if (Global.Platform == "DYKJFW")//快检服务
                {
                    dt = sqlSet.GetResultTable("", "", 1, isave, out err);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["IsUpload"].ToString() == "是")//不允许重传
                            {
                                continue;
                            }
                            string ruld = QuickInspectServing.GetServiceURL(Global.ServerAdd, 6);//上传地址
                            QuickServerResult udata = new QuickServerResult();

                            DataTable dts = sqlSet.GetCompany("b.reg_id=r.rid and r.reg_name = '" + dt.Rows[i]["CheckUnit"].ToString() + "'", "", 0, out err);
                            if (dts != null && dts.Rows.Count > 0)
                            {
                                udata.regId = dts.Rows[0]["rid"].ToString();//经营户ID
                                udata.regUserName = dts.Rows[0]["ope_shop_name"].ToString();//档口名称
                                udata.regUserId = dts.Rows[0]["bid"].ToString();//档口ID
                               
                            }

                            dts = sqlSet.GetCheckItem("detect_item_name='" + dt.Rows[i]["Checkitem"].ToString() + "'", "", 2);//查检测项目ID
                            if (dts != null && dts.Rows.Count > 0)
                            {
                                udata.itemId = dts.Rows[0]["cid"].ToString();
                            }

                            udata.id = dt.Rows[i]["ChkNum"].ToString().Replace("-", "").Trim();//仪器的检测编号
                       
                            //qsr.itemId = "";//dt.Rows[0]["item_id"].ToString();//检测任务检测项目
                            udata.departId = Global.depart_id;// dt.Rows[0]["t_task_departId"].ToString(); 
                            udata.departName = Global.d_depart_name;// "天河区食药监局";  //?
                            udata.pointId = Global.point_id;// dt.Rows[0]["s_point_id"].ToString();
                            udata.pointName = Global.p_point_name;//  检测点
                           
                            udata.regName = dt.Rows[i]["CheckUnit"].ToString();//dt.Rows[0]["s_reg_name"].ToString(); //被检单位
                            udata.statusFalg = "0";
                            udata.remark = "";
                            udata.foodId = dt.Rows[i]["SampleCode"].ToString();//dt.Rows[0]["food_id"].ToString(); ;
                            udata.foodName = dt.Rows[i]["SampleName"].ToString();
                            udata.foodTypeId = dt.Rows[i]["SampleCode"].ToString();// result[i].SampleCode;
                            udata.foodTypeName = dt.Rows[i]["SampleCategory"].ToString();
                            udata.checkCode = "";//dt.Rows[0]["tid"].ToString(); 
                            udata.itemName = dt.Rows[i]["Checkitem"].ToString();
                            udata.checkResult = dt.Rows[i]["CheckData"].ToString();
                            udata.limitValue = "<" + dt.Rows[i]["LimitData"].ToString();
                            udata.checkAccord = dt.Rows[i]["TestBase"].ToString();
                            udata.checkUnit = dt.Rows[i]["Unit"].ToString();
                            udata.conclusion = dt.Rows[i]["Result"].ToString();
                            udata.auditorName = Global.realname;
                            udata.checkUserid = Global.id;
                            udata.checkUsername = Global.realname;//result[i].CheckUnitInfo;登录人账号
                            udata.checkAccordId = dt.Rows[i]["ChkNum"].ToString().Replace("-", "").Trim();
                            udata.checkDate = dt.Rows[i]["CheckTime"].ToString().Replace("/", "-");
                            udata.uploadName = Global.realname; //result[i].CheckUnitInfo;
                            udata.auditorId = Global.id;
                            udata.uploadId = Global.id;
                            udata.uploadDate = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            udata.deviceCompany = "广东达元绿洲食品安全科技股份有限公司";
                            udata.deviceId = Global.MachineSerialCode;
                            udata.deviceName = Global.ChkManchine; ;
                            udata.deviceModel = "荧光光";
                            udata.deviceMethod = "标准曲线法";
                            udata.dataSource = "1";
                            if (dt.Rows[i]["TID"].ToString() != "")
                            {
                                dts = sqlSet.GetQtask("ID=" + dt.Rows[i]["TID"].ToString(), "", 5);

                                if (dts != null && dts.Rows.Count > 0)
                                {
                                    udata.param1 = dts.Rows[0]["td_id"].ToString();
                                    udata.dataType = dts.Rows[0]["dataType"].ToString();
                                    udata.regUserId = dts.Rows[0]["s_ope_id"].ToString(); //dt.Rows[0]["s_ope_id"].ToString();//档口ID
                                    udata.regUserName = dts.Rows[0]["s_ope_shop_name"].ToString();//dt.Rows[0]["s_ope_shop_name"].ToString();//档口名称
                                    udata.regId = dts.Rows[0]["s_reg_id"].ToString();//dt.Rows[0]["s_reg_id"].ToString(); ;//经营户ID
                                    udata.taskId = dts.Rows[0]["t_id"].ToString();//任务ID为空
                                    udata.taskName = dts.Rows[0]["t_task_title"].ToString();//任务主题
                                    udata.samplingId = dts.Rows[0]["sampling_id"].ToString();//检测任务的样品ID;
                                    udata.samplingDetailId = dts.Rows[0]["tid"].ToString();//检测任务的tid
                                }
                            }
                            else
                            {
                                udata.param1 = ""; //dt.Rows[0]["td_id"].ToString();//预留参数
                                udata.dataType = "0";
                            }

                            string rtn= QuickInspectServing.KJFWUpData(udata, ruld);
                            if (rtn.Contains("msg") || rtn.Contains("success"))
                            {
                                ResultData Jresult = JsonHelper.JsonToEntity<ResultData>(rtn);
                                if (Jresult.msg == "操作成功" && Jresult.success == true)
                                {
                                    upok = upok + 1;
                                    sqlSet.SetUploadResult(dt.Rows[i]["ID"].ToString(), out err);
                                    isave = 0;
                                }
                            }
                        }
                    }
                }
                else if (Global.Platform == "ZJ")
                {
                    
                     dt = sqlSet.GetResultTable("", "", 1, isave, out err);
                     if (dt != null && dt.Rows.Count > 0)
                     {
                         string UrlA = Global.geturl(Global.ServerAdd, 3);

                         for (int i = 0; i < dt.Rows.Count; i++)
                         {
                             if (dt.Rows[0]["IsUpload"].ToString() == "是")
                             {
                                 continue;
                             }
                             string xml = Global.GetXMLstr(dt);
                             if (xml == "参考国标")
                             {
                                 continue;
                             }
                             if (xml == "无效")
                             {
                                 continue;
                             }
                             string UpRec = Global.HttpXML("POST", UrlA, xml);
                             JavaScriptSerializer Up = new JavaScriptSerializer();   //实例化一个能够序列化数据的类
                             Global.ToJsonLOG Uplist = Up.Deserialize<Global.ToJsonLOG>(UpRec);    //将json数据转化为对象类型并赋值给list
                             string UPresultCode = Uplist.resultCode;
                             string UPmessage = Uplist.message;
                             //Global.PostToken = Uplist.token;
                             if (UPresultCode == "10200")
                             {
                                 sqlSet.SetUploadResult(dt.Rows[i]["ID"].ToString(), out err);
                                 upok = upok + 1;
                             }
                             else
                             {
                                 NG++;
                                 errstr += UPmessage;
                             }
                         }
                     }
                }
                if (errstr == "")
                {
                    MessageBox.Show("共成功上传" + upok.ToString() + "条数据！");
                }
                else if (errstr.Length > 0 && upok > 0)
                {
                    MessageBox.Show("共成功上传" + upok.ToString() + "条数据；\r\n共" + NG + "条数据上传失败！\r\n 失败原因：" + errstr);
                }
                else
                {
                    MessageBox.Show("数据上传失败！\r\n 失败原因：" + errstr);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "数据上传");
            }
            btnadd.Enabled = true;
         
         
        }

        /// <summary>
        /// 消息通知事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NotificationEventHandler(object sender, MessageNotification.NotificationEventArgs e)
        {
            if (e.Message == "tongxin")
            {
                if (InvokeRequired)
                    BeginInvoke(new InvokeBtn(showbtn), btnlinkcom);
            }
            else if(e.Message =="xinxi")
            {
                if (dy7200s.rtndata == 0)
                {
                    MessageBox.Show("读取日期没有检测数据，请选择有数据的日期！","系统提示");
                    if (InvokeRequired)
                        BeginInvoke(new InvokeBtn(showbtn), btnlinkcom);
                    dy7200s.DataReadTable.Clear();
     
                    ShowResult(dy7200s.DataReadTable, true);
                }
                else
                {
                    dy7200s.ReadData();
                }
                
            }
            else if (e.Message == "data")
            {
                
                ShowResult(dy7200s.DataReadTable, true);
            }
            else if(e.Message =="end")
            {
                //ShowResult(dy7200s.DataReadTable, true);
                MessageBox.Show("共成功读取"+dy7200s.count +"条数据！");
                ShowResult(dy7200s.DataReadTable, true);
            }
        }

        /// <summary>
        /// 修改通信按钮名称
        /// </summary>
        /// <param name="btn"></param>
        private void showbtn(Button btn)
        {
            txtlink.Text = "已连接设备";
            btnlinkcom.Text = "断开设备";
            BtnReadHis.Enabled = true;
            BtnClear.Enabled = true;
            btnDatsave.Enabled = true;
            btnadd.Enabled = true;
            btnlinkcom.Enabled = true;
        }
        /// <summary>
        /// 调用结果
        /// </summary>
        /// <param name="dtbl"></param>
        private void ShowResult(DataTable dtbl, bool cleared)
        {
            if (InvokeRequired)
                BeginInvoke(new InvokeDelegate(showOnControl), dtbl);
            else
                showOnControl(dtbl);
        }

        /// <summary>
        /// 把数据绑定数据控件显示结果
        /// </summary>
        /// <param name="dtbl"></param>
        private void showOnControl(DataTable dtbl)
        {
            if (dtbl == null)
            {
                Cursor = Cursors.Default;
                BtnReadHis.Enabled = true;
                BtnClear.Enabled = true;
                return;
            }
          
            if (dtbl.Rows.Count > 0)
            {
                CheckDatas.DataSource = dtbl.DefaultView;
                //CheckDatas.Columns["ID"].Visible  =false;
            }
            else
            {
                CheckDatas.DataSource = dtbl;
            }
            Cursor = Cursors.Default;
            CheckDatas.Refresh();
            BtnReadHis.Enabled = true;
            BtnClear.Enabled = true;
        }

        /// <summary>
        /// 刷新串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnRefresh_Click(object sender, EventArgs e)
        {

            string[] Port = SerialPort.GetPortNames();
            cmbCOMbox.Items.Clear();
            if (Port.Length == 0)
            {
                cmbCOMbox.Items.Add("没有COM口");
            }
            foreach (string c in SerialPort.GetPortNames())
            {
                cmbCOMbox.Items.Add(c);
            }
            cmbCOMbox.SelectedIndex = 0;
            btnlinkcom.Enabled = true;

        }
        /// <summary>
        /// 关闭串口
        /// </summary>
        public void closecom()
        {
            //dy7200.Close();
            dy7200s.IniSearialport(WorkstationDAL.Model.clsShareOption.ComPort, "9600");//判断打开串口后关闭
        }

        /// <summary>
        /// datagridview 单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void CheckDatas_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (CheckDatas.CurrentCell.ColumnIndex < 1)
            {
                CheckDatas.EditMode = DataGridViewEditMode.EditProgrammatically;
                cmbSample.Visible = false;
                cmbChkItem.Visible = false;
                cmbAdd.Visible = false;
                cmbDetectUnitNature.Visible = false;
                cmbProductCompany.Visible = false;
                cmbProductAddr.Visible = false;
                return;
            }
            try
            {
                if (CheckDatas.CurrentCell.ColumnIndex > -1 && CheckDatas.CurrentCell.RowIndex > -1)
                {
                    if (CheckDatas.CurrentCell.ColumnIndex == 1)//样品名称
                    {
                        cmbChkItem.Visible = false;
                        cmbAdd.Visible = false;
                        cmbChkUnit.Visible = false;
                        cmbChker.Visible = false;
                        cmbDetectUnit.Visible = false;
                        cmbGetSampleAddr.Visible = false;
                        cmbDetectUnitNature.Visible = false;
                        cmbProductCompany.Visible = false;
                        cmbProductAddr.Visible = false;
                        cmbSample.Text = CheckDatas.CurrentCell.Value.ToString();

                        Rectangle srect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                        cmbSample.Left = srect.Left;
                        cmbSample.Top = srect.Top;
                        cmbSample.Width = srect.Width;
                        cmbSample.Height = srect.Height;
                        cmbSample.Visible = true;

                        FrmSearchSample window = new FrmSearchSample();
                        window._item = CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测项目"].Value.ToString().Trim();
                        window.ShowDialog();
                        if (Global.iSampleName == "")
                        {
                            return;
                        }
                        CheckDatas.CurrentCell.Value = cmbSample.Text = Global.iSampleName;
                        string sql = "FtypeNmae='" + Global.iSampleName + "'" + " and  Name='" + CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测项目"].Value.ToString().Trim() + "'";
                        DataTable dt = sqlSet.GetChkItem(sql, "", out err);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测依据"].Value = dt.Rows[0][2].ToString();
                        }
                    }
                    else if (CheckDatas.CurrentCell.ColumnIndex == 2)//检测项目
                    {
                        if (CheckDatas.CurrentCell.Value.ToString()!="")
                        {
                            return;
                        }
                        cmbChkItem.Visible = false ;
                        cmbAdd.Visible = false;
                        cmbChkUnit.Visible = false;
                        cmbChker.Visible = false;
                        cmbDetectUnit.Visible = false;
                        cmbGetSampleAddr.Visible = false;
                        cmbDetectUnitNature.Visible = false;
                        cmbProductCompany.Visible = false;
                        cmbProductAddr.Visible = false;
                        frmItemshow frm = new frmItemshow();
                        frm.ShowDialog();
                        if(frm.Itemcodes !="")
                            CheckDatas.CurrentCell.Value = frm.Itemcodes;
                    }
                    else if ((CheckDatas.CurrentCell.ColumnIndex > 3 && CheckDatas.CurrentCell.ColumnIndex < 7) || (CheckDatas.CurrentCell.ColumnIndex > 12 && CheckDatas.CurrentCell.ColumnIndex < 15)
                        || (CheckDatas.CurrentCell.ColumnIndex > 15 && CheckDatas.CurrentCell.ColumnIndex < 18))
                    {
                        cmbSample.Visible = false;
                        cmbChkItem.Visible = false;
                        cmbChkUnit.Visible = false;
                        cmbChker.Visible = false;
                        cmbDetectUnit.Visible = false;
                        cmbGetSampleAddr.Visible = false;
                        cmbAdd.Visible = true;
                        cmbDetectUnitNature.Visible = false;
                        cmbProductCompany.Visible = false;
                        cmbProductAddr.Visible = false;

                        cmbAdd.Text = CheckDatas.CurrentCell.Value.ToString();
                        Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                        cmbAdd.Left = rect.Left;
                        cmbAdd.Top = rect.Top;
                        cmbAdd.Width = rect.Width;
                        cmbAdd.Height = rect.Height;

                    }
                    else if (CheckDatas.CurrentCell.ColumnIndex == 10)//检测单位
                    {
                        cmbSample.Visible = false;
                        cmbChkItem.Visible = false;
                        cmbChkUnit.Visible = true;
                        cmbChker.Visible = false;
                        cmbDetectUnit.Visible = false;
                        cmbGetSampleAddr.Visible = false;
                        cmbAdd.Visible = false;
                        cmbDetectUnitNature.Visible = false;
                        cmbProductCompany.Visible = false;
                        cmbProductAddr.Visible = false;

                        cmbChkUnit.Text = CheckDatas.CurrentCell.Value.ToString();
                        Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                        cmbChkUnit.Left = rect.Left;
                        cmbChkUnit.Top = rect.Top;
                        cmbChkUnit.Width = rect.Width;
                        cmbChkUnit.Height = rect.Height;

                    }
                 
                    else if (CheckDatas.CurrentCell.ColumnIndex == 15)//生产单位
                    {
                        cmbSample.Visible = false;
                        cmbChkItem.Visible = false;
                        cmbChkUnit.Visible = false;
                        cmbChker.Visible = false;
                        cmbDetectUnit.Visible = false;
                        cmbGetSampleAddr.Visible = false;
                        cmbAdd.Visible = false;
                        cmbDetectUnitNature.Visible = false;
                        cmbProductCompany.Visible = true;
                        cmbProductAddr.Visible = false;

                        cmbProductCompany.Text = CheckDatas.CurrentCell.Value.ToString();
                        Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                        cmbProductCompany.Left = rect.Left;
                        cmbProductCompany.Top = rect.Top;
                        cmbProductCompany.Width = rect.Width;
                        cmbProductCompany.Height = rect.Height;

                    }
                 
                    else if (CheckDatas.CurrentCell.ColumnIndex == 11)//被检单位
                    {
                        cmbSample.Visible = false;
                        cmbChkItem.Visible = false;
                        cmbChkUnit.Visible = false;
                        cmbChker.Visible = false;
                        cmbDetectUnit.Visible = true;
                        cmbGetSampleAddr.Visible = false;
                        cmbAdd.Visible = false;
                        cmbDetectUnitNature.Visible = false;
                        cmbProductCompany.Visible = false;
                        cmbProductAddr.Visible = false;

                        cmbDetectUnit.Text = CheckDatas.CurrentCell.Value.ToString();
                        Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                        cmbDetectUnit.Left = rect.Left;
                        cmbDetectUnit.Top = rect.Top;
                        cmbDetectUnit.Width = rect.Width;
                        cmbDetectUnit.Height = rect.Height;
                    }
                    else if (CheckDatas.CurrentCell.ColumnIndex == 12)//检测员
                    {
                        cmbSample.Visible = false;
                        cmbChkItem.Visible = false;
                        cmbChkUnit.Visible = false;
                        cmbChker.Visible = true;
                        cmbDetectUnit.Visible = false;
                        cmbGetSampleAddr.Visible = false;
                        cmbAdd.Visible = false;
                        cmbDetectUnitNature.Visible = false;
                        cmbProductCompany.Visible = false;
                        cmbProductAddr.Visible = false;

                        cmbChker.Text = CheckDatas.CurrentCell.Value.ToString();
                        Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                        cmbChker.Left = rect.Left;
                        cmbChker.Top = rect.Top;
                        cmbChker.Width = rect.Width;
                        cmbChker.Height = rect.Height;

                    }

                    else
                    {
                        cmbSample.Visible = false;
                        cmbChkItem.Visible = false;
                        cmbAdd.Visible = false;
                        cmbChkUnit.Visible = false;
                        cmbChker.Visible = false;
                        cmbDetectUnit.Visible = false;
                        cmbGetSampleAddr.Visible = false;
                        cmbDetectUnitNature.Visible = false;
                        cmbProductCompany.Visible = false;
                        cmbProductAddr.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        //滑动、改变单元格隐藏控件
        protected override void CheckDatas_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            cmbAdd.Visible = false;
            cmbChkItem.Visible = false;
            cmbSample.Visible = false;
            cmbChkUnit.Visible = false;
            cmbChker.Visible = false;
            cmbDetectUnit.Visible = false;
            cmbGetSampleAddr.Visible = false;
            cmbDetectUnitNature.Visible = false;
            cmbProductCompany.Visible = false;
            cmbProductAddr.Visible = false;
        }

        protected override void CheckDatas_Scroll(object sender, ScrollEventArgs e)
        {
            CheckDatas.Refresh();
            cmbAdd.Visible = false;
            cmbChkItem.Visible = false;
            cmbSample.Visible = false;
            cmbChkUnit.Visible = false;
            cmbChker.Visible = false;
            cmbDetectUnit.Visible = false;
            cmbGetSampleAddr.Visible = false;
            cmbDetectUnitNature.Visible = false;
            cmbProductCompany.Visible = false;
            cmbProductAddr.Visible = false;
        }

    }
}

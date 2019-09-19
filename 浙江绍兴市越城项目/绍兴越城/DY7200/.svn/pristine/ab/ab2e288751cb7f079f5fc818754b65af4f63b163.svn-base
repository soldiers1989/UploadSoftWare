using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorkstationUI.Basic;
using WorkstationModel.Instrument;
using WorkstationDAL.Basic;
using WorkstationModel.Model;
using WorkstationBLL.Mode;
using WorkstationDAL.Model;
using System.IO.Ports;
using WorkstationModel.UpData;
using WorkstationUI.function;

namespace WorkstationUI.machine
{
    public partial class ucDY5800 :  BasicContent 
    {
        private clsDY5800 dy5800 = new clsDY5800();
        private clsSaveResult resultdata = new clsSaveResult();
        public ComboBox cmbAdd = new ComboBox();//下拉列表
        private ComboBox cmbChkItem = new ComboBox();//检测项目
        private ComboBox cmbSample = new ComboBox();//样品名称
        private ComboBox cmbChkUnit = new ComboBox();//检测单位
        private ComboBox cmbDetectUnit = new ComboBox();//被检单位
        private ComboBox cmbGetSampleAddr = new ComboBox();//采样地址
        private ComboBox cmbChker = new ComboBox();//检测员
        private ComboBox cmbDetectUnitNature = new ComboBox();//被检单位性质
        private ComboBox cmbProductCompany = new ComboBox();//生产单位
        private ComboBox cmbProductAddr = new ComboBox();//产地
        private ComboBox cmbGenertAddr = new ComboBox();//产地地址
        private clsSetSqlData sqlSet = new clsSetSqlData();
        public delegate void Rshowdata();
        private DataTable cdt = null;
        private string err = "";

        public ucDY5800()
        {
            InitializeComponent();
        }

        private void ucDY5800_Load(object sender, EventArgs e)
        {
            BtnReadHis.Enabled = false;
            //BtnClear.Enabled = false;
            LbTitle.Text = Global.ChkManchine;
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
            //cmbChkItem.DropDownStyle = ComboBoxStyle.DropDownList;
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
            cmbChkUnit.SelectedIndexChanged += cmbChkUnit_SelectedIndexChanged;
            //cmbChkUnit.MouseClick += cmbChkUnit_MouseClick;
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
            cmbChker.MouseClick += cmbChker_MouseClick;
            cmbChker.SelectedIndexChanged += cmbChker_SelectedIndexChanged;
            cmbChker.KeyUp += cmbChker_KeyUp;
            CheckDatas.Controls.Add(cmbChker);

            //产地
            cmbProductAddr.Items.Add("以下相同");
            cmbProductAddr.Items.Add("删除");
            cmbProductAddr.Visible = false;
            cmbProductAddr.MouseClick += cmbProductAddr_MouseClick;
            cmbProductAddr.SelectedIndexChanged += cmbProductAddr_SelectedIndexChanged;
            cmbProductAddr.KeyUp += cmbProductAddr_KeyUp;
            CheckDatas.Controls.Add(cmbProductAddr);

            //产地地址
            cmbGenertAddr.Items.Add("以下相同");
            cmbGenertAddr.Items.Add("删除");
            cmbGenertAddr.Visible = false;
            //cmbGenertAddr.MouseClick += cmbGenertAddr_MouseClick;
            cmbGenertAddr.SelectedIndexChanged += cmbGenertAddr_SelectedIndexChanged;
            cmbGenertAddr.KeyUp += cmbGenertAddr_KeyUp;
            CheckDatas.Controls.Add(cmbGenertAddr);
            //生产单位
            cmbProductCompany.Items.Add("以下相同");
            cmbProductCompany.Items.Add("删除");
            cmbProductCompany.Visible = false;
            cmbProductCompany.MouseClick += cmbProductCompany_MouseClick;
            cmbProductCompany.SelectedIndexChanged += cmbProductCompany_SelectedIndexChanged;
            cmbProductCompany.KeyUp += cmbProductCompany_KeyUp;
            CheckDatas.Controls.Add(cmbProductCompany);
            //被检单位性质
            cmbDetectUnitNature.Items.Add("以下相同");
            cmbDetectUnitNature.Items.Add("删除");
            cmbDetectUnitNature.Visible = false;
            cmbDetectUnitNature.MouseClick += cmbDetectUnitNature_MouseClick;
            cmbDetectUnitNature.KeyUp += cmbDetectUnitNature_KeyUp;
            cmbDetectUnitNature.SelectedIndexChanged += cmbDetectUnitNature_SelectedIndexChanged;
            CheckDatas.Controls.Add(cmbDetectUnitNature);

            cdt = sqlSet.GetInformation("", "", out err);
            if (cdt != null && cdt.Rows.Count > 0)
            {
                for (int n = 0; n < cdt.Rows.Count; n++)
                {
                    cmbChkUnit.Items.Add(cdt.Rows[n]["TestUnitName"].ToString());//检测单位
                    cmbDetectUnit.Items.Add(cdt.Rows[n]["DetectUnitName"].ToString());//被检单位
                    cmbGetSampleAddr.Items.Add(cdt.Rows[n]["SampleAddress"].ToString());//采样地址
                    cmbChker.Items.Add(cdt.Rows[n]["Tester"].ToString());//检测员
                    cmbGenertAddr.Items.Add(cdt.Rows[n]["TestUnitAddr"].ToString());//产地地址
                    cmbProductCompany.Items.Add(cdt.Rows[n]["ProductCompany"].ToString());//生产单位
                    cmbDetectUnitNature.Items.Add(cdt.Rows[n]["DetectUnitNature"].ToString());//被检单位性质
                    cmbProductAddr.Items.Add(cdt.Rows[n]["ProductAddr"].ToString());//产地
                   
                }
            }
            MessageNotification.GetInstance().DataRead += NotificationEventHandler;
            clsUpdateMessage.LabelUpdated += clsUpdateMessage_LabelUpdated;
        }
        private void cmbGenertAddr_KeyUp(object sender, KeyEventArgs e)
        {
            CheckDatas.CurrentCell.Value = cmbGenertAddr.Text;
        }

        private void cmbGenertAddr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).Text == "删除")
            {
                CheckDatas.CurrentCell.Value = "";
                cmbGenertAddr.Text = "";
                cmbGenertAddr.Visible = false;
            }
            else if (((ComboBox)sender).Text == "以下相同")
            {
                for (int i = CheckDatas.CurrentCell.RowIndex; i < CheckDatas.Rows.Count; i++)
                {
                    CheckDatas.Rows[i].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = CheckDatas.CurrentCell.Value.ToString();
                }
                cmbGenertAddr.Text = "";
                cmbGenertAddr.Visible = false;
            }
            else if (((ComboBox)sender).Text == "输入")
            {
                cmbGenertAddr.Text = "";
                CheckDatas.CurrentCell.Value = "";
                cmbGenertAddr.Visible = false;
            }
            else
            {
                CheckDatas.CurrentCell.Value = ((ComboBox)sender).Text;
                cmbGenertAddr.Visible = false;
            }
        }
        private  void cmbDetectUnitNature_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).Text == "删除")
            {
                CheckDatas.CurrentCell.Value = "";
                cmbDetectUnitNature.Visible = false;
            }
            else if (((ComboBox)sender).Text == "以下相同")
            {
                for (int i = CheckDatas.CurrentCell.RowIndex; i < CheckDatas.Rows.Count; i++)
                {
                    CheckDatas.Rows[i].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = CheckDatas.CurrentCell.Value.ToString();
                }
                cmbDetectUnitNature.Visible = false;
            }
            else if (((ComboBox)sender).Text == "输入")
            {
                CheckDatas.CurrentCell.Value = "";
            }
            else
            {
                CheckDatas.CurrentCell.Value = ((ComboBox)sender).Text;
                cmbDetectUnitNature.Visible = false;
            }
        }

        private void cmbProductAddr_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void cmbDetectUnitNature_KeyUp(object sender, KeyEventArgs e)
        {
            CheckDatas.CurrentCell.Value = cmbDetectUnitNature.Text.Trim();
        }

        private void cmbDetectUnitNature_MouseClick(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void cmbProductCompany_KeyUp(object sender, KeyEventArgs e)
        {
            CheckDatas.CurrentCell.Value = cmbProductCompany.Text.Trim();
        }

        private void cmbProductCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).Text == "删除")
            {
                CheckDatas.CurrentCell.Value = "";
                cmbProductCompany.Visible = false;
            }
            else if (((ComboBox)sender).Text == "以下相同")
            {
                for (int i = CheckDatas.CurrentCell.RowIndex; i < CheckDatas.Rows.Count; i++)
                {
                    CheckDatas.Rows[i].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = CheckDatas.CurrentCell.Value.ToString();
                }
                cmbProductCompany.Visible = false;
            }
            else if (((ComboBox)sender).Text == "输入")
            {
                CheckDatas.CurrentCell.Value = "";
            }
            else
            {
                CheckDatas.CurrentCell.Value = ((ComboBox)sender).Text;
                cmbProductCompany.Visible = false;
            }
        }

        private void cmbProductCompany_MouseClick(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void cmbProductAddr_KeyUp(object sender, KeyEventArgs e)
        {
            CheckDatas.CurrentCell.Value = cmbProductAddr.Text.Trim();
        }

        private void cmbProductAddr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).Text == "删除")
            {
                CheckDatas.CurrentCell.Value = "";
                cmbProductAddr.Visible = false;
            }
            else if (((ComboBox)sender).Text == "以下相同")
            {
                for (int i = CheckDatas.CurrentCell.RowIndex; i < CheckDatas.Rows.Count; i++)
                {
                    CheckDatas.Rows[i].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = CheckDatas.CurrentCell.Value.ToString();
                }
                cmbProductAddr.Visible = false;
            }
            else if (((ComboBox)sender).Text == "输入")
            {
                CheckDatas.CurrentCell.Value = "";
            }
            else
            {
                CheckDatas.CurrentCell.Value = ((ComboBox)sender).Text;
                cmbProductAddr.Visible = false;
            }
        }

        private void cmbSample_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).Text == "删除")
            {
                CheckDatas.CurrentCell.Value = "";
                cmbSample.Visible = false;
            }
            else if (((ComboBox)sender).Text == "以下相同")
            {
                for (int i = CheckDatas.CurrentCell.RowIndex; i < CheckDatas.Rows.Count; i++)
                {
                    CheckDatas.Rows[i].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = CheckDatas.CurrentCell.Value.ToString();
                }
                cmbSample.Visible = false;
            }
            else if (((ComboBox)sender).Text == "输入")
            {
                CheckDatas.CurrentCell.Value = "";
            }
            else
            {
                CheckDatas.CurrentCell.Value = ((ComboBox)sender).Text;
                cmbSample.Visible = false;
            }
        }
        private void cmbSample_KeyUp(object sender, KeyEventArgs e)
        {
            CheckDatas.CurrentCell.Value = cmbSample.Text.Trim();
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
            //string err = string.Empty;
            if (DateTime.Now - dtCmbLastClick < new TimeSpan(0, 0, 0, 0, GetDoubleClickTime()))
            {
                // 双击事件处理方式
                try
                {
                    FrmSearchSample window = new FrmSearchSample();
                    window._item = CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测项目"].Value.ToString().Trim();
                    window.ShowDialog();
                    CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["样品名称"].Value = Global.iSampleName;

                    cmbSample.Text = Global.iSampleName;
                    cmbSample.Visible = false;
                    CheckDatas.CurrentCell.Value = Global.iSampleName;
                    if (Global.iSampleName == "")
                    {
                        return;
                    }
                    //string err = string.Empty;
                    string sql = "FtypeNmae='" + cmbSample.Text + "'" + " and  Name='" + CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测项目"].Value.ToString() + "'";

                    DataTable dt = sqlSet.GetChkItem(sql, "", out err);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测依据"].Value = dt.Rows[0]["ItemDes"].ToString();
                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["标准值"].Value = dt.Rows[0]["StandardValue"].ToString();
                        string symbol = dt.Rows[0]["Demarcate"].ToString();
                        switch (symbol)
                        {
                            case "≤":
                                {
                                    if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测结果"].Value.ToString()) < Decimal.Parse(dt.Rows[0]["StandardValue"].ToString()) ||
                                        Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测结果"].Value.ToString()) == Decimal.Parse(dt.Rows[0]["StandardValue"].ToString()))
                                    {
                                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["结论"].Value = "合格";
                                    }
                                    else
                                    {
                                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["结论"].Value = "不合格";
                                    }
                                }
                                break;
                            case "<":
                                {
                                    if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测结果"].Value.ToString()) < Decimal.Parse(dt.Rows[0]["StandardValue"].ToString()))
                                    {
                                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["结论"].Value = "合格";
                                    }
                                    else
                                    {
                                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["结论"].Value = "不合格";
                                    }
                                }
                                break;
                            case "≥":
                                {
                                    if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测结果"].Value.ToString()) > Decimal.Parse(dt.Rows[0]["StandardValue"].ToString()) ||
                                        Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测结果"].Value.ToString()) == Decimal.Parse(dt.Rows[0]["StandardValue"].ToString()))
                                    {
                                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["结论"].Value = "合格";
                                    }
                                    else
                                    {
                                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["结论"].Value = "不合格";
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
                        if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测结果"].Value.ToString()) < 50)
                        {
                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["结论"].Value = "合格";
                        }
                        else
                        {
                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["结论"].Value = "不合格";
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
                dtCmbLastClick = DateTime.Now;
            }
        }
        private void cmbChkItem_SelectedIndexChanged(object sender, EventArgs e)
        {

            cmbChkItem.Visible = false;
            if (((ComboBox)sender).Text == "删除")
            {
                CheckDatas.CurrentCell.Value = "";
                cmbChkItem.Visible = false;
            }
            else if (((ComboBox)sender).Text == "以下相同")
            {
                for (int i = CheckDatas.CurrentCell.RowIndex; i < CheckDatas.Rows.Count; i++)
                {
                    CheckDatas.Rows[i].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = CheckDatas.CurrentCell.Value.ToString();
                }
                cmbChkItem.Visible = false;
            }
            else if (((ComboBox)sender).Text == "输入")
            {
                CheckDatas.CurrentCell.Value = "";
            }
            else
            {
                CheckDatas.CurrentCell.Value = ((ComboBox)sender).Text;
                cmbChkItem.Visible = false;
            }
        }
        private void cmbChkItem_KeyUp(object sender, KeyEventArgs e)
        {
            CheckDatas.CurrentCell.Value = cmbChkItem.Text.Trim();
        }
        private void cmbAdd_KeyUp(object sender, KeyEventArgs e)
        {
            CheckDatas.CurrentCell.Value = cmbAdd.Text.Trim();
        }
        private void cmbAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cmbChkItem.Visible = false;
            //cmbSample.Visible = false;
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
                CheckDatas.CurrentCell.Value = "";
            }
            else
            {
                CheckDatas.CurrentCell.Value = ((ComboBox)sender).Text;
                cmbAdd.Visible = false;
            }
        }
        //检测员按键弹起事件
        private void cmbChker_KeyUp(object sender, KeyEventArgs e)
        {
            CheckDatas.CurrentCell.Value = cmbChker.Text.Trim();
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

        /// <summary>
        /// 消息更新事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void clsUpdateMessage_LabelUpdated(object sender, clsUpdateMessage.LabelUpdateEventArgs e)
        {
            LbTitle.Text = Global.ChkManchine;
        }
        /// <summary>
        /// 消息通知事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NotificationEventHandler(object sender, MessageNotification.NotificationEventArgs e)
        {
            if (e.Code == MessageNotification.NotificationInfo.ReadDY5800)
            {
                if (this.InvokeRequired)
                {
                    try
                    {
                        this.Invoke(new Rshowdata(showdata));
                    }
                    catch (ObjectDisposedException)
                    {
                        return;
                    }
                }
                else
                {
                    showdata();
                }
            }
        }
        private void showdata()
        {
            if (clsDY5800.DataReadTable != null && clsDY5800.DataReadTable.Rows.Count >0)
            {
                CheckDatas.DataSource = clsDY5800.DataReadTable;

                for (int i = 0; i < CheckDatas.Rows.Count; i++)
                {
    
                    if (CheckDatas.Rows[i].Cells["结论"].Value.ToString().Trim() == "不合格")
                    {
                        CheckDatas.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                        CheckDatas.Refresh();
                    }
                    
                }
            }
           

        }
        public void closecom()
        {
            string Pdata = dy5800.IniSearialport(WorkstationDAL.Model.clsShareOption.ComPort, "9600");//判断打开串口后关闭
        }
        /// <summary>
        /// 刷新
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
            btnlinkcom.Enabled = true;

        }
        /// <summary>
        /// 打开串口、连接设备
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
                    string Pdata =dy5800.IniSearialport(WorkstationDAL.Model.clsShareOption.ComPort, "9600");
                    if (Pdata == "OK")
                    {
                        txtlink.Text = "已连接设备";
                        btnlinkcom.Text = "断开连接";
                    }
                }
                else if (btnlinkcom.Text == "断开连接")
                {
                    string Pdata = dy5800.IniSearialport(WorkstationDAL.Model.clsShareOption.ComPort, "9600");//判断打开串口后关闭
                    if (Pdata == "OK")
                    {
                        btnlinkcom.Text = "连接设备";
                        txtlink.Text = "未连接";  
                    }
                }

                btnlinkcom.Enabled = true;
            }
            catch (JH.CommBase.CommPortException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        //数据保存
        protected override void btnDatsave_Click(object sender, EventArgs e)
        {
            btnDatsave.Enabled = false;
            int isave = 0;
            int iok = 0;
            string chk = "";
            string err = string.Empty;
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

            try
            {
                if (CheckDatas.Rows.Count > 0)
                {
                    for (int i = 0; i < CheckDatas.Rows.Count; i++)
                    {
                        if (CheckDatas.Rows[i].Cells["已保存"].Value.ToString() != "是")
                        {
                            //查询样品编号
                            //cdt = sqlSet.GetSampleDetail("foodName='" + CheckDatas.Rows[i].Cells["样品名称"].Value.ToString().Trim() + "'", "", out err);
                            //cdt = sqlSet.GetItemStandard("sampleName='" + CheckDatas.Rows[i].Cells["样品名称"].Value.ToString().Trim() + "' and itemName='" +
                            //    CheckDatas.Rows[i].Cells["检测项目"].Value.ToString().Trim() + "'", "", out err);
                            //string samplecode = "";
                            //if (cdt != null && cdt.Rows.Count > 0)
                            //{
                            //    samplecode = cdt.Rows[0]["SampleNum"].ToString();
                            //}

                            resultdata.Save = "是";
                            //resultdata.Gridnum = CheckDatas.Rows[i].Cells[1].Value.ToString();
                            resultdata.CheckNumber = Global.GUID(null, 1);
                            resultdata.SampleName = CheckDatas.Rows[i].Cells["样品名称"].Value.ToString().Trim();
                            resultdata.SampleCode = CheckDatas.Rows[i].Cells["样品编号"].Value.ToString().Trim();
                            resultdata.Checkitem = CheckDatas.Rows[i].Cells["检测项目"].Value.ToString().Trim();
                            resultdata.CheckData = CheckDatas.Rows[i].Cells["浓度值"].Value.ToString().Trim();
                            resultdata.Xiguagndu = CheckDatas.Rows[i].Cells["吸光度"].Value.ToString().Trim();
                            resultdata.Unit = CheckDatas.Rows[i].Cells["单位"].Value.ToString().Trim();
                            resultdata.Testbase = CheckDatas.Rows[i].Cells["检测依据"].Value.ToString().Trim();
                            resultdata.LimitData = CheckDatas.Rows[i].Cells["标准值"].Value.ToString().Trim();//标准值
                            resultdata.Instrument = CheckDatas.Rows[i].Cells["检测仪器"].Value.ToString().Trim();//检测仪器
                            resultdata.Result = CheckDatas.Rows[i].Cells["结论"].Value.ToString().Trim();
                            resultdata.detectunit = CheckDatas.Rows[i].Cells["检测单位"].Value.ToString().Trim();//检测单位
                            resultdata.Gettime = CheckDatas.Rows[i].Cells["采样时间"].Value.ToString().Trim();//采样时间
                            resultdata.Getplace = CheckDatas.Rows[i].Cells["采样地点"].Value.ToString().Trim();
                            resultdata.CheckUnit = CheckDatas.Rows[i].Cells["被检单位"].Value.ToString().Trim();
                            resultdata.CheckUnitNature = CheckDatas.Rows[i].Cells["被检企业性质"].Value.ToString().Trim();
                            resultdata.Tester = CheckDatas.Rows[i].Cells["检测员"].Value.ToString().Trim();
                            chk = CheckDatas.Rows[i].Cells["检测时间"].Value.ToString().Replace("-", "/").Trim();
                            resultdata.CheckTime = DateTime.Parse(chk);
                            resultdata.SampleType = CheckDatas.Rows[i].Cells["样品种类"].Value.ToString().Trim();//样品种类
                            resultdata.sampleNum = CheckDatas.Rows[i].Cells["检测数量"].Value.ToString().Trim();//检测样品数量
                            resultdata.ProductPlace = CheckDatas.Rows[i].Cells["产地"].Value.ToString().Trim();
                            resultdata.ProductCompany = CheckDatas.Rows[i].Cells["生产单位"].Value.ToString().Trim();
                            resultdata.ProductDate = CheckDatas.Rows[i].Cells["生产日期"].Value.ToString().Trim();
                            resultdata.SendDate = CheckDatas.Rows[i].Cells["送检日期"].Value.ToString().Trim();
                            resultdata.TreatResult = CheckDatas.Rows[i].Cells["处理结果"].Value.ToString().Trim();
                            resultdata.HoleNumber = CheckDatas.Rows[i].Cells["通道号"].Value.ToString().Trim();//通道号
                            resultdata.MachineCode = "DY-5800";

                            iok = sqlSet.ResuInsert(resultdata, out err);
                            if (iok == 1)
                            {
                                isave = isave + 1;
                                CheckDatas.Rows[i].Cells[0].Value = "是";
                            }
                        }
                    }
                    if (isave == 0)
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
        /// 清除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void BtnClear_Click(object sender, EventArgs e)
        {
            BtnClear.Enabled = false;
            clsDY5800.DataReadTable.Clear();
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
            BtnClear.Enabled = true;
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
            string cregid = "";
            string ogranco = "";

            for (int j = 0; j < CheckDatas.Rows.Count; j++)
            {
                if (CheckDatas.Rows[j].Cells[0].Value.ToString() == "否")
                {
                    if (CheckDatas.Rows[j].Cells["结论"].Value.ToString().Trim() == "无效")
                    {
                        continue;
                    }
                    else
                    {
                        MessageBox.Show("请保存数据再上传", "提示");
                        return;
                    }

                }
            }

            DialogResult tishi = MessageBox.Show("共有" + CheckDatas.Rows.Count + "条数据是否上传", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
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

            try
            {
                for (int i = 0; i < CheckDatas.Rows.Count; i++)
                {
                    //查询数据是否已上传
                    StringBuilder sb = new StringBuilder();
                    sb.Append("CheckTime=#");
                    sb.Append(CheckDatas.Rows[i].Cells["检测时间"].Value.ToString().Replace("-", "/").Trim());
                    sb.Append("# and CheckData='");
                    sb.Append(CheckDatas.Rows[i].Cells["检测结果"].Value.ToString().Trim());
                    sb.Append("' and Machine='");
                    sb.Append(Global.ChkManchine);
                    sb.Append("'");
                    sb.Append(" and SampleName='");
                    sb.Append(CheckDatas.Rows[i].Cells["样品名称"].Value.ToString().Replace("\0\0", "").Trim());
                    sb.Append("' and Checkitem='");
                    sb.Append(CheckDatas.Rows[i].Cells["检测项目"].Value.ToString().Trim());
                    sb.Append("'");

                    DataTable dup = sqlSet.GetSave(sb.ToString(), out err);
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
                    model.sysCode = Global.GUID("N", 1);
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
                MessageBox.Show("共成功能够上传" + upok + "条数据", "数据上传");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            btnadd.Enabled = true;

            //for (int j = 0; j < CheckDatas.Rows.Count; j++)
            //{
            //    if (CheckDatas.Rows[j].Cells[0].Value.ToString() == "否")
            //    {
            //        MessageBox.Show("请保存数据再上传", "提示");
            //        return;
            //    }
            //}

            //DialogResult tishi = MessageBox.Show("共有" + CheckDatas.Rows.Count + "条数据是否上传", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            //if (tishi == DialogResult.No)
            //{
            //    return;
            //}
            //btnadd.Enabled = false;

            //string err = "";
            //int isupdata = 0;
            //int IsSuccess = 0;

            //string errstr = "";

            //try
            //{

            //    DataTable dt = sqlSet.GetResultTable("", "", 1, CheckDatas.Rows.Count, out err);
            //    if (dt != null && dt.Rows.Count > 0)
            //    {
            //        for (int i = 0; i < dt.Rows.Count; i++)
            //        {
            //            if (dt.Rows[i]["IsUpload"].ToString() == "是")//不允许重传
            //            {
            //                isupdata = isupdata + 1;
            //                CheckDatas.Rows[i].DefaultCellStyle.ForeColor = Color.Green;
            //                continue;
            //            }
            //            WebReference.PutService putServer = new WebReference.PutService();
            //            string web = putServer.save(
            //              dt.Rows[i]["SampleName"].ToString(),//品种名称
            //              dt.Rows[i]["Checkitem"].ToString(),//检测项目
            //              dt.Rows[i]["SampleCategory"].ToString(),//果蔬类别
            //              "",//监测模式 
            //              dt.Rows[i]["DetectUnit"].ToString(),//检测单位
            //              dt.Rows[i]["Tester"].ToString(),//检 测 人
            //              dt.Rows[i]["SampleCode"].ToString(),//样品编号 
            //              dt.Rows[i]["ProduceAddr"].ToString(),//产地
            //              dt.Rows[i]["SampleAddress"].ToString(),//抽样地点
            //              dt.Rows[i]["TestBase"].ToString(),//依据标准
            //              dt.Rows[i]["LimitData"].ToString(),//标准值（抑制率）
            //              dt.Rows[i]["CheckData"].ToString(),//实测值（抑制率）
            //              dt.Rows[i]["Result"].ToString(),//单项比对结果
            //              DateTime.Parse(dt.Rows[i]["CheckTime"].ToString()),//检测日期
            //              DateTime.Parse(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),//上传日期
            //              dt.Rows[i]["ProduceUnit"].ToString(),//生产单位
            //               DateTime.Parse(dt.Rows[i]["ProductDatetime"].ToString()),//生产日期
            //              dt.Rows[i]["CheckUnit"].ToString(),//被检企业
            //              dt.Rows[i]["CompanyNeture"].ToString(),//被检企业性质
            //              Global.IntrumManifacture,//检测仪器厂商
            //              DateTime.Parse(dt.Rows[i]["SendTestDate"].ToString()),//送检日期
            //              dt.Rows[i]["DoResult"].ToString(),//处理结果
            //              dt.Rows[i]["ChkNum"].ToString()//业务唯一ID
            //              );
            //            if (web == "Ok")
            //            {
            //                IsSuccess = IsSuccess + 1;
            //                sqlSet.SetUploadResult(dt.Rows[i]["ID"].ToString(), out err);
            //            }
            //            else
            //            {
            //                errstr = errstr + "," + web;
            //            }
            //        }
            //    }
            //    if (errstr == "")
            //    {
            //        MessageBox.Show("共成功上传" + IsSuccess.ToString() + "条数据； 共" + isupdata + "条数据已传！");
            //    }
            //    else
            //    {
            //        MessageBox.Show("共成功上传" + IsSuccess.ToString() + "条数据；共" + isupdata + "条数据已传！ 提示信息：" + errstr);
            //    }
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.Message, "Error");
            //}

            btnadd.Enabled = true;

            //快检车平台

            //if (CheckDatas.Rows.Count < 1)
            //{
            //    MessageBox.Show("没有检测数据上传", "提示");
            //    return;
            //}

            //if (Global.ServerAdd.Length == 0)
            //{
            //    MessageBox.Show("服务器地址不能为空", "提示");
            //    return;
            //}
            //if (Global.ServerName.Length == 0)
            //{
            //    MessageBox.Show("用户名不能为空", "提示");
            //    return;
            //}
            //if (Global.ServerPassword.Length == 0)
            //{
            //    MessageBox.Show("密码不能为空", "提示");
            //    return;
            //}

            //string err = "";
            //int upok = 0;
            //string cregid = "";
            //string ogranco = "";

            //for (int j = 0; j < CheckDatas.Rows.Count; j++)
            //{
            //    if (CheckDatas.Rows[j].Cells[0].Value.ToString() == "否")
            //    {
            //        MessageBox.Show("请保存数据再上传", "提示");
            //        return;
            //    }
            //}

            //DialogResult tishi = MessageBox.Show("共有" + CheckDatas.Rows.Count + "条数据是否上传", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            //if (tishi == DialogResult.No)
            //{
            //    return;
            //}
            //btnadd.Enabled = false;

            //try
            //{
            //    for (int i = 0; i < CheckDatas.Rows.Count; i++)
            //    {
            //        //查询数据是否已上传
            //        StringBuilder sb = new StringBuilder();
            //        sb.Append("CheckTime=#");
            //        sb.Append(CheckDatas.Rows[i].Cells[14].Value.ToString().Replace("-", "/").Trim());
            //        sb.Append("# and CheckData='");
            //        sb.Append(CheckDatas.Rows[i].Cells[3].Value.ToString().Trim());
            //        sb.Append("' and Machine='");
            //        sb.Append(Global.ChkManchine);
            //        sb.Append("'");
            //        sb.Append(" and SampleName='");
            //        sb.Append(CheckDatas.Rows[i].Cells[1].Value.ToString().Replace("\0\0", "").Trim());
            //        sb.Append("' and Checkitem='");
            //        sb.Append(CheckDatas.Rows[i].Cells[2].Value.ToString().Trim());
            //        sb.Append("'");

            //        DataTable dup = sqlSet.GetSave(sb.ToString(), out err);
            //        if (dup != null && dup.Rows.Count > 0)
            //        {
            //            if (dup.Rows[0][0].ToString() == "是")
            //            {
            //                //MessageBox.Show("本条记录已上传");
            //                continue;
            //            }

            //        }
            //        //查询被检单位
            //        DataTable dtcompany = sqlSet.GetExamedUnit("regName='" + CheckDatas.Rows[i].Cells[12].Value.ToString().Trim() + "'", "", out err);
            //        if (dtcompany != null && dtcompany.Rows.Count > 0)
            //        {
            //            cregid = dtcompany.Rows[0][1].ToString();
            //            ogranco = dtcompany.Rows[0][5].ToString();
            //        }

            //        string samplecode = "";
            //        //查询样品编号
            //        DataTable ds = sqlSet.GetSampleDetail("foodName='" + CheckDatas.Rows[i].Cells[1].Value.ToString().Trim() + "'", "", out err);
            //        //DataTable ds = sqlSet.GetItemStandard("sampleName='" + CheckDatas.Rows[i].Cells[1].Value.ToString().Trim() + "' and itemName='" +
            //        //    CheckDatas.Rows[i].Cells[2].Value.ToString().Trim() + "'", "", out err);
            //        if (ds != null && ds.Rows.Count > 0)
            //        {
            //            samplecode = ds.Rows[0][2].ToString();
            //        }

            //        clsUpLoadCheckData upDatas = new clsUpLoadCheckData();
            //        upDatas.result = new List<clsUpLoadCheckData.results>();
            //        clsUpLoadCheckData.results model = new clsUpLoadCheckData.results();
            //        model.sysCode = Global.GUID();
            //        model.foodName = CheckDatas.Rows[i].Cells[1].Value.ToString().Trim();
            //        model.foodCode = samplecode == "" ? "0000100310002" : samplecode;
            //        model.foodType = CheckDatas.Rows[i].Cells[15].Value.ToString().Trim() == "" ? "蔬菜" : CheckDatas.Rows[i].Cells[15].Value.ToString().Trim();
            //        model.sampleNo = "";
            //        model.planCode = "";
            //        model.checkPId = Global.pointID;
            //        DateTime dt = DateTime.Parse(CheckDatas.Rows[i].Cells[14].Value.ToString().Replace("/", "-").Trim());
            //        model.checkDate = dt.ToString("yyyy-MM-dd HH:mm:ss"); //DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            //        model.checkAccord = CheckDatas.Rows[i].Cells[5].Value.ToString().Trim();
            //        model.checkItemName = CheckDatas.Rows[i].Cells[2].Value.ToString().Trim();
            //        model.checkDevice = Global.ChkManchine; //CheckDatas.Rows[i].Cells[6].Value.ToString();
            //        model.regId = cregid;
            //        model.ckcName = CheckDatas.Rows[i].Cells[12].Value.ToString().Trim();
            //        model.cdId = "";
            //        model.ckcCode = ogranco;
            //        model.checkResult = CheckDatas.Rows[i].Cells[3].Value.ToString().Trim();
            //        model.checkUnit = CheckDatas.Rows[i].Cells[4].Value.ToString().Trim();
            //        model.limitValue = "<" + CheckDatas.Rows[i].Cells[6].Value.ToString().Trim() + CheckDatas.Rows[i].Cells[4].Value.ToString().Trim();
            //        model.checkConclusion = CheckDatas.Rows[i].Cells[8].Value.ToString().Trim();
            //        model.dataStatus = 1;
            //        model.dataSource = 0;
            //        model.checkUser = CheckDatas.Rows[i].Cells[13].Value.ToString().Trim();
            //        model.dataUploadUser = CheckDatas.Rows[i].Cells[13].Value.ToString().Trim();
            //        model.deviceCompany = "广东达元";
            //        model.deviceModel = Global.ChkManchine.Substring(0, 7);
            //        upDatas.result.Add(model);
            //        string json = JsonHelper.EntityToJson(upDatas);
            //        string rtn = InterfaceHelper.UploadChkData(json, out err);
            //        ResultMsg msgResult = null;
            //        msgResult = JsonHelper.JsonToEntity<ResultMsg>(rtn);
            //        if (msgResult.resultCode.Equals("success1"))
            //        {
            //            upok = upok + 1;
            //            CheckDatas.Rows[i].DefaultCellStyle.ForeColor = Color.Green;//上传成功变绿色
            //            clsUpdateData ud = new clsUpdateData();
            //            ud.result = CheckDatas.Rows[i].Cells[2].Value.ToString().Trim();
            //            ud.ChkTime = CheckDatas.Rows[i].Cells[13].Value.ToString().Trim();
            //            ud.intrument = CheckDatas.Rows[i].Cells[6].Value.ToString().Trim();
            //            ud.ChkSample = CheckDatas.Rows[i].Cells[1].Value.ToString().Trim();
            //            ud.Chkxiangmu = CheckDatas.Rows[i].Cells[2].Value.ToString().Trim();
            //            sqlSet.SetUpLoadData(ud, out err);
            //        }
            //    }
            //    MessageBox.Show("共成功能够上传" + upok + "条数据", "数据上传");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error");
            //}
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
                return;
            }
            try
            {
                if (CheckDatas.CurrentCell.ColumnIndex > -1 && CheckDatas.CurrentCell.RowIndex > -1)
                {
                    //if (CheckDatas.CurrentCell.ColumnIndex == 2)
                    //{
                    //    cmbAdd.Visible = false;
                    //    cmbSample.Visible = false;
                    //    cmbChkUnit.Visible = false;
                    //    cmbChker.Visible = false;
                    //    cmbDetectUnit.Visible = false;
                    //    cmbGetSampleAddr.Visible = false;
                    //    cmbDetectUnitNature.Visible = false;
                    //    cmbProductCompany.Visible = false;
                    //    cmbProductAddr.Visible = false;

                    //    cmbChkItem.Text = CheckDatas.CurrentCell.Value.ToString();

                    //    Rectangle irect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                    //    cmbChkItem.Left = irect.Left;
                    //    cmbChkItem.Top = irect.Top;
                    //    cmbChkItem.Width = irect.Width;
                    //    cmbChkItem.Height = irect.Height;
                    //    cmbChkItem.Visible = true;
                    //}
                    //else 
                    if (CheckDatas.CurrentCell.ColumnIndex == 2)
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
                    }
                    else if (CheckDatas.CurrentCell.ColumnIndex == 8 || CheckDatas.CurrentCell.ColumnIndex == 9 || CheckDatas.CurrentCell.ColumnIndex == 11 || CheckDatas.CurrentCell.ColumnIndex > 22)//检测依据和标准值，采样时间、送检日期
                    {
                        cmbChkItem.Visible = false;
                        cmbSample.Visible = false;
                        cmbChkUnit.Visible = false;
                        cmbChker.Visible = false;
                        cmbDetectUnit.Visible = false;
                        cmbGetSampleAddr.Visible = false;
                        cmbAdd.Visible = true;
                        cmbDetectUnitNature.Visible = false;
                        cmbProductCompany.Visible = false;
                        cmbProductAddr.Visible = false;
                        cmbAdd.Text = CheckDatas.CurrentCell.Value.ToString();
                        Rectangle srect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                        cmbAdd.Left = srect.Left;
                        cmbAdd.Top = srect.Top;
                        cmbAdd.Width = srect.Width;
                        cmbAdd.Height = srect.Height;
                        
                    }
                    else if (CheckDatas.CurrentCell.ColumnIndex == 13)//检测单位
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
                    else if (CheckDatas.CurrentCell.ColumnIndex == 21)//产地
                    {
                        cmbSample.Visible = false;
                        cmbChkItem.Visible = false;
                        cmbChkUnit.Visible = false;
                        cmbChker.Visible = false;
                        cmbDetectUnit.Visible = false;
                        cmbGetSampleAddr.Visible = false;
                        cmbAdd.Visible = false;
                        cmbDetectUnitNature.Visible = false;
                        cmbProductCompany.Visible = false;
                        cmbProductAddr.Visible = true;

                        cmbProductAddr.Text = CheckDatas.CurrentCell.Value.ToString();
                        Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                        cmbProductAddr.Left = rect.Left;
                        cmbProductAddr.Top = rect.Top;
                        cmbProductAddr.Width = rect.Width;
                        cmbProductAddr.Height = rect.Height;

                    }
                    else if (CheckDatas.CurrentCell.ColumnIndex == 22)//生产单位
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
                  
                    else if (CheckDatas.CurrentCell.ColumnIndex == 15)//采样地点
                    {
                        cmbSample.Visible = false;
                        cmbChkItem.Visible = false;
                        cmbChkUnit.Visible = false;
                        cmbChker.Visible = false;
                        cmbDetectUnit.Visible = false;
                        cmbGetSampleAddr.Visible = true;
                        cmbAdd.Visible = false;
                        cmbDetectUnitNature.Visible = false;
                        cmbProductCompany.Visible = false;
                        cmbProductAddr.Visible = false;

                        cmbGetSampleAddr.Text = CheckDatas.CurrentCell.Value.ToString();
                        Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                        cmbGetSampleAddr.Left = rect.Left;
                        cmbGetSampleAddr.Top = rect.Top;
                        cmbGetSampleAddr.Width = rect.Width;
                        cmbGetSampleAddr.Height = rect.Height;
                    }
                    else if (CheckDatas.CurrentCell.ColumnIndex == 16)//被检单位
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
                    else if (CheckDatas.CurrentCell.ColumnIndex == 17)//被检单位性质
                    {
                        cmbSample.Visible = false;
                        cmbChkItem.Visible = false;
                        cmbChkUnit.Visible = false;
                        cmbChker.Visible = false;
                        cmbDetectUnit.Visible = false;
                        cmbGetSampleAddr.Visible = false;
                        cmbAdd.Visible = false;
                        cmbDetectUnitNature.Visible = true;
                        cmbProductCompany.Visible = false;
                        cmbProductAddr.Visible = false;

                        cmbDetectUnitNature.Text = CheckDatas.CurrentCell.Value.ToString();
                        Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                        cmbDetectUnitNature.Left = rect.Left;
                        cmbDetectUnitNature.Top = rect.Top;
                        cmbDetectUnitNature.Width = rect.Width;
                        cmbDetectUnitNature.Height = rect.Height;
                    }
                    else if (CheckDatas.CurrentCell.ColumnIndex == 18)//检测员
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
        protected override void CheckDatas_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            cmbAdd.Visible = false;
            cmbChkItem.Visible = false;
            cmbSample.Visible = false;
            cmbChkUnit.Visible = false;
            cmbChker.Visible = false;
            cmbDetectUnit.Visible = false;
            cmbGetSampleAddr.Visible = false;
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
        }

    }
}

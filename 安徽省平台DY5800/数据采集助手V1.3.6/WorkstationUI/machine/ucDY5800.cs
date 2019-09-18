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
using WorkstationDAL.AnHui;

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
        private int isave = 0;//保存的记录数

        public ucDY5800()
        {
            InitializeComponent();
        }

        private void ucDY5800_Load(object sender, EventArgs e)
        {
            BtnReadHis.Visible  = false;
            label1.Visible = false;
            DTPStart.Visible = false;
            label2.Visible = false;
            DTPEnd.Visible = false;

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

            cdt = sqlSet.GetAHCompany("", "", out err);//安徽

            ////cdt = sqlSet.GetInformation("", "", out err);
            if (cdt != null && cdt.Rows.Count > 0)
            {
                for (int n = 0; n < cdt.Rows.Count; n++)
                {
                    if (cdt.Rows[n]["isShows"].ToString() == "True")
                    {
                        //dy5800.unitInfo[0, 0] = cdt.Rows[n]["DetectUnitName"].ToString();
                        //dy5800.unitInfo[0, 1] = cdt.Rows[n]["SampleAddress"].ToString();
                        //dy5800.unitInfo[0, 2] = cdt.Rows[n]["Tester"].ToString();
                        dy5800.unitInfo[0, 3] = cdt.Rows[n]["FullName"].ToString();//检测单位
                        Global.tCompany = cdt.Rows[n]["FullName"].ToString();//检测单位
                        //dy5800.unitInfo[0, 4] = cdt.Rows[n]["DetectUnitNature"].ToString();//被检单位性质
                        //dy5800.unitInfo[0, 5] = cdt.Rows[n]["ProductAddr"].ToString();//产地
                        //dy5800.unitInfo[0, 6] = cdt.Rows[n]["ProductCompany"].ToString();//生产单位
                    }
                    cmbDetectUnit.Items.Add(cdt.Rows[n]["FullName"].ToString());//被检单位名称
            //        if (cdt.Rows[n]["isShows"].ToString()=="True")
            //        {
                        
            //        }
            //        cmbDetectUnit.Items.Add(cdt.Rows[n]["FullName"].ToString());//被检单位名称
            //        //cmbChkUnit.Items.Add(cdt.Rows[n]["isShows"].ToString());//已选择
                    
            //        //cmbGetSampleAddr.Items.Add(cdt.Rows[n]["Incorporator"].ToString());//法人代表
            //        //cmbChker.Items.Add(cdt.Rows[n]["Address"].ToString());//地址
            //        //cmbGenertAddr.Items.Add(cdt.Rows[n]["LinkMan"].ToString());//联系人
            //        //cmbProductCompany.Items.Add(cdt.Rows[n]["LinkInfo"].ToString());//更新时间
            //        //cmbDetectUnitNature.Items.Add(cdt.Rows[n]["rid"].ToString());//被检单位性质
            //        //cmbProductAddr.Items.Add(cdt.Rows[n]["reg_type"].ToString());//产地

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
                    if (Global.iSampleName == "")
                    {
                        return;
                    }
                    CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["样品名称"].Value = Global.iSampleName;
                    cmbSample.Text = Global.iSampleName;
                    cmbSample.Visible = false;
                    CheckDatas.CurrentCell.Value = Global.iSampleName;
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
                                    if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["浓度值"].Value.ToString()) < Decimal.Parse(dt.Rows[0]["StandardValue"].ToString()) ||
                                        Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["浓度值"].Value.ToString()) == Decimal.Parse(dt.Rows[0]["StandardValue"].ToString()))
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
                                    if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["浓度值"].Value.ToString()) < Decimal.Parse(dt.Rows[0]["StandardValue"].ToString()))
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
                                    if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["浓度值"].Value.ToString()) > Decimal.Parse(dt.Rows[0]["StandardValue"].ToString()) ||
                                        Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["浓度值"].Value.ToString()) == Decimal.Parse(dt.Rows[0]["StandardValue"].ToString()))
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
                        if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["浓度值"].Value.ToString()) < 50)
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
                    }
                    
                }
                CheckDatas.Refresh();
                isave = 0;
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
            isave = 0;
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
            bool isitem = false;

            try
            {
                if (CheckDatas.Rows.Count > 0)
                {
                    for (int i = 0; i < CheckDatas.Rows.Count; i++)
                    {
                        if (CheckDatas.Rows[i].Cells["已保存"].Value.ToString() != "是")
                        {
                            resultdata.Save = "是";
                            resultdata.CheckNumber = Global.GUID("N", 1);
                            resultdata.SampleName = CheckDatas.Rows[i].Cells["样品名称"].Value.ToString().Trim();
                            if (resultdata.SampleName=="")
                            {
                                continue;
                            }
                            resultdata.SampleCode = CheckDatas.Rows[i].Cells["样品编号"].Value.ToString().Trim();
                            resultdata.Checkitem = CheckDatas.Rows[i].Cells["检测项目"].Value.ToString().Trim();
                            DataTable dataTable = DataOperation.GetCheckProjectByName(resultdata.Checkitem);
                            if (dataTable != null && dataTable.Rows.Count > 0)
                            {
                                resultdata.CheckitemCode = dataTable.Rows[0]["codeId"].ToString();
                            }
                            else
                            {
                                if (isitem == false)
                                {
                                    MessageBox.Show("检测项目 [" + resultdata.Checkitem + "] 在后台查询不到数据，请联系平台管理员添加！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    isitem = true;
                                }
                                continue;
                            }
                            resultdata.CheckData = CheckDatas.Rows[i].Cells["浓度值"].Value.ToString().Trim();
                            resultdata.Xiguagndu = CheckDatas.Rows[i].Cells["吸光度"].Value.ToString().Trim();
                            resultdata.Unit = CheckDatas.Rows[i].Cells["单位"].Value.ToString().Trim();
                            resultdata.Testbase = CheckDatas.Rows[i].Cells["检测依据"].Value.ToString().Trim();
                            resultdata.LimitData = CheckDatas.Rows[i].Cells["标准值"].Value.ToString().Trim();//标准值
                            resultdata.Instrument = CheckDatas.Rows[i].Cells["检测仪器"].Value.ToString().Trim();//检测仪器
                            resultdata.Result = CheckDatas.Rows[i].Cells["结论"].Value.ToString().Trim();
                            //resultdata.detectunit = CheckDatas.Rows[i].Cells["检测单位"].Value.ToString().Trim();//检测单位
                            resultdata.Gettime = CheckDatas.Rows[i].Cells["采样时间"].Value.ToString().Trim();//采样时间
                            //resultdata.Getplace = CheckDatas.Rows[i].Cells["采样地点"].Value.ToString().Trim();
                            resultdata.CheckUnit = CheckDatas.Rows[i].Cells["被检单位"].Value.ToString().Trim();

                            //查询被检单位
                            if (resultdata.CheckUnit != "")
                            {
                                resultdata.CheckCompanyCode = DataOperation.NameFromCode(resultdata.CheckUnit);
                            }
                            else
                            {
                                resultdata.CheckCompanyCode = "0";
                            }

                            //resultdata.CheckUnitNature = CheckDatas.Rows[i].Cells["被检企业性质"].Value.ToString().Trim();
                            resultdata.Tester = CheckDatas.Rows[i].Cells["检测员"].Value.ToString().Trim();
                            chk = CheckDatas.Rows[i].Cells["检测时间"].Value.ToString().Trim();
                            resultdata.CheckTime = DateTime.Parse(chk);
                            resultdata.SampleType = CheckDatas.Rows[i].Cells["样品种类"].Value.ToString().Trim();//样品种类
                            
                            if (resultdata.SampleType=="")
                            {
                                //MessageBox.Show("样品种类不能为空","系统提示");
                                continue;
                                //return;
                            }
                            //查询样品种类编号
                            DataTable dtbl = DataOperation.GetSampleTypeByName(resultdata.SampleType);
                            if (dtbl != null && dtbl.Rows.Count > 0)
                            {
                                resultdata.SampleTypeCode = dtbl.Rows[0]["codeId"].ToString();//样品种类编号
                            }

                            //resultdata.sampleNum = CheckDatas.Rows[i].Cells["检测数量"].Value.ToString().Trim();//检测样品数量
                            //resultdata.ProductPlace = CheckDatas.Rows[i].Cells["产地"].Value.ToString().Trim();
                            resultdata.ProductCompany = CheckDatas.Rows[i].Cells["生产单位"].Value.ToString().Trim();
                            resultdata.ProductDate = CheckDatas.Rows[i].Cells["生产日期"].Value.ToString().Trim();
                            //resultdata.SendDate = CheckDatas.Rows[i].Cells["送检日期"].Value.ToString().Trim();
                            resultdata.TreatResult = CheckDatas.Rows[i].Cells["处理结果"].Value.ToString().Trim();
                            resultdata.HoleNumber = CheckDatas.Rows[i].Cells["通道号"].Value.ToString().Trim();//通道号
                            resultdata.MachineCode = "DY-5800";

                            if (CheckDatas.Rows[i].Cells["检测方法"].Value.ToString().Trim()=="定量")
                            {
                                resultdata.ChkMethod = "1";//检测方法
                            }
                            else if (CheckDatas.Rows[i].Cells["检测方法"].Value.ToString().Trim() == "定性")
                            {
                                resultdata.ChkMethod = "2";//检测方法
                            }
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
            if (Global.InstrumentType.Length == 0)
            {
                MessageBox.Show("仪器类型不能为空", "提示");
                
                return;
            }
            if (Global.IntrumentNums.Length == 0)
            {
                MessageBox.Show("仪器编号不能为空", "提示");
                
                return;
            }

            string err = "";
            int upok = 0;
            int ng = 0;

          
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
            if (isave == 0)
            {
                MessageBox.Show("请保存数据再上传", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult tishi = MessageBox.Show("共有" + isave + "条数据是否上传", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (tishi == DialogResult.No)
            {
                return;
            }
            Global.AnHuiInterface.ServerAddr = Global.ServerAdd;
            Global.AnHuiInterface.userName = Global.ServerName;
            Global.AnHuiInterface.passWord = Global.ServerPassword;
            Global.AnHuiInterface.instrument = Global.InstrumentType;//仪器类型
            Global.AnHuiInterface.instrumentNo = Global.IntrumentNums;//仪器编号
            Global.AnHuiInterface.interfaceVersion = Global.InterfaceVer;
            string outErr = "";
            try
            {
                DataTable dt = sqlSet.GetResultTable("", "", 1, isave, out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["IsUpload"].ToString() == "是")//不允许重传
                        {
                            continue;
                        }
                        clsInstrumentInfoHandle model = new clsInstrumentInfoHandle
                        {
                            interfaceVersion = Global.AnHuiInterface.interfaceVersion,
                            userName = Global.AnHuiInterface.userName,
                            instrument = Global.AnHuiInterface.instrument,
                            passWord = Global.AnHuiInterface.passWord
                        };
                        model.instrumentNo = model.userName + Global.AnHuiInterface.instrumentNo;
                        model.mac = Global.MacAddr;

                        model.gps = string.Empty;
                        model.fTpye = dt.Rows[i]["SampleCategoryCode"].ToString().Trim() == "" ? "1053" : dt.Rows[i]["SampleCategoryCode"].ToString().Trim(); ;

                        model.fName = dt.Rows[i]["SampleName"].ToString();
                        model.tradeMark = string.Empty;
                        model.foodcode = string.Empty;
                        model.proBatch = string.Empty;
                        model.proDate = dt.Rows[i]["ProductDatetime"].ToString().Replace('/', '-');//生产日期
                        model.proSpecifications = string.Empty;
                        model.manuUnit = string.Empty;
                        model.checkedNo = string.Empty;
                        model.sampleNo = dt.Rows[i]["SampleCode"].ToString().Trim().Length == 0 ? DateTime.Now.ToString("yyyyMMddHHmmss") : dt.Rows[i]["SampleCode"].ToString().Trim();
                        model.checkedUnit = dt.Rows[i]["ChkCompanyCode"].ToString().Length > 0 ? dt.Rows[i]["ChkCompanyCode"].ToString() : "0";
                        model.dataNum = dt.Rows[i]["ChkNum"].ToString().Length == 0 ? model.sampleNo : dt.Rows[i]["ChkNum"].ToString();
                        model.testPro = dt.Rows[i]["itemCode"].ToString().Length == 0 ? "默认检测项目" : dt.Rows[i]["itemCode"].ToString();
                        model.quanOrQual = dt.Rows[i]["ChkMethod"].ToString().Length == 0 ? "1" : dt.Rows[i]["ChkMethod"].ToString();
                        model.contents = dt.Rows[i]["CheckData"].ToString().Length == 0 ? "0" : dt.Rows[i]["CheckData"].ToString();
                        model.unit = dt.Rows[i]["Unit"].ToString();
                        model.testResult = dt.Rows[i]["Result"].ToString().Length == 0 ? "不合格" : dt.Rows[i]["Result"].ToString();
                        model.dilutionRa = string.Empty;
                        model.testRange = string.Empty;
                        model.standardLimit = dt.Rows[i]["LimitData"].ToString();
                        model.basedStandard = dt.Rows[i]["TestBase"].ToString();
                        model.testPerson = dt.Rows[i]["Tester"].ToString().Length == 0 ? Global.userlog : dt.Rows[i]["Tester"].ToString();
                        try
                        {
                            //DateTime dt = DateTime.Parse(data.CheckStartDate);
                            model.testTime = dt.Rows[i]["CheckTime"].ToString().Replace('/','-');
                        }
                        catch (Exception)
                        {
                            model.testTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        model.sampleTime = dt.Rows[i]["CheckTime"].ToString().Length == 0 ? model.testTime : dt.Rows[i]["CheckTime"].ToString().Replace('/', '-');
                        model.remark = dt.Rows[i]["DoResult"].ToString();
                        model.key = Global.AnHuiInterface.md5(model.userName + model.passWord + model.testTime +
                            model.instrumentNo + model.contents + model.testResult);

                        string str = Global.AnHuiInterface.instrumentInfoHandle(model);
                        List<string> rtnList = Global.AnHuiInterface.ParsingUploadXML(str);
                        if (rtnList[0].Equals("1"))
                        {
                            upok++;
                            sqlSet.SetUploadResult(dt.Rows[i]["ID"].ToString(), out err);
                        }
                        else
                        {
                            ng++;
                            outErr += "样品名称：[" + model.fName + "] 上传失败！\r\n异常信息：" + rtnList[1] + "；\r\n\r\n";
                        }

                    }
                }

                if (outErr == "")
                {
                    MessageBox.Show("数据上传成功！共成功上传" + upok + "条数据！", "数据上传", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (outErr.Length > 0 && upok > 0)
                {
                    MessageBox.Show("共成功上传" + upok + "条数据！\r\n上传失败" + ng + "条数据！\r\n失败原因：" + outErr, "数据上传", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (outErr.Length > 0 && upok == 0)
                {
                    MessageBox.Show("数据上传失败！！！\r\n失败原因：" + outErr, "数据上传", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                isave = 0;
                btnadd.Enabled = true;
            }
            catch (Exception ex)
            {
                btnadd.Enabled = true;
                MessageBox.Show(ex.Message,"提示");
            }
          
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
                    cmbAdd.Items.Clear();
                    cmbAdd.Items.Add("以下相同");
                    cmbAdd.Items.Add("删除");
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
                        FrmSearchSample window = new FrmSearchSample();
                        window._item = CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测项目"].Value.ToString().Trim();
                        window.ShowDialog();
                        if (Global.iSampleName == "")
                        {
                            return;
                        }
                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["样品名称"].Value = Global.iSampleName;
                        cmbSample.Text = Global.iSampleName;
                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["样品名称"].Value = Global.iSampleName;
                        cmbSample.Text = Global.iSampleName;
                        cmbSample.Visible = true;
                        CheckDatas.CurrentCell.Value = Global.iSampleName;
                  
                        string sql = "FtypeNmae='" + Global.iSampleName + "'" + " and  Name='" + CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测项目"].Value.ToString() + "'";
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
                                        if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["浓度值"].Value.ToString()) < Decimal.Parse(dt.Rows[0]["StandardValue"].ToString()) ||
                                            Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["浓度值"].Value.ToString()) == Decimal.Parse(dt.Rows[0]["StandardValue"].ToString()))
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
                                        if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["浓度值"].Value.ToString()) < Decimal.Parse(dt.Rows[0]["StandardValue"].ToString()))
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
                                        if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["浓度值"].Value.ToString()) > Decimal.Parse(dt.Rows[0]["StandardValue"].ToString()) ||
                                            Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["浓度值"].Value.ToString()) == Decimal.Parse(dt.Rows[0]["StandardValue"].ToString()))
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
                            if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["浓度值"].Value.ToString()) < 50)
                            {
                                CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["结论"].Value = "合格";
                            }
                            else
                            {
                                CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["结论"].Value = "不合格";
                            }
                        }

                    }
                    else if (CheckDatas.CurrentCell.ColumnIndex == 4)//样品种类
                    {
                        cmbChkItem.Visible = false;
                        cmbSample.Visible = false;
                        cmbChkUnit.Visible = false;
                        cmbChker.Visible = false;
                        cmbDetectUnit.Visible = false;
                        cmbGetSampleAddr.Visible = false;
                        cmbAdd.Visible = false ;
                        cmbDetectUnitNature.Visible = false;
                        cmbProductCompany.Visible = false;
                        cmbProductAddr.Visible = false;

                        Frmfoodtype frm = new Frmfoodtype();
                        frm.ShowDialog();
                        if(Global.AH_FoodType !="")
                        {
                            CheckDatas.CurrentCell.Value = Global.AH_FoodType;
                        }
                    }
                    else if (8 < CheckDatas.CurrentCell.ColumnIndex && CheckDatas.CurrentCell.ColumnIndex <12)//检测依据和标准值，采样时间、送检日期
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
                    //else if (CheckDatas.CurrentCell.ColumnIndex == 13)//检测单位
                    //{
                    //    cmbSample.Visible = false;
                    //    cmbChkItem.Visible = false;
                    //    cmbChkUnit.Visible = true;
                    //    cmbChker.Visible = false;
                    //    cmbDetectUnit.Visible = false;
                    //    cmbGetSampleAddr.Visible = false;
                    //    cmbAdd.Visible = false;
                    //    cmbDetectUnitNature.Visible = false;
                    //    cmbProductCompany.Visible = false;
                    //    cmbProductAddr.Visible = false;

                    //    cmbChkUnit.Text = CheckDatas.CurrentCell.Value.ToString();
                    //    Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                    //    cmbChkUnit.Left = rect.Left;
                    //    cmbChkUnit.Top = rect.Top;
                    //    cmbChkUnit.Width = rect.Width;
                    //    cmbChkUnit.Height = rect.Height;

                    //}
                    //else if (CheckDatas.CurrentCell.ColumnIndex == 21)//产地
                    //{
                    //    cmbSample.Visible = false;
                    //    cmbChkItem.Visible = false;
                    //    cmbChkUnit.Visible = false;
                    //    cmbChker.Visible = false;
                    //    cmbDetectUnit.Visible = false;
                    //    cmbGetSampleAddr.Visible = false;
                    //    cmbAdd.Visible = false;
                    //    cmbDetectUnitNature.Visible = false;
                    //    cmbProductCompany.Visible = false;
                    //    cmbProductAddr.Visible = true;

                    //    cmbProductAddr.Text = CheckDatas.CurrentCell.Value.ToString();
                    //    Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                    //    cmbProductAddr.Left = rect.Left;
                    //    cmbProductAddr.Top = rect.Top;
                    //    cmbProductAddr.Width = rect.Width;
                    //    cmbProductAddr.Height = rect.Height;

                    //}
                    //else if (CheckDatas.CurrentCell.ColumnIndex == 22)//生产单位
                    //{
                    //    cmbSample.Visible = false;
                    //    cmbChkItem.Visible = false;
                    //    cmbChkUnit.Visible = false;
                    //    cmbChker.Visible = false;
                    //    cmbDetectUnit.Visible = false;
                    //    cmbGetSampleAddr.Visible = false;
                    //    cmbAdd.Visible = false;
                    //    cmbDetectUnitNature.Visible = false;
                    //    cmbProductCompany.Visible = true;
                    //    cmbProductAddr.Visible = false;

                    //    cmbProductCompany.Text = CheckDatas.CurrentCell.Value.ToString();
                    //    Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                    //    cmbProductCompany.Left = rect.Left;
                    //    cmbProductCompany.Top = rect.Top;
                    //    cmbProductCompany.Width = rect.Width;
                    //    cmbProductCompany.Height = rect.Height;

                    //}
                  
                    //else if (CheckDatas.CurrentCell.ColumnIndex == 15)//采样地点
                    //{
                    //    cmbSample.Visible = false;
                    //    cmbChkItem.Visible = false;
                    //    cmbChkUnit.Visible = false;
                    //    cmbChker.Visible = false;
                    //    cmbDetectUnit.Visible = false;
                    //    cmbGetSampleAddr.Visible = true;
                    //    cmbAdd.Visible = false;
                    //    cmbDetectUnitNature.Visible = false;
                    //    cmbProductCompany.Visible = false;
                    //    cmbProductAddr.Visible = false;

                    //    cmbGetSampleAddr.Text = CheckDatas.CurrentCell.Value.ToString();
                    //    Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                    //    cmbGetSampleAddr.Left = rect.Left;
                    //    cmbGetSampleAddr.Top = rect.Top;
                    //    cmbGetSampleAddr.Width = rect.Width;
                    //    cmbGetSampleAddr.Height = rect.Height;
                    //}
                    else if (CheckDatas.CurrentCell.ColumnIndex == 14)//被检单位
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
                    //else if (CheckDatas.CurrentCell.ColumnIndex == 17)//被检单位性质
                    //{
                    //    cmbSample.Visible = false;
                    //    cmbChkItem.Visible = false;
                    //    cmbChkUnit.Visible = false;
                    //    cmbChker.Visible = false;
                    //    cmbDetectUnit.Visible = false;
                    //    cmbGetSampleAddr.Visible = false;
                    //    cmbAdd.Visible = false;
                    //    cmbDetectUnitNature.Visible = true;
                    //    cmbProductCompany.Visible = false;
                    //    cmbProductAddr.Visible = false;

                    //    cmbDetectUnitNature.Text = CheckDatas.CurrentCell.Value.ToString();
                    //    Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                    //    cmbDetectUnitNature.Left = rect.Left;
                    //    cmbDetectUnitNature.Top = rect.Top;
                    //    cmbDetectUnitNature.Width = rect.Width;
                    //    cmbDetectUnitNature.Height = rect.Height;
                    //}
                    //else if (CheckDatas.CurrentCell.ColumnIndex == 18)//检测员
                    //{
                    //    cmbSample.Visible = false;
                    //    cmbChkItem.Visible = false;
                    //    cmbChkUnit.Visible = false;
                    //    cmbChker.Visible = true;
                    //    cmbDetectUnit.Visible = false;
                    //    cmbGetSampleAddr.Visible = false;
                    //    cmbAdd.Visible = false;
                    //    cmbDetectUnitNature.Visible = false;
                    //    cmbProductCompany.Visible = false;
                    //    cmbProductAddr.Visible = false;

                    //    cmbChker.Text = CheckDatas.CurrentCell.Value.ToString();
                    //    Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                    //    cmbChker.Left = rect.Left;
                    //    cmbChker.Top = rect.Top;
                    //    cmbChker.Width = rect.Width;
                    //    cmbChker.Height = rect.Height;

                    //}
                    else if (14 < CheckDatas.CurrentCell.ColumnIndex && CheckDatas.CurrentCell.ColumnIndex < 19 || CheckDatas.CurrentCell.ColumnIndex==20)
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
                    else if(CheckDatas.CurrentCell.ColumnIndex ==20)//检测方法
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
                        cmbAdd.Items.Clear();
                        cmbAdd.Items.Add("以下相同");
                        cmbAdd.Items.Add("删除");
                        cmbAdd.Items.Add("定量");
                        cmbAdd.Items.Add("定性");

                        cmbAdd.Text = CheckDatas.CurrentCell.Value.ToString();
                        Rectangle srect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                        cmbAdd.Left = srect.Left;
                        cmbAdd.Top = srect.Top;
                        cmbAdd.Width = srect.Width;
                        cmbAdd.Height = srect.Height;

                        //cmbSample.DropDownStyle = ComboBoxStyle.DropDownList;
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

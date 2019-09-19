using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorkstationUI.Basic;
using System.Runtime.InteropServices;
using WorkstationDAL.HID;
using WorkstationDAL.Model;
using System.IO;
using WorkstationDAL.Basic;
using WorkstationBLL.Mode;
using WorkstationModel.Model;

namespace WorkstationUI.machine
{
    public partial class ucLZ2000 : BasicContent  
    {    
        public ucLZ2000()
        {
            InitializeComponent();           
        }
        private  clsSaveResult resultdata = new clsSaveResult();
        private clsSetSqlData sqlSet = new clsSetSqlData();
        public DataTable SaveReadTable = null;
        private bool m_IsCreatedDataTable = false;
        private DeviceManagement MyDeviceManagement = new DeviceManagement();
        private DataTable cdt = null;
        private string[,] unitInfo = new string[1, 4];
        private ComboBox cmbAdd = new ComboBox();//其他信息录入
        private ComboBox cmbChkItem = new ComboBox();//检测项目
        private ComboBox cmbSample = new ComboBox();//样品名称
        private ComboBox cmbChkUnit = new ComboBox();//检测单位
        private ComboBox cmbDetectUnit = new ComboBox();//被检单位
        private ComboBox cmbGetSampleAddr = new ComboBox();//采样地址
        private ComboBox cmbChker = new ComboBox();//检测员
        private StringBuilder strWhere = new StringBuilder();
        protected string[,] _checkItems;
        private string err = string.Empty;

        private void ucLZ2000_Load(object sender, EventArgs e)
        {
            clsLZ2000();
            string err = string.Empty;
           
            cmbAdd.Visible = false;
            cmbAdd.Items.Add("输入");
            cmbAdd.Items.Add("以下相同");
            cmbAdd.Items.Add("删除");
            cmbAdd.KeyUp += cmbAdd_KeyUp;
            cmbAdd.SelectedIndexChanged += cmbAdd_SelectedIndexChanged;
            CheckDatas.Controls.Add(cmbAdd);
            //检测项目
            cmbChkItem.Visible = false;
            cmbChkItem.Items.Add("以下相同");
            cmbChkItem.Items.Add("删除");
            //cmbChkItem.DropDownStyle = ComboBoxStyle.DropDownList;
            //cmbChkItem.SelectedIndex = 0;
            cmbChkItem.KeyUp += cmbChkItem_KeyUp;
            cmbChkItem.SelectedIndexChanged += cmbChkItem_SelectedIndexChanged;
            CheckDatas.Controls.Add(cmbChkItem);
            //样品名称
            cmbSample.Visible = false;
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
            string itemcode = string.Empty;

            if (Global.ChkManchine != "")
            {
                try
                {
                    strWhere.Clear();
                    strWhere.AppendFormat("WHERE Name='{0}'", Global.ChkManchine);
       
                    DataTable dt = sqlSet.GetIntrument(strWhere.ToString(), out err);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        
                        itemcode = dt.Rows[0]["ChkStdCode"].ToString();
                        WorkstationDAL.Model.clsShareOption.ComPort = dt.Rows[0]["communication"].ToString();
                       
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
            _checkItems = clsStringUtil.GetDY3000DYAry(itemcode);
            for (int j = 0; j < _checkItems.GetLength(0); j++)
            {
                cmbChkItem.Items.Add(_checkItems[j, 0]);
                //DataTable dt = sqlSet.GetSample("IsLock=false And IsReadOnly=true and CheckItemCodes like '%{" + _checkItems[j, 1] + ":%'", "SysCode", out err);
                DataTable dt = sqlSet.GetSample("Name='" + _checkItems[j, 0] + "'", "idx", out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cmbSample.Items.Add(dt.Rows[i]["FtypeNmae"].ToString());
                    }

                }
            }
            try
            {
                cdt = sqlSet.GetInformation("", "", out err);
                if (cdt != null && cdt.Rows.Count > 0)
                {
                    for (int n = 0; n < cdt.Rows.Count; n++)
                    {
                        if (cdt.Rows[n]["iChecked"].ToString() == "是")
                        {
                            unitInfo[0, 0] = cdt.Rows[n]["TestUnitName"].ToString(); //检测单位
                            unitInfo[0, 1] = cdt.Rows[n]["DetectUnitName"].ToString();//被检单位
                            unitInfo[0, 2] = cdt.Rows[n]["SampleAddress"].ToString();//采样地址
                            unitInfo[0, 3] = cdt.Rows[n]["Tester"].ToString();//检测人
                        }
                        cmbChkUnit.Items.Add(cdt.Rows[n]["TestUnitName"].ToString());//检测单位
                        cmbDetectUnit.Items.Add(cdt.Rows[n]["DetectUnitName"].ToString());//被检单位
                        cmbGetSampleAddr.Items.Add(cdt.Rows[n]["SampleAddress"].ToString());//采样地址
                        cmbChker.Items.Add(cdt.Rows[n]["Tester"].ToString());//检测员
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
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
        ////检测单位单击事件
        //private void cmbChkUnit_MouseClick(object sender, MouseEventArgs e)
        //{

        //}
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
        /// 查找设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnlinkcom_Click(object sender, EventArgs e)
        {
            Global.hid_vid = "72f";
            Global.hid_pid = "a032";
            if (FindTheHid(Global.hid_vid, Global.hid_pid))
            {
                txtlink.Text = "已创建连接";
                //MessageBox.Show("连接成功","提示");
            }
            else
            {
                txtlink.Text = "未创建连接";
                MessageBox.Show("找不到设备", "提示");
                return;
            }
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
                    CheckDatas.CurrentCell.Value = cmbSample.Text;
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
                                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["结论"].Value = "阴性";
                                    }
                                    else
                                    {
                                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["结论"].Value = "阳性";
                                    }
                                }
                                break;
                            case "<":
                                {
                                    if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测结果"].Value.ToString()) < Decimal.Parse(dt.Rows[0]["StandardValue"].ToString()))
                                    {
                                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["结论"].Value = "阴性";
                                    }
                                    else
                                    {
                                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["结论"].Value = "阳性";
                                    }
                                }
                                break;
                            case "≥":
                                {
                                    if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测结果"].Value.ToString()) > Decimal.Parse(dt.Rows[0]["StandardValue"].ToString()) ||
                                        Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测结果"].Value.ToString()) == Decimal.Parse(dt.Rows[0]["StandardValue"].ToString()))
                                    {
                                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["结论"].Value = "阴性";
                                    }
                                    else
                                    {
                                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["结论"].Value = "阳性";
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
                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["结论"].Value = "阴性";
                        }
                        else
                        {
                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["结论"].Value = "阳性";
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
        private void cmbSample_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbAdd.Visible = false;
            cmbChkItem.Visible = false;
            //string err = string.Empty;
            if (cmbSample.Text == "以下相同")
            {
                try
                {
                    for (int j = CheckDatas.CurrentCell.RowIndex; j < CheckDatas.Rows.Count; j++)
                    {
                        CheckDatas.Rows[j].Cells["样品名称"].Value = CheckDatas.CurrentCell.Value.ToString();
                        string sql = "FtypeNmae='" + cmbSample.Text + "'" + " and  Name='" + CheckDatas.Rows[j].Cells["检测项目"].Value.ToString() + "'";
                        DataTable dt = sqlSet.GetChkItem(sql, "", out err);
                        
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            CheckDatas.Rows[j].Cells["检测依据"].Value = dt.Rows[0]["ItemDes"].ToString();
                            CheckDatas.Rows[j].Cells["标准值"].Value = dt.Rows[0]["StandardValue"].ToString();
                            string symbol = dt.Rows[0]["Demarcate"].ToString();
                            switch (symbol)
                            {
                                case "≤":
                                    {
                                        if (Decimal.Parse(CheckDatas.Rows[j].Cells["检测结果"].Value.ToString()) < Decimal.Parse(dt.Rows[0]["StandardValue"].ToString()) ||
                                            Decimal.Parse(CheckDatas.Rows[j].Cells["检测结果"].Value.ToString()) == Decimal.Parse(dt.Rows[0]["StandardValue"].ToString()))
                                        {
                                            CheckDatas.Rows[j].Cells["结论"].Value = "阴性";
                                        }
                                        else
                                        {
                                            CheckDatas.Rows[j].Cells["结论"].Value = "阳性";
                                        }
                                    }
                                    break;
                                case "<":
                                    {
                                        if (Decimal.Parse(CheckDatas.Rows[j].Cells["检测结果"].Value.ToString()) < Decimal.Parse(dt.Rows[0]["StandardValue"].ToString()))
                                        {
                                            CheckDatas.Rows[j].Cells["结论"].Value = "阴性";
                                        }
                                        else
                                        {
                                            CheckDatas.Rows[j].Cells["结论"].Value = "阳性";
                                        }
                                    }
                                    break;
                                case "≥":
                                    {
                                        if (Decimal.Parse(CheckDatas.Rows[j].Cells["检测结果"].Value.ToString()) > Decimal.Parse(dt.Rows[0]["StandardValue"].ToString()) ||
                                            Decimal.Parse(CheckDatas.Rows[j].Cells["检测结果"].Value.ToString()) == Decimal.Parse(dt.Rows[0]["StandardValue"].ToString()))
                                        {
                                            CheckDatas.Rows[j].Cells["结论"].Value = "阴性";
                                        }
                                        else
                                        {
                                            CheckDatas.Rows[j].Cells["结论"].Value = "阳性";
                                        }
                                    }
                                    break;

                                default:
                                    break;
                            }
                        }
                        else
                        {
                            CheckDatas.Rows[j].Cells["检测依据"].Value = "GB/T 5009.199-2003";
                            CheckDatas.Rows[j].Cells["标准值"].Value = "50";
                            if (Decimal.Parse(CheckDatas.Rows[j].Cells[3].Value.ToString()) < 50)
                            {
                                CheckDatas.Rows[j].Cells["结论"].Value = "阴性";
                            }
                            else
                            {
                                CheckDatas.Rows[j].Cells["结论"].Value = "阳性";
                            }
                        }
                    }
                    
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
                    string sql = "FtypeNmae='" + cmbSample.Text + "'" + " and  Name='" + CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测项目"].Value.ToString() + "'";
                    DataTable dt = sqlSet.GetChkItem(sql, "", out err);
                    
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测依据"].Value = dt.Rows[0]["ItemDes"].ToString();
                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["标准值"].Value = dt.Rows[0]["StandardValue"].ToString();
                        string symbol = dt.Rows[0][4].ToString();
                        switch (symbol)
                        {
                            case "≤":
                                {
                                    if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) < Decimal.Parse(dt.Rows[0][3].ToString()) ||
                                        Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) == Decimal.Parse(dt.Rows[0][3].ToString()))
                                    {
                                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["结论"].Value = "阴性";
                                    }
                                    else
                                    {
                                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["结论"].Value = "阳性";
                                    }
                                }
                                break;
                            case "<":
                                {
                                    if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) < Decimal.Parse(dt.Rows[0][3].ToString()))
                                    {
                                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["结论"].Value = "阴性";
                                    }
                                    else
                                    {
                                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["结论"].Value = "阳性";
                                    }
                                }
                                break;
                            case "≥":
                                {
                                    if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) > Decimal.Parse(dt.Rows[0][3].ToString()) ||
                                        Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) == Decimal.Parse(dt.Rows[0][3].ToString()))
                                    {
                                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["结论"].Value = "阴性";
                                    }
                                    else
                                    {
                                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["结论"].Value = "阳性";
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
                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["结论"].Value = "阴性";
                        }
                        else
                        {
                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["结论"].Value = "阳性";
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }
        private void cmbSample_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                try
                {
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
                                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["结论"].Value = "阴性";
                                    }
                                    else
                                    {
                                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["结论"].Value = "阳性";
                                    }
                                }
                                break;
                            case "<":
                                {
                                    if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测结果"].Value.ToString()) < Decimal.Parse(dt.Rows[0]["StandardValue"].ToString()))
                                    {
                                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["结论"].Value = "阴性";
                                    }
                                    else
                                    {
                                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["结论"].Value = "阳性";
                                    }
                                }
                                break;
                            case "≥":
                                {
                                    if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测结果"].Value.ToString()) > Decimal.Parse(dt.Rows[0]["StandardValue"].ToString()) ||
                                        Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测结果"].Value.ToString()) == Decimal.Parse(dt.Rows[0]["StandardValue"].ToString()))
                                    {
                                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["结论"].Value = "阴性";
                                    }
                                    else
                                    {
                                        CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["结论"].Value = "阳性";
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
                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["结论"].Value = "阴性";
                        }
                        else
                        {
                            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["结论"].Value = "阳性";
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
        private void cmbChkItem_KeyUp(object sender, KeyEventArgs e)
        {
            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = cmbChkItem.Text;
        }
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
        /// <summary>
        /// 获取输入值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbAdd_KeyUp(object sender, KeyEventArgs e)
        {
            if(cmbAdd.Text !="")
            {
                CheckDatas.CurrentCell.Value = cmbAdd.Text;
            }                       
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
        //查询显示本次测试数据
        //public void  searchdata()
        //{
        //    try
        //    {
        //        //string err = string.Empty;
        //        DataTable dt = sqlSet.GetTempData(" order by ID", out err);
        //        if (dt.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                AddTable(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(),
        //                    dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString(), dt.Rows[i][6].ToString());
        //            }
        //            CheckDatas.DataSource = SaveReadTable;
        //            for (int i = 0; i < CheckDatas.Rows.Count; i++)
        //            {
        //                if (CheckDatas.Rows[i].Cells[5].Value.ToString() == "阳性" || CheckDatas.Rows[i].Cells[5].Value.ToString() == "不合格")
        //                {
        //                    CheckDatas.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
        //                }
        //                CheckDatas.Rows[i].Cells[7].Value = Global.ChkManchine;
        //            }
        //            CheckDatas.Columns[6].Width = 180;                  
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error");
        //    }
        //}
        private byte[] ReadAndWriteToDevice()
        {
            byte[] outdatas = new byte[16];
            outdatas[0] = 0x55;
            outdatas[1] = 0x2;
            outdatas[2] = 0x1;
            outdatas[3] = 0x00;
            byte[] inputdatas = new byte[10000];
            byte[] inputs = this.StringToBytes("7E 81 00 08 27 2F 0D 0A", new string[] { ",", " " }, 16);
            if (inputs != null && inputs.Length > 0)
            {
                outdatas = inputs;
            }
            string STime = "100";
            this.MyDeviceManagement.InputAndOutputReports(0, false, outdatas, ref inputdatas, Convert.ToInt32(STime));           
            Application.DoEvents();            
            string a = this.BytesToString(inputdatas, 0, inputdatas.Length, "0x", " ", 16);
            string[] d = a.Split(' ');
            AnalyData(d);//解析数据
            CheckDatas.DataSource = SaveReadTable;
            for (int i = 0; i < CheckDatas.Rows.Count; i++)
            {
                if (CheckDatas.Rows[i].Cells["结论"].Value.ToString() == "阳性" || CheckDatas.Rows[i].Cells["结论"].Value.ToString() == "不合格")
                {
                    CheckDatas.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                }
                CheckDatas.Rows[i].Cells["检测仪器"].Value = Global.ChkManchine;
                CheckDatas.Rows[i].Cells["检测单位"].Value = unitInfo[0, 0];//检测单位
                CheckDatas.Rows[i].Cells["采样地点"].Value = unitInfo[0, 2];
                CheckDatas.Rows[i].Cells["被检单位"].Value = unitInfo[0, 1];
                CheckDatas.Rows[i].Cells["检定员"].Value = unitInfo[0, 3];
                //判断是否保存过数据
                strWhere.Length = 0;
                strWhere.AppendFormat(" CheckData='{0}'", CheckDatas.Rows[i].Cells[3].Value.ToString());
                strWhere.AppendFormat(" AND Checkitem='{0}'", CheckDatas.Rows[i].Cells["检测项目"].Value.ToString());
                strWhere.AppendFormat(" AND CheckTime=#{0}#", DateTime.Parse(CheckDatas.Rows[i].Cells["检测时间"].Value.ToString().Replace("-", "/")));
                CheckDatas.Rows[i].Cells[0].Value = (sqlSet.IsExist(strWhere.ToString()) == true ? "是" : "否");
            }
            CheckDatas.Columns[13].Width = 170;
            CheckDatas.Columns[7].Width = 160;
            CheckDatas.Columns[9].Width = 160;
            CheckDatas.Columns[14].Width = 180;
            return inputdatas;            
        }
        /// <summary>
        /// 将给定的字符串按照给定的分隔符和进制转换成字节数组
        /// </summary>
        /// <param name="str">给定的字符串</param>
        /// <param name="splitString">分隔符</param>
        /// <param name="fromBase">给定的进制</param>
        /// <returns>转换后的字节数组</returns>
        public byte[] StringToBytes(string str, string[] splitString, int fromBase)
        {
            string[] strBytes = str.Split(splitString, StringSplitOptions.RemoveEmptyEntries);
            if (strBytes == null || strBytes.Length == 0)
                return null;
            byte[] _return = new byte[strBytes.Length];
            for (int i = 0; i < strBytes.Length; i++)
            {
                try
                {
                    _return[i] = Convert.ToByte(strBytes[i], fromBase);
                }
                catch (SystemException)
                {
                    MessageBox.Show("发现不可转换的字符串->" + strBytes[i]);
                }
            }
            return _return;
        }
        /// <summary>
        /// 将给定的数组转换成符合格式要求的字符串
        /// </summary>
        /// <param name="data">给定的数组</param>
        /// <param name="startIndex">起始字节序号</param>
        /// <param name="length">转换长度</param>
        /// <param name="prefix">前缀，例如 0x </param>
        /// <param name="splitString">分隔符</param>
        /// <param name="fromBase">进制10或16,其它值一律按16进制处理</param>
        /// <returns>字符串</returns>
        public string BytesToString(byte[] data, int startIndex, int length, string prefix, string splitString, int fromBase)
        {
            string _return = "";
            if (data == null)
                return _return;
            for (int i = 0; i < length; i++)
            {
                if (startIndex + i < data.Length)
                {
                    switch (fromBase)
                    {
                        case 10:
                            _return += string.Format("{0}{1:d3}", prefix, data[i + startIndex]);
                            break;
                        default:
                            _return += string.Format("{0}{1:X2}", prefix, data[i + startIndex]);
                            break;
                    }
                    if (i < data.Length - 1)
                        _return += splitString;
                }
            }
            return _return;
        }      
        /// <summary>
        /// 解析USB返回的数据
        /// </summary>
        /// <param name="data"></param>
        private void AnalyData(string[] data)
        {
            int iCount = 32;
            string item = string.Empty;
            string  num = string.Empty ;
            int num1=0;
           
            string chktime = string.Empty;
            string colour = string.Empty;
            string chkdata = string.Empty;
            string chkresult = string.Empty;
            string temp=string.Empty ;
            int year = 0;
            int mon = 0;
            int day = 0;
            int h = 0;
            int mine = 0;
            int miao = 0;
          
            string zhi = string.Empty;
            double  Y = 0;
            double YD = 0;
            string zheng = string.Empty;
            for (int i = 0; i < data.Length; i++)
            {
                if ((data[i * iCount + 0] == "0x7E") && (data[i * iCount + 1] == "0x81") && (data[i * iCount + 2] == "0x00") && (data[i * iCount + 3] == "0x0B") && (data[i * iCount + 4] == "0x28") && (data[i * iCount + 5] == "0x00")
                        && (data[i * iCount + 6] == "0xA5") && (data[i * iCount +7] == "0x5A") && (data[i * iCount + 8] == "0xDC") && (data[i * iCount + 9] == "0x0D") && (data[i * iCount + 10] == "0x0A"))
                {
                    break;
                }
                if ((data[i * iCount + 0] == "0x7E") && (data[i * iCount + 1] == "0x81") && (data[i * iCount + 2] == "0x00") && (data[i * iCount + 3] == "0x1B") && (data[i * iCount + 4] == "0x28"))
                {
                    item = data[i * iCount + 5] == "0x00" ? "农药残留" : "";
                    //检测编号
                    temp = data[i * iCount + 6].Substring(data[i * iCount + 6].Length - 2, 2) + data[i * iCount + 7].Substring(data[i * iCount + 7].Length - 2, 2)
                        + data[i * iCount + 8].Substring(data[i * iCount + 8].Length - 2, 2) + data[i * iCount + 9].Substring(data[i * iCount + 9].Length - 2, 2);
                    num1 = Convert.ToInt32(temp, 16);
                    num = num1.ToString();
                    //if (data[i * iCount + 6] != "0x00")
                    //{
                    //    temp = data[i * iCount + 6].Substring(data[i * iCount + 6].Length -2,2);                            
                    //    num1 =Convert.ToInt32(temp,16);
                    //}
                    //if (data[i * iCount + 7] != "0x00")
                    //{
                    //    temp = data[i * iCount +7].Substring(data[i * iCount + 7].Length -2,2);
                    //    num2 = Convert.ToInt32(temp, 16);
                    //}
                    //if (data[i * iCount + 8] != "0x00")
                    //{
                    //    temp = data[i * iCount +8].Substring(data[i * iCount + 8].Length -2,2);
                    //    num3 = Convert.ToInt32(temp, 16);
                    //}
                    //if (data[i * iCount + 9] != "0x00")
                    //{
                    //    temp = data[i * iCount + 9].Substring(data[i * iCount + 9].Length - 2, 2);
                    //    num4 = Convert.ToInt32(temp, 16);
                    //}
                    ////序列号
                    //num =num1.ToString ()+num2.ToString()+num3.ToString ()+num4.ToString(); 
                    //时间
                    temp = data[i * iCount + 10].Substring(data[i * iCount + 10].Length - 2, 2);
                    year = Convert.ToInt32(temp,16);
                    temp = data[i * iCount + 11].Substring(data[i * iCount + 11].Length - 2, 2);
                    mon = Convert.ToInt32(temp, 16);
                    temp = data[i * iCount + 12].Substring(data[i * iCount + 12].Length - 2, 2);
                    day = Convert.ToInt32(temp, 16);
                    temp = data[i * iCount + 13].Substring(data[i * iCount + 13].Length - 2, 2);
                    h = Convert.ToInt32(temp, 16);
                    temp = data[i * iCount + 14].Substring(data[i * iCount + 14].Length - 2, 2);
                    mine= Convert.ToInt32(temp, 16);
                    temp = data[i * iCount + 15].Substring(data[i * iCount + 15].Length - 2, 2);
                    miao = Convert.ToInt32(temp, 16);
                    chktime = "20" + year.ToString() + "/" + mon.ToString() + "/" + day.ToString() + " " + h.ToString ()+":"+mine.ToString ()+":"+miao.ToString();
                    //chktime = "20" + Convert.ToString(data[i * iCount + 6], 10) + "-" + Convert.ToString(data[i * iCount + 7], 10) + "-" + Convert.ToString(data[i * iCount + 8], 10)
                    //    + " " + Convert.ToString(data[i * iCount + 9], 10) + ":" + Convert.ToString(data[i * iCount + 10], 10) + ":" + Convert.ToString(data[i * iCount + 11], 10);
                    //颜色值
                    colour = "100，112，254";
                    zhi = data[i * iCount + 19].Substring(data[i * iCount + 19].Length - 2, 2) + data[i * iCount + 20].Substring(data[i * iCount + 20].Length - 2, 2) +
                        data[i * iCount + 21].Substring(data[i * iCount + 21].Length - 2, 2)+ data[i * iCount + 22].Substring(data[i * iCount + 22].Length - 2, 2);                   
                    Y = Convert.ToInt32(zhi, 16) / 10;
                    YD = Convert.ToInt32(zhi, 16) % 10;
                    //Y = Y1.ToString();
                    //检测数据
                    chkdata = Y.ToString() +"."+YD.ToString ();
                    //结论
                    chkresult = data[i * iCount + 23] == "0x01" ? "阴性" : "阳性";
                    TableNewRow(item, num, chktime, colour, chkdata, chkresult);
                }
                //else
                //{
                //    if ((data[i * iCount + 1] == "0x7E") && (data[i * iCount + 2] == "0x81") && (data[i * iCount + 3] == "0x00") && (data[i * iCount + 4] == "0x0B") && (data[i * iCount + 5] == "0x28") && (data[i * iCount + 5] == "0x00")
                //        && (data[i * iCount + 5] == "0xA5") && (data[i * iCount + 5] == "0x5A") && (data[i * iCount + 5] == "0xDC") && (data[i * iCount + 5] == "0x0D") && (data[i * iCount + 5] =="0x0A"))
                //    {
                //        return;
                //    }
                //}
            }
        }
        /// <summary>
        /// 数据保存到表格
        /// </summary>
        /// <param name="item"></param>
        /// <param name="num"></param>
        /// <param name="chktime"></param>
        /// <param name="colour"></param>
        /// <param name="chkdata"></param>
        /// <param name="chkresult"></param>
        private void TableNewRow(string item, string num, string chktime, string colour, string chkdata, string chkresult)
        {
            DataRow dr;
            dr = SaveReadTable.NewRow();

            dr["已保存"] = "否";
            dr["样品名称"] = "";
            dr["检测项目"] = item;
            dr["检测结果"] = chkdata;
            dr["单位"] = "%";
            dr["检测依据"] = "";
            dr["标准值"] = "";
            dr["检测仪器"] = "";
            dr["结论"] = chkresult;
            dr["检测单位"] = "";
            dr["采样时间"] = System.DateTime.Now.ToString() ;
            dr["采样地点"] = "";
            dr["被检单位"] = "";
            dr["检定员"] = "";
            dr["检测时间"] = chktime;
            dr["样品种类"] = "";
            dr["检测数量"] = "";
            SaveReadTable.Rows.Add(dr);
        }
        /// <summary>
        /// 读取USB口数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override  void BtnReadHis_Click(object sender, EventArgs e)
        {
            SaveReadTable.Clear();//清除缓存的数据
            clsLZ2000();
            CheckDatas.DataSource = null;

            Global.hid_vid = "72f";
            Global.hid_pid = "a032";
            try
            {
                if (FindTheHid(Global.hid_vid, Global.hid_pid))
                {
                   
                }
                else
                {
                    MessageBox.Show("找不到设备","提示");
                    return;
                }
                //  Don't allow another transfer request until this one completes.
                //  Move the focus away from cmdOnce to prevent the focus from 
                //  switching to the next control in the tab order on disabling the button.                         
                BtnReadHis.Enabled = false;
                ReadAndWriteToDevice();
                Global.TableRowNum = SaveReadTable.Rows.Count; 
                BtnReadHis.Enabled  = true;    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message ,"Error");
                return;
            }
        }
        ///  <summary>
        ///  用VID和PID查找HID设备
        ///  </summary>
        ///  <returns>
        ///   True： 找到设备
        ///  </returns>
        private Boolean FindTheHid(string iVid, string iPid)
        {
            int myVendorID = 0x03EB;
            int myProductID = 0x2013;
            if (iVid != null && iPid != null)
            {
                int vid = 0;
                int pid = 0;
                try
                {
                    vid = Convert.ToInt32(iVid, 16);
                    pid = Convert.ToInt32(iPid, 16);
                    myVendorID = vid;
                    myProductID = pid;
                }
                catch (SystemException e)
                {
                    MessageBox.Show(e.Message, "Error");
                }
            }
            return this.MyDeviceManagement.findHidDevices(ref myVendorID, ref myProductID);//, this);
        }

        public void clsLZ2000()
        {
            if (!m_IsCreatedDataTable)
            {
                SaveReadTable = new DataTable("checkDtbl");//去掉Static
                DataColumn dataCol;
               
                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "已保存";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);//int,string
                dataCol.ColumnName = "样品名称";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测项目";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测结果";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "单位";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测依据";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "标准值";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测仪器";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "结论";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测单位";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "采样时间";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "采样地点";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "被检单位";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检定员";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测时间";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "样品种类";
                SaveReadTable.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测数量";
                SaveReadTable.Columns.Add(dataCol);

                m_IsCreatedDataTable = true;
            }
        }
       
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnDatsave_Click(object sender, EventArgs e)
        {
            clsSaveResult resultdata = new clsSaveResult();
            int isave = 0;
            int iok = 0;
            string chk = "";
            //string err = string.Empty;
            try
            {
                if (CheckDatas.Rows.Count > 0)
                {
                    for (int i = 0; i < CheckDatas.Rows.Count; i++)
                    {
                        if (CheckDatas.Rows[i].Cells["已保存"].Value.ToString() != "是")
                        {
                            resultdata.Save = "是";
                            //resultdata.Gridnum = CheckDatas.Rows[i].Cells[1].Value.ToString();
                            resultdata.SampleName = CheckDatas.Rows[i].Cells["样品名称"].Value.ToString().Trim();
                            resultdata.Checkitem = CheckDatas.Rows[i].Cells["检测项目"].Value.ToString().Trim();
                            resultdata.CheckData = CheckDatas.Rows[i].Cells["检测结果"].Value.ToString().Trim();
                            resultdata.Unit = CheckDatas.Rows[i].Cells["单位"].Value.ToString().Trim();
                            resultdata.Testbase = CheckDatas.Rows[i].Cells["检测依据"].Value.ToString().Trim();
                            resultdata.LimitData = CheckDatas.Rows[i].Cells["标准值"].Value.ToString().Trim();//标准值
                            resultdata.Instrument = CheckDatas.Rows[i].Cells["检测仪器"].Value.ToString().Trim();//检测仪器
                            resultdata.Result = CheckDatas.Rows[i].Cells["结论"].Value.ToString().Trim();
                            resultdata.detectunit = CheckDatas.Rows[i].Cells["检测单位"].Value.ToString().Trim();//检测单位
                            resultdata.Gettime = CheckDatas.Rows[i].Cells["采样时间"].Value.ToString().Trim();//采样时间
                            resultdata.Getplace = CheckDatas.Rows[i].Cells["采样地点"].Value.ToString().Trim();
                            resultdata.CheckUnit = CheckDatas.Rows[i].Cells["被检单位"].Value.ToString().Trim();
                            resultdata.Tester = CheckDatas.Rows[i].Cells["检定员"].Value.ToString().Trim();
                            //chk = CheckDatas.Rows[i].Cells["检测时间"].Value.ToString().Replace("-", "/").Trim();
                            resultdata.CheckTime = CheckDatas.Rows[i].Cells["检测时间"].Value.ToString().Trim();
                            resultdata.SampleType = CheckDatas.Rows[i].Cells["样品种类"].Value.ToString();//样品种类
                            resultdata.sampleNum = CheckDatas.Rows[i].Cells["检测数量"].Value.ToString();//检测样品数量
                            
                            iok = sqlSet.ResuInsert(resultdata, out err);
                            if(iok ==1)
                            {
                                isave = isave + 1;
                                CheckDatas.Rows[i].Cells["已保存"].Value = "是";
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
                MessageBox.Show(ex.Message, "Error");
            }
        }

        /// <summary>
        /// 添加数据到表
        /// </summary>
        /// <param name="save"></param>
        /// <param name="num"></param>
        /// <param name="item"></param>
        /// <param name="chkdata"></param>
        /// <param name="Unit"></param>
        /// <param name="Result"></param>
        /// <param name="CheckTime"></param>
        private void AddTable(string save,string num, string item,string chkdata, string Unit,string Result, string CheckTime)
        {
            DataRow dr;
            dr = SaveReadTable.NewRow();
 
            dr["已保存"] = save;
            dr["样品名称"] = "";
            dr["检测项目"] = item;
            dr["检测结果"] = chkdata;
            dr["单位"] = Unit;//检测值
            dr["检测依据"] = "";
            dr["标准值"] = "";
            dr["检测仪器"] = "";
            dr["结论"] = Result;
            dr["检测单位"] = "";
            dr["采样时间"] = System.DateTime.Now.ToString();
            dr["采样地点"] = "";
            dr["被检单位"] = "";
            dr["检定员"] = "";
            dr["检测时间"] = CheckTime;
            dr["检测数量"] = "1";
            dr["样品种类"] = "";
           
            SaveReadTable.Rows.Add(dr);
        }
        //public MainForm mf = new MainForm();            
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnadd_Click(object sender, EventArgs e)
        {
            //string err=string.Empty ;
            //Global.EditorSave = null;
            //string[,] arr = new string[CheckDatas.Rows.Count, 7];
            //for(int i=0;i<CheckDatas.Rows.Count ;i++)
            //{
            //    arr[i, 0] = CheckDatas.Rows[i].Cells[0].Value.ToString();
            //    arr[i, 1] = CheckDatas.Rows[i].Cells[1].Value.ToString();
            //    arr[i, 2] = CheckDatas.Rows[i].Cells[2].Value.ToString();
            //    arr[i, 3] = CheckDatas.Rows[i].Cells[3].Value.ToString();
            //    arr[i, 4] = CheckDatas.Rows[i].Cells[4].Value.ToString();
            //    arr[i, 5] = CheckDatas.Rows[i].Cells[5].Value.ToString();
            //    arr[i, 6] = CheckDatas.Rows[i].Cells[6].Value.ToString();
            //}
            //Global.EditorSave = arr;
            //function.frmSetResult Sform = new function.frmSetResult();
            //DialogResult dr = Sform.ShowDialog(this);
            //if (dr == DialogResult.OK)
            //{
            //    for (int j = 0; j < CheckDatas.Rows.Count; j++)
            //    {
            //        CheckDatas.Rows[j].Cells[0].Value="是";
            //        sqlSet.UpdateTempResult("是",CheckDatas.Rows[j].Cells[1].Value.ToString() ,out err);
            //    }

            //}
            //Sform.Show();
        }
        protected override void BtnClear_Click(object sender, EventArgs e)
        {
            SaveReadTable.Clear();
            CheckDatas.DataSource = null;
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
                cmbChker.Visible = false;
                cmbDetectUnit.Visible = false;
                cmbGetSampleAddr.Visible = false;               
                cmbSample.Visible = false;
                return;
            }
            try
            {
                if (CheckDatas.CurrentCell.ColumnIndex > -1 && CheckDatas.CurrentCell.RowIndex > -1)
                {
                    if (CheckDatas.CurrentCell.ColumnIndex == 2)
                    {
                        cmbAdd.Visible = false;
                        cmbSample.Visible = false;
                        cmbChkItem.Text = CheckDatas.CurrentCell.Value.ToString();
                        Rectangle irect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                        cmbChkItem.Left = irect.Left;
                        cmbChkItem.Top = irect.Top;
                        cmbChkItem.Width = irect.Width;
                        cmbChkItem.Height = irect.Height;
                        cmbChkItem.Visible = true;
                        cmbChker.Visible = false;
                        cmbDetectUnit.Visible = false;
                        cmbGetSampleAddr.Visible = false;
                       
                    }
                    else if (CheckDatas.CurrentCell.ColumnIndex == 1)
                    {
                        cmbChkItem.Visible = false;
                        cmbAdd.Visible = false;
                        cmbSample.Visible = true;
                        cmbSample.Text = CheckDatas.CurrentCell.Value.ToString();
                        Rectangle srect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                        cmbSample.Left = srect.Left;
                        cmbSample.Top = srect.Top;
                        cmbSample.Width = srect.Width;
                        cmbSample.Height = srect.Height;
                        cmbChker.Visible = false;
                        cmbDetectUnit.Visible = false;
                        cmbGetSampleAddr.Visible = false;
                                           
                    }
                    else if (CheckDatas.CurrentCell.ColumnIndex == 9)//检测单位
                    {
                        cmbChkUnit.Text = CheckDatas.CurrentCell.Value.ToString();
                        CheckDatas.EditMode = DataGridViewEditMode.EditOnKeystroke;
                        Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                        cmbChkUnit.Left = rect.Left;
                        cmbChkUnit.Top = rect.Top;
                        cmbChkUnit.Width = rect.Width;
                        cmbChkUnit.Height = rect.Height;
                        cmbChkUnit.Visible = true;
                        cmbAdd.Visible = false;
                        cmbChker.Visible = false;
                        cmbDetectUnit.Visible = false;
                        cmbGetSampleAddr.Visible = false;
                        cmbChkItem.Visible = false;
                        cmbSample.Visible = false;
                    }
                    else if (CheckDatas.CurrentCell.ColumnIndex == 10)//采样时间
                    {
                        cmbAdd.Text = CheckDatas.CurrentCell.Value.ToString();
                        CheckDatas.EditMode = DataGridViewEditMode.EditOnKeystroke;
                        Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                        cmbAdd.Left = rect.Left;
                        cmbAdd.Top = rect.Top;
                        cmbAdd.Width = rect.Width;
                        cmbAdd.Height = rect.Height;
                        cmbChkUnit.Visible = false;
                        cmbAdd.Visible = true;
                        cmbChker.Visible = false;
                        cmbDetectUnit.Visible = false;
                        cmbGetSampleAddr.Visible = false;
                        cmbChkItem.Visible = false;
                        cmbSample.Visible = false;
                    }
                    else if (CheckDatas.CurrentCell.ColumnIndex == 11)//采样地点
                    {
                        cmbGetSampleAddr.Text = CheckDatas.CurrentCell.Value.ToString();
                        CheckDatas.EditMode = DataGridViewEditMode.EditOnKeystroke;
                        Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                        cmbGetSampleAddr.Left = rect.Left;
                        cmbGetSampleAddr.Top = rect.Top;
                        cmbGetSampleAddr.Width = rect.Width;
                        cmbGetSampleAddr.Height = rect.Height;
                        cmbChkUnit.Visible = false;
                        cmbChker.Visible = false;
                        cmbDetectUnit.Visible = false;
                        cmbGetSampleAddr.Visible = true;
                        cmbAdd.Visible = false;
                        cmbChkItem.Visible = false;
                        cmbSample.Visible = false;
                    }
                    else if (CheckDatas.CurrentCell.ColumnIndex == 12)//被检单位
                    {
                        cmbDetectUnit.Text = CheckDatas.CurrentCell.Value.ToString();
                        CheckDatas.EditMode = DataGridViewEditMode.EditOnKeystroke;
                        Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                        cmbDetectUnit.Left = rect.Left;
                        cmbDetectUnit.Top = rect.Top;
                        cmbDetectUnit.Width = rect.Width;
                        cmbDetectUnit.Height = rect.Height;
                        cmbChkUnit.Visible = false;
                        cmbChker.Visible = false;
                        cmbDetectUnit.Visible = true;
                        cmbGetSampleAddr.Visible = false;
                        cmbAdd.Visible = false;
                        cmbChkItem.Visible = false;
                        cmbSample.Visible = false;
                    }
                    else if (CheckDatas.CurrentCell.ColumnIndex == 13)//检测员
                    {
                        cmbChker.Text = CheckDatas.CurrentCell.Value.ToString();
                        CheckDatas.EditMode = DataGridViewEditMode.EditOnKeystroke;
                        Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                        cmbChker.Left = rect.Left;
                        cmbChker.Top = rect.Top;
                        cmbChker.Width = rect.Width;
                        cmbChker.Height = rect.Height;
                        cmbChkUnit.Visible = false;
                        cmbChker.Visible = true;
                        cmbDetectUnit.Visible = false;
                        cmbGetSampleAddr.Visible = false;
                        cmbAdd.Visible = false;
                        cmbChkItem.Visible = false;
                        cmbSample.Visible = false;
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

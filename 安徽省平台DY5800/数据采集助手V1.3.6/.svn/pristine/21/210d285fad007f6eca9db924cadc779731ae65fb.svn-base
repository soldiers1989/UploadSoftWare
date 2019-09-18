using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorkstationUI.Basic;
using WorkstationDAL.HID;
using WorkstationModel.ATP;
using WorkstationBLL.Mode;
using WorkstationDAL.Model;
using WorkstationDAL.Basic;
using WorkstationModel.Model;

namespace WorkstationUI.machine
{
    public partial class ucDY6100 : BasicContent
    {
        private clsSetSqlData sqlSet = new clsSetSqlData();
        private bool IsCreatDT = false;//指示是否创建数据表对象
        private DataTable checkDtbl = new DataTable("checkDtbl");
        private DeviceManagement MyDeviceManagement = new DeviceManagement();
        private int count = 0;
        private List<clsATP> _atpLsit = new List<clsATP>();
        private StringBuilder strWhere = new StringBuilder();
        private clsSetSqlData sqldata = new clsSetSqlData();
        private clsSaveResult resultdata = new clsSaveResult();
        private ComboBox cmbAdd = new ComboBox();
        private ComboBox cmbChkItem = new ComboBox();//检测项目
        private ComboBox cmbSample = new ComboBox();//样品名称
        private ComboBox cmbChkUnit = new ComboBox();//检测单位
        private ComboBox cmbDetectUnit = new ComboBox();//被检单位
        private ComboBox cmbGetSampleAddr = new ComboBox();//采样地址
        private ComboBox cmbChker = new ComboBox();//检测员
        private DataTable cdt = null;
        private string[,] unitInfo = new string[1, 4];
        protected string[,] _checkItems;
        private clsdiary dy = new clsdiary();

        public ucDY6100()
        {
            InitializeComponent();
        }

        private void ucDY6100_Load(object sender, EventArgs e)
        {
            try
            {
                LbTitle.Text = Global.ChkManchine;
                string err = string.Empty;
                CreateDataTable();
                //其他信息输入
                cmbAdd.Visible = false;
                //cmbAdd.Items.Add("请选择...");
                cmbAdd.Items.Add("输入");
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

                string itemcode = string.Empty;

                if (Global.ChkManchine != "")
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
                _checkItems = clsStringUtil.GetAry(itemcode);

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
                dy.savediary(DateTime.Now.ToString(), Global.ChkManchine + "打开成功" , "成功");
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), Global.ChkManchine + "错误：" + ex.Message, "错误");
                MessageBox.Show(ex.Message, "打开");
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
        /// 查找设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnlinkcom_Click(object sender, EventArgs e)
        {         
            btnlinkcom.Enabled = false;             
            try
            {
                String strvid = "0483", strpid = "5750";
                int myVendorID = 0x03EB;
                int myProductID = 0x2013;
                int vid = 0;
                int pid = 0;
                try
                {
                    vid = Convert.ToInt32(strvid, 16);
                    pid = Convert.ToInt32(strpid, 16);
                    myVendorID = vid;
                    myProductID = pid;
                }
                catch (SystemException ex)
                {
                    throw ex;
                }
                if (MyDeviceManagement.findHidDevices(ref myVendorID, ref myProductID))
                {
                    //建立通讯
                    String cmd = "0xFF 0x08 0x30 0x00 0x00 0x00 0x38 0xFE";
                    byte[] bt = ReadAndWriteToDevice(cmd);
                    txtlink.Text = "已连接设备";
                }
                else
                {
                    txtlink.Text = "未创建连接";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            btnlinkcom.Enabled = true;

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
        private  void cmbSample_MouseClick(object sender, MouseEventArgs e)
        {
            if (DateTime.Now - dtCmbLastClick < new TimeSpan(0, 0, 0, 0, GetDoubleClickTime()))
            {
                // 双击事件处理方式
                try
                {
                    CheckDatas.CurrentCell.Value = cmbSample.Text;
                    string err = string.Empty;
                    string sql = "FtypeNmae='" + cmbSample.Text + "'" + " and  Name='" + CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[2].Value.ToString() + "'";

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
                        if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) < 50)
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
        private void cmbSample_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbAdd.Visible = false;
            cmbChkItem.Visible = false;
            string err = string.Empty;

            if (cmbSample.Text == "以下相同")
            {
                try
                {
                    for (int j = CheckDatas.CurrentCell.RowIndex; j < CheckDatas.Rows.Count; j++)
                    {
                        CheckDatas.Rows[j].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = CheckDatas.CurrentCell.Value.ToString();
                    //    string sql = "FtypeNmae='" + cmbSample.Text + "'" + " and  Name='" + CheckDatas.Rows[j].Cells[2].Value.ToString() + "'";

                    //    DataTable dt = sqlSet.GetChkItem(sql, "", out err);
                    //    if (dt != null)
                    //    {
                    //        if (dt.Rows.Count > 0)
                    //        {
                    //            CheckDatas.Rows[j].Cells["检测依据"].Value = dt.Rows[0][2].ToString();
                    //            CheckDatas.Rows[j].Cells["标准值"].Value = dt.Rows[0][3].ToString();
                    //            string symbol = dt.Rows[0][4].ToString();
                    //            switch (symbol)
                    //            {
                    //                case "≤":
                    //                    {
                    //                        if (Decimal.Parse(CheckDatas.Rows[j].Cells[3].Value.ToString()) < Decimal.Parse(dt.Rows[0][3].ToString()) ||
                    //                            Decimal.Parse(CheckDatas.Rows[j].Cells[3].Value.ToString()) == Decimal.Parse(dt.Rows[0][3].ToString()))
                    //                        {
                    //                            CheckDatas.Rows[j].Cells[8].Value = "合格";
                    //                        }
                    //                        else
                    //                        {
                    //                            CheckDatas.Rows[j].Cells[8].Value = "不合格";
                    //                        }
                    //                    }
                    //                    break;
                    //                case "<":
                    //                    {
                    //                        if (Decimal.Parse(CheckDatas.Rows[j].Cells[3].Value.ToString()) < Decimal.Parse(dt.Rows[0][3].ToString()))
                    //                        {
                    //                            CheckDatas.Rows[j].Cells[8].Value = "合格";
                    //                        }
                    //                        else
                    //                        {
                    //                            CheckDatas.Rows[j].Cells[8].Value = "不合格";
                    //                        }
                    //                    }
                    //                    break;
                    //                case "≥":
                    //                    {
                    //                        if (Decimal.Parse(CheckDatas.Rows[j].Cells[3].Value.ToString()) > Decimal.Parse(dt.Rows[0][3].ToString()) ||
                    //                            Decimal.Parse(CheckDatas.Rows[j].Cells[3].Value.ToString()) == Decimal.Parse(dt.Rows[0][3].ToString()))
                    //                        {
                    //                            CheckDatas.Rows[j].Cells[8].Value = "合格";
                    //                        }
                    //                        else
                    //                        {
                    //                            CheckDatas.Rows[j].Cells[8].Value = "不合格";
                    //                        }
                    //                    }
                    //                    break;

                    //                default:
                    //                    break;

                    //            }
                    //        }
                    //        else
                    //        {
                    //            CheckDatas.Rows[j].Cells["检测依据"].Value = "GB/T 5009.199-2003";
                    //            CheckDatas.Rows[j].Cells["标准值"].Value = "50";
                    //            if (Decimal.Parse(CheckDatas.Rows[j].Cells[3].Value.ToString()) < 50)
                    //            {
                    //                CheckDatas.Rows[j].Cells[8].Value = "合格";
                    //            }
                    //            else
                    //            {
                    //                CheckDatas.Rows[j].Cells[8].Value = "不合格";
                    //            }
                    //        }
                    //    }
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
        }
        private void cmbSample_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyValue == 13)
            //{
            //    try
            //    {
            //        string err = string.Empty;
            //        string sql = "FtypeNmae='" + cmbSample.Text + "'" + " and  Name='" + CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[2].Value.ToString() + "'";

            //        DataTable dt = sqlSet.GetChkItem(sql, "", out err);
            //        if (dt != null)
            //        {
            //            if (dt.Rows.Count > 0)
            //            {
            //                CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测依据"].Value = dt.Rows[0][2].ToString();
            //                CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["标准值"].Value = dt.Rows[0][3].ToString();
            //                string symbol = dt.Rows[0][4].ToString();
            //                switch (symbol)
            //                {
            //                    case "≤":
            //                        {
            //                            if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) < Decimal.Parse(dt.Rows[0][3].ToString()) ||
            //                                Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) == Decimal.Parse(dt.Rows[0][3].ToString()))
            //                            {
            //                                CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "合格";
            //                            }
            //                            else
            //                            {
            //                                CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "不合格";
            //                            }
            //                        }
            //                        break;
            //                    case "<":
            //                        {
            //                            if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) < Decimal.Parse(dt.Rows[0][3].ToString()))
            //                            {
            //                                CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "合格";
            //                            }
            //                            else
            //                            {
            //                                CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "不合格";
            //                            }
            //                        }
            //                        break;
            //                    case "≥":
            //                        {
            //                            if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) > Decimal.Parse(dt.Rows[0][3].ToString()) ||
            //                                Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) == Decimal.Parse(dt.Rows[0][3].ToString()))
            //                            {
            //                                CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "合格";
            //                            }
            //                            else
            //                            {
            //                                CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "不合格";
            //                            }
            //                        }
            //                        break;

            //                    default:
            //                        break;

            //                }

            //            }
            //            else
            //            {
            //                CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测依据"].Value = "GB/T 5009.199-2003";
            //                CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["标准值"].Value = "50";
            //                if (Decimal.Parse(CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[3].Value.ToString()) < 50)
            //                {
            //                    CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "合格";
            //                }
            //                else
            //                {
            //                    CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[8].Value = "不合格";
            //                }
            //            }

            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message, "Error");
            //    }
            //}
            //else
            //{
                //CheckDatas.CurrentCell.Value = cmbSample.Text;
            //}
            CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = cmbSample.Text;
        }

        private void cmbChkItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSample.Visible = false;
            cmbAdd.Visible = false;
            if (cmbChkItem.Text == "以下相同")
            {             
                for (int k = CheckDatas.CurrentCell.RowIndex; k < CheckDatas.Rows.Count; k++)
                {
                    CheckDatas.Rows[k].Cells[CheckDatas.CurrentCell.ColumnIndex].Value = CheckDatas.CurrentCell.Value.ToString();
                }
                cmbChkItem.Visible = false;
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
        /// <summary>
        /// 获取输入值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbAdd_KeyUp(object sender, KeyEventArgs e)
        {
            CheckDatas.CurrentCell.Value = cmbAdd.Text;
            //cmbAdd.Visible = false;
        }
        /// <summary>
        /// 选择给定的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                cmbAdd.Text = "";
            }
            else
            {
                CheckDatas.CurrentCell.Value = ((ComboBox)sender).Text;
                cmbAdd.Visible = false;
            }
        }

        ///  <summary>
        ///  用VID和PID查找HID设备
        ///  </summary>
        ///  <returns>
        ///   True： 找到设备
        ///  </returns>
        private Boolean FindTheHid()
        {
            String strvid = "0483", strpid = "5750";
            int myVendorID = 0x03EB, myProductID = 0x2013, vid = 0, pid = 0;
            try
            {
                vid = Convert.ToInt32(strvid, 16);
                pid = Convert.ToInt32(strpid, 16);
                myVendorID = vid;
                myProductID = pid;

                if (this.MyDeviceManagement.findHidDevices(ref myVendorID, ref myProductID))
                {
                    clsATPCommunication.getCommunication();

                    return true;
                }
            }
            catch (SystemException e)
            {
                throw e;
            }
            return false;
        }

        /// <summary>
        /// 构造表格对象
        /// </summary>
        private void CreateDataTable()
        {
            if (!IsCreatDT)
            {
                DataColumn dataCol;
              
                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "已保存";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);//int,string
                dataCol.ColumnName = "样品名称";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测项目";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测结果";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "单位";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测依据";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "标准值";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测仪器";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "结论";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测单位";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "采样时间";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "采样地点";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "被检单位";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测员";
                checkDtbl.Columns.Add(dataCol);

                dataCol = new DataColumn();
                dataCol.DataType = typeof(string);
                dataCol.ColumnName = "检测时间";
                checkDtbl.Columns.Add(dataCol);

                IsCreatDT = true;
            }
        }

        protected override void BtnReadHis_Click(object sender, EventArgs e)
        {
            BtnReadHis.Enabled = false;
            if (!FindTheHid())
            {
                MessageBox.Show("未找到ATP设备！", "Error");              
            }
            else
            {
                ReadData();
            }
            BtnReadHis.Enabled = true;
        }
        /// <summary>
        /// 读取ATP数据
        /// </summary>
        private void ReadData()
        {
            try
            {
                //获取总数据数
                count = getCount();
                int leny = 0, lenz = 0, index = 0;
                _atpLsit = new List<clsATP>();
                if (count > 0)
                {
                    //每条数据三条记录，取余
                    lenz = count % 3;
                    if (count >= 3)
                        leny = count / 3;
                    else
                        leny = count;
                    if (lenz > 0)
                        leny++;
                    for (int i = 0; i < leny; i++)
                    {
                        String cmd = clsATPCommunication.getCmd(index), str = "";
                        index += 3;
                        byte[] data = ReadAndWriteToDevice(cmd);
                        if (data[0] == 0xff && data[2] == 0x31 && data[3] == 0x01)
                        {
                            i--;
                            continue;
                            //count = (data[4] << 8) | data[5];
                        }
                        List<byte[]> dataList = clsATPCommunication.getByteList(data);
                        if (dataList.Count > 0)
                        {
                            for (int j = 0; j < dataList.Count; j++)
                            {
                                byte[] btlist = dataList[j];
                                clsATP model = new clsATP();
                                //model.Atp_CheckName = clsUserInfoOpr.NameFromCode(FrmMain.formMain.userCode);
                                String strC = "", str1 = btlist[1].ToString("X2"), str2 = btlist[2].ToString("X2"), str3 = btlist[3].ToString("X2");
                                if (!str1.Equals("00") && !str1.Equals("FE"))
                                    strC += str1;
                                if (!str2.Equals("00"))
                                    strC += str2;
                                if (!str3.Equals("00"))
                                    strC += str3;
                                if (strC.Equals(""))
                                    strC = "0";

                                model.Atp_RLU = Int32.Parse(strC, System.Globalization.NumberStyles.HexNumber).ToString();
                                model.Atp_Upper = Convert.ToString((btlist[14] + btlist[15]), 10);
                                model.Atp_Lower = Convert.ToString((btlist[16]), 10);
                                if (int.Parse(model.Atp_RLU, 0) > int.Parse(model.Atp_Upper, 0))
                                    str = "超标";
                                else if (int.Parse(model.Atp_RLU, 0) < int.Parse(model.Atp_Lower, 0))
                                    str = "警告";
                                else if (int.Parse(model.Atp_RLU, 0) >= int.Parse(model.Atp_Lower, 0) && int.Parse(model.Atp_RLU, 0) <= int.Parse(model.Atp_Upper, 0))
                                    str = "通过";
                                model.Atp_Result = str;
                                model.Atp_CheckData = "20" + Convert.ToString(btlist[5], 10) + "-"
                                    + int.Parse(Convert.ToString(btlist[6], 10)).ToString("D2")
                                    + "-" + int.Parse(Convert.ToString(btlist[7], 10)).ToString("D2");
                                model.Atp_CheckData += " " + int.Parse(Convert.ToString(btlist[8], 10)).ToString("D2") + ":"
                                    + int.Parse(Convert.ToString(btlist[9], 10)).ToString("D2")
                                    + ":" + int.Parse(Convert.ToString(btlist[17], 10)).ToString("D2");
                                if (btlist[5] != 0)
                                    _atpLsit.Add(model);
                            }
                        }
                    }
                    if (_atpLsit.Count > 0)
                    {
                        checkDtbl.Clear();
                        _atpLsit.Sort(delegate(clsATP x, clsATP y)
                        {
                            return y.Atp_CheckData.CompareTo(x.Atp_CheckData);
                        });

                        //加载数据到列表
                        foreach (clsATP item in _atpLsit)
                        {
                            DataRow dr;
                            //还要判断是否已经保存的数据               
                            strWhere.Length = 0;
                            strWhere.AppendFormat(" CheckData='{0}'", item.Atp_RLU);
                            strWhere.AppendFormat(" AND Checkitem='{0}'", "洁净度");
                            strWhere.AppendFormat(" AND CheckTime=#{0}#", DateTime.Parse(item.Atp_CheckData.Replace("-", "/")));
                           
                            dr = checkDtbl.NewRow();                        
                            dr["已保存"] = (sqlSet.IsExist(strWhere.ToString()) == true ? "是" : "否");
                            dr["样品名称"] = "";
                            dr["检测项目"] = "洁净度";
                            dr["检测结果"] = item.Atp_RLU;
                            dr["单位"] = "RLU";//检测值的单位
                            dr["检测依据"] = "";
                            dr["标准值"] = item.Atp_Upper;
                            dr["检测仪器"] = Global.ChkManchine; 
                            dr["结论"] = item.Atp_Result;
                            dr["检测单位"] = unitInfo[0, 0];//检测单位
                            dr["采样时间"] = System.DateTime.Now.ToString() ;
                            dr["采样地点"] = unitInfo[0, 2];
                            dr["被检单位"] = unitInfo[0, 1];
                            dr["检测员"] = unitInfo[0, 3];
                            dr["检测时间"] = item.Atp_CheckData;
                            checkDtbl.Rows.Add(dr);
                        }
                        CheckDatas.DataSource = checkDtbl;//绑定数据显示  
                        for (int i = 0; i < CheckDatas.Rows.Count; i++)
                        {
                            if (CheckDatas.Rows[i].Cells["结论"].Value.ToString() == "警告")
                            {
                                CheckDatas.Rows[i].DefaultCellStyle.ForeColor = Color.Orange;
                            }
                            else if (CheckDatas.Rows[i].Cells["结论"].Value.ToString() == "超标")
                            {
                                CheckDatas.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                            }                        
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 获取总记录数
        /// </summary>
        /// <returns></returns>
        private int getCount()
        {
            byte[] bt = new byte[8];
            bt[0] = 0xFF;
            bt[1] = 0x08;
            bt[2] = 0x31;
            bt[3] = 0x01;
            bt[4] = 0x00;
            bt[5] = 0x00;
            bt[6] = 0x3A;
            bt[7] = 0xFE;
            byte[] outdatas = new byte[64];
            int len = 0;
            MyDeviceManagement.WriteReport(0, false, bt, ref outdatas, 100);
            while (MyDeviceManagement.ReadReport(0, false, bt, ref outdatas, 100))
            {
                if (outdatas[2] == 0x31 && outdatas[3] == 0x01)
                {
                    len = outdatas[5];
                    break;
                }
            }
            return len;
        }
        /// <summary>
        /// 读取下位机返回的数据
        /// </summary>
        /// <returns></returns>
        private byte[] ReadAndWriteToDevice(String cmd)
        {
            int len = 64;
            byte[] inputdatas = new byte[len];
            try
            {
                byte[] outdatas = new byte[len];
                outdatas[0] = 0x55;
                outdatas[1] = 0x2;
                outdatas[2] = 0x1;
                outdatas[3] = 0x00;
                byte[] inputs = clsATPCommunication.StringToBytes(cmd, new string[] { ",", " " }, 16);
                if (inputs != null && inputs.Length > 0)
                    outdatas = inputs;
                System.Windows.Forms.Application.DoEvents();
                this.MyDeviceManagement.InputAndOutputReports(0, false, outdatas, ref inputdatas, 100);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return inputdatas;
        }

        protected override void BtnClear_Click(object sender, EventArgs e)
        {
            checkDtbl.Clear();
            CheckDatas.DataSource = checkDtbl;
        }

       
        /// <summary>
        /// 数据保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnDatsave_Click(object sender, EventArgs e)
        {

            int isave = 0;
            int iok = 0;
            string chk = "";
            string err = string.Empty;
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
                            resultdata.SampleName = CheckDatas.Rows[i].Cells["样品名称"].Value.ToString();
                            resultdata.Checkitem = CheckDatas.Rows[i].Cells["检测项目"].Value.ToString();
                            resultdata.CheckData = CheckDatas.Rows[i].Cells["检测结果"].Value.ToString();
                            resultdata.Unit = CheckDatas.Rows[i].Cells["单位"].Value.ToString();
                            resultdata.Testbase = CheckDatas.Rows[i].Cells["检测依据"].Value.ToString();
                            resultdata.LimitData = CheckDatas.Rows[i].Cells["标准值"].Value.ToString();//标准值
                            resultdata.Instrument = CheckDatas.Rows[i].Cells["检测仪器"].Value.ToString();//检测仪器
                            resultdata.Result = CheckDatas.Rows[i].Cells["结论"].Value.ToString();
                            resultdata.detectunit = CheckDatas.Rows[i].Cells["检测单位"].Value.ToString();//检测单位
                            resultdata.Gettime = CheckDatas.Rows[i].Cells["采样时间"].Value.ToString();//采样时间
                            resultdata.Getplace = CheckDatas.Rows[i].Cells["采样地点"].Value.ToString();
                            resultdata.CheckUnit = CheckDatas.Rows[i].Cells["被检单位"].Value.ToString();
                            resultdata.Tester = CheckDatas.Rows[i].Cells["检测员"].Value.ToString();
                            chk = CheckDatas.Rows[i].Cells["检测时间"].Value.ToString().Replace("-", "/").Trim();
                            resultdata.CheckTime = DateTime.Parse(chk);

                            iok = sqlSet.ResuInsert(resultdata, out err);
                            if (iok == 1)
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
                cmbChkUnit.Visible = false;
                cmbChker.Visible = false;
                cmbDetectUnit.Visible = false;
                cmbGetSampleAddr.Visible = false;
                return;
            }
            try
            {
                if (CheckDatas.CurrentCell.ColumnIndex > -1 && CheckDatas.CurrentCell.RowIndex > -1)
                {
                    if (CheckDatas.CurrentCell.ColumnIndex == 2)
                    {
                        cmbChkItem.Text = CheckDatas.CurrentCell.Value.ToString();

                        Rectangle irect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                        cmbChkItem.Left = irect.Left;
                        cmbChkItem.Top = irect.Top;
                        cmbChkItem.Width = irect.Width;
                        cmbChkItem.Height = irect.Height;
                        cmbChkItem.Visible = true;
                        cmbChkUnit.Visible = false;
                        cmbAdd.Visible = false;
                        cmbChker.Visible = false;
                        cmbDetectUnit.Visible = false;
                        cmbGetSampleAddr.Visible = false;
                        cmbSample.Visible = false;
                    }
                    else if (CheckDatas.CurrentCell.ColumnIndex == 1)
                    {
                        cmbChkItem.Visible = false;
                        cmbAdd.Visible = false;
                        cmbSample.Text = CheckDatas.CurrentCell.Value.ToString();
                        Rectangle srect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                        cmbSample.Left = srect.Left;
                        cmbSample.Top = srect.Top;
                        cmbSample.Width = srect.Width;
                        cmbSample.Height = srect.Height;
                        cmbSample.Visible = true;
                        cmbChkUnit.Visible = false;                       
                        cmbChker.Visible = false;
                        cmbDetectUnit.Visible = false;
                        cmbGetSampleAddr.Visible = false;
                    }
                    else if (CheckDatas.CurrentCell.ColumnIndex == 5)
                    {                       
                        cmbSample.Text = CheckDatas.CurrentCell.Value.ToString();
                        Rectangle srect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                        cmbSample.Left = srect.Left;
                        cmbSample.Top = srect.Top;
                        cmbSample.Width = srect.Width;
                        cmbSample.Height = srect.Height;
                        cmbSample.Visible = true;
                        cmbChkUnit.Visible = false;                       
                        cmbChker.Visible = false;
                        cmbDetectUnit.Visible = false;
                        cmbGetSampleAddr.Visible = false;
                        cmbChkItem.Visible = false;
                        cmbAdd.Visible = false;
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
                        cmbSample.Visible = false;
                        cmbChkItem.Visible = false;
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
                        cmbSample.Visible = false;
                        cmbChkItem.Visible = false;
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
                        cmbSample.Visible = false;
                        cmbChkItem.Visible = false;
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
                        cmbSample.Visible = false;
                        cmbChkItem.Visible = false;
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
                        cmbSample.Visible = false;
                        cmbChkItem.Visible = false;
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

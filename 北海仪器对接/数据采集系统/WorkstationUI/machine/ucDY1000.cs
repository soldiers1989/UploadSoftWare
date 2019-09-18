using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorkstationUI.Basic;
using WorkstationModel.Instrument  ;
using WorkstationModel.Model;
using WorkstationDAL.Basic;
using WorkstationBLL.Mode;
using WorkstationDAL.Model;
using System.IO.Ports;
using WorkstationUI.function;
using System.Configuration;
using System.Web.Script.Serialization;
using WorkstationModel.UpData;
using WorkstationModel.Thread;
using System.Collections;

namespace WorkstationUI.machine
{
    public partial class ucDY1000 :BasicContent 
    {
        public ucDY1000()
        {
            InitializeComponent();
        }
        public  clsDY3000DY curDY3000DY = new clsDY3000DY();
        protected string[,] _checkItems;
        private delegate void InvokeDelegate(DataTable dtbl);
        private delegate void InvokeBtn(Button btn);
        private  clsSaveResult resultdata = new clsSaveResult();
        private  clsSetSqlData sqlSet = new clsSetSqlData();
        private StringBuilder strWhere = new StringBuilder();
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
        private DataTable cdt = null;
        public ArrayList arr = new ArrayList();
        private clsUpLoadData udata = new clsUpLoadData();
        private clsdiary dy = new clsdiary();
        public void ucDY1000_Load(object sender, EventArgs e)
        {
            try
            {
                label5.Visible = false;
                cmbMethod.Visible = false;
                BtnReadHis.Enabled = false;
                BtnClear.Enabled = false;
                btnDatsave.Enabled = false;
                btnadd.Enabled = false;
                GloadThread._subthread = new SubThread();
                //btnadd.Visible = false;
                string err = string.Empty;
                labelTitle.Text = Global.ChkManchine;
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
                clsUpdateMessage.LabelUpdated += clsUpdateMessage_LabelUpdated;
                MessageNotification.GetInstance().DataRead += NotificationEventHandler;
                GloadThread._subthread.MessageSend += _subthread_MessageSend;
          
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
                CheckDatas.Controls.Add(cmbDetectUnitNature);

                string itemcode = string.Empty;
            
                if (Global.ChkManchine !="")
                {
                    strWhere.Clear();
                    strWhere.AppendFormat("WHERE Name='{0}'", Global.ChkManchine);
                    DataTable dt = sqlSet.GetIntrument(strWhere.ToString(), out err);
                    if (dt != null && dt.Rows.Count > 0)
                    {                       
                        itemcode = dt.Rows[0][4].ToString();                    
                    }
                }
                _checkItems = clsStringUtil.GetDY3000DYAry(itemcode);
                if (Global.ChkManchine == "DY-6600手持执法快检通")
                {
                    label5.Visible = true;
                    cmbMethod.Visible = true;
                    label5.Location = new Point(680,52);
                    cmbMethod.Location = new Point(710,49);
                    BtnReadHis.Location = new Point(790, 49);
                    BtnClear.Location = new Point(870, 49);
                    btnDatsave.Location = new Point(950, 49);
                    btnadd.Location = new Point(1035, 49);

                    string[] arrTEM = itemcode.Split('}');
                    string Temp62 = string.Empty;
                    string Temp42 = string.Empty;
                    string Temp72 = string.Empty;

                    for (int i = 0; i < arrTEM.Length; i++)
                    {
                        if (arrTEM[i].Contains("金标法"))
                            Temp62 = Temp62 + arrTEM[i].ToString() + "}";
                        if (arrTEM[i].Contains("非试纸法"))
                            Temp42 = Temp42 + arrTEM[i].ToString() + "}";
                        if (arrTEM[i].Contains("干化学法"))
                            Temp72 = Temp72 + arrTEM[i].ToString() + "}";
                    }
                    clsDY3000DY.CheckItemsArray = clsStringUtil.GetDY3000DYAry(Temp42);
                    clsDY3000DY.CheckItemsArray62 = clsStringUtil.GetDY3000DYAry(Temp62);
                    clsDY3000DY.CheckItemsArray72 = clsStringUtil.GetDY3000DYAry(Temp72);
                }
                else
                {
                    clsDY3000DY.CheckItemsArray = _checkItems;
                    clsDY3000DY.CheckItemsArray62 = _checkItems;
                    clsDY3000DY.CheckItemsArray72 = _checkItems;
                }  
           
                if (Global.ChkManchine != "DY-6600手持执法快检通")
                {
                    for (int j = 0; j < _checkItems.GetLength(0); j++)
                    {
                        cmbChkItem.Items.Add(_checkItems[j, 0]);
                        DataTable dt = sqlSet.GetSample("Name='" + _checkItems[j, 0] + "'", "idx", out err);
                        if (dt != null && dt.Rows.Count > 0)
                        {                        
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                cmbSample.Items.Add(dt.Rows[i]["FtypeNmae"].ToString());
                            }                           
                        }
                    }
                }

                //cdt = sqlSet.GetExamedUnit("", "", out err);
                //if (cdt != null && cdt.Rows.Count > 0)
                //{
                //    for (int n = 0; n < cdt.Rows.Count; n++)
                //    {
                //        cmbDetectUnit.Items.Add(cdt.Rows[n][2].ToString());//被检单位
                //        cmbGetSampleAddr.Items.Add(cdt.Rows[n][3].ToString());//采样地址
                //        curDY3000DY.unitInfo[0, 1] = cdt.Rows[n][3].ToString();
                //        curDY3000DY.unitInfo[0, 2] = cdt.Rows[n][2].ToString();
                //    }
                //}
                cdt = sqlSet.GetInformation("", "", out err);
                
                if (cdt != null && cdt.Rows.Count > 0)
                {
                    for (int n = 0; n < cdt.Rows.Count; n++)
                    {
                        if (cdt.Rows[n]["iChecked"].ToString() == "是")
                        {
                            curDY3000DY.unitInfo[0, 0] = cdt.Rows[n]["TestUnitName"].ToString();//检测单位
                            curDY3000DY.unitInfo[0, 1] = cdt.Rows[n]["DetectUnitName"].ToString();//被检单位
                            curDY3000DY.unitInfo[0, 2] = cdt.Rows[n]["Tester"].ToString();//检测人
                            //curDY3000DY.unitInfo[0, 3] = cdt.Rows[n]["SampleAddress"].ToString();//采样地址
                            curDY3000DY.unitInfo[0, 3] = cdt.Rows[n]["DetectUnitNature"].ToString();//被检单位性质
                            curDY3000DY.unitInfo[0, 4] = cdt.Rows[n]["ProductAddr"].ToString();//产地
                            curDY3000DY.unitInfo[0, 5] = cdt.Rows[n]["ProductCompany"].ToString();//生产单位
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
                dy.savediary(DateTime.Now.ToString(), Global.ChkManchine + "打开成功", "成功");
            }
            catch (Exception ex)
            {
                dy.savediary(DateTime.Now.ToString(), Global.ChkManchine+"错误：" + ex.Message, "错误");
                MessageBox.Show(ex.Message, "打开");
            }
        }

        private void cmbProductAddr_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private  void cmbDetectUnitNature_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private  void cmbDetectUnitNature_MouseClick(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void cmbProductCompany_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private  void cmbProductCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void cmbProductCompany_MouseClick(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private  void cmbProductAddr_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void cmbProductAddr_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        public delegate void MessageHandler(MessageEventArgs e);
        public void Message(MessageEventArgs e)
        {
            BtnClear.Enabled = true;
            BtnReadHis.Enabled = true;         
        }
        /// <summary>
        /// 定时线程消息事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _subthread_MessageSend(object sender, MessageEventArgs e)
        {
            if (e.Message == "恢复按钮")
            {
                //实例化代理
                MessageHandler handler = new MessageHandler(Message);
                //调用Invoke
                this.Invoke(handler, new object[] { e });
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
        /// 消息更新事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected  void clsUpdateMessage_LabelUpdated(object sender, clsUpdateMessage.LabelUpdateEventArgs e)
        {
            if (e.Code == "RS232DY3000")
            {
                labelTitle.Text = Global.ChkManchine;
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
                    //CheckDatas.CurrentCell.Value = cmbSample.Text.Trim();
                    string err = string.Empty;
                    //if (CheckDatas.CurrentCell.Value.ToString().Trim() != "")
                    //{
                        FrmSearchSample window = new FrmSearchSample();
                        window._item = CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测项目"].Value.ToString().Trim();
                        window.ShowDialog();
                        CheckDatas.CurrentCell.Value = cmbSample.Text = Global.iSampleName;
                        string sql = "FtypeNmae='" + Global.iSampleName + "'" + " and  Name='" + CheckDatas.Rows[CheckDatas.CurrentCell.RowIndex].Cells["检测项目"].Value.ToString().Trim() + "'";
                        DataTable dt = sqlSet.GetChkItem(sql, "", out err);

                        if (dt != null && dt.Rows.Count > 0)
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

                    //}
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

            if (Global.ChkManchine == "DY-6600手持执法快检通")
            {
                if (cmbSample.Text == "以下相同")
                {
                    for (int j = CheckDatas.CurrentCell.RowIndex; j < CheckDatas.Rows.Count; j++)
                    {
                      
                        CheckDatas.Rows[j].Cells[1].Value = CheckDatas.CurrentCell.Value.ToString();

                        string sql = "sampleName='" + cmbSample.Text + "'" + " and  itemName='" + CheckDatas.Rows[j].Cells[2].Value.ToString() + "'";

                        DataTable dt = sqlSet.GetItemStandard(sql, "", out err);
                        if (dt != null && dt.Rows.Count >0)
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
                        for (int j = CheckDatas.CurrentCell.RowIndex; j < CheckDatas.Rows.Count; j++)
                        {
                            CheckDatas.Rows[j].Cells[1].Value = CheckDatas.CurrentCell.Value.ToString();
                            string sql = "FtypeNmae='" + cmbSample.Text + "'" + " and  Name='" + CheckDatas.Rows[j].Cells[2].Value.ToString() + "'";
                            DataTable dt = sqlSet.GetChkItem(sql, "", out err);
                            if (dt != null)
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    CheckDatas.Rows[j].Cells["检测依据"].Value = dt.Rows[0][2].ToString();
                                    CheckDatas.Rows[j].Cells["标准值"].Value = dt.Rows[0][3].ToString();
                                    string symbol = dt.Rows[0][4].ToString();
                                    switch (symbol)
                                    {
                                        case "≤":
                                            {
                                                if (Decimal.Parse(CheckDatas.Rows[j].Cells[3].Value.ToString()) < Decimal.Parse(dt.Rows[0][3].ToString()) ||
                                                    Decimal.Parse(CheckDatas.Rows[j].Cells[3].Value.ToString()) == Decimal.Parse(dt.Rows[0][3].ToString()))
                                                {
                                                    CheckDatas.Rows[j].Cells[8].Value = "合格";
                                                }
                                                else
                                                {
                                                    CheckDatas.Rows[j].Cells[8].Value = "不合格";
                                                }
                                            }
                                            break;
                                        case "<":
                                            {
                                                if (Decimal.Parse(CheckDatas.Rows[j].Cells[3].Value.ToString()) < Decimal.Parse(dt.Rows[0][3].ToString()))
                                                {
                                                    CheckDatas.Rows[j].Cells[8].Value = "合格";
                                                }
                                                else
                                                {
                                                    CheckDatas.Rows[j].Cells[8].Value = "不合格";
                                                }
                                            }
                                            break;
                                        case "≥":
                                            {
                                                if (Decimal.Parse(CheckDatas.Rows[j].Cells[3].Value.ToString()) > Decimal.Parse(dt.Rows[0][3].ToString()) ||
                                                    Decimal.Parse(CheckDatas.Rows[j].Cells[3].Value.ToString()) == Decimal.Parse(dt.Rows[0][3].ToString()))
                                                {
                                                    CheckDatas.Rows[j].Cells[8].Value = "合格";
                                                }
                                                else
                                                {
                                                    CheckDatas.Rows[j].Cells[8].Value = "不合格";
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
                                        CheckDatas.Rows[j].Cells[8].Value = "合格";
                                    }
                                    else
                                    {
                                        CheckDatas.Rows[j].Cells[8].Value = "不合格";
                                    }
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
        /// <summary>
        /// 关闭串口
        /// </summary>
        public void closecom()
        {
            curDY3000DY.Close();
        }

        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnlinkcom_Click(object sender, EventArgs e)
        {
            btnlinkcom.Enabled = false;
          
            WorkstationDAL.Model.clsShareOption.ComPort = cmbCOMbox.Text;
            
            if (btnlinkcom.Text == "连接设备")
            {
                try
                {
                    if (!curDY3000DY.Online)
                    {
                        curDY3000DY.Open();
                        curDY3000DY.communicate();//通信测试
                        //txtlink.Text = "已连接设备";
                        //btnlinkcom.Text = "断开设备";
                        //MessageBox.Show("仪器连接成功", "提示",MessageBoxButtons.OK ,MessageBoxIcon.Information);
                    }
                    else
                    {
                        //curDY3000DY.Close();
                        MessageBox.Show("串口已打开", "提示");
                    }
                }
                catch (JH.CommBase.CommPortException ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            else if (btnlinkcom.Text == "断开设备")
            {
                curDY3000DY.Close();
                txtlink.Text = "未连接";
                btnlinkcom.Text = "连接设备"; 
            }
            btnlinkcom.Enabled = true;
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
        /// <summary>
        /// 数据读取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void BtnReadHis_Click(object sender, EventArgs e)
        {
            bool blOnline = false;
            try
            {
                blOnline = curDY3000DY.Online;// Global.IsConnect;
            }
            catch (JH.CommBase.CommPortException ex)
            {
                MessageBox.Show(ex.Message + "，无法与仪器正常通讯，请重启界面！");
                BtnReadHis.Enabled = true;
                BtnClear.Enabled = true;
                return;
            }

            if (!blOnline)
            {
                MessageBox.Show(this, "串口连接有误!", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cursor = Cursors.Default;
                BtnReadHis.Enabled = true;
                BtnClear.Enabled = true;
                return;
            }
            if (blOnline)
            {
                //DTPEnd.Value = DTPStart.Value.Date.AddDays(1).AddSeconds(-1);            
                BtnReadHis.Enabled = false;
                BtnClear.Enabled = false;
                Cursor = Cursors.WaitCursor;
                if (Global.ChkManchine == "DY-6600手持执法快检通")
                {
                    if (cmbMethod.Text == "")
                    {
                        MessageBox.Show("请选择检测方式", "提示");
                        BtnReadHis.Enabled = true ;
                        BtnClear.Enabled = true;
                        return;
                    }
                    cmbSample.Items.Clear();
                    arr.Clear();
                    cmbSample.Items.Add("以下相同");
                    cmbSample.Items.Add("删除");
                    curDY3000DY.ReadHistory2(DTPStart.Value.Date, DTPEnd.Value, cmbMethod.Text);
                }
                else
                {
                    curDY3000DY.ReadHistory(DTPStart.Value.Date, DTPEnd.Value);
                }
                Global.Rdt.Elapsed += Rdt_Elapsed;
                Global.Rdt.AutoReset = false;//只执行一次
                Global.Rdt.Enabled = true;
            }
        }
        /// <summary>
        /// 定时时间到
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  void Rdt_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            GloadThread._subthread.StartSend();//启动线程
        }
        /// <summary>
        /// 清楚数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void BtnClear_Click(object sender, EventArgs e)
        {
            CheckDatas.DataSource = null;
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
            else
            {          
                DataView vie = curDY3000DY.DataReadTable.DefaultView;
                //vie.RowFilter = "检测时间 > #" + DTPStart.Value.Date + "# and 检测时间 < #" + DTPEnd.Value + "#";
                DataTable dt = vie.ToTable();
                ShowResult(dt, true);
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
            BtnReadHis.Enabled = true ;
            BtnClear.Enabled = true ;
            btnDatsave.Enabled = true ;
            btnadd.Enabled = true ;
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

            if (!cleared && dtbl.Rows.Count <= 0)
            {
                string msg = "没有采集到相应数据,可能是仪器没有相应检测数据!";
                MessageBox.Show(msg, "无数据", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
            string err="";
            DataView dv = null;
            if (dtbl.Rows.Count > 0)
            {
                dv = dtbl.DefaultView;
                dv.Sort = "检测时间 ASC";
                CheckDatas.DataSource = dv;
                //CheckDatas.Columns["采样时间"].Width = 180;
                //CheckDatas.Columns["检测项目"].Width = 150;
                //CheckDatas.Columns["检测单位"].Width = 150;
                //CheckDatas.Columns[7].Width = 180;
                //CheckDatas.Columns[14].Width = 180;
              
                if (CheckDatas.Rows.Count > 0)
                {
                    for (int i = 0; i < CheckDatas.Rows.Count; i++)
                    {
                        if (CheckDatas.Rows[i].Cells["结论"].Value.ToString() == "不合格" || CheckDatas.Rows[i].Cells["结论"].Value.ToString() == "阳性")
                        {
                            CheckDatas.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                        }
                        if (arr.Contains(CheckDatas.Rows[i].Cells[2].Value.ToString()))
                        {
                            continue;
                        }
                        else
                        {
                            arr.Add(CheckDatas.Rows[i].Cells[2].Value.ToString());
                        }
                    }
                    if (Global.ChkManchine == "DY-6600手持执法快检通")
                    {
                        if (arr != null && arr.Count > 0)
                        {
                            for (int j = 0; j < arr.Count; j++)
                            {
                                DataTable dt = sqlSet.GetDownItemID("itemName='"+arr[j]+"'", "", out err);
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    cmbSample.Items.Add(dt.Rows[0][1].ToString());
                                }
                            }
                        }
                    }
                }       
            }           
            Cursor = Cursors.Default;

            BtnReadHis.Enabled = true;
            BtnClear.Enabled = true;
        }

        protected override void winClose()
        {
            if (MessageNotification.GetInstance() != null)
            {
                MessageNotification.GetInstance().DataRead -= NotificationEventHandler;
            }
            try
            {
                if (curDY3000DY != null)
                {
                    if (curDY3000DY.Online)
                        curDY3000DY.Close();
                    curDY3000DY = null;
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message,"Error");
            }
            base.winClose();
        }
        /// <summary>
        /// 检测数据保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnDatsave_Click(object sender, EventArgs e)
        {
            int isave = 0;
            int iok = 0;
            string chk = "";
            string err = string.Empty;
            
            btnDatsave.Enabled = false;
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
                            cdt = sqlSet.GetItemStandard("sampleName='" + CheckDatas.Rows[i].Cells["样品名称"].Value.ToString().Trim() + "' and itemName='" +
                                CheckDatas.Rows[i].Cells["检测项目"].Value.ToString().Trim() + "'", "", out err);
                            string samplecode = "";
                            if (cdt != null && cdt.Rows.Count > 0)
                            {
                                samplecode = cdt.Rows[0]["SampleNum"].ToString();
                            }

                            resultdata.Save = "是";
                            //resultdata.Gridnum = CheckDatas.Rows[i].Cells[1].Value.ToString();
                            resultdata.CheckNumber = Global.GUID(null, 1);
                            resultdata.SampleName = CheckDatas.Rows[i].Cells["样品名称"].Value.ToString().Trim();
                            resultdata.SampleCode = samplecode == "" ? "000010001" : samplecode;
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
            for (int j = 0; j < CheckDatas.Rows.Count; j++)
            {
                if (CheckDatas.Rows[j].Cells[0].Value.ToString() == "否")
                {
                    MessageBox.Show("请保存数据再上传","提示");
                    return;
                }
            }

            DialogResult tishi = MessageBox.Show("共有" + CheckDatas.Rows.Count+"条数据是否上传","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
            if (tishi == DialogResult.No)
            {
                return;
            }
            btnadd.Enabled = false;

            string err = "";
            int isupdata = 0;
            int IsSuccess = 0;
           
            string errstr = "";

            try
            {
               
                DataTable dt = sqlSet.GetResultTable("", "", 1, CheckDatas.Rows.Count, out err);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["IsUpload"].ToString() == "是")//不允许重传
                        {
                            isupdata = isupdata + 1;
                            CheckDatas.Rows[i].DefaultCellStyle.ForeColor = Color.Green;
                            continue;
                        }
                        WebReference.PutService putServer = new WebReference.PutService();
                        string web = putServer.save(
                          dt.Rows[i]["SampleName"].ToString(),//品种名称
                          dt.Rows[i]["Checkitem"].ToString(),//检测项目
                          dt.Rows[i]["SampleCategory"].ToString(),//果蔬类别
                          "",//监测模式 
                          dt.Rows[i]["DetectUnit"].ToString(),//检测单位
                          dt.Rows[i]["Tester"].ToString(),//检 测 人
                          dt.Rows[i]["SampleCode"].ToString(),//样品编号 
                          dt.Rows[i]["ProduceAddr"].ToString(),//产地
                          dt.Rows[i]["SampleAddress"].ToString(),//抽样地点
                          dt.Rows[i]["TestBase"].ToString(),//依据标准
                          dt.Rows[i]["LimitData"].ToString(),//标准值（抑制率）
                          dt.Rows[i]["CheckData"].ToString(),//实测值（抑制率）
                          dt.Rows[i]["Result"].ToString(),//单项比对结果
                          DateTime.Parse(dt.Rows[i]["CheckTime"].ToString()),//检测日期
                          DateTime.Parse(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),//上传日期
                          dt.Rows[i]["ProduceUnit"].ToString(),//生产单位
                           DateTime.Parse(dt.Rows[i]["ProductDatetime"].ToString()),//生产日期
                          dt.Rows[i]["CheckUnit"].ToString(),//被检企业
                          dt.Rows[i]["CompanyNeture"].ToString(),//被检企业性质
                          Global.IntrumManifacture,//检测仪器厂商
                          DateTime.Parse(dt.Rows[i]["SendTestDate"].ToString()),//送检日期
                          dt.Rows[i]["DoResult"].ToString(),//处理结果
                          dt.Rows[i]["ChkNum"].ToString()//业务唯一ID
                          );
                        if (web == "Ok")
                        {
                            IsSuccess = IsSuccess + 1;
                            sqlSet.SetUploadResult(dt.Rows[i]["ID"].ToString(), out err);
                        }
                        else
                        {
                            errstr = errstr + "," + web;
                        }
                    }
                }
                if (errstr == "")
                {
                    MessageBox.Show("共成功上传" + IsSuccess.ToString() + "条数据； 共" + isupdata + "条数据已传！");
                }
                else
                {
                    MessageBox.Show("共成功上传" + IsSuccess.ToString() + "条数据；共" + isupdata + "条数据已传！ 提示信息：" + errstr);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
                
            btnadd.Enabled = true;
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
                    if (CheckDatas.CurrentCell.ColumnIndex == 2)
                    {
                        cmbAdd.Visible = false;
                        cmbSample.Visible = false;
                        cmbChkUnit.Visible = false;
                        cmbChker.Visible = false;
                        cmbDetectUnit.Visible = false;
                        cmbGetSampleAddr.Visible = false;
                        cmbDetectUnitNature.Visible = false;
                        cmbProductCompany.Visible = false;
                        cmbProductAddr.Visible = false;

                        cmbChkItem.Text = CheckDatas.CurrentCell.Value.ToString();

                        Rectangle irect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                        cmbChkItem.Left = irect.Left;
                        cmbChkItem.Top = irect.Top;
                        cmbChkItem.Width = irect.Width;
                        cmbChkItem.Height = irect.Height;
                        cmbChkItem.Visible = true;
                    }
                    else if (CheckDatas.CurrentCell.ColumnIndex == 1)
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
                    else if (CheckDatas.CurrentCell.ColumnIndex ==5 || CheckDatas.CurrentCell.ColumnIndex == 6)//检测依据和标准值
                    {
                        if (Global.ChkManchine == "DY-6600手持执法快检通")
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
                        
                    }
                    else if (CheckDatas.CurrentCell.ColumnIndex == 9)//检测单位
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
                    else if (CheckDatas.CurrentCell.ColumnIndex == 10 || CheckDatas.CurrentCell.ColumnIndex>14)//采样时间
                    {

                        if (CheckDatas.CurrentCell.ColumnIndex == 18)//产地
                        {
                            cmbSample.Visible = false;
                            cmbChkItem.Visible = false;
                            cmbChkUnit.Visible = false;
                            cmbChker.Visible = false ;
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
                        else if (CheckDatas.CurrentCell.ColumnIndex == 19)//生产单位
                        {
                            cmbSample.Visible = false;
                            cmbChkItem.Visible = false;
                            cmbChkUnit.Visible = false;
                            cmbChker.Visible = false ;
                            cmbDetectUnit.Visible = false;
                            cmbGetSampleAddr.Visible = false;
                            cmbAdd.Visible = false;
                            cmbDetectUnitNature.Visible = false;
                            cmbProductCompany.Visible = true ;
                            cmbProductAddr.Visible = false;

                            cmbProductCompany.Text = CheckDatas.CurrentCell.Value.ToString();
                            Rectangle rect = CheckDatas.GetCellDisplayRectangle(CheckDatas.CurrentCell.ColumnIndex, CheckDatas.CurrentCell.RowIndex, false);
                            cmbProductCompany.Left = rect.Left;
                            cmbProductCompany.Top = rect.Top;
                            cmbProductCompany.Width = rect.Width;
                            cmbProductCompany.Height = rect.Height;

                        }
                        else
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
                       
                    }
                    else if (CheckDatas.CurrentCell.ColumnIndex == 11)//采样地点
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
                    else if (CheckDatas.CurrentCell.ColumnIndex == 12)//被检单位
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
                    else if (CheckDatas.CurrentCell.ColumnIndex == 13)//被检单位性质
                    {
                        cmbSample.Visible = false;
                        cmbChkItem.Visible = false;
                        cmbChkUnit.Visible = false;
                        cmbChker.Visible = false ;
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
                    else if (CheckDatas.CurrentCell.ColumnIndex == 14)//检测员
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
        }

        protected override void CheckDatas_Scroll(object sender, ScrollEventArgs e)
        {
            cmbAdd.Visible = false;
            cmbChkItem.Visible = false;
            cmbSample.Visible = false;
            cmbChkUnit.Visible = false;
            cmbChker.Visible = false;
            cmbDetectUnit.Visible = false;
            cmbGetSampleAddr.Visible = false;
        }
        /// <summary>
        /// 选择检测方式加载样品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }
    }
}

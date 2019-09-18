using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorkstationModel.Model;
using System.IO;
using System.IO.Ports;
using WorkstationDAL.Basic;
using WorkstationBLL.Mode;
using WorkstationModel.HID;
using WorkstationDAL.Model;
using WorkstationDAL.HID;

namespace WorkstationUI.function
{
    public partial class ucSystemset : UserControl
    {
        public ucSystemset()
        {
            InitializeComponent();
        }

        private void ucSystemset_Load(object sender, EventArgs e)
        {
            string[] Port = SerialPort.GetPortNames();

            ShowIntrument();

            if (Port.Length == 0)
            {
                cmbCom.Items.Add("没有COM口");
                cmbCom.SelectedIndex = 0;
                return;
            }
            foreach (string c in SerialPort.GetPortNames())
            {
                cmbCom.Items.Add(c);
            }
           cmbCom.SelectedIndex = 0;
        }
        clsSetCom model = new clsSetCom();
        clsSetSqlData SqlData = new clsSetSqlData();
        clsIntrument machine = new clsIntrument();
        private DeviceManagement MyDeviceManagement = new DeviceManagement();

        /// <summary>
        /// 显示仪器名称在cmb上
        /// </summary>
        private void ShowIntrument()
        {           
            try
            {
                string Sql = "order by ID";
                DataTable dt = SqlData.SearchIntrument(Sql, "");
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<clsIntrument> Instrument = (List<clsIntrument>)clsStringUtil.DataTableToIList<clsIntrument>(dt, 1);                  
                    for (int i = 0; i < Instrument.Count;i++ )                       
                    {
                        cmbIntrument.Items.Add(Instrument[i].Name);
                    }
                    cmbIntrument.SelectedIndex = 19;
                }              
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常!\n" + ex.Message);
            }
        }

        /// <summary>
        /// 保存串口号到数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnadd_Click(object sender, EventArgs e)
        {
            string err = string.Empty;

            //插入新的数据
            try
            {
                model.ComPort = cmbCom.Text.Trim();
                model.ComPort = model.ComPort.Substring(model.ComPort.IndexOf("M") + 1);
                SqlData.updateCom(model, out err);

                if (!err.Equals(string.Empty))
                {
                    MessageBox.Show(this, "数据库操作出错！");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常!\n" + ex.Message);
            }

        }
        /// <summary>
        /// 保存新添加的仪器到数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnmachine_Click(object sender, EventArgs e)
        {
            string err = string.Empty;
            if (tbxmachine.Text == string.Empty)
            {
                MessageBox.Show("请输入仪器名称再单击确定", "提示");
                return;
            }
            try
            {
                machine.Name = tbxmachine.Text;
                SqlData.updateIntrument(machine, out err);
                if (!err.Equals(string.Empty))
                {
                    MessageBox.Show(this, "数据库操作出错！");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常!\n" + ex.Message);
            }
        }

     
        //public DeviceManagement MyDeviceManagement = new DeviceManagement();
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
                    //this.richTextBox_Msg.Text += e.Message;
                }
            }

            return this.MyDeviceManagement.findHidDevices(ref myVendorID, ref myProductID);//, this);
        }
        /// <summary>
        /// 根据VID、PID查找对应的设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void cmbvid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbpid_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void btnHID_Click_1(object sender, EventArgs e)
        {
            btnHID.Enabled = false;
            UInt16 PID = 0;
            UInt16 VID = 0;
            //删除控件历史记录
            for (int i = 0; i < cmbvid.Items.Count; i++)
            {
                cmbvid.Items.Remove(i);
            }


            for (int i = 0; i < cmbpid.Items.Count; i++)
            {
                cmbpid.Items.Remove(i);
            }

            hidSearch.PnPEntityInfo[] icode = hidSearch.AllUsbDevices;
            for (int i = 0; i < icode.Length; i++)
            {
                if (icode[i].Name.Contains("USB"))
                {
                    PID = icode[i].ProductID;
                    VID = icode[i].VendorID;
                    cmbpid.Items.Add(Convert.ToString(PID, 16));
                    cmbvid.Items.Add(Convert.ToString(VID, 16));
                    txthid.AppendText(icode[i].Name + " VID=" + Convert.ToString(VID, 16) + " PID=" + Convert.ToString(PID, 16) + " " + icode[i].Description + "\r\n");
                }
            }
            MessageBox.Show("搜索完成请查看", "提示");
            btnHID.Enabled = true;
        }

        private void cmbvid_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            Global.hid_vid = cmbvid.Text;  
        }

        private void cmbpid_SelectedIndexChanged(object sender, EventArgs e)
        {
            Global.hid_pid = cmbpid.Text;
        }

        private void btnmachine_Click_1(object sender, EventArgs e)
        {
            string err = string.Empty;
            if (tbxmachine.Text == string.Empty)
            {
                MessageBox.Show("请输入仪器名称再单击确定", "提示");
                return;
            }
            try
            {
                machine.Name = tbxmachine.Text;
                SqlData.updateIntrument(machine, out err);
                if (!err.Equals(string.Empty))
                {
                    MessageBox.Show(this, "数据库操作出错！");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常!\n" + ex.Message);
            }
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            try
            {

                if (FindTheHid(cmbvid.Text, cmbpid.Text))
                {
                    txtSearch.Text += "发现设备->\r\n";
                    for (int i = 0; i < MyDeviceManagement.DeviceCount; i++)
                    {
                        txtSearch.Text += this.MyDeviceManagement[i].myDevicePathName + "\r\n";
                    }

                }
            }
            catch (Exception ex)
            {
                txtSearch.Text += ex.Message + "\r\n";
                throw;
            }
        }
        /// <summary>
        /// 选择测试仪器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbIntrument_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Global.TestInstrument = cmbIntrument.Text;
        }

      
    }
}

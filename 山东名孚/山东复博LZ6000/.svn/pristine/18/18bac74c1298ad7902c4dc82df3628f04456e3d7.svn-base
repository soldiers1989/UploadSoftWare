using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FoodClient.shandong;

namespace FoodClient.Basic
{
    public partial class FrmUnitSynchronize : TitleBarBase
    {
        public FrmUnitSynchronize()
        {
            InitializeComponent();
        }

        private void FrmUnitSynchronize_Load(object sender, EventArgs e)
        {
            lblDeviceName.Text  = "检测单位同步";
        }

        private void btnUploadUnit_Click(object sender, EventArgs e)
        {
            if (txtUnitID.Text.Trim() == "")
            {
                MessageBox.Show("检测单位ID不能为空");
                return;
            }
            if (txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("密码不能为空");
                return;
            }
            if (txtUnitName.Text.Trim() == "")
            {
                MessageBox.Show("单位名称不能为空");
                return;
            }
            if (txtLinkPerson.Text.Trim() == "")
            {
                MessageBox.Show("单位联系人不能为空");
                return;
            }
            if (txtUnitDetailAddr.Text.Trim() == "")
            {
                MessageBox.Show("单位地址不能为空");
                return;
            }

            string xmlUnit="<T_farmsDTS>"
                 +"<Farms>"
                     +"<FARMID>"+txtUnitID.Text.Trim()+"</FARMID>"
                         +"<PWD>"+txtPassword.Text.Trim()+"</PWD>" 
                          +"<NAMES>"+ txtUnitName.Text.Trim()+"</NAMES>" 
                          +"<REGIONID>"+txtUnitAddr.Text.Trim()+"</REGIONID>"
                         + "<ADDRE>" +txtUnitDetailAddr.Text.Trim ()+"</ADDRE>"
                         +"<FARMURL>" +txtWebAddr.Text.Trim()+"</FARMURL>"
                         +"<LINKMAN>" +txtLinkPerson.Text.Trim()+ "</LINKMAN>"
                          +"<TELEPHONE> "+txtLinkPhoto.Text.Trim ()+"</TELEPHONE>"
                          +"<BUILDTIME>"+txtCreateDate.Text.Trim()+"</BUILDTIME>"
                          +"<USERID>" +txtUserID.Text.Trim()+"</USERID>"
                         +"<GROUPID>"+txtGroundID.Text.Trim()+"</GROUPID>"
                    +"</Farms>"
                +"</T_farmsDTS>";
            object[] args = new object[1];
            args.SetValue(xmlUnit, 0);

            object ob = DynamicWeb.InvokeWebService(FoodClient.AnHui.Global.AnHuiInterface.ServerAddr, "SyncFarmsInfoList2005", args);
            if (ob.ToString() == "success")
            {
                MessageBox.Show("检测单位信息上传成功");
            }
            else
            {
                MessageBox.Show(ob.ToString());
            }
        }
    }
}

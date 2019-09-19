using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DY.FoodClientLib;
using System.Data;

namespace FoodClient
{
    public partial class FrmProprietorsDetail : TitleBarBase
    {
        public FrmProprietorsDetail()
        {
            InitializeComponent();
        }

        private void FrmProprietorsDetail_Load(object sender, EventArgs e)
        {
            TitleBarText = "经营户明细";
        }

        internal void GetValue(clsProprietors proMode)
        {
            teCdbuslicence.Text = proMode.Cdbuslicence;
            teCiname.Text = proMode.Ciname;
            teCdname.Text = proMode.Cdname;
            teCdcardid.Text = proMode.Cdcardid;
            teDisplayName.Text = proMode.DisplayName;
            teRegCapital.Text = proMode.RegCapital;
            teUnit.Text = proMode.Unit;
            teIncorporator.Text = proMode.Incorporator;
            dtpRegDate.Value = Convert.ToDateTime(proMode.RegDate);

            textBox1.Text = proMode.Cdcode;
            textBox2.Text = proMode.CAllow ;

            tePostCode.Text = proMode.PostCode;
            teAddress.Text = proMode.Address;
            teLinkMan.Text = proMode.LinkMan;
            teLinkInfo.Text = proMode.LinkInfo;
            teCreditLevel.Text = proMode.CreditLevel;
            teCreditRecord.Text = proMode.CreditRecord;
            teProductInfo.Text = proMode.ProductInfo;
            teOtherInfo.Text = proMode.OtherInfo;
            teCheckLevel.Text = proMode.CheckLevel;
            teFoodSafeRecord.Text = proMode.FoodSafeRecord;

            //teIsReadOnly.Text=proMode.IsReadOnly;
            teRemark.Text = proMode.Remark;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
           this . Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DY.FoodClientLib.Model;

namespace FoodClient.Test
{
    public partial class ShowTask : TitleBarBase
    {
        public ShowTask()
        {
            InitializeComponent();
        }

        public clsTask _clsTask = null;

        private void ShowTask_Load(object sender, EventArgs e)
        {
            try
            {
                if (_clsTask != null)
                {
                    tCPTITLE.Text = _clsTask.CPTITLE;
                    tCPCODE.Text = _clsTask.CPCODE;
                    tCPSDATE.Text = _clsTask.CPSDATE;
                    tCPEDATE.Text = _clsTask.CPEDATE;
                    tCPTPROPERTY.Text = _clsTask.CPTPROPERTY;
                    tCPFROM.Text = _clsTask.CPFROM;
                    tCPPORG.Text = _clsTask.CPPORG;
                    tCPPORGID.Text = _clsTask.CPPORGID;
                    tCPEDITOR.Text = _clsTask.CPEDITOR;
                    tPLANDETAIL.Text = _clsTask.PLANDETAIL;
                    tCPMEMO.Text = _clsTask.CPMEMO;
                    tPLANDCOUNT.Text = _clsTask.PLANDCOUNT;
                    tBAOJINGTIME.Text = _clsTask.BAOJINGTIME;
                    tCPEDDATE.Text = _clsTask.CPEDDATE;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btn_ret_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

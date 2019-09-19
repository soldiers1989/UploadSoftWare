using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorkstationDAL.Model;

namespace WorkstationUI.machine
{
    public partial class ucJunLuo : Basic.BasicContent
    {
        public ucJunLuo()
        {
            InitializeComponent();
        }

        private void ucJunLuo_Load(object sender, EventArgs e)
        {
            LbTitle.Text = Global.ChkManchine;
            System.Diagnostics.Process.Start(Application.StartupPath + "\\BacterialColonyAnalyse\\BacterialColonyAnalyse.exe");
        }
    }
}

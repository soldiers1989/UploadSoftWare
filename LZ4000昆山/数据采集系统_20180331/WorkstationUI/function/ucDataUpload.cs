using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WorkstationUI.function
{
    public partial class ucDataUpload : UserControl
    {
        public ucDataUpload()
        {
            InitializeComponent();
        }

        private void ucDataUpload_Load(object sender, EventArgs e)
        {
            CheckDatas.SelectAll();
        }
    }
}

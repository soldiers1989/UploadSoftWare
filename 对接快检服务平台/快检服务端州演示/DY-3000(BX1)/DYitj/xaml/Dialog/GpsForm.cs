using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AIO.xaml.Dialog
{
    public partial class GpsForm : Form
    {
        public GpsForm()
        {
            InitializeComponent();
        }

        private void GpsForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.webBrowser1.Navigate(Environment.CurrentDirectory + "\\Others\\GPS.htm");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

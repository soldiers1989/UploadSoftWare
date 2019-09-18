using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FoodClient
{
    public partial class FrmProgressBar : Form
    {
        public FrmProgressBar()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Increase process bar
        /// </summary>
        /// <param name="nValue">the value increased</param>
        /// <returns></returns>
        public bool Increase(int nValue)
        {
            if (nValue > 0)
            {
                if (prgBar.Value + nValue < 100)
                {
                    prgBar.Value += nValue;
                    return true;
                }
                else
                {

                    prgBar.Value =100;
                    this.Close();
                    return false;
                }
            }
            return false;

        }
    }
}

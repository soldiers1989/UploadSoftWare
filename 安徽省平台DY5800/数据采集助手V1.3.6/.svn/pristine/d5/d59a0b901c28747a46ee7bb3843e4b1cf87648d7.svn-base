using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using WorkstationDAL.AnHui;
using WorkstationDAL.Model;

namespace WorkstationUI.function
{
    public partial class Frmfoodtype : Form
    {
        public Frmfoodtype()
        {
            InitializeComponent();
        }
        private void Frmfoodtype_Load(object sender, EventArgs e)
        {
             Global.AH_FoodType = "";
             DataTable dtbl = DataOperation.GetSampleTypeByName("");
             if (dtbl != null && dtbl.Rows.Count > 0)
             {
                 bindTreeView1(dtbl);
             }
        }

        private void bindTreeView1(DataTable dts)
        {
            DataRow[] dr = dts.Select("pid ='-1' and typeNum='SPFL'");
            for (int i = 0; i < dr.Length; i++)
            {
                TreeNode tn = new TreeNode();
                tn.Text = dr[i]["name"].ToString();
                tn.Tag = dr[i]["ID"].ToString();
                FillTree(tn, dts);
                treeViewSample.Nodes.Add(tn);
            }
        }
        private void FillTree(TreeNode node, DataTable dt)
        {
            DataRow[] drr = dt.Select("pid='" + node.Tag.ToString() + "'");
            if (drr.Length > 0)
            {
                for (int i = 0; i < drr.Length; i++)
                {
                    TreeNode tnn = new TreeNode();
                    tnn.Text = drr[i]["name"].ToString();
                    tnn.Tag = drr[i]["ID"].ToString();
                    if (drr[i]["pid"].ToString() == node.Tag.ToString())
                    {
                        FillTree(tnn, dt);
                    }
                    node.Nodes.Add(tnn);
                }
            }
        }

        private void labelClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            Global.AH_FoodType = treeViewSample.SelectedNode.Text;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void labelClose_MouseEnter(object sender, EventArgs e)
        {
            labelClose.ForeColor = Color.Red;
        }

        private void labelClose_MouseLeave(object sender, EventArgs e)
        {
            labelClose.ForeColor = Color.White;
        }

        private void treeViewSample_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Global.AH_FoodType = treeViewSample.SelectedNode.Text;
            this.Close();
        }

    }
}

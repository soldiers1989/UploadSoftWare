using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DY.FoodClientLib;

namespace FoodClientTools
{
    public partial class FrmDataMng : Form
    {
        public FrmDataMng()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 清空数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearData_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show(this, "是否确认清空数据库所有数据", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dlr == DialogResult.Yes)
            {
                Cursor.Current = Cursors.WaitCursor;
                StringBuilder sb = new StringBuilder();

                string msg = string.Empty;
                clsResultOpr resultBll = new clsResultOpr();
                resultBll.DeleteAll(out msg);
                if (string.Empty == msg)
                {
                    sb.AppendLine("检测数据清理成功");
                }
                else
                {
                    sb.AppendLine(msg);
                }

                msg = string.Empty;
                HolesSetingOpr hBll = new HolesSetingOpr();
                if (hBll.Delete(""))
                {
                    sb.AppendLine("孔位设置清理成功");
                }

                msg = string.Empty;
                clsCompanyOpr companyBll = new clsCompanyOpr();
                companyBll.Delete("", out msg);
                if (string.Empty == msg)
                {
                    sb.AppendLine("被检单位信息清理成功");
                }
                else
                {
                    sb.AppendLine(msg);
                }

                msg = string.Empty;
                clsCheckComTypeOpr comtypeBll = new clsCheckComTypeOpr();
                companyBll.Delete("", out msg);
                if (string.Empty == msg)
                {
                    sb.AppendLine("检测点清理成功");
                }
                else
                {
                    sb.AppendLine(msg);
                }

                msg = string.Empty;
                clsCheckItemOpr checkItemBll = new clsCheckItemOpr();
                checkItemBll.Delete("", out msg);
                if (string.Empty == msg)
                {
                    sb.AppendLine("检测项目清理成功");
                }
                else
                {
                    sb.AppendLine(msg);
                }

                msg = string.Empty;
                clsDistrictOpr disBll = new clsDistrictOpr();
                disBll.Delete("", out msg);
                if (string.Empty == msg)
                {
                    sb.AppendLine("组织机构清理成功");
                }
                else
                {
                    sb.AppendLine(msg);
                }

                msg = string.Empty;
                clsFoodClassOpr foodBll = new clsFoodClassOpr();
                foodBll.Delete("", out msg);
                if (string.Empty == msg)
                {
                    sb.AppendLine("食品种类清理成功");
                }
                else
                {
                    sb.AppendLine(msg);
                }

                msg = string.Empty;
                clsProduceAreaOpr pAreaBll = new clsProduceAreaOpr();
                pAreaBll.Delete("", out msg);
                if (string.Empty == msg)
                {
                    sb.AppendLine("产品产地清理成功");
                }
                else
                {
                    sb.AppendLine(msg);
                }

                clsCompanyKindOpr ckBll = new clsCompanyKindOpr();
                ckBll.Delete("", out msg);
                if (string.Empty == msg)
                {
                    sb.AppendLine("单位类别清理成功");
                }
                else
                {
                    sb.AppendLine(msg);
                }

                MessageBox.Show(sb.ToString());
                sb.Length = 0;

                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// 压缩数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDataMng_Click(object sender, EventArgs e)
        {
            try
            {
                DataBase.CompactAccessDB();
                MessageBox.Show("已经完成数据库压缩与修复！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("数据库修复失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
    }
}

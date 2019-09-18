using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkstationBLL.Mode;
using System.Windows.Forms;

namespace WorkstationModel.Model
{
    public class clsdiary
    {
        clsSetSqlData sql = new clsSetSqlData();
        /// <summary>
        /// 保存操作记录
        /// </summary>
        public void savediary(string t,string d,string r)
        {
            try
            {               
                sql.insertDairy(t,d,r);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");                
            }
 
        }
    }
}

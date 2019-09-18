using System;
using DY.FoodClientLib.Model;

namespace FoodClient.Test
{
    public partial class ShowBaseInfo : TitleBarBase
    {
        public ShowBaseInfo()
        {
            InitializeComponent();
        }

        public clsBaseInfos _model = null;

        private void ShowBaseInfo_Load(object sender, EventArgs e)
        {
            if (_model != null)
            {
                TITLE.Text = _model.TITLE;
                PDATE.Text = _model.PDATE;
                AUTHOR.Text = _model.AUTHOR;
                PUBLISHER.Text = _model.PUBLISHER;
                STATUSES.Text = _model.STATUSES;
                CONTENT.Text = _model.CONTENT;
                CARNAME.Text = _model.CARNAME;
                EDATE.Text = _model.EDATE;
                VNUM.Text = _model.VNUM;
                INFORTYPE.Text = _model.INFORTYPE;
            }
        }

        private void btn_ret_Click(object sender, EventArgs e)
        {
            this.Close();
        }
      
    }
}

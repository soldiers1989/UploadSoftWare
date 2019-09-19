using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DYSeriesDataSet.DataModel
{
    public class clsManageTask
    {
        private int _ID = -1;
        public int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }
        private string _t_id = "";
        public string t_id 
        {
            get
            {
                return _t_id;
            }
            set
            {
                _t_id=value ;
            }
        }
        private string _t_task_code="";
        public string t_task_code
        {
            get
            {
                return _t_task_code;
            }
            set
            {
                _t_task_code = value;
            }
        }
        private string _t_task_title = "";
        public string t_task_title 
        {
            get
            {
                return _t_task_title;
            }
            set
            {
                _t_task_title = value;
            }
        }
        private string _t_task_content = "";
        public string t_task_content 
        {
            get
            {
                return _t_task_content;
            }
            set
            {
                _t_task_content = value;
            }
        }
        private string _t_task_detail_pId = "";
        public string t_task_detail_pId
        {
            get
            {
                return _t_task_detail_pId;
            }
            set
            {
                _t_task_detail_pId = value;
            }
        }
        private string _t_project_id = "";

        public string t_project_id
        {
            get
            {
                return _t_project_id;
            }
            set
            {
                _t_project_id = value;
            }
        }
        private string _t_task_type = "";
        public string t_task_type
        {
            get
            {
                return _t_task_type;
            }
            set
            {
                _t_task_type = value;
            }
        }
        private string _t_task_source = "";
        public string t_task_source 
        {
            get
            {
                return _t_task_source;
            }
            set
            {
                _t_task_source = value;
            }
        }
        private string _t_task_status = "";

        public string t_task_status
        {
            get
            {
                return _t_task_status;
            }
            set
            {
                _t_task_status = value;
            }
        }
        private string _t_task_total = "";

        public string t_task_total
        {
            get
            {
                return _t_task_total;
            }
            set
            {
                _t_task_total = value;
            }
        }
        private string _t_sample_number = "";

        public string t_sample_number
        {
            get
            {
                return _t_sample_number;
            }
            set
            {
                _t_sample_number = value;
            }
        }

        private string _t_task_sdate = "";

        public string t_task_sdate
        {
            get
            {
                return _t_task_sdate;
            }
            set
            {
                _t_task_sdate = value;
            }
        }
        private string _t_task_edate = "";
        public string t_task_edate
        {
            get
            {
                return _t_task_edate.Substring(0,10);
            }
            set
            {
                _t_task_edate = value;
            }
        }
        private string _t_task_pdate = "";

        public string t_task_pdate
        {
            get
            {
                return _t_task_pdate;
            }
            set
            {
                _t_task_pdate = value;
            }
        }
        private string _t_task_fdate = "";

        public string t_task_fdate
        {
            get
            {
                return _t_task_fdate;
            }
            set
            {
                _t_task_fdate = value;
            }
        }
        private string _t_task_departId = "";
        public string t_task_departId
        {
            get
            {
                return _t_task_departId;
            }
            set
            {
                _t_task_departId = value;
            }
        }
        private string _t_task_announcer = "";
        public string t_task_announcer
        {
            get
            {
                return _t_task_announcer;
            }
            set
            {
                _t_task_announcer = value;
            }
        }
        private string _t_task_cdate = "";
        public string t_task_cdate
        {
            get
            {
                return _t_task_cdate;
            }
            set
            {
                _t_task_cdate = value;
            }
        }
        private string _t_remark = "";

        public string t_remark
        {
            get
            {
                return _t_remark;
            }
            set
            {
                _t_remark = value;
            }
        }
        private string _t_view_flag = "";
        public string t_view_flag
        {
            get
            {
                return _t_view_flag;
            }
            set
            {
                _t_view_flag = value;
            }
        }
        private string _t_delete_flag = "";
        public string t_delete_flag
        {
            get
            {
                return _t_delete_flag;
            }
            set
            {
                _t_delete_flag = value;
            }
        }
        private string _t_create_by = "";
        public string t_create_by
        {
            get
            {
                return _t_create_by;
            }
            set
            {
                _t_create_by = value;
            }
        }
        private string _t_create_date = "";
        public string t_create_date
        {
            get
            {
                return _t_create_date;
            }
            set
            {
                _t_create_date = value;
            }
        }
        private string _t_update_by = "";
        public string t_update_by
        {
            get
            {
                return _t_update_by;
            }
            set
            {
                _t_update_by = value;
            }
        }
        private string _t_update_date = "";
        public string t_update_date
        {
            get
            {
                return _t_update_date;
            }
            set
            {
                _t_update_date = value;
            }
        }
        private string _d_id = "";
        public string d_id
        {
            get
            {
                return _d_id;
            }
            set
            {
                _d_id = value;
            }
        }
        private string _d_task_id = "";
        public string d_task_id
        {
            get
            {
                return _d_task_id;
            }
            set
            {
                _d_task_id = value;
            }
        }
        private string _d_detail_code = "";
        public string d_detail_code
        {
            get
            {
                return _d_detail_code;
            }
            set
            {
                _d_detail_code = value;
            }
        }
        private string _d_sample_id = "";
        public string d_sample_id
        {
            get
            {
                return _d_sample_id;
            }
            set
            {
                _d_sample_id = value;
            }
        }
        private string _d_sample = "";
        public string d_sample
        {
            get
            {
                return _d_sample;
            }
            set
            {
                _d_sample = value;
            }
        }
        private string _d_item_id = "";
        public string d_item_id
        {
            get
            {
                return _d_item_id;
            }
            set
            {
                _d_item_id = value;
            }
        }
        private string _d_d_item = "";
        public string d_item
        {
            get
            {
                return _d_d_item;
            }
            set
            {
                _d_d_item = value;
            }
        }
        private string _d_task_fdate = "";
        public string d_task_fdate
        {
            get
            {
                return _d_task_fdate;
            }
            set
            {
                _d_task_fdate = value;
            }
        }
        private string _d_receive_pointid = "";
        public string d_receive_pointid
        {
            get
            {
                return _d_receive_pointid;
            }
            set
            {
                _d_receive_pointid = value;
            }
        }
        private string _d_receive_point = "";
        public string d_receive_point
        {
            get
            {
                return _d_receive_point;
            }
            set
            {
                _d_receive_point = value;
            }
        }
        private string _d_receive_nodeid = "";
        public string d_receive_nodeid
        {
            get
            {
                return _d_receive_nodeid;
            }
            set
            {
                _d_receive_nodeid = value;
            }
        }
        private string _d_receive_node = "";
        public string d_receive_node
        {
            get
            {
                return _d_receive_node;
            }
            set
            {
                _d_receive_node = value;
            }
        }
        private string _d_receive_userid = "";
        public string d_receive_userid
        {
            get
            {
                return _d_receive_userid;
            }
            set
            {
                _d_receive_userid = value;
            }
        }
        private string _d_receive_username = "";
        public string d_receive_username
        {
            get
            {
                return _d_receive_username;
            }
            set
            {
                _d_receive_username = value;
            }
        }
        private string _d_receive_status = "";
        public string d_receive_status
        {
            get
            {
                return _d_receive_status;
            }
            set
            {
                _d_receive_status = value;
            }
        }
        private string _d_task_total = "";
        public string d_task_total
        {
            get
            {
                return _d_task_total;
            }
            set
            {
                _d_task_total = value;
            }
        }
        private string _d_sample_number = "";
        public string d_sample_number
        {
            get
            {
                return _d_sample_number;
            }
            set
            {
                _d_sample_number = value;
            }
        }
        private string _d_remark = "";
        public string d_remark
        {
            get
            {
                return _d_remark;
            }
            set
            {
                _d_remark = value;
            }
        }

    }
}

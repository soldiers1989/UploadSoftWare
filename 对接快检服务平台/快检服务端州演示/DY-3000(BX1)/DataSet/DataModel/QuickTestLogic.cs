using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DYSeriesDataSet.DataModel
{
    public class QuickTestLogic
    {
        private string _userName = string.Empty;
        private string _password = string.Empty;
        private string _deviceCode = string.Empty;
        private string _softWareVersion = string.Empty;
        private string _place = string.Empty;
        private string _ip = string.Empty;
        private string _param1 = string.Empty;
        private string _param2 = string.Empty;
        /// <summary>
        /// 用户名
        /// </summary>
        public string userName
        {
            set
            {
                _userName=value  ;
            }
            get
            {
                return _userName;
            }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string password
        {
            set
            {
                _password=value ;
            }
            get
            {
                return _password;
            }
        }
        /// <summary>
        /// 机器码
        /// </summary>
        public string deviceCode
        {
            set
            {
                _deviceCode=value ;
            }
            get
            {
                return _deviceCode;
            }
        }
        /// <summary>
        /// 软件名称和版本
        /// </summary>
        public string softWareVersion
        {
            set
            {
                _softWareVersion =value;
            }
            get
            {
                return _softWareVersion;
            }
        }
        /// <summary>
        /// GPS坐标
        /// </summary>
        public string place
        {
            set
            {
                _place= value ;
            }
            get
            {
                return _place;
            }
        }
        /// <summary>
        /// IP地址
        /// </summary>
        public string ip
        {
            set
            {
                    _ip=value ;
            }
            get
            {
                return _ip;
            }
        }
        /// <summary>
        /// 参数1
        /// </summary>
        public string param1
        {
            set
            {
                    _param1=value ;
            }
            get
            {
                return _param1;
            }
        }
        /// <summary>
        /// 参数2
        /// </summary>
        public string param2
        {
            set
            {
                    _param2=value ;
            }
            get
            {
                return _param2;
            }
        }
       
    }
    /// <summary>
    /// 解析返回数据
    /// </summary>
    public class ResultData
    {
        public string attributes { get; set; }
        public object obj { get; set; }
        public string resultCode { get; set; }
        public string msg { get; set; }
        public bool success { get; set; }
    }
    public class objdata
    {
        public string rights { get; set; }
        public object user { get; set; }
        public string token { get; set; }
       
    }

    public class usd
    {
        public object duser { get; set; }
        public List<userdata> user { get; set; }
    }

    public class tasknum
    {
        public string count { get; set; }
        
    }
    //任务管理数据
    public class ManageTask
    {
        public object Ttask { get; set; }
        public List<ManageTaskNum> tasks { get; set; }
    }

    public class ManageTaskNum
    {
        public string tasksNumber { get; set; }
    }

    //消息公告
    public class ManageGonggao
    {
        public object Tgonggao { get; set; }
        public List<MessageBullite> obj { get; set; }
    }
    public class MessageBullite
    {
        public string id { get; set; }
        public string from_user_id { get; set; }
        public string from_user_name { get; set; }
        public string to_user_id { get; set; }
        public string to_user_type { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string file_path { get; set; }
        public string file_name { get; set; }
        public string sendtime { get; set; }
        public string group_id { get; set; }
        public string group_point_id { get; set; }
        public string log_id { get; set; }
        public string log_read_status { get; set; }
        public string log_read_time { get; set; }

    }

    //消息公告数量
    public class ManagegonggaoNum
    {
        public string total { get; set; }
        public string unread { get; set; }
    }

    public class userdata
    {
        public string user_name { get; set; }
        public string id { get; set; }
        public string realname { get; set; }
        public string depart_id { get; set; }
        public string point_id { get; set; }
        //public string d_depart_name { get; set; }
        //public string d_id { get; set; }
        /// <summary>
        /// 检测点编号
        /// </summary>
        public string p_point_code { get; set; }
        /// <summary>
        /// 检测点名称
        /// </summary>
        public string p_point_name { get; set; }
        /// <summary>
        /// 检测点类型
        /// </summary>
        public string p_point_type { get; set; }
        /// <summary>
        /// 所属机构名称
        /// </summary>
        public string d_depart_name { get; set; }
        /// <summary>
        /// 行政机构ID
        /// </summary>
        public string d_id { get; set; }
 
    }
    //检测任务
    public class ManageTaskn
    {
        public object Tresult { get; set; }
        public List<ManageTaskTest> tasks { get; set; }
    }
    public class ManageTaskTest
    {
        public string t_id { get; set; }
        public string t_task_code { get; set; }
        public string t_task_title { get; set; }
        public string t_task_content { get; set; }
        public string t_task_detail_pId { get; set; }
        public string t_project_id { get; set; }
        public string t_task_type { get; set; }
        public string t_task_source { get; set; }
        public string t_task_status { get; set; }
        public string t_task_total { get; set; }
        public string t_sample_number { get; set; }
        public string t_task_sdate { get; set; }
        public string t_task_edate { get; set; }
        public string t_task_pdate { get; set; }
        public string t_task_fdate { get; set; }
        public string t_task_departId { get; set; }
        public string t_task_announcer { get; set; }
        public string t_task_cdate { get; set; }
        public string t_remark { get; set; }
        public string t_view_flag { get; set; }
        public string t_delete_flag { get; set; }
        public string t_create_by { get; set; }
        public string t_create_date { get; set; }
        public string t_update_by { get; set; }
        public string t_update_date { get; set; }
        public string d_id { get; set; }
        public string d_task_id { get; set; }
        public string d_detail_code { get; set; }
        public string d_sample_id { get; set; }
        public string d_sample { get; set; }
        public string d_item_id { get; set; }
        public string d_item { get; set; }
        public string d_task_fdate { get; set; }
        public string d_receive_pointid { get; set; }
        public string d_receive_point { get; set; }
        public string d_receive_nodeid { get; set; }
        public string d_receive_node { get; set; }
        public string d_receive_userid { get; set; }
        public string d_receive_username { get; set; }
        public string d_receive_status { get; set; }
        public string d_task_total { get; set; }
        public string d_sample_number { get; set; }
        public string d_remark { get; set; }

    }


    public class Mtask
    {
        public List<MMtask> result { get; set; }
    }
    public class MMtask
    {
        public string id { get; set; }
        public string sampling_id { get; set; }
        public string sample_code { get; set; }
        public string food_id { get; set; }
        public string food_name { get; set; }
        public string sample_number { get; set; }
        public string purchase_amount { get; set; }
        public string sample_date { get; set; }
        public string purchase_date { get; set; }
        public string item_id { get; set; }
        public string item_name { get; set; }//被检单位
        public string origin { get; set; }
        public string supplier { get; set; }
        public string supplier_address { get; set; }
        public string supplier_person { get; set; }
        public string supplier_phone { get; set; }
        public string batch_number { get; set; }
        public string status { get; set; }
        public string recevie_device { get; set; }
        public string ope_shop_name { get; set; }
        public string remark { get; set; }
        public string param1 { get; set; }
        public string param2 { get; set; }
        public string param3 { get; set; }
        public string s_id { get; set; }
        public string s_sampling_no { get; set; }
        public string s_sampling_date { get; set; }
        public string s_point_id { get; set; }
        public string s_reg_id { get; set; }
        public string s_reg_name { get; set; }
        public string s_reg_licence { get; set; }
        public string s_reg_link_person { get; set; }
        public string s_reg_link_phone { get; set; }
        public string s_ope_id { get; set; }
        public string s_ope_shop_code { get; set; }
        public string s_ope_shop_name { get; set; }
        public string s_qrcode { get; set; }
        public string s_task_id { get; set; }
        public string s_status { get; set; }
        public string s_place_x { get; set; }

        public string s_place_y { get; set; }
        public string s_sampling_userid { get; set; }
        public string s_sampling_username { get; set; }
        public string s_ope_signature { get; set; }
        public string s_create_by { get; set; }
        public string s_create_date { get; set; }
        public string s_update_by { get; set; }
        public string s_update_date { get; set; }
        public string s_sheet_address { get; set; }
        public string s_param1 { get; set; }
        public string s_param2 { get; set; }
        public string s_param3 { get; set; }
        public string t_id { get; set; }
        public string t_task_code { get; set; }
        public string t_task_title { get; set; }
        public string t_task_content { get; set; }
        public string t_task_detail_pId { get; set; }
        public string t_project_id { get; set; }
        public string t_task_type { get; set; }
        public string t_task_source { get; set; }
        public string t_task_status { get; set; }
        public string t_task_total { get; set; }
        public string t_sample_number { get; set; }
        public string t_task_sdate { get; set; }
        public string t_task_edate { get; set; }

        public string t_task_pdate { get; set; }
        public string t_task_fdate { get; set; }
        public string t_task_departId { get; set; }
        public string t_task_announcer { get; set; }
        public string t_task_cdate { get; set; }
        public string t_remark { get; set; }
        public string t_view_flag { get; set; }
        public string t_file_path { get; set; }
        public string t_delete_flag { get; set; }
        public string t_create_by { get; set; }

        public string t_create_date { get; set; }
        public string t_update_by { get; set; }
        public string t_update_date { get; set; }
        public string td_id { get; set; }
        public string td_task_id { get; set; }
        public string td_detail_code { get; set; }
        public string td_sample_id { get; set; }
        public string td_sample { get; set; }
        public string td_item_id { get; set; }
        public string td_item { get; set; }
        public string td_task_fdate { get; set; }
        public string td_receive_pointid { get; set; }
        public string td_receive_point { get; set; }
        public string td_receive_nodeid { get; set; }
        public string td_receive_node { get; set; }
        public string td_receive_userid { get; set; }
        public string td_receive_username { get; set; }
        public string td_receive_status { get; set; }
        public string td_task_total { get; set; }
        public string td_sample_number { get; set; }
        public string td_remark { get; set; }
        public string mokuai { get; set; }
        public string username { get; set; }
        public string dataType { get; set; }
        public string s_personal { get; set; }
    }


    public class taskdata
    {
        public object  dtask{ get; set; }
        public List<ttTask> tasks{ get; set; }
    }
    public class ttTask
    {
        public string t_id { get; set; }
        public string t_task_code { get; set; }
        public string t_task_title { get; set; }
        public string t_task_content { get; set; }
        public string t_task_detail_pId { get; set; }
        public string t_project_id { get; set; }
        public string t_task_type { get; set; }
        public string t_task_source { get; set; }
        public string t_task_status { get; set; }
        public string t_task_total { get; set; }
        public string t_sample_number { get; set; }
        public string t_task_sdate { get; set; }
        public string t_task_edate { get; set; }
        public string t_task_pdate { get; set; }
        public string t_task_fdate { get; set; }
        public string t_task_departId { get; set; }
        public string t_task_announcer { get; set; }
        public string t_task_cdate { get; set; }
        public string t_remark { get; set; }
        public string t_view_flag { get; set; }
        public string t_delete_flag { get; set; }
        public string t_create_by { get; set; }
        public string t_create_date { get; set; }
        public string t_update_by { get; set; }
        public string t_update_date { get; set; }
        public string d_id { get; set; }
        public string d_task_id { get; set; }
        public string d_detail_code { get; set; }
        public string d_sample_id { get; set; }
        public string d_sample { get; set; }
        public string d_item_id { get; set; }
        public string d_item { get; set; }
        public string d_task_fdate { get; set; }
        public string d_receive_pointid { get; set; }
        public string d_receive_point { get; set; }
        public string d_receive_nodeid { get; set; }
        public string d_receive_node { get; set; }
        public string d_receive_userid { get; set; }
        public string d_receive_username { get; set; }
        public string d_receive_status { get; set; }
        public string d_task_total { get; set; }
        public string d_sample_number { get; set; }
        public string d_remark { get; set; }
      
    }
    //仪器注册类
    public class zhuce
    {
        public string id { get; set; }
        public string device_name { get; set; }
        public string serial_number { get; set; }
        public string update_date { get; set; }
    }

    /// <summary>
    /// 检测项目下载
    /// </summary>
    public class detectitem
    {
        public List<CheckItem> detectItem { get; set; }
    }
    public class CheckItem
    {
        public string	id { get; set; }
        public string	detect_item_name  	{ get; set; }
        public string	detect_item_typeid  	{ get; set; }
        public string	standard_id  	{ get; set; }
        public string	detect_sign  	{ get; set; }
        public string	detect_value   	{ get; set; }
        public string	detect_value_unit  	{ get; set; }
        public string @checked { get; set; }//关键字
        public string	cimonitor_level 	{ get; set; }
        public string	remark 	{ get; set; }
        public string	delete_flag	{ get; set; }
        public string	create_by  	{ get; set; }
        public string	create_date  	{ get; set; }
        public string	update_by 	{ get; set; }
        public string	update_date 	{ get; set; }
        public string	t_id  	{ get; set; }
        public string	t_item_name  	{ get; set; }
        public string	t_sorting 	{ get; set; }
        public string	t_remark  	{ get; set; }
        public string	t_delete_flag 	{ get; set; }
        public string	t_create_by  	{ get; set; }
        public string	t_create_date 	{ get; set; }
        public string	t_update_by  	{ get; set; }
        public string	t_update_date	{ get; set; }

    }
    public class sampletype
    {
        public List<foodtype> food { get; set; }
    }
    public class foodtype
    {
         public string	 id 	{ get; set; }
         public string	 food_name 	{ get; set; }
         public string	 food_name_en 	{ get; set; }
         public string	 food_name_other 	{ get; set; }
         public string	 parent_id 	{ get; set; }
         public string	 cimonitor_level 	{ get; set; }
         public string	 sorting 	{ get; set; }
         public string	 @checked 	{ get; set; }
         public string	 delete_flag 	{ get; set; }
         public string	 create_by 	{ get; set; }
         public string	 create_date 	{ get; set; }
         public string	 update_by 	{ get; set; }
         public string	 update_date	{ get; set; }
         public string	 isFood 	{ get; set; }

    }
    public class standardlist
    {
        public List<standard> standard { get; set; }
    }
    public class standard
    {
        public string id { get; set; }
        public string std_code { get; set; }
        public string std_name { get; set; }
        public string std_title { get; set; }
        public string std_unit { get; set; }
        public string std_type { get; set; }
        public string std_status { get; set; }
        public string imp_time { get; set; }
        public string rel_time { get; set; }
        public string url_path { get; set; }
        public string use_status { get; set; }
        public string delete_flag { get; set; }
        public string std_id { get; set; }
        public string sorting { get; set; }
        public string remark { get; set; }
        public string create_by { get; set; }
        public string create_date { get; set; }
        public string update_by { get; set; }
        public string update_date { get; set; }

    }

    public class Lawlist
    {
        public List<law> laws { get; set; }
    }

    public class law
    {
        public string id { get; set; }
        public string law_name { get; set; }
        public string law_type { get; set; }
        public string law_unit { get; set; }
        public string law_num { get; set; }
        public string law_status { get; set; }
        public string law_notes { get; set; }
        public string rel_date { get; set; }
        public string imp_date { get; set; }
        public string failure_date { get; set; }
        public string url_path { get; set; }
        public string use_status { get; set; }
        public string delete_flag { get; set; }
        public string create_by { get; set; }
        public string create_date { get; set; }
        public string update_by { get; set; }
        public string update_date { get; set; }
    }

    public class fooditemlist
    {
        public List<fooditem> foodItem { get; set; }
    }
    public class fooditem
    {
        public string	id  	{ get; set; }
        public string	food_id	{ get; set; }
        public string	food_id1 { get; set; }
        public string	item_id { get; set; }
        public string	detect_sign { get; set; }
        public string	detect_value { get; set; }
        public string	detect_value_unit  	{ get; set; }
        public string	remark { get; set; }
        public string	use_default	{ get; set; }
        public string	@checked { get; set; }
        public string	delete_flag	{ get; set; }
        public string	create_by  	{ get; set; }
        public string	create_date  { get; set; }
        public string	update_by  	{ get; set; }
        public string	update_date  { get; set; }

    }
    public class checkVersion
    {
        public string attributes { get; set; }
        public string msg { get; set; }
        public bool success { get; set; }
        public string resultCode { get; set; }
        public object obj { get; set; }      
    }
    public class getversion
    {
        public string id { get; set; }
        public string app_type { get; set; }
        public string app_name { get; set; }
        public string version { get; set; }
        public string url_path { get; set; }
        public string description { get; set; }
        public string imp_date { get; set; }
        public string delete_flag { get; set; }
        public string create_by { get; set; }
        public string create_date { get; set; }
        public string update_by { get; set; }
        public string update_date { get; set; }
        public string param1 { get; set; }
        public string param2 { get; set; }
        public string param3 { get; set; }   

    }

}

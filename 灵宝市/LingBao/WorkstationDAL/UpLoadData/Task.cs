using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkstationDAL.UpLoadData
{
    class Task
    {
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

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data ;
using System.IO ;

namespace DataSetModel.DataSentence
{

    public class KJFWBaseData
    {
        private StringBuilder sb=new StringBuilder();
        /// <summary>
        /// 查询仪器检测项目
        /// </summary>
        /// <param name="wheresql"></param>
        /// <param name="orderby"></param>
        /// <param name="type"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetMachineItem(string wheresql, string orderby, int type, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dt = null;
            sb.Length = 0;
            try
            {
                if (type == 1)
                {
                    //sb.Append("select m.item_id,m.project_type,m.detect_method,m.detect_unit,m.invalid_value,m.wavelength,m.pre_time,");
                    //sb.Append("m.dec_time,m.yin_min,m.yin_max,m.yang_min,m.yang_max,m.yinT,m.yangT,m.absX,m.ctAbsX,m.division,m.parameter,m.reserved1,m.reserved2");
                    //sb.Append(",m.reserved3,(select detect_item_name from DetectItem where cid=m.item_id) as itemname from MachineItem m");
                }
                else if (type == 2)
                {
                    sb.Append("select m.item_id,m.project_type,m.detect_method,m.detect_unit,m.invalid_value,m.wavelength,m.pre_time,m.dec_time,m.yin_min,m.yin_max,");
                    sb.Append("m.yang_min,m.yang_max,m.yinT,m.yangT,m.absX,m.ctAbsX,m.division,m.parameter,m.reserved1,m.reserved2,m.reserved3,");
                    sb.Append("d.detect_item_name as itemname from MachineItem m,DetectItem d where d.cid=m.item_id ");
                }

                if (!wheresql.Equals(string.Empty))
                {
                    //sb.Append(" WHERE ");
                    sb.Append(wheresql);
                }
                if (!orderby.Equals(string.Empty))
                {
                    sb.Append(" order by ");
                    sb.Append(orderby);
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "MachineItem" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["MachineItem"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
        }
        /// <summary>
        /// 获取样品检测项目标准
        /// </summary>
        /// <param name="wheresql"></param>
        /// <param name="orderby"></param>
        /// <param name="type"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetSamplestd(string wheresql, string orderby, int type, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dt = null;
            sb.Length = 0;
            try
            {
                if (type == 1)
                {
                    sb.Append("select std.food_id,std.item_id,std.detect_sign,std.detect_value,std.detect_value_unit,std.update_date,f.food_name as");
                    sb.Append(" foodname,d.detect_item_name as itemname,sd.std_code as standard from SampleStandard std ,foodlist f,DetectItem ");
                    sb.Append("d,StandardList sd where f.fid=std.food_id and d.cid=std.item_id and sd.sid=d.standard_id");
                }
                if (!wheresql.Equals(string.Empty))
                {
                    //sb.Append(" WHERE ");
                    sb.Append(wheresql);
                }
                if (!orderby.Equals(string.Empty))
                {
                    sb.Append(" order by ");
                    sb.Append(orderby);
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "SampleStandard" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["SampleStandard"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
        }
        /// <summary>
        /// 查询样品
        /// </summary>
        /// <param name="wheresql"></param>
        /// <param name="orderby"></param>
        /// <param name="type"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetSample(string wheresql, string orderby, int type, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dt = null;
            sb.Length = 0;
            try
            {
                if (type == 1)
                {
                    sb.Append("select * from foodlist ");
                    //sb.Append(" foodname,d.detect_item_name as itemname,sd.std_code as standard from SampleStandard std ,foodlist f,DetectItem ");
                    //sb.Append("d,StandardList sd where f.fid=std.food_id and d.cid=std.item_id and sd.sid=d.standard_id");
                }
                if (!wheresql.Equals(string.Empty))
                {
                    sb.Append(" WHERE ");
                    sb.Append(wheresql);
                }
                if (!orderby.Equals(string.Empty))
                {
                    sb.Append(" order by ");
                    sb.Append(orderby);
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "SampleStandard" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["SampleStandard"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
        }
        /// <summary>
        /// 查询仪器检测项目
        /// </summary>
        /// <param name="wheresql"></param>
        /// <param name="orderby"></param>
        /// <param name="type"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetItem(string wheresql, string orderby, int type, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dt = null;
            sb.Length = 0;
            try
            {
                if (type == 1)
                {
                    sb.Append("select d.cid,d.detect_item_name,d.detect_item_typeid,d.standard_id,d.detect_sign,d.detect_value,d.detect_value_unit,d.checked");
                    sb.Append(",d.cimonitor_level,d.remark,d.delete_flag,d.create_by,d.create_date,d.update_by,d.update_date,d.t_id,d.t_item_name,d.t_item_name");
                    sb.Append(",d.t_sorting,d.t_remark,d.t_delete_flag,d.t_create_by,d.t_create_date,d.t_update_by,d.t_update_date,s.std_code as standard  from DetectItem d,StandardList s where s.sid=d.standard_id");
                }
                if (!wheresql.Equals(string.Empty))
                {
                    //sb.Append(" WHERE ");
                    sb.Append(wheresql);
                }
                if (!orderby.Equals(string.Empty))
                {
                    sb.Append(" order by ");
                    sb.Append(orderby);
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "SampleStandard" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["SampleStandard"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
        }
        /// <summary>
        /// 查询标准名称
        /// </summary>
        /// <param name="wheresql"></param>
        /// <param name="orderby"></param>
        /// <param name="type"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetStandlist(string wheresql, string orderby, int type, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dt = null;
            sb.Length = 0;
            try
            {
                if (type == 1)
                {
                    sb.Append("select * from StandardList");
                }
                if (!wheresql.Equals(string.Empty))
                {
                    sb.Append(" WHERE ");
                    sb.Append(wheresql);
                }
                if (!orderby.Equals(string.Empty))
                {
                    sb.Append(" order by ");
                    sb.Append(orderby);
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "StandardList" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["StandardList"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
 
        }
        /// <summary>
        /// 查询法律法规数据
        /// </summary>
        /// <param name="wheresql"></param>
        /// <param name="orderby"></param>
        /// <param name="type"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public DataTable GetLaws(string wheresql, string orderby, int type, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dt = null;
            sb.Length = 0;
            try
            {
                if (type == 1)
                {
                    sb.Append("select * from LawsDownlist");
                    //sb.Append(",d.cimonitor_level,d.remark,d.delete_flag,d.create_by,d.create_date,d.update_by,d.update_date,d.t_id,d.t_item_name,d.t_item_name");
                    //sb.Append(",d.t_sorting,d.t_remark,d.t_delete_flag,d.t_create_by,d.t_create_date,d.t_update_by,d.t_update_date,s.std_code as standard  from DetectItem d,StandardList s where s.sid=d.standard_id");
                }
                if (!wheresql.Equals(string.Empty))
                {
                    sb.Append(" WHERE ");
                    sb.Append(wheresql);
                }
                if (!orderby.Equals(string.Empty))
                {
                    sb.Append(" order by ");
                    sb.Append(orderby);
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "SampleStandard" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["SampleStandard"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
        }
    }

    public class standlist
    {
        public string sid { get; set; }
        public string std_code { get; set; }
        public string std_name { get; set; }
        public string std_title { get; set; }
        public string std_unit { get; set; }
        public string std_type { get; set; }
        public string std_status { get; set; }
        public string imp_time { get; set; }
        public string rel_time { get; set; }
        public string url_path { get; set; }
        public string update_date { get; set; }
        //public string wid { get; set; }
    }

    public class laws
    {
        public string wid { get; set; }
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

    public class Items
    {
        public string cid { get; set; }
        public string detect_item_name { get; set; }
        public string detect_item_typeid { get; set; }
        public string standard_id { get; set; }
        public string detect_sign { get; set; }
        public string detect_value { get; set; }
        public string detect_value_unit { get; set; }
        public string @checked { get; set; }
        public string cimonitor_level { get; set; }
        public string remark { get; set; }
        public string delete_flag { get; set; }
        public string create_by { get; set; }
        public string create_date { get; set; }
        public string update_by { get; set; }
        public string update_date { get; set; }
        public string t_id { get; set; }

        public string t_item_name { get; set; }
        public string t_sorting { get; set; }
        public string t_remark { get; set; }
        public string t_delete_flag { get; set; }
        public string t_create_by { get; set; }
        public string t_create_date { get; set; }
        public string t_update_by { get; set; }
        public string t_update_date { get; set; }
        public string standard { get; set; }
    }

    public class samples
    {
        public string fid { get; set; }
        public string food_name { get; set; }
        public string food_name_en { get; set; }
        public string food_name_other { get; set; }
        public string parent_id { get; set; }
        public string cimonitor_level { get; set; }
        public string sorting { get; set; }
        public string @checked { get; set; }
        public string delete_flag { get; set; }
        public string create_by { get; set; }
        public string create_date { get; set; }
        public string update_by { get; set; }
        public string update_date { get; set; }
        public string isFood { get; set; }
    }

    public class Ssamplestd
    {
        public string food_id { get; set; }
        public string item_id { get; set; }
        public string detect_sign { get; set; }
        public string detect_value { get; set; }
        public string detect_value_unit { get; set; }
        public string update_date { get; set; }
        public string foodname { get; set; }
        public string itemname { get; set; }
        public string standard { get; set; }  
    }

    public class Smachineiten
    {
        public string item_id { get; set; }
        public string project_type { get; set; }
        public string detect_method { get; set; }
        public string detect_unit { get; set; }
        public string invalid_value { get; set; }
        public string wavelength { get; set; }
        public string pre_time { get; set; }
        public string dec_time { get; set; }
        public string yin_min { get; set; }
        public string yin_max { get; set; }
        public string yang_min { get; set; }
        public string yang_max { get; set; }
        public string yinT { get; set; }
        public string yangT { get; set; }
        public string absX { get; set; }
        public string ctAbsX { get; set; }
        public string division { get; set; }
        public string parameter { get; set; }
        public string reserved1 { get; set; }
        public string reserved2 { get; set; }
        public string reserved3 { get; set; }
        public string itemname { get; set; }

    }
    public class regulatorylist
    {
        public IList<regulator> regulatory { get; set; }
    }
    public class regulator
    {
        public string  id { get; set; }
        public string  depart_id { get; set; }
        public string  reg_name { get; set; }
        public string  reg_type { get; set; }
        public string  link_user { get; set; }
        public string  link_phone { get; set; }
        public string  link_idcard { get; set; }
        public string  fax { get; set; }
        public string  post{ get; set; }
        public string  region_id{ get; set; }
        public string  reg_address { get; set; }
        public string  place_x { get; set; }
        public string  place_y { get; set; }
        public string  remark { get; set; }
        public string  @checked { get; set; }
        public string  delete_flag	{ get; set; }
        public string  sorting { get; set; }
        public string  create_by   { get; set; }
        public string  create_date  { get; set; }
        public string  update_by { get; set; }
        public string  update_date	{ get; set; }
        public string ope_shop_code { get; set; }
    }

    public class businesslist
    {
        public IList<Manbusiness> business { get; set; }
    }

    public class Manbusiness
    {
         public string 	id { get; set; }
         public string 	reg_id { get; set; }
         public string 	ope_shop_name  	{ get; set; }
         public string 	ope_shop_code  { get; set; }
         public string 	ope_name { get; set; }
         public string  ope_idcard  { get; set; }
         public string 	ope_phone  { get; set; }
         public string  credit_rating  { get; set; }
         public string 	monitoring_level  { get; set; }
         public string 	qrcode 	{ get; set; }
         public string 	remark  { get; set; }
         public string 	 @checked  { get; set; }
         public string 	delete_flag  { get; set; }
         public string 	sorting  { get; set; }
         public string 	create_by  { get; set; }
         public string 	create_date  { get; set; }
         public string 	update_by  { get; set; }
         public string 	update_date  { get; set; }

    }
    public class personlist
    {
        public List<persons> personnel { get; set; }
    }
    public class persons
    {
        public string id { get; set; }
        public string reg_id { get; set; }
        public string name { get; set; }
        public string job_title { get; set; }
        public string idcard { get; set; }
        public string phone { get; set; }
        public string proof_code { get; set; }
        public string proof_sdate { get; set; }
        public string proof_edate { get; set; }
        public string remark { get; set; }
        public string delete_flag { get; set; }
        public string sorting { get; set; }
        public string create_by { get; set; }
        public string create_date { get; set; }
        public string update_by { get; set; }
        public string update_date { get; set; }

    }
}

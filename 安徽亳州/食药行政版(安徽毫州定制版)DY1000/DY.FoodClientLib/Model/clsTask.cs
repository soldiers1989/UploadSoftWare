using System;
using System.Collections.Generic;
using System.Text;

namespace DY.FoodClientLib.Model
{
    public class clsTask
    {
        public clsTask() 
        {

        }

        private Int32 _ID = 0;
        /// <summary>
        /// ID
        /// </summary>
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private String _CPTITLE = String.Empty;
        /// <summary>
        /// 任务主题
        /// </summary>
        public String CPTITLE
        {
            get { return _CPTITLE; }
            set { _CPTITLE = value; }
        }

        private String _CPCODE = String.Empty;
        /// <summary>
        /// 任务编号
        /// </summary>
        public String CPCODE
        {
            get { return _CPCODE; }
            set { _CPCODE = value; }
        }

        private String _CPSDATE = String.Empty;
        /// <summary>
        /// 起始日期
        /// </summary>
        public String CPSDATE
        {
            get { return _CPSDATE; }
            set { _CPSDATE = value; }
        }

        private String _CPEDATE = String.Empty;
        /// <summary>
        /// 结束日期
        /// </summary>
        public String CPEDATE
        {
            get { return _CPEDATE; }
            set { _CPEDATE = value; }
        }

        private String _CPTPROPERTY = String.Empty;
        /// <summary>
        /// 任务性质
        /// </summary>
        public String CPTPROPERTY
        {
            get { return _CPTPROPERTY; }
            set { _CPTPROPERTY = value; }
        }

        private String _CPFROM = String.Empty;
        /// <summary>
        /// 任务来源
        /// </summary>
        public String CPFROM
        {
            get { return _CPFROM; }
            set { _CPFROM = value; }
        }

        private String _CPPORG = String.Empty;
        /// <summary>
        /// 机构名称
        /// </summary>
        public String CPPORG
        {
            get { return _CPPORG; }
            set { _CPPORG = value; }
        }

        private String _CPPORGID = String.Empty;
        /// <summary>
        /// 机构编码
        /// </summary>
        public String CPPORGID
        {
            get { return _CPPORGID; }
            set { _CPPORGID = value; }
        }

        private String _CPEDITOR = String.Empty;
        /// <summary>
        /// 编制人
        /// </summary>
        public String CPEDITOR
        {
            get { return _CPEDITOR; }
            set { _CPEDITOR = value; }
        }

        private String _CPEDDATE = String.Empty;
        /// <summary>
        /// 编制时间
        /// </summary>
        public String CPEDDATE
        {
            get { return _CPEDDATE; }
            set { _CPEDDATE = value; }
        }

        private String _PLANDETAIL = String.Empty;
        /// <summary>
        /// 详细内容
        /// </summary>
        public String PLANDETAIL
        {
            get { return _PLANDETAIL; }
            set { _PLANDETAIL = value; }
        }

        private String _CPMEMO = String.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        public String CPMEMO
        {
            get { return _CPMEMO; }
            set { _CPMEMO = value; }
        }

        private String _PLANDCOUNT = String.Empty;
        /// <summary>
        /// 计划抽样数量
        /// </summary>
        public String PLANDCOUNT
        {
            get { return _PLANDCOUNT; }
            set { _PLANDCOUNT = value; }
        }

        private String _BAOJINGTIME = String.Empty;
        /// <summary>
        /// 报警时间
        /// </summary>
        public String BAOJINGTIME
        {
            get { return _BAOJINGTIME; }
            set { _BAOJINGTIME = value; }
        }

    }
}

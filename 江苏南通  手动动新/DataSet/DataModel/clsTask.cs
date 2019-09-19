
namespace DYSeriesDataSet
{
    /// <summary>
    /// clsUserInfo 的摘要说明。
    /// </summary>
    public class clsTask
    {
        public clsTask()
        {
        }

        private string _CPCODE;
        private string _CPTITLE;
        private string _CPSDATE;
        private string _CPEDATE;
        private string _CPTPROPERTY;
        private string _CPFROM;
        private string _CPEDITOR;
        private string _CPPORGID;
        private string _CPPORG;
        private string _CPEDDATE;
        private string _CPMEMO;
        private string _PLANDETAIL;
        private string _PLANDCOUNT;
        private string _BAOJINGTIME;
        private string _UDate;

        private string _sampleid;
        private string _samplename;
        private string _itemid;
        private string _itemname;
        /// <summary>
        /// 样品ID
        /// </summary>
        public string sampleid
        {
            set
            {
                _sampleid = value;
            }
            get
            {
                return _sampleid;
            }
        }
        /// <summary>
        /// 样品名称
        /// </summary>
        public string samplename
        {
            set
            {
                _samplename = value;
            }
            get
            {
                return _samplename;
            }
        }
        /// <summary>
        /// 检测项目ID
        /// </summary>
        public string itemid
        {
            set
            {
                _itemid = value;
            }
            get
            {
                return _itemid;
            }
        }
        /// <summary>
        /// 检测项目名称
        /// </summary>
        public string itemname
        {
            set
            {
                _itemname = value;
            }
            get
            {
                return _itemname;
            }
        }
        private string _istest = "";
        /// <summary>
        /// 是否已测试
        /// </summary>
        public string istest
        {
            set
            {
                _istest = value;
            }
            get
            {
                return _istest;
            }
        }

        private string _tasktime = string.Empty;
        /// <summary>
        /// 发送时间
        /// </summary>
        public string tasktime
        {
            set
            {
                _tasktime = value;
            }
            get
            {
                return _tasktime;
            }
        }
        /// <summary>
        /// 报警时间
        /// </summary>
        public string BAOJINGTIME
        {
            set
            {
                _BAOJINGTIME = value;
            }
            get
            {
                return _BAOJINGTIME;
            }
        }

        /// <summary>
        /// 任务编号
        /// </summary>
        public string CPCODE
        {
            set
            {
                _CPCODE = value;
            }
            get
            {
                return _CPCODE;
            }
        }
        /// <summary>
        /// 任务主题
        /// </summary>
        public string CPTITLE
        {
            set
            {
                _CPTITLE = value;
            }
            get
            {
                return _CPTITLE;
            }
        }
        /// <summary>
        /// 起始日期
        /// </summary>
        public string CPSDATE
        {
            set
            {
                _CPSDATE = value;
            }
            get
            {
                return _CPSDATE;
            }
        }
        /// <summary>
        /// 结束日期
        /// </summary>
        public string CPEDATE
        {
            set
            {
                _CPEDATE = value;
            }
            get
            {
                return _CPEDATE;
            }
        }
        public string CPTPROPERTY
        {
            get { return _CPTPROPERTY; }
            set { _CPTPROPERTY = value; }
        }

        public string CPFROM
        {
            get { return _CPFROM; }
            set { _CPFROM = value; }
        }

        public string CPEDITOR
        {
            get { return _CPEDITOR; }
            set { _CPEDITOR = value; }
        }

        public string CPPORGID
        {
            get { return _CPPORGID; }
            set { _CPPORGID = value; }
        }

        public string CPPORG
        {
            get { return _CPPORG; }
            set { _CPPORG = value; }
        }

        public string CPEDDATE
        {
            get { return _CPEDDATE; }
            set { _CPEDDATE = value; }
        }

        public string CPMEMO
        {
            get { return _CPMEMO; }
            set { _CPMEMO = value; }
        }

        public string PLANDETAIL
        {
            get { return _PLANDETAIL; }
            set { _PLANDETAIL = value; }
        }

        public string PLANDCOUNT
        {
            get { return _PLANDCOUNT; }
            set { _PLANDCOUNT = value; }
        }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public string UDate
        {
            get { return _UDate; }
            set { _UDate = value; }
        }

    }
}
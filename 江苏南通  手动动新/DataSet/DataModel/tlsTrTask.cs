using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DYSeriesDataSet.DataModel
{
    public class tlsTrTask
    {
        public tlsTrTask() { }

        /// <summary>
        /// 任务编号
        /// </summary>
        private string cpCode;

        /// <summary>
        /// 任务主题
        /// </summary>
        private string cpTitle;

        /// <summary>
        /// 起始日期
        /// </summary>
        private string cpSdate;

        /// <summary>
        /// 结束日期
        /// </summary>
        private string cpEdate;

        /// <summary>
        /// 任务性质
        /// </summary>
        private string cpTprorerty;

        /// <summary>
        /// 任务来源
        /// </summary>
        private string cpFrom;

        /// <summary>
        /// 编制人
        /// </summary>
        private string cpEditor;

        /// <summary>
        /// 机构编码（编制人所属机构）
        /// </summary>
        private string cpPorgid;

        /// <summary>
        /// 机构名称（编制人所属机构）
        /// </summary>
        private string cpPorg;

        /// <summary>
        /// 编制日期
        /// </summary>
        private string cpEddate;

        /// <summary>
        /// 备注
        /// </summary>
        private string cpMemo;

        /// <summary>
        /// 任务详细内容
        /// </summary>
        private string Plandetail;

        /// <summary>
        /// 计划抽检数量
        /// </summary>
        private string Plandcount;

        /// <summary>
        /// 报警时间
        /// </summary>
        private string BaojingTime;

        private string _tasktime = "";

        public string tasktime
        {
            get 
            {
                return _tasktime;
            }
            set
            {
                _tasktime = value;
            }
        }

        private string _ItemName = "";
        public string ItemName
        {
            get
            {
                return _ItemName;
            }
            set
            {
                _ItemName = value;
            }
        }


        private string _ItemID = "";
        public string ItemID
        {
            set
            {
                _ItemID = value;
            }
            get
            {
                return _ItemID;
            }

        }

        private string _SampleName = "";
        public string SampleName
        {
            get 
            {
                return _SampleName;
            }
            set
            {
                _SampleName = value;
            }
        }

        private string _SampleID = "";
        public string SampleID
        {
            get
            {
                return _SampleID;
            }
            set 
            {
                _SampleID=value ;
            }
        }

        private string _istest = "";
        /// <summary>
        /// 是否测试
        /// </summary>
        public string istest
        {
            get 
            {
                return _istest;
            }
            set 
            {
                _istest = value;
            }
        }

        /// <summary>
        /// 任务编号
        /// </summary>
        public string CPCODE
        {
            get
            {
                return cpCode;
            }
            set
            {
                cpCode = value;
            }
        }

        /// <summary>
        /// 任务主题
        /// </summary>
        public string CPTITLE
        {
            get
            {
                return cpTitle;
            }
            set
            {
                cpTitle = value;
            }
        }

        /// <summary>
        /// 起始日期
        /// </summary>
        public string CPSDATE
        {
            get
            {
                return cpSdate;
            }
            set
            {
                cpSdate = value;
            }
        }

        /// <summary>
        /// 结束日期
        /// </summary>
        public string CPEDATE
        {
            get
            {
                return cpEdate;
            }
            set
            {
                cpEdate = value;
            }
        }

        /// <summary>
        /// 任务性质
        /// </summary>
        public string CPTPROPERTY
        {
            get
            {
                return cpTprorerty;
            }
            set
            {
                cpTprorerty = value;
            }
        }

        /// <summary>
        /// 任务来源
        /// </summary>
        public string CPFROM
        {
            get
            {
                return cpFrom;
            }
            set
            {
                cpFrom = value;
            }
        }

        /// <summary>
        /// 编制人
        /// </summary>
        public string CPEDITOR
        {
            get
            {
                return cpEditor;
            }
            set
            {
                cpEditor = value;
            }
        }

        /// <summary>
        /// 机构编码（编制人所属机构）
        /// </summary>
        public string CPPORGID
        {
            get
            {
                return cpPorgid;
            }
            set
            {
                cpPorgid = value;
            }
        }

        /// <summary>
        /// 机构名称
        /// </summary>
        public string CPPORG
        {
            get
            {
                return cpPorg;
            }
            set
            {
                cpPorg = value;
            }
        }

        /// <summary>
        /// 编制日期
        /// </summary>
        public string CPEDDATE
        {
            get
            {
                return cpEddate;
            }
            set
            {
                cpEddate = value;
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string CPMEMO
        {
            get
            {
                return cpMemo;
            }
            set
            {
                cpMemo = value;
            }
        }

        /// <summary>
        /// 任务详细内容
        /// </summary>
        public string PLANDETAIL
        {
            get
            {
                return Plandetail;
            }
            set
            {
                Plandetail = value;
            }
        }

        /// <summary>
        /// 计划抽检数量
        /// </summary>
        public string PLANDCOUNT
        {
            get
            {
                return Plandcount;
            }
            set
            {
                Plandcount = value;
            }
        }

        /// <summary>
        /// 报警时间
        /// </summary>
        public string BAOJINGTIME
        {
            get
            {
                return BaojingTime;
            }
            set
            {
                BaojingTime = value;
            }
        }

    }
}

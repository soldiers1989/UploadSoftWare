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
        /// 完成数量
        /// </summary>
        public int CompleteNum { get; set; }

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

        private string _CompleteState = string.Empty;
        /// <summary>
        /// 完成状态
        /// </summary>
        public string CompleteState
        {
            get
            {
                DateTime bjsj = DateTime.Now;
                //有报警时间
                if (DateTime.TryParse(BAOJINGTIME, out bjsj))
                {
                    //报警时间前完成
                    if (DateTime.Parse(BAOJINGTIME) > DateTime.Now)
                    {
                        if (int.Parse(PLANDCOUNT) > CompleteNum)
                        {
                            return "未完成";
                        }
                        else
                        {
                            return "完成";
                        }
                    }
                    else
                    {
                        if (int.Parse(PLANDCOUNT) > CompleteNum)
                        {
                            return "超时未完成";
                        }
                        else
                        {
                            return "超时完成";
                        }
                    }
                }
                else
                {
                    if (int.Parse(PLANDCOUNT) > CompleteNum)
                    {
                        return "未完成";
                    }
                    else
                    {
                        return "完成";
                    }
                }
            }
            set
            {
                _CompleteState = value;
            }
        }

        private string _CompleteProgress = string.Empty;

        /// <summary>
        /// 完成进度
        /// </summary>
        public string CompleteProgress
        {
            get
            {
                double progress = 0;
                if (CompleteNum > 0)
                {
                    progress = (double)CompleteNum / int.Parse(PLANDCOUNT);
                    return progress.ToString("P");
                }
                else
                {
                    return "0.00%";
                }
            }
            set { _CompleteProgress = value; }
        }

    }
}
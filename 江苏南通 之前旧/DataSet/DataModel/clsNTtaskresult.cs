using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DYSeriesDataSet.DataModel
{
    public class clsNTtaskresult
    {
        private string _SampleID = "";
        public string SampleID
        {
            set
            {
                 _SampleID=value ;
            }
            get
            {
                return _SampleID;
            }
        }
        private string _SampleName = "";
        public string SampleName
        {
            set 
            {
                _SampleName=value;
            }
            get 
            {
                return _SampleName;
            }
        }
        private string _ItemID = "";
        public string ItemID
        {
            set
            {
                 _ItemID=value ;
            }
            get 
            {
                return _ItemID;
            }
        }
        private string _ItemName = "";
        public string ItemName
        {
            set 
            {
                 _ItemName=value ;
            }
            get
            {
                return _ItemName;
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
                _istest=value;
            }
            get
            {
                return _istest;
            }
        }
        private string _tasktime = "";
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
    }
}


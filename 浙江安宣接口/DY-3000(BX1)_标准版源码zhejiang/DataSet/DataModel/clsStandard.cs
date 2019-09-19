using System;

namespace DYSeriesDataSet
{
	/// <summary>
	/// clsStandard 的摘要说明。
	/// </summary>
	public class clsStandard
	{
		public clsStandard()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
        private string _SysCode = "";
        private string _StdCode = "";
        private string _StdDes = "";
        private string _ShortCut = "";
        private string _StdInfo = "";
        private string _StdType = "";
        private string _LawsRegulations = "";
        private bool _IsReadOnly = false;
        private bool _IsLock = false;
        private string _Remark = "";
        private string _UDate = "";

		public string SysCode
		{
			set
			{
				_SysCode=value;
			}
			get
			{
				return _SysCode;
			}
		}
		public string StdCode
		{
			set
			{
				_StdCode=value;
			}
			get
			{
				return _StdCode;
			}
		}
		public string StdDes
		{
			set
			{
				_StdDes=value;
			}
			get
			{
				return _StdDes;
			}		
		}
		public string ShortCut
		{
			set
			{
				_ShortCut=value;
			}
			get
			{
				return _ShortCut;
			}		
		}
		public string StdInfo
		{
			set
			{
				_StdInfo=value;
			}
			get
			{
				return _StdInfo;
			}		
		}
		public string StdType
		{
			set
			{
				_StdType=value;
			}
			get
			{
				return _StdType;
			}		
		}
        /// <summary>
        /// 法律法规
        /// </summary>
        public string LawsRegulations
        {
            set
            {
                _LawsRegulations = value;
            }
            get
            {
                return _LawsRegulations;
            }	
        }
        /// <summary>
        /// 审核
        /// </summary>
		public bool IsReadOnly
		{
			set
			{
				_IsReadOnly=value;
			}
			get
			{
				return _IsReadOnly;
			}		
		}
        /// <summary>
        /// 锁定
        /// </summary>
		public bool IsLock
		{
			set
			{
				_IsLock=value;
			}
			get
			{
				return _IsLock;
			}		
		}
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set
            {
                _Remark = value;
            }
            get
            {
                return _Remark;
            }
        }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public string UDate 
        {
            set
            {
                _UDate = value;
            }
            get
            {
                return _UDate;
            }
        }
	}
}

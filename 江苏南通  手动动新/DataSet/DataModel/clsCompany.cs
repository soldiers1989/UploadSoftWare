using System;

namespace DYSeriesDataSet
{
	/// <summary>
	/// clsCompany 的摘要说明。
	/// </summary>
	public class clsCompany
	{
		public clsCompany()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

        private int _id;
		private string _SysCode;
        private string _Ciid;
		private string _StdCode;
		private string _CompanyID;
		private string _OtherCodeInfo;
		private string _FullName;
		private string _ShortName;
		private string _DisplayName;
		private string _ShortCut;
		private string _Property;
		private string _KindCode;
		private long _RegCapital;
		private string _Unit;
		private string _Incorporator;
		private DateTime? _RegDate;
		private string _DistrictCode;
		private string _PostCode;
		private string _Address;
		private string _LinkMan;
		private string _LinkInfo;
		private string _CreditLevel;
		private string _CreditRecord;
		private string _ProductInfo;
		private string _OtherInfo;
		private string _FoodSafeRecord;
		private string _CheckLevel;
		private bool _IsReadOnly;
		private bool _IsLock;
		private string _Remark;
		private string _ComProperty;

        private string _TSign;
        private string _CAllow;

        private string _ISSUEAGENCY;
        private string _ISSUEDATE;
        private string _PERIODSTART;
        private string _PERIODEND;
        private string _VIOLATENUM;
        private string _LONGITUDE;
        private string _LATITUDE;
        private string _SCOPE;
        private string _PUNISH;
        private string _UDate;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 违规处理情况
        /// </summary>
        public string PUNISH
        {
            get { return _PUNISH; }
            set { _PUNISH = value; }
        }
        /// <summary>
        /// 发证机构
        /// </summary>
        public string ISSUEAGENCY
        {
            get { return _ISSUEAGENCY; }
            set { _ISSUEAGENCY = value; }
        }
        /// <summary>
        /// 发证日期
        /// </summary>
        public string ISSUEDATE
        {
            get { return _ISSUEDATE; }
            set { _ISSUEDATE = value; }
        }
        /// <summary>
        /// 有效起始日期
        /// </summary>
        public string PERIODSTART
        {
            get { return _PERIODSTART; }
            set { _PERIODSTART = value; }
        }
        /// <summary>
        /// 有些截取日期
        /// </summary>
        public string PERIODEND
        {
            get { return _PERIODEND; }
            set { _PERIODEND = value; }
        }
        /// <summary>
        /// 违规次数
        /// </summary>
        public string VIOLATENUM
        {
            get { return _VIOLATENUM; }
            set { _VIOLATENUM = value; }
        }
        /// <summary>
        /// 经度
        /// </summary>
        public string LONGITUDE
        {
            get { return _LONGITUDE; }
            set { _LONGITUDE = value; }
        }
        /// <summary>
        /// 纬度
        /// </summary>
        public string LATITUDE
        {
            get { return _LATITUDE; }
            set { _LATITUDE = value; }
        }
        /// <summary>
        /// 规模
        /// </summary>
        public string SCOPE
        {
            get { return _SCOPE; }
            set { _SCOPE = value; }
        }
        /// <summary>
        /// 许可证号
        /// </summary>
        public string CAllow
        {
            get { return _CAllow; }
            set { _CAllow = value; }
        }

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
        /// <summary>
        /// 外键
        /// </summary>
        public string Ciid
        {
            set
            {
                _Ciid = value;
            }
            get
            {
                return _Ciid;
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
		public string CompanyID
		{
			set
			{
				_CompanyID=value;
			}
			get
			{
				return _CompanyID;
			}		
		}
		public string OtherCodeInfo
		{
			set
			{
				_OtherCodeInfo=value;
			}
			get
			{
				return _OtherCodeInfo;
			}		
		}
		public string FullName
		{
			set
			{
				_FullName=value;
			}
			get
			{
				return _FullName;
			}		
		}
		public string ShortName
		{
			set
			{
				_ShortName=value;
			}
			get
			{
				return _ShortName;
			}		
		}
		public string DisplayName
		{
			set
			{
				_DisplayName=value;
			}
			get
			{
				return _DisplayName;
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
		public string Property
		{
			set
			{
				_Property=value;
			}
			get
			{
				return _Property;
			}		
		}
		public string KindCode
		{
			set
			{
				_KindCode=value;
			}
			get
			{
				return _KindCode;
			}		
		}
		public long RegCapital
		{
			set
			{
				_RegCapital=value;
			}
			get
			{
				return _RegCapital;
			}		
		}
		public string Unit
		{
			set
			{
				_Unit=value;
			}
			get
			{
				return _Unit;
			}		
		}
		public string Incorporator
		{
			set
			{
				_Incorporator=value;
			}
			get
			{
				return _Incorporator;
			}		
		}
		public DateTime? RegDate
		{
			set
			{
				_RegDate=value;
			}
			get
			{
				return _RegDate;
			}		
		}
		public string DistrictCode
		{
			set
			{
				_DistrictCode=value;
			}
			get
			{
				return _DistrictCode;
			}		
		}
		public string PostCode
		{
			set
			{
				_PostCode=value;
			}
			get
			{
				return _PostCode;
			}		
		}
		public string Address
		{
			set
			{
				_Address=value;
			}
			get
			{
				return _Address;
			}		
		}
		public string LinkMan
		{
			set
			{
				_LinkMan=value;
			}
			get
			{
				return _LinkMan;
			}		
		}
		public string LinkInfo
		{
			set
			{
				_LinkInfo=value;
			}
			get
			{
				return _LinkInfo;
			}		
		}
		public string CreditLevel
		{
			set
			{
				_CreditLevel=value;
			}
			get
			{
				return _CreditLevel;
			}		
		}
		public string CreditRecord
		{
			set
			{
				_CreditRecord=value;
			}
			get
			{
				return _CreditRecord;
			}		
		}
		public string ProductInfo
		{
			set
			{
				_ProductInfo=value;
			}
			get
			{
				return _ProductInfo;
			}		
		}
		public string OtherInfo
		{
			set
			{
				_OtherInfo=value;
			}
			get
			{
				return _OtherInfo;
			}		
		}
		public string FoodSafeRecord
		{
			set
			{
				_FoodSafeRecord=value;
			}
			get
			{
				return _FoodSafeRecord;
			}		
		}
		public string CheckLevel
		{
			set
			{
				_CheckLevel=value;
			}
			get
			{
				return _CheckLevel;
			}		
		}
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
		public string Remark
		{
			set
			{
				_Remark=value;
			}
			get
			{
				return _Remark;
			}		
		}
		public string ComProperty
		{
			set
			{
				_ComProperty=value;
			}
			get
			{
				return _ComProperty;
			}		
		}
        /// <summary>
        /// 生产或其他标示
        /// </summary>
        public string TSign
        {
            get { return _TSign; }
            set { _TSign = value; }
        }

        /// <summary>
        /// 更新时间
        /// </summary>
        public string UDate
        {
            get { return _UDate; }
            set { _UDate = value; }
        }
	}
}

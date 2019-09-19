using System;

namespace DataSetModel
{
	/// <summary>
	/// clsCheckComType 的摘要说明。
	/// </summary>
	public class clsCheckComType
	{
		public clsCheckComType()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		private long _ID;
		private string _TypeName;
		private string _NameCall;
		private string _AreaCall;
		private string _VerType;
		private bool _IsReadOnly;
		private bool _IsLock;
		private string _ComKind;
		private string _AreaTitle;
		private string _NameTitle;
		private string _DomainTitle;
		private string _SampleTitle;

		public long ID
		{
			set
			{
				_ID=value;
			}
			get
			{
				return _ID;
			}
		}
		public string TypeName
		{
			set
			{
				_TypeName=value;
			}
			get
			{
				return _TypeName;
			}		
		}
		public string NameCall
		{
			set
			{
				_NameCall=value;
			}
			get
			{
				return _NameCall;
			}		
		}
		public string AreaCall
		{
			set
			{
				_AreaCall=value;
			}
			get
			{
				return _AreaCall;
			}		
		}
		public string VerType
		{
			set
			{
				_VerType=value;
			}
			get
			{
				return _VerType;
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
		public string ComKind
		{
			set
			{
				_ComKind=value;
			}
			get
			{
				return _ComKind;
			}		
		}
		public string AreaTitle
		{
			set
			{
				_AreaTitle=value;
			}
			get
			{
				return _AreaTitle;
			}		
		}
		public string NameTitle
		{
			set
			{
				_NameTitle=value;
			}
			get
			{
				return _NameTitle;
			}		
		}
		public string DomainTitle
		{
			set
			{
				_DomainTitle=value;
			}
			get
			{
				return _DomainTitle;
			}		
		}
		public string SampleTitle
		{
			set
			{
				_SampleTitle=value;
			}
			get
			{
				return _SampleTitle;
			}		
		}
	}
}

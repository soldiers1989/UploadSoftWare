using System;

namespace DY.FoodClientLib
{
	/// <summary>
	/// clsCompanyKind 的摘要说明。
	/// </summary>
	public class clsCompanyKind
	{
		public clsCompanyKind()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		private string _SysCode;
		private string _StdCode;
		private string _Name;
		private string _CompanyProperty;
		private bool _IsReadOnly;
		private bool _IsLock;
		private string _Remark;

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
		public string Name
		{
			set
			{
				_Name=value;
			}
			get
			{
				return _Name;
			}		
		}
		public string CompanyProperty
		{
			set
			{
				_CompanyProperty=value;
			}
			get
			{
				return _CompanyProperty;
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
	}
}

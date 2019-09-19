using System;

namespace DataSetModel
{
	/// <summary>
	/// clsCom_UserUnit 的摘要说明。
	/// </summary>
	public class clsCom_UserUnit
	{
		public clsCom_UserUnit()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		private string _SysCode;
		private string _StdCode;
		private string _FullName;
		private string _ShortName;
		private string _DisplayName;
		private string _ShortCut;
		private string _DistrictCode;
		private string _PostCode;
		private string _Address;
		private string _LinkMan;
		private string _LinkInfo;
		private string _WWWInfo;
		private int _IsReadOnly;
		private int _IsLock;
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
		public string WWWInfo
		{
			set
			{
				_WWWInfo=value;
			}
			get
			{
				return _WWWInfo;
			}		
		}
		public int IsReadOnly
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
		public int IsLock
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

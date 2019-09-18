using System;

namespace DY.FoodClientLib
{
	/// <summary>
	/// clsCheckItem 的摘要说明。
	/// </summary>
	public class clsCheckItem
	{
		private string _SysCode;
		private string _StdCode;
		private string _ItemDes;
		private string _CheckType;
		private string _StandardCode;
		private string _StandardValue;
		private string _Unit;
		private string _DemarcateInfo;
		private string _TestValue;
		private string _OperateHelp;
		private string _CheckLevel;
		private bool _IsReadOnly;
		private bool _IsLock;
		private string _Remark;
		
		public clsCheckItem()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
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
		public string ItemDes
		{
			set
			{
				_ItemDes=value;
			}
			get
			{
				return _ItemDes;
			}		
		}
		public string CheckType
		{
			set
			{
				_CheckType=value;
			}
			get
			{
				return _CheckType;
			}		
		}
		public string StandardValue
		{
			set
			{
				_StandardValue=value;
			}
			get
			{
				return _StandardValue;
			}		
		}
		public string StandardCode
		{
			set
			{
				_StandardCode=value;
			}
			get
			{
				return _StandardCode;
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
		public string DemarcateInfo
		{
			set
			{
				_DemarcateInfo=value;
			}
			get
			{
				return _DemarcateInfo;
			}		
		}
		public string TestValue
		{
			set
			{
				_TestValue=value;
			}
			get
			{
				return _TestValue;
			}		
		}
		public string OperateHelp
		{
			set
			{
				_OperateHelp=value;
			}
			get
			{
				return _OperateHelp;
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
	}
}

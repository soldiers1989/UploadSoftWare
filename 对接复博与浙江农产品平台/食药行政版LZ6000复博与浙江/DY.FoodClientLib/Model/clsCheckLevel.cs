using System;

namespace DY.FoodClientLib
{
	/// <summary>
	/// clsCheckLevel 的摘要说明。
	/// </summary>
	public class clsCheckLevel
	{
		public clsCheckLevel()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		private string _CheckLevel;
		private bool _IsReadOnly;
		private bool _IsLock;
		private string _Remark;

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

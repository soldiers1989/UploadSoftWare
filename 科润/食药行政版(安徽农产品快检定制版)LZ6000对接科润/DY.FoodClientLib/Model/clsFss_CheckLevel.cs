using System;

namespace DY.FoodClientLib
{
	/// <summary>
	/// clsFss_CheckLevel 的摘要说明。
	/// </summary>
	public class clsFss_CheckLevel
	{
		public clsFss_CheckLevel()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		private string _CheckLevel;
		private int _IsReadOnly;
		private int _IsLock;
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

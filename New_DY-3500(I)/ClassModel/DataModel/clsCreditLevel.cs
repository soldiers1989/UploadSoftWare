using System;

namespace DataSetModel
{
	/// <summary>
	/// clsCreditLevel 的摘要说明。
	/// </summary>
	public class clsCreditLevel
	{
		public clsCreditLevel()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		private string _CreditLevel;
		private bool _IsReadOnly;
		private bool _IsLock;
		private string _Remark;

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

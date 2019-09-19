using System;

namespace DataSetModel
{
	/// <summary>
	/// clsStandardType 的摘要说明。
	/// </summary>
	public class clsStandardType
	{
		public clsStandardType()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		private string _StdName;
		private bool _IsReadOnly;
		private bool _IsLock;
		private string _Remark;

		public string StdName
		{
			set
			{
				_StdName=value;
			}
			get
			{
				return _StdName;
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

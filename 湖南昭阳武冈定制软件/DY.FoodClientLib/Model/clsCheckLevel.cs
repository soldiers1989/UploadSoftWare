using System;

namespace DY.FoodClientLib
{
	/// <summary>
	/// clsCheckLevel ��ժҪ˵����
	/// </summary>
	public class clsCheckLevel
	{
		public clsCheckLevel()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
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

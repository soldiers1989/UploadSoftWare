using System;

namespace DY.FoodClientLib
{
	/// <summary>
	/// clsFss_CreditLevel ��ժҪ˵����
	/// </summary>
	public class clsFss_CreditLevel
	{
		public clsFss_CreditLevel()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		private string _CreditLevel;
		private int _IsReadOnly;
		private int _IsLock;
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

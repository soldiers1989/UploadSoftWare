using System;

namespace DY.FoodClientLib
{
	/// <summary>
	/// clsFss_StandardType ��ժҪ˵����
	/// </summary>
	public class clsFss_StandardType
	{
		public clsFss_StandardType()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		private string _StdName;
		private int _IsReadOnly;
		private int _IsLock;
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

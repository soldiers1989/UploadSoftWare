using System;

namespace DYSeriesDataSet
{
	/// <summary>
	/// clsCheckType ��ժҪ˵����
	/// </summary>
	public class clsCheckType
	{
		private string _Name;
		private bool _IsReadOnly;
		private bool _IsLock;
		private string _Remark;
		
		public clsCheckType()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
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

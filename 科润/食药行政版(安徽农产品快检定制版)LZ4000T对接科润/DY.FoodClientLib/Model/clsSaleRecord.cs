using System;

namespace DY.FoodClientLib
{
	/// <summary>
	/// clsSaleRecord 的摘要说明。
	/// </summary>
	public class clsSaleRecord
	{
		public clsSaleRecord()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		private string _SysCode;
		private string _StdCode;
		private string _CompanyID;
		private string _CompanyName;
		private string _DisplayName;
		private DateTime _SaleDate;
		private string _FoodID;
		private string _FoodName;
		private string _Model;
		private decimal _SaleNumber;
		private decimal _Price;
		private string  _Unit;
		private string  _PurchaserID;
		private string _PurchaserName;
		private string _LinkInfo;
		private string _LinkMan;
		private string _MakeMan;
		private string _Remark;
		private string _DistrictCode;
		private bool _IsSended;
		private DateTime _SentDate;
		

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
		public string CompanyID
		{
			set
			{
				_CompanyID=value;
			}
			get
			{
				return _CompanyID;
			}		
		}
		public string CompanyName
		{
			set
			{
				_CompanyName=value;
			}
			get
			{
				return _CompanyName;
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
		public DateTime SaleDate
		{
			set
			{
				_SaleDate=value;
			}
			get
			{
				return _SaleDate;
			}		
		}
		public string FoodID
		{
			set
			{
				_FoodID=value;
			}
			get
			{
				return _FoodID;
			}		
		}
		public string FoodName
		{
			set
			{
				_FoodName=value;
			}
			get
			{
				return _FoodName;
			}		
		}
		public string Model
		{
			set
			{
				_Model=value;
			}
			get
			{
				return _Model;
			}		
		}
		public decimal SaleNumber
		{
			set
			{
				_SaleNumber=value;
			}
			get
			{
				return _SaleNumber;
			}		
		}
		public decimal Price
		{
			set
			{
				_Price=value;
			}
			get
			{
				return _Price;
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
		public string PurchaserID
		{
			set
			{
				_PurchaserID=value;
			}
			get
			{
				return _PurchaserID;
			}		
		}
		public string PurchaserName
		{
			set
			{
				_PurchaserName=value;
			}
			get
			{
				return _PurchaserName;
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
		public string MakeMan
		{
			set
			{
				_MakeMan=value;
			}
			get
			{
				return _MakeMan;
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
		public bool IsSended
		{
			set
			{
				_IsSended=value;
			}
			get
			{
				return _IsSended;
			}		
		}
		public DateTime SentDate
		{
			set
			{
				_SentDate=value;
			}
			get
			{
				return _SentDate;
			}		
		}
	}
}

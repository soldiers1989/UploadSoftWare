using System;

namespace DYSeriesDataSet
{
	/// <summary>
	/// clsStandard ��ժҪ˵����
	/// </summary>
	public class clsStandard
	{
		public clsStandard()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
        private string _SysCode = "";
        private string _StdCode = "";
        private string _StdDes = "";
        private string _ShortCut = "";
        private string _StdInfo = "";
        private string _StdType = "";
        private string _LawsRegulations = "";
        private bool _IsReadOnly = false;
        private bool _IsLock = false;
        private string _Remark = "";
        private string _UDate = "";

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
		public string StdDes
		{
			set
			{
				_StdDes=value;
			}
			get
			{
				return _StdDes;
			}		
		}
		public string ShortCut
		{
			set
			{
				_ShortCut=value;
			}
			get
			{
				return _ShortCut;
			}		
		}
		public string StdInfo
		{
			set
			{
				_StdInfo=value;
			}
			get
			{
				return _StdInfo;
			}		
		}
		public string StdType
		{
			set
			{
				_StdType=value;
			}
			get
			{
				return _StdType;
			}		
		}
        /// <summary>
        /// ���ɷ���
        /// </summary>
        public string LawsRegulations
        {
            set
            {
                _LawsRegulations = value;
            }
            get
            {
                return _LawsRegulations;
            }	
        }
        /// <summary>
        /// ���
        /// </summary>
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
        /// <summary>
        /// ����
        /// </summary>
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
        /// <summary>
        /// ��ע
        /// </summary>
        public string Remark
        {
            set
            {
                _Remark = value;
            }
            get
            {
                return _Remark;
            }
        }
        /// <summary>
        /// ����������
        /// </summary>
        public string UDate 
        {
            set
            {
                _UDate = value;
            }
            get
            {
                return _UDate;
            }
        }
	}
}

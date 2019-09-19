using System;

namespace DYSeriesDataSet
{
	/// <summary>
	/// clsCheckItem ��ժҪ˵����
	/// </summary>
	public class clsCheckItem
	{
        private string _SysCode = "";
        private string _StdCode = "";
        private string _ItemDes = "";
        private string _CheckType = "";
        private string _StandardCode = "";
        private string _StandardValue = "";
        private string _Unit = "";
        private string _DemarcateInfo = "";
        private string _TestValue = "";
        private string _OperateHelp = "";
        private string _CheckLevel = "";
        private bool _IsReadOnly = false;
        private bool _IsLock = false;
        private string _Remark = "";
        private string _UDate = "";
		
		public clsCheckItem()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

        /// <summary>
        /// ����
        /// </summary>
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
        /// <summary>
        /// �����Ŀ���
        /// </summary>
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
        /// <summary>
        /// �����Ŀ����
        /// </summary>
		public string ItemDes
		{
			set
			{
				_ItemDes=value;
			}
			get
			{
				return _ItemDes;
			}		
		}
        /// <summary>
        /// �������
        /// </summary>
		public string CheckType
		{
			set
			{
				_CheckType=value;
			}
			get
			{
				return _CheckType;
			}		
		}
        /// <summary>
        /// ��׼ֵ
        /// </summary>
		public string StandardValue
		{
			set
			{
				_StandardValue=value;
			}
			get
			{
				return _StandardValue;
			}		
		}
        /// <summary>
        /// �����ĿID��������
        /// </summary>
		public string StandardCode
		{
			set
			{
				_StandardCode=value;
			}
			get
			{
				return _StandardCode;
			}		
		}
        /// <summary>
        /// ��λ
        /// </summary>
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
        /// <summary>
        /// ����׼ֵ����
        /// </summary>
		public string DemarcateInfo
		{
			set
			{
				_DemarcateInfo=value;
			}
			get
			{
				return _DemarcateInfo;
			}		
		}
        /// <summary>
        /// ����ֵ
        /// </summary>
		public string TestValue
		{
			set
			{
				_TestValue=value;
			}
			get
			{
				return _TestValue;
			}		
		}
        /// <summary>
        /// ����˵��
        /// </summary>
		public string OperateHelp
		{
			set
			{
				_OperateHelp=value;
			}
			get
			{
				return _OperateHelp;
			}		
		}
        /// <summary>
        /// ���ȼ�
        /// </summary>
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
        /// <summary>
        /// �Ƿ������
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
        /// �Ƿ�����
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
        /// ������ʱ��
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

using System;

namespace DYSeriesDataSet
{
	/// <summary>
	/// ������ʵ����
	/// </summary>
	public class clsMachine
	{
		public clsMachine()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
        private int _LinkComNo;
        private bool _IsSupport;
        private bool _IsShow;
		private string _SysCode;
		private string _MachineName;
		private string _ShortCut;
		private string _MachineModel;
		private string _Company;
		private string _Protocol;

		private float _TestValue;
		private string _TestSign;
		private string _LinkStdCode;
        private int _orderId;


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
        /// ��������
        /// </summary>
		public string MachineName
		{
			set
			{
				_MachineName=value;
			}
			get
			{
				return _MachineName;
			}		
		}
        /// <summary>
        /// ��ݱ���
        /// </summary>
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
        /// <summary>
        /// �ͺ�
        /// </summary>
		public string MachineModel
		{
			set
			{
				_MachineModel=value;
			}
			get
			{
				return _MachineModel;
			}		
		}
        /// <summary>
        /// ��������
        /// </summary>
		public string Company
		{
			set
			{
				_Company=value;
			}
			get
			{
				return _Company;
			}		
		}
        /// <summary>
        /// ���ò��
        /// </summary>
		public string Protocol
		{
			set
			{
				_Protocol=value;
			}
			get
			{
				return _Protocol;
			}		
		}
        /// <summary>
        /// ʹ�ö˿ں�
        /// </summary>
		public int LinkComNo
		{
			set
			{
				_LinkComNo=value;
			}
			get
			{
				return _LinkComNo;
			}		
		}
        /// <summary>
        /// �Ƿ�Ĭ��
        /// </summary>
		public bool IsSupport
		{
			set
			{
				_IsSupport=value;
			}
			get
			{
				return _IsSupport;
			}		
		}
        /// <summary>
        /// ���ֵ
        /// </summary>
		public float TestValue
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
        /// ������
		/// </summary>
		public string TestSign
		{
			set
			{
				_TestSign=value;
			}
			get
			{
				return _TestSign;
			}		
		}

		/// <summary>
        /// ���ñ�׼�����룩
		/// </summary>
		public string LinkStdCode
		{
			set
			{
				_LinkStdCode=value;
			}
			get
			{
				return _LinkStdCode;
			}		
		}

        /// <summary>
        /// �Ƿ���ʾ����Ӧ�ô�����
        /// </summary>
        public bool IsShow
        {
            get { return _IsShow; }
            set { _IsShow = value; }
        }

        /// <summary>
        /// ������
        /// </summary>
        public int OrderId
        {
            get { return _orderId; }
            set { _orderId = value; }
        }
	}
}

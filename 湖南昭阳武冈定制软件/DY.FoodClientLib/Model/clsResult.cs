using System;

namespace DY.FoodClientLib
{
	/// <summary>
	///����¼ʵ����
    ///ԭ����û���κ�ע��
    ///�޸ģ��¹��� 2011-06-23
	/// </summary>
    public class clsResult
    {
        public clsResult()
        {
        }
        private string _SysCode;
        private string _ResultType;
        private string _CheckNo;
        private string _StdCode;
        private string _FoodCode;
        private string _CheckPlaceCode;
        private string _CheckedCompany;
        private string _CheckedCompanyName;
        private string _CheckedComDis;
        private string _SampleUnit;
        private string _SampleCode;
        private string _ImportNum;
        private string _SaveNum;
        private DateTime? _ProduceDate;
        private DateTime _TakeDate;
        private DateTime _CheckStartDate;
        private DateTime _CheckEndDate;
        private string _SampleBaseNum;
        private string _SampleNum;
        private string _Unit;
        private string _SampleLevel;
        private string _SampleModel;
        private string _SampleState;
        private string _SentCompany;
        private string _Provider;
        private string _Standard;
        private string _CheckMachine;
        private string _CheckTotalItem;
        private string _CheckValueInfo;
        private string _Result;
        private string _UpperCompany;
        private string _UpperCompanyName;
        private string _OrCheckNo;
        private string _CheckType;
        private string _CheckUnitCode;
        private string _ProduceCompany;
        private string _Checker;
        private string _Assessor;
        private string _Organizer;
        private bool _IsSended;
        private DateTime _SendedDate;
        private string _Sender;
        private string _Remark;
        private string _ProducePlace;
        private string _StandValue;
        private string _ResultInfo;
        private string _CheckPlanCode;
        private string _SaleNum;
        private string _Price;
        private string _CheckederVal;
        private string _IsSentCheck;
        private string _CheckederRemark;
        private bool _IsReSended;
        private string _Notes;

        private string _holesNum;
        private string _machineSampleNum;
        private string _machineItemName;

        /// <summary>
        /// ��λ�������ֶ�
        /// </summary>
        public string HolesNum
        {
            get { return _holesNum; }
            set { _holesNum = value; }
        }

        /// <summary>
        /// ����������/������
        /// </summary>
        public string MachineSampleNum
        {
            get { return _machineSampleNum; }
            set { _machineSampleNum = value; }
        }
        /// <summary>
        /// ���������Ŀ����
        /// </summary>
        public string MachineItemName
        {
            get { return _machineItemName; }
            set { _machineItemName = value; }
        }

        /// <summary>
        /// ϵͳ����
        /// </summary>
        public string SysCode
        {
            set
            {
                _SysCode = value;
            }
            get
            {
                return _SysCode;
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public string ResultType
        {
            set
            {
                _ResultType = value;
            }
            get
            {
                return _ResultType;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public string CheckNo
        {
            set
            {
                _CheckNo = value;
            }
            get
            {
                return _CheckNo;
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string StdCode
        {
            set
            {
                _StdCode = value;
            }
            get
            {
                return _StdCode;
            }
        }
        /// <summary>
        /// ʳƷ����
        /// </summary>
        public string FoodCode
        {
            set
            {
                _FoodCode = value;
            }
            get
            {
                return _FoodCode;
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public string CheckPlaceCode
        {
            set
            {
                _CheckPlaceCode = value;
            }
            get
            {
                return _CheckPlaceCode;
            }
        }
        /// <summary>
        /// ��ⵥλ
        /// </summary>
        public string CheckedCompany
        {
            set
            {
                _CheckedCompany = value;
            }
            get
            {
                return _CheckedCompany;
            }
        }
        /// <summary>
        /// ��ⵥλ����
        /// </summary>
        public string CheckedCompanyName
        {
            set
            {
                _CheckedCompanyName = value;
            }
            get
            {
                return _CheckedCompanyName;
            }
        }
        /// <summary>
        /// ��ⵥλ����
        /// </summary>
        public string CheckedComDis
        {
            set
            {
                _CheckedComDis = value;
            }
            get
            {
                return _CheckedComDis;
            }
        }
        /// <summary>
        ///  ��Ʒ��λ
        /// </summary>
        public string SampleUnit
        {
            set
            {
                _SampleUnit = value;
            }
            get
            {
                return _SampleUnit;
            }
        }
        /// <summary>
        /// ��Ʒ���� 
        /// </summary>
        public string SampleCode
        {
            set
            {
                _SampleCode = value;
            }
            get
            {
                return _SampleCode;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ImportNum
        {
            set
            {
                _ImportNum = value;
            }
            get
            {
                return _ImportNum;
            }
        }
        public string SaveNum
        {
            set
            {
                _SaveNum = value;
            }
            get
            {
                return _SaveNum;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public DateTime? ProduceDate
        {
            set
            {
                _ProduceDate = value;
            }
            get
            {
                return _ProduceDate;
            }
        }
        /// <summary>
        /// ������� 
        /// </summary>
        public DateTime TakeDate
        {
            set
            {
                _TakeDate = value;
            }
            get
            {
                return _TakeDate;
            }
        }
        /// <summary>
        /// ��⿪ʼ����
        /// </summary>
        public DateTime CheckStartDate
        {
            set
            {
                _CheckStartDate = value;
            }
            get
            {
                return _CheckStartDate;
            }
        }
        /// <summary>
        /// ������ʱ��
        /// </summary>
        public DateTime CheckEndDate
        {
            set
            {
                _CheckEndDate = value;
            }
            get
            {
                return _CheckEndDate;
            }
        }
        /// <summary>
        /// ��Ʒ������
        /// </summary>
        public string SampleBaseNum
        {
            set
            {
                _SampleBaseNum = value;
            }
            get
            {
                return _SampleBaseNum;
            }
        }
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        public string SampleNum
        {
            set
            {
                _SampleNum = value;
            }
            get
            {
                return _SampleNum;
            }
        }
        /// <summary>
        /// ��λ
        /// </summary>
        public string Unit
        {
            set
            {
                _Unit = value;
            }
            get
            {
                return _Unit;
            }
        }
        /// <summary>
        /// ��Ʒˮƽ
        /// </summary>
        public string SampleLevel
        {
            set
            {
                _SampleLevel = value;
            }
            get
            {
                return _SampleLevel;
            }
        }
        /// <summary>
        /// ��Ʒ���
        /// </summary>
        public string SampleModel
        {
            set
            {
                _SampleModel = value;
            }
            get
            {
                return _SampleModel;
            }
        }
        /// <summary>
        /// ��Ʒ״̬
        /// </summary>
        public string SampleState
        {
            set
            {
                _SampleState = value;
            }
            get
            {
                return _SampleState;
            }
        }
        public string SentCompany
        {
            set
            {
                _SentCompany = value;
            }
            get
            {
                return _SentCompany;
            }
        }
        /// <summary>
        /// ��Ӧ��
        /// </summary>
        public string Provider
        {
            set
            {
                _Provider = value;
            }
            get
            {
                return _Provider;
            }
        }

        /// <summary>
        /// ��׼
        /// </summary>
        public string Standard
        {
            set
            {
                _Standard = value;
            }
            get
            {
                return _Standard;
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public string CheckMachine
        {
            set
            {
                _CheckMachine = value;
            }
            get
            {
                return _CheckMachine;
            }
        }
        /// <summary>
        /// �����Ŀ����
        /// </summary>
        public string CheckTotalItem
        {
            set
            {
                _CheckTotalItem = value;
            }
            get
            {
                return _CheckTotalItem;
            }
        }
        /// <summary>
        /// ���ֵ��Ϣ
        /// </summary>
        public string CheckValueInfo
        {
            set
            {
                _CheckValueInfo = value;
            }
            get
            {
                return _CheckValueInfo;
            }
        }

        /// <summary>
        /// �����
        /// </summary>
        public string Result
        {
            set
            {
                _Result = value;
            }
            get
            {
                return _Result;
            }
        }
        public string UpperCompany
        {
            set
            {
                _UpperCompany = value;
            }
            get
            {
                return _UpperCompany;
            }
        }
        public string UpperCompanyName
        {
            set
            {
                _UpperCompanyName = value;
            }
            get
            {
                return _UpperCompanyName;
            }
        }
        public string OrCheckNo
        {
            set
            {
                _OrCheckNo = value;
            }
            get
            {
                return _OrCheckNo;
            }
        }
        public string CheckType
        {
            set
            {
                _CheckType = value;
            }
            get
            {
                return _CheckType;
            }
        }
        public string CheckUnitCode
        {
            set
            {
                _CheckUnitCode = value;
            }
            get
            {
                return _CheckUnitCode;
            }
        }
        /// <summary>
        /// �����
        /// </summary>
        public string Checker
        {
            set
            {
                _Checker = value;
            }
            get
            {
                return _Checker;
            }
        }
        /// <summary>
        /// ������λ
        /// </summary>
        public string ProduceCompany
        {
            set
            {
                _ProduceCompany = value;
            }
            get
            {
                return _ProduceCompany;
            }
        }
        /// <summary>
        /// ��׼��
        /// </summary>
        public string Assessor
        {
            set
            {
                _Assessor = value;
            }
            get
            {
                return _Assessor;
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string Organizer
        {
            set
            {
                _Organizer = value;
            }
            get
            {
                return _Organizer;
            }
        }
        /// <summary>
        /// �Ƿ��ϴ�
        /// </summary>
        public bool IsSended
        {
            set
            {
                _IsSended = value;
            }
            get
            {
                return _IsSended;
            }
        }
        /// <summary>
        /// �ϴ�����
        /// </summary>
        public DateTime SendedDate
        {
            set
            {
                _SendedDate = value;
            }
            get
            {
                return _SendedDate;
            }
        }
        /// <summary>
        /// �ϴ���
        /// </summary>
        public string Sender
        {
            set
            {
                _Sender = value;
            }
            get
            {
                return _Sender;
            }
        }
        /// <summary>
        /// ��ע˵��
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
        /// ����
        /// </summary>
        public string ProducePlace
        {
            set
            {
                _ProducePlace = value;
            }
            get
            {
                return _ProducePlace;
            }
        }
        /// <summary>
        /// ��׼ֵ
        /// </summary>
        public string StandValue
        {
            set
            {
                _StandValue = value;
            }
            get
            {
                return _StandValue;
            }
        }

        /// <summary>
        /// ���������Ϣ
        /// </summary>
        public string ResultInfo
        {
            set
            {
                _ResultInfo = value;
            }
            get
            {
                return _ResultInfo;
            }
        }
        /// <summary>
        /// ���ƻ�����
        /// </summary>
        public string CheckPlanCode
        {
            set
            {
                _CheckPlanCode = value;
            }
            get
            {
                return _CheckPlanCode;
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string SaleNum
        {
            set
            {
                _SaleNum = value;
            }
            get
            {
                return _SaleNum;
            }
        }
        /// <summary>
        /// �۸�
        /// </summary>
        public string Price
        {
            set
            {
                _Price = value;
            }
            get
            {
                return _Price;
            }
        }
        /// <summary>
        /// ���ֵ
        /// </summary>
        public string CheckederVal
        {
            set
            {
                _CheckederVal = value;
            }
            get
            {
                return _CheckederVal;
            }
        }
        public string IsSentCheck
        {
            set
            {
                _IsSentCheck = value;
            }
            get
            {
                return _IsSentCheck;
            }
        }
        /// <summary>
        /// ����˱�ע
        /// </summary>
        public string CheckederRemark
        {
            set
            {
                _CheckederRemark = value;
            }
            get
            {
                return _CheckederRemark;
            }
        }
        public string Notes
        {
            set
            {
                _Notes = value;
            }
            get
            {
                return _Notes;
            }
        }
        /// <summary>
        /// �Ƿ��Ѿ��ط�
        /// </summary>
        public bool IsReSended
        {
            set
            {
                _IsReSended = value;
            }
            get
            {
                return _IsReSended;
            }
        }
    }
}

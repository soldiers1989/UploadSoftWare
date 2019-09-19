using System;
using System.Data;

namespace DY.FoodClientLib
{
	/// <summary>
	/// ����������Ϣ��
    /// ԭ������û�м��κ�ע�͡�
    /// �޸Ĳ��ִ��벢��ע�ͣ����ܻ������ⲻ��ȷ�ĵط�
	/// </summary>
    public class ShareOption
    {
        /// <summary>
        ///��̬���ֹ�ⲿ����
        /// </summary>
        private ShareOption()
        {
   
        }

        #region ��������
        /// <summary>
        /// �¶�,���Ϊ010101
        /// </summary>
        public static decimal SysTemperature = 25;

        /// <summary>
        /// ʪ�ȣ����Ϊ010102
        /// </summary>
        public static decimal SysHumidity = 40;

        /// <summary>
        /// ��λ, ���Ϊ010103
        /// </summary>
        public static string SysUnit = "����";
        #endregion

        /// <summary>
        /// ϵͳ����,2011-06-22����
        /// </summary>
        public static string SystemTitle = "��ԪʳƷ��ȫ��⹤��վϵͳ";

        /// <summary>
        /// �Զ���¼,���Ϊ020101
        /// </summary>
        public static bool SysAutoLogin = false;

        /// <summary>
        /// ϵͳ�˳�ʱ�Ƿ񵯳���ʾ,��Ӧ���ݿ���Ϊ020103
        /// </summary>
        public static bool SysExitPrompt = false;

        /// <summary>
        ///�����ֹ�¼�뱻�쵥λ�еľ�Ӫ��,��Ӧ���ݿ���Ϊ020105
        /// </summary>
        public static bool AllowHandInputCheckUint = false;

        /// <summary>
        /// �Զ����ɼ����,��Ӧ���ݿ���Ϊ020106
        /// </summary>
        public static bool SysStdCodeSame = true;

        /// <summary>
        /// ��ʱ��ʾ1����,ϵͳ���� ��Ӧ���ݿ���Ϊ020301
        /// </summary>
        public static decimal SysTimer1 = 1;
        //ϵͳ���� ���Ϊ020302
        public static decimal SysTimer2 = 3;
        //ϵͳ���� ���Ϊ020303
        public static decimal SysTimer3 = 10;
        //ϵͳ���� ���Ϊ020304
        public static decimal SysTimer4 = 15;

        /// <summary>
        /// ϵͳ��������,��Ӧ���ݿ���Ϊ020305
        /// </summary>
        public static string SysTimerEndPlayWav = "awoke.WAV";

        /// <summary>
        /// �������ڵ���
        /// </summary>
        public static int MaxLevel = 10;

        /// <summary>
        /// ָʾ�Ƿ�ֻ������
        /// �̶�ֵֻ���ֶ�
        /// </summary>
        public static bool DefaultIsReadOnly = false;

        /// <summary>
        /// �Ƿ����л���,��ʵ���ǻ��棬���ڴ��ų־ô�����д��Ż�
        /// </summary>
        public static bool IsRunCache = false;

        /// <summary>
        /// �������ݼ�
        /// </summary>
        public static DataTable DtblDistrict ;

        /// <summary>
        /// �û���λ�������ݼ�
        /// </summary>
        public static DataTable DtblCheckCompany ;

        /// <summary>
        /// ��������ݼ�
        /// </summary>
        public static DataTable DtblChecker ;

        /// <summary>
        /// ����������λ�ͱ��쵥λ,�̶�ֵ
        /// </summary>
        public static string CompanyProperty0 = "���߶���";

        /// <summary>
        /// ���õ�λ����,�̶�ֵ��ʾ���쵥λ
        /// </summary>
        public static string CompanyProperty1 = "���쵥λ";

        /// <summary>
        /// ����������λ��ǩ,�̶�ֵ���ڱ�ʾ������λ
        /// </summary>
        public static string CompanyProperty2 = "������λ";

        /// <summary>
        /// �ָ��ַ�,�̶�ֵ
        /// </summary>
        public static string SplitStr = "/";

        /// <summary>
        ///�����������ʽ.��ͨ�����ݿ�����
        /// </summary>
        public static string FormatStrMachineCode = string.Empty;

        /// <summary>
        /// �ֹ������׼�������ʽ����ͨ�����ݿ�����
        /// </summary>
        public static string FormatStandardCode = string.Empty;

        /// <summary>
        /// ʳƷ��������
        /// </summary>
        public static int FoodCodeLevel = 5;
        public static int CompanyCodeLen = 5;
        public static int CompanyKindCodeLen = 3;

        /// <summary>
        /// ��������������
        /// </summary>
        public static int DistrictCodeLevel = 3;

        /// <summary>
        /// �����Ŀ���볤��,�̶�ֵ
        /// </summary>
        public static int CheckItemCodeLen = 5;

        /// <summary>
        /// ��׼�볤��,�̶�ֵ
        /// </summary>
        public static int StandardCodeLen = 4;

        /// <summary>
        /// �������볤��,�̶�ֵ
        /// </summary>
        public static int MachineCodeLen = 3;

        /// <summary>
        /// �û���λ���볤��
        /// </summary>
        public static int UserUnitCodeLevel = 4;

        /// <summary>
        /// �û����볤��
        /// </summary>
        public static int UserCodeLen = 2;

        /// <summary>
        /// ���쵥λ���볤��,�̶�ֵ6
        /// </summary>
        public static int CompanyStdCodeLen = 6;

        /// <summary>
        /// 
        /// </summary>
        public static int RecordCodeLen = 9;

        /// <summary>
        /// ��λ��׼�볤��
        /// </summary>
        public static int CompanyStdCodeLevel = 6;

        /// <summary>
        /// ��������ϸ�,�̶�ֵ Failure
        /// </summary>
        public static string ResultFailure = "���ϸ�";

        /// <summary>
        /// ������ϸ�,�̶�ֵ eligibility
        /// </summary>
        public static string ResultEligi = "�ϸ�";

        /// <summary>
        /// �û�Ĭ�ϵ�λ����
        /// </summary>
        public static string DefaultUserUnitCode = "0001";

        /// <summary>
        /// ʳƷ��������1
        /// </summary>
        public static string FoodType1 = "0000100001";

        /// <summary>
        /// ʳƷ��������2
        /// </summary>
        public static string FoodType2 = "0000100003";

        /// <summary>
        /// ʳƷ��������3
        /// </summary>
        public static string FoodType3 = "00001";

        /// <summary>
        /// �Ƿ���������ӣ�����ʶ����͵�����, Ĭ��trueΪ������,falseΪ�����
        /// </summary>
        public static bool IsDataLink = true;

        /// <summary>
        /// ��ǰ�汾,��ͨ�����ݿ�����,�޸�Ϊ������/�����棬��������ֶ�ҲҪ��ͬ
        /// </summary>
        public static string SystemVersion = "������"; //"������"��"������";

        /// <summary>
        /// ��ʶ�������ǩ,�̶�ֵ��
        /// Ӧ�ñ�����SystemVersion��ͬ��
        /// </summary>
        public static string LocalBaseVersion = "������";// "������";

        /// <summary>
        /// ��ҵ�汾��ǩ,�̶�ֵ
        /// </summary>
        public static string EnterpriseVersion = "��ҵ��";

        /// <summary>
        /// ϵͳӦ�ö������ͣ��ǣ�����,ʳҩ,ҩ��,ũҵ.����ͨ�����ݿ�����
        /// </summary>
        public static string ApplicationTag="����";

        /// <summary>
        /// ϵͳӦ��Ϊ�������ͱ�ǩ
        /// Industry and commerce Application Tag
        /// </summary>
        public static string ICAppTag = "����";

        /// <summary>
        /// ϵͳӦ��Ϊʳҩ���ͱ�ǩ
        /// Food and Drug Application Tag
        /// </summary>
        public static string FDAppTag = "ʳҩ";

        /// <summary>
        ///  ϵͳӦ��Ϊũ���
        /// Agriculture Inspection
        /// </summary>
        public static string AGRInsAppTag = "ũ��";

        #region ������������ز���

        /// <summary>
        /// ���ݿ������ַ���ǰ�벿�֣�Ϊ�������ϴ��������ز�ͬ���ݿ�֮����л�
        /// </summary>
        public static string DBConnStrPref = @"Provider = Microsoft.Jet.OLEDB.4.0.1;Data Source =";

        /// <summary>
        /// δ���ͱ�ǩ,�̶�ֵ
        /// </summary>
        public static string SendState0 = "δ����";

        /// <summary>
        /// �Ѿ����ͱ�ǩ,�̶�ֵ
        /// </summary>
        public static string SendState1 = "�ѷ���";

        /// <summary>
        ///����������IP ���Ϊ020201
        /// </summary>
        public static string SysServerIP = string.Empty;

        /// <summary>
        /// �����û��˺�,���Ϊ020202
        /// </summary>
        public static string SysServerID = string.Empty;

        /// <summary>
        /// �����û�����,ϵͳ���ñ��Ϊ020203
        /// </summary>
        public static string SysServerPass = string.Empty;

        /// <summary>
        /// ��ǰwebservice���ӽӿ�����,����ͨ�����ݿ�����
        /// </summary>
        public static string InterfaceType = "J2EE";

        /// <summary>
        /// J2EE�汾�ӿڱ�ǩ,�̶�ֵ
        /// </summary>
        public static string InterfaceJ2EE = "J2EE";

        /// <summary>
        /// NET�汾�ӿڱ�ǩ,�̶�ֵ
        /// </summary>
        public static string InterfaceDotNET = "NET";

        #endregion

        /// <summary>
        /// ������֯ ���� �����г�.
        /// ����ͨ�����ݿ�����
        /// </summary>
        public static string AreaTitle = string.Empty;

        /// <summary>
        /// �ܼ���/��λ  ���� ��λ����.
        /// ����ͨ�����ݿ�����
        /// </summary>
        public static string NameTitle = string.Empty;

        /// <summary>
        /// ����/����/���ƺ�  ���� λ��.
        /// ����ͨ�����ݿ�����
        /// </summary>
        public static string DomainTitle = string.Empty;

        /// <summary>
        /// ��Ʒ���� ���� ��Ʒ���� ����.����ͨ�����ݿ�����
        /// ����Ҫд"����"����
        /// </summary>
        public static string SampleTitle = string.Empty;

        #region ������ز���

        /// <summary>
        /// �ֶ����뷨,�̶�ֵ
        /// </summary>
        public static string ResultType1 = "������ֶ�";

        /// <summary>
        /// ��⿨����,�̶�ֵ
        /// </summary>
        public static string ResultType2 = "��⿨";

        /// <summary>
        /// ������ⷨ,�̶�ֵ
        /// </summary>
        public static string ResultType3 = "������ⷨ";

        /// <summary>
        /// ���Զ�����,�̶�ֵ
        /// </summary>
        public static string ResultType4 = "���Զ�����";

        /// <summary>
        /// �����Զ���ⷨ,�̶�ֵ
        /// </summary>
        public static string ResultType5 = "������Զ�";

        /// <summary>
        ///�����Ŀƴ���ַ�������
        ///��ӦtMachine����LinkStdCode�ֶ�
        /// </summary>
        public static string DefaultCheckItemCode = "0001";

        /// <summary>
        /// ��ǰ�������
        /// ��ӦtMachine����SysCode�ֶ�
        /// </summary>
        public static string DefaultMachineCode = "001";

        /// <summary>
        /// �������ں�,��ӦtMachine
        /// </summary>
        public static string ComPort = "COM1:";

        /// <summary>
        /// ��׼���ֵ����ӦtMachine����TestValue�ֶ�
        /// </summary>
        public static decimal DefaultLimitValue = 50;
     
        /// <summary>
        /// ��ʶ��ͬDY5000�汾����
        /// ��������,��LD��ʾ�׶Ȱ�.
        /// ����ͨ�����ݿ�����
        /// </summary>
        public static string DY5000Name = "DY5000LD";

        /// <summary>
        /// DY3000�汾���°�(DY3000DY)���߾ɰ�DY3000.
        /// ����ͨ�����ݿ�����
        /// </summary>
        public static string CurDY3000Tag = "DY3000";

        #endregion

        /// <summary>
        /// ������λ��ǩ����,���ݲ�ͬӦ�ó������ò�ͬ��ǩ����
        /// </summary>
        public static string ProductionUnitNameTag = "��Ӧ��";

        /// <summary>
        /// ���ܹ�ɨ��ʱ��������λΪ����
        /// </summary>
        public static int RockeyScanInterval = 10;

        //������ʶ�׶ȵ�DY5000��DY3000���Ѿ��������� 
        //�޸���:  2011-6-16

        //public static string frmDY3000Tag = "DY3000";
        //���������ֶ�û����
        //public static bool IsExistDY5000 = false;
        //public static bool IsExistDY5000LD = false;

        /////���´���û�����õĵط����Ѿ�ע����
        /////�޸���:,2011-06-22

        //public static int ResultTypeCodeLen = 3;
        //public static int ResultCodeLen = 5;
        //public static int MMI_MarketRegionCodeLen = 4;
        //public static int MMI_StallCodeLen = 10;

        //�����ֶθ�����ʲô��˼��������������û�еط��õ����õ��ĵط����κ����壬��ȥ��
        //public static string ResultTypeCode1 = "001";
        //public static string ResultTypeCode2 = "002";
        //public static string ResultTypeCode3 = "003";
        //public static string ResultTypeCode4 = "004";
        //public static string ResultTypeCode5 = "005";
        ///// <summary>
        /////Ĭ�Ϸ���
        ///// </summary>
        //public static string DefaultSign = "��";
        ///// <summary>
        ///// Ĭ�ϵ�λ
        ///// </summary>
        //public static string DefaultUnit = string.Empty;
        ///// <summary>
        ///// ϵͳ��ǰ����
        ///// </summary>
        //public static string SysCurMachine = string.Empty;
        ///// <summary>
        ///// �Ƿ�Ĭ��
        ///// </summary>
        //public static bool IsDefault = false;
    }
}

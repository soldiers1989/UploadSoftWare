using Microsoft.Win32.SafeHandles; 
using System;
using System.Runtime.InteropServices;

namespace Synoxo.USBHidDevice
{
    [StructLayout(LayoutKind.Sequential)]
    public struct HIDD_ATTRIBUTES
    {
        public Int32 Size;
        public UInt16 VendorID;
        public UInt16 ProductID;
        public UInt16 VersionNumber;
    }

    public struct HIDP_CAPS
    {
        public Int16 Usage;
        public Int16 UsagePage;
        public Int16 InputReportByteLength;
        public Int16 OutputReportByteLength;
        public Int16 FeatureReportByteLength;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public Int16[] Reserved;
        public Int16 NumberLinkCollectionNodes;
        public Int16 NumberInputButtonCaps;
        public Int16 NumberInputValueCaps;
        public Int16 NumberInputDataIndices;
        public Int16 NumberOutputButtonCaps;
        public Int16 NumberOutputValueCaps;
        public Int16 NumberOutputDataIndices;
        public Int16 NumberFeatureButtonCaps;
        public Int16 NumberFeatureValueCaps;
        public Int16 NumberFeatureDataIndices;
    }

    //  If IsRange is false, UsageMin is the Usage and UsageMax is unused.
    //  If IsStringRange is false, StringMin is the String index and StringMax is unused.
    //  If IsDesignatorRange is false, DesignatorMin is the designator index and DesignatorMax is unused.
    public struct HidP_Value_Caps
    {
        public Int16 UsagePage;
        public Byte ReportID;
        public Int32 IsAlias;
        public Int16 BitField;
        public Int16 LinkCollection;
        public Int16 LinkUsage;
        public Int16 LinkUsagePage;
        public Int32 IsRange;
        public Int32 IsStringRange;
        public Int32 IsDesignatorRange;
        public Int32 IsAbsolute;
        public Int32 HasNull;
        public Byte Reserved;
        public Int16 BitSize;
        public Int16 ReportCount;
        public Int16 Reserved2;
        public Int16 Reserved3;
        public Int16 Reserved4;
        public Int16 Reserved5;
        public Int16 Reserved6;
        public Int32 LogicalMin;
        public Int32 LogicalMax;
        public Int32 PhysicalMin;
        public Int32 PhysicalMax;
        public Int16 UsageMin;
        public Int16 UsageMax;
        public Int16 StringMin;
        public Int16 StringMax;
        public Int16 DesignatorMin;
        public Int16 DesignatorMax;
        public Int16 DataIndexMin;
        public Int16 DataIndexMax;
    }

    internal partial class Hid
    {
        //  API declarations for HID communications.

        //  from hidpi.h
        //  Typedef enum defines a set of integer constants for HidP_Report_Type

        public const Int16 HidP_Input = 0;
        public const Int16 HidP_Output = 1;
        public const Int16 HidP_Feature = 2;

        /// <summary>
        /// �Ƴ��ȴ��ڻ������еı���
        /// </summary>
        /// <param name="HidDeviceObject">ָ���豸�ľ��</param>
        /// <returns>�ɹ���True , ʧ�ܣ�False</returns>
        [DllImport("hid.dll", SetLastError = true)]
        public static extern Boolean HidD_FlushQueue(SafeFileHandle HidDeviceObject);

        /// <summary>
        /// �ͷ���HidD_GetPreparsedData��������Ļ�����
        /// </summary>
        /// <param name="PreparsedData">ָ��HidD_GetPreparsedData�������ص�PreparsedData���ݽṹָ��</param>
        /// <returns>�ɹ���True , ʧ�ܣ�False</returns>
        [DllImport("hid.dll", SetLastError = true)]
        public static extern Boolean HidD_FreePreparsedData(IntPtr PreparsedData);

        /// <summary>
        /// ��HID�豸��ȡ�������GUID��Retrieves the interface class GUID for the HID class.
        /// </summary>
        /// <param name="HidGuid">ϵͳ������豸��GUID�ַ�����A System.Guid object for storing the GUID</param>
        [DllImport("hid.dll", SetLastError = true)]
        public static extern void HidD_GetHidGuid(ref System.Guid HidGuid);

        /// <summary>
        /// ��ȡ�豸����
        /// </summary>
        /// <param name="HidDeviceObject">�豸���</param>
        /// <param name="Attributes">HIDD_ATTRIBUTES�ṹ</param>
        /// <returns>�ɹ���True , ʧ�ܣ�False</returns>
        [DllImport("hid.dll", SetLastError = true)]
        public static extern Boolean HidD_GetAttributes(SafeFileHandle HidDeviceObject, ref HIDD_ATTRIBUTES Attributes);

        /// <summary>
        /// Ŀ��: ��ȡ�豸���ܲ���
        /// ���ڱ�׼�豸�����磺�ֱ���������ҳ��豸���������
        /// �������֪�������Ŀͻ��豸������Ҫ�˺���
        /// </summary>
        /// <param name="PreparsedData">ָ��HidD_GetPreparsedData���صĽṹָ��</param>
        /// <param name="Capabilities">ָ��HIDP_CAPS structure�Ľṹָ��</param>
        /// <returns>�ɹ���True , ʧ�ܣ�False</returns>
        [DllImport("hid.dll", SetLastError = true)]
        public static extern Int32 HidP_GetCaps(IntPtr PreparsedData, ref HIDP_CAPS Capabilities);

        /// <summary>
        /// ���ŷ������Ա������豸��Attempts to send a Feature report to the device
        /// </summary>
        /// <param name="HidDeviceObject">HID�豸���</param>
        /// <param name="lpReportBuffer">ָ�򱨸�ID�ͱ�����Ļ�����ָ��</param>
        /// <param name="ReportBufferLength">��������С</param>
        /// <returns>�ɹ���True , ʧ�ܣ�False</returns>
        [DllImport("hid.dll", SetLastError = true)]
        public static extern Boolean HidD_SetFeature(SafeFileHandle HidDeviceObject, Byte[] lpReportBuffer, Int32 ReportBufferLength);

        /// <summary>
        /// ���Ŷ�ȡ�豸�ı��������
        /// </summary>
        /// <param name="HidDeviceObject">HID�豸���</param>
        /// <param name="lpReportBuffer">��������ID�����ݵĻ�����ָ��</param>
        /// <param name="ReportBufferLength">����������</param>
        /// <returns>�ɹ���True , ʧ�ܣ�False</returns>
        [DllImport("hid.dll", SetLastError = true)]
        public static extern Boolean HidD_GetFeature(SafeFileHandle HidDeviceObject, Byte[] lpReportBuffer, Int32 ReportBufferLength);

        /// <summary>
        /// �����ÿ��ƴ��䷢��������浽�豸
        /// ��ҪWin XP���Ժ�İ汾֧��
        /// </summary>
        /// <param name="HidDeviceObject">HID�豸���</param>
        /// <param name="lpReportBuffer">ָ�򱨸�ID�ͱ������ָ��</param>
        /// <param name="ReportBufferLength">���泤��</param>
        /// <returns>�ɹ���True , ʧ�ܣ�False</returns>
        [DllImport("hid.dll", SetLastError = true)]
        public static extern Boolean HidD_SetOutputReport(SafeFileHandle HidDeviceObject, Byte[] lpReportBuffer, Int32 ReportBufferLength);

        /// <summary>
        /// �ÿ��ƴ��䣨control transfer�����Ŵ�HID�豸��ȡ����ı��棬��֧��Windows XP�Ժ�İ汾
        /// </summary>
        /// <param name="HidDeviceObject">HID�豸���</param>
        /// <param name="lpReportBuffer">��������ID�����ݵĻ�����ָ��</param>
        /// <param name="ReportBufferLength">����������</param>
        /// <returns>�ɹ���True , ʧ�ܣ�False</returns>
        [DllImport("hid.dll", SetLastError = true)]
        public static extern Boolean HidD_GetInputReport(SafeFileHandle HidDeviceObject, Byte[] lpReportBuffer, Int32 ReportBufferLength);

        /// <summary>
        /// Sets the number of Input reports the host can store
        /// ���������ܴ洢�����뱨�������
        /// </summary>
        /// <param name="HidDeviceObject">HID�豸���</param>
        /// <param name="NumberBuffers">������������An integer to hold the number of buffers</param>
        /// <returns>�ɹ���True , ʧ�ܣ�False</returns>
        [DllImport("hid.dll", SetLastError = true)]
        public static extern Boolean HidD_SetNumInputBuffers(SafeFileHandle HidDeviceObject, Int32 NumberBuffers);

        /// <summary>
        /// ��ȡHost�ܴ洢�����뱨�����ֵ����֧��Windows 98 Gold��������������������µı��浽������򶪵��������ı���(ldest report)
        /// </summary>
        /// <param name="HidDeviceObject">HID�豸�����a handle to a device</param>
        /// <param name="NumberBuffers">���ֻ�������ֵ��������integer to hold the number of buffers</param>
        /// <returns>�ɹ���True , ʧ�ܣ�False</returns>
        [DllImport("hid.dll", SetLastError = true)]
        public static extern Boolean HidD_GetNumInputBuffers(SafeFileHandle HidDeviceObject, ref Int32 NumberBuffers);

        /// <summary>
        /// API����: HidD_GetPreparsedData
        /// Ŀ��: ��ȡָ���������������Ϣ�ṹ��ָ��.
        /// HidP_GetCaps ������ API ������Ҫһ��ָ�򻺳�����ָ��.
        /// </summary>
        /// <param name="HidDeviceObject">һ����CreateFile�������ص��ļ����</param>
        /// <param name="PreparsedData">һ��ָ��Buff��ָ��</param>
        /// <returns>�ɹ���True , ʧ�ܣ�False</returns>
        [DllImport("hid.dll", SetLastError = true)]
        public static extern Boolean HidD_GetPreparsedData(SafeFileHandle HidDeviceObject, ref IntPtr PreparsedData);

        /// <summary>
        /// ���»�ȡһ�� HidP_ValueCaps�ṹ���У�ÿ���ṹ����һ��ֵ���������˳���δ��
        /// retrieves a buffer containing an array of HidP_ValueCaps structures.
        ///  Each structure defines the capabilities of one value.
        ///  This application doesn't use this data.
        /// </summary>
        /// <param name="ReportType">��hidpi.h�еı���ö�����ͣ�A report type enumerator from hidpi.h</param>
        /// <param name="ValueCaps">�������л�����ָ�룻A pointer to a buffer for the returned array</param>
        /// <param name="ValueCapsLength">�豸��HidP_Caps�ṹ�洢����The NumberInputValueCaps member of the device's HidP_Caps structure</param>
        /// <param name="PreparsedData">��HidD_GetPreparsedData�������ص�PreparsedData���ݽṹָ�룬A pointer to the PreparsedData structure returned by HidD_GetPreparsedData</param>
        /// <returns>�ɹ���True , ʧ�ܣ�False</returns>
        [DllImport("hid.dll", SetLastError = true)]
        public static extern Int32 HidP_GetValueCaps(Int32 ReportType, Byte[] ValueCaps, ref Int32 ValueCapsLength, IntPtr PreparsedData);
    }
}

///  <summary>
///  Routines for detecting devices and receiving device notifications.
///  </summary>
  
using System;
using System.Runtime.InteropServices; 
using System.Windows.Forms;
using System.Diagnostics;

namespace Synoxo.USBHidDevice
{
    /// <summary>
    /// �����Control��������Ҫ���ǵ�Ҫ��Window���ϵͳ��Ϣ�ķ���
    /// �Ա�֪ͨ�û�USB����¼�
    /// </summary>
	public partial class DeviceManagement
	{
		///  <summary>
		///  �Ƚ��豸��·���������ڷ���������ӵĻ����Ƴ�������ͨѶ��ƥ���豸����
        ///  Compares two device path names. Used to find out if the device name 
		///  of a recently attached or removed device matches the name of a 
		///  device the application is communicating with.
		///  </summary>
		///  <param name="m"> һ�� WM_DEVICECHANGE ��Ϣ. ���� RegisterDeviceNotification ����
		///  �Ա� WM_DEVICECHANGE ��Ϣ���Դ��ݵ� OnDeviceChange ���г���.. </param>
		///  <param name="mydevicePathName"> a device pathname returned by 
		///  SetupDiGetDeviceInterfaceDetail in an SP_DEVICE_INTERFACE_DETAIL_DATA structure. </param>
		///  <returns>
		///  ����ƥ�䷵��True�����򷵻�False
		///  </returns>
		public Boolean DeviceNameMatch(Message m, String mydevicePathName)
		{
			Int32 stringSize;

			try
			{
				DEV_BROADCAST_DEVICEINTERFACE_1 devBroadcastDeviceInterface = new DEV_BROADCAST_DEVICEINTERFACE_1();
				DEV_BROADCAST_HDR devBroadcastHeader = new DEV_BROADCAST_HDR();

				// Message �� LParam ����  is ָ�� DEV_BROADCAST_HDR �ṹ��ָ��.
				Marshal.PtrToStructure(m.LParam, devBroadcastHeader);

				if ((devBroadcastHeader.dbch_devicetype == DBT_DEVTYP_DEVICEINTERFACE))
				{
					// dbch_devicetype ����ָ����Ӧ���豸�Ľӿ� 
					// ��� LParam ����ʵ������DEV_BROADCAST_HDR��ʼ�� DEV_BROADCAST_INTERFACE�ṹ  

					// ͨ����ȥ�ṹ�в���dbch_name���ֵ�32�ֽں�õ����ַ��ĸ���������һ���ַ������ֽڣ������ٳ���2
					stringSize = System.Convert.ToInt32((devBroadcastHeader.dbch_size - 32) / 2);

					//  devBroadcastDeviceInterface�е�dbcc_name �����������豸���� 
					// ȥ��ǰ��Ŀո���ƥ���ַ�������         
					devBroadcastDeviceInterface.dbcc_name = new Char[stringSize + 1];

					// �����ݴӲ��ܹ�������ݿ鴫�䵽�ܹ����devBroadcastDeviceInterface������
					Marshal.PtrToStructure(m.LParam, devBroadcastDeviceInterface);

					// ���豸����String���ʹ洢
					String DeviceNameString = new String(devBroadcastDeviceInterface.dbcc_name, 0, stringSize);

					// �Ƚ������ӵ��豸���ֺ�������֪���豸(mydevicePathName)���֣�ƥ�䷵��true
					if ((String.Compare(DeviceNameString, mydevicePathName, true) == 0))
					{
						return true;
					}
					else
					{
						return false;
					}
				}
			}
			catch (Exception ex)
			{
				throw;
			}

			return false;
		}

		///  <summary>
		///  Use SetupDi API functions to retrieve the device path name of an
		///  attached device that belongs to a device interface class.
		///  </summary>
		///  <param name="myGuid"> an interface class GUID. </param>
		///  <param name="devicePathName"> a pointer to the device path name of an attached device. </param>
		///  <returns>	True if a device is found, False if not</returns>
        public Boolean FindDeviceFromGuid(System.Guid myGuid, ref String[] devicePathName)
        {
            Int32 bufferSize = 0;
            IntPtr detailDataBuffer = IntPtr.Zero;
            Boolean deviceFound;
            IntPtr deviceInfoSet = new System.IntPtr();
            Boolean lastDevice = false;
            Int32 memberIndex = 0;
            SP_DEVICE_INTERFACE_DATA MyDeviceInterfaceData = new SP_DEVICE_INTERFACE_DATA();
            Boolean success;

            try
            {
                //  ����API ����
                deviceInfoSet = SetupDiGetClassDevs(ref myGuid, IntPtr.Zero, IntPtr.Zero, DIGCF_PRESENT | DIGCF_DEVICEINTERFACE);
                Debug.WriteLine("�ں���FindDeviceFromGuid��" + this.MyDebugging.ResultOfAPICall("SetupDiGetClassDevs"));

                deviceFound = false;
                memberIndex = 0;

                // The cbSize element of the MyDeviceInterfaceData structure must be set to
                // the structure's size in bytes. 
                // The size is 28 bytes for 32-bit code and 32 bits for 64-bit code.
                MyDeviceInterfaceData.cbSize = Marshal.SizeOf(MyDeviceInterfaceData);

                do
                {
                    // Begin with 0 and increment through the device information set until no more devices are available.

                    // ����API����
                    success = SetupDiEnumDeviceInterfaces(deviceInfoSet, IntPtr.Zero, ref myGuid, memberIndex, ref MyDeviceInterfaceData);

                    Debug.WriteLine("�ں���FindDeviceFromGuid��" + this.MyDebugging.ResultOfAPICall("SetupDiEnumDeviceInterfaces"));
                    // Find out if a device information set was retrieved.
                    if (!success)
                    {
                        lastDevice = true;
                    }
                    else
                    {
                        // ����API���� 
                        success = SetupDiGetDeviceInterfaceDetail(deviceInfoSet, ref MyDeviceInterfaceData, IntPtr.Zero, 0, ref bufferSize, IntPtr.Zero);
                        Debug.WriteLine("�ں���FindDeviceFromGuid��" + this.MyDebugging.ResultOfAPICall("SetupDiGetDeviceInterfaceDetail"));

                        // ʹ�÷��صĻ�������СΪSP_DEVICE_INTERFACE_DETAIL_DATA�ṹ�����ڴ�
                        detailDataBuffer = Marshal.AllocHGlobal(bufferSize);

                        // Store cbSize in the first bytes of the array. The number of bytes varies with 32- and 64-bit systems.
                        Marshal.WriteInt32(detailDataBuffer, (IntPtr.Size == 4) ? (4 + Marshal.SystemDefaultCharSize) : 8);

                        // �ٴε���SetupDiGetDeviceInterfaceDetail ��������δ���һ��ָ���DetailDataBuffer����������Ļ�������С
                        success = SetupDiGetDeviceInterfaceDetail(deviceInfoSet, ref MyDeviceInterfaceData, detailDataBuffer, bufferSize, ref bufferSize, IntPtr.Zero);
                        Debug.WriteLine("�ں���FindDeviceFromGuid��" + this.MyDebugging.ResultOfAPICall("SetupDiGetDeviceInterfaceDetail"));

                        // �Թ� cbsize (4 bytes) �Ի��devicePathName�ĵ�ַ
                        IntPtr pDevicePathName = new IntPtr(detailDataBuffer.ToInt32() + 4);

                        // ��ȡdevicePathName�ַ���.
                        devicePathName[memberIndex] = Marshal.PtrToStringAuto(pDevicePathName);

                        if (detailDataBuffer != IntPtr.Zero)
                        {
                            // �ͷ��� AllocHGlobal ������ڴ�
                            Marshal.FreeHGlobal(detailDataBuffer);
                        }
                        deviceFound = true;
                    }
                    memberIndex++;
                }
                while (!((lastDevice == true)));

                return deviceFound;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //  ����API�����ͷ��ڴ�
                if (deviceInfoSet != IntPtr.Zero)
                {
                    SetupDiDestroyDeviceInfoList(deviceInfoSet);
                    Debug.WriteLine("�ں���FindDeviceFromGuid��" + this.MyDebugging.ResultOfAPICall("SetupDiDestroyDeviceInfoList"));
                }
            }
        }

		///  <summary>
		///  ���豸�����γ�ʱ���������Ϣ
        ///  Requests to receive a notification when a device is attached or removed.
		///  </summary>
		///  <param name="devicePathName"> �豸��� handle to a device. </param>
		///  <param name="formHandle"> �����¼��Ĵ����� handle to the window that will receive device events. </param>
		///  <param name="classGuid"> �豸�ӿ� GUID device interface GUID. </param>
		///  <param name="deviceNotificationHandle"> �����豸�¼���� returned device notification handle. </param>
		///  <returns>
		///  �ɹ�����True
		///  </returns>
		public Boolean RegisterForDeviceNotifications(String devicePathName, IntPtr formHandle, Guid classGuid, ref IntPtr deviceNotificationHandle)
		{
			// A DEV_BROADCAST_DEVICEINTERFACE header holds information about the request.
			DEV_BROADCAST_DEVICEINTERFACE devBroadcastDeviceInterface = new DEV_BROADCAST_DEVICEINTERFACE();
			IntPtr devBroadcastDeviceInterfaceBuffer = IntPtr.Zero;
			Int32 size = 0;

			try
			{
				// Set the parameters in the DEV_BROADCAST_DEVICEINTERFACE structure.
				// Set the size.
				size = Marshal.SizeOf(devBroadcastDeviceInterface);
				devBroadcastDeviceInterface.dbcc_size = size;

				// Request to receive notifications about a class of devices.
				devBroadcastDeviceInterface.dbcc_devicetype = DBT_DEVTYP_DEVICEINTERFACE;

				devBroadcastDeviceInterface.dbcc_reserved = 0;

				// Specify the interface class to receive notifications about.
				devBroadcastDeviceInterface.dbcc_classguid = classGuid;

				// Allocate memory for the buffer that holds the DEV_BROADCAST_DEVICEINTERFACE structure.
				devBroadcastDeviceInterfaceBuffer = Marshal.AllocHGlobal(size);

				// Copy the DEV_BROADCAST_DEVICEINTERFACE structure to the buffer.
				// Set fDeleteOld True to prevent memory leaks.
				Marshal.StructureToPtr(devBroadcastDeviceInterface, devBroadcastDeviceInterfaceBuffer, true);

                // ����API����
				deviceNotificationHandle = RegisterDeviceNotification(formHandle, devBroadcastDeviceInterfaceBuffer, DEVICE_NOTIFY_WINDOW_HANDLE);
                Debug.WriteLine("�ں���RegisterForDeviceNotifications��" + this.MyDebugging.ResultOfAPICall("RegisterDeviceNotification"));

				// �ӷǹ����devBroadcastDeviceInterfaceBuffer���ݿ鼯�ϵ��ܹ����devBroadcastDeviceInterface���� 
                // Marshal data from the unmanaged block devBroadcastDeviceInterfaceBuffer to the managed object devBroadcastDeviceInterface
				Marshal.PtrToStructure(devBroadcastDeviceInterfaceBuffer, devBroadcastDeviceInterface);

				if ((deviceNotificationHandle.ToInt32() == IntPtr.Zero.ToInt32()))
				{
					return false;
				}
				else
				{
					return true;
				}
			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
				if (devBroadcastDeviceInterfaceBuffer != IntPtr.Zero)
				{
					// �ͷ���AllocHGlobal����������ڴ�ռ䡣 Free the memory allocated previously by AllocHGlobal.
					Marshal.FreeHGlobal(devBroadcastDeviceInterfaceBuffer);
				}
			}
		}

		///  <summary>
		///  Requests to stop receiving notification messages when a device in an
		///  interface class is attached or removed.
		///  </summary>
		///  <param name="deviceNotificationHandle"> handle returned previously by
		///  RegisterDeviceNotification. </param>
		public void StopReceivingDeviceNotifications(IntPtr deviceNotificationHandle)
		{
			try
			{
				//  ����API����
				DeviceManagement.UnregisterDeviceNotification(deviceNotificationHandle);
                Debug.WriteLine("�ں���StopReceivingDeviceNotifications��" + this.MyDebugging.ResultOfAPICall("UnregisterDeviceNotification"));
            }
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}






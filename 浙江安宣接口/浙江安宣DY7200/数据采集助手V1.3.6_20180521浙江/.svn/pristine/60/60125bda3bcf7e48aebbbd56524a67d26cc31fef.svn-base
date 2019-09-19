using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Text.RegularExpressions;

namespace WorkstationModel.HID
{
    public class hidSearch
    {
        public struct PnPEntityInfo
        {
            public String PNPDeviceID;      // 设备ID
            public String Name;             // 设备名称
            public String Description;      // 设备描述
            public String Service;          // 服务
            public String Status;           // 设备状态
            public UInt16 VendorID;         // 供应商标识
            public UInt16 ProductID;        // 产品编号 
            public Guid ClassGuid;          // 设备安装类GUID
        }

        #region UsbDevice
        /// <summary>
        /// 获取所有的USB设备实体（过滤没有VID和PID的设备）
        /// </summary>
        public static PnPEntityInfo[] AllUsbDevices
        {
            get
            {
                return WhoUsbDevice(UInt16.MinValue, UInt16.MinValue, Guid.Empty);
            }
        }

        /// <summary>
        /// 查询USB设备实体（设备要求有VID和PID）
        /// </summary>
        /// <param name="VendorID">供应商标识，MinValue忽视</param>
        /// <param name="ProductID">产品编号，MinValue忽视</param>
        /// <param name="ClassGuid">设备安装类Guid，Empty忽视</param>
        /// <returns>设备列表</returns>

        public static int numb;//统计usb设备数量   

        public static PnPEntityInfo[] WhoUsbDevice(UInt16 VendorID, UInt16 ProductID, Guid ClassGuid)
        {
            numb = 0;

            List<PnPEntityInfo> UsbDevices = new List<PnPEntityInfo>();

            // 获取USB控制器及其相关联的设备实体
            ManagementObjectCollection USBControllerDeviceCollection = new ManagementObjectSearcher("SELECT * FROM Win32_USBControllerDevice").Get();
            if (USBControllerDeviceCollection != null)
            {
                foreach (ManagementObject USBControllerDevice in USBControllerDeviceCollection)
                {   // 获取设备实体的DeviceID
                    String Dependent = (USBControllerDevice["Dependent"] as String).Split(new Char[] { '=' })[1];

                    // 过滤掉没有VID和PID的USB设备
                    Match match = Regex.Match(Dependent, "VID_[0-9|A-F]{4}&PID_[0-9|A-F]{4}");
                    if (match.Success)
                    {
                        UInt16 theVendorID = Convert.ToUInt16(match.Value.Substring(4, 4), 16);   // 供应商标识
                        if (VendorID != UInt16.MinValue && VendorID != theVendorID) continue;

                        UInt16 theProductID = Convert.ToUInt16(match.Value.Substring(13, 4), 16); // 产品编号
                        if (ProductID != UInt16.MinValue && ProductID != theProductID) continue;

                        ManagementObjectCollection PnPEntityCollection = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE DeviceID=" + Dependent).Get();
                        if (PnPEntityCollection != null)
                        {
                            foreach (ManagementObject Entity in PnPEntityCollection)
                            {
                                //Guid theClassGuid = new Guid(Entity["ClassGuid"] as String);    // 设备安装类GUID
                                //if (ClassGuid != Guid.Empty && ClassGuid != theClassGuid) continue;

                                PnPEntityInfo Element;
                                Element.PNPDeviceID = Entity["PNPDeviceID"] as String;  // 设备ID
                                Element.Name = Entity["Name"] as String;                // 设备名称
                                Element.Description = Entity["Description"] as String;  // 设备描述
                                Element.Service = Entity["Service"] as String;          // 服务
                                Element.Status = Entity["Status"] as String;            // 设备状态
                                Element.VendorID = theVendorID;     // 供应商标识
                                Element.ProductID = theProductID;   // 产品编号
                                Element.ClassGuid = Guid.Empty;//theClassGuid;   // 设备安装类GUID

                                UsbDevices.Add(Element);

                                numb = numb + 1;
                            }
                        }
                    }
                }
            }

            if (UsbDevices.Count == 0) return null; else return UsbDevices.ToArray();
        }


        #endregion
    }
}

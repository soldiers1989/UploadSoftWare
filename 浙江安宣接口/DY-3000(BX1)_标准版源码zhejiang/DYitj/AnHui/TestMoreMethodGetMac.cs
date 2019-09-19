using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;

namespace AIO.AnHui
{
    public class TestMoreMethodGetMac
    {
        public enum NCBCONST
        {
            NCBNAMSZ = 16,
            MAX_LANA = 254,
            NCBENUM = 0x37,
            NRC_GOODRET = 0x00,
            NCBRESET = 0x32,
            NCBASTAT = 0x33,
            NUM_NAMEBUF = 30,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ADAPTER_STATUS
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] adapter_address;
            public byte rev_major;
            public byte reserved0;
            public byte adapter_type;
            public byte rev_minor;
            public ushort duration;
            public ushort frmr_recv;
            public ushort frmr_xmit;
            public ushort iframe_recv_err;
            public ushort xmit_aborts;
            public uint xmit_success;
            public uint recv_success;
            public ushort iframe_xmit_err;
            public ushort recv_buff_unavail;
            public ushort t1_timeouts;
            public ushort ti_timeouts;
            public uint reserved1;
            public ushort free_ncbs;
            public ushort max_cfg_ncbs;
            public ushort max_ncbs;
            public ushort xmit_buf_unavail;
            public ushort max_dgram_size;
            public ushort pending_sess;
            public ushort max_cfg_sess;
            public ushort max_sess;
            public ushort max_sess_pkt_size;
            public ushort name_count;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NAME_BUFFER
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)NCBCONST.NCBNAMSZ)]
            public byte[] name;
            public byte name_num;
            public byte name_flags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NCB
        {
            public byte ncb_command;
            public byte ncb_retcode;
            public byte ncb_lsn;
            public byte ncb_num;
            public IntPtr ncb_buffer;
            public ushort ncb_length;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)NCBCONST.NCBNAMSZ)]
            public byte[] ncb_callname;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)NCBCONST.NCBNAMSZ)]
            public byte[] ncb_name;
            public byte ncb_rto;
            public byte ncb_sto;
            public IntPtr ncb_post;
            public byte ncb_lana_num;
            public byte ncb_cmd_cplt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] ncb_reserve;
            public IntPtr ncb_event;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct LANA_ENUM
        {
            public byte length;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)NCBCONST.MAX_LANA)]
            public byte[] lana;
        }

        [StructLayout(LayoutKind.Auto)]
        public struct ASTAT
        {
            public ADAPTER_STATUS adapt;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)NCBCONST.NUM_NAMEBUF)]
            public NAME_BUFFER[] NameBuff;
        }
        public class Win32API
        {
            [DllImport("NETAPI32.DLL")]
            public static extern char Netbios(ref   NCB ncb);
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class IP_Adapter_Addresses
        {
            public uint Length;
            public uint IfIndex;
            public IntPtr Next;
            public IntPtr AdapterName;
            public IntPtr FirstUnicastAddress;
            public IntPtr FirstAnycastAddress;
            public IntPtr FirstMulticastAddress;
            public IntPtr FirstDnsServerAddress;
            public IntPtr DnsSuffix;
            public IntPtr Description;
            public IntPtr FriendlyName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public Byte[] PhysicalAddress;
            public uint PhysicalAddressLength;
            public uint flags;
            public uint Mtu;
            public uint IfType;
            public uint OperStatus;
            public uint Ipv6IfIndex;
            public uint ZoneIndices;
            public IntPtr FirstPrefix;
        }
        /// <summary>
        /// 获取MAC地址的几种方法
        /// 作者：yys(Sam Young)
        /// 日期：2011-12-21
        /// </summary>
        public class MoreMethodGetMAC
        {
            /// <summary>
            /// 获取PC所有MAC地址
            /// </summary>
            /// <returns></returns>
            public List<string> GetMacAddressList()
            {
                List<string> macList = new List<string>();
                try
                {
                    string mac = GetMacAddressByWMI();
                    if (!mac.Equals(""))
                    {
                        macList.Add(mac);
                    }
                    mac = GetMacAddressByNetBios();
                    if (!mac.Equals(""))
                    {
                        macList.Add(mac);
                    }
                    mac = GetMacAddressBySendARP();
                    if (!mac.Equals(""))
                    {
                        macList.Add(mac);
                    }
                    mac = GetMacAddressByAdapter();
                    if (!mac.Equals(""))
                    {
                        macList.Add(mac);
                    }
                    mac = GetMacAddressByDos();
                    if (!mac.Equals(""))
                    {
                        macList.Add(mac);
                    }
                }
                catch (Exception)
                {

                }
                return macList;
            }

            /// <summary>
            /// 通过WMI获得电脑的mac地址
            /// </summary>
            /// <returns></returns>
            public string GetMacAddressByWMI()
            {
                string mac = "";
                //try
                //{
                //    ManagementObjectSearcher query = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = 'TRUE'");
                //    ManagementObjectCollection queryCollection = query.Get();

                //    foreach (ManagementObject mo in queryCollection)
                //    {
                //        if (mo["IPEnabled"].ToString() == "True")
                //        {
                //            mac = mo["MacAddress"].ToString();
                //            break;
                //        }
                //    }
                //}
                //catch (Exception)
                //{

                //}
                return mac;
            }

            [DllImport("Iphlpapi.dll")]
            static extern int SendARP(Int32 DestIP, Int32 SrcIP, ref Int64 MacAddr, ref Int32 PhyAddrLen);
            /// <summary>
            /// SendArp获取MAC地址
            /// </summary>
            /// <returns></returns>
            public string GetMacAddressBySendARP()
            {
                StringBuilder strReturn = new StringBuilder();
                try
                {
                    System.Net.IPHostEntry Tempaddr = (System.Net.IPHostEntry)Dns.GetHostEntry(Dns.GetHostName());
                    System.Net.IPAddress[] TempAd = Tempaddr.AddressList;
                    Int32 remote = (int)TempAd[0].AddressFamily;
                    Int64 macinfo = new Int64();
                    Int32 length = 6;
                    SendARP(remote, 0, ref macinfo, ref length);

                    string temp = System.Convert.ToString(macinfo, 16).PadLeft(12, '0').ToUpper();

                    int x = 12;
                    for (int i = 0; i < 6; i++)
                    {
                        if (i == 5) { strReturn.Append(temp.Substring(x - 2, 2)); }
                        else { strReturn.Append(temp.Substring(x - 2, 2) + ":"); }
                        x -= 2;
                    }

                    return strReturn.ToString();
                }
                catch
                {
                    return "";
                }
            }

            [DllImport("Iphlpapi.dll")]
            public static extern uint GetAdaptersAddresses(uint Family, uint flags, IntPtr Reserved,
                IntPtr PAdaptersAddresses, ref uint pOutBufLen);

            /// <summary>
            /// 通过适配器信息获取MAC地址
            /// </summary>
            /// <returns></returns>
            public string GetMacAddressByAdapter()
            {
                string macAddress = "";
                try
                {
                    IntPtr PAdaptersAddresses = new IntPtr();

                    uint pOutLen = 100;
                    PAdaptersAddresses = Marshal.AllocHGlobal(100);

                    uint ret =
                        GetAdaptersAddresses(0, 0, (IntPtr)0, PAdaptersAddresses, ref pOutLen);

                    if (ret == 111)
                    {
                        Marshal.FreeHGlobal(PAdaptersAddresses);
                        PAdaptersAddresses = Marshal.AllocHGlobal((int)pOutLen);
                        ret = GetAdaptersAddresses(0, 0, (IntPtr)0, PAdaptersAddresses, ref pOutLen);
                    }

                    IP_Adapter_Addresses adds = new IP_Adapter_Addresses();

                    IntPtr pTemp = PAdaptersAddresses;

                    while (pTemp != (IntPtr)0)
                    {
                        Marshal.PtrToStructure(pTemp, adds);
                        string adapterName = Marshal.PtrToStringAnsi(adds.AdapterName);
                        string FriendlyName = Marshal.PtrToStringAuto(adds.FriendlyName);
                        string tmpString = string.Empty;

                        for (int i = 0; i < 6; i++)
                        {
                            tmpString += string.Format("{0:X2}", adds.PhysicalAddress[i]);

                            if (i < 5)
                            {
                                tmpString += ":";
                            }
                        }


                        RegistryKey theLocalMachine = Registry.LocalMachine;

                        RegistryKey theSystem
                            = theLocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces");
                        RegistryKey theInterfaceKey = theSystem.OpenSubKey(adapterName);

                        if (theInterfaceKey != null)
                        {
                            macAddress = tmpString;
                            break;
                        }

                        pTemp = adds.Next;
                    }
                }
                catch
                { }
                return macAddress;
            }

            /// <summary>
            /// 通过NetBios获取MAC地址
            /// </summary>
            /// <returns></returns>
            public string GetMacAddressByNetBios()
            {
                string macAddress = "";
                try
                {
                    string addr = "";
                    int cb;
                    ASTAT adapter;
                    NCB Ncb = new NCB();
                    char uRetCode;
                    LANA_ENUM lenum;

                    Ncb.ncb_command = (byte)NCBCONST.NCBENUM;
                    cb = Marshal.SizeOf(typeof(LANA_ENUM));
                    Ncb.ncb_buffer = Marshal.AllocHGlobal(cb);
                    Ncb.ncb_length = (ushort)cb;
                    uRetCode = Win32API.Netbios(ref   Ncb);
                    lenum = (LANA_ENUM)Marshal.PtrToStructure(Ncb.ncb_buffer, typeof(LANA_ENUM));
                    Marshal.FreeHGlobal(Ncb.ncb_buffer);
                    if (uRetCode != (short)NCBCONST.NRC_GOODRET)
                        return "";

                    for (int i = 0; i < lenum.length; i++)
                    {
                        Ncb.ncb_command = (byte)NCBCONST.NCBRESET;
                        Ncb.ncb_lana_num = lenum.lana[i];
                        uRetCode = Win32API.Netbios(ref   Ncb);
                        if (uRetCode != (short)NCBCONST.NRC_GOODRET)
                            return "";

                        Ncb.ncb_command = (byte)NCBCONST.NCBASTAT;
                        Ncb.ncb_lana_num = lenum.lana[i];
                        Ncb.ncb_callname[0] = (byte)'*';
                        cb = Marshal.SizeOf(typeof(ADAPTER_STATUS)) + Marshal.SizeOf(typeof(NAME_BUFFER)) * (int)NCBCONST.NUM_NAMEBUF;
                        Ncb.ncb_buffer = Marshal.AllocHGlobal(cb);
                        Ncb.ncb_length = (ushort)cb;
                        uRetCode = Win32API.Netbios(ref   Ncb);
                        adapter.adapt = (ADAPTER_STATUS)Marshal.PtrToStructure(Ncb.ncb_buffer, typeof(ADAPTER_STATUS));
                        Marshal.FreeHGlobal(Ncb.ncb_buffer);

                        if (uRetCode == (short)NCBCONST.NRC_GOODRET)
                        {
                            if (i > 0)
                                addr += ":";
                            addr = string.Format("{0,2:X}:{1,2:X}:{2,2:X}:{3,2:X}:{4,2:X}:{5,2:X}",
                                  adapter.adapt.adapter_address[0],
                                  adapter.adapt.adapter_address[1],
                                  adapter.adapt.adapter_address[2],
                                  adapter.adapt.adapter_address[3],
                                  adapter.adapt.adapter_address[4],
                                  adapter.adapt.adapter_address[5]);
                        }
                    }
                    macAddress = addr.Replace(' ', '0');

                }
                catch
                {

                }

                return macAddress;
            }

            /// <summary>
            /// 通过DOS命令获得MAC地址
            /// </summary>
            /// <returns></returns>
            public string GetMacAddressByDos()
            {
                string macAddress = "";
                Process p = null;
                StreamReader reader = null;
                try
                {
                    ProcessStartInfo start = new ProcessStartInfo("cmd.exe");

                    start.FileName = "ipconfig";
                    start.Arguments = "/all";

                    start.CreateNoWindow = true;

                    start.RedirectStandardOutput = true;

                    start.RedirectStandardInput = true;

                    start.UseShellExecute = false;

                    p = Process.Start(start);

                    reader = p.StandardOutput;

                    string line = reader.ReadLine();

                    while (!reader.EndOfStream)
                    {
                        if (line.ToLower().IndexOf("physical address") > 0 || line.ToLower().IndexOf("物理地址") > 0)
                        {
                            int index = line.IndexOf(":");
                            index += 2;
                            macAddress = line.Substring(index);
                            macAddress = macAddress.Replace('-', ':');
                            break;
                        }
                        line = reader.ReadLine();
                    }
                }
                catch
                {

                }
                finally
                {
                    if (p != null)
                    {
                        p.WaitForExit();
                        p.Close();
                    }
                    if (reader != null)
                    {
                        reader.Close();
                    }
                }
                return macAddress;
            }

            /// <summary>
            /// 通过网络适配器获取MAC地址
            /// </summary>
            /// <returns></returns>
            public string GetMacAddressByNetworkInformation()
            {
                string macAddress = "";
                try
                {
                    NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                    foreach (NetworkInterface adapter in nics)
                    {
                        if (!adapter.GetPhysicalAddress().ToString().Equals(""))
                        {
                            macAddress = adapter.GetPhysicalAddress().ToString();
                            for (int i = 1; i < 6; i++)
                            {
                                macAddress = macAddress.Insert(3 * i - 1, ":");
                            }
                            break;
                        }
                    }
                }
                catch
                {
                }
                return macAddress;
            }
        }

    }
}

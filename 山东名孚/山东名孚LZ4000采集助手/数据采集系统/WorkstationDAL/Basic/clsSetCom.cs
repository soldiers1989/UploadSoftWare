using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkstationDAL.Basic
{
    public class clsSetCom
    {
        private string _COM_Baud = string.Empty;
        private string _COM_Stop = string.Empty;
        private string _COM_Parity = string.Empty;
        private string _COM_Databit = string.Empty;
        private string _COM_Port = string.Empty;

        //串口波特率
        public string ComBaud
        {
            set
            {
                _COM_Baud = value;
            }
            get
            {
                return _COM_Baud;
            }
        }
        //串口停止位
        public string ComStopBit
        {
            set
            {
                _COM_Stop = value;
            }
            get
            {
                return _COM_Stop;
            }
        }
        //串口校验位
        public string ComParity
        {
            set
            {
                _COM_Parity = value;
            }
            get
            {
                return _COM_Parity;
            }
        }
        //串口数据位
        public string ComDataBit
        {
            set
            {
                _COM_Databit = value;
            }
            get
            {
                return _COM_Databit;
            }
        }
        //串口端口号
        public string ComPort
        {
            set
            {
                _COM_Port = value;
            }
            get
            {
                return _COM_Port;
            }
        }

        /// <summary> 
        /// DataTable装换为泛型集合 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="p_DataSet">DataSet</param> 
        /// <param name="p_TableName">待转换数据表名称</param> 
        /// <returns></returns> 
        /// 2015年12月10日 wenj 
        //public static IList<T> DataTableToIList<T>(DataTable p_DataSet, int p_TableName)
        //{
        //    List<T> list = new List<T>();
        //    T t = default(T);
        //    PropertyInfo[] propertypes = null;
        //    string tempName = string.Empty;
        //    foreach (DataRow row in p_DataSet.Rows)
        //    {
        //        t = Activator.CreateInstance<T>();
        //        propertypes = t.GetType().GetProperties();
        //        foreach (PropertyInfo pro in propertypes)
        //        {
        //            tempName = pro.Name;
        //            if (p_DataSet.Columns.Contains(tempName))
        //            {
        //                object value = row[tempName];
        //                if (!value.ToString().Equals(""))
        //                {
        //                    pro.SetValue(t, value, null);
        //                }
        //            }
        //        }
        //        list.Add(t);
        //    }
        //    return list.Count == 0 ? null : list;
        //}
    }
}

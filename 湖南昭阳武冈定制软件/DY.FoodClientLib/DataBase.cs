using System;
using System.Text;
using System.Data;
using System.Data.OleDb;
 
namespace DY.FoodClientLib
{
	/// <summary>
        /// ���ݿ����������
        /// �޸ģ��¹��� 2011-06-05
        /// �޸ļӼ̳�IDisposable�ӿڣ��ͷŷ��й���Դ
	/// </summary>
	public class DataBase:IDisposable
	{
        /// <summary>
        /// �ṩ��̬��������ֹ�ⲿ����
        /// </summary>
        private DataBase()
        {
        }

        //��ʵ����д�������ļ���
        private static readonly string password = "dy05xl378";
        private static readonly string dataName = "local.Mdb";
        private static readonly string getLocalDBPathString = string.Format("{0}Data\\{1}", AppDomain.CurrentDomain.BaseDirectory, dataName);
        private static readonly string defCnnString = string.Format("Provider = Microsoft.Jet.OLEDB.4.0.1;Data Source ={0};Persist Security Info=False;Jet OLEDB:Database Password={1}", getLocalDBPathString, password);

        private static OleDbConnection conn = null;
        private static OleDbCommand cmd = null;

        /// <summary>
        /// ʵ�ֽӿ�Dispose�ķ���
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                Close();
                if (cmd != null)
                {
                    cmd.Dispose();
                    cmd = null;
                }
                if (conn != null)
                {
                    conn.Dispose();
                    conn = null;
                }
            }
        }
        private static void Open()
        {
            if (conn != null && conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
        }
        private void Close()
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        ///// <summary>
        ///// ��ȡ�����ַ���
        ///// </summary>
        ///// <returns></returns>
        //public static string GetConnectString()
        //{
        //    string defConnString = string.Format("Provider = Microsoft.Jet.OLEDB.4.0.1;Data Source ={0};Persist Security Info=False;Jet OLEDB:Database Password={1}", GetLocalDBPathString, password);
        //    return defConnString;
        //}

        ///// <summary>
        ///// ��ȡ���ݿ��·���������ݿ�����
        ///// </summary>
        ///// <returns></returns>
        //public static string GetLocalDBPathString()
        //{
        //    string path = string.Format("{0}Data\\{1}", AppDomain.CurrentDomain.BaseDirectory, dataName);
        //    return path;
        //}

        /// <summary>
        /// ͨ����һ���ֵ
        /// </summary>
        /// <param name="cmdText">��ѯ����</param>
        /// <param name="errMsg">������Ϣ</param>
        /// <returns></returns>
        public static object GetOneValue(string cmdText, out string errMsg)
        {
            return GetOneValue(defCnnString, cmdText, out errMsg);
        }

        /// <summary>
        /// ��ȡ�����ָ���е�һ��ֵ
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="cmdText"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static object GetOneValue(string connString, string cmdText, out string errMsg)
        {
            errMsg = string.Empty;
            object obj = null;

            using (conn = new OleDbConnection(connString))
            {
                using (cmd = new OleDbCommand(cmdText, conn))
                {
                    Open();
                    OleDbDataReader oDr = cmd.ExecuteReader();
                    if (!oDr.Read())
                    {
                        obj = null;
                    }
                    else
                    {
                        obj = oDr[0];
                    }
                    oDr.Close();
                }
            }
            return obj;

            //try
            //{
            //    if (oConn == null)
            //    {
            //        oConn = new OleDbConnection(cnnString);
            //    }
            //    if (oCmd == null)
            //    {
            //        oCmd = new OleDbCommand(cmdText, oConn);
            //    }
            //    //oConn.Open();
            //    Open();
            //    oDr = oCmd.ExecuteReader();
            //    if (!oDr.Read())
            //    {
            //        oReturn = null;
            //    }
            //    else
            //    {
            //        oReturn = oDr[0];
            //    }
            //    oDr.Close();
            //    //return oReturn;
            //}
            //catch (Exception e)
            //{
            //    sErrMsg = e.Message;
            //    //System.Diagnostics.Debug.WriteLine(e.Message);
            //    //return null;
            //}
           
            //return oReturn;
        }

		/// <summary>
		/// ���зǲ�ѯ�������
		/// </summary>
		/// <param name="cmdText">�������</param>
		/// <param name="errMsg">������Ϣ</param>
		/// <returns></returns>
        public static bool ExecuteCommand(string cmdText, out string errMsg)
        {
            return ExecuteCommand(defCnnString, cmdText, out errMsg);
        }
		
		/// <summary>
		/// ִ���������
		/// </summary>
		/// <param name="cnnString"></param>
		/// <param name="cmdText"></param>
		/// <param name="errMsg"></param>
		/// <returns></returns>
        public static bool ExecuteCommand(string cnnString, string cmdText, out string errMsg)
        {
            errMsg = string.Empty;

            int iReturn = -1;
            using (conn = new OleDbConnection(cnnString))
            {
                using (cmd = new OleDbCommand(cmdText, conn))
                {
                    Open();
                    iReturn = cmd.ExecuteNonQuery();
                }
            }
            if (iReturn < 0)
            {
                //errMsg = "�������ݿ�����";
                return false;
            }
            else
            {
                return true;
            }
            //}
            //catch(Exception e)
            //{
            //    sErrMsg = e.Message;
            //    //System.Diagnostics.Debug.WriteLine(e.Message);
            //    return false;
            //}
            //finally
            //{
            //    if (oConn != null)
            //    {
            //        if (oConn.State == ConnectionState.Open)
            //        {
            //            oConn.Close();
            //        }
            //        oConn = null;
            //    }
            //}
            //return iReturn > 0;
        }

        /// <summary>
        /// ִ���������
        /// </summary>
        /// <param name="cmmdText"></param>
        /// <returns></returns>
        public static bool ExecuteCommand(string cmmdText)
        {
            int iRet = 0;
            using (conn = new OleDbConnection(defCnnString))
            {
                using (cmd = new OleDbCommand(cmmdText, conn))
                {
                    Open();
                    iRet = cmd.ExecuteNonQuery();
                }
            }
            return iRet > 0;
        }


        /// <summary>
        /// ��ѯ��������� DataTable���ݼ�
        /// </summary>
        /// <param name="cmdText">��ѯ���</param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string cmdText)
        {
            DataTable dtbl = new DataTable();
            try
            {
                using (conn = new OleDbConnection(defCnnString))
                {
                    using (cmd = new OleDbCommand(cmdText, conn))
                    {
                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = cmd;
                        da.SelectCommand.Connection = conn;
                        da.Fill(dtbl);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return dtbl;
        }

        /// <summary>
        /// ��ȡ������ݼ��Ľ��������һ��DataSet
        /// </summary>
        /// <param name="cmdArry">��ѯ���</param>
        /// <param name="tblNames">����</param>
        /// <param name="errMsg">������Ϣ</param>
        /// <returns></returns>
        public static DataSet GetDataSet(string[] cmdArry, string[] tblNames, out string errMsg)
        {
            //string cnnString = GetConnectString();
            return GetDataSet(defCnnString, cmdArry, tblNames, out errMsg);
        }

        public static DataSet GetDataSet(string connString, string[] cmdArry, string[] tblNames, out string errMsg)
        {
            errMsg = string.Empty;
            int nameLength = cmdArry.Length;
            if (nameLength != tblNames.Length)
            {
                errMsg = "��ѯ���ĸ���������ĸ�����һ����";
                return null;
            }
            using (conn = new OleDbConnection(connString))
            {
                using (cmd = new OleDbCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    OleDbDataAdapter oDa = new OleDbDataAdapter(cmd);
                    DataSet tempDst = new DataSet();
                    for (int i = 0; i < nameLength; ++i)
                    {
                        if (cmdArry[i].Trim() != string.Empty)
                        {
                            cmd.CommandText = cmdArry[i];
                            oDa.Fill(tempDst, tblNames[i]);
                        }
                    }
                    return tempDst;
                }
            }

            //try
            //{
            //    if (oConn == null)
            //    {
            //        oConn = new OleDbConnection(connString);
            //    }
            //    if (oConn == null)
            //    {
            //        oCmd = new OleDbCommand();
            //    }
            //    oCmd.CommandType = CommandType.Text;
            //    oCmd.Connection = oConn;

            //    oDa = new OleDbDataAdapter(oCmd);

            //    DataSet tempDst = new DataSet();

            //    for (int i = 0; i < nameLength; ++i)
            //    {
            //        if (cmdArry[i].Trim() != string.Empty)
            //        {
            //            oCmd.CommandText = cmdArry[i];
            //            oDa.Fill(tempDst, tblNames[i]);
            //        }
            //    }

            //    return tempDst;
            //}
            //catch (Exception e)
            //{
            //    sErrMsg = e.Message;
            //    System.Diagnostics.Debug.WriteLine(e.Message);
            //    return null;
            //}
        }

        /// <summary>
        /// ��ȡָ�������ݼ�
        /// </summary>
        /// <param name="tblName">��ѯ�ı�</param>
        /// <param name="strWhere">��ѯ��������"where,and�ؼ��֣������ҪOrder BY ��Ҫдȫ"</param>
        /// <param name="columnList">ָ������</param>
        /// <returns></returns>
        public static DataTable GetColumnList(string tblName, int top, string strWhere, params string[] columnList)
        {
            string cmdText = ConcatQueryColumn(tblName, top, strWhere, columnList);
            return ExecuteDataTable(cmdText);
        }
        /// <summary>
        /// ��ϲ�ѯ���
        /// </summary>
        /// <param name="tblName">��ѯ�ı�</param>
        /// <param name="strWhere">��ѯ��������"where,and�ؼ��֣������ҪOrder BY ��Ҫдȫ"</param>
        /// <param name="columnList">ָ������,�������ѯȫ����,Ϊ�˸��Ӹ�Чʱ����ָ������</param>
        /// <returns></returns>
        public static string ConcatQueryColumn(string tblName, int top, string strWhere, params string[] columnList)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (top > 0)
            {
                sb.Append("TOP ");
                sb.Append(top);
                sb.Append(" ");
            }
            int len = 0;
            if (columnList != null)
            {
                len = columnList.Length;
            }
            int i = 0;

            while (i < len - 1)//��Ϊ��Ч��������ֻ��һ��ʱ������ѭ���ڲ�
            {
                sb.Append(columnList[i]);
                sb.Append(",");
                i++;
            }

            if (len >= 1)//һ������
            {
                sb.Append(columnList[len - 1]);
            }
            else //����ṩ�Ĳ�ѯ����Ϊ��ʱ�����ȫ��
            {
                sb.Append(" * ");
            }

            sb.Append(" FROM ");
            sb.Append(tblName);
            sb.Append(" WHERE 1=1 ");//Ϊ�˸���ͨ��
            if (strWhere != null && strWhere.Trim().Length != 0)//����˳���ܷ�
            {
                sb.Append(" AND ");
                sb.Append(strWhere);
            }
            return sb.ToString();
        }
        /// <summary>
        /// ѹ������
        /// </summary>
        public static void CompactAccessDB()
        {
            object[] oParams;
           // string connectionString = GetConnectString();

            object objJRO = Activator.CreateInstance(Type.GetTypeFromProgID("JRO.JetEngine"));
            oParams = new object[] {defCnnString, string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\tempdb.mdb;Jet OLEDB:Engine Type=5;Jet OLEDB:Database Password={0}",password)};
            objJRO.GetType().InvokeMember("CompactDatabase",System.Reflection.BindingFlags.InvokeMethod, null,objJRO,oParams);

            System.IO.File.Delete(getLocalDBPathString);
            System.IO.File.Move("C:\\tempdb.mdb", getLocalDBPathString);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(objJRO);
            objJRO = null;
        }


        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="cnnString">�����ַ���</param>
        /// <returns></returns>
        public static bool TestConnection(string cnnString)
        {
            using (conn = new OleDbConnection(cnnString))
            {
                Open();
                if (conn.State == ConnectionState.Open)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
	}
}

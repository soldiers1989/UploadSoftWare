using System;
using System.Text;
using System.Data;
using System.Data.OleDb;
 
namespace DY.FoodClientLib
{
	/// <summary>
        /// 数据库操作公共类
        /// 修改：陈国利 2011-06-05
        /// 修改加继承IDisposable接口，释放非托管资源
	/// </summary>
	public class DataBase:IDisposable
	{
        /// <summary>
        /// 提供静态方法，防止外部构造
        /// </summary>
        private DataBase()
        {
        }

        //其实可以写在配置文件中
        private static readonly string password = "dy05xl378";
        private static readonly string dataName = "local.Mdb";
        private static readonly string getLocalDBPathString = string.Format("{0}Data\\{1}", AppDomain.CurrentDomain.BaseDirectory, dataName);
        private static readonly string defCnnString = string.Format("Provider = Microsoft.Jet.OLEDB.4.0.1;Data Source ={0};Persist Security Info=False;Jet OLEDB:Database Password={1}", getLocalDBPathString, password);

        private static OleDbConnection conn = null;
        private static OleDbCommand cmd = null;

        /// <summary>
        /// 实现接口Dispose的方法
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
        ///// 获取连接字符串
        ///// </summary>
        ///// <returns></returns>
        //public static string GetConnectString()
        //{
        //    string defConnString = string.Format("Provider = Microsoft.Jet.OLEDB.4.0.1;Data Source ={0};Persist Security Info=False;Jet OLEDB:Database Password={1}", GetLocalDBPathString, password);
        //    return defConnString;
        //}

        ///// <summary>
        ///// 获取数据库的路径包括数据库名称
        ///// </summary>
        ///// <returns></returns>
        //public static string GetLocalDBPathString()
        //{
        //    string path = string.Format("{0}Data\\{1}", AppDomain.CurrentDomain.BaseDirectory, dataName);
        //    return path;
        //}

        /// <summary>
        /// 通过单一结果值
        /// </summary>
        /// <param name="cmdText">查询命令</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns></returns>
        public static object GetOneValue(string cmdText, out string errMsg)
        {
            return GetOneValue(defCnnString, cmdText, out errMsg);
        }

        /// <summary>
        /// 获取结果集指定列第一行值
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
		/// 运行非查询命令语句
		/// </summary>
		/// <param name="cmdText">命令语句</param>
		/// <param name="errMsg">错误信息</param>
		/// <returns></returns>
        public static bool ExecuteCommand(string cmdText, out string errMsg)
        {
            return ExecuteCommand(defCnnString, cmdText, out errMsg);
        }
		
		/// <summary>
		/// 执行命令操作
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
                //errMsg = "操作数据库有误";
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
        /// 执行命令操作
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
        /// 查询结果集返回 DataTable数据集
        /// </summary>
        /// <param name="cmdText">查询语句</param>
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
        /// 获取多个数据集的结果并返回一个DataSet
        /// </summary>
        /// <param name="cmdArry">查询语句</param>
        /// <param name="tblNames">表名</param>
        /// <param name="errMsg">错误消息</param>
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
                errMsg = "查询语句的个数与表名的个数不一样。";
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
        /// 获取指定列数据集
        /// </summary>
        /// <param name="tblName">查询的表</param>
        /// <param name="strWhere">查询条件不用"where,and关键字，但如果要Order BY 则要写全"</param>
        /// <param name="columnList">指定列名</param>
        /// <returns></returns>
        public static DataTable GetColumnList(string tblName, int top, string strWhere, params string[] columnList)
        {
            string cmdText = ConcatQueryColumn(tblName, top, strWhere, columnList);
            return ExecuteDataTable(cmdText);
        }
        /// <summary>
        /// 组合查询语句
        /// </summary>
        /// <param name="tblName">查询的表</param>
        /// <param name="strWhere">查询条件不用"where,and关键字，但如果要Order BY 则要写全"</param>
        /// <param name="columnList">指定列名,如果不查询全部列,为了更加高效时必须指定列名</param>
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

            while (i < len - 1)//更为高效的作法，只有一列时不进入循环内部
            {
                sb.Append(columnList[i]);
                sb.Append(",");
                i++;
            }

            if (len >= 1)//一个以上
            {
                sb.Append(columnList[len - 1]);
            }
            else //如果提供的查询列名为空时，则查全部
            {
                sb.Append(" * ");
            }

            sb.Append(" FROM ");
            sb.Append(tblName);
            sb.Append(" WHERE 1=1 ");//为了更加通用
            if (strWhere != null && strWhere.Trim().Length != 0)//条件顺序不能反
            {
                sb.Append(" AND ");
                sb.Append(strWhere);
            }
            return sb.ToString();
        }
        /// <summary>
        /// 压缩数据
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
        /// 测试连接
        /// </summary>
        /// <param name="cnnString">连接字符串</param>
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

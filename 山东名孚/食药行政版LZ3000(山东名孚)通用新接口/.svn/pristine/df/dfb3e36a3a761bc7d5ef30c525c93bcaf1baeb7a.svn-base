using System;
using System.Data;
using System.Text;

namespace DY.FoodClientLib
{
	/// <summary>
	/// tMachine操作数据库层类
	/// </summary>
	public class clsMachineOpr
	{
		public clsMachineOpr()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

        private StringBuilder sb = new StringBuilder();
		/// <summary>
		/// 部分修改保存
		/// </summary>
		/// <param name="OprObject">对象clsMachine的一个实例参数</param>
		/// <returns></returns>
        public int UpdatePart(clsMachine OprObject, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
          
            try
            {
                sb.Length = 0;
                sb.Append("UPDATE tMachine SET ");
                sb.AppendFormat("LinkComNo={0}", OprObject.LinkComNo);
                sb.AppendFormat(",IsSupport={0}", OprObject.IsSupport);
                sb.AppendFormat(",TestValue={0}", OprObject.TestValue);
                sb.AppendFormat(",TestSign='{0}'", OprObject.TestSign);
                sb.AppendFormat(",LinkStdCode='{0}'", OprObject.LinkStdCode);
                sb.AppendFormat(" WHERE SysCode='{0}'", OprObject.SysCode);

                //string updateSql="update tMachine set "
                //    + "LinkComNo=" + OprObject.LinkComNo + "," 
                //    + "IsSupport=" + OprObject.IsSupport + "," 
                //    + "TestValue=" + OprObject.TestValue + "," 
                //    + "TestSign='" + OprObject.TestSign + "'," 
                //    + "LinkStdCode='" + OprObject.LinkStdCode + "'" 
                //    + " where SysCode='" + OprObject.SysCode + "' ";
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);

                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }

            return rtn;
        }

        /// <summary>
        /// 部分修改保存
        /// </summary>
        /// <param name="model">对象clsMachine的一个实例参数</param>
        /// <returns></returns>
        public int InsertOrUpdate(clsMachine model,bool isAdd, out string sErrMsg)
        {
            sErrMsg = string.Empty;
            int rtn = 0;
            sb.Length = 0;
            try
            {
                //sb.AppendFormat("IF Exists(SELECT SysCode FROM tMachine WHERE SysCode='{0}')",model.SysCode);//如果存在进行update操作
                if (!isAdd)
                {
                    sb.Append("UPDATE tMachine SET ");

                    sb.AppendFormat("LinkComNo={0}", model.LinkComNo);
                    sb.AppendFormat(",MachineName='{0}'", model.MachineName);
                    sb.AppendFormat(",MachineModel='{0}'", model.MachineModel);
                    sb.AppendFormat(",Company='{0}'", model.Company);
                    sb.AppendFormat(",Protocol='{0}'", model.Protocol);
                    sb.AppendFormat(",IsSupport={0}", model.IsSupport);
                    sb.AppendFormat(",IsShow={0}", model.IsShow);
                    sb.AppendFormat(",TestValue={0}", model.TestValue);
                    sb.AppendFormat(",TestSign='{0}'", model.TestSign);
                    sb.AppendFormat(",LinkStdCode='{0}'", model.LinkStdCode);
                    sb.AppendFormat(",OrderId='{0}'", model.OrderId);
                    sb.AppendFormat(" WHERE SysCode='{0}'", model.SysCode);
                }
                else
                {

                    //sb.Append(" ELSE ");
                    sb.Append("INSERT INTO tMachine");
                    sb.Append("(SysCode,MachineName,MachineModel,Company,Protocol,IsSupport,");
                    sb.Append("LinkComNo,TestSign,TestValue,LinkStdCode,IsShow,OrderId)");
                    sb.Append("VALUES(");

                    sb.AppendFormat("'{0}'", model.SysCode);
                    sb.AppendFormat(",'{0}'", model.MachineName);
                    sb.AppendFormat(",'{0}'", model.MachineModel);
                    sb.AppendFormat(",'{0}'", model.Company);
                    sb.AppendFormat(",'{0}'", model.Protocol);
                    sb.AppendFormat(",{0}", model.IsSupport);
                    sb.AppendFormat(",{0}", model.LinkComNo);
                    sb.AppendFormat(",'{0}'", model.TestSign);
                    sb.AppendFormat(",{0}", model.TestValue);
                    sb.AppendFormat(",'{0}'", model.LinkStdCode);
                    sb.AppendFormat(",{0}", model.IsShow);
                    sb.AppendFormat(",{0}", model.OrderId);

                    sb.Append(")");
                }
                //string updateSql="update tMachine set "
                //    + "LinkComNo=" + OprObject.LinkComNo + "," 
                //    + "IsSupport=" + OprObject.IsSupport + "," 
                //    + "TestValue=" + OprObject.TestValue + "," 
                //    + "TestSign='" + OprObject.TestSign + "'," 
                //    + "LinkStdCode='" + OprObject.LinkStdCode + "'" 
                //    + " where SysCode='" + OprObject.SysCode + "' ";
                DataBase.ExecuteCommand(sb.ToString(), out sErrMsg);
                sb.Length = 0;
                rtn = 1;
            }
            catch (Exception e)
            {
                sErrMsg = e.Message;
            }

            return rtn;
        }
         
        /// <summary>
        /// 判断是否在
        /// </summary>
        /// <param name="sysCode"></param>
        /// <returns></returns>
        public bool IsExists(string sysCode)
        {
            string sErr=string.Empty;
            int ret = 0;
            string cmdText=string.Format("SELECT COUNT(SysCode) FROM tMachine WHERE SysCode='{0}'", sysCode); 
           object obj=DataBase.GetOneValue(cmdText,out sErr);
           if (obj != null &&obj != DBNull.Value)
           {
               ret = Convert.ToInt32(obj);
                
           }
           return (ret != 0);
        }


        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="code"></param>
        public void Delete(string code,out string sErrmsg)
        {
            string cmdText = string.Format("DELETE FROM tMachine WHERE Syscode='{0}'",code);
            DataBase.ExecuteCommand(cmdText, out sErrmsg);
        }
		
		/// <summary>
		/// 根据查询串条件查询记录
		/// </summary>
		/// <param name="whereSql">查询条件串,不含Where</param>
		/// <param name="orderBySql">排序串,不含Order</param>
        /// <param name="type">查询不同情况</param>
		/// <returns></returns>
        public DataTable GetAsDataTable(string whereSql, string orderBySql, int type)
        {
            string errMsg = string.Empty;
            DataTable dtbl = null;
            sb.Length = 0;
            try
            {
                if (type == 0)
                {
                    // selectSql = 
                    sb.Append("SELECT ");
                    sb.Append(" SysCode,MachineName,ShortCut,MachineModel,Company,Protocol");
                    sb.Append(",LinkComNo,IsSupport,TestValue,TestSign,LinkStdCode,IsShow");
                    sb.Append(" FROM tMachine");
                }
                else if (type == 1)
                {
                    sb.Append("SELECT MachineName,SysCode FROM tMachine");
                }
                else if (type == 2)
                {
                    sb.Append("SELECT SysCode,TestValue,LinkStdCode,LinkComNo,IsSupport FROM tMachine");
                }

                if (whereSql != null && whereSql.Length > 0)
                {
                    sb.Append(" WHERE ");
                    sb.Append(whereSql);
                }
                if (orderBySql != null && orderBySql.Length != 0)
                {
                    sb.Append(" ORDER BY ");
                    sb.Append(orderBySql);
                }
                string[] cmd = new string[1];
                cmd[0] = sb.ToString();
                string[] names = new string[1];
                names[0] = "Machine";
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["Machine"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                //Log.WriteLog("查询clsMachine",e);
                errMsg = e.Message;
            }

            return dtbl;
        }

        /// <summary>
        /// 查询指定列的值
        /// </summary>
        /// <param name="sFind"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetNameFromCode(string sFind, string code)
        {
            string sErrMsg = string.Empty;
            if (code.Equals(string.Empty))
            {
                return string.Empty;
            }

            try
            {
                string sql = string.Format("SELECT {0} FROM tMachine WHERE syscode='{1}' order by syscode", sFind, code);
                Object obj = DataBase.GetOneValue(sql, out sErrMsg);
                if (obj == null || obj == DBNull.Value)
                {
                    return string.Empty;
                }
                else
                {
                    return obj.ToString();
                }
            }
            catch (Exception e)
            {
                sErrMsg = e.Message;
                return null;
            }
        }
		
        /// <summary>
        /// 获取仪器名称
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
		public static string GetMachineNameFromCode(string code)
		{
			return GetNameFromCode("MachineName",code);
		}	

        /// <summary>
        /// 默认的仪器列表数
        /// </summary>
        /// <param name="sErrMsg"></param>
        /// <returns></returns>
        public static int GetRecCount(out string sErrMsg)
        {
            sErrMsg = string.Empty;
            int rtn = 0;

            try
            {
                string sql = "SELECT Count(*) FROM tMachine WHERE IsSupport=True";
                Object obj = DataBase.GetOneValue(sql, out sErrMsg);
                if (obj == null || obj == DBNull.Value)
                {
                    rtn = 0;
                }
                else
                {
                    rtn = Convert.ToInt32(obj);
                }
            }
            catch (Exception e)
            {
                sErrMsg = e.Message;
                rtn = -1;
            }

            return rtn;
        }

        /// <summary>
        /// 更新IsSupport字段数据
        /// </summary>
        /// <param name="sErrMsg"></param>
        /// <returns></returns>
		public static int UpdateIsSupport(out string sErrMsg)
		{
			sErrMsg=string.Empty;
			int rtn=0;			

			try
			{
				string updateSql="update tMachine set IsSupport=False";
				DataBase.ExecuteCommand(updateSql,out sErrMsg);

				rtn=1;
			}
			catch(Exception e)
			{
				sErrMsg=e.Message;
			}

			return rtn;
		}
        
        /// <summary>
        /// 获取DY5000仪器协议版本
        /// </summary>
        public static void GetDY5000()
        {
            string sErrMsg = string.Empty;
            try
            {
                string sql = "SELECT Protocol FROM tMachine WHERE SysCode='010' And MachineModel='DY5000'";
                Object obj = DataBase.GetOneValue(sql, out sErrMsg);//获取协议列值
                if (obj.ToString().Equals("RS232DY5000"))
                {
                    ShareOption.DY5000Name = "DY5000";
                }
                else if (obj.ToString().Equals("RS232DY5000LD"))
                {
                    ShareOption.DY5000Name = "DY5000LD";
                }
                else
                {
                    ShareOption.DY5000Name = string.Empty;
                }
            }
            catch (Exception e)
            {
                sErrMsg = e.Message;
            }
        }

        /// <summary>
        ///获取DY3000仪器协议版本
        /// </summary>
        public static void GetDY3000()
        {
            string sErrMsg = string.Empty;
            try
            {
                string sql = "Select Protocol From tMachine Where SysCode='009' And MachineModel='DY3000'";
                Object o = DataBase.GetOneValue(sql, out sErrMsg);
                if (o.ToString().Equals("RS232DY3000DY"))
                {
                    ShareOption.CurDY3000Tag = "DY3000DY";
                }
                else if (o.ToString().Equals("RS232DY3000"))
                {
                    ShareOption.CurDY3000Tag = "DY3000";
                }
                else
                {
                    ShareOption.CurDY3000Tag = string.Empty;
                }
            }
            catch (Exception e)
            {
                sErrMsg = e.Message;
            }
        }

        /// <summary>
        /// 通过用户名设置各用户所能操作的菜单
        /// </summary>
        /// <param name="userName">用户登记名</param>
        /// <returns></returns>
        public System.Collections.Hashtable IsMenuRight()//, string assetCode
        {
            System.Collections.Hashtable htbl = new System.Collections.Hashtable();
            DataTable dtbl = DataBase.GetColumnList("tMachine", 0, "IsShow=true", "SysCode,IsShow");
            //List<NDYLib.Model.Sys_Right> list = GetModelList(string.Format("UserName='{0}'", userName));
            if (dtbl != null && dtbl.Rows.Count > 0)
            {
                
                for (int i = 0; i < dtbl.Rows.Count; i++)
                {
                    //v = Convert.ToBoolean(dtbl.Rows[i]["IsShow"]);
                    htbl.Add(dtbl.Rows[i]["SysCode"], dtbl.Rows[i]["IsShow"]);
                }
                dtbl.Dispose();
                dtbl = null;
            }
            return htbl;
        }

        /// <summary>
        /// 获取指定列指定结果集结果集
        /// 作者：
        /// </summary>
        /// <param name="top">表示前N个记录，小于等于0表示"*"</param>
        /// <param name="strWhere">过滤条件组合串，输入字符串中不能包含where关键字，需要排序时在条件后加 order by</param>
        /// <param name="columns">表示1个或者多个字段字符串，多个字段之间用","隔开如： "ID,Name,Value,...."</param>
        /// <returns>返回结果集</returns>
        public DataTable GetColumnDataTable(int top, string strWhere, params string[] columns)
        {
            return DataBase.GetColumnList("tMachine", top, strWhere, columns);
        }
	}
}

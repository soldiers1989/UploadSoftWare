using System;
using System.Data;
using System.Text;

namespace DY.FoodClientLib
{
	/// <summary>
	/// 单位类别
	/// </summary>
	public class clsCompanyKindOpr
	{
		public clsCompanyKindOpr()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

      private StringBuilder sb = new StringBuilder();

		/// <summary>
		/// 部分修改保存
		/// </summary>
		/// <param name="model">对象clsCompanyKind的一个实例参数</param>
		/// <returns></returns>
      public int UpdatePart(clsCompanyKind model, out string errMsg)
      {
          errMsg = string.Empty;
          int rtn = 0;
          try
          {
              sb.Length = 0;
              sb.Append("UPDATE TCOMPANYKIND SET ");
              sb.AppendFormat("StdCode='{0}',", model.StdCode);
              sb.AppendFormat("Name='{0}',", model.Name);
              sb.AppendFormat("CompanyProperty='{0}',", model.CompanyProperty);
              sb.AppendFormat("IsReadOnly={0},", model.IsReadOnly);
              sb.AppendFormat("IsLock={0},", model.IsLock);
              sb.AppendFormat("Remark='{0}'", model.Remark);
              sb.AppendFormat("Ksign='{0}'", model.Ksign);

              sb.AppendFormat(" WHERE SysCode='{0}',", model.SysCode);
              DataBase.ExecuteCommand(sb.ToString(), out errMsg);
              sb.Length = 0;
              rtn = 1;
          }
          catch (Exception e)
          {
              //Log.WriteLog("更新clsCompanyKind",e);
              errMsg = e.Message;
          }

          return rtn;
      }
        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="whereSql"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
      public int Delete(string whereSql, out string errMsg)
      {
          errMsg = string.Empty;
          int rtn = 0;

          try
          {
              sb.Length = 0;
              sb.Append("DELETE FROM tCompanyKind");

              if (!whereSql.Equals(""))
              {
                  sb.Append(" WHERE ");
                  sb.Append(whereSql);
              }
              DataBase.ExecuteCommand(sb.ToString(), out errMsg);
              sb.Length = 0;
              rtn = 1;
          }
          catch (Exception e)
          {
              errMsg = e.Message;
          }

          return rtn;
      }
		
		/// <summary>
		/// 根据主键编号删除记录
		/// </summary>
		/// <param name="%pkname%">主键编号</param>
		/// <returns></returns>
      public int DeleteByPrimaryKey(string mainkey, out string errMsg)
      {
          errMsg = string.Empty;
          int rtn = 0;

          try
          {
              sb.Length = 0;
              sb.Append("DELETE FROM tCompanyKind");
              sb.Append(" WHERE SysCode='");
              sb.Append(mainkey);
              sb.Append("'");
              DataBase.ExecuteCommand(sb.ToString(), out errMsg);
              sb.Length = 0;
              rtn = 1;
          }
          catch (Exception e)
          {
              //Log.WriteLog("通过主键删除clsCompanyKind",e);
              errMsg = e.Message;
          }

          return rtn;
      }

		
		/// <summary>
		/// 根据查询串条件查询记录
		/// </summary>
		/// <param name="whereSql">查询条件串,不含Where</param>
		/// <param name="orderBySql">排序串,不含Order</param>
		/// <returns></returns>
      public DataTable GetAsDataTable(string whereSql, string orderBySql, int type)
      {
          string errMsg = string.Empty;
          DataTable dt = null;

          try
          {
              sb.Length = 0;
              if (type == 0)
              {
                  sb.Append("SELECT SYSCODE,STDCODE,NAME,COMPANYPROPERTY,ISLOCK,ISREADONLY,REMARK,Ksign FROM TCOMPANYKIND");
              }
              else if (type == 1)
              {
                  sb.Append("SELECT SYSCODE,NAME,STDCODE FROM TCOMPANYKIND");
              }

              if (!whereSql.Equals(""))
              {
                  sb.Append(" WHERE ");
                  sb.Append(whereSql);
              }
              if (!orderBySql.Equals(""))
              {
                  sb.Append(" ORDER BY ");
                  sb.Append(orderBySql);
              }
              string[] cmd = new string[1] { sb.ToString() };
              //cmd[0]=selectSql;
              string[] names = new string[1] { "CompanyKind" };
              dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["CompanyKind"];
              sb.Length = 0;
          }
          catch (Exception e)
          {
              //Log.WriteLog("查询clsCompanyKind",e);
              errMsg = e.Message;
          }

          return dt;
      }

		/// <summary>
		/// 插入一条明细记录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
      public int Insert(clsCompanyKind model, out string errMsg)
      {
          errMsg = string.Empty;
          int rtn = 0;

          try
          {
              sb.Length = 0;
              sb.Append("INSERT INTO tCompanyKind(SysCode,StdCode,Name,CompanyProperty,IsReadOnly,IsLock,Remark,Ksign)");
              sb.Append(" VALUES(");
              sb.AppendFormat("'{0}',", model.SysCode);
              sb.AppendFormat("'{0}',", model.StdCode);
              sb.AppendFormat("'{0}',", model.Name);
              sb.AppendFormat("'{0}',", model.CompanyProperty);
              sb.AppendFormat("{0},", model.IsReadOnly);
              sb.AppendFormat("{0},", model.IsLock);
              sb.AppendFormat("'{0}',", model.Remark);
              sb.AppendFormat("'{0}'", model.Ksign);
              sb.Append(")");
              DataBase.ExecuteCommand(sb.ToString(), out errMsg);
              sb.Length = 0;
              rtn = 1;
          }
          catch (Exception e)
          {
              //Log.WriteLog("添加clsCompanyKind",e);
              errMsg = e.Message;
          }

          return rtn;
      }
        /// <summary>
        /// 获取最大编号
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
      public int GetMaxNO(out string errMsg)
      {
          errMsg = string.Empty;
          int rtn;

          try
          {
              string sql = "SELECT SYSCODE FROM TCOMPANYKIND ORDER BY SYSCODE DESC";
              Object o = DataBase.GetOneValue(sql, out errMsg);
              if (o == null)
              {
                  rtn = 0;
              }
              else
              {
                  rtn = Convert.ToInt32(o.ToString());
              }
          }
          catch (Exception e)
          {
              errMsg = e.Message;
              rtn = -1;
          }

          return rtn;
      }

		public static string NameFromCode(string scode)
		{
			string errMsg=string.Empty; 
			if(scode.Equals(""))
			{
				return "";
			}

			try
			{

                string sql = string.Format("SELECT NAME FROM TCOMPANYKIND WHERE syscode='{0}' order by syscode", scode);
				Object o=DataBase.GetOneValue(sql,out errMsg);
				if(o==null)
				{
                    return string.Empty ;
				}
				else
				{
					return o.ToString();
				}
			}
			catch(Exception e)
			{
				errMsg=e.Message;
				return null;
			}
		}

        public static bool ExistSameValue(string sName)
        {
            string errMsg = string.Empty;

            try
            {
                string sql = string.Format("SELECT NAME FROM TCOMPANYKIND WHERE Name='{0}' ", sName);
                Object o = DataBase.GetOneValue(sql, out errMsg);
                if (o == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return true;
            }
        }
	}
}

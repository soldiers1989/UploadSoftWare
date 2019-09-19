using System;
using System.Data;

namespace DYSeriesDataSet
{
	/// <summary>
	///用户信息操作类
	/// </summary>
    public class clsUserInfoOpr
    {
        public clsUserInfoOpr()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 部分修改保存
        /// </summary>
        /// <param name="model">对象clsUserInfo的一个实例参数</param>
        /// <returns></returns>
        public int UpdatePart(clsUserInfo model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                string updateSql = "update tUserInfo set "
                    + "LoginID='" + model.LoginID + "',"
                    + "[Name]='" + model.Name + "',"
                    + "[PassWord]='" + model.PassWord + "',"
                    + "UnitCode='" + model.UnitCode + "',"
                    //+ "WebLoginID='" + model.WebLoginID + "',"
                    //+ "WebPassWord='" + model.WebPassWord + "',"
                    + "IsAdmin=" + model.IsAdmin + ","
                    + "IsLock=" + model.IsLock + ","
                    + "Remark='" + model.Remark + "'"
                    + " where UserCode='" + model.UserCode + "' ";
                DataBase.ExecuteCommand(updateSql, out errMsg);

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
                string deleteSql = string.Format("DELETE from tUserInfo where UserCode='{0}'", mainkey);
                DataBase.ExecuteCommand(deleteSql, out errMsg);

                //deleteSql = "delete from tResult"
                //    + " where Checker='" + mainkey + "'"
                //    + " or Assessor='" + mainkey + "'"
                //    + " or Organizer='" + mainkey + "'";
                //DataBase.ExecuteCommand(deleteSql, out sErrMsg);

                rtn = 1;
            }
            catch (Exception e)
            {
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
                string selectSql = "";
                if (type == 0)
                {
                    selectSql = "select UserCode,LoginID,Name,PassWord,UnitCode,WebLoginID,WebPassWord,IsLock,IsAdmin,Remark  from tUserInfo";
                }
                else if (type == 1)
                {
                    selectSql = "select Name,UserCode from tUserInfo";
                }

                if (!whereSql.Equals(""))
                {
                    selectSql += " where " + whereSql;
                }
                if (!orderBySql.Equals(""))
                {
                    selectSql += " order by " + orderBySql;
                }
                string[] cmd = new string[1] { selectSql };
                string[] names = new string[1] { "UserInfo" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["UserInfo"];
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }

            return dt;
        }

        /// <summary>
        /// 插入一条明细记录
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public int Insert(clsUserInfo userModel, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                 //WebLoginID,WebPassWord,
                //+ userModel.WebLoginID + "','"
                //+ userModel.WebPassWord + "',"
                string insertSql = "INSERT INTO tUserInfo(UserCode,[LoginID],[Name],[PassWord],UnitCode,IsAdmin,IsLock,[Remark])"
                    + " VALUES('"
                    + userModel.UserCode + "','"
                    + userModel.LoginID + "','"
                    + userModel.Name + "','"
                    + userModel.PassWord + "','"
                    + userModel.UnitCode + "',"
                    + userModel.IsAdmin + ","
                    + userModel.IsLock + ",'"
                    + userModel.Remark + "')";
                DataBase.ExecuteCommand(insertSql, out errMsg);

                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }

            return rtn;
        }

        public int GetMaxNO(out string errMsg)
        {
            errMsg = string.Empty;
            int rtn;

            try
            {
                string sql = "select UserCode from tUserInfo order by UserCode desc";
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

        /// <summary>
        /// 通代码获取名称
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string NameFromCode(string code)
        {
            string errMsg = string.Empty;
            if (code.Equals(""))
            {
                return "";
            }

            try
            {
                string sql = string.Format("SELECT NAME FROM TUSERINFO WHERE USERCODE='{0}' ORDER BY USERCODE", code);
                   
                Object o = DataBase.GetOneValue(sql, out errMsg);
                if (o == null)
                {
                    return "";
                }
                else
                {
                    return o.ToString();
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
        }

        public bool ExistSameValue(string code)
        {
            string errMsg = string.Empty;

            try
            {
                string sql = "select LoginID from tUserInfo where LoginID='" + code
                    + "' order by LoginID";
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

        /// <summary>
        /// 获取用户实例
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        public clsUserInfo GetInfo(string whereSql)
        {
            string errMsg = string.Empty;
            DataTable dt = null;
            clsUserInfo userInfo = null;

            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("SELECT UserCode,LoginID,Name,PassWord,UnitCode,WebLoginID,WebPassWord,IsLock,IsAdmin,Remark FROM tUserInfo");

                if (!whereSql.Equals(""))
                {
                    sb.Append(" WHERE ");
                    sb.Append(whereSql);
                }

                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "UserInfo" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["UserInfo"];
                if (dt.Rows.Count > 0)
                {
                    userInfo = new clsUserInfo();
                    userInfo.UserCode = dt.Rows[0]["UserCode"].ToString();
                    userInfo.LoginID = dt.Rows[0]["LoginID"].ToString();
                    userInfo.Name = dt.Rows[0]["Name"].ToString();
                    userInfo.PassWord = dt.Rows[0]["PassWord"].ToString();
                    userInfo.UnitCode = dt.Rows[0]["UnitCode"].ToString();
                    userInfo.WebLoginID = dt.Rows[0]["WebLoginID"].ToString();
                    userInfo.WebPassWord = dt.Rows[0]["WebPassWord"].ToString();
                    userInfo.Remark = dt.Rows[0]["Remark"].ToString();
                    userInfo.IsLock = Convert.ToBoolean(dt.Rows[0]["IsLock"]);
                    userInfo.IsAdmin = Convert.ToBoolean(dt.Rows[0]["IsAdmin"]);
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }

            return userInfo;
        }
    }
}

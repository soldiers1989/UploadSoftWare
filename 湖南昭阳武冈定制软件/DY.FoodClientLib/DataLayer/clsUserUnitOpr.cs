using System;
using System.Data;
using System.Text;

namespace DY.FoodClientLib
{
	/// <summary>
	/// �û���λ
	/// </summary>
	public class clsUserUnitOpr
	{
		public clsUserUnitOpr()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
        StringBuilder sb = new StringBuilder();

		/// <summary>
		/// �����޸ı���
		/// </summary>
		/// <param name="model">����clsUserUnit��һ��ʵ������</param>
		/// <returns></returns>
        public int UpdatePart(clsUserUnit model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sb.Length = 0;
                sb.Append(" UPDATE tUserUnit SET ");
                sb.AppendFormat(" StdCode='{0}'", model.StdCode);
                sb.AppendFormat(",CompanyID='{0}'", model.CompanyID);
                sb.AppendFormat(",FullName='{0}'", model.FullName);
                sb.AppendFormat(",ShortName='{0}'", model.ShortName);
                sb.AppendFormat(",DisplayName='{0}'", model.DisplayName);
                sb.AppendFormat(",ShortCut='{0}'", model.ShortCut);
                sb.AppendFormat(",DistrictCode='{0}'", model.DistrictCode);
                sb.AppendFormat(",PostCode='{0}'", model.PostCode);
                sb.AppendFormat(",Address='{0}'", model.Address);
                sb.AppendFormat(",LinkMan='{0}'", model.LinkMan);
                sb.AppendFormat(",LinkInfo='{0}'", model.LinkInfo);
                sb.AppendFormat(",WWWInfo='{0}'", model.WWWInfo);
                sb.Append(",IsReadOnly=");
                sb.Append(model.IsReadOnly);

                sb.Append(",IsLock=");
                sb.Append(model.IsLock);

                sb.AppendFormat(",Remark='{0}'", model.Remark);
                sb.AppendFormat(" WHERE SysCode='{0}'", model.SysCode);

                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                sb.Length = 0;

                rtn = 1;
            }
            catch (Exception e)
            {
                //Log.WriteLog("����clsUserUnit",e);
                errMsg = e.Message;
            }

            return rtn;
        }
		
		/// <summary>
		/// �����������ɾ����¼
		/// </summary>
		/// <param name="%pkname%">�������</param>
		/// <returns></returns>
		public int DeleteByPrimaryKey(string mainkey,out string errMsg)
		{
			errMsg=string.Empty;        
			int rtn = 0;
            sb.Length = 0;
			try
			{
                sb.Append("DELETE FROM tUserUnit WHERE SysCode='");
                sb.Append(mainkey);
                sb.Append("'");
                DataBase.ExecuteCommand(sb.ToString(), out errMsg);
                //string deleteSql="delete from tUserUnit"
                //    + " where SysCode='" + mainkey + "' ";
				//DataBase.ExecuteCommand(deleteSql,out sErrMsg);

				rtn=1;
			}
			catch(Exception e)
			{
				//Log.WriteLog("ͨ������ɾ��clsUserUnit",e);
				errMsg=e.Message;;
			}

			return rtn;
		}
		
		/// <summary>
		/// ���ݲ�ѯ��������ѯ��¼
		/// </summary>
		/// <param name="whereSql">��ѯ������,����Where</param>
		/// <param name="orderBySql">����,����Order</param>
        /// <param name="type">��ѯ���</param>
		/// <returns></returns>
        public DataTable GetAsDataTable(string whereSql, string orderBySql, int type)
        {
            string errMsg = string.Empty;
            DataTable dtbl = null;

            try
            {
                sb.Length = 0;
                if (type == 0)
                {
                    sb.Append("SELECT A.SYSCODE,A.STDCODE,A.FULLNAME,A.SHORTNAME,A.DISPLAYNAME,A.SHORTCUT,A.DISTRICTCODE,B.NAME AS DISTRICTNAME,A.POSTCODE,A.ADDRESS,A.LINKMAN,A.LINKINFO,A.WWWINFO,A.ISREADONLY,A.ISLOCK,A.REMARK,A.CompanyId FROM TUSERUNIT AS A LEFT JOIN TDISTRICT AS B ON A.DISTRICTCODE=B.SYSCODE ");
                }
                else if (type == 1)
                {
                    sb.Append("SELECT FULLNAME,STDCODE,SYSCODE,CompanyID FROM TUSERUNIT");
                }

                if (!whereSql.Equals(string.Empty))
                {
                    sb.Append(" WHERE ");
                    sb.Append(whereSql);
                }
                if (!orderBySql.Equals(string.Empty))
                {
                    sb.Append(" ORDER BY ");
                    sb.Append(orderBySql);
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "UserUnit" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["UserUnit"];
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }

            return dtbl;
        }

		/// <summary>
		/// ����һ����ϸ��¼
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
        public int Insert(clsUserUnit model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                sb.Length = 0;
                sb.Append("INSERT INTO tUserUnit(SysCode,StdCode,FullName,ShortName,DisplayName,ShortCut,DistrictCode,PostCode,Address,LinkMan,LinkInfo,WWWInfo,IsReadOnly,IsLock,Remark,CompanyID)");
                sb.Append(" VALUES(");
                sb.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{7}','{8}','{9}','{10}','{11}',{12},{13},'{14}','{15}'", model.SysCode, model.StdCode, model.FullName, model.ShortName, model.DisplayName, model.ShortCut, model.DistrictCode, model.PostCode, model.Address, model.LinkMan, model.LinkInfo, model.WWWInfo, model.IsReadOnly, model.IsLock, model.Remark, model.CompanyID);
                sb.Append(")");
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
        /// ��ȡ�����
        /// </summary>
        /// <param name="code"></param>
        /// <param name="lev">���</param>
        /// <param name="sErrMsg"></param>
        /// <returns></returns>
        public int GetMaxNO(string code, int lev, out string sErrMsg)
        {
            sErrMsg = string.Empty;
            int rtn;
            try
            {
                string sql = string.Format("SELECT syscode FROM tUserUnit WHERE syscode LIKE '{0}' ORDER BY syscode DESC", code
                    + StringUtil.RepeatChar('_', lev));
                object obj = DataBase.GetOneValue(sql, out sErrMsg);
                if (obj == null)
                {
                    rtn = 0;
                }
                else
                {
                    string rightStr = obj.ToString().Substring(obj.ToString().Length - lev, lev);
                    rtn = Convert.ToInt32(rightStr);
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
        /// ͨ�������ȡ����Ŀ���ֶ�ֵ
        /// </summary>
        /// <param name="find">Ŀ���ֶ�</param>
        /// <param name="code">�û���λ����</param>
        /// <returns></returns>
        public static string GetNameFromCode(string find, string code)
        {
            string errMsg = string.Empty;
            if (string.IsNullOrEmpty(code))
            {
                return string.Empty;
            }

            try
            {
                string sql = string.Format("SELECT {0} FROM TUSERUNIT WHERE SYSCODE='{1}' ORDER BY SYSCODE", find, code);
                Object obj = DataBase.GetOneValue(sql, out errMsg);
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
                errMsg = e.Message;
                return null;
            }
        }

        /// <summary>
        /// ͨ�������ȡ����
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
		public static string GetNameFromCode(string code)
		{
			return clsUserUnitOpr.GetNameFromCode("FullName",code);
		}

        /// <summary>
        /// ͨ����׼�����ȡ����
        /// </summary>
        /// <param name="code">��λ����</param>
        /// <returns></returns>
        public static string GetNameFromStandarCode(string code)
        {
            string sErrMsg = string.Empty;
            if (code.Equals(string.Empty))
            {
                return string.Empty;
            }

            try
            {
                string sql = string.Format("SELECT FullName FROM tUserUnit WHERE stdcode='{0}' ORDER BY syscode", code);
                Object obj = DataBase.GetOneValue(sql, out sErrMsg);
                if (obj == null)
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
        /// �ж��Ƿ����
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool ExistCode(string code)
        {
            string errMsg = string.Empty;
            if (code.Equals(string.Empty))
            {
                return false;
            }
            try
            {
                string sql = string.Format("SELECT SYSCODE FROM TUSERUNIT WHERE SYSCODE='{0}'", code);
                object obj = DataBase.GetOneValue(sql, out errMsg);
                if (obj == null)
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
                return false;
            }
        }
        /// <summary>
        /// ��ȡ��׼����
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetStdCode(string code)
        {
            string errMsg = string.Empty;
            if (code.Equals(string.Empty))
            {
                return string.Empty;
            }

            try
            {
                string sql = string.Format("SELECT STDCODE FROM TUSERUNIT WHERE SYSCODE='{0}'", code);
                object obj = DataBase.GetOneValue(sql, out errMsg);
                if (obj == null)
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
                errMsg = e.Message;
                return string.Empty;
            }
        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <returns></returns>
        public string GetCheckPointType()
        {
            string errMsg = string.Empty;

            try
            {
                string sql = "SELECT SHORTCUT FROM TUSERUNIT ORDER BY SYSCODE";
                object obj = DataBase.GetOneValue(sql, out errMsg);
                if (obj == null)
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
                errMsg = e.Message;
                return string.Empty;
            }
        }

        /// <summary>
        /// ��ȡ�������ݼ�
        /// </summary>
        /// <param name="pType"></param>
        /// <returns></returns>
        public DataTable GetParents(out int pType)
        {
            string errMsg = string.Empty;
            pType = 0;
            DataTable dt = null;

            try
            {
                string sql = "SELECT DistrictCode FROM TUSERUNIT ORDER BY SYSCODE";
                object obj = DataBase.GetOneValue(sql, out errMsg);
                if (obj == null)
                {
                    pType = -1;
                }
                else
                {
                    string selectSql = "SELECT Name AS DistrictName,syscode FROM tDistrict WHERE syscode LIKE '" + obj.ToString() + "___'"
                        + " And IsReadOnly = TRUE And IsLock = FALSE";
                    string[] cmd = new string[1] { selectSql };
                    string[] names = new string[1] { "DistrictName" };
                    dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["DistrictName"];
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dt;
        }
	}
}

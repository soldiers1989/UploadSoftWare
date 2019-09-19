using System;
using System.Data;
using System.Text;

namespace DYSeriesDataSet
{
    /// <summary>
    /// ��������
    /// </summary>
    public class clsDistrictOpr
    {
        public clsDistrictOpr()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        StringBuilder sql = new StringBuilder();
        /// <summary>
        /// �����޸ı���
        /// </summary>
        /// <param name="model">����clsDistrict��һ��ʵ������</param>
        /// <returns></returns>
        public int UpdatePart(clsDistrict model, int lev, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                sql.Length = 0;
                sql.Append("UPDATE tDistrict SET ");
                sql.Append(" StdCode='");
                sql.Append(model.StdCode);
                sql.Append("'");
                sql.Append(",Name='");
                sql.Append(model.Name);
                sql.Append("'");
                sql.Append(",ShortCut='");
                sql.Append(model.ShortCut);
                sql.Append("'");
                sql.Append(",DistrictIndex=");
                sql.Append(model.DistrictIndex);
                sql.Append(",CheckLevel='");
                sql.Append(model.CheckLevel);
                sql.Append("'");
                sql.Append(",IsLocal=");
                sql.Append(model.IsLocal);
                sql.Append(",IsReadOnly=");
                sql.Append(model.IsReadOnly);
                sql.Append(",IsLock=");
                sql.Append(model.IsLock);
                sql.Append(",Remark='");
                sql.Append(model.Remark);
                sql.Append("' WHERE SysCode='");
                sql.Append(model.SysCode);
                sql.Append("'");
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;

                //string updateSql="update tDistrict set "
                //+ "StdCode='" + model.StdCode + "'," 
                //+ "Name='" + model.Name + "'," 
                //+ "ShortCut='" + model.ShortCut + "'," 
                //+ "DistrictIndex=" + model.DistrictIndex + "," 
                //+ "CheckLevel='" + model.CheckLevel + "',"
                //+ "IsLocal=" + model.IsLocal + ","
                //+ "IsReadOnly=" + model.IsReadOnly + "," 
                //+ "IsLock=" + model.IsLock + "," 
                //+ "Remark='" + model.Remark + "'" 
                //+ " where SysCode='" + model.SysCode + "' ";
                //DataBase.ExecuteCommand(updateSql,out sErrMsg);

                if (model.IsLocal)
                {
                    sql.Append("UPDATE tDistrict SET ");
                    sql.Append(" IsLocal=");
                    sql.Append(model.IsLocal);
                    sql.Append(" WHERE SysCode LIKE '");
                    sql.Append(model.SysCode);
                    sql.Append("%'");
                    DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                    sql.Length = 0;
                    //string sql="update tDistrict set "
                    //    + "IsLocal=" + model.IsLocal 
                    //    + " where SysCode like '" + model.SysCode + "%'";
                    //DataBase.ExecuteCommand(sql,out sErrMsg);
                }

                rtn = 1;
            }
            catch (Exception e)
            {
                //Log.WriteLog("����clsDistrict",e);
                errMsg = e.Message;
            }

            return rtn;
        }
        /// <summary>
        /// ɾ��
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
                sql.Length = 0;
                sql.Append("DELETE FROM TDISTRICT");

                if (!whereSql.Equals(""))
                {
                    sql.Append(" WHERE ");
                    sql.Append(whereSql);
                }
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);

                rtn = 1;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }

            return rtn;
        }

        /// <summary>
        /// �����������ɾ����¼
        /// </summary>
        /// <param name="%pkname%">�������</param>
        /// <returns></returns>
        public int DeleteByPrimaryKey(string mainkey, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                string deleteSql = "DELETE FROM TDISTRICT WHERE SYSCODE='" + mainkey + "' ";
                DataBase.ExecuteCommand(deleteSql, out errMsg);
                rtn = 1;
            }
            catch (Exception e)
            {
                //Log.WriteLog("ͨ������ɾ��clsDistrict",e);
                errMsg = e.Message; ;
            }

            return rtn;
        }

        /// <summary>
        /// ���ݲ�ѯ��������ѯ��¼
        /// </summary>
        /// <param name="whereSql">��ѯ������,����Where</param>
        /// <param name="orderBySql">����,����Order</param>
        /// <returns></returns>
        public DataTable GetAsDataTable(string whereSql, string orderBySql, int type)
        {
            string errMsg = string.Empty;
            DataTable dt = null;

            try
            {
                sql.Length = 0;
                if (type == 0)
                {
                    sql.Append("SELECT SysCode,StdCode,Name,ShortCut,DistrictIndex,CheckLevel,");
                    sql.Append("IsLocal,IsLock,IsReadOnly,Remark");
                    sql.Append(" FROM tDistrict");
                }
                else if (type == 1)
                {
                    sql.Append("SELECT SysCode,Name,StdCode  FROM tDistrict");
                }

                if (!whereSql.Equals(""))
                {
                    sql.Append(" WHERE ");
                    sql.Append(whereSql);
                }
                if (!orderBySql.Equals(""))
                {
                    sql.Append(" ORDER BY ");
                    sql.Append(orderBySql);
                }
                string[] cmd = new string[1] { sql.ToString() };
                string[] names = new string[1] { "District" };
                dt = DataBase.GetDataSet(cmd, names, out errMsg).Tables["District"];
            }
            catch (Exception e)
            {
                //Log.WriteLog("��ѯclsDistrict",e);
                errMsg = e.Message;
            }

            return dt;
        }

        /// <summary>
        /// ����һ����ϸ��¼
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(clsDistrict model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {
                sql.Length = 0;
                sql.Append("INSERT INTO tDistrict(SysCode,StdCode,Name,ShortCut,DistrictIndex,CheckLevel,IsLocal,IsReadOnly,IsLock,Remark)");
                sql.Append(" VALUES(");
                sql.AppendFormat("'{0}',", model.SysCode);
                sql.AppendFormat("'{0}',", model.StdCode);
                sql.AppendFormat("'{0}',", model.Name);
                sql.AppendFormat("'{0}',", model.ShortCut);
                sql.AppendFormat("{0},", model.DistrictIndex);
                sql.AppendFormat("'{0}',", model.CheckLevel);
                sql.AppendFormat("{0},", model.IsLocal);
                sql.AppendFormat("{0},", model.IsReadOnly);
                sql.AppendFormat("{0},", model.IsLock);
                sql.AppendFormat("'{0}'", model.Remark);
                sql.Append(")");
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;

                rtn = 1;
            }
            catch (Exception e)
            {
                //Log.WriteLog("���clsDistrict",e);
                errMsg = e.Message;
            }

            return rtn;
        }
        /// <summary>
        /// ��ȡ�����
        /// </summary>
        /// <param name="code"></param>
        /// <param name="lev"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public int GetMaxNO(string code, int lev, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn;

            try
            {
                sql.Length = 0;
                sql.Append("SELECT SYSCODE FROM TDISTRICT WHERE SYSCODE LIKE");
                sql.AppendFormat(" '{0}'", code);
                sql.Append(" ORDER BY SYSCODE DESC");
                Object o = DataBase.GetOneValue(sql.ToString(), out errMsg);
                sql.Length = 0;
                if (o == null)
                {
                    rtn = 0;
                }
                else
                {
                    string rightStr = o.ToString().Substring(o.ToString().Length - lev, lev);
                    rtn = Convert.ToInt32(rightStr);
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
        /// �ж��Ƿ����ɾ��
        /// </summary>
        /// <param name="code"></param>
        /// <param name="lev"></param>
        /// <returns></returns>
        public bool CanDelete(string code, int lev)
        {
            string errMsg = string.Empty;

            try
            {
                sql.Length = 0;
                sql.Append("SELECT SYSCODE FROM TDISTRICT WHERE SYSCODE LIKE ");
                sql.AppendFormat("'{0}'", code);
                sql.Append(" ORDER BY SYSCODE DESC");
                Object o = DataBase.GetOneValue(sql.ToString(), out errMsg);
                if (o == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return false;
            }
        }

        /// <summary>
        /// �ж��Ƿ����ɾ��
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool CanDelete(string code)
        {
            string errMsg = string.Empty;

            try
            {
                sql.Length = 0;
                sql.Append("SELECT TDISTRICT.SYSCODE FROM TDISTRICT,TCOMPANY");
                sql.AppendFormat(" WHERE tDistrict.SysCode=tCompany.DistrictCode and tDistrict.syscode='{0}'", code);
                sql.Append(" ORDER BY TDISTRICT.SYSCODE DESC");
                Object o = DataBase.GetOneValue(sql.ToString(), out errMsg);
                sql.Length = 0;
                sql.Append("SELECT TDISTRICT.SYSCODE FROM TDISTRICT,TUSERUNIT");
                sql.AppendFormat(" WHERE TDISTRICT.SYSCODE=TUSERUNIT.CHECKITEMCODE AND TDISTRICT.SYSCODE='{0}'", code);
                sql.Append(" ORDER BY TDISTRICT.SYSCODE DESC");
                Object o2 = DataBase.GetOneValue(sql.ToString(), out errMsg);
                sql.Length = 0;
                if (o == null && o2 == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return false;
            }
        }

        public static string NameFromCode(string code)
        {
            string errMsg = string.Empty;
            if (code.Equals(""))
            {
                return string.Empty;
            }

            try
            {
                string sql = string.Format("SELECT NAME FROM TDISTRICT WHERE SYSCODE='{0}' ORDER BY SYSCODE", code);
                Object obj = DataBase.GetOneValue(sql, out errMsg);
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
                return null;
            }
        }

        /// <summary>
        /// ��ȡ������������ַ���
        /// </summary>
        /// <param name="code"></param>
        /// <param name="lev"></param>
        /// <returns></returns>
        public static string LevelNamesFromCode(string code, int lev)
        {
            string errMsg = string.Empty;
            string ret = "";
            if (code.Equals(""))
            {
                return string.Empty;
            }

            try
            {
                int mod = code.Length / lev;
                for (int i = 1; i < mod; i++)
                {
                    if (i > 1)
                    {
                        //ret += ShareOption.SplitStr;
                    }

                    ret += clsDistrictOpr.NameFromCode(code.Substring(0, lev * i));
                }
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }

            return ret;
        }

        /// <summary>
        /// �ж��Ƿ����
        /// </summary>
        /// <param name="strCode"></param>
        /// <returns></returns>
        public static bool DistrictIsMX(string strCode)
        {
            string errMsg = string.Empty;
            if (strCode.Equals(""))
            {
                return false;
            }

            try
            {//
                string sql = string.Format("SELECT SYSCODE FROM TDISTRICT WHERE SYSCODE='{0}' AND SYSCODE NOT IN (SELECT SYSCODE FROM TDISTRICT AS A WHERE EXISTS (SELECT SYSCODE FROM TDISTRICT  WHERE LEFT(SYSCODE, LEN(A.SYSCODE)) = A.SYSCODE AND SYSCODE <> A.SYSCODE))", strCode);
                //string sql = string.Format("SELECT SYSCODE FROM TDISTRICT WHERE LEN(SYSCODE) < {0}", strCode.Length);
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
                return false;
            }
        }
        /// <summary>
        /// �ж��Ƿ����
        /// </summary>
        /// <param name="swhere"></param>
        /// <returns></returns>
        public static bool DistrictIsExist(string swhere)
        {
            string sErrMsg = string.Empty;
            if (swhere.Equals(""))
            {
                return false;
            }

            try
            {
                string sql = "SELECT SYSCODE FROM TDISTRICT WHERE " + swhere;
                Object o = DataBase.GetOneValue(sql, out sErrMsg);
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
                sErrMsg = e.Message;
                return false;
            }
        }
    }
}

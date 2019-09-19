//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace DYSeriesDataSet.DataSentence
//{
//    class clsTaskOpr
//    {
//    }
//}
using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;


namespace DYSeriesDataSet
{
    /// <summary>
    /// 食品类别操作类
    /// </summary>
    public class clsTaskOpr
    {
        public clsTaskOpr()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        private StringBuilder sb = new StringBuilder();
        /// <summary>
        /// 部分修改保存
        /// </summary>
        /// <param name="model">对象clsTask的一个实例参数</param>
        /// <returns></returns>
        public int UpdatePart(clsTask model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;

            try
            {

                sb.Length = 0;
                sb.AppendFormat("UPDATE tTask SET CPTITLE='{0}'", model.CPTITLE);
                //sb.AppendFormat(",Name='{0}'", model.Name);
                //sb.AppendFormat(",ShortCut='{0}'", model.ShortCut);
                //sb.AppendFormat(",CheckLevel='{0}'", model.CheckLevel);
                //sb.AppendFormat(",CheckItemCodes='{0}'", model.CheckItemCodes);
                //sb.AppendFormat(",CheckItemValue='{0}'", model.CheckItemValue);
                //sb.Append(",IsReadOnly=");
                //sb.Append(model.IsReadOnly);
                //sb.Append(",IsLock=");
                //sb.Append(model.IsLock);
                //sb.AppendFormat(",Remark='{0}'", model.Remark);
                //sb.AppendFormat(",FoodProperty='{0}'", model.FoodProperty);
                sb.AppendFormat(" WHERE CPCODE='{0}'", model.CPCODE);
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
        /// 删除操作 tTask
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
                string deleteSql = "DELETE FROM tTask";

                if (!whereSql.Equals(string.Empty))
                {
                    deleteSql += string.Format(" WHERE {0} ", whereSql);
                }
                DataBase.ExecuteCommand(deleteSql, out errMsg);
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
                string deleteSql = string.Format("DELETE FROM tTask WHERE CPCODE='{0}' ", mainkey);
                DataBase.ExecuteCommand(deleteSql, out errMsg);

                rtn = 1;
            }
            catch (Exception e)
            {
                //Log.WriteLog("通过主键删除clsTask",e);
                errMsg = e.Message; ;
            }

            return rtn;
        }

        /// <summary>
        /// 根据样品名称|样品编号查找样品
        /// </summary>
        /// <param name="SampleName">样品名称</param>
        /// <param name="Name">项目名称</param>
        /// <param name="IsPreciseQuery">true 精确查找 | false 模糊查找</param>
        /// <returns></returns>
        public DataTable GetSampleByNameOrCode(string SampleName, string Name, bool IsPreciseQuery, bool IsFirst, int type)
        {
            string errMsg = string.Empty;
            DataTable dtbl = null;
            try
            {
                sb.Length = 0;
                if (type == 1)
                {
                    //if (IsFirst)
                    //{
                    //    sb.Append("Select Top 200 ID,FtypeNmae,SampleNum,Name,ItemDes,StandardValue,Demarcate,Unit From ttStandDecide ");
                    //}
                    //else
                    //{
                    sb.Append("Select ID,FtypeNmae,SampleNum,Name,ItemDes,StandardValue,Demarcate,Unit From ttStandDecide ");
                    //}
                    if (!SampleName.Equals(string.Empty) && !Name.Equals(string.Empty))
                    {
                        sb.AppendFormat(" WHERE FtypeNmae Like '%{0}%'", SampleName);
                        sb.AppendFormat(IsPreciseQuery ? " AND Name Like '{0}'" : " AND Name Like '%{0}%'", Name);
                    }
                    else if (SampleName.Equals(string.Empty) && !Name.Equals(string.Empty))
                    {
                        sb.AppendFormat(IsPreciseQuery ? " WHERE Name Like '{0}'" : " WHERE Name Like '%{0}%'", Name);
                    }
                    else if (!SampleName.Equals(string.Empty) && Name.Equals(string.Empty))
                    {
                        sb.AppendFormat(" WHERE FtypeNmae Like '%{0}%'", SampleName);
                    }
                }
                else if (type == 2)
                {
                    sb.AppendFormat("Select Top 1 ID,FtypeNmae,SampleNum,Name,ItemDes,StandardValue,Demarcate,Unit,UDate From ttStandDecide Where SampleNum Like'{0}'", SampleName);
                }
                else if (type == 3)
                {
                    sb.Append("Select ID,itemNmae,itemCode From ZJCheckitem ");
                    
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "tStandDecide" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["tStandDecide"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dtbl;
        }

        /// <summary>
        /// 根据项目名称查找相关检测项目
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public DataTable GetProjectByName(string Name)
        {
            string errMsg = string.Empty;
            DataTable dtbl = null;
            try
            {
                sb.Length = 0;
                sb.Append("Select SysCode,StdCode,ItemDes,CheckType,StandardCode,StandardValue,Unit,DemarcateInfo From tCheckItem ");
                if (!Name.Equals(string.Empty))
                {
                    sb.Append(" Where ItemDes Like '%");
                    sb.Append(Name + "%'");
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "tCheckItem" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["tCheckItem"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
            }
            return dtbl;
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
            DataTable dtbl = null;

            try
            {
                sb.Length = 0;

                if (type == 0)
                {
                    sb.Append("SELECT CPCODE,CPTITLE,CPSDATE,CPEDATE,CPTPROPERTY,");
                    sb.Append("CPFROM,CPEDITOR,CPPORGID,CPPORG,CPEDDATE,CPMEMO,PLANDETAIL,PLANDCOUNT FROM tTask");
                }
                else if (type == 1)
                {
                    sb.Append("SELECT * FROM tTask");
                }
                else if (type == 2)
                {
                    sb.Append("SELECT CPCODE,CPTITLE  FROM tTask");
                }
                if (type == 3)
                {
                    // SELECT CPCODE,CPTITLE,CPSDATE,CPEDATE,CPTPROPERTY,CPFROM,CPEDITOR,CPPORGID,CPPORG,CPEDDATE,PLANDETAIL,
                    //PLANDCOUNT,BAOJINGTIME,(SELECT COUNT(*) FROM ttResultSecond WHERE CheckPlanCode=m.CPCODE ) AS v30,'未完成' as finish
                    // FROM tTask as m where (cdate(BAOJINGTIME)>=#2015-10-26 22:21:23#)
                    sb.Append(" SELECT CPCODE,CPTITLE,CPSDATE,CPEDATE,CPTPROPERTY,CPFROM,CPEDITOR,CPPORGID,CPPORG,CPEDDATE,PLANDETAIL,");
                    sb.Append("PLANDCOUNT,BAOJINGTIME,(SELECT COUNT(*) FROM ttResultSecond WHERE CheckPlanCode=m.CPCODE ) AS v30,'未完成' as finish");
                    sb.Append(",(v30/PLANDCOUNT) as Num  FROM tTask as m");
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
                    sb.Append(" ASC ");
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "task" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["task"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                //Log.WriteLog("查询clsTask",e);
                errMsg = e.Message;
            }
            return dtbl;
        }

        /// <summary>
        /// 根据任务编号查询任务名称
        /// </summary>
        /// <param name="code">编号</param>
        /// <returns></returns>
        public string SearchTaskNameByCode(string code)
        {

            string errMsg = string.Empty;
            DataTable dtbl = null;

            try
            {
                sb.Length = 0;
                sb.Append("SELECT CPTITLE FROM tTask");
                if (!code.Equals(string.Empty))
                {
                    sb.Append(" WHERE CPCODE Like '%" + code + "%'");
                }
                string[] cmd = new string[1] { sb.ToString() };
                string[] names = new string[1] { "task" };
                dtbl = DataBase.GetDataSet(cmd, names, out errMsg).Tables["task"];
                sb.Length = 0;
            }
            catch (Exception e)
            {
                //Log.WriteLog("查询clsTask",e);
                errMsg = e.Message;
            }
            return string.Empty;
        }

        /// <summary>
        /// 插入一条明细记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(clsTask model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sb.Length = 0;
                sb.Append("Insert Into tTask");
                sb.Append("(CPCODE,CPTITLE,CPSDATE,CPEDATE,CPTPROPERTY,CPFROM,CPEDITOR");
                sb.Append(",CPPORGID,CPPORG,CPEDDATE,CPMEMO,PLANDETAIL,BAOJINGTIME,PLANDCOUNT)");
                sb.Append("VALUES(");
                sb.AppendFormat("'{0}',", model.CPCODE);
                sb.AppendFormat("'{0}',", model.CPTITLE);
                sb.AppendFormat("'{0}',", model.CPSDATE);
                sb.AppendFormat("'{0}',", model.CPEDATE);
                sb.AppendFormat("'{0}',", model.CPTPROPERTY);
                sb.AppendFormat("'{0}',", model.CPFROM);
                sb.AppendFormat("'{0}',", model.CPEDITOR);
                sb.AppendFormat("'{0}',", model.CPPORGID);
                sb.AppendFormat("'{0}',", model.CPPORG);
                sb.AppendFormat("'{0}',", model.CPEDDATE);
                sb.AppendFormat("'{0}',", model.CPMEMO);
                sb.AppendFormat("'{0}',", model.PLANDETAIL);
                sb.AppendFormat("'{0}',", model.BAOJINGTIME);
                sb.AppendFormat("'{0}'", model.PLANDCOUNT);
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
        /// 任务更新 修改|新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int InsertOrUpdate(clsTask model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            try
            {
                sb.Length = 0;
                DataTable dt = new clsCompanyOpr().GetAsDataTable("CPCODE='" + model.CPCODE + "'", string.Empty, 11);
                if (dt != null && dt.Rows.Count > 0)
                {
                    sb.AppendFormat("Update tTask Set CPCODE='{0}',CPTITLE='{1}',CPSDATE='{2}',CPEDATE='{3}',CPTPROPERTY='{4}',",
                        model.CPCODE, model.CPTITLE, model.CPSDATE, model.CPEDATE, model.CPTPROPERTY);
                    sb.AppendFormat("CPFROM='{0}',CPEDITOR='{1}',CPPORGID='{2}',CPPORG='{3}',CPEDDATE='{4}',",
                        model.CPFROM, model.CPEDITOR, model.CPPORGID, model.CPPORG, model.CPEDDATE);
                    sb.AppendFormat("CPMEMO='{0}',PLANDETAIL='{1}',PLANDCOUNT='{2}',BAOJINGTIME='{3}',UDate='{4}' Where CPCODE='{5}'",
                        model.CPMEMO, model.PLANDCOUNT, model.PLANDCOUNT, model.BAOJINGTIME, model.UDate, model.CPCODE);
                }
                else
                {
                    sb.Append("Insert Into tTask");
                    sb.Append("(CPCODE,CPTITLE,CPSDATE,CPEDATE,CPTPROPERTY,CPFROM,CPEDITOR");
                    sb.Append(",CPPORGID,CPPORG,CPEDDATE,CPMEMO,PLANDETAIL,BAOJINGTIME,PLANDCOUNT,UDate)");
                    sb.Append("VALUES(");
                    sb.AppendFormat("'{0}',", model.CPCODE);
                    sb.AppendFormat("'{0}',", model.CPTITLE);
                    sb.AppendFormat("'{0}',", model.CPSDATE);
                    sb.AppendFormat("'{0}',", model.CPEDATE);
                    sb.AppendFormat("'{0}',", model.CPTPROPERTY);
                    sb.AppendFormat("'{0}',", model.CPFROM);
                    sb.AppendFormat("'{0}',", model.CPEDITOR);
                    sb.AppendFormat("'{0}',", model.CPPORGID);
                    sb.AppendFormat("'{0}',", model.CPPORG);
                    sb.AppendFormat("'{0}',", model.CPEDDATE);
                    sb.AppendFormat("'{0}',", model.CPMEMO);
                    sb.AppendFormat("'{0}',", model.PLANDETAIL);
                    sb.AppendFormat("'{0}',", model.BAOJINGTIME);
                    sb.AppendFormat("'{0}',", model.PLANDCOUNT);
                    sb.AppendFormat("'{0}'", model.UDate);
                    sb.Append(")");
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
        /// 获取最大值
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
                string sql = string.Format("SELECT CPCODE FROM tTask WHERE CPCODE LIKE '{0}' ORDER BY CPCODE DESC", code);
                Object o = DataBase.GetOneValue(sql, out errMsg);
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
        /// 通过代码获取名称
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string NameFromCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return string.Empty;
            }
            string errMsg = string.Empty;


            try
            {
                string sql = string.Format("SELECT CPTITLE FROM tTask WHERE CPCODE='{0}' ORDER BY CPCODE", code);
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
        /// 通过食品类别获取检测项目编号
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        //public static string CheckItemsFromCode(string code)
        //{
        //    string sErrMsg = string.Empty;
        //    if (code.Equals(string.Empty))
        //    {
        //        return string.Empty;
        //    }

        //    try
        //    {
        //        string sql = string.Format("select CheckItemCodes from tFoodClass where CPCODE='{0}' order by CPCODE", code);
        //        Object o = DataBase.GetOneValue(sql, out sErrMsg);
        //        if (o == null)
        //        {
        //            return string.Empty;
        //        }
        //        else
        //        {
        //            return o.ToString();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        sErrMsg = e.Message;
        //        return null;
        //    }
        //}

        public string errMsg { get; set; }
    }
}
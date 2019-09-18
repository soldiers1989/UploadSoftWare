using System;
using System.Collections.Generic;
using System.Text;
using DY.FoodClientLib;

namespace FoodClient.AnHui
{
    public static class DataOperation
    {
        private static StringBuilder sql = new StringBuilder();
        public static int Del(string type, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            sql.Length = 0;
            string table = string.Empty;
            try
            {
                if (type.Equals("data_dictionary"))
                {
                    table = "ah_data_dictionary";
                }
                else if (type.Equals("checked_unit"))
                {
                    table = "ah_checked_unit";
                }
                else if (type.Equals("standard_limit"))
                {
                    table = "ah_standard_limit";
                }
                else if (type.Equals("Company"))
                {
                    table = "tCompany";
                }

                sql.AppendFormat("Delete From {0}", table);
                DataBase.ExecuteCommand(sql.ToString());
                sql.Length = 0;
                rtn = 1;
            }
            catch (Exception ex)
            {
                rtn = 0;
                errMsg = ex.Message;
            }
            return rtn;
        }

        public static int Insert(data_dictionary model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            sql.Length = 0;
            try
            {
                sql.Append("Insert Into ah_data_dictionary ");
                sql.Append("(ID,name,codeId,typeNum,pid,remark,status) ");
                sql.AppendFormat("VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                    model.id, model.name, model.codeId, model.typeNum, model.pid, model.remark, model.status);
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;
                rtn = 1;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                rtn = 0;
            }
            return rtn;
        }

        public static int Insert(standard_limit model, out string errMsg)
        {
            errMsg = string.Empty;
            int rtn = 0;
            sql.Length = 0;
            try
            {
                sql.Append("Insert Into ah_standard_limit ");
                sql.Append("(ID,foodType,testItem,testBasis,decisionBasis,minLimit,maxLimit,unit) ");
                sql.AppendFormat("VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                    model.id, model.foodType, model.testItem, model.testBasis, model.decisionBasis,
                    model.minLimit, model.maxLimit, model.unit);
                DataBase.ExecuteCommand(sql.ToString(), out errMsg);
                sql.Length = 0;
                rtn = 1;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                rtn = 0;
            }
            return rtn;
        }

    }
}

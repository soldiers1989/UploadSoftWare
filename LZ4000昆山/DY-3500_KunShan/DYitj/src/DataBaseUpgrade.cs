using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIO.src
{
    /// <summary>
    /// Note：数据库升级列表
    /// Creater：wenj
    /// Time：2018/1/25 15:54:06
    /// Version：V1.0.0
    /// </summary>
    public class DataBaseUpgrade
    {
        public static List<string> getSql()
        {
            List<string> sqlList = new List<string>();
            sqlList.Add("create table ks_AreaMarket(SysCode varchar(255) identity(1,1) PRIMARY KEY)");//创建表
            sqlList.Add("alter table ks_AreaMarket add LicenseNo varchar(255)");//新增列
            sqlList.Add("alter table ks_AreaMarket add MarketName varchar(255)");
            sqlList.Add("alter table ks_AreaMarket add Abbreviation varchar(255)");
            sqlList.Add("alter table ks_AreaMarket add MarketRef varchar(255)");
            sqlList.Add("alter table Ks_Business add LicenseNo varchar(255)");
            sqlList.Add("alter table ttResultSecond add MarketType varchar(255)");
            return sqlList;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace AIO.src
{
    /// <summary>
    /// 昆山帮助类
    /// Create Wenj
    /// Time 2017年11月8日
    /// </summary>
    public static class KunShanHelper
    {

        static StringBuilder xmlStr = new StringBuilder();
        static string response = string.Empty;
        static sDataInfrace.sDataInfrace service = new sDataInfrace.sDataInfrace();

        /// <summary>
        /// 获取令牌 调用接口前获取令牌，接口调用结束后注销令牌
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool GetToken(out string errMsg)
        {
            if (Global.KsTokenNo.Length > 0) KunShanHelper.LogoutToken(out errMsg);

            errMsg = string.Empty;
            KunShanEntity.GetTokenRequest.webService getTokenRequest = new KunShanEntity.GetTokenRequest.webService();
            getTokenRequest.request = new KunShanEntity.GetTokenRequest.Request();
            getTokenRequest.request.name = Global.KsUser;
            getTokenRequest.request.password = Global.MD5(Global.KsPwd);
            response = XmlHelper.EntityToXml<KunShanEntity.GetTokenRequest.webService>(getTokenRequest);
            System.Console.WriteLine(string.Format("checkIn-Request:{0}", response));
            response = service.checkIn(response);
            System.Console.WriteLine(string.Format("checkIn-Response:{0}", response));
            KunShanEntity.GetTokenResponse.webService getTokenResponse = XmlHelper.XmlToEntity<KunShanEntity.GetTokenResponse.webService>(response);
            if (getTokenResponse != null && getTokenResponse.response.error.Length == 0 &&
                getTokenResponse.response.tokenNo.Length > 0)
            {
                Global.KsTokenNo = getTokenResponse.response.tokenNo;
                return true;
            }
            else if (getTokenResponse.response.error.Length > 0)
            {
                errMsg = getTokenResponse.response.error;
            }

            return false;
        }

        /// <summary>
        /// 注销令牌 调用接口结束后需要注销令牌
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static bool LogoutToken(out string errMsg)
        {
            errMsg = string.Empty;
            KunShanEntity.LogoutTokenRequest.webService logoutTokenRequest = new KunShanEntity.LogoutTokenRequest.webService();
            logoutTokenRequest.request = new KunShanEntity.LogoutTokenRequest.Request();
            logoutTokenRequest.request.tokenNo = Global.KsTokenNo;
            response = XmlHelper.EntityToXml<KunShanEntity.LogoutTokenRequest.webService>(logoutTokenRequest);
            System.Console.WriteLine(string.Format("checkOut-Request:{0}", response));
            response = service.checkOut(response);
            System.Console.WriteLine(string.Format("checkOut-Response:{0}", response));
            KunShanEntity.LogoutTokenResponse.webService logoutTokenResponse =
                XmlHelper.XmlToEntity<KunShanEntity.LogoutTokenResponse.webService>(response);
            if (logoutTokenResponse != null && logoutTokenResponse.response.error.Length == 0 &&
                logoutTokenResponse.response.ResultCode.Equals("1000"))
            {
                Global.KsTokenNo = errMsg = "";
                return true;
            }
            else if (logoutTokenResponse.response.error.Length > 0)
            {
                errMsg = logoutTokenResponse.response.error;
            }

            return false;
        }

        /// <summary>
        /// 获取营业执照相关信息
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static void GetMarket(out string errMsg)
        {
            try
            {
                errMsg = string.Empty;
                KunShanEntity.QueryMarketRequest.webService getQueryMarket = new KunShanEntity.QueryMarketRequest.webService();
                getQueryMarket.request = new KunShanEntity.QueryMarketRequest.Request();
                getQueryMarket.request.name = Global.KsUser;
                getQueryMarket.request.password = Global.MD5(Global.KsPwd);
                response = XmlHelper.EntityToXml<KunShanEntity.QueryMarketRequest.webService>(getQueryMarket);
                System.Console.WriteLine(string.Format("QueryMarket-Request:{0}", response));
                response = service.QueryMarket(response);
                System.Console.WriteLine(string.Format("QueryMarket-Response:{0}", response));
                System.Console.WriteLine(response);
                errMsg = response;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
        }

        public static string Upload(string xml, out string errMsg)
        {
            errMsg = string.Empty;
            response = string.Empty;
            try
            {
                response = service.saveQuickCheckItemInfo(xml);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }

            return response;
        }


        public static string AreaMarket = string.Empty;
        /// <summary>
        /// 分局辖区内的单位主体下载 - 分局版本
        /// </summary>
        /// <param name="errMsg"></param>
        public static void GetAreaMarket(out string errMsg)
        {
            try
            {
                AreaMarket = errMsg = string.Empty;
                KunShanEntity.GetTokenRequest.webService querySignContact = new KunShanEntity.GetTokenRequest.webService();
                querySignContact.request = new KunShanEntity.GetTokenRequest.Request();
                querySignContact.request.name = Global.KsUser;
                querySignContact.request.password = Global.MD5(Global.KsPwd);
                response = XmlHelper.EntityToXml<KunShanEntity.GetTokenRequest.webService>(querySignContact);
                System.Console.WriteLine(string.Format("GetAreaMarket-Request:{0}", response));
                AreaMarket = service.GetAreaMarket(response);
                System.Console.WriteLine(string.Format("GetAreaMarket-Response:{0}", AreaMarket));
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
        }

        /// <summary>
        /// 根据单位主体信息获取经营户 - 市场版本
        /// </summary>
        public static string AreaSignContact = string.Empty;
        public static string GetAreaSignContact(string no, out string errMsg)
        {
            try
            {
                AreaSignContact = errMsg = string.Empty;
                KunShanEntity.GetAreaSignContactReqyest.webService getAreaSignContactReqyest = new KunShanEntity.GetAreaSignContactReqyest.webService();
                getAreaSignContactReqyest.request = new KunShanEntity.GetAreaSignContactReqyest.Request();
                getAreaSignContactReqyest.request.LicenseNo = no;
                getAreaSignContactReqyest.head = new KunShanEntity.GetAreaSignContactReqyest.Head();
                getAreaSignContactReqyest.head.name = Global.KsUser;
                getAreaSignContactReqyest.head.password = Global.MD5(Global.KsPwd);
                response = XmlHelper.EntityToXml<KunShanEntity.GetAreaSignContactReqyest.webService>(getAreaSignContactReqyest);
                System.Console.WriteLine(string.Format("GetAreaSignContactReqyest-Request:{0}", response));
                AreaSignContact = service.GetAreaSignContact(response);
                System.Console.WriteLine(string.Format("GetAreaSignContactReqyest-Response:{0}", AreaSignContact));
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }

            return AreaSignContact;
        }

        /// <summary>
        /// 经营户下载 - 市场版本
        /// </summary>
        public static string SignContact = string.Empty;
        public static void QuerySignContact(out string errMsg)
        {
            try
            {
                SignContact = errMsg = string.Empty;
                KunShanEntity.GetTokenRequest.webService querySignContact = new KunShanEntity.GetTokenRequest.webService();
                querySignContact.request = new KunShanEntity.GetTokenRequest.Request();
                querySignContact.request.name = Global.KsUser;
                querySignContact.request.password = Global.MD5(Global.KsPwd);
                response = XmlHelper.EntityToXml<KunShanEntity.GetTokenRequest.webService>(querySignContact);
                System.Console.WriteLine(string.Format("QuerySignContact-Request:{0}", response));
                SignContact = service.QuerySignContact(response);
                System.Console.WriteLine(string.Format("QuerySignContact-Response:{0}", SignContact));
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
        }

        /// <summary>
        /// 检测项目
        /// </summary>
        public static string CheckItem = string.Empty;
        public static void QueryCheckItem(out string errMsg)
        {
            try
            {
                CheckItem = errMsg = string.Empty;
                KunShanEntity.CheckItemRequest.webService checkItem = new KunShanEntity.CheckItemRequest.webService();
                checkItem.head = new KunShanEntity.CheckItemRequest.Head();
                checkItem.head.name = Global.KsUser;
                checkItem.head.password = Global.MD5(Global.KsPwd);
                checkItem.request = new KunShanEntity.CheckItemRequest.Request();
                checkItem.request.Id = "0";
                checkItem.request.UpdateDate = "";//DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss");
                response = XmlHelper.EntityToXml<KunShanEntity.CheckItemRequest.webService>(checkItem);
                System.Console.WriteLine(string.Format("QueryCheckItem-Request:{0}", response));
                CheckItem = service.QueryCheckItem(response);
                System.Console.WriteLine(string.Format("QueryCheckItem-Response:{0}", CheckItem));
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
        }

        /// <summary>
        /// 样品信息
        /// </summary>
        public static string SalesItem = string.Empty;
        public static void QuerySalesItem(out string errMsg)
        {
            try
            {
                SalesItem = errMsg = string.Empty;
                KunShanEntity.SalesItemRequest.webService salesItem = new KunShanEntity.SalesItemRequest.webService();
                salesItem.head = new KunShanEntity.SalesItemRequest.Head();
                salesItem.head.name = Global.KsUser;
                salesItem.head.password = Global.MD5(Global.KsPwd);
                salesItem.request = new KunShanEntity.SalesItemRequest.Request();
                salesItem.request.Id = "0";
                salesItem.request.UpdateDate = "";//DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss");
                response = XmlHelper.EntityToXml<KunShanEntity.SalesItemRequest.webService>(salesItem);
                System.Console.WriteLine(string.Format("QuerySalesItem-Resquest:{0}", response));
                SalesItem = service.QuerySalesItem(response);
                System.Console.WriteLine(string.Format("QuerySalesItem-Response:{0}", SalesItem));
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace DYSeriesDataSet
{
   public class clsProprietors
    {
       public clsProprietors()
       { 
       
       }

       private string _Cdcode;
       private string _Cdbuslicence;

       private string _CAllow;

       private string _Ciid;
       private string _Ciname;
       private string _Cdname;
       private string _Cdcardid;
       private string _DisplayName;
       private string _Property;
       private string _KindCode;
       private string _RegCapital;
       private string _Unit;
       private string _Incorporator;
       private DateTime _RegDate;
       private string _DistrictCode;
       private string _PostCode;
       private string _Address;
       private string _LinkMan;
       private string _LinkInfo;
       private string _CreditLevel;
       private string _CreditRecord;
       private string _ProductInfo;
       private string _OtherInfo;
       private string _CheckLevel;
       private string _FoodSafeRecord;
       private bool _IsLock;
       private bool _IsReadOnly;
       private string _Remark;

       /// <summary>
       /// 被检经营户编号
       /// </summary>
       public string Cdcode
       {
           get { return _Cdcode; }
           set { _Cdcode = value; }
       }
       /// <summary>
       /// 营业执照号
       /// </summary>
       public string Cdbuslicence
       {
           get { return _Cdbuslicence; }
           set { _Cdbuslicence = value; }
       }
       public string CAllow
       {
           get { return _CAllow; }
           set { _CAllow = value; }
       }
       /// <summary>
       /// 所属单位编号
       /// </summary>
       public string Ciid
       {
           get { return _Ciid; }
           set { _Ciid = value; }
       }
       /// <summary>
       /// 所属单位名称
       /// </summary>
       public string Ciname
       {
           get { return _Ciname; }
           set { _Ciname = value; }
       }
       /// <summary>
       /// 商户名称
       /// </summary>
       public string Cdname
       {
           get { return _Cdname; }
           set { _Cdname = value; }
       }
       /// <summary>
       /// 身份证号
       /// </summary>
       public string Cdcardid
       {
           get { return _Cdcardid; }
           set { _Cdcardid = value; }
       }
       /// <summary>
       /// 档口/店面/车牌号
       /// </summary>
       public string DisplayName
       {
           get { return _DisplayName; }
           set { _DisplayName = value; }
       }
       /// <summary>
       /// 单位性质(被檢單位、生產單位)
       /// </summary>
       public string Property
       {
           get { return _Property; }
           set { _Property = value; }
       }
       /// <summary>
       /// 单位类别代码
       /// </summary>
       public string KindCode
       {
           get { return _KindCode; }
           set { _KindCode = value; }
       }
       /// <summary>
       /// 注册资金
       /// </summary>
       public string RegCapital
       {
           get { return _RegCapital; }
           set { _RegCapital = value; }
       }
       /// <summary>
       /// 资金单位
       /// </summary>
       public string Unit
       {
           get { return _Unit; }
           set { _Unit = value; }
       }
       /// <summary>
       /// 法人代表
       /// </summary>
       public string Incorporator
       {
           get { return _Incorporator; }
           set { _Incorporator = value; }
       }
       /// <summary>
       /// 注册时间
       /// </summary>
       public DateTime RegDate
       {
           get { return _RegDate; }
           set { _RegDate = value; }
       }
       /// <summary>
       /// 行政机构
       /// </summary>
       public string DistrictCode
       {
           get { return _DistrictCode; }
           set { _DistrictCode = value; }
       }
       /// <summary>
       /// 邮政编码
       /// </summary>
       public string PostCode
       {
           get { return _PostCode; }
           set { _PostCode = value; }
       }
       /// <summary>
       /// 地址
       /// </summary>
       public string Address
       {
           get { return _Address; }
           set { _Address = value; }
       }
       /// <summary>
       /// 联系人
       /// </summary>
       public string LinkMan
       {
           get { return _LinkMan; }
           set { _LinkMan = value; }
       }
       /// <summary>
       /// 联系方式
       /// </summary>
       public string LinkInfo
       {
           get { return _LinkInfo; }
           set { _LinkInfo = value; }
       }
       /// <summary>
       /// 信用等级
       /// </summary>
       public string CreditLevel
       {
           get { return _CreditLevel; }
           set { _CreditLevel = value; }
       }
       /// <summary>
       /// 信用记录
       /// </summary>
       public string CreditRecord
       {
           get { return _CreditRecord; }
           set { _CreditRecord = value; }
       }
       /// <summary>
       /// 产品信息
       /// </summary>
       public string ProductInfo
       {
           get { return _ProductInfo; }
           set { _ProductInfo = value; }
       }
       /// <summary>
       /// 其他信息
       /// </summary>
       public string OtherInfo
       {
           get { return _OtherInfo; }
           set { _OtherInfo = value; }
       }
       /// <summary>
       /// 监控级别
       /// </summary>
       public string CheckLevel
       {
           get { return _CheckLevel; }
           set { _CheckLevel = value; }
       }
       /// <summary>
       /// 食品安全记录
       /// </summary>
       public string FoodSafeRecord
       {
           get { return _FoodSafeRecord; }
           set { _FoodSafeRecord = value; }
       }
       /// <summary>
       /// 是否锁定
       /// </summary>
       public bool IsLock
       {
           get { return _IsLock; }
           set { _IsLock = value; }
       }
       /// <summary>
       /// 是否审核
       /// </summary>
       public bool IsReadOnly
       {
           get { return _IsReadOnly; }
           set { _IsReadOnly = value; }
       }
       /// <summary>
       /// 备注说明
       /// </summary>
       public string Remark
       {
           get { return _Remark; }
           set { _Remark = value; }
       }

    }
}

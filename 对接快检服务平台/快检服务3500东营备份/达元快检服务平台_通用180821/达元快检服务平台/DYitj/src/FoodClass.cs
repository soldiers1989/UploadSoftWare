using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AIO
{
    [Serializable]
    public class FoodClass
    {
        [XmlAttribute]
        public string SysCode { get; set; }

        [XmlAttribute]
        public string StdCode { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string ShortCut { get; set; }

        [XmlAttribute]
        public string CheckLevel { get; set; }

        [XmlAttribute]
        public string CheckItemCodes { get; set; }

        [XmlAttribute]
        public string CheckItemValue { get; set; }

        [XmlAttribute]
        public string IsLock { get; set; }

        [XmlAttribute]
        public string IsReadOnly { get; set; }

        [XmlAttribute]
        public string Remark { get; set; }
    }

    [Serializable]
    public class Company
    {
        [XmlAttribute]
        public string SysCode { get; set; }
        
        [XmlAttribute]
        public string StdCode { get; set; }

        [XmlAttribute]
        public string CompanyID { get; set; }

        [XmlAttribute]
        public string FullName { get; set; }

        [XmlAttribute]
        public string DisplayName { get; set; }

        [XmlAttribute]
        public string Property { get; set; }

        [XmlAttribute]
        public string KindCode { get; set; }

        [XmlAttribute]
        public string RegCapital { get; set; }

        [XmlAttribute]
        public string Unit { get; set; }

        [XmlAttribute]
        public string Incorporator { get; set; }

        [XmlAttribute]
        public string RegData { get; set; }

        [XmlAttribute]
        public string DistrictCode { get; set; }

        [XmlAttribute]
        public string PostCode { get; set; }

        [XmlAttribute]
        public string Address { get; set; }

        [XmlAttribute]
        public string LinkMan { get; set; }

        [XmlAttribute]
        public string LinkInfo { get; set; }

        [XmlAttribute]
        public string CreditLevel { get; set; }

        [XmlAttribute]
        public string CreditRecord { get; set; }

        [XmlAttribute]
        public string ProductInfo { get; set; }

        [XmlAttribute]
        public string OtherInfo { get; set; }

        [XmlAttribute]
        public string CheckLevel { get; set; }

        [XmlAttribute]
        public string FoodSafeRecord { get; set; }

        [XmlAttribute]
        public string IsLock { get; set; }

        [XmlAttribute]
        public string IsReadOnly { get; set; }

        [XmlAttribute]
        public string Remark { get; set; }

        [XmlAttribute]
        public string Sign { get; set; }
    }


    [Serializable]
    public class Dealer
    {
        [XmlAttribute]
        public string Cdcode { get; set; }

        [XmlAttribute]
        public string Cdbuslicence { get; set; }

        [XmlAttribute]
        public string Ciid { get; set; }

        [XmlAttribute]
        public string Ciname { get; set; }

        [XmlAttribute]
        public string Cdname { get; set; }

        [XmlAttribute]
        public string Cdcardid { get; set; }
        
        [XmlAttribute]
        public string DisplayName { get; set; }

        [XmlAttribute]
        public string Property { get; set; }

        [XmlAttribute]
        public string KindCode { get; set; }

        [XmlAttribute]
        public string RegCapital { get; set; }

        [XmlAttribute]
        public string Unit { get; set; }

        [XmlAttribute]
        public string Incorporator { get; set; }

        [XmlAttribute]
        public string RegDate { get; set; }

        [XmlAttribute]
        public string DistrictCode { get; set; }

        [XmlAttribute]
        public string PostCode { get; set; }

        [XmlAttribute]
        public string Address { get; set; }

        [XmlAttribute]
        public string LinkMan { get; set; }

        [XmlAttribute]
        public string LinkInfo { get; set; }

        [XmlAttribute]
        public string CreditLevel { get; set; }

        [XmlAttribute]
        public string CreditRecord { get; set; }

        [XmlAttribute]
        public string ProductInfo { get; set; }

        [XmlAttribute]
        public string OtherInfo { get; set; }

        [XmlAttribute]
        public string CheckLevel { get; set; }

        [XmlAttribute]
        public string FoodSafeRecord { get; set; }

        [XmlAttribute]
        public string IsReadOnly { get; set; }

        [XmlAttribute]
        public string Remark { get; set; }
    }
    //检测项目属性设置
    [Serializable]
    public class CHECKITEMS
    {
        //项目名称
        [XmlAttribute]
        public string Name { get; set; }
        //监管通上项目名称 --------2016-06-19 NewAdd
        [XmlAttribute]
        public string ItemDes { get; set; }
        //监管通上项目编号--------2016-06-19 NewAdd
        [XmlAttribute]
        public string SysCode { get; set; }
        //检测单位
        [XmlAttribute]
        public string Unit { get; set; }
        //操作注释
        [XmlAttribute]
        public string HintStr { get; set; }
        //密码
        [XmlAttribute]
        public string Password { get; set; }
        //样品编号
        [XmlAttribute]
        public string SampleNum { get; set; }
        //胶体金C值
        [XmlAttribute]
        public string InvalidC { get; set; }
        //孔
        [XmlAttribute]
        public string hole1 { get; set; }

        [XmlAttribute]
        public string hole2 { get; set; }
        //大型检测方法
        [XmlAttribute]
        public string CheckType { get; set; }
        //波长
        [XmlAttribute]
        public string Wave { get; set; }
        //方法序号
        [XmlAttribute]
        public string Method { get; set; }
        //小型方法名称
        [XmlAttribute]
        public string MethodName { get; set; }
    }

    [Serializable]
    public class NewDataSet
    {
        List<FoodClass> foodClasses = new List<FoodClass>();
    }
}

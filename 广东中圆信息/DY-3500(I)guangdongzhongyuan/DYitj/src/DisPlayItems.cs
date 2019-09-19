using System;
using System.Xml.Serialization;
namespace AIO
{
    [Serializable]
    public class DisPlayItems
    {
        //项目名称
        [XmlAttribute]
        public string Name { get; set; }
        //检测单位
        [XmlAttribute]
        public string ItemDes { get; set; }
        //操作注释
        [XmlAttribute]
        public string SysCode { get; set; }
        //密码
        [XmlAttribute]
        public string Unit { get; set; }
        //样品编号
        [XmlAttribute]
        public string HintStr { get; set; }
        //胶体金C值
        [XmlAttribute]
        public string Password { get; set; }
        //孔
        [XmlAttribute]
        public string SampleNum { get; set; }
        //项目名称
        [XmlAttribute]
        public string InvalidC { get; set; }
        //检测单位
        [XmlAttribute]
        public string hole1 { get; set; }
        //操作注释
        [XmlAttribute]
        public string hole2 { get; set; }
        //密码
        [XmlAttribute]
        public string CheckType { get; set; }
        //样品编号
        [XmlAttribute]
        public string Wave { get; set; }
        //胶体金C值
        [XmlAttribute]
        public string Method { get; set; }
        //孔
        [XmlAttribute]
        public string MethodName { get; set; }
    }
}
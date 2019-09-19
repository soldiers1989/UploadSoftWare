using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace DYSeriesDataSet.DataModel
{
    public class NTInformation
    {
        /// <summary>  
        /// 反序列化XML为类实例  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="xmlObj"></param>  
        /// <returns></returns>  
        public static T DeserializeXML<T>(string xmlObj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringReader reader = new StringReader(xmlObj))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
        /// <summary>
        /// 主键id，唯一标识
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 样品唯一编号
        /// 检测样品的唯一编号
        /// 编号规则：计划类型 +主体类型+市场编号+计划生成时间+三位数自增长
        /// </summary>
        public string SAMPLEUUID { get; set; }

        /// <summary>
        /// 检测设备的唯一编号
        /// </summary>
        public string JCSB { get; set; }

        /// <summary>
        /// 检测设备的通道号
        /// </summary>
        public string SBTDH { get; set; }

        /// <summary>
        /// 检测市场的唯一标识
        /// </summary>
        public string MARKET { get; set; }

        /// <summary>
        /// 样品录入时间，格式为:YYYY-MM-DDhh24:mi:ss
        /// </summary>
        public string SAMPLEINTIME { get; set; }

        /// <summary>
        /// 检测样品的状态值（0、未检测 1、已检测）
        /// </summary>
        public string SAMPLESTATE { get; set; }

        /// <summary>
        /// 摊位被抽检到的食品要检测的类别
        /// </summary>
        public string JCLB { get; set; }

        /// <summary>
        /// 摊位被抽检到的食品要检测的项目，例如农药残留
        /// </summary>
        public string JCXM { get; set; }

        /// <summary>
        /// 样品名称
        /// </summary>
        public string SAMPLENAME { get; set; }

        /// <summary>
        /// 检测结果值（0阳性，1阴性）
        /// </summary>
        public string RESULTVALUE { get; set; }


        public class RESULT
        {

            public string XMLRESULT { get; set; }

            public string XMLMESSAGE { get; set; }

            public string JCSB { get; set; }

            public string SENDDATE { get; set; }

            public List<TESTPLANINFO> TESTPLANINFOS { get; set; }

        }

        public class TESTPLANINFO
        {
            /// <summary>
            /// 样品唯一编号
            /// </summary>     
            public string SAMPLEUUID { get; set; }
            /// <summary>
            /// 样品名称
            /// </summary>
            public string SAMPLENAME { get; set; }
            /// <summary>
            /// 设备通道号
            /// </summary>
            public string SBTDH { get; set; }
            public string SBTDHNAME { get; set; }
            public string JCXMID { get; set; }
            /// <summary>
            /// 检测项目名称
            /// </summary>
            public string JCXMNAME { get; set; }
        }

    }
}

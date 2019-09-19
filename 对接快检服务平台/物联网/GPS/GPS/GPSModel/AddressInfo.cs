using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace GPS.GPSModel
{
    public class AddressInfo
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 详情
        /// </summary>
        public Content content { get; set; }
        /// <summary>
        /// 返回状态 0 为成功
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 地址详情
        /// </summary>
        public class Address_detail
        {
            /// <summary>
            /// 城市
            /// </summary>
            public string city { get; set; }
            /// <summary>
            /// 城市编号
            /// </summary>
            public int city_code { get; set; }
            /// <summary>
            /// 行政区
            /// </summary>
            public string district { get; set; }
            /// <summary>
            /// 所属省份
            /// </summary>
            public string province { get; set; }
            /// <summary>
            /// 街道
            /// </summary>
            public string street { get; set; }
            /// <summary>
            /// 街道编号
            /// </summary>
            public string street_number { get; set; }
        }

        /// <summary>
        /// 坐标
        /// </summary>
        public class Point
        {
            /// <summary>
            /// 
            /// </summary>
            public string x { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string y { get; set; }
        }

        /// <summary>
        /// 地址内容
        /// </summary>
        public class Content
        {
            /// <summary>
            /// 地址
            /// </summary>
            public string address { get; set; }
            /// <summary>
            /// 地址详情
            /// </summary>
            public Address_detail address_detail { get; set; }
            /// <summary>
            /// 坐标详情
            /// </summary>
            public Point point { get; set; }
        }

        /// <summary>
        /// 获取位置信息
        /// </summary>
        /// <returns></returns>
        public static AddressInfo GetAddressByBaiduAPI()
        {
            string ak = "71uvZYtsYej6XlhkpA7BmQ50owd59pz1";
            string coor = "bd09ll";
            HttpWebRequest request;
            HttpWebResponse response;
            //string url = "https://webapi.amap.com/maps?v=1.4.14&key=7a2f38c258f874c2dc799a4429cf179e";
            string url = string.Format("http://api.map.baidu.com/location/ip?ak={0}&coor={1}", ak, coor);
            try
            {
                request = HttpWebRequest.Create(url) as HttpWebRequest;
                response = request.GetResponse() as HttpWebResponse;
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(stream, Encoding.UTF8))
                    {
                        string json = sr.ReadToEnd();
                        AddressInfo detail = (AddressInfo)JsonConvert.DeserializeObject(json, typeof(AddressInfo));
                        return detail;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

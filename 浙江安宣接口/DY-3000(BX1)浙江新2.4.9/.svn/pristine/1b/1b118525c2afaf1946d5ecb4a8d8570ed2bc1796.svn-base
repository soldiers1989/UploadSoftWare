using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DYSeriesDataSet.DataModel.Wisdom
{
    /// <summary>
    /// 快检设备开关机状态上报
    /// </summary>
    public class deviceStatus
    {

        public deviceStatus() { }

        public class Request
        {
            private String _deviceid = string.Empty;
            /// <summary>
            /// 唯一机器码
            /// </summary>
            public String deviceid
            {
                get { return _deviceid; }
                set { _deviceid = value; }
            }
            private String _longitude = string.Empty;
            /// <summary>
            /// 经度
            /// </summary>
            public String longitude
            {
                get { return _longitude; }
                set { _longitude = value; }
            }
            private String _latitude = string.Empty;
            /// <summary>
            /// 维度
            /// </summary>
            public String latitude
            {
                get { return _latitude; }
                set { _latitude = value; }
            }
            private String _deviceStatus = string.Empty;
            /// <summary>
            /// 1代表开机，2代表保持运行，0代表关机
            /// </summary>
            public String deviceStatus
            {
                get { return _deviceStatus; }
                set { _deviceStatus = value; }
            }
        }

        public class Response
        {
            private String _code = string.Empty;
            /// <summary>
            /// 0	上报成功	表述正常
            /// 1	参数有误(返回具体的错误内容)	usr/pwd/result等参数缺失或者为空
            /// 2	返回具体的错误内容	json字符串解析失败	不符合JSON格式
            /// 3	返回具体的错误内容	系统处理失败
            /// </summary>
            public String code
            {
                get { return _code; }
                set { _code = value; }
            }
            private String _message = string.Empty;
            /// <summary>
            /// 具体内容
            /// </summary>
            public String message
            {
                get { return _message; }
                set { _message = value; }
            }
        }

    }
}

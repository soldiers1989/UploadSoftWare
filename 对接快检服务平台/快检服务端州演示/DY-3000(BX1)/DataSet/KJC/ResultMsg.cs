using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DYSeriesDataSet.KJC
{
    /// <summary>
    /// Note：
    /// Creater：wenj
    /// Time：2018/4/16 16:36:11
    /// Company：食安科技
    /// Web Site：http://www.chinafst.cn/
    /// Version：V1.0.0
    /// </summary>
    public class ResultMsg
    {
        /// <summary>
        /// 结果代码
        /// </summary>
        public string resultCode { get; set; }

        /// <summary>
        /// 结果描述
        /// </summary>
        public string resultDescripe { get; set; }

        /// <summary>
        /// json对象
        /// </summary>
        public object result { get; set; }

    }
}

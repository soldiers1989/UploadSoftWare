using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkstationModel.UpData
{
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkstationBLL.Mode
{
    /// <summary>
    /// 更新窗体的消息通知委托事件
    /// </summary>
    public  class clsUpdateMessage
    {
        //声明一个更新Label的委托
        public delegate void LabelUpdateHandler(object sender, LabelUpdateEventArgs e);

        //声明一个更新Address的事件
        public static event LabelUpdateHandler LabelUpdated;
        public class LabelUpdateEventArgs : System.EventArgs
        {
            private string Lvalue;
            private string _code;
            //private string mCity;
            //private string mZipCode;
            public LabelUpdateEventArgs(string mcode, string lvalue)
            {
                _code = mcode;
                Lvalue = lvalue;
                //this.mState = sState;
                //this.mCity = sCity;
                //this.mZipCode = sZipCode;
            }
            //label名称
            public string Slabel 
            {
                get 
                { 
                    return Lvalue; 
                }
                set
                {
                    Lvalue = value;
                }
            }
            //事件代码
            public string Code
            { 
                get
                {
                    return _code;
                }
                set
                {
                    _code = value;
                }
            }
            //public string City { get { return mCity; } }
            //public string ZipCode { get { return mZipCode; } }
        }
        /// <summary>
        /// 发送信息函数
        /// </summary>
        /// <param name="notic"></param>
        public void SendOutMessage(string lcode,string notic)
        {
            LabelUpdateEventArgs luea = new LabelUpdateEventArgs(lcode,notic);

            LabelUpdated(this, luea);           
        }
    }
}

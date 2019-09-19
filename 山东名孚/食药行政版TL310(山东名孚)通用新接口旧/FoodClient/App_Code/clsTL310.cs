using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JH.CommBase;
using DY.FoodClientLib;

namespace FoodClient.App_Code
{
    public class clsTL310 : CommBase
    {



        /// <summary>
        /// 设置波特
        /// </summary>
        /// <returns></returns>
        protected override CommBaseSettings CommSettings()
        {
            CommBaseSettings cs = new CommBaseSettings();
            cs.SetStandard(ShareOption.ComPort, 9600, Handshake.none, Parity.none);//9600
            return cs;
        }
        /// <summary>
        /// 串口返回数据
        /// </summary>
        /// <param name="c"></param>
        protected override void OnRxChar(byte c)
        {
            
            
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        public void ReadHistory(DateTime startDate, DateTime endDate)
        {

        }
    }
}

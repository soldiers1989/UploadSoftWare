using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkstationDAL.Basic
{
    public class clsIntrument
    {
        
        //串口波特率
        private string _Name = string.Empty;
        public string Name
        {
            set
            {
                _Name = value;
            }
            get
            {
                return _Name;
            }
        }
    }
}

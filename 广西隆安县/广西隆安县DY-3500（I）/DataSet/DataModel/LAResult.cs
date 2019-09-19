using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DYSeriesDataSet.DataModel
{
    public  class LAResult
    {
        private string _resultCode = "";
        public string resultCode
        {
            set { _resultCode = value; }
            get { return _resultCode; }
        }
        private string _resultDescripe = "";
        public string resultDescripe
        {
            set { _resultDescripe = value; }
            get { return _resultDescripe; }
        }
    }
}

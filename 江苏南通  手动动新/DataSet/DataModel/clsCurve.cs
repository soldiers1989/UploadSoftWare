using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DYSeriesDataSet.DataModel
{
    public class clsCurve
    {
        private string _Time;
        private Double _DIanliu;
        private Double _DIanwei;

        public string Time
        {
            set
            {
                _Time = value;
            }
            get
            {
                return _Time;
            }
        }

        public Double Dianliu
        {
            set
            {
                _DIanliu = value;
            }
            get
            {
                return _DIanliu;
            }
        }

        public Double Dianwei
        {
            set
            {
                _DIanwei = value;
            }
            get
            {
                return _DIanwei;
            }
        }
    }
}

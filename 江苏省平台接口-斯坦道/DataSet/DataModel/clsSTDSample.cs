using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DYSeriesDataSet.DataModel
{
    public class clsSTDSample
    {
        private string _sampleNum = "";
        public string sampleNum
        {
            set { _sampleNum = value; }
            get { return _sampleNum; }
        }
        private string _productName = "";
        public string productName
        {
            set { _productName = value; }
            get { return _productName; }
        }
        private string _category = "";
        public string category
        {
            set { _category = value; }
            get { return _category; }
        }

        private string _foodTypeId = "";
        public string foodTypeId
        {
            set { _foodTypeId = value; }
            get { return _foodTypeId; }
        }
        private string _enterpriseId = "";
        public string enterpriseId
        {
            set { _enterpriseId = value; }
            get { return _enterpriseId; }
        }

        private string _enterpriseName = "";
        public string enterpriseName
        {
            set { _enterpriseName = value; }
            get { return _enterpriseName; }
        }

        private string _stallNumber = "";
        public string stallNumber
        {
            set { _stallNumber = value; }
            get { return _stallNumber; }
        }
        private string _productPlaceInfo = "";
        public string productPlaceInfo
        {
            set { _productPlaceInfo = value; }
            get { return _productPlaceInfo; }
        }
        private string _isDetection = "";
        public string isDetection
        {
            set { _isDetection = value; }
            get { return _isDetection; }
        }
        private string _createtime = "";
        public string createtime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
    }
}

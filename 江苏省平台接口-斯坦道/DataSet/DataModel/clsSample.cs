using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DYSeriesDataSet.DataModel
{
    public class clsSample
    {
        private string _id = "";
        public string id
        {
            set { _id = value; }
            get { return _id; }
        }
        private string _name = "";
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _typeLevel = "";
        public string typeLevel
        {
            get { return _typeLevel; }
            set { _typeLevel = value; }
        }
        private string _typeLevelName = "";
        public string typeLevelName
        {
            get { return _typeLevelName; }
            set { _typeLevelName = value; }
        }

        private string _hierarchy = "";
        public string hierarchy
        {
            get { return _hierarchy; }
            set { _hierarchy = value; }
        }
    }
    /// <summary>
    /// 下载的检测单位
    /// </summary>
    public class JSCompany
    {
        private string _id = "";
        public string id 
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _name = "";
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _type = "";
        public string type
        {
            get { return _type; }
            set { _type = value; }
        }
        private string _legalPerson = "";
        public string legalPerson
        {
            get { return _legalPerson; }
            set { _legalPerson = value; }
        }
        private string _legalPersonContact = "";
        public string legalPersonContact
        {
            get { return _legalPersonContact; }
            set { _legalPersonContact = value; }
        }
        private string _address = "";
        public string address
        {
            get { return _address; }
            set { _address = value; }
        }
        private string _creditLevel = "";
        public string creditLevel
        {
            get { return _creditLevel; }
            set { _creditLevel = value; }
        }
        private string _licenceNumber = "";
        public string licenceNumber
        {
            get { return _licenceNumber; }
            set { _licenceNumber = value; }
        }
 
    }
}

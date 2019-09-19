using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DYSeriesDataSet.DataModel
{
    public class clsLASample
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
        private object _result;
        public object result
        {
            set { _result = value; }
            get { return _result; }
        }
    }

    public class SampleInfo
    {
        public string cdIdNum { get; set; }
        public string cdName { get; set; }
        public string regBusLicence { get; set; }
        public string regContactPerson { get; set; }
        public string regName { get; set; }
        public object detailModels { get; set; }
    }

    public class detailsample
    {
        public  object checkItemsModels { get; set; }
        public string foodName { get; set; }
        public string foodType { get; set; }
        public string nominalSourcePlace { get; set; }
        public string productionPlace { get; set; }
        public string sampleNO { get; set; }

        public string supplier { get; set; }
        public string supplierContact { get; set; }

        public string supplierPhone { get; set; }
        public string takeInDate { get; set; }

        public string takeInNumber { get; set; }

    }
    public class checkItemList
    {
        public string checkAccord { get; set; }
        public string checkItem { get; set; }
    }
  
    public class downsample
    {
        private int _ID = 0;
        public int ID
        {
            set { _ID = value; }
            get { return _ID; }
        }

        private string _isTest = "";
        public string isTest
        {
            set { _isTest = value; }
            get { return _isTest; }
        }
        private string _cdName = "";
        public string cdName
        {
            set { _cdName = value; }
            get { return _cdName; }
        }

        private string _cdIdNum = "";
        public string cdIdNum
        {
            set { _cdIdNum = value; }
            get { return _cdIdNum; }
        }
        private string _regBusLicence = "";
        public string regBusLicence
        {
            set { _regBusLicence = value; }
            get { return _regBusLicence; }
        }

        private string _regName = "";
        public string regName
        {
            set { _regName = value; }
            get { return _regName; }
        }
        private string _regContactPerson = "";
        public string regContactPerson
        {
            set { _regContactPerson = value; }
            get { return _regContactPerson; }
        }
        private string _sampleNO = "";
        public string sampleNO
        {
            set { _sampleNO = value; }
            get { return _sampleNO; }
        }
        private string _foodType = "";
        public string foodType
        {
            set { _foodType = value; }
            get { return _foodType; }
        }
        private string _foodName = "";
        public string foodName
        {
            set { _foodName = value; }
            get { return _foodName; }
        }
        private string _checkItem = "";
        public string checkItem
        {
            set { _checkItem = value; }
            get { return _checkItem; }
        }
        private string _checkAccord = "";
        public string checkAccord
        {
            set { _checkAccord = value; }
            get { return _checkAccord; }
        }
        private string _productionPlacee = "";
        public string productionPlace
        {
            set { _productionPlacee = value; }
            get { return _productionPlacee; }
        }
        private string _nominalSourcePlace = "";
        public string nominalSourcePlace
        {
            set { _nominalSourcePlace = value; }
            get { return _nominalSourcePlace; }
        }

        private string _takeInNumber = "";
        public string takeInNumber
        {
            set { _takeInNumber = value; }
            get { return _takeInNumber; }
        }

        private string _takeInDate = "";
        public string takeInDate
        {
            set { _takeInDate = value; }
            get { return _takeInDate; }
        }

        private string _supplier = "";
        public string supplier
        {
            set { _supplier = value; }
            get { return _supplier; }
        }

        private string _supplierContact = "";
        public string supplierContact
        {
            set { _supplierContact = value; }
            get { return _supplierContact; }
        }

        private object _supplierPhone = "";
        public object supplierPhone 
        {
            set { _supplierPhone = value; }
            get { return _supplierPhone; }
        }

    }

}

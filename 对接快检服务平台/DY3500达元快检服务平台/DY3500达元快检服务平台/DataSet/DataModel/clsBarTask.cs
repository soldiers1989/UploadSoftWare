using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DYSeriesDataSet.DataModel
{
    public  class clsBarTask
    {
        private int _ID = 0;
        public int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                 _ID=value ;
            }
        }

        private string _bid = string.Empty;
        public string bid
        {
            get 
            {
                return _bid;
            }
            set
            {
                 _bid = value;
            }
        }

        private string _bagCode = string.Empty;
        public string bagCode
        {
            get
            {
                return _bagCode;
            }
            set
            {
                 _bagCode = value;
            }
        }

        private string _foodName = string.Empty;
        public string foodName
        {
            get
            {
                return _foodName;
            }
            set
            {
                _foodName=value ;
            }
        }

        private string _itemName = string.Empty;
        public string itemName
        {
            get
            {
                return _itemName;
            }
            set
            {
                _itemName = value;
            }
        }

        private string _mokuai = string.Empty;
        public string mokuai
        {
            get
            {
                return _mokuai;
            }
            set
            {
                _mokuai = value;
            }
        }

        private string _IsTest = string.Empty;
        public string IsTest
        {
            get
            {
                return _IsTest;
            }
            set
            {
                _IsTest = value;
            }
        }
        private string _getSampleTime = string.Empty;

        public string getSampleTime
        {
            get
            {
                return _getSampleTime;
            }
            set
            {
                _getSampleTime = value;
            }
        }

        private string _InBarCode = string.Empty;
        public string InBarCode
        {
            get
            {
                return _InBarCode;
            }
            set
            {
                _InBarCode = value;
            }
        }

    }
}

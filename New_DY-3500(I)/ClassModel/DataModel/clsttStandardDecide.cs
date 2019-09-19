using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataSetModel
{
    public class clsttStandardDecide
    {
        public clsttStandardDecide()
        {

        }
        #region Field Members

        private int m_ID = 0;
        private string m_FtypeNmae = string.Empty;
        private string m_SampleNum = string.Empty;
        private string m_Name = string.Empty;
        private string m_ItemDes = string.Empty;
        private string m_StandardValue = string.Empty;
        private string m_Demarcate = string.Empty;
        private string m_Unit = string.Empty;
        private string m_UDate = string.Empty;    

        #endregion

        #region Property Members

        public int ID
        {
            get
            {
                return m_ID;
            }
            set
            {
                m_ID = value;
            }
        }

        public string FtypeNmae
        {
            get
            {
                return m_FtypeNmae;
            }
            set
            {
                m_FtypeNmae = value;
            }
        }

        public string SampleNum
        {
            get
            {
                return m_SampleNum;
            }
            set
            {
                m_SampleNum = value;
            }
        }

        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }

        public string ItemDes
        {
            get
            {
                return m_ItemDes;
            }
            set
            {
                m_ItemDes = value;
            }
        }

        public string StandardValue
        {
            get
            {
                return m_StandardValue;
            }
            set
            {
                m_StandardValue = value;
            }
        }

        public string Demarcate
        {
            get
            {
                return m_Demarcate;
            }
            set
            {
                m_Demarcate = value;
            }
        }

        public string Unit
        {
            get
            {
                return m_Unit;
            }
            set
            {
                m_Unit = value;
            }
        }

        public string UDate
        {
            get
            {
                return m_UDate;
            }
            set
            {
                m_UDate = value;
            }
        }


        #endregion
    }
}

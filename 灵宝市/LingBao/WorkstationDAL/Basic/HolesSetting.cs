using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkstationDAL.Basic
{
    /// <summary>
    /// 检测项目孔位设置实体类
    /// 作者：
    /// 2011-06-26
    /// </summary>
    public class HolesSetting
    {
        private int _id;
        private string _checkItemId;
        private string _holesIndex;
        private bool _isShowOnData;
        private bool _isDouble;
        private string _machineCode;

        /// <summary>
        /// 检测仪器代码
        /// </summary>
        public string MachineCode
        {
            get { return _machineCode; }
            set { _machineCode = value; }
        }

        /// <summary>
        /// 自动编号
        /// </summary>
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// 检测项目编号
        /// </summary>
        public string CheckItemId
        {
            get { return _checkItemId; }
            set { _checkItemId = value; }
        }

        /// <summary>
        /// 样品孔位编号如A1,A2...H12;
        /// </summary>
        public string HolesIndex
        {
            get { return _holesIndex; }
            set { _holesIndex = value; }
        }
        /// <summary>
        /// 是否在检测数据列表中显示非样品孔位数据
        /// </summary>
        public bool IsShowOnData
        {
            get { return _isShowOnData; }
            set { _isShowOnData = value; }
        }

        /// <summary>
        /// 是否双孔检测
        /// </summary>
        public bool IsDouble
        {
            get { return _isDouble; }
            set { _isDouble = value; }
        }
    }
}

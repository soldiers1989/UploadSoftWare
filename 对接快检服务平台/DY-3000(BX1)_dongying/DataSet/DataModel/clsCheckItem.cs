using System;

namespace DYSeriesDataSet
{
	/// <summary>
	/// clsCheckItem 的摘要说明。
	/// </summary>
	public class clsCheckItem
	{
        private string _SysCode = "";
        private string _StdCode = "";
        private string _ItemDes = "";
        private string _CheckType = "";
        private string _StandardCode = "";
        private string _StandardValue = "";
        private string _Unit = "";
        private string _DemarcateInfo = "";
        private string _TestValue = "";
        private string _OperateHelp = "";
        private string _CheckLevel = "";
        private bool _IsReadOnly = false;
        private bool _IsLock = false;
        private string _Remark = "";
        private string _UDate = "";
		
		public clsCheckItem()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

        /// <summary>
        /// 编码
        /// </summary>
		public string SysCode
		{
			set
			{
				_SysCode=value;
			}
			get
			{
				return _SysCode;
			}
		}
        /// <summary>
        /// 检测项目编号
        /// </summary>
		public string StdCode
		{
			set
			{
				_StdCode=value;
			}
			get
			{
				return _StdCode;
			}		
		}
        /// <summary>
        /// 检测项目名称
        /// </summary>
		public string ItemDes
		{
			set
			{
				_ItemDes=value;
			}
			get
			{
				return _ItemDes;
			}		
		}
        /// <summary>
        /// 检测类型
        /// </summary>
		public string CheckType
		{
			set
			{
				_CheckType=value;
			}
			get
			{
				return _CheckType;
			}		
		}
        /// <summary>
        /// 标准值
        /// </summary>
		public string StandardValue
		{
			set
			{
				_StandardValue=value;
			}
			get
			{
				return _StandardValue;
			}		
		}
        /// <summary>
        /// 检测项目ID（主键）
        /// </summary>
		public string StandardCode
		{
			set
			{
				_StandardCode=value;
			}
			get
			{
				return _StandardCode;
			}		
		}
        /// <summary>
        /// 单位
        /// </summary>
		public string Unit
		{
			set
			{
				_Unit=value;
			}
			get
			{
				return _Unit;
			}		
		}
        /// <summary>
        /// 检测标准值符号
        /// </summary>
		public string DemarcateInfo
		{
			set
			{
				_DemarcateInfo=value;
			}
			get
			{
				return _DemarcateInfo;
			}		
		}
        /// <summary>
        /// 测试值
        /// </summary>
		public string TestValue
		{
			set
			{
				_TestValue=value;
			}
			get
			{
				return _TestValue;
			}		
		}
        /// <summary>
        /// 操作说明
        /// </summary>
		public string OperateHelp
		{
			set
			{
				_OperateHelp=value;
			}
			get
			{
				return _OperateHelp;
			}		
		}
        /// <summary>
        /// 检测等级
        /// </summary>
		public string CheckLevel
		{
			set
			{
				_CheckLevel=value;
			}
			get
			{
				return _CheckLevel;
			}		
		}
        /// <summary>
        /// 是否已审核
        /// </summary>
		public bool IsReadOnly
		{
			set
			{
				_IsReadOnly=value;
			}
			get
			{
				return _IsReadOnly;
			}		
		}
        /// <summary>
        /// 是否锁定
        /// </summary>
		public bool IsLock
		{
			set
			{
				_IsLock=value;
			}
			get
			{
				return _IsLock;
			}		
		}
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set
            {
                _Remark = value;
            }
            get
            {
                return _Remark;
            }
        }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public string UDate
        {
            set
            {
                _UDate = value;
            }
            get
            {
                return _UDate;
            }
        }
	}
}

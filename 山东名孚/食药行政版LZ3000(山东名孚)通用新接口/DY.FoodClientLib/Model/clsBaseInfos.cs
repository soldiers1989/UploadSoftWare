using System;
using System.Collections.Generic;
using System.Text;

namespace DY.FoodClientLib.Model
{
    public class clsBaseInfos
    {
        public clsBaseInfos() 
        {

        }

        private Int32 _ID = 0;
        /// <summary>
        /// ID
        /// </summary>
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private String _TITLE = String.Empty;
        /// <summary>
        /// 标题
        /// </summary>
        public String TITLE
        {
            get { return _TITLE; }
            set { _TITLE = value; }
        }

        private String _PDATE = String.Empty;
        /// <summary>
        /// 发布时间
        /// </summary>
        public String PDATE
        {
            get { return _PDATE; }
            set { _PDATE = value; }
        }

        private String _AUTHOR = String.Empty;
        /// <summary>
        /// 作者
        /// </summary>
        public String AUTHOR
        {
            get { return _AUTHOR; }
            set { _AUTHOR = value; }
        }

        private String _PUBLISHER = String.Empty;
        /// <summary>
        /// 发布人
        /// </summary>
        public String PUBLISHER
        {
            get { return _PUBLISHER; }
            set { _PUBLISHER = value; }
        }

        private String _STATUSES = String.Empty;
        /// <summary>
        /// 状态
        /// </summary>
        public String STATUSES
        {
            get { return _STATUSES; }
            set { _STATUSES = value; }
        }

        private String _CONTENT = String.Empty;
        /// <summary>
        /// 信息内容
        /// </summary>
        public String CONTENT
        {
            get { return _CONTENT; }
            set { _CONTENT = value; }
        }

        private String _CARNAME = String.Empty;
        /// <summary>
        /// 接受检测车或仪器
        /// </summary>
        public String CARNAME
        {
            get { return _CARNAME; }
            set { _CARNAME = value; }
        }

        private String _INFORTYPE = String.Empty;
        /// <summary>
        /// 信息类别：1、法律法规；2、政策制度；3、投诉管理；4、建议管理；
        /// 5、通知文告；6、红黑榜；7、工作动态；8、资料下载
        /// </summary>
        public String INFORTYPE
        {
            get { return _INFORTYPE; }
            set { _INFORTYPE = value; }
        }

        private String _EDATE = String.Empty;
        /// <summary>
        /// 编辑时间
        /// </summary>
        public String EDATE
        {
            get { return _EDATE; }
            set { _EDATE = value; }
        }

        private String _SDATE = String.Empty;
        /// <summary>
        /// 固顶时间
        /// </summary>
        public String SDATE
        {
            get { return _SDATE; }
            set { _SDATE = value; }
        }

        private String _VNUM = String.Empty;
        /// <summary>
        /// 浏览次数
        /// </summary>
        public String VNUM
        {
            get { return _VNUM; }
            set { _VNUM = value; }
        }
    }
}

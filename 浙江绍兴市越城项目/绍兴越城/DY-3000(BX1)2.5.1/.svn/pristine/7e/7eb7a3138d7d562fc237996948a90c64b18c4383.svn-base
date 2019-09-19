using System.Windows.Forms;

namespace BatteryManage
{
    public class BasicPanel : Panel
    {
        /// <summary>
        /// 是否允许通过panel来移动窗口
        /// </summary>
        public bool IsMoveWindow = false;
        public BasicPanel()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer, true);
            //强制分配样式重新应用到控件上
            UpdateStyles();
            BackColor = System.Drawing.Color.Transparent;
            BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        }

    }
}
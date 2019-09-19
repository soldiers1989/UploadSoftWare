using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BatteryManage
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Threading.Mutex mutex = new System.Threading.Mutex(false, "BatteryManage");
            bool Running = !mutex.WaitOne(0, false);
            if (!Running) Application.Run(new BatteryManage());
            //else MessageBox.Show("程序已经启动");
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new BatteryManage());
        }
    }
}

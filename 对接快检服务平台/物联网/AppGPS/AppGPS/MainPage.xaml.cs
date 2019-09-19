using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Geolocation;//引用此命名空间
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using Windows.ApplicationModel;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace AppGPS
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        private static SemaphoreSlim asyncLock = new SemaphoreSlim(1);//1：信号容量，即最多几个异步线程一起执行，保守起见设为1
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                LoadState();
                //异步获取位置，保存到变量中
                //var position = LocationManager.GetPosition();
                var position = AppGPS.model.LocationManager.GetPosition();

                System.Threading.Timer timer = new Timer(new TimerCallback(timerCall), null, 0, 11003);//120S也就是获取一次定位
                //string local = "";
                ////维度
                //double lat = position.Coordinate.Point.Position.Latitude;
                ////经度
                //double lon = position.Coordinate.Point.Position.Longitude;
            }
            catch (Exception ex)
            {

            }
        }

        private async void delays(int tt)
        {
            await Task.Delay(tt);
        }
        private async void btnSetState_Click(object sender, RoutedEventArgs e)
        {
            var task = await StartupTask.GetAsync("Appkong");
            if (task.State == StartupTaskState.Disabled)
            {
                await task.RequestEnableAsync();
            }

            // 重新加载状态
            await LoadState();
        }

        public async Task LoadState()
        {
            var task = await StartupTask.GetAsync("Appkong");
            this.tbState.Text = $"Status: {task.State}";
            switch (task.State)
            {
                case StartupTaskState.Disabled:
                    // 禁用状态
                    this.btnSetState.Content = "启用";
                    this.btnSetState.IsEnabled = true;
                    break;
                case StartupTaskState.DisabledByPolicy:
                    // 由管理员或组策略禁用
                    this.btnSetState.Content = "被系统策略禁用";
                    this.btnSetState.IsEnabled = false;
                    break;
                case StartupTaskState.DisabledByUser:
                    // 由用户手工禁用
                    this.btnSetState.Content = "被用户禁用";
                    this.btnSetState.IsEnabled = false;
                    break;
                case StartupTaskState.Enabled:
                    // 当前状态为已启用
                    this.btnSetState.Content = "已启用";
                    this.btnSetState.IsEnabled = false;
                    break;
            }
        }
        private void timerCall(object obj)
        {
            try
            {
                var position = AppGPS.model.LocationManager.GetPosition();
            }
            catch (Exception ex)
            {

            }

        }

        private void btnScanfAddr_Click(object sender, RoutedEventArgs e)
        {
            txtPath.Text = AppGPS.model.Global.Addr;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace New_DY_3500_I_.FenGuangDu
{
    /// <summary>
    /// FGDWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FGDWindow : Window
    {
        private string path = Environment.CurrentDirectory ;
        private bool fgtest = false;//进入分光光度测试
        private bool jttest = false;//进入胶体金测试
        private bool ghxtest = false;//进入干化学测试
        private bool ksjc = false;//快速检测
        private bool datacentre = false;//进入数据中心
        private bool search = false;//进入查询记录
        private bool sysset = false;//进入系统设置
        private bool exitsys = false;//退出系统
        private Brush _borderBrushNormal = new SolidColorBrush(Color.FromRgb(0x00, 0x7C, 0xC2));

        public FGDWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //ButtonFGD.ContentTemplate.Triggers.
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("是否退出系统？","提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Environment.Exit(0);
            }

        }

        private void ButtonExit_MouseEnter(object sender, MouseEventArgs e)
        {
            //string path = Environment.CurrentDirectory + "\\image\\Exit.png";
            //ButtonExit.Background = new ImageBrush(new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute)));
            
        }

        private void ButtonExit_MouseLeave(object sender, MouseEventArgs e)
        {

        }
        /// <summary>
        /// 右上角菜单初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonMenu_Initialized(object sender, EventArgs e)
        {
            //设置右键菜单为null   
            this.ButtonMenu.ContextMenu = null;  
        }
        /// <summary>
        /// 右上角菜单单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonMenu_Click(object sender, RoutedEventArgs e)
        {
            //目标  
            this.contextMenu.PlacementTarget = this.ButtonMenu;
            //位置  
            this.contextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            //显示菜单  
            this.contextMenu.IsOpen = true;  

        }
        /// <summary>
        /// 退出系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitSystem_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RepairPasswoed_Click(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// 系统帮助
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpSystem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RepairPasswoed_MouseEnter(object sender, MouseEventArgs e)
        {
            RepairPasswoed.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#272727"));
        }

        private void RepairPasswoed_MouseLeave(object sender, MouseEventArgs e)
        {
            RepairPasswoed.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E0E0E0"));
        }

        private void ExitSystem_MouseEnter(object sender, MouseEventArgs e)
        {
            ExitSystem.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#272727"));
        }

        private void ExitSystem_MouseLeave(object sender, MouseEventArgs e)
        {
            ExitSystem.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E0E0E0"));
        }

        /// <summary>
        /// 干化学鼠标进入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonGHX_MouseEnter(object sender, MouseEventArgs e)
        {
            //ImageBrush brush = new ImageBrush();
            //brush.ImageSource = new BitmapImage(new Uri("Images/test.png", UriKind.Relative));
            //ButtonGHX.Background = brush;
        }
        /// <summary>
        /// 干化学鼠标离开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonGHX_MouseLeave(object sender, MouseEventArgs e)
        {
            //ImageBrush brush = new ImageBrush();
            //brush.ImageSource = new BitmapImage(new Uri("Images/test.png", UriKind.Relative));
            //ButtonGHX.Background = brush;
        }
        /// <summary>
        /// 干化学鼠标单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonGHX_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //ImageBrush brush = new ImageBrush();
            //brush.ImageSource = new BitmapImage(new Uri("Images/test.png", UriKind.Relative));
            //ButtonGHX.Background = brush;
        }

        private void LabelRapidTest_MouseEnter(object sender, MouseEventArgs e)
        {
            string imge = path + "\\image\\RapidTestSelect.png";
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(imge, UriKind.Relative));
            LabelRapidTest.Background = brush;
        }

        private void LabelRapidTest_MouseLeave(object sender, MouseEventArgs e)
        {
            if (ksjc == false)
            {
                string imge = path + "\\image\\RapidTest.png";
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(imge, UriKind.Relative));
                LabelRapidTest.Background = brush;
            }
        }

        private void LabelDataManage_MouseEnter(object sender, MouseEventArgs e)
        {
            string imge = path + "\\image\\DataManageSelect.png";
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(imge, UriKind.Relative));
            LabelDataManage.Background = brush;
        }

        private void LabelDataManage_MouseLeave(object sender, MouseEventArgs e)
        {
            if (datacentre == false)
            {
                string imge = path + "\\image\\DataManage.png";
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(imge, UriKind.Relative));
                LabelDataManage.Background = brush;
            }
        }

        private void LabelEnquiryRecord_MouseLeave(object sender, MouseEventArgs e)
        {
            if (search == false)
            {
                string imge = path + "\\image\\EnquaryRecord.png";
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(imge, UriKind.Relative));
                LabelEnquiryRecord.Background = brush;
            }
            
        }

        private void LabelEnquiryRecord_MouseEnter(object sender, MouseEventArgs e)
        {
            string imge = path + "\\image\\EnquaryRecordSelect.png";
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(imge, UriKind.Relative));
            LabelEnquiryRecord.Background = brush;
        }

        private void LabelSystmDesign_MouseEnter(object sender, MouseEventArgs e)
        {
            string imge = path + "\\image\\SystemDesignSelect.png";
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(imge, UriKind.Relative));
            LabelSystmDesign.Background = brush;
        }

        private void LabelSystmDesign_MouseLeave(object sender, MouseEventArgs e)
        {
            if(sysset==false)
            {
                string imge = path + "\\image\\SystemDesign.png";
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(imge, UriKind.Relative));
                LabelSystmDesign.Background = brush;
            }
        }

        private void LabelExit_MouseLeave(object sender, MouseEventArgs e)
        {
            if (exitsys == false)
            {
                string imge = path + "\\image\\Exit.png";
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(imge, UriKind.Relative));
                LabelExit.Background = brush;
            }
            
        }

        private void LabelExit_MouseEnter(object sender, MouseEventArgs e)
        {
            string imge = path + "\\image\\ExitSelect.png";
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(imge, UriKind.Relative));
            LabelExit.Background = brush;
        }

        private void LabelTestJTJ_MouseEnter(object sender, MouseEventArgs e)
        {
             string imge = path +"\\image\\jiaotijinselect.png";
             ImageBrush brush = new ImageBrush();
             brush.ImageSource = new BitmapImage(new Uri(imge, UriKind.Relative));
             LabelTestJTJ.Background = brush;
        }

        private void LabelTestJTJ_MouseLeave(object sender, MouseEventArgs e)
        {
            if (jttest == false)
            {
                string imge = path + "\\image\\jiaotijin.png";
                LabelTestJTJ.Background = new ImageBrush(new BitmapImage(new Uri(imge, UriKind.RelativeOrAbsolute)));
            }
        }

        private void LabelTestFGD_MouseLeave(object sender, MouseEventArgs e)
        {
            if(fgtest ==false )
            {
                string imge = path + "\\image\\fenguang.png";
                LabelTestFGD.Background = new ImageBrush(new BitmapImage(new Uri(imge, UriKind.RelativeOrAbsolute)));
            }
        }

        private void LabelTestFGD_MouseEnter(object sender, MouseEventArgs e)
        {
            string imge = path + "\\image\\fenguangselect.png";
            LabelTestFGD.Background = new ImageBrush(new BitmapImage(new Uri(imge, UriKind.RelativeOrAbsolute)));
        }

        private void LabelTestGHX_MouseLeave(object sender, MouseEventArgs e)
        {
            if (ghxtest == false)
            {
                string imge = path + "\\image\\ghx.png";
                LabelTestGHX.Background = new ImageBrush(new BitmapImage(new Uri(imge, UriKind.RelativeOrAbsolute)));
            }
        }

        private void LabelTestGHX_MouseEnter(object sender, MouseEventArgs e)
        {
            string imge = path + "\\image\\ghxSelect.png";
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(imge, UriKind.Relative));
            LabelTestGHX.Background = brush;
        }
        /// <summary>
        /// 分光光度测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelTestFGD_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fgtest = true;
            jttest = false;//进入胶体金测试
            ghxtest = false;//进入干化学测试
            string imge = path + "\\image\\fenguangselect.png";
            LabelTestFGD.Background = new ImageBrush(new BitmapImage(new Uri(imge, UriKind.RelativeOrAbsolute)));
            LabelTestJTJ.Background = new ImageBrush(new BitmapImage(new Uri(path + "\\image\\jiaotijin.png", UriKind.RelativeOrAbsolute)));
            LabelTestGHX.Background = new ImageBrush(new BitmapImage(new Uri(path + "\\image\\ghx.png", UriKind.RelativeOrAbsolute)));
            GidTestJTJ.Visibility = Visibility.Collapsed;
            GridTestFGD.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// 胶体金测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelTestJTJ_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fgtest = false;
            jttest = true;//进入胶体金测试
            ghxtest = false;//进入干化学测试
            string imge = path + "\\image\\jiaotijinselect.png";
            LabelTestJTJ.Background = new ImageBrush(new BitmapImage(new Uri(imge, UriKind.RelativeOrAbsolute)));
            LabelTestGHX.Background = new ImageBrush(new BitmapImage(new Uri(path + "\\image\\ghx.png", UriKind.RelativeOrAbsolute)));
            LabelTestFGD.Background = new ImageBrush(new BitmapImage(new Uri(path + "\\image\\fenguang.png", UriKind.RelativeOrAbsolute)));
            
            GridTestFGD.Visibility = Visibility.Collapsed;
            GidTestJTJ.Visibility = Visibility.Visible;

            for (int i = 0; i < 4; i++)
            {
                UIElement element=JTJLayout(i,"","");
                WrapPanelChannel.Children.Add(element);
            }
        }
        /// <summary>
        /// 干化学测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelTestGHX_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fgtest = false;
            jttest = false ;//进入胶体金测试
            ghxtest = true ;//进入干化学测试
            string imge = path + "\\image\\ghxSelect.png";
            LabelTestGHX.Background = new ImageBrush(new BitmapImage(new Uri(imge, UriKind.RelativeOrAbsolute)));
            LabelTestFGD.Background = new ImageBrush(new BitmapImage(new Uri(path + "\\image\\fenguang.png", UriKind.RelativeOrAbsolute)));
            LabelTestJTJ.Background = new ImageBrush(new BitmapImage(new Uri(path + "\\image\\jiaotijin.png", UriKind.RelativeOrAbsolute)));
        }
        /// <summary>
        /// 快速检测单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelRapidTest_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ksjc = true ;//快速检测
            datacentre = false;//进入数据中心
            search = false;//进入查询记录
            sysset = false;//进入系统设置
            exitsys = false;//退出系统
            string imge = path + "\\image\\RapidTestSelect.png";
            LabelRapidTest.Background = new ImageBrush(new BitmapImage(new Uri(imge, UriKind.RelativeOrAbsolute)));
            LabelDataManage.Background = new ImageBrush(new BitmapImage(new Uri(path + "\\image\\DataManage.png", UriKind.RelativeOrAbsolute)));
            LabelEnquiryRecord.Background = new ImageBrush(new BitmapImage(new Uri(path + "\\image\\EnquaryRecord.png", UriKind.RelativeOrAbsolute)));
            LabelSystmDesign.Background = new ImageBrush(new BitmapImage(new Uri(path + "\\image\\SystemDesign.png", UriKind.RelativeOrAbsolute)));
            LabelExit.Background = new ImageBrush(new BitmapImage(new Uri(path + "\\image\\Exit.png", UriKind.RelativeOrAbsolute)));
        }
        /// <summary>
        /// 数据管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelDataManage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ksjc = false;//快速检测
            datacentre = true;//进入数据中心
            search = false;//进入查询记录
            sysset = false;//进入系统设置
            exitsys = false;//退出系统
            string imge = path + "\\image\\DataManageSelect.png";
            LabelRapidTest.Background = new ImageBrush(new BitmapImage(new Uri(path + "\\image\\RapidTest.png", UriKind.RelativeOrAbsolute)));
            LabelDataManage.Background = new ImageBrush(new BitmapImage(new Uri(imge, UriKind.RelativeOrAbsolute)));
            LabelEnquiryRecord.Background = new ImageBrush(new BitmapImage(new Uri(path + "\\image\\EnquaryRecord.png", UriKind.RelativeOrAbsolute)));
            LabelSystmDesign.Background = new ImageBrush(new BitmapImage(new Uri(path + "\\image\\SystemDesign.png", UriKind.RelativeOrAbsolute)));
            LabelExit.Background = new ImageBrush(new BitmapImage(new Uri(path + "\\image\\Exit.png", UriKind.RelativeOrAbsolute)));
        }
        /// <summary>
        /// 查询记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelEnquiryRecord_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ksjc = false;//快速检测
            datacentre = false;//进入数据中心
            search = true;//进入查询记录
            sysset = false;//进入系统设置
            exitsys = false;//退出系统
            string imge = path + "\\image\\EnquaryRecordSelect.png";
            LabelRapidTest.Background = new ImageBrush(new BitmapImage(new Uri(path + "\\image\\RapidTest.png", UriKind.RelativeOrAbsolute)));
            LabelDataManage.Background = new ImageBrush(new BitmapImage(new Uri(path + "\\image\\DataManage.png", UriKind.RelativeOrAbsolute)));
            LabelEnquiryRecord.Background = new ImageBrush(new BitmapImage(new Uri(imge, UriKind.RelativeOrAbsolute)));
            LabelSystmDesign.Background = new ImageBrush(new BitmapImage(new Uri(path + "\\image\\SystemDesign.png", UriKind.RelativeOrAbsolute)));
            LabelExit.Background = new ImageBrush(new BitmapImage(new Uri(path + "\\image\\Exit.png", UriKind.RelativeOrAbsolute)));
        }
        /// <summary>
        /// 系统设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelSystmDesign_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ksjc = false;//快速检测
            datacentre = false;//进入数据中心
            search = false  ;//进入查询记录
            sysset = true;//进入系统设置
            exitsys = false;//退出系统
            string imge = path + "\\image\\SystemDesignSelect.png";
            LabelRapidTest.Background = new ImageBrush(new BitmapImage(new Uri(path + "\\image\\RapidTest.png", UriKind.RelativeOrAbsolute)));
            LabelDataManage.Background = new ImageBrush(new BitmapImage(new Uri(path + "\\image\\DataManage.png", UriKind.RelativeOrAbsolute)));
            LabelEnquiryRecord.Background = new ImageBrush(new BitmapImage(new Uri(path + "\\image\\EnquaryRecord.png", UriKind.RelativeOrAbsolute)));
            LabelSystmDesign.Background = new ImageBrush(new BitmapImage(new Uri(imge, UriKind.RelativeOrAbsolute)));
            LabelExit.Background = new ImageBrush(new BitmapImage(new Uri(path + "\\image\\Exit.png", UriKind.RelativeOrAbsolute)));
        }
        /// <summary>
        /// 退出系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelExit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ksjc = false;//快速检测
            datacentre = false;//进入数据中心
            search = false;//进入查询记录
            sysset = false ;//进入系统设置
            exitsys = true;//退出系统
            string imge = path + "\\image\\ExitSelect.png";
            LabelRapidTest.Background = new ImageBrush(new BitmapImage(new Uri(path + "\\image\\RapidTest.png", UriKind.RelativeOrAbsolute)));
            LabelDataManage.Background = new ImageBrush(new BitmapImage(new Uri(path + "\\image\\DataManage.png", UriKind.RelativeOrAbsolute)));
            LabelEnquiryRecord.Background = new ImageBrush(new BitmapImage(new Uri(path + "\\image\\EnquaryRecord.png", UriKind.RelativeOrAbsolute)));
            LabelSystmDesign.Background = new ImageBrush(new BitmapImage(new Uri(path + "\\image\\SystemDesign.png", UriKind.RelativeOrAbsolute)));
            LabelExit.Background = new ImageBrush(new BitmapImage(new Uri(imge, UriKind.RelativeOrAbsolute)));
            if (MessageBox.Show("是否退出系统？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Environment.Exit(0);
            }

        }
        /// <summary>
        /// 胶体金布局
        /// </summary>
        private UIElement JTJLayout(int channel, string sampleNum, string sampleName)
        {
                Border border = new Border()
                {
                    Width = 185,
                    Height = 350,
                    Margin = new Thickness(8),
                    BorderThickness = new Thickness(1),
                    BorderBrush = _borderBrushNormal,
                    CornerRadius = new CornerRadius(5),
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    Name = "border"
                };
                StackPanel stackPanel = new StackPanel()
                {
                    Width = 185,
                    Height = 350,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    Name = "stackPanel"
                };
                Grid grid = new Grid()
                {
                    Width = 185,
                    Background =new SolidColorBrush(Color.FromRgb(0xc0, 0xc0, 0xc0)),
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    Height = 40
                };
                Label label = new Label()
                {
                    FontSize = 20,
                    Foreground=Brushes.White ,
                    VerticalAlignment = System.Windows.VerticalAlignment.Center,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    Content = "检测通道" + (channel + 1)
                };
                Canvas rootCanvas = new Canvas()
                {
                    Width = 185,
                    Height = 200,
                    //Background = Brushes.Gray,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    Name = "rootCanvas"
                };
                grid.Children.Add(label);
                stackPanel.Children.Add(grid);
                stackPanel.Children.Add(rootCanvas);
                border.Child = stackPanel;

                return border;
        }
        /// <summary>
        /// 选择检测项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonItem_Click(object sender, RoutedEventArgs e)
        {
            ButtonItem.Background = new SolidColorBrush(Color.FromRgb(0x00, 0xbf, 0xff));
            GridSelectData.Visibility = Visibility.Visible;
            GridSelect.Visibility = Visibility.Collapsed;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            GridSelectData.Visibility = Visibility.Collapsed ;
            GridSelect.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// 检测项目和样品的确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageDetermin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GridSelectData.Visibility = Visibility.Visible ;
            GridSelect.Visibility = Visibility.Collapsed;
        }
        /// <summary>
        /// 检测项目样品的取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageCancel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GridSelectData.Visibility = Visibility.Collapsed;
            GridSelect.Visibility = Visibility.Visible;
        }
    }
}

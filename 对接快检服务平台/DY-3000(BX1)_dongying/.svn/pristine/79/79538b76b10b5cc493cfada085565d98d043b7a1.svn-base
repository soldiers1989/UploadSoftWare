using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AIO.src;
using Microsoft.Win32;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// ImportItems.xaml 的交互逻辑
    /// </summary>
    public partial class ImportItems : Window
    {
        public ImportItems()
        {
            InitializeComponent();
        }

        int[] _IndexToMethod = { 0, 1, 3, 4 };

        private int GetMethod(string name)
        {
            switch (name)
            {
                case "抑制率":
                    return 0;

                case "标准曲线":
                    return 1;

                case "动力学法":
                    return 3;

                case "系数法":
                    return 4;

                default:
                    return -1;
            }
        }

        /// <summary>
        /// 湿化学
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("是否清空现有项目重新添加?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Global.fgdItems = new List<DYFGDItemPara>();
                Global.SerializeToFile(Global.fgdItems, Global.fgdItemsFile);
            }

            OpenFileDialog op = new OpenFileDialog();
            string error = string.Empty, repeatSample = string.Empty;
            int importNum = 0;
            DataTable dt = null;
            op.Filter = "Excel (*.xls)|*.xls";
            if ((bool)(op.ShowDialog()))
            {
                dt = ExcelHelper.ImportExcel(op.FileName.Trim(), 0);
                if (dt != null && dt.Rows.Count > 0)
                {
                    int num = 0;
                    for (int i = 1; i < dt.Rows.Count; i++)
                    {
                        DYFGDItemPara item = new DYFGDItemPara();
                        item.Name = dt.Rows[i][1].ToString();//检测项目
                        item.Unit = dt.Rows[i][2].ToString();//单位
                        item.HintStr = "";//简要操作
                        item.Password = "123";//密码
                        item.SampleNum = 1;//样品编号
                        int.TryParse(dt.Rows[i][4].ToString(), out item.Wave);//波长
                        item.Method = GetMethod(dt.Rows[i][3].ToString());//检测方法
                        if (item.Method == -1)
                        {
                            MessageBox.Show("检测方法名称不正确！\r\n正确的名称：抑制率，标准曲线，动力学法，系数法");
                            return;
                        }

                        if (0 == item.Method)//抑制率
                        {
                            int.TryParse(dt.Rows[i][6].ToString(), out item.ir.PreHeatTime);//预热时间
                            int.TryParse(dt.Rows[i][7].ToString(), out item.ir.DetTime);//检测时间
                            //阴性范围
                            string[] mins = dt.Rows[i][24].ToString().Split('到');
                            if (mins.Length == 2)
                            {
                                int.TryParse(mins[0], out item.ir.MinusL);
                                int.TryParse(mins[1], out item.ir.MinusH);
                            }
                            else
                            {
                                MessageBox.Show("抑制率-阴阳性判断-阴性值格式错误！\r\n正确格式：0到50");
                                return;
                            }
                            //阳性范围
                            mins = dt.Rows[i][23].ToString().Split('到');
                            if (mins.Length == 2)
                            {
                                int.TryParse(mins[0], out item.ir.PlusL);
                                int.TryParse(mins[1], out item.ir.PlusH);
                            }
                            else
                            {
                                MessageBox.Show("抑制率-阴阳性判断-阳性值格式错误！\r\n正确格式：50到100");
                                return;
                            }
                        }
                        else if (1 == item.Method)//标准曲线
                        {
                            int.TryParse(dt.Rows[i][6].ToString(), out item.sc.PreHeatTime);//预热时间
                            int.TryParse(dt.Rows[i][7].ToString(), out item.sc.DetTime);//检测时间
                            double.TryParse(dt.Rows[i][9].ToString(), out item.sc.A0);//A曲线
                            double.TryParse(dt.Rows[i][10].ToString(), out item.sc.A1);//
                            double.TryParse(dt.Rows[i][11].ToString(), out item.sc.A2);//
                            double.TryParse(dt.Rows[i][12].ToString(), out item.sc.A3);//
                            double.TryParse(dt.Rows[i][13].ToString(), out item.sc.AFrom);//
                            double.TryParse(dt.Rows[i][14].ToString(), out item.sc.ATo);//
                            double.TryParse(dt.Rows[i][15].ToString(), out item.sc.B0);//B曲线
                            double.TryParse(dt.Rows[i][16].ToString(), out item.sc.B1);//
                            double.TryParse(dt.Rows[i][17].ToString(), out item.sc.B2);//
                            double.TryParse(dt.Rows[i][18].ToString(), out item.sc.B3);//
                            double.TryParse(dt.Rows[i][19].ToString(), out item.sc.BFrom);//
                            double.TryParse(dt.Rows[i][20].ToString(), out item.sc.BTo);//
                            double.TryParse(dt.Rows[i][21].ToString(), out item.sc.CCA);//校正曲线A
                            double.TryParse(dt.Rows[i][22].ToString(), out item.sc.CCB);//校正曲线B
                            double.TryParse("0", out item.sc.HLevel);//国标上限
                            double.TryParse("0", out item.sc.LLevel);//国标下限
                        }
                        else if (3 == item.Method)//动力学法
                        {
                            int.TryParse(dt.Rows[i][6].ToString(), out item.dn.PreHeatTime);//预热时间
                            int.TryParse(dt.Rows[i][7].ToString(), out item.dn.DetTime);//检测时间
                            double.TryParse(dt.Rows[i][9].ToString(), out item.dn.A0);//A曲线
                            double.TryParse(dt.Rows[i][10].ToString(), out item.dn.A1);//
                            double.TryParse(dt.Rows[i][11].ToString(), out item.dn.A2);//
                            double.TryParse(dt.Rows[i][12].ToString(), out item.dn.A3);//
                            double.TryParse(dt.Rows[i][13].ToString(), out item.dn.AFrom);//
                            double.TryParse(dt.Rows[i][14].ToString(), out item.dn.ATo);//
                            double.TryParse(dt.Rows[i][15].ToString(), out item.dn.B0);//B曲线
                            double.TryParse(dt.Rows[i][16].ToString(), out item.dn.B1);//
                            double.TryParse(dt.Rows[i][17].ToString(), out item.dn.B2);//
                            double.TryParse(dt.Rows[i][18].ToString(), out item.dn.B3);//
                            double.TryParse(dt.Rows[i][19].ToString(), out item.dn.BFrom);//
                            double.TryParse(dt.Rows[i][20].ToString(), out item.dn.BTo);//
                            double.TryParse(dt.Rows[i][21].ToString(), out item.dn.CCA);//校正曲线A
                            double.TryParse(dt.Rows[i][22].ToString(), out item.dn.CCB);//校正曲线B
                            double.TryParse("0", out item.dn.HLevel);//国标上限
                            double.TryParse("0", out item.dn.LLevel);//国标下限
                        }
                        else if (4 == item.Method)//系数法
                        {
                            double.TryParse(dt.Rows[i][9].ToString(), out item.co.A0);//A曲线
                            double.TryParse(dt.Rows[i][10].ToString(), out item.co.A1);//
                            double.TryParse(dt.Rows[i][11].ToString(), out item.co.A2);//
                            double.TryParse(dt.Rows[i][12].ToString(), out item.co.A3);//
                            double.TryParse(dt.Rows[i][14].ToString(), out item.co.LLevel);
                            double.TryParse(dt.Rows[i][13].ToString(), out item.co.HLevel);
                        }

                        Global.fgdItems.Add(item);
                        Global.SerializeToFile(Global.fgdItems, Global.fgdItemsFile);
                        importNum++;
                        //Console.WriteLine(dt.Rows[i][0].ToString());//序号
                        //Console.WriteLine(dt.Rows[i][8].ToString());//标准名称
                    }

                    MessageBox.Show(string.Format("本次成功导入 {0} 条检测项目！", importNum));
                }
            }
        }

        /// <summary>
        /// 干化学
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

    }
}
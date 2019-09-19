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
using System.IO;

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
            if(name =="0")
            {
                name = "抑制率";
            }
            else if (name == "1")
            {
                name = "标准曲线";
            }
            else if (name == "2")
            {
                name = "动力学法";
            }
            else if (name == "3")
            {
                name = "系数法";
            }


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
        /// <summary>
        /// 打开Excel文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectFlie_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openfd = new OpenFileDialog();

                openfd.Filter = "Excel (*.xls)|*.xls";
                if ((bool)(openfd.ShowDialog()))
                {
                    txtExcelPath.Text = openfd.FileName.Trim();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
        /// <summary>
        /// 更新分光项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFenGuang_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtExcelPath.Text.Trim() == "")
                {
                    MessageBox.Show("请选择需要更新的项目文件再单击更新","项目更新",MessageBoxButton.OK ,MessageBoxImage.Information );
                    return;
                }
                btnFenGuang.IsEnabled = false;
                if (MessageBox.Show("是否已备份检测项目,数据导入后将清空之前的数据", "系统提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Global.fgdItems = new List<DYFGDItemPara>();
                    Global.SerializeToFile(Global.fgdItems, Global.fgdItemsFile);
                }
                else
                {
                    btnFenGuang.IsEnabled = true ;
                    return;
                }
                //if (MessageBox.Show("是否清空现有项目重新添加?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                //{
                //    Global.fgdItems = new List<DYFGDItemPara>();
                //    Global.SerializeToFile(Global.fgdItems, Global.fgdItemsFile);
                //}
                string error = string.Empty, repeatSample = string.Empty;
                int importNum = 0;

                DataTable dt = ExcelHelper.ImportExcelss(txtExcelPath.Text.Trim(), 0);
                if (dt != null && dt.Rows.Count > 0)
                {
                    int num = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DYFGDItemPara item = new DYFGDItemPara();
                        item.Name = dt.Rows[i]["项目名称"].ToString();//检测项目名称
                        item.Unit = dt.Rows[i]["检测值单位"].ToString();//检测值单位
                        item.HintStr = dt.Rows[i]["简要提示"].ToString();//简要操作
                        item.Password = dt.Rows[i]["密码"].ToString();//密码
                        item.Ver = ExcelHelper.VerItem;//版本
                        //item.SampleNum = 1;//样品编号
                       
                        int.TryParse(doubledata(dt.Rows[i]["流水号"].ToString()), out item.SampleNum);//流水号是、样品编号
                        int.TryParse(doubledata(dt.Rows[i]["波长段"].ToString()), out item.Wave);//波长
   
                        item.Method = GetMethod(dt.Rows[i]["方法名称"].ToString());//检测方法
                        if (item.Method == -1)
                        {
                            MessageBox.Show("检测方法名称不正确！\r\n正确的名称：抑制率，标准曲线，动力学法，系数法");
                            return;
                        }

                        //if (0 == item.Method)//抑制率
                        //{
                            int.TryParse(doubledata(dt.Rows[i]["预热时间"].ToString()), out item.ir.PreHeatTime);//预热时间
                            int.TryParse(doubledata(dt.Rows[i]["检测时间"].ToString()), out item.ir.DetTime);//检测时间
                            //阴性范围
                            string ta= dt.Rows[i]["阴性范围a"].ToString();
                            double dou = double.Parse(ta);
                            item.ir.MinusL =(int)dou;
                            //int.TryParse(ta, out item.ir.MinusL);
                            ta = dt.Rows[i]["阴性范围b"].ToString();
                            dou = double.Parse(ta);
                            item.ir.MinusH = (int)dou;
                            //int.TryParse(ta, out item.ir.MinusH);

                            //阳性范围
                            ta = dt.Rows[i]["阳性范围a"].ToString();
                            dou = double.Parse(ta);
                            item.ir.PlusL = (int)dou;
                            //int.TryParse(ta, out item.ir.PlusL);
                            ta = dt.Rows[i]["阳性范围b"].ToString();
                            dou = double.Parse(ta);
                            item.ir.PlusH = (int)dou;
                            //int.TryParse(ta, out item.ir.PlusH);
                          
                        //}
                        //else if (1 == item.Method)//标准曲线
                        //{

                            int.TryParse(doubledata(dt.Rows[i]["预热时间"].ToString()), out item.sc.PreHeatTime);//预热时间
                            int.TryParse(doubledata(dt.Rows[i]["检测时间"].ToString()), out item.sc.DetTime);//检测时间

                            double.TryParse(dt.Rows[i]["标准曲线a1"].ToString(), out item.sc.A0);//A曲线
                            double.TryParse(dt.Rows[i]["标准曲线b1"].ToString(), out item.sc.A1);//
                            double.TryParse(dt.Rows[i]["标准曲线c1"].ToString(), out item.sc.A2);//
                            double.TryParse(dt.Rows[i]["标准曲线d1"].ToString(), out item.sc.A3);//
                            double.TryParse(dt.Rows[i]["标准曲线from1"].ToString(), out item.sc.AFrom);//
                            double.TryParse(dt.Rows[i]["标准曲线to1"].ToString(), out item.sc.ATo);//
                            double.TryParse(dt.Rows[i]["标准曲线a0"].ToString(), out item.sc.B0);//B曲线
                            double.TryParse(dt.Rows[i]["标准曲线b0"].ToString(), out item.sc.B1);//
                            double.TryParse(dt.Rows[i]["标准曲线c0"].ToString(), out item.sc.B2);//
                            double.TryParse(dt.Rows[i]["标准曲线d0"].ToString(), out item.sc.B3);//
                            double.TryParse(dt.Rows[i]["标准曲线from0"].ToString(), out item.sc.BFrom);//
                            double.TryParse(dt.Rows[i]["标准曲线to0"].ToString(), out item.sc.BTo);//
                            double.TryParse(dt.Rows[i]["校正曲线a"].ToString(), out item.sc.CCA);//校正曲线A
                            double.TryParse(dt.Rows[i]["校正曲线b"].ToString(), out item.sc.CCB);//校正曲线B
                            double.TryParse(dt.Rows[i]["国标值上"].ToString(), out item.sc.HLevel);//国标上限
                            double.TryParse(dt.Rows[i]["国标值下"].ToString(), out item.sc.LLevel);//国标下限
                        //}
                        //else if (3 == item.Method)//动力学法
                        //{
                            int.TryParse(doubledata(dt.Rows[i]["预热时间"].ToString()), out item.dn.PreHeatTime);//预热时间
                            int.TryParse(doubledata(dt.Rows[i]["检测时间"].ToString()), out item.dn.DetTime);//检测时间

                            double.TryParse(dt.Rows[i]["标准曲线a1"].ToString(), out item.dn.A0);//A曲线
                            double.TryParse(dt.Rows[i]["标准曲线b1"].ToString(), out item.dn.A1);//
                            double.TryParse(dt.Rows[i]["标准曲线c1"].ToString(), out item.dn.A2);//
                            double.TryParse(dt.Rows[i]["标准曲线d1"].ToString(), out item.dn.A3);//
                            double.TryParse(dt.Rows[i]["标准曲线from1"].ToString(), out item.dn.AFrom);//
                            double.TryParse(dt.Rows[i]["标准曲线to1"].ToString(), out item.dn.ATo);//
                            double.TryParse(dt.Rows[i]["标准曲线a0"].ToString(), out item.dn.B0);//B曲线
                            double.TryParse(dt.Rows[i]["标准曲线b0"].ToString(), out item.dn.B1);//
                            double.TryParse(dt.Rows[i]["标准曲线c0"].ToString(), out item.dn.B2);//
                            double.TryParse(dt.Rows[i]["标准曲线d0"].ToString(), out item.dn.B3);//
                            double.TryParse(dt.Rows[i]["标准曲线from0"].ToString(), out item.dn.BFrom);//
                            double.TryParse(dt.Rows[i]["标准曲线to0"].ToString(), out item.dn.BTo);//
                            double.TryParse(dt.Rows[i]["校正曲线a"].ToString(), out item.dn.CCA);//校正曲线A
                            double.TryParse(dt.Rows[i]["校正曲线b"].ToString(), out item.dn.CCB);//校正曲线B
                            double.TryParse(dt.Rows[i]["国标值上"].ToString(), out item.dn.HLevel);//国标上限
                            double.TryParse(dt.Rows[i]["国标值下"].ToString(), out item.dn.LLevel);//国标下限
                        //}
                        //else if (4 == item.Method)//系数法
                        //{
                            double.TryParse(dt.Rows[i]["标准曲线a0"].ToString(), out item.co.A0);//A曲线
                            double.TryParse(dt.Rows[i]["标准曲线b0"].ToString(), out item.co.A1);//
                            double.TryParse(dt.Rows[i]["标准曲线c0"].ToString(), out item.co.A2);//
                            double.TryParse(dt.Rows[i]["标准曲线d0"].ToString(), out item.co.A3);//
                            double.TryParse(dt.Rows[i]["国标值上"].ToString(), out item.sc.HLevel);//国标上限
                            double.TryParse(dt.Rows[i]["国标值下"].ToString(), out item.sc.LLevel);//国标下限
                        //}

                        Global.fgdItems.Add(item);
                        Global.SerializeToFile(Global.fgdItems, Global.fgdItemsFile);
                        importNum++;
                        //Console.WriteLine(dt.Rows[i][0].ToString());//序号
                        //Console.WriteLine(dt.Rows[i][8].ToString());//标准名称
                    }

                    MessageBox.Show(string.Format("本次成功导入 {0} 条检测项目！", importNum));
                    btnFenGuang.IsEnabled = true ;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message );
                btnFenGuang.IsEnabled = true;
            }
        }
        /// <summary>
        /// 返回浮点数据转换成整型
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string doubledata(string data)
        {
            string rtn = "";
            if (data.Contains("."))
            {
                double dd = double.Parse(data);
                int ss = (int)dd;
                rtn = ss.ToString();
            }
            else
            {
                rtn = data;
            }

            return rtn;
        }
        /// <summary>
        /// 更新胶体金项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAllUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtExcelPath.Text.Trim() == "")
                {
                    MessageBox.Show("请选择需要更新的项目文件再单击更新", "项目更新", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                btnAllUpdate.IsEnabled = false;
                //是否是新模块
                bool IsNewModel = (Global.JtjVersionInfo != null && Global.JtjVersionInfo[1] >= 0x20);

                if (MessageBox.Show("是否已备份检测项目,数据导入后将清空之前的数据", "系统提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Global.jtjItems = new List<DYJTJItemPara>();
                    Global.SerializeToFile(Global.jtjItems, Global.jtjItemsFile);
                }
                else
                {
                    btnAllUpdate.IsEnabled =true ;
                    return;
                }
                //if (MessageBox.Show("是否清空现有项目重新添加?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                //{
                //    Global.jtjItems = new List<DYJTJItemPara>();
                //    Global.SerializeToFile(Global.jtjItems, Global.jtjItemsFile);
                //}
                int importNum = 0;
                DataTable dt = ExcelHelper.ImportExcelss(txtExcelPath.Text.Trim(), 1);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for(int i=0;i<dt.Rows.Count ;i++)
                    {
                        DYJTJItemPara _item = new DYJTJItemPara();
                        _item.Name = dt.Rows[i]["项目名称"].ToString();//检测项目名称
                        int.TryParse(dt.Rows[i]["方法（1消线;2比色）"].ToString(), out _item.Method);//检测方法
                        _item.Method = _item.Method - 1;
                        _item.Unit = dt.Rows[i]["检测值单位"].ToString();
                        _item.HintStr = dt.Rows[i]["简要提示"].ToString();
                        _item.Password = dt.Rows[i]["密码"].ToString();
                        int.TryParse(dt.Rows[i]["流水号"].ToString(),out _item.SampleNum);
                        _item.Ver = ExcelHelper.VerItem;//版本
                        //_item.SampleNum = dt.Rows[i]["流水号"].ToString();
                        _item.Hole[0].Use = true;
                        _item.Hole[1].Use = true;
                        _item.Hole[2].Use = true;
                        _item.Hole[3].Use = true;
                        //胶体金计算条件  目前没用
                        int.TryParse("1", out _item.pointCNum);
                        int.TryParse("2", out _item.pointTNum);
                        int.TryParse("3", out _item.maxPoint);
                        //if (IsNewModel)
                        //{
                            double.TryParse(dt.Rows[i]["C1值"].ToString(), out _item.newInvalidC);
                        //}
                        //else
                        //{
                            double.TryParse(dt.Rows[i]["C2值"].ToString(), out _item.InvalidC );
                        //}

                        //定性消线
                        //if (0== _item.Method)
                        //{
                            //if (IsNewModel)
                            //{
                            _item.dxxx.newMaxT = new double[Global.deviceHole.SxtCount];
                            _item.dxxx.newMinT = new double[Global.deviceHole.SxtCount];
                            if (Global.deviceHole.SxtCount == 2)
                            {
                                double.TryParse(dt.Rows[i]["T1A值"].ToString(), out _item.dxxx.newMinT[0]);
                                double.TryParse(dt.Rows[i]["T1B值"].ToString(), out _item.dxxx.newMaxT[0]);
                                double.TryParse(dt.Rows[i]["T1A值"].ToString(), out _item.dxxx.newMinT[1]);
                                double.TryParse(dt.Rows[i]["T1B值"].ToString(), out _item.dxxx.newMaxT[1]);
                            }
                            else if (Global.deviceHole.SxtCount == 4)
                            {
                                double.TryParse(dt.Rows[i]["T1A值"].ToString(), out _item.dxxx.newMinT[0]);
                                double.TryParse(dt.Rows[i]["T1B值"].ToString(), out _item.dxxx.newMaxT[0]);
                                double.TryParse(dt.Rows[i]["T1A值"].ToString(), out _item.dxxx.newMinT[1]);
                                double.TryParse(dt.Rows[i]["T1B值"].ToString(), out _item.dxxx.newMaxT[1]);

                                double.TryParse(dt.Rows[i]["T1A值"].ToString(), out _item.dxxx.newMinT[2]);
                                double.TryParse(dt.Rows[i]["T1B值"].ToString(), out _item.dxxx.newMaxT[2]);
                                double.TryParse(dt.Rows[i]["T1A值"].ToString(), out _item.dxxx.newMinT[3]);
                                double.TryParse(dt.Rows[i]["T1B值"].ToString(), out _item.dxxx.newMaxT[3]);
                            }

                            //}
                            //else
                            //{
                            _item.dxxx.MaxT = new double[Global.deviceHole.SxtCount];
                            _item.dxxx.MinT = new double[Global.deviceHole.SxtCount];
                            if (Global.deviceHole.SxtCount == 2)
                            {
                                double.TryParse(dt.Rows[i]["T2A值"].ToString(), out _item.dxxx.MinT[0]);
                                double.TryParse(dt.Rows[i]["T2B值"].ToString(), out _item.dxxx.MaxT[0]);
                                double.TryParse(dt.Rows[i]["T2A值"].ToString(), out _item.dxxx.MinT[1]);
                                double.TryParse(dt.Rows[i]["T2B值"].ToString(), out _item.dxxx.MaxT[1]);
                            }
                            else if (Global.deviceHole.SxtCount == 4)
                            {
                                double.TryParse(dt.Rows[i]["T2A值"].ToString(), out _item.dxxx.MinT[0]);
                                double.TryParse(dt.Rows[i]["T2B值"].ToString(), out _item.dxxx.MaxT[0]);
                                double.TryParse(dt.Rows[i]["T2A值"].ToString(), out _item.dxxx.MinT[1]);
                                double.TryParse(dt.Rows[i]["T2B值"].ToString(), out _item.dxxx.MaxT[1]);

                                double.TryParse(dt.Rows[i]["T2A值"].ToString(), out _item.dxxx.MinT[2]);
                                double.TryParse(dt.Rows[i]["T2B值"].ToString(), out _item.dxxx.MaxT[2]);
                                double.TryParse(dt.Rows[i]["T2A值"].ToString(), out _item.dxxx.MinT[3]);
                                double.TryParse(dt.Rows[i]["T2B值"].ToString(), out _item.dxxx.MaxT[3]);
                            }

                            //}
                            //}
                            ////定性比色
                            //else if (1== _item.Method)
                            //{
                            //if (IsNewModel)
                            //{
                            _item.dxbs.newMaxT = new double[Global.deviceHole.SxtCount];
                            _item.dxbs.newMinT = new double[Global.deviceHole.SxtCount];
                            if (Global.deviceHole.SxtCount == 2)
                            {
                                double.TryParse(dt.Rows[i]["T1/C1A值"].ToString(), out _item.dxbs.newMinT[0]);
                                double.TryParse(dt.Rows[i]["T1/C1B值"].ToString(), out _item.dxbs.newMaxT[0]);
                                double.TryParse(dt.Rows[i]["T1/C1A值"].ToString(), out _item.dxbs.newMinT[1]);
                                double.TryParse(dt.Rows[i]["T1/C1B值"].ToString(), out _item.dxbs.newMaxT[1]);
                            }
                            else if (Global.deviceHole.SxtCount == 4)
                            {
                                double.TryParse(dt.Rows[i]["T1/C1A值"].ToString(), out _item.dxbs.newMinT[0]);
                                double.TryParse(dt.Rows[i]["T1/C1B值"].ToString(), out _item.dxbs.newMaxT[0]);
                                double.TryParse(dt.Rows[i]["T1/C1A值"].ToString(), out _item.dxbs.newMinT[1]);
                                double.TryParse(dt.Rows[i]["T1/C1B值"].ToString(), out _item.dxbs.newMaxT[1]);

                                double.TryParse(dt.Rows[i]["T1/C1A值"].ToString(), out _item.dxbs.newMinT[2]);
                                double.TryParse(dt.Rows[i]["T1/C1B值"].ToString(), out _item.dxbs.newMaxT[2]);
                                double.TryParse(dt.Rows[i]["T1/C1A值"].ToString(), out _item.dxbs.newMinT[3]);
                                double.TryParse(dt.Rows[i]["T1/C1B值"].ToString(), out _item.dxbs.newMaxT[3]);
                            }
                            //}
                            //else
                            //{
                            _item.dxbs.MaxT = new double[Global.deviceHole.SxtCount];
                            _item.dxbs.MinT = new double[Global.deviceHole.SxtCount];
                            if (Global.deviceHole.SxtCount == 2)
                            {
                                double.TryParse(dt.Rows[i]["T2/C2A值"].ToString(), out _item.dxbs.MinT[0]);
                                double.TryParse(dt.Rows[i]["T2/C2B值"].ToString(), out _item.dxbs.MaxT[0]);
                                double.TryParse(dt.Rows[i]["T2/C2A值"].ToString(), out _item.dxbs.MinT[1]);
                                double.TryParse(dt.Rows[i]["T2/C2B值"].ToString(), out _item.dxbs.MaxT[1]);
                            }
                            else if (Global.deviceHole.SxtCount == 4)
                            {
                                double.TryParse(dt.Rows[i]["T2/C2A值"].ToString(), out _item.dxbs.MinT[0]);
                                double.TryParse(dt.Rows[i]["T2/C2B值"].ToString(), out _item.dxbs.MaxT[0]);
                                double.TryParse(dt.Rows[i]["T2/C2A值"].ToString(), out _item.dxbs.MinT[1]);
                                double.TryParse(dt.Rows[i]["T2/C2B值"].ToString(), out _item.dxbs.MaxT[1]);

                                double.TryParse(dt.Rows[i]["T2/C2A值"].ToString(), out _item.dxbs.MinT[2]);
                                double.TryParse(dt.Rows[i]["T2/C2B值"].ToString(), out _item.dxbs.MaxT[2]);
                                double.TryParse(dt.Rows[i]["T2/C2A值"].ToString(), out _item.dxbs.MinT[3]);
                                double.TryParse(dt.Rows[i]["T2/C2B值"].ToString(), out _item.dxbs.MaxT[3]);
                            }

                        Global.jtjItems.Add(_item);
                        Global.SerializeToFile(Global.jtjItems, Global.jtjItemsFile);
                        importNum++;
                    }
                }
                MessageBox.Show(string.Format("本次成功导入 {0} 条检测项目！", importNum));
                btnAllUpdate.IsEnabled = true ;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                btnAllUpdate.IsEnabled = true;
            }
        }
        /// <summary>
        /// 更新干化学项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGanHuaXue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtExcelPath.Text.Trim() == "")
                {
                    MessageBox.Show("请选择需要更新的项目文件再单击更新", "项目更新", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                btnGanHuaXue.IsEnabled = false;
                if (MessageBox.Show("是否已备份检测项目,数据导入后将清空之前的数据", "系统提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Global.gszItems = new List<DYGSZItemPara>();
                    Global.SerializeToFile(Global.gszItems, Global.gszItemsFile);
                }
                else
                {
                    btnGanHuaXue.IsEnabled = true;
                    return;
                }
                // if (MessageBox.Show("是否清空现有项目重新添加?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                //{
                //    Global.gszItems = new List<DYGSZItemPara>();
                //    Global.SerializeToFile(Global.gszItems, Global.gszItemsFile);
                //}

                DataTable dt = ExcelHelper.ImportExcelss(txtExcelPath.Text.Trim(), 2);
                int importNum = 0;
                if (dt != null && dt.Rows.Count > 0)
                {
                    for(int i=0;i<dt.Rows.Count;i++)
                    {
                        DYGSZItemPara _item = new DYGSZItemPara();
                        _item.Name = dt.Rows[i]["项目名称"].ToString();//项目名称
                        _item.Unit = dt.Rows[i]["检测值单位"].ToString();//项目检测单位
                        _item.HintStr = dt.Rows[i]["简要提示"].ToString();//简要提示:新添加实验--注释
                        _item.Password = dt.Rows[i]["密码"].ToString();//密码
                        Int32.TryParse(dt.Rows[i]["流水号"].ToString(), out _item.SampleNum);//样品编号
                        _item.Ver = ExcelHelper.VerItem;//版本

                        //检测方法选择
                        if (dt.Rows[i]["方法名称"].ToString() == "定性")
                        {
                            _item.Method = 0;
                        }
                        else if (dt.Rows[i]["方法名称"].ToString() == "定量")
                        {
                            _item.Method = 1;
                        }
                        _item.Hole[0].Use =true;
                        _item.Hole[1].Use = true;
                        _item.Hole[2].Use = true;
                        _item.Hole[3].Use = true;

                        //if (0 == _item.Method)
                        //{
                            if (_item.dx.Min == null || _item.dx.Max == null)
                            {
                                _item.dx.Min = new double[2];
                                _item.dx.Max = new double[2];
                            }
                            double.TryParse(dt.Rows[i]["1Min"].ToString(), out _item.dx.Min[0]);
                            double.TryParse(dt.Rows[i]["2Min"].ToString(), out _item.dx.Min[1]);
                            double.TryParse(dt.Rows[i]["1Max"].ToString(), out _item.dx.Max[0]);
                            double.TryParse(dt.Rows[i]["2Max"].ToString(), out _item.dx.Max[1]);
                        //}
                        //else if (1 == _item.Method)
                        //{
                            //_item.dl.RGBSel = ComboBoxDLRGBSel.SelectedIndex;
                            Double.TryParse(dt.Rows[i]["标准曲线a0"].ToString(), out _item.dl.A0);
                            Double.TryParse(dt.Rows[i]["标准曲线b0"].ToString(), out _item.dl.A1);
                            Double.TryParse(dt.Rows[i]["标准曲线c0"].ToString(), out _item.dl.A2);
                            Double.TryParse(dt.Rows[i]["标准曲线d0"].ToString(), out _item.dl.A3);
                            Double.TryParse(dt.Rows[i]["校正曲线a"].ToString(), out _item.dl.B0);
                            Double.TryParse(dt.Rows[i]["校正曲线b"].ToString(), out _item.dl.B1);
                            Double.TryParse(dt.Rows[i]["上限值"].ToString(), out _item.dl.LimitL);
                            Double.TryParse(dt.Rows[i]["下限值"].ToString(), out _item.dl.LimitH);

                        //}

                        Global.gszItems.Add(_item);
                        Global.SerializeToFile(Global.gszItems, Global.gszItemsFile);
                        importNum++;
                    }
                }
                MessageBox.Show(string.Format("本次成功导入 {0} 条检测项目！", importNum));
                btnGanHuaXue.IsEnabled = true ;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                btnGanHuaXue.IsEnabled = true;
            }
        }
        /// <summary>
        /// 更新全部项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtExcelPath.Text.Trim() == "")
                {
                    MessageBox.Show("请选择需要更新的项目文件再单击更新", "项目更新", MessageBoxButton.OK, MessageBoxImage.Information);

                    return;
                }
                btnAll.IsEnabled = false;
                if (MessageBox.Show("是否已备份检测项目,数据导入后将清空之前的数据", "系统提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    //分光
                    Global.fgdItems = new List<DYFGDItemPara>();
                    Global.SerializeToFile(Global.fgdItems, Global.fgdItemsFile);
                    //胶体金
                    Global.jtjItems = new List<DYJTJItemPara>();
                    Global.SerializeToFile(Global.jtjItems, Global.jtjItemsFile);
                    //干化学
                    Global.gszItems = new List<DYGSZItemPara>();
                    Global.SerializeToFile(Global.gszItems, Global.gszItemsFile);
                }
                else
                {
                    btnAll.IsEnabled = true;
                    return;
                }

                string error = string.Empty, repeatSample = string.Empty;

                //分光
                DataTable dt = ExcelHelper.ImportExcelss(txtExcelPath.Text.Trim(), 0);
                if (dt != null && dt.Rows.Count > 0)
                {
                    int num = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DYFGDItemPara item = new DYFGDItemPara();
                        item.Name = dt.Rows[i]["项目名称"].ToString();//检测项目名称
                        item.Unit = dt.Rows[i]["检测值单位"].ToString();//检测值单位
                        item.HintStr = dt.Rows[i]["简要提示"].ToString();//简要操作
                        item.Password = dt.Rows[i]["密码"].ToString();//密码
                        item.Ver = ExcelHelper.VerItem;//版本

                        int.TryParse(doubledata(dt.Rows[i]["流水号"].ToString()), out item.SampleNum);//流水号是、样品编号
                        int.TryParse(doubledata(dt.Rows[i]["波长段"].ToString()), out item.Wave);//波长

                        item.Method = GetMethod(dt.Rows[i]["方法名称"].ToString());//检测方法
                        if (item.Method == -1)
                        {
                            MessageBox.Show("检测方法名称不正确！\r\n正确的名称：抑制率，标准曲线，动力学法，系数法");
                            return;
                        }

                        //if (0 == item.Method)//抑制率
                        //{
                        int.TryParse(doubledata(dt.Rows[i]["预热时间"].ToString()), out item.ir.PreHeatTime);//预热时间
                        int.TryParse(doubledata(dt.Rows[i]["检测时间"].ToString()), out item.ir.DetTime);//检测时间
                        //阴性范围
                        string ta = dt.Rows[i]["阴性范围a"].ToString();
                        double dou = double.Parse(ta);
                        item.ir.MinusL = (int)dou;
                        //int.TryParse(ta, out item.ir.MinusL);
                        ta = dt.Rows[i]["阴性范围b"].ToString();
                        dou = double.Parse(ta);
                        item.ir.MinusH = (int)dou;
                        //int.TryParse(ta, out item.ir.MinusH);

                        //阳性范围
                        ta = dt.Rows[i]["阳性范围a"].ToString();
                        dou = double.Parse(ta);
                        item.ir.PlusL = (int)dou;
                        //int.TryParse(ta, out item.ir.PlusL);
                        ta = dt.Rows[i]["阳性范围b"].ToString();
                        dou = double.Parse(ta);
                        item.ir.PlusH = (int)dou;
                        //int.TryParse(ta, out item.ir.PlusH);

                        //}
                        //else if (1 == item.Method)//标准曲线
                        //{

                        int.TryParse(doubledata(dt.Rows[i]["预热时间"].ToString()), out item.sc.PreHeatTime);//预热时间
                        int.TryParse(doubledata(dt.Rows[i]["检测时间"].ToString()), out item.sc.DetTime);//检测时间

                        double.TryParse(dt.Rows[i]["标准曲线a1"].ToString(), out item.sc.A0);//A曲线
                        double.TryParse(dt.Rows[i]["标准曲线b1"].ToString(), out item.sc.A1);//
                        double.TryParse(dt.Rows[i]["标准曲线c1"].ToString(), out item.sc.A2);//
                        double.TryParse(dt.Rows[i]["标准曲线d1"].ToString(), out item.sc.A3);//
                        double.TryParse(dt.Rows[i]["标准曲线from1"].ToString(), out item.sc.AFrom);//
                        double.TryParse(dt.Rows[i]["标准曲线to1"].ToString(), out item.sc.ATo);//
                        double.TryParse(dt.Rows[i]["标准曲线a0"].ToString(), out item.sc.B0);//B曲线
                        double.TryParse(dt.Rows[i]["标准曲线b0"].ToString(), out item.sc.B1);//
                        double.TryParse(dt.Rows[i]["标准曲线c0"].ToString(), out item.sc.B2);//
                        double.TryParse(dt.Rows[i]["标准曲线d0"].ToString(), out item.sc.B3);//
                        double.TryParse(dt.Rows[i]["标准曲线from0"].ToString(), out item.sc.BFrom);//
                        double.TryParse(dt.Rows[i]["标准曲线to0"].ToString(), out item.sc.BTo);//
                        double.TryParse(dt.Rows[i]["校正曲线a"].ToString(), out item.sc.CCA);//校正曲线A
                        double.TryParse(dt.Rows[i]["校正曲线b"].ToString(), out item.sc.CCB);//校正曲线B
                        double.TryParse(dt.Rows[i]["国标值上"].ToString(), out item.sc.HLevel);//国标上限
                        double.TryParse(dt.Rows[i]["国标值下"].ToString(), out item.sc.LLevel);//国标下限
                        //}
                        //else if (3 == item.Method)//动力学法
                        //{
                        int.TryParse(doubledata(dt.Rows[i]["预热时间"].ToString()), out item.dn.PreHeatTime);//预热时间
                        int.TryParse(doubledata(dt.Rows[i]["检测时间"].ToString()), out item.dn.DetTime);//检测时间

                        double.TryParse(dt.Rows[i]["标准曲线a1"].ToString(), out item.dn.A0);//A曲线
                        double.TryParse(dt.Rows[i]["标准曲线b1"].ToString(), out item.dn.A1);//
                        double.TryParse(dt.Rows[i]["标准曲线c1"].ToString(), out item.dn.A2);//
                        double.TryParse(dt.Rows[i]["标准曲线d1"].ToString(), out item.dn.A3);//
                        double.TryParse(dt.Rows[i]["标准曲线from1"].ToString(), out item.dn.AFrom);//
                        double.TryParse(dt.Rows[i]["标准曲线to1"].ToString(), out item.dn.ATo);//
                        double.TryParse(dt.Rows[i]["标准曲线a0"].ToString(), out item.dn.B0);//B曲线
                        double.TryParse(dt.Rows[i]["标准曲线b0"].ToString(), out item.dn.B1);//
                        double.TryParse(dt.Rows[i]["标准曲线c0"].ToString(), out item.dn.B2);//
                        double.TryParse(dt.Rows[i]["标准曲线d0"].ToString(), out item.dn.B3);//
                        double.TryParse(dt.Rows[i]["标准曲线from0"].ToString(), out item.dn.BFrom);//
                        double.TryParse(dt.Rows[i]["标准曲线to0"].ToString(), out item.dn.BTo);//
                        double.TryParse(dt.Rows[i]["校正曲线a"].ToString(), out item.dn.CCA);//校正曲线A
                        double.TryParse(dt.Rows[i]["校正曲线b"].ToString(), out item.dn.CCB);//校正曲线B
                        double.TryParse(dt.Rows[i]["国标值上"].ToString(), out item.dn.HLevel);//国标上限
                        double.TryParse(dt.Rows[i]["国标值下"].ToString(), out item.dn.LLevel);//国标下限
                        //}
                        //else if (4 == item.Method)//系数法
                        //{
                        double.TryParse(dt.Rows[i]["标准曲线a0"].ToString(), out item.co.A0);//A曲线
                        double.TryParse(dt.Rows[i]["标准曲线b0"].ToString(), out item.co.A1);//
                        double.TryParse(dt.Rows[i]["标准曲线c0"].ToString(), out item.co.A2);//
                        double.TryParse(dt.Rows[i]["标准曲线d0"].ToString(), out item.co.A3);//
                        double.TryParse(dt.Rows[i]["国标值上"].ToString(), out item.sc.HLevel);//国标上限
                        double.TryParse(dt.Rows[i]["国标值下"].ToString(), out item.sc.LLevel);//国标下限
                        //}
                        Global.fgdItems.Add(item);
                        Global.SerializeToFile(Global.fgdItems, Global.fgdItemsFile);
                    }
                }
                //胶体金
                dt = ExcelHelper.ImportExcelss(txtExcelPath.Text.Trim(), 1);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DYJTJItemPara _item = new DYJTJItemPara();
                        _item.Name = dt.Rows[i]["项目名称"].ToString();//检测项目名称
                        int.TryParse(dt.Rows[i]["方法（1消线;2比色）"].ToString(), out _item.Method);//检测方法
                        _item.Method = _item.Method - 1;
                        _item.Unit = dt.Rows[i]["检测值单位"].ToString();
                        _item.HintStr = dt.Rows[i]["简要提示"].ToString();
                        _item.Password = dt.Rows[i]["密码"].ToString();
                        _item.Ver = ExcelHelper.VerItem;//版本

                        int.TryParse(dt.Rows[i]["流水号"].ToString(), out _item.SampleNum);
                        //_item.SampleNum = dt.Rows[i]["流水号"].ToString();
                        _item.Hole[0].Use = true;
                        _item.Hole[1].Use = true;
                        _item.Hole[2].Use = true;
                        _item.Hole[3].Use = true;
                        //胶体金计算条件  目前没用
                        int.TryParse("1", out _item.pointCNum);
                        int.TryParse("2", out _item.pointTNum);
                        int.TryParse("3", out _item.maxPoint);

                        double.TryParse(dt.Rows[i]["C1值"].ToString(), out _item.newInvalidC);

                        double.TryParse(dt.Rows[i]["C2值"].ToString(), out _item.InvalidC);

                        _item.dxxx.newMaxT = new double[Global.deviceHole.SxtCount];
                        _item.dxxx.newMinT = new double[Global.deviceHole.SxtCount];
                        if (Global.deviceHole.SxtCount == 2)
                        {
                            double.TryParse(dt.Rows[i]["T1A值"].ToString(), out _item.dxxx.newMinT[0]);
                            double.TryParse(dt.Rows[i]["T1B值"].ToString(), out _item.dxxx.newMaxT[0]);
                            double.TryParse(dt.Rows[i]["T1A值"].ToString(), out _item.dxxx.newMinT[1]);
                            double.TryParse(dt.Rows[i]["T1B值"].ToString(), out _item.dxxx.newMaxT[1]);
                        }
                        else if (Global.deviceHole.SxtCount == 4)
                        {
                            double.TryParse(dt.Rows[i]["T1A值"].ToString(), out _item.dxxx.newMinT[0]);
                            double.TryParse(dt.Rows[i]["T1B值"].ToString(), out _item.dxxx.newMaxT[0]);
                            double.TryParse(dt.Rows[i]["T1A值"].ToString(), out _item.dxxx.newMinT[1]);
                            double.TryParse(dt.Rows[i]["T1B值"].ToString(), out _item.dxxx.newMaxT[1]);

                            double.TryParse(dt.Rows[i]["T1A值"].ToString(), out _item.dxxx.newMinT[2]);
                            double.TryParse(dt.Rows[i]["T1B值"].ToString(), out _item.dxxx.newMaxT[2]);
                            double.TryParse(dt.Rows[i]["T1A值"].ToString(), out _item.dxxx.newMinT[3]);
                            double.TryParse(dt.Rows[i]["T1B值"].ToString(), out _item.dxxx.newMaxT[3]);
                        }


                        _item.dxxx.MaxT = new double[Global.deviceHole.SxtCount];
                        _item.dxxx.MinT = new double[Global.deviceHole.SxtCount];
                        if (Global.deviceHole.SxtCount == 2)
                        {
                            double.TryParse(dt.Rows[i]["T2A值"].ToString(), out _item.dxxx.MinT[0]);
                            double.TryParse(dt.Rows[i]["T2B值"].ToString(), out _item.dxxx.MaxT[0]);
                            double.TryParse(dt.Rows[i]["T2A值"].ToString(), out _item.dxxx.MinT[1]);
                            double.TryParse(dt.Rows[i]["T2B值"].ToString(), out _item.dxxx.MaxT[1]);
                        }
                        else if (Global.deviceHole.SxtCount == 4)
                        {
                            double.TryParse(dt.Rows[i]["T2A值"].ToString(), out _item.dxxx.MinT[0]);
                            double.TryParse(dt.Rows[i]["T2B值"].ToString(), out _item.dxxx.MaxT[0]);
                            double.TryParse(dt.Rows[i]["T2A值"].ToString(), out _item.dxxx.MinT[1]);
                            double.TryParse(dt.Rows[i]["T2B值"].ToString(), out _item.dxxx.MaxT[1]);

                            double.TryParse(dt.Rows[i]["T2A值"].ToString(), out _item.dxxx.MinT[2]);
                            double.TryParse(dt.Rows[i]["T2B值"].ToString(), out _item.dxxx.MaxT[2]);
                            double.TryParse(dt.Rows[i]["T2A值"].ToString(), out _item.dxxx.MinT[3]);
                            double.TryParse(dt.Rows[i]["T2B值"].ToString(), out _item.dxxx.MaxT[3]);
                        }

                        _item.dxbs.newMaxT = new double[Global.deviceHole.SxtCount];
                        _item.dxbs.newMinT = new double[Global.deviceHole.SxtCount];
                        if (Global.deviceHole.SxtCount == 2)
                        {
                            double.TryParse(dt.Rows[i]["T1/C1A值"].ToString(), out _item.dxbs.newMinT[0]);
                            double.TryParse(dt.Rows[i]["T1/C1B值"].ToString(), out _item.dxbs.newMaxT[0]);
                            double.TryParse(dt.Rows[i]["T1/C1A值"].ToString(), out _item.dxbs.newMinT[1]);
                            double.TryParse(dt.Rows[i]["T1/C1B值"].ToString(), out _item.dxbs.newMaxT[1]);
                        }
                        else if (Global.deviceHole.SxtCount == 4)
                        {
                            double.TryParse(dt.Rows[i]["T1/C1A值"].ToString(), out _item.dxbs.newMinT[0]);
                            double.TryParse(dt.Rows[i]["T1/C1B值"].ToString(), out _item.dxbs.newMaxT[0]);
                            double.TryParse(dt.Rows[i]["T1/C1A值"].ToString(), out _item.dxbs.newMinT[1]);
                            double.TryParse(dt.Rows[i]["T1/C1B值"].ToString(), out _item.dxbs.newMaxT[1]);

                            double.TryParse(dt.Rows[i]["T1/C1A值"].ToString(), out _item.dxbs.newMinT[2]);
                            double.TryParse(dt.Rows[i]["T1/C1B值"].ToString(), out _item.dxbs.newMaxT[2]);
                            double.TryParse(dt.Rows[i]["T1/C1A值"].ToString(), out _item.dxbs.newMinT[3]);
                            double.TryParse(dt.Rows[i]["T1/C1B值"].ToString(), out _item.dxbs.newMaxT[3]);
                        }

                        _item.dxbs.MaxT = new double[Global.deviceHole.SxtCount];
                        _item.dxbs.MinT = new double[Global.deviceHole.SxtCount];
                        if (Global.deviceHole.SxtCount == 2)
                        {
                            double.TryParse(dt.Rows[i]["T2/C2A值"].ToString(), out _item.dxbs.MinT[0]);
                            double.TryParse(dt.Rows[i]["T2/C2B值"].ToString(), out _item.dxbs.MaxT[0]);
                            double.TryParse(dt.Rows[i]["T2/C2A值"].ToString(), out _item.dxbs.MinT[1]);
                            double.TryParse(dt.Rows[i]["T2/C2B值"].ToString(), out _item.dxbs.MaxT[1]);

                        }
                        else if (Global.deviceHole.SxtCount == 4)
                        {
                            double.TryParse(dt.Rows[i]["T2/C2A值"].ToString(), out _item.dxbs.MinT[0]);
                            double.TryParse(dt.Rows[i]["T2/C2B值"].ToString(), out _item.dxbs.MaxT[0]);
                            double.TryParse(dt.Rows[i]["T2/C2A值"].ToString(), out _item.dxbs.MinT[1]);
                            double.TryParse(dt.Rows[i]["T2/C2B值"].ToString(), out _item.dxbs.MaxT[1]);

                            double.TryParse(dt.Rows[i]["T2/C2A值"].ToString(), out _item.dxbs.MinT[2]);
                            double.TryParse(dt.Rows[i]["T2/C2B值"].ToString(), out _item.dxbs.MaxT[2]);
                            double.TryParse(dt.Rows[i]["T2/C2A值"].ToString(), out _item.dxbs.MinT[3]);
                            double.TryParse(dt.Rows[i]["T2/C2B值"].ToString(), out _item.dxbs.MaxT[3]);
                        }

                        Global.jtjItems.Add(_item);
                        Global.SerializeToFile(Global.jtjItems, Global.jtjItemsFile);
                    }

                }
                //干化学

                dt = ExcelHelper.ImportExcelss(txtExcelPath.Text.Trim(), 2);

                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DYGSZItemPara _item = new DYGSZItemPara();
                        _item.Name = dt.Rows[i]["项目名称"].ToString();//项目名称
                        _item.Unit = dt.Rows[i]["检测值单位"].ToString();//项目检测单位
                        _item.HintStr = dt.Rows[i]["简要提示"].ToString();//简要提示:新添加实验--注释
                        _item.Password = dt.Rows[i]["密码"].ToString();//密码
                        _item.Ver = ExcelHelper.VerItem;//版本

                        Int32.TryParse(dt.Rows[i]["流水号"].ToString(), out _item.SampleNum);//样品编号

                        //检测方法选择
                        if (dt.Rows[i]["方法名称"].ToString() == "定性")
                        {
                            _item.Method = 0;
                        }
                        else if (dt.Rows[i]["方法名称"].ToString() == "定量")
                        {
                            _item.Method = 1;
                        }
                        _item.Hole[0].Use = true;
                        _item.Hole[1].Use = true;
                        _item.Hole[2].Use = true;
                        _item.Hole[3].Use = true;

                        //if (0 == _item.Method)
                        //{
                        if (_item.dx.Min == null || _item.dx.Max == null)
                        {
                            _item.dx.Min = new double[2];
                            _item.dx.Max = new double[2];
                        }
                        double.TryParse(dt.Rows[i]["1Min"].ToString(), out _item.dx.Min[0]);
                        double.TryParse(dt.Rows[i]["2Min"].ToString(), out _item.dx.Min[1]);
                        double.TryParse(dt.Rows[i]["1Max"].ToString(), out _item.dx.Max[0]);
                        double.TryParse(dt.Rows[i]["2Max"].ToString(), out _item.dx.Max[1]);
                        //}
                        //else if (1 == _item.Method)
                        //{
                        //_item.dl.RGBSel = ComboBoxDLRGBSel.SelectedIndex;
                        Double.TryParse(dt.Rows[i]["标准曲线a0"].ToString(), out _item.dl.A0);
                        Double.TryParse(dt.Rows[i]["标准曲线b0"].ToString(), out _item.dl.A1);
                        Double.TryParse(dt.Rows[i]["标准曲线c0"].ToString(), out _item.dl.A2);
                        Double.TryParse(dt.Rows[i]["标准曲线d0"].ToString(), out _item.dl.A3);
                        Double.TryParse(dt.Rows[i]["校正曲线a"].ToString(), out _item.dl.B0);
                        Double.TryParse(dt.Rows[i]["校正曲线b"].ToString(), out _item.dl.B1);
                        Double.TryParse(dt.Rows[i]["上限值"].ToString(), out _item.dl.LimitL);
                        Double.TryParse(dt.Rows[i]["下限值"].ToString(), out _item.dl.LimitH);

                        //}

                        Global.gszItems.Add(_item);
                        Global.SerializeToFile(Global.gszItems, Global.gszItemsFile);

                    }
                }

                MessageBox.Show("导入检测项目成功！");
                btnAll.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                btnAll.IsEnabled = true;
            }

        }

       

      
        private void labelclose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void labelclose_MouseLeave(object sender, MouseEventArgs e)
        {
            labelclose.Foreground = new SolidColorBrush(Colors.Red);
        }

        private void labelclose_MouseEnter(object sender, MouseEventArgs e)
        {
            labelclose.Foreground = new SolidColorBrush(Colors.SeaGreen);
        }
        /// <summary>
        /// 备份
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnbackup_Click(object sender, RoutedEventArgs e)
        {
            btnbackup.IsEnabled = false;
            string path = AppDomain.CurrentDomain.BaseDirectory + "BackUpItems";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            List<DYFGDItemPara> fitem = new List<DYFGDItemPara>();
            Global.SerializeToFile(fitem, path + "\\" + "fgdItems.dat"); //清空
            Global.SerializeToFile(Global.fgdItems , path + "\\" + "fgdItems.dat");//写入备份

            List<DYJTJItemPara> jitem = new List<DYJTJItemPara>();
            Global.SerializeToFile(jitem, path + "\\" + "jtjItems.dat"); //清空
            Global.SerializeToFile(Global.jtjItems, path + "\\" + "jtjItems.dat");//写入备份

            List<DYGSZItemPara> gitem = new List<DYGSZItemPara>();
            Global.SerializeToFile(gitem, path + "\\" + "gszItems.dat"); //清空
            Global.SerializeToFile(Global.gszItems, path + "\\" + "gszItems.dat");//写入备份
            
            //File.Copy(Global.gszItemsFile, path + "\\" + "fgdItems.dat", true);
            //File.Copy(Global.jtjItemsFile, path + "\\" + "jtjItems.dat", true);
            //File.Copy(Global.fgdItemsFile, path + "\\" + "gszItems.dat", true);
            MessageBox.Show("检测项目备份成功","系统提示");
            btnbackup.IsEnabled = true ;
        }
        /// <summary>
        /// 还原
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReCover_Click(object sender, RoutedEventArgs e)
        {
            btnReCover.IsEnabled = false;
            string path = AppDomain.CurrentDomain.BaseDirectory + "BackUpItems";
            if (!Directory.Exists(path))
            {
                MessageBox.Show("暂无备份数据", "系统提示");
                btnReCover.IsEnabled = true;
                return;
            }

            string pf = path + "\\" + "fgdItems.dat";
            string jf = path + "\\" + "jtjItems.dat";
            string gf = path + "\\" + "gszItems.dat";

            if (!File .Exists(pf))
            {
                MessageBox.Show("找不到fgdItems.dat文件", "系统提示");
                btnReCover.IsEnabled = true;
                return;
            }
            if (!File.Exists(jf))
            {
                MessageBox.Show("找不到jtjItems.dat文件", "系统提示");
                btnReCover.IsEnabled = true;
                return;
            }
            if (!File.Exists(gf))
            {
                MessageBox.Show("找不到gszItems.dat文件", "系统提示");
                btnReCover.IsEnabled = true;
                return;
            }

            List<DYFGDItemPara> fitem = new List<DYFGDItemPara>();
            Global.SerializeToFile(fitem, Global.fgdItemsFile); //清空
            Global.SerializeToFile(Global.fgdItems, Global.fgdItemsFile);//写入备份

            List<DYJTJItemPara> jitem = new List<DYJTJItemPara>();
            Global.SerializeToFile(jitem, Global.jtjItemsFile); //清空
            Global.SerializeToFile(Global.jtjItems, Global.jtjItemsFile);//写入备份

            List<DYGSZItemPara> gitem = new List<DYGSZItemPara>();
            Global.SerializeToFile(gitem, Global.gszItemsFile); //清空
            Global.SerializeToFile(Global.gszItems, Global.gszItemsFile);//写入备份

            //File.Copy(path + "\\" + "fgdItems.dat", Global.fgdItemsFile, true);
            //File.Copy(path + "\\" + "jtjItems.dat", Global.jtjItemsFile, true);
            //File.Copy(path + "\\" + "gszItems.dat", Global.gszItemsFile, true);

            Global.DeSerializeFromFile(out Global.fgdItems, path + "\\" + "fgdItems.dat");
            Global.DeSerializeFromFile(out Global.jtjItems, path + "\\" + "jtjItems.dat");
            Global.DeSerializeFromFile(out Global.gszItems, path + "\\" + "gszItems.dat");

            Global.SerializeToFile(Global.fgdItems, Global.fgdItemsFile);//写入
            Global.SerializeToFile(Global.jtjItems, Global.jtjItemsFile);//写入
            Global.SerializeToFile(Global.gszItems, Global.gszItemsFile);//写入

            MessageBox.Show("检测项目还原成功", "系统提示");
            

            btnReCover.IsEnabled = true;
        }

    }
}
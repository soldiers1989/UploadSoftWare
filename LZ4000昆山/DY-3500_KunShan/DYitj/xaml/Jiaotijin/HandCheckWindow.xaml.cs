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
using DYSeriesDataSet;

namespace AIO.xaml.Jiaotijin
{
    /// <summary>
    /// HandCheckWindow.xaml 的交互逻辑
    /// </summary>
    public partial class HandCheckWindow : Window
    {
        public HandCheckWindow()
        {
            InitializeComponent();
        }

        public DYJTJItemPara _item = null;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_item == null)
            {
                MessageBox.Show("出现异常！");
                this.Close();
                return;
            }

            lb_ItenName.Content = _item.Name;//项目
            txt_CheckType.Text = _methodToString[_item.Method];//检测方法
            if (!Global.KsVersion.Equals("1"))
            {
                txt_Company.Text = _item.Hole[0].CompanyName;//经营户
                txt_Company.DataContext = _item.Hole[0].CompanyCode;
            }
            else
            {
                txt_Company.Text = "";
                txt_Company.DataContext = "";
            }
            txt_FoodCode.Text = string.Format("{0:D5}", _item.SampleNum);//样品编号
            txt_FoodName.Text = _item.Hole[0].SampleName;//样品名称
            txt_FoodName.DataContext = _item.Hole[0].SampleCode;

            txt_Result.Text = "";//检测结论
            txt_CheckInfo.Text = LoginWindow._userAccount.UserName;//检测人

            List<string> comboList = new List<string>();
            comboList.Add("请选择");
            comboList.Add("阴性");
            comboList.Add("阳性");
            comboList.Add("可疑");
            cb_CheckValue.ItemsSource = comboList;
            cb_CheckValue.SelectedIndex = 0;
        }

        private string[,] _CheckValue;
        private string[] _methodToString = { "定性消线法", "定性比色法", "定量法(T)", "定量法(T/C)", "定性比色法(T/C)" };
        private string errMsg = string.Empty;
        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            if (txt_Result.Text.Trim().Length == 0)
            {
                MessageBox.Show("请选择检测结果！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            _CheckValue = new string[1, 24];
            _CheckValue[0, 0] = String.Format("{0:D2}", 1);
            _CheckValue[0, 1] = "胶体金";
            _CheckValue[0, 2] = _item.Name;
            _CheckValue[0, 3] = _methodToString[_item.Method];
            _CheckValue[0, 4] = cb_CheckValue.SelectedValue.ToString();
            _CheckValue[0, 5] = _item.Unit;
            _CheckValue[0, 6] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            _CheckValue[0, 7] = LoginWindow._userAccount.UserName;
            _CheckValue[0, 8] = _item.Hole[0].SampleName;
            _CheckValue[0, 9] = txt_Result.Text;
            _CheckValue[0, 10] = "阴性";
            _CheckValue[0, 11] = txt_FoodCode.Text;
            _CheckValue[0, 12] = "参考国标";
            _CheckValue[0, 13] = string.Empty;
            _CheckValue[0, 14] = _item.Hole[0].CompanyName;
            _CheckValue[0, 15] = string.Empty;
            _CheckValue[0, 16] = string.Empty;

            //经营户
            //DataTable dtblB = opr.GetAsDataTable("Ks_Business", string.Format(" ID = '{0}'", _item.Hole[0].CompanyCode), out errMsg);
            //if (dtblB != null && dtblB.Rows.Count > 0)
            //{
            //    _CheckValue[0, 17] = dtblB.Rows[0]["IdentityCard"].ToString();
            //    _CheckValue[0, 18] = dtblB.Rows[0]["TWNum"].ToString();
            //    _CheckValue[0, 19] = dtblB.Rows[0]["TWNume"].ToString();
            //}
            DataTable dtblB = opr.GetAsDataTable("Ks_Business", string.Format(" ID = '{0}'", _item.Hole[0].CompanyCode), out errMsg);
            if (dtblB != null && dtblB.Rows.Count > 0)
            {
                _CheckValue[0, 17] = dtblB.Rows[0]["IdentityCard"].ToString();
                _CheckValue[0, 18] = dtblB.Rows[0]["TWNum"].ToString();
                _CheckValue[0, 19] = dtblB.Rows[0]["TWNume"].ToString();
                dtblB = opr.GetAsDataTable("ks_AreaMarket", string.Format(" LicenseNo = '{0}'", dtblB.Rows[0]["LicenseNo"].ToString()), out errMsg);
                if (dtblB != null && dtblB.Rows.Count > 0)
                {
                    _CheckValue[0, 14] = dtblB.Rows[0]["MarketName"].ToString() + "|" +
                        dtblB.Rows[0]["LicenseNo"].ToString() + "|" + GetMarketType(dtblB.Rows[0]["MarketRef"].ToString());
                }
            }
            else
            {
                dtblB = opr.GetAsDataTable("ks_AreaMarket", string.Format(" LicenseNo = '{0}'", _item.Hole[0].CompanyCode), out errMsg);
                if (dtblB != null && dtblB.Rows.Count > 0)
                {
                    _CheckValue[0, 14] = dtblB.Rows[0]["MarketName"].ToString() + "|" +
                        dtblB.Rows[0]["LicenseNo"].ToString() + "|" + GetMarketType(dtblB.Rows[0]["MarketRef"].ToString());
                }
            }
            //样品品类
            _CheckValue[0, 20] = _item.Hole[0].SampleCode;
            _CheckValue[0, 21] = _item.Hole[0].SampleName;

            //项目
            DataTable dtblC = opr.GetAsDataTable("Ks_CheckItem", string.Format(" ItemName = '{0}'", _item.Name), out errMsg);
            if (dtblC != null && dtblC.Rows.Count > 0)
            {
                _CheckValue[0, 22] = dtblC.Rows[0]["ParentCode"].ToString();
                _CheckValue[0, 23] = dtblC.Rows[0]["ItemCode"].ToString();
            }

            if (TestResultConserve.ResultConserve(_CheckValue) > 0)
            {
                MessageBox.Show("手工录入成功！\r\n\r\nTips：稍后可以在[检测记录查询管理]中进行上传操作！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                UpdateSampleNum();
                this.Close();
            }
        }

        private string GetMarketType(string name)
        {
            switch (name)
            {
                case "批发市场":
                    return "1";

                case "农贸市场":
                    return "2";

                case "检测机构":
                    return "3";

                case "餐饮单位":
                    return "4";

                case "食品生产企业":
                    return "5";

                case "商场超市":
                    return "6";

                case "个体工商户":
                    return "7";

                case "食材配送企业":
                    return "8";

                case "单位食堂":
                    return "9";

                case "集体用餐配送和中央厨房":
                    return "10";

                case "农产品基地":
                    return "11";

                default:
                    return "0";
            }
        }

        /// <summary>
        /// 更新样品编号
        /// </summary>
        private void UpdateSampleNum()
        {
            _item.SampleNum++;
            Global.SerializeToFile(Global.jtjItems, Global.jtjItemsFile);
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cb_CheckValue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cb_CheckValue != null)
            {
                if (cb_CheckValue.SelectedValue == null)
                    return;

                string val = cb_CheckValue.SelectedValue.ToString();
                switch (val)
                {
                    case "阴性":
                        txt_Result.Text = "合格";
                        break;

                    case "阳性":
                        txt_Result.Text = "不合格";
                        break;

                    case "可疑":
                        txt_Result.Text = "可疑";
                        break;

                    default:
                        txt_Result.Text = "";
                        break;
                }
            }
        }

    }
}
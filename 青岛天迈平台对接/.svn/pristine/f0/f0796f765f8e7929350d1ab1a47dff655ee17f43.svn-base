using AIO.src;
using DYSeriesDataSet;
using System;
using System.Data;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// AddSampleidWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddSampleidWindow : Window
    {
        public AddSampleidWindow()
        {
            InitializeComponent();
        }
        public String sampleid = string.Empty;
        private String _sampleDate = string.Empty, _prodate = string.Empty;
        private getsample.Response _model = new getsample.Response();
        public String btnSaveValue = "保存";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.tb_sampleid.Text = sampleid;
            this.btn_Save.Content = btnSaveValue;
            if (btnSaveValue.Equals("修改"))
                this.Title = "修改 - 快检单号";
        }

        /// <summary>
        /// 修改时初始化界面
        /// </summary>
        public void SetValue(uploadSample.Request model)
        {
            tb_sampleid.IsEnabled = false;
            sampleid = model.detailsList[0].sampleid;
            tb_productName.Text = model.detailsList[0].foodName;
            tb_sampleDept.Text = model.detailsList[0].sampCompany;
            tb_barcode.Text = model.detailsList[0].barcode;
            tb_sampPerson.Text = model.detailsList[0].sampPerson;
            dp_sampleDate.Text = model.detailsList[0].sampDate;
            tb_bsampCompany.Text = model.detailsList[0].bsampCompany;
            tb_bscompAddr.Text = model.detailsList[0].bscompAddr;
            tb_bscompCont.Text = model.detailsList[0].bscompCont;
            tb_bscompPhon.Text = model.detailsList[0].bscompPhon;
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CheckData())
                {
                    _model.bscompAddr = tb_bscompAddr.Text.Trim();
                    _model.scompAddr = tb_scompAddr.Text.Trim();
                    _model.bscompPhon = tb_bscompPhon.Text.Trim();
                    _model.scompCont = tb_scompCont.Text.Trim();
                    _model.scompPhon = tb_scompPhon.Text.Trim();
                    _model.procompPhon = tb_procompPhon.Text.Trim();
                    Wisdom.GETSAMPLE_RESPONSE = _model;
                    if (btn_Save.Content.Equals("保存"))
                    {
                        Insert();
                    }
                    else if (btn_Save.Content.Equals("修改"))
                    {
                        Update();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 修改快检单号
        /// </summary>
        private void Update()
        {
            try
            {
                int rtn = WisdomCls.Update();
                if (rtn == 1)
                {
                    if (MessageBox.Show("更新成功!是否返回上一级?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 新增一条快检单号
        /// </summary>
        private void Insert()
        {
            try
            {
                int rtn = WisdomCls.Insert();
                if (rtn == 1)
                {
                    if (MessageBox.Show("新增快检单号成功!是否返回上一级?", "操作提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 检测数据完整性|唯一性
        /// </summary>
        /// <returns></returns>
        private Boolean CheckData()
        {
            String str = string.Empty;
            try
            {
                if (tb_sampleid.Text.Trim().Length == 0)
                {
                    MessageBox.Show("快检单号不能为空!", "操作提示");
                    tb_sampleid.Focus();
                    return false;
                }
                else
                {
                    int type = 2;
                    str = tb_sampleid.Text.Trim();
                    if (btn_Save.Content.Equals("保存"))
                    {
                        String whereSql = str.Length > 0 ? "SAMPLENUM = '" + str + "'" : string.Empty, orderBy = string.Empty;
                        if (Search(whereSql, orderBy, type))
                        {
                            MessageBox.Show("快检单号 " + str + " 已存在 - [本地数据]!!", "操作提示");
                            return false;
                        }
                        else if (SearchCloud())
                        {
                            MessageBox.Show("快检单号 " + str + " 已存在 - [云监管平台]!!", "操作提示");
                            return false;
                        }
                    }
                    _model.sampleid = str;
                }

                str = tb_productName.Text.Trim();
                if (str.Length == 0)
                {
                    MessageBox.Show("产品名称不能为空!", "操作提示");
                    tb_productName.Focus();
                    return false;
                }
                else
                {
                    _model.productName = str;
                }

                str = tb_sampPerson.Text.Trim();
                if (str.Length == 0)
                {
                    MessageBox.Show("抽样人不能为空!", "操作提示");
                    tb_sampPerson.Focus();
                    return false;
                }
                else
                {
                    _model.sampPerson = str;
                }

                str = tb_bsampCompany.Text.Trim();
                if (str.Length == 0)
                {
                    MessageBox.Show("被抽样单位不能为空!", "操作提示");
                    tb_bsampCompany.Focus();
                    return false;
                }
                else
                {
                    _model.bsampCompany = str;
                }

                str = _sampleDate;
                if (str.Length == 0)
                {
                    MessageBox.Show("抽样日期不能为空!", "操作提示");
                    return false;
                }
                else
                {
                    _model.sampleDate = str;
                }

                str = tb_sampleDept.Text.Trim();
                if (str.Length == 0)
                {
                    MessageBox.Show("抽样单位不能为空!", "操作提示");
                    tb_sampleDept.Focus();
                    return false;
                }
                else
                {
                    _model.sampleDept = str;
                }

                str = tb_bscompCont.Text.Trim();
                if (str.Length == 0)
                {
                    MessageBox.Show("被抽样单位负责人不能为空!", "操作提示");
                    tb_bscompCont.Focus();
                    return false;
                }
                else
                {
                    _model.bscompCont = str;
                }

                str = tb_bscompAddr.Text.Trim();
                if (str.Length == 0)
                {
                    MessageBox.Show("被抽样地址不能为空!", "操作提示");
                    tb_bscompAddr.Focus();
                    return false;
                }
                else
                {
                    _model.bscompAddr = str;
                }

                //str = dp_prodate.Text.Trim();
                //if (str.Length == 0)
                //{
                //    MessageBox.Show("生产日期不能为空!", "操作提示");
                //    return false;
                //}
                //else
                //{
                //    _model.prodate = str;
                //}

                //str = tb_shelfLife.Text.Trim();
                //if (str.Length == 0)
                //{
                //    MessageBox.Show("保质期不能为空!", "操作提示");
                //    tb_shelfLife.Focus();
                //    return false;
                //}
                //else
                //{
                //    _model.shelfLife = str;
                //}

                //str = tb_proCompany.Text.Trim();
                //if (str.Length == 0)
                //{
                //    MessageBox.Show("生产者名称不能为空!", "操作提示");
                //    tb_proCompany.Focus();
                //    return false;
                //}
                //else
                //{
                //    _model.proCompany = str;
                //}

                str = tb_barcode.Text.Trim();
                if (str.Length == 0)
                {
                    //MessageBox.Show("条形码不能为空!", "操作提示");
                    //tb_barcode.Focus();
                    //return false;
                }
                else
                {
                    _model.barCode = str;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        /// <summary>
        /// 根据快检单号检索云平台是否有数据 有数据返回true，没有则返回false
        /// </summary>
        private Boolean SearchCloud()
        {
            try
            {
                Wisdom.GETSAMPLE_REQUEST = new DYSeriesDataSet.getsample.Request()
                {
                    deviceid = Wisdom.DeviceID,
                    sampleid = tb_sampleid.Text.Trim()
                };
                String json = Wisdom.HttpPostRequest(Wisdom.GETSAMPLE);
                JavaScriptSerializer js = new JavaScriptSerializer();
                getsample.Response model = js.Deserialize<getsample.Response>(json);
                if (model.result.Equals("0"))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }

        /// <summary>
        /// 查询本地数据 本地有数据返回true，没有则返回false
        /// </summary>
        private Boolean Search(String whereSql, String orderBySql, int type)
        {
            try
            {
                type = (whereSql.Length == 0 && orderBySql.Length == 0) ? 1 : 2;
                DataTable dataTable = WisdomCls.GetAsDataTable(whereSql, orderBySql, type);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }

        private void dp_sampleDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dp_sampleDate.SelectedDate != null)
            {
                DateTime time = (DateTime)dp_sampleDate.SelectedDate;
                _sampleDate = time.ToString("yyyy-MM-dd");
            }
        }

        private void dp_prodate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dp_prodate.SelectedDate != null)
            {
                DateTime time = (DateTime)dp_prodate.SelectedDate;
                _prodate = time.ToString("yyyy-MM-dd");
            }
        }

    }
}

using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using AIO.AnHui;
using com.lvrenyang;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// SearchFoodTypeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SearchFoodTypeWindow : Window
    {
        public SearchFoodTypeWindow()
        {
            InitializeComponent();
        }
        private List<FoodType> nodes = new List<FoodType>();
        private List<FoodType> outputList = null;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable dtbl = DataOperation.GetSampleTypeByName("");
            if (dtbl != null && dtbl.Rows.Count > 0)
            {
                nodes = (List<FoodType>)IListDataSet.DataTableToIList<FoodType>(dtbl);
                outputList = Bind(nodes);
                this.treeView.ItemsSource = outputList;
            }
        }

        /// <summary>
        /// 绑定数
        /// </summary>
        /// <param name="fdt"></param>
        /// <returns></returns>
        private List<FoodType> Bind(List<FoodType> fdt)
        {
            List<FoodType> outputList = new List<FoodType>();
            for (int i = 0; i < fdt.Count; i++)
            {
                try
                {
                    if (fdt[i].pid.Equals("-1") && fdt[i].typeNum.Equals("SPFL"))
                        outputList.Add(fdt[i]);
                    else
                    {
                        FoodType fds = FindDownward(fdt, fdt[i]);
                        if (fds.FoodTypes == null) fds.FoodTypes = new List<FoodType>();
                        fds.FoodTypes.Add(fdt[i]);
                        //FindDownward(fdt, fdt[i]).FoodTypes.Add(fdt[i]);
                    }
                }
                catch (System.Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            }
            return outputList;
        }

        /// <summary>
        /// 向下查询子集
        /// </summary>
        /// <param name="fdts"></param>
        /// <param name="ParentName"></param>
        /// <returns></returns>
        private FoodType FindDownward(List<FoodType> fdts, FoodType fd)
        {
            if (fdts == null) return null;
            for (int i = 0; i < fdts.Count; i++)
            {
                if (fdts[i].ID.Equals(fd.pid))
                    return fdts[i];
                FoodType node = FindDownward(fdts[i].FoodTypes, fd);
                if (node != null) return node;
            }
            return null;
        }

        private FoodType selectModel = null;
        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeView item = sender as TreeView;
            if (item != null)
            {
                selectModel = item.SelectedItem as FoodType;
            }
        }

        private void Btn_Selected_Click(object sender, RoutedEventArgs e)
        {
            if (selectModel != null)
            {
                Global.projectName = selectModel.name;
                Global.projectUnit = selectModel.codeId;
                this.Close();
            }
        }

        private void Btn_Closed_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void treeView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TreeView item = sender as TreeView;
            if (item != null)
            {
                selectModel = item.SelectedItem as FoodType;
                if (selectModel != null)
                {
                    Global.projectName = selectModel.name;
                    Global.projectUnit = selectModel.codeId;
                    this.Close();
                }
            }
        }
    }

    public class FoodType
    {
        private string _ID = string.Empty;

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _typeNum = string.Empty;

        public string typeNum
        {
            get { return _typeNum; }
            set { _typeNum = value; }
        }

        private string _name = string.Empty;
        /// <summary>
        /// 种类名称
        /// </summary>
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _codeId = string.Empty;
        /// <summary>
        /// 种类编号
        /// </summary>
        public string codeId
        {
            get { return _codeId; }
            set { _codeId = value; }
        }
        private string _pid = string.Empty;
        /// <summary>
        /// 父类ID
        /// </summary>
        public string pid
        {
            get { return _pid; }
            set { _pid = value; }
        }
        private string _remark = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        public string remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        public List<FoodType> FoodTypes { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using AIO.src;
using com.lvrenyang;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// FoodCategoriesWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FoodCategoriesWindow : Window
    {
        public FoodCategoriesWindow()
        {
            InitializeComponent();
        }

        private List<Node> nodes = null;
        private List<Node> outputList2016 = null;
        private List<Node> outputList2017 = null;
        private string errMsg = string.Empty;


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 1;
            //2017
            nodes = new List<Node>();
            DataTable dtbl = SqlHelper.GetDataTable("tFoodSpecies", "Years='2017'", out errMsg);
            if (dtbl != null && dtbl.Rows.Count > 0)
            {
                nodes = (List<Node>)IListDataSet.DataTableToIList<Node>(dtbl);
                outputList2016 = Bind(nodes);
                for (int i = 0; i < outputList2016.Count; i++)
                {
                    outputList2016[i].Name = string.Format("{0} {1}", i + 1, outputList2016[i].Name);
                }
                this.treeView2016.ItemsSource = outputList2016;
            }

            //2018
            nodes = new List<Node>();
            dtbl = SqlHelper.GetDataTable("tFoodSpecies", "Years='2018'", out errMsg);
            if (dtbl != null && dtbl.Rows.Count > 0)
            {
                nodes = (List<Node>)IListDataSet.DataTableToIList<Node>(dtbl);
                outputList2017 = Bind(nodes);
                for (int i = 0; i < outputList2017.Count; i++)
                {
                    outputList2017[i].Name = string.Format("{0} {1}", i + 1, outputList2017[i].Name);
                }
                this.treeView2017.ItemsSource = outputList2017;
            }
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 绑定数
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        private List<Node> Bind(List<Node> nodes)
        {
            List<Node> outputList = new List<Node>();
            try
            {
                for (int i = 0; i < nodes.Count; i++)
                {
                    if (nodes[i].Level == 1)
                        outputList.Add(nodes[i]);
                    else
                        FindDownward(nodes, nodes[i]).Nodes.Add(nodes[i]);
                }
            }
            catch (System.Exception ex)
            {

            }
            return outputList;
        }

        /// <summary>
        /// 向下查询子集
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="ParentName"></param>
        /// <returns></returns>
        private Node FindDownward(List<Node> nodes, Node nd)
        {
            if (nodes == null) return null;
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].Name.Trim().Equals(nd.ParentName.Trim()) && nodes[i].Level + 1 == nd.Level)
                    return nodes[i];
                Node node = FindDownward(nodes[i].Nodes, nd);
                if (node != null) return node;
            }
            return null;
        }

        private void treeView2017_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeView item = sender as TreeView;
            if (item != null)
            {
                Node node = item.SelectedItem as Node;
                if (node != null)
                {
                    TxtCheckItem.Text = string.Format("食品种类名称：{0}\r\n食品风险等级：{1}\r\n更新时间：{2}\r\n备注：{3}\r\n抽检项目：{4}",
                        node.Name, node.RiskLevel, node.Years, node.Notes, node.CheckItems);
                }
            }
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabControl.SelectedIndex == 0)
            {
                lbTitle.Content = "2017年食品安全抽检计划";
            }
            else if (tabControl.SelectedIndex == 1)
            {
                lbTitle.Content = "2018年食品安全抽检计划";
            }
            else
            {
                lbTitle.Content = "2018年食品安全抽检计划";
            }
        }

        private void Btn_Update_Click(object sender, RoutedEventArgs e)
        {
            Btn_Update.IsEnabled = false;
            TimerWindow window = new TimerWindow();
            window.lb_NTTimer.Content = "正在获取更新数据，请稍等···";
            window.ShowInTaskbar = false;
            window.ShowDialog();
            MessageBox.Show("已更新为最新数据！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            Btn_Update.IsEnabled = true;
        }

        private void treeView2016_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeView item = sender as TreeView;
            if (item != null)
            {
                Node node = item.SelectedItem as Node;
                if (node != null)
                {
                    TxtCheckItem_2016.Text = string.Format("食品种类名称：{0}\r\n食品风险等级：{1}\r\n更新时间：{2}\r\n备注：{3}\r\n抽检项目：{4}",
                        node.Name, node.RiskLevel, node.Years, node.Notes, node.CheckItems);
                }
            }
        }

        private void TxtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string val = TxtName.Text.Trim();
            if (val.Length == 0)
            {
                this.treeView2016.ItemsSource = outputList2016;
                this.treeView2017.ItemsSource = outputList2017;
                return;
            }

            if (tabControl.SelectedIndex == 0)//2017
            {
                if (outputList2016 != null && outputList2016.Count > 0)
                {

                }
            }
            else if (tabControl.SelectedIndex == 1)//2018
            {
                if (outputList2017 != null && outputList2017.Count > 0)
                {

                }
            }
            else if (tabControl.SelectedIndex == 2)//2018
            {

            }
        }

    }

    public class Node
    {
        public Node()
        {
            this.Nodes = new List<Node>();
            this.ParentName = "";
        }
        public int ID { get; set; }
        /// <summary>
        /// 食品种类名称
        /// </summary>
        public string Name { get; set; }
        private string _ParentName = string.Empty;

        /// <summary>
        /// 父类名称
        /// </summary>
        public string ParentName
        {
            get { return _ParentName; }
            set { _ParentName = value; }
        }
        private int _Level = 0;
        /// <summary>
        /// 分级
        /// </summary>
        public int Level
        {
            get { return _Level; }
            set { _Level = value; }
        }

        private string _RiskLevel = string.Empty;

        /// <summary>
        /// 风险等级
        /// </summary>
        public string RiskLevel
        {
            get { return _RiskLevel; }
            set { _RiskLevel = value; }
        }
        private string _CheckItems = string.Empty;
        /// <summary>
        /// 检测项目
        /// </summary>
        public string CheckItems
        {
            get { return _CheckItems; }
            set { _CheckItems = value; }
        }
        private string _Notes = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        public string Notes
        {
            get { return _Notes; }
            set { _Notes = value; }
        }
        /// <summary>
        /// 所属年
        /// </summary>
        public string Years { get; set; }
        public List<Node> Nodes { get; set; }
    }

}
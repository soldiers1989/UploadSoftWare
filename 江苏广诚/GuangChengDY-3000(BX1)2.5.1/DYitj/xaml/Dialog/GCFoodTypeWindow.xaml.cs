using AIO.src;
using com.lvrenyang;
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

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// GCFoodTypeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GCFoodTypeWindow : Window
    {
        private List<Node> nodes = null;
        private List<Node> outputList2016 = null;
        private List<Node> outputList2017 = null;
        private string errMsg = "";
        public GCFoodTypeWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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
                this.treeView.ItemsSource = outputList2016;
            }
        }
        /// <summary>
        /// 绑定树
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
       
       

        private void treeView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TreeView item = sender as TreeView;
            if (item != null)
            {
                Node node = item.SelectedItem as Node;
                if (node != null)
                {
                    Global.projectName = node.Name.ToString ();
                    //Global.projectUnit = selectModel.codeId;
                    this.Close();
                }
            }
           
        }

        private void Btn_Selected_Click(object sender, RoutedEventArgs e)
        {
          
              if(Global.projectName !="" )
              {
                  this.Close();
              }   
              else
              {
                  MessageBox.Show("请选择需要的种类再单击选择！","提示",MessageBoxButton.OK ,MessageBoxImage.Warning );
              }
             
            
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Global.projectName = "";
            this.Close();
        }

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeView item = sender as TreeView;
            if (item != null)
            {
                Node node = item.SelectedItem as Node;
                if (node != null)
                {
                    Global.projectName = node.Name.ToString();
                    //Global.projectUnit = selectModel.codeId;
               
                }
            }
        }

       
    }
}

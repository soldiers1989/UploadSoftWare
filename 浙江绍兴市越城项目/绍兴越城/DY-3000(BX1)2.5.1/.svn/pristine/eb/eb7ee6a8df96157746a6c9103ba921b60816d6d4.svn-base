using System;
using System.Collections.Generic;
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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls.Primitives;
using DYSeriesDataSet;

namespace AIO.xaml.Dialog
{
    /// <summary>
    /// Add.xaml 的交互逻辑
    /// </summary>
    public partial class Add : Window
    {
        public ObservableCollection<Student> StuList { get; set; }
        clsttStandardDecideOpr bll = new clsttStandardDecideOpr();

        public Add()
        {
            InitializeComponent();

            StuList = new ObservableCollection<Student>();

            this.DataContext = this;

            grd.Focus();
        }

        private void DataGrid_InitializingNewItem(object sender, InitializingNewItemEventArgs e)
        {
            if (StuList == null || StuList.Count == 0)
            {
                ((Student)e.NewItem).Id = 1;
            }
            else
            {
                ((Student)e.NewItem).Id = StuList.Max(p => p.Id) + 1;
            }
            ((Student)e.NewItem).Selected = true;
        }

        private void grd_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                grd.SelectedIndex++;
                grd.BeginEdit();
            }
        }

        private void grd_Loaded(object sender, RoutedEventArgs e)
        {
            DataGridCell cell = GetCell(0, 2);
            if (cell != null)
            {
                grd.SelectedIndex = 0;
                cell.Focus();
                grd.BeginEdit();
            }
        }

        /// <summary>
        /// 根据行、列索引取的对应单元格对象
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public DataGridCell GetCell(int row, int column)
        {
            DataGridRow rowContainer = GetRow(row);

            if (rowContainer != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(rowContainer);

                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                if (cell == null)
                {
                    grd.ScrollIntoView(rowContainer, grd.Columns[column]);
                    cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                }
                return cell;
            }
            return null;
        }

        /// <summary>
        /// 根据行索引取的行对象
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public DataGridRow GetRow(int index)
        {
            DataGridRow row = (DataGridRow)grd.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                grd.ScrollIntoView(grd.Items[index]);
                row = (DataGridRow)grd.ItemContainerGenerator.ContainerFromIndex(index);
            }
            return row;
        }

        /// <summary>
        /// 获取指定类型的子元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

        private void grd_InitializingNewItem(object sender, InitializingNewItemEventArgs e)
        {
            if (StuList == null || StuList.Count == 0)
            {
                ((Student)e.NewItem).Id = 1;
            }
            else
            {
                ((Student)e.NewItem).Id = StuList.Max(p => p.Id) + 1;
            }
            ((Student)e.NewItem).Selected = true;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (grd.SelectedItems.Count > 0)
            {
                
            }
            clsttStandardDecide model = new clsttStandardDecide();
            string err = string.Empty;
            //model.FtypeNmae = FtypeNmae.Text;
            //model.SampleNum = SampleNum.Text;
            //model.Name = Name.Text;
            //model.ItemDes = ItemDes.Text;
            //model.StandardValue = StandardValue.Text;
            //model.Demarcate = Demarcate.Text;
            //model.Unit = Unit.Text;
            err = string.Empty;
            //bll.Insert(model, out err);
        }

    }

    public class Student : INotifyPropertyChanged
    {
        public bool Selected { get; set; }
        public int Id { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DY.FoodClientLib;

namespace FoodClient
{
    /// <summary>
    /// 设置非样品孔状态
    /// </summary>
    public partial class FrmHolesSet : Form
    {
        private string path = string.Empty;
        private string machineCode = string.Empty;
        private Image holesImg = null;
        private Image NonHolesImg = null;//代表非孔位
      
        private readonly HolesSetingOpr holesBll = new HolesSetingOpr();
        private System.Collections.Hashtable htbl = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmHolesSet(string code)
        {
            Cursor.Current = Cursors.WaitCursor;
            InitializeComponent();
            machineCode = code;

            //两种状态图形
            path = AppDomain.CurrentDomain.BaseDirectory + "Img//pic1.jpg";
            holesImg = Bitmap.FromFile(path);

            path = AppDomain.CurrentDomain.BaseDirectory + "Img//pic2.jpg";
            NonHolesImg = Bitmap.FromFile(path);
        }

        /// <summary>
        /// 窗体加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmHolesSet_Load(object sender, EventArgs e)
        {
            bindCheckItem();

            createPicBoxList();
            Cursor.Current = Cursors.Default;

            //char temp = 'A';
            //int y = Convert.ToInt32(temp);
            //MessageBox.Show(y.ToString());
        }

        private string[,] strCheckItems;
       // private string strCheckItemCode = string.Empty;

        /// <summary>
        /// 绑定检测项目
        /// </summary>
        private void bindCheckItem()
        {
            //操作数据层类
            // clsCheckItemOpr itemBll = new clsCheckItemOpr();

            string strLinkStdCode = clsMachineOpr.GetNameFromCode("LinkStdCode", machineCode);

            strCheckItems = StringUtil.GetAry(strLinkStdCode);
            int len = strCheckItems.GetLength(0);
            if (strCheckItems.GetLength(0) <= 0)
            {
                return;
            }
            DataTable dtbl = new DataTable();
            DataColumn dataCol;

            ///////////////////新增
            dataCol = new DataColumn();
            dataCol.DataType = typeof(string);//检测项目代码
            dataCol.ColumnName = "SysCode";
            dtbl.Columns.Add(dataCol);

            dataCol = new DataColumn();
            dataCol.DataType = typeof(string);//仪器名称
            dataCol.ColumnName = "ItemDes";
            dtbl.Columns.Add(dataCol);

            string strWhere = string.Empty;

            DataRow dar = null;
            if (len >= 1)
            {
                for (int i = 0; i < len; i++)
                {
                    dar = dtbl.NewRow();
                    dar["SysCode"] = strCheckItems[i, 1].ToString();
                    dar["ItemDes"] = strCheckItems[i, 0].ToString();
                    dtbl.Rows.Add(dar);
                    //if (strCheckItems[i, 1].ToString() != "-1")
                    //{
                    // strSql += "'" + strCheckItems[i, 1].ToString() + "',";
                    // }
                }

                //strSql = strSql.Substring(0, strSql.Length - 1);
                //strWhere = string.Format("IsLock=false And SysCode In({0})", strSql);
            }
            //else
            //{
                //strWhere = string.Format("IsLock=false And SysCode ='{0}'", strCheckItems[0, 1].ToString());
            //}


            //获取DataTable数据集
            //clsCheckItemOpr itemBll = new clsCheckItemOpr();
            //DataTable dtbl = itemBll.GetAsDataTable(strWhere, "SysCode", 1);

            ///插入一个默认选项
            dar = dtbl.NewRow();
            dar["SysCode"] = "0";
            dar["ItemDes"] = "请选择检测项目";
            dtbl.Rows.InsertAt(dar, 0);//指定起始位置插入

            cmbCheckItem.DataSource = dtbl;
            cmbCheckItem.DisplayMember = "ItemDes";
            cmbCheckItem.ValueMember = "SysCode";
        }

        /// <summary>
        /// 图片创建控件,并初始化一共是96个
        /// </summary>
        private void createPicBoxList()
        {
            PictureBox pb = null;
            for (int j = 0; j < 8; j++)
            {
                for (int i = 0; i < 12; i++)
                {
                    pb = new PictureBox();
                    pb.BackColor = Color.White;

                    //通过这个标识记录不同的编号y在前面.记X从1开始索引,Y直接保存A...H这种
                    //已经转化为A1,A2,....H12这种格式,其中A的ASCII=65
                    pb.Tag = (Convert.ToChar(j + 65)).ToString() + (i + 1).ToString();
                    pb.Name = "pb";//通过这个Name标记所有PictureBox控件. 
                    pb.Size = new Size(56, 56);
                    pb.Location = new Point(i * 56, j * 56);
                    pb.Image = holesImg;
                    pb.Click += new System.EventHandler(this.pictureBox_Click);//添加Click事件
                    this.groupBox.Controls.Add(pb);//加载到groupBox容器内
                }
            }
        }

        /// <summary>
        /// 取出数据列表
        /// </summary>
        /// <param name="itemId"></param>
        private void getLastResult(string itemId)
        {
            DataTable dtbl = holesBll.GetDataTable(0, string.Format("checkItemId='{0}' AND MachineCode='{1}'", itemId, machineCode), "HolesIndex,IsShowOnData,IsDouble");

            if (dtbl != null && dtbl.Rows.Count > 0)
            {
                htbl = new System.Collections.Hashtable();
                string holes = string.Empty;
                string temp = string.Empty;
                int i = 0;

                //int y = 0;
                //只要有一行是double就行了。
                bool isDoubleChecked = Convert.ToBoolean(dtbl.Rows[i]["IsDouble"]);
                if (isDoubleChecked)
                {
                    rbDouble.Checked = true;
                }
                else
                {
                    rbSingle.Checked = true;
                }

                chbIsShowOnData.Checked = Convert.ToBoolean(dtbl.Rows[i]["IsShowOnData"]);

                //把查询孔位装到Hashtable中,防止两重循环
                for (i = 0; i < dtbl.Rows.Count; i++)
                {
                    holes = dtbl.Rows[i]["holesIndex"].ToString();

                    // temp = holes.Substring(0, 1);//取出A1中的A
                    // y = Convert.ToInt32(Convert.ToChar(temp));
                    //temp = (y - 65).ToString() + holes.Substring(1, holes.Length - 1);

                    htbl.Add(holes, "1");
                }

                foreach (PictureBox pb in this.groupBox.Controls.Find("pb", true))
                {
                    temp = pb.Tag.ToString();
                    if (htbl[temp] != null && htbl[temp].ToString() == "1")
                    {
                        pb.Image = NonHolesImg;
                    }
                }
            }
            else
            {
                foreach (PictureBox pb in this.groupBox.Controls.Find("pb", true))
                {
                    pb.Image = holesImg;
                }
            }
        }


        /// <summary>
        /// 点击图片按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       private void pictureBox_Click(object sender, EventArgs e)
       {
           PictureBox pic = sender as PictureBox;//定义鼠标当前点击picureBox的行为
           if (pic == null)
           {
               return;
           }
           if (rbDouble.Checked)//如果双孔检测...有些麻烦
           {
               string tag = pic.Tag.ToString();
               int len = 1;
               int x = 0;
               int y = 0;//因为前面的Y已经修改为如:A,B,....H这种
               if (tag.Length > 2)//对于两位数以上的,11,12两孔
               {
                   len = 2;
               }
               // y = Convert.ToInt32(tag.Substring(0, 1));//A....H

               x = Convert.ToInt32(tag.Substring(1, len));
               y = x+1;
               if ((x - 1) % 2 != 0)//控制点击第一孔时，邻近第二孔跟第一孔同时变化
               {
                   y = x - 1;
                   //return;
               }

               // string tag2 = y.ToString()+ (x + 1).ToString();//注意：Y在前面的
               string tag2 = tag.Substring(0, 1) + (y).ToString();//x+1

               foreach (PictureBox pb in groupBox.Controls.Find("pb", true))
               {
                   if (pb.Tag.ToString() == tag2)//隔避右边那个孔
                   {
                       if (pb.Image.Size == holesImg.Size)//如果原来是没有设置的
                       {
                           pb.Image = NonHolesImg;
                       }
                       else //如果是已经设置的，再点击就反过来
                       {
                           pb.Image = holesImg;
                       }
                       break;
                   }
               }
           }
           //不管是检测单孔或者双孔 鼠标当前点击的孔都起作用
           if (pic.Image.Size == holesImg.Size)//通过判断两个图片的不同大小，来判断当前图片状态
           {
               pic.Image = NonHolesImg;
           }
           else
           {
               pic.Image = holesImg;
           }

           this.groupBox.Refresh();
           //在原图片上画线
           //Bitmap img1 = new Bitmap(img, 55, 55); 
           //Graphics gs = Graphics.FromImage(img1);
           //Pen pen = new Pen(Color.Red, 2F);
           //gs.DrawLine(pen, new Point(0, 0), new Point(55, 55));
           //img1.Save(AppDomain.CurrentDomain.BaseDirectory + "Img//pic4.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
           //gs.Dispose();
           //MessageBox.Show("操作成功");
       }

        //private void drawEllipse(float x, float y)
        //{
        //    Graphics gs = pictureBox1.CreateGraphics();
        //    Brush brush = new SolidBrush(Color.Red);
        //    //Pen pen = new Pen(Color.Red);
        //   // gs.FillEllipse(brush, x, y, 30, 30);
        //    gs.DrawEllipse(new Pen(Color.Black, 1), x, y, 30, 30);
        //    this.pictureBox1.Refresh();
        //}

        /// <summary>
        /// 保存按钮.
        /// !!!注意检测项目修改为保存仪器名称，不是编号。因为数据从仪器传输过来没有项目编号,只能通过名称比较
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       private void btnOK_Click(object sender, EventArgs e)
       {
           if (cmbCheckItem.SelectedValue == null || cmbCheckItem.SelectedValue.ToString() == "0")
           {
               MessageBox.Show("检测项目必选");
               return;
           }
           this.Cursor = Cursors.WaitCursor;
           bool flag = false;
           string temp = string.Empty;
           HolesSetting model = new HolesSetting();
           model.CheckItemId = cmbCheckItem.Text; //cmbCheckItem.SelectedValue.ToString();//直接保存仪器名称
           model.IsShowOnData = chbIsShowOnData.Checked;//显示非样品孔位数据，选上则表示要显示
           model.IsDouble = rbDouble.Checked;
           model.MachineCode = machineCode;

           string strWhere = string.Format("CheckItemId='{0}' AND MachineCode='{1}'", model.CheckItemId, machineCode);
           try
           {
               if (holesBll.IsExist(strWhere))//判断是否存在
               {
                   holesBll.Delete(strWhere);//删除要比更新要快,更新的话每次都要对库内状态和界面图片状态进行判断.所以直接删除了再添加
               }

               foreach (PictureBox pb in this.groupBox.Controls.Find("pb", true))
               {
                   if (pb.Image.Size == NonHolesImg.Size)//如果是第二种状态
                   {
                       temp = pb.Tag.ToString();
                       model.HolesIndex = temp;//直接保存Tag标签
                       flag = holesBll.Insert(model);
                   }
               }
               if (flag)
               {
                   MessageBox.Show("操作成功");
                   this.Cursor = Cursors.Default;
               }
           }
           catch (Exception ex)
           {
               this.Cursor = Cursors.Default;
               MessageBox.Show(ex.Message);
           }
           //finally
           //{
           //    winClose();//最后关闭当前窗口
           //}
       }

        /// <summary>
        /// 关闭窗口时处理
        /// </summary>
        private void winClose()
        {
            if (htbl != null)
            {
                htbl.Clear();
            }
            this.holesImg.Dispose();
            this.NonHolesImg.Dispose();
            this.Dispose();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            winClose();
            //base.OnClosing(e);
        }
        /// <summary>
        ///取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            winClose();
        }


        private void cmbCheckItem_SelectedValueChanged(object sender, EventArgs e)
        {
            ClearALL();
            string obj = cmbCheckItem.Text;
            if (!string.IsNullOrEmpty(obj) || obj != "请选择检测项目")
            {
                getLastResult(obj);
                //this.Refresh();
            }
        }

        /// <summary>
        /// 清空全部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearAll_Click(object sender, EventArgs e)
        {
            ClearALL();
        }

        private void ClearALL()
        {
            foreach (PictureBox pb in this.groupBox.Controls.Find("pb", true))
            {
                pb.Image = holesImg;
            }
        }

        private void rbSingle_CheckedChanged(object sender, EventArgs e)
        {
            //if (rbDouble.Checked)
            {
                ClearALL();
            }
        }

    }
}

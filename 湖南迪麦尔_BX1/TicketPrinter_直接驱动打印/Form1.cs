using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TicketPrinter
{
    public partial class Form1 : Form
    {
        private string Pcontent = "";
        private string QRCode = "";
        public Form1()
        {
            InitializeComponent();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            //pictureBox1.Image = QRCodeHelper.CreateQRCode(tbxQrCode.Text.Trim());

            string temp = this.rbxContent.Text;
            if (!this.rbxContent.Text.StartsWith("\n"))
            {
                temp = "\n" + this.rbxContent.Text;//首行需要空行
            }
            //TicketPrinterHelper.Print(temp);

            PrintModel pmTitle = new PrintModel();
            pmTitle.FontFamily = this.cbxTitleFontFamily.Text.Trim();
            pmTitle.FontSize = Convert.ToInt32(this.nudTitleFontSize.Value);
            pmTitle.IsBold = checkBox1.Checked;
            ////pmTitle.Text = new System.IO.StringReader("\n" + this.tbxTitle.Text);//首行需要空行,某些打印机首行不为空时会出现“首行乱码问题”
            pmTitle.Text = new System.IO.StringReader("\n" + this.tbxTitle.Text);
            PrintModel pmContent1 = new PrintModel();
            pmContent1.FontFamily = this.cbxContent1FontFamily.Text.Trim();
            pmContent1.FontSize = Convert.ToInt32(this.nudContent1FontSize.Value);
            pmContent1.IsBold = false;
            pmContent1.Text = new System.IO.StringReader(this.tbxContent.Text);

            int ptime = Convert.ToInt32(this.numUpDownTime.Value);
            for (int i = 0; i < ptime; i++)
            {
                PrintModel pmContent2 = new PrintModel();
                pmContent2.FontFamily = this.cbxContent2FontFamily.Text.Trim();
                pmContent2.FontSize = Convert.ToInt32(this.nudContent2FontSize.Value);
                pmContent2.IsBold = false;
                pmContent2.Text = new System.IO.StringReader(this.rbxContent.Text);

                //PrintModel[] pms = new PrintModel[] { pmTitle, pmContent1, pmContent2 };
                PrintModel[] pms = new PrintModel[] { pmContent2 };

                //TicketPrinterHelper.Print(pms, this.tbxQrCode.Text.Trim());

                TicketPrinterHelper.Print(pms, Pcontent);

                pictureBox1.Image = QRCodeHelper.CreateQRCode(tbxQrCode.Text.Trim());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.tbxTitle.Text = "      农药残留";
            //this.tbxContent.Text = "*****************************************";
            StringBuilder sb = new StringBuilder();
            //sb.Append("\n");
            //sb.Append("*****************************************\n");
            sb.Append("             农药残留" + "\n");
            sb.Append("   检测设备：" + "便携式食品综合分析仪"+ "\n");
            sb.Append("   检测类型：" + "分光光度" + "\n");
            sb.AppendFormat("   检测依据：{0}\r\n", "GB/T5009.199-2003");
            sb.AppendFormat("   检测日期：{0}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sb.AppendFormat("   检测值单位：{0}\r\n", "%");
            sb.AppendFormat("   被检单位：{0}\r\n", "广东达元绿洲食品安全科技股份有限公司");
            sb.AppendFormat("   编号  样品   结果   判定\r\n", "");
            //sb.AppendFormat("-------------------------------\r\n");
            sb.AppendFormat("   {0} {1}  {2}  {3}\r\n", "0001", "黄瓜", "0.5", "不合格");
            //sb.AppendFormat("-------------------------------\r\n");
            //sb.AppendFormat("   对照值：{0}  Abs\r\n", "0.225");
            sb.AppendFormat("   检测人员：{0}", "达元");
            //sb.AppendFormat("   审核人员：{0}\r\n", "");

         
            Pcontent = sb.ToString();
            QRCode = sb.ToString(); 
            this.rbxContent.Text = sb.ToString(); 
            //tbxQrCode.Text= sb.ToString(); 
            tbxQrCode.Text = "检测日期：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n"
                +"检测结果：合格\r\n检测人员：达元";

            FontFamily[] fontFamilies;

            cbxContent1FontFamily.Items.Clear();
            this.cbxContent2FontFamily.Items.Clear();
            this.cbxTitleFontFamily.Items.Clear();
            InstalledFontCollection installedFontCollection = new InstalledFontCollection();
            fontFamilies = installedFontCollection.Families;
            int count = fontFamilies.Length;
            for (int j = 0; j < count; ++j)
            {
                cbxContent1FontFamily.Items.Add(fontFamilies[j].Name);
                this.cbxContent2FontFamily.Items.Add(fontFamilies[j].Name);
                this.cbxTitleFontFamily.Items.Add(fontFamilies[j].Name);
                if (fontFamilies[j].Name.Equals("宋体"))
                {
                    cbxContent1FontFamily.SelectedIndex = j;
                    cbxContent2FontFamily.SelectedIndex = j;
                    cbxTitleFontFamily.SelectedIndex = j;
                }
            } 
        }
    }
}

using System;
using C1.Win.C1FlexGrid;
using System.Windows.Forms;
using System.Drawing;
using C1.Win.C1Report;
using System.Data;

namespace DY.FoodClientLib
{
	/// <summary>
	/// PrintOperation 的摘要说明。
	/// </summary>
	public class PrintOperation
	{
		public PrintOperation()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 打印Grid
		/// </summary>
		/// <param name="FlexGrid">Grid控件</param>
		/// <param name="strHeader">表头（居中）</param>
		/// <param name="strFooter">表尾（居中）</param>
		/// <returns></returns>
		static public void PrintGrid(C1FlexGrid FlexGrid,string strHeader,string strFooter)
		{
            int nFixedCount = 0, nFixedWith = 0;
			System.Drawing.Color cFixedBorderColor,cNormalBorderColor;
			try
			{
				//设置页脚
				if (strFooter == null)
				{
					strFooter = "\t" + "第{0}页" + "共{1}页";
				}

				//设置表头背景
				for (int iNext=0; iNext<FlexGrid.Rows.Fixed; ++iNext)
				{
					FlexGrid.Rows[iNext].StyleNew.BackColor =  Color.White;
				}
				//设置fixed col
				nFixedCount=FlexGrid.Cols.Fixed;
				nFixedWith=FlexGrid.Cols[0].WidthDisplay;
				if ( nFixedCount >0)
				{
					FlexGrid.Cols[0].Visible =false;
				}

				//设置线的颜色为黑色
				cFixedBorderColor=FlexGrid.Styles.Fixed.Border.Color;
				cNormalBorderColor=FlexGrid.Styles.Normal.Border.Color;
				
				FlexGrid.Styles.Fixed.Border.Color=System.Drawing.SystemColors.ControlText;
				FlexGrid.Styles.Normal.Border.Color=System.Drawing.SystemColors.ControlText;

				//设置高亮选择
				FlexGrid.HighLight = HighLightEnum.Never;

				//设置表头字体
				FlexGrid.PrintParameters.HeaderFont = new Font("宋体",24,FontStyle.Bold|FontStyle.Underline);

				FlexGrid.PrintGrid("",PrintGridFlags.ActualSize,
					"\t"+strHeader,strFooter);

				//恢复Grid表头背景
				Color clControl = Color.FromKnownColor(KnownColor.Control);
				for (int iNext=0; iNext<FlexGrid.Rows.Fixed; ++iNext)
				{
					FlexGrid.Rows[iNext].StyleNew.BackColor =  clControl;
				}

				//恢复高亮显示
				FlexGrid.HighLight = HighLightEnum.Always;
				//恢复fixed column
				FlexGrid.Styles.Fixed.Border.Color=cFixedBorderColor;
				FlexGrid.Styles.Normal.Border.Color=cNormalBorderColor;

				if ( nFixedCount >0)
				{
					FlexGrid.Cols[0].Visible =true;
				}
				if (nFixedCount>0 )
				{
					FlexGrid.Cols[0].WidthDisplay=nFixedWith;
				}
				return;
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message,"提示！",MessageBoxButtons.OK,MessageBoxIcon.Error);
				System.Diagnostics.Debug.WriteLine(e.Message);
				return;
			}
		}

		/// <summary>
		/// 打印Grid
		/// </summary>
		/// <param name="FlexGrid">Grid控件</param>
		/// <param name="strHeader">表头（居中）</param>
		/// <param name="strFooter">表尾（居中）</param>
		/// <param name="bLandscape">配置横打纵打（ture横打false纵打）</param>
		/// <returns></returns>
		static public void PrintGrid(C1FlexGrid FlexGrid,string strHeader,string strFooter,bool bLandscape)
		{
			FlexGrid.PrintParameters.PrintDocument.DefaultPageSettings.Landscape = bLandscape;
			PrintGrid(FlexGrid,strHeader,strFooter);
		}

		/// <summary>
		/// 打印预览Grid
		/// </summary>
		/// <param name="FlexGrid">Grid控件</param>
		/// <param name="strHeader">表头（居中）</param>
		/// <param name="strFooter">表尾（居中）</param>
		/// <returns></returns>
        static public void PreviewGrid(C1FlexGrid FlexGrid, string strHeader, string strFooter)
        {
            int nFixedCount = 0, nFixedWith = 0;
            System.Drawing.Color cFixedBorderColor, cNormalBorderColor;
            string path = AppDomain.CurrentDomain.BaseDirectory + "PrintView.ico";// Application.StartupPath;
            //if (text1.Substring(text1.Length-1,1)!="\\")
            //{
            //    text1=text1+"\\";
            //}
            //text1=text1+"PrintView.ico";
            if (System.IO.File.Exists(path))
            {
                System.Drawing.Icon frmIcon = new System.Drawing.Icon(path);
                FlexGrid.PrintParameters.PrintPreviewDialog.Icon = frmIcon;
            }
            try
            {
                //设置页脚
                if (strFooter == null || strFooter == "")
                {
                    strFooter = "\t" + "第{0}页" + "共{1}页";
                }

                //设置表头背景
                for (int iNext = 0; iNext < FlexGrid.Rows.Fixed; ++iNext)
                {
                    FlexGrid.Rows[iNext].StyleNew.BackColor = Color.White;
                }

                //变成黑白的打印状态
                C1.Win.C1FlexGrid.CellStyle styleWhite = FlexGrid.Styles.Add("styleWhite");
                styleWhite.BackColor = Color.White;
                for (int iNext = FlexGrid.Rows.Fixed - 1; iNext < FlexGrid.Rows.Count; ++iNext)
                {
                    FlexGrid.Rows[iNext].Style = styleWhite;
                }

                //设置fixed col
                nFixedCount = FlexGrid.Cols.Fixed;
                nFixedWith = FlexGrid.Cols[0].WidthDisplay;
                if (FlexGrid.Cols.Fixed > 0)
                {
                    FlexGrid.Cols[0].Visible = false;
                }
                //设置线的颜色为黑色
                cFixedBorderColor = FlexGrid.Styles.Fixed.Border.Color;
                cNormalBorderColor = FlexGrid.Styles.Normal.Border.Color;

                FlexGrid.Styles.Fixed.Border.Color = System.Drawing.SystemColors.ControlText;
                FlexGrid.Styles.Normal.Border.Color = System.Drawing.SystemColors.ControlText;

                //设置高亮选择
                FlexGrid.HighLight = HighLightEnum.Never;

                //设置预览对话框100%显示
                FlexGrid.PrintParameters.PrintPreviewDialog.PrintPreviewControl.AutoZoom = false;
                FlexGrid.PrintParameters.PrintPreviewDialog.PrintPreviewControl.Zoom = 1.0;
                FlexGrid.PrintParameters.PrintPreviewDialog.WindowState = FormWindowState.Maximized;
                FlexGrid.PrintParameters.HeaderFont = new Font("宋体", 24, FontStyle.Bold | FontStyle.Underline);

                FlexGrid.PrintGrid("", PrintGridFlags.ShowPreviewDialog | PrintGridFlags.ShowPageSetupDialog,
                    "\t" + strHeader, strFooter);

                //恢复Grid表头背景
                Color clControl = Color.FromKnownColor(KnownColor.Control);
                for (int iNext = 0; iNext < FlexGrid.Rows.Fixed; ++iNext)
                {
                    FlexGrid.Rows[iNext].StyleNew.BackColor = clControl;
                }

                //恢复高亮显示
                FlexGrid.HighLight = HighLightEnum.Always;

                FlexGrid.Styles.Fixed.Border.Color = cFixedBorderColor;
                FlexGrid.Styles.Normal.Border.Color = cNormalBorderColor;

                if (FlexGrid.Cols.Fixed > 0)
                {
                    FlexGrid.Cols[0].Visible = true;
                }
                if (nFixedCount > 0)
                {
                    FlexGrid.Cols[0].WidthDisplay = nFixedWith;
                }
                return;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "提示！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine(e.Message);
                return;
            }
        }

		/// <summary>
		/// 打印预览Grid
		/// </summary>
		/// <param name="FlexGrid">Grid控件</param>
		/// <param name="strHeader">表头（居中）</param>
		/// <param name="strFooter">表尾（居中）</param>
		/// <param name="bLandscape">配置横打纵打（ture横打false纵打）</param>
		/// <returns></returns>
		static public void PreviewGrid(C1FlexGrid FlexGrid,string strHeader,string strFooter,bool bLandscape)
		{
			FlexGrid.PrintParameters.PrintDocument.DefaultPageSettings.Landscape = bLandscape;
			PreviewGrid(FlexGrid,strHeader,strFooter);
		}

		/// <summary>
		/// 预览C1Report报表
		/// </summary>
		/// <param name="c1Report">报表控件</param>
		/// <param name="DataBind">绑定的数据集</param>
		/// <param name="strReportName">报表名字</param>
		public static  void PreviewC1Report(C1Report c1Report,DataTable DataBind,string strReportName)
		{
			try
			{
				c1Report.Load(Application.StartupPath + "\\CheckWorkSheet.xml",strReportName);
				c1Report.DataSource.Recordset=DataBind;

                PrintPreviewDialog PrintPreDlg = new PrintPreviewDialog();
                PrintPreDlg.Document = c1Report.Document;
                PrintPreDlg.PrintPreviewControl.AutoZoom = false;
                PrintPreDlg.PrintPreviewControl.Zoom = 1.0;
                PrintPreDlg.WindowState = FormWindowState.Maximized;
                PrintPreDlg.ShowDialog();
				//PrintPreDlg.Dispose();
				return;
			}
			catch (Exception e)
			{
				MessageBox.Show("打印报表时出错，错误信息： " + e.Message,"提示信息！",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			}
		}
	}
}

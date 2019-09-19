using System;
using C1.Win.C1FlexGrid;
using System.Windows.Forms;
using System.Drawing;
using C1.Win.C1Report;
using System.Data;

namespace DY.FoodClientLib
{
	/// <summary>
	/// PrintOperation ��ժҪ˵����
	/// </summary>
	public class PrintOperation
	{
		public PrintOperation()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// ��ӡGrid
		/// </summary>
		/// <param name="FlexGrid">Grid�ؼ�</param>
		/// <param name="strHeader">��ͷ�����У�</param>
		/// <param name="strFooter">��β�����У�</param>
		/// <returns></returns>
		static public void PrintGrid(C1FlexGrid FlexGrid,string strHeader,string strFooter)
		{
            int nFixedCount = 0, nFixedWith = 0;
			System.Drawing.Color cFixedBorderColor,cNormalBorderColor;
			try
			{
				//����ҳ��
				if (strFooter == null)
				{
					strFooter = "\t" + "��{0}ҳ" + "��{1}ҳ";
				}

				//���ñ�ͷ����
				for (int iNext=0; iNext<FlexGrid.Rows.Fixed; ++iNext)
				{
					FlexGrid.Rows[iNext].StyleNew.BackColor =  Color.White;
				}
				//����fixed col
				nFixedCount=FlexGrid.Cols.Fixed;
				nFixedWith=FlexGrid.Cols[0].WidthDisplay;
				if ( nFixedCount >0)
				{
					FlexGrid.Cols[0].Visible =false;
				}

				//�����ߵ���ɫΪ��ɫ
				cFixedBorderColor=FlexGrid.Styles.Fixed.Border.Color;
				cNormalBorderColor=FlexGrid.Styles.Normal.Border.Color;
				
				FlexGrid.Styles.Fixed.Border.Color=System.Drawing.SystemColors.ControlText;
				FlexGrid.Styles.Normal.Border.Color=System.Drawing.SystemColors.ControlText;

				//���ø���ѡ��
				FlexGrid.HighLight = HighLightEnum.Never;

				//���ñ�ͷ����
				FlexGrid.PrintParameters.HeaderFont = new Font("����",24,FontStyle.Bold|FontStyle.Underline);

				FlexGrid.PrintGrid("",PrintGridFlags.ActualSize,
					"\t"+strHeader,strFooter);

				//�ָ�Grid��ͷ����
				Color clControl = Color.FromKnownColor(KnownColor.Control);
				for (int iNext=0; iNext<FlexGrid.Rows.Fixed; ++iNext)
				{
					FlexGrid.Rows[iNext].StyleNew.BackColor =  clControl;
				}

				//�ָ�������ʾ
				FlexGrid.HighLight = HighLightEnum.Always;
				//�ָ�fixed column
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
				MessageBox.Show(e.Message,"��ʾ��",MessageBoxButtons.OK,MessageBoxIcon.Error);
				System.Diagnostics.Debug.WriteLine(e.Message);
				return;
			}
		}

		/// <summary>
		/// ��ӡGrid
		/// </summary>
		/// <param name="FlexGrid">Grid�ؼ�</param>
		/// <param name="strHeader">��ͷ�����У�</param>
		/// <param name="strFooter">��β�����У�</param>
		/// <param name="bLandscape">���ú���ݴ�ture���false�ݴ�</param>
		/// <returns></returns>
		static public void PrintGrid(C1FlexGrid FlexGrid,string strHeader,string strFooter,bool bLandscape)
		{
			FlexGrid.PrintParameters.PrintDocument.DefaultPageSettings.Landscape = bLandscape;
			PrintGrid(FlexGrid,strHeader,strFooter);
		}

		/// <summary>
		/// ��ӡԤ��Grid
		/// </summary>
		/// <param name="FlexGrid">Grid�ؼ�</param>
		/// <param name="strHeader">��ͷ�����У�</param>
		/// <param name="strFooter">��β�����У�</param>
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
                //����ҳ��
                if (strFooter == null || strFooter == "")
                {
                    strFooter = "\t" + "��{0}ҳ" + "��{1}ҳ";
                }

                //���ñ�ͷ����
                for (int iNext = 0; iNext < FlexGrid.Rows.Fixed; ++iNext)
                {
                    FlexGrid.Rows[iNext].StyleNew.BackColor = Color.White;
                }

                //��ɺڰ׵Ĵ�ӡ״̬
                C1.Win.C1FlexGrid.CellStyle styleWhite = FlexGrid.Styles.Add("styleWhite");
                styleWhite.BackColor = Color.White;
                for (int iNext = FlexGrid.Rows.Fixed - 1; iNext < FlexGrid.Rows.Count; ++iNext)
                {
                    FlexGrid.Rows[iNext].Style = styleWhite;
                }

                //����fixed col
                nFixedCount = FlexGrid.Cols.Fixed;
                nFixedWith = FlexGrid.Cols[0].WidthDisplay;
                if (FlexGrid.Cols.Fixed > 0)
                {
                    FlexGrid.Cols[0].Visible = false;
                }
                //�����ߵ���ɫΪ��ɫ
                cFixedBorderColor = FlexGrid.Styles.Fixed.Border.Color;
                cNormalBorderColor = FlexGrid.Styles.Normal.Border.Color;

                FlexGrid.Styles.Fixed.Border.Color = System.Drawing.SystemColors.ControlText;
                FlexGrid.Styles.Normal.Border.Color = System.Drawing.SystemColors.ControlText;

                //���ø���ѡ��
                FlexGrid.HighLight = HighLightEnum.Never;

                //����Ԥ���Ի���100%��ʾ
                FlexGrid.PrintParameters.PrintPreviewDialog.PrintPreviewControl.AutoZoom = false;
                FlexGrid.PrintParameters.PrintPreviewDialog.PrintPreviewControl.Zoom = 1.0;
                FlexGrid.PrintParameters.PrintPreviewDialog.WindowState = FormWindowState.Maximized;
                FlexGrid.PrintParameters.HeaderFont = new Font("����", 24, FontStyle.Bold | FontStyle.Underline);

                FlexGrid.PrintGrid("", PrintGridFlags.ShowPreviewDialog | PrintGridFlags.ShowPageSetupDialog,
                    "\t" + strHeader, strFooter);

                //�ָ�Grid��ͷ����
                Color clControl = Color.FromKnownColor(KnownColor.Control);
                for (int iNext = 0; iNext < FlexGrid.Rows.Fixed; ++iNext)
                {
                    FlexGrid.Rows[iNext].StyleNew.BackColor = clControl;
                }

                //�ָ�������ʾ
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
                MessageBox.Show(e.Message, "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine(e.Message);
                return;
            }
        }

		/// <summary>
		/// ��ӡԤ��Grid
		/// </summary>
		/// <param name="FlexGrid">Grid�ؼ�</param>
		/// <param name="strHeader">��ͷ�����У�</param>
		/// <param name="strFooter">��β�����У�</param>
		/// <param name="bLandscape">���ú���ݴ�ture���false�ݴ�</param>
		/// <returns></returns>
		static public void PreviewGrid(C1FlexGrid FlexGrid,string strHeader,string strFooter,bool bLandscape)
		{
			FlexGrid.PrintParameters.PrintDocument.DefaultPageSettings.Landscape = bLandscape;
			PreviewGrid(FlexGrid,strHeader,strFooter);
		}

		/// <summary>
		/// Ԥ��C1Report����
		/// </summary>
		/// <param name="c1Report">����ؼ�</param>
		/// <param name="DataBind">�󶨵����ݼ�</param>
		/// <param name="strReportName">��������</param>
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
				MessageBox.Show("��ӡ����ʱ����������Ϣ�� " + e.Message,"��ʾ��Ϣ��",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			}
		}
	}
}

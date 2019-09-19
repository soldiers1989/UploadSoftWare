using System;
using System.Windows;
using System.Windows.Documents;
using AIO;
using com.lvrenyang;
using DYSeriesDataSet.DataModel;

namespace WpfPrintDemo
{
    class OrderDocumentRenderer : IDocumentRenderer
    {
        private int _num = 0;
        private string logType = "print-error";
        public void Render(FlowDocument doc, object data)
        {
            try
            {
                TableRowGroup group = doc.FindName("rowsDetails") as TableRowGroup;
                Style styleCell = doc.Resources["BorderedCell"] as Style;
                if (Global.PrintType.Equals(""))
                {
                    foreach (DYSeriesDataSet.DataModel.ReportClass.ReportDetail item in ((ReportClass)data).r_reportList)
                    {
                        TableRow row = new TableRow();
                        //序号
                        TableCell cell = new TableCell(new Paragraph(new Run((_num += 1).ToString())));
                        cell.Style = styleCell;
                        row.Cells.Add(cell);
                        //食品名称
                        cell = new TableCell(new Paragraph(new Run(item.FoodName)));
                        cell.Style = styleCell;
                        row.Cells.Add(cell);
                        //项目名称
                        cell = new TableCell(new Paragraph(new Run(item.ProjectName)));
                        cell.Style = styleCell;
                        row.Cells.Add(cell);
                        //单位
                        cell = new TableCell(new Paragraph(new Run(item.Unit)));
                        cell.Style = styleCell;
                        row.Cells.Add(cell);
                        //检测数据
                        cell = new TableCell(new Paragraph(new Run(item.CheckData)));
                        cell.Style = styleCell;
                        row.Cells.Add(cell);

                        group.Rows.Add(row);
                    }
                }
                else if (Global.PrintType.Equals("GS"))
                {
                    foreach (DYSeriesDataSet.DataModel.clsReportGS.ReportDetail item in ((clsReportGS)data).reportDetailList)
                    {
                        TableRow row = new TableRow();
                        //序号
                        TableCell cell = new TableCell(new Paragraph(new Run((_num += 1).ToString())));
                        cell.Style = styleCell;
                        row.Cells.Add(cell);
                        //项目名称
                        cell = new TableCell(new Paragraph(new Run(item.ProjectName)));
                        cell.Style = styleCell;
                        row.Cells.Add(cell);
                        //单位
                        cell = new TableCell(new Paragraph(new Run(item.Unit)));
                        cell.Style = styleCell;
                        row.Cells.Add(cell);
                        //标准值
                        cell = new TableCell(new Paragraph(new Run(item.InspectionStandard)));
                        cell.Style = styleCell;
                        row.Cells.Add(cell);
                        //实测值
                        cell = new TableCell(new Paragraph(new Run(item.IndividualResults)));
                        cell.Style = styleCell;
                        row.Cells.Add(cell);
                        //检测结果
                        cell = new TableCell(new Paragraph(new Run(item.IndividualDecision)));
                        cell.Style = styleCell;
                        row.Cells.Add(cell);

                        group.Rows.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
                FileUtils.OprLog(6, logType, ex.ToString());
                MessageBox.Show("异常(Render):\n" + ex.Message);
            }
        }
    }
}
using GemBox.Spreadsheet;
using NigelFinanceManage.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NigelFinanceManage.Service
{
    public class DayRangeReport : ReportTemplate
    {
        ExcelOutput output;
        ExcelFile workbook;
        ExcelWorksheet worksheet;
        DateTime dateFrom;
        DateTime dateTo;
        DataTable dt;
        int row;
        string financeType;

        public DayRangeReport(string financeType, DateTime dateFrom, DateTime dateTo, DataTable dt)
        {
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            SpreadsheetInfo.FreeLimitReached +=
             (sender, e) => e.FreeLimitReachedAction = FreeLimitReachedAction.ContinueAsTrial;
            output = new ExcelOutput
                ("Report_" + financeType + "_" + 
                dateFrom.ToString("yyyyMMdd") + "_" +
                dateTo.ToString("yyyyMMdd")  + ".xls");
            this.workbook = new ExcelFile();
            this.worksheet = workbook.Worksheets.Add("Report");
            this.dateFrom = dateFrom;
            this.dateTo = dateTo;
            this.dt = dt;
            this.financeType = financeType;
            row = 0;
            worksheet.Columns[3].Width = 35 * 256;
            worksheet.Columns[1].Width = 15 * 256;
            worksheet.Columns[2].Width = 12 * 256;
            worksheet.Columns[1].Style.HorizontalAlignment = HorizontalAlignmentStyle.Right;
        }

        public override void printTitle()
        {
            worksheet.Cells[row, 0].Value = financeType.ToUpper() + " DAY RANGE REPORT";
            worksheet.Cells[row++, 0].Style.Font.Weight = ExcelFont.BoldWeight;
            worksheet.Cells[row, 2].Value = dateFrom.ToString("dd.MM.yyyy")
                + " - " + dateTo.ToString("dd.MM.yyyy");
            worksheet.Cells[row++, 2].Style.Font.Italic = true;
        }

        public override void printData()
        {
            worksheet.InsertDataTable(dt,
            new InsertDataTableOptions()
            {
                ColumnHeaders = true,
                StartRow = 4
            });
            row += dt.Rows.Count + 4;
        }

        public override void printTotal()
        {
            int total = 0;
            foreach (DataRow dr in this.dt.Rows)
            {
                total += int.Parse(dr["Amount"].ToString());
            }
            worksheet.Cells[row, 0].Value = "Total";
            worksheet.Cells[row, 1].Value = total;
            worksheet.Cells[row, 1].Style.Font.Size = 18 * 20;
        }

        public override void save()
        {
            workbook.Save(output.Path);
        }
    }
}

using Microsoft.IdentityModel.Tokens;

namespace SeverGrpc_NHibernate.Utilities
{
    public class ExcelService : IExcelService
    {
        public async Task<ExcelPackage> ExportExcel(string tenSheet, List<string> HeaderExcel, List<List<string>> BodyExcel, string CellNameAddress, int RowHeaderIndex, int RecordIndex)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excel = new ExcelPackage();
            ExcelWorksheet workSheet = excel.Workbook.Worksheets.Add(tenSheet);
            workSheet.Cells[CellNameAddress].Style.Font.Bold = true;
            workSheet.Cells[CellNameAddress].Value = tenSheet;

            for (int i = 1; i <= HeaderExcel.Count; i++)
            {
                workSheet.Cells[RowHeaderIndex, i].Value = HeaderExcel[i - 1];
                workSheet.Cells[RowHeaderIndex, i].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                workSheet.Cells[RowHeaderIndex, i].Style.Font.Bold = true;
            };
            int recordIndex = RecordIndex;
            for (int i = 1; i <= BodyExcel.Count; i++)
            {
                for (int j = 1; j <= BodyExcel[0].Count(); j++)
                {
                    workSheet.Cells[recordIndex, j].Value = !BodyExcel[i - 1][j - 1].IsNullOrEmpty() ? BodyExcel[i - 1][j - 1] : "-";
                    if (BodyExcel[i - 1][j - 1] != null && BodyExcel[i - 1][j - 1].Contains("\n"))
                        workSheet.Cells[recordIndex, j].Style.WrapText = true;
                }
                recordIndex++;
            }
            for (int i = 1; i <= HeaderExcel.Count; i++)
            {
                workSheet.Column(i).AutoFit();
            }
            return excel;
        }

    }
}

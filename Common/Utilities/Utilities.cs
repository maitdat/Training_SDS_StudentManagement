using Common;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Globalization;
using System.Linq.Expressions;

namespace SeverGrpc_NHibernate.Utilities
{
    public static class Utilities
    {
        public static IQueryable<TSource> ApplyPaging<TSource>(this IQueryable<TSource> source, int pageNo, int pageSize)
        {
            return pageSize > 0 ? source.Skip((pageNo - 1) * pageSize).Take(pageSize) : source;
        }

        public static IQueryable<TSource> ApplyPaging<TSource>(this IEnumerable<TSource> source, int pageNo, int pageSize)
        {
            return pageSize > 0 ? source.Skip((pageNo - 1) * pageSize).Take(pageSize).AsQueryable() : source.AsQueryable();
        }

        public static IQueryable<TSource> ApplyPaging<TSource>(this IEnumerable<TSource> source, int pageNo, int pageSize, out int totalItem)
        {
            totalItem = source.Count();
            return pageSize > 0 ? source.Skip((pageNo - 1) * pageSize).Take(pageSize).AsQueryable() : source.AsQueryable();
        }

        public static IQueryable<T> ApplySortingDate<T>(this IQueryable<T> query, Sort sortType, Expression<Func<T, DateTime>> orderByExpression)
        {
            return sortType == 0 ? query : sortType == Sort.Asc ? query.OrderBy(orderByExpression) : query.OrderByDescending(orderByExpression);
        }

        public static string GetUrlAnhDaiDien(string urlPath, string userName)
        {
            return urlPath + "api/PhatTuUser/AnhDaiDien/" + userName;
        }

        public static bool CheckHasSpecialChar(string input)
        {
            var charMathInPath = Path.GetInvalidFileNameChars();
            foreach (var item in charMathInPath)
            {
                if (input.Contains(item)) return true;
            }

            return false;
        }

        public static string? NullIfEmpty(this string s)
        {
            return string.IsNullOrEmpty(s) ? null : s;
        }

        public static string? NullIfWhiteSpace(this string s)
        {
            return string.IsNullOrWhiteSpace(s) ? null : s;
        }

        public static string GenerateRandomPassword(int numberChar)
        {
            Random random = new Random();
            string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, numberChar)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static DateTime? ConvertDateTimeFromString(string dateValue)
        {
            DateTime parsedDate;
            string[] formats = { "dd/M/yyyy", "dd/MM/yyyy", "d/M/yyyy", "dd/M/yyyy hh:mm:ss tt", "dd/MM/yyyy hh:mm:ss tt", "d/M/yyyy hh:mm:ss tt" };

            if (DateTime.TryParseExact(dateValue, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
            {
                return parsedDate;
            }

            return null;
        }

        public static ExcelPackage ExportExcel(string sheetName, List<string> HeaderExcel, List<List<string>> BodyExcel, string CellNameAddress, int RowHeaderIndex, int RecordIndex)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excel = new ExcelPackage();
            ExcelWorksheet workSheet = excel.Workbook.Worksheets.Add(sheetName);
            workSheet.Cells[CellNameAddress].Style.Font.Bold = true;
            workSheet.Cells[CellNameAddress].Value = sheetName;

            for (int i = 1; i <= HeaderExcel.Count; i++)
            {
                workSheet.Cells[RowHeaderIndex, i].Value = HeaderExcel[i - 1];
                workSheet.Cells[RowHeaderIndex, i].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                workSheet.Cells[RowHeaderIndex, i].Style.Font.Bold = true;
            }
            ;
            int recordIndex = RecordIndex;
            for (int i = 1; i <= BodyExcel.Count; i++)
            {
                for (int j = 1; j <= BodyExcel[0].Count(); j++)
                {
                    workSheet.Cells[recordIndex, j].Value = !string.IsNullOrEmpty(BodyExcel[i - 1][j - 1]) ? BodyExcel[i - 1][j - 1] : "-";
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

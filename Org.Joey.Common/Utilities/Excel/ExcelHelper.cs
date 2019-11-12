namespace Org.Joey.Common
{
    using System.Collections.Generic;
    using OfficeOpenXml;
    using System;
    using System.Xml.Serialization;

    public class ExcelHelper
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(ExcelHelper));
        public static IEnumerable<T> ReadExcel<T>(string excel,
            Func<int, string[], object[], T> convert,
            int workSeetIndex = 0,
            int startRowIndex = 1,
            int startColumnIndex = 1)
        {
            using (var package = new ExcelPackage(new System.IO.FileInfo(excel)))
            {
                var worksheet = package.Workbook.Worksheets[workSeetIndex];
                var columns = worksheet.Dimension.Columns;
                var headers = new string[columns];

                for (int index = startColumnIndex; index <= columns; index++)
                {
                    headers[index - startColumnIndex] = worksheet.Cells[startRowIndex, index].Value?.ToString();
                }
                for (var row = startRowIndex + 1; row <= worksheet.Dimension.Rows; row++)
                {
                    var rangs = new string[columns];
                    for (int cel = startColumnIndex; cel <= columns; cel++)
                    {
                        var value = worksheet.Cells[row, cel].Value?.ToString();
                        rangs[cel - startColumnIndex] = value;
                    }
                    yield return convert(row, headers, rangs);
                }

            }
        }
        public static void ReadExcel(string excel)
        {
            using (var package = new ExcelPackage(new System.IO.FileInfo(excel)))
            {
                var table = package.Workbook.Worksheets[0].PivotTables[0].PivotTableXml;
                
                
                     

            }
        }
    }
}

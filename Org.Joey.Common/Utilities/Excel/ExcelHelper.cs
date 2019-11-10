namespace Org.Joey.Common
{
    using System.Collections.Generic;
    using OfficeOpenXml;
    using System;
    public class ExcelHelper
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(ExcelHelper));
        public static IEnumerable<T> ReadExcel<T>(string excel,
            Func<int, string[], object[], T> convert,
            int workSeetIndex)
        {
            using (var package = new ExcelPackage(new System.IO.FileInfo(excel)))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var columns = worksheet.Dimension.Columns;
                var headers = new string[columns];

                for (int index = 1; index <= columns; index++)
                {
                    headers[index - 1] = worksheet.Cells[1, index].Value?.ToString();
                }
                for (var row = 2; row <= worksheet.Dimension.Rows; row++)
                {                    
                    var rangs = new string[columns];
                    for (int cel = 1; cel <= columns; cel++)
                    {
                        var value = worksheet.Cells[row, cel].Value?.ToString();                    
                        rangs[cel - 1] = value;
                    }
                    yield return convert(row, headers, rangs);                    
                }

            }
        }
    }
}

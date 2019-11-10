
namespace CPR.Data.Import
{
    using System.Collections.Generic;
    using System.Linq;
    using System;
    using CPR.Data.Import.Models;
    using Org.Joey.Common.Models;

    public static class StringExtension
    {
        public static DataSourceNames? DetectDatasource(this string fileNmae)
        {
            if (Constants.ChatValidStrings.Any(o =>
            {
                return fileNmae.IndexOf(o, System.StringComparison.OrdinalIgnoreCase) >= 0;
            }))
            {
                return DataSourceNames.Chat;
            }

            if (Constants.AvayaValidStrings.Any(o =>
            {
                return fileNmae.IndexOf(o, System.StringComparison.OrdinalIgnoreCase) >= 0;
            }))
            {
                return DataSourceNames.Avaya;
            }

            if (Constants.msxSQOValidStrings.Any(o =>
            {
                return fileNmae.IndexOf(o, System.StringComparison.OrdinalIgnoreCase) >= 0;
            }))
            {
                return DataSourceNames.MSXSQO;
            }
            if (Constants.msxTQLValidStrings.Any(o =>
            {
                return fileNmae.IndexOf(o, System.StringComparison.OrdinalIgnoreCase) >= 0;
            }))
            {
                return DataSourceNames.MSXTQL;
            }
            return null;
        }

        public static T TryFirst<T>(this IEnumerable<T> array, Func<T, bool> prediction)
            where T : class
        {
            try
            {
                return array.First(prediction);
            }
            catch
            {
                return default(T);
            }
        }
        public static void Fix(this ExcelSignleRow row,
            DataSourceNames source,
            Mapping[] mappings)
        {
            var properties = new List<PropertyObject>();
            properties.AddRange(row.Properties);
            FixBatchJobID(source, row.BatchJob.Id, properties, mappings);
            row.Properties = properties.ToArray();
        }

        private static void FixBatchJobID(DataSourceNames source, string batchJobId, List<PropertyObject> properties, Mapping[] mappings)
        {
            properties.Add(new PropertyObject()
            {
                Descriptor = new PropertyDescriptor() { FiledName = "BatchJobId", Type = System.Data.SqlDbType.NVarChar, Length = 50 },
                Value = batchJobId
            });
        }
        private static void FixVolume(DataSourceNames source, List<PropertyObject> properties)
        {
            if (source == DataSourceNames.Chat)
            {
                properties.Add(new PropertyObject()
                {
                    Descriptor = new PropertyDescriptor() { FiledName = "BatchJobId", Type = System.Data.SqlDbType.Int },
                    Value = 1
                });
            }
        }

    }
}


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
            if (Constants.DirectVolumeTeamLevel.Any(o =>
            {
                return fileNmae.IndexOf(o, System.StringComparison.OrdinalIgnoreCase) >= 0;
            }))
            {
                return DataSourceNames.DirectVolumeTeamLevel;
            }
            if (Constants.PhoneVolume.Any(o =>
            {
                return fileNmae.IndexOf(o, System.StringComparison.OrdinalIgnoreCase) >= 0;
            }))
            {
                return DataSourceNames.PhoneVolume;
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

        public static bool PrimaryKeyRequired(this ExcelSignleRow row, DataSourceNames name)
        {
            switch (name)
            {
                case DataSourceNames.Avaya:
                case DataSourceNames.Chat:
                    var agent = row.Properties.TryFirst(o => o.Descriptor.FiledName.Equals("Agent"))?.Value?.ToString();
                    var skillSet = row.Properties.TryFirst(o => o.Descriptor.FiledName.Equals("SkillSet"))?.Value?.ToString();
                    bool bCreatedDateTime = DateTime.TryParse(
                        row.Properties.TryFirst(o => o.Descriptor.FiledName.Equals("CreatedDateTime"))?.Value?.ToString(),
                         out DateTime dt);
                    var createdDateTime = row.Properties.TryFirst(o => o.Descriptor.FiledName.Equals("CreatedDateTime"))?.Value?.ToString();
                    return string.IsNullOrEmpty(agent) == false && string.IsNullOrEmpty(skillSet) == false && bCreatedDateTime == true;
                case DataSourceNames.MSXSQO:
                    var opportunityId = row.Properties.TryFirst(o => o.Descriptor.FiledName.Equals("OpportunityId"))?.Value?.ToString();
                    return string.IsNullOrEmpty(opportunityId) == false;
                case DataSourceNames.MSXTQL:
                    var leadId = row.Properties.TryFirst(o => o.Descriptor.FiledName.Equals("OpportunityId"))?.Value?.ToString();
                    return string.IsNullOrEmpty(leadId) == false;
            }
            return false;
        }


    }
}

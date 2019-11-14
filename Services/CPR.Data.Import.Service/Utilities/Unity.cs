



namespace CPR.Data.Import
{
    using CPR.Data.Import.Models;
    using Microsoft.Extensions.Configuration;
    using Org.Joey.Common;
    using System;
    using System.Collections.Generic;
    using Microsoft.SqlServer.Server;
    using Org.Joey.Common.Models;
    using System.Linq;
    public static class Unity
    {

        public static PropertyDescriptor[] GetDescriptor(DataSourceNames name)
        {
            return IoC.GetService<IConfiguration>()
                .GetSection("mappings")
                .Get<Dictionary<DataSourceNames, PropertyDescriptor[]>>()[name];
        }
        public static string GetWatchingFolder()
        {
            return IoC.GetService<IConfiguration>()
                .GetValue<string>("watchingFolder");
        }

        public static SqlDataRecord Convert(ExcelSignleRow row, DataSourceNames name)
        {
            try
            {
                if (row == null) throw new ArgumentNullException("data can't be null in convert");
                if (row.PrimaryKeyRequired(name) == false)
                {
                    throw new NullReferenceException($"Primary Key can't be null{name}");
                }
                var record = new SqlDataRecord(Constants.Structures[name]);
                for (var index = 0; index < Constants.Structures[name].Length; index++)
                {
                    var fieldName = Constants.Structures[name][index].Name;
                    var property = row.Properties.TryFirst(o => o.Descriptor.FiledName.Equals(fieldName, StringComparison.OrdinalIgnoreCase));
                    if (property == null)
                        record.SetDBNull(index);
                    else
                    {

                        if (property.Descriptor.Type == System.Data.SqlDbType.DateTime)
                        {
                            if (property.Value.TryToUnixStampDateTime(out long? timestamp))
                            {
                                record.SetInt64(index, timestamp ?? 0);
                            }
                            else
                            {
                                record.SetDBNull(index);
                            }
                        }
                        else if (property.Descriptor.Type == System.Data.SqlDbType.Int)
                        {
                            if (int.TryParse(property.Value?.ToString(), out int val))
                            {
                                record.SetInt32(index, val);
                            }
                            else
                            {
                                record.SetDBNull(index);
                            }
                        }
                        else if (property.Descriptor.Type == System.Data.SqlDbType.Money)
                        {
                            if (decimal.TryParse(property.Value?.ToString(), out decimal money))
                            {
                                record.SetSqlMoney(index, money);
                            }
                            else
                            {
                                record.SetDBNull(index);
                            }
                        }
                        else
                        {
                            record.SetStringIfNullOrEmpty(index, property.Value?.ToString());
                        }
                    }
                }
                return record;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        static string lastMonth = string.Empty;
        static string lastWeek = string.Empty;
        public static SqlDataRecord ConvertforTeamLevel(ExcelSignleRow row, DataSourceNames name)
        {
            var record = new SqlDataRecord(Constants.Structures[name]);
            for (var index = 0; index < Constants.Structures[name].Length; index++)
            {
                var month = row.JMetadata.TryGetValue<string>("$.Month");
                var week = row.JMetadata.TryGetValue<string>("$.Week");
                if (month == null && lastMonth != string.Empty)
                    month = lastMonth;
                if (month != null && month.IndexOf("Grand Total") >= 0) return null;
                if (string.IsNullOrEmpty(week))
                    week = "Week 1";
                switch (Constants.Structures[name][index].Name)
                {
                    case "Monthly":
                        record.SetStringIfNullOrEmpty(index, month);
                        break;
                    case "Weekly":
                        record.SetStringIfNullOrEmpty(index, week);
                        break;
                    case "IsTotalLine":
                        record.SetBoolean(index, month.IndexOf("Total") >= 0);
                        break;
                    case "BatchJobId":
                        record.SetStringIfNullOrEmpty(index, row.BatchJob?.Id);
                        break;
                    case "Metadata":
                        record.SetStringIfNullOrEmpty(index, row.JMetadata?.ToString());
                        break;
                }
                if (month != null)
                    lastMonth = month;
            }
            return record;
        }

        public static SqlDataRecord Convert(TeamLevelReport report, DataSourceNames name)
        {
            var record = new SqlDataRecord(Constants.Structures[name]);
            for (var index = 0; index < Constants.Structures[name].Length; index++)
            {
                switch (Constants.Structures[name][index].Name)
                {
                    case "Monthly":
                        record.SetStringIfNullOrEmpty(index, report.Monthly);
                        break;
                    case "Weekly":
                        record.SetStringIfNullOrEmpty(index, report.Weekly);
                        break;
                    case "IsTotalLine":
                        record.SetBoolean(index, report.IsTotalLine);
                        break;
                    case "BatchJobId":
                        record.SetStringIfNullOrEmpty(index, report.BatchJob);
                        break;
                    case "Metadata":
                        record.SetStringIfNullOrEmpty(index, report.Metadata);
                        break;
                }
            }
            return record;
        }
        public static TeamLevelReport Convertfrom(ExcelSignleRow row)
        {
            var month = row.JMetadata.TryGetValue<string>("$.Month");
            return new TeamLevelReport()
            {
                Monthly = month,
                Weekly = row.JMetadata.TryGetValue<string>("$.Week"),
                BatchJob = row.BatchJob.Id,
                IsTotalLine = (month != null && month.IndexOf("total", StringComparison.OrdinalIgnoreCase) >= 0),
                Metadata = row.JMetadata.ToString()
            };
        }

        public static IEnumerable<TeamLevelReport> Fix(this IEnumerable<TeamLevelReport> reports)
        {
            var array = reports.ToArray();
            for (int i = 0; i < array.Length; i++)
            {
                if (string.IsNullOrEmpty(array[i].Weekly)) array[i].Weekly = "Week 1";
                if (i != 0 && string.IsNullOrEmpty(array[i].Monthly))
                {
                    array[i].Monthly = array[i - 1].Monthly;
                }
            }
            return array.GroupBy(o => new { o.Weekly, o.Monthly })
                .Select((ctx) =>
                {
                    return ctx.ToList().First();
                }).Where(o => !string.IsNullOrEmpty(o.Monthly) && !string.IsNullOrEmpty(o.Weekly));
        }
    }
}

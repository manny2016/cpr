﻿

namespace CPR.Data.Import.Services
{
    using System;
    using System.IO;
    using System.Threading;
    using CPR.Data.Import.Models;
    using Org.Joey.Common;
    using Org.Joey.Common.Models;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json.Linq;

    public class DataImportService : IProcessService<ExcelSignleRow>
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(DataImportService));
        private ImportSettings settings;
        private readonly IDataImportDirectly directly;
        public DataImportService(ImportSettings settings,
            IDataImportDirectly directly)
        {
            this.settings = settings;
            this.directly = directly;

        }
        public void Dispose()
        {

        }




        public void Process(Action<ExcelSignleRow> pass, CancellationToken token)
        {
            var job = GenernateBatchJob();
            this.directly.SaveBatchJobs(job);
            foreach (var item in job.FileEntities)
            {
                var source = item.FileName.DetectDatasource();
                if (source == null)
                {
                    Logger.Error($"Not a vaild data source ({item.FileName}).");
                    continue;
                }
                if (Constants.IndividualDataSources.Any(o => o.Equals(source)))
                {
                    ReadIndividualDatasource(pass, job, item, source.Value);
                }
                if (Constants.TeamLevelDataSources.Any(o => o.Equals(source)))
                {
                    ReadTeamLevelDatasource(pass, job, item, source.Value);
                }
            }
        }
        private void ReadIndividualDatasource(Action<ExcelSignleRow> pass,
            BatchJob jobs,
            FileEntity item,
            DataSourceNames source)
        {
            try
            {
                var startRuning = DateTime.UtcNow;

                var count = 0;
                foreach (var row in ExcelHelper.ReadExcel<ExcelSignleRow>(item.FileName, Convert, 0))
                {
                    row.DataSourceName = source;
                    row.BatchJob = jobs;
                    var descriptors = Unity.GetDescriptor(source);
                    for (var i = 0; i < row.Properties.Length; i++)
                    {
                        var header = row.Properties[i].Descriptor.Header;
                        row.Properties[i].Descriptor = descriptors.TryFirst(o => o.Header.Equals(header, StringComparison.OrdinalIgnoreCase));
                    }
                    row.Properties = row.Properties.Where(o => o.Descriptor != null).ToArray();
                    row.Fix(source, this.directly.GetMappings());
                    row.DateTime = startRuning;
                    count++;
                    pass(row);
                }

                Logger.Warn($"Total Rows {count};{item.FileName}");
            }
            catch (IOException ex)
            {
                Logger.Error($"Excel file can't be read;{ex.Message}", ex);
                return;
            }
        }
        private void ReadTeamLevelDatasource(Action<ExcelSignleRow> pass,
            BatchJob job,
            FileEntity item,
            DataSourceNames source)
        {
            try
            {
                var startRuning = DateTime.UtcNow;
                var startRowIndex = source == DataSourceNames.TeamLevelReport ? 19 : 1;
                var startColumnIndex = source == DataSourceNames.TeamLevelReport ? 2 : 1;

                foreach (var jMetadata in ExcelHelper.ReadExcel<JObject>(item.FileName, 0, startRowIndex, startColumnIndex))
                {
                    pass(new ExcelSignleRow()
                    {
                        BatchJob = job,
                        DataSourceName = source,
                        DateTime = startRuning,
                        JMetadata = jMetadata,
                        Properties = null,
                    });
                }
            }
            catch (IOException ex)
            {
                Logger.Error($"Excel file can't be read;{ex.Message}", ex);
                return;
            }
        }
        public ExcelSignleRow Convert(int index, string[] headers, object[] objects)
        {
            var row = new ExcelSignleRow();
            row.Properties = new PropertyObject[headers.Length];
            for (var i = 0; i < headers.Length; i++)
            {
                row.Properties[i] = new PropertyObject()
                {
                    Descriptor = new PropertyDescriptor() { Header = headers[i] },
                    Value = objects[i]
                };
            }
            return row;
        }
        private BatchJob GenernateBatchJob()
        {

            var directory = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, Unity.GetWatchingFolder())
                .PromissDirectoryExists());
            var lastImportTime = this.directly.GetNextTime($"[{System.Net.Dns.GetHostName()}]CPR Data Processing");
            var guid = Guid.NewGuid().ToString();
            var entities = directory.GetFiles("*.xls?")
                .Where((ctx) =>
                {
                    if (lastImportTime == null) return true;
                    return ctx.LastWriteTimeUtc.ToUnixStampDateTime() > lastImportTime;
                }).Select(ctx =>
                {
                    return new FileEntity()
                    {
                        FileName = ctx.FullName,
                        MD5 = ctx.FullName.GetMd5HashFromFile()
                    };
                });
            return new BatchJob()
            {
                Id = Guid.NewGuid().ToString(),
                Metadata = entities.SerializeToJson(),
                CreatedDateTime = DateTime.UtcNow.ToUnixStampDateTime(),
                Agent = System.Net.Dns.GetHostName(),
                FileEntities = entities.ToArray()
            };
        }



    }
}

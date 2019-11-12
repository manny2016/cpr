

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
            var jobs = GenernateBatchJob();
            this.directly.SaveBatchJobs(jobs);
            foreach (var item in jobs.FileEntities)
            {
                try
                {
                    var startRuning = DateTime.UtcNow;
                    var source = item.FileName.DetectDatasource();
                    if (source == null)
                    {
                        Logger.Error($"Not a vaild data source ({item.FileName}).");
                        continue;
                    }
                    var count = 0;
                    foreach (var row in ExcelHelper.ReadExcel<ExcelSignleRow>(item.FileName, Convert, 1))
                    {
                        row.DataSourceName = source.Value;
                        row.BatchJob = jobs;
                        var descriptors = Unity.GetDescriptor(source.Value);

                        for (var i = 0; i < row.Properties.Length; i++)
                        {
                            var header = row.Properties[i].Descriptor.Header;
                            row.Properties[i].Descriptor = descriptors.TryFirst(o => o.Header.Equals(header, StringComparison.OrdinalIgnoreCase));
                        }
                        row.Properties = row.Properties.Where(o => o.Descriptor != null).ToArray();
                        row.Fix(source.Value, this.directly.GetMappings());
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



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
        public DataImportService(ImportSettings settings)
        {
            this.settings = settings;
        }
        public void Dispose()
        {

        }

        public void Process(Action<ExcelSignleRow> pass, CancellationToken token)
        {

        }
        private IEnumerable<BatchJob> GenernateBatchJobs()
        {
            var directory = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, Unity.GetWatchingFolder())
                .PromissDirectoryExists());
            return directory.GetFiles("*.xls?").Select(ctx =>
            {
                return new BatchJob()
                {
                    Agent = System.Net.Dns.GetHostName(),
                    CreatedDateTime = DateTime.UtcNow.ToUnixStampDateTime(),
                    FileName = ctx.FullName,
                    MD5 = ctx.FullName.GetMd5HashFromFile(),
                };
            });
        }
        

    }
}

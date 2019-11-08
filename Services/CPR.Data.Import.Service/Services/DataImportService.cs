

namespace CPR.Data.Import.Services
{
    using System;
    using System.Threading;
    using CPR.Data.Import.Models;
    using Org.Joey.Common;
    using Org.Joey.Common.Models;
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
            var excels = new string[] { };
        }

    }
}

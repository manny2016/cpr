
namespace CPR.Data.Import.Services
{
    using System.Collections.Generic;
    using Org.Joey.Common;
    using Org.Joey.Common.Models;
    public class DataImportResultService : IProcessingResultService<ExcelSignleRow>
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(DataImportResultService));
        public void Dispose()
        {
            
        }

        public void Save(IEnumerable<ExcelSignleRow> results)
        {
            
        }
    }
}

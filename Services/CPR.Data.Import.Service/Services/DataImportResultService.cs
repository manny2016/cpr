
namespace CPR.Data.Import.Services
{
    using System.Collections.Generic;
    using Org.Joey.Common;
    using Org.Joey.Common.Models;
    using System.Linq;

    public class DataImportResultService : IProcessingResultService<ExcelSignleRow>
    {
        private readonly IDataImportDirectly directly;
        public DataImportResultService(IDataImportDirectly directly)
        {
            this.directly = directly;
        }
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(DataImportResultService));
        public void Dispose()
        {

        }

        public void Save(IEnumerable<ExcelSignleRow> results)
        {
            this.directly.Import(results);
        }
    }
}



namespace CPR.Data.Import.Models
{
    using CPR.Data.Import.Services;
    using Microsoft.Extensions.Configuration;
    using Org.Joey.Common;
    using Org.Joey.Common.Models;
    public class ImportSettings : IProcessSetting<ExcelSignleRow>
    {
        public ImportSettings(IConfiguration configuration)
        {

        }
        public IProcessService<ExcelSignleRow> GenerateProcessService()
        {
            return new DataImportService(this, IoC.GetService<IDataImportDirectly>());
        }
    }
}


namespace CPR.Data.Import.Models
{
    using Org.Joey.Common;
    using Org.Joey.Common.Models;
    using CPR.Data.Import.Services;
    public class ImportState : ProcessState<ExcelSignleRow>
    {
        public ImportState(ImportSettings setting) : base(setting)
        {
        }

        public override string Name
        {
            get
            {
                return $"[{System.Net.Dns.GetHostName()}]CPR Data Processing";
            }
        }

        public override IProcessingResultService<ExcelSignleRow> GenerateProcessingResultService()
        {
            return new DataImportResultService(IoC.GetService<IDataImportDirectly>());
        }
    }
}

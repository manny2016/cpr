



namespace CPR.Data.Import
{
    using System.Collections.Generic;
    using CPR.Data.Import.Models;
    using Org.Joey.Common.Models;
    public interface IDataImportDirectly
    {
        long? GetNextTime(string name = null);
        //void UpdateSchedule(string name = null, long? lastImportDateTime = null);
        void Import(IEnumerable<ExcelSignleRow> records);
        void SaveBatchJobs(BatchJob model);
        Mapping[] GetMappings();
    }
}

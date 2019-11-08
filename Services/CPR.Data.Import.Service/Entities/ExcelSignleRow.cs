

namespace Org.Joey.Common.Models
{    
    using CPR.Data.Import.Models;
    using CPR.Data.Import;
    public class ExcelSignleRow : IEntityWithTimestamp
    {
        public DataSourceNames DataSourceName { get; set; }
        public BatchJob BatchJob { get; set; }
        public long Timestamp { get; }
    }
}

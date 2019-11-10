

namespace Org.Joey.Common.Models
{
    using CPR.Data.Import.Models;
    using CPR.Data.Import;
    using System;
    using Org.Joey.Common;
    public class ExcelSignleRow : IEntityWithTimestamp
    {
        public DataSourceNames DataSourceName { get; set; }
        public BatchJob BatchJob { get; set; }
        public PropertyObject[] Properties { get; set; }
        public DateTime DateTime { get; set; }
        public long Timestamp
        {
            get
            {
                return this.DateTime.ToUnixStampDateTime();
            }
        }
    }
}

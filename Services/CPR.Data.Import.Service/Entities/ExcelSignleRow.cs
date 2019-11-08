using CPR.Data.Import;
using System;
using System.Collections.Generic;
using System.Text;

namespace Org.Joey.Common.Models
{
    public class ExcelSignleRow : IEntityWithTimestamp
    {
        public DataSourceNames DataSourceName { get; set; }        
        public long Timestamp { get; }
    }
}

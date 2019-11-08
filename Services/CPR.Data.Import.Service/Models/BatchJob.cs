using System;
using System.Collections.Generic;
using System.Text;

namespace CPR.Data.Import.Models
{
    public class BatchJob
    {
        public int Id { get; set; }
        public string Agent { get; set; }
        public string FileName { get; set; }
        public string MD5 { get; set; }
        public long CreatedDateTime { get; set; }
    }
}

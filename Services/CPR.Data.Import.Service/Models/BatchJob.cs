using System;
using System.Collections.Generic;
using System.Text;

namespace CPR.Data.Import.Models
{
    public class BatchJob
    {
        public string Id { get; set; }
        public string Agent { get; set; }
        public string Metadata { get; set; }
        public string AttachInfo { get; set; }
        public FileEntity[] FileEntities { get; set; }
        public long CreatedDateTime { get; set; }
    }
    
    public class FileEntity
    {
        public string FileName { get; set; }
        public string MD5 { get; set; }
    }
}

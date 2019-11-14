using System;
using System.Collections.Generic;
using System.Text;

namespace Org.Joey.Common.Models
{
    public class TeamLevelReport
    {
        public string Monthly { get; set; }
        public string Weekly { get; set; }
        public bool IsTotalLine { get; set; }
        public string BatchJob { get; set; }
        public string Metadata { get; set; }
    }
}

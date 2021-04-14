using System;
using System.Collections.Generic;

namespace DALNetCore.Models
{
    public partial class BuildStatistics
    {
        public long Id { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string BuildNumber { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
    }
}

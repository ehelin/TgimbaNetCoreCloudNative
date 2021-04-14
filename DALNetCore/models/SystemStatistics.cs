using System;
using System.Collections.Generic;

namespace DALNetCore.Models
{
    public partial class SystemStatistics
    {
        public long Id { get; set; }
        public bool? WebsiteIsUp { get; set; }
        public bool? DatabaseIsUp { get; set; }
        public bool? AzureFunctionIsUp { get; set; }
        public DateTime? Created { get; set; }
    }
}

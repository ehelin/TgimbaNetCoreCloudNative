using System;
using System.Collections.Generic;

namespace DALNetCore.aurorapostgresqlgeneratedmodels
{
    public partial class Buildstatistic
    {
        public long Id { get; set; }
        public DateTime? Starttime { get; set; }
        public DateTime? Endtime { get; set; }
        public string Buildnumber { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
    }
}

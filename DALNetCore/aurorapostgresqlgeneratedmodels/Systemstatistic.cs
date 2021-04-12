using System;
using System.Collections.Generic;

namespace DALNetCore.aurorapostgresqlgeneratedmodels
{
    public partial class Systemstatistic
    {
        public long Id { get; set; }
        public bool? Websiteisup { get; set; }
        public bool? Databaseisup { get; set; }
        public bool? Azurefunctionisup { get; set; }
        public DateTime? Created { get; set; }
    }
}

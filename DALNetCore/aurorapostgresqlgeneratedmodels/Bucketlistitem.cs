using System;
using System.Collections.Generic;

namespace DALNetCore.aurorapostgresqlgeneratedmodels
{
    public partial class Bucketlistitem
    {
        public Bucketlistitem()
        {
            Bucketlistusers = new HashSet<Bucketlistuser>();
        }

        public long Bucketlistitemid { get; set; }
        public string Listitemname { get; set; }
        public DateTime? Created { get; set; }
        public string Category { get; set; }
        public bool? Achieved { get; set; }
        public int? Categorysortorder { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        public virtual ICollection<Bucketlistuser> Bucketlistusers { get; set; }
    }
}

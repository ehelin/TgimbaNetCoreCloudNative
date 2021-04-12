using System;
using System.Collections.Generic;

namespace DALNetCore.aurorapostgresqlgeneratedmodels
{
    public partial class Bucketlistuser
    {
        public long Bucketlistuserid { get; set; }
        public long? Bucketlistitemid { get; set; }
        public long? Userid { get; set; }

        public virtual Bucketlistitem Bucketlistitem { get; set; }
        public virtual Bucketuser User { get; set; }
    }
}

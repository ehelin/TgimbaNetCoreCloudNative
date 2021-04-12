using System;
using System.Collections.Generic;

namespace DALNetCore.aurorapostgresqlgeneratedmodels
{
    public partial class Bucketuser
    {
        public Bucketuser()
        {
            Bucketlistusers = new HashSet<Bucketlistuser>();
        }

        public long Userid { get; set; }
        public string Username { get; set; }
        public string Salt { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime? Created { get; set; }
        public string Createdby { get; set; }
        public DateTime? Modified { get; set; }
        public string Modifiedby { get; set; }

        public virtual ICollection<Bucketlistuser> Bucketlistusers { get; set; }
    }
}

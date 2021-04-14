using System;
using System.Collections.Generic;

namespace DALNetCore.Models
{
    public partial class BucketListUser
    {
        public long BucketListUserId { get; set; }
        public long? BucketListItemId { get; set; }
        public long? UserId { get; set; }

        public virtual BucketListItem BucketListItem { get; set; }
        public virtual User User { get; set; }
    }
}

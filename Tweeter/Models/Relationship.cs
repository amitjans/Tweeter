using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Tweeter.Models
{
    public partial class Relationship
    {
        public long Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? FollowerId { get; set; }
        public long? FollowedId { get; set; }

        public virtual User Followed { get; set; }
        public virtual User Follower { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Tweeter.Models
{
    public partial class Relationship
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id", Order = 0)]
        public long Id { get; set; }

        [DataType(DataType.DateTime), Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [ForeignKey("Follower"), Column("follower_id")]
        public long? FollowerId { get; set; }

        [ForeignKey("Followed"), Column("followed_id")]
        public long? FollowedId { get; set; }

        public virtual User Followed { get; set; }
        public virtual User Follower { get; set; }
    }
}
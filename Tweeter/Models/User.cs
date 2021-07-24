using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Tweeter.Models
{
    [Table("user")]
    public partial class User
    {
        public User()
        {
            RelationshipFolloweds = new HashSet<Relationship>();
            RelationshipFollowers = new HashSet<Relationship>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id", Order = 0)]
        public long Id { get; set; }

        [Required, Column("username", Order = 1), MaxLength(255)]
        public string Username { get; set; }

        [Required, Column("password", Order = 2), MaxLength(255)]
        public string Password { get; set; }

        [Required, Column("name", Order = 3), MaxLength(255)]
        public string Name { get; set; }

        [Required, Column("lastname", Order = 4), MaxLength(255)]
        public string Lastname { get; set; }

        [Required, EmailAddress, Column("email", Order = 5), MaxLength(255)]
        public string Email { get; set; }

        [DataType(DataType.DateTime), Column("created_at", Order = 6)]
        public DateTime? CreatedAt { get; set; }

        [Column("verified", Order = 7)]
        public bool Verified { get; set; }

        [InverseProperty("Followed")]
        public virtual ICollection<Relationship> RelationshipFolloweds { get; set; }

        [InverseProperty("Follower")]
        public virtual ICollection<Relationship> RelationshipFollowers { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Tweeter.Models
{
    public partial class User
    {
        public User()
        {
            RelationshipFolloweds = new HashSet<Relationship>();
            RelationshipFollowers = new HashSet<Relationship>();
        }

        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool Verified { get; set; }

        public virtual ICollection<Relationship> RelationshipFolloweds { get; set; }
        public virtual ICollection<Relationship> RelationshipFollowers { get; set; }
    }
}

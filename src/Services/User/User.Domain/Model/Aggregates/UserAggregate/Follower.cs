using SocialNetworkingKata.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace User.Domain.Model.Aggregates.UserAggregate
{
    public class Follower : EntityBase
    {
        public DateTime DateFrom { get; set; }

        public int UserId { get; set; }
        public User FollowerUser { get; set; }
        public int FollowedId { get; set; }
        public User FollowedUser { get; set; }

    }
}

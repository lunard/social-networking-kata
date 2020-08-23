using SocialNetworkingKata.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace User.Domain.Model.Aggregates.UserAggregate
{
    public class Message: EntityBase
    {
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}

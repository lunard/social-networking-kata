using SocialNetworkingKata.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace User.Domain.Model.Aggregates.UserAggregate
{
    public class User: EntityBase, IAggregateRoot
    {
        private IReadOnlyCollection<Message> _messages;
        private IReadOnlyCollection<Follower> _followers;
        private IReadOnlyCollection<Follower> _followedList;

        public string Name { get; private set; }

        public IReadOnlyCollection<Message> Messages => _messages;
        public IReadOnlyCollection<Follower> Followers => _followers;
        public IReadOnlyCollection<Follower> FollowedList => _followedList;

        public User()
        {
            _messages = new List<Message>();
            _followers = new List<Follower>();
            _followedList = new List<Follower>();
        }

        public User(string name)
        {
            Name = name;
        }
    }
}

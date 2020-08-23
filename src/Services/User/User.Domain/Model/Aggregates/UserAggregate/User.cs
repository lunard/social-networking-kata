using SocialNetworkingKata.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace User.Domain.Model.Aggregates.UserAggregate
{
    public class User : EntityBase, IAggregateRoot
    {
        private ICollection<Message> _messages;
        private ICollection<Follower> _followers;
        private ICollection<Follower> _followedList;

        public string Name { get; private set; }

        public ICollection<Message> Messages => _messages;
        public ICollection<Follower> Followers => _followers;
        public ICollection<Follower> FollowedList => _followedList;

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

        public bool AddMessage(Message m)
        {
            try
            {
                _messages.Add(m);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddFollowed(Follower follower)
        {
            try
            {
                if (!_followedList.Any(f => f.FollowedId == follower.FollowedId && f.FollowerId == follower.FollowerId))
                    _followedList.Add(follower);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

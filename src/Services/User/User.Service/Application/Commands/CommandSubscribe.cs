using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using System.Text;

namespace User.Service.Application.Commands
{
    [DataContract]
    public class CommandSubscribe : CommandBase
    {
        private string _followedUserName;
        public String FollowedUserName => _followedUserName;

        public CommandSubscribe(string followedUserName, string userName) : base(userName)
        {
            _followedUserName = userName == _followedUserName ? throw new DuplicateNameException($"User '{followedUserName}' already followed by '{userName}'") : followedUserName;
        }
    }
}

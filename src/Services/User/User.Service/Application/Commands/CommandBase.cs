using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace User.Service.Application.Commands
{
    [DataContract]
    public class CommandBase
    {
        private string _userName;

        [DataMember]
        public string UserName => _userName;

        public CommandBase(string userName)
        {
            _userName = String.IsNullOrWhiteSpace(userName) ? throw new NotImplementedException("User name") : userName;
        }
    }
}

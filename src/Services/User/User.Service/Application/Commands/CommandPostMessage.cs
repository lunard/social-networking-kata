using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace User.Service.Application.Commands
{
    [DataContract]
    public class CommandPostMessage: CommandBase
    {
        private string _content;
        public String Content => _content;

        public CommandPostMessage(string userName, string content): base (userName)
        {
            _content = !ValidateContent(content) ? throw new InvalidOperationException("Message content is not valid") : content;
        }

        public bool ValidateContent(string content)
        {
            return true;
        }
    }
}

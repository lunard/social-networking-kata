﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace User.Service.Application.Commands
{
    [DataContract]
    public class CommandAddMessage: CommandBase
    {
        private string _content;
        public String Content => _content;

        public CommandAddMessage(string content, string userName): base (userName)
        {
            _content = !ValidateContent(content) ? throw new InvalidOperationException("Message content is not valid") : content;
        }

        public bool ValidateContent(string content)
        {
            return true;
        }
    }
}
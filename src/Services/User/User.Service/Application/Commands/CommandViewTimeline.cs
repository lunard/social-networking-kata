using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace User.Service.Application.Commands
{
    [DataContract]
    public class CommandViewTimeline : CommandBase
    {
        public CommandViewTimeline(string userName) : base(userName)
        {
        }
    }
}

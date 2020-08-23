using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.Service.Application.Commands;

namespace User.Service
{
    public interface IUserService
    {
        Task<bool> Post(CommandPostMessage command);

        Task<bool> Subscribe(CommandSubscribe command);
    }
}

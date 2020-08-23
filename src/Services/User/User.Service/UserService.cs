using Microsoft.EntityFrameworkCore;
using SocialNetworkingKata.Infrastructure.Data.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using User.Domain.Model.Aggregates.UserAggregate;
using User.Service.Application.Commands;
using UserModel = User.Domain.Model.Aggregates.UserAggregate.User;
namespace User.Service
{
    public class UserService : IUserService
    {
        // What is missing:
        // Logging ... Serilog with its Skins !!

        // Use Dependency Injection (DI)
        private IAsyncRepository<UserModel> _userRepository;
        public UserService(
            IAsyncRepository<UserModel> userRepository)
        {
            _userRepository = userRepository == null ? throw new NotImplementedException() : userRepository;
        }

        public async Task<bool> Post(CommandPostMessage command)
        {
            var user = (await _userRepository.FindAsync(u => u.Name == command.UserName, include: o => o.Include(u => u.Messages))).FirstOrDefault();

            if (user == null)
                return false;

            var message = new Message()
            {
                UserId = user.Id,
                Date = DateTime.Now,
                Content = command.Content
            };

            var result = user.AddMessage(message);

            if (result)
            {
                await _userRepository.UpdateAsync(user);
                result = await _userRepository.UnitOfWork.SaveChangesAsync();
            }

            return result;
        }
    }
}

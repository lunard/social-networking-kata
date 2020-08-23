using Microsoft.EntityFrameworkCore;
using SocialNetworkingKata.Infrastructure.Data.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using User.Domain.Model.Aggregates.UserAggregate;
using User.Service.Application.Commands;
using UserModel = User.Domain.Model.Aggregates.UserAggregate.User;
using FollowerModel = User.Domain.Model.Aggregates.UserAggregate.Follower;
using System.Collections.Generic;
using User.Service.Application.ViewModel;

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

        public async Task<bool> Subscribe(CommandSubscribe command)
        {
            var followerUser = (await _userRepository.FindAsync(u => u.Name == command.UserName, include: o => o.Include(u => u.FollowedList))).FirstOrDefault();
            var followedUser = (await _userRepository.FindAsync(u => u.Name == command.FollowedUserName, include: o => o.Include(u => u.Followers))).FirstOrDefault();

            if (followedUser == null || followerUser == null)
                return false;

            var follower = new FollowerModel()
            {
                DateFrom = DateTime.Now,
                FollowerId = followerUser.Id,
                FollowedId = followedUser.Id
            };

            var result = followedUser.AddFollowed(follower);

            if (result)
            {
                await _userRepository.UpdateAsync(followedUser);
                result = await _userRepository.UnitOfWork.SaveChangesAsync();
            }

            return true;
        }

        public async Task<IEnumerable<MessageViewModel>> ViewTimeline(CommandViewTimeline command)
        {
            var result = new List<MessageViewModel>();

            var user = (await _userRepository.FindAsync(u => u.Name == command.UserName, include: o => o.Include(u => u.Messages))).FirstOrDefault();

            if (user == null)
                throw new Exception($"User {command.UserName} not found!");

            // here would be better to use AutoMapper
            foreach (var message in user.Messages)
            {
                MessageViewModel m = new MessageViewModel()
                {
                    Content = message.Content,
                    UserName = command.UserName,
                    Date = message.Date
                };

                result.Add(m);
            }

            return result;
        }
    }
}

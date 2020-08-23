using Microsoft.Extensions.Configuration;
using SocialNetworkingKata.Infrastructure.Data.Implementations.EntityFrameworkCore;
using SocialNetworkingKata.Infrastructure.Helpers;
using System;
using System.Reflection;
using UserModel = User.Domain.Model.Aggregates.UserAggregate.User;
using User.Infrastructure.Data;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using User.Service.Application.Commands;
using User.Service;
using SocialNetworkingKata.Infrastructure.Data.Interfaces;

namespace SocialNetworkingKata.UnitTests.Services.Infrastructure
{
    public class User_Service_Subscribe
    {
        UserService userService;
        IAsyncRepository<UserModel> userRepository;

        public User_Service_Subscribe()
        {
            IConfiguration configuration = ConfigurationHelper.BuildConfiguration(AppDomain.CurrentDomain.BaseDirectory);

            var uof = new UnitOfWork(configuration);
            userRepository = new EfRepository<UserModel>(uof);

            userService = new UserService(userRepository);
        }

        [Fact]
        public async void Post_Subscribe()
        {
            var command = new CommandSubscribe("Alice", "Bob");
           
            var commandResult = await userService.Subscribe(command);

            Assert.True(commandResult);
            var aliceUser = (await userRepository.FindAsync(predicate: u => u.Name == "Alice", include: u => u.Include(u => u.FollowedList))).FirstOrDefault();
            var bobUser = (await userRepository.FindAsync(predicate: u => u.Name == "Bob")).FirstOrDefault();

            Assert.NotNull(aliceUser);
            Assert.NotNull(bobUser);

            Assert.Contains(bobUser.Id, aliceUser.FollowedList.Select(f => f.FollowedId));
        }
    }
}

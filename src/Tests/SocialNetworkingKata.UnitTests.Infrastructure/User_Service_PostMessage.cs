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
    public class User_Service_PostMessage
    {
        UserService userService;
        IAsyncRepository<UserModel> userRepository;

        public User_Service_PostMessage()
        {
            IConfiguration configuration = ConfigurationHelper.BuildConfiguration(AppDomain.CurrentDomain.BaseDirectory);

            var uof = new UnitOfWork(configuration);
            userRepository = new EfRepository<UserModel>(uof);

            userService = new UserService(userRepository);
        }

        [Fact]
        public async void Post_Test()
        {
            var message = "Hei, this is a new posted message";
            var command = new CommandPostMessage("Alice", message);
           
            var commandResult = await userService.Post(command);

            Assert.True(commandResult);
            var aliceUser = (await userRepository.FindAsync(predicate: o => o.Name == "Alice", include: o => o.Include(u => u.Messages))).FirstOrDefault();
            Assert.NotNull(aliceUser);
            Assert.Contains(message, aliceUser.Messages.Select(m => m.Content));
        }
    }
}

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

namespace SocialNetworkingKata.UnitTests.Services.Infrastructure
{
    public class User_Service_PostMessage
    {
        UserService userService;
        public User_Service_PostMessage()
        {
            IConfiguration configuration = ConfigurationHelper.BuildConfiguration(AppDomain.CurrentDomain.BaseDirectory);

            var uof = new UnitOfWork(configuration);
            var userRepository = new EfRepository<UserModel>(uof);

            userService = new UserService(userRepository);
        }

        [Fact]
        public async void Post_Test()
        {
            var command = new CommandPostMessage("Alice", "Hei, this is a new posted message");
           
            var commandResult = await userService.Post(command);

            Assert.True(commandResult);
        }
    }
}

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
    public class User_Service_ViewTimeline
    {
        UserService userService;
        IAsyncRepository<UserModel> userRepository;

        public User_Service_ViewTimeline()
        {
            IConfiguration configuration = ConfigurationHelper.BuildConfiguration(AppDomain.CurrentDomain.BaseDirectory);

            var uof = new UnitOfWork(configuration);
            userRepository = new EfRepository<UserModel>(uof);

            userService = new UserService(userRepository);
        }

        [Fact]
        public async void ViewTimeline_Test()
        {
            var command = new CommandViewTimeline("Alice");
           
            var commandResult = await userService.ViewTimeline(command);
        }
    }
}

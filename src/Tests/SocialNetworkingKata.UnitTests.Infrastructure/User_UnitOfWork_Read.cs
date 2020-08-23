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

namespace SocialNetworkingKata.UnitTests.Services.Infrastructure
{
    public class User_UnitOfWork_Read
    {
        EfRepository<UserModel> userRepository;
        public User_UnitOfWork_Read()
        {
            IConfiguration configuration = ConfigurationHelper.BuildConfiguration(AppDomain.CurrentDomain.BaseDirectory);

            var uof = new UnitOfWork(configuration);
            userRepository = new EfRepository<UserModel>(uof);
        }

        [Fact]
        public async void ReadOnlyUsers_Test()
        {
            var users = await userRepository.ListAllAsync();
            Assert.NotEmpty(users);
        }

        [Fact]
        public async void ReadUsersAndMessages_Test()
        {
            var aliceUser = (await userRepository.FindAsync(predicate: o => o.Name == "Alice", include: o => o.Include(u => u.Messages))).FirstOrDefault();
            Assert.NotNull(aliceUser);
            Assert.Contains("Hello, I'm Alice!", aliceUser.Messages.Select(m => m.Content));
        }
    }
}

using Microsoft.Extensions.Configuration;
using SocialNetworkingKata.Infrastructure.Data.Implementations.EntityFrameworkCore;
using SocialNetworkingKata.Infrastructure.Data.Interfaces;
using SocialNetworkingKata.Infrastructure.Helpers;
using System;
using System.Data;
using System.Threading.Tasks;
using User.Infrastructure.Data;
using User.Service;
using UserModel = User.Domain.Model.Aggregates.UserAggregate.User;

namespace SocialNetworkingKata
{
    //I focused not on the ConsoleApplication itself, but on the Services/Infrastructure/Database part of the architecture
    //I followed the DDD design, to implement a Clean Architecture

    // What is missing (in this class, lots :-p ):
    // Generic Host: would be really better to implement the console application with a GenericHost, using the HostBuilder
    // Logging ... Serilog with its Skins !!
    // Dependency Injection using Autofac

    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = ConfigurationHelper.BuildConfiguration(AppDomain.CurrentDomain.BaseDirectory);
            var uof = new UnitOfWork(configuration);
            var userRepository = new EfRepository<UserModel>(uof);

            var userService = new UserService(userRepository);

            Task.Run(async () =>
            {
                while (true)
                {
                    var line = Console.ReadLine();

                    if (line.Contains("->"))
                    {
                        var arguments = line.Split(new String[] { "->" }, StringSplitOptions.RemoveEmptyEntries);
                        var userName = arguments[0].Trim();
                        var message = arguments[1].Trim();

                        var command = new User.Service.Application.Commands.CommandPostMessage(userName, message);

                        await userService.Post(command);
                    }
                    else if (line.Contains("follows"))
                    {
                        var arguments = line.Split(new String[] { "follows" }, StringSplitOptions.RemoveEmptyEntries);
                        var followerUserName = arguments[0].Trim();
                        var followedUserName = arguments[1].Trim();

                        var command = new User.Service.Application.Commands.CommandSubscribe(followerUserName, followedUserName);

                        await userService.Subscribe(command);

                        var viewTimelineCommand = new User.Service.Application.Commands.CommandViewTimeline(followerUserName);
                        var messages = await userService.ViewTimeline(viewTimelineCommand);
                        foreach (var message in messages)
                        {
                            Console.WriteLine(message);
                        }
                    }
                    else
                    {
                        var userName = line.Trim();

                        var command = new User.Service.Application.Commands.CommandViewTimeline(userName);

                        var messages = await userService.ViewTimeline(command);
                        foreach (var message in messages)
                        {
                            Console.WriteLine(message);
                        }
                    }
                }
            }).Wait();

        }
    }
}

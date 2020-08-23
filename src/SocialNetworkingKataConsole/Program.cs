using System;

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
            Console.WriteLine("Hello World!");
        }
    }
}

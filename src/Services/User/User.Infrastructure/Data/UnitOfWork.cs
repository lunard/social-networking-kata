using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SocialNetworkingKata.Infrastructure.Data.Implementations.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using User.Infrastructure.Data.EfEntityConfigurations;
using UserModel = User.Domain.Model.Aggregates.UserAggregate.User;
using MessageModel = User.Domain.Model.Aggregates.UserAggregate.Message;
using FollowerModel = User.Domain.Model.Aggregates.UserAggregate.Follower;

namespace User.Infrastructure.Data
{
    public class UnitOfWork : EfUnitOfWork
    {
        public UnitOfWork(IConfiguration configuration) : base(configuration) { 
        }
        
        public override DbContext CreateDbContext()
        {
            return new UserDbContext(_configuration.GetValue<string>("ConnectionString"));
        }

        private class UserDbContext : DbContext
        {
            private string _connectionString;
            public UserDbContext(string connextionString): base()
            {
                _connectionString = String.IsNullOrWhiteSpace(connextionString) ? throw new NotImplementedException("SQL Server DbContext connection string") : connextionString;
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(_connectionString);

                base.OnConfiguring(optionsBuilder);
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.ApplyConfiguration<UserModel>(new UserEntityTypeConfiguration());
                modelBuilder.ApplyConfiguration<MessageModel>(new MessageEntityTypeConfiguration());
                modelBuilder.ApplyConfiguration<FollowerModel>(new FollowerEntityTypeConfiguration());
            }
        }
    }
}

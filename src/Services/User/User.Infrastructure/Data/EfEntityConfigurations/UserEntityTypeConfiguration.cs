using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

using UserModel = User.Domain.Model.Aggregates.UserAggregate.User;

namespace User.Infrastructure.Data.EfEntityConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.ToTable("User", "User");
            builder.HasKey(o => o.Id);
            builder.Property<int>("Id").IsRequired();

            builder.Property<string>("Name").IsRequired().HasMaxLength(100);

            // User's messages
            builder
                .Metadata
                .FindNavigation(nameof(UserModel.Messages))
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder
                .HasMany(u => u.Messages)
                .WithOne(m => m.User)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(m => m.UserId);

            // User's followers

            builder
                .HasMany(u => u.Followers)
                .WithOne(f => f.FollowedUser)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(f => f.FollowedId);

            builder
               .HasMany(u => u.FollowedList)
               .WithOne(f => f.FollowerUser)
               .OnDelete(DeleteBehavior.Cascade)
               .HasForeignKey(f => f.FollowerId);

            builder
              .Metadata
              .FindNavigation(nameof(UserModel.Followers))
              .SetPropertyAccessMode(PropertyAccessMode.Property);

            builder
               .Metadata
               .FindNavigation(nameof(UserModel.FollowedList))
               .SetPropertyAccessMode(PropertyAccessMode.Property);

        }

    }
}

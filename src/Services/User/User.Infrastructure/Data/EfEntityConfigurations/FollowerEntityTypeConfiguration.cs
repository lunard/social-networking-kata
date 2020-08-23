using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

using FollowerModel = User.Domain.Model.Aggregates.UserAggregate.Follower;

namespace User.Infrastructure.Data.EfEntityConfigurations
{
    public class FollowerEntityTypeConfiguration : IEntityTypeConfiguration<FollowerModel>
    {
        public void Configure(EntityTypeBuilder<FollowerModel> builder)
        {
            builder.ToTable("Follower", "User");
            builder.HasKey(o => o.Id);
            builder.Property<int>("Id").IsRequired();

            builder.Property<int>("FollowedId").IsRequired();
            builder.Property<int>("FollowerId").IsRequired();
        }

    }
}

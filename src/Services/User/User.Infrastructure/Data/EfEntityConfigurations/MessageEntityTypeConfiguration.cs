using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

using MessageModel = User.Domain.Model.Aggregates.UserAggregate.Message;

namespace User.Infrastructure.Data.EfEntityConfigurations
{
    public class MessageEntityTypeConfiguration : IEntityTypeConfiguration<MessageModel>
    {
        public void Configure(EntityTypeBuilder<MessageModel> builder)
        {
            builder.ToTable("Message", "User");
            builder.HasKey(o => o.Id);
            builder.Property<int>("Id").IsRequired();

            builder.Property<int>("UserId").IsRequired();
        }

    }
}

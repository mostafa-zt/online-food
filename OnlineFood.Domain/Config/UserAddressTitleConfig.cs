using OnlineFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Config
{
    public class UserAddressTitleConfig : IEntityTypeConfiguration<UserAddressTitle>
    {
        public void Configure(EntityTypeBuilder<UserAddressTitle> builder)
        {
            builder.ToTable(name: "UserAddressTitles", schema: "User");
            builder.Property(x => x.Title).IsRequired().HasMaxLength(25);
        }
    }
}

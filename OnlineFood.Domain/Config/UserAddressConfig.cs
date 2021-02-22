using OnlineFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Config
{
    public class UserAddressConfig : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            builder.ToTable(name: "UserAddresses", schema: "User");
            builder.Property(x => x.PhoneNumber).HasMaxLength(25);

            builder.HasOne(x => x.UserAddressTitle).WithMany(x => x.UserAddresses).HasForeignKey(x => x.UserAddressTitleId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.CityArea).WithMany(x => x.UserAddresses).HasForeignKey(x => x.CityAreaId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Config
{
    public class SellerConfig : IEntityTypeConfiguration<Seller>
    {
        public void Configure(EntityTypeBuilder<Seller> builder)
        {
            builder.ToTable(name: "Sellers", schema: "Seller");

            builder.Property(x => x.PhoneNumber).HasMaxLength(25);
            builder.Property(x => x.FullAddress).HasMaxLength(250);
            builder.Property(x => x.ZipCode).HasMaxLength(25);

            builder.HasOne(x => x.City).WithMany().HasForeignKey(x => x.CityId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

using OnlineFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Config
{
    public class RestaurantImageConfig : IEntityTypeConfiguration<RestaurantImage>
    {
        public void Configure(EntityTypeBuilder<RestaurantImage> builder)
        {
            builder.ToTable(name: "RestaurantImages", schema: "Restaurant");
            builder.Property(x => x.ImageExtension).HasMaxLength(5);
            builder.Property(x => x.ImageFileName).HasMaxLength(250);
            builder.Property(x => x.ImageUrl).HasMaxLength(300);

            builder.HasOne(x => x.Restaurant).WithMany(x => x.RestaurantImages).HasForeignKey(x => x.RestaurantId);
        }
    }
}

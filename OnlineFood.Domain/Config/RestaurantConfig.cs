using OnlineFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Config
{
    public class RestaurantConfig : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.ToTable(name: "Restaurants", schema: "Restaurant");
            builder.Property(x => x.Title).HasMaxLength(25);
            builder.Property(x => x.FullAddress).HasMaxLength(250);
            builder.Property(x => x.LogoUrl).HasMaxLength(300);

            builder.Property(x => x.RestaurantCourierCost).HasColumnType("decimal(18, 2)");

            builder.HasOne(x => x.RestaurantLevelEconomy).WithMany(x => x.Restaurants).HasForeignKey(x => x.RestaurantLevelEconomyId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}

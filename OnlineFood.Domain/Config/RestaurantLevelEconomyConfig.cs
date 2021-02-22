using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Config
{
    public class RestaurantLevelEconomyConfig : IEntityTypeConfiguration<RestaurantLevelEconomy>
    {
        public void Configure(EntityTypeBuilder<RestaurantLevelEconomy> builder)
        {
            builder.ToTable(name: "RestaurantLevelEconomies", schema: "Restaurant");
            builder.Property(x => x.Title).HasMaxLength(25);

            builder.HasData(
                    new RestaurantLevelEconomy() { CreatorDateTime = DateTime.Now, Title = "Luxury", ActivityStatus = Enum.ActivityStatus.Available, Id = 1 },
                    new RestaurantLevelEconomy() { CreatorDateTime = DateTime.Now, Title = "Economic", ActivityStatus = Enum.ActivityStatus.Available, Id = 2 });
        }
    }
}

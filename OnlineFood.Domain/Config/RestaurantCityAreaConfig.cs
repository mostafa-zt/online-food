using OnlineFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Config
{
    public class RestaurantCityAreaConfig : IEntityTypeConfiguration<RestaurantCityArea>
    {
        public void Configure(EntityTypeBuilder<RestaurantCityArea> builder)
        {
            builder.ToTable(name: "RestaurantCityAreas", schema: "Restaurant");

            builder.HasOne(x => x.Restaurant).WithMany(x => x.RestaurantCityAreas).HasForeignKey(x => x.RestaurantId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.CityArea).WithMany(x => x.RestaurantCityAreas).HasForeignKey(x => x.CityAreaId);
            //builder.HasOne(x => x.City).WithMany().HasForeignKey(x => x.CityId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

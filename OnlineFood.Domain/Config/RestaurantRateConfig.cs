using OnlineFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Config
{
    public class RestaurantRateConfig : IEntityTypeConfiguration<RestaurantRate>
    {
        public void Configure(EntityTypeBuilder<RestaurantRate> builder)
        {
            builder.ToTable(name: "RestaurantRates", schema: "Restaurant");

            builder.HasOne(x => x.Restaurant).WithMany(x => x.RestaurantRates).HasForeignKey(x => x.RestaurantId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

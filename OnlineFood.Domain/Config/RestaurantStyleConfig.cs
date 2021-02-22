using OnlineFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Config
{
    public class RestaurantStyleConfig : IEntityTypeConfiguration<RestaurantStyle>
    {
        public void Configure(EntityTypeBuilder<RestaurantStyle> builder)
        {
            builder.ToTable(name: "RestaurantStyles", schema: "Restaurant");

            builder.HasOne(x => x.Restaurant).WithMany(x => x.RestaurantStyles).HasForeignKey(x => x.RestaurantId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.RestaurantType).WithMany(x => x.RestaurantStyles).HasForeignKey(x => x.RestaurantTypeId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

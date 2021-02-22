using OnlineFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Config
{
    public class RestaurantMenuConfig : IEntityTypeConfiguration<RestaurantMenu>
    {
        public void Configure(EntityTypeBuilder<RestaurantMenu> builder)
        {
            builder.ToTable(name: "RestaurantMenus", schema: "Restaurant");
            
            builder.HasOne(x => x.Restaurant).WithMany(x => x.RestaurantMenus).HasForeignKey(x => x.RestaurantId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.RestaurantFoodCategory).WithMany(x => x.RestaurantMenus).HasForeignKey(x => x.RestaurantFoodCategoryId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

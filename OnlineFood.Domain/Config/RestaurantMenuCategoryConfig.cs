using OnlineFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Config
{
    public class RestaurantMenuCategoryConfig : IEntityTypeConfiguration<RestaurantMenuCategory>
    {
        public void Configure(EntityTypeBuilder<RestaurantMenuCategory> builder)
        {
            builder.ToTable(name: "RestaurantMenuCategories", schema: "Restaurant");
            builder.Property(x => x.Title).HasMaxLength(25);
            builder.Property(x => x.Description).HasMaxLength(250);

            builder.HasOne(x => x.RestaurantMenu).WithMany(x => x.RestaurantMenuCategories).HasForeignKey(x => x.RestaurantMenuId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
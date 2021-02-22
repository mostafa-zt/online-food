using OnlineFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Config
{
    public class RestaurantMenuDetailConfig : IEntityTypeConfiguration<RestaurantMenuDetail>
    {
        public void Configure(EntityTypeBuilder<RestaurantMenuDetail> builder)
        {
            builder.ToTable(name: "RestaurantMenuDetails", schema: "Restaurant");
            builder.Property(x => x.Title).IsRequired().HasMaxLength(25);
            builder.Property(x => x.Description).HasMaxLength(250);

            builder.HasOne(x => x.RestaurantMenuCategory).WithMany(x => x.RestaurantMenuDetails).HasForeignKey(x => x.RestaurantMenuCategoryId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Config
{
    public class UserRestaurantMenuFavoriteConfig : IEntityTypeConfiguration<UserRestaurantMenuFavorite>
    {
        public void Configure(EntityTypeBuilder<UserRestaurantMenuFavorite> builder)
        {
            builder.ToTable(name: "UserRestaurantMenuFavorites", schema: "User");

            builder.HasOne(x => x.RestaurantMenu).WithMany().HasForeignKey(x => x.RestaurantMenuId);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Config
{
    public class UserRestaurantFavoriteConfig : IEntityTypeConfiguration<UserRestaurantFavorite>
    {
        public void Configure(EntityTypeBuilder<UserRestaurantFavorite> builder)
        {
            builder.ToTable(name: "UserRestaurantFavorites", schema: "User");

            builder.HasOne(x => x.Restaurant).WithMany().HasForeignKey(x => x.RestaurantId);
        }
    }
}

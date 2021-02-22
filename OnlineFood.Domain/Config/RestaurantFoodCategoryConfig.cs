using OnlineFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Config
{
   public class RestaurantFoodCategoryConfig  : IEntityTypeConfiguration<RestaurantFoodCategory>
    {
        public void Configure(EntityTypeBuilder<RestaurantFoodCategory> builder)
        {
            builder.ToTable(name: "RestaurantFoodCategories", schema: "Restaurant");
            builder.Property(x => x.Title).HasMaxLength(25);
            //builder.Property(x => x.IconClassName).HasMaxLength(25);
            //builder.HasOne(x => x.RestaurantCategory).WithMany(x => x.RestaurantFoodCategories).HasForeignKey(x => x.RestaurantCategoryId).OnDelete(DeleteBehavior.Restrict);

            builder.HasData(new List<RestaurantFoodCategory>() {
                              { new RestaurantFoodCategory() { Title = "Burger"  , ActivityStatus = Enum.ActivityStatus.Available , CreatorDateTime = DateTime.Now , Id =1  } },
                              { new RestaurantFoodCategory() { Title = "FastFood", ActivityStatus = Enum.ActivityStatus.Available , CreatorDateTime = DateTime.Now , Id=2  } },
                              { new RestaurantFoodCategory() { Title = "Drink", ActivityStatus = Enum.ActivityStatus.Available , CreatorDateTime = DateTime.Now , Id=3  } },
                              { new RestaurantFoodCategory() { Title = "Cupcake",  ActivityStatus = Enum.ActivityStatus.Available , CreatorDateTime = DateTime.Now , Id=4 } },
                              { new RestaurantFoodCategory() { Title = "Sandwich", ActivityStatus = Enum.ActivityStatus.Available , CreatorDateTime = DateTime.Now , Id=5  } },
                              { new RestaurantFoodCategory() { Title = "Desert", ActivityStatus = Enum.ActivityStatus.Available , CreatorDateTime = DateTime.Now , Id=6  } },
                              { new RestaurantFoodCategory() { Title = "Salad", ActivityStatus = Enum.ActivityStatus.Available , CreatorDateTime = DateTime.Now , Id=7  } },
                              { new RestaurantFoodCategory() { Title = "Pizza", ActivityStatus = Enum.ActivityStatus.Available , CreatorDateTime = DateTime.Now , Id=8  } },
                              { new RestaurantFoodCategory() { Title = "Psta",  ActivityStatus = Enum.ActivityStatus.Available , CreatorDateTime = DateTime.Now , Id=9  } },
                              { new RestaurantFoodCategory() { Title = "Ice Cream",  ActivityStatus = Enum.ActivityStatus.Available , CreatorDateTime = DateTime.Now , Id=10  } },
                              { new RestaurantFoodCategory() { Title = "Soup", ActivityStatus = Enum.ActivityStatus.Available , CreatorDateTime = DateTime.Now , Id=11  } },
                              { new RestaurantFoodCategory() { Title = "Seafood",ActivityStatus = Enum.ActivityStatus.Available , CreatorDateTime = DateTime.Now , Id=12  } }
                           });

        }
    }
}

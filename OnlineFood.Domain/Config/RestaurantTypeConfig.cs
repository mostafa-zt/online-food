using OnlineFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Config
{
    public class RestaurantTypeConfig : IEntityTypeConfiguration<RestaurantType>
    {
        public void Configure(EntityTypeBuilder<RestaurantType> builder)
        {
            builder.ToTable(name: "RestaurantTypes", schema: "Restaurant");
            builder.Property(x => x.Title).IsRequired().HasMaxLength(25);

            builder.HasData(new List<RestaurantType>() {
                              { new RestaurantType() { Title = "Fastfood"  , ActivityStatus = Enum.ActivityStatus.Available , CreatorDateTime = DateTime.Now , Id =1  } },
                              { new RestaurantType() { Title = "Italian"  , ActivityStatus = Enum.ActivityStatus.Available , CreatorDateTime = DateTime.Now , Id =4  } },
                              { new RestaurantType() { Title = "Cafe"  , ActivityStatus = Enum.ActivityStatus.Available , CreatorDateTime = DateTime.Now , Id =5  } },
                              { new RestaurantType() { Title = "Seafood"  , ActivityStatus = Enum.ActivityStatus.Available , CreatorDateTime = DateTime.Now , Id =6  } },
                              { new RestaurantType() { Title = "Breakfast"  , ActivityStatus = Enum.ActivityStatus.Available , CreatorDateTime = DateTime.Now , Id =7  } },
                              { new RestaurantType() { Title = "Turkish"  , ActivityStatus = Enum.ActivityStatus.Available , CreatorDateTime = DateTime.Now , Id =8  } },
                              { new RestaurantType() { Title = "Chinese"  , ActivityStatus = Enum.ActivityStatus.Available , CreatorDateTime = DateTime.Now , Id =9  } },
                              { new RestaurantType() { Title = "Japanese"  , ActivityStatus = Enum.ActivityStatus.Available , CreatorDateTime = DateTime.Now , Id =10  } }
                           });
        }
    }
}

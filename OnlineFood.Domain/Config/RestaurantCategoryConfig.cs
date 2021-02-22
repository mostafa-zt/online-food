using OnlineFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Config
{
    public class RestaurantCategoryConfig : IEntityTypeConfiguration<RestaurantCategory>
    {
        public const string Rstaurant = "Restaurant";
        public const string CofeShop = "CofeShop";
        public const string Confectionary = "Confectionary";
        public const string SuperMarket = "SuperMarket";
        public const string Bakery = "Bakery";

        public void Configure(EntityTypeBuilder<RestaurantCategory> builder)
        {
            builder.ToTable(name: "RestaurantCategories", schema: "Restaurant");
            builder.Property(x => x.Title).HasMaxLength(25);
            builder.Property(x => x.TitleEng).HasMaxLength(25);

            //builder.HasData(new List<RestaurantCategory>() {
            //                  { new RestaurantCategory() { Title = "رستوران", TitleEng = Rstaurant  , ActivityStatus = Enum.ActivityStatus.Available , CreatorDateTime = DateTime.Now , Id =1  } },
            //                  { new RestaurantCategory() { Title = "کافی شاپ", TitleEng = CofeShop  , ActivityStatus = Enum.ActivityStatus.Available , CreatorDateTime = DateTime.Now , Id=2  } },
            //                  { new RestaurantCategory() { Title = "شیرینی فروشی", TitleEng = Confectionary  , ActivityStatus = Enum.ActivityStatus.Available , CreatorDateTime = DateTime.Now , Id=3  } },
            //                  { new RestaurantCategory() { Title = "سوپرمارکت", TitleEng = SuperMarket  , ActivityStatus = Enum.ActivityStatus.Available , CreatorDateTime = DateTime.Now , Id=4 } },
            //                  { new RestaurantCategory() { Title = "نانوایی", TitleEng = Bakery  , ActivityStatus = Enum.ActivityStatus.UnAvailable , CreatorDateTime = DateTime.Now , Id=5 } }
            //               });
        }
    }

}

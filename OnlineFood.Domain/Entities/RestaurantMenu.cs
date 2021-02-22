using OnlineFood.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Entities
{
    public class RestaurantMenu : BaseEntity
    {
        public RestaurantFoodCategory RestaurantFoodCategory { get; set; }
        public int RestaurantFoodCategoryId { get; set; }

        public Restaurant Restaurant { get; set; }
        public int RestaurantId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Entities
{
    public class UserRestaurantMenuFavorite : BaseEntity
    {
        public RestaurantMenu RestaurantMenu { get; set; }
        public int RestaurantMenuId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Entities
{
    public class UserRestaurantFavorite : BaseEntity
    {
        public Restaurant Restaurant { get; set; }
        public int  RestaurantId { get; set; }
    }
}

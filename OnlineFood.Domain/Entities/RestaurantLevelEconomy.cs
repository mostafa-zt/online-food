using OnlineFood.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Entities
{
    /// <summary>
    /// سطح اقتصادی رستوران متوسط/بالا
    /// </summary>
    public class RestaurantLevelEconomy :BaseEntity
    {
        /// <summary>
        /// عنوان
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// فعال است؟
        /// </summary>
        public ActivityStatus ActivityStatus { get; set; }

        public ICollection<Restaurant> Restaurants { get; set; }
    }
}

using OnlineFood.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Entities
{
    /// <summary>
    /// گروه بندی غذا کباب/خوراک/ایرانی/خورشت/ساندویچ
    /// </summary>
    public class RestaurantFoodCategory : BaseEntity
    {
        public string Title { get; set; }

        public decimal? Price { get; set; }

        public string Description { get; set; }

        public ActivityStatus ActivityStatus { get; set; }

        public virtual RestaurantImage RestaurantImage { get; set; }
        public int? RestaurantImageId { get; set; }

        public ICollection<RestaurantMenu> RestaurantMenus { get; set; }
    }
}

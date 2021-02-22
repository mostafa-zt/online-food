using OnlineFood.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Entities
{
    /// <summary>
    /// رستوران
    /// </summary>
    public class Restaurant : BaseEntity
    {
        public string Title { get; set; }
        public string FullAddress { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public RestaurantLevelEconomy RestaurantLevelEconomy { get; set; }
        public int? RestaurantLevelEconomyId { get; set; }

        public TimeSpan? FromDeliveryTime { get; set; }
        public TimeSpan? ToDeliveryTime { get; set; }

        public decimal? RestaurantCourierCost { get; set; }

        public RestaurantActivityStatus RestaurantActivityStatus { get; set; }

        public string LogoUrl { get; set; }

        public ICollection<RestaurantMenu> RestaurantMenus { get; set; }

        public ICollection<RestaurantStyle> RestaurantStyles { get; set; }

        public ICollection<RestaurantWorkingHour> RestaurantWorkingHours { get; set; }
 
        public ICollection<RestaurantRate> RestaurantRates { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ICollection<RestaurantImage> RestaurantImages { get; set; }

        public ICollection<UserComment> UserComments { get; set; }
    }
}

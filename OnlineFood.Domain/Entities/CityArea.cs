using OnlineFood.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Entities
{
    /// <summary>
    /// محدوده/محله شهر
    /// </summary>
    public class CityArea : BaseEntity
    {
        /// <summary>
        /// نام محله
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// نام محله به انگلیسی
        /// </summary>
        public string TitleEng { get; set; }

        /// <summary>
        /// فعال است؟
        /// </summary>
        public ActivityStatus ActivityStatus { get; set; }

        /// <summary>
        /// شهر
        /// </summary>
        public City City { get; set; }
        public int CityId { get; set; }
        
        /// <summary>
        /// طول جغرافیایی
        /// </summary>
        public string Longitude { get; set; }

        /// <summary>
        /// عرض جغرافیایی
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// آدرس های کاربران
        /// </summary>
        public ICollection<UserAddress> UserAddresses { get; set; }

        /// <summary>
        /// مناطق تحت پوشش رستوران
        /// </summary>
        public ICollection<RestaurantCityArea> RestaurantCityAreas { get; set; }

        /// <summary>
        /// رستوران ها
        /// </summary>
        public ICollection<Restaurant> Restaurants { get; set; }
    }
}

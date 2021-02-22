using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Entities
{
    /// <summary>
    /// مناطق تحت پوشش رستوران
    /// </summary>
    public class RestaurantCityArea : BaseEntity
    {
        ///// <summary>
        ///// شهر
        ///// </summary>
        //public City City { get; set; }
        //public int CityId { get; set; }
        /// <summary>
        /// محدوده/محله
        /// </summary>
        public CityArea CityArea { get; set; }
        public int CityAreaId { get; set; }

        /// <summary>
        /// رستوران
        /// </summary>
        public Restaurant Restaurant { get; set; }
        public int RestaurantId { get; set; }

        public bool IsRestaurantPlace { get; set; }
    }
}

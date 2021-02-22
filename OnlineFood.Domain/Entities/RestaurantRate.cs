using OnlineFood.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Entities
{
    /// <summary>
    /// امتیاز رستوران
    /// </summary>
    public class RestaurantRate : BaseEntity
    {
        /// <summary>
        /// نوع امتیاز رستوران
        /// </summary>
        public RestaurantRateType RestaurantRateType { get; set; }

        /// <summary>
        /// تعداد امتیاز
        /// </summary>
        public int RateNumber { get; set; }

        ///// <summary>
        ///// امتیاز
        ///// </summary>
        //public int Rate { get; set; }

        /// <summary>
        /// امتیاز
        /// </summary>
        public SatisfactionStatus Rate { get; set; }

        /// <summary>
        /// رستوران
        /// </summary>
        public Restaurant Restaurant { get; set; }
        public int RestaurantId { get; set; }
    }
}

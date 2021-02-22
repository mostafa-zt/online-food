using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Entities
{
    /// <summary>
    /// سبد خرید
    /// </summary>
    public class OrderCart : BaseEntity
    {
        /// <summary>
        /// تعداد
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// غذا رستوران
        /// </summary>
        public RestaurantMenu RestaurantMenu { get; set; }
        public int RestaurantMenuId { get; set; }
    }
}

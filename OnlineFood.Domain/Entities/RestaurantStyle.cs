using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Entities
{
    public class RestaurantStyle : BaseEntity
    {
        /// <summary>
        /// نوع رستوران
        /// </summary>
        public RestaurantType RestaurantType { get; set; }
        public int RestaurantTypeId { get; set; }

        /// <summary>
        /// رستوران
        /// </summary>
        public Restaurant Restaurant { get; set; }
        public int RestaurantId { get; set; }
    }
}

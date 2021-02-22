using OnlineFood.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Entities
{
    /// <summary>
    /// گروبندی جزییات غذا
    /// </summary>
    public class RestaurantMenuCategory : BaseEntity
    {
        /// <summary>
        /// عنوان
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// توضیح
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// حداکثر انتخاب مجاز
        /// </summary>
        public int? MaximumAllowedSelection { get; set; }

        /// <summary>
        /// وضعیت غذا
        /// </summary>
        public RestaurantMenuStatus RestaurantMenuStatus { get; set; }

        /// <summary>
        /// غذا رستوران
        /// </summary>
        public RestaurantMenu RestaurantMenu { get; set; }
        public int RestaurantMenuId { get; set; }

        /// <summary>
        /// جزییات غذا
        /// </summary>
        public ICollection<RestaurantMenuDetail> RestaurantMenuDetails { get; set; }

    }
}

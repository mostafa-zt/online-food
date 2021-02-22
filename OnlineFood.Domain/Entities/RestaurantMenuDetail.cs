using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Entities
{
    /// <summary>
    /// جزییات غذا
    /// </summary>
    public class RestaurantMenuDetail : BaseEntity
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
        /// گروبندی جزییات غذا
        /// </summary>
        public RestaurantMenuCategory RestaurantMenuCategory { get; set; }
        public int RestaurantMenuCategoryId { get; set; }

    }
}

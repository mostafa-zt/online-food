using OnlineFood.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Entities
{
    /// <summary>
    /// نوع رستوران ایرانی/فست فود/کبابی/صبحانه
    /// </summary>
    public class RestaurantType : BaseEntity
    {
        public string Title { get; set; }

        /// <summary>
        /// فعال است؟
        /// </summary>
        public ActivityStatus ActivityStatus { get; set; }

        ///// <summary>
        ///// گروه بندی نوع بازار
        ///// </summary>
        //public RestaurantCategory RestaurantCategory { get; set; }
        //public int RestaurantCategoryId { get; set; }

        public ICollection<RestaurantStyle> RestaurantStyles { get; set; }
    }
}

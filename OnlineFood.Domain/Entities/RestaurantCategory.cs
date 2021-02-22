using OnlineFood.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Entities
{
    /// <summary>
    /// گروه بندی انواع عرضه غذایی /رستوران/شیرینی فروشی/سوپرمارکت
    /// </summary>
    public class RestaurantCategory : BaseEntity
    {
        /// <summary>
        /// عنوان
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// عنوان انگلیسی
        /// </summary>
        public string TitleEng { get; set; }

        /// <summary>
        /// فعال است؟
        /// </summary>
        public ActivityStatus ActivityStatus { get; set; }

        ///// <summary>
        ///// زستوران ها
        ///// </summary>
        //public ICollection<Restaurant> Restaurants { get; set; }

        ///// <summary>
        ///// گروه بندی غذا ها
        ///// </summary>
        //public ICollection<RestaurantFoodCategory> RestaurantFoodCategories { get; set; }

        public ICollection<Seller> Sellers { get; set; }
    }
}

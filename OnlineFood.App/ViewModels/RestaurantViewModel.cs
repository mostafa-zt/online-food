using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Web.ViewModels
{
    public class RestaurantViewModel
    {
        /// <summary>
        /// لینک جستجوی رستوران ها با محدودیت فقط شهر
        /// </summary>
        public string SearchLinkWithCityLimitation { get; set; }
        /// <summary>
        /// لینک جستجوی رستوران ها با محدودیت شهر و محله
        /// </summary>
        public string SearchLinkWithCityAreaLimitation { get; set; }


        public int TotalOrderCartNumber { get; set; }


        public SearchViewModel SearchViewModel { get; set; }
        /// <summary>
        /// لیست رستوران ها
        /// </summary>
        public List<ListRestaurantViewModel> ListRestaurantViewModels { get; set; }
        /// <summary>
        /// لیست سطح اقتصادی
        /// </summary>
        public List<ListRestaurantLevelEconomyViewModel> ListRestaurantLevelEconomyViewModels { get; set; }
        /// <summary>
        /// لیست نوع رستوران
        /// </summary>
        public List<ListRestaurantTypeViewModel> ListRestaurantTypeViewModels { get; set; }
        /// <summary>
        /// لیست گروه بندی غذا
        /// </summary>
        public List<ListRestaurantFoodCategoryViewModel> ListRestaurantFoodCategoryViewModels { get; set; }
    }

    public class ListRestaurantFoodCategoryViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string IconClassName { get; set; }
        public string FilterGroup { get; set; }
    }

    public class ListRestaurantTypeViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FilterGroup { get; set; }
    }

    public class ListRestaurantLevelEconomyViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FilterGroup { get; set; }
    }

    public class ListRestaurantViewModel
    {
        /// <summary>
        /// نام رستوران
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// محله
        /// </summary>
        public string CityArea { get; set; }

        /// <summary>
        /// آدرس لوگو
        /// </summary>
        public string LogoUrl { get; set; }

        /// <summary>
        /// آدرس هدر
        /// </summary>
        public string HeaderUrl { get; set; }

        /// <summary>
        /// تعداد پیام ها
        /// </summary>
        public int UserComments { get; set; }

        /// <summary>
        /// تعداد نظرات
        /// </summary>
        public int RateCount { get; set; }

        /// <summary>
        /// امتیاز
        /// </summary>
        public int Rate { get; set; }

        /// <summary>
        /// لینک رستوران
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// زمان تحویل
        /// </summary>
        public string DeliveryTime { get; set; }

        /// <summary>
        /// حداقل سفارش
        /// </summary>
        public string MinOrder { get; set; }

        /// <summary>
        /// نوع رستوران
        /// </summary>
        public List<string> RestaurantTypes { get; set; }

    }
}

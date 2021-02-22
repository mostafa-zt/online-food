using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Web.ViewModels
{
    public class HomeViewModel
    {
        public SearchViewModel SearchViewModel { get; set; }
        /// <summary>
        /// رستوران های محبوب
        /// </summary>
        public List<PopularRestaurantViewModel> PopularRestaurantViewModels { get; set; }

        /// <summary>
        /// رستوران های جدید 
        /// </summary>
        public List<NewRestaurantViewModel> NewRestaurantViewModels { get; set; }
    }

    public class ListCityAreaViewModel
    {
        //public int CityAreaId { get; set; }
        ///// <summary>
        ///// نام محله
        ///// </summary>
        //public string CityAreaTitle { get; set; }
        ///// <summary>
        ///// نام انگلیسی محله
        ///// </summary>
        //public string CityAreaTitleEng { get; set; }
        //public int CityId { get; set; }
        ///// <summary>
        ///// نام شهر
        ///// </summary>
        //public string CityTitle { get; set; }
        ///// <summary>
        ///// نام انگلیسی شهر
        ///// </summary>
        //public string CityTitleEng { get; set; }

        /// <summary>
        /// نام انگلیسی محله
        /// </summary>
        public string  Value { get; set; }
        /// <summary>
        /// نام محله
        /// </summary>
        public string Title { get; set; }
        public bool Selected { get; set; }
    }

    public class SearchViewModel
    {
        ///// <summary>
        ///// شهر
        ///// </summary>
        //[Display(Name = "شهر")]
        //public int CityId { get; set; }
        //public string CityTitle { get; set; }
        //public string CityTitleEng { get; set; }
        //public string CityAreaId { get; set; }
        //public string CityAreaTitle { get; set; }
        //public string CityAreaTitleEng { get; set; }
        //// <summary>
        ///// محدوده/محله
        ///// </summary>
        //[Display(Name = "محله")]
        //public string CityArea { get; set; }

        public int CityId { get; set; }
        public string CityTitle{ get; set; }
        public string CityAreaTitle { get; set; }
        public string CityTitleEng { get; set; }
        public string CityAreaTitleEng { get; set; }
        public string FoodTitle { get; set; }
        public string FoodTitleFriendlyUrl { get; set; }
        public string Location { get; set; }
        /// <summary>
        /// لیستی از محله ها
        /// </summary>
        public List<ListCityAreaViewModel> ListCityAreaViewModels { get; set; }
    }

    #region polpular restaurant - رستوران های محبوب
    public class PopularRestaurantViewModel
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
    }
    #endregion

    #region new restaurants - رستوران های جدید
    public class NewRestaurantViewModel
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
    } 
    #endregion
}

using OnlineFood.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Web.Areas.Admin.ViewModels
{
    public class SearchRestaurantViewModel
    {
        /// <summary>
        /// نام رستوران
        /// </summary>
        [Display(Name = "نام رستوران")]
        public string Title { get; set; }

        /// <summary>
        /// نام رستوران به انگلیسی
        /// </summary>
        [Display(Name = "نام رستوران به انگلیسی")]
        public string TitleEng { get; set; }

        ///// <summary>
        ///// آدرس کامل
        ///// </summary>
        //public string FullAddress { get; set; }

        /// <summary>
        /// محدوده/محله
        /// </summary>
        [Display(Name = "محدوده")]
        public List<int> CityArea { get; set; }

        /// <summary>
        /// حداقل سفارش قابل قبول
        /// </summary>
        [Display(Name = "حداقل سفارش قابل قبول")]
        public decimal? AcceptableMinimumOrder { get; set; }

        /// <summary>
        /// سطح اقتصادی رستوران
        /// </summary>
        [Display(Name = "سطح اقتصادی")]
        public List<int> RestaurantLevelEconomy { get; set; }

        /// <summary>
        /// زمان ارسال
        /// </summary>
        [Display(Name = "زمان ارسال")]
        public TimeSpan? DeliveryTime { get; set; }

        /// <summary>
        /// هزینه پیک/ارسال به مناطق تخت پوشش
        /// </summary>
        [Display(Name = "هزینه پیک")]
        public decimal? RestaurantCourierCost { get; set; }

        /// <summary>
        /// آیا تمام منطق شهر را پوشش میدهد
        /// </summary>
        [Display(Name = "آیا تمام منطق شهر را پوشش میدهد؟")]
        public bool SelectedAllRestaurantCityAreas { get; set; }

        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        [Display(Name = "از تاریخ")]
        public string FromCreatorDateTime { get; set; }

        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        [Display(Name = "تا تاریخ")]
        public string ToCreatorDateTime { get; set; }

        /// <summary>
        /// وضعیت رستوران/فعال است؟
        /// </summary>
        [Display(Name = "وضعیت رستوران")]
        public List<int> RestaurantActivityStatus { get; set; }

        ///// <summary>
        ///// مناطق تحت پوشش
        ///// </summary>
        //public ICollection<RestaurantCityArea> RestaurantCityAreas { get; set; }

    }

    public class DetailRestaurantViewModel
    {
        public int Id { get; set; }

        public int? SellerId { get; set; }

        /// <summary>
        /// نام رستوران
        /// </summary>
        [Display(Name = "نام رستوران")]
        public string Title { get; set; }

        /// <summary>
        /// نام رستوران به انگلیسی
        /// </summary>
        [Display(Name = "نام رستوران به انگلیسی")]
        public string TitleEng { get; set; }

        /// <summary>
        /// آدرس کامل
        /// </summary>
        [Display(Name = "آدرس کامل")]
        public string FullAddress { get; set; }

        /// <summary>
        /// محدوده/محله
        /// </summary>
        [Display(Name = "محله")]
        public string CityArea { get; set; }

        /// <summary>
        /// شهر
        /// </summary>
        [Display(Name = "شهر")]
        public string City { get; set; }

        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        [Display(Name = "تاریخ ایجاد")]
        public DateTime? CreatorDateTime { get; set; }

        ///// <summary>
        ///// طول جغرافیایی
        ///// </summary>
        //[Display(Name = "محله")]
        //public string Longitude { get; set; }

        ///// <summary>
        ///// عرض جغرافیایی
        ///// </summary>
        //public string Latitude { get; set; }

        /// <summary>
        /// حداقل سفارش قابل قبول
        /// </summary>
        [Display(Name = "حداقل سفارش قابل قبول")]
        public decimal? AcceptableMinimumOrder { get; set; }

        /// <summary>
        /// سطح اقتصادی رستوران
        /// </summary>
        [Display(Name = "سطح اقتصادی رستوران")]
        public string RestaurantLevelEconomy { get; set; }

        /// <summary>
        /// زمان ارسال
        /// </summary>
        [Display(Name = "زمان ارسال")]
        public TimeSpan? DeliveryTime { get; set; }

        /// <summary>
        /// هزینه پیک/ارسال به مناطق تخت پوشش
        /// </summary>
        [Display(Name = "هزینه پیک")]
        public decimal? RestaurantCourierCost { get; set; }

        public List<RestaurantImagesViewModel> RestaurantImagesViewModels { get; set; }

        /// <summary>
        /// لیستی از ساعات کاری
        /// </summary>
        [Display(Name = "ساعات کاری")]
        public List<WorkingHoursViewModel> WorkingHoursViewModels { get; set; }


        /// <summary>
        /// روند ثبت
        /// </summary>
        public SellerRegistrationProcess SellerRegistrationProcess { get; set; }

        /// <summary>
        /// فعال است؟
        /// </summary>
        [Display(Name = "وضعیت فعالیت")]
        [Required(ErrorMessage = "{0} را وارد نمایید", AllowEmptyStrings = false)]
        public RestaurantActivityStatus RestaurantActivityStatus { get; set; }

    }

    public class WorkingHoursViewModel
    {
        public int Id { get; set; }
        /// <summary>
        /// شروع زمان کاری
        /// </summary>
        public string Start { get; set; }
        /// <summary>
        /// پایان زمان کاری
        /// </summary>
        public string End { get; set; }
        /// <summary>
        /// روز هفته
        /// </summary>
        public string Day { get; set; }

        /// <summary>
        /// عنوان فارسی روز
        /// </summary>
        public string Title { get; set; }
    }

    /// <summary>
    /// عکس های رستوران
    /// </summary>
    public class RestaurantImagesViewModel
    {
        public string Type { get; set; }

        /// <summary>
        /// عنوان عکس
        /// </summary>
        public string Title { get; set; }

        [Display(Name = "آدرس")]
        public string Url { get; set; }

        [Display(Name = "پسوند")]
        public string Extension { get; set; }

        [Display(Name = "سایز")]
        public string Size { get; set; }
    }
}

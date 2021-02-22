using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Web.ViewModels
{
    public class RestaurantMenuViewModel
    {
        public int RestaurantId { get; set; }
        /// <summary>
        /// لوگوی رستوران
        /// </summary>
        public string LogoUrl { get; set; }

        /// <summary>
        /// تصویر هدر رستوران
        /// </summary>
        public string HeaderUrl { get; set; }

        /// <summary>
        /// نام رستوران
        /// </summary>
        public string RestaurantTitle { get; set; }

        /// <summary>
        /// آدرس رستوران
        /// </summary>
        public string RestaurantFullAddress { get; set; }

        /// <summary>
        /// شهر
        /// </summary>
        public string RestaurantCity { get; set; }

        /// <summary>
        /// محله
        /// </summary>
        public string RestaurantCityArea { get; set; }

        /// <summary>
        /// آیا رستوران باز است؟
        /// </summary>
        public bool IsOpened { get; set; }

        /// <summary>
        /// ساعات باقیمانده تا شروع ساعت کاری
        /// </summary>
        public string RemainingHours  { get; set; }

        /// <summary>
        /// متوسط زمان ارسال/پیک
        /// </summary>
        public TimeSpan? FromDeliveryTime { get; set; }
        public TimeSpan? ToDeliveryTime { get; set; }
        public string DeliveryTime { get; set; }

        /// <summary>
        /// لینک رستوران 
        /// </summary>
        public string RestaurantLink { get; set; }

        /// <summary>
        /// لینک بازگشت
        /// </summary>
        public string ReturnLink { get; set; }

        /// <summary>
        /// لینک جستجوی رستوران ها با محدودیت فقط شهر
        /// </summary>
        public string SearchLinkWithCityLimitation { get; set; }
        /// <summary>
        /// لینک جستجوی رستوران ها با محدودیت شهر و محله
        /// </summary>
        public string SearchLinkWithCityAreaLimitation { get; set; }
        /// <summary>
        /// منوی رستوران
        /// </summary>
        public List<ListRestaurantMenuViewModel> ListRestaurantMenuViewModels { get; set; }
        /// <summary>
        /// سبد خرید
        /// </summary>
        public OrderCartViewModel OrderCartViewModel { get; set; }

        /// <summary>
        /// اطلاعات کلی رستوران
        /// </summary>
        public RestaurantInfoViewModel RestaurantInfoViewModel { get; set; }
    }

    public class ListRestaurantMenuViewModel
    {
        public int RestaurantFoodCategoryId { get; set; }
        public string RestaurantFoodCategory { get; set; }
        public string RestaurantFoodCategoryClassname { get; set; }
        public List<SubListRestaurantMenuViewModel> SubListRestaurantMenuViewModels { get; set; }
    }

    public class SubListRestaurantMenuViewModel
    {
        public int Id { get; set; }
        /// <summary>
        /// آدرس تصویر غذا
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// نام
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// توضیح
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// قیمت
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// تعداد نظرات کاربران
        /// </summary>
        public int UserComments { get; set; }
        /// <summary>
        /// آیا سفارش داده شده؟
        /// </summary>
        public bool IsOrdered { get; set; }

        public int NumberOfOrder { get; set; }
    }

    public class ListRestaurantMenuFoodCategoryViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string IconClassName { get; set; }
    }

    public class RestaurantInfoViewModel
    {
        public string RestaurantTilte { get; set; }
        public string RestaurantAddress { get; set; }
        /// <summary>
        /// آیا تمام منطق شهر را پوشش میدهد
        /// </summary>
        public bool SelectedAllRestaurantCityAreas { get; set; }
        public List<string> RestaurantTypes { get; set; }
        public List<string> RestaurantFoodCategories { get; set; }
        /// <summary>
        /// حداقل سفارش قابل قبول
        /// </summary>
        public decimal? AcceptableMinimumOrder { get; set; }
        /// <summary>
        /// هزینه پیک/ارسال به مناطق تخت پوشش
        /// </summary>
        public decimal? RestaurantCourierCost { get; set; }
        /// <summary>
        /// متوسط زمان ارسال/پیک
        /// </summary>
        public string DeliveryTime { get; set; }
        //public TimeSpan? ToDeliveryTime { get; set; }
        /// <summary>
        /// مناطق تحت پوشش
        /// </summary>
        public List<string> RestaurantCityAreas { get; set; }
        public List<ListDaysViewModel> ListDaysViewModels{ get; set; }

        public List<ListRestaurantWorkingHourViewModel> ListRestaurantWorkingHourViewModels { get; set; }
    }

    public class ListRestaurantWorkingHourViewModel
    {
        /// <summary>
        /// روز هفته
        /// </summary>
        public string Day { get; set; }

        /// <summary>
        /// ساعت کاری رستوران
        /// </summary>
        public string WorkingHour { get; set; }

        /// <summary>
        /// آیا رستوران باز است؟
        /// </summary>
        public bool IsOpened { get; set; }

        /// <summary>
        /// آیا امروز است؟
        /// </summary>
        public bool IsToday{ get; set; }
    }
    public class ListDaysViewModel
    {
        public Domain.Enum.DaysOfWeek Day { get; set; }
        public bool IsToday { get; set; }
    }
}

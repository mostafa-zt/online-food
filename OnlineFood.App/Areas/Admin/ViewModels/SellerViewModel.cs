using OnlineFood.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Web.Areas.Admin.ViewModels
{
    public class SellerViewModel
    {
        public int MyProperty { get; set; }
    }

    
    public class SearchSellerViewModel
    {
        /// <summary>
        /// نام کاربری
        /// </summary>
        [Display(Name = "نام کاربری")]
        public string Username { get; set; }

        /// <summary>
        /// ایمیل
        /// </summary>
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        #region اطلاعات حقیقی/فردی
        /// <summary>
        /// کد ملی
        /// </summary>
        [Display(Name = "کد ملی")]
        public string NationalCode { get; set; }

        /// <summary>
        /// شماره شناسنامه
        /// </summary>
        [Display(Name = "شماره شناسنامه")]
        public string BirthCertificateNumber { get; set; }

        /// <summary>
        /// نام
        /// </summary>
        [Display(Name = "نام")]
        public string Firstname { get; set; }

        /// <summary>
        /// نام خانوادگی
        /// </summary>
        [Display(Name = "نام خانوادگی")]
        public string Lastname { get; set; }

        /// <summary>
        /// نام و نام خانوادگی فروشنده
        /// </summary>
        public string Fullname => string.Format("{0} {1}", Firstname, Lastname);

        /// <summary>
        /// تاریخ تولد
        /// </summary>
        [Display(Name = "تاریخ تولد")]
        public string BirthDate { get; set; }

        /// <summary>
        /// جنسیت
        /// </summary>
        [Display(Name = "جنسیت")]
        public List<int> Gender { get; set; }
        #endregion

        #region اطلاعات حقوقی/شرکتی
        /// <summary>
        /// نام شرکت
        /// </summary>
        [Display(Name = "نام شرکت")]
        public string CompanyTitle { get; set; }

        /// <summary>
        /// نوع شرکت
        /// </summary>
        [Display(Name = "نوع شرکت")]
        public List<int> CompanyType { get; set; }

        /// <summary>
        /// شماره ثبت
        /// </summary>
        [Display(Name = "شماره ثبت")]
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// شناسه ملی
        /// </summary>
        [Display(Name = "شناسه ملی")]
        public string NationalId { get; set; }

        /// <summary>
        /// کد اقتصادی
        /// </summary>
        [Display(Name = "کد اقتصادی")]
        public string EconomicCode { get; set; }
        #endregion

        #region اطلاعات تماس
        /// <summary>
        /// شهر
        /// </summary>
        [Display(Name = "شهر")]
        public List<int> City { get; set; }

        ///// <summary>
        ///// آدرس کامل
        ///// </summary>
        //public string FullAddress { get; set; }

        /// <summary>
        /// کد پستی
        /// </summary>
        [Display(Name = "کد پستی")]
        public string ZipCode { get; set; }

        /// <summary>
        /// شماره تلفن ثابت
        /// </summary>
        [Display(Name = "تلفن ثابت")]
        public string TelphoneNumber { get; set; }

        /// <summary>
        /// شماره موبایل
        /// </summary>
        [Display(Name = "شماره موبایل")]
        public string PhoneNumber { get; set; }
        #endregion

        #region اطلاعات تجاری
        /// <summary>
        /// نوع بازار
        /// </summary>
        [Display(Name = "نوع بازار")]
        public List<int> RestaurantCategory { get; set; }

        /// <summary>
        ///  نوع جایگاه کاری 
        /// </summary>
        [Display(Name = "نوع جایگاه کاری")]
        public List<int> SellerPosition { get; set; }

        /// <summary>
        /// شماره شبا
        /// </summary>
        [Display(Name = "شماره شبا")]
        public string ShabaNumber { get; set; }

        /// <summary>
        /// نام رستوران یا فروشگاه
        /// </summary>
        [Display(Name = "نام رستوران")]
        public string Title { get; set; }
        #endregion

        /// <summary>
        /// کد رهگیری
        /// </summary>
        [Display(Name = "کد رهگیری")]
        public string TrackingCode { get; set; }

        /// <summary>
        /// نوع فروشنده
        /// </summary>
        [Display(Name = "نوع فروشنده")]
        public List<int> SellerType { get; set; }

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
        /// فعال است؟
        /// </summary>
        [Display(Name = "وضعیت فعالیت")]
        public List<int> ActivityStatus { get; set; }
    }

    public class DetailSellerViewModel
    {
        public int Id { get; set; }

        public int RestaurantId { get; set; }

        /// <summary>
        /// نام کاربری
        /// </summary>
        [Display(Name = "نام کاربری")]
        public string Username { get; set; }

        /// <summary>
        /// ایمیل
        /// </summary>
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        #region اطلاعات حقیقی/فردی
        /// <summary>
        /// کد ملی
        /// </summary>
        [Display(Name = "کد ملی")]
        public string NationalCode { get; set; }

        /// <summary>
        /// شماره شناسنامه
        /// </summary>
        [Display(Name = "شماره شناسنامه")]
        public string BirthCertificateNumber { get; set; }

        /// <summary>
        /// نام
        /// </summary>
        [Display(Name = "نام")]
        public string Firstname { get; set; }

        /// <summary>
        /// نام خانوادگی
        /// </summary>
        [Display(Name = "نام خانوادگی")]
        public string Lastname { get; set; }

        /// <summary>
        /// نام و نام خانوادگی فروشنده
        /// </summary>
        public string Fullname => string.Format("{0} {1}", Firstname, Lastname);

        /// <summary>
        /// تاریخ تولد
        /// </summary>
        [Display(Name = "تاریخ تولد")]
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// جنسیت
        /// </summary>
        [Display(Name = "جنسیت")]
        public Gender? Gender { get; set; }
        #endregion

        #region اطلاعات حقوقی/شرکتی
        /// <summary>
        /// نام شرکت
        /// </summary>
        [Display(Name = "نام شرکت")]
        public string CompanyTitle { get; set; }

        /// <summary>
        /// نوع شرکت
        /// </summary>
        [Display(Name = "نوع شرکت")]
        public CompanyType? CompanyType { get; set; }

        /// <summary>
        /// شماره ثبت
        /// </summary>
        [Display(Name = "شماره ثبت")]
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// شناسه ملی
        /// </summary>
        [Display(Name = "شناسه ملی")]
        public string NationalId { get; set; }

        /// <summary>
        /// کد اقتصادی
        /// </summary>
        [Display(Name = "کد اقتصادی")]
        public string EconomicCode { get; set; }
        #endregion

        #region اطلاعات تماس
        /// <summary>
        /// شهر
        /// </summary>
        [Display(Name = "شهر")]
        public string City { get; set; }

        /// <summary>
        /// آدرس کامل
        /// </summary>
        [Display(Name = "آدرس کامل")]
        public string FullAddress { get; set; }

        /// <summary>
        /// کد پستی
        /// </summary>
        [Display(Name = "کد پستی")]
        public string ZipCode { get; set; }

        /// <summary>
        /// شماره تلفن ثابت
        /// </summary>
        [Display(Name = "تلفن ثابت")]
        public string TelphoneNumber { get; set; }

        /// <summary>
        /// شماره موبایل
        /// </summary>
        [Display(Name = "شماره موبایل")]
        public string PhoneNumber { get; set; }
        #endregion

        #region اطلاعات تجاری
        /// <summary>
        /// نوع بازار
        /// </summary>
        [Display(Name = "نوع بازار")]
        public string RestaurantCategory { get; set; }

        /// <summary>
        ///  نوع جایگاه کاری 
        /// </summary>
        [Display(Name = "نوع جایگاه کاری")]
        public string SellerPosition { get; set; }

        /// <summary>
        /// شماره شبا
        /// </summary>
        [Display(Name = "شماره شبا")]
        public string ShabaNumber { get; set; }

        /// <summary>
        /// نام رستوران یا فروشگاه
        /// </summary>
        [Display(Name = "نام رستوران")]
        public string Title { get; set; }
        #endregion

        /// <summary>
        /// کد رهگیری
        /// </summary>
        [Display(Name = "کد رهگیری")]
        public string TrackingCode { get; set; }

        /// <summary>
        /// نوع فروشنده
        /// </summary>
        [Display(Name = "نوع فروشنده")]
        public SellerType SellerType { get; set; }

        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        [Display(Name = "تاریخ ایجاد")]
        public DateTime? CreatorDateTime { get; set; }

        /// <summary>
        /// روند ثبت
        /// </summary>
        //[Display(Name = "روند ثبت")]
        //[Required(ErrorMessage = "{0} را وارد نمایید", AllowEmptyStrings = false)]
        public SellerRegistrationProcess SellerRegistrationProcess { get; set; }

        /// <summary>
        /// لیستی از مدارک
        /// </summary>
        public List<SellerImageViewModel> SellerImageViewModels { get; set; }
    }

    /// <summary>
    /// مدارک بارگذاری شده فروشندگان
    /// </summary>
    public class SellerImageViewModel
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

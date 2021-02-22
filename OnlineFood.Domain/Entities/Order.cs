using OnlineFood.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Entities
{
    /// <summary>
    /// سفارش
    /// </summary>
    public class Order : BaseEntity
    {
        /// <summary>
        /// توضیحات
        /// </summary>
        public string Description { get; set; }

        public int Test { get; set; }
        public string Testss { get; set; }


        /// <summary>
        /// آدرس کاربر
        /// </summary>
        public string UserAddress { get; set; }
        public int? UserAddressId { get; set; }

        /// <summary>
        /// عنوان آدرس
        /// </summary>
        public string UserAddressTitle { get; set; }
        public int UserAddressTitleId { get; set; }

        /// <summary>
        /// تعداد کل سفارش
        /// </summary>
        public int TotalNumber { get; set; }

        /// <summary>
        /// قیمت کل
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// هزینه پیک
        /// </summary>
        public decimal? RestaurantCourierPrice { get; set; }

        /// <summary>
        /// هزینه بسته بندی
        /// </summary>
        public decimal? PackagingPrice { get; set; }

        /// <summary>
        /// ارزش افزوده
        /// </summary>
        public decimal? ValueAddedPrice { get; set; }

        /// <summary>
        /// قیمت نهایی/قابل پرداخت
        /// </summary>
        public decimal FinalPrice { get; set; }

        /// <summary>
        /// نوع تحویل سفارش
        /// </summary>
        public DeliveryType DeliveryType { get; set; }

        /// <summary>
        /// وضعیت سفارش
        /// </summary>
        public OrderLogistic OrderLogistic { get; set; }

        /// <summary>
        /// روش پرداخت
        /// </summary>
        public PaymentMethod PaymentMethod { get; set; }

        /// <summary>
        /// وضعیت پرداخت
        /// </summary>
        public PaymentStatus PaymentStatus { get; set; }

        /// <summary>
        /// رستوران
        /// </summary>
        public Restaurant Restaurant { get; set; }
        public int RestaurantId { get; set; }

        /// <summary>
        /// رزرو غذا/تحویل در رستوران
        /// </summary>
        public OrderReservation OrderReservation { get; set; }

        /// <summary>
        /// جزییات سفارشات
        /// </summary>
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

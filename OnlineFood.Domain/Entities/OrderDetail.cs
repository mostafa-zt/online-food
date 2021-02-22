using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Entities
{
    /// <summary>
    /// جزییات سفارش
    /// </summary>
    public class OrderDetail
    {
        public int Id { get; set; }

        /// <summary>
        /// تعداد
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// نام غذا
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// توضیح غذا 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// قیمت
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// توضیح قیمت
        /// </summary>
        public string PriceDescription { get; set; }

        /// <summary>
        /// قیمت کل
        /// </summary>
        public decimal TotalPrice { get; set; }
        
        /// <summary>
        /// غذا
        /// </summary>
        public int RestaurantMenuId { get; set; }

        /// <summary>
        /// سفارش
        /// </summary>
        public Order Order { get; set; }
        public int OrderId { get; set; }
    }
}

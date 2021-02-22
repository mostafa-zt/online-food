using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Common.Model
{
    public class OrderBasket
    {
        /// <summary>
        /// کلید اصلی غذا
        /// </summary>
        public int MenuId { get; set; }
        /// <summary>
        /// تعداد
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// قیمت
        /// </summary>
        public decimal  Price { get; set; }
        /// <summary>
        /// عنوان غذا
        /// </summary>
        public string Title { get; set; }
    }
}

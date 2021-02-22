using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Web.ViewModels
{
    public class OrderCartViewModel
    {
        /// <summary>
        /// نام رستوران
        /// </summary>
        public string RestaurantTitle { get; set; }
        /// <summary>
        /// لوگوی رستوران
        /// </summary>
        public string RestaurantLogo { get; set; }

        /// <summary>
        /// کل مبلغ سفارش
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// تعداد کل سفارش
        /// </summary>
        public int TotalNumber { get; set; }

        /// <summary>
        /// آیا سبد خرید خالی است؟
        /// </summary>
        public bool IsEmpty { get; set; }

        /// <summary>
        /// آیا رستوران باز است؟
        /// </summary>
        public bool IsOpened { get; set; }

        public List<ListOrderCartViewModel> ListOrderCartViewModels { get; set; }
    }
    public class ListOrderCartViewModel
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
        public decimal Price { get; set; }
        /// <summary>
        /// عنوان غذا
        /// </summary>
        public string Title { get; set; }
    }
}

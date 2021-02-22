using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineFood.Domain.Enum
{
    /// <summary>
    /// وضعیت تایید رستوران
    /// </summary>
    public enum RestaurantActivityStatus
    {
        /// <summary>
        /// آماده سفارش گیری
        /// </summary>
        [Description("آماده سفارش گیری")]
        [Display(Name = "آماده سفارش گیری")]
        ReadyToOrder = 1,
        /// <summary>
        /// غیر فعال
        /// </summary>
        [Description("غیر فعال")]
        [Display(Name = "غیر فعال")]
        UnReadyToOrder = 2
    }
}

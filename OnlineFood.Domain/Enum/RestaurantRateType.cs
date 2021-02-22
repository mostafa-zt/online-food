using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineFood.Domain.Enum
{
    /// <summary>
    /// نوع امتیاز رستوران
    /// </summary>
    public enum RestaurantRateType
    {
        [Description("تحویل")]
        [Display(Name = "تحویل")]
        Delivery = 1,
        [Description("قیمت غذا")]
        [Display(Name = "قیمت غذا")]
        Price = 2,
        [Description("کیفیت غذا")]
        [Display(Name = "کیفیت غذا")]
        Quality = 3
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineFood.Domain.Enum
{
    /// <summary>
    /// نوع تصویر مدارک فروشنده
    /// </summary>
    public enum SellerImageType
    {
        /// <summary>
        /// تصویر پشت کارت ملی
        /// </summary>
        [Description("تصویر پشت کارت ملی")]
        [Display(Name = "تصویر پشت کارت ملی")]
        BehindTheCard = 1,
        /// <summary>
        /// تصویر روی کارت ملی
        /// </summary>
        [Description("تصویر روی کارت ملی")]
        [Display(Name = "تصویر روی کارت ملی")]
        FrontTheCard = 2
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineFood.Domain.Enum
{
    /// <summary>
    /// نوع فروشنده حقیقی/حقوقی
    /// </summary>
    public enum SellerType
    {
        /// <summary>
        /// فروشنده حقیقی
        /// </summary>
        [Description("فروشنده حقیقی")]
        [Display(Name = "فروشنده حقیقی")]
        Personal = 1,
        /// <summary>
        /// فروشنده حقوقی
        /// </summary>
        [Description("فروشنده حقوقی")]
        [Display(Name = "فروشنده حقوقی")]
        Legal = 2
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineFood.Domain.Enum
{
    /// <summary>
    /// نوع شرکت
    /// </summary>
    public enum CompanyType
    {
        /// <summary>
        /// سهامی عام
        /// </summary>
        [Description("سهامی عام")]
        [Display(Name = "سهامی عام")]
        PublicStock =1 ,
        /// <summary>
        /// سهامی خاص
        /// </summary>
        [Description("سهامی خاص")]
        [Display(Name = "سهامی خاص")]
        PrivateStock
    }
}

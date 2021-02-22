using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineFood.Domain.Enum
{
    /// <summary>
    /// جنسیت
    /// </summary>
    public enum Gender
    {
        [Description("مرد")]
        [Display(Name = "مرد")]
        Male = 1,
        [Description("زن")]
        [Display(Name = "زن")]
        Female = 2
    }
}

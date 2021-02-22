using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineFood.Domain.Enum
{
    /// <summary>
    /// روزهای هفته
    /// </summary>
    public enum DaysOfWeek
    {
        [Description("Sunday")]
        [Display(Name = "Sunday", Order =2)]
        Sunday = 0,
        [Description("Monday")]
        [Display(Name = "Monday", Order = 3)]
        Monday = 1,
        [Description("Tuesday")]
        [Display(Name = "Tuesday", Order = 4)]
        Tuesday = 2,
        [Description("Wednesday")]
        [Display(Name = "Wednesday", Order = 5)]
        Wednesday = 3,
        [Description("Thursday")]
        [Display(Name = "Thursday", Order = 6)]
        Thursday = 4,
        [Description("Friday")]
        [Display(Name = "Friday", Order = 7)]
        Friday = 5,
        [Description("Saturday")]
        [Display(Name = "Saturday", Order = 1)]
        Saturday = 6,
    }
}

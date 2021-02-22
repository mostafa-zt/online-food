using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineFood.Domain.Enum
{
   public enum ActivityStatus
    {
        [Description("Available")]
        [Display(Name = "Available")]
        Available =1 ,
        [Description("UnAvailable")]
        [Display(Name = "UnAvailable")]
        UnAvailable =2
    }
}

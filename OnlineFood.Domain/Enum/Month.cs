using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineFood.Domain.Enum
{
    public enum Month
    {
        [Description("فروردین")]
        [Display(Name = "فروردین")]
        Farvardin = 1,
        [Description("اردیبهشت")]
        [Display(Name = "اردیبهشت")]
        Ordibehesht = 2,
        [Description("خرداد")]
        [Display(Name = "خرداد")]
        Khordad = 3,
        [Description("تیر")]
        [Display(Name = "تیر")]
        Tir = 4,
        [Description("مرداد")]
        [Display(Name = "مرداد")]
        Mordada = 5,
        [Description("شهریور")]
        [Display(Name = "شهریور")]
        Shahrivar = 6,
        [Description("مهر")]
        [Display(Name = "مهر")]
        Mehr = 7,
        [Description("آبان")]
        [Display(Name = "آبان")]
        Aban = 8,
        [Description("آذر")]
        [Display(Name = "آذر")]
        Azar = 9,
        [Description("دی")]
        [Display(Name = "دی")]
        Dey = 10,
        [Description("بهمن")]
        [Display(Name = "بهمن")]
        Bahman = 11,
        [Description("اسفند")]
        [Display(Name = "اسفند")]
        Esfand = 12,
    }
}

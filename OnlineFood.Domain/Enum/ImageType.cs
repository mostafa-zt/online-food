using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineFood.Domain.Enum
{
    /// <summary>
    /// نوع عکس رستوران بالای صفحه/لوگو/منو
    /// </summary>
    public enum ImageType
    {
        /// <summary>
        /// لوگو
        /// </summary>
        [Description("Logo")]
        [Display(Name = "Logo")]
        Logo = 1,
        /// <summary>
        /// هدر
        /// </summary>
        [Description("Header")]
        [Display(Name = "Header")]
        Header = 2,
        /// <summary>
        /// غذا
        /// </summary>
        [Description("Menu")]
        [Display(Name = "Menu")]
        Menu
    }
}

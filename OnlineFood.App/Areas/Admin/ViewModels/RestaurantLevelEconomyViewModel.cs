using OnlineFood.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Web.Areas.Admin.ViewModels
{
    /// <summary>
    /// سطح اقتصادی رستوران
    /// </summary>
    public class SearchRestaurantLevelEconomyViewModel
    {
        /// <summary>
        /// سطح اقتصادی رستوران
        /// </summary>
        [Display(Name = "سطح اقتصادی رستوران")]
        public string Title { get; set; }

        /// <summary>
        /// فعال است؟
        /// </summary>
        [Display(Name = "وضعیت فعالیت")]
        public List<int> ActivityStatus { get; set; }


        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        [Display(Name = "از تاریخ")]
        public string FromCreatorDateTime { get; set; }


        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        [Display(Name = "تا تاریخ")]
        public string ToCreatorDateTime { get; set; }
    }

    public class CreateRestaurantLevelEconomyViewModel
    {
        /// <summary>
        /// سطح اقتصادی رستوران
        /// </summary>
        [StringLength(25, ErrorMessage = "{0} را صحیح وارد نمایید ، حداکثر {1} کاراکتر میباشد")]
        [Display(Name = "سطح اقتصادی رستوران")]
        [Required(ErrorMessage = "{0} را وارد نمایید", AllowEmptyStrings = false)]
        public string Title { get; set; }

        /// <summary>
        /// فعال است؟
        /// </summary>
        [Display(Name = "وضعیت فعالیت")]
        [Required(ErrorMessage = "{0} را وارد نمایید", AllowEmptyStrings = false)]
        public ActivityStatus ActivityStatus { get; set; }
    }

    public class EditRestaurantLevelEconomyViewModel
    {
        public int Id { get; set; }
        /// <summary>
        /// سطح اقتصادی رستوران
        /// </summary>
        [StringLength(25, ErrorMessage = "{0} را صحیح وارد نمایید ، حداکثر {1} کاراکتر میباشد")]
        [Display(Name = "سطح اقتصادی رستوران")]
        [Required(ErrorMessage = "{0} را وارد نمایید", AllowEmptyStrings = false)]
        public string Title { get; set; }

        /// <summary>
        /// فعال است؟
        /// </summary>
        [Display(Name = "وضعیت فعالیت")]
        [Required(ErrorMessage = "{0} را وارد نمایید", AllowEmptyStrings = false)]
        public ActivityStatus ActivityStatus { get; set; }
    }
}

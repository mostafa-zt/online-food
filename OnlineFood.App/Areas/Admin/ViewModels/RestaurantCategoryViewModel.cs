using OnlineFood.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Web.Areas.Admin.ViewModels
{
    public class SearchRestaurantCategoryViewModel
    {
        /// <summary>
        /// گروه بندی عرضه غذایی
        /// </summary>
        [Display(Name = "گروه بندی عرضه غذایی")]
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

    public class CreateRestaurantCategoryViewModel
    {
        /// <summary>
        /// گروه بندی عرضه غذایی
        /// </summary>
        [StringLength(25, ErrorMessage = "{0} را صحیح وارد نمایید ، حداکثر {1} کاراکتر میباشد")]
        [Display(Name = "گروه بندی عرضه غذایی")]
        [Required(ErrorMessage = "{0} را وارد نمایید", AllowEmptyStrings = false)]
        public string Title { get; set; }

        /// <summary>
        /// فعال است؟
        /// </summary>
        [Display(Name = "وضعیت فعالیت")]
        [Required(ErrorMessage = "{0} را وارد نمایید", AllowEmptyStrings = false)]
        public ActivityStatus ActivityStatus { get; set; }
    }

    public class EditRestaurantCategoryViewModel
    {
        public int Id { get; set; }
        /// <summary>
        /// گروه بندی عرضه غذایی
        /// </summary>
        [StringLength(25, ErrorMessage = "{0} را صحیح وارد نمایید ، حداکثر {1} کاراکتر میباشد")]
        [Display(Name = "گروه بندی عرضه غذایی")]
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

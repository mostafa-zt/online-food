using OnlineFood.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Web.Areas.Admin.ViewModels
{
    /// <summary>
    /// عنوان آدرس
    /// </summary>
    public class SearchUserAddressTitleViewModel
    {
        /// <summary>
        /// عنوان آدرس
        /// </summary>
        [Display(Name = "عنوان آدرس")]
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

    public class CreateUserAddressTitleViewModel
    {
        /// <summary>
        /// عنوان آدرس
        /// </summary>
        [StringLength(25, ErrorMessage = "{0} را صحیح وارد نمایید ، حداکثر {1} کاراکتر میباشد")]
        [Display(Name = "عنوان آدرس")]
        [Required(ErrorMessage = "{0} را وارد نمایید", AllowEmptyStrings = false)]
        public string Title { get; set; }

        /// <summary>
        /// فعال است؟
        /// </summary>
        [Display(Name = "وضعیت فعالیت")]
        [Required(ErrorMessage = "{0} را وارد نمایید", AllowEmptyStrings = false)]
        public ActivityStatus ActivityStatus { get; set; }
    }

    public class EditUserAddressTitleViewModel
    {
        public int Id { get; set; }
        /// <summary>
        /// عنوان آدرس
        /// </summary>
        [StringLength(25, ErrorMessage = "{0} را صحیح وارد نمایید ، حداکثر {1} کاراکتر میباشد")]
        [Display(Name = "عنوان آدرس")]
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

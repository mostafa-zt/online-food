using OnlineFood.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Web.Areas.Admin.ViewModels
{
    public class SearchCityAreaViewModel
    {
        /// <summary>
        /// نام محله
        /// </summary>
        [Display(Name = "نام محله")]
        public string Title { get; set; }

        /// <summary>
        /// نام محله به انگلیسی
        /// </summary>
        [Display(Name = "نام محله به انگلیسی")]
        public string TitleEng { get; set; }

        /// <summary>
        /// مختصات جغرافیایی
        /// </summary>
        [Display(Name = "مختصات جغرافیایی")]
        public string GeographicalCoordinates { get; set; }

        /// <summary>
        /// فعال است؟
        /// </summary>
        [Display(Name = "وضعیت فعالیت")]
        public List<int> ActivityStatus { get; set; }

        /// <summary>
        /// شهر
        /// </summary>
        [Display(Name = "شهر")]
        public List<int> Cities { get; set; }

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

    public class CreateCityAreaViewModel
    {
        /// <summary>
        /// نام محله
        /// </summary>
        [StringLength(25, ErrorMessage = "{0} را صحیح وارد نمایید ، حداکثر {1} کاراکتر میباشد")]
        [Display(Name = "نام محله")]
        [Required(ErrorMessage = "{0} را وارد نمایید", AllowEmptyStrings = false)]
        public string Title { get; set; }

        /// <summary>
        /// نام محله به انگلیسی
        /// </summary>
        [Display(Name = "نام محله به انگلیسی")]
        [Required(ErrorMessage = "{0} را وارد نمایید", AllowEmptyStrings = false)]
        public string TitleEng { get; set; }

        /// <summary>
        /// شهر
        /// </summary>
        [Display(Name = "شهر")]
        [Required(ErrorMessage = "{0} را وارد نمایید", AllowEmptyStrings = false)]
        public int CityId { get; set; }

        /// <summary>
        /// فعال است؟
        /// </summary>
        [Display(Name = "وضعیت فعالیت")]
        [Required(ErrorMessage = "{0} را وارد نمایید", AllowEmptyStrings = false)]
        public ActivityStatus ActivityStatus { get; set; }

        /// <summary>
        /// طول جغرافیایی
        /// </summary>
        [Display(Name = "طول جغرافیایی")]
        [Required(ErrorMessage = "{0} را وارد نمایید", AllowEmptyStrings = false)]
        public string Longitude { get; set; }

        /// <summary>
        /// عرض جغرافیایی
        /// </summary>
        [Display(Name = "عرض جغرافیایی")]
        [Required(ErrorMessage = "{0} را وارد نمایید", AllowEmptyStrings = false)]
        public string Latitude { get; set; }
    }

    public class EditCityAreaViewModel
    {
        public int Id { get; set; }
        /// <summary>
        /// نام محله
        /// </summary>
        [StringLength(25, ErrorMessage = "{0} را صحیح وارد نمایید ، حداکثر {1} کاراکتر میباشد")]
        [Display(Name = "نام محله")]
        [Required(ErrorMessage = "{0} را وارد نمایید", AllowEmptyStrings = false)]
        public string Title { get; set; }

        /// <summary>
        /// نام محله به انگلیسی
        /// </summary>
        [Display(Name = "نام محله به انگلیسی")]
        [Required(ErrorMessage = "{0} را وارد نمایید", AllowEmptyStrings = false)]
        public string TitleEng { get; set; }

        /// <summary>
        /// شهر
        /// </summary>
        [Display(Name = "شهر")]
        [Required(ErrorMessage = "{0} را وارد نمایید", AllowEmptyStrings = false)]
        public int CityId { get; set; }

        /// <summary>
        /// فعال است؟
        /// </summary>
        [Display(Name = "وضعیت فعالیت")]
        [Required(ErrorMessage = "{0} را وارد نمایید", AllowEmptyStrings = false)]
        public ActivityStatus ActivityStatus { get; set; }

        /// <summary>
        /// طول جغرافیایی
        /// </summary>
        [Display(Name = "طول جغرافیایی")]
        [Required(ErrorMessage = "{0} را وارد نمایید", AllowEmptyStrings = false)]
        public string Longitude { get; set; }

        /// <summary>
        /// عرض جغرافیایی
        /// </summary>
        [Display(Name = "عرض جغرافیایی")]
        [Required(ErrorMessage = "{0} را وارد نمایید", AllowEmptyStrings = false)]
        public string Latitude { get; set; }
    }
}

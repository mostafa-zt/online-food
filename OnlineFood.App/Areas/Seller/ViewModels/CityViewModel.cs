using OnlineFood.Domain.Enum;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Web.Areas.Seller.ViewModels
{
    public class SearchCityViewModel
    {
        [Display(Name = "City")]
        public string Title { get; set; }

        [Display(Name = "Activity Status")]
        public List<int> ActivityStatus { get; set; }

        [Display(Name = "From Date")]
        public DateTime? FromCreatorDateTime { get; set; }

        [Display(Name = "To Date")]
        public DateTime? ToCreatorDateTime { get; set; }
    }

    public class CreateCityViewModel
    {
        [StringLength(25, ErrorMessage = "{0}: Please enter maximum {1} characters ")]
        [Display(Name = "City")]
        [Required(ErrorMessage = "{0} is required", AllowEmptyStrings = false)]
        public string Title { get; set; }

        [Display(Name = "Activity Status")]
        [Required(ErrorMessage = "{0} is required", AllowEmptyStrings = false)]
        public ActivityStatus ActivityStatus { get; set; }
    }

    public class EditCityViewModel
    {
        public int Id { get; set; }

        [StringLength(25, ErrorMessage = "{0}: Please enter maximum {1} characters ")]
        [Display(Name = "City")]
        [Required(ErrorMessage = "{0} is required", AllowEmptyStrings = false)]
        public string Title { get; set; }

        [Display(Name = "Activity Status")]
        [Required(ErrorMessage = "{0} is required", AllowEmptyStrings = false)]
        public ActivityStatus ActivityStatus { get; set; }
    }
}

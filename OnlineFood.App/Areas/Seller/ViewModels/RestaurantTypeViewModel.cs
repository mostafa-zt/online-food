using OnlineFood.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Web.Areas.Seller.ViewModels
{
    public class SearchRestaurantTypeViewModel
    {
        [Display(Name = "Restaurant Type")]
        public string Title { get; set; }

        [Display(Name = "Activity Status")]
        public List<int> ActivityStatus { get; set; }

        [Display(Name = "From Date")]
        public DateTime? FromCreatorDateTime { get; set; }

        [Display(Name = "To Date")]
        public DateTime? ToCreatorDateTime { get; set; }
    }

    public class CreateRestaurantTypeViewModel
    {
        [StringLength(25, ErrorMessage = "{0} should be maximum {1} characters")]
        [Display(Name = "Reataurant Type")]
        [Required(ErrorMessage = "{0} is required", AllowEmptyStrings = false)]
        public string Title { get; set; }

        [Display(Name = "Activity Status")]
        [Required(ErrorMessage = "{0} is required", AllowEmptyStrings = false)]
        public ActivityStatus ActivityStatus { get; set; }
    }

    public class EditRestaurantTypeViewModel
    {
        public int Id { get; set; }

        [StringLength(25, ErrorMessage = "{0} should be maximum {1} characters")]
        [Display(Name = "Reataurant Type")]
        [Required(ErrorMessage = "{0} is required", AllowEmptyStrings = false)]
        public string Title { get; set; }

        [Display(Name = "Activity Status")]
        [Required(ErrorMessage = "{0} is required", AllowEmptyStrings = false)]
        public ActivityStatus ActivityStatus { get; set; }
    }
}

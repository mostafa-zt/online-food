using Microsoft.AspNetCore.Http;
using OnlineFood.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Web.Areas.Seller.ViewModels
{
    /// <summary>
    /// نوع غذا
    /// </summary>
    public class SearchRestaurantFoodCategoryViewModel
    {
        [Display(Name = "Food Name")]
        public string Title { get; set; }

        [Display(Name = "Activity Status")]
        public List<int> ActivityStatus { get; set; }

        [Display(Name = "From Date")]
        public DateTime? FromCreatorDateTime { get; set; }

        [Display(Name = "To Date")]
        public DateTime? ToCreatorDateTime { get; set; }
    }

    public class CreateRestaurantFoodCategoryViewModel
    {
        [StringLength(25, ErrorMessage = "{0} should be maximum {1} characters")]
        [Display(Name = "Food Name")]
        [Required(ErrorMessage = "{0} is required", AllowEmptyStrings = false)]
        public string Title { get; set; }

        [Display(Name = "Activity Status")]
        [Required(ErrorMessage = "{0} is required", AllowEmptyStrings = false)]
        public ActivityStatus ActivityStatus { get; set; }

        public List<GalleryRestaurantImageViewModel> GalleryRestaurantImageViewModels { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "{0} is required", AllowEmptyStrings = false)]
        [Display(Name = "Price")]
        [DataType(DataType.Currency, ErrorMessage = "{0} is not valid")]
        public decimal? Price { get; set; }

        [Display(Name = "Food Image")]
        public IFormFile ImageFile { get; set; }

        public int? RestaurantImageId { get; set; }
    }

    public class EditRestaurantFoodCategoryViewModel
    {
        public int Id { get; set; }

        [StringLength(25, ErrorMessage = "{0} should be maximum {1} characters")]
        [Display(Name = "Food Name")]
        [Required(ErrorMessage = "{0} is required", AllowEmptyStrings = false)]
        public string Title { get; set; }

        [Display(Name = "Activity Status")]
        [Required(ErrorMessage = "{0} is required", AllowEmptyStrings = false)]
        public ActivityStatus ActivityStatus { get; set; }

        public List<GalleryRestaurantImageViewModel> GalleryRestaurantImageViewModels { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "{0} is required", AllowEmptyStrings = false)]
        [Display(Name = "Price")]
        [DataType(DataType.Currency, ErrorMessage = "{0} is not valid")]
        public decimal? Price { get; set; }

        [Display(Name = "Food Image")]
        public IFormFile ImageFile { get; set; }

        public int? RestaurantImageId { get; set; }

    }
}

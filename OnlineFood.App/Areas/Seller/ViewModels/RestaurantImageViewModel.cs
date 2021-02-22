using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Web.Areas.Seller.ViewModels
{
    public class UploadRestaurantImageViewModel
    {
        [Display(Name = "Upload Image")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required")]
        public IFormFile ImageFile { get; set; }

        public int RestaurantId { get; set; }

        public List<GalleryRestaurantImageViewModel> GalleryRestaurantImageViewModels { get; set; }
    }
    public class GalleryRestaurantImageViewModel
    {
        public int Id { get; set; }

        public string Type { get; set; }

        [Display(Name = "URL")]
        public string Url { get; set; }

        [Display(Name = "Extension")]
        public string Extension { get; set; }

        [Display(Name = "Size")]
        public string Size { get; set; }
    }
}

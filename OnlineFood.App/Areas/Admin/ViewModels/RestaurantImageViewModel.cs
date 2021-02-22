using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Web.Areas.Admin.ViewModels
{
    public class CreateRestaurantImageViewModel : BaseViewModel
    {
        /// <summary>
        /// تصویر غذا
        /// </summary>
        [Display(Name = "تصویر غذا")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} را وارد نمائید")]
        public IFormFile ImageFile { get; set; }

        public int RestaurantId { get; set; }
    }
    public class EditRestaurantImageViewModel :BaseViewModel
    {
        /// <summary>
        /// تصویر غذا
        /// </summary>
        [Display(Name = "تصویر غذا")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} را وارد نمائید")]
        public IFormFile ImageFile { get; set; }

        /// <summary>
        /// لینک
        /// </summary>
        public string ImageFileUrl { get; set; }

        /// <summary>
        /// نام فایل
        /// </summary>
        public string ImageFileName { get; set; }

        public int ImageFileId { get; set; }

        public int RestaurantId { get; set; }
    }
    public class RestaurantImageViewModel
    {
        public int RestaurantId { get; set; }

        public List<GalleryRestaurantImageViewModel> GalleryRestaurantImageViewModels { get; set; }
    }
    public class GalleryRestaurantImageViewModel
    {
        public int Id { get; set; }

        /// <summary>
        /// نوع عکس
        /// </summary>
        public string Type { get; set; }

        [Display(Name = "آدرس")]
        public string Url { get; set; }

        [Display(Name = "پسوند")]
        public string Extension { get; set; }

        [Display(Name = "سایز")]
        public string Size { get; set; }
    }
}

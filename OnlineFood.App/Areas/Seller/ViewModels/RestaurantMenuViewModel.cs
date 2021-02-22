using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Web.Areas.Seller.ViewModels
{
    public class ManageRestaurantMenuViewModel
    {
        public int RestaurantId { get; set; }
        public RestaurantMenuViewModel RestaurantMenuViewModels { get; set; }
    }

    public class CreateRestaurantMenuViewModel
    {
        public int RestaurantId { get; set; }


        [StringLength(25, ErrorMessage = "{0} را صحیح وارد نمایید ، حداکثر {1} کاراکتر میباشد")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} را وارد نمائید")]
        [Display(Name = "نام غذا")]
        [RegularExpression("^[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z0-9]+[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z0-9- _]*$", ErrorMessage = "{0} شامل حروف و اعداد میباشد")]
        //^[A-Za-z0-9 _]*[A-Za-z0-9][A-Za-z0-9 _]*$
        public string Title { get; set; }

        /// <summary>
        /// توضیح غذا
        /// </summary>
        [StringLength(250, ErrorMessage = "{0} را صحیح وارد نمایید ، حداکثر {1} کاراکتر میباشد")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} را وارد نمائید")]
        [Display(Name = "توضیح غذا")]
        //[RegularExpression("^[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z0-9]+[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z- _]*$", ErrorMessage = "{0} شامل حروف و اعداد میباشد")]
        [RegularExpression("^[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z0-9]+[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z0-9- _]*$", ErrorMessage = "{0} شامل حروف و اعداد میباشد")]
        public string Description { get; set; }

        /// <summary>
        /// قیمت
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} را وارد نمائید")]
        [Display(Name = "قیمت")]
        [DataType(DataType.Currency, ErrorMessage = "{0} را صحیح وارد نمایید")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,###0}")]
        public decimal Price { get; set; }

        /// <summary>
        /// توضیح قیمت
        /// </summary>
        [StringLength(250, ErrorMessage = "{0} را صحیح وارد نمایید ، حداکثر {1} کاراکتر میباشد")]
        [Display(Name = "توضیح قیمت")]
        [RegularExpression("^[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z0-9]+[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z0-9- _]*$", ErrorMessage = "{0} شامل حروف و اعداد میباشد")]
        public string PriceDescription { get; set; }

        /// <summary>
        /// تصویر غذا
        /// </summary>
        [Display(Name = "تصویر غذا")]
        public IFormFile ImageFile { get; set; }

        /// <summary>
        /// عکس انتخابی
        /// </summary>
        public int? RestaurantImageId { get; set; }

        /// <summary>
        /// گروه بندی غذا 
        /// </summary>
        [StringLength(25, ErrorMessage = "{0} را صحیح وارد نمایید ، حداکثر {1} کاراکتر میباشد")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} را وارد نمائید")]
        [Display(Name = "گروه بندی غذا")]
        [RegularExpression("^[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z0-9]+[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z- _]*$", ErrorMessage = "{0} شامل حروف و اعداد میباشد")]
        public string RestaurantFoodCategory { get; set; }
        public int? RestaurantFoodCategoryId { get; set; }

        /// <summary>
        /// گالری عکی های رستوران/غذا
        /// </summary>
        public List<GalleryRestaurantImageViewModel> GalleryRestaurantImageViewModels { get; set; }

        public List<ListRestaurantMenuViewModel> ListRestaurantMenuViewModels { get; set; }
    }

    public class EditRestaurantMenuViewModel
    {
        public int Id { get; set; }

        public int RestaurantId { get; set; }

        /// <summary>
        /// نام غذا
        /// </summary>
        [StringLength(25, ErrorMessage = "{0} را صحیح وارد نمایید ، حداکثر {1} کاراکتر میباشد")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} را وارد نمائید")]
        [Display(Name = "نام غذا")]
        [RegularExpression("^[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z0-9]+[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z0-9- _]*$", ErrorMessage = "{0} شامل حروف و اعداد میباشد")]
        //^[A-Za-z0-9 _]*[A-Za-z0-9][A-Za-z0-9 _]*$
        public string Title { get; set; }

        /// <summary>
        /// توضیح غذا
        /// </summary>
        [StringLength(250, ErrorMessage = "{0} را صحیح وارد نمایید ، حداکثر {1} کاراکتر میباشد")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} را وارد نمائید")]
        [Display(Name = "توضیح غذا")]
        [RegularExpression("^[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z0-9]+[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z0-9- _]*$", ErrorMessage = "{0} شامل حروف و اعداد میباشد")]
        public string Description { get; set; }

        /// <summary>
        /// قیمت
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} را وارد نمائید")]
        [Display(Name = "قیمت")]
        [DataType(DataType.Currency, ErrorMessage = "{0} را صحیح وارد نمایید")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,###0}")]
        public decimal Price { get; set; }

        /// <summary>
        /// توضیح قیمت
        /// </summary>
        [StringLength(250, ErrorMessage = "{0} را صحیح وارد نمایید ، حداکثر {1} کاراکتر میباشد")]
        [Display(Name = "توضیح قیمت")]
        [RegularExpression("^[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z0-9]+[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z0-9- _]*$", ErrorMessage = "{0} شامل حروف و اعداد میباشد")]
        public string PriceDescription { get; set; }


        /// <summary>
        /// تصویر غذا
        /// </summary>
        [Display(Name = "تصویر غذا")]
        public IFormFile ImageFile { get; set; }

        /// <summary>
        /// عکس انتخابی
        /// </summary>
        public int? RestaurantImageId { get; set; }

        /// <summary>
        /// گروه بندی غذا 
        /// </summary>
        [StringLength(25, ErrorMessage = "{0} را صحیح وارد نمایید ، حداکثر {1} کاراکتر میباشد")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} را وارد نمائید")]
        [Display(Name = "گروه بندی غذا")]
        [RegularExpression("^[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z0-9]+[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z- _]*$", ErrorMessage = "{0} شامل حروف و اعداد میباشد")]
        public string RestaurantFoodCategory { get; set; }
        public int? RestaurantFoodCategoryId { get; set; }

        /// <summary>
        /// گالری عکی های رستوران/غذا
        /// </summary>
        public List<GalleryRestaurantImageViewModel> GalleryRestaurantImageViewModels { get; set; }

        public List<ListRestaurantMenuViewModel> ListRestaurantMenuViewModels { get; set; }
    }

    #region ListRestaurantMenuViewModel
    public class RestaurantMenuViewModel
    {
        public List<ListRestaurantMenuViewModel> ListRestaurantMenuViewModels { get; set; }
    }
    public class ListRestaurantMenuViewModel
    {
        public int RestaurantFoodCategoryId { get; set; }
        public string RestaurantFoodCategory { get; set; }
        public string RestaurantFoodCategoryClassname { get; set; }
        public List<SubListRestaurantMenuViewModel> SubListRestaurantMenuViewModels { get; set; }
    }

    public class SubListRestaurantMenuViewModel
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int? ImageId { get; set; }
        public string Title { get; set; }
        public string TitleDescription { get; set; }
        public decimal Price { get; set; }
        public string PriceDescription { get; set; }
    }
    #endregion
}

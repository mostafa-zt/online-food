using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Web.Areas.Seller.ViewModels
{
    public class EditRestaurantViewModel
    {
        public int Id { get; set; }

        [StringLength(25, ErrorMessage = "{0} should be maximum {1} characters")]
        [Required(ErrorMessage = "{0} is required", AllowEmptyStrings = false)]
        [Display(Name = "Restaurant Name")]
        public string Title { get; set; }

        [Display(Name = "City")]
        public int? CityId { get; set; }

        [StringLength(250, ErrorMessage = "{0} should be maximum {1} characters")]
        [Required(ErrorMessage = "{0} is required", AllowEmptyStrings = false)]
        [Display(Name = "Full Address")]
        public string FullAddress { get; set; }

        [Display(Name = "Postal Code")]
        [Required(ErrorMessage = "{0} is required", AllowEmptyStrings = false)]
        public string PostalCode { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "{0} is required", AllowEmptyStrings = false)]
        [Display(Name = "Budget Level ")]
        public int RestaurantLevelEconomyId { get; set; }

        [Display(Name = "Delivery Time Average, From: ")]
        public TimeSpan? FromDeliveryTime { get; set; }

        [Display(Name = "Delivery Time Average, To: ")]
        public TimeSpan? ToDeliveryTime { get; set; }

        [Required(ErrorMessage = "{0} is required", AllowEmptyStrings = false)]
        [Display(Name = "Delivery Cost Average")]
        [DataType(DataType.Currency, ErrorMessage = "{0} is not valid")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,###0}")]
        public decimal? RestaurantCourierCost { get; set; }

        [Display(Name = "Restaurant Types")]
        public List<int> RestaurantTypes { get; set; }

        [Display(Name = "Restaurant Menus")]
        public List<int> RestaurantFoodTypes { get; set; }

        public List<DaysOfWeekViewModel> DaysOfWeekViewModel { get; set; }

        [Display(Name = "Working Hours, From: ")]
        public TimeSpan? StartTime { get; set; }

        [Display(Name = "Working Hours, To: ")]
        public TimeSpan? EndTime { get; set; }

        #region Upload Restaurant Logo Image
        [Display(Name = "Restaurant Logo Image")]
        public IFormFile LogoFile { get; set; }
        public string LogoFileUrl { get; set; }
        public string LogoFileName { get; set; }
        public int LogoFileId { get; set; }
        #endregion

        public List<WorkingHoursViewModel> WorkingHoursViewModels { get; set; }
    }

    public class DaysOfWeekViewModel
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string Title { get; set; }
        public bool IsEnabled { get; set; }
    }

    public class WorkingHoursViewModel
    {
        public int Id { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Day { get; set; }
        public string Title { get; set; }
    }


    public class RestaurantViewModel
    {
        [Display(Name = "Restaurant Name")]
        public string Title { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Full Address")]
        public string FullAddress { get; set; }

        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Budget Level ")]
        public string RestaurantLevelEconomy { get; set; }

        [Display(Name = "Delivery Time Average")]
        public string DeliveryTime { get; set; }

        //[Display(Name = "Delivery Time Average, To: ")]
        //public TimeSpan? ToDeliveryTime { get; set; }

        [Display(Name = "Delivery Cost Average")]
        public decimal? RestaurantCourierCost { get; set; }

        [Display(Name = "Restaurant Types")]
        public string RestaurantTypes { get; set; }

        [Display(Name = "Restaurant Menus")]
        public List<RestaurantFoodTypeViewMdel> RestaurantFoodTypeViewMdels { get; set; }

        //public List<DaysOfWeekViewModel> DaysOfWeekViewModel { get; set; }

        [Display(Name = "Working Hours, From: ")]
        public TimeSpan? StartTime { get; set; }

        [Display(Name = "Working Hours, To: ")]
        public TimeSpan? EndTime { get; set; }
        
        public List<WorkingHoursViewModel> WorkingHoursViewModels { get; set; }

        public bool IsExist { get; set; }

    }

    public class RestaurantFoodTypeViewMdel
    {
        public string Price { get; set; }
        public string FoodName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}

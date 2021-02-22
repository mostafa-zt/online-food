using Microsoft.AspNetCore.Http;
using OnlineFood.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Web.Areas.Seller.ViewModels
{
    public class ProfileViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required", AllowEmptyStrings = false)]
        [EmailAddress(ErrorMessage = "{0} is not correct")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "{0} is required", AllowEmptyStrings = false)]
        public int? CityId { get; set; }

        [StringLength(250, ErrorMessage = "{0} should be maximum {1} characters")]
        [Display(Name = "Full Address")]
        [Required(ErrorMessage = "{0} is required", AllowEmptyStrings = false)]
        public string FullAddress { get; set; }

        //[StringLength(10, ErrorMessage = "{0} should be maximum {1} characters")]
        [Display(Name = "Postal Code")]
        [Required(ErrorMessage = "{0} is required", AllowEmptyStrings = false)]
        //[RegularExpression("^[0-9]$", ErrorMessage = "{0} should contain only number")]
        public string ZipCode { get; set; }

        //[StringLength(250, ErrorMessage = "{0} should be maximum {1} characters",MinimumLength =9)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "{0} is required", AllowEmptyStrings = false)]
        public string PhoneNumber { get; set; }

        [StringLength(25, ErrorMessage = "{0} should be maximum {1} characters")]
        [Required(ErrorMessage = "{0} is required", AllowEmptyStrings = false)]
        [Display(Name = "First Name")]
        public string Firstname { get; set; }

        [StringLength(25, ErrorMessage = "{0} should be maximum {1} characters")]
        [Required(ErrorMessage = "{0} is required", AllowEmptyStrings = false)]
        [Display(Name = "Last Name")]
        public string Lastname { get; set; }
        public string Fullname => string.Format("{0} {1}", Firstname, Lastname);
    }
}

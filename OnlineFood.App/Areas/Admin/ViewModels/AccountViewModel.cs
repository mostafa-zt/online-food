﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineFood.Web.Areas.Admin.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "{0} را وارد نمایید", AllowEmptyStrings = false)]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} را وارد نمایید", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [Display(Name = "مرا به خاطر بسپار؟")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }

    public class AccessDeniedViewModel
    {
        public string ReturnUrl { get; set; }
    }
}

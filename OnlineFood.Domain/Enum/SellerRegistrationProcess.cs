using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineFood.Domain.Enum
{
    /// <summary>
    /// روند ثبت رستوران
    /// </summary>
    public enum SellerRegistrationProcess
    {
        /// <summary>
        /// ثبت درخواست
        /// </summary>
        [Description("ثبت درخواست")]
        [Display(Name = "ثبت درخواست")]
        SubmitApplication = 1,

        /// <summary>
        /// تایید مدارک
        /// </summary>
        [Description("تایید مدارک")]
        [Display(Name = "تایید مدارک")]
        DocumentConfirmation = 2,

        /// <summary>
        /// ثبت رستوران
        /// </summary>
        [Description("ثبت رستوران")]
        [Display(Name = "ثبت رستوران")]
        RestaurantRegistration = 3,

        ///// <summary>
        ///// ثبت منوی رستوران
        ///// </summary>
        //[Description("ثبت منوی رستوران")]
        //[Display(Name = "ثبت منوی رستوران")]
        //MenuRegistration = 4,

        ///// <summary>
        ///// در حال سفارش گیری
        ///// </summary>
        //[Description("در حال سفارش گیری")]
        //[Display(Name = "در حال سفارش گیری")]
        //ReadyToOrder = 5
    }
}

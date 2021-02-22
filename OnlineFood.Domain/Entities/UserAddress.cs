using OnlineFood.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Entities
{
    public class UserAddress : BaseEntity
    {
        /// <summary>
        /// آدرس دقیق و کامل
        /// </summary>
        public string FullAdderss { get; set; }

        ///// <summary>
        ///// محدوده شهر
        ///// </summary>
        //public CityArea CityArea { get; set; }
        //public int CityAreaId { get; set; }

        /// <summary>
        /// عنوان آدرس
        /// </summary>
        public UserAddressTitle UserAddressTitle { get; set; }
        public int UserAddressTitleId { get; set; }

        /// <summary>
        /// شماره موبایل
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}

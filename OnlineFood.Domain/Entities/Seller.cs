using OnlineFood.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineFood.Domain.Entities
{
    /// <summary>
    /// فروشنده
    /// </summary>
    public class Seller : BaseEntity
    {
        #region Conatct Info
        public City City { get; set; }
        public int? CityId { get; set; }
        public string FullAddress { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        #endregion
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime? BirthDate { get; set; }
        public Gender? Gender { get; set; }
        public User User { get; set; }

    }
}

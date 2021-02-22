using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineFood.Domain.Entities
{
    /// <summary>
    /// کیف پول کاربر
    /// </summary>
    public class UserWallet
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// اعتبار کاربر
        /// </summary>
        public decimal Credit { get; set; }

        public User User { get; set; }
    }
}

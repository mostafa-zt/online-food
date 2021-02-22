using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Entities
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<RoleClaim> RoleClaims { get; set; }

        /// <summary>
        /// آیا نقش سیستمی هستند؟
        /// </summary>
        public bool IsSystemRole { get; set; }

        /// <summary>
        /// آیا حساب  کاربران این گروه کاربری مسدود شود؟
        /// </summary>
        public bool IsBanned { get; set; }

        /// <summary>
        /// نام نقش به فارسی
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        public DateTime? CreatorDateTime { get; set; }

    }
}

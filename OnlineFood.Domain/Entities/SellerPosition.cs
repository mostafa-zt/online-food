using OnlineFood.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Entities
{
    /// <summary>
    /// جایگاه کاری مدیر/کارمند  
    /// </summary>
    public class SellerPosition : BaseEntity
    {
        /// <summary>
        /// عنوان
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// فعال است؟
        /// </summary>
        public ActivityStatus ActivityStatus { get; set; }

        public ICollection<Seller> Sellers { get; set; }
    }
}

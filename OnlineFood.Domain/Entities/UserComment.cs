using OnlineFood.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Entities
{
    public class UserComment : BaseEntity
    {
        /// <summary>
        /// نظر
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// موضوع
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// غذاهای خریداری شده
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// وضعیت رضایتمندی
        /// امتیاز
        /// </summary>
        public SatisfactionStatus SatisfactionStatus { get; set; }

        /// <summary>
        /// منوی رستوران-غذا
        /// </summary>
        public Restaurant Restaurant { get; set; }
        public int RestaurantId { get; set; }

        public UserComment Parent { get; set; }
        public int? ParentId { get; set; }
        public ICollection<UserComment> Children { get; set; }
    }
}

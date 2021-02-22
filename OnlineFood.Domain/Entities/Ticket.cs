using OnlineFood.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Entities
{
    /// <summary>
    /// تیکت
    /// </summary>
    public class Ticket : BaseEntity
    {
        /// <summary>
        /// موضوع
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// متن
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// وضعیت اولویت تیکت
        /// </summary>
        public PriorityStatus PriorityStatus { get; set; }

        /// <summary>
        /// گیرنده تیکت
        /// </summary>
        public User ReceiverUserId { get; set; }

        /// <summary>
        /// ارسال کننده
        /// </summary>
        public Ticket Sender { get; set; }
        public int? SenderId { get; set; }

        /// <summary>
        /// دریافت کننده ها
        /// </summary>
        public ICollection<Ticket> Receivers { get; set; }
    }
}

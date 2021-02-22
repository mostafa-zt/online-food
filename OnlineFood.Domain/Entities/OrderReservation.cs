using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Entities
{
    public class OrderReservation 
    {
        public int Id { get; set; }

        /// <summary>
        /// روز تحویل
        /// </summary>
        public DateTime DeliveryDate { get; set; }

        /// <summary>
        /// ساعت تحویل
        /// </summary>
        public TimeSpan  DeliveryHour{ get; set; }

        /// <summary>
        /// سفارش
        /// </summary>
        public Order Order { get; set; }
    }
}

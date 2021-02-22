using OnlineFood.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Entities
{
    /// <summary>
    /// ساعات کاری رستوران در روزهای هفته
    /// </summary>
    public class RestaurantWorkingHour : BaseEntity
    {
        /// <summary>
        /// روز هفته
        /// </summary>
        public DaysOfWeek DaysOfWeek { get; set; }

        /// <summary>
        /// زمان شروع
        /// </summary>
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// زمان پایان
        /// </summary>
        public TimeSpan EndTime { get; set; }

        /// <summary>
        /// رستوران
        /// </summary>
        public Restaurant Restaurant { get; set; }
        public int RestaurantId { get; set; }
    }
}

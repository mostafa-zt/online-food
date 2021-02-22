using OnlineFood.Domain.Enum;
using System.Collections.Generic;

namespace OnlineFood.Domain.Entities
{
    /// <summary>
    /// عنوان آدرس /خوابگاه/محل کار/خانه
    /// </summary>
    public class UserAddressTitle : BaseEntity
    {
        /// <summary>
        /// عنوان
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// فعال است؟
        /// </summary>
        public ActivityStatus ActivityStatus { get; set; }

        public ICollection<UserAddress> UserAddresses { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Enum
{
    /// <summary>
    /// وضعیت سفارش
    /// </summary>
    public enum OrderLogistic
    {
        Waiting = 1,
        Processing = 2,
        Preparing = 3,
        Delivering = 4,
        Delivered = 5
    }
}

using OnlineFood.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Entities
{
    /// <summary>
    /// شهر
    /// </summary>
    public class City : BaseEntity
    {
        public string Title { get; set; }
        public ActivityStatus ActivityStatus { get; set; }
        //public ICollection<CityArea> CityAreas { get; set; }
    }
}

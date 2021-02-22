using OnlineFood.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Entities
{
    /// <summary>
    /// تصویر رستوران
    /// </summary>
    public class RestaurantImage : BaseEntity
    {
        /// <summary>
        /// مسیر عکس
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// پسوند عکس
        /// </summary>
        public string ImageExtension { get; set; }

        /// <summary>
        /// نام فایل عکس
        /// </summary>
        public string ImageFileName { get; set; }

        /// <summary>
        /// اندازه عکس
        /// </summary>
        public long ImageSize { get; set; }

        /// <summary>
        /// نوع عکس
        /// </summary>
        public ImageType ImageType { get; set; }

        /// <summary>
        /// رستوران
        /// </summary>
        public Restaurant Restaurant { get; set; }
        public int RestaurantId { get; set; }

        public virtual ICollection<RestaurantMenu> RestaurantMenus { get; set; }
    }
}

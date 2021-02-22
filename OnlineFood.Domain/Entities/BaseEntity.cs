using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineFood.Domain.Entities
{
    public abstract class BaseEntity : Entity
    {
        [Key]
        public virtual int Id { get; set; }
    }
    public abstract class Entity
    {
        /// <summary>
        /// حذف به صورت منطقی
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// تاریخ حذف به صورت منطقی
        /// </summary>
        public DateTime? DeletedDateTime { get; set; }
        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        public DateTime? CreatorDateTime { get; set; }
        /// <summary>
        /// کاربر ایجاد کننده
        /// </summary>
        public User CreatorUser { get; set; }
        /// <summary>
        /// آیدی کلید خارجی کاربر ایجاد کننده
        /// </summary>
        public int? CreatorUserId { get; set; }
        /// <summary>
        /// <summary>
        /// آی پی کاربر ایجاد کننده
        /// </summary>
        [MaxLength(20)]
        public string CreatorUserIpAddress { get; set; }
        /// <summary>
        /// تاریخ آخرین ویرایش
        /// </summary>
        public DateTime? ModifierDateTime { get; set; }
        /// <summary>
        /// کاربر ویرایش کننده
        /// </summary>
        public User ModifierUser { get; set; }
        /// <summary>
        ///  آیدی کلید خارجی کاربر ویرایش کننده 
        /// </summary>
        public int? ModifierUserId { get; set; }
        /// <summary>
        /// آی پی کاربر آخرین ویرایش
        /// </summary>
        [MaxLength(20)]
        public string ModifierUserIpAddress { get; set; }
    }
}

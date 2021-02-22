using OnlineFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Config
{
    public class OrderCartConfig : IEntityTypeConfiguration<OrderCart>
    {
        public void Configure(EntityTypeBuilder<OrderCart> builder)
        {
            builder.ToTable(name: "OrderCarts", schema: "Restaurant");

            builder.HasOne(x => x.RestaurantMenu).WithMany(x => x.OrderCarts).HasForeignKey(x => x.RestaurantMenuId);            
        }
    }
}

using OnlineFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Config
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable(name: "Orders", schema: "Restaurant");
            builder.Property(x => x.Description).HasMaxLength(250);
            builder.Property(x => x.UserAddress).HasMaxLength(250);
            builder.Property(x => x.UserAddressTitle).HasMaxLength(25);
            builder.Property(x => x.FinalPrice).HasColumnType("decimal(18, 2)");
            builder.Property(x => x.PackagingPrice).HasColumnType("decimal(18, 2)");
            builder.Property(x => x.TotalPrice).HasColumnType("decimal(18, 2)");
            builder.Property(x => x.ValueAddedPrice).HasColumnType("decimal(18, 2)");
            builder.Property(x => x.RestaurantCourierPrice).HasColumnType("decimal(18, 2)");

            builder.HasOne(x => x.Restaurant).WithMany(x => x.Orders).HasForeignKey(x => x.RestaurantId).OnDelete(DeleteBehavior.Restrict);

            //Configuring One To One Relationships
            builder.HasOne(a => a.OrderReservation).WithOne(b => b.Order).HasForeignKey<OrderReservation>(f => f.Id);

           
        }
    }
}

using OnlineFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Config
{
    public class OrderDetailConfig : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable(name: "OrderDetails", schema: "Restaurant");
            builder.Property(x => x.Title).IsRequired().HasMaxLength(25);
            builder.Property(x => x.Description).HasMaxLength(250);
            builder.Property(x => x.PriceDescription).HasMaxLength(250);

            builder.Property(x => x.TotalPrice).HasColumnType("decimal(18, 2)");
            builder.Property(x => x.Price).HasColumnType("decimal(18, 2)");

            builder.HasOne(x => x.Order).WithMany(x => x.OrderDetails).HasForeignKey(x => x.OrderId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

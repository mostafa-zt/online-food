using OnlineFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Config
{
    public class OrderReservationConfig: IEntityTypeConfiguration<OrderReservation>
    {
        public void Configure(EntityTypeBuilder<OrderReservation> builder)
        {
            builder.ToTable(name: "OrderReservations", schema: "Restaurant");
        }
    }
}

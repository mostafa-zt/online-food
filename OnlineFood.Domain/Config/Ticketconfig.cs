using OnlineFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Config
{
    public class TicketConfig : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable(name: "Tickets", schema: "Restaurant");
            builder.Property(x => x.Subject).IsRequired().HasMaxLength(25);
            builder.Property(x => x.Text).IsRequired().HasMaxLength(1000);

            builder.HasOne(x => x.Sender).WithMany(x => x.Receivers).HasForeignKey(x => x.SenderId);
        }
    }
}

using OnlineFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Config
{
    public class SellerPositionConfig : IEntityTypeConfiguration<SellerPosition>
    {
        public void Configure(EntityTypeBuilder<SellerPosition> builder)
        {
            builder.ToTable(name: "SellerPositions", schema: "Seller");
            builder.Property(x => x.Title).HasMaxLength(25);
        }
    }
}

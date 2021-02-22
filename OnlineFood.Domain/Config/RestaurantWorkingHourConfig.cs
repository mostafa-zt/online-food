using OnlineFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Config
{
    public class RestaurantWorkingHourConfig : IEntityTypeConfiguration<RestaurantWorkingHour>
    {
        public void Configure(EntityTypeBuilder<RestaurantWorkingHour> builder)
        {
            builder.ToTable(name: "RestaurantWorkingHours", schema: "Restaurant");

            builder.HasOne(x => x.Restaurant).WithMany(x => x.RestaurantWorkingHours).HasForeignKey(x => x.RestaurantId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

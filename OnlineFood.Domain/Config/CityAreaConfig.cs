using OnlineFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Config
{
    public class CityAreaConfig : IEntityTypeConfiguration<CityArea>
    {
        public void Configure(EntityTypeBuilder<CityArea> builder)
        {
            builder.ToTable(name: "CityAreas", schema: "Restaurant");
            builder.Property(x => x.Title).HasMaxLength(25);
            builder.Property(x => x.TitleEng).HasMaxLength(25);
            builder.Property(x => x.Longitude).HasMaxLength(50);
            builder.Property(x => x.Latitude).HasMaxLength(50);


            //Cascade - dependents should be deleted
            //Restrict - dependents are unaffected
            //SetNull - the foreign key values in dependent rows should update to NULL

            builder.HasOne(x => x.City).WithMany(x => x.CityAreas).HasForeignKey(x => x.CityId);
        }
    }
}
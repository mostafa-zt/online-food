using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Config
{
    public class CityConfig : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable(name: "Cities", schema: "Restaurant");
            builder.Property(x => x.Title).HasMaxLength(25);

            builder.HasData(new List<City>() {
                              { new City() {  ActivityStatus = Enum.ActivityStatus.Available, Id =1 , Title= "Vancouver" ,CreatorDateTime=DateTime.Now } },
                              { new City() {  ActivityStatus = Enum.ActivityStatus.Available, Id =2 , Title= "Toronto" , CreatorDateTime=DateTime.Now } },
                              { new City() {  ActivityStatus = Enum.ActivityStatus.Available, Id =3 , Title= "Montreal" ,CreatorDateTime=DateTime.Now } },
                              { new City() {  ActivityStatus = Enum.ActivityStatus.Available, Id =4 , Title= "Montreal" ,CreatorDateTime=DateTime.Now } },
                              { new City() {  ActivityStatus = Enum.ActivityStatus.Available, Id =5 , Title= "Calgary" ,CreatorDateTime=DateTime.Now } }
                           });
        }
    }
}

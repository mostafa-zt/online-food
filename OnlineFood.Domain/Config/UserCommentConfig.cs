using OnlineFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Domain.Config
{
    public class UserCommentConfig  : IEntityTypeConfiguration<UserComment>
    {
        public void Configure(EntityTypeBuilder<UserComment> builder)
        {
            builder.ToTable(name: "UserComments", schema: "User");
            builder.Property(x => x.Comment).HasMaxLength(1000);
            builder.Property(x => x.Subject).HasMaxLength(25);
            builder.Property(x => x.Tag).HasMaxLength(1000);

            builder.HasOne(x => x.Restaurant).WithMany(x => x.UserComments).HasForeignKey(x => x.RestaurantId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Parent).WithMany(x => x.Children).HasForeignKey(x => x.ParentId);
        }
    }
}

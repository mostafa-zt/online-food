using OnlineFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnlineFood.Domain.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(name: "Users", schema: "User");
            // Each User can have many UserClaims
            builder.HasMany(e => e.Claims)
                    .WithOne()
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

            // Each User can have many UserLogins
            builder.HasMany(e => e.Logins)
                    .WithOne()
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

            // Each User can have many UserTokens
            builder.HasMany(e => e.Tokens)
                    .WithOne()
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

            // Each User can have many entries in the UserRole join table
            builder.HasMany(e => e.UserRoles)
                    .WithOne()
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

            builder.Property(x => x.Firstname).HasMaxLength(25);
            builder.Property(x => x.Lastname).HasMaxLength(25);
            builder.Property(x => x.PhoneNumber).HasMaxLength(25);

            builder.HasOne(a => a.Seller).WithOne(b => b.User)
                  .HasForeignKey<Seller>(f => f.Id);
        }
    }
}

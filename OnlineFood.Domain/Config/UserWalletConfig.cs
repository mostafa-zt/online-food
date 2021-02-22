using OnlineFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnlineFood.Domain.Config
{
    public class UserWalletConfig : IEntityTypeConfiguration<UserWallet>
    {
        public void Configure(EntityTypeBuilder<UserWallet> builder)
        {
            builder.ToTable(name: "UserWallets", schema: "User");
            builder.Property(x => x.Credit).HasColumnType("decimal(18, 2)");
            ////Configuring One To One Relationships
            //builder.HasOne(a => a.User).WithOne(b => b.UserWallet)
            //       .HasForeignKey<UserWallet>(f => f.Id);
        }
    }
}

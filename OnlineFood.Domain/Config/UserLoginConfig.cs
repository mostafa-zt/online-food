using OnlineFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnlineFood.Domain.Config
{
    public class UserLoginConfig : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.ToTable(name: "UserLogins", schema: "User");

            builder.HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId });
        }
    }
}
